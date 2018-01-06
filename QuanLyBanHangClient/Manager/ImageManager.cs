using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager {
    class ImageManager {
        static private ImageManager _instance = null;
        static public ImageManager getInstance() {
            if (_instance == null) {
                _instance = new ImageManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/image";
        private string _imageLocalPath = "image/";
        private string _fullImagePath = "";

        private Queue<KeyValuePair<int, Action<byte[]>>> _queue;
        private bool _isDownloading = false;

        private Dictionary<int, byte[]> _imagesData = new Dictionary<int, byte[]>();
        private Dictionary<int, byte[]> ImagesData { get { return _imagesData; } }
        ImageManager() {
            _fullImagePath = AppDomain.CurrentDomain.BaseDirectory + _imageLocalPath;
            if (!Directory.Exists(_fullImagePath)) {
                Directory.CreateDirectory(_fullImagePath);
            }
        }
        private void setImageDataAndStoreLocal(int imageId, byte[] imageData) {
            _imagesData[imageId] = imageData;
            var image = UtilFuction.ByteToImage(imageData);
            image.Save(_fullImagePath + imageId.ToString(), ImageFormat.Jpeg);
        }
        private bool loadImageFromLocal(int imageId) {
            try {
                var image = Image.FromFile(_fullImagePath + imageId.ToString());
                _imagesData[imageId] = UtilFuction.ImageToBinary(image);
            } catch (Exception ex) {
                return false;
            }
            return true;
        }
        private void pushDownloadToQueue(int imageId, Action<byte[]> cb) {
            _queue.Enqueue(new KeyValuePair<int, Action<byte[]>>(imageId, cb));
            checkAndDownload();
        }
        private void checkAndDownload() {
            if(_isDownloading
                || _queue.Count <= 0) {
                return;
            }
            _isDownloading = true;
            var downloadData = _queue.Dequeue();
            
            Action<byte[]> cbSuccess = delegate (byte[] imageData) {
                setImageDataAndStoreLocal(downloadData.Key, imageData);
                downloadData.Value?.Invoke(imageData);
                _isDownloading = false;
                checkAndDownload();
            };
            Action<HttpStatusCode> cbFail = delegate (HttpStatusCode rsCode) {
                _isDownloading = false;
                checkAndDownload();
            };
            RequestManager.getInstance().LoadImage(
                API_CONTROLLER + "/" + downloadData.Key.ToString(),
                cbSuccess,
                cbFail
                );
        }
        public bool checkImageExistLocal(int imageId) {
            if(_imagesData.ContainsKey(imageId)
                || loadImageFromLocal(imageId)) {
                return true;
            }
            return false;
        }
        
        public bool loadImageFromLocal(int imageId, out byte[] imageData) {
            if(checkImageExistLocal(imageId)) {
                imageData = _imagesData[imageId];
                return true;
            }
            imageData = null;
            return false;
        }
        public async Task<bool> loadImage(
            int imageId,
            Action<byte[]> cb
            ) {
            //get from local
            if (checkImageExistLocal(imageId)) {
                cb?.Invoke(_imagesData[imageId]);
                return true;
            }
            pushDownloadToQueue(imageId, cb);
            return false;
        }
        public async Task uploadImage(
            byte[] imageData,
            Action<NetworkResponse> cbSuccess,
            Action<string> cbFail
            ) {
            RequestManager.getInstance().UploadImage(
                API_CONTROLLER,
                imageData,
                delegate (NetworkResponse result) {
                    if (result.Successful) {
                        int imageId = 0;
                        int.TryParse(result.Data.ToString(), out imageId);
                        setImageDataAndStoreLocal(imageId, imageData);
                    }
                    cbSuccess?.Invoke(result);
                },
                delegate (string err) {
                    cbFail?.Invoke(err);
                }
                );
        }
    }
}

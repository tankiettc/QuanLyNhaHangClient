using QuanLyBanHangClient.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class RestaurantInfoManager {
        static private RestaurantInfoManager _instance = null;
        static public RestaurantInfoManager getInstance() {
            if (_instance == null) {
                _instance = new RestaurantInfoManager();
            }
            return _instance;
        }
        RestaurantInfoManager() {
            try {
                _info = load();
            } catch (Exception ex) {
                _info = createNewRestaurnatInfoAndSave();
            }
        }
        private RestaurantInfo _info = null;
        public RestaurantInfo Info { get {return _info;}  }

        private void save(RestaurantInfo restaurantInfo) {
            var filePath = getFilePath();
            UtilFuction.WriteToBinaryFile<RestaurantInfo>(filePath, restaurantInfo);
        }
        private RestaurantInfo load() {
            var filePath = getFilePath();
            return UtilFuction.ReadFromBinaryFile<RestaurantInfo>(filePath);
        }
        private string getFilePath() {
            return Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "restaurantInfo.binary");
        }
        private RestaurantInfo createNewRestaurnatInfoAndSave() {
            var filePath = getFilePath();
            var rsi = new RestaurantInfo();
            rsi.Name = "Your Restaurant Name";
            rsi.Phone = "Your Number Phone";
            rsi.Address = "Your Address";
            rsi.Path = "";
            save(rsi);
            return rsi;
        }

        public void setName(string newName, bool isSave = true) {
            _info.Name = newName;
            if (isSave) {
                save(_info);
            }
        }
        public void setPhone(string newPhone, bool isSave = true) {
            _info.Phone = newPhone;
            if(isSave) {
                save(_info);
            }
        }
        public void setAddess(string newAddress, bool isSave = true) {
            _info.Address = newAddress;
            if (isSave) {
                save(_info);
            }
        }
        public void setPath(string newPath, bool isSave = true) {
            _info.Path = newPath;
            if (isSave) {
                save(_info);
            }
        }
    }
}

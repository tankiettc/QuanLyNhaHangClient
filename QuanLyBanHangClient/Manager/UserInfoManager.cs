using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    [Serializable]
    public class UserInfo {
        public String token { get; set; }
        public String type { get; set; }
        public long expiredTime { get; set; }
        public bool isKeepSignedIn { get; set; }
        public bool isRememberPass { get; set; }
        public string previousPass { get; set; }
        public string previousAcc { get; set; }
        public UserInfo() {

        }
    }
    class UserInfoManager {
        static private UserInfoManager _instance = null;
        static public UserInfoManager getInstance() {
            if (_instance == null) {
                _instance = new UserInfoManager();
            }
            return _instance;
        }
        UserInfo _userInfo;
        public UserInfo userInfo { get { return _userInfo; } }
        UserInfoManager() {
            try {
                _userInfo = load();
            } catch(Exception ex) {
                _userInfo = createNewUserAndSave();
            }
        }
        private UserInfo createNewUserAndSave() {
            var filePath = getFilePath();
            var userInfo = new UserInfo();
            userInfo.token = "";
            userInfo.type = "";
            userInfo.expiredTime = 0;
            userInfo.isKeepSignedIn = true;
            userInfo.isRememberPass = true;
            save(userInfo);
            return userInfo;
        }


        private void save(UserInfo userInfo) {
            var filePath = getFilePath();
            UtilFuction.WriteToBinaryFile<UserInfo>(filePath, userInfo);
        }
        private UserInfo load() {
            var filePath = getFilePath();
            return UtilFuction.ReadFromBinaryFile<UserInfo>(filePath);
        }
        private string getFilePath() {
            return Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "userData.binary");
        }
        public async Task getAccessTokenAsync(string userName, string password, Action<UserInfo> cbSuccess, Action<HttpStatusCode> cbFail) {
            Action<string> cbSuccessSent =
                    delegate (string result) {
                        UserInfo userInfoFromSV = JsonConvert.DeserializeObject<UserInfo>(result);
                        if (_userInfo != null) {
                            _userInfo.expiredTime = userInfoFromSV.expiredTime;
                            _userInfo.token = userInfoFromSV.token;
                            _userInfo.type = userInfoFromSV.type;
                            save(_userInfo);
                        }
                        cbSuccess?.Invoke(_userInfo);
                    };
            await RequestManager.getInstance().getAccessTokenAsync(
                userName,
                password,
                cbSuccessSent,
                cbFail
                );
        }
        public void resetLoginInfo() {
            if(_userInfo != null) {
                _userInfo.expiredTime = 0;
                _userInfo.token = "";
                _userInfo.type = "";
                save(_userInfo);
            }
        }
        public void setIsRememberPass(bool? isChecked, bool isSave = true) {
            if (_userInfo != null) {
                _userInfo.isRememberPass = (bool)isChecked;
                if (isSave) {
                    save(_userInfo);
                }
            }
        }

        public void setIsKeepSignedIn(bool? isChecked, bool isSave = true) {
            if (_userInfo != null) {
                _userInfo.isKeepSignedIn = (bool)isChecked;
                if (isSave) {
                    save(_userInfo);
                }
            }
        }
        public void setPreviousAcc(string previousAcc, bool isSave = true) {
            if (_userInfo != null) {
                _userInfo.previousAcc = previousAcc;
                if (isSave) {
                    save(_userInfo);
                }
            }
        }
        public void setPreviousPass(string previousPass, bool isSave = true) {
            if (_userInfo != null) {
                _userInfo.previousPass = previousPass;
                if(isSave) {
                    save(_userInfo);
                }
            }
        }
    }
}

using Newtonsoft.Json;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class UserManager {
        static private UserManager _instance = null;
        static public UserManager getInstance() {
            if (_instance == null) {
                _instance = new UserManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/user";
        private Dictionary<string, User> _userList;
        public Dictionary<string, User> UserList { get { return _userList; } }
        private UserManager() {
            _userList = new Dictionary<string, User>();
        }
        #region Network
        public async Task getAllUserFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _userList.Clear();
                    List<User> userListFromSV = JsonConvert.DeserializeObject<List<User>>(networkResponse.Data.ToString());
                    userListFromSV.ForEach(delegate (User user) {
                        _userList.Add(user.Username, user);
                    });
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task getUserByUsernameFromServerAndUpdate(
                    string  username,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    User userFromSV = JsonConvert.DeserializeObject<User>(networkResponse.Data.ToString());
                    _userList[username] = userFromSV;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER + "?username=" + username,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task createUserFromServerAndUpdate(
                    string userName,
                    string password,
                    int role = 1,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    User userCreated = JsonConvert.DeserializeObject<User>(networkResponse.Data.ToString());
                    _userList[userCreated.Username] = userCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Username", userName),
                new KeyValuePair<string, string>("Password", password)
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task changePasswordFromServerAndUpdate(
                    string userName,
                    string password,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    UserList[userName].password = password;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Username", userName),
                new KeyValuePair<string, string>("Password", password)
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER + "/changepassword",
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

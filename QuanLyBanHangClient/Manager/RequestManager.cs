using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyBanHangClient.Manager {
    class NetworkResponse {
        public bool Successful { get; set; }
        public JContainer Data { get; set; }
        public String ErrorDescription { get; set; }
        public String ErrorMessage { get; set; }
        public String ErrorCode { get; set; }
    }
    class RequestManager {
        static private RequestManager _instance = null;
        static public RequestManager getInstance() {
            if (_instance == null) {
                _instance = new RequestManager();
            }
            return _instance;
        }
        const string domainName = "http://quanlybanhangapi.azurewebsites.net";
        const string schemeAuthorization = "Bearer";
        const string tokenAuthorization = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYmYiOjE1MTIxNTMzNjQsImV4cCI6MTU0MzY4OTM2NCwiaWF0IjoxNTEyMTUzMzY0fQ.1Iya30pFQBMTaL65fbObUBNg0v9ZtnLia4IGX7W78ug";
        HttpClient client = null;

        private RequestManager() {
            client = new HttpClient();
            client.BaseAddress = new Uri(domainName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schemeAuthorization, tokenAuthorization);
        }

        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task postAsync(
                    string requestUri,
                    KeyValuePair<string, string> [] keysValue,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            try {
                var content = new FormUrlEncodedContent(keysValue);
                var result = await client.PostAsync(requestUri, content);
                
                if(result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    var jsonObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                    var networkResult = new NetworkResponse() {
                        Successful = jsonObject["Successful"].Value<bool>(),
                        Data = jsonObject["Data"] as JContainer,
                        ErrorDescription = jsonObject["ErrorDescription"].Value<string>(),
                        ErrorMessage = jsonObject["ErrorMessage"].Value<string>(),
                        ErrorCode = jsonObject["ErrorCode"].Value<string>()
                    };
                    cbSuccessSent?.Invoke(networkResult);
                } else {
                    cbError?.Invoke("err:" + result.StatusCode.ToString());
                }
            } catch(Exception ex) {
                cbError?.Invoke(ex.ToString());
                Debug.WriteLine("ex: " + ex);
            }
        }

        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task getAsync(
                    string requestUri,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
                    ) {
            try {
                var result = await client.GetAsync(requestUri);

                if (result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    var jsonObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                    var networkResult = new NetworkResponse() {
                        Successful = jsonObject["Successful"].Value<bool>(),
                        Data = jsonObject["Data"] as JContainer,
                        ErrorDescription = jsonObject["ErrorDescription"].Value<string>(),
                        ErrorMessage = jsonObject["ErrorMessage"].Value<string>(),
                        ErrorCode = jsonObject["ErrorCode"].Value<string>()
                    };
                    cbSuccessSent?.Invoke(networkResult);
                } else {
                    cbError?.Invoke("err:" + result.StatusCode.ToString());
                }
            } catch (Exception ex) {
                cbError?.Invoke(ex.ToString());
                Debug.WriteLine("ex: " + ex);
            }
        }


        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task putAsync(
                    string requestUri,
                    KeyValuePair<string, string>[] keysValue,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
                    ) {
            try {
                var content = new FormUrlEncodedContent(keysValue);
                var result = await client.PutAsync(requestUri, content);

                if (result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    var jsonObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                    var networkResult = new NetworkResponse() {
                        Successful = jsonObject["Successful"].Value<bool>(),
                        Data = jsonObject["Data"] as JContainer,
                        ErrorDescription = jsonObject["ErrorDescription"].Value<string>(),
                        ErrorMessage = jsonObject["ErrorMessage"].Value<string>(),
                        ErrorCode = jsonObject["ErrorCode"].Value<string>()
                    };
                    cbSuccessSent?.Invoke(networkResult);
                } else {
                    cbError?.Invoke("err:" + result.StatusCode.ToString());
                }
            } catch (Exception ex) {
                cbError?.Invoke(ex.ToString());
                Debug.WriteLine("ex: " + ex);
            }
        }

        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task deleteAsync(
                    string requestUri,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
                    ) {
            try {
                var result = await client.DeleteAsync(requestUri);

                if (result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    var jsonObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                    var networkResult = new NetworkResponse() {
                        Successful = jsonObject["Successful"].Value<bool>(),
                        Data = jsonObject["Data"] as JContainer,
                        ErrorDescription = jsonObject["ErrorDescription"].Value<string>(),
                        ErrorMessage = jsonObject["ErrorMessage"].Value<string>(),
                        ErrorCode = jsonObject["ErrorCode"].Value<string>()
                    };
                    cbSuccessSent?.Invoke(networkResult);
                } else {
                    cbError?.Invoke("err:" + result.StatusCode.ToString());
                }
            } catch (Exception ex) {
                cbError?.Invoke(ex.ToString());
                Debug.WriteLine("ex: " + ex);
            }
        }

        Grid _loadingAnim = null;
        public Grid LoadingAnm { set { _loadingAnim = value; } }
        public void showLoading() {
            if(_loadingAnim == null) {
                return;
            }
            _loadingAnim.Visibility = System.Windows.Visibility.Visible;
        }
        public void hideLoading() {
            if (_loadingAnim == null) {
                return;
            }
            _loadingAnim.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}

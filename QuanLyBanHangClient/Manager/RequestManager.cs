using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace QuanLyBanHangClient.Manager {
    class NetworkResponse {
        public bool Successful { get; set; }
        public JToken Data { get; set; }
        public String ErrorDescription { get; set; }
        public String ErrorMessage { get; set; }
        public String ErrorCode { get; set; }
    }
    enum RequestType {
        Post,
        Put,
        Delete,
        Get
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
        //const string schemeAuthorization = "Bearer";
        //const string tokenAuthorization = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYmYiOjE1MTIxNTMzNjQsImV4cCI6MTU0MzY4OTM2NCwiaWF0IjoxNTEyMTUzMzY0fQ.1Iya30pFQBMTaL65fbObUBNg0v9ZtnLia4IGX7W78ug";
        HttpClient client = null;

        private RequestManager() {
            client = new HttpClient();
            client.BaseAddress = new Uri(domainName);
        }
        private async Task request(
                    RequestType requestType,
                    string requestUri,
                    KeyValuePair<string, string>[] keysValue,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            try {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserInfoManager.getInstance().userInfo.type, UserInfoManager.getInstance().userInfo.token);

                var content = new FormUrlEncodedContent(keysValue);
                HttpResponseMessage result;
                switch (requestType) {
                    case RequestType.Post:
                        result = await client.PostAsync(requestUri, content);
                        break;
                    case RequestType.Put:
                        result =  await client.PutAsync(requestUri, content);
                        break;
                    case RequestType.Delete:
                        result =  await client.DeleteAsync(requestUri);
                        break;
                    default:
                        result =  await client.GetAsync(requestUri);
                        break;
                }
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
        private async Task requestJson(
                    RequestType requestType,
                    string requestUri,
                    object obj,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            try {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserInfoManager.getInstance().userInfo.type, UserInfoManager.getInstance().userInfo.token);

                var content = new StringContent(JsonConvert.SerializeObject(obj).ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                switch (requestType) {
                    case RequestType.Post:
                        result = await client.PostAsync(requestUri, content);
                        break;
                    case RequestType.Put:
                        result = await client.PutAsync(requestUri, content);
                        break;
                    case RequestType.Delete:
                        result = await client.DeleteAsync(requestUri);
                        break;
                    default:
                        result = await client.GetAsync(requestUri);
                        break;
                }
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
        public async Task postAsync(
                    string requestUri,
                    KeyValuePair<string, string> [] keysValue,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            await request(
                RequestType.Post,
                requestUri,
                keysValue,
                cbSuccessSent,
                cbError);
        }

        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task postAsyncJson(
                    string requestUri,
                    object obj,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            await requestJson(
                RequestType.Post,
                requestUri,
                obj,
                cbSuccessSent,
                cbError);
        }

        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task getAsync(
                    string requestUri,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
                    ) {
            await request(
                RequestType.Get,
                requestUri,
                new KeyValuePair<string, string>[0],
                cbSuccessSent,
                cbError);
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
                await request(
                    RequestType.Put,
                    requestUri,
                    keysValue,
                    cbSuccessSent,
                    cbError);
            }

        public async Task putAsyncJson(
                    string requestUri,
                    object obj,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            await requestJson(
                RequestType.Put,
                requestUri,
                obj,
                cbSuccessSent,
                cbError);
        }
        /**
         * requestUri: exanple: "/user/login"
         */
        public async Task deleteAsync(
                    string requestUri,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
                    ) {
            await request(
                RequestType.Delete,
                requestUri,
                new KeyValuePair<string, string>[0],
                cbSuccessSent,
                cbError);
        }
        public async Task getAccessTokenAsync(string userName, string password, Action<string> cbSuccess, Action<HttpStatusCode> cbFail) {
            try {
                KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Username", userName),
                new KeyValuePair<string, string>("Password", password)
                };
                var content = new FormUrlEncodedContent(keys);
                HttpResponseMessage result = await client.PostAsync("/api/token", content);

                if (result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    cbSuccess?.Invoke(resultContent);
                } else {
                    cbFail?.Invoke(result.StatusCode);
                }
            } catch (Exception ex) {
                cbFail?.Invoke(HttpStatusCode.NotFound);
            }
        }
        async public Task UploadImage(
            string requestUri, 
            byte[] ImageData,
            Action<NetworkResponse> cbSuccessSent = null,
            Action<string> cbError = null) {
            try {
                var requestContent = new MultipartFormDataContent();
                //    here you can specify boundary if you need---^
                var imageContent = new ByteArrayContent(ImageData);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, "image", "image.jpg");

                var result = await client.PostAsync(requestUri, requestContent);

                if (result.IsSuccessStatusCode) {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    var jsonObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                    var networkResult = new NetworkResponse() {
                        Successful = jsonObject["Successful"].Value<bool>(),
                        Data = JToken.FromObject(jsonObject["Data"].ToString()),
                        ErrorDescription = jsonObject["ErrorDescription"].Value<string>(),
                        ErrorMessage = jsonObject["ErrorMessage"].Value<string>(),
                        ErrorCode = jsonObject["ErrorCode"].Value<string>()
                    };
                    cbSuccessSent?.Invoke(networkResult);
                } else {
                    cbError?.Invoke("err:" + result.StatusCode.ToString());
                }
            }
            catch (Exception ex) {
                cbError?.Invoke(ex.ToString());
                Debug.WriteLine("ex: " + ex);
            }
        }
        public async Task LoadImage(string requestUri, Action<byte[]> cbSuccess, Action<HttpStatusCode> cbFail) {
            try {
                var result = await client.GetAsync(requestUri);

                if (result.IsSuccessStatusCode) {
                    byte[] img = await result.Content.ReadAsByteArrayAsync();
                    cbSuccess?.Invoke(img);
                } else {
                    cbFail?.Invoke(result.StatusCode);
                }
            } catch(Exception ex) {
                cbFail?.Invoke(HttpStatusCode.NotFound);
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

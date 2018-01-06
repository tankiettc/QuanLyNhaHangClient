using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.PrepareFoodTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class PrepareFoodManager
    {
        static private PrepareFoodManager _instance = null;
        static public PrepareFoodManager getInstance() {
            if(_instance == null) {
                _instance = new PrepareFoodManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/prepareFood";
        private Dictionary<int, PrepareFood> _prepareFoodList;
        public Dictionary<int, PrepareFood> PrepareFoodList { get { return _prepareFoodList; } }
        private PrepareFoodManager() {
            _prepareFoodList = new Dictionary<int, PrepareFood>();
        }
        #region Network

        public async Task getAllPrepareFoodFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _prepareFoodList.Clear();
                    List<PrepareFood> prepareFoodListFromSV = JsonConvert.DeserializeObject<List<PrepareFood>>(networkResponse.Data.ToString());
                    prepareFoodListFromSV.ForEach(delegate (PrepareFood prepareFood) {
                        _prepareFoodList.Add(prepareFood.PrepareFoodId, prepareFood);
                    });
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER + "/prepare",
                newCBSuccessSent,
                cbError
                );
        }
        public async Task setStatePrepareFoodAndUpdate(
                    int prepareFoodId,
                    PrepareFoodState newState,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                   PrepareFood prepareFood = JsonConvert.DeserializeObject<PrepareFood>(networkResponse.Data.ToString());
                    _prepareFoodList[prepareFood.PrepareFoodId] = prepareFood;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("prepareFoodId", prepareFoodId.ToString()),
                new KeyValuePair<string, string>("prepareStateId", ((int)newState).ToString())
            };
            await RequestManager.getInstance().postAsync(
                "/api/mobile/SetPrepareFoodTo",
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

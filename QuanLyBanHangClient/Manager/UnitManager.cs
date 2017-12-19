using Newtonsoft.Json;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class UnitManager
    {
        static private UnitManager _instance = null;
        static public UnitManager getInstance() {
            if(_instance == null) {
                _instance = new UnitManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/unit";
        private Dictionary<int, Unit> _unitList;
        public Dictionary<int, Unit> UnitList { get { return _unitList; } }
        private UnitManager() {
            _unitList = new Dictionary<int, Unit>();
        }
        #region Network

        public async Task getAllUnitFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _unitList.Clear();
                    List<Unit> unitListFromSV = JsonConvert.DeserializeObject<List<Unit>>(networkResponse.Data.ToString());
                    unitListFromSV.ForEach(delegate (Unit unit) {
                        _unitList.Add(unit.UnitId, unit);
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
        //public async Task getUnitByIdFromServerAndUpdate(
        //            int unitId,
        //            Action<NetworkResponse> cbSuccessSent = null,
        //            Action<string> cbError = null
        //    ) {
        //    Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
        //        if (networkResponse.Successful) {
        //            Unit unitFromSV = JsonConvert.DeserializeObject<Unit>(networkResponse.Data.ToString());
        //            _unitList[unitFromSV.UnitId] = unitFromSV;
        //        }
        //        cbSuccessSent?.Invoke(networkResponse);
        //    };
        //    await RequestManager.getInstance().getAsync(
        //        API_CONTROLLER + "/" + unitId,
        //        newCBSuccessSent,
        //        cbError
        //        );
        //}
        public async Task createUnitFromServerAndUpdate(
                    string name,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Unit unitCreated = JsonConvert.DeserializeObject<Unit>(networkResponse.Data.ToString());
                    _unitList[unitCreated.UnitId] = unitCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name)
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task updateUnitFromServerAndUpdate(
                    int unitId,
                    string name,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Unit unitCreated = JsonConvert.DeserializeObject<Unit>(networkResponse.Data.ToString());
                    _unitList[unitCreated.UnitId] = unitCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name)
            };
            await RequestManager.getInstance().putAsync(
                API_CONTROLLER + "/" + unitId,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task deleteUnitFromServerAndUpdate(
                    int unitId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                //debug
                //update tu sv
                if (networkResponse.Successful) {
                    _unitList.Remove(unitId);
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().deleteAsync(
                API_CONTROLLER + "/" + unitId,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

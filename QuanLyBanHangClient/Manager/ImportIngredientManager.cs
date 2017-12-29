using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using QuanLyBanHangAPI.model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class ImportBillManager
    {
        static private ImportBillManager _instance = null;
        static public ImportBillManager getInstance() {
            if(_instance == null) {
                _instance = new ImportBillManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/importingredient";
        private Dictionary<int, ImportBill> _importBillList = null;
        public Dictionary<int, ImportBill> ImportBillList { get { return _importBillList; } }
        private ImportBillManager() {
            _importBillList = new Dictionary<int, ImportBill>();
        }

        #region Network

        public async Task getAllImportBillFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _importBillList.Clear();
                    List<ImportBill> importBillListFromSV = JsonConvert.DeserializeObject<List<ImportBill>>(networkResponse.Data.ToString());
                    importBillListFromSV.ForEach(delegate (ImportBill importBill) {
                        _importBillList.Add(importBill.ImportBillId, importBill);
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
        public async Task getImportBillByIdFromServerAndUpdate(
                    int importBillId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    ImportBill importBillFromSV = JsonConvert.DeserializeObject<ImportBill>(networkResponse.Data.ToString());
                    _importBillList[importBillFromSV.ImportBillId] = importBillFromSV;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER + "/" + importBillId,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task createImportBillFromServerAndUpdate(
                    List<IngredientWithImportBill> listIngredientWithImportBill,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    ImportBill ingreddientCreated = JsonConvert.DeserializeObject<ImportBill>(networkResponse.Data.ToString());
                    _importBillList[ingreddientCreated.ImportBillId] = ingreddientCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            var myObject = (dynamic)new JArray();
            foreach (IngredientWithImportBill ingredientWithImportBill in listIngredientWithImportBill) {
                myObject.Add(JObject.Parse(JsonConvert.SerializeObject(ingredientWithImportBill)));
            }
            await RequestManager.getInstance().postAsyncJson(
                API_CONTROLLER,
                myObject,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task deleteImportBillFromServerAndUpdate(
                    int id,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _importBillList.Remove(id);
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().deleteAsync(
                API_CONTROLLER + "/" + id,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

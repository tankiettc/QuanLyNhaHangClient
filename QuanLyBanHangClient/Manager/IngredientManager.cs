using Newtonsoft.Json;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class IngredientManager
    {
        static private IngredientManager _instance = null;
        static public IngredientManager getInstance() {
            if(_instance == null) {
                _instance = new IngredientManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/ingredient";
        private Dictionary<int, Ingredient> _ingredientList = null;
        public Dictionary<int, Ingredient> IngredientList { get { return _ingredientList; } }
        private IngredientManager() {
            _ingredientList = new Dictionary<int, Ingredient>();
        }

        #region Network

        public async Task getAllIngredientFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _ingredientList.Clear();
                    List<Ingredient> ingredientListFromSV = JsonConvert.DeserializeObject<List<Ingredient>>(networkResponse.Data.ToString());
                    ingredientListFromSV.ForEach(delegate (Ingredient ingredient) {
                        _ingredientList.Add(ingredient.IngredientId, ingredient);
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
        public async Task getIngredientByIdFromServerAndUpdate(
                    int ingredientId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Ingredient ingredientFromSV = JsonConvert.DeserializeObject<Ingredient>(networkResponse.Data.ToString());
                    _ingredientList[ingredientFromSV.IngredientId] = ingredientFromSV;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER + "/" + ingredientId,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task createIngredientFromServerAndUpdate(
                    string name,
                    int unitId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Ingredient ingreddientCreated = JsonConvert.DeserializeObject<Ingredient>(networkResponse.Data.ToString());
                    _ingredientList[ingreddientCreated.IngredientId] = ingreddientCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name),
                new KeyValuePair<string, string>("UnitId", unitId.ToString())
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task updateIngredientFromServerAndUpdate(
                    int ingredientId,
                    string name,
                    int unitId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Ingredient ingreddientCreated = JsonConvert.DeserializeObject<Ingredient>(networkResponse.Data.ToString());
                    _ingredientList[ingreddientCreated.IngredientId] = ingreddientCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name),
                new KeyValuePair<string, string>("UnitId", unitId.ToString())
            };
            await RequestManager.getInstance().putAsync(
                API_CONTROLLER + "/" + ingredientId,
                keys,
                cbSuccessSent,
                cbError
                );
        }
        public async Task deleteIngredientFromServerAndUpdate(
                    int id,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _ingredientList.Remove(id);
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

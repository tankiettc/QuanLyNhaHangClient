using Newtonsoft.Json;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager {
    class FoodManager {
        static private FoodManager _instance = null;
        static public FoodManager getInstance() {
            if (_instance == null) {
                _instance = new FoodManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/food";
        private Dictionary<int, Food> _foodList;
        public Dictionary<int, Food> FoodList { get { return _foodList; } }
        private FoodManager() {
            _foodList = new Dictionary<int, Food>();
        }
        #region Network

        public async Task getAllFoodFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _foodList.Clear();
                    List<Food> foodListFromSV = JsonConvert.DeserializeObject<List<Food>>(networkResponse.Data.ToString());
                    foodListFromSV.ForEach(delegate (Food food) {
                        _foodList.Add(food.FoodId, food);
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
        public async Task getFoodByIdFromServerAndUpdate(
                    int foodId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Food foodFromSV = JsonConvert.DeserializeObject<Food>(networkResponse.Data.ToString());
                    _foodList[foodFromSV.FoodId] = foodFromSV;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER + "/" + foodId,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task createFoodFromServerAndUpdate(
                    string name,
                    List<IngredientWithFood> ingredientWithFoods,
                    decimal price,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Food foodCreated = JsonConvert.DeserializeObject<Food>(networkResponse.Data.ToString());
                    _foodList[foodCreated.FoodId] = foodCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name),
                new KeyValuePair<string, string>("IngredientWithFoods", JsonConvert.SerializeObject(ingredientWithFoods)),
                new KeyValuePair<string, string>("Price", price.ToString())
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task updateFoodFromServerAndUpdate(
                    int foodId,
                    string name,
                    List<IngredientWithFood> ingredientWithFoods,
                    decimal price,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Food foodCreated = JsonConvert.DeserializeObject<Food>(networkResponse.Data.ToString());
                    _foodList[foodCreated.FoodId] = foodCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("Name", name),
                new KeyValuePair<string, string>("IngredientWithFoods", JsonConvert.SerializeObject(ingredientWithFoods)),
                new KeyValuePair<string, string>("Price", price.ToString())
            };
            await RequestManager.getInstance().putAsync(
                API_CONTROLLER + "/" + foodId,
                keys,
                cbSuccessSent,
                cbError
                );
        }
        public async Task deleteFoodFromServerAndUpdate(
                    int id,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                //debug
                //update tu sv
                if (networkResponse.Successful) {
                    _foodList.Remove(id);
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

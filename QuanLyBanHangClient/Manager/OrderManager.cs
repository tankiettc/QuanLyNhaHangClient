using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class OrderManager
    {
        static private OrderManager _instance = null;
        static public OrderManager getInstance() {
            if(_instance == null) {
                _instance = new OrderManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/order";
        private Dictionary<int, Order> _orderList;
        public Dictionary<int, Order> OrderList { get { return _orderList; } }
        private OrderManager() {
            _orderList = new Dictionary<int, Order>();
        }
        #region Network

        public async Task getAllOrderFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _orderList.Clear();
                    List<Order> orderListFromSV = JsonConvert.DeserializeObject<List<Order>>(networkResponse.Data.ToString());
                    orderListFromSV.ForEach(delegate (Order order) {
                        if (order.FoodWithOrders == null) {
                            order.FoodWithOrders = new List<FoodWithOrder>();
                        }
                        _orderList.Add(order.OrderId, order);
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
        public async Task createOrderFromServerAndUpdate(
                    int tableId,
                    List<FoodWithOrder> listFoodWithOrder,
                    Action<NetworkResponse, int> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                int newOrderId = 0;
                if (networkResponse.Successful) {
                    Order orderCreated = JsonConvert.DeserializeObject<Order>(networkResponse.Data.ToString());
                    if(orderCreated.FoodWithOrders == null) {
                        orderCreated.FoodWithOrders = new List<FoodWithOrder>();
                    }
                    _orderList[orderCreated.OrderId] = orderCreated;
                    newOrderId = orderCreated.OrderId;
                }
                cbSuccessSent?.Invoke(networkResponse, newOrderId);
            };
            var myObject = (dynamic)new JObject();
            myObject.FoodWithOrder = (dynamic)new JArray();
            foreach (FoodWithOrder foodWithOrder in listFoodWithOrder) {
                myObject.FoodWithOrder.Add(JObject.Parse(JsonConvert.SerializeObject(foodWithOrder)));
            }
            await RequestManager.getInstance().postAsyncJson(
                API_CONTROLLER + "/new?tableId=" + tableId.ToString(),
                myObject,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task updateOrderWithFoodsFromServerAndUpdate(
                    int orderId,
                    List<FoodWithOrder> listFoodWithOrder,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Order orderCreated = JsonConvert.DeserializeObject<Order>(networkResponse.Data.ToString());
                    if (orderCreated.FoodWithOrders == null) {
                        orderCreated.FoodWithOrders = new List<FoodWithOrder>();
                    }
                    _orderList[orderCreated.OrderId] = orderCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };

            var myObject = (dynamic)new JObject();
            myObject.FoodWithOrder = (dynamic)new JArray();
            foreach (FoodWithOrder foodWithOrder in listFoodWithOrder) {
                myObject.FoodWithOrder.Add(JObject.Parse(JsonConvert.SerializeObject(foodWithOrder)));
            }
            await RequestManager.getInstance().putAsyncJson(
                API_CONTROLLER + "/updatefood/" + orderId,
                myObject,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task payOrderFromServerAndUpdate(
                    int orderId,
                    double moneyReceive,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Order orderCreated = JsonConvert.DeserializeObject<Order>(networkResponse.Data.ToString());
                    _orderList[orderCreated.OrderId] = orderCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("MoneyReceive", moneyReceive.ToString())
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER + "/pay/" + orderId,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task cacelOrderFromServerAndUpdate(
                    int orderId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _orderList.Remove(orderId);
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER + "/cancel/" + orderId,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

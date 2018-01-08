using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void exportBillAsPdf(string path, Order order) {
            if(order == null) {
                return;
            }
            try {
                FileStream fs = new FileStream(path + "\\Order" + order.OrderId.ToString() + ".pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                string orderStr = "Nhà hàng: " + RestaurantInfoManager.getInstance().Info.Name;
                orderStr += "\n" + "Số điện thoại: " + RestaurantInfoManager.getInstance().Info.Phone;
                orderStr += "\n" + "Địa chỉ: " + RestaurantInfoManager.getInstance().Info.Address;
                orderStr += "\n" + "Thời điểm: " + DateTime.Now.ToString("H:mm:ss  dd/MM/yyyy");

                orderStr += "\n\n" + "             Hóa đơn bán hàng ";
                foreach (FoodWithOrder foodWithOrder in order.FoodWithOrders) {
                    string strFoodName = foodWithOrder.Food.FoodId + "-" + foodWithOrder.Food.Name;
                    string strQuantity = foodWithOrder.Quantities.ToString();
                    string strFoodPrice = "x  " + UtilFuction.formatMoney(foodWithOrder.Food.Price);
                    orderStr += "\n"
                        + strFoodName + UtilFuction.getSpacesFromQuantityChar(50, strFoodName)
                        + strQuantity + UtilFuction.getSpacesFromQuantityChar(10, strQuantity)
                        + strFoodPrice + UtilFuction.getSpacesFromQuantityChar(10, strFoodPrice);
                }
                string totalStr = "TỔNG CỘNG: ";
                orderStr += "\n\n" + totalStr + UtilFuction.getSpacesFromQuantityChar(70, totalStr) + UtilFuction.formatMoney(order.BillMoney);

                orderStr += "\n\n\n Nếu quý khách có nhu cầu xuất hóa đơn, \n xin liên hệ với chúng tôi trong ngày.";

                iTextSharp.text.Font font = null;
                try {
                    var vini = BaseFont.CreateFont("c:/windows/fonts/aachenb.ttf", BaseFont.IDENTITY_H, true);
                    font = new iTextSharp.text.Font(vini, 14);
                } catch (Exception ex) {

                }
                if (font != null) {
                    document.Add(new Paragraph(18, orderStr, font));
                } else {
                    document.Add(new Paragraph(16, orderStr, font));
                }

                document.Close();
                fs.Close();
            } catch(Exception ex) {
            }
        }
    }
}

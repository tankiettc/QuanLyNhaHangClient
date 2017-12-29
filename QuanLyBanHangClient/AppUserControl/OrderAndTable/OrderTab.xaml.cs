using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.OrderTab.Models;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBanHangClient.AppUserControl.OrderTab
{
    /// <summary>
    /// Interaction logic for OrderTab.xaml
    /// </summary>
    public partial class OrderTab : UserControl
    {
        private decimal totalAllOrder = 0;
        public OrderAndTableTab orderAndTableTab { get; set; }
        public Table TableData { get; set; }
        public OrderTab()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+.");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void reloadAllUIOrderTab(Table tableData) {
            TableData = tableData;
            LVOrderInfo.Items.Clear();
            foreach (KeyValuePair<int, Order> entry in OrderManager.getInstance().OrderList) {
                if (entry.Value != null) {
                    if (entry.Value.TableId == tableData.TableId
                        && (entry.Value.BillMoney > entry.Value.MoneyReceive || entry.Value.BillMoney == 0)) {
                        LVOrderInfo.Items.Add(
                            new OrderInfo(entry.Value.OrderId, this)
                            );
                    }
                }
            }
            onChangeMoney();
        }
        public void onChangeMoney() {
            decimal totalMoney = 0;
            foreach (OrderInfo orderInfo in LVOrderInfo.Items.OfType<OrderInfo>()) {
                totalMoney += orderInfo.billMoney;
            }
            TextBlockTotalAllOrder.Text = "Thành tiền: " + Constant.formatMoney(totalMoney) + " VND";
            totalAllOrder = totalMoney;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            orderAndTableTab.reloadTableUI(false);
            orderAndTableTab.TableTabCustom.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e) {
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse, int> cbSuccessSent =
                    delegate (NetworkResponse networkResponse, int newOrderId) {
                        RequestManager.getInstance().hideLoading();
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            LVOrderInfo.Items.Add(new OrderInfo(newOrderId, this));
                        }
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            OrderManager.getInstance().createOrderFromServerAndUpdate(
                TableData.TableId,
                new List<FoodWithOrder>(),
                cbSuccessSent,
                cbError
                );
        }

        private void BtnRemoveOrder_Click(object sender, RoutedEventArgs e) {
            if(LVOrderInfo.SelectedIndex < 0) {
                return;
            }
            var orderInfoWillCancel = ((OrderInfo)LVOrderInfo.SelectedItem);
            var orderIdWillCancel = orderInfoWillCancel.OrderId;

            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        RequestManager.getInstance().hideLoading();
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            LVOrderInfo.Items.Remove(orderInfoWillCancel);
                            onChangeMoney();
                        }
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            OrderManager.getInstance().cacelOrderFromServerAndUpdate(
                orderIdWillCancel,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e) {
            foreach (OrderInfo orderInfo in LVOrderInfo.Items.OfType<OrderInfo>()) {
                if(orderInfo.BtnAccept.IsVisible) {
                    WindownsManager.getInstance().showMessageBoxConfirm("Vui lòng xác nhận mọi thay đổi trước khi thanh toán.");
                    return;
                }
            }

            var result = WindownsManager.getInstance().showMessageBoxYesNoCustom("Bạn có muốn thanh toán các order của bàn này?");
            if(result == MessageBoxResult.Yes) {
                Action pay = null;
                pay = delegate () {
                    if(LVOrderInfo.Items.Count > 0) {
                        onPayOneOrder((OrderInfo)LVOrderInfo.Items[LVOrderInfo.Items.Count - 1], pay);
                    }
                };
                pay();
            }
        }

        private void onPayOneOrder(OrderInfo orderInfo, Action cb) {
            var orderInfoWillPay = orderInfo;
            var orderIdWillPay = orderInfo.OrderId;
            var moneyWillPay = (double) OrderManager.getInstance().OrderList[orderIdWillPay].BillMoney;

            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        RequestManager.getInstance().hideLoading();
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            LVOrderInfo.Items.Remove(orderInfoWillPay);
                            onChangeMoney();
                        }
                        cb();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            OrderManager.getInstance().payOrderFromServerAndUpdate(
                orderIdWillPay,
                (double) OrderManager.getInstance().OrderList[orderIdWillPay].BillMoney,
                cbSuccessSent,
                cbError
                );
        }
    }
}

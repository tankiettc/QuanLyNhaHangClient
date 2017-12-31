using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.OrderTab.Models;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBanHangClient.AppUserControl.OrderAndTable
{
    /// <summary>
    /// Interaction logic for OrderHistoryTab.xaml
    /// </summary>
    public partial class OrderHistoryTab : UserControl
    {
        public OrderHistoryTab() {
            InitializeComponent();
        }

        public void reloadTable() {
            LVFilterTable.Items.Clear();
            foreach (KeyValuePair<int, Table> entry in TableManager.getInstance().TableList) {
                if (entry.Value != null) {
                    var checkBox = new CheckBox();
                    checkBox.Content = "Bàn số " + entry.Value.TableId.ToString();
                    checkBox.FontSize = 11;
                    checkBox.Foreground = new SolidColorBrush(Colors.White);
                    checkBox.Tag = entry.Value.TableId;
                    checkBox.IsChecked = true;
                    LVFilterTable.Items.Add(checkBox);
                }
            }
        }

        public void reloadListViewOrder(bool isBaseOnFilter) {
            List<int> listTableId = new List<int>();
            double timeFrom = 0;
            double timeTo = UtilFuction.GetTime(DateTime.Now);
            if(isBaseOnFilter) {
                foreach(CheckBox checkBox in LVFilterTable.Items.OfType<CheckBox>()) {
                    if(checkBox.IsChecked == true) {
                        listTableId.Add((int)checkBox.Tag);
                    }
                }
                if(listTableId.Count == LVFilterTable.Items.Count) {
                    listTableId.Clear();
                }

                if(CheckBoxFilterDate.IsChecked == true) {
                    if(DatePickerFrom.SelectedDate != null) {
                        timeFrom = UtilFuction.GetTime(DatePickerFrom.SelectedDate.Value) - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds;
                    }
                    if(DatePickerTo.SelectedDate != null) {
                        timeTo = UtilFuction.GetTime(DatePickerTo.SelectedDate.Value) + (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds);
                    }
                    if (timeFrom >= timeTo) {
                        timeFrom = 0;
                        timeTo = DateTime.Now.Millisecond;
                    }
                }
            }
            LVOrderInfo.Items.Clear();
            foreach(KeyValuePair<int, Order> entry in OrderManager.getInstance().OrderList) {
                if(entry.Value != null) {
                    if (listTableId.Count > 0
                        && !listTableId.Contains(entry.Value.TableId)) {
                        continue;
                    }
                    if(UtilFuction.GetTime(entry.Value.CreatedDate) < timeFrom
                        || UtilFuction.GetTime(entry.Value.CreatedDate) > timeTo) {
                        continue;
                    }
                    var orderInfo = new OrderInfo(entry.Value.OrderId, null, this);
                    LVOrderInfo.Items.Add(orderInfo);

                    orderInfo.ExpandOrderInfo.IsExpanded = false;
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            Visibility = Visibility.Hidden;
        }
        private void BtnFilterOrder_Click(object sender, RoutedEventArgs e) {
            reloadListViewOrder(true);
        }

        private void BtnRemoveOrder_Click(object sender, RoutedEventArgs e) {
            if (LVOrderInfo.SelectedIndex < 0) {
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
        private void CheckBoxFilterDate_Checked(object sender, RoutedEventArgs e) {
            FilterDateGroup.Visibility = Visibility.Visible;
        }

        private void CheckBoxFilterDate_Unchecked(object sender, RoutedEventArgs e) {
            FilterDateGroup.Visibility = Visibility.Hidden;
        }
        private void CheckBoxSelectTables_Checked(object sender, RoutedEventArgs e) {
            if(LVFilterTable == null) {
                return;
            }
            foreach (CheckBox checkBox in LVFilterTable.Items.OfType<CheckBox>()) {
                checkBox.IsChecked = true;
            }
        }

        private void CheckBoxSelectTables_Unchecked(object sender, RoutedEventArgs e) {
            if (LVFilterTable == null) {
                return;
            }
            foreach (CheckBox checkBox in LVFilterTable.Items.OfType<CheckBox>()) {
                checkBox.IsChecked = false;
            }
        }
    }
}

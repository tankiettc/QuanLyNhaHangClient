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
using System.Windows.Documents;
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
        public OrderHistoryTab()
        {
            InitializeComponent();
        }

        public void reloadListViewOrder(bool isBaseOnFilter) {
            List<int> listTableId = new List<int>();
            double timeFrom = 0;
            double timeTo = Constant.GetTime(DateTime.Now);
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
                    timeFrom = Constant.GetTime(DatePickerFrom.SelectedDate.Value) - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds;
                    timeTo = Constant.GetTime(DatePickerFrom.SelectedDate.Value)  + (TimeSpan.TicksPerDay * TimeSpan.TicksPerMillisecond - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds);
                    if(timeFrom >= timeTo) {
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
                    if(entry.Value.CreatedDate.Millisecond < timeFrom
                        || entry.Value.CreatedDate.Millisecond > timeTo) {
                        continue;
                    }
                    LVOrderInfo.Items.Add(new OrderInfo(entry.Value.OrderId, null, this));
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            this.Visibility = Visibility.Hidden;
        }
        private void BtnFilterOrder_Click(object sender, RoutedEventArgs e) {

        }

        private void BtnRemoveOrder_Click(object sender, RoutedEventArgs e) {

        }
        private void CheckBoxFilterDate_Checked(object sender, RoutedEventArgs e) {

        }

        private void CheckBoxFilterDate_Unchecked(object sender, RoutedEventArgs e) {

        }
        private void CheckBoxSelectTables_Checked(object sender, RoutedEventArgs e) {

        }

        private void CheckBoxSelectTables_Unchecked(object sender, RoutedEventArgs e) {

        }
    }
}

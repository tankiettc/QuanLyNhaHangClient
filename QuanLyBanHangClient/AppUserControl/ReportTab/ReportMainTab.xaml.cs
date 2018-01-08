using QuanLyBanHangAPI.model;
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

namespace QuanLyBanHangClient.AppUserControl.ReportTab
{
    /// <summary>
    /// Interaction logic for ReportMainTab.xaml
    /// </summary>
    public partial class ReportMainTab : UserControl
    {
        enum ChartTime {
            ChartByHourse,
            ChartByDate,
            ChartByMonth,
            ChartByYear
        }
        public ReportMainTab()
        {
            InitializeComponent();
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            MyValue.Add(new KeyValuePair<string, int>("Administration", 20));
            MyValue.Add(new KeyValuePair<string, int>("Management", 36));
            MyValue.Add(new KeyValuePair<string, int>("Development", 89));
            MyValue.Add(new KeyValuePair<string, int>("Support", 270));
            MyValue.Add(new KeyValuePair<string, int>("Sales", 140));
            LineChart1.DataContext = MyValue;
        }
        private void calculateAndReloadChart() {
            var rp = ReportManager.getInstance().ReportModel;
            var orders = ReportManager.getInstance().ReportModel.payOrders.ToList();
            Dictionary<string, int> valuesByTimes = new Dictionary<string, int>();
            ChartTime chartTime = ChartTime.ChartByHourse;
            if (rp.startDate == rp.endDate) { //gio
                chartTime = ChartTime.ChartByHourse;
                for(int i = 0; i < 24; i++) {
                    valuesByTimes[i.ToString()] = 0;
                }
            } else if ((rp.endDate - rp.startDate).TotalDays >= 735) { // nam
                chartTime = ChartTime.ChartByYear;
                for (int i = rp.startDate.Year; i <= rp.endDate.Year; i++) {
                    valuesByTimes[i.ToString()] = 0;
                }
            } else if((rp.endDate - rp.startDate).TotalDays >= 24)  { // thang
                chartTime = ChartTime.ChartByMonth;
                for (int i = rp.startDate.Year; i <= rp.endDate.Year; i++) {
                    for(int j = 1; j <= 12; j++) {
                        var month = new DateTime(i, j, 1);
                        var monthTs = UtilFuction.GetTime(month);
                        var startTs = UtilFuction.GetTime(rp.startDate) - TimeSpan.TicksPerDay * 31 * 10000;
                        var endTs = UtilFuction.GetTime(rp.endDate);
                        if (startTs <= monthTs && endTs >= monthTs) {
                            valuesByTimes[j.ToString("D2") + '/' + i.ToString()] = 0;
                        }
                    }
                }
            } else { //ngay
                chartTime = ChartTime.ChartByDate;
                for (int i = rp.startDate.Year; i <= rp.endDate.Year; i++) {
                    for (int j = 1; j <= 12; j++) {
                        var totalDay = DateTime.DaysInMonth(i, j);
                        for(int z = 1; z <= totalDay; z++) {
                            var day = new DateTime(i, j, z);
                            var dayTs = UtilFuction.GetTime(day);
                            var startTs = UtilFuction.GetTime(rp.startDate);
                            var endTs = UtilFuction.GetTime(rp.endDate);
                            if (startTs <= dayTs && endTs >= dayTs) {
                                valuesByTimes[z.ToString("D2") + '/' + j.ToString("D2")] = 0;
                            }
                        }
                    }
                }
            }
            while(orders.Count > 0) {
                var frontOrder = orders[0];
                switch (chartTime) {
                    case ChartTime.ChartByHourse: {
                            valuesByTimes[frontOrder.CreatedDate.TimeOfDay.Hours.ToString()] += 1;
                            break;
                        }
                    case ChartTime.ChartByDate: {
                            valuesByTimes[frontOrder.CreatedDate.ToString("dd/MM")] += 1;
                            break;
                        }
                    case ChartTime.ChartByMonth: {
                            valuesByTimes[frontOrder.CreatedDate.ToString("MM/yyyy")] += 1;
                            break;
                        }
                    case ChartTime.ChartByYear: {
                            valuesByTimes[frontOrder.CreatedDate.ToString("yyyy")] += 1;
                            break;
                        }
                }
                
                orders.RemoveAt(0);
            }
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, int> entry in valuesByTimes) {
                MyValue.Add(new KeyValuePair<string, int>(entry.Key, entry.Value));
            }

            //List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            //MyValue.Add(new KeyValuePair<string, int>("Administration", 20));
            //MyValue.Add(new KeyValuePair<string, int>("Management", 36));
            //MyValue.Add(new KeyValuePair<string, int>("Development", 89));
            //MyValue.Add(new KeyValuePair<string, int>("Support", 270));
            //MyValue.Add(new KeyValuePair<string, int>("Sales", 140));
            LineChart1.DataContext = MyValue;

        }
        public void reloadUI() {
            if(ComboxBoxDate.SelectedIndex < 0) {
                return;
            }
            string format = "MM/dd/yyyy";
            string startDate = DateTime.Now.ToString(format);
            string endDate = DateTime.Now.ToString(format);
            if (ComboxBoxDate.SelectedIndex == 2) {
                if (DatePickerFrom.SelectedDate != null) {
                    startDate = DatePickerFrom.SelectedDate.Value.ToString(format);
                } else {
                    startDate = "1/1/2017";
                }
                if (DatePickerTo.SelectedDate != null) {
                    endDate = DatePickerTo.SelectedDate.Value.ToString(format);
                } 
            } else if(ComboxBoxDate.SelectedIndex == 1) {
                startDate = "1/1/2017";
            }
            RequestManager.getInstance().showLoading();
            ReportManager.getInstance().getAllReportModelFromServerAndUpdate(
                startDate,
                endDate,
                delegate (NetworkResponse rs) {
                    var rp = ReportManager.getInstance().ReportModel;
                    TextBlockTotalFood.Text = rp.totalFood.ToString();
                    TextBlockTotalFoodSold.Text = rp.totalFoodSold.ToString();
                    TextBlockTotalMoneyReceive.Text = rp.totalMoneyReceive.ToString();
                    TextBlockEstimatedProfit.Text = rp.estimatedProfit.ToString();
                    TextBlockEstimatedTotalFoodMoneyUse.Text = rp.estimatedTotalFoodMoneyUse.ToString();
                    TextBlocktotalOrder.Text = rp.totalOrder.ToString();
                    TextBlockTotalPayOrder.Text = rp.totalPayOrder.ToString();
                    TextBlockTotalProcessingFoodCancel.Text = rp.totalProcessingFoodCancel.ToString();
                    TextBlockTotalQueueFoodCancel.Text = rp.totalQueueFoodCancel.ToString();
                    TextBlockTotalProfitLossByCancelProcessingFood.Text = rp.totalProfitLossByCancelProcessingFood.ToString();
                    calculateAndReloadChart();
                    RequestManager.getInstance().hideLoading();
                },
                delegate (string err) {
                    RequestManager.getInstance().hideLoading();
                }
                );
        }

        private void ComboxBoxDate_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnFilterOrder.IsEnabled = false;
            if(FilterDateGroup != null) {
                FilterDateGroup.Visibility = Visibility.Hidden;
            }
            if (ComboxBoxDate.SelectedIndex < 0) {
            } else if(ComboxBoxDate.SelectedIndex == 0
                || ComboxBoxDate.SelectedIndex == 1) {
            } else {
                BtnFilterOrder.IsEnabled = true;
                FilterDateGroup.Visibility = Visibility.Visible;
            }
        }

        private void BtnFilterOrder_Click(object sender, RoutedEventArgs e) {
            reloadUI();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) {
            reloadUI();
        }
    }
}

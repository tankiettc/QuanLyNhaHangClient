using QuanLyBanHangAPI.model.SQL;
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

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportHistoryTab {
    /// <summary>
    /// Interaction logic for ImportHistoryTab.xaml
    /// </summary>
    public partial class ImportHistoryTab : UserControl
    {
        public ImportIngredientTab ParentTab { get; set; }
        public ImportHistoryTab()
        {
            InitializeComponent();
        }
        public void reloadListViewBill(bool isBaseOnFilter)
        {
            double timeFrom = 0;
            double timeTo = UtilFuction.GetTime(DateTime.Now);
            if (isBaseOnFilter)
            {
                if (CheckBoxFilterDate.IsChecked == true)
                {
                    if (DatePickerFrom.SelectedDate != null)
                    {
                        timeFrom = UtilFuction.GetTime(DatePickerFrom.SelectedDate.Value) - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds;
                    }
                    if (DatePickerTo.SelectedDate != null)
                    {
                        timeTo = UtilFuction.GetTime(DatePickerTo.SelectedDate.Value) + (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond - DatePickerFrom.SelectedDate.Value.TimeOfDay.TotalMilliseconds);
                    }
                    if (timeFrom >= timeTo)
                    {
                        timeFrom = 0;
                        timeTo = DateTime.Now.Millisecond;
                    }
                }
            }
            LVBill.Items.Clear();
            foreach (KeyValuePair<int, ImportBill> entry in ImportBillManager.getInstance().ImportBillList)
            {
                if (entry.Value != null)
                {
                    if (UtilFuction.GetTime(entry.Value.CreatedDate) < timeFrom
                        || UtilFuction.GetTime(entry.Value.CreatedDate) > timeTo)
                    {
                        continue;
                    }
                    var billCell = new ImportHistoryCell(entry.Value);
                    LVBill.Items.Add(billCell);
                }
            }
        }
        public void getFromServerAndReloadUI(Action cb = null)
        {
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful)
                        {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        }
                        else
                        {
                            reloadListViewBill(true);
                        }
                        cb?.Invoke();
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            ImportBillManager.getInstance().getAllImportBillFromServerAndUpdate(
                cbSuccessSent,
                cbError
                );
        }
        private void Bill_DoubleClick(object sender, MouseButtonEventArgs e) {
            if(LVBill.SelectedIndex < 0) {
                return;
            }
            ParentTab.reloadDetailTab(((ImportHistoryCell)LVBill.SelectedItem).importBill);
            ParentTab.goToDetailTab();
        }
        private void BtnDetailBill_Click(object sender, RoutedEventArgs e) {
            if (LVBill.SelectedIndex < 0) {
                return;
            }
            ParentTab.reloadDetailTab(((ImportHistoryCell)LVBill.SelectedItem).importBill);
            ParentTab.goToDetailTab();
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            getFromServerAndReloadUI();
        }

        private void BtnRemoveBill_Click(object sender, RoutedEventArgs e)
        {
            if (LVBill.SelectedIndex < 0) {
                return;
            }
            var selectedItem = ((ImportHistoryCell)LVBill.SelectedItem);
            var billIdSelected = selectedItem.importBill.ImportBillId;
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            LVBill.Items.Remove(selectedItem);
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            ImportBillManager.getInstance().deleteImportBillFromServerAndUpdate(
                billIdSelected,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnFilterBill_Click(object sender, RoutedEventArgs e)
        {
            reloadListViewBill(true);
        }

        private void CheckBoxFilterDate_Checked(object sender, RoutedEventArgs e)
        {
            FilterDateGroup.Visibility = Visibility.Visible ;
        }

        private void CheckBoxFilterDate_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterDateGroup.Visibility = Visibility.Hidden ;
        }

        private void LVBill_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnDetailBill.IsEnabled = LVBill.SelectedIndex >= 0;
        }
    }
}

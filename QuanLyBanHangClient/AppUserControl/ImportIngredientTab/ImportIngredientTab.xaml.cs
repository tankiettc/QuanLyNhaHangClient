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

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab {
    /// <summary>
    /// Interaction logic for ImportIngredientTab.xaml
    /// </summary>
    public partial class ImportIngredientTab : UserControl {
        public ImportIngredientTab() {
            InitializeComponent();
            ((ImportTab.ImportTab)(TabImportIngredient.Content)).ParentTab = this;
            ((ImportHistoryTab.ImportHistoryTab)(TabHistory.Content)).ParentTab = this;
            ((ImportDetailTab.ImportDetailTab)(TabDetail.Content)).ParentTab = this;
        }
        public void reloadDetailTab(ImportBill importBill) {
            ((ImportDetailTab.ImportDetailTab)(TabDetail.Content)).reloadUI(importBill);
        }
        public void goToDetailTab() {
            TabControlMain.SelectedItem = TabDetail;
        }

        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (TabHistory.IsSelected
                && ImportBillManager.getInstance().ImportBillList.Count <= 0) {
                ((ImportHistoryTab.ImportHistoryTab)(TabHistory.Content)).getFromServerAndReloadUI();
            }
        }
    }
}

using QuanLyBanHangAPI.model.SQL;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportTab;
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

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportDetailTab
{
    /// <summary>
    /// Interaction logic for ImportDetailTab.xaml
    /// </summary>
    public partial class ImportDetailTab : UserControl {
        public ImportIngredientTab ParentTab { get; set; }
        ImportBill _importBill;
        ImportBill importBill { get {return _importBill; } }
        public ImportDetailTab()
        {
            InitializeComponent();
        }
        public void reloadUI(ImportBill importBillRoot)
        {
            _importBill = importBillRoot;
            TextBoxId.Text = _importBill.ImportBillId.ToString();
            TextBoxTime.Text = _importBill.CreatedDate.ToString();
            TextBoxTotal.Text = Constant.formatMoney(_importBill.BillMoney);
            LVIngredient.Items.Clear();
            foreach(IngredientWithImportBill ingredient in _importBill.IngredientWithImportBills) {
                LVIngredient.Items.Add(new ImportIngredientCell(ingredient));
            }
        }
    }
}

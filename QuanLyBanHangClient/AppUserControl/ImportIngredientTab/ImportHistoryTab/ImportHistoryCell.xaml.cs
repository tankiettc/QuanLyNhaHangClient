using QuanLyBanHangAPI.model.SQL;
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

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportHistoryTab
{
    /// <summary>
    /// Interaction logic for ImportHistoryCell.xaml
    /// </summary>
    public partial class ImportHistoryCell : UserControl
    {
        public ImportBill importBill { get; set; }
        public ImportHistoryCell(ImportBill _importBill)
        {
            InitializeComponent();
            importBill = _importBill;
            reloadUI();
        }
        public void reloadUI()
        {
            TextBlockBillId.Text = "Hóa đơn #" + importBill.ImportBillId.ToString();
            TextBlockTime.Text = importBill.CreatedDate.ToShortDateString();
            TextBlockMoney.Text = UtilFuction.formatMoney(importBill.BillMoney);
        }
    }
}

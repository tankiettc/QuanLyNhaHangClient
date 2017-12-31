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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportTab
{
    /// <summary>
    /// Interaction logic for ImportIngredientCell.xaml
    /// </summary>
    public partial class ImportIngredientCell : UserControl
    {
        public IngredientWithImportBill _ingredientWithImportBill { get; }
        ImportTab _importTab;

        public ImportIngredientCell(IngredientWithImportBill ingredientWithImportBill, ImportTab importTab = null)
        {
            InitializeComponent();
            _ingredientWithImportBill = ingredientWithImportBill;
            _importTab = importTab;
            reloadUI();
        }

        public void reloadUI() {
            if(_ingredientWithImportBill == null) {
                TextBlockIngredient.Text = "Error";
                TextBlockPrice.Text = "Error";
                TextBlockQuantity.Text = "Error";
                TextBlockTotal.Text = "Error";
                return;
            }
            TextBlockIngredient.Text = _ingredientWithImportBill.Ingredient.Name;
            TextBlockPrice.Text = UtilFuction.formatMoney(_ingredientWithImportBill.SinglePricePerUnit)
                + " / "  
                +  UnitManager.getInstance().UnitList[_ingredientWithImportBill.Ingredient.UnitId].Name;
            TextBlockQuantity.Text = _ingredientWithImportBill.Quantities.ToString();
            TextBlockTotal.Text = UtilFuction.formatMoney((decimal)_ingredientWithImportBill.Quantities * _ingredientWithImportBill.SinglePricePerUnit); 
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {
            if(_importTab == null) {
                return;
            }
            BtnRemove.Visibility = Visibility.Visible;
            BtnRemove.IsEnabled = false;
            Storyboard sb = (Storyboard) Application.Current.FindResource("FadeAnim");
            sb.Begin(BtnRemove);

            enableBtnRemove();
        }
        private async void enableBtnRemove() {
            await Task.Delay(500);
            BtnRemove.IsEnabled = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            if (_importTab == null) {
                return;
            }
            if (BtnRemove.Visibility == Visibility.Hidden) {
                return;
            }
            BtnRemove.Visibility = Visibility.Hidden;
            BtnRemove.Opacity = 0;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e) {
            if(_importTab != null) {
                _importTab.removeItemLV(this);
            }
        }
    }
}

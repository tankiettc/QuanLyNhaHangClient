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

        public ImportIngredientCell(IngredientWithImportBill ingredientWithImportBill)
        {
            InitializeComponent();
            _ingredientWithImportBill = ingredientWithImportBill;
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
            TextBlockPrice.Text = Constant.formatMoney(_ingredientWithImportBill.SinglePricePerUnit)
                + " / "  
                +  UnitManager.getInstance().UnitList[_ingredientWithImportBill.Ingredient.UnitId].Name;
            TextBlockQuantity.Text = _ingredientWithImportBill.Quantities.ToString();
            TextBlockTotal.Text = Constant.formatMoney((decimal)_ingredientWithImportBill.Quantities * _ingredientWithImportBill.SinglePricePerUnit); 
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {
            BtnRemove.Visibility = Visibility.Visible;
            Storyboard sb = (Storyboard) Application.Current.FindResource("FadeAnim");
            sb.Begin(BtnRemove);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            if(BtnRemove.Visibility == Visibility.Hidden) {
                return;
            }
            BtnRemove.Visibility = Visibility.Hidden;
            BtnRemove.Opacity = 0;
        }
    }
}

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

namespace QuanLyBanHangClient.AppUserControl.FoodTab
{
    /// <summary>
    /// Interaction logic for FoodCell.xaml
    /// </summary>
    public partial class FoodCell : UserControl
    {
        public Food FoodData { get; set; }
        FoodTab _foodTab;
        public FoodCell()
        {
            InitializeComponent();
        }
        public FoodCell(
            Food food,
            FoodTab foodTab
            ) {
            InitializeComponent();
            FoodData = food;
            _foodTab = foodTab;
            setupUI();
        }

        private void setupUI() {
            if(FoodData == null) {
                return;
            }
            TextBlockName.Text = FoodData.FoodId + " - " + FoodData.Name;
            TextBlockPrice.Text = UtilFuction.formatMoney(FoodData.Price) + " vnđ";
            TextBlockCategory.Text = FoodCategorizeManager.getInstance().FoodCategorizeList[FoodData.FoodCategorizeId].Name;
        }

        public void setImageFood(System.Drawing.Image foodImage) {
            if(foodImage != null) {
                ImageFood.Source = UtilFuction.imageToBitmapSource(foodImage);
            } else {
                ImageFood.Source = (ImageSource) Application.Current.FindResource("ImageDefaultFood");
            }
        }


        private void TextBlockName_MouseUp(object sender, MouseButtonEventArgs e) {

            if (_foodTab != null
                && FoodData != null) {
                _foodTab.showEditFoodView(FoodData.FoodId);
            }
        }
    }
}

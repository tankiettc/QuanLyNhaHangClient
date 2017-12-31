using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace QuanLyBanHangClient.AppUserControl.OrderTab.Models
{
    /// <summary>
    /// Interaction logic for OrderWithFood.xaml
    /// </summary>
    public partial class OrderWithFood : UserControl {
        public int previousQuantity = 0;
        public OrderWithFood(
            FoodWithOrder foodWithOrder,
            OrderInfo _orderInfo) {
            InitializeComponent();
            _foodWithOrder = foodWithOrder;
            orderInfo = _orderInfo;
            setupOrderWithFoodUI();
        }
        public FoodWithOrder _foodWithOrder { get; set; }
        public OrderInfo orderInfo { get; set; }

        public void setupOrderWithFoodUI() {
            if (_foodWithOrder != null) {
                textBlockName.Text = _foodWithOrder.Food.Name;
                TextBoxQuantity.Text = _foodWithOrder.Quantities.ToString();
                textBlockTotal.Text = UtilFuction.formatMoney(_foodWithOrder.Quantities * _foodWithOrder.Food.Price);
                previousQuantity = _foodWithOrder.Quantities;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            int newQuantity = 0;
            if(!int.TryParse(TextBoxQuantity.Text, out newQuantity)) {
                return;
            }
            orderInfo.onEditOrderWithFood(this);
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e) {
            orderInfo.onRemoveOrderWithFood(this);
        }

        private void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e) {
            if(_foodWithOrder == null) {
                return;
            }
            int newQuantity = 0;
            if (!int.TryParse(TextBoxQuantity.Text, out newQuantity)) {
                BtnEdit.IsEnabled = false;
                return;
            }
            textBlockTotal.Text = UtilFuction.formatMoney (newQuantity * _foodWithOrder.Food.Price);
            BtnEdit.IsEnabled = true;
            orderInfo.BtnAccept.Visibility = Visibility.Visible;
            orderInfo.onChangeMoney();
        }
    }
}

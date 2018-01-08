using QuanLyBanHangAPI.model;
using QuanLyBanHangAPI.model.SQL;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
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

namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportTab
{
    /// <summary>
    /// Interaction logic for ImportTab.xaml
    /// </summary>
    public partial class ImportTab : UserControl {
        public ImportIngredientTab ParentTab { get; set; }
        public ImportTab()
        {
            InitializeComponent();
            reloadTotalBill();
        }
        public void setupComboBoxIngredient() {
            if(ComboBoxIngredient.Items.Count > 0) {
                return;
            }
            ComboBoxIngredient.Items.Clear();

            var ingredientsName = new List<ComboBoxItem>();
            foreach (KeyValuePair<int, Ingredient> entry in IngredientManager.getInstance().IngredientList) {
                if (entry.Value != null) {
                    var item = new ComboBoxItem();
                    item.Tag = entry.Key;
                    item.Content = entry.Value.Name;
                    item.Style = (Style)Application.Current.FindResource("ComboBoxItemRoundedStyle");

                    ingredientsName.Add(item);
                }
            }
            ComboBoxIngredient.ItemsSource = ingredientsName;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+.");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ComboBoxIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(ComboBoxIngredient.SelectedIndex < 0) {
                return;
            }
            var unitId = IngredientManager.getInstance().IngredientList[(int)((ComboBoxItem)ComboBoxIngredient.SelectedItem).Tag].UnitId;
            TextBlockUnit.Text = " / 1 " + UnitManager.getInstance().UnitList[unitId].Name;
        }
        private void checkAndReloadTotal() {
            decimal price = 0;
            decimal quantity = 0;
            if (!decimal.TryParse(TextBoxPrice.Text, out price)
                || !decimal.TryParse(TextBoxQuantity.Text, out quantity)) {
                TextBoxTotal.Text = "0";
                return;
            }
            TextBoxTotal.Text = UtilFuction.formatMoney(price * quantity);
        }
        private void reloadTotalBill() {
            decimal totalBill = 0;
            var ingredientWithImportBillList = new List<IngredientWithImportBill>();
            foreach (ImportIngredientCell importIngredientCell in LVIngredient.Items.OfType<ImportIngredientCell>()) {
                ingredientWithImportBillList.Add(importIngredientCell._ingredientWithImportBill);
                totalBill += (importIngredientCell._ingredientWithImportBill.SinglePricePerUnit * (decimal)importIngredientCell._ingredientWithImportBill.Quantities);
            }
            TextBlockTotal.Text = "Tổng cộng: " + UtilFuction.formatMoney(totalBill) + " VND";
        }
        private void TextBoxPrice_TextChanged(object sender, TextChangedEventArgs e) {
            checkAndReloadTotal();
        }

        private void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e) {
            checkAndReloadTotal();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            if (ComboBoxIngredient.SelectedIndex < 0
                || TextBoxTotal.Text == "0"
                || string.IsNullOrEmpty(TextBoxTotal.Text)) {
                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                return;
            }
            decimal quantity = 0;
            decimal.TryParse(TextBoxQuantity.Text, out quantity);

            decimal price = 0;
            decimal.TryParse(TextBoxPrice.Text, out price);

            var ingredientId = (int)((ComboBoxItem)ComboBoxIngredient.SelectedItem).Tag;

            var importIngredientWithBill = new IngredientWithImportBill();
            importIngredientWithBill.IngredientId = ingredientId;
            importIngredientWithBill.Ingredient = IngredientManager.getInstance().IngredientList[ingredientId];
            importIngredientWithBill.Quantities = (float)quantity;
            importIngredientWithBill.SinglePricePerUnit = price;

            LVIngredient.Items.Add(new ImportIngredientCell(importIngredientWithBill, this));
            reloadTotalBill();
        }
        private void BtnImport_Click(object sender, RoutedEventArgs e) {
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            LVIngredient.Items.Clear();
                            reloadTotalBill();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            var ingredientWithImportBillList = new List<IngredientWithImportBill>();
            foreach (ImportIngredientCell importIngredientCell in LVIngredient.Items.OfType<ImportIngredientCell>()) {
                ingredientWithImportBillList.Add(new IngredientWithImportBill()
                {
                    IngredientId = importIngredientCell._ingredientWithImportBill.Ingredient.IngredientId,
                    SinglePricePerUnit = importIngredientCell._ingredientWithImportBill.SinglePricePerUnit,
                    Quantities = importIngredientCell._ingredientWithImportBill.Quantities
                });
            }
            ImportBillManager.getInstance().createImportBillFromServerAndUpdate(
                ingredientWithImportBillList,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) {
            LVIngredient.Items.Clear();
            reloadTotalBill();
        }

        public void removeItemLV(ImportIngredientCell importIngredientCell) {
            LVIngredient.Items.Remove(importIngredientCell);
            reloadTotalBill();
        }
    }
}

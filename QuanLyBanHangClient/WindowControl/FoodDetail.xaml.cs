using QuanLyBanHangAPI.model;
using QuanLyBanHangAPI.model.SQL;
using QuanLyBanHangClient.AppUserControl.FoodTab;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace QuanLyBanHangClient.WindowControl {
    /// <summary>
    /// Interaction logic for FoodDetail.xaml
    /// </summary>
    public partial class FoodDetail : Window {
        private int _foodDetailId = Constant.ID_CREATE_NEW;
        private FoodTab _foodTab = null;
        private ObservableCollection<IngredientWithFoodTable> _ingredientsWithFood = new ObservableCollection<IngredientWithFoodTable>();
        public FoodDetail(FoodTab foodTab, int foodId = Constant.ID_CREATE_NEW) {
            InitializeComponent();
            _foodDetailId = foodId;
            _foodTab = foodTab;
            this.DataGridIngredient.ItemsSource = _ingredientsWithFood;
            setupUI();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+.");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void setupUI() {
            var foodId = _foodDetailId;

            var categorizeNames = new List<ComboData>();
            foreach (KeyValuePair<int, FoodCategorize> entry in FoodCategorizeManager.getInstance().FoodCategorizeList) {
                if (entry.Value != null) {
                    categorizeNames.Add(new ComboData() { Id = entry.Key, Value = entry.Value.Name });
                }
            }
            ComboBoxCategory.ItemsSource = categorizeNames;
            ComboBoxCategory.DisplayMemberPath = "Value";
            ComboBoxCategory.SelectedValuePath = "Id";

            var ingredientsName = new List<ComboData>();
            foreach (KeyValuePair<int, Ingredient> entry in IngredientManager.getInstance().IngredientList) {
                if (entry.Value != null) {
                    ingredientsName.Add(new ComboData() { Id = entry.Key, Value = entry.Value.Name });
                }
            }
            ComboBoxIngredient.ItemsSource = ingredientsName;
            ComboBoxIngredient.DisplayMemberPath = "Value";
            ComboBoxIngredient.SelectedValuePath = "Id";

            if (foodId != Constant.ID_CREATE_NEW) {
                var foodData = FoodManager.getInstance().FoodList[foodId];
                setupUIWithFoodData(foodData);

                BtnConfirm.Content = "Sửa";

                TextBlockId.Visibility = Visibility.Visible;
                TextBoxId.Visibility = Visibility.Visible;
                BtnCopy.Visibility = Visibility.Collapsed;
                ComboBoxFoodCopy.Visibility = Visibility.Collapsed;
            } else {
                var foodNames = new List<ComboData>();
                foreach (KeyValuePair<int, Food> entry in FoodManager.getInstance().FoodList) {
                    if (entry.Value != null) {
                        foodNames.Add(new ComboData() { Id = entry.Key, Value = entry.Value.Name });
                    }
                }
                ComboBoxFoodCopy.ItemsSource = foodNames;
                ComboBoxFoodCopy.DisplayMemberPath = "Value";
                ComboBoxFoodCopy.SelectedValuePath = "Id";
            }
        }
        private void setupUIWithFoodData(Food foodData) {
            TextBoxId.Text = foodData.FoodId.ToString();
            TextBoxName.Text = foodData.Name;
            TextBoxPrice.Text = foodData.Price.ToString();

            ComboBoxCategory.SelectedValue = foodData.FoodCategorizeId;

            foreach (IngredientWithFood ingredientWithFood in foodData.IngredientWithFoods) {
                _ingredientsWithFood.Add(new IngredientWithFoodTable() {
                    Id = ingredientWithFood.IngredientId,
                    Name = IngredientManager.getInstance().IngredientList[ingredientWithFood.IngredientId].Name,
                    UnitName = UnitManager.getInstance().UnitList[IngredientManager.getInstance().IngredientList[ingredientWithFood.IngredientId].UnitId].Name,
                    Quantity = ingredientWithFood.Quantities
                });
            }

        }
        private List<IngredientWithFood> getIngredientWithFoodListFromTable() {
            var result = new List<IngredientWithFood>();

            foreach(IngredientWithFoodTable item in DataGridIngredient.Items) {
                result.Add(new IngredientWithFood() {
                    IngredientId = item.Id,
                    Quantities = (float) Convert.ToDouble(item.Quantity)
                });
            }
            return result;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if(((ComboData)ComboBoxCategory.SelectedItem) == null
                || String.IsNullOrEmpty(TextBoxName.Text)
                || String.IsNullOrEmpty(TextBoxPrice.Text)) {
                WindownsManager.getInstance().showMessageBoxCheckInfoAgain();
                return;
            }
            loadingAnim.Visibility = Visibility.Visible;
            Action< NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            if (_foodTab != null) {
                                _foodTab.reloadFoodTableUI();
                            }
                            this.Close();
                        }
                        loadingAnim.Visibility = Visibility.Hidden;
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        loadingAnim.Visibility = Visibility.Hidden;
                    };

            var ingredientsWithFood = getIngredientWithFoodListFromTable();
            decimal price = 0;
            Decimal.TryParse(TextBoxPrice.Text, out price);
            try {
                if (_foodDetailId != Constant.ID_CREATE_NEW) {
                    FoodManager.getInstance().updateFoodFromServerAndUpdate(
                        _foodDetailId,
                        TextBoxName.Text,
                        ingredientsWithFood,
                        price,
                        Convert.ToInt64(((ComboData)ComboBoxCategory.SelectedItem).Id),
                        cbSuccessSent,
                        cbError
                        );
                } else {
                    FoodManager.getInstance().createFoodFromServerAndUpdate(
                        TextBoxName.Text,
                        ingredientsWithFood,
                        price,
                        Convert.ToInt64(((ComboData)ComboBoxCategory.SelectedItem).Id),
                        cbSuccessSent,
                        cbError
                        );
                }
            } catch(Exception ex) {

            }
        }

        private void DataGridIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnRemove.IsEnabled = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            float quantity = 0;
            if(!float.TryParse(TextBoxQuantity.Text, out quantity)
                || ComboBoxIngredient.SelectedIndex < 0) {
                WindownsManager.getInstance().showMessageBoxCheckInfoAgain();
                return;
            }
            var id = ((ComboData)ComboBoxIngredient.SelectedItem).Id;
            var name = IngredientManager.getInstance().IngredientList[id].Name;
            var unitName = UnitManager.getInstance().UnitList[IngredientManager.getInstance().IngredientList[id].UnitId].Name;

            foreach (IngredientWithFoodTable ingredientWithFoodTable in DataGridIngredient.Items) {
                if (ingredientWithFoodTable.Id == id) {
                    WindownsManager.getInstance().showMessageBoxCheckInfoAgain();
                    return;
                }
            }

            _ingredientsWithFood.Add(new IngredientWithFoodTable() {
                Id = id,
                Name = name,
                UnitName = unitName,
                Quantity = quantity
            });
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e) {
            if(DataGridIngredient.SelectedIndex < 0) {
                return;
            }
            _ingredientsWithFood.Remove((IngredientWithFoodTable)DataGridIngredient.SelectedItem);
        }

        private void ComboBoxIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(ComboBoxIngredient.SelectedIndex >= 0) {
                TextBlockUnit.Text = UnitManager.getInstance().UnitList[IngredientManager.getInstance().IngredientList[((ComboData)ComboBoxIngredient.SelectedItem).Id].UnitId].Name;
            }
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e) {
            if(ComboBoxFoodCopy.SelectedIndex < 0) {
                return;
            }
            var foodData = FoodManager.getInstance().FoodList[((ComboData)ComboBoxFoodCopy.SelectedItem).Id];
            setupUIWithFoodData(foodData);
        }
    }

    class IngredientWithFoodTable : INotifyPropertyChanged {
        private int id;
        public int Id {
            get { return this.id; }
            set {
                if (this.id != value) {
                    this.id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }
        private string name;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        private float quantity;
        public float Quantity {
            get { return this.quantity; }
            set {
                if (this.quantity != value) {
                    this.quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }
        private string unitName;
        public string UnitName {
            get { return this.unitName; }
            set {
                if (this.unitName != value) {
                    this.unitName = value;
                    this.NotifyPropertyChanged("UnitName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

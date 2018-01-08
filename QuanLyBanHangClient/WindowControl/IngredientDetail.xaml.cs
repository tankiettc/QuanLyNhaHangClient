using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.IngredientTab;
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
using System.Windows.Shapes;

namespace QuanLyBanHangClient.WindowControl {
    /// <summary>
    /// Interaction logic for IngredientDetail.xaml
    /// </summary>
    public partial class IngredientDetail : Window {
        private int _ingredientDetailId = Constant.ID_CREATE_NEW;
        private IngredientTab _ingredientTab = null;
        public IngredientDetail(IngredientTab ingredientTab, int ingredientId = Constant.ID_CREATE_NEW) {
            InitializeComponent();
            _ingredientDetailId = ingredientId;
            _ingredientTab = ingredientTab;
            setupUI();
        }
        private void setupUI() {
            var ingredientId = _ingredientDetailId;

            var unitNames = new List<ComboData>();
            foreach (KeyValuePair<int, Unit> entry in UnitManager.getInstance().UnitList) {
                if (entry.Value != null) {
                    unitNames.Add(new ComboData() { Id = entry.Key, Value = entry.Value.Name });
                }
            }
            ComboBoxUnit.ItemsSource = unitNames;
            ComboBoxUnit.DisplayMemberPath = "Value";
            ComboBoxUnit.SelectedValuePath = "Id";



            if (ingredientId != Constant.ID_CREATE_NEW) {
                var ingredientData = IngredientManager.getInstance().IngredientList[ingredientId];
                TextBoxId.Text = ingredientData.IngredientId.ToString();
                TextBoxName.Text = ingredientData.Name;

                ComboBoxUnit.SelectedValue = ingredientData.UnitId;
                Title = "Chi tiết nguyên liệu";
                TextBlockNameWindow.Text = "Chi tiết nguyên liệu";
                BtnConfirm.Content = "Sửa";
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if(((ComboData)ComboBoxUnit.SelectedItem) == null
                || String.IsNullOrEmpty(TextBoxName.Text)) {
                WindownsManager.getInstance().showMessageBoxCheckInfoAgain();
                return;
            }
            loadingAnim.Visibility = Visibility.Visible;
            Action< NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            if (_ingredientTab != null) {
                                _ingredientTab.reloadIngredientTableUI();
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

            if (_ingredientDetailId != Constant.ID_CREATE_NEW) {
                IngredientManager.getInstance().updateIngredientFromServerAndUpdate(
                    _ingredientDetailId,
                    TextBoxName.Text,
                    ((ComboData)ComboBoxUnit.SelectedItem).Id, 
                    cbSuccessSent,
                    cbError
                    );
            } else {
                IngredientManager.getInstance().createIngredientFromServerAndUpdate(
                    TextBoxName.Text,
                    ((ComboData)ComboBoxUnit.SelectedItem).Id,
                    cbSuccessSent,
                    cbError
                    );
            }
        }
    }
}

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
    public partial class UnitDetail : Window {
        private int _unitDetailId = Constant.ID_CREATE_NEW;
        private IngredientTab _ingredientTab = null;
        public UnitDetail(IngredientTab ingredientTab, int unitId = Constant.ID_CREATE_NEW) {
            InitializeComponent();
            _unitDetailId = unitId;
            _ingredientTab = ingredientTab;
            setupUI();
        }
        private void setupUI() {
            var unitId = _unitDetailId;

            if (unitId != Constant.ID_CREATE_NEW) {
                var unitData = UnitManager.getInstance().UnitList[unitId];
                TextBoxId.Text = unitData.UnitId.ToString();
                TextBoxName.Text = unitData.Name;

                BtnConfirm.Content = "Sửa";
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if(String.IsNullOrEmpty(TextBoxName.Text)) {
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
                                _ingredientTab.reloadUnitTableUI();
                                _ingredientTab.reloadIngredientTableUI(true);
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

            if (_unitDetailId != Constant.ID_CREATE_NEW) {
                UnitManager.getInstance().updateUnitFromServerAndUpdate(
                    _unitDetailId,
                    TextBoxName.Text,
                    cbSuccessSent,
                    cbError
                    );
            } else {
                UnitManager.getInstance().createUnitFromServerAndUpdate(
                    TextBoxName.Text,
                    cbSuccessSent,
                    cbError
                    );
            }
        }

    }
}

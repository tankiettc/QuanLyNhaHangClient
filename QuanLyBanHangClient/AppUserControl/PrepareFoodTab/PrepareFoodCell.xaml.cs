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

namespace QuanLyBanHangClient.AppUserControl.PrepareFoodTab {
    /// <summary>
    /// Interaction logic for PrepareFoodCell.xaml
    /// </summary>
    public enum PrepareFoodState {
        Cancel = -1,
        Queue = 0,
        Cooking = 1,
        Cooked = 2,
        Served = 3
    }
    public partial class PrepareFoodCell : UserControl {
        PrepareFood _prepareFood;
        bool _isReloading = false;
        public PrepareFoodCell() {
            InitializeComponent();
        }
        public PrepareFoodCell(PrepareFood prepareFood) {
            InitializeComponent();
            _prepareFood = prepareFood;
            resetUI();
        }
        public void resetUI() {
            if(_prepareFood == null) {
                return;
            }
            _isReloading = true;
            var foodData = FoodManager.getInstance().FoodList[_prepareFood.FoodId];
            TextBlockName.Text = foodData.Name;
            TextBlockTableId.Text = _prepareFood.TableId.ToString();
            if(_prepareFood.PrepareStateId >= 0) {
                ComboBoxState.IsEnabled = true;
                ComboBoxState.SelectedItem = ComboBoxState.Items[_prepareFood.PrepareStateId];
                for(int i = 0; i < ComboBoxState.Items.Count - 1; i++) {
                    if(_prepareFood.PrepareStateId > i) {
                        ((ComboBoxItem)ComboBoxState.Items[i]).Visibility = Visibility.Collapsed;
                    } else {
                        ((ComboBoxItem)ComboBoxState.Items[i]).Visibility = Visibility.Visible;
                    }
                }
            } else {
                ComboBoxState.SelectedItem = ComboBoxState.Items[4];
                ComboBoxState.IsEnabled = false;
            }
            //ComboBoxState.IsDropDownOpen = true;
            _isReloading = false;
        }

        private void ComboBoxState_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(_prepareFood == null ||
            _isReloading == true) {
                return;
            }
            var prepareFoodState = (PrepareFoodState)ComboBoxState.SelectedIndex;
            RequestManager.getInstance().showLoading();
            PrepareFoodManager.getInstance().setStatePrepareFoodAndUpdate(
                _prepareFood.PrepareFoodId,
                prepareFoodState,
                delegate (NetworkResponse rs) {
                    if (rs.Successful) {
                        _prepareFood = PrepareFoodManager.getInstance().PrepareFoodList[_prepareFood.PrepareFoodId];
                        resetUI();
                    } else {
                        WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                    }
                    RequestManager.getInstance().hideLoading();
                },
                delegate (string err) {
                    WindownsManager.getInstance().showMessageBoxErrorNetwork();
                    RequestManager.getInstance().hideLoading();
                }
                );
        }
    }
}

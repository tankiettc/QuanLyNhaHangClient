using QuanLyBanHangClient.AppUserControl.FoodTab;
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

namespace QuanLyBanHangClient.WindowControl
{
    /// <summary>
    /// Interaction logic for CategorizeDetail.xaml
    /// </summary>
    public partial class CategorizeDetail : Window {
        private int _foodWithCategorizeDetailId = Constant.ID_CREATE_NEW;
        private FoodTab _foodTab = null;
        public CategorizeDetail(FoodTab foodTab, int foodWithCategorizeId = Constant.ID_CREATE_NEW) {
            InitializeComponent();
            _foodWithCategorizeDetailId = foodWithCategorizeId;
            _foodTab = foodTab;
            setupUI();
        }
        private void setupUI() {
            var foodWithCategorizeId = _foodWithCategorizeDetailId;

            if (foodWithCategorizeId != Constant.ID_CREATE_NEW) {
                var foodWithCategorizeData = FoodCategorizeManager.getInstance().FoodCategorizeList[foodWithCategorizeId];
                TextBoxId.Text = foodWithCategorizeData.FoodCategorizeId.ToString();
                TextBoxName.Text = foodWithCategorizeData.Name;

                this.Title = "Chi tiết loại món";
                TextBlockNameWindow.Text = "Chi tiết loại món";
                BtnConfirm.Content = "Sửa";
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if (String.IsNullOrEmpty(TextBoxName.Text)) {
                WindownsManager.getInstance().showMessageBoxCheckInfoAgain();
                return;
            }
            loadingAnim.Visibility = Visibility.Visible;
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            if (_foodTab != null) {
                                _foodTab.reloadCategoryTableUI();
                                _foodTab.reloadFoodTableUI(true);
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

            if (_foodWithCategorizeDetailId != Constant.ID_CREATE_NEW) {
                FoodCategorizeManager.getInstance().updateFoodCategorizeFromServerAndUpdate(
                    _foodWithCategorizeDetailId,
                    TextBoxName.Text,
                    cbSuccessSent,
                    cbError
                    );
            } else {
                FoodCategorizeManager.getInstance().createFoodCategorizeFromServerAndUpdate(
                    TextBoxName.Text,
                    cbSuccessSent,
                    cbError
                    );
            }
        }

    }
}

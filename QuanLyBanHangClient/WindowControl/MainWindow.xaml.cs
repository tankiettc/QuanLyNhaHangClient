using Newtonsoft.Json.Linq;
using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.FoodTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportTab;
using QuanLyBanHangClient.AppUserControl.IngredientTab;
using QuanLyBanHangClient.AppUserControl.OrderTab;
using QuanLyBanHangClient.AppUserControl.PrepareFoodTab;
using QuanLyBanHangClient.AppUserControl.ReportTab;
using QuanLyBanHangClient.Manager;
using QuanLyBanHangClient.WindowControl;
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

namespace QuanLyBanHangClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        bool isReloading = false;
        public MainWindow() {
            InitializeComponent();
            RequestManager.getInstance().LoadingAnm = loadingAnim;
            reloadAllInfo();
        }
        void reloadAllInfo() {
            isReloading = true;
            ((IngredientTab)(TabItemIngredient.Content)).reloadUnitTableUI(true, delegate () {
                ((IngredientTab)(TabItemIngredient.Content)).reloadIngredientTableUI(true, delegate() {
                    ((FoodTab)(TabItemFood.Content)).reloadCategoryTableUI(true, delegate () {
                    ((FoodTab)(TabItemFood.Content)).reloadFoodTableUI(true, delegate() {
                        ((OrderAndTableTab)(TabItemOrder.Content)).reloadOrderUI(true, delegate () {
                            ((OrderAndTableTab)(TabItemOrder.Content)).reloadTableUI(true, delegate() {
                                ((ReportMainTab)TabItemReport.Content).reloadUI();
                                isReloading = false;
                            });
                        });
                    });
                    });
                });
            });

        }
        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(isReloading) {
                return;
            }
            var tabPrepareFood = ((PrepareFoodTab)TabItemPrepareFood.Content);

            if (TabItemIngredient.IsSelected
                && IngredientManager.getInstance().IngredientList.Count <= 0) {
                ((IngredientTab)(TabItemIngredient.Content)).reloadUnitTableUI(true, delegate () {
                    ((IngredientTab)(TabItemIngredient.Content)).reloadIngredientTableUI(true);
                });
                tabPrepareFood.IsOpeningThisTab = false;
            } else if (TabItemFood.IsSelected
                && FoodManager.getInstance().FoodList.Count <= 0) {
                ((FoodTab)(TabItemFood.Content)).reloadCategoryTableUI(true, delegate () {
                    ((FoodTab)(TabItemFood.Content)).reloadFoodTableUI(true);
                });
                tabPrepareFood.IsOpeningThisTab = false;
            } else if (TabItemOrder.IsSelected
                && TableManager.getInstance().TableList.Count <= 0) {
                ((OrderAndTableTab)(TabItemOrder.Content)).reloadOrderUI(true, delegate () {
                    ((OrderAndTableTab)(TabItemOrder.Content)).reloadTableUI(true);
                });
                tabPrepareFood.IsOpeningThisTab = false;
            } else if (TabItemRespository.IsSelected) {
                var tabImportIngredient = ((ImportIngredientTab)TabItemRespository.Content).TabImportIngredient;
                ((ImportTab)tabImportIngredient.Content).setupComboBoxIngredient();
                tabPrepareFood.IsOpeningThisTab = false;
            } else if (TabItemPrepareFood.IsSelected
                && !tabPrepareFood.IsOpeningThisTab) {
                tabPrepareFood.IsOpeningThisTab = true;
                tabPrepareFood.reloadAndUpdateUI();
            } else if (TabItemReport.IsSelected) {
                tabPrepareFood.IsOpeningThisTab = false;
            }
        }


        private void BtnChangePass_Click(object sender, RoutedEventArgs e) {
            WindownsManager.getInstance().showChangePasswordWindow(UserInfoManager.getInstance().userInfo.previousAcc);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e) {
            var loginPage = new Login(false);
            loginPage.Show();
            Close();
        }


        private void ButttonUser_Click(object sender, RoutedEventArgs e) {
            if (BtnChangePass.Visibility == Visibility.Visible) {
                BtnChangePass.Visibility = Visibility.Collapsed;
                BtnLogout.Visibility = Visibility.Collapsed;
                RotateTransform rotateTransform = new RotateTransform(0, 0.5, 0.5);
                ImageArrowAccount.LayoutTransform = rotateTransform;
            } else {
                BtnChangePass.Visibility = Visibility.Visible;
                BtnLogout.Visibility = Visibility.Visible;
                RotateTransform rotateTransform = new RotateTransform(-90, 0.5, 0.5);
                ImageArrowAccount.LayoutTransform = rotateTransform;
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e) {
            var window = new ChangeRestaurantInfoWindow();
            window.ShowDialog();
        }
    }
}

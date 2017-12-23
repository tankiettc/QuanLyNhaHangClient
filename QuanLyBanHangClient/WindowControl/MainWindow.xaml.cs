using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.FoodTab;
using QuanLyBanHangClient.AppUserControl.IngredientTab;
using QuanLyBanHangClient.AppUserControl.OrderTab;
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

namespace QuanLyBanHangClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        bool isReloading = false;
        public MainWindow()
        {
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
                        isReloading = false;
                    });
                    });
                });
            });

        }
        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(isReloading) {
                return;
            }
            if(TabItemIngredient.IsSelected
                && IngredientManager.getInstance().IngredientList.Count <= 0) {
                ((IngredientTab)(TabItemIngredient.Content)).reloadUnitTableUI(true, delegate () {
                    ((IngredientTab)(TabItemIngredient.Content)).reloadIngredientTableUI(true);
                });
            } else if(TabItemFood.IsSelected
                && FoodManager.getInstance().FoodList.Count <= 0) {
                ((FoodTab)(TabItemFood.Content)).reloadCategoryTableUI(true, delegate () {
                    ((FoodTab)(TabItemFood.Content)).reloadFoodTableUI(true);
                });
            } else if(TabItemOrder.IsSelected
                && TableManager.getInstance().TableList.Count <= 0) {
                ((OrderAndTableTab)(TabItemOrder.Content)).reloadOrderUI(true, delegate () {
                    ((OrderAndTableTab)(TabItemOrder.Content)).reloadTableUI(true);
                });
            }
        }
    }
}

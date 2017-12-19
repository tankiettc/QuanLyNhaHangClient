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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBanHangClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
            RequestManager.getInstance().LoadingAnm = loadingAnim;
        }
        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(TabItemIngredient.IsSelected
                && IngredientManager.getInstance().IngredientList.Count <= 0) {
                ((IngredientTab)(TabItemIngredient.Content)).reloadUnitTableUI(true, delegate () {
                    ((IngredientTab)(TabItemIngredient.Content)).reloadIngredientTableUI(true);
                });
            }
        }
    }
}

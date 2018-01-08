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
    /// Interaction logic for ChangeRestaurantInfoWindow.xaml
    /// </summary>
    public partial class ChangeRestaurantInfoWindow : Window
    {
        public ChangeRestaurantInfoWindow()
        {
            InitializeComponent();
            setupUI();
        }
        private void setupUI() {
            var info = RestaurantInfoManager.getInstance().Info;
            TextBoxName.Text = info.Name;
            TextBoxPhone.Text = info.Phone;
            TextBoxAddress.Text = info.Address;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            RestaurantInfoManager.getInstance().setName(TextBoxName.Text, false);
            RestaurantInfoManager.getInstance().setPhone(TextBoxPhone.Text, false);
            RestaurantInfoManager.getInstance().setAddess(TextBoxAddress.Text);
            WindownsManager.getInstance().showMessageBoxConfirm("Đổi thông tin thành công", "Chúc mừng");
        }
    }
}

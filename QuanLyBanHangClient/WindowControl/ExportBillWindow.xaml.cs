using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyBanHangClient.WindowControl {
    /// <summary>
    /// Interaction logic for ExportBillWindow.xaml
    /// </summary>
    public partial class ExportBillWindow : Window {
        List<Order> _orderList = null;
        Action _afterConfirm;
        public ExportBillWindow(List<Order> orderList, Action afterConfirm) {
            InitializeComponent();
            _orderList = orderList;
            _afterConfirm = afterConfirm;
            TextBoxAddress.Text = RestaurantInfoManager.getInstance().Info.Path;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e) {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            var path = dialog.SelectedPath;
            TextBoxAddress.Text = path;
            bool exist = checkAndUpdateUI(path);
            if(exist) {
                RestaurantInfoManager.getInstance().setPath(path);
            }
        }

        private bool checkAndUpdateUI(string path) {
            if (Directory.Exists(path)
                && !string.IsNullOrEmpty(path)) {
                TextBlockState.Text = "Đường dẫn tồn tại, có thể lập hóa đơn";
                TextBlockState.Foreground = new SolidColorBrush(Colors.Green);
                BtnConfirm.IsEnabled = true;
                return true;
            } else {
                TextBlockState.Text = "Xin chọn lại đường dẫn";
                TextBlockState.Foreground = new SolidColorBrush(Colors.Red);
                BtnConfirm.IsEnabled = false;
                return false;
            }
        }

        private void TextBoxAddress_TextChanged(object sender, TextChangedEventArgs e) {
            checkAndUpdateUI(TextBoxAddress.Text);
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if(_orderList != null) {
                _orderList.ForEach(delegate (Order order) {
                    OrderManager.getInstance().exportBillAsPdf(TextBoxAddress.Text, order);
                });
            }
            _afterConfirm?.Invoke();
            Close();
        }
    }
}

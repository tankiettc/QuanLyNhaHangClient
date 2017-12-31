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
    /// Interaction logic for ChangePassWindow.xaml
    /// </summary>
    public partial class ChangePassWindow : Window
    {
        string _userName;
        public ChangePassWindow(string userName)
        {
            InitializeComponent();
            _userName = userName;
            Title = "Đổi mật khẩu - Tài khoản: " + userName;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(PasswordBoxNewPassword.Password)) {
                WindownsManager.getInstance().showMessageBoxConfirm("Chưa nhập mật khẩu mới", "Đổi mật khẩu thất bại");
                return;
            } else if (string.IsNullOrEmpty(PasswordBoxConfirmPassword.Password)) {
                WindownsManager.getInstance().showMessageBoxConfirm("Chưa nhập mật khẩu xác nhận", "Đổi mật khẩu thất bại");
                return;
            } else if(!PasswordBoxConfirmPassword.Password.Equals(PasswordBoxNewPassword.Password)) {
                WindownsManager.getInstance().showMessageBoxConfirm("Mật khẩu xác nhận không khớp", "Đổi mật khẩu thất bại");
                return;
            }
            loadingAnim.Visibility = Visibility.Visible;
            var userInfoManager = UserInfoManager.getInstance();
            Action<NetworkResponse> cbSuccess =
                    delegate (NetworkResponse result) {
                        if(result.Successful) {
                            WindownsManager.getInstance().showMessageBoxConfirm("Mật khẩu của tài khoản " + _userName + " đã được cập nhật", "Thành công");
                        } else {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        }
                        loadingAnim.Visibility = Visibility.Hidden;
                    };
            Action<string> cbFail = delegate (string err) {
                WindownsManager.getInstance().showMessageBoxErrorNetwork();
                loadingAnim.Visibility = Visibility.Hidden;
            };
            UserManager.getInstance().changePasswordFromServerAndUpdate(
                _userName,
                PasswordBoxNewPassword.Password,
                cbSuccess,
                cbFail
                );
        }
    }
}

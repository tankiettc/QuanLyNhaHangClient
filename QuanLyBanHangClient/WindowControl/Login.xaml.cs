using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(bool isFromOpenApp)
        {
            InitializeComponent();
            setUpUI(isFromOpenApp);
        }
        public Login() {
            InitializeComponent();
            setUpUI(true);
        }
        private void setUpUI(bool isFromOpenApp) {
            var userInfoManager = UserInfoManager.getInstance();
            CheckBoxKeepLogin.IsChecked = userInfoManager.userInfo.isKeepSignedIn;
            CheckBoxRememberPass.IsChecked = userInfoManager.userInfo.isRememberPass;
            if(userInfoManager.userInfo.isRememberPass) {
                PasswordBoxPassword.Password = userInfoManager.userInfo.previousPass;
                TextBoxUserName.Text = userInfoManager.userInfo.previousAcc;
            }
            if (!isFromOpenApp) {
                userInfoManager.resetLoginInfo();
            } else if (userInfoManager.userInfo.isKeepSignedIn
                && !string.IsNullOrEmpty(userInfoManager.userInfo.token)) {
                BtnLogin_Click(null, null);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e) {
            if(sender != null) { 
                if(string.IsNullOrEmpty(TextBoxUserName.Text)) {
                    WindownsManager.getInstance().showMessageBoxConfirm("Chưa nhập tài khoản", "Đăng nhập thất bại");
                    return;
                } else if(string.IsNullOrEmpty(PasswordBoxPassword.Password)) {
                    WindownsManager.getInstance().showMessageBoxConfirm("Chưa nhập mật khẩu", "Đăng nhập thất bại");
                    return;
                }
            }
            loadingAnim.Visibility = Visibility.Visible;
            var userInfoManager = UserInfoManager.getInstance();
            Action<UserInfo> cbSuccess =
                    delegate (UserInfo result) {
                        userInfoManager.setIsKeepSignedIn(CheckBoxKeepLogin.IsChecked, false);
                        userInfoManager.setIsRememberPass(CheckBoxRememberPass.IsChecked, false);
                        userInfoManager.setPreviousPass(CheckBoxRememberPass.IsChecked == false ? "" : PasswordBoxPassword.Password, false);
                        userInfoManager.setPreviousAcc(TextBoxUserName.Text);

                        MainWindow mainWin = new MainWindow();
                        mainWin.Show();

                        Close();
                    };
            Action<HttpStatusCode> cbFail = delegate (HttpStatusCode code) {
                if (code == HttpStatusCode.Unauthorized) {
                    WindownsManager.getInstance().showMessageBoxConfirm("Sai tài khoản hoặc mật khẩu", "Đăng nhập thất bại");
                } else {
                    WindownsManager.getInstance().showMessageBoxErrorNetwork();
                }
                loadingAnim.Visibility = Visibility.Hidden;
            };
            userInfoManager.getAccessTokenAsync(
                TextBoxUserName.Text,
                PasswordBoxPassword.Password,
                cbSuccess,
                cbFail
                );
        }

        private void CheckBoxRememberPass_Checked(object sender, RoutedEventArgs e) {
            var userInfoManager = UserInfoManager.getInstance();
            userInfoManager.setIsRememberPass(true);
        }

        private void CheckBoxRememberPass_Unchecked(object sender, RoutedEventArgs e) {
            var userInfoManager = UserInfoManager.getInstance();
            userInfoManager.setPreviousPass("", false);
            userInfoManager.setIsRememberPass(false);

        }

        private void CheckBoxKeepLogin_Checked(object sender, RoutedEventArgs e) {
            var userInfoManager = UserInfoManager.getInstance();
            userInfoManager.setIsKeepSignedIn(true);
        }
        

        private void CheckBoxKeepLogin_Unchecked(object sender, RoutedEventArgs e) {
            UserInfoManager.getInstance().setIsKeepSignedIn(false);
        }
    }
}

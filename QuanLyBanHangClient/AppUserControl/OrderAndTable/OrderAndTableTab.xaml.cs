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

namespace QuanLyBanHangClient.AppUserControl.OrderTab {
    /// <summary>
    /// Interaction logic for OrderAndTableTab.xaml
    /// </summary>
    public partial class OrderAndTableTab : UserControl {
        public OrderAndTableTab() {
            InitializeComponent();
            TableTabCustom.orderAndTableTab = this;
            OrderTabCustom.orderAndTableTab = this;
        }
        public void reloadTableUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if (isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadTableUI();
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                TableManager.getInstance().getAllTableFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {
                TableTabCustom.reloadLVTable();
                cbAfterReload?.Invoke();
            }

        }
        public void reloadOrderUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if (isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadOrderUI(false, cbAfterReload);
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                OrderManager.getInstance().getAllOrderFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {

                cbAfterReload?.Invoke();
            }
        }

    }
}

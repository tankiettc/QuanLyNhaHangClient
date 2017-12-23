using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.AppUserControl.OrderTab.Models;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHangClient.AppUserControl.OrderTab {
    /// <summary>
    /// Interaction logic for TableTab.xaml
    /// </summary>
    public partial class TableTab : UserControl {
        public OrderAndTableTab orderAndTableTab {
            get; set;
        }
        public TableTab() {
            InitializeComponent();
        }
        public void reloadLVTable() {
            LVTable.Items.Clear();
            var tableList = TableManager.getInstance().TableList;
            foreach (KeyValuePair<int, Table> entry in tableList) {
                if (entry.Value != null) {
                    var tableInfo = new TableInfo(entry.Value);
                    LVTable.Items.Add(tableInfo);
                }
            }
            BtnRemoveTable.IsEnabled = false;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Table_DoubleClick(object sender, MouseButtonEventArgs e) {
            if(LVTable.SelectedIndex < 0) {
                return;
            }
            TableInfo tableInfo = (TableInfo) LVTable.SelectedItem;
            orderAndTableTab.OrderTabCustom.reloadAllUIOrderTab(tableInfo.TableData);
            orderAndTableTab.OrderTabCustom.Visibility = System.Windows.Visibility.Visible;
            orderAndTableTab.TableTabCustom.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LVTable_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(LVTable.SelectedItem == null) {
                return;
            }
            BtnChangeTableId.IsEnabled = false;
            BtnRemoveTable.IsEnabled = true;
            TextBoxCurrentTableId.Text = ((TableInfo)LVTable.SelectedItem).TableData.TableId.ToString();
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e) {
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            reloadLVTable();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            var tableIdCreate = 0;
            while (true) {
                if(!TableManager.getInstance().TableList.ContainsKey(tableIdCreate)) {
                    break;
                }
                tableIdCreate++;
            }
            TableManager.getInstance().createTableFromServerAndUpdate(
                tableIdCreate,
                cbSuccessSent,
                cbError
            );
        }

        private void BtnChangeTableId_Click(object sender, System.Windows.RoutedEventArgs e) {
            int newTableId = 0;
            int oldTableId = ((TableInfo)LVTable.SelectedItem).TableData.TableId;
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {

                            foreach (KeyValuePair<int, Order> entry in OrderManager.getInstance().OrderList) {
                                if (entry.Value != null
                                && entry.Value.TableId == oldTableId) {
                                    entry.Value.TableId = newTableId;
                                }
                            }
                            reloadLVTable();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            TableManager.getInstance().updateTableFromServerAndUpdate(
                oldTableId,
                newTableId,
                cbSuccessSent,
                cbError
            );
        }

        private void TextBoxCurrentTableId_TextChanged(object sender, TextChangedEventArgs e) {
            if (LVTable.SelectedIndex < 0) {
                return;
            }
            int newTableId = 0;
            int oldTableId = ((TableInfo)LVTable.SelectedItem).TableData.TableId;
            if (!int.TryParse(TextBoxCurrentTableId.Text, out newTableId)
                || newTableId == oldTableId) {
                BtnChangeTableId.IsEnabled = false;
                return;
            }
            BtnChangeTableId.IsEnabled = true;
        }

        private void BtnRefresh_Click(object sender, System.Windows.RoutedEventArgs e) {
            if(orderAndTableTab == null) {
                return;
            }
            orderAndTableTab.reloadOrderUI(true, delegate () {
                orderAndTableTab.reloadTableUI(true);
            });
        }

        private void BtnRemoveTable_Click(object sender, System.Windows.RoutedEventArgs e) {
            int oldTableId = ((TableInfo)LVTable.SelectedItem).TableData.TableId;
            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            reloadLVTable();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };
            TableManager.getInstance().deleteTableromServerAndUpdate(
                oldTableId,
                cbSuccessSent,
                cbError
            );
        }

        private void BtnHistory_Click(object sender, System.Windows.RoutedEventArgs e) {
            HistoryTab.reloadListViewOrder(false);
            HistoryTab.Visibility = System.Windows.Visibility.Visible;
        }
    }
}

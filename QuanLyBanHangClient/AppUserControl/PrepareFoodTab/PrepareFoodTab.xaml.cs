using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace QuanLyBanHangClient.AppUserControl.PrepareFoodTab
{
    /// <summary>
    /// Interaction logic for PrepareFoodTab.xaml
    /// </summary>
    public partial class PrepareFoodTab : UserControl
    {
        public bool IsOpeningThisTab { get; set; }
        public bool IsChangingState { get; set; }
        System.Windows.Threading.DispatcherTimer reloadTimer;
        public PrepareFoodTab()
        {
            InitializeComponent();

            IsOpeningThisTab = false;
            IsChangingState = false;

            reloadTimer = new System.Windows.Threading.DispatcherTimer();
            reloadTimer.Tick += new EventHandler(reloadTimer_Tick);
            reloadTimer.Interval = new TimeSpan(0, 0, 7);
            reloadTimer.Start();
        }
        void updateUI() {
            LVPrepareFood.Items.Clear();
            int selectIndex = ComboBoxState.SelectedIndex;
            int prepareFoodState = -1;
            switch(selectIndex) {
                case 0: {
                        prepareFoodState = 3;
                        break;
                    }
                case 1: {
                        prepareFoodState = 2;
                        break;
                    }
                case 2: {
                        prepareFoodState = 1;
                        break;
                    }
                case 3: {
                        prepareFoodState = 0;
                        break;
                    }
                default:
                    break;
            }
            foreach (KeyValuePair<int, PrepareFood> entry in PrepareFoodManager.getInstance().PrepareFoodList) {
                if (entry.Value != null) {
                    if ((prepareFoodState >= 0
                        && entry.Value.PrepareStateId == prepareFoodState) || prepareFoodState < 0) {
                        LVPrepareFood.Items.Add(new PrepareFoodCell(entry.Value));
                    }
                }
            }
        }
        private void reloadTimer_Tick(object sender, EventArgs e) {
            if(!IsOpeningThisTab) {
                reloadTimer.Stop();
                return;
            }
            reloadAndUpdateUI();
        }
        public void reloadAndUpdateUI() {
            if(reloadTimer == null
                || IsChangingState) {
                return;
            }
            reloadTimer.Stop();
            RequestManager.getInstance().showLoading();
            PrepareFoodManager.getInstance().getAllPrepareFoodFromServerAndUpdate(
                delegate (NetworkResponse rs) {
                    if (rs.Successful) {
                        updateUI();
                        if(!reloadTimer.IsEnabled) {
                            reloadTimer.Start();
                        }
                    }
                    RequestManager.getInstance().hideLoading();
                },
                delegate (string err) {
                    if (!reloadTimer.IsEnabled) {
                        reloadTimer.Start();
                    }
                    RequestManager.getInstance().hideLoading();
                }
                );
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;

            ICollectionView cv = CollectionViewSource.GetDefaultView(LVPrepareFood.Items);
            cv.Filter = o => {
                /* change to get data row value */
                PrepareFoodCell p = o as PrepareFoodCell;
                if (!string.IsNullOrEmpty(filterText)) {
                    return (p.TextBlockName.Text.ToUpper().Contains(filterText.ToUpper()) || p.TextBlockTableId.Text.ToUpper().Contains(filterText.ToUpper()));
                } else {
                    return true;
                }
                /* end change to get data row value */
            };
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) {
            reloadAndUpdateUI();
        }

        private void ComboBoxState_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            reloadAndUpdateUI();
        }
    }
}

using QuanLyBanHangAPI.model;
using QuanLyBanHangClient.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyBanHangClient.AppUserControl.IngredientTab {
    /// <summary>
    /// Interaction logic for IngredientTab.xaml
    /// </summary>
    public partial class IngredientTab : UserControl {
        private ObservableCollection<IngredientTable> ingredientListTable = new ObservableCollection<IngredientTable>();
        private ObservableCollection<UnitTable> unitListTable = new ObservableCollection<UnitTable>();

        public IngredientTab() {
            InitializeComponent();
            DataGridIngredient.ItemsSource = ingredientListTable;
            DataGridUnit.ItemsSource = unitListTable;
        }

        private void BtnAddIngredient_Click(object sender, RoutedEventArgs e) {
            WindownsManager.getInstance().showDetailIngredientWindow(this);
        }

        private void BtnRemoveIngredient_Click(object sender, RoutedEventArgs e) {
            var mesResult = WindownsManager.getInstance().showMessageBoxConfirmDelete();
            if(mesResult == MessageBoxResult.No) {
                return;
            }

            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            this.reloadIngredientTableUI();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };

            IngredientTable ingredientTable = DataGridIngredient.SelectedItem as IngredientTable;
            IngredientManager.getInstance().deleteIngredientFromServerAndUpdate(
                ingredientTable.Id,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnEditIngredient_Click(object sender, RoutedEventArgs e) {
            if(DataGridIngredient.SelectedItem != null) {
                IngredientTable ingredientTable = DataGridIngredient.SelectedItem as IngredientTable;
                WindownsManager.getInstance().showDetailIngredientWindow(this, ingredientTable.Id);
            }
        }

        private void BtnAddUnit_Click(object sender, RoutedEventArgs e) {
            WindownsManager.getInstance().showDetailUnitWindow(this);
        }

        private void BtnRemoveUnit_Click(object sender, RoutedEventArgs e) {
            var mesResult = WindownsManager.getInstance().showMessageBoxConfirmDelete();
            if (mesResult == MessageBoxResult.No) {
                return;
            }

            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            RequestManager.getInstance().hideLoading();
                        } else {
                            reloadUnitTableUI();
                            reloadIngredientTableUI(true);
                        }
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };

            UnitTable unitTable = DataGridUnit.SelectedItem as UnitTable;
            UnitManager.getInstance().deleteUnitFromServerAndUpdate(
                unitTable.Id,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnEditUnit_Click(object sender, RoutedEventArgs e) {
            if (DataGridUnit.SelectedItem != null) {
                UnitTable unitTable = DataGridUnit.SelectedItem as UnitTable;
                WindownsManager.getInstance().showDetailUnitWindow(this, unitTable.Id);
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e) {

        }

        public void reloadIngredientTableUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if(isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadIngredientTableUI(false, cbAfterReload);
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                IngredientManager.getInstance().getAllIngredientFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {
                ingredientListTable.Clear();
                foreach (KeyValuePair<int, Ingredient> entry in IngredientManager.getInstance().IngredientList) {
                    if (entry.Value != null) {
                        ingredientListTable.Add(new IngredientTable() {
                            Id = entry.Value.IngredientId,
                            Name = entry.Value.Name,
                            UnitName = entry.Value.Unit.Name
                        });
                    }
                }
                cbAfterReload?.Invoke();
            }

        }
        public void reloadUnitTableUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if(isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadUnitTableUI(false, cbAfterReload);
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                UnitManager.getInstance().getAllUnitFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {
                unitListTable.Clear();
                foreach (KeyValuePair<int, Unit> entry in UnitManager.getInstance().UnitList) {
                    if (entry.Value != null) {
                        unitListTable.Add(new UnitTable() {
                            Id = entry.Value.UnitId,
                            Name = entry.Value.Name
                        });
                    }
                }
                cbAfterReload?.Invoke();
            }
        }

        private void DataGridIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnEditIngredient.IsEnabled = true;
            BtnRemoveIngredient.IsEnabled = true;

            BtnEditUnit.IsEnabled = false;
            BtnRemoveUnit.IsEnabled = false;
        }

        private void DataGridUnit_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnEditIngredient.IsEnabled = false;
            BtnRemoveIngredient.IsEnabled = false;

            BtnEditUnit.IsEnabled = true;
            BtnRemoveUnit.IsEnabled = true;
        }
    }
    class IngredientTable : INotifyPropertyChanged {
        private int id;
        public int Id {
            get { return this.id; }
            set {
                if (this.id != value) {
                    this.id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }
        private string name;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        private string unitName;
        public string UnitName {
            get { return this.unitName; }
            set {
                if (this.unitName != value) {
                    this.unitName = value;
                    this.NotifyPropertyChanged("UnitName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }


    class UnitTable : INotifyPropertyChanged {
        private int id;
        public int Id {
            get { return this.id; }
            set {
                if (this.id != value) {
                    this.id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }
        private string name;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

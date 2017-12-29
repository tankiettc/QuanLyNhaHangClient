using QuanLyBanHangAPI.model;
using QuanLyBanHangAPI.model.SQL;
using QuanLyBanHangClient.Manager;
using QuanLyBanHangClient.WindowControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace QuanLyBanHangClient.AppUserControl.FoodTab
{
    /// <summary>
    /// Interaction logic for FoodTab.xaml
    /// </summary>
    public partial class FoodTab : UserControl
    {
        private ObservableCollection<FoodTable> foodListTable = new ObservableCollection<FoodTable>();
        private ObservableCollection<CategoryTable> categoryListTable = new ObservableCollection<CategoryTable>();

        public FoodTab() {
            InitializeComponent();
            DataGridCategory.ItemsSource = categoryListTable;
            DataGridFood.ItemsSource = foodListTable;
        }


        private void BtnAddFood_Click(object sender, RoutedEventArgs e) {
            WindownsManager.getInstance().showDetailFoodWindow(this);
        }

        private void BtnRemoveFood_Click(object sender, RoutedEventArgs e) {
            var mesResult = WindownsManager.getInstance().showMessageBoxConfirmDelete();
            if (mesResult == MessageBoxResult.No) {
                return;
            }

            RequestManager.getInstance().showLoading();
            Action<NetworkResponse> cbSuccessSent =
                    delegate (NetworkResponse networkResponse) {
                        if (!networkResponse.Successful) {
                            WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                        } else {
                            this.reloadFoodTableUI();
                        }
                        RequestManager.getInstance().hideLoading();
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };

            FoodTable foodTable = DataGridFood.SelectedItem as FoodTable;
            FoodManager.getInstance().deleteFoodFromServerAndUpdate(
                foodTable.Id,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnEditFood_Click(object sender, RoutedEventArgs e) {
            if (DataGridFood.SelectedItem != null) {
                FoodTable foodTable = DataGridFood.SelectedItem as FoodTable;
                WindownsManager.getInstance().showDetailFoodWindow(this, foodTable.Id);
            }
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e) {
            WindownsManager.getInstance().showDetailFoodWithCategorizeWindow(this);
        }

        private void BtnRemoveCategory_Click(object sender, RoutedEventArgs e) {
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
                            reloadCategoryTableUI();
                            reloadFoodTableUI(true);
                        }
                    };

            Action<string> cbError =
                    delegate (string err) {
                        WindownsManager.getInstance().showMessageBoxErrorNetwork();
                        RequestManager.getInstance().hideLoading();
                    };

            CategoryTable categoryTable = DataGridCategory.SelectedItem as CategoryTable;
            FoodCategorizeManager.getInstance().deleteFoodCategorizeFromServerAndUpdate(
                categoryTable.Id,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnEditCategory_Click(object sender, RoutedEventArgs e) {
            if (DataGridCategory.SelectedItem != null) {
                CategoryTable categoryTable = DataGridCategory.SelectedItem as CategoryTable;
                WindownsManager.getInstance().showDetailFoodWithCategorizeWindow(this, categoryTable.Id);
            }
        }

        private void DataGridFood_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnRemoveFood.IsEnabled = true;
            BtnEditFood.IsEnabled = true;

            BtnRemoveCategory.IsEnabled = false;
            BtnEditCategory.IsEnabled = false;
        }

        private void DataGridCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) { 
            BtnRemoveFood.IsEnabled = false;
            BtnEditFood.IsEnabled = false;

            BtnRemoveCategory.IsEnabled = true;
            BtnEditCategory.IsEnabled = true;

        }
        public void reloadFoodTableUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if (isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadFoodTableUI(false, cbAfterReload);
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                FoodManager.getInstance().getAllFoodFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {
                foodListTable.Clear();
                foreach (KeyValuePair<int, Food> entry in FoodManager.getInstance().FoodList) {
                    if (entry.Value != null) {
                        foodListTable.Add(new FoodTable() {
                            Id = entry.Value.FoodId,
                            Name = entry.Value.Name,
                            Price = entry.Value.Price,
                            Category = FoodCategorizeManager.getInstance().FoodCategorizeList[entry.Value.FoodCategorizeId]?.Name
                        });
                    }
                }
                cbAfterReload?.Invoke();
            }

        }
        public void reloadCategoryTableUI(bool isReloadFromServer = false, Action cbAfterReload = null) {
            if (isReloadFromServer) {
                RequestManager.getInstance().showLoading();
                Action<NetworkResponse> cbSuccessSent =
                        delegate (NetworkResponse networkResponse) {
                            RequestManager.getInstance().hideLoading();
                            if (!networkResponse.Successful) {
                                WindownsManager.getInstance().showMessageBoxSomeThingWrong();
                            } else {
                                reloadCategoryTableUI(false, cbAfterReload);
                            }
                        };

                Action<string> cbError =
                        delegate (string err) {
                            WindownsManager.getInstance().showMessageBoxErrorNetwork();
                            RequestManager.getInstance().hideLoading();
                        };
                FoodCategorizeManager.getInstance().getAllFoodCategorizeFromServerAndUpdate(
                    cbSuccessSent,
                    cbError
                    );
            } else {
                categoryListTable.Clear();
                foreach (KeyValuePair<int, FoodCategorize> entry in FoodCategorizeManager.getInstance().FoodCategorizeList) {
                    if (entry.Value != null) {
                        categoryListTable.Add(new CategoryTable() {
                            Id = entry.Value.FoodCategorizeId,
                            Name = entry.Value.Name
                        });
                    }
                }
                cbAfterReload?.Invoke();
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;

            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridFood.ItemsSource);
            cv.Filter = o => {
                /* change to get data row value */
                FoodTable p = o as FoodTable;
                if (!string.IsNullOrEmpty(filterText)) {
                    return (p.Name.ToUpper().Contains(filterText.ToUpper()) || p.Category.ToUpper().Contains(filterText.ToUpper()));
                } else {
                    return true;
                }
                /* end change to get data row value */
            };

            ICollectionView cv2 = CollectionViewSource.GetDefaultView(DataGridCategory.ItemsSource);
            cv2.Filter = o => {
                /* change to get data row value */
                CategoryTable p = o as CategoryTable;
                if (!string.IsNullOrEmpty(filterText)) {
                    return (p.Name.ToUpper().Contains(filterText.ToUpper()));
                } else {
                    return true;
                }
                /* end change to get data row value */
            };
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) {
            reloadCategoryTableUI(true, delegate () {
                reloadFoodTableUI(true);
            });
        }
    }
    class FoodTable : INotifyPropertyChanged {
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
        private decimal price;
        public decimal Price {
            get { return this.price; }
            set {
                if (this.price != value) {
                    this.price = value;
                    this.NotifyPropertyChanged("Price");
                }
            }
        }
        private string category;
        public string Category {
            get { return this.category; }
            set {
                if (this.category != value) {
                    this.category = value;
                    this.NotifyPropertyChanged("Category");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }


    class CategoryTable : INotifyPropertyChanged {
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

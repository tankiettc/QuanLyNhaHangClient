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
        private ObservableCollection<CategoryTable> categoryListTable = new ObservableCollection<CategoryTable>();

        public FoodTab() {
            InitializeComponent();
            DataGridCategory.ItemsSource = categoryListTable;
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

            FoodCell foodTable = LVFood.SelectedItem as FoodCell;
            FoodManager.getInstance().deleteFoodFromServerAndUpdate(
                foodTable.FoodData.FoodId,
                cbSuccessSent,
                cbError
                );
        }

        private void BtnEditFood_Click(object sender, RoutedEventArgs e) {
            if (LVFood.SelectedItem != null) {
                FoodCell foodTable = LVFood.SelectedItem as FoodCell;
                showEditFoodView(foodTable.FoodData.FoodId);
            }
        }
        public void showEditFoodView(int foodId) {
            WindownsManager.getInstance().showDetailFoodWindow(this, foodId);
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
                LVFood.Items.Clear();
                foreach (KeyValuePair<int, Food> entry in FoodManager.getInstance().FoodList) {
                    if (entry.Value != null) {
                        var foodCell = new FoodCell(entry.Value, this);
                        LVFood.Items.Add(foodCell);

                        var imageId = entry.Value.ImageId ?? default(int);
                        if (entry.Value.ImageId != null
                            && imageId >= 0) {
                            if (ImageManager.getInstance().checkImageExistLocal(imageId)) {
                                ImageManager.getInstance().loadImage(imageId, delegate (byte[] imageData) {
                                    foodCell.setImageFood(UtilFuction.ByteToImage(imageData));
                                });
                            } else {
                                ImageManager.getInstance().loadImage(imageId, delegate (byte[] imageData) {
                                    checkAndSetImageForFoodCell(entry.Value.FoodId, imageId, UtilFuction.ByteToImage(imageData));
                                });
                            }
                        }
                    }
                }
                cbAfterReload?.Invoke();
            }

        }
        private void checkAndSetImageForFoodCell(int foodId, int imageId, System.Drawing.Image image) {
            foreach(var foodCell in LVFood.Items.OfType<FoodCell>()) {
                if(foodCell != null
                    && foodCell.FoodData != null
                    && foodCell.FoodData.FoodId == foodId
                    && foodCell.FoodData.ImageId == imageId) {
                    foodCell.setImageFood(image);
                }
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

            ICollectionView cv = CollectionViewSource.GetDefaultView(LVFood.ItemsSource);
            cv.Filter = o => {
                /* change to get data row value */
                FoodCell p = o as FoodCell;
                if (!string.IsNullOrEmpty(filterText)) {
                    return (p.TextBlockName.Text.ToUpper().Contains(filterText.ToUpper()) || p.TextBlockCategory.Text.ToUpper().Contains(filterText.ToUpper()));
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

        private void DataGridCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DataGridCategory.SelectedItem != null) {
                CategoryTable categoryTable = DataGridCategory.SelectedItem as CategoryTable;
                WindownsManager.getInstance().showDetailFoodWithCategorizeWindow(this, categoryTable.Id);
            }
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

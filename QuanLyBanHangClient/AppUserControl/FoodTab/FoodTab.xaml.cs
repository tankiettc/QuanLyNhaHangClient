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
        public ObservableCollection<FoodDataGrid> FoodList
        {
            get;
            set;
        }
        public FoodTab()
        {
            FoodList = new ObservableCollection<FoodDataGrid>();
            InitializeComponent();
            this.DataGridFood.ItemsSource = FoodList;
            FoodList.Add(
                new FoodDataGrid {
                    ID = 0,
                    Name = "Nem xao xã ớt",
                    Price = 15000
                }

            );

            FoodList.Add(
                new FoodDataGrid {
                    ID = 1,
                    Name = "Cơm Chiên dương châu với trừng",
                    Price = 17000
                }

            );

            FoodList.Add(
                new FoodDataGrid {
                    ID = 2,
                    Name = "hsjgfjdshgfjsdhgfjshdgf",
                    Price = 1000
                }

            );
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e) {
            DataGridRow row = sender as DataGridRow;
            // Some operations with this row
            var foodDetail = new FoodDetail();
            foodDetail.Show();
        }

    }
    public class FoodDataGrid : INotifyPropertyChanged
    {
        int id;
        string name;
        double price;
        public int ID
        {
            get { return id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("ID");
                };
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                };
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (this.price != value)
                {
                    this.price = value;
                    this.NotifyPropertyChanged("Price");
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

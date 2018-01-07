using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace QuanLyBanHangClient.AppUserControl.ReportTab
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {

        public Chart()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Tag != null)
                {
                    var items = (List<Bar>)this.Tag;
                    var itemColWidth = this.Width / items.Count;

                    this.DataContext = new RecordCollection(items);
                    afgeqqw.Tag = itemColWidth;
                }
            }
            catch (Exception)
            {

            }
        }
    }

    class RecordCollection : ObservableCollection<Record> {

        public RecordCollection(List<Bar> barvalues) {
            Random rand = new Random();
            BrushCollection brushcoll = new BrushCollection();

            foreach (Bar barval in barvalues) {
                int num = rand.Next(brushcoll.Count / 3);
                Add(new Record(barval.Value, brushcoll[num], barval.BarName));
            }
        }

    }

    class BrushCollection : ObservableCollection<Brush> {
        public BrushCollection() {
            Type _brush = typeof(Brushes);
            PropertyInfo[] props = _brush.GetProperties();
            foreach (PropertyInfo prop in props) {
                //Brush _color = (Brush)prop.GetValue(null, null);
                //if (_color != Brushes.LightSteelBlue && _color != Brushes.White &&
                //     _color != Brushes.WhiteSmoke && _color != Brushes.LightCyan &&
                //     _color != Brushes.LightYellow && _color != Brushes.Linen)
                //    Add(_color);
                Brush _color = new SolidColorBrush(Colors.White);
                Add(_color);
            }
        }
    }

    class Bar {

        public string BarName { set; get; }

        public int Value { set; get; }

    }

    class Record : INotifyPropertyChanged {
        public Brush Color { set; get; }

        public string Name { set; get; }

        private int _data;
        public int Data {
            set {
                if (_data != value) {
                    _data = value;

                }
            }
            get {
                return _data;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Record(int value, Brush color, string name) {
            Data = value;
            Color = color;
            Name = name;
        }

        protected void PropertyOnChange(string propname) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}

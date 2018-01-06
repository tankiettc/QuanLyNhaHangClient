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

namespace QuanLyBanHangClient.AppUserControl.Custom {
    /// <summary>
    /// Interaction logic for ComboBoxRoundedImage.xaml
    /// </summary>
    public partial class ComboBoxRoundedImage : UserControl {
        Action<int> _selectedCB = null;
        bool _isChangeTextFromCode = false;
        public Size ImageSize { get; set; }
        public ComboBoxRoundedImage() {
            InitializeComponent();
            ImageSize = new Size(15, 15);
            //((ContentPresenter) ComboBoxData.FindName("ContentSite")).Visibility = Visibility.Hidden;
        }

        public void setSelectedCB(Action<int> selectedCB) {
            _selectedCB = selectedCB;
        }

        public ComboBoxItem addItem(string txt, ImageSource imgSrc) {
            var item = createItem(txt, imgSrc);
            item.Tag = ComboBoxData.Items.Count;
            ComboBoxData.Items.Add(item);
            return item;
        }
        public void clear() {
            ComboBoxData.Items.Clear();
            clearSearch();
        }

        private void clearSearch() {
            _isChangeTextFromCode = true;
            TextBoxSearch.Text = "";
            ImageSearch.Source = null;
        }
        private ComboBoxItem createItem(string txt, ImageSource imgSrc) {
            var item = new ComboBoxItem();
            item.Style = (Style) Application.Current.FindResource("ComboBoxItemRoundedStyle");

            var dock = new DockPanel();
            var image = new Image();
            image.Name = "ImageData";
            image.Source = imgSrc;
            image.Width = ImageSize.Width;
            image.Height = ImageSize.Height;
            image.Stretch = Stretch.Fill;

            var txtBlock = new TextBlock();
            txtBlock.Name = "TextBlockData";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            txtBlock.VerticalAlignment = VerticalAlignment.Center;
            txtBlock.TextWrapping = TextWrapping.Wrap;
            txtBlock.Text = txt;

            dock.Children.Add(image);
            dock.Children.Add(txtBlock);

            item.Content = dock;
            return item;
        }
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;

            ICollectionView cv = CollectionViewSource.GetDefaultView(ComboBoxData.Items);
            cv.Filter = o => {
                /* change to get data row value */
                ComboBoxItem p = o as ComboBoxItem;
                var txt = ((TextBlock)((DockPanel)p.Content).Children[1]).Text;

                if (!string.IsNullOrEmpty(filterText)
                && !_isChangeTextFromCode) {
                    return (txt.ToUpper().Contains(filterText.ToUpper()));
                } else {
                    return true;
                }
                /* end change to get data row value */
            };
            _isChangeTextFromCode = false;
        }

        private void ComboBoxData_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ComboBoxData.SelectedIndex < 0) {
                clearSearch();
                return;
            }

            var itemSelected = ((ComboBoxItem)ComboBoxData.SelectedItem);
            var txt = ((TextBlock)((DockPanel)itemSelected.Content).Children[1]).Text;
            var imageSrc = ((Image)((DockPanel)itemSelected.Content).Children[0]).Source;

            ImageSearch.Source = imageSrc;
            _isChangeTextFromCode = true;
            TextBoxSearch.Text = txt;

            _selectedCB?.Invoke(ComboBoxData.SelectedIndex);
        }


        private void ComboBoxData_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key >= Key.Space && e.Key <= Key.Z) {
                TextBoxSearch.Text += e.Key.ToString();
            } else if(e.Key == Key.Back
                && !TextBoxSearch.Text.Equals("")) {
                TextBoxSearch.Text = TextBoxSearch.Text.Substring(0, TextBoxSearch.Text.Length - 1);
            }
        }
    }
}

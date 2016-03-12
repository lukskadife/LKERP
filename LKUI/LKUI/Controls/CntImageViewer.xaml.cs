using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LKLibrary.DbClasses;

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntImageViewer.xaml
    /// </summary>
    public partial class CntImageViewer : UserControl
    {
        public class ImageSrc
        {
            public int Id;
            public BitmapImage Image { get; set; }
        }

        private List<ImageSrc> _Images = new List<ImageSrc>();

        public ImageSrc SeciliImage;

        public CntImageViewer()
        {
            InitializeComponent();
            DGridImage.ItemsSource = _Images;
        }

        private void DGridImage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageSrc secilen = ((System.Windows.Controls.DataGrid)(sender)).CurrentItem as ImageSrc;
            if (secilen == null || secilen.Image == null) Img.Source = null;
            else
            {
                this.SeciliImage = secilen;
                Img.Source = secilen.Image;
            }
        }

        public void AddImage(ImageSrc newImage)
        {
            if (_Images == null) _Images = new List<ImageSrc>();
            _Images.Add(newImage);
            DGridImage.Items.Refresh();
        }

        public void DeleteImage(ImageSrc img)
        {
            _Images.Remove(img);
            DGridImage.Items.Refresh();
        }
    }
}

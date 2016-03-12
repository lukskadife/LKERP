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
using System.Windows.Shapes;
using ETSevk.Classes;

namespace ETSevk
{
    /// <summary>
    /// Interaction logic for PageBarkodKontrol.xaml
    /// </summary>
    public partial class PageBarkodKontrol : Window
    {
        public PageBarkodKontrol()
        {
            InitializeComponent();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<vMamulKumaslar> list = DGridBarkodlar.ItemsSource as List<vMamulKumaslar>;

            TxtCount.Content = (list == null ? 0 : list.Count).ToString() + " adet";
            TxtSum.Content = (list == null ? 0 : list.Sum(s => s.Metre)).ToString() + " metre";
        }
    }
}

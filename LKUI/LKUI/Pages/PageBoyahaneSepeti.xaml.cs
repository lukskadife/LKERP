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
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageBoyahaneSepeti.xaml
    /// </summary>
    public partial class PageBoyahaneSepeti : UserControl
    {
        public PageBoyahaneSepeti()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSepet.ItemsSource = Boyahane.BoyahaneSepetiKumaslariGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridSepet.ToExcel<vMamulKumaslar>();
        }
    }
}

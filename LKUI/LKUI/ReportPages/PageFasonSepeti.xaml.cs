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
using LKLibrary.DbClasses;
using LKUI.Classes;


namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageFasonSepeti.xaml
    /// </summary>
    public partial class PageFasonSepeti : UserControl
    {
        public PageFasonSepeti()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridFasonSepeti.ItemsSource = new DBEvents().GetGeneric<vFasonSepeti>().ToList();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridFasonSepeti.ToExcel<vFasonSepeti>();
        }
    }
}

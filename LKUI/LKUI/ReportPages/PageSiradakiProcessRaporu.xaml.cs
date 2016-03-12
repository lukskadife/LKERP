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
using LKLibrary.Classes;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageSiradakiProcessRaporu.xaml
    /// </summary>
    public partial class PageSiradakiProcessRaporu : UserControl
    {
        public PageSiradakiProcessRaporu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSonrakiProcess.ItemsSource = null;
            DGridSonrakiProcess.ItemsSource = Rapor.SiradakiProcessGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridSonrakiProcess.ToExcel<vSiradakiProcessRaporu>();
        }
    }
}

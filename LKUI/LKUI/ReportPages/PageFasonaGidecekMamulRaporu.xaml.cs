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
using LKUI.Classes;
using LKLibrary.DbClasses;
using LKLibrary.Classes;
using Microsoft.Win32;
using System.IO;
using Telerik.Windows.Controls;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageFasonaGidecekMamulRaporu.xaml
    /// </summary>
    public partial class PageFasonaGidecekMamulRaporu : UserControl
    {
        public PageFasonaGidecekMamulRaporu()
        {
            InitializeComponent();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vMamulKumaslar>();
            //DGridRapor.ToExcel();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ItemsSource = Rapor.FasonaGonderilecekMamulleriGetir();
        }
    }
}

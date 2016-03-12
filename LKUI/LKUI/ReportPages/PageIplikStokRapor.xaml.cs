using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageIplikStokRapor.xaml
    /// </summary>
    public partial class PageIplikStokRapor : UserControl
    {
        public PageIplikStokRapor()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ItemsSource = Rapor.IplikStokRaporuGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vIplikStok>();
        }
    }
}

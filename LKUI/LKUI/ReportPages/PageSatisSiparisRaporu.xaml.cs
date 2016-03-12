using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageSatisSiparisRaporu.xaml
    /// </summary>
    public partial class PageSatisSiparisRaporu : UserControl
    {
        public PageSatisSiparisRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ItemsSource = Rapor.SatisSiparisRaporuGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vSatisSiparisRaporu>();
        }
    }
}

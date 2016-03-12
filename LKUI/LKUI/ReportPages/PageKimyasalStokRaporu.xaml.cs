using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageKimyasalStokRaporu.xaml
    /// </summary>
    public partial class PageKimyasalStokRaporu : UserControl
    {
        public PageKimyasalStokRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ItemsSource = Rapor.KimyasalStokRaporuGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vKimyasalStokRaporu>();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageTezgahPlanRaporu.xaml
    /// </summary>
    public partial class PageTezgahPlanRaporu : UserControl
    {
        public PageTezgahPlanRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridPlanRapor.ItemsSource = LKLibrary.Classes.Planlama.PlanRaporuGetir();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridPlanRapor.ToExcel<vPlanRapor>();
        }
    }
}

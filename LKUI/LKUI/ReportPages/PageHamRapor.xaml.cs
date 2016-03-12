using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageHamRapor.xaml
    /// </summary>
    public partial class PageHamRapor : UserControl
    {
        public PageHamRapor()
        {
            InitializeComponent();
        }

        IQueryable<vHamKumaslar> LstHamStok;
        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            LstHamStok = HamKumas.HamStoklariGetir();
            DGridHamStok.ItemsSource = LstHamStok.ToList();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridHamStok.ToExcel<vHamKumaslar>();
        }
    }
}

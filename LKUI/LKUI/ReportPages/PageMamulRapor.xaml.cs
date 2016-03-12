using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageMamulRapor.xaml
    /// </summary>
    public partial class PageMamulRapor : UserControl
    {
        public PageMamulRapor()
        {
            InitializeComponent();
        }

        public List<vMamulKumaslar> LstHamStok;

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            LstHamStok = Rapor.MamulStokRaporuGetir() ;
            DGridMamulStok.ItemsSource = LstHamStok;            
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridMamulStok.ToExcel<vMamulKumaslar>();
        }
    }
}

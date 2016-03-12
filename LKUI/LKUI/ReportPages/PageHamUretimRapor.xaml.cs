using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageHamUretimRapor.xaml
    /// </summary>
    public partial class PageHamUretimRapor : UserControl
    {
        public PageHamUretimRapor()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridHamStok.ItemsSource = Rapor.HamUretimRaporuGetir(DateIlkTarih.SelectedDate.Value.Date, DateSonTarih.SelectedDate.Value.Date);
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridHamStok.ToExcel<vHamUretimRaporu>();
        }
    }
}

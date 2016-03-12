using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageIplikGirisRaporu.xaml
    /// </summary>
    public partial class PageIplikGirisRaporu : UserControl
    {
        public PageIplikGirisRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DGridRapor.ItemsSource = Rapor.IplikGirisRaporuGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vIplikGirisRaporu>();
        }
    }
}

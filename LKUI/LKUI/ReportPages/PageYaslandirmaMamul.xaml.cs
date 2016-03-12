using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageYaslandirmaMamul.xaml
    /// </summary>
    public partial class PageYaslandirmaMamul : UserControl
    {
        public PageYaslandirmaMamul()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DGridMamulStok.ItemsSource = Rapor.MamulStokRaporuGetir().FindAll(c => DateIlkTarih.SelectedDate.Value <= c.Tarih && c.Tarih <= DateSonTarih.SelectedDate.Value).OrderBy(o => o.Tarih);
            List<vKonsolKumasRaporu> list = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabMamulStok");
            DGridTip.ItemsSource = list.FindAll(c => DateIlkTarih.SelectedDate.Value <= c.Tarih && c.Tarih <= DateSonTarih.SelectedDate.Value).OrderBy(o => o.Tarih); ;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridMamulStok.ToExcel();
        }

        private void MITipToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridTip.ToExcel();
        }
    }
}

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
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageYaslandirmaHam.xaml
    /// </summary>
    public partial class PageYaslandirmaHam : UserControl
    {
        public PageYaslandirmaHam()
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

            DGridHamStok.ItemsSource = HamKumas.HamStoklariGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value).OrderBy(o => o.Tarih);
            List<vKonsolKumasRaporu> list = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabHamStok");
            DGridTip.ItemsSource = list.FindAll(c => DateIlkTarih.SelectedDate.Value <= c.Tarih && c.Tarih <= DateSonTarih.SelectedDate.Value).OrderBy(o => o.Tarih);
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridHamStok.ToExcel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MITipToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridTip.ToExcel();
        }
    }
}

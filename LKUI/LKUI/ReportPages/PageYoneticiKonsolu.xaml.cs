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
using LKLibrary.DbClasses;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageYoneticiKonsolu.xaml
    /// </summary>
    public partial class PageYoneticiKonsolu : UserControl
    {
        public PageYoneticiKonsolu()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabBoyahaneDurus.IsSelected)
                {
                    DGridBoyahaneDurus.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolDuruslarRaporu>("TabBoyahaneDurus");
                }

                else if (TabBoyahaneRandiman.IsSelected)
                {
                    DGridBoyahaneRandiman.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<object>("TabBoyahaneRandiman");
                }

                else if (TabBoyahaneSiparisler.IsSelected)
                {
                    DGridBoyahaneSiparisler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolBoyaSiparisRaporu>("TabBoyahaneSiparisler");
                }

                else if (TabDokumaDurus.IsSelected)
                {
                    DGridDokumaDurus.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolDuruslarRaporu>("TabDokumaDurus");
                }

                else if (TabDokumaSiparisleri.IsSelected)
                {
                    DGridDokumaSiparisler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vPlanRapor>("TabDokumaSiparisleri");
                }

                else if (TabGunlukHamUretim.IsSelected)
                {
                    DGridGunlukHamUretim.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabGunlukHamUretim");
                }

                else if (TabGunlukMamulUretim.IsSelected)
                {
                    DGridGunlukMamulUretim.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabGunlukMamulUretim");
                }

                else if (TabOGunHamUretim.IsSelected)
                {
                    DGridOGunHamUretim.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabOGunHamUretim");
                }

                else if (TabOGunMamulUretim.IsSelected)
                {
                    DGridOGunMamulUretim.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabOGunMamulUretim");
                }

                else if (TabGunlukSevkiyat.IsSelected)
                {
                    DGridGunlukSevkiyat.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabGunlukSevkiyat");
                }
                else if (TabFasonSevkiyat.IsSelected)
                {
                    DGridGunlukSevkiyat.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabFasonSevkiyat");
                }

                else if (TabOGunSevkiyat.IsSelected)
                {
                    DGridOGunSevkiyat.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabOGunSevkiyat");
                }

                else if (TabHamStok.IsSelected)
                {
                    DGridHamStok.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabHamStok");
                }

                else if (TabKalanSiparisler.IsSelected)
                {
                    DGridKalanSiparisler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolSiparisRaporu>("TabKalanSiparisler");
                }

                else if (TabMamulStok.IsSelected)
                {
                    DGridMamulStok.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolKumasRaporu>("TabMamulStok");
                }

                else if (TabTerminGecikenler.IsSelected)
                {
                    DGridTerminGecikenler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolSiparisRaporu>("TabTerminGecikenler");
                }

                else if (TabTezgahRandiman.IsSelected)
                {
                    DGridTezgahRandiman.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<object>("TabTezgahRandiman");
                }

                else if (TabYeniSiparisler.IsSelected)
                {
                    DGridYeniSiparisler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolSiparisRaporu>("TabYeniSiparisler");
                }

                else if (TabOYeniSiparisler.IsSelected)
                {
                    DGridOYeniSiparisler.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vKonsolSiparisRaporu>("TabOYeniSiparisler");
                }

                else if (TabTezgahPlan.IsSelected)
                {
                    DtlChart.LoadPlan();
                }

                else if (TabTezgahSiparisPlan.IsSelected)
                {
                    DtlSiparisChart.LoadPlan();
                }
            }
        }

        private void MIKalanSiparislerToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridKalanSiparisler.ToExcel<vKonsolSiparisRaporu>();
        }

        private void MIDokumaSiparislerToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridDokumaSiparisler.ToExcel<object>();
        }

        private void MIBoyahaneSiparisleriToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridBoyahaneSiparisler.ToExcel<vKonsolBoyaSiparisRaporu>();
        }

        private void MITerminGecikenlerToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridTerminGecikenler.ToExcel<vKonsolSiparisRaporu>();
        }

        private void MIMamulStokToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridMamulStok.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIGunlukSevkiyat_Click(object sender, RoutedEventArgs e)
        {
            DGridGunlukSevkiyat.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIYeniSiparisler_Click(object sender, RoutedEventArgs e)
        {
            DGridYeniSiparisler.ToExcel<vKonsolSiparisRaporu>();
        }

        private void MIGunlukHamUretim_Click(object sender, RoutedEventArgs e)
        {
            DGridGunlukHamUretim.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIGunlukMamulUretim_Click(object sender, RoutedEventArgs e)
        {
            DGridGunlukMamulUretim.ToExcel<vKonsolKumasRaporu>();
        }

        private void MITezgahRandiman_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MIBoyahaneRandiman_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MIDokumaDurus_Click(object sender, RoutedEventArgs e)
        {
            DGridDokumaDurus.ToExcel<vKonsolDuruslarRaporu>();
        }

        private void MIBoyahaneDurus_Click(object sender, RoutedEventArgs e)
        {
            DGridBoyahaneDurus.ToExcel<vKonsolDuruslarRaporu>();
        }

        private void MIHamStokToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridHamStok.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIOYeniSiparisler_Click(object sender, RoutedEventArgs e)
        {
            DGridOYeniSiparisler.ToExcel<vKonsolSiparisRaporu>();
        }

        private void MIOGunlukSevkiyat_Click(object sender, RoutedEventArgs e)
        {
            DGridOGunSevkiyat.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIOGunlukHamUretim_Click(object sender, RoutedEventArgs e)
        {
            DGridOGunHamUretim.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIOGunlukMamulUretim_Click(object sender, RoutedEventArgs e)
        {
            DGridOGunMamulUretim.ToExcel<vKonsolKumasRaporu>();
        }

        private void MIFasonSevkiyat_Click(object sender, RoutedEventArgs e)
        {
            DGridFasonSevkiyat.ToExcel<vKonsolKumasRaporu>();
        }
    }
}

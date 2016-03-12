using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlEtiket.xaml
    /// </summary>
    public partial class DtlEtiket : UserControl
    {
        public DtlEtiket()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();
            CmbEtiket.ItemsSource = Mamul.MamulEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0;

            TxtBarkod.Focus();
        }

        private void TxtBarkod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GrdBarkod.DataContext = Mamul.BarkodGetir(TxtBarkod.Text);
            }
        }

        private void BtnYazdir_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar mamul = GrdBarkod.DataContext as vMamulKumaslar;
            if (mamul == null)
            {
                MessageBox.Show("Barkod okutulmamış.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            tblAyarlar raporTaslak = CmbEtiket.SelectedItem as tblAyarlar;

            if (raporTaslak == null || string.IsNullOrEmpty(raporTaslak.Adi))
            {
                MessageBox.Show("Rapor seçili değil.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (raporTaslak != null)
            {
                DtlRapor rapor = new DtlRapor();
                DtlRapor.RaporItem item1 = new DtlRapor.RaporItem("DS_Mamul", new List<vMamulKumaslar>() { mamul });
                rapor.RaporYazdir(raporTaslak.Adi, new List<DtlRapor.RaporItem>() { item1 });
            }
            else MessageBox.Show("Yazdırmada hata oluştu..!\n\nEtiket bulunamadı.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}

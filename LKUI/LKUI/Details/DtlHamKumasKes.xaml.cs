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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlHamKumasKes.xaml
    /// </summary>
    public partial class DtlHamKumasKes : UserControl
    {
        public DtlHamKumasKes()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbEtiket.ItemsSource = HamKumas.HamEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0;
            TxtBarkod.Focus();
        }

        HamKesim _KesimIslem;

        private void TxtBarkod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    _KesimIslem = new HamKesim();
                    _KesimIslem.BarkodOkut(TxtBarkod.Text);
                    GrdAna.DataContext = _KesimIslem.AnaKumas;
                    GrdParca.DataContext = _KesimIslem.ParcaKumas;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void TxtKesilenMt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GrdParca.DataContext != null)
            {
                _KesimIslem.AnaKumas = GrdParca.DataContext as vHamKumaslar;
                TxtKesilenKg.Text = _KesimIslem.ParcaKgHesapla().ToString();
            }
        }

        private void TxtKesilenMt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKesilenMt.SelectAll();
        }

        private void TxtKesilenKg_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKesilenMt.SelectAll();
        }

        public void HamEtiketYazdir(vHamKumaslar ham)
        {
            DtlRapor raporlama = new DtlRapor();
            List<DtlRapor.RaporItem> items = new List<DtlRapor.RaporItem>() { new DtlRapor.RaporItem("DS_HamEtiket", new List<vHamKumaslar>() { ham }) };
            tblAyarlar raporTaslak = CmbEtiket.SelectedItem as tblAyarlar;

            if (raporTaslak == null || string.IsNullOrEmpty(raporTaslak.Adi))
            {
                MessageBox.Show("Rapor seçili değil.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (raporlama.RaporYazdir(raporTaslak.Adi, items) == false)
                MessageBox.Show("Hata oluştu.\n\nEtiket yazdırılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnKes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _KesimIslem.AnaKumas = GrdAna.DataContext as vHamKumaslar;
                if (TxtKesilenKg.TextGirisiDogruMu == false | TxtKesilenMt.TextGirisiDogruMu == false)
                {
                    MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (_KesimIslem == null) throw new Exception("Barkod okutulmamış..!");

                if (_KesimIslem.Parcala())
                {
                    MessageBox.Show((GrdAna.DataContext as vHamKumaslar).Barkod + " barkodlu kumaş " + (GrdAna.DataContext as vHamKumaslar).Metre.ToString() + " mt. ve " +
                        (GrdParca.DataContext as vHamKumaslar).Metre.ToString() + " mt olarak kesildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);

                    if (ChckYazdir.IsChecked.Value)
                    {
                        HamEtiketYazdir(_KesimIslem.AnaKumas);
                        HamEtiketYazdir(_KesimIslem.ParcaKumas);
                    }

                    GrdAna.DataContext = null;
                    GrdParca.DataContext = null;
                    TxtBarkod.Text = "";
                    TxtBarkod.Focus();
                }
                else MessageBox.Show("Hata oluştu.\n\nKesilemedi..!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}

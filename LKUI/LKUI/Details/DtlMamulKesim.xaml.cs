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
    /// Interaction logic for DtlMamulKesim.xaml
    /// </summary>
    public partial class DtlMamulKesim : UserControl
    {
        public DtlMamulKesim()
        {
            InitializeComponent();
        }

        MamulKesim _KesimIslem;

        private void TxtBarkod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    _KesimIslem = new MamulKesim();
                    _KesimIslem.BarkodOkut(TxtBarkod.Text);
                    GrdAnaMamul.DataContext = _KesimIslem.AnaMamul;
                    GrdParca.DataContext = _KesimIslem.ParcaMamul;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }                
            }
        }

        public void MamulEtiketYazdir(vMamulKumaslar mamul)
        {
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

        private void BtnKes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtKesilenKg.TextGirisiDogruMu == false | TxtKesilenMt.TextGirisiDogruMu == false)
                {
                    MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (_KesimIslem == null) throw new Exception("Barkod okutulmamış..!");

                if (_KesimIslem.Parcala())
                {
                    MessageBox.Show((GrdAnaMamul.DataContext as vMamulKumaslar).Barkod + " barkodlu kumaş " + (GrdAnaMamul.DataContext as vMamulKumaslar).Metre.ToString() + " mt. ve " +
                        (GrdParca.DataContext as vMamulKumaslar).Metre.ToString() + " mt olarak kesildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);

                    if (ChckYazdir.IsChecked.Value && _KesimIslem.AnaMamul.Durum != "Kesilen")
                    {
                        MamulEtiketYazdir(_KesimIslem.AnaMamul);
                        MamulEtiketYazdir(_KesimIslem.AnaMamul);
                    }

                    GrdAnaMamul.DataContext = null;
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

        private void TxtKesilenMt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKesilenMt.SelectAll();
        }

        private void TxtKesilenKg_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKesilenKg.SelectAll();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbEtiket.ItemsSource = Mamul.MamulEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0;
            TxtBarkod.Focus();
        }

        private void TxtKesilenMt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GrdParca.DataContext != null)
            {
                _KesimIslem.ParcaMamul = GrdParca.DataContext as vMamulKumaslar;
                TxtKesilenKg.Text = (_KesimIslem != null && _KesimIslem.ParcaMamul != null && _KesimIslem.ParcaMamul.Kg != null) ? _KesimIslem.ParcaMamul.Kg.ToString() : "0";
            }
        }
    }
}

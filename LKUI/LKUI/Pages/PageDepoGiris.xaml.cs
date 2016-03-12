using System.Windows.Controls;
using System.Windows.Input;
using LKUI.Details;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Linq;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageDepoGiris.xaml
    /// </summary>
    public partial class PageDepoGiris : UserControl
    {
        private tblDurumlar _Durum;
        public PageDepoGiris()
        {
            InitializeComponent();

            this._Durum = new tblDurumlar().DurumGetir(App.ClickedMenuItemId);
        }

        MalzemeTalep _Talep = new MalzemeTalep();
        List<vTalepKarsilama> _ListTalepKarsilama;
        private void TaleplerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vTalepKarsilama secilen = DGridTalepKarsilama.SelectedItem as vTalepKarsilama;
            if (DGridTalepKarsilama.SelectedItem == null) return;
            DGridKarsilananlar.ItemsSource = new vTalepStokGiris().GetTalepKarsilama(secilen.Id);
            if (DGridKarsilananlar.ItemsSource != null && (DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).FindAll(c => c.MalzemeGirisId == 0).Count > 0) BtnOnayla.IsEnabled = false;
            else if (DGridKarsilananlar.ItemsSource != null)
            {
                BtnOnayla.IsEnabled = true;
                if ((DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).Exists(c => c.MalzemeBagId == 39))
                {
                    DClmAmbalaj.Visibility = System.Windows.Visibility.Visible;
                    DClmLotNo.Visibility = System.Windows.Visibility.Visible;
                    DClmRenk.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    DClmAmbalaj.Visibility = System.Windows.Visibility.Hidden;
                    DClmLotNo.Visibility = System.Windows.Visibility.Hidden;
                    DClmRenk.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            ChildSatinAlma.Caption = "No : " + secilen.No + "   -   Tedarikçi : " + secilen.TedarikciAdi;
            ChildSatinAlma.Show();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _ListTalepKarsilama = _Talep.KarsilamaFormlariGetirWithDurum(this._Durum.Id);
            LoadPage();
        }

        private void LoadPage()
        {
            DGridTalepKarsilama.ItemsSource = _Talep.KarsilamaFormlariGetirWithDurum(this._Durum.Id);
        }

        private void ChildSatinAlma_Closed(object sender, System.EventArgs e)
        {
            LoadPage();
        }

        private void TxtTedarikciKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.TedarikciKodu.ToUpper().Contains(TxtTedarikciKodu.Text.ToUpper()));

        }

        private void TxtTedarikciAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.TedarikciAdi.ToUpper().Contains(TxtTedarikciAdi.Text.ToUpper()));
        }

        private void TxtTalepEdenKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.PersonelKodu.Contains(TxtTalepEdenKodu.Text));
        }

        private void TxtTalepEdenAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.PersonelAdi.ToUpper().Contains(TxtTalepEdenAdi.Text.ToUpper()));
        }


        private void BtnYazdir_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnOnayla_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            vTalepKarsilama karsilamaFormu = DGridTalepKarsilama.SelectedItem as vTalepKarsilama;
            if (karsilamaFormu == null) return;
            if (_Talep.TaratilanBelgeKontrolu(karsilamaFormu.Id) == false)
            {
                MessageBox.Show("Belge eksik...\n\nOnaylanamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            int eksiklerCount = (DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).FindAll(c => c.Miktar > c.AlinanMiktar).Count;
            if (eksiklerCount > 0)
            {
                MessageBox.Show("Eksik olan malzemeler var.\n\nOnaylanamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_Talep.TalepFormDurumuGuncelle(karsilamaFormu, _Talep.YeniDurumGetir(karsilamaFormu.DurumId)))
            {
                MessageBox.Show("Onaylandı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                ChildSatinAlma.Close();
            }
            else MessageBox.Show("Onaylamada hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnFaturaEkle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "FATURA SEÇ";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                vTalepKarsilama secilen = ((System.Windows.FrameworkElement)(sender)).DataContext as vTalepKarsilama;
                if (_Talep.KarsilamaBelgesiEkle(secilenDosya, secilen.Id, MalzemeTalep.Belge.Fatura))
                    MessageBox.Show("Dosya eklendi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Hata..!\n\nDosya eklenemedi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if ((DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).Count <= 0) return;

            if ((DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).Exists(c => c.AlinanMiktar > c.Miktar))
            {
                MessageBox.Show("Depo giriş miktarı talep miktarından fazla olamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            bool sonuc = new Stok().StokGirisleriYap(DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>, App.PersonelId);
            if (sonuc)
            {
                MessageBox.Show("Stok girişleri yapıldı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                BtnOnayla.IsEnabled = true;
                vTalepStokGiris temp = (DGridKarsilananlar.ItemsSource as List<vTalepStokGiris>).FirstOrDefault();
                DGridKarsilananlar.ItemsSource = new vTalepStokGiris().GetTalepKarsilama(temp.TalepKarsilamaId);
            }
            else MessageBox.Show("Hata..!\n\nStok girişleri yapılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnIrsaliyeEkle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "İRSALİYE SEÇ";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                vTalepKarsilama secilen = ((System.Windows.FrameworkElement)(sender)).DataContext as vTalepKarsilama;
                if (_Talep.KarsilamaBelgesiEkle(secilenDosya, secilen.Id, MalzemeTalep.Belge.Irsaliye))
                    MessageBox.Show("Dosya eklendi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Hata..!\n\nDosya eklenemedi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDepoKaydet_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}

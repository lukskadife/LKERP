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
using System.ComponentModel;
using LKUI.Details;
using System.Reflection;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageHamKumasGirisi.xaml
    /// </summary>
    public partial class PageHamKumasGirisi : UserControl
    {
        public PageHamKumasGirisi()
        {
            InitializeComponent();
        }

        HamKumas _KumasIslem;
        TezgahHaberlesme _Tezgah;
        tblHamHatalari _Hata = new tblHamHatalari();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbDokumaci.ItemsSource = HamKumas.DokumacilariGetir();
            //CmbEtiket.ItemsSource = HamKumas.EtiketleriGetir();
            CmbHata.ItemsSource = HamKumas.HataTanimlariGetir();
            CmbKaliteci.ItemsSource = HamKumas.KalitecileriGetir();
            CmbTezgahNo.ItemsSource = HamKumas.TezgahlariGetir();
            CmbTipNo.ItemsSource = HamKumas.TipleriGetir();

            //Tezgah haberleşmesi hazırlanıyor.
            _Tezgah = new TezgahHaberlesme();
            _Tezgah.TezgahHareketEtti += new TezgahHaberlesme.TezgahEvent(_Tezgah_TezgahHareketEtti);
            if (_Tezgah.HazirMi == false) MessageBox.Show("Tezgah ile iletişim yok..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

            _KumasIslem = new HamKumas("Ham") { Tarih = DateTime.Today };
            GrdUst.DataContext = _KumasIslem;
            GrdHata.DataContext = _Hata;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;

            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;

            CmbEtiket.ItemsSource = HamKumas.HamEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0;
        }

        void _Tezgah_TezgahHareketEtti()
        {
            string snc =_Tezgah.ReturnDeger;
            TxtKacinciMetre.Text = _Tezgah.ReturnDeger;
        }

        private void EtiketYazdir(vHamKumaslar ham)
        {
            DtlRapor raporlama = new DtlRapor();
            List<DtlRapor.RaporItem> items = new List<DtlRapor.RaporItem>() { new DtlRapor.RaporItem("DS_HamEtiket", new List<vHamKumaslar>(){ham})};
            tblAyarlar raporTaslak = CmbEtiket.SelectedItem as tblAyarlar;

            if (raporTaslak == null || string.IsNullOrEmpty(raporTaslak.Adi))
            {
                MessageBox.Show("Rapor seçili değil.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (raporlama.RaporYazdir(raporTaslak.Adi, items) == false)
                MessageBox.Show("Hata oluştu.\n\nEtiket yazdırılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void HamKaydet()
        {
            BtnKaydet.Focus();
            if (_KumasIslem.AltZeminLevent == null || _KumasIslem.AltZeminLevent.LeventNo != TxtAltZeminSonuc.Text)
            {
                MessageBox.Show("Alt zemin levent girişi doğru değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_KumasIslem.UstZeminLevent == null || _KumasIslem.UstZeminLevent.LeventNo != TxtUstZeminSonuc.Text)
            {
                MessageBox.Show("Üst zemin levent girişi doğru değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_KumasIslem.HavLevent == null || _KumasIslem.HavLevent.LeventNo != TxtHavSonuc.Text)
            {
                MessageBox.Show("Hav levent girişi doğru değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            try
            {
                if (TxtMetreAlt.TextGirisiDogruMu == false | TxtMetreÜst.TextGirisiDogruMu == false | TxtVaryant.TextGirisiDogruMu == false | TxtKgAlt.TextGirisiDogruMu == false
                    | TxtKgÜst.TextGirisiDogruMu == false ) return;

                if (_KumasIslem.HamKumasKaydet() == false)
                {
                    MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (ChckEtiket.IsChecked.Value)
                {
                    EtiketYazdir(HamKumas.HamKumasGetir(_KumasIslem.KumasUst.Id, true));
                    System.Threading.Thread.Sleep(2000);
                    EtiketYazdir(HamKumas.HamKumasGetir(_KumasIslem.KumasAlt.Id, true));
                }
                LoadPage();
                _KumasIslem = new HamKumas("Ham") { Tarih = DateTime.Today, KaliteciId = _KumasIslem.KaliteciId };
                GrdUst.DataContext = _KumasIslem;
                GrdHata.DataContext = _Hata;
                DGridHata.ItemsSource = _KumasIslem.Hatalar;
                Refresh();
                TxtHavSonuc.Text = "";
                TxtAltZeminSonuc.Text = "";
                TxtUstZeminSonuc.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            HamKaydet();
        }

        private void HataEkle()
        {
            if (CmbHata.SelectedItem == null | TxtUzunluk.TextGirisiDogruMu == false | TxtKacinciMetre.TextGirisiDogruMu == false) return;
            tblHamHatalari ekle = GrdHata.DataContext as tblHamHatalari;
            ekle.Metresi = TxtKacinciMetre.DoubleTxt == null ? 0 : TxtKacinciMetre.DoubleTxt.Value;
            ekle.HataKodu = (CmbHata.SelectedItem as vHataTanim).Kodu;
            ekle.HataAdi = (CmbHata.SelectedItem as vHataTanim).Adi;
            ekle.HataAltVarMi = true;
            ekle.HataUstVarMi = true;

            if (_KumasIslem.Hatalar != null)
                if (_KumasIslem.HataEkle(ekle))
                {
                    DGridHata.Items.Refresh();
                    _Hata = new tblHamHatalari() { Metresi = _Hata.Metresi };
                    GrdHata.DataContext = _Hata;
                }

            Refresh();
            TxtUzunluk.Focus();
        }

        public void Refresh()
        {
            StackAlt.DataContext = null;
            StackAlt.DataContext = _KumasIslem.KumasAlt;

            StackUst.DataContext = null;
            StackUst.DataContext = _KumasIslem.KumasUst;

            DGridHata.ItemsSource = null;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;
        }

        private void BtnHataEkle_Click(object sender, RoutedEventArgs e)
        {
            HataEkle();
        }

        private void BtnHataSil_Click(object sender, RoutedEventArgs e)
        {
            HataSil();
        }

        private void HataSil()
        {
            if (DGridHata.SelectedItem == null) return;
            _KumasIslem.HataSil(DGridHata.SelectedItem as tblHamHatalari);
            DGridHata.Items.Refresh();
            Refresh();
        }

        private void GrdHata_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void LoadPage()
        {
            if (this.IsLoaded)
            {
                if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;
                DGridTartımDetayları.ItemsSource = HamKumas.HamKayitlariGetir(DPBaslangic.SelectedDate.Value.Date, DPBitis.SelectedDate.Value.Date).OrderByDescending(o => o.Id).ToList();
            }
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridTartımDetayları.SelectedItem == null) return;
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;

            if (MessageBox.Show("Kayıt silinsin mi ?\n\nBarkod : " + secilen.Barkod, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (HamKumas.HamKumasSil(secilen)) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            _KumasIslem = new HamKumas("Ham") { Tarih = DateTime.Today };
            GrdUst.DataContext = _KumasIslem;
            GrdHata.DataContext = _Hata;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;
            Refresh();
        }

        private void BtnDüzelt_Click(object sender, RoutedEventArgs e)
        {
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;
            if (secilen == null) return;

            CmbDuzeltKaliteci.ItemsSource = HamKumas.KalitecileriGetir();
            CmbDuzeltTezgahNo.ItemsSource = HamKumas.TezgahlariGetir();
            CmbDuzeltTipNo.ItemsSource = HamKumas.TipleriGetir();
            CmbDuzeltKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();

            childDuzelt.DataContext = secilen.ViewToTbl();
            childDuzelt.Show();
        }

        private void CmbTipNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_KumasIslem.UstZeminLevent != null) TxtUstZemin.Text = _KumasIslem.UstZeminLevent.LeventNo;
            else TxtUstZemin.Text = "";
            if (_KumasIslem.AltZeminLevent != null) TxtAltZemin.Text = _KumasIslem.AltZeminLevent.LeventNo;
            else TxtAltZemin.Text = "";
            if (_KumasIslem.HavLevent != null) TxtHav.Text = _KumasIslem.HavLevent.LeventNo;
            else TxtHav.Text = "";
        }

        private void CmbTezgahNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_KumasIslem.UstZeminLevent != null) TxtUstZemin.Text = _KumasIslem.UstZeminLevent.LeventNo;
            else TxtUstZemin.Text = "";
            if (_KumasIslem.AltZeminLevent != null) TxtAltZemin.Text = _KumasIslem.AltZeminLevent.LeventNo;
            else TxtAltZemin.Text = "";
            if (_KumasIslem.HavLevent != null) TxtHav.Text = _KumasIslem.HavLevent.LeventNo;
            else TxtHav.Text = "";
        }

        private void MIHamEtiketYazdir_Click(object sender, RoutedEventArgs e)
        {
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;

            if (secilen == null) return;
            EtiketYazdir(HamKumas.HamKumasGetir(secilen.Id, true));
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                HataSil();
            }
            else if (e.Key == Key.F3)
            {
                HataEkle();
            }
            else if (e.Key == Key.F9)
            {
                HamKaydet();
            }
            else if (e.Key == Key.F2) TxtUzunluk.Focus();
        }

        private void CmbHata_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HataEkle();
        }

        private void TxtKacinciMetre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TxtUzunluk.Focus();
        }

        private void TxtUzunluk_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TxtUzunluk.TextGirisiDogruMu) CmbHata.Focus();
        }

        private void TxtMetreAlt_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMetreAlt.SelectAll();
        }

        private void TxtKgAlt_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKgAlt.SelectAll();
        }

        private void TxtGramajAlt_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtGramajAlt.SelectAll();
        }

        private void TxtMetreÜst_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMetreÜst.SelectAll();
        }

        private void TxtKgÜst_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKgÜst.SelectAll();
        }

        private void TxtGramajUst_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtGramajUst.SelectAll();
        }

        private void BtnDuzeltKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (HamKumas.HamKumasDuzelt(childDuzelt.DataContext as tblHamKumaslar))
            {
                LoadPage();
                childDuzelt.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nDüzeltilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MIHamHataHaritasiYazdir_Click(object sender, RoutedEventArgs e)
        {
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;
            if (secilen == null) return;

            LKUI.Details.DtlRapor raporPage = new LKUI.Details.DtlRapor();
            bool rprTamam = raporPage.RaporGoster("RprHamHataHaritasi", new List<DtlRapor.RaporItem>() 
            { 
                new DtlRapor.RaporItem( "DS_HataHaritasi", HamKumas.HataHaritasiGetir(secilen))
            });

            if (rprTamam == false) return;

            childgenel.Content = raporPage;
            childgenel.Show();
        }

        private void CmbHata_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CmbHata.IsDropDownOpen == false) CmbHata.IsDropDownOpen = true;
            }
            catch { }
        }

        private void TxtMetreAlt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetreAlt.SelectAll();
        }

        private void TxtMetreÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetreÜst.SelectAll();
        }

        private void TxtKgÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKgÜst.SelectAll();
        }

        private void TxtKgAlt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKgAlt.SelectAll();
        }

        private void TxtGramajAlt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtGramajAlt.SelectAll();
        }

        private void TxtGramajUst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtGramajUst.SelectAll();
        }

        private void TxtHataAdetAlt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataAdetAlt.SelectAll();
        }

        private void TxtHataAdetÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataAdetÜst.SelectAll();
        }

        private void TxtHataPuanÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataPuanÜst.SelectAll();
        }

        private void TxtHataPuanAlt_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataPuanAlt.SelectAll();
        }
    }
}

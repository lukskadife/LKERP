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
    /// Interaction logic for PageMamulKumasGirisi.xaml
    /// </summary>
    public partial class PageMamulKumasGirisi : UserControl
    {
        public PageMamulKumasGirisi()
        {
            InitializeComponent();
        }

        Mamul _Islem;
        TezgahHaberlesme _Tezgah;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangicTarihi.SelectedDate = DateTime.Today;
            DPBitisTarihi.SelectedDate = DateTime.Today;
            CmbHata.ItemsSource = Mamul.MamulHatalariGetir();            
            CmbKaliteci.ItemsSource = Mamul.KalitecileriGetir();

            _Tezgah = new TezgahHaberlesme();
            _Tezgah.TezgahHareketEtti += new TezgahHaberlesme.TezgahEvent(_Tezgah_TezgahHareketEtti);
            if (_Tezgah.HazirMi == false) MessageBox.Show("Tezgah ile iletişim yok..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

            DPTarih.SelectedDate = DateTime.Today;
            _Islem = new Mamul();

            CmbEtiket.ItemsSource = Mamul.MamulEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0;

            Refresh();
        }

        void _Tezgah_TezgahHareketEtti()
        {
            string snc = _Tezgah.ReturnDeger;
            TxtKacinciMetre.Text = _Tezgah.ReturnDeger;
        }

        private void Refresh()
        {
            GrdKumas.DataContext = null;
            GrdKumas.DataContext = _Islem.YeniMamulKumas;
            this.DataContext = _Islem;
            DGridBarkotBirlestirme.ItemsSource = null;
            DGridBarkotBirlestirme.ItemsSource = _Islem.MamulKumaslar;
            DGridParttilenenBarkotlar.ItemsSource = null;
            DGridParttilenenBarkotlar.ItemsSource = _Islem.PartiHamKumaslari;
            DGridHataEkle.Items.Refresh();            
            if (_Islem.Partisi != null && _Islem.Partisi.RePartiMi) 
                CmbHamBarkot.ItemsSource = _Islem.RePartiKumaslari;
            else CmbHamBarkot.ItemsSource = _Islem.PartiHamKumaslari;

            if (CmbHamBarkot.SelectedItem == null) TxtHamKalan.Content = "";
        }

        private void LoadPage()
        {
            if (DPBitisTarihi.SelectedDate == null || DPBaslangicTarihi.SelectedDate == null) return;

            DGridMamuller.ItemsSource = Mamul.MamulleriGetir(DPBaslangicTarihi.SelectedDate.Value, DPBitisTarihi.SelectedDate.Value).OrderByDescending(o => o.Id).ToList();
        }

        private void DPBaslangicTarihi_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitisTarihi_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void TxtBarkot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _Islem = new Mamul();
                try
                {
                    _Islem.BarkodOkut(TxtBarkot.Text);
                    Refresh();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void HataEkle()
        {
            vHataTanim hata = CmbHata.SelectedItem as vHataTanim;
            if (hata == null) return;

            if (TxtKacinciMetre.TextGirisiDogruMu == false | TxtUzunluk.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Lütfen kırmızı alanları doldurunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            try
            {
                bool snc = _Islem.HataEkle(new LKLibrary.DbClasses.tblMamulHatalari()
                {
                    HataId = hata.Id,
                    HataAdi = hata.Adi,
                    HataKodu = hata.Kodu,
                    Metresi = TxtKacinciMetre.DoubleTxt == null ? 0 : TxtKacinciMetre.DoubleTxt.Value,
                    Uzunluk = TxtUzunluk.DoubleTxt == null ? 0 : (TxtUzunluk.DoubleTxt.Value)
                });

                if (snc)
                {
                    TxtUzunluk.Text = "";
                    TxtUzunluk.Focus();
                    CmbHata.SelectedItem = null;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            Refresh();
        }

        private void BtnHataEkle_Click(object sender, RoutedEventArgs e)
        {
            HataEkle();
        }

        private void HataSil()
        {
            tblMamulHatalari secilen = DGridHataEkle.SelectedItem as tblMamulHatalari;
            if (secilen == null) return;

            if (MessageBox.Show(string.Format("Silinsin mi ?\n\nHata : {0}\nMetresi : {1}", secilen.HataAdi, secilen.Metresi), App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.HataSil(secilen)) Refresh();
            else MessageBox.Show("Hata oluştu.\n\nsilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnHataSil_Click(object sender, RoutedEventArgs e)
        {
            HataSil();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vMamulKumaslar secilen = DGridMamuller.SelectedItem as vMamulKumaslar;
                if (secilen == null) return;

                if (MessageBox.Show(string.Format("Silinsin mi ?\n\nBarkod : {0}", secilen.Barkod), App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;

                _Islem.MamulSil(secilen);
                LoadPage();
                Refresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
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

        public void KumasKes(bool parcaMi)
        {
            try
            {
                if (!TxtMetre.TextGirisiDogruMu | !TxtEn.TextGirisiDogruMu | CmbKaliteci.GirisYapildiMi == false | CmbHamBarkot.GirisYapildiMi == false | TxtKg.TextGirisiDogruMu == false | CmbTur.GirisYapildiMi == false)
                {
                    MessageBox.Show("Lütfen kırmızı alanları doldurunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                string tip = _Islem.YeniMamulKumas.TipNo, metre = _Islem.YeniMamulKumas.Metre.ToString();


                vMamulKumaslar yazdir = _Islem.YeniMamulKumas;
                List<tblMamulHatalari> hataYazdir = _Islem.Hatalar;
                if (_Islem.KumasKes(parcaMi))
                {
                    if (ChckYazdir.IsChecked.HasValue && ChckYazdir.IsChecked.Value && parcaMi == false)
                    {
                        MamulEtiketYazdir(yazdir);
                        System.Threading.Thread.Sleep(2000);
                        MamulEtiketYazdir(yazdir);
                    }
                    if (ChckHarita.IsChecked.HasValue && ChckHarita.IsChecked.Value && parcaMi == false)
                    {
                        DtlRapor.RaporItem item1 = new DtlRapor.RaporItem("DS_HataHaritasi", hataYazdir);
                        new DtlRapor().RaporYazdir("RprMamulHataHaritasi", new List<DtlRapor.RaporItem>() { item1 });
                    }

                    LoadPage();

                    _Islem = new Mamul() { YeniMamulKumas = _Islem.YeniMamulKumas, PartiHamKumaslari = _Islem.PartiHamKumaslari, RePartiKumaslari = _Islem.RePartiKumaslari , MamulKumaslar = _Islem.MamulKumaslar, Partisi = _Islem.Partisi, FasonamiGidecek = _Islem.FasonamiGidecek};
                    Refresh();
                }
                else MessageBox.Show("İşlem başarısız.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                if (exp.Message == "Girilen Kg değerini kontrol ediniz!") { TxtKg.Focus(); TxtKg.SelectAll(); }
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnKes_Click(object sender, RoutedEventArgs e)
        {
            KumasKes(false);
        }

        private void BtnKalite_Click(object sender, RoutedEventArgs e)
        {
            KaliteHesapla();
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            _Islem = new Mamul();
            Refresh();
        }

        private void CmbHamBarkot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridHamHatalari.ItemsSource = null;

            if (CmbHamBarkot.SelectedItem is vReProcessBarkodlari)
            {
                vReProcessBarkodlari mamul = CmbHamBarkot.SelectedItem as vReProcessBarkodlari;
                CmbTur.SelectedValue = mamul.Tur;
            }

            else
            {
                vHamKumaslar secilen = CmbHamBarkot.SelectedItem as vHamKumaslar;
                if (secilen == null) return;

                if (_Islem != null && _Islem.YeniMamulKumas != null)
                {
                    TxtHamKalan.Content = _Islem.HamKalanMiktarGetir(secilen.Id).ToString();
                    _Islem.YeniMamulKumas.Tur = secilen.Tur;
                    CmbTur.SelectedValue = secilen.Tur;
                    bool turUstMu = secilen.Tur == "Ust" ? true : false;
                    DGridHamHatalari.ItemsSource = HamKumas.HamHatalariGetir(secilen.Id, turUstMu);
                }
            }
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void SboxHata_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HataEkle();
        }

        private void KaliteHesapla()
        {
            if (!TxtMetre.TextGirisiDogruMu | !TxtEn.TextGirisiDogruMu | CmbKaliteci.GirisYapildiMi == false | CmbHamBarkot.GirisYapildiMi == false | TxtKg.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Lütfen kırmızı alanları doldurunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            _Islem.HataPuanlariHesapla();
            _Islem.KaliteHesapla();
            Refresh();
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //kalite hesaplama
                case Key.F8:
                    BtnKalite.Focus();
                    KaliteHesapla();
                    break;

                //kumaş kes
                case Key.F5:
                    BtnKes.Focus();
                    KumasKes(false);
                    break;

                //kumaş parça
                case Key.F6:
                    BtnParca.Focus();
                    KumasKes(true);
                    break;

                case Key.F3:
                    HataEkle();
                    break;

                case Key.F4:
                    HataSil();
                    break;

                case Key.F2:
                    TxtUzunluk.Focus();
                    break;
            }
        }

        private void MIMamulYazdir_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridMamuller.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;
            //vHamKumaslar ham = secilen.HamId == null ? null : HamKumas.HamKumasGetir(secilen.HamId.Value, true);
            //secilen.LotNo = ham == null ? null : ham.LotNo;
            MamulEtiketYazdir(secilen);
        }

        private void BtnParca_Click(object sender, RoutedEventArgs e)
        {
            KumasKes(true);
        }

        private void BtnBirlestir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Islem.BarkodBirlestir())
                {
                    LoadPage();
                    DGridBarkotBirlestirme.ItemsSource = _Islem.MamulKumaslar;
                    if (_Islem.BirlestirilenKumas != null)
                    {
                        MamulEtiketYazdir(_Islem.BirlestirilenKumas);
                        MamulEtiketYazdir(_Islem.BirlestirilenKumas);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void MIHataHaritasi_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridMamuller.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;

            LKUI.Details.DtlRapor raporPage = new LKUI.Details.DtlRapor();
            bool rprTamam = raporPage.RaporGoster("RprMamulHataHaritasi", new List<DtlRapor.RaporItem>() 
            { 
                new DtlRapor.RaporItem( "DS_HataHaritasi", Mamul.MamulHataHaritasiGetir(secilen.Id))
            });

            if (rprTamam == false) return;

            ChildGenel.Content = raporPage;
            ChildGenel.Show();
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ClmHamHata.Width.Value > 0) ClmHamHata.Width = new GridLength(0);
            else ClmHamHata.Width = new GridLength(100, GridUnitType.Star);
        }

        private void TxtKacinciMetre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TxtUzunluk.Focus();
        }

        private void TxtUzunluk_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TxtUzunluk.TextGirisiDogruMu) CmbHata.Focus();
        }

        private void CmbHata_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HataEkle();
        }

        private void TxtMetre_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMetre.SelectAll();
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridMamuller.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;

            if (secilen.SevkId.HasValue && secilen.SevkId.Value != 0)
            {
                MessageBox.Show("Barkod sevk edilmiş.\n\nDüzeltilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            CmbDuzeltKaliteci.ItemsSource = Mamul.KalitecileriGetir();
            CmbDuzeltTipNo.ItemsSource = vKumas.KumaslariGetir(true);
            CmbDuzeltKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();

            childDuzelt.DataContext = secilen.ViewToTable();
            childDuzelt.Show();
        }

        private void BtnDuzeltKaydet_Click(object sender, RoutedEventArgs e)
        {
            tblMamulKumaslar mamul = childDuzelt.DataContext as tblMamulKumaslar;
            if (mamul == null) return;

            if (Mamul.MamulDuzelt(mamul))
            {
                LoadPage();
                childDuzelt.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nDüzeltilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TxtKg_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKg.Focus();
            TxtKg.SelectAll();
        }

        private void TxtDuzeltKg_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtDuzeltKg.SelectAll();
        }

        private void CmbHata_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CmbHata.IsDropDownOpen == false) CmbHata.IsDropDownOpen = true;
            }
            catch
            {
            }
        }

        private void TxtMetre_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetre.Focus();
            TxtMetre.SelectAll();
        }

        private void TxtHataAdet_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataAdet.Focus();
            TxtHataAdet.SelectAll();
        }

        private void TxtHataPuan_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtHataPuan.Focus();
            TxtHataPuan.SelectAll();
        }
    }
}

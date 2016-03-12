using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageIplikGiris.xaml
    /// </summary>
    public partial class PageIplikGiris : UserControl
    {
        public PageIplikGiris()
        {
            InitializeComponent();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            if (_SecilenTip == "")
            {
                MessageBox.Show("Tip seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            _IplikIslem = new Iplik(Enums.Hareketler.IplikGiris);
            DGridIplik.ItemsSource = Iplikler;
            DpTarih.SelectedDate = DateTime.Today;
            DGridIplik.IsEnabled = true;
            DGridSecilenIplikler.ItemsSource = _IplikIslem.GirisIplikleri;
            ChildIplikGirisleri.Show();
        }

        private void BtnTamam_Click(object sender, RoutedEventArgs e)
        {
            if (_IplikIslem.HareketKaydet())
                ChildIplikGirisleri.Close();
        }

        Iplik _IplikIslem;
        string _SecilenTip = "";

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridIplikGirisleri.ItemsSource = Iplik.IplikGirisleriGetir(_SecilenTip, DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void CmbHareketTipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string secilen = (CmbHareketTipi.SelectedItem as ComboBoxItem).Content.ToString();

                int ind1 = secilen.IndexOf('(');
                int ind2 = secilen.IndexOf(')', ind1 + 1);

                _SecilenTip = secilen.Substring(ind1 + 1, ind2 - ind1 - 1);
                if (_SecilenTip.Substring(0, 1) == "F") SBoxFason.ZorunluMu = true;
                else SBoxFason.ZorunluMu = false;
                DGridIplikGirisleri.ItemsSource = Iplik.IplikGirisleriGetir(_SecilenTip, DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
            }
            catch { }
        }

        List<tblMalzemeler> Iplikler;

        private void PageGiris_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;

            Iplikler = new vMalzemeler().IplikleriGetir();
            SBoxSatici.ItemsSource = tblFirmalar.TedarikcileriGetir();
            SBoxFason.ItemsSource = tblFirmalar.TedarikcileriGetir();
            CmbPersonel.ItemsSource = tblPersoneller.PersonelleriGetir(true);
            CmbRenk.ItemsSource = vRenkler.RenkleriGetir();
        }

        private void DGridIplik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tblMalzemeler secilen = DGridIplik.SelectedItem as tblMalzemeler;
            if (secilen == null) return;
            TxtSecilenIplik.Text = secilen == null ? "" : secilen.Kodu + " - " + secilen.Adi;

            vIplikGiris giris = new vIplikGiris();
            giris.NetKgChanged += new vIplikGiris.NetKgEvent(giris_NetKgChanged);

            giris.Adi = secilen.Adi;
            giris.Ambalaj = CmbAmbalaj.SelectedValue == null ? "" : CmbAmbalaj.SelectedValue.ToString();
            giris.BobinSayisi = TxtBobinSayisi.DoubleTxt;
            giris.BrutKg = TxtBrütKg.DoubleTxt;
            giris.GirisTanim = _SecilenTip;
            giris.Id = 0;
            giris.Tarih = DpTarih.SelectedDate;
            giris.Kodu = secilen.Kodu;
            giris.LotNo = TxtLotNo.Text;
            giris.MalzemeId = secilen.Id;
            giris.NetKg = null;
            giris.PersonelAdi = App.PersonelAdi;
            giris.PersonelId = App.PersonelId;
            giris.RenkId = CmbRenk.SelectedValue as int?;
            giris.Tarih = DateTime.Today;

            GridIplikEkle.DataContext = giris;
        }

        void giris_NetKgChanged()
        {
            vIplikGiris giris = GridIplikEkle.DataContext as vIplikGiris;
            GridIplikEkle.DataContext = null;
            GridIplikEkle.DataContext = giris;
        }

        private void BtnIplikSec_Click(object sender, RoutedEventArgs e)
        {
            if (GridIplikEkle.DataContext == null)
            {
                MessageBox.Show("İplik seçmediniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (CmbAmbalaj.GirisYapildiMi == false | CmbRenk.GirisYapildiMi == false | CmbPersonel.SelectedItem == null | !TxtLotNo.TextGirisiDogruMu | !TxtBobinSayisi.TextGirisiDogruMu
                | !TxtNetKg.TextGirisiDogruMu | DpTarih.SelectedDate == null | (SBoxFason.ZorunluMu && SBoxFason.GirisYapildiMi == false) | SBoxSatici.GirisYapildiMi == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!");
                return;
            }

            vIplikGiris giris = GridIplikEkle.DataContext as vIplikGiris;
            giris.RenkAdi = (CmbRenk.SelectedItem as tblRenkler).Adi;
            if (_SecilenTip.Substring(0, 1) == "F") giris.FasonAdi = (SBoxFason.SelectedItem as tblFirmalar).Adi;
            giris.SaticiAdi = (SBoxSatici.SelectedItem as tblFirmalar).Adi;
            _IplikIslem.IplikKaydet(giris, _SecilenTip);
            GridIplikEkle.DataContext = null;
            DGridSecilenIplikler.Items.Refresh();
            SBoxFason.Clear();
            SBoxSatici.Clear();
        }

        private void ChildIplikGirisleri_Closed(object sender, System.EventArgs e)
        {
            DGridSecilenIplikler.ItemsSource = null;
            GridIplikEkle.DataContext = null;
            TxtSecilenIplik.Text = "";
            TxtIplikAdFiltre.Text = "";
            TxtIplikKoduFiltre.Text = "";
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vIplikGiris secilen = DGridIplikGirisleri.SelectedItem as vIplikGiris;
            if (secilen == null) return;
            if (MessageBox.Show("Seçilen iplik çıkışı silinsin mi ..?" + "\n\nKodu : " + secilen.Kodu + "\nAdı : " + secilen.Adi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_IplikIslem == null) _IplikIslem = new Iplik(Enums.Hareketler.IplikGiris);
            if (_IplikIslem.GirisSil(secilen)) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vIplikGiris secilen = DGridIplikGirisleri.SelectedItem as vIplikGiris;
            if (secilen == null) return;
            _IplikIslem = new Iplik(Enums.Hareketler.IplikGiris, secilen);
            DGridIplik.ItemsSource = Iplikler.FindAll(c => c.Id == secilen.MalzemeId);
            GridIplikEkle.DataContext = secilen;
            DGridSecilenIplikler.ItemsSource = _IplikIslem.GirisIplikleri;
            DGridIplik.IsEnabled = false;
            ChildIplikGirisleri.Show();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void TxtIplikKoduFiltre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplik.ItemsSource = Iplikler.FindAll(c => c.Adi.ToUpper().Contains(TxtIplikAdFiltre.Text.ToUpper()) && c.Kodu.ToUpper().Contains(TxtIplikKoduFiltre.Text.ToUpper()));
        }

        private void TxtIplikAdFiltre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplik.ItemsSource = Iplikler.FindAll(c=>c.Adi.ToUpper().Contains(TxtIplikAdFiltre.Text.ToUpper()) && c.Kodu.ToUpper().Contains(TxtIplikKoduFiltre.Text.ToUpper()));
        }
    }
}

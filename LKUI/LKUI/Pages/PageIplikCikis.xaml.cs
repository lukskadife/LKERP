using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System.Linq;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageİplikCikislari.xaml
    /// </summary>
    public partial class PageIplikCikis : UserControl
    {
        public PageIplikCikis()
        {
            InitializeComponent();
        }

        Iplik _IplikIslem;

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            if (_SecilenTip == "" || _SecilenTip == null)
            {
                MessageBox.Show("Tip seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            _IplikIslem = new Iplik(Enums.Hareketler.IplikCikis);
            DGridSecilenIpliklerCikis.ItemsSource = _IplikIslem.CikisIplikleri;
            DGridIplikKartlari.ItemsSource = _Iplikler;
            DGridIplik.IsEnabled = true;
            ChildIplikCikis.Show();
        }

        string _SecilenTip;
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
                LoadPage();
            }
            catch { }
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridIplikCikislari.ItemsSource = Iplik.IplikCikislariGetir(_SecilenTip, DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void BtnTamam_Click_(object sender, RoutedEventArgs e)
        {
            if (_IplikIslem.HareketKaydet())
            {
                ChildIplikCikis.Close();
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        vIplikStok SecilenIplik;
        private void DGridIplik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridIplik.SelectedItem == null) return;
            SecilenIplik = DGridIplik.SelectedItem as vIplikStok;
            TxtSecilenIplikCikis.Text = SecilenIplik.Kodu + "  -  " + SecilenIplik.Adi;
            GridIplikEkleCikis.DataContext = new vIplikCikis();
            DpTarihCikis.SelectedDate = DateTime.Today;
        }

        List<tblMalzemeler> _Iplikler;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
            DpTarihCikis.SelectedDate = DateTime.Today;

            SBoxFason.ItemsSource = tblFirmalar.FirmalariGetir();
            CmbPersonelCikis.ItemsSource = tblPersoneller.PersonelleriGetir(true);
            _Iplikler = new vMalzemeler().GruptakiMalzemeleriGetir(39);
        }

        private void ChildIplikCikis_Closed(object sender, EventArgs e)
        {
            DGridIplik.ItemsSource = null;
            TxtSecilenIplikCikis.Text = "";
            GridIplikEkleCikis.DataContext = null;
            TxtIplikAdFiltreCikis.Text = "";
            TxtIplikKoduFiltreCikis.Text = "";
            DGridIplikKartlari.IsEnabled = true;
            DGridIplik.IsEnabled = true;
            DGridIplikKartlari.SelectedItems.Clear();
        }

        private void BtnIplikSec_Click(object sender, RoutedEventArgs e)
        {
            if (SecilenIplik == null)
            {
                MessageBox.Show("İplik seçmediniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (!TxtBobinSayisi.TextGirisiDogruMu | !TxtNetKg.TextGirisiDogruMu | !CmbPersonelCikis.GirisYapildiMi | DpTarihCikis.SelectedDate == null | (SBoxFason.ZorunluMu && SBoxFason.GirisYapildiMi == false))
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!");
                return;
            }
            if (GridIplikEkleCikis.DataContext == null) return;
            
            vIplikCikis secilen = GridIplikEkleCikis.DataContext as vIplikCikis;
            secilen.PersonelAdi = (CmbPersonelCikis.SelectedItem as tblPersoneller).Adi;
            secilen.LotNo = SecilenIplik.LotNo;
            secilen.RenkId = SecilenIplik.RenkId;
            secilen.RenkAdi = SecilenIplik.RenkAdi;
            secilen.SaticiAdi = SecilenIplik.Satici;
            secilen.SaticiId = SecilenIplik.SaticiId;
            secilen.Kodu = SecilenIplik.Kodu;
            secilen.Adi = SecilenIplik.Adi;
            secilen.MalzemeId = SecilenIplik.MalzemeId;
            secilen.Tarih = DpTarihCikis.SelectedDate;

            if (_SecilenTip.Substring(0, 1) == "F")
            {
                secilen.CikisTanimId = (SBoxFason.SelectedItem as tblFirmalar).Id;
                secilen.FasonAdi = (SBoxFason.SelectedItem as tblFirmalar).Adi;
            }
            else 
            {
                secilen.CikisTanimId = null;
                secilen.FasonAdi = null;
            }

            secilen.Ambalaj = SecilenIplik.Ambalaj;
            
            _IplikIslem.IplikKaydet(secilen, _SecilenTip);
            DGridSecilenIpliklerCikis.Items.Refresh();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vIplikCikis secilen = DGridIplikCikislari.SelectedItem as vIplikCikis;
            if (secilen == null) return;
            if (MessageBox.Show("Seçilen iplik çıkışı silinsin mi ..?" + "\n\nKodu : " + secilen.Kodu + "\nAdı : " + secilen.Adi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_IplikIslem == null) _IplikIslem = new Iplik(Enums.Hareketler.IplikCikis);
            if (_IplikIslem.CikisSil(secilen)) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            vIplikCikis secilen = DGridIplikCikislari.SelectedItem as vIplikCikis;
            if (secilen == null) return;
            vIplikStok stokKaydi = new vIplikStok()
            {
                Adi = secilen.Adi,
                Ambalaj = secilen.Ambalaj,
                BobinSayisi = secilen.BobinSayisi == null ? 0 : secilen.BobinSayisi.Value,
                Kodu = secilen.Kodu,
                LotNo = secilen.LotNo,
                MalzemeId = secilen.MalzemeId,
                NetKg = secilen.NetKg == null ? 0 : secilen.NetKg.Value,
                RenkAdi = secilen.RenkAdi,
                RenkId = secilen.RenkId,
                Satici = secilen.SaticiAdi,
                SaticiId = secilen.SaticiId == null ? 0 : secilen.SaticiId.Value
            };

            _IplikIslem = new Iplik(Enums.Hareketler.IplikCikis, secilen);
            DGridIplikKartlari.ItemsSource = new List<tblMalzemeler>() { new vMalzemeler().IplikGetir(secilen.MalzemeId) };
            DGridIplik.ItemsSource = new List<vIplikStok>() { stokKaydi };
            SecilenIplik = stokKaydi;
            GridIplikEkleCikis.DataContext = secilen;
            DGridSecilenIpliklerCikis.ItemsSource = _IplikIslem.CikisIplikleri;
            DGridIplikKartlari.IsEnabled = false;
            DGridIplik.IsEnabled = false;
            TxtSecilenIplikCikis.Text = secilen.Adi;
            ChildIplikCikis.Show();
        }

        private void TxtIplikKoduFiltreCikis_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplikKartlari.ItemsSource = _Iplikler.FindAll(c => c.Adi.ToUpper().Contains(TxtIplikAdFiltreCikis.Text.ToUpper()) && c.Kodu.ToUpper().Contains(TxtIplikKoduFiltreCikis.Text.ToUpper()));
            DGridIplik.ItemsSource = null;
            DGridIplikKartlari.SelectedItems.Clear();
        }

        private void TxtIplikAdFiltreCikis_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplikKartlari.ItemsSource = _Iplikler.FindAll(c => c.Adi.ToUpper().Contains(TxtIplikAdFiltreCikis.Text.ToUpper()) && c.Kodu.ToUpper().Contains(TxtIplikKoduFiltreCikis.Text.ToUpper()));
            DGridIplik.ItemsSource = null;
            DGridIplikKartlari.SelectedItems.Clear();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DGridIplikKartlari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblMalzemeler secilen = DGridIplikKartlari.SelectedItem as tblMalzemeler;
            if (secilen == null) return;
            DGridIplik.ItemsSource = new Stok().IplikStoklariGetir(secilen.Id);
        }
    }
}

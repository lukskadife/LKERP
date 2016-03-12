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

namespace LKUI.Pages
{
    public partial class PageLevent : UserControl
    {
        int _DurumId;
        public PageLevent(vLeventHareket levent)
        {
            InitializeComponent();
            _DurumId = levent.Durum;
            _Levent = new Levent(levent);
        }

        private void BtnİplikSec_Click(object sender, RoutedEventArgs e)
        {
            ChildİplikSec.Show();
        }

        Stok _IplikStok = new Stok();
        List<tblMalzemeler> IplikKartlari;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSecilenIplikler.ItemsSource = new List<vIplikStok>();
            DGridKullanilacakIplikler.ItemsSource = new List<vIplikStok>();
            CmbTipNo.ItemsSource = Levent.KumaslariGetir();
            CmbRenkNo.ItemsSource = vRenkler.RenkleriGetir();
            CmbCekenPersonel.ItemsSource = Levent.CozguPersoneliGetir();
            CmbTezgah.ItemsSource = new Makina().MakinalariGetir(1);
            IplikKartlari = new vMalzemeler().GruptakiMalzemeleriGetir(39, false);
            DGridIplikKartlari.ItemsSource = IplikKartlari;

            LoadPage();

            if (GrdUst.DataContext is vLeventHareket)
            {
                (GrdUst.DataContext as vLeventHareket).BantSayisiChanged += new vLeventHareket.BantSayisiEvent(PageLevent_BantSayisiChanged);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            if (DGridIplik.SelectedItem == null) return;
            if (TxtBobin.TextGirisiDogruMu == false || TxtNetKg.TextGirisiDogruMu == false) return;

            vIplikStok secilen = DGridIplik.SelectedItem as vIplikStok;
            if (secilen.BobinSayisi < TxtBobin.DoubleTxt.Value)
            {
                MessageBox.Show("Yeterli bobin yok..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
                return;
            }
            if (secilen.NetKg < TxtNetKg.DoubleTxt.Value)
            {
                MessageBox.Show("Yeterli kilo yok..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
                return;
            }
            secilen.NetKg = TxtNetKg.DoubleTxt.Value;
            secilen.BobinSayisi = TxtBobin.DoubleTxt.Value;

            (DGridSecilenIplikler.ItemsSource as List<vIplikStok>).Add(secilen);
            DGridSecilenIplikler.Items.Refresh();
            GridIplikEkle.DataContext = null;
            TxtBobin.Text = "";
            TxtNetKg.Text = "";
        }

        private void DGridIplik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridIplikEkle.DataContext = DGridIplik.SelectedItem;
        }

        Levent _Levent;
        private void BtnTamam_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSecilenIplikler.ItemsSource == null) return;

            _Levent.KullanilanIplikEkle(DGridSecilenIplikler.ItemsSource as List<vIplikStok>);

            DGridKullanilacakIplikler.Items.Refresh();
            ChildİplikSec.Close();
        }

        private void BtnSecilenKaldir_Click(object sender, RoutedEventArgs e)
        {
            (DGridSecilenIplikler.ItemsSource as List<vIplikStok>).Remove((sender as FrameworkElement).DataContext as vIplikStok);
            DGridSecilenIplikler.Items.Refresh();
        }

        public void Kaydet()
        {
            if (_Levent.Leventler.Count == 0)
            {
                MessageBox.Show("Leventler eklenmemiş.\n\nKaydedilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (_Levent.LeventleriKaydet())
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            Kaydet();
        } 

        public void LoadPage()
        {
            GrdLevent.DataContext = new vLeventHareket()
            {
                Durum = _DurumId,
                BantSayisi = null,
                BobinAdedi = null,
                BobinMetre = null,
                LeventEni = null,
                LeventNo = null,
                Metre = null,
                ResteMetre = null,
                TelAdedi = null,
            };
            
            GrdUst.DataContext = _Levent.Leventler.Count > 0 ? _Levent.Leventler[0] : new vLeventHareket();
            DGridKullanilacakIplikler.ItemsSource = _Levent.KullanilanIplikler;
            DGridLeventler.ItemsSource = _Levent.Leventler;

            GrdUst.IsEnabled = false;
            if (_Levent.Leventler.Count == 0)
            {
                GrdUst.IsEnabled = true;
                BtnİplikSec.IsEnabled = true;
            }
            else
            {
                GrdUst.IsEnabled = false;
                BtnİplikSec.IsEnabled = false;
            }

            DGridKullanilacakIplikler.Items.Refresh();
            DGridLeventler.Items.Refresh();
        }

        private void BtnLeventEkle_Click(object sender, RoutedEventArgs e)
        {
            if (_Levent.KullanilanIplikler.Count == 0)
            {
                MessageBox.Show("İplikler eklenmeden levent eklenemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (TxtLeventEni.TextGirisiDogruMu == false | TxtTelAd.TextGirisiDogruMu == false | TxtBobinAd.TextGirisiDogruMu == false | TxtBobinMetre.TextGirisiDogruMu == false
                | TxtResteMetre.TextGirisiDogruMu == false | TxtLeventCap.TextGirisiDogruMu == false)
                return;
            vLeventHareket levent = GrdLevent.DataContext as vLeventHareket;
            vLeventHareket ust = GrdUst.DataContext as vLeventHareket;
            levent.BantSayisi = ust.BantSayisi;
            levent.BobinAdedi = ust.BobinAdedi;
            levent.BobinMetre = ust.BobinMetre;
            levent.ResteMetre = ust.ResteMetre;
            levent.RenkId = ust.RenkId;
            levent.TelAdedi = ust.TelAdedi;
            levent.TipId = ust.TipId;
            levent.Aciklama = ust.Aciklama;

            levent.CekenPersonel = (CmbCekenPersonel.SelectedItem as tblPersoneller).Adi;
            levent.TezgahAdi = (CmbTezgah.SelectedItem as tblMakinalar).Adi;
            levent.Cozgu = CmbCozgu.Text;
            levent.Durum = _Levent.DurumId;

            dynamic snc = _Levent.LeventEkle(levent);
            if (snc is bool && snc == true)
            {
                LoadPage();
                CmbCozgu.SelectedIndex = -1;
                CmbCekenPersonel.SelectedItem = null;
                CmbTezgah.SelectedItem = null;
            }
            else if (snc == 1) MessageBox.Show("Bu levent zaten kullanımda.\n\nEklenemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            else if (snc == 2) MessageBox.Show("Bu tezgah dolu.\n\nEklenemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChildİplikSec_Closed(object sender, EventArgs e)
        {
            DGridSecilenIplikler.ItemsSource = new List<vIplikStok>();
            DGridIplikKartlari.SelectedItems.Clear();
        }

        private void BtnLeventSil_Click(object sender, RoutedEventArgs e)
        {
            if (_Levent.LeventleriSil())
            {
                DGridKullanilacakIplikler.ItemsSource = _Levent.KullanilanIplikler;
                DGridLeventler.ItemsSource = _Levent.Leventler;
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void PageLevent_BantSayisiChanged()
        {
            vLeventHareket tmp = GrdUst.DataContext as vLeventHareket;
            GrdUst.DataContext = null;
            GrdUst.DataContext = tmp;
        }

        private void DGridIplikKartlari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblMalzemeler secilen = DGridIplikKartlari.SelectedItem as tblMalzemeler;
            if (secilen == null) return;
            DGridIplik.ItemsSource = _IplikStok.IplikStoklariGetir(secilen.Id);
        }

        private void TxtIplikTipFiltre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplikKartlari.ItemsSource = IplikKartlari.Where(c => c.Kodu.ToUpper().Contains(TxtIplikTipFiltre.Text.ToUpper()) && c.Adi.ToUpper().Contains(TxtIplikAdFiltre.Text.ToUpper()));
        }

        private void TxtIplikAdFiltre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridIplikKartlari.ItemsSource = IplikKartlari.Where(c => c.Kodu.ToUpper().Contains(TxtIplikTipFiltre.Text.ToUpper()) && c.Adi.ToUpper().Contains(TxtIplikAdFiltre.Text.ToUpper()));
        }

    }
}

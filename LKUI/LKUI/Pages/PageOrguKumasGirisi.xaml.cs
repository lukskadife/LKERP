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
using System.IO.Ports;
using System.Windows.Threading;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageOrguKumasGirisi.xaml
    /// </summary>
    public partial class PageOrguKumasGirisi : UserControl
    {
        //OrmeTezgahHaberlesme _OrmeTezgahHaberlesme;

        public PageOrguKumasGirisi()
        {
            InitializeComponent();
           
           
           
        }

        HamKumas _KumasIslem;
        TezgahHaberlesme _Tezgah;
       
        tblHamHatalari _Hata = new tblHamHatalari();
        //private serialPort _seriPort = new SerialPort() { PortName = "COM1", BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One, Handshake = Handshake.None };

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbEtiket.ItemsSource = HamKumas.HamEtiketleriGetir();
            CmbEtiket.SelectedIndex = 0; 
            CmbHata.ItemsSource = HamKumas.OrmeHataTanimlariGetir();
            CmbKaliteci.ItemsSource = HamKumas.OrmeKalitecileriGetir();
            CmbTezgahNo.ItemsSource = HamKumas.OrmeTezgahlariGetir();
            CmbTipNo.ItemsSource = HamKumas.OrmeTipleriGetir();

          

            //_OrmeTezgahHaberlesme = new OrmeTezgahHaberlesme();
            ////_OrmeTezgahHaberlesme.TezgahHareketEtti += new OrmeTezgahHaberlesme.TezgahEvent(_OrmeTezgah_TezgahHareketEtti);
            ////if(_OrmeTezgahHaberlesme.HazirMi==false) MessageBox.Show("Tezgahla iletişim sağlanamadı");
            //TxtKacinciMetre.Text = _OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri;
          
            _KumasIslem = new HamKumas("Örme") { Tarih = DateTime.Today };
            GrdUst.DataContext = _KumasIslem;
            GrdHata.DataContext = _Hata;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;

            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
            
        }

      
        void _OrmeTezgah_TezgahHareketEtti()
        {
            //string snc = _OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri;
            //TxtKacinciMetre.Text = _OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri;
            //TxtMetreÜst.Text = _OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri;
            //TxtAciklama.Text = _OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri;
            
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

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            HamKaydet();
        }

        private void HamKaydet()
        {
            BtnKaydet.Focus();            
            try
            {
                if ( TxtMetreÜst.TextGirisiDogruMu == false | TxtVaryant.TextGirisiDogruMu == false | TxtKgÜst.TextGirisiDogruMu == false | CmbTipNo.SelectedIndex == -1 
                    | CmbTezgahNo.SelectedIndex == -1 | CmbSagSol.SelectedIndex==-1 | CmbKaliteci.SelectedIndex == -1 | CmbABCD.SelectedIndex == -1) return;

                _KumasIslem.KumasAlt.Metre = Convert.ToDouble(TxtMetreÜst.Text);
                _KumasIslem.KumasAlt.Kg = Convert.ToDouble(TxtKgÜst.Text);
                _KumasIslem.KumasAlt.Tur = CmbSagSol.SelectedValue.ToString() + " " + CmbABCD.SelectedValue.ToString();
                if (_KumasIslem.OrmeKumasKaydet() == false)
                {
                    MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                EtiketYazdir(HamKumas.HamKumasGetir(_KumasIslem.KumasAlt.Id, true));
               
                LoadPage();
                _KumasIslem = new HamKumas("Örme") { Tarih = DateTime.Today, KaliteciId = _KumasIslem.KaliteciId };
                GrdUst.DataContext = _KumasIslem;
                GrdHata.DataContext = _Hata;
                DGridHata.ItemsSource = _KumasIslem.Hatalar;
                Refresh();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public void Refresh()
        {           
            DGridHata.ItemsSource = null;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;
            CmbABCD.SelectedIndex = -1;
            CmbSagSol.SelectedIndex = -1;
        }

        private void LoadPage()
        {
            if (this.IsLoaded)
            {
               
                if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;
                DGridTartımDetayları.ItemsSource = HamKumas.OrmeKayitlariGetir(DPBaslangic.SelectedDate.Value.Date, DPBitis.SelectedDate.Value.Date).OrderByDescending(o => o.Id).ToList();
            }
        }

        private void EtiketYazdir(vHamKumaslar ham)
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
        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            _KumasIslem = new HamKumas("Örme") { Tarih = DateTime.Today };
            CmbSagSol.SelectedIndex = -1;
            CmbABCD.SelectedIndex = -1;
            CmbKaliteci.SelectedIndex = -1;
            CmbTezgahNo.SelectedIndex = -1;
            CmbTipNo.SelectedIndex = -1;
            TxtVaryant.Text = null;
            TxtKgÜst.Text = null;
            TxtMetreÜst.Text = null;
            TxtAciklama.Text = null;
            GrdHata.DataContext = _Hata;
            DGridHata.ItemsSource = _KumasIslem.Hatalar;
            Refresh();
        }

        private void CmbHata_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HataEkle();
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

        private void HataEkle()
        {
            if (CmbHata.SelectedItem == null | TxtUzunluk.TextGirisiDogruMu == false | TxtKacinciMetre.TextGirisiDogruMu == false) return;
            tblHamHatalari ekle = GrdHata.DataContext as tblHamHatalari;
//            ekle.Metresi = Convert.ToDouble(_OrmeTezgahHaberlesme.OrmeOkunanMetreDegeri);
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
        private void MIHamEtiketYazdir_Click(object sender, RoutedEventArgs e)
        {
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;

            if (secilen == null) return;
            EtiketYazdir(HamKumas.HamKumasGetir(secilen.Id, true));
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

        private void BtnDüzelt_Click(object sender, RoutedEventArgs e)
        {
            vHamKumaslar secilen = DGridTartımDetayları.SelectedItem as vHamKumaslar;
            if (secilen == null) return;

            CmbDuzeltKaliteci.ItemsSource = HamKumas.OrmeKalitecileriGetir();
            CmbDuzeltTezgahNo.ItemsSource = HamKumas.OrmeTezgahlariGetir();
            CmbDuzeltTipNo.ItemsSource = HamKumas.OrmeTipleriGetir();
            CmbDuzeltKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();

            childDuzelt.DataContext = secilen.ViewToTbl();
            childDuzelt.Show();

        }

        private void CmbHata_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CmbHata.IsDropDownOpen == false) CmbHata.IsDropDownOpen = true;
            }
            catch { }
        }

       
        private void TxtMetreÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetreÜst.SelectAll();
        }

        private void TxtMetreÜst_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMetreÜst.SelectAll();
        }

        private void TxtKgÜst_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKgÜst.SelectAll();
        }

        private void TxtKgÜst_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtKgÜst.SelectAll();
        }

        private void TxtKacinciMetre_KeyUp(object sender, KeyEventArgs e)
        {
               
            if (e.Key == Key.Enter) TxtUzunluk.Focus();
        }

        private void TxtUzunluk_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TxtUzunluk.TextGirisiDogruMu) CmbHata.Focus();
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
    }
}

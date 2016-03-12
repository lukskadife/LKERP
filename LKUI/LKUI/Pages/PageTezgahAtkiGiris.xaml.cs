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
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageTezgahAtkiGiris.xaml
    /// </summary>
    public partial class PageTezgahAtkiGiris : UserControl
    {
        public PageTezgahAtkiGiris()
        {
            InitializeComponent();
        }

        private DateTime _SonAtkiGirisTarihi;
        private int _SonAtkiDokumaci;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbTezgah.ItemsSource = new Makina().MakinalariGetir(1);
            CmbTip.ItemsSource = vKumas.KumaslariGetir(true).OrderBy(o=>o.TipNo).ToList();
            CmbDokumaci.ItemsSource = HamKumas.DokumacilariGetir();
            _SonAtkiGirisTarihi = Makina.TezgahAtkiSonGirisTarihiGetir();
            _SonAtkiDokumaci = Makina.TezgahAtkiSonDokumaciGetir();
            DPBaslangic.SelectedDate = _SonAtkiGirisTarihi;
            DPBitis.SelectedDate = DateTime.Today;
        }

        private void LoadPage()
        {
            if (DPBitis.SelectedDate == null || DPBitis.SelectedDate == null) return;
            DGridAtki.ItemsSource = Makina.TezgahAtkiGirisleriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void ChildAtkiEkle_Closed(object sender, EventArgs e)
        {
            ChildAtkiEkle.DataContext = null;
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            _SonAtkiDokumaci = Makina.TezgahAtkiSonDokumaciGetir();
            ChildAtkiEkle.DataContext = new vTezgahAtkiGiris() { Tarih = _SonAtkiGirisTarihi, PlanOteledi = false ,DokumaciId=_SonAtkiDokumaci}; 
            ChildAtkiEkle.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vTezgahAtkiGiris secilen = DGridAtki.SelectedItem as vTezgahAtkiGiris;
            if (secilen == null) return;

            if (secilen.PlanOteledi == true)
            {
                MessageBox.Show("Bu atkı girişi silinemez.\n\nPlanları ötelemiş..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (MessageBox.Show("Silinecek..?\n\nTezgah : " + secilen.TezgahKodu + " - " + secilen.TezgahAdi, App.AlertCaption, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (secilen == null) return;

            if (Makina.TezgahAtkiGirisiSil(secilen)) LoadPage();
            else MessageBox.Show("Silinemedi..!", App.AlertCaption, MessageBoxButton.OK);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (ChildAtkiEkle.DataContext == null) return;

            if (CmbPosta.GirisYapildiMi == false | CmbTezgah.GirisYapildiMi == false | CmbTip.GirisYapildiMi == false | TxtBaslangic.TextGirisiDogruMu == false | TxtBitis.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (TxtBaslangic.IntTxt > TxtBitis.IntTxt)
            {
                MessageBox.Show("Başlangıç bitişten büyük olamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            vTezgahAtkiGiris atkiGiris = ChildAtkiEkle.DataContext as vTezgahAtkiGiris;
            try
            {
                if (Makina.TezgahAtkiGirisiEkle(atkiGiris))
                {
                    LoadPage();
                    ChildAtkiEkle.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nGiriş yapılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch { }
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildAtkiEkle.Close();
        }

        private void CmbTezgah_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vTezgahAtkiGiris giris = ChildAtkiEkle.DataContext as vTezgahAtkiGiris;

            if (giris != null)
            {
                TxtBaslangic.Text = giris.Postasi != null ? Makina.TezgahAtkiSonSayaciGetir(giris.TezgahId, giris.Postasi).ToString() : "";
                CmbTip.SelectedValue = giris == null ? 0 : Makina.TezgahaBagliTipGetir(giris.TezgahId);
            }

            else
            {
                TxtBaslangic.Text = "";
                CmbTip.SelectedValue = null;
            }
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridAtki.ToExcel<vTezgahAtkiGiris>();
        }

        private void TxtBaslangic_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtBaslangic.SelectAll();
        }

        private void TxtBitis_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtBitis.SelectAll();
        }

        private void TxtBaslangic_TextChanged(object sender, TextChangedEventArgs e)
        {
            FarkGoster();
        }

        private void TxtBitis_TextChanged(object sender, TextChangedEventArgs e)
        {
            FarkGoster();
        }

        private void FarkGoster()
        {
            if (TxtBaslangic.TextGirisiDogruMu == false || TxtBitis.TextGirisiDogruMu == false)
            {
                LblFark.Content = "";
                return;
            }
            LblFark.Content = TxtBitis.IntTxt - TxtBaslangic.IntTxt;
        }

        private void CmbPosta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vTezgahAtkiGiris giris = ChildAtkiEkle.DataContext as vTezgahAtkiGiris;

            if (giris != null) TxtBaslangic.Text = giris.TezgahId != 0 ? Makina.TezgahAtkiSonSayaciGetir(giris.TezgahId, giris.Postasi).ToString() : "0";
            else TxtBaslangic.Text = "";
        }

        private void TxtBaslangic_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtBaslangic.SelectAll();
        }

        private void TxtBitis_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtBitis.SelectAll();
        }
    }
}

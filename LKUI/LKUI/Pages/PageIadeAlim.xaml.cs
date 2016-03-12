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
    /// <summary>
    /// Interaction logic for PageIadeAlim.xaml
    /// </summary>
    public partial class PageIadeAlim : UserControl
    {
        public PageIadeAlim()
        {
            InitializeComponent();
        }

        Iade _Islem = new Iade();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSiparis.ItemsSource = Iade.IadeSiparisleriGetir();
            CmbPersonel.ItemsSource = Sevkiyat.SevkPersoneliGetir();
            CmbKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();
        }

        private void BtnIadeEkle_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSiparis.SelectedItem == null) return;
            ChildIade.DataContext = new vMamulKumaslar() { Tarih = DateTime.Today };
            ChildIade.Show();
        }

        private void BtnIadeDuzelt_Click(object sender, RoutedEventArgs e)
        {
            IadeDuzelt();
        }

        private void BtnIadeSil_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridIadeler.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;

            if (MessageBox.Show("Kayıt silinsin mi..?\n\nBarkod : " + secilen.Barkod, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.IadeSil(secilen) == true)
            {
                DGridIadeler.ItemsSource = null;
                DGridIadeler.ItemsSource = _Islem.IadeBarkodlari;
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);                 
        }

        private void IadeDuzelt()
        {
            vMamulKumaslar secilen = DGridIadeler.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;
            ChildIade.DataContext = secilen;
            ChildIade.Show();
        }

        private void DGridSiparis_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridSiparis.SelectedItem == null) return;

            _Islem.IadeSiparisi = DGridSiparis.SelectedItem as vSiparisler;
            DGridIadeler.ItemsSource = null;
            DGridIadeler.ItemsSource = _Islem.IadeBarkodlari;
            CmbTip.ItemsSource = _Islem.IadeTipleriGetir();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            bool snc = false;
            vMamulKumaslar iade = ChildIade.DataContext as vMamulKumaslar;

            if (CmbKalite.GirisYapildiMi == false | CmbPersonel.GirisYapildiMi == false | CmbRenkNo.GirisYapildiMi == false | CmbTip.GirisYapildiMi == false | 
                TxtIadeSebebi.TextGirisiDogruMu == false | TxtMetre.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur.\n\nBoş geçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (iade.Id == 0) snc = _Islem.IadeEkle(iade);
            else snc = _Islem.IadeDuzelt(iade);

            if (snc)
            {
                DGridIadeler.ItemsSource = null;
                DGridIadeler.ItemsSource = _Islem.IadeBarkodlari;
                ChildIade.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CmbTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vSiparisAct secilen = CmbTip.SelectedItem as vSiparisAct;
            if (secilen == null)
            {
                CmbRenkNo.ItemsSource = null;
                return;
            }
            CmbRenkNo.ItemsSource = _Islem.IadeRenkleriGetir(secilen.TipId);
        }
    }
}

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
    /// Interaction logic for PageTicariMal.xaml
    /// </summary>
    public partial class PageTicariMal : UserControl
    {
        public PageTicariMal()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;

            //CmbFirma.ItemsSource = tblFirmalar.FirmalariGetir();
            CmbTipNo.ItemsSource = vKumas.KumaslariGetir(true);
            CmbKalite.ItemsSource = tblKaliteTanim.KaliteleriGetir();
            CmbPersonel.ItemsSource = Siparis.PersonelleriGetir();
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridTicariler.ItemsSource = Mamul.TicariMamulleriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildTicari.DataContext = new vMamulKumaslar();
            ChildTicari.Show();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridTicariler.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;

            if (secilen.SevkId != 0)
            {
                MessageBox.Show("Sevk edilmiş.\n\nDüzeltilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            ChildTicari.DataContext = secilen;
            ChildTicari.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridTicariler.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;

            if (secilen.SevkId != 0)
            {
                MessageBox.Show("Sevk edilmiş.\n\nSilinemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (MessageBox.Show("Giriş silinsin mi ?\n\nBarkod : " + secilen.Barkod, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (Mamul.TicariMamulSil(secilen)) LoadPage();
            else MessageBox.Show("Hata oluştu.\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void ChildTicari_Closed(object sender, EventArgs e)
        {
            ChildTicari.DataContext = null;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar ticari = ChildTicari.DataContext as vMamulKumaslar;
            if (ticari == null) return;

            if (CmbKalite.GirisYapildiMi == false | CmbPersonel.GirisYapildiMi == false | CmbTipNo.GirisYapildiMi == false | CmbTur.GirisYapildiMi == false |
                TxtBarkod.TextGirisiDogruMu == false | TxtKg.TextGirisiDogruMu == false | TxtMetre.TextGirisiDogruMu == false | TxtRenkNo.TextGirisiDogruMu == false | TxtEn.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            try
            {
                if (Mamul.TicariMamulKaydet(ticari))
                {
                    ChildTicari.Close();
                    LoadPage();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void TxtEn_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtEn.SelectAll();
        }

        private void TxtMetre_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetre.SelectAll();
        }
    }
}

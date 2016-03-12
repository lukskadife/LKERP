using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using System;
using LKLibrary.DbClasses;
using System.Collections.Generic;

namespace LKUI.Pages
{
    public partial class PageProcessFiyatListesi : UserControl
    {
        public PageProcessFiyatListesi()
        {
            InitializeComponent();
        }

        Siparis _Siparis = new Siparis();

        private void ComboBoxYil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (_Siparis.FiyatKaydet(ListFiyat))
            {
                LoadPage();
                MessageBox.Show("Kaydetme başarılı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu..\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ComboBoxAy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        void LoadPage()
        {
            if (!string.IsNullOrEmpty(ComboBoxYil.Text) && !string.IsNullOrEmpty(ComboBoxAy.Text))
            {
                int yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content), ay = ComboBoxAy.SelectedIndex + 1;
                if (yil == DateTime.Now.Year && ay == DateTime.Now.Month) ListFiyat = _Siparis.ProsesFiyatListesiGetir(yil, ay);
                else ListFiyat = _Siparis.ProsesFiyatListesiGetir(yil, ay);
                DGridSabitFiyatListesi.ItemsSource = ListFiyat;
                ListFiyat.ForEach(c => c.Dovizler = ListDoviz);
                ListFiyat.ForEach(c => c.Prosesler = ListProses);
            }
        }

        List<vFiyatListeleri> ListFiyat;
        List<vAyarlar> ListDoviz;
        List<tblProses> ListProses;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListDoviz = new vAyarlar().DovizleriGetir();
            ListProses = tblProses.ProsesleriGetir();
            ComboBoxAy.SelectedIndex = DateTime.Now.Month - 1;
            ComboBoxYil.Text = DateTime.Now.Year.ToString();
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vFiyatListeleri secilen = (sender as FrameworkElement).DataContext as vFiyatListeleri;
            if (secilen == null) return;

            if (MessageBox.Show("Kayıt silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bool sonuc = true;

                if (secilen.Id != 0 && _Siparis.FiyatSil(secilen) == false) sonuc = false;

                if (sonuc)
                {
                    (DGridSabitFiyatListesi.ItemsSource as List<vFiyatListeleri>).Remove(secilen);
                    DGridSabitFiyatListesi.Items.Refresh();
                    MessageBox.Show("Kayıt silindi..!", App.AlertCaption);
                }
                else MessageBox.Show("Hata oluştu..\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            (DGridSabitFiyatListesi.ItemsSource as List<vFiyatListeleri>).Add(new vFiyatListeleri() { Dovizler = ListDoviz, Prosesler = ListProses, Yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content), Ay = ComboBoxAy.SelectedIndex + 1 });
            DGridSabitFiyatListesi.Items.Refresh();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using LKLibrary.DbClasses;
using System.Collections.Generic;
using LKLibrary.Classes;
using System;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for DtlSabitSatisFiyatListesi.xaml
    /// </summary>
    public partial class PageSabitSatisFiyatListesi : UserControl
    {
        public PageSabitSatisFiyatListesi()
        {
            InitializeComponent();
        }

        List<vFiyatListeleri> ListFiyat;
        List<vAyarlar> ListDoviz;
        Siparis _Siparis = new Siparis();

        void LoadPage()
        {
            if (!string.IsNullOrEmpty(ComboBoxYil.Text) && !string.IsNullOrEmpty(ComboBoxAy.Text))
            {
                int yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content), ay = ComboBoxAy.SelectedIndex + 1;
                if (yil == DateTime.Now.Year && ay == DateTime.Now.Month) ListFiyat = _Siparis.SabitFiyatListesiGetir(yil, ay);
                else ListFiyat = _Siparis.SabitFiyatListesiGetir(yil, ay);
                DataGridSabitFiyatListesi.ItemsSource = ListFiyat;
                ListFiyat.ForEach(c => c.Dovizler = ListDoviz);
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            ListFiyat.ForEach(c => c.Yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content));
            ListFiyat.ForEach(c => c.Ay = ComboBoxAy.SelectedIndex + 1);
            ListFiyat.ForEach(c => c.OlusturanPersonelId = App.PersonelId);
            if (_Siparis.FiyatKaydet(ListFiyat.FindAll(c=>c.Tip != null)))
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

        private void ComboBoxYil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListDoviz = new vAyarlar().DovizleriGetir();
            ComboBoxAy.SelectedIndex = DateTime.Now.Month - 1;
            ComboBoxYil.Text = DateTime.Now.Year.ToString();
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vFiyatListeleri secilen = (sender as FrameworkElement).DataContext as vFiyatListeleri;
            if (secilen == null) return;

            if (MessageBox.Show(secilen.MusteriAdi + " kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bool sonuc = true;

                if (secilen.Id != 0 && _Siparis.FiyatSil(secilen) == false) sonuc = false;

                if (sonuc)
                {
                    (DataGridSabitFiyatListesi.ItemsSource as List<vFiyatListeleri>).Remove(secilen);
                    DataGridSabitFiyatListesi.Items.Refresh();
                    MessageBox.Show(secilen.MusteriAdi + " kaydı silindi..!", App.AlertCaption);
                }
                else MessageBox.Show("Hata oluştu..\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            (DataGridSabitFiyatListesi.ItemsSource as List<vFiyatListeleri>).Add(new vFiyatListeleri() { Dovizler = ListDoviz, Yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content), Ay = ComboBoxAy.SelectedIndex + 1  });
            DataGridSabitFiyatListesi.Items.Refresh();
        }
    }
}

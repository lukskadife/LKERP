using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using System;
using LKLibrary.DbClasses;
using System.Collections.Generic;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for DtlMüsteriFiyatları.xaml
    /// </summary>
    public partial class PageMusteriFiyatlari : UserControl
    {
        public PageMusteriFiyatlari()
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

        //private void ComboBoxAy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    LoadPage();
        //}

        private void BtnMusteri_Click(object sender, RoutedEventArgs e)
        {
            DGridMusteriler.ItemsSource = tblFirmalar.MusterileriGetir();
            ChildMusteriler.Show();
        }

        void LoadPage()
        {
            if (!string.IsNullOrEmpty(ComboBoxYil.Text))
            {
                int yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content);
                //, ay = ComboBoxAy.SelectedIndex + 1;
                ListFiyat = _Siparis.MusteriFiyatListesiGetir(yil);
                DGridMusteriFiyatListesi.ItemsSource = ListFiyat;
                ListFiyat.ForEach(c => c.Dovizler = ListDoviz);
            }
        }

        List<vFiyatListeleri> ListFiyat;
        List<vAyarlar> ListDoviz;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListDoviz = new vAyarlar().DovizleriGetir();
            //ComboBoxAy.SelectedIndex = DateTime.Now.Month -1;
            ComboBoxYil.Text = DateTime.Now.Year.ToString(); 
            LoadPage();
        }

        private void DGridMusteriler_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DGridMusteriler.SelectedItem == null) return;

            tblFirmalar musteri = DGridMusteriler.SelectedItem as tblFirmalar;
            (DGridMusteriFiyatListesi.SelectedItem as vFiyatListeleri).MusteriAdi = musteri.Adi;
            (DGridMusteriFiyatListesi.SelectedItem as vFiyatListeleri).MusteriId = musteri.Id;
            (DGridMusteriFiyatListesi.SelectedItem as vFiyatListeleri).MusteriKodu = musteri.Kodu;
            DGridMusteriFiyatListesi.Items.Refresh();
            ChildMusteriler.Close();
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
                    (DGridMusteriFiyatListesi.ItemsSource as List<vFiyatListeleri>).Remove(secilen);
                    DGridMusteriFiyatListesi.Items.Refresh();
                    MessageBox.Show(secilen.MusteriAdi + " kaydı silindi..!", App.AlertCaption);
                }
                else MessageBox.Show("Hata oluştu..\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            //Ay parametresi kaldırılacak.
            (DGridMusteriFiyatListesi.ItemsSource as List<vFiyatListeleri>).Add(new vFiyatListeleri(){ Dovizler = ListDoviz, Yil = Convert.ToInt32((ComboBoxYil.SelectedValue as ComboBoxItem).Content), Ay = ComboBoxYil.SelectedIndex + 1 });
            DGridMusteriFiyatListesi.Items.Refresh();
        }
    }
}

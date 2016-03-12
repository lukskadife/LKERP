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
using System.Windows.Shapes;
using ETSevk.Classes;

namespace ETSevk
{
    /// <summary>
    /// Interaction logic for PageSevkBelge.xaml
    /// </summary>
    public partial class PageSevkBelge : Window
    {
        public PageSevkBelge(vSevk belge)
        {
            InitializeComponent();
            this.DataContext = belge;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }

        public enum KapatmaSekli { Kaydet, Vazgec }
        public KapatmaSekli Kapatilma = KapatmaSekli.Vazgec;

        private void CmbMusteri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vSiparisler musteri = CmbMusteri.SelectedItem as vSiparisler;
            if (musteri == null)
            {
                CmbSiparis.ItemsSource = null;
                return;
            }

            CmbSiparis.ItemsSource = Sevkiyat.MusteriSiparisleriGetir(musteri.FirmaId);
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            Kapatilma = KapatmaSekli.Vazgec;
            Close();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (CmbMusteri.SelectedItem == null || CmbSevkEden.SelectedItem == null || CmbSiparis.SelectedItem == null)
            {
                PageMesaj.Show("Mavi renkteki alanları doldurunuz..", PageMesaj.MesajTip.Tamam);
                return;
            }

            Kapatilma = KapatmaSekli.Kaydet;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbMusteri.ItemsSource = Sevkiyat.MusterileriGetir();
            CmbSevkEden.ItemsSource = Sevkiyat.SevkPersoneliGetir();
        }
    }
}

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
    /// Interaction logic for PageBarkod.xaml
    /// </summary>
    public partial class PageBarkod : Window
    {
        public PageBarkod(Sevkiyat islem)
        {
            InitializeComponent();
            _Islem = islem;
            this.DataContext = islem.SevkBelge;
            DGridOkutulan.ItemsSource = _Islem.Okutulanlar;
            TxtCount.Content = _Islem.Okutulanlar.Count.ToString() + " adet";
            TxtSum.Content = _Islem.Okutulanlar.Sum(s => s.Metre).ToString() + " metre";
        }

        Sevkiyat _Islem;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBarkod.Focus();
        }

        private void TxtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TxtBarkod.Text == "") return;
                try
                {
                    _Islem.BarkodOkut(TxtBarkod.Text);
                    DGridOkutulan.ItemsSource = _Islem.Okutulanlar;
                    TxtCount.Content = _Islem.Okutulanlar.Count.ToString() + " adet";
                    TxtSum.Content = _Islem.Okutulanlar.Sum(s => s.Metre).ToString() + " metre";
                }
                catch (Exception exc)
                {
                    PageMesaj.Show(exc.Message, PageMesaj.MesajTip.Tamam);
                }
                TxtBarkod.Text = "";
            }
        }

        private void BtnBarkodSil_Click(object sender, RoutedEventArgs e)
        {
            vSevkiyatBarkodlari silinecek = DGridOkutulan.SelectedItem as vSevkiyatBarkodlari;
            if (silinecek == null) return;

            if (PageMesaj.Show("Kayıt silinsin mi ?\n\nBarkod : " + silinecek.Barkod, PageMesaj.MesajTip.Hayir) == PageMesaj.MesajTip.Hayir)
                return;

            if (_Islem.BarkodSil(silinecek))
            {
                DGridOkutulan.ItemsSource = _Islem.Okutulanlar;
                DGridOkutulan.Items.Refresh();
                TxtCount.Content = _Islem.Okutulanlar.Count.ToString() + " adet";
                TxtSum.Content = _Islem.Okutulanlar.Sum(s => s.Metre).ToString() + " metre";
                //PageMesaj.Show("Kayıt silindi..!\n\nBarkod : " + silinecek.Barkod, PageMesaj.MesajTip.Tamam);
            }
            else PageMesaj.Show("Hata oluştu.\n\nKayıt silinemedi..!", PageMesaj.MesajTip.Tamam);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }

        private void BtnKontrol_Click(object sender, RoutedEventArgs e)
        {
            PageBarkodKontrol kontrol = new PageBarkodKontrol();
            kontrol.DGridBarkodlar.ItemsSource = _Islem.SevkEmriMamulleriGetir();
            kontrol.ShowDialog();
        }

        private void BtnSevkMsj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

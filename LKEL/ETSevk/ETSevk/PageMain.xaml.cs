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
using ETSevk.Classes;

namespace ETSevk
{
    /// <summary>
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Window
    {
        public PageMain()
        {
            InitializeComponent();
        }

        Sevkiyat _Islem = new Sevkiyat();

        private void DGridSevkBelge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;
        }

        PageSevkBelge _PageBelge;
        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            _Islem.SevkBelge = new vSevk() { Tarih = DateTime.Today };
            _PageBelge = new PageSevkBelge(_Islem.SevkBelge);
            _PageBelge.Closed += new EventHandler(_PageBelge_Closed);
            _PageBelge.ShowDialog();
        }

        void _PageBelge_Closed(object sender, EventArgs e)
        {
            if (_PageBelge.Kapatilma == PageSevkBelge.KapatmaSekli.Kaydet)
            {
                if (_Islem.SevkiyatKaydet() == false) PageMesaj.Show("Hata oluştu.\n\nKaydedilemedi..!", PageMesaj.MesajTip.Tamam);
                LoadPage();
            }
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;
            _PageBelge = new PageSevkBelge(_Islem.SevkBelge);
            _PageBelge.Closed += new EventHandler(_PageBelge_Closed);
            if (_Islem.Okutulanlar != null && _Islem.Okutulanlar.Count != 0 && _Islem.SevkBelge.TipRenkKontrolDevreDisi) _PageBelge.CheckTipRenkDevreDisi.IsEnabled = false;
            else _PageBelge.CheckTipRenkDevreDisi.IsEnabled = true;
            _PageBelge.ShowDialog();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vSevk secilen = DGridSevkBelge.SelectedItem as vSevk;
            if (secilen == null) return;

            if (PageMesaj.Show("Kayıt silinsin mi ?\n\nBelge No : " + _Islem.SevkBelge.BelgeNo + "\nSipariş No : " + _Islem.SevkBelge.SozlesmeNo, PageMesaj.MesajTip.Hayir) == PageMesaj.MesajTip.Hayir)
                return;

            if (_Islem.SevkiyatSil())
            {
                LoadPage();
                PageMesaj.Show("Kayıt silindi..!\n\nBelge No : " + secilen.BelgeNo + "\nSipariş No : " + secilen.SozlesmeNo, PageMesaj.MesajTip.Tamam);
            }
            else PageMesaj.Show("Hata oluştu.\n\nKayıt silinemedi..!", PageMesaj.MesajTip.Tamam);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void DGridSevkBelge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Islem.SevkBelge = DGridSevkBelge.SelectedItem as vSevk;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
            this.Title = "Sevkiyat v1.0.12";
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridSevkBelge.ItemsSource = Sevkiyat.SevkiyatlariGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnSevkiyat_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;
            PageBarkod barkod = new PageBarkod(_Islem);
            barkod.ShowDialog();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }

        private void BtnMamulSayim_Click(object sender, RoutedEventArgs e)
        {
            PageSayim sayim = new PageSayim(Sayim.SayimTipi.Mamul);
            sayim.ShowDialog();
        }

        private void BtnHamSayim_Click(object sender, RoutedEventArgs e)
        {
            PageSayim sayim = new PageSayim(Sayim.SayimTipi.Ham);
            sayim.ShowDialog();
        }

        private void BtnKafes_Click(object sender, RoutedEventArgs e)
        {
            PageKafes kafes = new PageKafes();
            kafes.ShowDialog();
        }
    }
}

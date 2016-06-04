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
using LKUI.Details;
using LKUI.Controls;
using System.ComponentModel;
using System.Threading;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSevkiyat.xaml
    /// </summary>
    public partial class PageSevkiyat : UserControl
    {
        public PageSevkiyat()
        {
            InitializeComponent();
        }

        Sevkiyat _Islem = new Sevkiyat();

        private void DGridSevkBelge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;
            ChildBelge.DataContext = _Islem.SevkBelge;
            if (_Islem.Okutulanlar != null && _Islem.Okutulanlar.Count != 0 && _Islem.SevkBelge.TipRenkKontrolDevreDisi) CheckTipRenkDevreDisi.IsEnabled = false;
            else CheckTipRenkDevreDisi.IsEnabled = true;
            ChildBelge.Show();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            _Islem.SevkBelge = new vSevk() { Tarih = DateTime.Today };
            ChildBelge.DataContext = _Islem.SevkBelge;
            ChildBelge.Show();
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;
            ChildBelge.DataContext = _Islem.SevkBelge;
            if (_Islem.Okutulanlar != null && _Islem.Okutulanlar.Count != 0 && _Islem.SevkBelge.TipRenkKontrolDevreDisi) CheckTipRenkDevreDisi.IsEnabled = false;
            else CheckTipRenkDevreDisi.IsEnabled = true;
            ChildBelge.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vSevk secilen = DGridSevkBelge.SelectedItem as vSevk;
            if (secilen == null) return;

            if (MessageBox.Show("Kayıt silinsin mi ?\n\nBelge No : " + _Islem.SevkBelge.BelgeNo + "\nSipariş No : " + _Islem.SevkBelge.SozlesmeNo, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.SevkiyatSil())
            {
                LoadPage();
                MessageBox.Show("Kayıt silindi..!\n\nBelge No : " + secilen.BelgeNo + "\nSipariş No : " + secilen.SozlesmeNo, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildBelge.Close();
        }

        private void Refresh()
        {
            DGridSiparisler.ItemsSource = null;
            DGridOkutulan.ItemsSource = null;
            DGridSiparisler.ItemsSource = _Islem.Siparisleri;
            DGridOkutulan.ItemsSource = _Islem.Okutulanlar;
        }

        private void DGridSevkBelge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Islem.SevkBelge = DGridSevkBelge.SelectedItem as vSevk;
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;

            CmbMusteri.ItemsSource = Sevkiyat.MusterileriGetir();
            CmbSevkEden.ItemsSource = Sevkiyat.SevkPersoneliGetir();
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridOkutulan.ItemsSource = null;
            DGridSiparisler.ItemsSource = null;
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

        private void ChildBelge_Closed(object sender, EventArgs e)
        {
            if (_Islem.SevkBelge != null) DGridSevkBelge.SelectedItem = _Islem.SevkBelge;
        }

        private void BtnBarkodSil_Click(object sender, RoutedEventArgs e)
        {
            vSevkiyatBarkodlari silinecek = DGridOkutulan.SelectedItem as vSevkiyatBarkodlari;
            if (silinecek == null) return;

            if (MessageBox.Show("Kayıt silinsin mi ?\n\nBarkod : " + silinecek.Barkod, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.BarkodSil(silinecek))
            {
                Refresh();
                MessageBox.Show("Kayıt silindi..!\n\nBarkod : " + silinecek.Barkod, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
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
                    TxtBarkod.Text = "";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }

            }
        }

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

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (CmbMusteri.SelectedItem == null || CmbSiparis.SelectedItem == null)
            {
                MessageBox.Show("Mavi renkteki alanları doldurunuz..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_Islem.SevkiyatKaydet())
            {
                ChildBelge.Close();
                LoadPage();
                DGridSevkBelge.SelectedItem = _Islem.SevkBelge;
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
            ChildGenel.Caption = "";
            ChildGenel.Top = 0;
            ChildGenel.Left = 0;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar secilen = DGridOkutulan.SelectedItem as vMamulKumaslar;
            if (secilen == null) return;
            CntIsdTextBox tbox = (sender as CntIsdTextBox);
            if (tbox.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kutu numarası sayısal olmalıdır.\n\nKaydedilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                tbox.Text = "";
                secilen.KutuId = null;
                return;
            }

            secilen.KutuId = tbox.IntTxt;
            if (_Islem.MamulBarkodGuncelle(secilen) == false)
                MessageBox.Show("Hata oluştu.\n\nKutu numarası kaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridSevkBelge_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            _Islem.SevkBelge = DGridSevkBelge.SelectedItem as vSevk;
            Refresh();
        }

        private void MIPackListYazdir_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;

            ChildGenel.Content = new DtlPackList(_Islem.SevkBelge.Id);
            ChildGenel.Show();
        }

        private void MISevkEmriYazdir_Click(object sender, RoutedEventArgs e)
        {
            if (_Islem == null || _Islem.SevkBelge == null || DGridSevkBelge.SelectedItem == null) return;

            Rapor rapor = new Rapor("RprSevkEmriListesi");
            DtlRapor dtlRapor = new DtlRapor();
            ChildGenel.Content = dtlRapor;
            ChildGenel.Show();

            Microsoft.Reporting.WinForms.ReportDataSource item1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            item1.Name = "DS_SevkEmri";
            item1.Value = _Islem.SevkEmriMamulleriGetir();
            dtlRapor.viewerInstance.LocalReport.DataSources.Add(item1);

            dtlRapor.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
            dtlRapor.viewerInstance.RefreshReport();
        }

        private void MIIrsaliyeyeAktar_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSevkBelge.SelectedItem == null) return;

                Logo lg = new Logo();                
                //lg.MamulKumasIrsaliyeAktar(_Islem.SevkBelge);
                //lg.IplikIrsaliyeAktar(1405);
                //lg.TekKatIplikSarfFisi();
                //lg.AtkiIplikSarfFisi();
                //lg.CozguIplikSarfFisi();
                //lg.CiftKatIplikUretimdenGirisFisi();
                lg.BoyaSarfFisi();
                MessageBox.Show("Irsaliye aktarıldı. Lütfen kontrol ediniz!");      
        }
    }
}

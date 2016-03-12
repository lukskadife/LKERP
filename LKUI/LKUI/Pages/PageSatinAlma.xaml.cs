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
using Telerik.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using LKUI.Details;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSatinAlma.xaml
    /// </summary>
    public partial class PageSatinAlma : UserControl
    {
        public PageSatinAlma()
        {
            InitializeComponent();
        }

        SatinAlma _Islem;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<tblMalzemeler> gruplar = new List<tblMalzemeler>() { new tblMalzemeler() { Id = 0, Adi = "Tümü" } };
            gruplar.AddRange(new vMalzemeler().MalzemeGruplariGetir().OrderBy(o => o.Adi));
            CmbGrup.ItemsSource = gruplar;
            CmbGrup.SelectedIndex = 0;
            CmbTedarikciler.ItemsSource = tblFirmalar.TedarikcileriGetir();
            CmbOdeme.ItemsSource = SatinAlma.OdemeSekilleriGetir();
            RadioAmirOnay.IsChecked = true;
            LoadPage();
        }

        private void LoadPage()
        {
            tblMalzemeler secilen = CmbGrup.SelectedItem as tblMalzemeler;
            DGridBelgeler.ItemsSource = SatinAlma.BelgeleriGetir(secilen.Id, _SeciliDurum);  
        }

        private void ColumnsVisibility()
        {
            if (_Islem.Belge.TalepGrupId == 39)
            {
                DClmRenk.IsVisible = true;
                if (_Islem.Belge.Durum == "Depo" || _Islem.Belge.Durum == "Tamamlandı")
                {
                    DClmAmbalaj.IsVisible = true;
                    DClmLotNo.IsVisible = true;
                    DClmBobin.IsVisible = true;
                }
                else
                {
                    DClmAmbalaj.IsVisible = false;
                    DClmLotNo.IsVisible = false;
                    DClmBobin.IsVisible = false;
                }
            }
            else
            {
                DClmRenk.IsVisible = false;
                DClmAmbalaj.IsVisible = false;
                DClmLotNo.IsVisible = false;
                DClmBobin.IsVisible = false;
            }

            if (_Islem.Belge.Durum == "Depo")
            {
                DClmMiktar.IsVisible = false;
                DClmGelecek.IsVisible = false;
                DClmMevcut.IsVisible = false;
            }
            else
            {
                DClmMiktar.IsVisible = true;
                DClmGelecek.IsVisible = true;
                DClmMevcut.IsVisible = true;
            }

            DClmDepo.IsVisible = (_Islem.Belge.Durum == "Depo" || _Islem.Belge.Durum == "Tamamlandı") ? true : false;
            DClmBFiyat.IsVisible = (_Islem.Belge.Durum == "Satın Al" || _Islem.Belge.Durum == "Onay" || _Islem.Belge.Durum == "Tedarik Et") ? true : false;
            DClmKur.IsVisible = (_Islem.Belge.Durum == "Satın Al" || _Islem.Belge.Durum == "Onay" || _Islem.Belge.Durum == "Tedarik Et") ? true : false;
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            tblMalzemeler seciliGrup = CmbGrup.SelectedItem as tblMalzemeler;
            if (seciliGrup.Id == 0)
            {
                MessageBox.Show("Grup seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _Islem = new SatinAlma(seciliGrup.Id, App.PersonelId);

            ColumnsVisibility();

            BtnSatirEkle.Visibility = System.Windows.Visibility.Visible;
            GrdBelge.DataContext = _Islem.Belge;
            DGridTalepler.ItemsSource = _Islem.Talepler;
            ChildTalepler.Show();
        }

        private void Duzelt()
        {
            vTalepKarsilama secilen = DGridBelgeler.SelectedItem as vTalepKarsilama;
            if (secilen == null) return;

            _Islem = new SatinAlma(secilen.TalepGrupId.Value, App.PersonelId, secilen);

            ColumnsVisibility();

            GrdBelge.DataContext = _Islem.Belge;
            DGridTalepler.ItemsSource = _Islem.Talepler;
            ChildTalepler.Show();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vTalepKarsilama secilen = DGridBelgeler.SelectedItem as vTalepKarsilama;
            if (secilen == null) return;

            if (secilen.Durum == "Amir Onayı" || secilen.Durum == "Satın Al" || secilen.Durum == "Onay")
            {
                if (MessageBox.Show(secilen.No + " numaralı talep silinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;

                if (SatinAlma.BelgeSil(secilen)) LoadPage();
                else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show(secilen.Durum + " durumundaki satın alma silinemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void SatirEkle()
        {
            if (_Islem.Belge.Durum != "Amir Onayı") return;
            _Islem.SatirEkle();
            DGridTalepler.ItemsSource = null;
            DGridTalepler.ItemsSource = _Islem.Talepler;
        }

        private void BtnSatirEkle_Click(object sender, RoutedEventArgs e)
        {
            SatirEkle();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Islem.Belge = GrdBelge.DataContext as vTalepKarsilama;
                if (_Islem.Kaydet())
                {
                    LoadPage();
                    GrdBelge.DataContext = null;
                    GrdBelge.DataContext = _Islem.Belge;
                    MessageBox.Show("Talebiniz " + _Islem.Belge.No + " talep numarasıyla kaydedilmiştir..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        string _SeciliDurum = "";

        private void CmbGrup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();           
        }

        private void ChildTalepler_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3) SatirEkle();
        }

        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBox combo = sender as RadComboBox;
            vTalepKarsilamaAct sec = combo.DataContext as vTalepKarsilamaAct;
            if (sec != null && combo.SelectedItem != null && sec.MalzemeKodu != (combo.SelectedItem as tblMalzemeler).Kodu)
            {
                _Islem.SatirGuncelle(sec);
                DGridTalepler.Items.Refresh();
            }
        }

        private void RadioAmirOnay_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Amir Onayı";
            BtnSatirEkle.Visibility = System.Windows.Visibility.Visible;
            LoadPage();
        }

        private void RadioSatinAl_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Satın Al";
            BtnTeklifYazdir.Visibility = System.Windows.Visibility.Visible;
            BtnSatinAlmaYazdir.Visibility = System.Windows.Visibility.Hidden;
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void RadioOnay_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Onay";
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void RadioTedarikEt_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Tedarik Et";
            BtnTeklifYazdir.Visibility = System.Windows.Visibility.Hidden;
            BtnSatinAlmaYazdir.Visibility = System.Windows.Visibility.Visible;
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void RadioDepo_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Depo";
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void RadioTamamlandi_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Tamamlandı";
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void RadioIptal_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "İptal";
            BtnSatirEkle.Visibility = System.Windows.Visibility.Hidden;
            LoadPage();
        }

        private void ChildTalepler_Closed(object sender, EventArgs e)
        {
            GrdBelge.DataContext = null;
            DGridTalepler.ItemsSource = null;
            ChildDijitalBelgeler.Close();
            ChildGenel.Close();
        }

        private void BtnSatirSil_Click(object sender, RoutedEventArgs e)
        {
            vTalepKarsilamaAct secilen = (sender as Button).DataContext as vTalepKarsilamaAct;
            DGridTalepler.SelectedItem = secilen;

            if (MessageBox.Show("Satır silinecek..!\n\nNot: Silme işlemi geri alınamaz.", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            if (secilen == null) return;
            if (_Islem.SatirSil(secilen))
            {
                DGridTalepler.ItemsSource = null;
                DGridTalepler.ItemsSource = _Islem.Talepler;
            }
            else MessageBox.Show("Hata oluştu.\n\nSatır silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridBelgeler_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void BtnIrsaliyeEkle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "İRSALİYE SEÇ";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                if (_Islem.DijitalBelgeEkle(secilenDosya, SatinAlma.BelgeEnumu.Irsaliye))
                    MessageBox.Show("Dosya eklendi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Hata..!\n\nDosya eklenemedi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnFaturaEkle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "FATURA SEÇ";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                if (_Islem.DijitalBelgeEkle(secilenDosya, SatinAlma.BelgeEnumu.Fatura))
                    MessageBox.Show("Dosya eklendi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Hata..!\n\nDosya eklenemedi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGoster_Click(object sender, RoutedEventArgs e)
        {
            DGridDijitalBelge.ItemsSource = _Islem.DijitalBelgeleriGetir();
            ChildDijitalBelgeler.Show();
        }

        private void BtnDijitalAc_Click(object sender, RoutedEventArgs e)
        {
            tblTalepKarsilamaBelgeleri secilen = (sender as FrameworkElement).DataContext as tblTalepKarsilamaBelgeleri;
            if (secilen == null) return;
            DGridDijitalBelge.SelectedItem = secilen;

            _Islem.DijitalBelgeAc(secilen);
        }
        
        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void BtnSatinAlmaYazdir_Click(object sender, RoutedEventArgs e)
        {
            DtlRapor raporlama = new DtlRapor();

            List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSTalepKarsilama", new List<vTalepKarsilama>(){_Islem.Belge}),
                    new DtlRapor.RaporItem("DSTalepKarsilamaAct", _Islem.Talepler)
                };
            raporlama.RaporGoster("RprSatinAlma", list);

            ChildGenel.Content = raporlama;
            ChildGenel.Show();
        }

        private void BtnTeklifYazdir_Click(object sender, RoutedEventArgs e)
        {
            DtlRapor raporlama = new DtlRapor();

            List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSTalepKarsilama", new List<vTalepKarsilama>(){_Islem.Belge}),
                    new DtlRapor.RaporItem("DSTalepKarsilamaAct", _Islem.Talepler)
                };
            raporlama.RaporGoster("RprTeklifFormu", list);

            ChildGenel.Content = raporlama;
            ChildGenel.Show();
        }
    }
}

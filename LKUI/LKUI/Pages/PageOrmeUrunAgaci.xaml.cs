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
using LKUI.Details;
using LKLibrary.DbClasses;
using LKLibrary.Classes;
using LKUI.Controls;
using LKUI.Classes;
using Microsoft.Win32;
using System.IO;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageOrmeUrunAgaci.xaml
    /// </summary>
    public partial class PageOrmeUrunAgaci : UserControl
    {
        List<vKumas> LstKumas;
        FuarKumas _KumasIslem = new FuarKumas();

        public PageOrmeUrunAgaci()
        {
            InitializeComponent();
        }

        void LoadPage()
        {
            LstKumas = _KumasIslem.KumaslariGetir().Where(c=>c.KumasCinsi == 396).ToList();

            DGridDokumaUrunAgaci.ItemsSource = LstKumas;
        }

        private void Duzelt()
        {
            DtlOrmeUrunAgaci DtlAgac = new DtlOrmeUrunAgaci(DGridDokumaUrunAgaci.SelectedItem as vKumas);
            DtlAgac.Kaydedildi += () =>
            {
                ChildDokumaUrunAgaci.Close();
            };
            DtlAgac.Vazgecildi += () =>
            {
                ChildDokumaUrunAgaci.Close();
            };
            ChildDokumaUrunAgaci.Content = DtlAgac;
            ChildDokumaUrunAgaci.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnTipEkle_Click(object sender, RoutedEventArgs e)
        {
            vKumas yeniKumas = new vKumas()
            {
                TipNo = "",
                TipAdi = "",
                Varyant = "",
                DovizId = 32,
                Yik1 = false,
                Yik2 = false,
                Yik3 = false,
                Yik4 = false,
                Yik5 = false,
                Dosemelik = false,
                Perdelik = false,
                Elbiselik = false,
                Likrali = false,
                AktifMi = true
            };

            DtlOrmeUrunAgaci DtlAgac = new DtlOrmeUrunAgaci(yeniKumas);
            DtlAgac.Kaydedildi += () =>
            {
                ChildDokumaUrunAgaci.Close();
            };
            DtlAgac.Vazgecildi += () =>
            {
                ChildDokumaUrunAgaci.Close();
            };
            ChildDokumaUrunAgaci.Content = DtlAgac;
            ChildDokumaUrunAgaci.Show();
        }

        private void BtnTipDüzelt_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void ChildDokumaUrunAgaci_Closed(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void BtnTipSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridDokumaUrunAgaci.SelectedItem == null) return;

            vKumas silinecek = DGridDokumaUrunAgaci.SelectedItem as vKumas;

            if (MessageBox.Show("Silinecek..?\n\nTip No : " + silinecek.TipNo + "\nTip Adı :" + silinecek.TipAdi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)
                == MessageBoxResult.No)
                return;

            if (_KumasIslem.KumasSil(silinecek))
            {
                MessageBox.Show("Kayıt Silindi..!\n\nTip No : " + silinecek.TipNo + "\nTip Adı :" + silinecek.TipAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void DGridDokumaUrunAgaci_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void LoadTipResimleri(int tipId)
        {
            List<tblKumasResimleri> resimler = tblKumasResimleri.TipResimleriGetir(tipId);

            _Viewer = new CntImageViewer();
            foreach (var item in resimler)
            {
                _Viewer.AddImage(new CntImageViewer.ImageSrc() { Id = item.Id, Image = Operations.StringToImage(item.Resim) });
            }
        }

        CntImageViewer _Viewer;

        private void BtnResimGoster_Click(object sender, RoutedEventArgs e)
        {
            vKumas secilen = (sender as FrameworkElement).DataContext as vKumas;
            if (secilen == null) return;

            LoadTipResimleri(secilen.Id);

            GrdImg.Children.Clear();
            GrdImg.Children.Add(_Viewer);
            ChildResimler.DataContext = secilen;
            ChildResimler.Show();
        }

        private void BtnTipResimSil_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Seçilen resim silinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;

            if (_Viewer == null || _Viewer.SeciliImage == null) return;

            if (tblKumasResimleri.Sil(_Viewer.SeciliImage.Id))
                _Viewer.DeleteImage(_Viewer.SeciliImage);
            else MessageBox.Show("Hata oluştu.\n\nResim silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTipResimEkle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Resim Seç";
            dialog.Filter = "Resim (Jpg, Png, Gif, Bmp)|*.jpg;*.png;*.gif;*.bmp";
            if (dialog.ShowDialog().Value)
            {
                vKumas tip = ChildResimler.DataContext as vKumas;

                string secilenDosya = dialog.FileName;
                if (Operations.DosyaBoyutuGetir(secilenDosya) > 200)
                {
                    MessageBox.Show("200kb 'den büyük resim eklenemez.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                tblKumasResimleri yeniResim = new tblKumasResimleri()
                {
                    Resim = Convert.ToBase64String(ExtensionMethods.FileToByteArray(secilenDosya)),
                    TipId = tip.Id
                };

                if (yeniResim.Ekle()) _Viewer.AddImage(new CntImageViewer.ImageSrc() { Id = yeniResim.Id, Image = Operations.StringToImage(yeniResim.Resim) });
            }
        }

        private void BtnSatisHakki_Click(object sender, RoutedEventArgs e)
        {
            vKumas secilen = (sender as FrameworkElement).DataContext as vKumas;
            if (secilen == null) return;

            DGridSatisHakki.ItemsSource = secilen.SatisHaklariGetir();
            _SecilenTip = secilen;

            ChildSatisHaklari.Caption = "Tip No : " + secilen.TipNo;
            ChildSatisHaklari.Show();
        }

        vKumas _SecilenTip;
        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (_SecilenTip.SatisHaklariKaydet(DGridSatisHakki.ItemsSource as List<vMusteriTipSatisHakkı>))
                MessageBox.Show("Kaydedildi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTakdirNameEkle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Dosya Seç";
            dialog.Filter = "Dosya (Word, Excel, Pdf)|*.doc;*.docx;*.xls;*.xlsx;*.pdf";
            if (dialog.ShowDialog().Value)
            {
                vKumas tip = ChildResimler.DataContext as vKumas;
                string secilenDosya = dialog.FileName;
                if (_SecilenTip.TakdirNameEkle(secilenDosya))
                    DGridTakdirName.ItemsSource = _SecilenTip.TakdirNameleriGetir();
                else MessageBox.Show("Hata oluştu.\n\nDosya eklenemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnTakdirNameGoster_Click(object sender, RoutedEventArgs e)
        {
            tblKumasBelgeleri secilen = (sender as FrameworkElement).DataContext as tblKumasBelgeleri;
            if (secilen == null) return;

            LKLibrary.DosyaServisi.SenfoniFiles file;
            try
            {
                LKLibrary.DosyaServisi.FileOperationServicesClient servis = new LKLibrary.DosyaServisi.FileOperationServicesClient();
                file = servis.GetFile(secilen.DosyaYolu);
            }
            catch { return; }

            if (file != null)
            {
                if (File.Exists(secilen.Adi)) File.Delete(secilen.Adi);
                string path = System.Environment.GetEnvironmentVariable("TEMP") + @"\" + @"LKERP";
                if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                string tempDosyaYolu = path + "\\" + secilen.Adi;
                ExtensionMethods.ByteArrayToFile(tempDosyaYolu, file.FileByteArray);
                System.Diagnostics.Process.Start(tempDosyaYolu);
            }
        }

        private void BtnTakdirName_Click(object sender, RoutedEventArgs e)
        {
            vKumas secilen = (sender as FrameworkElement).DataContext as vKumas;
            if (secilen == null) return;

            DGridTakdirName.ItemsSource = secilen.TakdirNameleriGetir();
            ChildTakdirName.Caption = "Tip No : " + secilen.TipNo;
            ChildTakdirName.Show();
            _SecilenTip = secilen;
        }

        private void BtnKaydetFinishHakki_Click(object sender, RoutedEventArgs e)
        {

            if (_SecilenTip.FinishHaklariKaydet(DGridFinishHakki.ItemsSource as List<vTipFinishHakkı>))
                MessageBox.Show("Kaydedildi..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void BtnFinishHakki_Click(object sender, RoutedEventArgs e)
        {
            vKumas secilen = (sender as FrameworkElement).DataContext as vKumas;
            if (secilen == null) return;

            DGridFinishHakki.ItemsSource = secilen.FinishHaklariGetir();
            _SecilenTip = secilen;

            ChildFinishHaklari.Caption = "Tip No : " + secilen.TipNo;
            ChildFinishHaklari.Show();
        }
  
    }
}

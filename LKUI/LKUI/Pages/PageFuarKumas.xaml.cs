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
using LKLibrary.DbClasses;
using LKLibrary.Classes;
using Microsoft.Win32;
using System.IO;
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageFuarKumas.xaml
    /// </summary>
    public partial class PageFuarKumas : UserControl
    {
        public PageFuarKumas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbTipNo.ItemsSource = vKumas.KumaslariGetir(true);
            CmbKategori.ItemsSource = tblFuarKumasKategori.KategorileriGetir();
            LoadPage();
        }

        private void LoadPage()
        {
            DGridKumaslar.ItemsSource = vFuarKumas.FuarKumaslariGetir();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildKumas.DataContext = new vFuarKumas()
            {
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

            ImgKumas.Source = null;
            ImgThmbKumas.Source = null;
            ChildKumas.Show();
        }

        private void Duzelt()
        {
            vFuarKumas secilen = DGridKumaslar.SelectedItem as vFuarKumas;
            if (secilen == null) return;
            DGridProsesler.ItemsSource = secilen.Prosesler;
            ImgKumas.Source = Operations.StringToImage(secilen.ImgData);
            ImgThmbKumas.Source = Operations.StringToImage(secilen.ImgThumbData);

            ChildKumas.DataContext = secilen;
            ChildKumas.Show();
        }

        private void BtnDüzelt_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vFuarKumas secilen = DGridKumaslar.SelectedItem as vFuarKumas;
            if (secilen == null) return;

            if (MessageBox.Show("Kayıt silinecek..?\n\nTip No : " + secilen.TipNo + "\nRenk No : " + secilen.RenkNo, App.AlertCaption, 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            if (secilen.Sil()) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridKumaslar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            vFuarKumas kumas = ChildKumas.DataContext as vFuarKumas;
            if (kumas == null) return;

            if (CmbTipNo.GirisYapildiMi == false | CmbKategori.GirisYapildiMi == false) return;

            if (TxtKumasEni.TextGirisiDogruMu == false | TxtGramajgm.TextGirisiDogruMu == false | TxtGramajgm2.TextGirisiDogruMu == false | TxtMamulDolar.TextGirisiDogruMu == false |
                TxtMamulEuro.TextGirisiDogruMu == false | TxtMamulTL.TextGirisiDogruMu == false | TxtDesenDolar.TextGirisiDogruMu == false | TxtDesenEuro.TextGirisiDogruMu == false |
                TxtDesenTL.TextGirisiDogruMu == false | TxtBaskiEuro.TextGirisiDogruMu == false | TxtBaskiDolar.TextGirisiDogruMu == false | TxtBaskiTL.TextGirisiDogruMu == false |
                TxtNakisDolar.TextGirisiDogruMu == false | TxtNakisEuro.TextGirisiDogruMu == false | TxtNakisTL.TextGirisiDogruMu == false | TxtHamDolar.TextGirisiDogruMu == false |
                TxtHamEuro.TextGirisiDogruMu == false | TxtHamTL.TextGirisiDogruMu == false)
            {
                return;
            }

            kumas.ImgData = ResimToBase64(ImgKumas);
            kumas.ImgThumbData = ResimToBase64(ImgThmbKumas);

            if (kumas.Kaydet())
            {
                MessageBox.Show("Kaydedildi.");
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!");
        }

        private void ChildKumas_Closed(object sender, EventArgs e)
        {
            ChildKumas.DataContext = null;
            DGridProsesler.ItemsSource = null;
        }

        private void BtnProEkle_Click(object sender, RoutedEventArgs e)
        {
            vFuarKumas kumas = ChildKumas.DataContext as vFuarKumas;
            if (kumas == null || kumas.Id == 0)
            {
                MessageBox.Show("Kumaş kaydedilmeden proses eklenemez..!");
                return;
            }

            DGridProses.ItemsSource = tblFuarProses.ProsesleriGetir();
            ChildProsesEkle.Show();
        }

        private void BtnProSil_Click(object sender, RoutedEventArgs e)
        {
            vFuarKumasProsesleri secilen = DGridProsesler.SelectedItem as vFuarKumasProsesleri;
            if (secilen == null) return;

            vFuarKumas kumas = ChildKumas.DataContext as vFuarKumas;
            if (kumas == null) return;

            if (secilen.Sil()) DGridProsesler.ItemsSource = kumas.Prosesler;
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!");
        }

        private void BtnProKaydet_Click(object sender, RoutedEventArgs e)
        {
            vFuarKumas kumas = ChildKumas.DataContext as vFuarKumas;
            if (kumas == null) return;

            List<tblFuarProses> secilenler = DGridProses.SelectedItems.Cast<tblFuarProses>().ToList();
            if (kumas.ProsesEkle(secilenler))
            {
                DGridProsesler.ItemsSource = kumas.Prosesler;
                ChildProsesEkle.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nEklenemedi..!");
        }

        private void BtnResimSec_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Büyük Resim Seç";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                ImgKumas.Source = Operations.StringToImage(Convert.ToBase64String(ExtensionMethods.FileToByteArray(secilenDosya)));
            }
        }

        private string ResimToBase64(Image img)
        {
            if (img.Source == null) return null;

            byte[] imgArray = ((img.Source as BitmapImage).StreamSource as MemoryStream).ToArray();

            return Convert.ToBase64String(imgArray);
        }

        private void BtnThmbResimSec_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Küçük Resim Seç";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                double dosyaBoyutu = Operations.DosyaBoyutuGetir(secilenDosya);
                if (dosyaBoyutu > 60)
                {
                    MessageBox.Show("60kb 'den büyük dosyalar küçük resme eklenemez.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                ImgThmbKumas.Source = Operations.StringToImage(Convert.ToBase64String(ExtensionMethods.FileToByteArray(secilenDosya)));
            }
        }
    }
}

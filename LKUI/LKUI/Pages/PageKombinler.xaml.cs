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
using LKUI.Classes;
using Microsoft.Win32;
using System.IO;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageKombinler.xaml
    /// </summary>
    public partial class PageKombinler : UserControl
    {
        public PageKombinler()
        {
            InitializeComponent();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildKombin.DataContext = new tblFuarKombin() { KombinAdi = "", KombinNo = "" };
            ImgKombin.Source = null;
            ImgThumbKombin.Source = null;
            ChildKombin.Show();
        }


        private void BtnTipEkle_Click(object sender, RoutedEventArgs e)
        {

            DGridKumaslar.ItemsSource = vFuarKumas.FuarKumaslariGetir();
            ChildFuarKumaslar.Show();
        }
 
        FuarKumas _Kumas = new FuarKumas();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        void LoadPage()
        {
            DGridKombinler.ItemsSource = _Kumas.KombinleriGetir();
        }

        void Duzelt()
        {
            tblFuarKombin kombin = DGridKombinler.SelectedItem as tblFuarKombin;
            if (kombin == null) return;

            ChildKombin.DataContext = kombin;
            ImgKombin.Source = Operations.StringToImage(kombin.ImgData);
            ImgThumbKombin.Source = Operations.StringToImage(kombin.ImgThumbData);

            ChildKombin.Show();
        }

        private string ResimToBase64(Image img)
        {
            if (img.Source == null) return null;

            byte[] imgArray = ((img.Source as BitmapImage).StreamSource as MemoryStream).ToArray();

            return Convert.ToBase64String(imgArray);
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            tblFuarKombin kombin = ChildKombin.DataContext as tblFuarKombin;
            kombin.ImgData = ResimToBase64(ImgKombin);
            kombin.ImgThumbData = ResimToBase64(ImgThumbKombin);

            if (kombin.Id == 0 && _Kumas.KombinNoVarMi(kombin.KombinNo.Trim()) == true)
            {
                MessageBox.Show("Kombin numarası daha önce kullanılmıştır.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (_Kumas.KombinKaydet(kombin))
            {
                LoadPage();
                ChildKombin.Close();
            }
            else MessageBox.Show("Kaydetme sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void Sil_Click(object sender, RoutedEventArgs e)
        {
            tblFuarKombin kombin = DGridKombinler.SelectedItem as tblFuarKombin;
            if (kombin == null) return;

            if (MessageBox.Show(kombin.KombinNo + " numaralı kombin silinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Kumas.KombinSil(kombin))
            {
                LoadPage();
                DGridKombinKumaslar.ItemsSource = null;
                ImgThumbKombinView.Source = null;
                MessageBox.Show(kombin.KombinNo + " numaralı kombin silindi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Silme esnasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridKombinler_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void BtnFuarKumasSecim_Click(object sender, RoutedEventArgs e)
        {
            tblFuarKombin kombin = DGridKombinler.SelectedItem as tblFuarKombin;
            List<vFuarKumas> secilenler = DGridKumaslar.SelectedItems.Cast<vFuarKumas>().ToList();

            if (_Kumas.KombineKumasEkle(secilenler, kombin.Id))
            {
                DGridKombinKumaslar.ItemsSource = _Kumas.KombinKumaslariGetir(kombin.Id);
                ChildFuarKumaslar.Close();
            }
        }

        private void BtnKumasSil_Click(object sender, RoutedEventArgs e)
        {
            vFuarKombinKumaslar kombinKumas = DGridKombinKumaslar.SelectedItem as vFuarKombinKumaslar;
            if (kombinKumas == null) return;

            if (MessageBox.Show("Seçili kayıt silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                return;

            if (_Kumas.KombinKumasSil(kombinKumas) == true)
            {
                DGridKombinKumaslar.ItemsSource = _Kumas.KombinKumaslariGetir(kombinKumas.KombinId);
                DGridKombinKumaslar.Items.Refresh();
            }
            else MessageBox.Show("Silme esnasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnThumbResimSec_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Küçük Resim Seç";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                FileInfo info = new FileInfo(secilenDosya);
                double dosyaBoyutu = Math.Round((double)info.Length / 1024, 2);
                if (dosyaBoyutu > 60)
                {
                    MessageBox.Show("60kb 'den büyük dosyalar küçük resme eklenemez.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                ImgThumbKombin.Source = Operations.StringToImage(Convert.ToBase64String(ExtensionMethods.FileToByteArray(secilenDosya)));
            }
        }

        private void BtnResimSec_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Büyük Resim Seç";
            if (dialog.ShowDialog().Value)
            {
                string secilenDosya = dialog.FileName;
                ImgKombin.Source = Operations.StringToImage(Convert.ToBase64String(ExtensionMethods.FileToByteArray(secilenDosya)));
            }
        }

        private void DGridKombinler_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            tblFuarKombin secilen = DGridKombinler.SelectedItem as tblFuarKombin;
            if (secilen == null || secilen.ImgThumbData == null) ImgThumbKombinView.Source = null;
            else ImgThumbKombinView.Source = Operations.StringToImage(secilen.ImgThumbData);

            DGridKombinKumaslar.ItemsSource = secilen == null ? null : _Kumas.KombinKumaslariGetir(secilen.Id);
        }

        private void BtnKumasEkle_Click(object sender, RoutedEventArgs e)
        {
            tblFuarKombin secilen = DGridKombinler.SelectedItem as tblFuarKombin;
            if (secilen == null) return;

            DGridKumaslar.ItemsSource = vFuarKumas.FuarKumaslariGetir();
            ChildFuarKumaslar.Caption = secilen.KombinNo + " - " + secilen.KombinAdi + " kombinine eklenecek kumaşları seçiniz...";
            ChildFuarKumaslar.Show();
        }
    }
}

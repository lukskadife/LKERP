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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageKafesKartlari.xaml
    /// </summary>
    public partial class PageKafesKartlari : UserControl
    {
        public PageKafesKartlari()
        {
            InitializeComponent();
        }

        private bool duzelt = false;
        tblAyarlar secilen;

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            clear();
            KafesKartEkleDuzenle.Show();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdi.Text == null | txtAdi.Text.Trim().ToString() == "")
            {
                MessageBox.Show("Adı alanını doldurunuz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtBarkod.Text == null | txtBarkod.Text.Trim().ToString() == "")
            {
                MessageBox.Show("Barkod alanını doldurunuz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!duzelt)
            {
                tblAyarlar kayit = new tblAyarlar();
                kayit.Adi = txtAdi.Text.ToString();
                kayit.Deger = txtBarkod.Text.ToString();

                if (!Ayarlar.KafesKartlariniKaydet(kayit)) MessageBox.Show("Hata oluştu.\n\nKayıt edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                secilen = DGridKafesKartlari.SelectedItem as tblAyarlar;
                secilen.Adi = txtAdi.Text.ToString();
                secilen.Deger = txtBarkod.Text.ToString();

                if (!Ayarlar.KafesKartniDuzelt(secilen)) MessageBox.Show("Hata oluştu.\n\nDüzeltilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            duzelt = false;
            DataLoad();
            KafesKartEkleDuzenle.Close();
            clear();
        }

        private void btnDuzelt_Click(object sender, RoutedEventArgs e)
        {

            if (DGridKafesKartlari.SelectedItem != null)
            {
                secilen = DGridKafesKartlari.SelectedItem as tblAyarlar;
                KafesKartEkleDuzenle.Show();
                clear();
                txtAdi.Text = secilen.Adi;
                txtBarkod.Text = secilen.Deger;
                duzelt = true;
            }

            else MessageBox.Show("Düzeltilecek Kaydı Seçiniz!...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Silme işlemini onaylıyor musunuz?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (DGridKafesKartlari.SelectedItem != null)
            {
                tblAyarlar secilen = DGridKafesKartlari.SelectedItem as tblAyarlar;
                Ayarlar.KafesKartniSil(secilen);
                DataLoad();
            }

            else MessageBox.Show("Sililenecek Kaydı Seçiniz!...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void clear()
        {
            txtAdi.Clear();
            txtBarkod.Clear();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }

        private void DataLoad()
        {
            DGridKafesKartlari.ItemsSource = null;
            DGridKafesKartlari.ItemsSource = Ayarlar.KafesKartlariniGetir();
        }
    }
}

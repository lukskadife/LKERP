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
    /// Interaction logic for PageKullanici.xaml
    /// </summary>
    public partial class PageKullanici : UserControl
    {
        public PageKullanici()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPersonel.ItemsSource = tblPersoneller.PersonelleriGetir();
            LoadKullanicilar();
        }

        private void LoadKullanicilar()
        {
            DGridKullanici.ItemsSource = vKullanicilar.KullanicilariGetir();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            Ekle();
        }

        private void Ekle()
        {
            pbEskiSifre.IsEnabled = false;
            ChildKullanici.DataContext = new vKullanicilar();
            ChildKullanici.Show();            
        }

        private void Duzelt()
        {
            vKullanicilar record = DGridKullanici.SelectedItem as vKullanicilar;
            if (record == null) return;

            pbEskiSifre.IsEnabled = true;
            ChildKullanici.DataContext = record;
            ChildKullanici.Show();
        }

        private void DGridKullanici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void BtnDüzelt_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            vKullanicilar record = ChildKullanici.DataContext as vKullanicilar;
            record.Sifre = pbSifre.Password;

            if (cmbPersonel.SelectedValue == null)
            {
                MessageBox.Show("Personel seçiniz..!", App.AlertCaption);
                return;
            }
            if (string.IsNullOrEmpty(pbSifre.Password))
            {
                MessageBox.Show("Bir şifre giriniz..!", App.AlertCaption);
                return;
            }
            if (string.IsNullOrEmpty(pbSifreOnay.Password))
            {
                MessageBox.Show("Şifreyi tekrarlayınız..!", App.AlertCaption);
                return;
            }
            if (pbSifre.Password != pbSifreOnay.Password)
            {
                MessageBox.Show("Onay şifresi, şifre ile aynı olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            try
            {
                if (record.KullaniciKaydet(pbEskiSifre.Password))
                {
                    LoadKullanicilar();
                    ChildKullanici.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKullanıcı kaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void ChildKullanici_Closed(object sender, EventArgs e)
        {
            pbEskiSifre.Password = "";
            pbSifre.Password = "";
            pbSifreOnay.Password = "";
        }
    }
}

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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlYetkiler.xaml
    /// </summary>
    public partial class DtlYetkiler : UserControl
    {
        public DtlYetkiler()
        {
            InitializeComponent();
        }

        Yetki _Yetki;
        private void RBtnBolum_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false) return;

            TViewYetkiler.ItemsSource = null;
            CmbBolumler.Visibility = System.Windows.Visibility.Visible;
            CmbPersonel.Visibility = System.Windows.Visibility.Hidden;

            CmbBolumler.SelectedValue = null;
        }

        private void RBtnKullanici_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false) return;

            TViewYetkiler.ItemsSource = null;
            CmbBolumler.Visibility = System.Windows.Visibility.Hidden;
            CmbPersonel.Visibility = System.Windows.Visibility.Visible;

            CmbPersonel.SelectedValue = null;
        }

        private void CmbBolumler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBolumler.SelectedValue == null) return;

            _Yetki = new Yetki(bolumId: (int)CmbBolumler.SelectedValue);
            TViewYetkiler.ItemsSource = _Yetki.YetkileriGetir();
        }

        private void CmbPersonel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbPersonel.SelectedValue == null) return;

            _Yetki = new Yetki(personelId: (int)CmbPersonel.SelectedValue);
            TViewYetkiler.ItemsSource = _Yetki.YetkileriGetir();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            List<vYetkiTanim> yetkiler = TViewYetkiler.ItemsSource as List<vYetkiTanim>;

            List<vYetkiTanim> yeniYetkiler = new List<vYetkiTanim>();

            foreach (vYetkiTanim item in yetkiler)
            {
                if (item.AltYetkiler.Count > 0) foreach (vYetkiTanim itemAlt in item.AltYetkiler) yeniYetkiler.Add(itemAlt);
                yeniYetkiler.Add(item);
            }

            if (_Yetki.YetkiKaydet(yeniYetkiler))
                MessageBox.Show("Yetkiler kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbBolumler.ItemsSource = new vPersonelBolumleri().PersonelBolumleriGetir();
            CmbPersonel.ItemsSource = tblPersoneller.PersonelleriGetir(true);

            RBtnKullanici_Checked(sender, e);
        }
    }
}

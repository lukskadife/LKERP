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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlMinStoklar.xaml
    /// </summary>
    public partial class DtlMinStoklar : UserControl
    {
        public DtlMinStoklar()
        {
            InitializeComponent();
        }

        List<tblMalzemeler> _ListMalzeme;
        private Stok _StokIslem;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _StokIslem = new Stok();
            _ListMalzeme = _StokIslem.MinStoklariGetir();
            DGridMinStoklar.ItemsSource = _ListMalzeme;
        }

        private void TxtKod_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtAd.Text)) DGridMinStoklar.ItemsSource = _ListMalzeme.FindAll(c => c.Kodu.ToUpper().Contains(TxtKod.Text.ToUpper()));
            else DGridMinStoklar.ItemsSource = _ListMalzeme.FindAll(c => c.Kodu.ToUpper().Contains(TxtKod.Text.ToUpper()) && c.Adi.ToUpper().Contains(TxtAd.Text.ToUpper()));
        }

        private void TxtAd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtKod.Text)) DGridMinStoklar.ItemsSource = _ListMalzeme.FindAll(c => c.Adi.ToUpper().Contains(TxtAd.Text.ToUpper()));
            else DGridMinStoklar.ItemsSource = _ListMalzeme.FindAll(c => c.Adi.ToUpper().Contains(TxtAd.Text.ToUpper()) && c.Kodu.ToUpper().Contains(TxtKod.Text.ToUpper()));
        }

        private void BtnMinStoklariKaydet_Click(object sender, RoutedEventArgs e)
        {
            List<tblMalzemeler> listToUpdate = _ListMalzeme.FindAll(c => c.MinStokDegistiMi == true);
            if (_StokIslem.MinStoklariGuncelle(listToUpdate))
            {
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                _ListMalzeme.ForEach(c => c.MinStokDegistiMi = false);
            }
            else MessageBox.Show("Hata oluştu..!\n\nKaydetme başarısız.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

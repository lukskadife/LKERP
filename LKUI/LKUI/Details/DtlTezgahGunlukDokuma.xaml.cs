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
    /// Interaction logic for DtlTezgahGunlukDokuma.xaml
    /// </summary>
    public partial class DtlTezgahGunlukDokuma : UserControl
    {
        public DtlTezgahGunlukDokuma()
        {
            InitializeComponent();
        }

        Makina makina = new Makina();
        List<tblMakinalar> listMakinalar;
        List<tblAyarlar> listTezgahVersiyonlari;

        private void TxtTezgahAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridDokumaPeriyot.ItemsSource = listMakinalar.FindAll(c => c.Adi.ToUpper().Contains(TxtTezgahAdi.Text.ToUpper()));
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {            
            if (makina.MakinaKaydet(DGridDokumaPeriyot.ItemsSource as List<tblMakinalar>))
            {
                Load();
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            listMakinalar = makina.MakinalariGetir(1);
            listTezgahVersiyonlari = tblAyarlar.TezgahVersiyonlari().OrderBy(c => c.Adi).ToList();
            listMakinalar.ForEach(c => c.TezgahVersiyonlari = listTezgahVersiyonlari);
            DGridDokumaPeriyot.ItemsSource = listMakinalar;
        }
    }
}

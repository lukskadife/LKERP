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
    /// Interaction logic for DtlBoyahaneGunlukMetraj.xaml
    /// </summary>
    public partial class DtlBoyahaneGunlukMetraj : UserControl
    {
        public DtlBoyahaneGunlukMetraj()
        {
            InitializeComponent();
        }

        Makina makina = new Makina();
        List<tblMakinalar> listMakinalar;

        private void TxtMakinaAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridMakina.ItemsSource = listMakinalar.FindAll(c => c.Adi.ToUpper().Contains(TxtMakinaAdi.Text.ToUpper()));
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (makina.MakinaKaydet(listMakinalar)) MessageBox.Show("Kaydedildi", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            listMakinalar = makina.MakinalariGetir(2);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listMakinalar = makina.MakinalariGetir(2);
            DGridMakina.ItemsSource = listMakinalar;
        }
    }
}

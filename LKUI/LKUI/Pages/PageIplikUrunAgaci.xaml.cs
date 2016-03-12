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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageİplikUrunAgaci.xaml
    /// </summary>
    public partial class PageIplikUrunAgaci : UserControl
    {
        public PageIplikUrunAgaci()
        {
            InitializeComponent();
        }

        vMalzemeler _Malzeme = new vMalzemeler();
        List<tblMalzemeler> _Iplikler;
        List<tblBukumDagitimAnahtari> _IplikGrubu;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _Iplikler = _Malzeme.IplikleriGetir();
            _IplikGrubu = tblBukumDagitimAnahtari.BukumDagitimAnahtariGetir().OrderBy(c=>c.Adi).ToList();
            _Iplikler.ForEach(c => c.ListIplikGrubu = _IplikGrubu);
            DGridİplikler.ItemsSource = _Iplikler;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (_Malzeme.MalzemeKaydet(DGridİplikler.ItemsSource as List<tblMalzemeler>))
            {
                _Iplikler = _Malzeme.IplikleriGetir();
                _IplikGrubu = tblBukumDagitimAnahtari.BukumDagitimAnahtariGetir();
                _Iplikler.ForEach(c => c.ListIplikGrubu = _IplikGrubu);
                DGridİplikler.ItemsSource = _Iplikler;
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

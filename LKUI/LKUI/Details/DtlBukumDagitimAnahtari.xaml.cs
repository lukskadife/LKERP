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
    /// Interaction logic for DtlBukumDagitimAnahtari.xaml
    /// </summary>
    public partial class DtlBukumDagitimAnahtari : UserControl
    {
        public DtlBukumDagitimAnahtari()
        {
            InitializeComponent();
        }

        tblBukumDagitimAnahtari DagitimAnahtari = new tblBukumDagitimAnahtari();
        List<tblBukumDagitimAnahtari> ListDagitimAnahtari = new List<tblBukumDagitimAnahtari>();
        
        public void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (DagitimAnahtari.Kaydet(ListDagitimAnahtari)) MessageBox.Show("Kaydedildi", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            LoadGrid();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            tblBukumDagitimAnahtari secilen = DGridBukumDagitimAnahtari.SelectedItem as tblBukumDagitimAnahtari;

            if (secilen == null) return;
            if (MessageBox.Show("Dağıtım anahtarı silinecek..?\n\nAnahtar : " + secilen.Adi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (secilen.Sil()) LoadGrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            ListDagitimAnahtari = tblBukumDagitimAnahtari.BukumDagitimAnahtariGetir();
            DGridBukumDagitimAnahtari.ItemsSource = ListDagitimAnahtari;
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {

            List<tblBukumDagitimAnahtari> temp = (DGridBukumDagitimAnahtari.ItemsSource as List<tblBukumDagitimAnahtari>);
            temp.Add(new tblBukumDagitimAnahtari()
            {
                Adi = null,
                Iscilik_Katsayisi = 0,
                Genel_Uretim_Katsayisi = 0               
            });

            DGridBukumDagitimAnahtari.ItemsSource = null;
            DGridBukumDagitimAnahtari.ItemsSource = temp;
        }
    }
}

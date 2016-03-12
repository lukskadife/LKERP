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
    /// Interaction logic for PageBaskiDesenUrunAgaci.xaml
    /// </summary>
    public partial class PageBaskiDesenUrunAgaci : UserControl
    {
        public PageBaskiDesenUrunAgaci()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridBaskiDesenUrunAgaci.ItemsSource = tblBaskiDesenUrunAgaci.UrunAgaciniGetir();
            CmbGrupAdi.ItemsSource = tblBaskiDesenUrunAgaci.GrupAdiGetir();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtKodu.TextGirisiDogruMu == false | CmbGrupAdi.SelectedValue.ToString().Equals("0")) return;

            tblBaskiDesenUrunAgaci urunAgaci = ChildProses.DataContext as tblBaskiDesenUrunAgaci;                       
            try
            {
                if (tblBaskiDesenUrunAgaci.Kaydet(ref urunAgaci) == true)
                {
                    DGridBaskiDesenUrunAgaci.Items.Refresh();
                    ChildProses.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildProses.DataContext = new tblBaskiDesenUrunAgaci() { Id = 0, BaskiDesenGrupTanimId=0  };
            ChildProses.Show();
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (DGridBaskiDesenUrunAgaci.SelectedItem == null) return;

            ChildProses.DataContext = DGridBaskiDesenUrunAgaci.SelectedItem; // convert viewTotable gerikiyor.
            ChildProses.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            //tblBaskiDesenUrunAgaci proc = DGridBaskiDesenUrunAgaci.SelectedItem as vBaskiDesenUrunAgaci; //convert viewTotable lazım
            //if (MessageBox.Show("Kayıt silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //    return;

            //if (tblProses.ProsesSil(proc))
            //{
            //    (DGridBaskiDesenUrunAgaci.ItemsSource as List<tblProses>).Remove(proc);
            //    DGridBaskiDesenUrunAgaci.Items.Refresh();
            //    DGridBaskiDesenUrunAgaci.Items.FilterDescriptors.Clear();
            //}
            //else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}

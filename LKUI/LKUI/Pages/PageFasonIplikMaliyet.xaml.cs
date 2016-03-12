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
using Telerik.Windows.Controls;
using System.Collections;
using System.Data;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageFasonIplikMaliyet.xaml
    /// </summary>
    /// 

    public partial class PageFasonIplikMaliyet : UserControl
    {

        List<vFasonIplikMaliyet> liste;
        List<tblMalzemeler> listeIplikler;
        Iplik _Islem = new Iplik();
        bool ilk = true;

        public PageFasonIplikMaliyet()
        {
            InitializeComponent();
        }

        private void DataLoad()
        {
            liste = Iplik.FasonIplikMaliyetleriGetir().OrderByDescending(c => c.Tarih).ToList();
            listeIplikler = Iplik.IplikleriGetir();
            liste.ForEach(c => c.ListIplikler = listeIplikler);
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ilk)
            {
                if (DPBaslangic.SelectedDate.HasValue == true && DPBitis.SelectedDate.HasValue == true) DGridIplikMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
                
                else MessageBox.Show("Lütfen tarihleri seçiniz! ...");
                
            }

        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ilk)
            {

                if (DPBaslangic.SelectedDate.HasValue == true && DPBitis.SelectedDate.HasValue == true) DGridIplikMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
                
                else MessageBox.Show("Lütfen tarihleri seçiniz! ...");

            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (DGridIplikMaliyet.ItemsSource == null) return;

            if (_Islem.FasonIplikKaydet(DGridIplikMaliyet.ItemsSource as List<vFasonIplikMaliyet>)) MessageBox.Show("Kayıt edildi...", "Kayıt İşlemi", MessageBoxButton.OK, MessageBoxImage.Information);

            else
            {
                MessageBox.Show("Kayıt edilmedi!...", "Kayıt İşlemi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Refresh();
        }

        private void Refresh()
        {
            DataLoad();
            DGridIplikMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
        }

        private void PageLoad(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            DPBaslangic.SelectedDate = date.AddDays(-(DateTime.Now.Day - 1));
            DPBitis.SelectedDate = DateTime.Now;
            DataLoad();
            ilk = false;
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            vFasonIplikMaliyet item = new vFasonIplikMaliyet();
            item.ListIplikler = Iplik.IplikleriGetir();
            item.FaturaTarihi = DateTime.Now;
            liste.Add(item);
            DGridIplikMaliyet.ItemsSource = liste;
        }

        private void SatirSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridIplikMaliyet.SelectedItem != null)
            {
                vFasonIplikMaliyet secilen = DGridIplikMaliyet.SelectedItem as vFasonIplikMaliyet;

                if (secilen == null) return;

                if (_Islem.FasonIplikSil(secilen.Id)) MessageBox.Show("Silinmiştir..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);

                else MessageBox.Show("Silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);

                Refresh();

            }
        }

    }
}

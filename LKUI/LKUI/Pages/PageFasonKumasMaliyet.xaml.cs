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
    /// Interaction logic for PageFasonKumasMaliyet.xaml
    /// </summary>
    /// 




    public partial class PageFasonKumasMaliyet : UserControl
    {

        List<vFasonKumasMaliyet> liste;
        Boyahane _Islem = new Boyahane();
        bool ilk = true;

        public PageFasonKumasMaliyet()
        {
            InitializeComponent();
        }

        private void DataLoad()
        {
            liste = Boyahane.FasonKumasMaliyetleriGetir().OrderByDescending(c => c.Tarih).ToList();
        }

        private void BtnFasonControl_Click(object sender, RoutedEventArgs e)
        {
            if (DPBaslangic.SelectedDate.HasValue == true && DPBitis.SelectedDate.HasValue == true)
            { 
                DGridFasonKumasMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
            }
            else
            {
                MessageBox.Show("Lütfen tarihleri seçiniz! ...");
            }
            
        }

        private void PageLoad(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            DPBaslangic.SelectedDate = date.AddDays(-(DateTime.Now.Day - 1));
            DPBitis.SelectedDate = DateTime.Now;
            DataLoad();
            ilk = false;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (DGridFasonKumasMaliyet.ItemsSource == null) return;
            List<vFasonKumasMaliyet> secilenler = DGridFasonKumasMaliyet.Items.Cast<vFasonKumasMaliyet>().ToList();

            if (secilenler.Count == 0) return;

            if (_Islem.FasonKumasKaydet(secilenler))
            {
                MessageBox.Show("Kayıt edildi...", "Kayıt İşlemi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            DGridFasonKumasMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ilk)
            {
                if (DPBaslangic.SelectedDate.HasValue == true && DPBitis.SelectedDate.HasValue == true)
                {
                    DGridFasonKumasMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
                }
                else
                {
                    MessageBox.Show("Lütfen tarihleri seçiniz! ...");
                }

            }

        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ilk)
            {
                if (DPBaslangic.SelectedDate.HasValue == true && DPBitis.SelectedDate.HasValue == true)
                {
                    DGridFasonKumasMaliyet.ItemsSource = liste.Where(c => c.Tarih >= DPBaslangic.SelectedDate.Value && c.Tarih <= DPBitis.SelectedDate.Value).ToList();
                }
                else
                {
                    MessageBox.Show("Lütfen tarihleri seçiniz! ...");
                }
            }

        }
    }
}

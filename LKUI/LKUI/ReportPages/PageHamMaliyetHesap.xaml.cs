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
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageHamMaliyetHesap.xaml
    /// </summary>
    public partial class PageHamMaliyetHesap : UserControl
    {
        public PageHamMaliyetHesap()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<tblAyarlar> aylar = tblAyarlar.AyarAgaciGetir("Aylar");
            cmbAy.ItemsSource = aylar;
            List<tblAyarlar> yillar = tblAyarlar.AyarAgaciGetir("Yıllar");
            cmbYil.ItemsSource = yillar;

            tblAyarlar currentAy = aylar.Find(c => c.Deger == DateTime.Now.Month.ToString());
            tblAyarlar currentYil = yillar.Find(c => c.Deger == DateTime.Now.Year.ToString());

            cmbAy.SelectedItem = currentAy;
            cmbYil.SelectedItem = currentYil;
        }

        private void Goster()
        {
            tblAyarlar ay = cmbAy.SelectedValue as tblAyarlar;
            tblAyarlar yil = cmbYil.SelectedValue as tblAyarlar;

            if (ay == null || yil == null)
            {
                DGridHamMaliyet.ItemsSource = null;
                return;
            }

            DGridHamMaliyet.ItemsSource = vAnahtar.Getir(ay.Id, yil.Id);
        }

        private void Hesapla()
        {
            tblAyarlar ay = cmbAy.SelectedValue as tblAyarlar;
            tblAyarlar yil = cmbYil.SelectedValue as tblAyarlar;

            if (ay == null || yil == null)
            {
                DGridHamMaliyet.ItemsSource = null;
                return;
            }

            vAnahtar.Hesapla(ay.Id, yil.Id);
            Goster();
        }

        private void BtnGoster_Click(object sender, RoutedEventArgs e)
        {
            Goster();
        }

        private void BtnHesapla_Click(object sender, RoutedEventArgs e)
        {
            Hesapla();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridHamMaliyet.ToExcel<vAnahtar>();
        }
    }
}

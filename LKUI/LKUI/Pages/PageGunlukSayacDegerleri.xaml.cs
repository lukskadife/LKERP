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
    /// Interaction logic for PageGünlük_SayacDegerleri.xaml
    /// </summary>
    public partial class PageGunluk_SayacDegerleri : UserControl
    {
        public PageGunluk_SayacDegerleri()
        {
            InitializeComponent();
        }

        Sayac _Sayac = new Sayac();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewELektrik.ItemsSource = _Sayac.SayacAltBasliklariGetir(1);

            TreeViewSu.ItemsSource = _Sayac.SayacAltBasliklariGetir(2);

            TreeViewDogalGaz.ItemsSource = _Sayac.SayacAltBasliklariGetir(3);
        }

        private void TreeViewELektrik_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TreeViewELektrik.SelectedItem == null) return;
            CmbSayac.ItemsSource = _Sayac.SayacAltBasliklariGetir((TreeViewELektrik.SelectedItem as tblSayaclar).Id);
            DataGridELektrik.ItemsSource = _Sayac.ElkSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewELektrik.SelectedItem as tblSayaclar).Id);
        }

        private void TreeViewELektrik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DpESayacTarih.SelectedDate = DateTime.Now.Date;
            ChildElektrikSayaci.Show();
        }

        private void TreeViewSu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DpSSayacTarih.SelectedDate = DateTime.Now.Date;
            ChildSuSayaci.Show();
        }

        private void TreeViewDogalGaz_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DpDSayacTarih.SelectedDate = DateTime.Now.Date;
            ChildDogalgazSayaci.Show();
        }

        private void BtnEKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtElkKWh.TextGirisiDogruMu == false) return;
            
            if (_Sayac.SayacGirisiKaydet(new tblSayacGirisleri()
            {
                kwh = Convert.ToDouble(TxtElkKWh.Text),
                Tarih = DpESayacTarih.SelectedDate.Value,
                SayacId = (CmbSayac.SelectedItem as tblSayaclar).Id,
                PersonelId = App.PersonelId
            }))
                DataGridELektrik.ItemsSource = _Sayac.ElkSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewELektrik.SelectedItem as tblSayaclar).Id);
            TxtElkKWh.Clear();

            ChildElektrikSayaci.Close();
        }

        private void BtnSKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtSKWh.TextGirisiDogruMu == false) return;
            if (_Sayac.SayacGirisiKaydet(new tblSayacGirisleri()
            {
                ton = Convert.ToDouble(TxtSKWh.Text),
                Tarih = DpSSayacTarih.SelectedDate.Value,
                SayacId = (TreeViewSu.SelectedItem as tblSayaclar).Id,
                PersonelId = App.PersonelId
            }))
                DataGridSu.ItemsSource = _Sayac.SuSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewSu.SelectedItem as tblSayaclar).BaglantiId);
            TxtSKWh.Clear();

            ChildSuSayaci.Close();
        }

        private void BtnDKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (_Sayac.SayacGirisiKaydet(new tblSayacGirisleri()
            {
                SayacId = (TreeViewDogalGaz.SelectedItem as tblSayaclar).Id,
                kwh = Convert.ToDouble(string.IsNullOrEmpty(TxtDKWh.Text) ? "0" : TxtDKWh.Text),
                m3 = Convert.ToDouble(string.IsNullOrEmpty(TxtDM3.Text) ? "0" : TxtDM3.Text),
                sm3 = Convert.ToDouble(string.IsNullOrEmpty(TxtDSM3.Text) ? "0" : TxtDSM3.Text),
                Tarih = DpDSayacTarih.SelectedDate.Value,
                PersonelId = App.PersonelId
            }))
                DataGridDogalgaz.ItemsSource = _Sayac.DgazSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewDogalGaz.SelectedItem as tblSayaclar).BaglantiId);
            TxtDKWh.Clear();
            TxtDM3.Clear();
            TxtDSM3.Clear();

            ChildDogalgazSayaci.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                switch ((TabSayac.SelectedItem as TabItem).Name)
                {
                    case "TIDgaz":
                        DataGridDogalgaz.ItemsSource = _Sayac.DgazSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewDogalGaz.Items[0] as tblSayaclar).BaglantiId);
                        break;
                    case "TISu":
                        DataGridSu.ItemsSource = _Sayac.SuSayacGirisiGetir(DateTime.Now.Date.Year, DateTime.Now.Date.Month, (TreeViewSu.Items[0] as tblSayaclar).BaglantiId);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DataGridELektrik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridELektrik.SelectedItem == null) return;
            vSayacGiris sec = DataGridELektrik.SelectedItem as vSayacGiris;

            DtlGunluk.LblYilAy.Content = sec.Tarih.ToString("yyyy MMMM");
            DtlGunluk.DGridGunluk.ItemsSource = _Sayac.ElkSayacGirisiGetir(sec.Tarih.Year, sec.Tarih.Month, sec.BolumId, true);
            ChildGunluk.Show();
        }

        private void DataGridSu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridSu.SelectedItem == null) return;
            vSayacGiris sec = DataGridSu.SelectedItem as vSayacGiris;

            DtlGunluk.LblYilAy.Content = sec.Tarih.ToString("yyyy MMMM");
            DtlGunluk.DGridGunluk.ItemsSource = _Sayac.SuSayacGirisiGetir(sec.Tarih.Year, sec.Tarih.Month, sec.BolumId, true);
            ChildGunluk.Show();
        }

        private void DataGridDogalgaz_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridDogalgaz.SelectedItem == null) return;
            vSayacGiris sec = DataGridDogalgaz.SelectedItem as vSayacGiris;

            DtlGunluk.LblYilAy.Content = sec.Tarih.ToString("yyyy MMMM");
            DtlGunluk.DGridGunluk.ItemsSource = _Sayac.DgazSayacGirisiGetir(sec.Tarih.Year, sec.Tarih.Month, sec.BolumId, true);
            ChildGunluk.Show();
        }

    }
}

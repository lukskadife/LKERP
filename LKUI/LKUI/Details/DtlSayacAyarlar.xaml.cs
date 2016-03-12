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
using LKUI.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for PageSayacAyarlar.xaml
    /// </summary>
    public partial class DtlSayacAyarlar : UserControl
    {
        public DtlSayacAyarlar()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem secilenItem = (TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem);
            if (secilenItem == null) return;
            TreeViewItem ustItem = secilenItem;
            while ((ustItem.DataContext as tblSayaclar).Derinlik != 1)
            {
                ustItem = ustItem.Parent as TreeViewItem;
            }

            tblSayaclar secilen = (TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar;
            if (secilen.Derinlik >= (ustItem.DataContext as tblSayaclar).MaxAltDal.Value)
            {
                MessageBox.Show("En fazla " + (ustItem.DataContext as tblSayaclar).MaxAltDal.ToString() + " derinliğe inilebilir..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            PopBolumTanımla.IsOpen = true;
        }

        private void BtnBolumEkle_Click(object sender, RoutedEventArgs e)
        {
            TreeViewElektrikBolumTanımla.Items.Add(TxtBolumEkle.Text);
            TxtBolumEkle.Clear();
            PopBolumTanımla.IsOpen = false;
        }

        private void TreeYukle()
        {
            TreeViewElektrikBolumTanımla.Items.Clear();
            List<tblSayaclar> sayacTanimlari = _Sayac.SayacTanimlariGetir();
            foreach (tblSayaclar ayar in sayacTanimlari)
            {

                TreeViewItem item = new TreeViewItem();
                item.DataContext = ayar;
                item.Header = ayar.Adi;
                foreach (tblSayaclar ayarAlt in _Sayac.SayacAltBasliklariGetir(ayar.Id))
                {
                    TreeViewItem itemAlt = new TreeViewItem();
                    itemAlt.DataContext = ayarAlt;
                    itemAlt.Header = ayarAlt.Adi;

                    foreach (tblSayaclar ayarAlt2 in _Sayac.SayacAltBasliklariGetir(ayarAlt.Id))
                    {
                        TreeViewItem itemAlt2 = new TreeViewItem();
                        itemAlt2.DataContext = ayarAlt2;
                        itemAlt2.Header = ayarAlt2.Adi;
                        
                        itemAlt.Items.Add(itemAlt2);
                    }

                    item.Items.Add(itemAlt);
                }

                TreeViewElektrikBolumTanımla.Items.Add(item);
            }
        }

        Sayac _Sayac;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _Sayac = new Sayac();
            TreeYukle();
        }

        bool _IsKaydet = true;
        private void PopBolumTanımla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(TxtBolumEkle.Text) == false)
                {
                    tblSayaclar secilen = (TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar;
                    if (secilen == null) return;

                    tblSayaclar sayac;
                    if (_IsKaydet)
                    {
                        sayac = new tblSayaclar()
                        {
                            BaglantiId = secilen.Id,
                            Adi = TxtBolumEkle.Text,
                            Derinlik = secilen.Derinlik + 1,
                            AktifMi = true                            
                        };
                    }
                    else
                    {
                        sayac = secilen;
                        sayac.Adi = TxtBolumEkle.Text;
                    }

                    if (_Sayac.SayacKaydet(sayac))
                    {
                        TreeYukle();
                        PopBolumTanımla.IsOpen = false;
                        _IsKaydet = true;
                    }
                    else MessageBox.Show("Hata oluştu.\n\nKayıt başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CntxEkle_Opened(object sender, RoutedEventArgs e)
        {
            if (TreeViewElektrikBolumTanımla.SelectedItem == null) return;
            if (((TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar).Derinlik == 1)
            {
                MIDuzelt.IsEnabled = false;
                MISil.IsEnabled = false;
            }
            else
            {
                MIDuzelt.IsEnabled = true;
                MISil.IsEnabled = true;
            }
        }

        private void MIDuzelt_Click(object sender, RoutedEventArgs e)
        {
            TxtBolumEkle.Text = ((TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar).Adi;
            PopBolumTanımla.IsOpen = true;
            _IsKaydet = false;
        }

        private void MISil_Click(object sender, RoutedEventArgs e)
        {
            tblSayaclar secilen = (TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar;
            secilen.AktifMi = false;
            
            if (MessageBox.Show(secilen.Adi + "\n\nSilinsin Mi..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                if (_Sayac.SayacKaydet(secilen)) TreeYukle();
        }

        private void PopBolumTanımla_Closed(object sender, EventArgs e)
        {
            TxtBolumEkle.Text = "";
        }

        private void TreeViewElektrikBolumTanımla_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TreeViewElektrikBolumTanımla.SelectedItem == null) return;
            if (((TreeViewElektrikBolumTanımla.SelectedItem as TreeViewItem).DataContext as tblSayaclar).Derinlik == 1) _IsKaydet = true;
        } 
    
    }
}

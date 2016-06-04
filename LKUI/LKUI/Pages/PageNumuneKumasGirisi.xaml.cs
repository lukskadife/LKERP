using LKLibrary.Classes;
using LKLibrary.DbClasses;
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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageNumuneKumasGirisi.xaml
    /// </summary>
    public partial class PageNumuneKumasGirisi : UserControl
    {
        public PageNumuneKumasGirisi()
        {
            InitializeComponent();
        }


       
        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildNumuneKumasGirisi.DataContext = new vNumuneKumaslar();
            
            CmbTip.ItemsSource = Numune.TipNumaralariGetir().Select(t=> new {
                TipId = t.Id,
                YeniTipNo=t.KumasCinsi == 395 ? t.TipNo + " - " + t.Varyant : t.TipNo
                //TipNo=t.TipNo + " - " + t.Varyant
            });
            CmbBirim.ItemsSource = Numune.NumuneBirimleriGetir();
            CmbBirim.SelectedIndex = 0;
            CmbFuarAdi.ItemsSource = Fuar.FuarlariGetir();

            CmbNumuneTuru.ItemsSource = Numune.NumuneTurunuGetir();
            CmbNumuneTuru.SelectedIndex = 0;
            CmbKoleksiyon.ItemsSource = Numune.KoleksiyonlariGetir();
            ChildNumuneKumasGirisi.Show();
        }
         

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            //numune giriş
            vNumuneKumaslar numune = ChildNumuneKumasGirisi.DataContext as vNumuneKumaslar;
            if (numune == null) return;

            if (CmbTip.GirisYapildiMi == false | CmbBirim.GirisYapildiMi == false | CmbFuarAdi.GirisYapildiMi == false | CmbNumuneTuru.GirisYapildiMi == false)
            {
                MessageBox.Show("Kırmızı Alanlar Zorunludur...!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else
            try
            {
                if (Numune.NumuneGirisKaydet(numune, App.KullaniciId))
                {
                    ChildNumuneKumasGirisi.Close();
                    LoadPage();
                }
                else MessageBox.Show("Hata Oluştu!!\n Kaydedilemedi...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
           // if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;
            DGridNumuneKumaslar.ItemsSource = Numune.NumuneEklenenKumaslariGetir();
        }

        private void ChildNumuneKumasGirisi_Closed(object sender, EventArgs e)
        {

        }

        private void MINumuneTalepEt_Click(object sender, RoutedEventArgs e)
        {
            ChildNumuneSevkTalepleri.DataContext = new vNumuneTalepleri(); 
            //numune giriş
            vNumuneKumaslar secilenTalep = DGridNumuneKumaslar.SelectedItem as vNumuneKumaslar;
            if (secilenTalep == null) return;

                       
            CmbMusteri.ItemsSource = Partileme.MusterileriGetir();
            CmbMusteri.IsEnabled = true;
            TxtChildTipNo.Text = secilenTalep.TipNo;
            TxtChildRenkNo.Text = secilenTalep.RenkNo;
            TxtChildBirim.Text = secilenTalep.BirimAdi;

            //TxtChildYeniMusteri.Text = "";
            TxtChildYeniMusteri.IsEnabled = false;
            ChildNumuneSevkTalepleri.Show();
            

        }

        private void ChBoxYeniMusteri_Checked(object sender, RoutedEventArgs e)
        {
            //CmbMusteri.ItemsSource = null;
            
           CmbMusteri.IsEnabled = false;
           CmbMusteri.SelectedItem = null;
           
            TxtChildYeniMusteri.Text = "";
            TxtChildYeniMusteri.IsEnabled = true;
           
          
           //TxtChildYeniMusteri.TxtTipi = "Gerekli";
        }

        private void ChBoxYeniMusteri_Unchecked(object sender, RoutedEventArgs e)
        {
            CmbMusteri.IsEnabled = true;
            CmbMusteri.ItemsSource = Partileme.MusterileriGetir();
            TxtChildYeniMusteri.IsEnabled = false;
            TxtChildYeniMusteri.Text = "";
        }

        private void BtnTalepKaydet_Click(object sender, RoutedEventArgs e)
        {
            vNumuneKumaslar nTalep = ChildNumuneKumasGirisi.DataContext as vNumuneKumaslar;
            if (nTalep == null) return;

            if (CmbMusteri == null | TxtChildYeniMusteri.Text == null)
            {   
                MessageBox.Show("Kırmızı alanları doldurmanız gerekmektedir...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else
            {
                
            
            }
        }

        private void BtnBarkodluNumuneEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildNumuneBarkodluKumasGirisi.DataContext = new vNumuneKumaslar();
            CmbBarkodluFuarAdi.ItemsSource = Fuar.FuarlariGetir();
            CmbBarkodluKoleksiyon.ItemsSource = Numune.KoleksiyonlariGetir();
            ChildNumuneBarkodluKumasGirisi.Show();
        }


        private void BtnBarkodluKaydet_Click(object sender, RoutedEventArgs e)
        {
            vNumuneKumaslar barkodluNumune = ChildNumuneBarkodluKumasGirisi.DataContext as vNumuneKumaslar;
            if (barkodluNumune == null) return;
            if (TxtBarkodluChildBarkod.Text == null)
            {
                MessageBox.Show("Barkod Alanını Doldurmalısınız !", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    vNumuneKumaslar okutulanKontrol = new DBEvents().GetGeneric<vNumuneKumaslar>(s => s.Barkod == TxtBarkodluChildBarkod.Text).FirstOrDefault();
                    if (okutulanKontrol != null)
                    { 
                        MessageBox.Show("Bu Barkod Zaten Kartela Bölümünde !", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    else if (Numune.BarkodluNumuneKaydet(barkodluNumune, App.KullaniciId, TxtBarkodluChildBarkod.Text))
                    {
                        Numune.MamulBarkoduGuncelle(TxtBarkodluChildBarkod.Text);
                        ChildNumuneBarkodluKumasGirisi.Close();
                        LoadPage();
                    }
                    else MessageBox.Show("Hata Oluştu!!\n Kaydedilemedi...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                
            }
        }

        

        

    }
}

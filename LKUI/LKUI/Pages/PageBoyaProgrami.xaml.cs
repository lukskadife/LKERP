using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;
using System.Collections.Generic;
using System.Windows.Media;
using System;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LKUI.Details;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageBoyaProgrami.xaml
    /// </summary>
    public partial class PageBoyaProgrami : UserControl
    {
        DBEvents db = new DBEvents();
        Partileme _PartiIslem { get; set; }

        public PageBoyaProgrami()
        {
            InitializeComponent();
        }

        //21 mart 2016

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridBoyaProgrami.ToExcel<vBoyaProgrami>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridBoyaProgrami.ItemsSource = Boyahane.BoyaProgramiGetir(true);
            DGridBoyaPrograminaAlinanlar.ItemsSource = Boyahane.BoyaPrograminaAlinanPartileriGetir();
            CmbMakina.ItemsSource = new Makina().MakinalariGetir(2);
        }

        private void LoadProgram()
        {
            DGridBoyaProgrami.ItemsSource = Boyahane.BoyaProgramiGetir();
            DGridBoyaPrograminaAlinanlar.ItemsSource = Boyahane.BoyaPrograminaAlinanPartileriGetir();
            CmbMakina.ItemsSource = new Makina().MakinalariGetir(2);
        }

        private void BtnBoyaProgIptal_Click(object sender, RoutedEventArgs e)
        {
            vBoyaProgrami secilen = (sender as FrameworkElement).DataContext as vBoyaProgrami;
            if (secilen == null) return;

            vPartiler parti = Partileme.PartiGetir(secilen.PartiId);
            parti.BoyaProgIptal = true;
            parti.BoyaProgIptalNedeni = secilen.BoyaProgIptalNedeni;
            if (Partileme.PartiDuzelt(parti) == false)
                MessageBox.Show("Hata oluştu..!\n\nİptal edilemedi.", App.AlertCaption, MessageBoxButton.OK);
            else LoadProgram();
        }

        private void BtnBoyaProgAktif_Click(object sender, RoutedEventArgs e)
        {
            vBoyaProgrami secilen = (sender as FrameworkElement).DataContext as vBoyaProgrami;
            if (secilen == null) return;

            vPartiler parti = Partileme.PartiGetir(secilen.PartiId);
            parti.BoyaProgIptal = false;
            parti.BoyaProgIptalNedeni = "";
            if (Partileme.PartiDuzelt(parti) == false)
                MessageBox.Show("Hata oluştu..!\n\nİptal edilemedi.", App.AlertCaption, MessageBoxButton.OK);
            else LoadProgram();
        }

        private void BtnBoyandi_Click(object sender, RoutedEventArgs e)
        {
            vBoyaProgrami secilen = (sender as FrameworkElement).DataContext as vBoyaProgrami;
            if (secilen == null) return;

            vPartiler parti = Partileme.PartiGetir(secilen.PartiId);
            parti.BoyandiMi = true;
            if (Partileme.PartiDuzelt(parti) == false)
                MessageBox.Show("Hata oluştu..!\n\nİşlem gerçekleştirilemedi.", App.AlertCaption, MessageBoxButton.OK);
            else LoadProgram();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                //if (TabItemPlan.IsSelected) DGridBoyaPlan.ItemsSource = Rapor.BoyaPlaniGetir();
            }
        }

        private void DGridBoyaProgrami_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {            
            if (e.Row.DataContext is vBoyaProgrami)
            {               
                bool boyandi = (e.Row.DataContext as vBoyaProgrami).BoyandiMi;
                bool hamPlanYapidi = (bool)(e.Row.DataContext as vBoyaProgrami).PartilendiMi;     
               // bool hamPlanYapidi = (bool)(e.Row.DataContext as vBoyaProgrami).PartilendiMi;
                bool boyaPrograminaAlindiMi = (bool)(e.Row.DataContext as vBoyaProgrami).BoyaProgaminaAlindiMi;

                //if (boyandi)
                //    e.Row.Background = new SolidColorBrush(Colors.Yellow); 
                //else if (boyandi == false && hamPlanYapidi == true)
                //    e.Row.Background = new SolidColorBrush(Colors.SkyBlue);
                //else if (boyandi == false && hamPlanYapidi == false)
                //    e.Row.Background = new SolidColorBrush(Colors.White);

                 if (boyandi==true)
                     e.Row.Background = new SolidColorBrush(Colors.Yellow);
                 else if (boyandi == false && hamPlanYapidi == true)
                     e.Row.Background = new SolidColorBrush(Colors.SkyBlue);
                 else if (boyandi == false && hamPlanYapidi == false)
                     e.Row.Background = new SolidColorBrush(Colors.White);
               
                 
                 if (boyandi==true && boyaPrograminaAlindiMi==true)
                     e.Row.Background = new SolidColorBrush(Colors.Red); 
                 else if (boyandi==false && boyaPrograminaAlindiMi == true)
                     e.Row.Background = new SolidColorBrush(Colors.Green);

                


            }
        }

        private void BtnBoyaPrograminaEkle_Click(object sender, RoutedEventArgs e)
        {
            vBoyaProgrami secilen = (sender as FrameworkElement).DataContext as vBoyaProgrami;
            if (secilen == null) return;

            if (secilen.DahaOnceBoyaPrograminaAlindiMi())
            {
                MessageBox.Show("Bu Parti Boya Programında Mevcuttur. Tekrar Ekleyemezsiniz", App.AlertCaption, MessageBoxButton.OK);
                //MessageBoxResult rs = MessageBox.Show("Bu Parti Şu anda Boya Programında! Lütfen Kontrol Ediniz.\n Yeniden sevk emri oluşturulsun mu..?\n\nParti No : " + secilen.PartiNo + "\nMüşteri : " + secilen.MusteriAdi
                //, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);

                //if (rs == MessageBoxResult.No)
                //{
                //    return;
                //}
                //else
                //{
                //    tblBoyaProgrami boyaProgrami = new tblBoyaProgrami();
                //    boyaProgrami.PartiId = secilen.PartiId;
                //    boyaProgrami.BoyaPrograminaAlinmaTarihi = DateTime.Now;
                //    boyaProgrami.BoyaProgaminaAlindiMi = true;
                //    boyaProgrami.BoyamaSayisi = boyaProgrami.BoyamaSayisi + 1;

                //    if (db.SaveGeneric<tblBoyaProgrami>(boyaProgrami) == true)
                //    {
                //        MessageBox.Show("Boya Planına Eklendi !", App.AlertCaption, MessageBoxButton.OK);
                //        LoadProgram();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Boya Planına Eklenirken Hata Oluştu !", App.AlertCaption, MessageBoxButton.OK);
                //        LoadProgram();
                //    }

                //}

            }
            else
            {
                tblBoyaProgrami boyaProgrami = new tblBoyaProgrami();
                boyaProgrami.PartiId = secilen.PartiId;
                boyaProgrami.BoyaPrograminaAlinmaTarihi = DateTime.Now;
                boyaProgrami.BoyaProgaminaAlindiMi = true;
                boyaProgrami.Boyandi = false;
                boyaProgrami.BoyamaSayisi = 1;
                //boyaProgrami.BoyanacakHafta = secilen.m
                if (db.SaveGeneric<tblBoyaProgrami>(boyaProgrami) == true)
                {
                    MessageBox.Show("Boya Planına Eklendi !", App.AlertCaption, MessageBoxButton.OK);
                    LoadProgram();
                }
                else
                {
                    MessageBox.Show("Boya Planına Eklenirken Hata Oluştu !", App.AlertCaption, MessageBoxButton.OK);
                    LoadProgram();
                }
            }

        }

        private void DGridBoyaPrograminaAlinanlar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //vBoyaProgramiSukru secilen = (sender as FrameworkElement).DataContext as vBoyaProgramiSukru;
            vBoyaProgramiSukru secilen = DGridBoyaPrograminaAlinanlar.SelectedItem as vBoyaProgramiSukru;
           if (secilen == null) return; 
           // MessageBox.Show(secilen.PartiId.ToString());
           // if (DGridPartileme.SelectedItem == null) return;

            ChildPartilemeEkle.DataContext = _PartiIslem;
            TxtTipChild.Text = secilen.TipNo;
            TxtPartiNoChild.Text = secilen.PartiNo;
            TxtRenkChild.Text = secilen.RenkNo;
            CmbMakina.SelectedValue = secilen.MakinaId;
            CmbHafta.SelectedValue = secilen.BoyanacakHafta;
            
            //if (string.IsNullOrEmpty(_PartiIslem.Parti.BoyaNotu)) BtnInfo.Visibility = System.Windows.Visibility.Hidden;
            //else BtnInfo.Visibility = System.Windows.Visibility.Visible;
            //DGridProsesler.ItemsSource = _PartiIslem.Processler;
            //if (CmbApreKodu.ItemsSource == null) CmbApreKodu.ItemsSource = Partileme.ApreleriGetir();
            ChildPartilemeEkle.Show();


        }

        private void btnBoyaProgramiDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vBoyaProgramiSukru secilen = DGridBoyaPrograminaAlinanlar.SelectedItem as vBoyaProgramiSukru;
            if (secilen == null) return;
            vPartiler parti = Partileme.PartiGetir(Convert.ToInt32(secilen.PartiId)); //PartiGetir(secilen.PartiId); 
            parti.MakinaId =Convert.ToInt32(CmbMakina.SelectedValue);
            if (Partileme.PartiDuzelt(parti) == false)
            {
                MessageBox.Show("Hata Oluştu..!\n\nMakina değiştirilemedi.", App.AlertCaption, MessageBoxButton.OK);
            }
            else
            {
                vBoyaProgramiSukru secilenBoyaProg = Partileme.BoyaProgramiGetir((int)secilen.PartiId);
                secilenBoyaProg.BoyanacakHafta = Convert.ToInt32(CmbHafta.SelectedValue);
                if (db.UpdateGeneric<vBoyaProgramiSukru>(secilenBoyaProg) == true)
                {
                    LoadProgram();
                    ChildPartilemeEkle.Close();
                }
                else
                    MessageBox.Show("Hata Oluştu..!\n\nBoyanacak Hafta Değiştirilemedi", App.AlertCaption, MessageBoxButton.OK);
            }
    
        }


    }
}

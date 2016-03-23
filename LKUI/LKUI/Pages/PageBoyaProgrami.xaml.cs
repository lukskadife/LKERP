using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Classes;
using System.Collections.Generic;
using System.Windows.Media;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageBoyaProgrami.xaml
    /// </summary>
    public partial class PageBoyaProgrami : UserControl
    {
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
        }

        private void LoadProgram()
        {
            DGridBoyaProgrami.ItemsSource = Boyahane.BoyaProgramiGetir();
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

                if (boyandi)
                    e.Row.Background = new SolidColorBrush(Colors.Yellow); 
                else if (boyandi == false && hamPlanYapidi == true)
                    e.Row.Background = new SolidColorBrush(Colors.SkyBlue);
                else if (boyandi == false && hamPlanYapidi == false)
                    e.Row.Background = new SolidColorBrush(Colors.White);

                


            }
        }
    }
}

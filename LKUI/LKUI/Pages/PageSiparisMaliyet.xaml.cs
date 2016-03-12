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
    /// Interaction logic for PageSiparisMaliyet.xaml
    /// </summary>
    public partial class PageSiparisMaliyet : UserControl
    {

        public PageSiparisMaliyet()
        {
            InitializeComponent();
        }

        Siparis _SiparisIslem = new Siparis();

        public void tumSiparislerFormunuAc(int secilenId, string siparisNo, string musteri, string durum)
        {
            Telerik.Windows.Controls.RadWindow wnd = new Telerik.Windows.Controls.RadWindow();
            wnd.Top = 30;
            wnd.Left = 5;
            wnd.Height = this.ActualHeight - 10;
            wnd.Width = this.ActualWidth - 10;
            wnd.Owner = Application.Current.MainWindow;
            wnd.Header = siparisNo + " - Özet Maliyeti";
            wnd.Content = new PageSiparisTumMaliyet(secilenId, siparisNo, musteri, durum);
            wnd.WindowState = WindowState.Normal;
            wnd.Show();
        }

        private void BtnMaliyet_Click(object sender, RoutedEventArgs e)
        {

            Siparis _Siparis = new Siparis();
            vSiparisler secilen = (sender as FrameworkElement).DataContext as vSiparisler;
            tumSiparislerFormunuAc(secilen.Id, secilen.SozlesmeNo, secilen.FirmaAdi, secilen.Durum);

        }

        private void Maliyet_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SiparisMaliyetleri_Loaded(object sender, RoutedEventArgs e)
        {
            PageLoad();
        }

        private void PageLoad()
        {
            DGridSiparisler.ItemsSource = null;
            DGridSiparisler.ItemsSource = _SiparisIslem.AcikTamamlandiSiparisleriGetir().OrderByDescending(c => c.Tarih);
        }

        private void DGridSiparisler_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            string skrZrr;
            if (e.Row.DataContext is vSiparisler)
            {
                //e.Row.Background = (e.Row.DataContext as vSiparisler).KarZarar ? new SolidColorBrush() : new SolidColorBrush(Colors.Gray);

                skrZrr = (e.Row.DataContext as vSiparisler).KarZarar;
                //Color bgcolor = (Color)ColorConverter.ConvertFromString("#f1efe2");//Colors.White
                if (skrZrr == "Zarar") e.Row.Background = new SolidColorBrush(Color.FromRgb(240, 157, 50));

                else if (skrZrr == "Kar") e.Row.Background = new SolidColorBrush(Color.FromRgb(98, 204, 104));

                else if (skrZrr == "Maliyetine") e.Row.Background = new SolidColorBrush(Color.FromRgb(42, 190, 252));

                else e.Row.Background = new SolidColorBrush(Colors.White);

            }
        }

        private void btnYenile_Click(object sender, RoutedEventArgs e)
        {
            PageLoad();
        }

    }
}

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
using Telerik.Windows.Controls;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageMakina_BakımArsiv.xaml
    /// </summary>
    public partial class PageMakinaBakimArsiv : UserControl
    { 
        public PageMakinaBakimArsiv()
        {
            InitializeComponent();
        }

        List<vBakimOnarim> ListBakimlar;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DpTarih.DisplayDate = System.DateTime.Now.Date;
            ListBakimlar = new Makina().BakimOnarimFormlariGetir(DpTarih.SelectedDate.HasValue ? DpTarih.SelectedDate.Value : DateTime.Now.Date, DateTime.Now.Date);
            DtMakinaBakımArsiv.ItemsSource = ListBakimlar;
        }

        private void TxtAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtMakinaBakımArsiv.ItemsSource = ListBakimlar.FindAll(c => c.MakinaAdi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }

        private void DtMakinaBakımArsiv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DtMakinaBakımArsiv.SelectedItem == null) return;

            vBakimOnarim secilen = DtMakinaBakımArsiv.SelectedItem as vBakimOnarim;
            PageBakimOnarim pageBakimOnarim = new PageBakimOnarim();
            pageBakimOnarim.BakimOnarimForm = secilen;


            RadWindow wnd = new RadWindow();
            wnd.Top = 30;
            wnd.Left = 5;
            wnd.Height = this.ActualHeight - 50;
            wnd.Width = this.ActualWidth - 30;
            wnd.Owner = Application.Current.MainWindow;
            wnd.Header = "Makina : " + secilen.MakinaAdi;
            wnd.Content = pageBakimOnarim;
            wnd.WindowState = WindowState.Normal;
            wnd.Show();
        }

        private void DpTarih_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBakimlar = new Makina().BakimOnarimFormlariGetir(DpTarih.SelectedDate.HasValue ? DpTarih.SelectedDate.Value : DateTime.Now.Date, DpTarih.SelectedDate.HasValue ? DpTarih.SelectedDate.Value : DateTime.Now.Date);
            DtMakinaBakımArsiv.ItemsSource = ListBakimlar;
        }

    }
}

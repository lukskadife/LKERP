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
using LKUI.Classes;
using Telerik.Windows.Controls;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageMakinaBakım.xaml
    /// </summary>
    public partial class PageMakinaBakım : UserControl
    {
        public PageMakinaBakım()
        {
            InitializeComponent();
        }

        List<vMakinaBakimTarihleri> ListBakimTarihleri;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.ClickedMenuItemId == 15) ListBakimTarihleri = new Makina().SiradakiBakimlariGetir(true);
            else ListBakimTarihleri = new Makina().SiradakiBakimlariGetir(false);
            DGridSiradakiBakimlar.ItemsSource = ListBakimTarihleri;
        }

        private void DGridSiradakiBakimlar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vMakinaBakimTarihleri secilen = DGridSiradakiBakimlar.SelectedItem as vMakinaBakimTarihleri;
            if (secilen == null) return;
            PageBakimOnarim pageBakimOnarim = new PageBakimOnarim();
            if (secilen.KalanGun <= 0) pageBakimOnarim.RadioButonPeriyodikBakım.IsChecked = true;
            pageBakimOnarim.SeciliMakina = new Makina().MakinaGetir(secilen.Id);

            RadWindow wnd = new RadWindow();
            wnd.Top = 30;
            wnd.Left = 5;
            wnd.Height = ((System.Windows.Window)(App.Current.MainWindow)).ActualHeight - 50;
            wnd.Width = ((System.Windows.Window)(App.Current.MainWindow)).ActualWidth - 30;
            wnd.Owner = Application.Current.MainWindow;
            wnd.Header = "Makina : " + secilen.Adi;
            wnd.Content = pageBakimOnarim;
            wnd.WindowState = WindowState.Normal;
            wnd.Show();
        }

        private void TxtAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridSiradakiBakimlar.ItemsSource = ListBakimTarihleri.FindAll(c => c.Adi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }


    }
}

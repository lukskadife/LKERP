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
    /// Interaction logic for PageBakımPeriyot.xaml
    /// </summary>
    public partial class PageBakımPeriyot : UserControl
    {
        public PageBakımPeriyot()
        {
            InitializeComponent();
        }

        Makina makina = new Makina();
        List<tblMakinalar> listMakinalar;
        private void TxtMakinaAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridBakımPeriyot.ItemsSource = listMakinalar.FindAll(c => c.Adi.ToUpper().Contains(TxtMakinaAdi.Text.ToUpper()));
        }

        List<tblMakinalar> Kategoriler = new List<tblMakinalar>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Kategoriler = Makina.MakinaKategorileriGetir();
            LoadPage();
            
        }

        private void LoadPage()
        {
            listMakinalar = makina.MakinalariGetir();
            listMakinalar.ForEach(c => c.Kategoriler = Kategoriler);
            DGridBakımPeriyot.ItemsSource = listMakinalar;
        }

        private void BtnPeriyotKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (makina.MakinaKaydet(listMakinalar))
            {
                MessageBox.Show("Kaydedildi", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridBakımPeriyot_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            FrameworkElement snd = sender as FrameworkElement;
            object obj = snd.DataContext;

            (DGridBakımPeriyot.ItemsSource as List<tblMakinalar>).FindAll(f => f.Kategoriler == null).ForEach(s => s.Kategoriler = Kategoriler);
        }
    }
}

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
    /// Interaction logic for PageFuarTanimlama.xaml
    /// </summary>
    public partial class PageFuarTanimlama : UserControl
    {
        public PageFuarTanimlama()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DPBaslangic.SelectedDate = DateTime.Today.AddDays(-360);
            //DPBitis.SelectedDate = DateTime.Today;
            LoadPage();
        }

        private void LoadPage()
        {
           // if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;
            DGridFuarlar.ItemsSource = Fuar.FuarlariGetir();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChildFuarTanimlama_Closed(object sender, EventArgs e)
        {

        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildFuarTanimlama.DataContext = new vFuarlar();
            ChildFuarTanimlama.Show();
        }
    }
}

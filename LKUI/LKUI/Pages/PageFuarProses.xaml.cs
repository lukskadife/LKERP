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
using LKLibrary.DbClasses;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageFuarProses.xaml
    /// </summary>
    public partial class PageFuarProses : UserControl
    {
        public PageFuarProses()
        {
            InitializeComponent();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildProses.DataContext = new tblFuarProses();
            ChildProses.Show();
        }

        private void LoadPage()
        {
            DGridProses.ItemsSource = tblFuarProses.ProsesleriGetir();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            tblFuarProses pro = DGridProses.SelectedItem as tblFuarProses;
            if (pro == null) return;

            if (pro.Sil()) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void ChildProses_Closed(object sender, EventArgs e)
        {
            ChildProses.DataContext = null;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            tblFuarProses pro = ChildProses.DataContext as tblFuarProses;
            if (pro == null) return;

            if (TxtFiyati.TextGirisiDogruMu == false) return;

            if (pro.Kaydet())
            {
                LoadPage();
                ChildProses.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!");
        }
    }
}

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
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSayim.xaml
    /// </summary>
    public partial class PageSayim : UserControl
    {
        public PageSayim()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabMamulSayim.IsSelected) DGridMamulSayim.ItemsSource = vSayimMamul.MamulSayimlariGetir();

                if (TabHamSayim.IsSelected) DGridHamSayim.ItemsSource = vSayimHam.HamSayimlariGetir();
            }
        }

        private void MIMamulOkutulmayanlar_Click(object sender, RoutedEventArgs e)
        {
            DGridOkutulmayanlar.ItemsSource = vSayimMamul.OkutulmayanlariGetir();
            ClmRenkNo.IsVisible = true;
            ChildOkutulmayanlar.Show();
        }

        private void MIHamOkutulmayanlar_Click(object sender, RoutedEventArgs e)
        {
            DGridOkutulmayanlar.ItemsSource = vSayimHam.OkutulmayanlariGetir();
            ClmRenkNo.IsVisible = false;
            ChildOkutulmayanlar.Show();
        }

        private void ChildOkutulmayanlar_Closed(object sender, EventArgs e)
        {
            DGridOkutulmayanlar.ItemsSource = null;
        }

        private void MIOkutulmayanlarExcel_Click(object sender, RoutedEventArgs e)
        {
            if (TabMamulSayim.IsSelected) DGridOkutulmayanlar.ToExcel<vSayimMamul>();

            if (TabHamSayim.IsSelected) DGridOkutulmayanlar.ToExcel<vSayimHam>();
        }

        private void MIOkutulanlarExcel_Click(object sender, RoutedEventArgs e)
        {
            if (TabMamulSayim.IsSelected) DGridMamulSayim.ToExcel<vSayimMamul>();

            if (TabHamSayim.IsSelected) DGridHamSayim.ToExcel<vSayimHam>();
        }
    }
}

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
using LKUI.Classes;
using LKLibrary.DbClasses;
using LKLibrary.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageKimyasalSarfiyatTipBazli.xaml
    /// </summary>
    public partial class PageKimyasalSarfiyatTipBazli : UserControl
    {
        public PageKimyasalSarfiyatTipBazli()
        {
            InitializeComponent();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vKimyasalSarfiyatTipBazli>();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (CmbAy.SelectedIndex < 0 || string.IsNullOrEmpty((CmbYil.SelectedValue as ComboBoxItem).Content.ToString()))
                return;
            int ay = (CmbAy.SelectedIndex + 1);
            int yil = Convert.ToInt32((CmbYil.SelectedValue as ComboBoxItem).Content);

            DGridRapor.ItemsSource = Rapor.KimyasalSarfiyatlariTipBazliGetir(yil, ay);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbYil.Text = DateTime.Now.Year.ToString();
            CmbAy.SelectedIndex = DateTime.Now.Month - 1;
        }
    }
}

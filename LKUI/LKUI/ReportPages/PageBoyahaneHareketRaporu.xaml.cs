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
using LKUI.Classes;
using LKLibrary.DbClasses;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageBoyahaneHareketRaporu.xaml
    /// </summary>
    public partial class PageBoyahaneHareketRaporu : UserControl
    {
        public PageBoyahaneHareketRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null) return;
            //DGridRapor.ItemsSource = Boyahane.BoyahaneProcessleriGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
            DGridRapor.ItemsSource = Rapor.BoyahaneHareketRaporuGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vBoyahaneHareketRaporu>();
        }
    }
}

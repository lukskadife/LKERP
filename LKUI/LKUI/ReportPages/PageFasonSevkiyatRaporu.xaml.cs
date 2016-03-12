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

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageFasonSevkiyatRaporu.xaml
    /// </summary>
    public partial class PageFasonSevkiyatRaporu : UserControl
    {
        public PageFasonSevkiyatRaporu()
        {
            InitializeComponent();
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DGridRapor.ItemsSource = Rapor.FasonSevkiyatRaporuGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vMamulSevkiyatRaporu>();
        }
    }
}

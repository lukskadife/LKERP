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
using Telerik.Pivot.Core;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageHataDagilimMamulRaporu.xaml
    /// </summary>
    public partial class PageHataDagilimMamulRaporu : UserControl
    {
        public PageHataDagilimMamulRaporu()
        {
            InitializeComponent();
            this.dataProvider = this.Resources["DataProvider"] as LocalDataSourceProvider;

        }

        private LocalDataSourceProvider dataProvider;

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            this.dataProvider.ItemsSource = Rapor.HataDagilimMamulRaporuGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
            DGridRapor.DataProvider.Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }
    }
}

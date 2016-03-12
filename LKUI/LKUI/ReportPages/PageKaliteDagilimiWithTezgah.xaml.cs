using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKUI.Classes;
using LKLibrary.DbClasses;
using Telerik.Windows.Data;
using System.Linq;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageKaliteDagilimiWithTezgah.xaml
    /// </summary>
    public partial class PageKaliteDagilimiWithTezgah : UserControl
    {
        string _Tur;
        public PageKaliteDagilimiWithTezgah()
        {
            InitializeComponent();
            //_Tur = tur;
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //if (_Tur == "ham")
                DGridRapor.ItemsSource = Rapor.TezgahBazliKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vKaliteDagilimTezgah>();
        }
    }

    public class HataliOranHesabiTezgah : AggregateFunction<vKaliteDagilimTezgah, Double?>
    {
        public HataliOranHesabiTezgah()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.HATALI) / rapor.Sum(p => p.URETIM));
        }
    }

    public class BirKaliteOranHesabiTezgah : AggregateFunction<vKaliteDagilimTezgah, Double?>
    {
        public BirKaliteOranHesabiTezgah()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.BIRKALITE) / rapor.Sum(p => p.URETIM));
        }
    }

    public class IkiKaliteOranHesabiTezgah : AggregateFunction<vKaliteDagilimTezgah, Double?>
    {
        public IkiKaliteOranHesabiTezgah()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.IKIKALITE) / rapor.Sum(p => p.URETIM));
        }
    }
}

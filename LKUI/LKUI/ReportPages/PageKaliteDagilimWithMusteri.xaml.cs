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
    /// Interaction logic for PageKaliteDagilimRaporuWithMusteri.xaml
    /// </summary>
    public partial class PageKaliteDagilimiWithMusteri : UserControl
    {
        string _Tur;
        public PageKaliteDagilimiWithMusteri(string tur)
        {
            InitializeComponent();
            _Tur = tur;
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            if (DateIlkTarih.SelectedDate == null || DateSonTarih.SelectedDate == null)
            {
                MessageBox.Show("Tarih aralığı seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_Tur == "ham")
            {
                DGridRapor.ItemsSource = Rapor.MusteriBazliHamKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
            }
            if (_Tur == "mamul") DGridRapor.ItemsSource = Rapor.MusteriBazliMamulKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vKaliteDagilimMusteri>();
        }
    }

    public class HataliOranHesabiMusteri : AggregateFunction<vKaliteDagilimMusteri, Double?>
    {
        public HataliOranHesabiMusteri()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.HATALI) / rapor.Sum(p => p.URETIM));
        }
    }

    public class BirKaliteOranHesabiMusteri : AggregateFunction<vKaliteDagilimMusteri, Double?>
    {
        public BirKaliteOranHesabiMusteri()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.BIRKALITE) / rapor.Sum(p => p.URETIM));
        }
    }

    public class IkiKaliteOranHesabiMusteri : AggregateFunction<vKaliteDagilimMusteri, Double?>
    {
        public IkiKaliteOranHesabiMusteri()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.IKIKALITE) / rapor.Sum(p => p.URETIM));
        }
    }
}

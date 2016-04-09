using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using Telerik.Windows.Data;
using LKLibrary.DbClasses;
using System.Linq;
using LKUI.Classes;

namespace LKUI.ReportPages
{
    public partial class PageKaliteDagilimiWithTip : UserControl
    {
        string _Tur;
        public PageKaliteDagilimiWithTip(string tur)
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
                DGridRapor.ItemsSource = Rapor.TipBazliHamKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
            }
            if (_Tur == "mamul") DGridRapor.ItemsSource = Rapor.TipBazliMamulKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);
            if (_Tur == "MamulRenksiz") DGridRapor.ItemsSource = Rapor.TipBazliRenksizMamulKaliteDagilimiGetir(DateIlkTarih.SelectedDate.Value, DateSonTarih.SelectedDate.Value);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridRapor.ToExcel<vKaliteDagilimTip>();
        }
    }

    public class HataliOranHesabiTip : AggregateFunction<vKaliteDagilimTip, Double?>
    {
        public HataliOranHesabiTip()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.HATALI) / rapor.Sum(p => p.URETIM));
        }
    }

    public class BirKaliteOranHesabiTip : AggregateFunction<vKaliteDagilimTip, Double?>
    {
        public BirKaliteOranHesabiTip()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.BIRKALITE) / rapor.Sum(p => p.URETIM));
        }
    }

    public class IkiKaliteOranHesabiTip : AggregateFunction<vKaliteDagilimTip, Double?>
    {
        public IkiKaliteOranHesabiTip()
        {
            this.AggregationExpression = rapor => (rapor.Sum(p => p.IKIKALITE) / rapor.Sum(p => p.URETIM));
        }
    }
}

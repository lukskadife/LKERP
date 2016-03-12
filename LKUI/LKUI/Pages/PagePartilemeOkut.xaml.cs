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
using LKUI.Details;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PagePartilemeOkut.xaml
    /// </summary>
    public partial class PagePartilemeOkut : UserControl
    {
        public PagePartilemeOkut()
        {
            InitializeComponent();
        }

        Partileme _PartiIslem { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;           
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridPartileme.ItemsSource = Partileme.PartileriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);            
        }

        private void DGridPartileme_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridPartileme.SelectedItem == null) return;

            _PartiIslem = new Partileme(DGridPartileme.SelectedItem as vPartiler, true);

            btnAyar(_PartiIslem.Parti.BoyandiMi);
            if (_PartiIslem.Parti.RePartiMi == false) DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
            else DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
            DGridBarkodlar.Items.Refresh();
        }

        private void btnAyar(bool boyandiMi)
        {
            if (_PartiIslem.Parti.BoyandiMi == true)
            {
                BtnSil2.IsEnabled = false;
                TxtBarkot.IsEnabled = false;
            }
            else
            {
                BtnSil2.IsEnabled = true;
                TxtBarkot.IsEnabled = true;
            }
        }

        private void TxtBarkot_KeyDown(object sender, KeyEventArgs e)
        {
            if (TxtBarkot.Text.Length != 10) return;

            try
            {
                if (_PartiIslem == null)
                {
                    MessageBox.Show("Parti seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                _PartiIslem.BarkodEkle(TxtBarkot.Text);
                DGridBarkodlar.ItemsSource = null;
                if (_PartiIslem.Parti.RePartiMi == false) DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
                else DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
                TxtBarkot.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnSil2_Click(object sender, RoutedEventArgs e)
        {
            if (DGridBarkodlar.SelectedItem == null) return;

            if (MessageBox.Show("Kayıt silinsin mi ?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            bool snc = false;
            if (_PartiIslem != null)
            {
                if (_PartiIslem.Parti.RePartiMi == false)
                {
                    snc = _PartiIslem.BarkodSil(DGridBarkodlar.SelectedItem as vHamKumaslar);
                    DGridBarkodlar.ItemsSource = null;
                    DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
                }
                else
                {
                    snc = _PartiIslem.BarkodSil(DGridBarkodlar.SelectedItem as vReProcessBarkodlari);
                    DGridBarkodlar.ItemsSource = null;
                    DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
                }
            }
            if (snc == false) MessageBox.Show("Hata oluştu.\n\nBarkod silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MItemRefakatKartiGoster_Click(object sender, RoutedEventArgs e)
        {
            if (DGridPartileme.SelectedItem == null) return;

            if (_PartiIslem.Parti.RePartiMi == false)
            {

                Rapor rapor = new Rapor("RprRefakatKarti");
                DtlRapor dtlRapor = new DtlRapor();
                dtlRapor.Yazdirildi += new RoutedEventHandler(dtlRapor_Yazdirildi);
                ChildRapor.Content = dtlRapor;
                ChildRapor.Show();

                Partileme.RefakatKartRapor refakatKarti = new Partileme.RefakatKartRapor((DGridPartileme.SelectedItem as vPartiler).Id);
                dtlRapor.viewerInstance.LocalReport.DataSources.Clear();

                Microsoft.Reporting.WinForms.ReportDataSource item = new Microsoft.Reporting.WinForms.ReportDataSource();
                item.Name = "DS_Parti";
                item.Value = refakatKarti.Parti;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item);

                Microsoft.Reporting.WinForms.ReportDataSource item1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item1.Name = "DS_Siparis";
                item1.Value = refakatKarti.Siparis;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item1);

                Microsoft.Reporting.WinForms.ReportDataSource item2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item2.Name = "DS_PartiProcesses";
                item2.Value = refakatKarti.PartiProcesses;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item2);

                Microsoft.Reporting.WinForms.ReportDataSource item3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item3.Name = "DS_Apre";
                item3.Value = refakatKarti.ApreKimyasallari;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item3);

                Microsoft.Reporting.WinForms.ReportDataSource item4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item4.Name = "DS_ApreRenk";
                item4.Value = refakatKarti.ApreRenk;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item4);

                Microsoft.Reporting.WinForms.ReportDataSource item5 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item5.Name = "DS_PlanlananKumaslar";
                item5.Value = _PartiIslem.PlanlananBarkodlar;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item5);

                dtlRapor.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                dtlRapor.viewerInstance.RefreshReport();
            }
            else
            {
                Rapor rapor = new Rapor("RprRefakatKartiReParti");
                DtlRapor dtlRapor = new DtlRapor();
                dtlRapor.Yazdirildi += new RoutedEventHandler(dtlRapor_Yazdirildi);
                ChildRapor.Content = dtlRapor;
                ChildRapor.Show();

                Partileme.RefakatKartRapor refakatKarti = new Partileme.RefakatKartRapor((DGridPartileme.SelectedItem as vPartiler).Id);
                dtlRapor.viewerInstance.LocalReport.DataSources.Clear();

                Microsoft.Reporting.WinForms.ReportDataSource item = new Microsoft.Reporting.WinForms.ReportDataSource();
                item.Name = "DS_Parti";
                item.Value = refakatKarti.Parti;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item);

                Microsoft.Reporting.WinForms.ReportDataSource item1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item1.Name = "DS_Siparis";
                item1.Value = refakatKarti.Siparis;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item1);

                Microsoft.Reporting.WinForms.ReportDataSource item2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item2.Name = "DS_PartiProcesses";
                item2.Value = refakatKarti.PartiProcesses;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item2);

                Microsoft.Reporting.WinForms.ReportDataSource item3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item3.Name = "DS_Apre";
                item3.Value = refakatKarti.ApreKimyasallari;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item3);

                Microsoft.Reporting.WinForms.ReportDataSource item4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item4.Name = "DS_ApreRenk";
                item4.Value = refakatKarti.ApreRenk;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item4);

                dtlRapor.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                dtlRapor.viewerInstance.RefreshReport();
            }  

            
        }

        void dtlRapor_Yazdirildi(object sender, RoutedEventArgs e)
        {
            this._PartiIslem.RefakatKartiCikartildi();
        }

        private void ChildRapor_Closed(object sender, EventArgs e)
        {
            ChildRapor.Content = null;
        }


    }
}

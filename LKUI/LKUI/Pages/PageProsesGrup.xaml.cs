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
    /// Interaction logic for PageProsesGrup.xaml
    /// </summary>
    public partial class PageProsesGrup : UserControl
    {
        public PageProsesGrup()
        {
            InitializeComponent();
        }

        List<tblProses> prosesler;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            prosesler = tblProses.ProsesleriGetir(true).OrderBy(o => o.Adi).ToList();
            DGridGrup.ItemsSource = tblProsesGrup.ProcessGruplariGetir();
        }

        public void LoadGrupProcess()
        {
            if (DGridGrup.SelectedItem == null) return;

            List<vGrupProcess> grupProcessleri = vGrupProcess.GrupProcessleriGetir((DGridGrup.SelectedItem as tblProsesGrup).Id);
            grupProcessleri.ForEach(c => c.Processler = prosesler);
            DGridProses.ItemsSource = grupProcessleri;
        }

        private void DGridGrup_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            LoadGrupProcess();
        }

        private void BtnProsesEkle_Click(object sender, RoutedEventArgs e)
        {
            if (DGridGrup.SelectedItem == null) return;
            List<vGrupProcess> temp = (DGridProses.ItemsSource as List<vGrupProcess>);
            temp.Add(new vGrupProcess()
            {
                AktifMi = true,
                GrupAdi = (DGridGrup.SelectedItem as tblProsesGrup).Adi,
                GrupId = (DGridGrup.SelectedItem as tblProsesGrup).Id,
                Processler = prosesler
            });

            DGridProses.ItemsSource = null;
            DGridProses.ItemsSource = temp;
        }

        private void BtnProsesSil_Click(object sender, RoutedEventArgs e)
        {
            vGrupProcess secilen = DGridProses.SelectedItem as vGrupProcess;

            if (secilen == null) return;
            if (MessageBox.Show("Proses gruptan silinecek..?\n\nProses : " + secilen.ProcessAdi + "\nGrup : " + secilen.GrupAdi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (vGrupProcess.GrupProcesSil(secilen)) LoadGrupProcess();
        }

        private void BtnProsesKaydet_Click(object sender, RoutedEventArgs e)
        {
            List<vGrupProcess> list = DGridProses.ItemsSource as List<vGrupProcess>;
            if (list == null) return;

            try
            {
                if (vGrupProcess.GrupProcessleriKaydet(list) == true)
                {
                    LoadGrupProcess();
                    MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Kaydetme işlemi başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //vGrupProcess secilen = (sender as FrameworkElement).DataContext as vGrupProcess;
            //if (secilen != null)
            //{
            //    secilen.FasonMu = ((sender as ComboBox).SelectedItem as tblProses).FasonMu;
            //    DGridProses.Items.Refresh();
            //}
        }

        private void ComboBox_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

        }

        private void GridViewColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void BtnFinishEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildFinish.DataContext = new tblProsesGrup() { Id = 0, Adi = null };
            ChildFinish.Show();
        }

        private void BtnFinishDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (DGridGrup.SelectedItem == null) return;

            ChildFinish.DataContext = DGridGrup.SelectedItem;
            ChildFinish.Show();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtAdi.TextGirisiDogruMu == false) return;

            tblProsesGrup finish = ChildFinish.DataContext as tblProsesGrup;
            bool isInsert = false;
            if (finish.Id == 0) isInsert = true;
            try
            {
                if (tblProsesGrup.FinishKaydet(ref finish) == true)
                {
                    if (isInsert)
                    {
                        (DGridGrup.ItemsSource as List<tblProsesGrup>).Add(finish);                        
                    }
                    else DGridGrup.SelectedItem = finish;
                    ChildFinish.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}

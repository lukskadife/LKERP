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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageTezgahArizalari.xaml
    /// </summary>
    public partial class PageTezgahArizalari : UserControl
    {
        public PageTezgahArizalari()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbTezgah.ItemsSource = new Makina().MakinalariGetir();
            CmbAriza.ItemsSource = Makina.TezgahArizaTanimlariGetir();

            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DPBaslangic.SelectedDate != null && DPBitis.SelectedDate != null) LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DPBaslangic.SelectedDate != null && DPBitis.SelectedDate != null) LoadPage();
        }

        private void LoadPage()
        {
            if (DPBitis.SelectedDate == null || DPBitis.SelectedDate == null) return;
            DGridAriza.ItemsSource = Makina.TezgahArizalariGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildArizaEkle.DataContext = new vTezgahArizalari() { BaslangicTarihi = DateTime.Now, BitisTarihi = DateTime.Now };
            ChildArizaEkle.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vTezgahArizalari secilen = DGridAriza.SelectedItem as vTezgahArizalari;
            if (secilen == null) return;

            if (MessageBox.Show("Silinecek..?\n\nTezgah : " + secilen.TezgahKodu + " - " + secilen.TezgahAdi + "\nArıza : " + secilen.ArizaAdi, App.AlertCaption, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (Makina.TezgahArizaSil(secilen)) LoadPage();
            else MessageBox.Show("Silinemedi..!", App.AlertCaption, MessageBoxButton.OK);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void Kaydet()
        {
            if (ChildArizaEkle.DataContext == null) return;

            if (CmbAriza.SelectedItem == null)
            {
                MessageBox.Show("Arıza seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (CmbTezgah.SelectedItem == null)
            {
                MessageBox.Show("Tezgah seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (CmbPosta.SelectedItem == null)
            {
                MessageBox.Show("Posta seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (TxtSure.TextGirisiDogruMu == false) return;

            try
            {
                if (Makina.TezgahArizaEkle(ChildArizaEkle.DataContext as vTezgahArizalari))
                {
                    LoadPage();
                    ChildArizaEkle.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nArıza eklenemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            Kaydet();
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildArizaEkle.Close();
        }

        private void ChildArizaEkle_Closed(object sender, EventArgs e)
        {
            ChildArizaEkle.DataContext = null;
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridAriza.ToExcel<vTezgahArizalari>();
        }      
    }
}

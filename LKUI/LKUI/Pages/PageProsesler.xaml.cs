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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlProses.xaml
    /// </summary>
    public partial class DtlProses : UserControl
    {
        public DtlProses()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtAdi.TextGirisiDogruMu == false | TxtKodu.TextGirisiDogruMu == false) return;

            tblProses proses = ChildProses.DataContext as tblProses;
            bool isInsert = false;
            if (proses.Id == 0) isInsert = true;
            try
            {
                if (tblProses.ProsesKaydet(ref proses) == true)
                {
                    if (isInsert) (DGridProsesler.ItemsSource as List<tblProses>).Add(proses);
                    else DGridProsesler.SelectedItem = proses;
                    ChildProses.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildProses.DataContext = new tblProses() { Id = 0, AktifMi = true };
            ChildProses.Show();
        }

        private void BtnDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (DGridProsesler.SelectedItem == null) return;

            ChildProses.DataContext = DGridProsesler.SelectedItem;
            ChildProses.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            tblProses proc = DGridProsesler.SelectedItem as tblProses;
            if (MessageBox.Show("Proses silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (tblProses.ProsesSil(proc))
            {
                (DGridProsesler.ItemsSource as List<tblProses>).Remove(proc);
                DGridProsesler.Items.Refresh();
                DGridProsesler.Items.FilterDescriptors.Clear();
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridProsesler.ItemsSource = tblProses.ProsesleriGetir();
        }

        private void DGridProsesler_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for CntAciklama.xaml
    /// </summary>
    public partial class DtlAciklama : UserControl
    {
        public DtlAciklama()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbReceteNo.ItemsSource = KimyasalRecete.ReceteleriGetir(DateTime.Today.AddDays(-10), DateTime.Today);
        }

        private void CmbReceteNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vKimyasalRecete secilen = CmbReceteNo.SelectedItem as vKimyasalRecete;
            TxtAciklama.Text = (secilen == null || secilen.Aciklama == null) ? "" : secilen.Aciklama;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalRecete secilen = CmbReceteNo.SelectedItem as vKimyasalRecete;
            if (secilen == null) {
                MessageBox.Show("Reçete seçilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (string.IsNullOrEmpty(TxtAciklamaYaz.Text)) return;
            secilen.Aciklama = KimyasalRecete.ReceteAciklamaEkle(secilen, TxtAciklamaYaz.Text);
            TxtAciklama.Text = secilen.Aciklama;
            TxtAciklamaYaz.Clear();
        }
    }
}

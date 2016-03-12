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
    /// Interaction logic for PageBoyahaneUrunAgaci.xaml
    /// </summary>
    public partial class PageBoyahaneUrunAgaci : UserControl
    {
        public PageBoyahaneUrunAgaci()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbTip.ItemsSource = vKumas.KumaslariGetir(true);
            CmbKumasTipi.ItemsSource = tblAyarlar.BoyahaneKumasTipleriGetir();
            CmbFinish.ItemsSource = tblProsesGrup.ProcessGruplariGetir();
            CmbProcess.ItemsSource = tblProses.ProsesleriGetir(true);

            DGridUst.ItemsSource = vBoyahaneUrunAgaci.UrunAgaciGetir();
        }

        private void BtnUstEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildUst.DataContext = new vBoyahaneUrunAgaci();
            ChildUst.Show();
        }

        private void BtnUstDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaci secilen = DGridUst.SelectedItem as vBoyahaneUrunAgaci;
            if (secilen == null) return;

            ChildUst.DataContext = secilen;
            ChildUst.Show();
        }

        private void BtnUstSil_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaci secilen = DGridUst.SelectedItem as vBoyahaneUrunAgaci;
            if (secilen == null) return;

            if (MessageBox.Show("Seçilen kayıt silinecek..!\n\nTip No : " + secilen.TipNo + "\nFinish : " + secilen.ProsesGrupAdi
                , App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;

            if (secilen.Sil())
            {
                DGridUst.ItemsSource = vBoyahaneUrunAgaci.UrunAgaciGetir();
                DGridAlt.ItemsSource = null;
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnUstYenile_Click(object sender, RoutedEventArgs e)
        {
            DGridUst.ItemsSource = vBoyahaneUrunAgaci.UrunAgaciGetir();
            DGridAlt.ItemsSource = null;
        }

        private void BtnUstKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (!CmbTip.GirisYapildiMi | !CmbFinish.GirisYapildiMi | !CmbKumasTipi.GirisYapildiMi) return;

            vBoyahaneUrunAgaci urun = ChildUst.DataContext as vBoyahaneUrunAgaci;
            if (urun == null) return;

            if (urun.Kaydet())
            {
                ChildUst.Close();
                DGridUst.ItemsSource = vBoyahaneUrunAgaci.UrunAgaciGetir();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnProcessEkle_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaci urun = DGridUst.SelectedItem as vBoyahaneUrunAgaci;
            if (urun == null)
            {
                MessageBox.Show("Tip, finish seçmelisiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            ChildProses.DataContext = new vBoyahaneUrunAgaciAct() { UrunAgaciId = urun.Id, IslemSayisi = 1 };
            ChildProses.Show();
        }

        private void BtnProcessDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaciAct secilen = DGridAlt.SelectedItem as vBoyahaneUrunAgaciAct;
            if (secilen == null) return;

            ChildProses.DataContext = secilen;
            ChildProses.Show();
        }

        private void BtnProcessSil_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaciAct secilen = DGridAlt.SelectedItem as vBoyahaneUrunAgaciAct;
            if (secilen == null) return;

            if (MessageBox.Show("Seçilen kayıt silinecek..!\n\nProcess : " + secilen.ProsesAdi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;

            if (secilen.Sil()) DGridAlt.ItemsSource = vBoyahaneUrunAgaciAct.UrunAgaciProsesleriGetir(secilen.UrunAgaciId);
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void BtnProcessYenile_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneUrunAgaci urun = DGridUst.SelectedItem as vBoyahaneUrunAgaci;
            if (urun == null)
            {
                DGridAlt.ItemsSource = null;
                return;
            }

            DGridAlt.ItemsSource = vBoyahaneUrunAgaciAct.UrunAgaciProsesleriGetir(urun.Id);
        }

        private void BtnProcessKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProcess.GirisYapildiMi == false) return;
            vBoyahaneUrunAgaciAct secilen = ChildProses.DataContext as vBoyahaneUrunAgaciAct;
            if (secilen == null) return;

            if (secilen.Kaydet())
            {
                DGridAlt.ItemsSource = vBoyahaneUrunAgaciAct.UrunAgaciProsesleriGetir(secilen.UrunAgaciId);
                ChildProses.Close();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridUst_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            vBoyahaneUrunAgaci urun = DGridUst.SelectedItem as vBoyahaneUrunAgaci;
            if (urun == null)
            {
                DGridAlt.ItemsSource = null;
                return;
            }
            DGridAlt.ItemsSource = vBoyahaneUrunAgaciAct.UrunAgaciProsesleriGetir(urun.Id);
        }

    }
}

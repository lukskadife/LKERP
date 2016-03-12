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

namespace LKUI
{
    /// <summary>
    /// Interaction logic for PageLaboratuvarTestleri.xaml
    /// </summary>
    public partial class PageLaboratuvarTestleri : UserControl
    {
        public PageLaboratuvarTestleri()
        {
            InitializeComponent();

            if (App.ClickedMenuItemId == 23) TxtDurum.Text = "Bekleyen";
            else if (App.ClickedMenuItemId == 24) TxtDurum.Text = "Tümü";
        }

        Laboratuvar _Islem = new Laboratuvar();

        private void LoadPage()
        {
            if (_ClickedMenuId == 23) DGPartiTest.ItemsSource = Laboratuvar.BekleyenTestleriGetir();
            else if (_ClickedMenuId == 24) DGPartiTest.ItemsSource = Laboratuvar.TumTestleriGetir();
        }

        int _ClickedMenuId;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ClickedMenuId = App.ClickedMenuItemId;
            LoadPage();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (_Islem.TestKaydet())
            {
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                ChildTest.Close();
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChildTest_Closed(object sender, EventArgs e)
        {
            LBlPartiNo.Content = "";
            LBlTipNo.Content = "";
            LBlRenkNo.Content = "";
        }

        private void DGPartiTest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vLaboratuvarTest secilen = DGPartiTest.SelectedItem as vLaboratuvarTest;
            if (secilen == null) return;
            _Islem.TestId = secilen.TestId;
            ChildTest.DataContext = _Islem.Test;

            LBlTipNo.Content = secilen.TipNo;
            LBlRenkNo.Content = secilen.RenkNo;
            LBlPartiNo.Content = secilen.PartiNo;

            StackBoyutStabilitesiAtki.Height = _Islem.Test.BStabilAtkiKriter == null ? 0 : 25;
            StackBoyutStabilitesiCozgu.Height = _Islem.Test.BStabilCozguKriter == null ? 0 : 25;
            StackBoyutStabilitesiKTAtki.Height = _Islem.Test.KuruTemizAtkiKriter == null ? 0 : 25;
            StackBoyutStabilitesiKTCozgu.Height = _Islem.Test.KuruTemizCozguKriter == null ? 0 : 25;
            StackDikisKaymasiAtki.Height = _Islem.Test.DikisKayAtkiKriter == null ? 0 : 25;
            StackDikisKaymasiCozgu.Height = _Islem.Test.DikisKayCozguKriter == null ? 0 : 25;
            StackGramajm.Height = _Islem.Test.GrMKriter == null ? 0 : 25;
            StackGramajm2.Height = _Islem.Test.GrM2Kriter == null ? 0 : 25;
            StackHavKaybi.Height = _Islem.Test.HavKaybiKriter == null ? 0 : 25;
            StackIsikHasligi.Height = _Islem.Test.IsikHaslikKriter == null ? 0 : 25;
            StackKopmaMukavemetiAtki.Height = _Islem.Test.KopMukAtkiKriter == null ? 0 : 25;
            StackKopmaMukavemetiCozgu.Height = _Islem.Test.KopMukCozguKriter == null ? 0 : 25;
            StackKumasEni.Height = _Islem.Test.EnKriter == null ? 0 : 25;
            StackMartindale.Height = _Islem.Test.MartinDaleKriter == null ? 0 : 25;
            StackPilling.Height = _Islem.Test.PillingKriter == null ? 0 : 25;
            StackSurtmeHaslikKuru.Height = _Islem.Test.SKuruKriter == null ? 0 : 25;
            StackSurtmeHaslikYas.Height = _Islem.Test.SYasKriter == null ? 0 : 25;
            StackTerHasligi.Height = _Islem.Test.TerHaslikKriter == null ? 0 : 25;
            StackYikamaHasligi.Height = _Islem.Test.YikamaHaslikKriter == null ? 0 : 25;
            StackYirtilmaMukavemetiAtki.Height = _Islem.Test.YirtMukAtkiKriter == null ? 0 : 25;
            StackYirtilmaMukavemetiCozgu.Height = _Islem.Test.YirtMukCozguKriter == null ? 0 : 25;
            StackNot.Height = _Islem.Test.KriterAciklama == null ? 0 : 25;

            ChildTest.Show();
        }
    }
}

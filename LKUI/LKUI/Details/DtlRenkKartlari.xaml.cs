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
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for CntRenkKartlari.xaml
    /// </summary>
    public partial class DtlRenkKartlari : UserControl
    {
        public DtlRenkKartlari()
        {
            InitializeComponent();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            _Kart = new Boyahane.RenkKartlari(new tblKumasRenk() { AktifMi = true });
            ChildRenkKartlari.DataContext = _Kart.Renk;
            ChildRenkKartlari.Show();
        }

        public void LoadPage()
        {
            DGridRenkler.ItemsSource = KimyasalRecete.KumasRenkleriGetir();
            DGridKimyasal.ItemsSource = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Telerik.Windows.Controls.GridViewColumn countryColumn = this.DGridRenkler.Columns[0];
            Telerik.Windows.Controls.GridView.IColumnFilterDescriptor countryFilter = countryColumn.ColumnFilterDescriptor;
            countryFilter.SuspendNotifications();
            countryFilter.FieldFilter.Filter1.Operator = Telerik.Windows.Data.FilterOperator.Contains;
            countryFilter.ResumeNotifications();

            CmbKimyasal.ItemsSource = new vMalzemeler().ArananMalzemeleriGetir("", 44, vMalzemeler.AramaKriteri.None).OrderBy(o=>o.Adi).ToList();
            LoadPage();
        }

        private Boyahane.RenkKartlari _Kart;
        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtRenkKodu.TextGirisiDogruMu == false | TxtRenkAdi.TextGirisiDogruMu == false | CmbBoyarMadde.GirisYapildiMi == false | CmbAcikOrtaKoyu.GirisYapildiMi == false )
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_Kart.RenkKaydet())
            {
                _Kart.Logla(_Kart.Renk.Id, App.KullaniciId);
                LoadPage();
                ChildRenkKartlari.Close();
            }
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildRenkKartlari.Close();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            if (DGridRenkler.SelectedItem == null) return;
            ChildRenkKartlari.DataContext = _Kart.Renk;
            ChildRenkKartlari.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridRenkler.SelectedItem == null) return;
            if (MessageBox.Show(_Kart.Renk.Adi + " silinecek ..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            _Kart.RenkKartiSil();
            LoadPage();
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void BtnEkleKimyasal_Click(object sender, RoutedEventArgs e)
        {
            if (DGridRenkler.SelectedItem == null)
            {
                MessageBox.Show("Renk seçili değil.\n\nKimyasal eklenemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            ChildRenkKimyasal.DataContext = new vKumasRenkAct();
            ChildRenkKimyasal.Caption = "Renk :  " + _Kart.Renk.Adi;
            ChildRenkKimyasal.Show();
        }

        private void BtnKaydetKimyasal_Click(object sender, RoutedEventArgs e)
        {
            if (CmbKimyasal.GirisYapildiMi == false | CmbBoyaKimya.GirisYapildiMi == false | CmbGrLtOran.GirisYapildiMi == false | TxtMiktarKimyasal.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_Kart.ReceteItemKaydet(ChildRenkKimyasal.DataContext as vKumasRenkAct, App.KullaniciId))
            {
                DGridKimyasal.ItemsSource = null;
                DGridKimyasal.ItemsSource = _Kart.RenkKimyasallari;
                ChildRenkKimyasal.Close();
            }
            else MessageBox.Show("Kaydetme sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnVazgecKimyasal_Click(object sender, RoutedEventArgs e)
        {
            ChildRenkKimyasal.Close();
        }

        private void BtnSilKimyasal_Click(object sender, RoutedEventArgs e)
        {
            vKumasRenkAct secilen = DGridKimyasal.SelectedItem as vKumasRenkAct;
            if (secilen == null)
            {
                MessageBox.Show("Kimyasal seçmediniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (MessageBox.Show(secilen.KimyasalAdi + " silinsin mi..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            if (_Kart.ReceteItemSil(secilen, App.KullaniciId))
            {
                DGridKimyasal.ItemsSource = null;
                DGridKimyasal.ItemsSource = _Kart.RenkKimyasallari;
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnDüzenleKimyasal_Click(object sender, RoutedEventArgs e)
        {
            vKumasRenkAct secilen = DGridKimyasal.SelectedItem as vKumasRenkAct;
            if (secilen == null)
            {
                MessageBox.Show("Kimyasal seçmediniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            ChildRenkKimyasal.DataContext = DGridKimyasal.SelectedItem as vKumasRenkAct;
            ChildRenkKimyasal.Caption = "Renk :  " + _Kart.Renk.Adi;
            ChildRenkKimyasal.Show();
        }

        private void ChildRenkKartlari_Closed(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void DGridRenkler_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridRenkler.SelectedItem == null) return;

            _Kart = new Boyahane.RenkKartlari(DGridRenkler.SelectedItem as tblKumasRenk);
            DGridKimyasal.ItemsSource = _Kart.RenkKimyasallari;
        }
    }
}

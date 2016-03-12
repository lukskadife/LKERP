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
using LKUI.Details;
using LKLibrary.DbClasses;



namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSiparisler.xaml
    /// </summary>
    public partial class PageSiparisler : UserControl
    {
        public PageSiparisler()
        {
            InitializeComponent();
        }

        Siparis _SiparisIslem = new Siparis();
        string _SeciliDurum = "Bekliyor";

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSiparisTipi.SelectedItem == null || (ComboBoxSiparisTipi.SelectedItem as tblSiparisler).SozlesmeNo == "Tümü")
            {
                MessageBox.Show("Sipariş tipi seçiniz..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            DtlEkleDuzelt.SiparisForm = new vSiparisler()
            {
                BaglantiId = (ComboBoxSiparisTipi.SelectedItem as tblSiparisler).Id,
                Durum = "Bekliyor",
                SipTipi = (ComboBoxSiparisTipi.SelectedItem as tblSiparisler).SozlesmeNo,
                TerminTarihi = null,
                GuncelleyenPersId = App.PersonelId,
                OlusturanPersId = App.PersonelId,
                OlusturanPersAdi = App.PersonelAdi,
                Tarih = DateTime.Now.Date,
                SevkEdilebilirMi = false

            };

            DtlEkleDuzelt.LoadPage();
            ChildEkleDuzelt.Show();

        }

        private void RadioButtonBekliyor_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Bekliyor";
            LoadPage();
        }

        private void RadioButtonAcik_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Açık";
            LoadPage();
        }

        private void RadioButtonTamamlandi_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Tamamlandı";
            LoadPage();
        }

        private void RadioButtonİptal_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "İptal";
            LoadPage();
        }

        private void RadioButtonTerminBekliyor_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Termin Bekliyor";
            LoadPage();
        }

        private void RadioButtonOnayBekliyor_Checked(object sender, RoutedEventArgs e)
        {
            _SeciliDurum = "Onay Bekliyor";
            LoadPage();
        }

        bool IsPageLoaded = false;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxSiparisTipi.ItemsSource = _SiparisIslem.SiparisTipleriGetir();
            ComboBoxSiparisTipi.SelectedIndex = 0;
            RadioButtonBekliyor.IsChecked = true;
            IsPageLoaded = true;
        }

        private void LoadPage()
        {
            DGridSiparisler.ItemsSource = _SiparisIslem.SiparisleriGetir(durum: _SeciliDurum,
            sipTipId: ComboBoxSiparisTipi.SelectedItem == null ? 1 : (ComboBoxSiparisTipi.SelectedItem as tblSiparisler).Id).OrderByDescending(e=>e.Tarih);
        }

        void SiparisDuzelt()
        {
            if (DGridSiparisler.SelectedItem == null) return;

            DtlEkleDuzelt.SiparisForm = DGridSiparisler.SelectedItem as vSiparisler;
            DtlEkleDuzelt.LoadPage();
            ChildEkleDuzelt.Show();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            SiparisDuzelt();
        }

        private void ComboBoxSiparisTipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void Sil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSiparisler.SelectedItem == null) return;

            vSiparisler secilen = DGridSiparisler.SelectedItem as vSiparisler;

            if (MessageBox.Show("Sipariş silinecek..\n\nSözleşme No :  " + secilen.SozlesmeNo + "\nFirma :  " + secilen.FirmaAdi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.No) return;

            if (_SiparisIslem.SiparisSil(secilen))
            {
                MessageBox.Show("Sipariş silindi..\n\nSözleşme No :  " + secilen.SozlesmeNo + "\nFirma :  " + secilen.FirmaAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu..\n\nSipariş silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void DpBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsPageLoaded) LoadPage();
        }

        private void ChildEkleDuzelt_Closed(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void DGridSiparisler_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SiparisDuzelt();
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void DtlEkleDuzelt_Kaydedildi(object sender, RoutedEventArgs e)
        {
            DtlEkleDuzelt.GrdUrun.DataContext = null;
            DtlEkleDuzelt.DataGridSiparisDetay.ItemsSource = null;
            ChildEkleDuzelt.Close();
        }

        private void BtnSevkEmri_Click(object sender, RoutedEventArgs e)
        {
            vSiparisler secilen = (sender as FrameworkElement).DataContext as vSiparisler;
            if (secilen == null) return;

            ChildSevkEmri.Caption = "Sipariş No : " + secilen.SozlesmeNo + "  -  Müşteri : " + secilen.FirmaAdi;
            DGridSevkEmriSiparisSatirlari.ItemsSource = _SiparisIslem.SiparisUrunleriGetir(secilen.Id);
            ChildSevkEmri.DataContext = secilen;
            ChildSevkEmri.Show();
        }

        private void MIIadeSiparisEkle_Click(object sender, RoutedEventArgs e)
        {
            vSiparisler secilen = DGridSiparisler.SelectedItem as vSiparisler;
            if (secilen == null) return;
            tblSiparisler iadeTanim = (ComboBoxSiparisTipi.ItemsSource as List<tblSiparisler>).Find(c => c.BelgeTuru == "IS");
            if (iadeTanim == null) return;

            vSiparisler iadeSiparisi = new vSiparisler()
            {
                BaglantiId = iadeTanim.Id,
                BelgeTuru = iadeTanim.BelgeTuru,
                Durum = "Bekliyor",
                FirmaAdi = secilen.FirmaAdi,
                FirmaId = secilen.FirmaId,
                GuncelleyenPersAdi = App.PersonelAdi,
                GuncelleyenPersId = App.PersonelId,
                KarsiReferansNo = secilen.KarsiReferansNo,
                OdemeSekli = secilen.OdemeSekli,
                OlusturanPersAdi = App.PersonelAdi,
                OlusturanPersId = App.PersonelId,
                OrderNo = secilen.OrderNo,
                SevkEdilebilirMi = false,
                SevkiyatSekli = secilen.SevkiyatSekli,
                SevkYeri = secilen.SevkYeri,
                SipTipi = iadeTanim.SozlesmeNo,
                SipVeren = iadeTanim.SipVeren,
                Tarih = DateTime.Today,
                TopMiktar = secilen.TopMiktar,
                IadeSipNo = secilen.SozlesmeNo
            };

            DtlEkleDuzelt.SiparisForm = iadeSiparisi;
            DtlEkleDuzelt.LoadPage();
            ChildEkleDuzelt.Show();
        }

        private void ChildSevkEmri_Closed(object sender, EventArgs e)
        {
            DGridSevkEmriSiparisSatirlari.ItemsSource = null;
            DGridSevkEmriMamulleri.ItemsSource = null;
            ChildBarkodlar.Close();
        }

        private void BtnSevkBarkodlariSec_Click(object sender, RoutedEventArgs e)
        {
            DGridBarkodlar.ItemsSource = null;
            ChildBarkodlar.DataContext = null;

            vSiparisAct satir = (sender as FrameworkElement).DataContext as vSiparisAct;
            DGridSevkEmriSiparisSatirlari.SelectedItem = satir;

            DGridBarkodlar.ItemsSource = _SiparisIslem.SevkEmriMamulStoklariGetir();

            ClmTipNo.ColumnFilterDescriptor.SuspendNotifications();
            ClmTipNo.ColumnFilterDescriptor.Clear();
            ClmTipNo.ColumnFilterDescriptor.DistinctFilter.AddDistinctValue(satir.TipNo);
            ClmTipNo.ColumnFilterDescriptor.ResumeNotifications();

            ClmRenkNo.ColumnFilterDescriptor.SuspendNotifications();
            ClmRenkNo.ColumnFilterDescriptor.Clear();
            ClmRenkNo.ColumnFilterDescriptor.DistinctFilter.AddDistinctValue(satir.RenkNo);
            ClmRenkNo.ColumnFilterDescriptor.ResumeNotifications();

            ChildBarkodlar.Caption = "Sipariş Tip No : " + satir.TipNo + "  -  Sipariş Renk No : " + satir.RenkNo;
            ChildBarkodlar.DataContext = satir;

            ChildBarkodlar.Show();
        }

        private void BtnSevkEmriOlustur_Click(object sender, RoutedEventArgs e)
        {
            vSiparisler secilen = ChildSevkEmri.DataContext as vSiparisler;
            if (secilen == null) return;

            if (secilen.SevkEmriVarMi())
            {
                MessageBoxResult rs = MessageBox.Show("Daha önce sevk emri oluşturulmuş.\nYeni sevk emri oluşturulsun mu..?\n\nSipariş No : " + secilen.SozlesmeNo + "\nMüşteri : " + secilen.FirmaAdi
                , App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (rs == MessageBoxResult.No)
                {
                    ChildSevkEmri.Close();
                    return;
                }
            }

            else if (MessageBox.Show("Sevkiyat emri oluşturulacak..!\n\nSipariş No : " + secilen.SozlesmeNo + "\nMüşteri : " + secilen.FirmaAdi
                , App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            if (_SiparisIslem.SevkiyatEmriOlustur(secilen, App.PersonelId))
            {
                secilen.SevkEdilebilirMi = true;
                DGridSiparisler.Items.Refresh();
                ChildSevkEmri.Close();
            }
            else MessageBox.Show("Sevk emri verilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridSevkEmriSiparisSatirlari_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            vSiparisAct secilen = DGridSevkEmriSiparisSatirlari.SelectedItem as vSiparisAct;
            if (secilen == null)
            {
                DGridSevkEmriMamulleri.ItemsSource = null;
                return;
            }

            DGridSevkEmriMamulleri.ItemsSource = _SiparisIslem.SevkEmriMamulleriGetir(secilen.Id);
        }

        private void BtnBarkodEkle_Click(object sender, RoutedEventArgs e)
        {
            if (ChildBarkodlar.DataContext == null) return;

            if (DGridBarkodlar.SelectedItems == null || DGridBarkodlar.SelectedItems.Count == 0)
            {
                MessageBox.Show("Barkod seçmediniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            List<vMamulKumaslar> secilenler = DGridBarkodlar.SelectedItems.Cast<vMamulKumaslar>().ToList();
            vSiparisAct satir = ChildBarkodlar.DataContext as vSiparisAct;

            if (secilenler.Exists(c => c.TipNo != satir.TipNo || c.RenkNo != satir.RenkNo))
            {
                MessageBoxResult result = MessageBox.Show("Sipariş satırından farklı tip ve/veya renk no'daki barkodlar seçildi.\n\nEklemeye devam edilsin mi..?\n\nSipariş Tip No  : "
                    + satir.TipNo + "\nSipariş Renk No : " + satir.RenkNo, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No) return;
            }

            try
            {
                if (_SiparisIslem.SiparisSatiriSevkEmriMamulleriSec(secilenler, satir.Id))
                {
                    DGridSevkEmriMamulleri.ItemsSource = _SiparisIslem.SevkEmriMamulleriGetir(satir.Id);
                    ChildBarkodlar.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnSevkEmriMamulSil_Click(object sender, RoutedEventArgs e)
        {
            vMamulKumaslar mamul = (sender as FrameworkElement).DataContext as vMamulKumaslar;
            if (mamul == null) return;

            try
            {
                int satirId = mamul.SevkSiparisActId.Value;

                if (_SiparisIslem.SevkEmriMamuluKaldir(mamul))
                    DGridSevkEmriMamulleri.ItemsSource = _SiparisIslem.SevkEmriMamulleriGetir(satirId);
                else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Yenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private List<tblMalzemeler> _MamulKumaslar;

        //Gökhan 12.05.2014
        private void BtnMaliyet_Click(object sender, RoutedEventArgs e)
        {

        }

       

    }
}

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
using LKLibrary.Classes;
using System.Threading;
using System.Globalization;
using LKUI.Controls;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlSiparisEkleDüzenle.xaml
    /// </summary>
    public partial class DtlSiparisEkleDuzenle : UserControl
    {
        public DtlSiparisEkleDuzenle()
        {
            InitializeComponent();
        }

        

        #region RoutedEvents

        public static readonly RoutedEvent KaydedildiEvent = EventManager.RegisterRoutedEvent(
          "Kaydedildi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlSiparisEkleDuzenle));

        public event RoutedEventHandler Kaydedildi
        {
            add { AddHandler(KaydedildiEvent, value); }
            remove { RemoveHandler(KaydedildiEvent, value); }
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Urunler == null || _Urunler.Count == 0)
            {
                _Personeller = Siparis.PersonelleriGetir();
                _Urunler = vKumas.KumaslariGetir();
                _MamulKumaslar = Siparis.SiparisMamulleriGetir();
                DGridUrunSec.ItemsSource = _Urunler;
                //ComboBoxSonGüncelleyen.ItemsSource = _Personeller;
                ComboBoxHazirlayan.ItemsSource = _Personeller;
                _Dovizler = new vAyarlar().DovizleriGetir();                
            }
        }

        public vSiparisler SiparisForm;
        private List<tblPersoneller> _Personeller;
        private List<tblFirmalar> _Firmalar;
        private List<vKumas> _Urunler;
        private List<vAyarlar> _Dovizler;
        private List<tblMalzemeler> _MamulKumaslar;
        vTipBazliFinishler SecilenFinish = new vTipBazliFinishler();
        bool _LoadedPage;

        public void LoadPage()
        {
            _LoadedPage = false;
            _Firmalar = tblFirmalar.MusterileriGetir();
            if (this.SiparisForm != null && this.SiparisForm.SipTipi == "Fason Gönderim Siparişi")
            {
                List<tblFirmalar> tedarikciler = tblFirmalar.TedarikcileriGetir();
                _Firmalar.AddRange(tedarikciler);
                _Firmalar = _Firmalar.OrderBy(o => o.Adi).ToList();
            }
            ComboBoxFirma.ItemsSource = _Firmalar;

            GrdForm.DataContext = null;
            GrdUrun.DataContext = new vSiparisAct();
            GrdForm.DataContext = SiparisForm;
            ListSiparisUrunler = _Siparis.SiparisUrunleriGetir(SiparisForm == null ? -1 : SiparisForm.Id);

            ListSiparisUrunler.ForEach(c => c.ListDoviz = _Dovizler);
            ListSiparisUrunler.ForEach(c => c.ListMamuller = _MamulKumaslar);
            DataGridSiparisDetay.ItemsSource = ListSiparisUrunler;
            _LoadedPage = true;

            DataGridSiparisDetay.SelectedIndex = 0;
        }

        Siparis _Siparis = new Siparis();
        List<vSiparisAct> ListSiparisUrunler;

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxFirma.GirisYapildiMi == false | ComboBoxHazirlayan.GirisYapildiMi == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (ListSiparisUrunler.Find(c => c.TipId == 0) != null)
            {
                MessageBox.Show("Satırlar arasında seçili olmayan tipler var.\n\nKaydedilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            //Şimdilik zorunlu değil.
            //if (ListSiparisUrunler.Find(c => c.FinishId == 0) != null)
            //{
            //    MessageBox.Show("Finish seçilmemiş kayıtlar var.\n\nKaydedilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            //    return;
            //}

            vSiparisler siparis = GrdForm.DataContext as vSiparisler;

            if (siparis.Durum == "Tamamlandı" && ListSiparisUrunler.Find(c => c.Durum == "Açık") != null)
            {
                MessageBox.Show("Açık olan satırlar var.\n\nSipariş tamamlandı olarak kaydedilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            
            vSiparisAct termin = ListSiparisUrunler.OrderByDescending(c=>c.TerminTarihi).FirstOrDefault();
            if (termin == null || termin.TerminTarihi == null) siparis.TerminTarihi = null;
            else siparis.TerminTarihi = termin.TerminTarihi.Value;
                //siparis.TerminTarihi = (termin == null || termin.TerminTarihi == null) ? null : termin.TerminTarihi.Value;            

            if (_Siparis.SiparisKaydet(ref siparis, App.KullaniciId) && _Siparis.SiparisUrunKaydet((DataGridSiparisDetay.ItemsSource as List<vSiparisAct>).FindAll(c=>c.TipId > 0), siparis.Id))
            {
                LoadPage();
                TxtSözlesmeNo.Text = siparis.SozlesmeNo;
                MessageBox.Show("Sipariş kaydedildi..\n\nBelgeNo :  " + siparis.SozlesmeNo, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                RaiseEvent(new RoutedEventArgs(KaydedildiEvent));
            }
            else MessageBox.Show("Hata oluştu..\n\nSipariş kaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnUrunEkle_Click(object sender, RoutedEventArgs e)
        {
            int sira = 1;
            if (ListSiparisUrunler != null && ListSiparisUrunler.Count > 0) sira = ListSiparisUrunler.LastOrDefault().Sira + 1;

            ListSiparisUrunler.Add(new vSiparisAct() { ListDoviz = _Dovizler, Sira = sira, SiparisId = SiparisForm.Id, TerminTarihi = null, ListMamuller = _MamulKumaslar, Durum = "Kapalı" });

            DataGridSiparisDetay.Items.Refresh();
        }

        private void DGridUrunSec_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridUrunSec.SelectedItem == null) return;
            vKumas eklenecekUrun = DGridUrunSec.SelectedItem as vKumas;

            (DataGridSiparisDetay.SelectedItem as vSiparisAct).TipId = eklenecekUrun.Id;
            (DataGridSiparisDetay.SelectedItem as vSiparisAct).TipNo = eklenecekUrun.TipNo;
            (DataGridSiparisDetay.SelectedItem as vSiparisAct).TipAdi = eklenecekUrun.TipAdi;

            DataGridSiparisDetay.Items.Refresh();

            ChildUrunSec.Close();
        }

        private void BtnUrunSec_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSiparisDetay.SelectedItem == null) return;

            ChildUrunSec.Show();
        }

        private void TxtKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridUrunSec.ItemsSource = _Urunler.FindAll(c => c.TipNo.ToUpper().Contains(TxtKodu.Text.ToUpper()));
        }

        private void TextBoxAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridUrunSec.ItemsSource = _Urunler.FindAll(c => c.TipAdi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }

        private void DataGridSiparisDetay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GrdUrun.DataContext = DataGridSiparisDetay.SelectedItem as vSiparisAct;
        }

        public void LoadRezerveChild()
        {
            vSiparisAct secilen = DataGridSiparisDetay.SelectedItem as vSiparisAct;
            if (secilen == null) return;

            DGridStokDetay.ItemsSource = Siparis.RezerveyeUygunMamulGetir(secilen.TipId);
            (DGridStokDetay.ItemsSource as List<vMamulKumaslar>).FindAll(f => f.Aciklama != null).ForEach(c => c.Aciklama = c.Aciklama.Replace("\n", ""));

            if (secilen.Id != 0)
            {
                DGridRezerveler.ItemsSource = Siparis.RezerveleriGetir();
                (DGridRezerveler.ItemsSource as List<vMamulKumaslar>).FindAll(f => f.Aciklama != null).ForEach(c => c.Aciklama = c.Aciklama.Replace("\n", ""));
            }
        }

        private void BtnStokDetay_Click(object sender, RoutedEventArgs e)
        {
            LoadRezerveChild();
            ChildStokDetay.Show();
        }

        private void BtnUrunSil_Click(object sender, RoutedEventArgs e)
        {
            vSiparisAct secilen = (sender as FrameworkElement).DataContext as vSiparisAct;
            if (secilen == null) return;

            if (MessageBox.Show(secilen.TipNo + "\n\nSilinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (secilen.Id == 0)
            {
                (DataGridSiparisDetay.ItemsSource as List<vSiparisAct>).Remove(secilen);
                DataGridSiparisDetay.Items.Refresh();
            }
            else
            {
                if (_Siparis.SiparisUrunSil(secilen))
                {
                    if (SiparisForm.TerminTarihi == secilen.TerminTarihi)
                    {
                        vSiparisAct termin = ListSiparisUrunler.FindAll(c => c.Id != secilen.Id).OrderByDescending(c => c.TerminTarihi).FirstOrDefault();
                        SiparisForm.TerminTarihi = (termin == null || termin.TerminTarihi == null) ? DateTime.Now.Date : termin.TerminTarihi.Value;
                        _Siparis.SiparisKaydet(ref SiparisForm, App.KullaniciId);
                    }

                    LoadPage();
                    MessageBox.Show(secilen.TipNo + "\n\nSilindi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Hata oluştu..\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            vSiparisAct secilen = (sender as FrameworkElement).DataContext as vSiparisAct;
            if (secilen == null) return;
            if (secilen.Testler == null) secilen.Testler = new tblSiparisTestleri();
            ChildTestler.DataContext = secilen.Testler;
            ChildTestler.Show();
        }

        private void CntSelectBox_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            LKUI.Controls.CntSelectBox cnt = (sender as LKUI.Controls.CntSelectBox);
            tblMalzemeler secilen = cnt.SelectedItem as tblMalzemeler;
            vSiparisAct siparis = cnt.DataContext as vSiparisAct;

            siparis.TipId = 0;
            siparis.TipNo = null;
            siparis.TipAdi = null;
            siparis.RenkId = 0;
            siparis.FinishGrupId = 0;

            if (secilen != null && string.IsNullOrEmpty(secilen.Kodu) == false)
            {
                tblKumas kumas = Siparis.KumasTipGetir(secilen.Kodu);
                if (kumas != null)
                {
                    siparis.TipId = kumas.Id;
                    siparis.TipNo = kumas.TipNo;
                    siparis.TipAdi = kumas.TipAdi;
                    siparis.TipMalzemeId = secilen.Id;
                }
                else
                {
                    MessageBox.Show("Seçilen tip dokuma ürün ağacında bulunamadı.\n\nEklenemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    cnt.SelectedItem = null;
                    cnt.SelectedValue = null;
                    return;
                }

                if (kumas.MusteriSatisHakkiVarMi(SiparisForm.FirmaId) == false)
                {
                    MessageBox.Show("Satış hakkı yoktur..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    cnt.SelectedItem = null;
                    cnt.SelectedValue = null;
                    return;
                }

                tblKumasRenk renk = Siparis.KumasRenkGetir(secilen.Kodu);
                tblProsesGrup finish = Siparis.KumasFinishGetir(secilen.Kodu);

                if (renk != null) siparis.RenkId = renk.Id;
                if (finish != null) siparis.FinishGrupId = finish.Id;

                tblFiyatListeleri fiyat = _Siparis.TipFiyatiGetir(SiparisForm.FirmaId, kumas.TipNo);
                if (fiyat != null)
                {
                    siparis.BirimFiyat = fiyat.Fiyat;
                    siparis.DovizId = fiyat.DovizId;
                    DataGridSiparisDetay.Items.Refresh();
                }
            }
        }

        private void BtnRezerve_Click(object sender, RoutedEventArgs e)
        {
            vSiparisAct secilen = DataGridSiparisDetay.SelectedItem as vSiparisAct;
            if (secilen == null) return;

            if (secilen.Id == 0)
            {
                MessageBox.Show("Sipariş kaydedilmeden rezerve yapılamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            vMamulKumaslar mamul = DGridStokDetay.SelectedItem as vMamulKumaslar;
            if (mamul == null) return;

            if (Siparis.RezerveEt(mamul, secilen.Id)) LoadRezerveChild();
            else MessageBox.Show("Hata oluştu.\n\nRezerve edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnRezerveIptal_Click(object sender, RoutedEventArgs e)
        {
            vSiparisAct secilen = DataGridSiparisDetay.SelectedItem as vSiparisAct;
            if (secilen == null) return;

            vMamulKumaslar mamul = DGridRezerveler.SelectedItem as vMamulKumaslar;
            if (mamul == null) return;

            if (Siparis.RezerveIptalEt(mamul)) LoadRezerveChild();
            else MessageBox.Show("Hata oluştu.\n\nİptal edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ComboBoxFirma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_LoadedPage)
            {
                tblFirmalar secilen = ComboBoxFirma.SelectedItem as tblFirmalar;
                if (secilen == null) return;
                TxtOrderNo.Text = _Siparis.OrderNoGetir(secilen, SiparisForm.Id);
                SiparisForm.OrderNo = TxtOrderNo.Text;
                TxtSevkYeri.Text = _Siparis.SevkAdresiGetir(secilen);
                SiparisForm.SevkYeri = TxtSevkYeri.Text;
                TxtFaturaAdresi.Text = secilen.Adres;
                SiparisForm.FaturaAdresi = secilen.Adres;
            }
        }

        private void SatirEkle()
        {
            int sira = 1;
            if (ListSiparisUrunler != null && ListSiparisUrunler.Count > 0) sira = ListSiparisUrunler.LastOrDefault().Sira + 1;

            vSiparisAct yeniSiparis = new vSiparisAct() { ListDoviz = _Dovizler, Sira = sira, SiparisId = SiparisForm.Id, TerminTarihi = null, ListMamuller = _MamulKumaslar, Durum = "Kapalı",
             AntiStatik = false, Apresiz = false, NorApre = false, NorYanmazApre = false, SirtApre = false, YumApre = false, SuYagApre = false};
            ListSiparisUrunler.Add(yeniSiparis);
            DataGridSiparisDetay.Items.Refresh();
            DataGridSiparisDetay.SelectedItem = yeniSiparis;
        }

        private void _DtlSiparisEkleDuzelt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3) SatirEkle();
        }

        private void BtnYazdir_Click(object sender, RoutedEventArgs e)
        {
            DtlRapor raporlama = new DtlRapor();

            if (SiparisForm.BelgeTuru == "Proforma")
            {
                SiparisForm.ProformaNumarasiAl();
                List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSSiparis", new List<vSiparisler>(){SiparisForm}),
                    new DtlRapor.RaporItem("DSSiparisAct", DataGridSiparisDetay.ItemsSource )
                };
                raporlama.RaporGoster("RprProforma", list);
            }

            else if (SiparisForm.BelgeTuru == "Invoice")
            {
                List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSSiparis", new List<vSiparisler>(){SiparisForm}),
                    new DtlRapor.RaporItem("DSSiparisMamulleri", _Siparis.SiparisMamulleriGetir(SiparisForm.Id))
                };
                raporlama.RaporGoster("RprInvoice", list);
            }

            else if (SiparisForm.BelgeTuru == "Sözleşme")
            {
                List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSSiparis", new List<vSiparisler>(){SiparisForm}),
                    new DtlRapor.RaporItem("DSSiparisAct", DataGridSiparisDetay.ItemsSource )
                };
                raporlama.RaporGoster("RprSozlesme", list);
            }

            //23.03.2014 sukru
            else if (SiparisForm.BelgeTuru == "Fason Sözleşme")
            {
                List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                {
                    new DtlRapor.RaporItem("DSSiparis", new List<vSiparisler>(){SiparisForm}),
                    new DtlRapor.RaporItem("DSSiparisAct", DataGridSiparisDetay.ItemsSource )
                };
                raporlama.RaporGoster("RprFasonSozlesme", list);
            }


            ChildGenel.Content = raporlama;
            ChildGenel.Show();
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
            ChildGenel.Height = Double.NaN;
            ChildGenel.Width = Double.NaN;
            ChildGenel.Top = 0;
            ChildGenel.Left = 0;
        }

        private void BtnSatirEkle_Click(object sender, RoutedEventArgs e)
        {
            SatirEkle();
        }

        private void CntSelectBox_ItemNotSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tip seçilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTipResimGoster_Click(object sender, RoutedEventArgs e)
        {
            vSiparisAct secilen = (sender as FrameworkElement).DataContext as vSiparisAct;
            if (secilen == null) return;
            List<tblKumasResimleri> resimler = tblKumasResimleri.TipResimleriGetir(secilen.TipId);

            CntImageViewer _Viewer = new CntImageViewer();
            foreach (var item in resimler)
            {
                _Viewer.AddImage(new CntImageViewer.ImageSrc() { Id = item.Id, Image = LKUI.Classes.Operations.StringToImage(item.Resim) });
            }

            ChildGenel.Width = 700;
            ChildGenel.Height = 500;
            ChildGenel.Content = _Viewer;
            ChildGenel.Show();
        }

        private void BtnFinishSec_Click(object sender, RoutedEventArgs e)
        {
            DGridFinishSec.ItemsSource = tblFinish.FinishKartlariGetir((DataGridSiparisDetay.SelectedItem as vSiparisAct).TipNo);
            ChildFinishSec.Show();
        }
        
        private void DGridFinishSec_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridFinishSec.SelectedItem == null) return;
            SecilenFinish = DGridFinishSec.SelectedItem as vTipBazliFinishler;

            (DataGridSiparisDetay.SelectedItem as vSiparisAct).FinishId = SecilenFinish.FinishId;
            (DataGridSiparisDetay.SelectedItem as vSiparisAct).SecilenFinishAdi = SecilenFinish.FinishAdi;            

            DataGridSiparisDetay.Items.Refresh();
            ChildFinishSec.Close();
             
        }
    }
}
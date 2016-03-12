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
    /// Interaction logic for PageKimyasalRecete.xaml
    /// </summary>
    public partial class PageKimyasalRecete : UserControl
    {
        public PageKimyasalRecete()
        {
            InitializeComponent();
        }

        private void BtnReceteEkle_Click(object sender, RoutedEventArgs e)
        {
            _Islem = new KimyasalRecete() { };
            Refresh();
            ChildReceteEkle.Show();
        }

        public void PartiEkle(vPartiler secilen)
        {
            try
            {
                bool snc =_Islem.PartiEkle(secilen);
                if (!snc)
                    MessageBox.Show("Aynı parti ikinci kez eklenemez!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                DGridRecetePartileri.Items.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void menuItemEklePartiDetay_Click(object sender, RoutedEventArgs e)
        {
            if (DGridPartiDetaylari.SelectedItem == null) return;

            vPartiler secilen = DGridPartiDetaylari.SelectedItem as vPartiler;
            PartiEkle(secilen);
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        public void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridReceteDetaylari.ItemsSource = KimyasalRecete.ReceteleriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
            _Islem = new KimyasalRecete();
            Refresh();
        }

        //List<vPartiler> TumPartilerList;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbPersonel.ItemsSource = KimyasalRecete.PersonelleriGetir();
            CmbRenkNo.ItemsSource = KimyasalRecete.KumasRenkleriGetir(sadeceAktifler: true);
            CmbMakinaNo.ItemsSource = KimyasalRecete.MakinalariGetir();
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;

            //TumPartilerList = KimyasalRecete.TumPartileriGetir().OrderByDescending(c => c.Tarih).ToList();
            //DGridBütünPartiler.ItemsSource = TumPartilerList;
        }

        KimyasalRecete _Islem;
        public void Refresh()
        {
            this.DataContext = null;
            this.DataContext = _Islem;
            DGridPartiDetaylari.ItemsSource = _Islem.UygunPartiler;
        }

        private void DGridReceteDetaylari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGridReceteDetaylari.SelectedItem == null) return;

            vKimyasalRecete secilen = DGridReceteDetaylari.SelectedItem as vKimyasalRecete;
            _Islem = new KimyasalRecete(secilen);
            Refresh();
        }

        private void BtnReceteDuzenle_Click(object sender, RoutedEventArgs e)
        {
            CmbMakinaNo.SelectedValue = _Islem.Recete.MakinaId;
            CmbMakinaNo.Text = _Islem.Recete.Makina;
            ChildReceteEkle.Show();
        }

        private void BtnReceteSil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Reçete silinsin mi ?\n\nReçete No : " + _Islem.Recete.ReceteNo, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
                if (!_Islem.ReceteOnayaDusmusMu(_Islem.Recete.Id))
                {
                    _Islem.ReceteSil(App.PersonelId);
                    LoadPage();
                }
                else
                {
                    MessageBox.Show("Reçete onayda beklemektedir. Silinemez!...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (CmbMakinaNo.SelectedValue != null)
            {
                if (_Islem.ReceteKaydet(Convert.ToInt32(CmbMakinaNo.SelectedValue)))
                {
                    LoadPage();
                    ChildReceteEkle.Close();
                }
            }
            else
            {
                MessageBox.Show("Makinayı seçiniz!...");
            }
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildReceteEkle.Close();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnPartiKaydet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Islem.RecetePartileriKaydet();
                MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnPartiSil_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalRecetePartileri secilen = DGridRecetePartileri.SelectedItem as vKimyasalRecetePartileri;

            if (secilen == null) return;

            if (MessageBox.Show("Silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            _Islem.RecetePartisiSil(secilen);
            DGridRecetePartileri.Items.Refresh();
        }

        private void RefreshKimyasallarChild()
        {
            GrdKimyasallar.DataContext = null;
            GrdKimyasallar.DataContext = _Islem;
            TxtTartilanKg.Text = Math.Round(_Islem.ReceteninPartileri.Sum(c => c.TartilanKg), 2).ToString();           
            DGridApre.ItemsSource = _Islem.Apreler;
            DGridApre.Items.Refresh();
            DGridBoya.ItemsSource = _Islem.Boyalar;
            DGridBoya.Items.Refresh();
            DGridBoyaKasari.ItemsSource = _Islem.Kasarlar;
            DGridBoyaKasari.Items.Refresh();
            DGridOnIslem.ItemsSource = _Islem.OnIslemler;
            DGridOnIslem.Items.Refresh();
            DGridYikama.ItemsSource = _Islem.Yikamalar;
            DGridYikama.Items.Refresh();
            DGridKimyasal.ItemsSource = _Islem.Kimyasallar;
            DGridKimyasal.Items.Refresh();
        }

        private void BtnKimyasalEkle_Click(object sender, RoutedEventArgs e)
        {
            TxtTartilanKg.Text = "";
            TxtFloteApre.Text = "0";
            TxtFloteBoya.Text = "0";
            TxtFloteKasar.Text = "0";
            TxtFloteYikama.Text = "0";

            vKimyasalRecete secilen = DGridReceteDetaylari.SelectedItem as vKimyasalRecete;
            if (secilen == null) return;

            CmbApreNo.ItemsSource = KimyasalRecete.ApreRenkleriGetir();
            //if (MessageBox.Show(secilen.RenkKodu + " için aktif reçete yüklensin mi ?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //    _Islem.ReceteleriYukle(true);
            //else 
            _Islem.ReceteleriYukle(true);
            RefreshKimyasallarChild();
            TxtAciklama.Text = secilen.Aciklama;
            ChildKimyasallar.Caption = " Renk No  :  " + secilen.RenkKodu.ToString();
            ChildKimyasallar.Show();
        }

        private void BtnEkleKasar_Click(object sender, RoutedEventArgs e)
        {
            _Islem.KasarEkle();
            DGridBoyaKasari.Items.Refresh();
        }

        private void BtnEkleOnİslem_Click(object sender, RoutedEventArgs e)
        {
            _Islem.OnIslemEkle();
            DGridOnIslem.Items.Refresh();
        }

        private void BtnReceteYukle_Click(object sender, RoutedEventArgs e)
        {           
            if (!_Islem.OnaysizKimyasalVarMiRenkKartlari(_Islem.Recete.RenkId))
            {
                MessageBox.Show("Lab Renk Kartı Yüklendi.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                
                    ReceteyiTemizle();
                    _Islem.BoyaKimyasalYukle();
                    DGridBoya.Items.Refresh();
                    DGridKimyasal.Items.Refresh();
                
            }
            else
            {
                MessageBox.Show("Renk kartı onayda silinemez!...", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEkleKimyasal_Click(object sender, RoutedEventArgs e)
        {
            _Islem.KimyasalEkle();
            DGridKimyasal.Items.Refresh();
        }

        private void BtnEkleApre_Click(object sender, RoutedEventArgs e)
        {
            _Islem.ApreEkle();
            DGridApre.Items.Refresh();
        }

        private void BtnEkleYikama_Click(object sender, RoutedEventArgs e)
        {
            _Islem.YikamaEkle();
            DGridYikama.Items.Refresh();
        }

        private void BtnKaydetGenel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Islem.Recete.Aciklama = TxtAciklama.Text;
                //_Islem.Kaydet();
                //ben ekledim
                _Islem.KaydetNew(App.KullaniciId);
                //MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);                
                ChildKimyasallar.Close();
            }
            catch (Exception exc)
            {
                if (exc.Message == "bos")
                {
                    MessageBox.Show("Onay bekleyen işlemleriniz varsa kayıt edilmemiştir!", "Hatırlatma", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ChildKimyasallar.Close();
                }
                else
                {
                    MessageBox.Show("İşlem sırasında hata/hatalar oluştu.\n\n" + exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnVazgecGenel_Click(object sender, RoutedEventArgs e)
        {            
            ChildKimyasallar.Close();
        }

        private void BtnHesaplaKasar_Click(object sender, RoutedEventArgs e)
        {
            _Islem.KasarOranlariHesapla();
            DGridBoyaKasari.Items.Refresh();
        }

        private void BtnHesaplaBoya_Click(object sender, RoutedEventArgs e)
        {
            _Islem.BoyaKimyasalOranlariHesapla();
            _Islem.ApreOranlariHesapla();
            DGridBoya.Items.Refresh();
            DGridKimyasal.Items.Refresh();
            DGridApre.Items.Refresh();
        }

        private void BtnHesaplaYikama_Click(object sender, RoutedEventArgs e)
        {
            _Islem.YikamaOranlariHesapla();
            DGridYikama.Items.Refresh();
        }

        private void BtnApreYukle_Click(object sender, RoutedEventArgs e)
        {
            if (CmbApreNo.SelectedValue == null) return;
            foreach (var item in _Islem.Apreler)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            _Islem.Apreler.Clear();
            DGridApre.Items.Refresh();

            _Islem.ApreYukle((int)CmbApreNo.SelectedValue);
            DGridApre.ItemsSource = _Islem.Apreler;
            DGridApre.Items.Refresh();
        }

        private void BtnSilKasar_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalReceteAct secilen = DGridBoyaKasari.SelectedItem as vKimyasalReceteAct;
            if (secilen == null) return;

            if (MessageBox.Show("Boya kasarı kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.KasarSil(secilen))
            {
                
                DGridBoyaKasari.Items.Refresh();
               // _Islem.SilenPersonelKaydet(secilen.Id, App.KullaniciId);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!");
        }

        private void BtnSilOnİslem_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalReceteAct secilen = DGridOnIslem.SelectedItem as vKimyasalReceteAct;
            if (secilen == null) return;

            if (MessageBox.Show("Ön işlem kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.OnIslemSil(secilen))
            {
                DGridOnIslem.Items.Refresh();
                //_Islem.SilenPersonelKaydet(secilen.Id, App.KullaniciId);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!");
        }

        private void BtnSilApre_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalReceteAct secilen = DGridApre.SelectedItem as vKimyasalReceteAct;
            if (secilen == null) return;

            if (MessageBox.Show("Apre kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.ApreSil(secilen)) 
            { 
                DGridApre.Items.Refresh();
               // _Islem.SilenPersonelKaydet(secilen.Id, App.KullaniciId);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!");
        }

        private void BtnSilYikama_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalReceteAct secilen = DGridYikama.SelectedItem as vKimyasalReceteAct;
            if (secilen == null) return;

            if (MessageBox.Show("Yıkama kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Islem.YikamaSil(secilen))
            {
                DGridYikama.Items.Refresh();
                //_Islem.SilenPersonelKaydet(secilen.Id, App.KullaniciId);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!");
        }

        private void BtnSilKimyasal_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalReceteAct secilen = DGridKimyasal.SelectedItem as vKimyasalReceteAct;
            if (secilen == null) return;

            if (MessageBox.Show("Kimyasal kaydı silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            
            if (_Islem.KimyasalSil(secilen))
            {
                DGridKimyasal.Items.Refresh();
               // _Islem.SilenPersonelKaydet(secilen.Id, App.KullaniciId);
            }
            else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!");
        }

        private void ChildKimyasallar_Closed(object sender, EventArgs e)
        {
            TxtAciklama.Text = "";
        }

        private void BtnOnayla_Click(object sender, RoutedEventArgs e)
        {
            if (_Islem.ReceteKayitliMi(_Islem.Recete.Id))
            {
                bool receteOnayliMi = _Islem.OnayKontrol(_Islem.Recete.Id);

                if (receteOnayliMi)
                {
                    if (MessageBox.Show("Reçetenin onayı kaldırılacak...?\n\nReçete No : " + _Islem.Recete.ReceteNo + "\nRenk No : " + _Islem.Recete.RenkKodu, App.AlertCaption,
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                    if (!_Islem.SilinecekVarMi(_Islem.Recete.Id))
                    {
                        MessageBox.Show("Reçetenin Onayı Kaldırılamaz! Onay bekleyen talebiniz bulunmaktadır");
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("Reçete onaya gönderilecek...?\n\nReçete No : " + _Islem.Recete.ReceteNo + "\nRenk No : " + _Islem.Recete.RenkKodu, App.AlertCaption,
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                }
                if (_Islem.temizle(_Islem.Recete.Id))
                {
                    int tempi = _Islem.OnaylininId(_Islem.Recete.Id);

                    if (_Islem.Onayla())
                    {
                        _Islem.ReceteRename(tempi);
                        _Islem.receteAyarla(_Islem.Recete.Id, App.KullaniciId, receteOnayliMi);
                        LoadPage();
                        ChildKimyasallar.Close();
                    }
                    else MessageBox.Show("Hata oluştu.\n\nOnaylanamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Reçete onaylanamaz bu renkte onay bekleyen başka reçete var.");
                }
            }
            else
            {
                MessageBox.Show("Onaylama yapabilmek için önce reçeteyi kaydetmeniz gerekmektedir!...");
            }

        }

        private void BtnYazdir_Click(object sender, RoutedEventArgs e)
        {
            if (!_Islem.OnayKontrolKimyasal(_Islem.Recete.Id))
            {
                if (!_Islem.OnaylanmamisMi(_Islem.Recete.Id))
                {

                    //if (MessageBox.Show("Yazdırılsın Mı ?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    //    return;

                    if (_Islem.SiparisleriBoyandiIsaretle() == false)
                    {
                        MessageBox.Show("Yazdırılamadı..!\n\nSiparişler boyandı olarak işaretlenemedi.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    _Islem.Recete.Aciklama = TxtAciklama.Text;                   
                    _Islem.KaydetNew(App.KullaniciId);                    

                    DtlRapor raporlama = new DtlRapor();
                    List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                    {
                        new DtlRapor.RaporItem("DSBoya", _Islem.Boyalar),
                        new DtlRapor.RaporItem("DSKimyasal", _Islem.Kimyasallar),
                        new DtlRapor.RaporItem("DSApre", _Islem.Apreler),
                        new DtlRapor.RaporItem("DSKimyasalRecete", new List<vKimyasalRecete>(){ _Islem.Recete}),
                        new DtlRapor.RaporItem("DSRecetePartileri", _Islem.RecetePartiBilgileriGetir()),
                        new DtlRapor.RaporItem("DSKasar", _Islem.Kasarlar),
                        new DtlRapor.RaporItem("DSYikama", _Islem.Yikamalar),
                        new DtlRapor.RaporItem("DSOnIslem", _Islem.OnIslemler)

                    };

                    //raporlama.RaporGoster("RprBoyaRecetesi", list);
                    //Direk yazdırsın
                    raporlama.RaporYazdir("RprBoyaRecetesi", list);                    
                    ChildKimyasallar.Close();
                    //ChildGenel.Content = raporlama;
                    //ChildGenel.Show();
                }
                else
                {
                    MessageBox.Show("Reçetenizde onaylanmamış kimyasallar var onaylanmadan yazdırılamaz!", "Onaylanmamış Kimyasal Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Reçetenizde Onaylanmamış Kimyasal var yazdırılamaz!\n\nReçete No : " + _Islem.Recete.ReceteNo + "\nRenk No : " + _Islem.Recete.RenkKodu, "Onaysız Reçete Yazdırma Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            vPartiler secilen = ((Button)sender).DataContext as vPartiler;
            if (secilen == null) return;

            ChildGenel.Content = new LKUI.Details.DtlSiparisInfo(secilen.SiparisActId);
            ChildGenel.Show();
        }

        private void pgDGridUygunPartiler_Paged(object sender, RoutedEventArgs e)
        {
            DGridPartiDetaylari.ItemsSource = _Islem.UygunPartiler;
        }

        private void DGridReceteDetaylari_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridReceteDetaylari.SelectedItem == null) return;

            vKimyasalRecete secilen = DGridReceteDetaylari.SelectedItem as vKimyasalRecete;
            _Islem = new KimyasalRecete(secilen);
            Refresh();
        }

        private void menuItemStokCikisiYap_Click(object sender, RoutedEventArgs e)
        {
            vKimyasalRecete secilen = DGridReceteDetaylari.SelectedItem as vKimyasalRecete;
            if (secilen == null) return;
            if (secilen.StoktanDusulduMu && MessageBox.Show("Daha önce stok çıkışı yapılmış..!\n\nTekrar stok çıkışı yapılsın mı ?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            try
            {
                _Islem.ReceteleriYukle(false);
                secilen.StoktanDusulduMu = _Islem.KimyasalStokCikisiYap();
                if (secilen.StoktanDusulduMu == false) MessageBox.Show("Hata oluştu.\n\nStok çıkışı yapılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBox.Show("Stok çıkışı yapıldı.\n\nReçete No : " + secilen.ReceteNo, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                    DGridReceteDetaylari.Items.Refresh();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
            }
        }

        private void menuItemLogoyaAktar_Click(object sender, RoutedEventArgs e)
        {
            if (_Islem == null) return;      

            Logo lg = new Logo();
            lg.BoyaveKimyasalSarfFisi(_Islem.Recete);
            MessageBox.Show("Sarf fişi aktarıldı. Lütfen kontrol ediniz!");  
        }

        private void BtnMuadilYukle_Click(object sender, RoutedEventArgs e)
        {
            DGridMuadilReceteler.ItemsSource = KimyasalRecete.MuadilReceteleriGetir(_Islem.Recete.RenkId);
            ChildMuadilReceteler.Show();
        }

        private void menuItemMuadilSec_Click(object sender, RoutedEventArgs e)
        {
            if (DGridMuadilReceteler.SelectedItem == null) return;

            vKimyasalReceteMuadiller secilen = DGridMuadilReceteler.SelectedItem as vKimyasalReceteMuadiller;
           
            ReceteyiTemizle();  
            _Islem.BoyaKimyasalYukleMuadiklReceteden(secilen.ReceteId);
            DGridBoya.Items.Refresh();
            DGridKimyasal.Items.Refresh();
            DGridApre.Items.Refresh();
            DGridBoyaKasari.Items.Refresh();
            DGridOnIslem.Items.Refresh();
            DGridYikama.Items.Refresh();
            ChildMuadilReceteler.Close();            
        }

        private void ReceteyiTemizle()
        {
            foreach (var item in _Islem.Apreler)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            foreach (var item in _Islem.Yikamalar)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            foreach (var item in _Islem.OnIslemler)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            foreach (var item in _Islem.Boyalar)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            foreach (var item in _Islem.Kimyasallar)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            foreach (var item in _Islem.Kasarlar)
            {
                if (item.Id > 0)
                    _Islem.KasarSilVeritabanindan(item);
            }

            _Islem.Kasarlar.Clear();
            _Islem.Kimyasallar.Clear();
            _Islem.Boyalar.Clear();
            _Islem.Kasarlar.Clear();
            _Islem.OnIslemler.Clear();
            _Islem.Yikamalar.Clear();
            _Islem.Apreler.Clear();


            DGridApre.Items.Refresh();
            DGridBoya.Items.Refresh();
            DGridBoyaKasari.Items.Refresh();
            DGridKimyasal.Items.Refresh();
            DGridOnIslem.Items.Refresh();
            DGridYikama.Items.Refresh();
        }

        private void BtnTumReceteyiTemizle_Click(object sender, RoutedEventArgs e)
        {
            ReceteyiTemizle();            
        }

        private void BtnGuncelledim_Click(object sender, RoutedEventArgs e)
        {
            _Islem.NuanslariGuncelledim();
        }
    }

    public class ReceteMesajVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            vPartiler parti = (vPartiler)value;

            Visibility snc = Visibility.Hidden;

            if (parti != null && string.IsNullOrEmpty(parti.BoyaNotu) == false)
                snc = Visibility.Visible;

            return snc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Visibility.Hidden;
        }
    }
}

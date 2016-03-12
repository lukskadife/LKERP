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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSiparisTumMaliyet.xaml
    /// </summary>
    public partial class PageSiparisTumMaliyet : UserControl
    {
        private int siparisId;
        private string siparisNo, musteri, durum;

        List<vSiparisAct> ListSiparisUrunler = new List<vSiparisAct>();
        List<vSiparisKaliteRaporu> ListSiparisKalite = new List<vSiparisKaliteRaporu>();
        List<vSiparisParcaRaporu> ListSiparisParca = new List<vSiparisParcaRaporu>();
        List<vTumMaliyetlerGrup> ListTumMaliyetlerGroup = new List<vTumMaliyetlerGrup>();
        List<vSiparisKaliteRaporu> ListSiparisReProcess = new List<vSiparisKaliteRaporu>();

        Siparis _Siparis = new Siparis();

        public PageSiparisTumMaliyet(int siparisIdG, string siparisNoG, string musteriG, string durumG)
        {
            InitializeComponent();
            siparisId = siparisIdG;
            siparisNo = siparisNoG;
            musteri = musteriG;
            durum = durumG;
        }

        private void SiparisMaliyetiniAyarla(int sprsId, int btnId)
        {
            Maliyet.SiparisMaliyetleriniAyarla(sprsId, btnId);
        }

        private void PageLoad()
        {

            double birinciKal = 0, ikinciKal = 0, genelToplam = 0, metreFiyati = 0, satisFiyati = 0, hata = 0, parca = 0, sevkMetresi = 0, maliyetFiyati = 0, iplik = 0, bukum = 0, cozgu = 0, dokuma = 0, boyahane = 0, reProcess = 0;//
            int yuzde = 0;

            lblSiparisNo.Content = siparisNo;
            lblMusteri.Content = musteri;
            
            iplik = Maliyet.MaliyetIplikToplamGetir(siparisId);

            bukum = Maliyet.MaliyetBukumToplamGetir(siparisId);

            cozgu = Maliyet.MaliyetCozguToplamGetir(siparisId);

            dokuma = Maliyet.MaliyetDokumaToplamGetir(siparisId);

            boyahane = Maliyet.MaliyetBoyahaneToplamGetir(siparisId);

            genelToplam = iplik + bukum + cozgu + dokuma + boyahane;

            //Eksik//lblSonGuncellemeTarihi.Content = maliyetListesi.Select(c => c.Tarih).FirstOrDefault();

            ListSiparisUrunler = _Siparis.SiparisUrunleriGetir(siparisId);

            DGSiparisler.ItemsSource = ListSiparisUrunler;

            DGBoyahaneProcessler.ItemsSource = Maliyet.SiparisinBoyahaneProcessleriniGetir(siparisId);

            sevkMetresi = ListSiparisUrunler.Sum(c => c.SevkMiktar);

            lblSevkMetresi.Content = sevkMetresi + " m";

            ListSiparisKalite = Maliyet.SiparisinKaliteMetresiniGetir(siparisId);

            ListSiparisParca = Maliyet.SiparisinParcaMetresiniGetir(siparisId);

            ListSiparisReProcess = Maliyet.SiparisinReProcessMetresiniGetir(siparisId);

            if (ListSiparisKalite.Where(c => c.Kalitesi == "I.KALİTE").Any()) birinciKal = ListSiparisKalite.Where(c => c.Kalitesi == "I.KALİTE").FirstOrDefault().BrutMetre;

            if (ListSiparisKalite.Where(c => c.Kalitesi == "2.(IA) KLT").Any()) ikinciKal = ListSiparisKalite.Where(c => c.Kalitesi == "2.(IA) KLT").FirstOrDefault().BrutMetre;

            if (ListSiparisKalite.Where(c => c.Kalitesi == "HATALI").Any()) hata = ListSiparisKalite.Where(c => c.Kalitesi == "HATALI").FirstOrDefault().BrutMetre;

            if (ListSiparisParca.Where(c => c.Parca == "PARÇA").Any()) parca = ListSiparisParca.Where(c => c.Parca == "PARÇA").FirstOrDefault().ParcaMetre;

            if (ListSiparisReProcess.Count > 0) reProcess = ListSiparisReProcess.FirstOrDefault().BrutMetre;
            
            birinciKalite.Content = birinciKal + " m";

            ikinciKalite.Content = ikinciKal + " m";

            lblÜretimMetresi.Content = birinciKal + ikinciKal + " m";
            lblsiparisMetresi.Content = ListSiparisUrunler.Sum(c => c.Miktar) + " m";
            lblHata.Content = hata + " m";
            lblParca.Content = parca + " m";
            lblReProcess.Content = reProcess + " m";

            lblGenelToplam.Content = String.Format("{0:0.00}", genelToplam) + " TL";

            if (genelToplam == null) genelToplam = 0;

            if (durum == "Açık")
            {
                if (birinciKal != 0) metreFiyati = genelToplam / birinciKal;
            }
            else
            {
                if (sevkMetresi != 0) metreFiyati = genelToplam / sevkMetresi;//Normali bu
            }

            lblMetreFiyatiTL.Content = String.Format("{0:0.00}", metreFiyati);
            lblMetreFiyatiDlr.Content = String.Format("{0:0.00}", Maliyet.DovizKarsiliginiGetir(metreFiyati, 33));
            lblMetreFiyatiE.Content = String.Format("{0:0.00}", Maliyet.DovizKarsiliginiGetir(metreFiyati, 34));
            lblMetreFiyatiS.Content = String.Format("{0:0.00}", Maliyet.DovizKarsiliginiGetir(metreFiyati, 128));

            ListTumMaliyetlerGroup = Maliyet.TumMaliyetlerGrupGetir(siparisId);

            DGSiparisPartileri.ItemsSource = ListTumMaliyetlerGroup;

            foreach (var item in ListSiparisUrunler)
            {
                satisFiyati = satisFiyati + (Maliyet.TLKarsiliginiGetir(item.BirimFiyat, item.DovizId) * item.SevkMiktar);
                maliyetFiyati = maliyetFiyati + (item.SevkMiktar * metreFiyati);
            }

            if (satisFiyati > maliyetFiyati)
            {
                yuzde = Convert.ToInt32(karZararYuzdesiniHesapla(maliyetFiyati, satisFiyati, 0));
                lblKarZarar.Content = "%" + yuzde + " Kar Edilmiştir.";
                lblKarZarar.Foreground = System.Windows.Media.Brushes.White;
                lblKarZarar.Background = System.Windows.Media.Brushes.Green;
                _Siparis.SiparisKarZararKaydet(siparisId, "Kar", yuzde);
            }

            else if (satisFiyati < maliyetFiyati)
            {
                yuzde = Convert.ToInt32(karZararYuzdesiniHesapla(maliyetFiyati, satisFiyati, 1));
                lblKarZarar.Content = "%" + yuzde + " Zarar Edilmiştir.";
                lblKarZarar.Foreground = System.Windows.Media.Brushes.White;
                lblKarZarar.Background = System.Windows.Media.Brushes.Red;
                _Siparis.SiparisKarZararKaydet(siparisId, "Zarar", Convert.ToInt32(karZararYuzdesiniHesapla(maliyetFiyati, satisFiyati, 1)));
            }

            else
            {
                lblKarZarar.Content = "%0 Maliyetine Satılmıştır.";
                _Siparis.SiparisKarZararKaydet(siparisId, "Maliyetine", 0);
            }

            lblSatisToplam.Content = String.Format("{0:0.00}", satisFiyati) + " TL";


            lblIplik.Content = String.Format("{0:0.00}", iplik) + " TL";
            lblBukum.Content = String.Format("{0:0.00}", bukum) + " TL";
            lblCozgu.Content = String.Format("{0:0.00}", cozgu) + " TL";
            lblDokuma.Content = String.Format("{0:0.00}", dokuma) + " TL";
            lblBoyahane.Content = String.Format("{0:0.00}", boyahane) + " TL";

        }

        private double karZararYuzdesiniHesapla(double alis, double satis, int karZararId)
        {
            double yuzde = 0;

            if (alis != 0)
            {
                if (karZararId == 0) yuzde = ((satis - alis) * 100) / alis; //Kar

                else if (karZararId == 1)//Zarar
                {
                    yuzde = ((satis - alis) * 100) / alis;
                    yuzde = yuzde * (-1);
                }

                else yuzde = 0;//Maliyetine
            }

            return yuzde;
        }

        private void SiparisTumMaliyet_Loaded(object sender, RoutedEventArgs e)
        {
            SiparisMaliyetiniAyarla(siparisId, 1);
            PageLoad();
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            SiparisMaliyetiniAyarla(siparisId, 0);
            PageLoad();
        }

        private void btnOzet_Click(object sender, RoutedEventArgs e)
        {
            MaliyetFormu.Show();
            TabCntMaliyet.SelectedIndex = 0;
            DGSiparisPartileriMaliyetHam.ItemsSource = Maliyet.MaliyetTumunuGoster(siparisId);
        }


        private void TabCntMaliyet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TabCntMaliyet.SelectedIndex == 1) { if (DGIplikDetay.ItemsSource == null) DGIplikDetay.ItemsSource = Maliyet.MaliyetIplikDetayGetir(siparisId); }

            else if (TabCntMaliyet.SelectedIndex == 2) { if (DGBukumDetay.ItemsSource == null) DGBukumDetay.ItemsSource = Maliyet.MaliyetBukumDetayGetir(siparisId); }

            else if (TabCntMaliyet.SelectedIndex == 3) { if (DGCozguDetay.ItemsSource == null) DGCozguDetay.ItemsSource = Maliyet.MaliyetCozguDetayGetir(siparisId); }

            else if (TabCntMaliyet.SelectedIndex == 4) { if (DGDokumaDetay.ItemsSource == null) DGDokumaDetay.ItemsSource = Maliyet.MaliyetDokumaDetayGetir(siparisId); }

            else if (TabCntMaliyet.SelectedIndex == 5) { if (DGBoyahaneDetay.ItemsSource == null) DGBoyahaneDetay.ItemsSource = Maliyet.MaliyetBoyahaneDetayGetir(siparisId); }
            
        }

        private void MaliyetFormu_Closed(object sender, EventArgs e)
        {
            DGSiparisPartileriMaliyetHam.ItemsSource = null;
            DGIplikDetay.ItemsSource = null;
            DGBukumDetay.ItemsSource = null;
            DGCozguDetay.ItemsSource = null;
            DGDokumaDetay.ItemsSource = null;
            DGBoyahaneDetay.ItemsSource = null;
        }




    }
}

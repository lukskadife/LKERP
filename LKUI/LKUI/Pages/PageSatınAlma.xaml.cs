using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Details;
using LKUI.Controls;
using System.Linq;
using System.Collections.Generic;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSatınAlma.xaml
    /// </summary>
    public partial class PageSatınAlma : UserControl
    {
        public PageSatınAlma()
        {
            InitializeComponent();

            this._Durum = new tblDurumlar().DurumGetir(App.ClickedMenuItemId);
            TxtDurum.Text = this._Durum.DurumAdi;
        }

        private MalzemeTalep _Talep = new MalzemeTalep();
        private tblDurumlar _Durum;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DtTarih.DisplayDate = System.DateTime.Now.Date;
            LoadPage();
        }

        public List<vTalepMalzemeler> ListTalepler;

        private void LoadPage()
        {
            ListTalepler = _Talep.TalepAra(this._Durum.Id);
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c=>c.HepsiKarsilandiMi == false);
            DGridTalepler.Items.Refresh();
        }
       
        private void BtnFirmaKaydet_Click(object sender, RoutedEventArgs e)
        {
            _Talep.TalepFormuOlustur();
            ChildSatinAlma.Close();
        }

        private void TedarikciSec(vTalepMalzemeler secilen)
        {
            if (secilen == null) return;
            DtlMalzemeOrtFiyatlar cntOrtFiyatlar = new DtlMalzemeOrtFiyatlar(secilen);
            cntOrtFiyatlar.FirmaSelected += (snd, ea) =>
            {
                DtlMalzemeOrtFiyatlar cnt = snd as DtlMalzemeOrtFiyatlar;
                SatinAl(cnt.Secilen, Convert.ToDouble(cnt.TxtMiktar.Text), Convert.ToDouble(cnt.TxtFiyat.Text), cnt);
                if ((int)cnt.ComboBirim.SelectedValue == secilen.BirimId && Convert.ToDouble(cnt.TxtMiktar.Text) >= secilen.Miktar)
                {
                    ListTalepler[ListTalepler.FindIndex(c => c.TalepId == secilen.TalepId)].HepsiKarsilandiMi = true;
                    DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.HepsiKarsilandiMi == false);
                    DGridTalepler.Items.Refresh();
                }
                ChildSatinAlma.Close();
            };
            ChildSatinAlma.Caption = secilen.MalzemeAdi + " için fiyat listesidir...";
            ChildSatinAlma.Content = cntOrtFiyatlar;
            ChildSatinAlma.Show();
        }
      
        private void BtnFirma_Click(object sender, RoutedEventArgs e)
        {
            TedarikciSec(((System.Windows.FrameworkElement)(sender)).DataContext as vTalepMalzemeler);
        }

        private bool SatinAl(vMalzemeOrtFiyatlar firmaFiyat, double miktar, double fiyat, DtlMalzemeOrtFiyatlar cntOrtFiy)
        {
            vTalepKarsilama karsilamaForm = new vTalepKarsilama()
            {
                No = "Karş0001",
                PersonelAdi = App.PersonelAdi,
                PersonelId = App.PersonelId,
                Tarih = DateTime.Now,
                TedarikciAdi = firmaFiyat.TedarikciAdi,
                TedarikciId = firmaFiyat.TedarikciId
            };

            List<vTalepKarsilamaAct> listKarsilananlar = new List<vTalepKarsilamaAct>();
            vTalepMalzemeler ilgiliTalep = ListTalepler.Find(c=>c.TalepId == cntOrtFiy._TalepEdilenMalzeme.TalepId);
            if ((int)cntOrtFiy.ComboBirim.SelectedValue == ilgiliTalep.BirimId && Convert.ToDouble(cntOrtFiy.TxtMiktar.Text) >= ilgiliTalep.Miktar)
                ListTalepler.FindAll(c => c.TalepId == ilgiliTalep.TalepId).ForEach(c => c.HepsiKarsilandiMi = true);

            listKarsilananlar.Add(new vTalepKarsilamaAct()
            {
                BirimId = (cntOrtFiy.ComboBirim.SelectedItem as vMalzemeBirimleri).Id,
                BirimAdi = (cntOrtFiy.ComboBirim.SelectedItem as vMalzemeBirimleri).BirimAdi,
                Fiyat = fiyat,
                MalzemeAdi = cntOrtFiy._TalepEdilenMalzeme.MalzemeAdi,
                MalzemeId = firmaFiyat.MalzemeId,
                MalzemeKodu = cntOrtFiy._TalepEdilenMalzeme.MalzemeKodu,
                Miktar = miktar,
                TalepId = cntOrtFiy._TalepEdilenMalzeme.TalepId,
                Tarih = DateTime.Now,
                TedarikciAdi = firmaFiyat.TedarikciAdi,
                TedarikciId = firmaFiyat.TedarikciId,
                Doviz = (cntOrtFiy.ComboDoviz.SelectedItem as vAyarlar).Aciklama,
                DovizId = (int)cntOrtFiy.ComboDoviz.SelectedValue,
                Kur = string.IsNullOrEmpty(cntOrtFiy.TxtKur.Text) ? 0 : Convert.ToDouble(cntOrtFiy.TxtKur.Text)
            });

            CntSecilenFirma firmaForm = null;
            foreach (UIElement item in StackFirma.Children)
            {
                if (item is CntSecilenFirma)
                {
                    CntSecilenFirma cnt = item as CntSecilenFirma;

                    if (firmaFiyat.TedarikciId == cnt.TalepKarsilamaFormu.TedarikciId)
                    {
                        karsilamaForm = null;
                        cnt.Add(listKarsilananlar);
                        firmaForm = cnt;
                    }
                }
            }

            if (firmaForm == null)
            { 
                firmaForm = new CntSecilenFirma(karsilamaForm, listKarsilananlar);
                firmaForm.MouseDoubleClick += (snd, ea) =>
                {
                    CntSecilenFirma convertedCnt = snd as CntSecilenFirma;
                    DtlTalepKarsilama dtlKarsilananlar = new DtlTalepKarsilama(karsilamaForm,convertedCnt.ListTalepKarsilananlar, karsilamaForm.DurumId == _Talep.TalepTamamId);
                    dtlKarsilananlar.SatirSilindi += new DtlTalepKarsilama.SatirSilindiEvent(dtlKarsilananlar_SatirSilindi);
                    ChildSatinAlma.Caption = "No : " + convertedCnt.TalepKarsilamaFormu.No + "   -   Tedarikçi : " + convertedCnt.TalepKarsilamaFormu.TedarikciAdi;
                    ChildSatinAlma.Content = dtlKarsilananlar;
                    ChildSatinAlma.Show();

                    ChildSatinAlma.Closed += (s, e) =>
                    {
                        if (ChildSatinAlma.Content is LKUI.Details.DtlTalepKarsilama)
                        {
                            firmaForm.TalepKarsilamaFormu.OdemeSekli = dtlKarsilananlar.TxtOdemeSekli.Text;
                            firmaForm.TalepKarsilamaFormu.TerminTarihi = dtlKarsilananlar.DpTerminTarihi.SelectedDate;
                        }                        
                    };
                };

                StackFirma.Children.Add(firmaForm);
            }

            return true;
        }

        void dtlKarsilananlar_SatirSilindi(vTalepKarsilamaAct satirdanSilinen)
        {
            (DGridTalepler.ItemsSource as List<vTalepMalzemeler>).AddRange(_Talep.TalepAraWithTalepId(satirdanSilinen.TalepId.Value));
            DGridTalepler.Items.Refresh();
        }

        private void BtnKarsilamalariKaydet_Click(object sender, RoutedEventArgs e)
        {
            List<UIElement> silinecekler = new List<UIElement>();
            foreach (UIElement item in StackFirma.Children)
            {
                if (item is CntSecilenFirma)
                {
                    CntSecilenFirma cntFirma = item as CntSecilenFirma;
                    cntFirma.TalepKarsilamaFormu.DurumId = _Talep.ListTalepDurumlari[0].DurumId;

                    if (cntFirma.SaveKarsilama())
                    {
                        List<vTalepMalzemeler> listKarsilananlar = new List<vTalepMalzemeler>();

                        foreach (vTalepKarsilamaAct karsilanan in cntFirma.ListTalepKarsilananlar)
                            listKarsilananlar.Add(ListTalepler.Find(c => c.TalepId == karsilanan.TalepId));
                        
                        int yeniDurum = _Talep.TalepDurumlariGuncelle(listKarsilananlar, this._Durum.Id);

                        _Talep.TalepFormDurumuGuncelle(cntFirma.TalepKarsilamaFormu, yeniDurum);
                    }

                    silinecekler.Add(item);
                }
            }
            foreach (UIElement element in silinecekler) StackFirma.Children.Remove(element);
            LoadPage();
        }

        private void TxtFiltre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.MalzemeAdi.Contains(TxtAdi.Text));
        }

        private void TxtTalepEdenAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.TalepEdenAdi.Contains(TxtTalepEdenAdi.Text));
        }

        private void TxtTalepEdenKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.TalepEdenKodu.Contains(TxtTalepEdenKodu.Text));
        }

        private void TxtKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.MalzemeKodu.Contains(TxtKodu.Text));
        }

        private void TxtAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.MalzemeAdi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }

        private void TxtMiktar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.Miktar.ToString().Contains(TxtMiktar.Text));
        }

        private void TxtDetay_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.Detay.Contains(TxtDetay.Text));
        }

        private void DtTarih_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridTalepler.ItemsSource = ListTalepler.FindAll(c => c.Tarih == (DtTarih.SelectedDate));
        }

        private void DGridTalepler_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DGridTalepler.SelectedItem == null) return;
            TedarikciSec(DGridTalepler.SelectedItem as vTalepMalzemeler);
        }

        tblTalepler tmpTalep;
        private void BtnTalepDuzelt_Click(object sender, RoutedEventArgs e)
        {
            vTalepMalzemeler secilen = ((sender as FrameworkElement).DataContext as vTalepMalzemeler);
            if (secilen == null) return;
            if (secilen.TalepEdenId != App.PersonelId)
            {
                MessageBox.Show("Talep eden düzeltme yapabilir..\n\nTalep eden : " + secilen.TalepEdenAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            tmpTalep = new tblTalepler();
            tmpTalep.Id = secilen.TalepId;
            tmpTalep.DurumId = secilen.DurumId;
            vMalzemeler vMalzeme = new vMalzemeler().ArananMalzemeGetir(secilen.MalzemeId)[0];
            PageMalzemeIhtiyac.Malzeme malzeme = new PageMalzemeIhtiyac.Malzeme().vMalzemelerToMalzemeler(vMalzeme, new vPersonelBolumleri().PersonelBolumleriGetir(), vRenkler.RenkleriGetir());
            malzeme.Miktar = secilen.Miktar;
            malzeme.SecilenBirimId = secilen.BirimId;
            malzeme.Detay = secilen.Detay;
            malzeme.SecilenBolumId = secilen.BolumId;

            List<PageMalzemeIhtiyac.Malzeme> list = new List<PageMalzemeIhtiyac.Malzeme>();
            list.Add(malzeme);
            DGridGonder.ItemsSource = list;

            ChildTalepDuzelt.Show();
        }

        private void BtnTalepSil_Click(object sender, RoutedEventArgs e)
        {
            vTalepMalzemeler secilen = ((sender as FrameworkElement).DataContext as vTalepMalzemeler);
            if (secilen == null) return;

            if (MessageBox.Show(secilen.MalzemeAdi + "\n" + secilen.Miktar.ToString() + " " + secilen.BirimAdi + " " + "\n\nSilinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (secilen.TalepEdenId != App.PersonelId)
            {
                MessageBox.Show("Talep eden silebilir..\n\nTalep eden : " + secilen.TalepEdenAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (_Talep.TalepSil(secilen.TalepId))
            {
                MessageBox.Show("Talep silindi..!\n\nSilinen : " + secilen.Miktar.ToString() + " " + secilen.BirimAdi + " " + secilen.MalzemeAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
            }
            else MessageBox.Show("Talep silmede hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTalepGuncelle_Click(object sender, RoutedEventArgs e)
        {
            List<tblTalepler> listTalep = new List<tblTalepler>();
            LKUI.Pages.PageMalzemeIhtiyac.Malzeme m = DGridGonder.Items[0] as LKUI.Pages.PageMalzemeIhtiyac.Malzeme;
            tblTalepler tblTalep = new tblTalepler()
            {
                Detay = m.Detay,
                MalzemeId = m.Id,
                Miktar = Convert.ToDouble(m.Miktar.ToString("#.##")),
                TalepEdenId = App.PersonelId,
                BirimId = m.SecilenBirimId,
                Tarih = DateTime.Now,
                BolumId = m.SecilenBolumId
            };
            tblTalep.Id = tmpTalep.Id;
            tblTalep.DurumId = tmpTalep.DurumId;

            List<tblTalepler> list = new List<tblTalepler>();
            list.Add(tblTalep);
            if (_Talep.TalepGuncelle(list))
            {
                MessageBox.Show("Talep düzeltildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
                ChildTalepDuzelt.Close();
                tmpTalep = null;
            }
            else MessageBox.Show("Talep düzeltme sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    } 
}

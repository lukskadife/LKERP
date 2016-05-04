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
    /// Interaction logic for PagePartileme.xaml
    /// </summary>
    public partial class PagePartileme : UserControl
    {
        public PagePartileme()
        {
            InitializeComponent();
        }

        Partileme _PartiIslem { get; set; }

        private void BtnKumasGoster_Click(object sender, RoutedEventArgs e)
        {
            if (CmbSiparisNo.SelectedItem != null) DGridKumasGoster.ItemsSource = Partileme.SiparisTipleriGetir((int)CmbSiparisNo.SelectedValue);
            else DGridKumasGoster.ItemsSource = null;

            ChildKumasGoster.Show();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            _PartiIslem = new Partileme(new vPartiler() { Tarih = DateTime.Today, BoyamaTarihi = DateTime.Today, RePartiMi = false, PaketlendiMi = false, BoyandiMi = false });
            ChildPartilemeEkle.DataContext = _PartiIslem;
            if (CmbApreKodu.ItemsSource == null) CmbApreKodu.ItemsSource = Partileme.ApreleriGetir();
            DGridProsesler.ItemsSource = _PartiIslem.Processler;
            ChildPartilemeEkle.Show();
            TxtPartiMetresi.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbMusteri.ItemsSource = Partileme.MusterileriGetir();
            CmbOnaylayan.ItemsSource = Partileme.BoyaPersonelleriGetir();
            CmbPlanlayan.ItemsSource = Partileme.PlanlamaPersonelleriGetir();
            CmbProsesEkle.ItemsSource = Partileme.ProcessleriGetir();
            CmbGrupEkle.ItemsSource = tblProsesGrup.ProcessGruplariGetir();
            CmbMakina.ItemsSource = new Makina().MakinalariGetir(2);

            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
            DpTarih.SelectedDate = DateTime.Today;
            DGridProsesGruplari.ItemsSource = Partileme.FinishKartlariGetir();
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridPartileme.ItemsSource = Partileme.PartileriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
            ChildPartilemeEkle.DataContext = null;
        }

        private void CmbMusteri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbMusteri.SelectedItem == null) return;
            CmbSiparisNo.ItemsSource = Partileme.MusteriSiparisleriGetir(((LKLibrary.DbClasses.vSiparisler)(CmbMusteri.SelectedItem)).FirmaId);
            if (_PartiIslem.Parti.Id == 0) _PartiIslem.Parti.FinishKodId = 0;
            TxtTipChild.Text = "";
            TxtRenkChild.Text = "";
            LblProses.Content = "";
            DGridProsesler.Items.Refresh();
        }

        private void BtnProsesEkleChild_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProsesEkle.SelectedItem == null) return;
            if (_PartiIslem.ProcessEkle(CmbProsesEkle.SelectedItem as tblProses))
            {
                DGridProsesler.Items.Refresh();
            }
        }

        private void BtnProsesSilChild_Click(object sender, RoutedEventArgs e)
        {
            vPartiProcessleri secilen = DGridProsesler.SelectedItem as vPartiProcessleri;
            if (secilen == null) return;

            _PartiIslem.ProcessSil(secilen);
            DGridProsesler.Items.Refresh();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtPartiMetresi.TextGirisiDogruMu == false | CmbSiparisNo.GirisYapildiMi == false | CmbMusteri.GirisYapildiMi == false | CmbPlanlayan.GirisYapildiMi == false
                | TxtTipVaryant.TextGirisiDogruMu == false | TxtRenkVaryant.TextGirisiDogruMu == false)
            {
                MessageBox.Show("Kırmızı alanlar zorunludur.\n\nBoş geçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (_PartiIslem.Parti.SiparisActId == 0)
            {
                MessageBox.Show("Tip seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            try
            {
                _PartiIslem.Parti.RenkNo = TxtRenkChild.Text;
                if (_PartiIslem.PartiKaydet() == true)
                {
                    LoadPage();
                    ChildPartilemeEkle.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //parti düzeltme
        private void Duzelt()
        {
            if (DGridPartileme.SelectedItem == null) return;

            ChildPartilemeEkle.DataContext = _PartiIslem;
            TxtTipChild.Text = _PartiIslem.Parti.TipNo;
            TxtRenkChild.Text = _PartiIslem.Parti.RenkNo;
            LblProses.Content = _PartiIslem.Parti.FinishKodu;
            if (string.IsNullOrEmpty(_PartiIslem.Parti.BoyaNotu)) BtnInfo.Visibility = System.Windows.Visibility.Hidden;
            else BtnInfo.Visibility = System.Windows.Visibility.Visible;
            DGridProsesler.ItemsSource = _PartiIslem.Processler;
            if (CmbApreKodu.ItemsSource == null) CmbApreKodu.ItemsSource = Partileme.ApreleriGetir();
            ChildPartilemeEkle.Show();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void DGridPartileme_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridPartileme.SelectedItem == null) return;

            _PartiIslem = new Partileme(DGridPartileme.SelectedItem as vPartiler, true);

            btnAyar(_PartiIslem.Parti.BoyandiMi);
            if (_PartiIslem.Parti.RePartiMi == false)
            {
                DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
                DGridPlanlar.ItemsSource = _PartiIslem.PlanlananBarkodlar;
                BtnHamKumasPlanla.IsEnabled = true;
                BtnSilPlan.IsEnabled = true;
            }
            else
            {
                DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
                BtnHamKumasPlanla.IsEnabled = false;
                BtnSilPlan.IsEnabled = false;
                _PartiIslem.PlanlananBarkodlar = null;
            }
            DGridBarkodlar.Items.Refresh();
        }

        private void btnAyar(bool boyandiMi)
        {
            if (_PartiIslem.Parti.BoyandiMi == true)
            {
                BtnSil2.IsEnabled = false;
                TxtBarkot.IsEnabled = false;
            }
            else
            {
                BtnSil2.IsEnabled = true;
                TxtBarkot.IsEnabled = true;
            }
        }

        private void TxtBarkot_KeyDown(object sender, KeyEventArgs e)
        {
            if (TxtBarkot.Text.Length != 10) return;

            try
            {
                if (_PartiIslem == null)
                {
                    MessageBox.Show("Parti seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                _PartiIslem.BarkodEkle(TxtBarkot.Text);
                DGridBarkodlar.ItemsSource = null;
                if (_PartiIslem.Parti.RePartiMi == false) DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
                else DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
                TxtBarkot.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DGridKumasGoster_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vSiparisAct secilen = DGridKumasGoster.SelectedItem as vSiparisAct;
            if (secilen != null && string.IsNullOrEmpty(secilen.BoyaNotu) == false) BtnInfo.Visibility = System.Windows.Visibility.Visible;
            else BtnInfo.Visibility = System.Windows.Visibility.Hidden;

            string snc = _PartiIslem.PartiKullanilmisMi();
            if (snc != "")
            {
                MessageBox.Show(snc, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            
            if (secilen != null)
            {
                _PartiIslem.Parti.FinishKodId = secilen.FinishGrupId != null ? secilen.FinishGrupId.Value : 0;
                TxtTipChild.Text = secilen.TipNo;
                TxtRenkChild.Text = secilen.RenkNo;
                _PartiIslem.Parti.SiparisActId = secilen.Id;
                _PartiIslem.Parti.TipVaryant = secilen.Varyant;
                TxtTipVaryant.Text = secilen.Varyant;
                LblProses.Content = secilen.FinishAdi;
            }
            else
            {
                _PartiIslem.Parti.FinishKodId = 0;
                TxtTipChild.Text = "";
                TxtRenkChild.Text = "";
                _PartiIslem.Parti.TipVaryant = "";
                TxtTipVaryant.Text = "";
                _PartiIslem.Parti.SiparisActId = 0;
            }

            DGridProsesler.ItemsSource = _PartiIslem.Processler;
            ChildKumasGoster.Close();
        }

        private void ChildPartilemeEkle_Closed(object sender, EventArgs e)
        {
            TxtTipChild.Text = "";
            TxtRenkChild.Text = "";
            LblProses.Content = "";
            DGridPartileme.SelectedItems.Clear();
            BtnInfo.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            vPartiler secilen = DGridPartileme.SelectedItem as vPartiler;
            if (secilen == null) return;
            if (MessageBox.Show("Kayıt silinsin mi ?\n\nParti No : " + secilen.PartiNo.ToString(), App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            
            
            try
            {
                if (_PartiIslem != null && _PartiIslem.PartiSil() == false) DGridPlanlar.Items.Refresh();
                else MessageBox.Show("Hata oluştu.\n\nKayıt silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnSil2_Click(object sender, RoutedEventArgs e)
        {
            if (DGridBarkodlar.SelectedItem == null) return;

            if (MessageBox.Show("Kayıt silinsin mi ?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            bool snc = false;
            if (_PartiIslem != null)
            {
                if (_PartiIslem.Parti.RePartiMi == false)
                {
                    snc = _PartiIslem.BarkodSil(DGridBarkodlar.SelectedItem as vHamKumaslar);
                    DGridBarkodlar.ItemsSource = null;
                    DGridBarkodlar.ItemsSource = _PartiIslem.Barkodlar;
                }
                else 
                {
                    snc = _PartiIslem.BarkodSil(DGridBarkodlar.SelectedItem as vReProcessBarkodlari);
                    DGridBarkodlar.ItemsSource = null;
                    DGridBarkodlar.ItemsSource = _PartiIslem.ReBarkodlar;
                }
            }
            if (snc == false) MessageBox.Show("Hata oluştu.\n\nBarkod silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_PartiIslem.Parti.SiparisActId == 0)
            {
                MessageBox.Show("Sipariş tipi seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            ChildGenel.Content = new DtlSiparisInfo(siparisActId: _PartiIslem.Parti.SiparisActId);
            ChildGenel.Show();
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void MItemRefakatKartiGoster_Click(object sender, RoutedEventArgs e)
        {
            if (DGridPartileme.SelectedItem == null) return;

            if (_PartiIslem.Parti.RePartiMi == false)
            {

                Rapor rapor = new Rapor("RprRefakatKarti");
                DtlRapor dtlRapor = new DtlRapor();
                dtlRapor.Yazdirildi += new RoutedEventHandler(dtlRapor_Yazdirildi);
                ChildRapor.Content = dtlRapor;
                ChildRapor.Show();

                Partileme.RefakatKartRapor refakatKarti = new Partileme.RefakatKartRapor((DGridPartileme.SelectedItem as vPartiler).Id);
                dtlRapor.viewerInstance.LocalReport.DataSources.Clear();

                Microsoft.Reporting.WinForms.ReportDataSource item = new Microsoft.Reporting.WinForms.ReportDataSource();
                item.Name = "DS_Parti";
                item.Value = refakatKarti.Parti;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item);

                Microsoft.Reporting.WinForms.ReportDataSource item1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item1.Name = "DS_Siparis";
                item1.Value = refakatKarti.Siparis;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item1);

                Microsoft.Reporting.WinForms.ReportDataSource item2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item2.Name = "DS_PartiProcesses";
                item2.Value = refakatKarti.PartiProcesses;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item2);

                Microsoft.Reporting.WinForms.ReportDataSource item3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item3.Name = "DS_Apre";
                item3.Value = refakatKarti.ApreKimyasallari;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item3);

                Microsoft.Reporting.WinForms.ReportDataSource item4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item4.Name = "DS_ApreRenk";
                item4.Value = refakatKarti.ApreRenk;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item4);

                Microsoft.Reporting.WinForms.ReportDataSource item5 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item5.Name = "DS_PlanlananKumaslar";
                item5.Value = _PartiIslem.PlanlananBarkodlar;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item5);

                dtlRapor.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                dtlRapor.viewerInstance.RefreshReport();
            }
            else
            {
                Rapor rapor = new Rapor("RprRefakatKartiReParti");
                DtlRapor dtlRapor = new DtlRapor();
                dtlRapor.Yazdirildi += new RoutedEventHandler(dtlRapor_Yazdirildi);
                ChildRapor.Content = dtlRapor;
                ChildRapor.Show();

                Partileme.RefakatKartRapor refakatKarti = new Partileme.RefakatKartRapor((DGridPartileme.SelectedItem as vPartiler).Id);
                dtlRapor.viewerInstance.LocalReport.DataSources.Clear();

                Microsoft.Reporting.WinForms.ReportDataSource item = new Microsoft.Reporting.WinForms.ReportDataSource();
                item.Name = "DS_Parti";
                item.Value = refakatKarti.Parti;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item);

                Microsoft.Reporting.WinForms.ReportDataSource item1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item1.Name = "DS_Siparis";
                item1.Value = refakatKarti.Siparis;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item1);

                Microsoft.Reporting.WinForms.ReportDataSource item2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item2.Name = "DS_PartiProcesses";
                item2.Value = refakatKarti.PartiProcesses;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item2);

                Microsoft.Reporting.WinForms.ReportDataSource item3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item3.Name = "DS_Apre";
                item3.Value = refakatKarti.ApreKimyasallari;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item3);

                Microsoft.Reporting.WinForms.ReportDataSource item4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                item4.Name = "DS_ApreRenk";
                item4.Value = refakatKarti.ApreRenk;
                dtlRapor.viewerInstance.LocalReport.DataSources.Add(item4);                

                dtlRapor.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                dtlRapor.viewerInstance.RefreshReport();
            }
        }

        void dtlRapor_Yazdirildi(object sender, RoutedEventArgs e)
        {
            this._PartiIslem.RefakatKartiCikartildi();
        }

        private void ChildRapor_Closed(object sender, EventArgs e)
        {
            ChildRapor.Content = null;
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildPartilemeEkle.Close();
        }

        private void BtnReEkle_Click(object sender, RoutedEventArgs e)
        {
            _PartiIslem = new Partileme(new vPartiler() { Tarih = DateTime.Today, BoyamaTarihi = DateTime.Today, RePartiMi = true });
            ChildPartilemeEkle.DataContext = _PartiIslem;
            if (CmbApreKodu.ItemsSource == null) CmbApreKodu.ItemsSource = Partileme.ApreleriGetir();
            DGridProsesler.ItemsSource = _PartiIslem.Processler;
            ChildPartilemeEkle.Show();
            TxtPartiMetresi.Text = "";
        }

        private void DGridPartileme_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
        }

        private void TxtPartiMetresi_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtPartiMetresi.SelectAll();
        }

        private void BtnGrupEkle_Click(object sender, RoutedEventArgs e)
        {
            if (CmbGrupEkle.SelectedItem == null)
            {
                MessageBox.Show("Grup seçmediniz..!", App.AlertCaption, MessageBoxButton.OK);
                return;
            }

            _PartiIslem.GrupProsessleriEkle(CmbGrupEkle.SelectedItem as tblProsesGrup);
            DGridProsesler.ItemsSource = null;
            DGridProsesler.ItemsSource = _PartiIslem.Processler;
        }

        private void MItemHamKumasSarfFisiAktar_Click(object sender, RoutedEventArgs e)
        {
            if (_PartiIslem == null) return;

            Logo lg = new Logo();
            lg.HamKumasSarfFisi(_PartiIslem.Parti);
            MessageBox.Show("Sarf fişi aktarıldı. Lütfen kontrol ediniz!");
        }

        private void BtnHamKumasPlanla_Click(object sender, RoutedEventArgs e)
        {
            DGridMuadilHamKumaslar.ItemsSource = null;
            List<vHamKumaslar> muadiller = new List<vHamKumaslar>();
            muadiller = _PartiIslem.PlanlanmamisTipBazliHamKumaslariGetir(_PartiIslem.Parti.TipNo, _PartiIslem.Parti.TipVaryant,_PartiIslem.Parti.DigerTipNo1, _PartiIslem.Parti.DigerTipNo2, _PartiIslem.Parti.DigerTipNo3).ToList();
            DGridMuadilHamKumaslar.ItemsSource = muadiller;
            
            if (muadiller.Count == 0) MessageBox.Show("Uygun kumaş yok.\n\nVaryant ve diğer tip nolarını kontrol edin!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            else ChildMuadilHamKumaslar.Show();
        }

        private void BtnSilPlan_Click(object sender, RoutedEventArgs e)
        {
            if (DGridPlanlar.SelectedItem == null) return;
            
            bool snc = false;
            if (_PartiIslem != null)
            {
                if (_PartiIslem.Parti.RePartiMi == false)
                {
                    snc = _PartiIslem.PlanSil(DGridPlanlar.SelectedItem as vHamKumaslar);
                    DGridPlanlar.ItemsSource = null;
                    DGridPlanlar.ItemsSource = _PartiIslem.PlanlananBarkodlar;
                }               
            }
            if (snc == false) MessageBox.Show("Hata oluştu.\n\nPlan silinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void BtnOnay_Click(object sender, RoutedEventArgs e)
        {
            if (DGridMuadilHamKumaslar.ItemsSource == null) return;
            List<vHamKumaslar> secilenler = DGridMuadilHamKumaslar.SelectedItems.Cast<vHamKumaslar>().ToList();
            if (secilenler.Count == 0) return;
            try
            {
                if (_PartiIslem.PlanlananOlarakIsaretle(secilenler))
                {
                    DGridPlanlar.ItemsSource = null;
                    DGridPlanlar.ItemsSource = _PartiIslem.PlanlananBarkodlar;                    
                    ChildMuadilHamKumaslar.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nPlanlanamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridFilitreTemizle()
        {
            DGridMuadilHamKumaslar.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in DGridMuadilHamKumaslar.Columns)
            {
                column.ClearFilters();
            }
            DGridMuadilHamKumaslar.FilterDescriptors.ResumeNotifications();
        }

        private void ChildMuadilHamKumaslar_Closed(object sender, EventArgs e)
        {
            GridFilitreTemizle();
        }
    }
}

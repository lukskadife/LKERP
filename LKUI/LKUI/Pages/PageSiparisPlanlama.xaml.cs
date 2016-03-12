using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using LKUI.Details;
using Telerik.Windows.Controls;
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSiparisPlanlama.xaml
    /// </summary>
    public partial class PageSiparisPlanlama : UserControl
    {
        public PageSiparisPlanlama()
        {
            InitializeComponent();
        }

        Planlama _Plan;

        void PrepareTezgahDataGrid()
        {
            if (CmbAy.SelectedIndex < 0 || string.IsNullOrEmpty((CmbYil.SelectedValue as ComboBoxItem).Content.ToString()))
                return;
            int ay = (CmbAy.SelectedIndex + 1);
            int yil = Convert.ToInt32((CmbYil.SelectedValue as ComboBoxItem).Content);

            _Plan = new Planlama(App.PersonelId, ay, yil);

            //Datagrid column headerları 2 aylık planlamaya göre düzenleniyor. 
            foreach (Telerik.Windows.Controls.GridViewColumn item in DGridTezgahlar.Columns)
            {
                try
                {
                    int ind = Convert.ToInt32(item.UniqueName.Replace("Tarih", ""));

                    //Planlanan ilk ayın indisi 1 ile 31 arasındadır.
                    if (ind <= 31)
                    {
                        item.Header = ind.ToString("00") + "." + ay.ToString("00");
                        new DateTime(yil, ay, ind);//hesaplanan gün gerçekte var mı kontrolü.Eğer yoksa exception atacaktır ve o column hidden edilecektir. 
                        //Örn. 30 Şubat diye bir tarih olmayacağından o column hidden edilmelidir.
                    }

                    //Planı getirilecek ikinci ayın indisi ise 31 ile 62 arasındadır. 31 ile 62 sayıları sonraki ayın 1 ile 31'i şeklinde düzenlenmelidir.
                    else if (ind > 31)
                    {
                        int sonrakiAy = (ay == 12) ? 1 : ay + 1;
                        int sonrakiYil = (ay == 12) ? yil + 1 : yil;
                        item.Header = (ind - 31).ToString("00") + "." + sonrakiAy.ToString("00");
                        new DateTime(sonrakiYil, sonrakiAy, (ind - 31)); //hesaplanan gün gerçekte var mı kontrolü.Eğer yoksa exception atacaktır ve o column hidden edilecektir. 
                        //Örn. 30 Şubat diye bir tarih olmayacağından o column hidden edilmelidir.
                    }
                    

                    if (item.IsVisible == false) item.IsVisible = true;
                }
                catch (Exception e)
                {
                    string str = e.Message;
                    if (item.UniqueName.Contains("Tarih")) item.IsVisible = false;
                }
            }

            DGridTezgahlar.ItemsSource = _Plan.ListTezgahPlanlari;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSiparisler.ItemsSource = Planlama.SiparisUrunleriGetir();

            CmbTezgah.ItemsSource = new Makina().MakinalariGetir(1);
        }

        //private DataGridCellInfo _SelectedCell;
        private GridViewCellInfo _SelectedCell;
        private vTezgahPlanlama _SelectedPivotPlan;
        private DateTime? _SelectedTarih;
        private vPlanlama _SelectedPlan;

        private void DGridTezgahlar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //_SelectedCell = new DataGridCellInfo();
            //_SelectedPivotPlan = null;
            //_SelectedTarih = null;
            //_SelectedPlan = null;

            //if (DGridTezgahlar.SelectedCells.Count != 0)
            //{
            //    _SelectedCell = DGridTezgahlar.SelectedCells[0];
            //    _SelectedPivotPlan = _SelectedCell.Item as vTezgahPlanlama;
            //    _SelectedTarih = HucreTarihiGetir(DGridTezgahlar.SelectedCells[0]);
            //    if (_SelectedTarih != null) _SelectedPlan = _Plan.TezgahPlanDetayGetir(_SelectedPivotPlan.TezgahId, _SelectedTarih.Value);
            //}
        }

        DateTime? HucreTarihiGetir(GridViewCellInfo cell)
        {
            try
            {
                int yil = _Plan.PlanYili, ay = _Plan.PlanAyi;
                int gun = Convert.ToInt32(cell.Column.UniqueName.Replace("Tarih", ""));

                if (gun > 31)
                {
                    yil = (ay == 12) ? yil + 1 : yil;
                    ay = (ay == 12) ? 1 : ay + 1;
                    gun -= 31;
                }

                return new DateTime(yil, ay, gun);
            }
            catch
            {
                return null;
            }
        }

        private void CmbYil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrepareTezgahDataGrid();
        }

        private void CmbAy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrepareTezgahDataGrid();
        }

        private void menuItemKes_Click(object sender, RoutedEventArgs e)
        {
            List<vTezgahPlanlama> list = DGridTezgahlar.ItemsSource as List<vTezgahPlanlama>;
            _KesYapistir = new List<int>();

            foreach (GridViewCellInfo cell in DGridTezgahlar.SelectedCells)
            {
                if (list.IndexOf(cell.Item as vTezgahPlanlama) != list.IndexOf(DGridTezgahlar.SelectedCells[0].Item as vTezgahPlanlama))
                {
                    MessageBox.Show("Farklı satırdaki hücreler seçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                DateTime? tarih = HucreTarihiGetir(cell);
                if (tarih.HasValue == false) continue;

                _KesYapistir.Add(_Plan.TezgahPlanDetayGetir((cell.Item as vTezgahPlanlama).TezgahId, tarih.Value).Id);
            }
        }

        private void menuItemYapistir_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> tasinacakGunler = new List<DateTime>();
            foreach (GridViewCellInfo item in DGridTezgahlar.SelectedCells)
            {
                DateTime? tarih = HucreTarihiGetir(item);
                if (tarih.HasValue) tasinacakGunler.Add(tarih.Value);
            }
            _Plan.TezgahPlanTasi((DGridTezgahlar.SelectedCells[0].Item as vTezgahPlanlama).TezgahId, tasinacakGunler, _KesYapistir);
            LoadTezgahPlan();

            _KesYapistir = null;
        }

        List<int> _KesYapistir;

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_KesYapistir == null)
            {
                menuItemYapistir.Foreground = Brushes.Silver;
                menuItemYapistir.IsEnabled = false;
            }
            else
            {
                menuItemYapistir.Foreground = Brushes.Black;
                menuItemYapistir.IsEnabled = true;
            }

            if (_SelectedPlan == null)
            {
                menuItemSil.Foreground = Brushes.Silver;
                menuItemSil.IsEnabled = false;
            }
            else
            {
                menuItemSil.Foreground = Brushes.Black;
                menuItemSil.IsEnabled = true;
            }
        }

        void Otele(int gun)
        {
            if (DGridTezgahlar.SelectedCells.Count == 0 || DGridTezgahlar.SelectedCells.Count > 1) return;
            if (_SelectedTarih.HasValue == false) return;

            switch (_Plan.TezgahPlanOtele(_SelectedPivotPlan.TezgahId, _SelectedTarih.Value, gun))
	        {
                case 1:
                    LoadTezgahPlan();
                    break;
                case 0:
                    MessageBox.Show("Hata oluştu.\n\nİşlem yapılamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    break;
                case -1:
                    MessageBox.Show("Önceki günde plan var.\n\nGeri çekilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
                    break;
                default: break;
	        }
        }

        void LoadTezgahPlan()
        {
            _Plan = new Planlama(App.PersonelId, _Plan.PlanAyi, _Plan.PlanYili);
            DGridTezgahlar.ItemsSource = _Plan.ListTezgahPlanlari;
        }

        private void BtnOtele_Click(object sender, RoutedEventArgs e)
        {
            Otele(1);
        }

        private void BtnGeriCek_Click(object sender, RoutedEventArgs e)
        {
            Otele(-1);
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (CmbTezgah.SelectedItem == null)
            {
                MessageBox.Show("Tezgah seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (string.IsNullOrEmpty(TxtUretilenMetre.Text) || TxtUretilenMetre.Text == "0")
            {
                MessageBox.Show("Planlama yapılamaz.\n\nTezgahın günlük üreteceği metre tanımlı değil.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (string.IsNullOrEmpty(TxtToplamMetre.Text) || TxtToplamMetre.Text == "0")
            {
                MessageBox.Show("Planlanacak metre gerekli.\n\nKaydedilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (DpBasTarihi.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Başlangıç tarihi boş geçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            int hesaplananGunSayisi = (int)Math.Round((Convert.ToDouble(TxtToplamMetre.Text) / Convert.ToDouble(TxtUretilenMetre.Text)), 3);
            double sayi = Math.Round(Convert.ToDouble(TxtToplamMetre.Text) / Convert.ToDouble(TxtUretilenMetre.Text), 3);
            int asilGunSayisi = (int) Math.Ceiling(sayi);
            if (hesaplananGunSayisi != asilGunSayisi)
            {
                double girilmesiGereken = Math.Round(asilGunSayisi * Convert.ToDouble(TxtUretilenMetre.Text), 3);
                MessageBox.Show("Son gün planı tam değil.\n\nPlanlanacak metre " + girilmesiGereken.ToString() + " girilmelidir.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                TxtToplamMetre.Text = girilmesiGereken.ToString();
                return;
            }

            vPlanlama plan = GrdOzet.DataContext as vPlanlama;

            if (MessageBox.Show((CmbTezgah.SelectedItem as tblMakinalar).KodAd.ToString() + " tezgahına " + plan.TipNo + " tipinden planlama yapılacak.\n\nBaşlangıç Tarihi : "
                + plan.Tarih.Value.ToString("d MMMM yyyy") + "\nTahmini Bitiş Tarihi : " + (plan.Tarih.Value.AddDays(asilGunSayisi - 1)).ToString("d MMMM yyyy"), App.AlertCaption, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            try
            {
                _Plan.CozguyeAt(_PlanlanacakSiparis.TipId, Convert.ToDouble(TxtToplamMetre.Text), _PlanlanacakSiparis.SiparisId);
                int sonuc = _Plan.PlanlamaKaydet(plan, asilGunSayisi);
                if (sonuc == 1)
                {
                    LoadTezgahPlan();
                    DGridPlanlanacakSiparisler.ItemsSource = Planlama.SiparisUrunleriGetir2();
                    GrdOzet.DataContext = null;
                    TxtToplamMetre.Text = "";
                }
                else if (sonuc == -1) MessageBox.Show("Planlanan tarihlerde tezgahlar dolu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                else MessageBox.Show("Hata oluştu.\n\nKaydetme başarısız..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuItemSil_Click(object sender, RoutedEventArgs e)
        {
            if (_SelectedPlan == null) return;

            bool snc = true;
            foreach (GridViewCellInfo cell in DGridTezgahlar.SelectedCells)
            {
                vTezgahPlanlama planlar = cell.Item as vTezgahPlanlama;

                DateTime? tarihi = HucreTarihiGetir(cell);
                if (tarihi.HasValue)
                {
                    vPlanlama seciliPlan = _Plan.TezgahPlanDetayGetir(planlar.TezgahId, tarihi.Value);
                    if (_Plan.TezgahPlaniSil(seciliPlan) == false) snc = false;
                }
            }
            
            if (snc) LoadTezgahPlan();
        }

        vPlanSiparisleri2 _PlanlanacakSiparis;
        private void BtnPlanla_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri2 siparis = (sender as FrameworkElement).DataContext as vPlanSiparisleri2;
            GrdOzet.DataContext = new vPlanlama()
            {
                Miktar = null,
                SiparisId = siparis.SiparisId,
                SozlesmeNo = siparis.SozlesmeNo,
                TipId = siparis.TipId,
                TipNo = siparis.TipNo,
                Tarih = DateTime.Today
            };
            CmbTezgah.SelectedIndex = -1;
            if ((siparis.SiparisMetre - siparis.PlanMetre) <= 0)
            {
                MessageBox.Show("Siparişin tamamı planlanmış.\nPlan yapılamayacak.\n\nSipariş No : " + siparis.SozlesmeNo + "\nTip No : " + siparis.TipNo);
                return;
            }
            TxtToplamMetre.Text = (siparis.SiparisMetre - siparis.PlanMetre).ToString();
            _PlanlanacakSiparis = siparis;
        }

        private void CmbTezgah_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GrdOzet.DataContext == null) return;
            tblMakinalar tezgah = CmbTezgah.SelectedItem as tblMakinalar;
            if (CmbTezgah.SelectedValue != null)
            {
                DpBasTarihi.SelectedDate = _Plan.TezgahMusaitTarihiGetir((int)CmbTezgah.SelectedValue);
            }

            double gunlukDokuma = (tezgah == null || tezgah.DevirSayisi == null) ? 0 : Makina.TezgahGunlukDokumaMiktariGetir(tezgah.DevirSayisi == null ? 0 : tezgah.DevirSayisi.Value, (GrdOzet.DataContext as vPlanlama).TipId);
            (GrdOzet.DataContext as vPlanlama).Miktar = gunlukDokuma;
            TxtUretilenMetre.Text = gunlukDokuma.ToString();
        }

        void LoadCozguSepeti()
        {
            WrapCozgu.Children.Clear();
            foreach (vCozgu cozgu in Planlama.CozguSepetleriGetir())
            {
                CntKumasTipSepeti newSepet = new CntKumasTipSepeti();
                newSepet.HavHesaplaClicked += new RoutedEventHandler(newSepet_HavHesaplaClicked);
                newSepet.IsEmriClicked += new RoutedEventHandler(newSepet_IsEmriClicked);
                newSepet.DataContext = cozgu;

                WrapCozgu.Children.Add(newSepet);
            }
        }

        private void BtnCozgu_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri siparis = (sender as FrameworkElement).DataContext as vPlanSiparisleri;
            if (siparis.Miktar == 0) return;

            if (siparis.PlanMetre == 0 )
            {
                MessageBox.Show("Planlama yapılmadan çözgü yapılamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (siparis.CozguMetre + siparis.RezerveMetre >= siparis.PlanSiparisMetre)
            {
                MessageBox.Show("Daha önce çözgü sepetine atıldı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            try
            {
                vCozgu cozgu = _Plan.CozguyeAt(siparis.TipId, siparis.PlanMetre - siparis.CozguMetre, siparis.SiparisActId);
                if (cozgu == null) return;

                //çözgüdeki toplam miktar ile çözgüye yeni atılan miktar eşit değilse yeni bir sepet oluşturulmalıdır. foreach içerisine girmek gereksiz
                if (cozgu.Miktar != siparis.Miktar)
                    foreach (UIElement element in WrapCozgu.Children)
                    {
                        CntKumasTipSepeti cntSepet = (element as CntKumasTipSepeti);
                        vCozgu sepetCozgu = cntSepet.DataContext as vCozgu;

                        if (sepetCozgu.TipId == cozgu.TipId)
                        {
                            cntSepet.DataContext = cozgu;
                            break;
                        }
                    }

                //Eğer if şartı sağlanmıyorsa yeni bir sepet oluşturulmalı
                else
                {
                    CntKumasTipSepeti newSepet = new CntKumasTipSepeti();
                    newSepet.HavHesaplaClicked += new RoutedEventHandler(newSepet_HavHesaplaClicked);
                    newSepet.IsEmriClicked += new RoutedEventHandler(newSepet_IsEmriClicked);
                    newSepet.DataContext = cozgu;

                    WrapCozgu.Children.Add(newSepet);
                }

                siparis.CozguMetre = (siparis.CozguMetre == null ? 0 : siparis.CozguMetre) + siparis.Miktar;
                DGridSiparisler.Items.Refresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        void newSepet_IsEmriClicked(object sender, RoutedEventArgs e)
        {
            _SecilenCozguIsEmriSepeti = sender as CntKumasTipSepeti;
            _SecilenCozguIsEmri = _SecilenCozguIsEmriSepeti.DataContext as vCozgu;

            TxtTipNoIsEmri.Text = _SecilenCozguIsEmri.Tip;
            if (_Plan == null) _Plan = new Planlama(App.PersonelId, 0, 0);
            _Plan.CozguIsEmirleriYukle(_SecilenCozguIsEmri.TipId);
            DGridCozguIsEmri.ItemsSource = _Plan.CozguIsEmirleri;
            ChildCozguIsEmri.Caption = "Tip No : " + _SecilenCozguIsEmri.Tip + "  Miktar : " + _SecilenCozguIsEmri.Miktar.ToString();
            ChildCozguIsEmri.Show();
        }

        CntKumasTipSepeti _SecilenCozguIsEmriSepeti;
        vCozgu _SecilenCozguIsEmri;

        void newSepet_HavHesaplaClicked(object sender, RoutedEventArgs e)
        {
            DGridİplikİhtiyac.ItemsSource = null;
            TxtDokumaMetresi.Text = "";
            TxtAltZemin.Text = "";
            TxtHav.Text = "";
            TxtÜstZemin.Text = "";

            _SecilenCozguIsEmriSepeti = sender as CntKumasTipSepeti;
            _SecilenCozguIsEmri = (sender as CntKumasTipSepeti).DataContext as vCozgu;
            ChildHavHesapla.Caption = "Tip No : " + _SecilenCozguIsEmri.Tip + "  Miktar : " + _SecilenCozguIsEmri.Miktar.ToString();
            ChildHavHesapla.Show();
        }

        private void BtnHesapla_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtDokumaMetresi.Text)) return;
            if (_SecilenCozguIsEmri == null) return;

            if (TxtDokumaMetresi.DoubleTxt.Value > _SecilenCozguIsEmri.Miktar)
            {
                MessageBox.Show("Giriş çözgü sepetinden fazla olamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (_Plan == null) _Plan = new Planlama(App.PersonelId);

            _Plan.IhtiyacHesapla(TxtDokumaMetresi.DoubleTxt.Value, _SecilenCozguIsEmri.TipId);

            TxtAltZemin.Text = _Plan.AltZeminMetre.ToString();
            TxtHav.Text = _Plan.HavMetre.ToString();
            TxtÜstZemin.Text = _Plan.UstZeminMetre.ToString();
            DGridİplikİhtiyac.ItemsSource = _Plan.IplikIhtiyaclari;

        }

        private void BtnStok_Click(object sender, RoutedEventArgs e)
        {
            RezerveAc(sender);
        }

        private void BtnYeniTalep_Click(object sender, RoutedEventArgs e)
        {
            vMalzemeStokDurum secilen = DGridİplikİhtiyac.SelectedItem as vMalzemeStokDurum;
            if (secilen == null) return;
            PageTalep.BrdFiltre.Height = 0;
            PageTalep.DGridEkle.ItemsSource = null;
            PageTalep.DGridEkle.ItemsSource = new vMalzemeler().ArananMalzemeGetir(secilen.MalzemeId);
            PageTalep.DGridGonder.Items.Clear();

            ChildTalep.Show();
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri secilen = (sender as FrameworkElement).DataContext as vPlanSiparisleri;
            if (secilen == null) return;
            ChildGenel.Content = new DtlSiparisInfo(secilen.SiparisActId);
            ChildGenel.Show();
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.Content = null;
        }

        private void BtnHesaplaIsEmri_Click(object sender, RoutedEventArgs e)
        {
            if (!TxtDokumaMetresiIsEmri.TextGirisiDogruMu) return;

            ChildCozguIsEmri.DataContext = _Plan.CozguIsEmriHesapla(TxtDokumaMetresiIsEmri.DoubleTxt.Value);
        }

        private void BtnEkleIsEmri_Click(object sender, RoutedEventArgs e)
        {
            if (!TxtDokumaMetresiIsEmri.TextGirisiDogruMu || TxtDokumaMetresiIsEmri.DoubleTxt == 0) return;

            try
            {
                vCozguIsEmri hesaplar = ChildCozguIsEmri.DataContext as vCozguIsEmri;
                hesaplar.PersonelId = App.PersonelId;

                _Plan.CozguIsEmriEkle(hesaplar);
                DGridCozguIsEmri.Items.Refresh();

                ChildCozguIsEmri.DataContext = new vCozguIsEmri();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            
        }

        private void ChildCozguIsEmri_Closed(object sender, EventArgs e)
        {
            _SecilenCozguIsEmri = null;
            _SecilenCozguIsEmriSepeti = null;
            _Plan.CozguIsEmirleri.Clear();
            DGridCozguIsEmri.Items.Refresh();
            ChildCozguIsEmri.DataContext = new vCozguIsEmri();
            TxtTipNoIsEmri.Text = "";
        }

        private void BtnYazdirIsEmri_Click(object sender, RoutedEventArgs e)
        {
            if (_Plan.CozguIsEmirleri.Count == 0) return;

            try
            {
                if (_Plan.CozguIsEmirleriKaydet())
                {
                    _SecilenCozguIsEmri.Miktar -= _Plan.CozguIsEmirleri[0].DokumaMetre;
                    if (_SecilenCozguIsEmri.Miktar <= 0) WrapCozgu.Children.Remove(_SecilenCozguIsEmriSepeti);
                    else
                    {
                        _SecilenCozguIsEmriSepeti.DataContext = null;
                        _SecilenCozguIsEmriSepeti.DataContext = _SecilenCozguIsEmri;
                    }

                    _Plan.CozguIsEmirleri.ForEach(c => c.TezgahAdi = _Plan.CozguIsEmirleri[0].Tezgahlar.Find(f => f.Id == c.TezgahId).KodAd);
                    DGridCozguIsEmri.ToExcel<vCozguIsEmri>();
                    ChildCozguIsEmri.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnIsEmriSil_Click(object sender, RoutedEventArgs e)
        {
            vCozguIsEmri secilen = DGridCozguIsEmri.SelectedItem as vCozguIsEmri;
            if (secilen == null) return;

            if (MessageBox.Show("Seçili kayıt silinecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            if (_Plan.CozguIsEmriSil(secilen)) DGridCozguIsEmri.Items.Refresh();
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RezerveAc(object sender)
        {
            vPlanSiparisleri secilen = (sender as FrameworkElement).DataContext as vPlanSiparisleri;
            if (secilen == null) return;
            DGridHamStok.ItemsSource = Planlama.RezerveyeUygunlariGetir(secilen.TipId);
            DGridPlanlananStoklar.ItemsSource = Planlama.PlanlananTipMiktarlariGetir(secilen.TipId);
            ChildHamStokDurumu.Caption = secilen.SozlesmeNo + "  -  " + secilen.FirmaAdi;            
            ChildHamStokDurumu.Show();
            rezerveMusteriId = secilen.FirmaId;
        }

        int rezerveMusteriId;
        private void BtnRezerve_Click(object sender, RoutedEventArgs e)
        {
            vPlanRezerveUygunlar secilen = DGridHamStok.SelectedItem as vPlanRezerveUygunlar;
            if (secilen == null)
            {
                MessageBox.Show("Tip seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (TxtRezerveMiktar.TextGirisiDogruMu == false) return;

            if (_Plan.RezerveEt(rezerveMusteriId, secilen.TipId, secilen.KalitePuan, TxtRezerveMiktar.DoubleTxt.Value))
            {
                DGridHamStok.ItemsSource = Planlama.RezerveyeUygunlariGetir(secilen.TipId);
                TxtRezerveMiktar.Text = "";
            }
            else MessageBox.Show("Hata oluştu.\n\nRezerve kaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnRezerveGor_Click(object sender, RoutedEventArgs e)
        {
            vPlanRezerveUygunlar secilen = DGridHamStok.SelectedItem as vPlanRezerveUygunlar;
            if (secilen == null) return;

            DGridHamStokRezerve.ItemsSource = _Plan.RezerveleriGetir(secilen.TipId, secilen.KalitePuan);
            ChildRezerveler.Show();
        }

        private void BtnRezerveIptal_Click(object sender, RoutedEventArgs e)
        {
            vPlanRezerveler secilen = (sender as FrameworkElement).DataContext as vPlanRezerveler;

            if (MessageBox.Show("Rezerve silinecek..?\n\nTip : " + secilen.TipNo + "\nMetre : " + secilen.RezerveMetre, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Plan.RezerveSil(secilen))
            {
                DGridHamStokRezerve.ItemsSource = _Plan.RezerveleriGetir(secilen.TipId, secilen.KalitePuan);
                DGridHamStok.ItemsSource = Planlama.RezerveyeUygunlariGetir(secilen.TipId);
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTerminVer_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri siparis = (sender as FrameworkElement).DataContext as vPlanSiparisleri;
            if (siparis == null)
            {
                MessageBox.Show("Sipariş seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            DateTime? yeniTermin = (((sender as Button).Parent as StackPanel).FindName("DpTermin") as DatePicker).SelectedDate;

            if (_Plan.SiparisTerminVer(siparis, yeniTermin)) MessageBox.Show("Termin verildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Hata oluştu.\n\nTermin verilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MIPlanSil_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri2 secilen = DGridPlanlanacakSiparisler.SelectedItem as vPlanSiparisleri2;
            if (secilen == null) return;

            try
            {
                if (Planlama.SiparisPlaniSil(secilen))
                {
                    LoadTezgahPlan();
                    DGridPlanlanacakSiparisler.ItemsSource = Planlama.SiparisUrunleriGetir2();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void MICozguSil_Click(object sender, RoutedEventArgs e)
        {
            vPlanSiparisleri2 secilen = DGridPlanlanacakSiparisler.SelectedItem as vPlanSiparisleri2;
            if (secilen == null) return;

            if (Planlama.SiparisCozguSil(secilen))
            {
                LoadCozguSepeti();
                DGridPlanlanacakSiparisler.ItemsSource = Planlama.SiparisUrunleriGetir2();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabItemTezgahPlan.IsSelected)
                {
                    CmbYil.Text = DateTime.Now.Year.ToString();
                    CmbAy.SelectedIndex = DateTime.Now.Month - 1;
                    DGridPlanlanacakSiparisler.ItemsSource = Planlama.SiparisUrunleriGetir2();
                }
                else if (TabItemCozgu.IsSelected) LoadCozguSepeti();
                else if (TabTezgahDagilimlari.IsSelected) DGridTezgahDagilimlari.ItemsSource = Planlama.PlanRaporuGetir();
                else if (TabItemRapor.IsSelected) DGridPlanRapor.ItemsSource = Rapor.YoneticiKonsolRaporuGetir<vPlanRapor>("TabDokumaSiparisleri");
                else if (TabItemChart.IsSelected) DtlChartTip.LoadPlan();
                else if (TabItemSipChart.IsSelected) DtlChartSiparis.LoadPlan();
            }
        }

        private void DGridTezgahlar_SelectedCellsChanged(object sender, Telerik.Windows.Controls.GridView.GridViewSelectedCellsChangedEventArgs e)
        {
            _SelectedCell = null;
            _SelectedPivotPlan = null;
            _SelectedTarih = null;
            _SelectedPlan = null;

            if (DGridTezgahlar.SelectedCells.Count != 0)
            {
                _SelectedCell = DGridTezgahlar.SelectedCells[0];
                _SelectedPivotPlan = _SelectedCell.Item as vTezgahPlanlama;
                _SelectedTarih = HucreTarihiGetir(DGridTezgahlar.SelectedCells[0]);
                if (_SelectedTarih != null) _SelectedPlan = _Plan.TezgahPlanDetayGetir(_SelectedPivotPlan.TezgahId, _SelectedTarih.Value);
            }
        }

        private void DGridTezgahlar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GrdOzet.DataContext = _SelectedPlan;
        }

        private void MIPlanExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridTezgahlar.ToExcel<vTezgahPlanlama>();
        }
    }

    public class PlanMesajVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            vPlanSiparisleri sip = (vPlanSiparisleri)value;

            Visibility snc = Visibility.Hidden;

            if (sip != null && (string.IsNullOrEmpty(sip.BoyaNotu) == false || string.IsNullOrEmpty(sip.DokumaNotu) == false || string.IsNullOrEmpty(sip.NumuneNotu) == false
                || string.IsNullOrEmpty(sip.PlanlamaNotu) == false || string.IsNullOrEmpty(sip.TerminNotu) == false))
                snc = Visibility.Visible;

            return snc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Visibility.Hidden;
        }
    }
}

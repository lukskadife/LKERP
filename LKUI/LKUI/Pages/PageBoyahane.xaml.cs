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
using System.Windows.Threading;
using System.Diagnostics;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageBoyahane.xaml
    /// </summary>
    public partial class PageBoyahane : UserControl
    {
        public PageBoyahane()
        {
            InitializeComponent();
        }

        //ben
        Makina makina = new Makina();

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string pPartiNo, pProcessKodu;
        tblMakinalar secilenMakina;
        int secilenMakinaId = 0;
        int Sira = 0;
        string temps;

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            ChildBoyahaneEkle.DataContext = _Islem;
            ChildBoyahaneEkle.Show();
        }

        private void BtnDüzenle_Click(object sender, RoutedEventArgs e)
        {
            vBoyahaneProcess secilen = DGridBoyahane.SelectedItem as vBoyahaneProcess;
            if (secilen == null) return;
            TxtBarkod.IsEnabled = false;
            GrdProcess.Visibility = System.Windows.Visibility.Visible;
            _Islem.BarkodOkut(secilen.PartiNo);
            CmbProcess.ItemsSource = _Islem.PartiProcessleri;
            _Islem.SecilenProcess = secilen;
            ChildBoyahaneEkle.DataContext = _Islem;
            ChildBoyahaneEkle.Show();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DPBaslangic.SelectedDate != null && DPBitis.SelectedDate != null) LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DPBaslangic.SelectedDate != null && DPBitis.SelectedDate != null) LoadPage();
        }

        private void ChildBoyahaneEkle_Closed(object sender, EventArgs e)
        {
            GrdProcess.Visibility = System.Windows.Visibility.Hidden;
            _Islem = new Boyahane();
            TxtBarkod.Text = "";
            TxtBarkod.IsEnabled = true;
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridBoyahane.SelectedItem == null) return;
            vBoyahaneProcess secilen = DGridBoyahane.SelectedItem as vBoyahaneProcess;
            if (MessageBox.Show(String.Format("Kayıt silinsin mi ?\n\nParti No : {0}\nProcess :{1}", secilen.PartiNo.ToString(), secilen.ProcessAdi), App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            if (Boyahane.ProcessSil(secilen)) LoadPage();
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            ChildBoyahaneEkle.Close();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vPartiProcessleri secilen = CmbProcess.SelectedItem as vPartiProcessleri;
                if (secilen == null) return;

                if (CmbPersonel.GirisYapildiMi == false | CmbProcess.GirisYapildiMi == false | TxtMetreX.TextGirisiDogruMu == false)
                {
                    MessageBox.Show("Lütfen kırmızı alanları doldurunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                bool dahaOnceOkutulduMu = _Islem.ProsesDahaOnceOkutulduMu(secilen);
                if (dahaOnceOkutulduMu == false ||
                    (dahaOnceOkutulduMu == true &&
                    MessageBox.Show("Bu işlem daha önce okutulmuştur.\n\nDevam etmek istiyor musunuz..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    )
                {
                    _Islem.SecilenProcess.PersonelId = (CmbPersonel.SelectedItem as tblPersoneller).Id;
                    _Islem.SecilenProcess.Metre = TxtMetreX.DoubleTxt.HasValue == false ? 0 : TxtMetreX.DoubleTxt.Value;
                    _Islem.SecilenProcess.Aciklama = TxtAciklama.Text;
                    _Islem.ProcessKaydet(secilen);
                    ChildBoyahaneEkle.Close();
                    LoadPage();
                }
            }
            catch (Exception exc)
            {
                if (exc is System.ArgumentOutOfRangeException) MessageBox.Show("Paketleme prosesinden sonra proses okutulamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);

                else MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        Boyahane _Islem = new Boyahane();

        private void TxtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    _Islem.BarkodOkut(TxtBarkod.Text);
                    CmbProcess.SelectedValue = null;
                    CmbProcess.ItemsSource = _Islem.PartiProcessleri;
                    GrdProcess.Visibility = System.Windows.Visibility.Visible;
                    TxtBarkod.Text = "";
                }
                catch (Exception exc)
                {
                    if (exc is InvalidCastException) MessageBox.Show("Barkod girişi yanlış..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    else MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    GrdProcess.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void LoadPage()
        {
            if (DPBitis.SelectedDate == null || DPBitis.SelectedDate == null) return;
            DGridBoyahane.ItemsSource = Boyahane.BoyahaneProcessleriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbPersonel.ItemsSource = Boyahane.PersonelleriGetir();
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
            GrdProcess.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void CmbProcess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                vPartiProcessleri secilen = CmbProcess.SelectedItem as vPartiProcessleri;

                if (secilen == null || secilen.ProcessAdi == null) TxtProcNo.Text = "";
                else TxtProcNo.Text = secilen.ProcessKodu;
                _Islem.BoyahaneProcessSec(secilen);
                //GrdProcess.DataContext = _Islem.SecilenProcess;
                TxtMetreX.Text = "0";
                TxtAciklama.Text = "";
            }
        }

        private void TxtMetre_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtMetreX.SelectAll();
        }

        private void BtnYenileDeneme_Click(object sender, RoutedEventArgs e)
        {
            ProcessBarkodOkut.Show();
            ProcessBarkodOkut.FocusedElement = txtBarkodNew;
        }

        private void btnStartFiveMiniteGet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bTime.Content = DateTime.Now.ToLongTimeString();
            int btnTemp = 0;
            if (btnStart.Content.ToString() == "BAŞLA (F2)")
            {
                btnTemp = 1;
            }
            else
            {
                btnTemp = 2;
            }
            savingProcess("Başla", "", btnTemp);
            CalisanProcessRefresh();

            temps = null;
            temps = _Islem.SureyiHesapla(pPartiNo);
            DateTime dt = _Islem.TarihiGetir(pPartiNo);

            if (temps != null)
            {
                if (dt != null)
                {
                    bTime.Content = dt.ToLongTimeString();
                }
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_TickManuel);
                dispatcherTimer.Start();
                sw.Restart();
            }
            else
            {
                dispatcherTimer.Stop();
                gTime.Content = "00:00:00";
                bTime.Content = "00:00:00";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            ArizaEkle.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            savingProcess("Iptal", "", 4);
            CalisanProcessRefresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            savingProcess("Bitir", "", 5);
            CalisanProcessRefresh();
        }

        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            AciklamaEkle.Show();
        }

        private void ProcessBarkodOkut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                btnStart_Click(sender, e);
            }
            else if (e.Key == Key.F4)
            {
                btnStop_Click(sender, e);
            }
            else if (e.Key == Key.F6)
            {
                btnCancel_Click(sender, e);
            }
            else if (e.Key == Key.F8)
            {
                btnAdd_Click(sender, e);
            }
            else if (e.Key == Key.F11)
            {
                btnFinish_Click(sender, e);
            }
            else if (e.Key == Key.F12)
            {
                btnNote_Click(sender, e);
            }
        }

        private void ProcessBarkodOkut_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.IsEnabled = true;
            DGridMakinalar.ItemsSource = makina.MakinalariGetir(2).Where(c => c.Id != 177);
            cbxPersonel.ItemsSource = Boyahane.PersonelleriGetir().OrderBy(c => c.Adi).ToList();
            ProcessBarkodOkut.FocusedElement = txtBarkodNew;
            CalisanProcessRefresh();
        }

        private void DGridMakinalar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            secilenMakina = DGridMakinalar.SelectedItem as tblMakinalar;
            if (secilenMakina != null)
            {
                makineName.Content = secilenMakina.Adi;
            }
        }

        private void txtBarkodNewLoad()
        {
            try
            {
                if (txtBarkodNew.Text != string.Empty)
                {
                    string temp = txtBarkodNew.Text.ToUpper();

                    if (temp.Substring(0, 1) == "R")
                    {
                        pPartiNo = temp.Substring(0, 10);
                        pProcessKodu = temp.Substring(10, (temp.Length - 10));
                    }
                    else
                    {
                        pPartiNo = temp.Substring(0, 5);
                        pProcessKodu = temp.Substring(5, (temp.Length - 5));
                    }

                    bool partiVarMi = _Islem.PartiVarMi(pPartiNo);
                    if (partiVarMi == false)
                    {
                        throw new Exception("Parti Bulunamadı..!!");
                    }

                    bool processVarMi = _Islem.ProcessVarMi(pPartiNo, pProcessKodu);
                    if (processVarMi == false)
                    {
                        throw new Exception("Process Bulunamadı..!!");
                    }


                    int partiId = _Islem.PartiIdGetir(pPartiNo);

                    _PartiProcesses = Boyahane.SecilenProcessGetir(pPartiNo).OrderBy(o => o.Sira).ToList();
                    List<vBoyahaneProcess> BoyahaneProcessleriGetir = Boyahane.BoyahaneProcessleriGetir(pPartiNo);
                    vPartiProcessleri sonOkutulacak = _PartiProcesses.OrderBy(e => e.Sira).LastOrDefault();
                    bool dahaOnceOkutulduMu = false;

                    //if (BoyahaneProcessleriGetir.Where(c => c.CikisTarih == null).Count() > 0)
                    //    txtMetre.IsEnabled = true; // Devam eden process te açık yeni okutulacaklarda kapalıdır.
                    //else
                    //    txtMetre.IsEnabled = false;

                    var dogruSira = Boyahane.OkutulanProcessSiraGetir(partiId, pProcessKodu);
                    
                    if (dogruSira == null)                   
                        dahaOnceOkutulduMu = _Islem.ProsesDahaOnceOkutulduMu(_PartiProcesses.Where(c=>c.ProcessKodu == pProcessKodu).FirstOrDefault());
                    
                    if (dahaOnceOkutulduMu)
                        Sira = _PartiProcesses.Where(c => c.ProcessKodu == pProcessKodu).FirstOrDefault().Sira;
                    else
                        Sira = dogruSira.Sira;

                    var okutulan = _PartiProcesses.Where(c => c.ProcessKodu == pProcessKodu && c.Sira == Sira).FirstOrDefault();
                    txtIslem.Text = okutulan.ProcessAdi;
                    int siraTemp =0;
                   
                    siraTemp = Sira + 1;
           
                  

                    if (_PartiProcesses.Where(c => c.Sira == siraTemp).Count() != 0)
                    {
                        txtSonrakiIslem.Text = _PartiProcesses.Where(c => c.Sira == siraTemp).FirstOrDefault().ProcessAdi.ToString();
                    }
                    else
                    {
                        txtSonrakiIslem.Text = "";
                    }

                    if (pProcessKodu == "21")
                    {
                        try
                        {
                            var b = BoyahaneProcessleriGetir.Where(k => k.Sira == (okutulan.Sira - 1) && k.Silindi == false).Any();
                            if ((okutulan.Sira - 1 > 0) && !(BoyahaneProcessleriGetir.Where(k => k.Sira == (okutulan.Sira - 1) && k.Silindi == false).Any()))
                                throw new Exception("Önceki process okutulmamış.\n\nEklenemez..!");

                            if (BoyahaneProcessleriGetir.Where(c => c.CikisTarih == null).Count() > 0)
                                throw new Exception("Sonladırılmamış Process Var..!!");

                            if (BoyahaneProcessleriGetir.Where(k => k.ProcessId == okutulan.ProcessId && k.Silindi == false).Any())
                                throw new Exception("Paketleme daha önce okutulmuş. \nTekrardan okutulamaz..!!");

                            _Islem.Paketleme(pPartiNo, okutulan.Sira, partiId);
                            DGridProcess.ItemsSource = Boyahane.BoyahaneProcessleriGetir(pPartiNo);

                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        txtPartiNo.Text = pPartiNo;
                        DGridProcess.ItemsSource = BoyahaneProcessleriGetir;

                        temps = null;
                        temps = _Islem.SureyiHesapla(pPartiNo);
                        DateTime dt = _Islem.TarihiGetir(pPartiNo);

                        if (temps != null)
                        {
                            if (dt != null)
                            {
                                bTime.Content = dt.ToLongTimeString();
                            }
                            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_TickManuel);
                            dispatcherTimer.Start();
                            sw.Restart();
                        }
                        else
                        {
                            dispatcherTimer.Stop();
                            gTime.Content = "00:00:00";
                            bTime.Content = "00:00:00";
                        }

                        if (_Islem.ButonTextAyarla() == true)
                        {
                            btnStart.Content = "DEVAM (F2)";                          
                        }
                        else
                        {
                            btnStart.Content = "BAŞLA (F2)";
                            
                        }

                        vBoyahaneProcess metrePersonelMakina = _Islem.verileriGetir(pPartiNo, pProcessKodu);
                        if (metrePersonelMakina != null)
                        {
                            makineName.Content = metrePersonelMakina.MakinaAdi;
                            secilenMakinaId = _Islem.makinayiGetir(metrePersonelMakina.MakinaAdi, 2).Id;
                            cbxPersonel.SelectedValue = metrePersonelMakina.PersonelId;
                            txtMetre.Text = metrePersonelMakina.Metre.ToString();
                        }
                    }
                }
                ProcessBarkodOkut.FocusedElement = txtBarkodNew;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                txtBarkodNew.Clear();
            }
        }

        private void txtBarkodNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtBarkodNewLoad();
            }
        }

        private List<vPartiProcessleri> _PartiProcesses;
        public List<vPartiProcessleri> PartiProcesses { get { return _PartiProcesses; } }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = sw.Elapsed;
            gTime.Content = sw.Elapsed.ToString().Substring(0, 8);
        }

        public void dispatcherTimer_TickManuel(object sender, EventArgs e)
        {
            if (temps == null)
                temps = _Islem.SureyiHesapla(pPartiNo);

            TimeSpan ts = sw.Elapsed.Add(TimeSpan.Parse(temps));
            gTime.Content = ts.ToString().Substring(0, 8);
        }

        void clearBarcodeReadPage()
        {
            txtMetre.Clear();
            txtBarkodNew.Clear();
            txtPartiNo.Clear();
            txtIslem.Clear();
            txtSonrakiIslem.Clear();
            cbxPersonel.SelectedIndex = -1;
            DGridMakinalar.SelectedItem = null;
            secilenMakina = null;
            makineName.Content = "Makina Adı";
            secilenMakinaId = 0;
        }

        void savingProcess(string btn, string comment, int btnClick)
        {
            try
            {
                _Islem.BarkodOkut(pPartiNo);
                vPartiProcessleri secilen = Boyahane.SecilenProcessGetir(pPartiNo, pProcessKodu, Sira).FirstOrDefault(); 
                if (secilen == null) return;
               
                    try
                    {
                        double d = Convert.ToDouble(txtMetre.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Metre alanına rakam giriniz!");
                        return;
                    }
                
                if ((cbxPersonel.SelectedItem as tblPersoneller) == null)
                {
                    MessageBox.Show("Personel seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (secilenMakina == null && secilenMakinaId == 0)
                {
                    MessageBox.Show("Makina seçiniz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (txtBarkodNew.Text == string.Empty)
                {
                    MessageBox.Show("Barkodu okutunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                
                    if (txtMetre.Text == string.Empty)
                    {
                        MessageBox.Show("Metre alanını doldurunuz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                        return;
                    }
                

                bool dahaOnceOkutulduMu = _Islem.ProsesDahaOnceOkutulduMu(secilen);

                if (dahaOnceOkutulduMu != false)
                {
                    if (btnClick == 5 || MessageBox.Show("Bu işlem daha önce okutulmuştur.\n\nDevam etmek istiyor musunuz..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        tblPersoneller secilenPersonel = cbxPersonel.SelectedItem as tblPersoneller;
                        _Islem.BoyahaneProcessSec(secilen);
                        _Islem.SecilenProcess.PersonelId = (cbxPersonel.SelectedItem as tblPersoneller).Id;
                        if (secilenMakina != null)
                        {
                            _Islem.SecilenProcess.MakinaId = secilenMakina.Id;
                        }
                        else
                        {
                            _Islem.SecilenProcess.MakinaId = secilenMakinaId;
                        }
                        _Islem.SecilenProcess.ReProcess = true;
                        _Islem.SecilenProcess.Metre = Convert.ToDouble(txtMetre.Text);
                        _Islem.ProcessKaydet(secilen, btn, comment, _Islem.SecilenProcess.Metre);
                        ChildBoyahaneEkle.Close();
                        LoadPage();
                        DGridProcess.ItemsSource = Boyahane.BoyahaneProcessleriGetir(pPartiNo);
                        if (btnClick == 1)
                        {
                            sw.Reset();
                        }
                        else if (btnClick == 2)
                        {
                            sw.Reset();
                        }
                        else if (btnClick == 3)
                        {
                            sw.Reset();
                        }
                        else if (btnClick == 4)
                        {
                            sw.Reset();
                        }
                        else if (btnClick == 5)
                        {
                            dispatcherTimer.Stop();
                            sw.Reset();
                            gTime.Content = "00:00:00";
                            bTime.Content = "00:00:00";
                            clearBarcodeReadPage();
                        }
                    }
                    txtBarkodNewLoad();
                }
                else
                {
                    tblPersoneller secilenPersonel = cbxPersonel.SelectedItem as tblPersoneller;
                    _Islem.BoyahaneProcessSec(secilen);
                    _Islem.SecilenProcess.ReProcess = false;
                    _Islem.SecilenProcess.PersonelId = (cbxPersonel.SelectedItem as tblPersoneller).Id;
                    if (secilenMakina != null)
                    {
                        _Islem.SecilenProcess.MakinaId = secilenMakina.Id;
                    }
                    else
                    {
                        _Islem.SecilenProcess.MakinaId = secilenMakinaId;
                    }
                   
                    _Islem.SecilenProcess.Metre = Convert.ToDouble(txtMetre.Text);               
                    _Islem.ProcessKaydet(secilen, btn, comment, _Islem.SecilenProcess.Metre);
                    ChildBoyahaneEkle.Close();
                    LoadPage();
                    DGridProcess.ItemsSource = Boyahane.BoyahaneProcessleriGetir(pPartiNo);
                    dispatcherTimer.Start();
                    if (btnClick == 1)
                    {
                        sw.Reset();
                    }
                    else if (btnClick == 2)
                    {
                        sw.Reset();
                    }
                    else if (btnClick == 3)
                    {
                        sw.Reset();
                    }
                    else if (btnClick == 4)
                    {
                        sw.Reset();
                    }
                    else if (btnClick == 5)
                    {
                        dispatcherTimer.Stop();
                        sw.Reset();
                        gTime.Content = "00:00:00";
                        bTime.Content = "00:00:00";
                        clearBarcodeReadPage();
                    }
                }
               // txtBarkodNewLoad();
            }
            catch (Exception exc)
            {
                if (exc is System.ArgumentOutOfRangeException) MessageBox.Show("Paketleme prosesinden sonra proses okutulamaz..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);

                else MessageBox.Show(exc.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnNoteAdd_Click(object sender, RoutedEventArgs e)
        {
            savingProcess("Not", txtAciklamaAdd.Text, 6);
            txtAciklamaAdd.Clear();
            AciklamaEkle.Close();
        }

        private void ArizaEkle_Loaded(object sender, RoutedEventArgs e)
        {
            cmbArizaId.ItemsSource = Boyahane.ArizalariGetir().OrderBy(c => c.Id).ToList();
        }

        private void btnArizaAdd_Click(object sender, RoutedEventArgs e)
        {
            string alinanID = cmbArizaId.SelectedValue.ToString();
            if (alinanID != null)
            {
                alinanID = alinanID + "?" + txtArizaAdd.Text;
                savingProcess("Dur", alinanID.ToString(), 3);
                clearArizaEkle();
                ArizaEkle.Close();
            }
            else
            {
                MessageBox.Show("Lütfen arızayı seçiniz!...", "Arıza seçilmedi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void clearArizaEkle()
        {
            cmbArizaId.ItemsSource = Boyahane.ArizalariGetir().OrderBy(c => c.Id).ToList();
            txtArizaAdd.Clear();
        }       

        private void ProcessBarkodOkut_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clearBarcodeReadPage();
            DGridProcess.ItemsSource = null;
            dispatcherTimer.Stop();
            gTime.Content = "00:00:00";
            bTime.Content = "00:00:00";
            dispatcherTimer.IsEnabled = false;
            //sw.Reset();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearBarcodeReadPage();
        }

        private void CalisanProcessRefresh()
        {
            DGridProcesses.ItemsSource = Boyahane.CalisanProcessleriGetir().OrderByDescending(c => c.PartiId);
        }

    }
}

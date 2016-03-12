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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageBakımOnarım.xaml
    /// </summary>
    public partial class PageBakimOnarim : UserControl
    {
        public PageBakimOnarim()
        {
            InitializeComponent();
        }
        
        Makina.BakimOnarimTurleri _BakimOnarimTuru;
        public vBakimOnarim BakimOnarimForm;

        private tblMakinalar _SeciliMakina;
        public tblMakinalar SeciliMakina
        {
            get { return _SeciliMakina; }
            set
            {
                _SeciliMakina = value;
                TxtSeciliMakina.Text = value.Adi;
            }
        }

        private bool Kontroller()
        {
            if (TxtHarcananSure.Text.StringSayisalMi() == false)
            {
                MessageBox.Show("Harcanan süre sayısal olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            if (!DatePickerBaslangic.SelectedDate.HasValue || !DatePickerBitis.SelectedDate.HasValue)
            {
                MessageBox.Show("Başlangıç ve/veya bitiş tarihleri boş geçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            if (DatePickerBaslangic.SelectedDate.Value > DatePickerBitis.SelectedDate.Value)
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden büyük olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            return true;
        }

        List<string> SeciliVardiyalar = new List<string>();
        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (!Kontroller()) return;

            if (MessageBox.Show(SeciliMakina.Adi + " makinası için " + bakimStringi + " kaydedilecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_BakimOnarimTuru == Makina.BakimOnarimTurleri.Hicbiri) return;
            if (BakimOnarimForm == null) BakimOnarimForm = new vBakimOnarim();

            string vardiyalar = "";
            foreach (string item in SeciliVardiyalar) vardiyalar += (string.IsNullOrEmpty(vardiyalar) ? "" : "|") + item;

            if (BakimOnarimForm == null) BakimOnarimForm = new vBakimOnarim();
            
            BakimOnarimForm.Aciklama = TxtAciklama.Text;
            BakimOnarimForm.BasTarih = DatePickerBaslangic.SelectedDate.Value;
            BakimOnarimForm.BitisTarih = DatePickerBitis.SelectedDate.Value;
            BakimOnarimForm.HarcananSure = Convert.ToDouble(TxtHarcananSure.Text);
            BakimOnarimForm.MakinaAdi = SeciliMakina.Adi;
            BakimOnarimForm.MakinaId = SeciliMakina.Id;
            BakimOnarimForm.PersonelId = App.PersonelId;
            BakimOnarimForm.Turu = _BakimOnarimTuru.ToString();
            BakimOnarimForm.Vardiya = vardiyalar;
            BakimOnarimForm.OlusturmaTarihi = DateTime.Now;

            int bakimKayitSonucId = _Makina.BakimKaydet(BakimOnarimForm, _ListMalzemeler);
            if (bakimKayitSonucId == -1) MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                MessageBox.Show("Kaydetme başarılı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                this.BakimOnarimForm = _Makina.BakimOnarimFormuGetir(bakimKayitSonucId);
                LoadPage();
            }
        }

        private void BtnKullanılanMalzemeSec_Click(object sender, RoutedEventArgs e)
        {
            ChildKullanilanMalzemeSec.Show();
        }

        private void BtnSec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridEkle.ItemsSource = new vMalzemeler().ArananMalzemeleriGetir(TxtKodu.Text, (int)CmbMalzemeGruplari.SelectedValue, vMalzemeler.AramaKriteri.KodaGore);
        }

        private void TxtKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CmbMalzemeGruplari.SelectedValue == null) return;
            DGridEkle.ItemsSource = new vMalzemeler().ArananMalzemeleriGetir(TxtKodu.Text, (int)CmbMalzemeGruplari.SelectedValue, vMalzemeler.AramaKriteri.KodaGore);
        }

        Makina _Makina = new Makina();
        List<vBakimOnarimAct> _ListMalzemeler;

        private void EkleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TxtMalzemeMiktar.Text.StringSayisalMi() == false)
            {
                MessageBox.Show("Miktar girişi sayısal olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            vMalzemeBirimleri seciliBirim = CmbMalzemeBirim.SelectedItem as vMalzemeBirimleri;
            double miktar = new Stok().StokMiktariGetir((DGridEkle.SelectedItem as tblMalzemeler).Id);
            if (Convert.ToDouble(TxtMalzemeMiktar.Text) * (seciliBirim.AnaCarpan / seciliBirim.BirimCarpan) > miktar)
            {
                MessageBox.Show("Eklenemedi..\n\nStok yeterli değil..!\nMevcut stok : " + miktar.ToString(), App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            _ListMalzemeler.Add(new vBakimOnarimAct(){
                BakimOnarimId = BakimOnarimForm != null ? BakimOnarimForm.Id : 0,
                BirimAdi = (CmbMalzemeBirim.SelectedItem as vMalzemeBirimleri).BirimAdi,
                BirimId = (int)CmbMalzemeBirim.SelectedValue,
                Id = 0,
                MalzemeAdi = (DGridEkle.SelectedItem as tblMalzemeler).Adi,
                MalzemeId = (DGridEkle.SelectedItem as tblMalzemeler).Id,
                MalzemeKodu = (DGridEkle.SelectedItem as tblMalzemeler).Kodu,
                PersonelAdi = App.PersonelAdi,
                Miktar = Convert.ToDouble(TxtMalzemeMiktar.Text),
                PersonelId = App.PersonelId,
                OlusturmaTarihi = DateTime.Now,
            });
            DGridGonder.Items.Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbMalzemeGruplari.ItemsSource = new vMalzemeler().MalzemeGruplariGetir();
            LoadPage();
            if (SeciliMakina == null)
            {
                DGridMakina.ItemsSource = _Makina.MakinalariGetir();
                ChildMakina.Show();
            }
        }

        private void VardiyalariSec(List<string> vardiyalar)
        {
            for (int i = 0; i < vardiyalar.Count; i++)
            {
                if (vardiyalar[i] == checkBox1.Content.ToString()) checkBox1.IsChecked = true;
                else if (vardiyalar[i] == checkBox2.Content.ToString()) checkBox2.IsChecked = true;
                else if (vardiyalar[i] == checkBox3.Content.ToString()) checkBox3.IsChecked = true;
                else if (vardiyalar[i] == checkBox4.Content.ToString()) checkBox4.IsChecked = true;
            }
        }

        private void LoadPage()
        {
            if (BakimOnarimForm != null)
            {
                _ListMalzemeler = _Makina.BakimOnarimMalzemeleriGetir(BakimOnarimForm.Id);
                SeciliMakina = _Makina.MakinaGetir(BakimOnarimForm.MakinaId);

                _BakimOnarimTuru = (Makina.BakimOnarimTurleri)Enum.Parse(typeof(Makina.BakimOnarimTurleri), BakimOnarimForm.Turu);
                switch (_BakimOnarimTuru)
                {
                    case Makina.BakimOnarimTurleri.ArizaOnarim:
                        RadioButonArızaOnarım.IsChecked = true;
                        break;
                    case Makina.BakimOnarimTurleri.Diger:
                        RadioButonDiger.IsChecked = true;
                        break;
                    case Makina.BakimOnarimTurleri.PeriyodikBakim:
                        RadioButonPeriyodikBakım.IsChecked = true;
                        break;
                    default:
                        break;
                }
                TxtAciklama.Text = BakimOnarimForm.Aciklama;
                TxtHarcananSure.Text = BakimOnarimForm.HarcananSure.ToString();
                DatePickerBaslangic.SelectedDate = BakimOnarimForm.BasTarih;
                DatePickerBitis.SelectedDate = BakimOnarimForm.BitisTarih;
                VardiyalariSec(BakimOnarimForm.Vardiya.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            if (_ListMalzemeler == null) _ListMalzemeler = new List<vBakimOnarimAct>();
            DGridGonder.ItemsSource = _ListMalzemeler;
        }

        private void DGridEkle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGridEkle.SelectedItem == null) return;
            CmbMalzemeBirim.ItemsSource = new vMalzemeBirimleri().GetMalzemeBirimleri((DGridEkle.SelectedItem as tblMalzemeler).Id);
            CmbMalzemeBirim.SelectedIndex = 0;
        }

        private void RadioButonArızaOnarım_Checked(object sender, RoutedEventArgs e)
        {
            _BakimOnarimTuru = Makina.BakimOnarimTurleri.ArizaOnarim;
            bakimStringi = RadioButonArızaOnarım.Content.ToString();
        }

        string bakimStringi = "";
        private void RadioButonPeriyodikBakım_Checked(object sender, RoutedEventArgs e)
        {
            _BakimOnarimTuru = Makina.BakimOnarimTurleri.PeriyodikBakim;
            bakimStringi = RadioButonPeriyodikBakım.Content.ToString();
        }

        private void RadioButonDiger_Checked(object sender, RoutedEventArgs e)
        {
            _BakimOnarimTuru = Makina.BakimOnarimTurleri.Diger;
            bakimStringi = RadioButonDiger.Content.ToString();
        }

        private void DGridMakina_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridMakina.SelectedItem != null)
            {
                SeciliMakina = DGridMakina.SelectedItem as tblMakinalar;
                ChildMakina.Close();
            }
        }

        private void TxtSeciliMakina_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChildMakina.Show();
        }

        private void checkBox2_Checked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Add(checkBox2.Content.ToString());
        }

        private void checkBox3_Checked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Add(checkBox3.Content.ToString());
        }

        private void checkBox4_Checked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Add(checkBox4.Content.ToString());
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Add(checkBox1.Content.ToString());
        }

        private void checkBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Remove(checkBox2.Content.ToString());
        }

        private void checkBox4_Unchecked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Remove(checkBox4.Content.ToString());
        }

        private void checkBox3_Unchecked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Remove(checkBox3.Content.ToString());
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            SeciliVardiyalar.Remove(checkBox1.Content.ToString());
        }

        private void TextBoxAdi_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnMalzemeSil_Click(object sender, RoutedEventArgs e)
        {
            vBakimOnarimAct secilen = (sender as FrameworkElement).DataContext as vBakimOnarimAct;
            if (secilen == null) return;

            if (MessageBox.Show(secilen.Miktar + " " + secilen.BirimAdi + " " + secilen.MalzemeAdi + "\n\nSilinecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Makina.BakimOnarimMalzemeSil(secilen))
            {
                LoadPage();
                MessageBox.Show("Kayıt silindi..\n\nSilinen : " + secilen.Miktar + " " + secilen.BirimAdi + " " + secilen.MalzemeAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Silme sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

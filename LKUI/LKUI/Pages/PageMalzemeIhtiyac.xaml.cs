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
using LKUI.Classes;
using System.Windows.Markup;
using System.Globalization;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageMalzemeIhtiyac.xaml
    /// </summary>
    public partial class PageMalzemeIhtiyac : UserControl
    {
        public PageMalzemeIhtiyac()
        {
            InitializeComponent();
        }

        MalzemeTalep talep = new MalzemeTalep();

        public class Malzeme : vMalzemeler
        {
            public double Miktar { get; set; }
            public string Detay { get; set; }
            public int SecilenBirimId { get; set; }
            public List<vPersonelBolumleri> FabrikaBolumler { get; set; }
            public int SecilenBolumId { get; set; }
            public double MevcutStok { get; set; }
            public string GelecekStok { get; set; }
            public List<tblRenkler> Renkler { get; set; }
            public int? RenkId { get; set; }

            public Malzeme vMalzemelerToMalzemeler(vMalzemeler vMalz, List<vPersonelBolumleri> bolumler, List<tblRenkler> renkler)
            {
                Malzeme malz = new Malzeme()
                {
                    Id = vMalz.Id,
                    BaglantiId = vMalz.BaglantiId,
                    Miktar = 0,
                    Detay = "",
                    Kodu = vMalz.Kodu,
                    Adi = vMalz.Adi,
                    Birimleri = vMalz.Birimleri,
                    SecilenBirimId = vMalz.Birimleri.Count > 0 ? vMalz.Birimleri[0].Id : 0,
                    FabrikaBolumler = bolumler,
                    SecilenBolumId = bolumler.Count > 0 ? bolumler[0].Id : 0,
                    MevcutStok = vMalz.MevcutStok,
                    GelecekStok = vMalz.GelecekStok,
                    Renkler = vMalz.BaglantiId == 39 ? renkler : null,
                    RenkId = vMalz.BaglantiId == 39 && renkler.Count > 0 ? renkler[0].Id : 0
                };
                if (malz.RenkId == 0) malz.RenkId = null; //üstteki kod bloğunda 0 yerine direk null yazınca hata veriyor
                return malz;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblMalzemeler secilen = (GrupComboBox.SelectedItem as tblMalzemeler);
            pgDGridEkle.ItemIndex = 0;
            AdiTextBox.Text = "";
            _LstMalzemeler = new vMalzemeler().GruptakiMalzemeleriGetir(secilen.Id, birimlerGelsinMi: true);
            DGridEkle.ItemsSource = pgDGridEkle.GetPagedItemsSource<tblMalzemeler>(items: _LstMalzemeler);
            if (secilen.Id == 39) DClmRenk.Visibility = System.Windows.Visibility.Visible;
        }

        private void GonderButton_Click(object sender, RoutedEventArgs e)
        {
            if (DGridGonder.Items.Count == 0) return;
            List<tblTalepler> listTalep = new List<tblTalepler>();
            foreach (Malzeme m in DGridGonder.Items)
            {
                if (m.Miktar <= 0) continue;
                tblTalepler tblTalep = new tblTalepler()
                {
                    Detay = m.Detay,
                    MalzemeId = m.Id,
                    Miktar = Convert.ToDouble(m.Miktar.ToString("#.##")),
                    TalepEdenId = App.PersonelId,
                    BirimId = m.SecilenBirimId,
                    Tarih = DateTime.Now,
                    BolumId = m.SecilenBolumId,
                    RenkId = m.RenkId
                };
                listTalep.Add(tblTalep);
            }

            if (listTalep.Count == 0)
            {
                MessageBox.Show("Miktar 0'dan büyük olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (talep.TalepKaydet(listTalep))
            {
                MessageBox.Show("Talebiniz alındı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                DGridGonder.Items.Clear();
            }
            else MessageBox.Show("Kaydetme esnasında hata oluştu..!\n\nTalebiniz alınamadı.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GrupComboBox.ItemsSource = new vMalzemeler().MalzemeGruplariGetir();
            _ListBolumler = new vPersonelBolumleri().PersonelBolumleriGetir();
            _ListRenkler = vRenkler.RenkleriGetir();
        }

        private List<vPersonelBolumleri> _ListBolumler;
        private List<tblRenkler> _ListRenkler;

        List<tblMalzemeler> _LstMalzemeler;

        private void AdiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (GrupComboBox.SelectedItem == null) return;
            pgDGridEkle.ItemIndex = 0;
            DGridEkle.ItemsSource = pgDGridEkle.GetPagedItemsSource<tblMalzemeler>(_LstMalzemeler.FindAll(c => c.Adi.Contains(AdiTextBox.Text.ToUpper())));
        }

        private void BtnIptal_Click(object sender, RoutedEventArgs e)
        {
            DGridGonder.Items.Remove(DGridGonder.SelectedItem);
        }

        private void pgDGridEkle_Paged(object sender, RoutedEventArgs e)
        {
            DGridEkle.ItemsSource = pgDGridEkle.GetPagedItemsSource<tblMalzemeler>(_LstMalzemeler.FindAll(c => c.Adi.Contains(AdiTextBox.Text.ToUpper())));
        }

        private void DGridEkle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridEkle.SelectedItem == null) return;
            DGridGonder.Items.Add(new Malzeme().vMalzemelerToMalzemeler(vMalzemeler.tblMalzemelerTovMalzemeler(DGridEkle.SelectedItem as tblMalzemeler), _ListBolumler, _ListRenkler));
            DGridGonder.Items.Refresh();
        }
    }
}

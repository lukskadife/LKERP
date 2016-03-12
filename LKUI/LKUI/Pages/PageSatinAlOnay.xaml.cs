using System.Windows.Controls;
using System.Windows.Input;
using LKUI.Details;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System.Collections.Generic;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSatinAlOnay.xaml
    /// </summary>
    public partial class PageSatinAlOnay : UserControl
    {
        private tblDurumlar _Durum;
        public PageSatinAlOnay()
        {
            InitializeComponent();

            this._Durum = new tblDurumlar().DurumGetir(App.ClickedMenuItemId);
            TxtDurum.Text = this._Durum.DurumAdi;
        }
         
        MalzemeTalep _Talep = new MalzemeTalep();
        List<vTalepKarsilama> _ListTalepKarsilama;
        private void TaleplerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGridTalepKarsilama.SelectedItem == null) return;
            vTalepKarsilama karForm = DGridTalepKarsilama.SelectedItem as vTalepKarsilama;
            DtlTalepKarsilama detail = new DtlTalepKarsilama(karForm, _Talep.KarsilananMalzemeleriGetir(karForm.Id), karForm.DurumId == _Talep.TalepTamamId);
            detail.GrdTermin.Height = 0;
            if (karForm.DurumId == _Talep.TalepTamamId || karForm.DurumId == _Talep.TalepIptalId) detail.BtnOnayla.IsEnabled = false;

            detail.Onaylandi += (snd, ea) =>
            {
                DtlTalepKarsilama dtl = snd as DtlTalepKarsilama;
                if (karForm.DurumId == _Talep.TalepTamamId || karForm.DurumId == _Talep.TalepIptalId) return;
                _Talep.TalepDurumlariGuncelle(dtl.DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>, _Talep.TalepTamamId);
                _Talep.TalepFormDurumuGuncelle(karForm,  _Talep.YeniDurumGetir(karForm.DurumId));
                ChildSatinAlma.Close();
            };
            detail.IptalEdildi += (snd, ea) =>
            {
                DtlTalepKarsilama dtl = snd as DtlTalepKarsilama;
                if (karForm.DurumId == _Talep.TalepTamamId || karForm.DurumId == _Talep.TalepIptalId) return;
                _Talep.TalepDurumlariGuncelle(dtl.DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>, _Talep.TalepIptalId);
                _Talep.TalepFormDurumuGuncelle(karForm, _Talep.TalepIptalId);
                ChildSatinAlma.Close();
            };

            ChildSatinAlma.Caption = "No : " + karForm.No + "   -   Tedarikçi : " + karForm.TedarikciAdi;
            ChildSatinAlma.Content = detail;
            ChildSatinAlma.Show();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPage();
        }

        
        private void LoadPage()
        {
            _ListTalepKarsilama = _Talep.KarsilamaFormlariGetirWithDurum(this._Durum.Id);
 
            DGridTalepKarsilama.ItemsSource = _Talep.KarsilamaFormlariGetirWithDurum(this._Durum.Id);
        }

        private void ChildSatinAlma_Closed(object sender, System.EventArgs e)
        {
            LoadPage();
        }

        private void TxtTedarikciKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.TedarikciKodu.ToUpper().Contains(TxtTedarikciKodu.Text.ToUpper()));
            
        }

        private void TxtTedarikciAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.TedarikciAdi.ToUpper().Contains(TxtTedarikciAdi.Text.ToUpper()));
        }

        private void TxtTalepEdenKodu_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.PersonelKodu.Contains(TxtTalepEdenKodu.Text));
        }

        private void TxtTalepEdenAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridTalepKarsilama.ItemsSource = _ListTalepKarsilama.FindAll(c => c.PersonelAdi.ToUpper().Contains(TxtTalepEdenAdi.Text.ToUpper()));
        }
    }
}

using System.Collections.Generic;
using System.Windows.Controls;
using LKLibrary.DbClasses;
using System.Windows;
using System.Linq;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlTalepKarsilama.xaml
    /// </summary>
    public partial class DtlTalepKarsilama : UserControl
    {
        public DtlTalepKarsilama(vTalepKarsilama form, List<vTalepKarsilamaAct> listKarsilananlar, bool OnayliMi)
        {
            InitializeComponent();
            
            if (OnayliMi) BtnYazdir.IsEnabled = true;
            DpTerminTarihi.SelectedDate = form.TerminTarihi;
            TxtOdemeSekli.Text = form.OdemeSekli;
            DGridKarsilananlar.ItemsSource = listKarsilananlar;
            if (listKarsilananlar != null && listKarsilananlar.Count > 0 && listKarsilananlar[0].TalepKarsilamaId > 0)
                BtnTerminKaydet.Visibility = System.Windows.Visibility.Visible;
            else ClmnIptal.Visibility = System.Windows.Visibility.Hidden;

            if (form.DurumId == _Talep.TalepTamamId || form.DurumId == _Talep.TalepIptalId)
            {
                DpTerminTarihi.IsEnabled = false;
                TxtOdemeSekli.IsEnabled = false;
                BtnTerminKaydet.Visibility = System.Windows.Visibility.Hidden;
                ClmnIptal.Width = 0;
                ClmnIptal.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (form.DurumId == 0)
            {
                BtnIptal.Visibility = System.Windows.Visibility.Hidden;
                BtnOnayla.Content = "Kaydet";
                ClmnSil.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #region RoutedEvents

        public static readonly RoutedEvent OnayEvent = EventManager.RegisterRoutedEvent(
          "Onaylandi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlTalepKarsilama));

        public event RoutedEventHandler Onaylandi
        {
            add { AddHandler(OnayEvent, value); }
            remove { RemoveHandler(OnayEvent, value); }
        }

        public static readonly RoutedEvent IptalEvent = EventManager.RegisterRoutedEvent(
          "IptalEdildi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlTalepKarsilama));

        public event RoutedEventHandler IptalEdildi
        {
            add { AddHandler(IptalEvent, value); }
            remove { RemoveHandler(IptalEvent, value); }
        }

        public delegate void SatirSilindiEvent(vTalepKarsilamaAct satirdanSilinen);
        public event SatirSilindiEvent SatirSilindi;

        #endregion

        LKLibrary.Classes.MalzemeTalep _Talep = new LKLibrary.Classes.MalzemeTalep();

        private void BtnOnayla_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Onaylandı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            BtnYazdir.IsEnabled = true;
            RaiseEvent(new RoutedEventArgs(OnayEvent));
        }

        private void BtnYazdir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnIptal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("İptal Edildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            RaiseEvent(new RoutedEventArgs(IptalEvent));
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            List<vTalepKarsilamaAct> list = (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>);
            if (list != null || list.Count != 0)
            {
                int karsilamaId = list[0].TalepKarsilamaId;
                vTalepKarsilama form = _Talep.KarsilamaFormlariGetir(karsilamaId)[0];

                form.TerminTarihi = DpTerminTarihi.SelectedDate.HasValue ? DpTerminTarihi.SelectedDate.Value : System.DateTime.Now.Date;
                form.OdemeSekli = string.IsNullOrEmpty(TxtOdemeSekli.Text) ? "" : TxtOdemeSekli.Text;

                if (_Talep.KarsilamaGuncelle(form)) MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Hata oluştu..\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DGridKarsilananlar_ItemsSourceChanged(object sender, RoutedEventArgs e)
        {
            LblTutar.Content = (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>).Sum(c => c.FiyatTL * c.Miktar).ToString("#.00#") + " TL";
        }

        private void BtnUrunIptal_Click(object sender, RoutedEventArgs e)
        {
            vTalepKarsilamaAct secilen = (sender as FrameworkElement).DataContext as vTalepKarsilamaAct;
            if (secilen == null) return;

            if (MessageBox.Show(secilen.MalzemeAdi + " " + secilen.Miktar + " " + secilen.BirimAdi + "\n\nİptal edilecek..!", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (_Talep.KarsilamaMalzemeSil(secilen.Id))
            {
                (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>).Remove(secilen);
                DGridKarsilananlar.Items.Refresh();
                LblTutar.Content = (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>).Sum(c => c.FiyatTL * c.Miktar).ToString("#.00#") + " TL";
                MessageBox.Show(secilen.MalzemeAdi + " " + secilen.Miktar + " " + secilen.BirimAdi + "\n\nİptal edildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Hata oluştu..\n\nİptal edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DGridKarsilananlar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void BtnUrunSil_Click(object sender, RoutedEventArgs e)
        {
            vTalepKarsilamaAct secilen = DGridKarsilananlar.SelectedItem as vTalepKarsilamaAct;
            if (secilen== null) return;
            (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>).Remove(secilen);
            SatirSilindi(secilen);
            DGridKarsilananlar.Items.Refresh();
            LblTutar.Content = (DGridKarsilananlar.ItemsSource as List<vTalepKarsilamaAct>).Sum(c => c.FiyatTL * c.Miktar).ToString("#.00#") + " TL";
        }
    }
}

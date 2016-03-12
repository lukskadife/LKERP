using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System.Collections.Generic;
using System;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlMalzemeOrtFiyatlar.xaml
    /// </summary>
    public partial class DtlMalzemeOrtFiyatlar : UserControl
    {
        public vTalepMalzemeler _TalepEdilenMalzeme;

        #region RoutedEvents

        public static readonly RoutedEvent Firma_SelectEvent = EventManager.RegisterRoutedEvent(
          "FirmaSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlMalzemeOrtFiyatlar));

        public event RoutedEventHandler FirmaSelected
        {
            add { AddHandler(Firma_SelectEvent, value); }
            remove { RemoveHandler(Firma_SelectEvent, value); }
        }

        #endregion

        public DtlMalzemeOrtFiyatlar(vTalepMalzemeler talepMalzeme)
        {
            InitializeComponent();

            this._TalepEdilenMalzeme = talepMalzeme;
            DataGridOrtFiyatlar.ItemsSource = new MalzemeTalep().MalzemeFirmaFiyatlariGetir(this._TalepEdilenMalzeme.MalzemeId);
            TxtMiktar.Text = _TalepEdilenMalzeme.Miktar.ToString();
            ComboBirim.SelectedValue = talepMalzeme.BirimId;
            ComboDoviz.ItemsSource = new vAyarlar().DovizleriGetir();
        }

        public vMalzemeOrtFiyatlar Secilen;

        private void BtnFirmaKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBirim.SelectedValue == null)
            {
                MessageBox.Show("Birim seçilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.Secilen == null)
            {
                MessageBox.Show("Firma Seçilmedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TxtFiyat.Text.StringSayisalMi() && TxtMiktar.Text.StringSayisalMi()) RaiseEvent(new RoutedEventArgs(Firma_SelectEvent));
            else MessageBox.Show("Miktar ve Fiyat sayısal olmalıdır..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DataGridOrtFiyatlar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vMalzemeOrtFiyatlar secilen = DataGridOrtFiyatlar.SelectedItem as vMalzemeOrtFiyatlar;
            TxtSecilen.Text = secilen == null ? "" : secilen.TedarikciAdi;
            TxtFiyat.Text = secilen == null ? "" : secilen.OrtFiyat.ToString();
            this.Secilen = secilen;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBirim.ItemsSource = new vMalzemeBirimleri().GetMalzemeBirimleri(this._TalepEdilenMalzeme.MalzemeId);
        }

        private void ComboDoviz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vAyarlar secilen = (ComboDoviz.SelectedItem as vAyarlar);
            if (secilen == null) return;
            vKur kur = new vKur().DovizKuruGetir(secilen.Id);
            TxtKur.Text = kur == null ? "" : kur.LogoDovizSatis.ToString();

            if (secilen.Adi != "TL") TxtKur.Visibility = System.Windows.Visibility.Visible;
            else TxtKur.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}

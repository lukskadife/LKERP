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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlKumasTipBilgileri.xaml
    /// </summary>
    public partial class DtlKumasTipBilgileri : UserControl
    {
        enum Haslik { Acik, Orta, Koyu, None };

        public DtlKumasTipBilgileri()
        {
            InitializeComponent();
        }

        FuarKumas _KumasIslem = new FuarKumas();
        public vKumas _Kumas;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbDoviz.ItemsSource = new vAyarlar().DovizleriGetir();
        }

        public void LoadPage()
        {
            this.DataContext = _Kumas;
        }

        #region RoutedEvents

        public static readonly RoutedEvent KaydetEvent = EventManager.RegisterRoutedEvent(
          "Kaydedildi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlKumasTipBilgileri));

        public event RoutedEventHandler Kaydedildi
        {
            add { AddHandler(KaydetEvent, value); }
            remove { RemoveHandler(KaydetEvent, value); }
        }

        #endregion

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            vKumas kumas = this.DataContext as vKumas;
            if (!TxtKumasEni.TextGirisiDogruMu || !TxtGramajgm.TextGirisiDogruMu || !TxtGramajgm2.TextGirisiDogruMu || !TxtFiyat.TextGirisiDogruMu)
                return;

            try
            {
                if (_KumasIslem.TipKaydet(kumas))
                {
                    MessageBox.Show("Kaydedildi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                    RaiseEvent(new RoutedEventArgs(KaydetEvent));
                }
                else MessageBox.Show("Hata oluştu..!\n\nKaydedilemedi.", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

      
    }
}

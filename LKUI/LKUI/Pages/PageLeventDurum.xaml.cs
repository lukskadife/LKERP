using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using Telerik.Windows.Controls;
using LKUI.Classes;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageLevent.xaml
    /// </summary>
    public partial class PageLeventDurum : UserControl
    {
        public PageLeventDurum()
        {
            InitializeComponent();
        }

        int _DurumID;

        private void PreparePage(string durumTanim)
        {
            BtnCozgu.Width = 0;
            BtnDugum.Width = 0;
            BtnEkle.Width = 0;
            Btnİade.Width = 0;
            BtnTamamlandi.Width = 0;

            switch (durumTanim)
            {
                case "Bekleyen":
                    BtnEkle.Width = 50;
                    BtnCozgu.Width = 50;
                    TxtDurum.Text = "Bekleyen";
                    break;

                case "Cozgu":
                    BtnDugum.Width = 50;
                    TxtDurum.Text = "Çözgü";
                    break;

                case "Dugum":
                    BtnTamamlandi.Width = 50;
                    TxtDurum.Text = "Düğüm";
                    break;

                case "Tamam":
                    Btnİade.Width = 50;
                    TxtDurum.Text = "Tamam";
                    break;

                default:
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.ClickedMenuItemId != 0)
            {
                _DurumID = App.ClickedMenuItemId;
                tblDurumlar durum = new tblDurumlar().DurumGetir(_DurumID);
                PreparePage(durum.Tanim);
                

                foreach (tblDurumlar drm in tblDurumlar.DurumlariGetir(81))
                {
                    RadioButton radio = new RadioButton() { DataContext = drm, Content = drm.DurumAdi, IsChecked = false };
                    radio.Margin = new Thickness(0, 0, 10, 0);
                    if (drm.Id == _DurumID) radio.IsChecked = true;
                    radio.Click += new RoutedEventHandler(radio_Click);
                    StackRadio.Children.Add(radio);
                }

                LoadPage();
            }

            WndLevent = new RadWindow();
            Window main = Application.Current.MainWindow;

            WndLevent.Top = 30;
            WndLevent.Left = 5;
            WndLevent.Height = main.ActualHeight - 50;
            WndLevent.Width = main.ActualWidth - 30;
            WndLevent.Owner = main;
            WndLevent.Header = "Levent";
            WndLevent.WindowState = WindowState.Normal;
        }

        void radio_Click(object sender, RoutedEventArgs e)
        {
            tblDurumlar durum = (sender as RadioButton).DataContext as tblDurumlar;
            _DurumID = durum.Id;
            PreparePage(durum.Tanim);
            LoadPage();
        }

        RadWindow WndLevent;

        private void LoadPage()
        {
            DGridLeventler.ItemsSource = Levent.LeventleriGetir(durumId: _DurumID);
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            WndLevent.Content = new PageLevent(new vLeventHareket()
                {
                    Durum = 19,
                    BantSayisi = null,
                    BobinAdedi = null,
                    BobinMetre = null,
                    LeventEni = null,
                    LeventNo = null,
                    Metre = null,
                    ResteMetre = null,
                    TelAdedi = null,
                });

            WndLevent.Show();
        }

        private void DurumDegistir(string durumTanim)
        {
            if (DGridLeventler.SelectedItem == null) return;

            tblDurumlar yeniDurum = tblDurumlar.DurumGetir(durumTanim);
            if (yeniDurum == null) return;
            try
            {
                if (Levent.DurumDegistir(DGridLeventler.SelectedItem as vLeventHareket, yeniDurum.Id))
                    LoadPage();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            
        }

        private void BtnCozgu_Click(object sender, RoutedEventArgs e)
        {
            DurumDegistir("Cozgu");
        }

        private void BtnTamamlandi_Click(object sender, RoutedEventArgs e)
        {
            DurumDegistir("Tamam");
        }

        private void Btnİade_Click(object sender, RoutedEventArgs e)
        {
            //DurumDegistir("Iade");
            vLeventHareket secilen = DGridLeventler.SelectedItem as vLeventHareket;
            if (secilen == null) return;

            ChildLeventDuzelt.DataContext = Levent.IadeAl(secilen);
            ChildLeventDuzelt.Show();
        }

        private void BtnDugum_Click(object sender, RoutedEventArgs e)
        {
            DurumDegistir("Dugum");
        }

        private void BtnYenile_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void DGridLeventler_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vLeventHareket secilen = DGridLeventler.SelectedItem as vLeventHareket;
            if (secilen == null) return;

            WndLevent.Content = new PageLevent(secilen);
            WndLevent.Show();
        }

        private void menuItemDüzelt_Click(object sender, RoutedEventArgs e)
        {
            vLeventHareket secilen = DGridLeventler.SelectedItem as vLeventHareket;
            if (secilen == null) return;

            ChildLeventDuzelt.DataContext = secilen;
            ChildLeventDuzelt.Show();
        }

        private void BtnKaydetLeventDuzelt_Click(object sender, RoutedEventArgs e)
        {
            if (ChildLeventDuzelt.DataContext == null) return;

            if (new Levent(null).LeventKaydet(ChildLeventDuzelt.DataContext as vLeventHareket) == null)
            {
                MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if ((ChildLeventDuzelt.DataContext as vLeventHareket).IadeMi == false) LoadPage();
                ChildLeventDuzelt.DataContext = null;
                ChildLeventDuzelt.Close();
            }

        }

        private void ChildLeventDuzelt_Loaded(object sender, RoutedEventArgs e)
        {
            CmbTezgahLeventDuzelt.ItemsSource = new Makina().MakinalariGetir(1);
            CmbTipNoLeventDuzelt.ItemsSource = vKumas.KumaslariGetir();
            CmbDurum.ItemsSource = Levent.DurumlariGetir();
        }

        private void BtnVazgecLeventDuzelt_Click(object sender, RoutedEventArgs e)
        {
            ChildLeventDuzelt.Close();
        }

        private void MIExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridLeventler.ToExcel<vLeventHareket>();
        }
    }
}

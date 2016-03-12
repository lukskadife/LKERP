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
using System.Windows.Shapes;

namespace ETSevk
{
    /// <summary>
    /// Interaction logic for PageMesaj.xaml
    /// </summary>
    public partial class PageMesaj : Window
    {
        public enum MesajTip { Evet, Hayir, Tamam, None, Ok }
        public MesajTip Sonuc = MesajTip.None;

        public PageMesaj()
        {
            InitializeComponent();
        }

        private void BtnTamam_Click(object sender, RoutedEventArgs e)
        {
            Sonuc = MesajTip.Tamam;
            Close();
        }

        private void BtnEvet_Click(object sender, RoutedEventArgs e)
        {
            Sonuc = MesajTip.Evet;
            Close();
        }

        private void BtnHayir_Click(object sender, RoutedEventArgs e)
        {
            Sonuc = MesajTip.Hayir;
            Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 50;
            this.Left = 3;
        }

        public static MesajTip Show(string mesaj, MesajTip mesajBtn )
        {
            PageMesaj page = new PageMesaj();

            switch (mesajBtn)
            {
                case MesajTip.Evet:
                    page.BtnEvet.Visibility = System.Windows.Visibility.Visible;
                    page.BtnHayir.Visibility = System.Windows.Visibility.Visible;
                    page.BtnTamam.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnEvet.Focus();
                    break;

                case MesajTip.Hayir:
                    page.BtnEvet.Visibility = System.Windows.Visibility.Visible;
                    page.BtnHayir.Visibility = System.Windows.Visibility.Visible;
                    page.BtnTamam.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnHayir.Focus();
                    break;

                case MesajTip.Tamam:
                    page.BtnEvet.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnHayir.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnTamam.Visibility = System.Windows.Visibility.Visible;
                    page.BtnTamam.Focus();
                    break;

                case MesajTip.Ok:
                    page.BtnEvet.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnHayir.Visibility = System.Windows.Visibility.Hidden;
                    page.BtnTamam.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            try
            {
                (new System.Media.SoundPlayer(Environment.CurrentDirectory + "\\Windows Ding.wav")).Play();
            }
            catch (Exception exc) { }
            page.TxtMsj.Text = mesaj;
            page.ShowDialog();
            return page.Sonuc;
        }
    }
}

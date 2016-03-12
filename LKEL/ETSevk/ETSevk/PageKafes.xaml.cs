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
using ETSevk.Classes;

namespace ETSevk
{
    /// <summary>
    /// Interaction logic for PageKafes.xaml
    /// </summary>
    public partial class PageKafes : Window
    {
        public PageKafes()
        {
            InitializeComponent();
        }

        Kafes kafes = new Kafes();

        private void btnKafesBarkodDlt_Click(object sender, RoutedEventArgs e)
        {
            txtKafesBarkod.Clear();
            txtHamBarkod.Clear();
            txtKafesBarkod.Focus();
        }

        private void btnHamBarkodDlt_Click(object sender, RoutedEventArgs e)
        {
            txtHamBarkod.Clear();
            txtHamBarkod.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtKafesBarkod.Focus();
        }

        private void txtKafesBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtKafesBarkod.Text != null && txtKafesBarkod.Text.Trim().ToString() != "")
                {

                    if (kafes.KafesVarMi(txtKafesBarkod.Text.Trim().ToString())) txtHamBarkod.Focus();

                    else
                    {
                        PageMesaj.Show("Kafes Barkodu bulunamadı!...", PageMesaj.MesajTip.Ok);
                        txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
                    }

                }

                else txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void txtHamBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtHamBarkod.Text != null && txtHamBarkod.Text.Trim().ToString() != "")
                {

                    if (kafes.SevkEdilmemisBarkodVarMi(txtHamBarkod.Text.Trim().ToString()))
                    {
                        if (!kafes.KafesVarMi(txtKafesBarkod.Text.Trim().ToString()))
                        {
                            txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
                            PageMesaj.Show("Kafes Barkodu bulunamadı!...", PageMesaj.MesajTip.Ok);
                            //txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
                            return;
                        }

                        if (cmbKafesDikeyKodu.Text.Length == 0)
                        {
                            PageMesaj.Show("Kafes sırası seçin!", PageMesaj.MesajTip.Ok);
                            return;
                        }

                        kafes.KafesAta(txtKafesBarkod.Text.Trim().ToString(), txtHamBarkod.Text.Trim().ToString(), cmbKafesDikeyKodu.Text);
                        txtHamBarkod.Clear();
                        txtHamBarkod.Focus();
                    }

                    else
                    {
                        PageMesaj.Show("Ham Barkod bulunamadı!...", PageMesaj.MesajTip.Ok);
                        txtHamBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
                    }

                }
                else txtHamBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else txtHamBarkod.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }
    }
}

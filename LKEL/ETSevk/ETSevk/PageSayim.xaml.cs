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
    /// Interaction logic for PageSayim.xaml
    /// </summary>
    public partial class PageSayim : Window
    {
        private ETSevk.Classes.Sayim.SayimTipi _SayimBarkodTipi;
        public PageSayim(ETSevk.Classes.Sayim.SayimTipi sayimBarkodTipi)
        {
            InitializeComponent();
            _SayimBarkodTipi = sayimBarkodTipi;
            if (_SayimBarkodTipi == Sayim.SayimTipi.Ham)
            {
                RowRenk.Height = new GridLength(0);                
                this.Title = "Ham Sayım";
            }
            else
            {
                RowKafes.Height = new GridLength(0);
                this.Title = "Mamul Sayım";
                txtKafesBarkod.Visibility = System.Windows.Visibility.Hidden;
                btnKafesBarkodDlt.Visibility = System.Windows.Visibility.Hidden;
                cmbKafesDikeyKodu.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        Sayim _SayimIslem;
        Kafes kafes = new Kafes();

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }

        private void TxtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_SayimIslem == null)
                {
                    PageMesaj.Show("Hata oluştu.\nBarkod okutulamadı..!", PageMesaj.MesajTip.Tamam);
                    return;
                }


                try
                {
                    if (_SayimBarkodTipi == Sayim.SayimTipi.Ham)
                    {
                        if (kafes.KafesVarMi(txtKafesBarkod.Text.Trim().ToString())) TxtBarkod.Focus();

                        else
                        {
                            PageMesaj.Show("Kafes Barkodu bulunamadı!", PageMesaj.MesajTip.Ok);
                            txtKafesBarkod.BorderBrush = new SolidColorBrush(Colors.Red);
                            TxtBarkod.Clear();
                            txtKafesBarkod.Clear();
                            txtKafesBarkod.Focus();

                            return;
                        }

                        if (cmbKafesDikeyKodu.Text.Length == 0)
                        {
                            PageMesaj.Show("Kafes sırası seçin!", PageMesaj.MesajTip.Ok);
                            return;
                        }

                        if (_SayimIslem.BarkodOkut(TxtBarkod.Text,kafes.KafesIdGetir(txtKafesBarkod.Text),cmbKafesDikeyKodu.Text) == false)
                        {
                            PageMesaj.Show("Barkod okutulamadı..!", PageMesaj.MesajTip.Tamam);
                            return;
                        }  
                    }
                    else
                    {
                        if (_SayimIslem.BarkodOkut(TxtBarkod.Text) == false)
                        {
                            PageMesaj.Show("Barkod okutulamadı..!", PageMesaj.MesajTip.Tamam);
                            return;
                        }                        
                    }

                    GrdOkutulan.DataContext = _SayimIslem.SonOkutulan;
                    TxtAdet.Text = _SayimIslem.OkutulanCount.ToString("###,##0") + " adet";
                    TxtMetre.Text = _SayimIslem.OkutulanToplamMetre.ToString("###,##0.00") + " metre";
                    TxtBarkod.Text = "";
                    TxtBarkod.Focus();
                }
                catch (Exception exp)
                {
                    PageMesaj.Show(exp.Message, PageMesaj.MesajTip.Tamam);
                    TxtBarkod.Text = "";
                    return;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (PageMesaj.Show("Önceki sayım silinecek..?", PageMesaj.MesajTip.Hayir) == PageMesaj.MesajTip.Evet)
                _SayimIslem = new Sayim(_SayimBarkodTipi, true);
            else _SayimIslem = new Sayim(_SayimBarkodTipi, false);
            TxtAdet.Text = _SayimIslem.OkutulanCount.ToString("###,##0") + " adet";
            TxtMetre.Text = _SayimIslem.OkutulanToplamMetre.ToString("###,##0.00") + " metre";
            TxtBarkod.Focus();
        }

        private void btnKafesBarkodDlt_Click(object sender, RoutedEventArgs e)
        {
            txtKafesBarkod.Clear();            
            txtKafesBarkod.Focus();
        }

        private void txtKafesBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtKafesBarkod.Text != null && txtKafesBarkod.Text.Trim().ToString() != "")
                {

                    if (kafes.KafesVarMi(txtKafesBarkod.Text.Trim().ToString())) TxtBarkod.Focus();

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
    }
}

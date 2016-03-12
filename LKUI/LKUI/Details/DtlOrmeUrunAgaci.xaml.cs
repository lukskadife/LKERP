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
using LKLibrary.DbClasses;
using LKLibrary.Classes;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlOrmeUrunAgaci.xaml
    /// </summary>
    public partial class DtlOrmeUrunAgaci : UserControl
    {
        vKumas _kumas = new vKumas();
        List<tblMalzemeler> Iplikler { get; set; }
        public DtlOrmeUrunAgaci(vKumas kumas)
        {
            InitializeComponent();
            this.DataContext = kumas;
            _kumas = kumas;
        }

        #region RoutedEvents

        public delegate void KaydedildiEvent();
        public event KaydedildiEvent Kaydedildi;

        public delegate void VazgecildiEvent();
        public event VazgecildiEvent Vazgecildi;

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Iplikler = Iplik.IplikleriGetir();

            SelAtki1.ItemsSource = Iplikler;
            SelAtki2.ItemsSource = Iplikler;
            SelAtki3.ItemsSource = Iplikler;
            SelAtki4.ItemsSource = Iplikler;
            SelHav1.ItemsSource = Iplikler;
            SelHav2.ItemsSource = Iplikler;
            SelHav3.ItemsSource = Iplikler;
            SelHav4.ItemsSource = Iplikler;            
            SelZemin1.ItemsSource = Iplikler;
            SelZemin2.ItemsSource = Iplikler;
            SelZemin3.ItemsSource = Iplikler;
            SelZemin4.ItemsSource = Iplikler;
            CmbKumasCinsi.ItemsSource = tblAyarlar.KumasCinsleriniGetir().Where(c=>c.Id == 396);
            degerleriYukle();
        }

        //Gökhan 25.06.2014
        private void degerleriYukle()
        {
            if (_kumas.Atki1 != null) SelAtki1.SelectedValue = _kumas.Atki1;
            if (_kumas.Atki2 != null) SelAtki2.SelectedValue = _kumas.Atki2;
            if (_kumas.Atki3 != null) SelAtki3.SelectedValue = _kumas.Atki3;
            if (_kumas.Atki4 != null) SelAtki4.SelectedValue = _kumas.Atki4;
            if (_kumas.Hav1 != null) SelHav1.SelectedValue = _kumas.Hav1;
            if (_kumas.Hav2 != null) SelHav2.SelectedValue = _kumas.Hav2;
            if (_kumas.Hav3 != null) SelHav3.SelectedValue = _kumas.Hav3;
            if (_kumas.Hav4 != null) SelHav4.SelectedValue = _kumas.Hav4;
            if (_kumas.Zemin1 != null) SelZemin1.SelectedValue = _kumas.Zemin1;
            if (_kumas.Zemin2 != null) SelZemin2.SelectedValue = _kumas.Zemin2;
            if (_kumas.Zemin3 != null) SelZemin3.SelectedValue = _kumas.Zemin3;
            if (_kumas.Zemin4 != null) SelZemin4.SelectedValue = _kumas.Zemin4;            
            if (_kumas.KumasCinsi != null) CmbKumasCinsi.SelectedValue = _kumas.KumasCinsi;

        }

        //Gökhan 25.06.2014
        private void degerleriKaydet()
        {
            _kumas = this.DataContext as vKumas;

            if (SelAtki1.Text != null) _kumas.Atki1 = Convert.ToInt32(SelAtki1.SelectedValue);
            else _kumas.Atki1 = null;

            if (SelAtki2.Text != null) _kumas.Atki2 = Convert.ToInt32(SelAtki2.SelectedValue);
            else _kumas.Atki2 = null;

            if (SelAtki3.Text != null) _kumas.Atki3 = Convert.ToInt32(SelAtki3.SelectedValue);
            else _kumas.Atki3 = null;

            if (SelAtki4.Text != null) _kumas.Atki4 = Convert.ToInt32(SelAtki4.SelectedValue);
            else _kumas.Atki4 = null;

            if (SelHav1.Text != null) _kumas.Hav1 = Convert.ToInt32(SelHav1.SelectedValue);
            else _kumas.Hav1 = null;

            if (SelHav2.Text != null) _kumas.Hav2 = Convert.ToInt32(SelHav2.SelectedValue);
            else _kumas.Hav2 = null;

            if (SelHav3.Text != null) _kumas.Hav3 = Convert.ToInt32(SelHav3.SelectedValue);
            else _kumas.Hav3 = null;

            if (SelHav4.Text != null) _kumas.Hav4 = Convert.ToInt32(SelHav4.SelectedValue);
            else _kumas.Hav4 = null;

            if (SelZemin1.Text != null) _kumas.Zemin1 = Convert.ToInt32(SelZemin1.SelectedValue);
            else _kumas.Zemin1 = null;

            if (SelZemin2.Text != null) _kumas.Zemin2 = Convert.ToInt32(SelZemin2.SelectedValue);
            else _kumas.Zemin2 = null;

            if (SelZemin3.Text != null) _kumas.Zemin3 = Convert.ToInt32(SelZemin3.SelectedValue);
            else _kumas.Zemin3 = null;

            if (SelZemin4.Text != null) _kumas.Zemin4 = Convert.ToInt32(SelZemin4.SelectedValue);
            else _kumas.Zemin4 = null;           

            if (CmbKumasCinsi.Text != null) _kumas.KumasCinsi = Convert.ToInt32(CmbKumasCinsi.SelectedValue);
            else _kumas.KumasCinsi = 0;
           

            this.DataContext = _kumas;
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            _kumas = this.DataContext as vKumas;

            if (_kumas.TipNo.ToString() == null | _kumas.TipNo.ToString() == "")
            {

                MessageBox.Show("Tip No boş olamaz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CmbKumasCinsi.Text == null)
            {
                MessageBox.Show("Kumaş cinsi boş olamaz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            degerleriKaydet();
            try
            {
                if (new FuarKumas().TipKaydet(this.DataContext as vKumas) == false)
                {
                    MessageBox.Show("Hata oluştu.\n\nKaydedilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else Kaydedildi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            Vazgecildi();
        }
    }
}

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
    /// Interaction logic for DtlEnerjiAylıkFiyat.xaml
    /// </summary>
    public partial class DtlEnerjiAylıkFiyat : UserControl
    {
        public DtlEnerjiAylıkFiyat()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                labelElektrkKwh.Visibility = Visibility.Visible;
                labelSuTon.Visibility = Visibility.Hidden;
                labelDGazKwh.Visibility = Visibility.Hidden;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                labelElektrkKwh.Visibility = Visibility.Hidden;
                labelSuTon.Visibility = Visibility.Visible;
                labelDGazKwh.Visibility = Visibility.Hidden;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                labelElektrkKwh.Visibility = Visibility.Hidden;
                labelSuTon.Visibility = Visibility.Hidden;
                labelDGazKwh.Visibility = Visibility.Visible;
            }

            TxtEnerji.Text = _Sayac.SayacBirimFiyatGetir((comboBox1.SelectedItem as tblSayaclar).Id).ToString();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (TxtEnerji.TextGirisiDogruMu == false) return;
            tblSayacBirimFiyatlari birimFiyat = new tblSayacBirimFiyatlari()
            {
                Ay = DateTime.Now.Month,
                Fiyat = Convert.ToDouble( string.IsNullOrEmpty(TxtEnerji.Text) ? "0" : TxtEnerji.Text),
                OlusturanPersonelId = App.PersonelId,
                OlusturmaTarihi = DateTime.Now,
                SayacId = (comboBox1.SelectedItem as tblSayaclar).Id,
                Yil = DateTime.Now.Year
            };

            if (_Sayac.SayacBirimFiyatKaydet(birimFiyat))
                MessageBox.Show("Kaydedildi");
            else
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        Sayac _Sayac = new Sayac();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox1.ItemsSource = _Sayac.SayacTanimlariGetir();
        }
    }
}

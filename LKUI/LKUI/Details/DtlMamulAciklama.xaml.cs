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
    /// Interaction logic for DtlMamulAciklama.xaml
    /// </summary>
    public partial class DtlMamulAciklama : UserControl
    {
        public DtlMamulAciklama()
        {
            InitializeComponent();
        }

        tblMamulKumaslar _Mamul = null;

        private void TxtBarkod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _Mamul = Mamul.AciklamaBarkodOkut(TxtBarkod.Text);
                if (_Mamul == null)
                {
                    MessageBox.Show("Barkod bulunamadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    TxtAciklama.Text = "";
                    return;
                }

                else TxtAciklama.Text = string.IsNullOrEmpty(_Mamul.Aciklama) ? "" : _Mamul.Aciklama;
                TxtBarkod.SelectAll();
                TxtBarkod.Focus();
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            TxtBarkod.SelectAll();
            TxtBarkod.Focus();
            if (_Mamul == null) return;

            string tmp = _Mamul.Aciklama;
            _Mamul = Mamul.AciklamaEkle(_Mamul, TxtAciklamaYaz.Text);
            if (tmp != _Mamul.Aciklama) TxtAciklamaYaz.Text = "";
            TxtAciklama.Text = _Mamul.Aciklama;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBarkod.Focus();        
        }
    }
}

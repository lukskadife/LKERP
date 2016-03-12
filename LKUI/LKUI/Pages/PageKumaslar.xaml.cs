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
using LKUI.Details;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageKumaslar.xaml
    /// </summary>
    public partial class PageKumaslar : UserControl
    {
        public PageKumaslar()
        {
            InitializeComponent();
        }

        List<vKumas> LstKumas;
        FuarKumas _KumasIslem = new FuarKumas();

        void LoadPage()
        {
            LstKumas = _KumasIslem.KumaslariGetir();
            LstKumas.ForEach(c => c.Dovizler = _Dovizler);

            DGridKumaslar.ItemsSource = LstKumas;
        }

        List<vAyarlar> _Dovizler;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _Dovizler = new vAyarlar().DovizleriGetir();
            LoadPage();
        }

        private void Duzelt()
        {
            if (DGridKumaslar.SelectedItem == null) return;
            DtlKumasBilgi._Kumas = DGridKumaslar.SelectedItem as vKumas;
            DtlKumasBilgi.LoadPage();
            ChildBilgi.Show();
        }

        private void BtnDüzelt_Click(object sender, RoutedEventArgs e)
        {
            Duzelt();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            DtlKumasBilgi._Kumas = new vKumas()
            {
                TipNo = "",
                TipAdi = "",
                Varyant = "",
                DovizId = 32,
                Yik1 = false,
                Yik2 = false,
                Yik3 = false,
                Yik4 = false,
                Yik5 = false,
                Dosemelik = false,
                Perdelik = false,
                Elbiselik = false,
                Likrali = false
            };
            DtlKumasBilgi.LoadPage();
            ChildBilgi.Show();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridKumaslar.SelectedItem == null) return;

            vKumas silinecek = DGridKumaslar.SelectedItem as vKumas;

            if (MessageBox.Show("Silinecek..?\n\nTip No : " + silinecek.TipNo + "\nTip Adı :" + silinecek.TipAdi, App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)
                == MessageBoxResult.No)
                return;

            if (_KumasIslem.KumasSil(silinecek))
            {
                MessageBox.Show("Kayıt Silindi..!\n\nTip No : " + silinecek.TipNo + "\nTip Adı :" + silinecek.TipAdi, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPage();
            }
            else MessageBox.Show("Hata oluştu.\n\nSilinemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DtlKumasBilgi_Kaydedildi(object sender, RoutedEventArgs e)
        {
            LoadPage();
            ChildBilgi.Close();            
        }

        private void DGridKumaslar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Duzelt();
            //DtlDokumaUrunAgaci DtlAgac = new DtlDokumaUrunAgaci() { DataContext = DGridKumaslar.SelectedItem };
            //ChildDokumaBilgileri.Content = DtlAgac;
            //ChildDokumaBilgileri.Show();
        }

        private void TxtNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridKumaslar.ItemsSource = LstKumas.FindAll(c => c.TipNo.ToUpper().Contains(TxtNo.Text.ToUpper()) && c.TipAdi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }

        private void TxtAdi_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridKumaslar.ItemsSource = LstKumas.FindAll(c => c.TipNo.ToUpper().Contains(TxtNo.Text.ToUpper()) && c.TipAdi.ToUpper().Contains(TxtAdi.Text.ToUpper()));
        }
    }
}

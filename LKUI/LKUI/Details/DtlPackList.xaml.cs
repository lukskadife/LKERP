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
    /// Interaction logic for DtlPackList.xaml
    /// </summary>
    public partial class DtlPackList : UserControl
    {
        public DtlPackList(int sevkId)
        {
            InitializeComponent();
            _Islem = new PackList(sevkId);
        }

        PackList _Islem;
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridPaket.ItemsSource = _Islem.Paketler;
            DGridBarkod.ItemsSource = _Islem.Barkodlar;
        }

        private void BtnPaketKgDagit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Islem.PaketAgirliklariDagit();
                DGridBarkod.ItemsSource = null;
                DGridBarkod.ItemsSource = _Islem.Barkodlar;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnPackYazdir_Click(object sender, RoutedEventArgs e)
        {
            DtlRapor raporlama = new DtlRapor();
            List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>(){
                new DtlRapor.RaporItem("DSPackList", _Islem.Barkodlar),
                new DtlRapor.RaporItem("DSSiparis", new List<vSiparisler>() { _Islem.SiparisBelge})
            };
            raporlama.RaporGoster("RprPackingList", new List<DtlRapor.RaporItem>(list));

            ChildGenel.Content = raporlama;
            ChildGenel.Show();

            if (_Islem.Kaydet() == false) MessageBox.Show("Alias'lar kaydedilemedi..!");
        }

        private void BtnTekEtiketYazdir_Click(object sender, RoutedEventArgs e)
        {
            vPackList secilen = (sender as FrameworkElement).DataContext as vPackList;
            if (secilen == null) return;

            DtlRapor raporlama = new DtlRapor();
            
            List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                    {
                        new DtlRapor.RaporItem("DSKutuEtiket", new List<vPackList>(){ secilen })
                    };

            raporlama.RaporYazdir("RprSevkEtiket", new List<DtlRapor.RaporItem>(list));
        }

        private void BtnTumEtiketleriYazdir_Click(object sender, RoutedEventArgs e)
        {
            if (_Islem.Barkodlar == null || _Islem.Barkodlar.Count == 0) return;

            foreach (vPackList item in _Islem.Barkodlar)
            {
                DtlRapor raporlama = new DtlRapor();
                List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
                    {
                        new DtlRapor.RaporItem("DSKutuEtiket", new List<vPackList>(){ item })
                    };

                raporlama.RaporYazdir("RprSevkEtiket", list);
            }
        }

        private void ChildGenel_Closed(object sender, EventArgs e)
        {
            ChildGenel.DataContext = null;
            ChildGenel.Caption = "";
        }

        
    }
}

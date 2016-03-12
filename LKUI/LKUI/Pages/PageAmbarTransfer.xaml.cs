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
    /// Interaction logic for PageAmbarTransfer.xaml
    /// </summary>
    public partial class PageAmbarTransfer : UserControl
    {
        public PageAmbarTransfer()
        {
            InitializeComponent();
        }

        Transfer _transfer { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DPBaslangic.SelectedDate = DateTime.Today;
            DPBitis.SelectedDate = DateTime.Today;
        }

        private void LoadPage()
        {
            if (DPBaslangic.SelectedDate == null || DPBitis.SelectedDate == null) return;

            DGridPartileme.ItemsSource = Transfer.AmbarUstBelgeleriGetir(DPBaslangic.SelectedDate.Value, DPBitis.SelectedDate.Value);            
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            // Otomatik lüks kadife ambar transfer fişi oluşturacak.
            Transfer.AmbarUstFisiOlustur();
            LoadPage();
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            if (_transfer == null)
            {
                MessageBox.Show("Ambar fişi seçiniz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);                
                return;
            }

            if (Transfer.UstBelgeSilinebilirMi(_transfer.Ambar.Id)) throw new Exception("Transfer edilmiş kumaşlar var. Silinemez!");

            Transfer.AmbarUstFisiSil(_transfer.Ambar.Id);
            LoadPage();
        }

        private void DPBaslangic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void DPBitis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void BtnBarkodEkle_Click(object sender, RoutedEventArgs e)
        {
            if (_transfer != null)
            {
                DGridMuadilHamKumaslar.ItemsSource = null;               
                DGridMuadilHamKumaslar.ItemsSource = Transfer.TransferEdilecekHamKumaslariGetir();
                List<vHamKumaslarOrmeStok> muadiller = new List<vHamKumaslarOrmeStok>();
                muadiller = (List<vHamKumaslarOrmeStok>)DGridMuadilHamKumaslar.ItemsSource;


                if (muadiller.Count == 0) MessageBox.Show("Transfer edilecek örme kumaş yok!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                else ChildMuadilHamKumaslar.Show();
            }
            else
                MessageBox.Show("Önce ambar fişi seçiniz!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TxtBarkot_KeyDown(object sender, KeyEventArgs e)
        {
            if (TxtBarkot.Text.Length != 10) return;           

            try
            {
                if (_transfer == null)
                {
                    MessageBox.Show("Ambar fişi seçili değil..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    TxtBarkot.Text = "";
                    return;
                }

                Transfer.HamBarkoduEkle(TxtBarkot.Text, _transfer.Ambar.Id);
                DGridPlanlar.ItemsSource = Transfer.DepoyaAlinanlariGetir(_transfer.Ambar.Id);            
                TxtBarkot.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DGridPartileme_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (DGridPartileme.SelectedItem == null) return;

            _transfer = new Transfer(DGridPartileme.SelectedItem as vAmbarUst);

            DGridBarkodlar.ItemsSource = Transfer.TransferEdilenleriGetir(_transfer.Ambar.Id);
            DGridPlanlar.ItemsSource = Transfer.DepoyaAlinanlariGetir(_transfer.Ambar.Id);
        }      

        private void BtnTransferSil_Click(object sender, RoutedEventArgs e)
        {
            if (DGridBarkodlar.SelectedItem as vAmbarAct == null) return;            
            int hamId = (DGridBarkodlar.SelectedItem as vAmbarAct).HamBarkodId;            
            Transfer.BarkodSilinebilirMi(hamId);
            DGridBarkodlar.ItemsSource = Transfer.TransferEdilenleriGetir(_transfer.Ambar.Id);

        }

        private void MItemTransferSarfFisiAktar_Click(object sender, RoutedEventArgs e)
        {
            if (_transfer == null) return;

            Logo lg = new Logo();
            lg.MalzemeFisiAktar(_transfer.Ambar.Id, 1);
            MessageBox.Show("Transfer fişi aktarıldı. Lütfen kontrol ediniz!");
        }

        private void ChildMuadilHamKumaslar_Closed(object sender, EventArgs e)
        {
            GridFilitreTemizle();
        }

        private void BtnOnay_Click(object sender, RoutedEventArgs e)
        {
            if (DGridMuadilHamKumaslar.ItemsSource == null) return;
            List<vHamKumaslarOrmeStok> secilenler = DGridMuadilHamKumaslar.SelectedItems.Cast<vHamKumaslarOrmeStok>().ToList();            
            if (secilenler.Count == 0) return;
            try
            {
                if (Transfer.TransferEt(secilenler, _transfer.Ambar.Id))
                {
                    DGridBarkodlar.ItemsSource = null;
                    DGridBarkodlar.ItemsSource = Transfer.TransferEdilenleriGetir(_transfer.Ambar.Id);
                    ChildMuadilHamKumaslar.Close();
                }
                else MessageBox.Show("Hata oluştu.\n\nTransfer edilemedi.!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridFilitreTemizle()
        {
            DGridMuadilHamKumaslar.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in DGridMuadilHamKumaslar.Columns)
            {
                column.ClearFilters();
            }
            DGridMuadilHamKumaslar.FilterDescriptors.ResumeNotifications();
        }
    }
}

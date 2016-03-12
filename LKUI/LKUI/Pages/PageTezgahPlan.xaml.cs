using System;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;
using System.Collections.Generic;
using System.Windows.Media;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageTezgahPlan.xaml
    /// </summary>
    public partial class PageTezgahPlan : UserControl
    {
        public PageTezgahPlan()
        {
            InitializeComponent();
        }

        Planlama _Plan;

        void LoadPage()
        {
            if (CmbAy.SelectedIndex < 0 || string.IsNullOrEmpty((CmbYil.SelectedValue as ComboBoxItem).Content.ToString()))
                return;
            int ay = (CmbAy.SelectedIndex + 1);
            int yil = Convert.ToInt32((CmbYil.SelectedValue as ComboBoxItem).Content);

            _Plan = new Planlama(ay, yil);

            foreach (DataGridColumn item in DGridTezgahlar.Columns)
            {
                try
                {                    
                    int ind = Convert.ToInt32(item.SortMemberPath.Replace("Tarih", ""));
                    new DateTime(yil, ay, ind);
                    item.Header = ind.ToString("00") + "." + ay.ToString("00");
                    if (item.Visibility == System.Windows.Visibility.Hidden) item.Visibility = System.Windows.Visibility.Visible;
                }
                catch (Exception e)
                {
                    string str = e.Message;
                    if (item.SortMemberPath.Contains("Tarih")) item.Visibility = System.Windows.Visibility.Hidden;
                }
            }

            DGridTezgahlar.ItemsSource = _Plan.ListTezgahPlanlari;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbYil.Text = DateTime.Now.Year.ToString();
            CmbAy.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void DGridTezgahlar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DGridTezgahlar.SelectedCells.Count == 0) return;
            vTezgahPlanlama secilen = DGridTezgahlar.CurrentItem as vTezgahPlanlama;
            if (secilen == null) return;
            DateTime? date = HucreTarihiGetir(DGridTezgahlar.SelectedCells[0]);
            if (date.HasValue == false) return;
            vPlanlama plan = _Plan.TezgahPlanDetayGetir(secilen.TezgahId, date.Value);
            GrdOzet.DataContext = plan;
        }

        DateTime? HucreTarihiGetir(DataGridCellInfo cell)
        {
            try
            {
                return
                    new DateTime(_Plan.PlanYili, _Plan.PlanAyi, Convert.ToInt32(cell.Column.SortMemberPath.Replace("Tarih", "")));
            }
            catch
            {
                return null;
            }
        }

        private void CmbYil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void CmbAy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPage();
        }

        private void menuItemKes_Click(object sender, RoutedEventArgs e)
        {
            List<vTezgahPlanlama> list = DGridTezgahlar.ItemsSource as List<vTezgahPlanlama>;
            _KesYapistir = new List<int>();

            foreach (DataGridCellInfo cell in DGridTezgahlar.SelectedCells)
            {
                if (list.IndexOf(cell.Item as vTezgahPlanlama) != list.IndexOf(DGridTezgahlar.SelectedCells[0].Item as vTezgahPlanlama))
                {
                    MessageBox.Show("Farklı satırdaki hücreler seçilemez..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                DateTime? tarih = HucreTarihiGetir(cell);
                if (tarih.HasValue == false) continue;

                _KesYapistir.Add(_Plan.TezgahPlanDetayGetir((cell.Item as vTezgahPlanlama).TezgahId, tarih.Value).Id);
            }
        }

        private void menuItemYapistir_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> tasinacakGunler = new List<DateTime>();
            foreach (DataGridCellInfo item in DGridTezgahlar.SelectedCells)
            {
                DateTime? tarih = HucreTarihiGetir(item);
                if (tarih.HasValue) tasinacakGunler.Add(tarih.Value);
            }
            _Plan.TezgahPlanTasi((DGridTezgahlar.SelectedCells[0].Item as vTezgahPlanlama).TezgahId, tasinacakGunler, _KesYapistir);
            PlaniYukle();

            _KesYapistir = null;
        }

        List<int> _KesYapistir;

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_KesYapistir == null)
            {
                menuItemYapistir.Foreground = Brushes.Silver;
                menuItemYapistir.IsEnabled = false;
            }
            else
            {
                menuItemYapistir.Foreground = Brushes.Black;
                menuItemYapistir.IsEnabled = true;
            }
        }

        

        void PlaniYukle()
        {
            _Plan = new Planlama(_Plan.PlanAyi, _Plan.PlanYili);
            DGridTezgahlar.ItemsSource = _Plan.ListTezgahPlanlari;
        }

        void Otele(int gun)
        {
            //if (DGridTezgahlar.SelectedCells.Count == 0 || DGridTezgahlar.SelectedCells.Count > 1) return;
            //if (_SelectedTarih.HasValue == false) return;

            //if (_Plan.TezgahPlanOtele(_SelectedPivotPlan.TezgahId, _SelectedTarih.Value, gun)) LoadTezgahPlan();
        }

        private void BtnOtele_Click(object sender, RoutedEventArgs e)
        {
            Otele(1);
        }

        private void BtnGeriCek_Click(object sender, RoutedEventArgs e)
        {
            Otele(-1);
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            DataGridCellInfo cell = DGridTezgahlar.SelectedCells[0];
            DateTime? tarih = HucreTarihiGetir(cell);
            if (tarih.HasValue == false) return;
            vTezgahPlanlama secilen = cell.Item as vTezgahPlanlama;
            int gunSayisi = (int) (Convert.ToDouble(TxtToplamMetre.Text) / Convert.ToDouble(TxtÜretilenMetre.Text));
            //int sonuc = _Plan.PlanlamaKaydet(secilen.TezgahId, 6, secilen.TipId, tarih.Value, Convert.ToDouble(TxtÜretilenMetre.Text), gunSayisi);
            //if (sonuc == 1) PlaniYukle();
            //MessageBox.Show(sonuc.ToString());

        }

        private void menuItemSil_Click(object sender, RoutedEventArgs e)
        {
            DataGridCellInfo cell = DGridTezgahlar.SelectedCells[0];
            vTezgahPlanlama secilen = cell.Item as vTezgahPlanlama;
            DateTime? date = HucreTarihiGetir(cell);
            if (date.HasValue == false) return;
            bool snc = _Plan.TezgahPlaniSil(_Plan.TezgahPlanDetayGetir(secilen.TezgahId, date.Value));
            if (snc) PlaniYukle();
        }
    }
}

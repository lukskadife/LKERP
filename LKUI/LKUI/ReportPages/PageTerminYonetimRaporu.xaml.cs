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
using LKUI.Classes;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageTerminYonetimRaporu.xaml
    /// </summary>
    public partial class PageTerminYonetimRaporu : UserControl
    {
        public PageTerminYonetimRaporu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridSiparis.ItemsSource = Rapor.SiparisBazliTerminRaporuGetir();

            ClmSiparisGecikme.ColumnFilterDescriptor.SuspendNotifications();
            ClmSiparisGecikme.ColumnFilterDescriptor.FieldFilter.Filter1.Operator = Telerik.Windows.Data.FilterOperator.IsNotEqualTo;
            ClmSiparisGecikme.ColumnFilterDescriptor.FieldFilter.Filter1.Value = 0;
            ClmSiparisGecikme.ColumnFilterDescriptor.ResumeNotifications();            
        }

        private void BtnSiparisRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridSiparis.ItemsSource = Rapor.SiparisBazliTerminRaporuGetir();
        }

        private void BtnSatirRaporla_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MISiparisToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridSiparis.ToExcel();
        }

        private void MISatirToExcel_Click(object sender, RoutedEventArgs e)
        {
            DGridSatir.ToExcel();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabSatir.IsSelected && DGridSatir.ItemsSource == null)
                {
                    DGridSatir.ItemsSource = Rapor.SiparisSatirBazliTerminRaporuGetir();

                    ClmSatirGecikme.ColumnFilterDescriptor.SuspendNotifications();
                    ClmSatirGecikme.ColumnFilterDescriptor.FieldFilter.Filter1.Operator = Telerik.Windows.Data.FilterOperator.IsNotEqualTo;
                    ClmSatirGecikme.ColumnFilterDescriptor.FieldFilter.Filter1.Value = 0;
                    ClmSatirGecikme.ColumnFilterDescriptor.FieldFilter.LogicalOperator = Telerik.Windows.Data.FilterCompositionLogicalOperator.And;
                    ClmSatirGecikme.ColumnFilterDescriptor.FieldFilter.Filter2.Operator = Telerik.Windows.Data.FilterOperator.IsNotEqualTo;
                    ClmSatirGecikme.ColumnFilterDescriptor.FieldFilter.Filter2.Value = null;
                    ClmSatirGecikme.ColumnFilterDescriptor.ResumeNotifications();
                }
            }
        }

        
    }
}

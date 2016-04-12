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

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageReProcessRaporu.xaml
    /// </summary>
    public partial class PageReProcessRaporu : UserControl
    {
        public PageReProcessRaporu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateIlkTarih.SelectedDate = DateTime.Today;
            DateSonTarih.SelectedDate = DateTime.Today;
        }

        private void BtnRaporla_Click(object sender, RoutedEventArgs e)
        {
            DGridReProcess.ItemsSource = Rapor.ReProcessRaporuGetir(DateIlkTarih.SelectedDate.Value.Date, DateSonTarih.SelectedDate.Value.Date).OrderByDescending(c => c.CikisTarih);
        }


    }
}

using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageStokAlarm.xaml
    /// </summary>
    public partial class PageStokAlarm : UserControl
    { 
        public PageStokAlarm()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridStokAlarm.ItemsSource = new Stok().StokAlarmVerenleriGetir();
        }

        private void DGridStokAlarm_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            vMalzemeStokDurum secilen = DGridStokAlarm.SelectedItem as vMalzemeStokDurum;
            if (secilen == null) return;
            cntMalzemeIhtiyac.BrdFiltre.Height = 0;
            cntMalzemeIhtiyac.DGridEkle.ItemsSource = new vMalzemeler().ArananMalzemeGetir(secilen.MalzemeId);

            ChildTalep.Show();
        }
    }
}

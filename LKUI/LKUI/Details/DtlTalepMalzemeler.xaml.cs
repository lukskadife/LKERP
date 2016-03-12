using System.Windows.Controls;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlTalepMalzemeler.xaml
    /// </summary>
    public partial class DtlTalepMalzemeler : UserControl
    {
        public DtlTalepMalzemeler()
        {
            InitializeComponent();
        }
         
        private MalzemeTalep _Talep = new MalzemeTalep();
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DGridTalepler.ItemsSource = _Talep.TalepAra(App.ClickedMenuItemId, App.PersonelId);
        }
    }
}

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
using LKUI.Details;
using LKLibrary.DbClasses;

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageOnayliRecetelerRaporu.xaml
    /// </summary>
    public partial class PageOnayliRecetelerRaporu : UserControl
    {
        KimyasalRecete _Islem;
        public PageOnayliRecetelerRaporu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridOnayliRecetelerRaporu.ItemsSource = KimyasalRecete.ReceteleriGetir().OrderByDescending(c => c.Tarih);
        }

        private void menuItemRaporuGoster_Click(object sender, RoutedEventArgs e)
        {
            if (DGridOnayliRecetelerRaporu.SelectedItem == null) return;


            vKimyasalRecete secilen = DGridOnayliRecetelerRaporu.SelectedItem as vKimyasalRecete;
            _Islem = new KimyasalRecete(secilen);
            _Islem.Recete = secilen;
            _Islem.ReceteleriYukle(true);
            DtlRapor raporlama = new DtlRapor();
            List<DtlRapor.RaporItem> list = new List<DtlRapor.RaporItem>()
            {
                new DtlRapor.RaporItem("DSBoya", _Islem.Boyalar),
                new DtlRapor.RaporItem("DSKimyasal", _Islem.Kimyasallar),
                new DtlRapor.RaporItem("DSApre", _Islem.Apreler),
                new DtlRapor.RaporItem("DSKimyasalRecete", new List<vKimyasalRecete>(){ _Islem.Recete}),
                new DtlRapor.RaporItem("DSRecetePartileri", _Islem.RecetePartiBilgileriGetir()),
                new DtlRapor.RaporItem("DSKasar", _Islem.Kasarlar),
                new DtlRapor.RaporItem("DSYikama", _Islem.Yikamalar),
                new DtlRapor.RaporItem("DSOnIslem", _Islem.OnIslemler)

            };
            
            raporlama.RaporGoster("RprBoyaRecetesi", (System.Windows.SystemParameters.PrimaryScreenWidth - 100),(System.Windows.SystemParameters.PrimaryScreenHeight - 175), list);
            ChildGenel.Content = raporlama;
            ChildGenel.Show();
        }
    }
}

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

namespace LKUI.ReportPages
{
    /// <summary>
    /// Interaction logic for PageReceteDegisiklikleriniOnaylama.xaml
    /// </summary>
    public partial class PageReceteDegisiklikleriniOnaylama : UserControl
    {
        public PageReceteDegisiklikleriniOnaylama()
        {
            InitializeComponent();
        }

        KimyasalRecete _Islem = new KimyasalRecete();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PageLoad();
        }

        private void PageLoad()
        {
            DGridReceteDegisiklikleri.ItemsSource = Rapor.OnayliReceteDegisiklikleriRaporuGetir().Where(c => (c.OnayBirPersonelId.HasValue == false || c.OnayIkiPersonelId.HasValue == false) && (c.OnayBirPersonelId != App.KullaniciId && c.OnayIkiPersonelId != App.KullaniciId)).OrderByDescending(d => d.Id);
        }

        private void BtnOnay_Click(object sender, RoutedEventArgs e)
        {
            if (DGridReceteDegisiklikleri.ItemsSource == null) return;
            List<vKimyasalReceteActLog> secilenler = DGridReceteDegisiklikleri.SelectedItems.Cast<vKimyasalReceteActLog>().ToList();
            
            if (secilenler.Count == 0) return;
            try
            {
                if (_Islem.KimyasalDegisiklikleriniOnayla(secilenler,App.KullaniciId)) PageLoad();
                else MessageBox.Show("Hata oluştu.\n\nOnaylanmadı..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

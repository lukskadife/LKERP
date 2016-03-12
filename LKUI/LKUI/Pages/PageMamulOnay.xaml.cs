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

namespace LKUI.Pages
{
    /// <summary>
    /// Interaction logic for PageMamulOnay.xaml
    /// </summary>
    public partial class PageMamulOnay : UserControl
    {
        public PageMamulOnay()
        {
            InitializeComponent();
        }

        private void LoadPage()
        {
            DGridMamul.ItemsSource = Iade.MamulKontrolListesiGetir();
        }

        private void BtnBoyahaneIadeEt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Seçilen kayıtlar boyahaneye iade edilecek..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (DGridMamul.ItemsSource == null) return;
            List<vMamulOnay> secilenler = DGridMamul.SelectedItems.Cast<vMamulOnay>().ToList();
            if (secilenler.Count == 0) return;
            try
            {
                if (new Iade().BoyaheneyeIadeEt(secilenler))  LoadPage();
                else MessageBox.Show("Hata oluştu.\n\nİade edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnOnay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Seçilen kayıtlar onaylanacak..?", App.AlertCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (DGridMamul.ItemsSource == null) return;
            List<vMamulOnay> secilenler = DGridMamul.SelectedItems.Cast<vMamulOnay>().ToList();
            if (secilenler.Count == 0) return;
            try
            {
                if (new Iade().SevkEdilebilirIsaretle(secilenler)) LoadPage();
                else MessageBox.Show("Hata oluştu.\n\nİade edilemedi..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }
    }
}

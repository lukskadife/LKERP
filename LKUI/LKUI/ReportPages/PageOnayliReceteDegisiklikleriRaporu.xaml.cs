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
    /// Interaction logic for PageOnayliReceteDegisiklikleriRaporu.xaml
    /// </summary>
    public partial class PageOnayliReceteDegisiklikleriRaporu : UserControl
    {
        public PageOnayliReceteDegisiklikleriRaporu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DGridOnayliReceteDegisiklikleriRapor.ItemsSource = Rapor.OnayliReceteDegisiklikleriRaporuGetir().OrderByDescending(c => c.Id);
        }
    }
}

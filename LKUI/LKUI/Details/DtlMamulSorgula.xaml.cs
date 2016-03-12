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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlMamulSorgula.xaml
    /// </summary>
    public partial class DtlMamulSorgula : UserControl
    {
        public DtlMamulSorgula()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBarkod.Focus();
        }

        private void TxtBarkod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GrdMamul.DataContext = null;
                GrdMamul.DataContext = LKLibrary.Classes.Mamul.MamulBarkodSorgula(TxtBarkod.Text);
                TxtBarkod.Focus();
                TxtBarkod.SelectAll();
            }
        }
    }
}

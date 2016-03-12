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

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlSiparisInfo.xaml
    /// </summary>
    public partial class DtlSiparisInfo : UserControl
    {
        public DtlSiparisInfo(int siparisActId)
        {
            InitializeComponent();
            this.DataContext = new Siparis().SiparisUrunGetir(siparisActId);
        }

    }
}

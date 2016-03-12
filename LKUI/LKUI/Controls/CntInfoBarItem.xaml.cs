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

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntInfoBarItem.xaml
    /// </summary>
    public partial class CntInfoBarItem : UserControl
    {
        public LKLibrary.DbClasses.tblDurumlar Durum;

        #region RoutedEvents

        public static readonly RoutedEvent Item_ClickEvent = EventManager.RegisterRoutedEvent(
          "ItemClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntInfoBarItem));

        public event RoutedEventHandler ItemClicked
        {
            add { AddHandler(Item_ClickEvent, value); }
            remove { RemoveHandler(Item_ClickEvent, value); }
        }

        #endregion

        public CntInfoBarItem(LKLibrary.DbClasses.tblDurumlar durum)
        {
            InitializeComponent();

            this.Durum = durum;
            Stack.DataContext = this.Durum;
        }

        public bool MiktarGuncelle(int miktar)
        {
            try
            {
                TxtDurumMiktar.Text = miktar.ToString();
                return true;
            }
            catch (Exception e)
            {
                string str = e.Message;
                return false;
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            BrdAna.BorderThickness = new Thickness(2);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            BrdAna.BorderThickness = new Thickness(0);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(Item_ClickEvent));
        }
    }
}

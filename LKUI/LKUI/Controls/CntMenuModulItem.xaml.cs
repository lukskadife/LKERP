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
    /// Interaction logic for CntMenuModulItem.xaml
    /// </summary>
    public partial class CntMenuModulItem : UserControl
    {
        public LKLibrary.DbClasses.vYetkiMenu MenuItem;
        #region RoutedEvents

        public static readonly RoutedEvent ModulItem_ClickEvent = EventManager.RegisterRoutedEvent(
          "ModulItemClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntMenuModulItem));

        public event RoutedEventHandler ModulItemClick
        {
            add { AddHandler(ModulItem_ClickEvent, value); }
            remove { RemoveHandler(ModulItem_ClickEvent, value); }
        }

        #endregion

        public CntMenuModulItem(LKLibrary.DbClasses.vYetkiMenu menuItem)
        {
            InitializeComponent();

            this.MenuItem = menuItem;
            GridItem.DataContext = this.MenuItem;
        }

        private void TxtAciklama_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtAciklama.FontWeight = FontWeights.Bold;
        }

        private void TxtAciklama_MouseLeave(object sender, MouseEventArgs e)
        {
            TxtAciklama.FontWeight = FontWeights.Normal;
        }

        private void TxtAciklama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.ClickedMenuItemId = this.MenuItem.MenuId;
            RaiseEvent(new RoutedEventArgs(ModulItem_ClickEvent));
        }


    }
}

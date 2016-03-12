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
using LKLibrary.DbClasses;

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntMenuModul.xaml
    /// </summary>
    public partial class CntMenuModul : UserControl
    {
        public vYetkiMenu MenuModul;
        public vYetkiMenu AcilacakMenuItem;

        #region RoutedEvents

        public static readonly RoutedEvent OpenPage_ClickEvent = EventManager.RegisterRoutedEvent(
          "OpenPageClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntMenuModul));

        public event RoutedEventHandler OpenPageClick
        {
            add { AddHandler(OpenPage_ClickEvent, value); }
            remove { RemoveHandler(OpenPage_ClickEvent, value); }
        }

        #endregion

        public CntMenuModul(LKLibrary.DbClasses.vYetkiMenu menuModul, UIElement infoBar)
        {
            InitializeComponent();

            this.MenuModul = menuModul;
            GridMenuModul.DataContext = this.MenuModul;

            if (infoBar != null)
            {
                Grid.SetRow(infoBar, 1);
                GridMenuModul.Children.Add(infoBar);
            }

            foreach (LKLibrary.DbClasses.vYetkiMenu item in this.MenuModul.MenuItems)
            {
                if (item.Adi == "InfoBar")
                {
                    CntInfoBar infBar = new CntInfoBar(item);
                    infBar.BarItemClicked += (snd, ea) => {
                        App.ClickedMenuItemId = infBar.ClickedDurum.BaglantiId == -1 ? infBar.ClickedDurum.Id : infBar.ClickedDurum.BaglantiId;
                        AcilacakMenuItem = infBar.AcilacakMenuItem;
                        RaiseEvent(new RoutedEventArgs(OpenPage_ClickEvent));
                    };
                    StackItems.Children.Add(infBar);
                    continue;
                }

                CntMenuModulItem modulItem = new CntMenuModulItem(item);
                modulItem.ModulItemClick += (snd, ea) =>
                    {
                        AcilacakMenuItem = modulItem.MenuItem;
                        RaiseEvent(new RoutedEventArgs(OpenPage_ClickEvent));
                    };
                StackItems.Children.Add(modulItem);
            }
        }

        private double tempHeight = 0;
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ActualHeight == 30) this.Height = tempHeight;
            else
            {
                tempHeight = this.ActualHeight;
                this.Height = 30;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tempHeight = this.ActualHeight;
            this.Height = 30;
        }
    }
}

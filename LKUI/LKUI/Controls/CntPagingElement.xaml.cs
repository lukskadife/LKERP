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
    public partial class CntPagingElement : UserControl
    {
        public CntPagingElement()
        {
            InitializeComponent();

            if (ItemCount == 0) ItemCount = 10;
        }

        #region RoutedEvents
        public static readonly RoutedEvent PagedEvent = EventManager.RegisterRoutedEvent(
         "Paged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntPagingElement));

        public event RoutedEventHandler Paged
        {
            add { AddHandler(PagedEvent, value); }
            remove { RemoveHandler(PagedEvent, value); }
        }
        #endregion                           

        public List<T> GetPagedItemsSource<T>(List<T> items = null) where T : class
        {
            if (items == null) return null;

            if (ItemCount >= items.Count)
            {
                ItemIndex = 0;
                this.Visibility = System.Windows.Visibility.Hidden;
            }

            else
            {
                this.Visibility = System.Windows.Visibility.Visible;
                this.Height = 25;

                if (ItemIndex + ItemCount < items.Count)
                {
                    ImgNext.IsEnabled = true;
                    ImgNext.Opacity = 1;
                }

                else if (ItemIndex + ItemCount > items.Count)
                {
                    if (ItemIndex > items.Count) ItemIndex = ItemIndex - ItemCount;

                    ImgNext.IsEnabled = false;
                    ImgNext.Opacity = 0.2;
                }

                if (ItemIndex > 0)
                {
                    ImgPrevious.IsEnabled = true;
                    ImgPrevious.Opacity = 1;

                }

                else if (ItemIndex <= 0)
                {
                    ImgPrevious.IsEnabled = false;
                    ImgPrevious.Opacity = 0.2;
                }
            }

            return items.Skip(ItemIndex).Take(ItemCount).ToList();
        }

        public int ItemCount { get; set; }

        public int ItemIndex = 0;

        private void ImgNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemIndex = ItemIndex + ItemCount;

            RaiseEvent(new RoutedEventArgs(PagedEvent));
        }

        private void ImgPrevious_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemIndex = ItemIndex - ItemCount;

            if (ItemIndex <= 0) ItemIndex = 0;

            RaiseEvent(new RoutedEventArgs(PagedEvent));
        }
    }
}

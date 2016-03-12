using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows;

namespace LKUI.Controls
{
    class CntIsdDataGrid : DataGrid
    {
        #region RoutedEvents
        public static readonly RoutedEvent ItemsSourceChangedEvent = EventManager.RegisterRoutedEvent(
         "ItemsSourceChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntPagingElement));

        public event RoutedEventHandler ItemsSourceChanged
        {
            add { AddHandler(ItemsSourceChangedEvent, value); }
            remove { RemoveHandler(ItemsSourceChangedEvent, value); }
        }
        #endregion   

        public new IEnumerable ItemsSource
        {
            get
            {
                // Get the base source
                return base.ItemsSource;
            }
            set
            {
                // Remove event handling from previous source if exists
                if (base.ItemsSource != null && base.ItemsSource is INotifyCollectionChanged)
                {
                    (base.ItemsSource as INotifyCollectionChanged).CollectionChanged -= new NotifyCollectionChangedEventHandler(OnCollectionChanged);
                }

                // Add event handling if collection will notify
                if (value is INotifyCollectionChanged)
                {
                    var items = (INotifyCollectionChanged)value;
                    items.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
                }

                base.ItemsSource = value;

                // Call a method that notifies when collection is renewed
                this.OnCollectionNew();
            }
        }

        void OnCollectionNew()
        {
            RaiseEvent(new RoutedEventArgs(ItemsSourceChangedEvent));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ItemsSourceChangedEvent));
        }
    }
}

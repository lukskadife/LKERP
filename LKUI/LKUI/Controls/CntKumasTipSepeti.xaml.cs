using System;
using System.Collections.Generic;
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

namespace LKUI
{
	/// <summary>
	/// Interaction logic for CntKumasTipSepeti.xaml
	/// </summary>
	public partial class CntKumasTipSepeti : UserControl
	{
		public CntKumasTipSepeti()
		{
			this.InitializeComponent();
		} 

        #region RoutedEvents

        public static readonly RoutedEvent Item_ClickEvent = EventManager.RegisterRoutedEvent(
          "HavHesaplaClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntKumasTipSepeti));

        public event RoutedEventHandler HavHesaplaClicked
        {
            add { AddHandler(Item_ClickEvent, value); }
            remove { RemoveHandler(Item_ClickEvent, value); }
        }

        public static readonly RoutedEvent IsEmri_ClickEvent = EventManager.RegisterRoutedEvent(
         "IsEmriClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntKumasTipSepeti));

        public event RoutedEventHandler IsEmriClicked
        {
            add { AddHandler(IsEmri_ClickEvent, value); }
            remove { RemoveHandler(IsEmri_ClickEvent, value); }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(Item_ClickEvent));
        }

        private void BtnCIsEmri_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(IsEmri_ClickEvent));
        }
	}
}
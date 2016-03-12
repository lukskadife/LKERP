using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using System.Globalization;

namespace LKUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Sisteme login olan kullanıcının personel id'si
        /// </summary>
        public static int PersonelId;
        public static string PersonelAdi;

        /// <summary>
        /// Sisteme login yapan kullanicinin id'si
        /// </summary>
        public static int KullaniciId = 0;

        public static int PersonelBolumId;

        public static int ClickedMenuItemId;

        public static string AlertCaption = ".. I S D ..";

        App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
             new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}

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
    /// Interaction logic for CntLogin.xaml
    /// </summary>
    public partial class CntLogin : UserControl
    {
        #region RoutedEvents

        public static readonly RoutedEvent Login = EventManager.RegisterRoutedEvent(
          "Logined", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntLogin));

        public event RoutedEventHandler Logined
        {
            add { AddHandler(Login, value); }
            remove { RemoveHandler(Login, value); }
        }

        #endregion

        public CntLogin()
        {
            InitializeComponent();
        }

        public void BtnGiris_Click(object sender, RoutedEventArgs e)
        {
            //TxtName.Text = "sukru";
            //PsSifre.Password = "sukru";
            if (!string.IsNullOrEmpty(TxtName.Text) && !string.IsNullOrEmpty(PsSifre.Password))
                RaiseEvent(new RoutedEventArgs(Login));
        }

        private void BtnVazgec_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        ///     Simple behavior for the PasswordBox to provide a watermark text element.
        /// </summary>
        
    }

    public class PasswordBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));

        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = d as PasswordBox;
            if (pb == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
            else
            {
                pb.PasswordChanged -= PasswordChanged;
            }
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }
    }
}

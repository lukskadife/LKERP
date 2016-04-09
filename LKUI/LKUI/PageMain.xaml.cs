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
//using System.Windows.Navigation;
using System.Windows.Shapes;
using LKUI.Controls;
using LKUI.Classes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Navigation;

namespace LKUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class PageMain : Window
    {
        public PageMain()
        {
            InitializeComponent();
        }
        private LKLibrary.Classes.Menu _Menu;

        private void LoadPage()
        {
            this._Menu = new LKLibrary.Classes.Menu();
            StackMenu.Children.Clear();
            foreach (LKLibrary.DbClasses.vYetkiMenu item in _Menu.GetMenuItems(App.PersonelId, App.PersonelBolumId))
            {
                CntMenuModul modul = new CntMenuModul(item, (UIElement)Operations.GetPageWithStringName(item.Deger));
                modul.OpenPageClick += (snd, ea) =>
                    {
                        if (modul.AcilacakMenuItem.KontrolMu.HasValue && modul.AcilacakMenuItem.KontrolMu.Value)
                        {
                            //ChildMain = new Microsoft.Windows.Controls.ChildWindow() { IsModal = true, Height = Double.NaN, WindowStartupLocation = Microsoft.Windows.Controls.WindowStartupLocation.Center };
                            ChildMain.Margin = new Thickness(0);
                            ChildMain.WindowStartupLocation = Microsoft.Windows.Controls.WindowStartupLocation.Center;
                            ChildMain.Content = Operations.GetPageWithStringName(modul.AcilacakMenuItem.Deger);
                            ChildMain.Caption = modul.AcilacakMenuItem.Aciklama;
                            ChildMain.Show();
                        }
                        else
                        {

                            object objSayfa = Operations.GetPageWithStringName(modul.AcilacakMenuItem.Deger);
                          
                            if (objSayfa != null)
                            {
                                RadWindow wnd = new RadWindow();

                                wnd.Top = 30;
                                wnd.Left = 5;
                                wnd.Height = this.ActualHeight - 50;
                                wnd.Width = this.ActualWidth - 30;
                                wnd.Owner = Application.Current.MainWindow;
                                wnd.Header = modul.AcilacakMenuItem.Aciklama;
                                wnd.Content = objSayfa;
                                wnd.WindowState = WindowState.Normal;
                                RadWindowInteropHelper.SetAllowTransparency(wnd, false);
                                wnd.Show();           
                            }
                        }
                    };
                StackMenu.Children.Add(modul);
            }
            TxtKullanici.Text = App.PersonelAdi;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TxtVersiyon.Text = "LKERP v1.0.207.4.7";
            if (App.KullaniciId != 0) LoadPage();
            else
            {
                PopLogin.IsOpen = true;
            }
            CntLogon.BtnGiris_Click(sender, e);
        }

        private void CntLogin_Logined(object sender, RoutedEventArgs e)
        {
            LKLibrary.DbClasses.vKullanicilar kullanici = new LKLibrary.Classes.Menu().GetKullanici(CntLogon.TxtName.Text, CntLogon.PsSifre.Password);

            if (kullanici != null)
            {
                App.KullaniciId = kullanici.Id;
                App.PersonelAdi = kullanici.PersonelAdi;
                App.PersonelId = kullanici.PersonelId;
                App.PersonelBolumId = kullanici.BolumId.Value;
                LoadPage();
                PopLogin.IsOpen = false;
            }
            else
            {
                PopLogin.StaysOpen = false;
                PopLogin.Visibility = System.Windows.Visibility.Hidden;

                if (MessageBox.Show("Kullanıcı adı ve/veya şifre yanlış..", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Stop) == MessageBoxResult.OK)
                {
                }

                PopLogin.StaysOpen = true;
                PopLogin.IsOpen = true;
                CntLogon.TxtName.Clear();
                CntLogon.PsSifre.Clear();
                PopLogin.Visibility = Visibility.Visible;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5) LoadPage();
        }
    }
}

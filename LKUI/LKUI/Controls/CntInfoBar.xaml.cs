using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using LKLibrary.DbClasses;
using LKLibrary.Classes;
using System.Windows.Threading;

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntInfoBar.xaml
    /// </summary>
    public partial class CntInfoBar : UserControl
    {
        public LKLibrary.DbClasses.vYetkiMenu AcilacakMenuItem;
        #region RoutedEvents

        public static readonly RoutedEvent Item_ClickEvent = EventManager.RegisterRoutedEvent(
          "BarItemClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntInfoBarItem));

        public event RoutedEventHandler BarItemClicked
        {
            add { AddHandler(Item_ClickEvent, value); }
            remove { RemoveHandler(Item_ClickEvent, value); }
        }

        #endregion

        private LKLibrary.DbClasses.vYetkiMenu _BarAyarTanim;
         
        //DispatcherTimer TimerTalepKontrol = new DispatcherTimer();

        public CntInfoBar(LKLibrary.DbClasses.vYetkiMenu barAyar)
        {
            InitializeComponent();

            _BarAyarTanim = barAyar;
            //TimerTalepKontrol.Tick += new System.EventHandler(TimerTalepKontrol_Tick);
            //TimerTalepKontrol.Interval = new System.TimeSpan(0, 1, 0);
            //TimerTalepKontrol.Start();            
        }

        void DurumlariGuncelle()
        {
            //Ekrandaki sayfa ana sayfa ise count'ları hesaplar. Aksi durumda durumları güncellemeye gerek yoktur.
            //if (((System.Windows.Navigation.NavigationWindow)(Application.Current.MainWindow as System.Windows.Navigation.NavigationWindow)).NavigationService.Content is PageMain)
            //{
                List<vDurumCount> listDurumCounts = new LKLibrary.Classes.Menu().GetMenuItemCounts(_BarAyarTanim.Deger, App.PersonelId, _BarAyarTanim.MenuId);
                foreach (UIElement barItem in StackInfoBar.Children)
                {
                    if (barItem is CntInfoBarItem)
                    {
                        CntInfoBarItem infoBarItem = barItem as CntInfoBarItem;
                        int talepCount = (listDurumCounts == null || listDurumCounts.Count == 0) ? 0 : listDurumCounts.Find(c => c.Id == infoBarItem.Durum.Id).DurumCount;
                        infoBarItem.MiktarGuncelle(talepCount);
                    }
                }
            //}
            //else TimerTalepKontrol.Stop();
        }

        public tblDurumlar ClickedDurum;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<tblDurumlar> listBarItem =(List<LKLibrary.DbClasses.tblDurumlar>) (new LKLibrary.Classes.Bar().GetBarItems(this._BarAyarTanim.MenuId));
            listBarItem = listBarItem.OrderBy(c => c.Sira).ToList();

            foreach (tblDurumlar barItem in listBarItem)
            {
                CntInfoBarItem item = new CntInfoBarItem(barItem);
                item.Width = 100;
                item.ItemClicked += (snd, ea) =>
                    {
                        this.ClickedDurum = item.Durum;
                        AcilacakMenuItem = new LKLibrary.DbClasses.vYetkiMenu() { Deger = (snd as CntInfoBarItem).Durum.AcilacakSayfa, KontrolMu = (snd as CntInfoBarItem).Durum.KontrolMu };
                        //_AcilacakSayfa = (snd as CntInfoBarItem).Durum.AcilacakSayfa;
                        RaiseEvent(new RoutedEventArgs(Item_ClickEvent));
                    };
                StackInfoBar.Children.Add(item);
            }

            DurumlariGuncelle();
        }
    }
}

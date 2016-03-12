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
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlPlanGanttChartSiparis.xaml
    /// </summary>
    public partial class DtlPlanGanttChartSiparis : UserControl
    {
        public DtlPlanGanttChartSiparis()
        {
            InitializeComponent();
        }

        public void LoadPlan()
        {
            List<vPlanlama> planlar = vPlanlama.SonrakiPlanlariGetir();

            var groupedPlan = planlar.GroupBy(g => new { g.TezgahId, g.TezgahAdi, g.TezgahKodu }).Select(c => new
            {
                TezgahId = c.Key.TezgahId,
                TezgahKodu = c.Key.TezgahKodu,
                TezgahAdi = c.Key.TezgahAdi,
                IlkTarih = c.OrderBy(o => o.Tarih).FirstOrDefault().Tarih,
                SonTarih = c.OrderByDescending(o => o.Tarih).FirstOrDefault().Tarih
            }).ToList();

            ObservableCollection<GanttTask> list = new ObservableCollection<GanttTask>();

            foreach (var item in groupedPlan.OrderBy(o => o.TezgahKodu))
            {
                GanttTask newTask = new GanttTask(item.IlkTarih.Value, item.SonTarih.Value.AddHours(23).AddMinutes(59), item.TezgahKodu + " - " + item.TezgahAdi);

                var siparisler = planlar.FindAll(c => c.TezgahId == item.TezgahId).GroupBy(g => new { g.SiparisId, g.MusteriAdi, g.SozlesmeNo }).Select(s => new
                {
                    SiparisNo = s.Key.SozlesmeNo,
                    Musteri = s.Key.MusteriAdi,
                    IlkTarih = s.OrderBy(o => o.Tarih).First().Tarih,
                    SonTarih = s.OrderByDescending(o => o.Tarih).First().Tarih,
                    Miktar = Math.Round(s.Sum(t => t.Miktar).Value, 2)
                });

                foreach (var sipPlan in siparisler)
                {
                    newTask.Children.Add(new GanttTask(sipPlan.IlkTarih.Value, sipPlan.SonTarih.Value.AddHours(23).AddMinutes(59), sipPlan.SiparisNo + " - " + sipPlan.Musteri + " - " + sipPlan.Miktar.ToString("###,##0.00 metre")));
                }

                list.Add(newTask);
            }

            GanttViewPlan.VisibleRange = new VisibleRange(DateTime.Today.AddDays(-1), DateTime.Today.AddYears(1));
            GanttViewPlan.PixelLength = new TimeSpan(0, 52, 0);
            GanttViewPlan.TasksSource = list;
        }
    }
}

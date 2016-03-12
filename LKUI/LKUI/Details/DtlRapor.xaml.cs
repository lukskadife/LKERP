using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LKLibrary.Classes;
using LKUI.Classes;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Reporting.WinForms;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlRapor.xaml
    /// </summary>
    public partial class DtlRapor : UserControl
    {
        public DtlRapor()
        {
            InitializeComponent();
        }

        public class RaporItem
        {
            public RaporItem(string datasetName, object source)
            {
                this.DataSetName = datasetName;
                this.Source = source;
            }

            public string DataSetName;
            public object Source;
        }

        private void ItemEkle(string datasetName, object source)
        {
            Microsoft.Reporting.WinForms.ReportDataSource item = new Microsoft.Reporting.WinForms.ReportDataSource();
            item.Name = datasetName;
            item.Value = source;
            this.viewerInstance.LocalReport.DataSources.Add(item);
        }

        public string HataMesaji;
        public bool RaporGoster(string raporAdi, List<RaporItem> items = null)
        {
            if (items != null)
            {
                this.viewerInstance.LocalReport.DataSources.Clear();

                foreach (RaporItem item in items) ItemEkle(item.DataSetName, item.Source);
            }

            if (this.viewerInstance.LocalReport.DataSources.Count <= 0) return false;

            try
            {
                Rapor rapor = new Rapor(raporAdi);

                this.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                this.viewerInstance.RefreshReport();
                this.HataMesaji = "";
                
                return true;
            }
            catch (Exception exp)
            {
                this.HataMesaji = exp.Message;
                MessageBox.Show("Yazdırma sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool RaporGoster(string raporAdi, double formSizeWidth, double formSizeHeight, List<RaporItem> items = null)
        {
            if (items != null)
            {
                this.viewerInstance.LocalReport.DataSources.Clear();

                foreach (RaporItem item in items) ItemEkle(item.DataSetName, item.Source);
            }

            if (this.viewerInstance.LocalReport.DataSources.Count <= 0) return false;

            try
            {
                Rapor rapor = new Rapor(raporAdi);

                this.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;
                this.viewerInstance.RefreshReport();
                this.HataMesaji = "";
                this.FrmHost.Width = formSizeWidth;
                this.Width = formSizeWidth;
                this.FrmHost.Height = formSizeHeight;
                this.Height = formSizeHeight;
                return true;
            }
            catch (Exception exp)
            {
                this.HataMesaji = exp.Message;
                MessageBox.Show("Yazdırma sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        void OnRenderingCompletePrintOnly(object sender, System.ComponentModel.AsyncCompletedEventArgs args)
        {
            object objviewer = viewerInstance;
            object[] prms = { sender, args };
            PrintReportViewer.ExecuteFunction(objviewer, prms, "OnRenderingCompletePrintOnly");
        }
        bool streamed = false;
        Stream CreateStreamEMFPrintOnly(string name, string extension, Encoding encoding, string mimeType, bool useChunking, Microsoft.ReportingServices.Interfaces.StreamOper operation)
        {
            if (streamed)
                return null;
            streamed = true;
            object objviewer = viewerInstance;
            object[] prms = { name, extension, encoding, mimeType, useChunking, operation };
            Stream str = (Stream)PrintReportViewer.ExecuteFunction(objviewer, prms, "CreateStreamEMFPrintOnly");
            return str;
        }
        public Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream CreateAndRegisterStream
        {
            get
            {
                return new Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream(CreateStreamEMFPrintOnly);
            }
        }
        public System.ComponentModel.AsyncCompletedEventHandler AsyncCompletedEventHandler
        {
            get
            {
                return new System.ComponentModel.AsyncCompletedEventHandler(this.OnRenderingCompletePrintOnly);
            }
        }

        public bool RaporYazdir(string raporAdi, List<RaporItem> items = null)
        {
            try
            {
                Rapor rapor = new Rapor(raporAdi);
                this.viewerInstance.LocalReport.ReportPath = rapor.RaporTamAdi;

                if (items != null)
                {
                    foreach (RaporItem item in items) 
                        ItemEkle(item.DataSetName, item.Source);
                }

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    PrintReportViewer.PrintByPriner(this, this.viewerInstance, dp.ToString().Trim());
                    RaiseEvent(new RoutedEventArgs(YazdirildiEvent));
                }
                this.HataMesaji = "";

                return true;
            }
            catch (Exception exp)
            {
                this.HataMesaji = exp.Message;
                MessageBox.Show("Yazdırma sırasında hata oluştu..!", App.AlertCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #region RoutedEvents

        public static readonly RoutedEvent YazdirildiEvent = EventManager.RegisterRoutedEvent(
          "Yazdirildi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DtlSiparisEkleDuzenle));

        public event RoutedEventHandler Yazdirildi
        {
            add { AddHandler(YazdirildiEvent, value); }
            remove { RemoveHandler(YazdirildiEvent, value); }
        }

        #endregion

        private void viewerInstance_PrintingBegin(object sender, ReportPrintEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(YazdirildiEvent));
        }
    }
}

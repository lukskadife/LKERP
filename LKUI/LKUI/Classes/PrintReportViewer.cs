using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Drawing.Printing;

namespace LKUI.Classes
{
    public static class PrintReportViewer
    {
        internal static object ExecuteFunction(object obj, object[] parms, string fnName)
        {
            Type t = obj.GetType();
            MethodInfo[] infos = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var c = from pe in infos where pe.Name == fnName select pe;
            foreach (MethodInfo info in c)
            {
                object o = info.Invoke(obj, parms);
                return o;
            }
            return null;
              

        }
        static object GetPropertyVal(object obj, string properityName)
        {
            Type t = obj.GetType();
            PropertyInfo info = t.GetProperty(properityName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return info.GetValue(obj, null);
            
        }
        public static void WriteProperityVal(object srcobj, object val, string properityName)
        {
            var infos = from inf in srcobj.GetType().GetProperties() where inf.Name == properityName select inf;
            foreach (PropertyInfo inf in infos)
            {
                inf.SetValue(srcobj, val, null);
                
            }

        }
        public static void PrintwithDialog(Microsoft.Reporting.WinForms.ReportViewer  viewer)
        {
            {
                object[] parms = { viewer, RoutedEventArgs.Empty };
                ExecuteFunction(viewer, parms, "OnPrint");
            }
        }
        public static void PrintByPriner(LKUI.Details.DtlRapor report,Microsoft.Reporting.WinForms.ReportViewer viewer, string Printername)
        {
            viewer.RefreshReport();
            viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            PageSettings pagesettings = viewer.GetPageSettings();
            object objviewer = viewer;
            FieldInfo info = viewer.GetType().GetField("m_lastUIState", BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
            object m_lastUIState = info.GetValue(objviewer);
            object PostRenderArgs = null;
            var variables = from nn in viewer.GetType().Assembly.GetTypes() where nn.Name.Contains("ReportViewerStatus") || nn.Name.Contains("PostRenderArgs") select nn;
            foreach (Type type in variables)
            {
                if (type.Name.Contains("ReportViewerStatus"))
                {
                    object[] prms = { m_lastUIState };
                    ExecuteFunction(type, prms, "DoesStateAllowPrinting");
                }
                if (type.Name.Contains("PostRenderArgs"))
                {
                    object[] ooo = { false, false };
                    PostRenderArgs = Activator.CreateInstance(type, ooo);
                }
            }
            object pr = ExecuteFunction(objviewer, null, "CreateDefaultPrintSettings");
            (pr as System.Drawing.Printing.PrinterSettings).Copies = 1;
            //(pr as System.Drawing.Printing.PrinterSettings).PrinterName =  Printername ;

            {
                object[] prms = { objviewer, pr };
                ExecuteFunction(objviewer, prms, "OnPrintingBegin");
            }
            object[] processprms = { 0, 0 };
            string deviceInfo = ExecuteFunction(objviewer, processprms, "CreateEMFDeviceInfo").ToString();
            //ExecuteFunction(objviewer, null, "ProcessAsyncInvokes");
            WriteProperityVal(objviewer, true, "PrintDialogDisplayed");
            object[] parms = { "IMAGE", true, deviceInfo, Microsoft.Reporting.WinForms.PageCountMode.Estimate, report.CreateAndRegisterStream, report.AsyncCompletedEventHandler, PostRenderArgs, false };
            ExecuteFunction(objviewer, parms, "BeginAsyncRender");
            object currentReport = GetPropertyVal(objviewer, "CurrentReport");
            object fileManager = GetPropertyVal(currentReport, "FileManager");
            
            object ReportPrintDocument = null;
            var variables2 = from nn in viewer.GetType().Assembly.GetTypes() where nn.Name.Contains("ReportPrintDocument") select nn;
            foreach (Type type in variables2)
            {
                
               
                object[] parms2 = { fileManager, pagesettings.Clone() };
                ConstructorInfo ci = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { fileManager.GetType(), typeof(PageSettings) }, null);
                ReportPrintDocument = ci.Invoke(parms2);

                WriteProperityVal(ReportPrintDocument, pr, "PrinterSettings");
                WriteProperityVal(ReportPrintDocument, "Document" , "DocumentName");
                ExecuteFunction(ReportPrintDocument, null, "Print");
            }
            
        }
        
    }
}

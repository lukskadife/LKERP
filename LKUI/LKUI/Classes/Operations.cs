using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKUI.Pages;
using LKUI.Controls;
using LKUI.Details;
using LKLibrary.DbClasses;
using LKUI.ReportPages;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using Telerik.Windows.Controls;
using System.Windows.Controls;

namespace LKUI.Classes
{
    public class ExcelHeader
        {
            public string BindName;
            public string Header;
        }

    static class Operations
    {
        public static object GetPageWithStringName(string str)
        {
            switch (str)
            {
                case "PageSatınAlma": return new PageSatınAlma();
                case "PageMalzemeIhtiyac": return new PageMalzemeIhtiyac();
                case "PageSatinAlOnay": return new PageSatinAlOnay();
                case "DtlTalepMalzemeler": return new DtlTalepMalzemeler();
                case "PageStokAlarm": return new PageStokAlarm();
                case "DtlEntegrasyon": return new DtlEntegrasyon();
                case "PageBakımOnarım": return new PageBakimOnarim();
                case "PageBakımPeriyot": return new PageBakımPeriyot();
                case "PageMakinaBakım": return new PageMakinaBakım();
                case "PageDepoGiris": return new PageDepoGiris();
                case "DtlMinStoklar": return new DtlMinStoklar();
                case "PageMakinaBakimArsiv": return new PageMakinaBakimArsiv();
                case "DtlSayacAyarlar": return new DtlSayacAyarlar();
                case "PageGunlukSayacDegerleri": return new PageGunluk_SayacDegerleri();
                case "PageSabitSatisFiyatListesi": return new PageSabitSatisFiyatListesi();
                case "PageMusteriFiyatlari": return new PageMusteriFiyatlari();
                case "DtlEnerjiAylıkFiyat": return new DtlEnerjiAylıkFiyat();
                case "PageSiparisler": return new PageSiparisler();
                case "PageMüsteriProsesFiyat": return new PageMüsteriProsesFiyat();
                case "PageProcessFiyatListesi": return new PageProcessFiyatListesi();
                case "PageKumaslar": return new PageKumaslar();
                case "DtlYetkiler": return new DtlYetkiler();
                case "PageSiparisPlanlama": return new PageSiparisPlanlama();
                case "PageKombinler": return new PageKombinler();
                case "PageLeventDurum": return new PageLeventDurum();
                case "PageIplikUrunAgaci": return new PageIplikUrunAgaci();
                case "PageIplikCikis": return new PageIplikCikis();
                case "PageIplikGiris": return new PageIplikGiris();
                case "PageHamKumasGirisi": return new PageHamKumasGirisi();
                case "PagePartileme": return new PagePartileme();
                case "PageBoyahane": return new PageBoyahane();
                case "DtlRenkKartlari": return new DtlRenkKartlari();
                case "PageKimyasalRecete": return new PageKimyasalRecete();
                case "PageLaboratuvarTestleri": return new PageLaboratuvarTestleri();
                case "DtlAciklama": return new DtlAciklama();
                case "PageMamulKumasGirisi": return new PageMamulKumasGirisi();
                case "PageSevkiyat": return new PageSevkiyat();
                case "DtlTezgahGunlukDokuma": return new DtlTezgahGunlukDokuma();
                case "DtlMamulAciklama": return new DtlMamulAciklama();
                case "BoyaProgram": return new PageBoyaProgrami();      
                case "Page1": return new Page1();
                case "PageDokumaUrunAgaci": return new PageDokumaUrunAgaci();
                case "PageHamRapor": return new PageHamRapor();
                case "PageMamulRapor": return new PageMamulRapor();
                case "PageHamUretimRapor": return new PageHamUretimRapor();
                case "PageSatisSiparisRaporu": return new PageSatisSiparisRaporu();
                case "PageIplikCikisRaporu": return new PageIplikCikisRaporu();
                case "PageIplikGirisRaporu": return new PageIplikGirisRaporu();
                case "HamTipBazliKaliteDagilim": return new PageKaliteDagilimiWithTip("ham");
                case "MamulTipBazliKaliteDagilim": return new PageKaliteDagilimiWithTip("mamul");
                case "DtlProses": return new DtlProses();
                case "PageProsesGrup": return new PageProsesGrup();
                case "HamMusteriBazliKaliteDagilim": return new PageKaliteDagilimiWithMusteri("ham");
                case "MamulMusteriBazliKaliteDagilim": return new PageKaliteDagilimiWithMusteri("mamul");
                case "TezgahBazliKaliteDagilim": return new PageKaliteDagilimiWithTezgah();
                case "MamulUretimRaporu": return new PageMamulUretimRaporu();
                case "MamulSevkiyatRaporu": return new PageMamulSevkiyatRaporu();
                case "FasonSevkiyatRaporu": return new PageFasonSevkiyatRaporu();
                case "TezgahRandimanRaporu": return new PageTezgahRandimanRapor();
                case "KimyasalGirisRaporu": return new PageKimyasalGirisRaporu();
                case "KimyasalCikisRaporu": return new PageKimyasalCikisRaporu();
                case "HataDagilimHamTip": return new PageHataDagilimHamTipRaporu();
                case "HataDagilimHamTezgah": return new PageHataDagilimHamTezgahRaporu();
                case "HataDagilimMamul": return new PageHataDagilimMamulRaporu();
                case "PageIadeAlim": return new PageIadeAlim();
                case "PageMamulOnay": return new PageMamulOnay();
                case "PageBoyahaneSepeti": return new PageBoyahaneSepeti();
                case "PageIplikStokRapor": return new PageIplikStokRapor();
                case "PageKimyasalStokRaporu": return new PageKimyasalStokRaporu();
                case "PageBoyahaneHareketRaporu": return new PageBoyahaneHareketRaporu();
                case "PageTezgahArizalari": return new PageTezgahArizalari();
                case "PageTezgahAtkiGiris": return new PageTezgahAtkiGiris();
                case "DtlBoyahaneGunlukMetraj": return new DtlBoyahaneGunlukMetraj();
                case "PageYoneticiKonsolu": return new PageYoneticiKonsolu();
                case "PageTicariMal": return new PageTicariMal();
                case "DtlMamulKesim": return new DtlMamulKesim();
                case "DtlEtiket": return new DtlEtiket();
                case "PageSayim": return new PageSayim();
                case "PageFuarKumas": return new PageFuarKumas();
                case "PageFuarProses": return new PageFuarProses();
                case "PageKimyasalSarfiyatTipBazli": return new PageKimyasalSarfiyatTipBazli();
                case "DtlHamKesim": return new DtlHamKumasKes();
                case "PageFasonaGidecekMamulRaporu": return new PageFasonaGidecekMamulRaporu();
                case "PageBoyahaneUrunAgaci": return new PageBoyahaneUrunAgaci();
                case "PageYaslandirmaHam": return new PageYaslandirmaHam();
                case "PageYaslandirmaMamul": return new PageYaslandirmaMamul();
                case "PageTerminYonetimRaporu": return new PageTerminYonetimRaporu();
                case "PageSatinAlma": return new PageSatinAlma();
                case "DtlMamulSorgula": return new DtlMamulSorgula();
                case "DtlHamSorgula": return new DtlHamSorgula();
                case "DtlBukumDagitimAnahtari": return new DtlBukumDagitimAnahtari();
                case "PageKullanici": return new PageKullanici();
                case "PageHamMaliyetHesap": return new PageHamMaliyetHesap();
                case "PageOnayliReceteDegisiklikleriRaporu": return new PageOnayliReceteDegisiklikleriRaporu();
                case "PageOnayliRecetelerRaporu": return new PageOnayliRecetelerRaporu();
                case "PageReceteDegisiklikleriniOnaylama": return new PageReceteDegisiklikleriniOnaylama();
                case "PageFasonKumasMaliyet": return new PageFasonKumasMaliyet();
                case "PageSiparisMaliyet": return new PageSiparisMaliyet();
                case "PageTipBazindaSiparisMaliyeti": return new PageTipBazindaSiparisMaliyeti();
                case "PageFasonIplikMaliyet": return new PageFasonIplikMaliyet();
                case "PageReProcessRaporu": return new PageReProcessRaporu();
                case "PageKafesKartlari": return new PageKafesKartlari();
                case "PageSiradakiProcessRaporu": return new PageSiradakiProcessRaporu();
                case "PagePartilemeOkut": return new PagePartilemeOkut();
                case "PageOrguKumasGirisi": return new PageOrguKumasGirisi();
                case "PageFasonSepeti": return new PageFasonSepeti();
                case "PageAmbarTransfer": return new PageAmbarTransfer();
                case "PageOrmeUrunAgaci": return new PageOrmeUrunAgaci();
                default: return null;
            }
        }

        public static void ToExcel<T>(this Telerik.Windows.Controls.RadGridView dGrid) where T:class
        {
            if (dGrid.ItemsSource == null) return;

            List<ExcelHeader> headers = new List<ExcelHeader>();
            foreach (Telerik.Windows.Controls.GridViewColumn item in dGrid.Columns)
            {
                if (item.UniqueName == null || item.IsVisible == false) continue;

                headers.Add(new ExcelHeader()
                {
                    BindName = item.UniqueName,
                    Header = item.Header is TextBlock ? (item.Header as TextBlock).Text : item.Header.ToString()
                });
            }

            ExportToExcel<T, List<T>> exc = new ExportToExcel<T, List<T>>();
            exc.dataToPrint = dGrid.Items.OfType<T>().ToList<T>();
            exc.GenerateReport(headers);
        }

        public static void ToExcel<T>(this System.Windows.Controls.DataGrid dGrid) where T : class
        {
            if (dGrid.ItemsSource == null) return;

            List<ExcelHeader> headers = new List<ExcelHeader>();
            foreach (System.Windows.Controls.DataGridColumn item in dGrid.Columns)
            {
                if (item.SortMemberPath == null || item.Visibility == System.Windows.Visibility.Hidden || item.SortMemberPath == "") continue;

                headers.Add(new ExcelHeader()
                {
                    BindName = item.SortMemberPath,
                    Header = item.Header.ToString()
                });
            }

            ExportToExcel<T, List<T>> exc = new ExportToExcel<T, List<T>>();
            exc.dataToPrint = dGrid.Items.OfType<T>().ToList<T>();
            exc.GenerateReport(headers);
        }

        public static void ToExcel(this Telerik.Windows.Controls.RadGridView dGrid)
        {
            string extension = "xls";
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    dGrid.Export(stream,
                     new GridViewExportOptions()
                     {
                         Format = ExportFormat.ExcelML,
                         ShowColumnHeaders = true,
                         ShowColumnFooters = true,
                         ShowGroupFooters = false,
                     });

                    System.Diagnostics.Process.Start(dialog.FileName);
                }
            }
        }

        public static BitmapImage StringToImage(string binaryString)
        {
            if (binaryString == null) return null;
            byte[] binaryData = Convert.FromBase64String(binaryString);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();

            return bi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dosyaYolu">Dosya adı dizini ile birlikte verilmelidir.</param>
        /// <returns></returns>
        public static double DosyaBoyutuGetir(string dosyaYolu)
        {
            FileInfo info = new FileInfo(dosyaYolu);
            double dosyaBoyutu = Math.Round((double)info.Length / 1024, 2);

            return dosyaBoyutu;
        }
    }

    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<ExcelHeader> MyHeaders;

        public void RestoreHeaders()
        {
            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < MyHeaders.Count; n++)
            {
                objHeaders.Add(MyHeaders[n].Header);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();
        }

        public List<T> dataToPrint;
        // Excel object references.
        private Microsoft.Office.Interop.Excel.Application _excelApp = null;
        private Microsoft.Office.Interop.Excel.Workbooks _books = null;
        private Microsoft.Office.Interop.Excel._Workbook _book = null;
        private Microsoft.Office.Interop.Excel.Sheets _sheets = null;
        private Microsoft.Office.Interop.Excel._Worksheet _sheet = null;
        private Microsoft.Office.Interop.Excel.Range _range = null;
        private Microsoft.Office.Interop.Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport(List<ExcelHeader> headers)
        {
            try
            {
                MyHeaders = headers;

                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    }
                }
            }
            catch (Exception exp)
            {
                string str = exp.Message;
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        /// <summary>
        /// Make Microsoft Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];

            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember(header[i].ToString(), BindingFlags.GetProperty, null, item, null, new System.Globalization.CultureInfo("en-US"));
                    if (y is double || y is Double || y is Double?) objData[j, i] = (y == null) ? "" : y;
                    else objData[j, i] = (y == null) ? "" : y.ToString().Replace('=',':');
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);

            RestoreHeaders();
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            //PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < MyHeaders.Count; n++)
            {
                //if (MyHeaders.Find(c => c.BindName == MyHeaders[n].Name) == null) continue;
                objHeaders.Add(MyHeaders[n].BindName);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows
        (string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        /// <summary>
        /// Create Excel application parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _books = (Microsoft.Office.Interop.Excel.Workbooks)_excelApp.Workbooks;
            _book = (Microsoft.Office.Interop.Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Microsoft.Office.Interop.Excel.Sheets)_book.Worksheets;
            _sheet = (Microsoft.Office.Interop.Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}

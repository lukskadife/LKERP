using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityObjects;
using System.Data;


namespace LKLibrary.DbClasses
{
    public class Logo
    {

        /*
         * Kısaltmalar
         * MAI: Mal Alım İrsaliyesi
         * 
        */

        private DBEvents db = new DBEvents();
        private UnityApplication giris = new UnityApplication();

        private string fName, fEName;
   
        private void LogoExceptionSave(string fName, string message)
        {
            tblExceptionLog exception = new tblExceptionLog();

            exception.CompanyId = 0;
            exception.FunctionName = fName;
            exception.InnerMessage = "Logo.cs";
            exception.RecordDate = DateTime.Now;
            exception.Source = "LKLibrary.Logo.cs";
            exception.Message = message;

            db.SaveGeneric<tblExceptionLog>(exception);
        }
        
        private bool openConnection()
        {
            bool snc = giris.Connected;
            if (snc == false)
            {
                if (giris.Connect())
                {
                    snc= true;
                }
                else
                {
                    LogoExceptionSave("Connect()", "Connection'de hata var!...");
                    snc= false;
                }
            }

            return snc;
        }

        private bool loginUser()
        {
            if (giris.UserLogin("LOBJECT", "LOGO"))
            {                
                return true;
            }
            else
            {
                LogoExceptionSave("UserLogin()", "User Login'de hata var!...");
                return false;
            }
        }

        private bool loginCompany()
        {
            if (giris.CompanyLogin(216))
            {                
                return true;
            }
            else
            {
                LogoExceptionSave("CompanyLogin()", "Company Login'de hata var!...");
                return false;
            }
        }

        public void MamulKumasIrsaliyeAktar(vSevk SevkUstBelge)
        {
            fName = "Debug MamulKumasIrsaliteAktar";
            fEName = "MamulKumasIrsaliteAktar";
            
            try
            {
                

                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;
                
                List<vSevkGrup> SevkGrup = new DBEvents().GetGeneric<vSevkGrup>(c => c.SevkId == SevkUstBelge.Id).ToList();
                string MuhasebeKodu = new DBEvents().GetGenericWithSQLQuery<string>("exec spLogoMuhasebeKoduGetir " + SevkUstBelge.MusteriKodu.ToString(), new string[0]).FirstOrDefault();
                string OdemePlanKodu = new DBEvents().GetGenericWithSQLQuery<string>("exec spLogoOdemePlaniGetir " + SevkUstBelge.MusteriKodu.ToString(), new string[0]).FirstOrDefault();                
                
                //doSalesDispatch satış irsaliyeleri                    
                UnityObjects.IData irsaliye = giris.NewDataObject(DataObjectType.doSalesDispatch);
                irsaliye.New();                
                if (SevkUstBelge.SozlesmeNo.StartsWith("BS") || (SevkUstBelge.SozlesmeNo.StartsWith("NS")) || SevkUstBelge.SozlesmeNo.StartsWith("SS"))
                    irsaliye.DataFields.FieldByName("TYPE").Value = "8";  //8= TOPTAN SATIŞ İRSALİYESİ,
                else if (SevkUstBelge.SozlesmeNo.StartsWith("FS"))
                    irsaliye.DataFields.FieldByName("TYPE").Value = "35";  //35=FASON GÖNDERİ İRSALİYESİ
                else
                    irsaliye.DataFields.FieldByName("TYPE").Value = "8";  //8= TOPTAN SATIŞ İRSALİYESİ,    
                irsaliye.DataFields.FieldByName("NUMBER").Value = Convert.ToString(DateTime.Now.Year+ DateTime.Now.Month+ DateTime.Now.Day+DateTime.Now.Hour+ DateTime.Now.Minute+ DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("DATE").Value = SevkUstBelge.Tarih; //"31.10.2014";
                irsaliye.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("DOC_NUMBER").Value = Convert.ToString(DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("ARP_CODE").Value = SevkUstBelge.MusteriKodu;
                irsaliye.DataFields.FieldByName("GL_CODE").Value = MuhasebeKodu;//Muhasebe kodu
                irsaliye.DataFields.FieldByName("NOTES1").Value = SevkUstBelge.Aciklama;
                irsaliye.DataFields.FieldByName("PAYMENT_CODE").Value = OdemePlanKodu;
                irsaliye.DataFields.FieldByName("PRINT_COUNTER").Value = "1";
                irsaliye.DataFields.FieldByName("CREATED_BY").Value = "5";
                irsaliye.DataFields.FieldByName("DATE_CREATED").Value = SevkUstBelge.Tarih;
                irsaliye.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                irsaliye.DataFields.FieldByName("MIN_CREATED").Value = "34";
                irsaliye.DataFields.FieldByName("SEC_CREATED").Value = "26";
                irsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";             
                irsaliye.DataFields.FieldByName("DEDUCTIONPART1").Value = "2";
                irsaliye.DataFields.FieldByName("DEDUCTIONPART2").Value = "3";
                irsaliye.DataFields.FieldByName("AFFECT_RISK").Value = "1";
                irsaliye.DataFields.FieldByName("DISP_STATUS").Value = "1";
                irsaliye.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod
               
                Lines detay = irsaliye.DataFields.FieldByName("TRANSACTIONS").Lines;               
                int i=0;
                bool DovizliIslemMi=false;
                foreach (var sevk in SevkGrup)
            	{                         
                    if (detay.AppendLine())
                    {                 
                        detay[i].FieldByName("TYPE").Value = "0";
                        //hali hazırda kullanılan 03.A, 03.J VE 03.T İLE BAŞLAYAN mamül kodlarımız mevcuttur.
                        detay[i].FieldByName("MASTER_CODE").Value = sevk.Kodu;//"03.A.5300.00.150.9197.05"; 
                        detay[i].FieldByName("GL_CODE2").Value = "391.01.02"; //??????
                        detay[i].FieldByName("PAYMENT_CODE").Value = OdemePlanKodu;
                        detay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetMetre.ToString());   //"2097.7"; //MİKTAR
                        
                        detay[i].FieldByName("UNIT_CODE").Value = "MT"; //birim seti
                        detay[i].FieldByName("UNIT_CONV1").Value = "1";
                        detay[i].FieldByName("UNIT_CONV2").Value = "1";
                        detay[i].FieldByName("VAT_RATE").Value = "8";   //kdv

                        detay[i].FieldByName("DETAILS").Value = "";
                        detay[i].FieldByName("QCLIST").Value = "";                   
                        detay[i].FieldByName("DIST_ORD_REFERENCE").Value = "0";
                        detay[i].FieldByName("EDT_CURR").Value = "1";
                        detay[i].FieldByName("AFFECT_RISK").Value = "1";                    
                        detay[i].FieldByName("MONTH").Value = SevkUstBelge.Tarih.Month.ToString();
                        detay[i].FieldByName("YEAR").Value = SevkUstBelge.Tarih.Year.ToString();
                        detay[i].FieldByName("DATE").Value = SevkUstBelge.Tarih.ToShortDateString();

                        detay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.TLFiyati.ToString()); // --TL BİRİM FİYAT
                        detay[i].FieldByName("CURR_PRICE").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString()); //--DÖVİZ CİNSİ 1=$, 20=€, 17=gbp,
                        detay[i].FieldByName("PC_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT
                        detay[i].FieldByName("CURR_TRANSACTION").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString()); //--DÖVİZ CİNSİ 1=$, 20=€, 17=gbp,
                        detay[i].FieldByName("TC_XRATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString()); //--DÖVİZ KURU (İRSALİYE TARİHİNDEKİ)
                        detay[i].FieldByName("PR_RATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString()); //--DÖVİZ KURU (İRSALİYE TARİHİNDEKİ)
                        detay[i].FieldByName("EDT_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT
                        detay[i].FieldByName("EXCHLINE_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT  
                        detay[i].FieldByName("EDT_CURR").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        i++;

                        if (sevk.DovizCinsiLogo != 0)
                            DovizliIslemMi=true;
                    }
		 
	            }                

                if (DovizliIslemMi)
                {
                    irsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "2"; //--İŞLEM DÖVİZİ
                    irsaliye.DataFields.FieldByName("CURRSEL_DETAILS").Value = "2"; //--İŞLEM DÖVİZİ
                }                
                
                irsaliye.Post();

                ValidateErrors err = irsaliye.ValidateErrors;                

                if (!irsaliye.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");       
                    }
                }
                else
                {   
                   LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("MamulKumasIrsaliyeAktar", Message);
            }

        }

        public void IplikIrsaliyeAktar(int satinAlmaTalepId)
        {
            fName = "Debug IplikIrsaliteAktar";
            fEName = "IplikIrsaliteAktar";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vLogoIplikGirisTest> SevkGrup = new DBEvents().GetGeneric<vLogoIplikGirisTest>(c => c.SatinAlmaTalepId == satinAlmaTalepId).ToList();
                vLogoIplikGirisTest Header = new vLogoIplikGirisTest();
                Header = SevkGrup.FirstOrDefault();
                string MuhasebeKodu = new DBEvents().GetGenericWithSQLQuery<string>("exec spLogoMuhasebeKoduGetir " + Header.MusteriKodu.ToString(), new string[0]).FirstOrDefault();
                string OdemePlanKodu = new DBEvents().GetGenericWithSQLQuery<string>("exec spLogoOdemePlaniGetir " + Header.MusteriKodu.ToString(), new string[0]).FirstOrDefault();

                //doPurchDispatch Alım İrsaliye                   
                UnityObjects.IData irsaliye = giris.NewDataObject(DataObjectType.doPurchDispatch);
                irsaliye.New();
                irsaliye.DataFields.FieldByName("TYPE").Value = "1";  //1 = SATINALMA İRSALİYESİ,    
                irsaliye.DataFields.FieldByName("NUMBER").Value = Convert.ToString(DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("DATE").Value = Header.Tarih; //"31.10.2014";
                irsaliye.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("DOC_NUMBER").Value = Convert.ToString(DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                irsaliye.DataFields.FieldByName("ARP_CODE").Value = Header.MusteriKodu;
                irsaliye.DataFields.FieldByName("GL_CODE").Value = MuhasebeKodu;//Muhasebe kodu
                irsaliye.DataFields.FieldByName("NOTES1").Value = Header.LotNo;
                irsaliye.DataFields.FieldByName("PAYMENT_CODE").Value = OdemePlanKodu;
                irsaliye.DataFields.FieldByName("PRINT_COUNTER").Value = "1";
                irsaliye.DataFields.FieldByName("CREATED_BY").Value = "5";
                irsaliye.DataFields.FieldByName("DATE_CREATED").Value = Header.Tarih;
                irsaliye.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                irsaliye.DataFields.FieldByName("MIN_CREATED").Value = "34";
                irsaliye.DataFields.FieldByName("SEC_CREATED").Value = "26";
                irsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                irsaliye.DataFields.FieldByName("DEDUCTIONPART1").Value = "2";
                irsaliye.DataFields.FieldByName("DEDUCTIONPART2").Value = "3";
                irsaliye.DataFields.FieldByName("AFFECT_RISK").Value = "1";
                irsaliye.DataFields.FieldByName("DISP_STATUS").Value = "1";
                irsaliye.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines detay = irsaliye.DataFields.FieldByName("TRANSACTIONS").Lines;
                int i = 0;
                bool DovizliIslemMi = false;
                foreach (var sevk in SevkGrup)
                {
                    if (detay.AppendLine())
                    {
                        detay[i].FieldByName("TYPE").Value = "0";
                        //hali hazırda kullanılan 03.A, 03.J VE 03.T İLE BAŞLAYAN mamül kodlarımız mevcuttur.
                        detay[i].FieldByName("MASTER_CODE").Value = sevk.MalzemeKodu;//"03.A.5300.00.150.9197.05"; 
                        detay[i].FieldByName("GL_CODE2").Value = "391.01.02"; //??????
                        detay[i].FieldByName("PAYMENT_CODE").Value = OdemePlanKodu;
                        detay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR

                        detay[i].FieldByName("UNIT_CODE").Value = "KG"; //birim seti
                        detay[i].FieldByName("UNIT_CONV1").Value = "1";
                        detay[i].FieldByName("UNIT_CONV2").Value = "1";
                        detay[i].FieldByName("VAT_RATE").Value = "8";   //kdv

                        detay[i].FieldByName("DETAILS").Value = "";
                        detay[i].FieldByName("QCLIST").Value = "";
                        detay[i].FieldByName("DIST_ORD_REFERENCE").Value = "0";
                        detay[i].FieldByName("EDT_CURR").Value = "1";
                        detay[i].FieldByName("AFFECT_RISK").Value = "1";
                        detay[i].FieldByName("MONTH").Value = sevk.Tarih.Month.ToString();
                        detay[i].FieldByName("YEAR").Value = sevk.Tarih.Year.ToString();
                        detay[i].FieldByName("DATE").Value = sevk.Tarih.ToShortDateString();

                        detay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.TLFiyati.ToString()); // --TL BİRİM FİYAT
                        detay[i].FieldByName("CURR_PRICE").Value = "0"; //--DÖVİZ CİNSİ 1=$, 20=€, 17=gbp,
                        detay[i].FieldByName("PC_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT
                        detay[i].FieldByName("CURR_TRANSACTION").Value = "0"; //--DÖVİZ CİNSİ 1=$, 20=€, 17=gbp,
                        detay[i].FieldByName("TC_XRATE").Value = Convert.ToDouble(0); //--DÖVİZ KURU (İRSALİYE TARİHİNDEKİ)
                        detay[i].FieldByName("PR_RATE").Value = Convert.ToDouble(0); //--DÖVİZ KURU (İRSALİYE TARİHİNDEKİ)
                        detay[i].FieldByName("EDT_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT
                        detay[i].FieldByName("EXCHLINE_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString()); //--DÖVİZLİ BİRİM FİYAT  
                        detay[i].FieldByName("EDT_CURR").Value = "0";
                        i++;

                        DovizliIslemMi = false;
                    }
                }

                if (DovizliIslemMi)
                {
                    irsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "2"; //--İŞLEM DÖVİZİ
                    irsaliye.DataFields.FieldByName("CURRSEL_DETAILS").Value = "2"; //--İŞLEM DÖVİZİ
                }

                irsaliye.Post();

                ValidateErrors err = irsaliye.ValidateErrors;

                if (!irsaliye.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("IplikIrsaliyeAktar", Message);
            }

        }

        public void BoyaveKimyasalMalAlimAktar(vKimyasalRecete SevkUstBelge,List<vKimyasalRecetePartileri> RecetePartileri)
        {
            fName = "Debug MalAlimIrsaliyeAktar";
            fEName = "MalAlimIrsaliyeAktar";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                vKimyasalRecetePartileri parti = RecetePartileri.FirstOrDefault();
                List<vKimyasalSevkGrup> SevkGrup = new DBEvents().GetGeneric<vKimyasalSevkGrup>(c => c.ReceteId == SevkUstBelge.Id).ToList();
                string MuhasebeKodu = new DBEvents().GetGenericWithSQLQuery<string>("exec spLogoMuhasebeKoduGetir " + parti.MusteriKodu.ToString(), new string[0]).FirstOrDefault();               

                //doPurchDispatch mal alım irsaliyeleri                    
                UnityObjects.IData MalAlimirsaliye = giris.NewDataObject(DataObjectType.doPurchDispatch);
                MalAlimirsaliye.New();
                MalAlimirsaliye.DataFields.FieldByName("TYPE").Value = "1"; //1 mal alım irsaliyesi
                MalAlimirsaliye.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalAlimirsaliye.DataFields.FieldByName("DATE").Value = SevkUstBelge.Tarih; //"31.10.2014";
                MalAlimirsaliye.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalAlimirsaliye.DataFields.FieldByName("DOC_NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalAlimirsaliye.DataFields.FieldByName("ARP_CODE").Value = parti.MusteriKodu;
                MalAlimirsaliye.DataFields.FieldByName("GL_CODE").Value = MuhasebeKodu;//Muhasebe kodu
                MalAlimirsaliye.DataFields.FieldByName("NOTES1").Value = SevkUstBelge.Aciklama;

                MalAlimirsaliye.DataFields.FieldByName("PRINT_COUNTER").Value = "1";
                MalAlimirsaliye.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalAlimirsaliye.DataFields.FieldByName("DATE_CREATED").Value = SevkUstBelge.Tarih;
                MalAlimirsaliye.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalAlimirsaliye.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalAlimirsaliye.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalAlimirsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalAlimirsaliye.DataFields.FieldByName("DEDUCTIONPART1").Value = "2";
                MalAlimirsaliye.DataFields.FieldByName("DEDUCTIONPART2").Value = "3";
                MalAlimirsaliye.DataFields.FieldByName("AFFECT_RISK").Value = "1";
                MalAlimirsaliye.DataFields.FieldByName("DISP_STATUS").Value = "1";
                MalAlimirsaliye.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines malimdetay = MalAlimirsaliye.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                bool DovizliIslemMi = false;
                foreach (var sevk in SevkGrup)
                {
                    if (malimdetay.AppendLine())
                    {
                        malimdetay[i].FieldByName("TYPE").Value = "0";

                        malimdetay[i].FieldByName("MASTER_CODE").Value = sevk.Kodu;  //"901294"; 
                        malimdetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.Miktar.ToString());   //"2097.7"; //MİKTAR
                        malimdetay[i].FieldByName("UNIT_CODE").Value = "KG-AD"; //birim seti değişkenli satırdaki birimi almalı
                        malimdetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        malimdetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        malimdetay[i].FieldByName("VAT_RATE").Value = "";   //kdv alanıda değişkenli 1,8 ve 18 

                        malimdetay[i].FieldByName("DETAILS").Value = "";
                        malimdetay[i].FieldByName("QCLIST").Value = "";

                        malimdetay[i].FieldByName("DIST_ORD_REFERENCE").Value = "0";
                        malimdetay[i].FieldByName("EDT_CURR").Value = "1";
                        malimdetay[i].FieldByName("AFFECT_RISK").Value = "1";

                        malimdetay[i].FieldByName("MONTH").Value = SevkUstBelge.Tarih.Month.ToString();
                        malimdetay[i].FieldByName("YEAR").Value = SevkUstBelge.Tarih.Year.ToString();
                        malimdetay[i].FieldByName("DATE").Value = SevkUstBelge.Tarih.ToShortDateString();


                        //işlem dövizi için eklenen satır alanları
                        // satınalma işlemlerinde ben birim fiyat alanı göremedim. o yüzden standart 1 olarak girelim. deneme amaçlı
                        malimdetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());   // sipariş satırından alınacak.
                        malimdetay[i].FieldByName("CURR_PRICE").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        malimdetay[i].FieldByName("PC_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        malimdetay[i].FieldByName("CURR_TRANSACTION").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());

                        malimdetay[i].FieldByName("TC_XRATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());
                        malimdetay[i].FieldByName("PR_RATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());

                        malimdetay[i].FieldByName("EDT_CURR").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        malimdetay[i].FieldByName("EDT_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());

                        i++;
                        if (sevk.DovizCinsiLogo != 0)
                            DovizliIslemMi = true;
                        //işlem dövizi aynen mal alım irsaliyesinde de var.

                    }

                }

                if (DovizliIslemMi)
                {
                    MalAlimirsaliye.DataFields.FieldByName("CURRSEL_TOTALS").Value = "2"; //--İŞLEM DÖVİZİ
                    MalAlimirsaliye.DataFields.FieldByName("CURRSEL_DETAILS").Value = "2"; //--İŞLEM DÖVİZİ
                }

                MalAlimirsaliye.Post();

                ValidateErrors err = MalAlimirsaliye.ValidateErrors;

                if (!MalAlimirsaliye.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("BoyaveKimyasalIrsaliyeAktar", Message);
            }

        }  
  
        public void BoyaveKimyasalSarfFisi(vKimyasalRecete SevkUstBelge)
        {
            fName = "Debug BoyaveKimyasalSarfFisi";
            fEName = "BoyaveKimyasalSarfFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;
               
                List<vKimyasalSevkGrup> SevkGrup = new DBEvents().GetGeneric<vKimyasalSevkGrup>(c => c.ReceteId == SevkUstBelge.Id).ToList();
                                             
                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = SevkUstBelge.Tarih;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = SevkUstBelge.Tarih;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;                
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i+1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.Miktar.ToString());   //"2097.7"; //MİKTAR

                        FisDetay[i].FieldByName("UNIT_CODE").Value = "KG-AD"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("VAT_RATE").Value = "";   //kdv alanıda değişkenli 1,8 ve 18 

                        FisDetay[i].FieldByName("DETAILS").Value = "";
                        FisDetay[i].FieldByName("QCLIST").Value = "";

                        FisDetay[i].FieldByName("DATA_REFERENCE").Value = "0"; //SATIRIN LOGODAKİ IDSİ-LOGICALREFİ
                        FisDetay[i].FieldByName("EU_VAT_STATUS").Value = "4"; //BU SATIRIN ANLAMINI BİLMİYORUM????

                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";

                        //işlem dövizi için eklenen satır alanları
                        // satınalma işlemlerinde ben birim fiyat alanı göremedim. o yüzden standart 1 olarak girelim. deneme amaçlı
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());   // sipariş satırından alınacak.
                        FisDetay[i].FieldByName("CURR_PRICE").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        FisDetay[i].FieldByName("PC_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        FisDetay[i].FieldByName("CURR_TRANSACTION").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());

                        FisDetay[i].FieldByName("TC_XRATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());
                        FisDetay[i].FieldByName("PR_RATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());

                        FisDetay[i].FieldByName("EDT_CURR").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        FisDetay[i].FieldByName("EDT_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("BoyaveKimyasalSarfFisi", Message);
            }

        } 

        public void HamKumasSarfFisi(vPartiler SevkUstBelge)
        {
            fName = "Debug MalzemeFisiAktar";
            fEName = "MalzemeFisiAktar";
            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vHamKumasSevkGrup> SevkGrup = new DBEvents().GetGeneric<vHamKumasSevkGrup>(c => c.PartiId == SevkUstBelge.Id).ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = SevkUstBelge.Tarih;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = SevkUstBelge.Tarih;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetMetre.ToString());   //"2097.7"; //MİKTAR

                        FisDetay[i].FieldByName("UNIT_CODE").Value = "MT"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("VAT_RATE").Value = "";   //kdv alanıda değişkenli 1,8 ve 18 

                        FisDetay[i].FieldByName("DETAILS").Value = "";
                        FisDetay[i].FieldByName("QCLIST").Value = "";

                        FisDetay[i].FieldByName("DATA_REFERENCE").Value = "0"; //SATIRIN LOGODAKİ IDSİ-LOGICALREFİ
                        FisDetay[i].FieldByName("EU_VAT_STATUS").Value = "4"; //BU SATIRIN ANLAMINI BİLMİYORUM????

                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";

                        //işlem dövizi için eklenen satır alanları
                        // satınalma işlemlerinde ben birim fiyat alanı göremedim. o yüzden standart 1 olarak girelim. deneme amaçlı
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());   // sipariş satırından alınacak.
                        FisDetay[i].FieldByName("CURR_PRICE").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        FisDetay[i].FieldByName("PC_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        FisDetay[i].FieldByName("CURR_TRANSACTION").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());

                        FisDetay[i].FieldByName("TC_XRATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());
                        FisDetay[i].FieldByName("PR_RATE").Value = Convert.ToDouble(sevk.DovizKuruTL.ToString());

                        FisDetay[i].FieldByName("EDT_CURR").Value = Convert.ToString(sevk.DovizCinsiLogo.ToString());
                        FisDetay[i].FieldByName("EDT_PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("HamKumasSarfFisi", Message);
            }

        }

        public void TekKatIplikSarfFisi()
        {
            //Sadece 'BC' Büküme Çıkış İplikleri sarf ediliyor.
            fName = "Debug BoyaveKimyasalSarfFisi";
            fEName = "BoyaveKimyasalSarfFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vIplikTekKatSarf> SevkGrup = new DBEvents().GetGeneric<vIplikTekKatSarf>().ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = "TEK KAT SARF TEST";//Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR
                        FisDetay[i].FieldByName("UNIT_CODE").Value = "KG"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("TekKatIplikSarfFisi", Message);
            }

        }

        public void AtkiIplikSarfFisi()
        {
            //Sadece 'DC' Dokuma Çıkış İplikler Sarf Ediliyor.
            fName = "Debug AtkiIplikSarfFisi";
            fEName = "AtkiIplikSarfFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vIplikAtkiSarf> SevkGrup = new DBEvents().GetGeneric<vIplikAtkiSarf>().ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = "ATKI SARF TEST";//Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR
                        FisDetay[i].FieldByName("UNIT_CODE").Value = "KG"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("AtkiIplikSarfFisi", Message);
            }

        }

        public void CozguIplikSarfFisi()
        {
            //Sadece 'CCC' Levente kullanılan iplikler sarf ediliyor.
            fName = "Debug CozguIplikSarfFisi";
            fEName = "CozguIplikSarfFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vIplikCozguSarf> SevkGrup = new DBEvents().GetGeneric<vIplikCozguSarf>().ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = "ÇÖZGÜ SARF TEST";//Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR
                        FisDetay[i].FieldByName("UNIT_CODE").Value = "KG"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("CozguIplikSarfFisi", Message);
            }

        }

        public void CiftKatIplikUretimdenGirisFisi()
        {
            //Sadece 'BU' çift kat iplikler giriliyor.
            fName = "Debug CiftKatIplikUretimdenGirisFisi";
            fEName = "CiftKatIplikUretimdenGirisFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vIplikCiftKatGiris> SevkGrup = new DBEvents().GetGeneric<vIplikCiftKatGiris>().ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "13"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = "ÇİFT KAT GİRİŞ TEST";//Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR
                        FisDetay[i].FieldByName("UNIT_CODE").Value = "KG"; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("CiftKatIplikUretimdenGirisFisi", Message);
            }

        }

        public void BoyaSarfFisi()
        {
            //Reçete de kullanılan kimyasal maddeler sarf ediliyor.
            fName = "Debug BoyaSarfFisi";
            fEName = "BoyaSarfFisi";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                List<vKimyasalSarfFisi> SevkGrup = new DBEvents().GetGeneric<vKimyasalSarfFisi>().ToList();

                UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                MalzemeFisi.New();
                MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                MalzemeFisi.DataFields.FieldByName("TYPE").Value = "12"; //12 SARF FİŞİ, 13 ÜRETİMDEN GİRİŞ FİŞİ
                MalzemeFisi.DataFields.FieldByName("NUMBER").Value = Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DATE").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("TIME").Value = Convert.ToString(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = "BOYA SARFI (HAZİRAN-LKUI)";//Convert.ToString("LK" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
                MalzemeFisi.DataFields.FieldByName("CREATED_BY").Value = "5";
                MalzemeFisi.DataFields.FieldByName("DATE_CREATED").Value = DateTime.Now;
                MalzemeFisi.DataFields.FieldByName("HOUR_CREATED").Value = "15";
                MalzemeFisi.DataFields.FieldByName("MIN_CREATED").Value = "34";
                MalzemeFisi.DataFields.FieldByName("SEC_CREATED").Value = "26";
                MalzemeFisi.DataFields.FieldByName("CURRSEL_TOTALS").Value = "1";
                MalzemeFisi.DataFields.FieldByName("DATA_REFERENCE").Value = "2869"; //FİŞİN LOGODAKİ ID'Sİ--LOGICALREFİ  
                MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod

                Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                int i = 0;
                foreach (var sevk in SevkGrup)
                {
                    if (FisDetay.AppendLine())
                    {
                        FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu;  //"901294"; 
                        FisDetay[i].FieldByName("LINE_TYPE").Value = "0";  //0 MALZEME
                        FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1;  // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI

                        FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.NetKg.ToString());   //"2097.7"; //MİKTAR
                        FisDetay[i].FieldByName("UNIT_CODE").Value = sevk.BirimAdi; //birim seti değişkenli satırdaki birimi almalı
                        FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                        FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                        FisDetay[i].FieldByName("EDT_CURR").Value = "1";
                        FisDetay[i].FieldByName("PRICE").Value = Convert.ToDouble(sevk.BirimFiyat.ToString());
                        i++;
                    }
                }

                MalzemeFisi.Post();
                ValidateErrors err = MalzemeFisi.ValidateErrors;
                if (!MalzemeFisi.Post())
                {
                    for (int k = 0; k < err.Count; k++)
                    {
                        string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                        LogoExceptionSave(fEName, Message);
                        LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                    }
                }
                else
                {
                    LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("BoyaSarfFisi", Message);
            }

        }

        public void MalzemeFisiAktar(int SevkUstBelgeId, int cinsi)
        {
            fName = "Debug MalzemeFisiAktar";
            fEName = "MalzemeFisiAktar";

            try
            {
                if (!openConnection()) return;
                if (!loginUser()) return;
                if (!loginCompany()) return;

                if (cinsi == 1) //Örme kumaş transfer
                {
                    List<vAmbarAct> SevkGrup = new DBEvents().GetGeneric<vAmbarAct>(c => c.AmbarUstId == SevkUstBelgeId).ToList();


                    UnityObjects.IData MalzemeFisi = giris.NewDataObject(DataObjectType.doMaterialSlip);
                    MalzemeFisi.New();
                    MalzemeFisi.DataFields.FieldByName("GROUP").Value = "3"; //3 MALZEME YÖNETİM BÖLÜMÜ
                    MalzemeFisi.DataFields.FieldByName("AUXIL_CODE").Value = "ERP-LKUI"; //Özel Kod
                    MalzemeFisi.DataFields.FieldByName("SOURCE_DIVISION_NR").Value = 4;//Bursa İnegöl İşyeri numarası
                    MalzemeFisi.DataFields.FieldByName("TYPE").Value = 25; //Fiş Tipi ambar fişi için 25 tir
                    MalzemeFisi.DataFields.FieldByName("DATE").Value = Convert.ToDateTime(DateTime.Now); 
                    MalzemeFisi.DataFields.FieldByName("SOURCE_WH").Value = 3;  //Çıkış ambarı Integer dir.
                    MalzemeFisi.DataFields.FieldByName("DEST_WH").Value = 0;    //Giriş Ambarı Integer dir.
                    MalzemeFisi.DataFields.FieldByName("DOC_NUMBER").Value = SevkUstBelgeId.ToString(); //Fiş Belge No. İrsaliye no  String dir.

                    Lines FisDetay = MalzemeFisi.DataFields.FieldByName("TRANSACTIONS").Lines;

                    int i = 0;

                    foreach (var sevk in SevkGrup)
                    {
                        if (FisDetay.AppendLine())
                        {

                            FisDetay[i].FieldByName("ITEM_CODE").Value = sevk.Kodu; //-- "03.J.09100.000.M2024.40"; // sevk.Kodu;  //"901294"; "03.T.1427.00.143.1645.40";
                            FisDetay[i].FieldByName("LINE_TYPE").Value = 0;  //0 MALZEME
                            FisDetay[i].FieldByName("SOURCEINDEX").Value = 3;
                            FisDetay[i].FieldByName("DESTINDEX").Value = 0;
                            FisDetay[i].FieldByName("LINE_NUMBER").Value = i + 1; // HER SATIR İÇİN BİR BİR ARTACAK, İİLK SATIR 1, İKİNCİ SATIR 2, 3 SATIR 3 DEĞERİ ALMALI
                            FisDetay[i].FieldByName("QUANTITY").Value = Convert.ToDouble(sevk.Metre.ToString());   //"2097.7"; //MİKTAR
                            FisDetay[i].FieldByName("UNIT_CODE").Value = "MT"; //birim seti değişkenli satırdaki birimi almalı
                            FisDetay[i].FieldByName("PRICE").Value = sevk.TLFiyati;   // sipariş satırından alınacak.
                            FisDetay[i].FieldByName("UNIT_CONV1").Value = "1";
                            FisDetay[i].FieldByName("UNIT_CONV2").Value = "1";
                            FisDetay[i].FieldByName("EDT_CURR").Value = "1";

                            i++;


                        }

                    }


                    MalzemeFisi.Post();

                    ValidateErrors err = MalzemeFisi.ValidateErrors;

                    if (!MalzemeFisi.Post())
                    {
                        for (int k = 0; k < err.Count; k++)
                        {
                            string Message = "ValidateErrors List to Post:\n" + "Error: " + err[k].Error + "\nError ıd: " + err[k].ID;
                            LogoExceptionSave(fEName, Message);
                            LogoExceptionSave(fEName, "Post işlemi yapılırken hata oluştu!... ");
                        }
                    }
                    else
                    {
                        LogoExceptionSave(fName, "Post işlemi başarılı bir şekilde yapıldı. ");
                    }
                }

                else if (cinsi == 2) //iplik transfer
                {
 
                }

                giris.CompanyLogout();

                giris.UserLogout();

                giris.Disconnect();

                giris = null;

                System.GC.Collect();

            }
            catch (Exception ex)
            {
                string Message = "Message: " + ex.Message + "\nData: " + ex.Data + "\nHelpLink: " + ex.HelpLink + "\nInnerException: " + ex.InnerException + "\nSource: " + ex.Source + "\nStackTrace: " + ex.StackTrace + "\nTargetSite: " + ex.TargetSite; ;
                LogoExceptionSave("MalzemeFisiAktar", Message);
            }

        }  //MALZEME FİŞİ AKTARIMI SONU
    }
}

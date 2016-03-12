using System;
using System.Collections.Generic;
using System.Linq;
using LKLibrary.DbClasses;
using System.Linq.Expressions;

namespace LKLibrary.Classes
{
    public class Maliyet
    {
        //Gökhan 11.05.2014
        public static List<vProcessMaliyetleri> SiparisinProcessMaliyetleriniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vProcessMaliyetleri>("EXEC spProcessPartileriMaliyet '" + siparisId + "'", new string[0]);
        }

        //Gökhan 29.05.2014
        public static List<vSiparisKaliteRaporu> SiparisinKaliteMetresiniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiparisKaliteRaporu>("EXEC spSiparisKalite '" + siparisId + "'", new string[0]);
        }

        //Gökhan 09.07.2014
        public static List<vSiparisKaliteRaporu> SiparisinReProcessMetresiniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiparisKaliteRaporu>("EXEC spSiparisReProcess '" + siparisId + "'", new string[0]);
        }

        //Gökhan 02.07.2014
        public static List<vSiparisParcaRaporu> SiparisinParcaMetresiniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiparisParcaRaporu>("EXEC spSiparisParca '" + siparisId + "'", new string[0]);
        }

        //Gökhan 29.05.2014
        public static double DovizKarsiliginiGetir(double tutarTL, int dovizId)
        {
            int dovizTuru = 0;
            double deger = 0;

            if (dovizId == 128) dovizTuru = 17;//£ 

            else if (dovizId == 33) dovizTuru = 1;//$

            else if (dovizId == 34) dovizTuru = 20;//€

            else dovizTuru = 0;//TL
            
            DBEvents db = new DBEvents();

            double kur = db.GetGenericWithSQLQuery<vDovizKarsiliginiHesapla>("select dbo.GetirDovizKuru({0}) AS tutarDoviz", new object[] { dovizTuru }).FirstOrDefault().tutarDoviz;

            if (kur != 0 && kur != null) deger = tutarTL / kur;

            return deger;
        }

        //Gökhan 29.05.2014
        public static double TLKarsiliginiGetir(double tutarDoviz, int dovizId)
        {
            int dovizTuru = 0;
            double deger = 0;

            if (dovizId == 128) dovizTuru = 17;//£ 

            else if (dovizId == 33) dovizTuru = 1;//$

            else if (dovizId == 34) dovizTuru = 20;//€

            else dovizTuru = 0;//TL

            DBEvents db = new DBEvents();

            double kur = db.GetGenericWithSQLQuery<vDovizKarsiliginiHesapla>("select dbo.GetirDovizKuru({0}) AS tutarDoviz", new object[] { dovizTuru }).FirstOrDefault().tutarDoviz;

            if (kur != 0 && kur != null) deger = tutarDoviz * kur;

            return deger;
        }


        //Gökhan 04.06.2014
        public static List<vPartiler> SiparisinPartiMetreleriniGetir(string partiNo)
        {
            return new DBEvents().GetGeneric<vPartiler>(c=>c.PartiNo == partiNo).ToList();
        }

        //Gökhan 18.06.2014
        public static List<vIplikTumMaliyet> SiparisinIplikMaliyetleriniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vIplikTumMaliyet>("select * from dbo.fmIplikTumMaliyetler({0})", new object[] { siparisId }).ToList();
        }

        //Gökhan 18.06.2014
        public static List<vDokumaTumMaliyet> SiparisinDokumaMaliyetleriniGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vDokumaTumMaliyet>("exec spTumMaliyetler '" + siparisId + "'", new string[0]);
        }

        //Gökhan 04.06.2014
        public static List<vHamKumaslar> SiparisinGecenMetreleriniGetir(string partiNo)
        {
            DBEvents db = new DBEvents();
            return new DBEvents().GetGeneric<vHamKumaslar>(c => c.PartiId == db.GetGeneric<tblPartiler>(e=>e.PartiNo == partiNo).FirstOrDefault().Id).ToList();
        }

        //Gökhan 04.06.2014
        public static string SiparisinToplamTutarDovizGetir(double tutar)
        {
            DBEvents db = new DBEvents();
            string s = "";
            s = "   $: " + db.GetGenericWithSQLQuery<vDovizKarsiliginiHesapla>("EXEC spDovizKarsiliginiHesapla '" + Math.Round(tutar, 0) + "' , '" + 1 + "'", new string[0]).FirstOrDefault().tutarDoviz;
            s = s + "   €: " + db.GetGenericWithSQLQuery<vDovizKarsiliginiHesapla>("EXEC spDovizKarsiliginiHesapla '" + Math.Round(tutar, 0) + "' , '" + 20 + "'", new string[0]).FirstOrDefault().tutarDoviz;
            s = s + "   £: " + db.GetGenericWithSQLQuery<vDovizKarsiliginiHesapla>("EXEC spDovizKarsiliginiHesapla '" + Math.Round(tutar, 0) + "' , '" + 17 + "'", new string[0]).FirstOrDefault().tutarDoviz;
            s = "TL: " + tutar + s;
            return s;
        }


        //Gökhan 26.06.2014
        public static List<tblTmpSiparisMaliyet> MaliyetListesiniGetir(int siparisId)
        {
            DBEvents db = new DBEvents();
            return new DBEvents().GetGeneric<tblTmpSiparisMaliyet>(c => c.SiparisId == siparisId).ToList();
        }

        //Gökhan 27.06.2014
        public static void SiparisMaliyetleriniAyarla(int siparisId, int btnId)
        {
            DBEvents db = new DBEvents();
            db.GetGenericWithSQLQuery<tblTmpSiparisMaliyet>("exec spSiparisMaliyetleriniAyarla '" + siparisId + "' , '" + btnId + "'", new string[0]);
        }

        //Gökhan 27.06.2014
        public static List<vTumMaliyetlerGrup> TumMaliyetlerGrupGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vTumMaliyetlerGrup>("select * from dbo.fmTumMaliyetlerGrup({0})", new object[] { siparisId }).ToList();
        }

        //Gökhan 01.07.2014
        public static List<vIplikTumMaliyet> SiparisIplikTumMaliyetleriGetir(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vIplikTumMaliyet>("select * from dbo.fmIplikTumMaliyetler({0})", new object[] { siparisId }).ToList();
        }

        //Gökhan 08.07.2014
        public static List<vMaliyetTumunuGoster> MaliyetTumunuGoster(int siparisId)
        {
            return new DBEvents().GetGenericWithSQLQuery<vMaliyetTumunuGoster>("exec spMaliyetTumunuGoster '" + siparisId + "'", new string[0]);
        }

        public static List<vMaliyetTipBazinda> TipBazliMaliyetGoster(DateTime date1, DateTime date2)
        {            
            return new DBEvents().GetGenericWithSQLQuery<vMaliyetTipBazinda>("exec spSiparisMaliyetiRaporGenelTipBazinda '" + date1.Date.ToDateString() + "','" + date2.Date.ToDateString() + "'", new string[0]);
        }

        //Gökhan 07.07.2014
        public static List<tblMaliyetIplikDetay> MaliyetIplikDetayGetir(int siparisId)
        {
            return new DBEvents().GetGeneric<tblMaliyetIplikDetay>(c => c.SiparisId == siparisId);
        }

        //Gökhan 07.07.2014
        public static List<tblMaliyetBukumDetay> MaliyetBukumDetayGetir(int siparisId)
        {
            return new DBEvents().GetGeneric<tblMaliyetBukumDetay>(c => c.SiparisId == siparisId);
        }

        //Gökhan 07.07.2014
        public static List<tblMaliyetCozguDetay> MaliyetCozguDetayGetir(int siparisId)
        {
            return new DBEvents().GetGeneric<tblMaliyetCozguDetay>(c => c.SiparisId == siparisId);
        }

        //Gökhan 07.07.2014
        public static List<tblMaliyetDokumaDetay> MaliyetDokumaDetayGetir(int siparisId)
        {
            return new DBEvents().GetGeneric<tblMaliyetDokumaDetay>(c => c.SiparisId == siparisId);
        }

        //Gökhan 07.07.2014
        public static List<tblMaliyetBoyahaneDetay> MaliyetBoyahaneDetayGetir(int siparisId)
        {
            return new DBEvents().GetGeneric<tblMaliyetBoyahaneDetay>(c => c.SiparisId == siparisId);
        }

        //Gökhan 07.07.2014
        public static double MaliyetIplikToplamGetir(int siparisId)
        {
            
            DBEvents db1 = new DBEvents();
            List<tblMaliyetIplikDetay> liste1 = db1.GetGeneric<tblMaliyetIplikDetay>(c => c.SiparisId == siparisId).ToList();
            double sonuc = 0;
            foreach (var item in liste1)
            {
                sonuc = sonuc + (item.SatirToplami == null ? 0 : Convert.ToDouble(item.SatirToplami.ToString()));
            }
            return sonuc;
        }

        //Gökhan 07.07.2014
        public static double MaliyetBukumToplamGetir(int siparisId)
        {
            
            DBEvents db1 = new DBEvents();
            List<tblMaliyetBukumDetay> liste1 = db1.GetGeneric<tblMaliyetBukumDetay>(c => c.SiparisId == siparisId).ToList();
            double sonuc = 0;
            foreach (var item in liste1)
            {
                sonuc = sonuc + (item.SatirToplami == null ? 0 : Convert.ToDouble(item.SatirToplami.ToString()));
            }
            return sonuc;
        }

        //Gökhan 07.07.2014
        public static double MaliyetCozguToplamGetir(int siparisId)
        {
            
            DBEvents db1 = new DBEvents();
            List<tblMaliyetCozguDetay> liste1 = db1.GetGeneric<tblMaliyetCozguDetay>(c => c.SiparisId == siparisId).ToList();
            double sonuc = 0;
            foreach (var item in liste1)
            {
                sonuc = sonuc + (item.SatirToplami == null ? 0 : Convert.ToDouble(item.SatirToplami.ToString()));
            }
            return sonuc;
        }

        //Gökhan 07.07.2014
        public static double MaliyetDokumaToplamGetir(int siparisId)
        {
            
            DBEvents db1 = new DBEvents();
            List<tblMaliyetDokumaDetay> liste1 = db1.GetGeneric<tblMaliyetDokumaDetay>(c => c.SiparisId == siparisId).ToList();
            double sonuc = 0;
            foreach (var item in liste1)
            {
                sonuc = sonuc + (item.SatirToplami == null ? 0 : Convert.ToDouble(item.SatirToplami.ToString()));
            }
            return sonuc;
        }

        //Gökhan 07.07.2014
        public static double MaliyetBoyahaneToplamGetir(int siparisId)
        {

            DBEvents db1 = new DBEvents();
            List<tblMaliyetBoyahaneDetay> liste1 = db1.GetGeneric<tblMaliyetBoyahaneDetay>(c => c.SiparisId == siparisId).ToList();
            double sonuc = 0;
            foreach (var item in liste1)
            {
                sonuc = sonuc + (item.SatirToplami == null ? 0 : Convert.ToDouble(item.SatirToplami.ToString()));
            }
            return sonuc;
            
        }

        public static List<vBoyahaneProcessTotal> SiparisinBoyahaneProcessleriniGetir(int siparisId)
        {
            DBEvents db = new DBEvents();
            List<tblMaliyetBoyahaneDetay> boyahaneListe = db.GetGeneric<tblMaliyetBoyahaneDetay>(c => c.SiparisId == siparisId).ToList();
            List<vBoyahaneProcessTotal> processListe = new List<vBoyahaneProcessTotal>();
            var detay = boyahaneListe.Select(c => new
            {
                Process = c.Process,
                CalismaDk = c.ProcessCalismaDk,
                SatirToplami = c.SatirToplami,
                Sira = c.Sira
            }).ToList();

            var groupSnc = detay.GroupBy(g => new { g.Process,g.Sira }).Select(c => new
            {
                Process = c.Key.Process,
                CalismaDk = c.Sum(x => x.CalismaDk),
                SatirToplami = c.Sum(x => x.SatirToplami),
                Sira = c.Key.Sira
            }).ToList();

            foreach (var item in groupSnc)
            {
                vBoyahaneProcessTotal total = new vBoyahaneProcessTotal();
                total.CalismaDk = (double)item.CalismaDk;
                total.Process = item.Process;
                total.SatirToplami = (double)item.SatirToplami;
                total.Sira = (int)item.Sira;

                processListe.Add(total);
            }

            return processListe.OrderBy(o=>o.Sira).ToList();
        }



    }
}

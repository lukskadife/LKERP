using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Makina
    {
        DBEvents db = new DBEvents();

        #region statics

        public static double TezgahGunlukDokumaMiktariGetir(double devirSayisi, int tipId)
        {
            tblKumas tip = new DBEvents().GetGeneric<tblKumas>(c => c.Id == tipId).FirstOrDefault();

            if (tip == null || tip.AtkiSiklik == null) return 0;
            if (tip.AtkiSiklik == 0) return 0;

            double gunlukDokuma = ((devirSayisi / tip.AtkiSiklik.Value) * 60 * 24 * 2) / 100;

            return Math.Round(gunlukDokuma, 2);
        }

        public static List<tblMakinalar> MakinaKategorileriGetir()
        {
            return new DBEvents().GetGeneric<tblMakinalar>(c => c.BaglantiId == -1);
        }

        public static List<vTezgahArizalari> TezgahArizalariGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vTezgahArizalari>(c => ilkTarih <= c.BaslangicTarihi && c.BaslangicTarihi <= sonTarih.AddDays(1));
        }

        public static bool TezgahArizaEkle(vTezgahArizalari ariza)
        {
            if (ariza.BaslangicTarihi.HasValue == false) throw new Exception("Başlangıç tarihi boş geçilemez..!");

            if (ariza.FarkDakika.HasValue == false) throw new Exception("Süre boş geçilemez..!");

            ariza.BitisTarihi = ariza.BaslangicTarihi.Value.AddMinutes(ariza.FarkDakika.Value);
            if (ariza.Id != 0) return false;
            return new DBEvents().SaveGeneric<tblTezgahArizalari>(ariza.ViewToTbl());
        }

        public static bool TezgahArizaSil(vTezgahArizalari ariza)
        {
            return new DBEvents().DeleteGeneric<tblTezgahArizalari>(ariza.ViewToTbl());
        }

        public static List<tblTezgahArizaGrupAct> TezgahArizaTanimlariGetir()
        {
            return new DBEvents().GetGeneric<tblTezgahArizaGrupAct>();
        }

        public static List<vTezgahAtkiGiris> TezgahAtkiGirisleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vTezgahAtkiGiris>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih.AddDays(1));
        }

        public static bool TezgahAtkiGirisiEkle(vTezgahAtkiGiris atkiGiris)
        {
            tblTezgahAtkiGiris tbl = atkiGiris.ViewToTbl();
            bool snc = new DBEvents().SaveGeneric<tblTezgahAtkiGiris>(ref tbl);

            return snc;
        }

        public static bool TezgahAtkiGirisiSil(vTezgahAtkiGiris atkiGiris)
        {
            if (atkiGiris.PlanOteledi == true) return false;
            return new DBEvents().DeleteGeneric<tblTezgahAtkiGiris>(atkiGiris.ViewToTbl());
        }

        public static int TezgahAtkiSonSayaciGetir(int tezgahId, string posta)
        {
            try
            {
                string snc = new DBEvents().GetGenericWithSQLQuery<string>("select top 1 cast(AtkiBitis as varchar) from tblTezgahAtkiGiris "
                 + " where Postasi = '" + posta + "' and TezgahId = " + tezgahId.ToString() + " order by Id desc", new string[0]).FirstOrDefault();

                if (snc == null) return 0;

                return Convert.ToInt32(snc);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime TezgahAtkiSonGirisTarihiGetir()
        {
            string tarihStr = new DBEvents().GetGenericWithSQLQuery<string>("select cast(Tarih as varchar) Tarih from (select top 1 Tarih from tblTezgahAtkiGiris order by Tarih desc) as tbl", new string[0]).FirstOrDefault();
            if (tarihStr == null) return DateTime.Today;

            try
            {
                return Convert.ToDateTime(tarihStr);
            }
            catch (Exception exp)
            {
                return DateTime.Today;
            }
        }

        public static int TezgahAtkiSonDokumaciGetir()
        {
            string dokumaciId = new DBEvents().GetGenericWithSQLQuery<string>("select cast(DokumaciId as varchar) DokumaciId from (select TOP 1  DokumaciId from tblTezgahAtkiGiris  where DokumaciId IS NOT NULL order by Id desc) as tbl", new string[0]).FirstOrDefault();
            
            if (dokumaciId == null) return 0;

            try
            {
                return Convert.ToInt32(dokumaciId);
            }
            catch (Exception exp)
            {
                return 0;
            }
        }
        public static int TezgahaBagliTipGetir(int tezgahId)
        {
            tblLeventHareket levent = new DBEvents().GetGeneric<tblLeventHareket>(c => c.Durum == 21 && c.TezgahId == tezgahId).FirstOrDefault();

            if (levent == null) return 0;
            else return levent.TipId;
        }

        #endregion

        public enum BakimOnarimTurleri {Hicbiri, PeriyodikBakim, ArizaOnarim, Diger };

        public bool MakinaKaydet(List<tblMakinalar> listMakina)
        {
            listMakina = listMakina.FindAll(c => c.Kodu != null);
            if (listMakina == null) return false;
            List<tblMakinalar> saveList = listMakina.FindAll(c => c.Id == 0);
            saveList.ForEach(c => c.AktifMi = true);
            List<tblMakinalar> updateList = listMakina.FindAll(c => c.Id > 0);
            
            bool sonuc = true;
            if (saveList.Count > 0) if (db.SaveGeneric<tblMakinalar>(saveList) == false) sonuc = false;
            if (updateList.Count > 0) if (db.UpdateGeneric<tblMakinalar>(updateList) == false) sonuc = false;

            return sonuc;
        }

        public tblMakinalar MakinaGetir(int makinaId)
        {
            return db.GetGeneric<tblMakinalar>(c => c.Id == makinaId).FirstOrDefault();
        }

        public List<tblMakinalar> MakinalariGetir()
        {
            List<tblMakinalar> list = db.GetGeneric<tblMakinalar>(c => c.BaglantiId != -1 && c.AktifMi == true).OrderBy(o=>o.KodAd).ToList();
            return list != null ? list : new List<tblMakinalar>();
        }

        public List<tblMakinalar> MakinalariGetir(int bagId)
        {
            return db.GetGeneric<tblMakinalar>(c => c.BaglantiId == bagId && c.AktifMi == true);
        }

        public List<vBakimOnarimAct> BakimOnarimMalzemeleriGetir(int bakimOnarimFormId)
        {
            return db.GetGeneric<vBakimOnarimAct>(c => c.BakimOnarimId == bakimOnarimFormId);
        }

        public List<vBakimOnarim> BakimOnarimFormlariGetir(DateTime basTarih, DateTime bitisTarihi)
        {
            return db.GetGeneric<vBakimOnarim>(c => c.BitisTarih >= basTarih && c.BitisTarih <= bitisTarihi);
        }

        public vBakimOnarim BakimOnarimFormuGetir(int formId)
        {
            return db.GetGeneric<vBakimOnarim>(c => c.Id == formId).FirstOrDefault();
        }

        public List<vMakinaBakimTarihleri> SiradakiBakimlariGetir(bool kalanGunSifiraEsitler = true)
        {
            List<vMakinaBakimTarihleri> list;
            if (kalanGunSifiraEsitler) list = db.GetGeneric<vMakinaBakimTarihleri>(c => c.KalanGun <= 0);
            else list = db.GetGeneric<vMakinaBakimTarihleri>();

            return list != null ? list.OrderBy(o => o.KalanGun).ToList() : list;
        }

        public bool BakimOnarimMalzemeKaydet(List<vBakimOnarimAct> listMalzeme)
        {
            List<tblBakimOnarimAct> listTblMalzeme = listMalzeme.ConvertAll<tblBakimOnarimAct>(new Converter<vBakimOnarimAct, tblBakimOnarimAct>(vBakimOnarimAct.ViewToTable));
            List<tblBakimOnarimAct> updateList = listTblMalzeme.FindAll(c => c.Id > 0);
            List<tblBakimOnarimAct> saveList = listTblMalzeme.FindAll(c => c.Id == 0);

            bool sonuc = true;
            if (updateList.Count > 0) if (db.UpdateGeneric<tblBakimOnarimAct>(updateList) == false) sonuc = false;
            if (saveList.Count > 0) if (db.SaveGeneric<tblBakimOnarimAct>(saveList) == false) sonuc = false;

            return sonuc;
        }

        public bool BakimOnarimMalzemeSil(vBakimOnarimAct malzemeKaydi)
        {
            tblBakimOnarimAct tbl = vBakimOnarimAct.ViewToTable(malzemeKaydi);
            return db.DeleteGeneric<tblBakimOnarimAct>(tbl);
        }

        public int BakimOnarimFormKaydet(vBakimOnarim form)
        {
            tblBakimOnarim tbl = vBakimOnarim.ViewToTable(form);

            if (form.Id == 0) return db.SaveGeneric<tblBakimOnarim>(ref tbl) == false ? -1 : tbl.Id;
            else return db.UpdateGeneric<tblBakimOnarim>(tbl) == false ? -1 : tbl.Id;
        }

        /// <summary>
        /// Makina bakım kaydını kaydeder
        /// </summary>
        /// <param name="form">Bakım kayıt formu</param>
        /// <param name="kullanilanMalzemeler">Bakım esnasında kullanılan malzemelerin listesi</param>
        /// <returns></returns>
        public int BakimKaydet(vBakimOnarim form, List<vBakimOnarimAct> kullanilanMalzemeler)
        {
            int sonucId = BakimOnarimFormKaydet(form);
            if (sonucId == -1) return -1;

            kullanilanMalzemeler.ForEach(c => c.BakimOnarimId = sonucId);
            if (BakimOnarimMalzemeKaydet(kullanilanMalzemeler) == true) return sonucId;
            else return -1;
        }
        
        public DateTime SonrakiBakimTarihiGetir(int makinaId)
        {
            DateTime ilkTarih = DateTime.Now;
            tblMakinalar makina = db.GetGeneric<tblMakinalar>(c => c.Id == makinaId).FirstOrDefault();
            DateTime tarih = ilkTarih;
            int tarihCount = 20;
            while (tarihCount != 1)
            {
                tarihCount--;
                tarih = tarih.AddDays(1);
            }

            if (tarih.DayOfWeek == DayOfWeek.Saturday) tarih = tarih.AddDays(2);
            else if (tarih.DayOfWeek == DayOfWeek.Sunday) tarih = tarih.AddDays(1);
            return tarih;
        }
    }
}

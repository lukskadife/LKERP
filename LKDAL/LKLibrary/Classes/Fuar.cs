using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Fuar
    {
        private DBEvents db = new DBEvents();


        public static List<vFuarlar> FuarlariGetir()
        {
            return new DBEvents().GetGeneric<vFuarlar>().OrderByDescending(o => o.FuarBaslangicTarihi).ToList();
        }

        public static bool FuarKaydet(vFuarlar fuar)
        {
            if (fuar == null) return false;
            tblFuarlar tbl = fuar.ViewToTbl();
            if (tbl.Id == 0)
            {
                tblFuarlar kontrol = new DBEvents().GetGeneric<tblFuarlar>(k => k.Kodu == fuar.Kodu).FirstOrDefault();
                if (kontrol != null) throw new Exception("Aynı Kodlu Fuar Daha Önce eklenmiş...!");
                tbl.Kodu = tbl.Kodu;
                tbl.Adi = tbl.Adi;
                tbl.FuarTarihleri = tbl.FuarTarihleri;
                tbl.FuarYili = tbl.FuarYili;
                tbl.HallNo = tbl.HallNo;
                tbl.StandNo = tbl.StandNo;
                return new DBEvents().SaveGeneric<tblFuarlar>(tbl);
            }
            else return new DBEvents().UpdateGeneric<tblFuarlar>(tbl);
            
        }

        public static bool FuarSil(vFuarlar fuar)
        {
            if (fuar == null) return false;
            tblFuarlar fSil = new DBEvents().GetGeneric<tblFuarlar>(f => f.Id == fuar.Id).FirstOrDefault();
            return new DBEvents().DeleteGeneric<tblFuarlar>(fSil);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    //Gökhan 21.08.2014
    public class Ayarlar
    {
        
        public static List<tblAyarlar> KafesKartlariniGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.BaglantiId== 247);
        }

        public static bool KafesKartlariniKaydet(tblAyarlar item)
        {
            item.BaglantiId = 247;

            DBEvents db = new DBEvents();

            return db.SaveGeneric<tblAyarlar>(item);
        }

        public static bool KafesKartlariniKaydet(List<tblAyarlar> list)
        {
            foreach (var item in list) item.BaglantiId = 247;

            DBEvents db = new DBEvents();

            return db.SaveGeneric<tblAyarlar>(list);
        }


        public static bool KafesKartniDuzelt(tblAyarlar item)
        {
            if (item.BaglantiId != 247) item.BaglantiId = 247;

            DBEvents db = new DBEvents();

            return db.UpdateGeneric<tblAyarlar>(item);
        }

        public static bool KafesKartniSil(tblAyarlar item)
        {
            if (item.BaglantiId != 247) item.BaglantiId = 247;

            DBEvents db = new DBEvents();

            return db.DeleteGeneric<tblAyarlar>(item);
        }

    }
}

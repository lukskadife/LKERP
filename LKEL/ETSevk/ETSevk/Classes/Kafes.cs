using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETSevk.Classes
{
    //Gökhan 21.08.2014
    public class Kafes
    {
        DBEvents db = new DBEvents();

        public  bool KafesVarMi(string barkod)
        {
            return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 247 & c.Deger == barkod).Any();
        }

        public int KafesIdGetir(string barkod)
        {
            return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 247 & c.Deger == barkod).FirstOrDefault().Id;
        }

        public bool KafesAta(string kafesBarkod, string hamBarkod, string kafesDikeyKodu)
        {
            tblHamKumaslar item = new tblHamKumaslar();
            item = db.GetGeneric<tblHamKumaslar>(c => c.PartiId == null & c.Barkod == hamBarkod).FirstOrDefault();
            item.KafesId = KafesIdGetir(kafesBarkod);
            item.KafesDikeyKodu = kafesDikeyKodu;
            return db.UpdateGeneric<tblHamKumaslar>(item);
        }

        public bool SevkEdilmemisBarkodVarMi(string barkod)
        {
            return db.GetGeneric<tblHamKumaslar>(c => c.PartiId == null & c.Barkod == barkod).Any();
        }
    }
}

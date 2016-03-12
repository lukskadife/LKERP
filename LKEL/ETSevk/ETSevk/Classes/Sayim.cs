using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETSevk.Classes
{
    public class Sayim
    {
        private List<dynamic> Barkodlar { get; set; }
        
        public enum SayimTipi { Mamul, Ham };
        public SayimTipi BarkodTipi;

        public int OkutulanCount
        {
            get { return Barkodlar.Count; }
        }

        public double OkutulanToplamMetre
        {
            get {
                if (BarkodTipi == SayimTipi.Mamul) return Math.Round((Barkodlar.Cast<vSayimMamul>()).Sum<vSayimMamul>(s => s.Metre), 2);
                else if (BarkodTipi == SayimTipi.Ham) return Math.Round((Barkodlar.Cast<vSayimHam>()).Sum<vSayimHam>(s => s.Metre), 2);
                return 0;
            }
        }

        public dynamic SonOkutulan { get; set; }

        private DBEvents db = new DBEvents();

        public Sayim(SayimTipi barkodTip, bool oncekiSayimSilinsinMi = false)
        {
            BarkodTipi = barkodTip;

            if (oncekiSayimSilinsinMi)
            {
                if (BarkodTipi == SayimTipi.Mamul) db.GetGenericWithSQLQuery<string>("update tblMamulKumaslar set SayimIndisi = null", new string[0]);
                if (BarkodTipi == SayimTipi.Ham) db.GetGenericWithSQLQuery<string>("update tblHamKumaslar set SayimIndisi = null", new string[0]);
            }

            if (BarkodTipi == SayimTipi.Mamul)
            {
                Barkodlar = db.GetGeneric<vSayimMamul>().ToList<dynamic>();
                if (Barkodlar == null) Barkodlar = new List<vSayimMamul>().ToList<dynamic>();
            }

            else if (BarkodTipi == SayimTipi.Ham)
            {
                Barkodlar = db.GetGeneric<vSayimHam>().ToList<dynamic>();
                if (Barkodlar == null) Barkodlar = new List<vSayimHam>().ToList<dynamic>();
            }
        }

        public bool BarkodOkut(string barkod)
        {
            if (BarkodTipi == SayimTipi.Mamul) return MamulBarkodOkut(barkod);
            else if (BarkodTipi == SayimTipi.Ham) return HamBarkodOkut(barkod);

            return false;
        }

        public bool BarkodOkut(string barkod,int kafesId, string kafesDikeyKodu)
        {

            if (BarkodTipi == SayimTipi.Ham) return HamBarkodOkut(barkod, kafesId, kafesDikeyKodu);

            return false;
        }

        private bool MamulBarkodOkut(string barkod)
        {
            tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (mamul == null || barkod == "") throw new Exception("Barkod bulunamadı..!");
            if (mamul.SevkId != 0) throw new Exception("Sevk edilmiş..!");
            if (mamul.SayimIndisi != null && mamul.SayimIndisi != 0) throw new Exception("Daha önce okutuldu..!");

            switch (mamul.Durum)
            {
                case "ReProcess": throw new Exception("Mamul reprocess'te.");

                case "BoyaSepeti": throw new Exception("Mamul boyahane sepetinde.");

                case "Kesilen": throw new Exception("Mamul kesilmiş.");

                case "Silindi": throw new Exception("Mamul silinmiş.");
            }

            mamul.SayimIndisi = this.Barkodlar.Count + 1;
            if (db.UpdateGeneric<tblMamulKumaslar>(mamul))
            {
                vSayimMamul sayimBarkod = db.GetGeneric<vSayimMamul>(c => c.MamulId == mamul.Id).FirstOrDefault();
                if (sayimBarkod == null) return false;
                this.SonOkutulan = sayimBarkod;
                Barkodlar.Add(sayimBarkod);
                return true;
            }

            return false;
        }

        private bool HamBarkodOkut(string barkod)
        {
            tblHamKumaslar Ham = db.GetGeneric<tblHamKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (Ham == null) throw new Exception("Barkod bulunamadı..!");
            if (Ham.PartiId != null) throw new Exception("Partilenmiş..!");
            if (Ham.SayimIndisi != null && Ham.SayimIndisi != 0) throw new Exception("Daha önce okutuldu..!");

            Ham.SayimIndisi = this.Barkodlar.Count + 1;
            if (db.UpdateGeneric<tblHamKumaslar>(Ham))
            {
                vSayimHam sayimBarkod = db.GetGeneric<vSayimHam>(c => c.HamId == Ham.Id).FirstOrDefault();
                if (sayimBarkod == null) return false;
                this.SonOkutulan = sayimBarkod;
                Barkodlar.Add(sayimBarkod);
                return true;
            }

            return false;
        }

        private bool HamBarkodOkut(string barkod,int kafesId, string kafesDikeyKodu)
        {
            tblHamKumaslar Ham = db.GetGeneric<tblHamKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (Ham == null) throw new Exception("Barkod bulunamadı..!");
            if (Ham.PartiId != null) throw new Exception("Partilenmiş..!");
            if (Ham.SayimIndisi != null && Ham.SayimIndisi != 0) throw new Exception("Daha önce okutuldu..!");

            Ham.SayimIndisi = this.Barkodlar.Count + 1;
            Ham.KafesId = kafesId;
            Ham.KafesDikeyKodu = kafesDikeyKodu;
            if (db.UpdateGeneric<tblHamKumaslar>(Ham))
            {
                vSayimHam sayimBarkod = db.GetGeneric<vSayimHam>(c => c.HamId == Ham.Id).FirstOrDefault();
                if (sayimBarkod == null) return false;
                this.SonOkutulan = sayimBarkod;
                Barkodlar.Add(sayimBarkod);
                return true;
            }

            return false;
        }
    }
}

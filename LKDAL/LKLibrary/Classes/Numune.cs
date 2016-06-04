using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Numune
    {
        private DBEvents db = new DBEvents();
        //private DBEvents db = new DBEvents();


        public static List<vNumuneKumaslar> NumuneEklenenKumaslariGetir()
        {
            return new DBEvents().GetGeneric<vNumuneKumaslar>(n => n.Miktar > 0).OrderBy(o => o.TipNo).ToList();
        }


        public static List<tblKumas> TipNumaralariGetir()
        {
            return new DBEvents().GetGeneric<tblKumas>(k => k.AktifMi == true).OrderBy(o => o.TipNo).ToList();
        }

        public static List<tblAyarlar> NumuneBirimleriGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(a => a.BaglantiId == 12).OrderBy(o => o.Sira).ToList();
        }

        public static List<tblAyarlar> NumuneTurunuGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(t => t.BaglantiId == 449).OrderBy(o => o.Sira).ToList();
        }

        public static List<tblAyarlar> KoleksiyonlariGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(a => a.BaglantiId == 455).OrderBy(o => o.Sira).ToList();
        }

        public static bool NumuneGirisKaydet(vNumuneKumaslar numune, int kullaniciId)
        {
            if (numune == null) return false;
            tblNumuneKumaslar tbl = numune.ViewToTbl();
            if (tbl.Id == 0)
            {
                tbl.TipId = tbl.TipId;
                tbl.Varyant = tbl.Varyant;
                tbl.RenkNo = tbl.RenkNo;
                tbl.FasonIslemKodu = tbl.FasonIslemKodu;
                tbl.Koleksiyon = tbl.Koleksiyon;
                tbl.Miktar = tbl.Miktar;
                tbl.BirimId = tbl.BirimId;
                tbl.TrCode = tbl.TrCode;
                tbl.FuarId = tbl.FuarId;
                tbl.Aciklama = tbl.Aciklama;
                tbl.EkleyenKullaniciId = kullaniciId;
                tbl.SonDegistirenKullaniciId = tbl.SonDegistirenKullaniciId;
                tbl.EklenmeTarihi = DateTime.Now;
                tbl.KafesNo = tbl.KafesNo;
                tbl.KafesAltNo = tbl.KafesAltNo;
                tbl.KafesSiraNo = tbl.KafesSiraNo;
                tbl.Finish = tbl.Finish;

                return new DBEvents().SaveGeneric<tblNumuneKumaslar>(tbl);
            }
            else return new DBEvents().UpdateGeneric<tblNumuneKumaslar>(tbl);

        }


        public static bool BarkodluNumuneKaydet(vNumuneKumaslar numuneB, int kullaniciId, string gelenBarkod)
        {
            if (numuneB == null) return false;
            if (gelenBarkod == null) return false;

            tblNumuneKumaslarBarkodlu tblB = numuneB.ViewToTblBarkodlu();
            if (tblB.Id == 0)
            {
                tblB.MamulId = BarkotunIdsiniAl(gelenBarkod);
                tblB.BirimId = 448;
                tblB.TrCode = 450;
                tblB.FuarId = tblB.FuarId;
                tblB.Aciklama = tblB.Aciklama;
                tblB.EkleyenKullaniciId = kullaniciId;
                tblB.SonDegistirenKullaniciId = tblB.SonDegistirenKullaniciId;
                tblB.EklenmeTarihi = DateTime.Now;
                tblB.KafesNo = tblB.KafesNo;
                tblB.KafesAltNo = tblB.KafesAltNo;
                tblB.KafesSiraNo = tblB.KafesSiraNo;
                tblB.Finish = tblB.Finish;
                return new DBEvents().SaveGeneric<tblNumuneKumaslarBarkodlu>(tblB);
                
             }
            else return new DBEvents().UpdateGeneric<tblNumuneKumaslarBarkodlu>(tblB);
        }

        public static bool MamulBarkoduGuncelle(string gelenBarkod)
        {
            //bool snc = false;
            tblMamulKumaslar okulutanBarkod = new DBEvents().GetGeneric<tblMamulKumaslar>(s => s.Barkod == gelenBarkod /*&& s.SevkId==0*/).FirstOrDefault();
            if (okulutanBarkod == null)
            {
                tblMamulKumaslar mamul = new DBEvents().GetGeneric<tblMamulKumaslar>(c => c.Barkod == gelenBarkod).FirstOrDefault();
                if (mamul == null) throw new Exception("Barkod bulunamadı..!");
                if (mamul.SevkId != null && mamul.SevkId != 0) throw new Exception("Daha önce sevk edilmiş..! Bu Barkod Stokta Değil...!");
                throw new Exception("Barkod bulunamadı..!");
                return false;
            }
            else
            {
                okulutanBarkod.Durum = "Kartela";
                okulutanBarkod.SevkId = -1;
                new DBEvents().UpdateGeneric<tblMamulKumaslar>(okulutanBarkod);
                return true;
            }
        }

        public static int BarkotunIdsiniAl(string gelenBarkod)
        {
            vMamulKumaslar sBarkod = new DBEvents().GetGeneric<vMamulKumaslar>(m => m.Barkod == gelenBarkod).FirstOrDefault();
            if (sBarkod == null) return 0;
            else
                return sBarkod.Id;           
        }

        public static bool NumuneTalepKaydet(vNumuneTalepleri ntalep, int kullaniciId)
        {
            if (ntalep == null) return false;
            tblNumuneTalepleri tbl = ntalep.ViewToTbl();
            if (tbl.Id == 0)
            {
                //tbl.Id = tbl.Id;
                tbl.NumuneId = tbl.NumuneId;
                tbl.Miktar = tbl.Miktar;
                tbl.MusteriId = tbl.MusteriId;
                tbl.YeniMusteriAdi = tbl.YeniMusteriAdi;
                tbl.FuarId = tbl.FuarId;
                tbl.TalepTarihi = DateTime.Now;
                tbl.TalepEdenKullaniciId = kullaniciId;
                tbl.GonderildiMi = tbl.GonderildiMi;
                tbl.GonderimTarihi = tbl.GonderimTarihi;

                return new DBEvents().SaveGeneric<tblNumuneTalepleri>(tbl);
            }
            else return new DBEvents().UpdateGeneric<tblNumuneTalepleri>(tbl);
        }
    }
    
}

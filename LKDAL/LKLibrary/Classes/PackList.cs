using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;
using System.Data.Linq.Mapping;

namespace LKLibrary.Classes
{
    public class PackList
    {
        [Table(Name="tblAyarlar")]
        public class Paket
        {
            [Column]
            public int Id { get; set; }

            [Column]
            public int BaglantiId { get; set; }

            [Column]
            public string Adi { get; set; }

            [Column]
            public string Deger { get; set; }

            public int Adet { get; set; }
        }

        private tblSevk _SevkBelge;
        public vSiparisler SiparisBelge { get; set; }
        public List<vPackList> Barkodlar { get; set; }
        public List<Paket> Paketler { get; set; }

        private DBEvents db = new DBEvents();

        public PackList(int sevkId)
        {
            _SevkBelge = db.GetGeneric<tblSevk>(c=>c.Id == sevkId).FirstOrDefault();
            SiparisBelge = db.GetGeneric<vSiparisler>(c => c.Id == _SevkBelge.SiparisId).FirstOrDefault();
            Barkodlar = db.GetGenericWithSQLQuery<vPackList>("select * from vPackList where SevkId = {0} order by TipNo, RenkNo", new string[] { sevkId.ToString() });
            Paketler = db.GetGeneric<Paket>(c => c.BaglantiId == 173);
            if (Barkodlar != null && Barkodlar.Count != 0) Barkodlar.ForEach(c => c.KutuId = (Barkodlar.IndexOf(c) + 1));
        }

        public void PaketAgirliklariDagit()
        {
            if (Barkodlar == null || Barkodlar.Count == 0) return;

            List<Paket> secilenPaketler = Paketler.FindAll(c => c.Adet > 0);
            if (secilenPaketler == null || secilenPaketler.Count == 0) throw new Exception("Hiç paket seçilmemiş..!");

            double toplamPaketAgirlik = 0, barkodBasinaAgirlik = 0;
            try
            {
                toplamPaketAgirlik = secilenPaketler.Sum(s => s.Adet * Convert.ToDouble(s.Deger));
                barkodBasinaAgirlik = (double)(toplamPaketAgirlik / Barkodlar.Count);
            }
            catch
            {
                throw new Exception("Paket ağırlık girişi yanlış olan veri var.\n\nBrüt ağırlıklar hesaplanamadı..!");
            }

            Barkodlar.ForEach(c => c.BrutAgirlik = Math.Round((c.Kg + barkodBasinaAgirlik), 2));
        }

        public bool Kaydet()
        {
            if (this.Barkodlar == null || this.Barkodlar.Count == 0) return false;

            //önceki kayıtlar silinir, yeniden kayıt yapılır
            if (db.DeleteGeneric<tblPackAliases>(db.GetGeneric<tblPackAliases>(c => c.SevkId == this._SevkBelge.Id)))
                //alias girilen kayıtlar tabloya kayıt edilir.
                return db.SaveGeneric<tblPackAliases>(vPackList.ViewToTbl(this.Barkodlar.FindAll(f => f.TipAlias != null || f.RenkAlias != null || f.RollAlias != null)));

            return false;
        }
    }
}

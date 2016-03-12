using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Levent
    {
        public long SetId;
        public List<vLeventIplik> KullanilanIplikler = new List<vLeventIplik>();
        public List<vLeventHareket> Leventler = new List<vLeventHareket>();
        public int DurumId;
        private DBEvents db = new DBEvents();

        #region statics

        public static List<vLeventHareket> LeventleriGetir(int durumId = 0)
        {
            var query = PredicateBuilder.True<vLeventHareket>();
            if (durumId != 0) query = query.And(c => c.Durum == durumId);

            return new DBEvents().GetGeneric<vLeventHareket>(query);
        }

        public static bool DurumDegistir(vLeventHareket levent, int yeniDurumId)
        {
            //if (yeniDurumId == 21 && new DBEvents().GetGeneric<tblLeventHareket>(c => c.Durum == yeniDurumId && c.TezgahId == levent.TezgahId && c.Cozgu == levent.Cozgu).Count > 0)
            //    throw new Exception("Tezgahta dügüm atılan başka levent var");
            if (yeniDurumId == 20) levent.CozguTarihi = DateTime.Now;
            else if (yeniDurumId == 21) levent.DugumTarihi = DateTime.Now;
            else if (yeniDurumId == 22) levent.TamamlanmaTarihi = DateTime.Now;

            if (yeniDurumId == 21 && new DBEvents().GetGeneric<tblLeventHareket>(c => c.Durum == yeniDurumId && c.LeventNo == levent.LeventNo).Count > 0)
                throw new Exception(levent.LeventNo + " numaralı leventten düğüm atıldıda zaten var !");

            levent.Durum = yeniDurumId;
            return new DBEvents().UpdateGeneric<tblLeventHareket>(levent.TblLevent());

        }

        public static vLeventHareket IadeAl(vLeventHareket levent)
        {
            vLeventHareket iadeLevent = levent.CopyToNewObject();

            iadeLevent.Id = 0;
            iadeLevent.Metre = levent.KalanMetre;
            iadeLevent.Durum = 19;
            iadeLevent.Tarih = DateTime.Today;
            iadeLevent.CozguTarihi = null;
            iadeLevent.DugumTarihi = null;
            iadeLevent.TamamlanmaTarihi = null;
            iadeLevent.IadeMi = true;

            return iadeLevent;
        }

        public static List<tblDurumlar> DurumlariGetir()
        {
            return new DBEvents().GetGeneric<tblDurumlar>(c => c.AyarId == 81);
        }

        public static List<tblKumas> KumaslariGetir()
        {
            return new DBEvents().GetGeneric<tblKumas>().OrderBy(o => o.TipNo).ToList();
        }

        public static List<tblPersoneller> CozguPersoneliGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 10 && c.AktifMi == true);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iplikler"></param>
        /// <param name="leventler"></param>
        /// <param name="levent">null'dan farklı ise düzenleme modunda açılır. Yani leventler, iplikler de yüklenmiş olur. </param>
        public Levent(vLeventHareket levent, List<vIplikStok> iplikler = null, List<vLeventHareket> leventler = null)
        {
            if (levent != null)
            {
                this.DurumId = levent.Durum;
                this.KullanilanIplikler = db.GetGeneric<vLeventIplik>(c => c.SetId == levent.SetId);

                this.Leventler = new List<vLeventHareket>();
                if (levent.IadeMi) this.Leventler = db.GetGeneric<vLeventHareket>(c => c.Id == levent.Id);
                else this.Leventler = db.GetGeneric<vLeventHareket>(c => c.SetId == levent.SetId && c.IadeMi == false);

                this.SetId = levent.SetId == 0 ? Convert.ToInt64(DateTime.Now.ToString("yyMMddhhmmssfff")) : levent.SetId;
                return;
            }
        }

        private vLeventIplik VIplikStokToVLeventIplik(vIplikStok iplikStok, int hareketId)
        {
            return new vLeventIplik()
            {
                Ambalaj = iplikStok.Ambalaj,
                BobinSayisi = iplikStok.BobinSayisi,
                IplikAdi = iplikStok.Adi,
                IplikKodu = iplikStok.Kodu,
                LotNo = iplikStok.LotNo,
                MalzemeId = iplikStok.MalzemeId,
                NetKg = iplikStok.NetKg,
                RenkAdi = iplikStok.RenkAdi,
                RenkId = iplikStok.RenkId,
                Satici = iplikStok.Satici,
                SaticiId = iplikStok.SaticiId,
                SetHareketId = hareketId,
                SetId = this.SetId,
                Id = 0
            };
        }

        internal List<vLeventIplik> VIplikStokToVLeventIplik(List<vIplikStok> iplikler, int indis)
        {
            List<vLeventIplik> yeniList = new List<vLeventIplik>();

            foreach (vIplikStok item in iplikler)
            {
                yeniList.Add(VIplikStokToVLeventIplik(item, indis));
                indis++;
            }

            return yeniList;
        }

        public vLeventHareket LeventKaydet(vLeventHareket levent)
        {
            if (levent.Id == 0) 
            {
                tblLeventHareket tbl = levent.TblLevent();
                if (db.SaveGeneric<tblLeventHareket>(ref tbl) == false) return null;
                else {
                    levent.Id = tbl.Id;
                    return levent;
                }
            }
            else
            { 
                if (db.UpdateGeneric<tblLeventHareket>(levent.TblLevent()) == false) return null;
                else return levent;
            }
        }

        public bool LeventleriKaydet()
        {
            if (IplikleriKaydet() == false) return false;

            bool snc = true;

            List<tblLeventHareket> toSaveList = vLeventHareket.ViewToTbl(this.Leventler.FindAll(c => c.Id == 0));
            List<tblLeventHareket> toUpdList = vLeventHareket.ViewToTbl(this.Leventler.FindAll(c => c.Id != 0));

            if (toSaveList.Count > 0) if (db.SaveGeneric<tblLeventHareket>(toSaveList) == false) snc = false;
            if (toUpdList.Count > 0) if (db.SaveGeneric<tblLeventHareket>(toUpdList) == false) snc = false;

            if (snc) this.Leventler = db.GetGeneric<vLeventHareket>(c => c.SetId == this.SetId);

            return snc;
        }

        public void KullanilanIplikEkle(List<vIplikStok> iplikler)
        {
            this.KullanilanIplikler.AddRange(VIplikStokToVLeventIplik(iplikler, this.KullanilanIplikler.Count + 1));
        }

        /// <summary>
        /// verilen leventi this.Leventler list'i içerisine ekler. Ekleme başarılı ise true; levent kullanımda ise 1, tezgah kullanımda ise 2 dönderir.
        /// </summary>
        /// <param name="levent"></param>
        /// <returns></returns>
        public dynamic LeventEkle(vLeventHareket levent)
        {
            //levent numarasına göre leventin kullanımda olup olmadığı kontrol ediliyor
            //if (db.GetGeneric<tblLeventHareket>(c => c.Durum != 22 && c.LeventNo == levent.LeventNo).Count != 0 || this.Leventler.Exists(c => c.LeventNo == levent.LeventNo))
            //    return 1;

            levent.SetId = this.SetId;
            levent.Tarih = DateTime.Today.Date;
            levent.IadeMi = false;
            this.Leventler.Add(levent);
            return true;
        }

        private bool IplikleriKaydet()
        {
            List<tblMalzemeCikis> cikis = vLeventIplik.ViewToTblCikis(this.KullanilanIplikler);

            List<tblMalzemeCikis> toSave = cikis.FindAll(c => c.Id == 0);
            List<tblMalzemeCikis> toUpd = cikis.FindAll(c => c.Id != 0);

            bool snc = true;
            if (toSave.Count > 0) if (db.SaveGeneric<tblMalzemeCikis>(toSave) == false) snc = false;
            if (toUpd.Count > 0) if (db.UpdateGeneric<tblMalzemeCikis>(toUpd) == false) snc = false;

            if (snc) this.KullanilanIplikler = db.GetGeneric<vLeventIplik>(c => c.SetId == this.SetId);

            return snc;
        }

        public double? BantSayisiHesapla(vLeventHareket hareket)
        {
            if (hareket == null || hareket.BobinAdedi.HasValue == false || hareket.BobinAdedi.Value == 0 || hareket.TelAdedi.HasValue == false || hareket.TelAdedi.Value == 0) return 0;
            return Math.Round((1 + (hareket.TelAdedi.Value / hareket.BobinAdedi.Value)), 2);
        }

        public bool LeventleriSil()
        {
            if (this.Leventler.FindAll(c=>c.Id != 0).Count == 0)
            {
                this.Leventler = new List<vLeventHareket>();
                return true;
            }
            if (db.DeleteGeneric<tblMalzemeCikis>(db.GetGeneric<tblMalzemeCikis>(c => c.SetId == this.SetId)) == true)
            {
                this.KullanilanIplikler.ForEach(c => c.Id = 0);
                if (db.DeleteGeneric<tblLeventHareket>(vLeventHareket.ViewToTbl(this.Leventler)))
                {
                    this.Leventler = new List<vLeventHareket>();
                    return true;
                }
            }

            return false;
        }

        
    }
}

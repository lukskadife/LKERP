using System;
using System.Collections.Generic;
using System.Linq;
using LKLibrary.DbClasses;
using System.Linq.Expressions;


namespace LKLibrary.Classes
{
    public class Siparis
    {
        DBEvents db = new DBEvents();

        public List<tblFirmalar> MusterileriGetir(string adFiltre = "")
        {
            if (string.IsNullOrEmpty(adFiltre) == false) 
                return db.GetGeneric<tblFirmalar>(c => c.BaglantiId == 3 && c.Adi.ToUpper().Contains(adFiltre));
            return db.GetGeneric<tblFirmalar>(c => c.BaglantiId == 3);
        }

        public bool FiyatKaydet(List<vFiyatListeleri> yeniFiyat)
        {
            List<vFiyatListeleri> saveList = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in yeniFiyat)
            {
                vFiyatListeleri kontrol = db.GetGeneric<vFiyatListeleri>(c => c.Id == fiyat.Id).FirstOrDefault();
                if ((kontrol != null && fiyat.Aciklama == kontrol.Aciklama && fiyat.DovizId == kontrol.DovizId && fiyat.Fiyat == kontrol.Fiyat
                    && fiyat.Yil == kontrol.Yil && fiyat.Ay == kontrol.Ay && fiyat.Tip == kontrol.Tip && fiyat.MusteriId == kontrol.MusteriId) == false)
                {
                    fiyat.Id = 0;
                    fiyat.AktifMi = true;
                    fiyat.Tarih = DateTime.Now;

                    saveList.Add(fiyat);
                }
            }

            return db.SaveGeneric<tblFiyatListeleri>(vFiyatListeleri.ViewToTable(saveList));
        }

        public bool FiyatSil(vFiyatListeleri silinecek)
        {
            return db.DeleteGeneric<tblFiyatListeleri>(vFiyatListeleri.ViewToTable(silinecek));
        }

        //public List<vFiyatListeleri> MusteriFiyatListesiGetir()
        //{
        //    return db.GetGeneric<vFiyatListeleri>(c=>c.AktifMi == true && c.MusteriId.HasValue && c.ProcessId.HasValue == false);
        //}

        public List<vFiyatListeleri> MusteriFiyatListesiGetir(int yil, int ay)
        {
            if (yil == 0 || ay == 0) return null;
            List<vFiyatListeleri> fiyatlar = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in db.GetGeneric<vFiyatListeleri>(c => c.Yil == yil && c.Ay == ay && c.MusteriId.HasValue && c.ProcessId.HasValue == false).OrderByDescending(t => t.Tarih))
            {
                if (fiyatlar.Exists(c => c.MusteriId == fiyat.MusteriId && c.Tip == fiyat.Tip)) continue;
                fiyatlar.Add(fiyat);
            }

            return fiyatlar;
        }

        public List<vFiyatListeleri> MusteriFiyatListesiGetir(int yil)
        {
            if (yil == 0) return null;
            List<vFiyatListeleri> fiyatlar = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in db.GetGeneric<vFiyatListeleri>(c => c.Yil == yil && c.MusteriId.HasValue && c.ProcessId.HasValue == false).OrderByDescending(t => t.Tarih))
            {
                if (fiyatlar.Exists(c => c.MusteriId == fiyat.MusteriId && c.Tip == fiyat.Tip)) continue;
                fiyatlar.Add(fiyat);
            }

            return fiyatlar;
        }

        public List<vFiyatListeleri> SabitFiyatListesiGetir(int yil, int ay)
        {
            if (yil == 0 || ay == 0) return null;
            List<vFiyatListeleri> fiyatlar = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in db.GetGeneric<vFiyatListeleri>(c => c.Yil == yil && c.Ay == ay && c.MusteriId.HasValue == false && c.ProcessId.HasValue == false).OrderByDescending(t => t.Tarih))
            {
                if (fiyatlar.Exists(c => c.Tip == fiyat.Tip)) continue;
                fiyatlar.Add(fiyat);
            }

            return fiyatlar;
        }

        //public List<vFiyatListeleri> SabitFiyatListesiGetir()
        //{
        //    return db.GetGeneric<vFiyatListeleri>(c => c.MusteriId.HasValue == false && c.AktifMi == true && c.ProcessId.HasValue == false);
        //}

        //public List<vFiyatListeleri> MusteriProsesFiyatListesiGetir()
        //{
        //    return db.GetGeneric<vFiyatListeleri>(c => c.AktifMi == true && c.MusteriId.HasValue && c.ProcessId.HasValue);
        //}

        public List<vFiyatListeleri> MusteriProsesFiyatListesiGetir(int yil, int ay)
        {
            if (yil == 0 || ay == 0) return null;
            List<vFiyatListeleri> fiyatlar = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in db.GetGeneric<vFiyatListeleri>(c => c.Yil == yil && c.Ay == ay && c.MusteriId.HasValue && c.ProcessId.HasValue).OrderByDescending(t => t.Tarih))
            {
                if (fiyatlar.Exists(c => c.MusteriId == fiyat.MusteriId && c.Tip == fiyat.Tip)) continue;
                fiyatlar.Add(fiyat);
            }

            return fiyatlar;
        }

        //public List<vFiyatListeleri> ProsesFiyatListesiGetir()
        //{
        //    return db.GetGeneric<vFiyatListeleri>(c => c.AktifMi == true && c.MusteriId.HasValue == false && c.ProcessId.HasValue);
        //}

        public List<vFiyatListeleri> ProsesFiyatListesiGetir(int yil, int ay)
        {
            if (yil == 0 || ay == 0) return null;
            List<vFiyatListeleri> fiyatlar = new List<vFiyatListeleri>();

            foreach (vFiyatListeleri fiyat in db.GetGeneric<vFiyatListeleri>(c => c.Yil == yil && c.Ay == ay && c.MusteriId.HasValue == false && c.ProcessId.HasValue).OrderByDescending(t => t.Tarih))
            {
                if (fiyatlar.Exists(c => c.MusteriId == fiyat.MusteriId && c.Tip == fiyat.Tip)) continue;
                fiyatlar.Add(fiyat);
            }

            return fiyatlar;
        }

        public List<tblSiparisler> SiparisTipleriGetir()
        {
            return db.GetGeneric<tblSiparisler>(c => c.BaglantiId == -1);
        }

        public List<vSiparisler> SiparisleriGetir(string durum = "", int sipTipId = 1)
        {
            var query = PredicateBuilder.True<vSiparisler>();

            if (string.IsNullOrEmpty(durum) == false) query = query.And(c => c.Durum == durum);
            if (sipTipId != 1) query = query.And(c => c.BaglantiId == sipTipId);

            return db.GetGeneric<vSiparisler>(query);
        }

        //02.07.2014
        public List<vSiparisler> AcikTamamlandiSiparisleriGetir(string durum = "", int sipTipId = 1)
        {
            return db.GetGeneric<vSiparisler>(c => c.BaglantiId == 2 && (c.Durum == "Açık" || c.Durum == "Tamamlandı"));
        }

        public vSiparisler SiparisGetir(int siparisId)
        {
            return db.GetGeneric<vSiparisler>(c => c.Id == siparisId).FirstOrDefault();
        }

        public List<vSiparisAct> SiparisUrunleriGetir(int siparisId)
        {
            List<vSiparisAct> list = db.GetGeneric<vSiparisAct>(c => c.SiparisId == siparisId);

            foreach (vSiparisAct item in list)
            {
                tblSiparisTestleri test = db.GetGeneric<tblSiparisTestleri>(c => c.Id == item.TestId).FirstOrDefault();

                item.Testler = test == null ? new tblSiparisTestleri() : test;
            }

            return list;
        }

        public List<vSiparisAct> SiparisUrunleriGetir(string durum)
        {
            return db.GetGeneric<vSiparisAct>(c => c.Durum == durum);
        }

        public vSiparisAct SiparisUrunGetir(int siparisActId)
        {
            return db.GetGeneric<vSiparisAct>(c => c.Id == siparisActId).FirstOrDefault();
        }

        public bool SiparisUrunSil(vSiparisAct urun)
        {
            return db.DeleteGeneric<tblSiparisAct>(vSiparisAct.ViewToTable(urun));
        }

        public bool SiparisSil(vSiparisler silinecek)
        {
            List<tblSiparisAct> siparisList = db.GetGeneric<tblSiparisAct>(c => c.SiparisId == silinecek.Id);

            if (db.DeleteGeneric<tblSiparisAct>(siparisList))
            {
                tblSiparisler tbl = db.GetGeneric<tblSiparisler>(c => c.Id == silinecek.Id).FirstOrDefault();
                return db.DeleteGeneric<tblSiparisler>(tbl);
            }
            return false;
        }

        private bool TestleriKaydet(ref List<vSiparisAct> urunler)
        {
            bool snc = true;

            foreach (vSiparisAct item in urunler)
            {
                if (item.Testler == null) continue;
                if (item.Testler.Id == 0)
                {
                    tblSiparisTestleri test = item.Testler;
                    if (db.SaveGeneric<tblSiparisTestleri>(ref test))
                    {
                        item.TestId = test.Id;
                        item.Testler.Id = test.Id;
                    }
                    else snc = false;
                }
                else if (db.UpdateGeneric<tblSiparisTestleri>(item.Testler) == false) snc = false; ;
            }

            return snc;
        }

        public bool SiparisUrunKaydet(List<vSiparisAct> urunler, int sipId)
        {
            if (TestleriKaydet(ref urunler) == false) return false;

            urunler.FindAll(c => c.Id != 0 && c.Durum == "Kapalı" && c.KapanmaTarihi.HasValue == false).ForEach(a => a.KapanmaTarihi = DateTime.Now);
            urunler.FindAll(c => c.Durum == "Açık").ForEach(a => a.KapanmaTarihi = null);

            List<tblSiparisAct> tblToSave = vSiparisAct.ViewToTable(urunler.FindAll(c => c.Id == 0));
            List<tblSiparisAct> tblToUpd = vSiparisAct.ViewToTable(urunler.FindAll(c => c.Id != 0));

            tblToSave.ForEach(c => c.SiparisId = sipId);

            if (tblToSave.Count > 0) if (db.SaveGeneric<tblSiparisAct>(tblToSave) == false) return false;
            if (tblToUpd.Count > 0) if (db.UpdateGeneric<tblSiparisAct>(tblToUpd) == false) return false;

            return true;
        }

        public void SiparisKarZararKaydet(int siparisId, string karZarar, int yuzde)
        {
            tblSiparisler sprs = db.GetGeneric<tblSiparisler>(c => c.Id == siparisId).FirstOrDefault();
            sprs.KarZarar = karZarar;
            sprs.Yuzde = yuzde;
            db.UpdateGeneric<tblSiparisler>(sprs);
        }

        public bool SiparisKaydet(ref vSiparisler siparis, int kullaniciId)
        {
            tblSiparisler tblSip = vSiparisler.ViewToTable(siparis);
            tblSip.GuncelleyenPersId = kullaniciId;
            try
            {
                tblSip.OrderSayi = Convert.ToInt32(tblSip.OrderNo.Substring(tblSip.OrderNo.IndexOf('-') + 1));
            }
            catch { }

            if (tblSip.KapanmaTarihi.HasValue == false && tblSip.Durum == "Tamamlandı")
            {
                tblSip.KapanmaTarihi = DateTime.Now;
                siparis.KapanmaTarihi = DateTime.Now;
            }
            else if (tblSip.Durum != "Tamamlandı")
            {
                tblSip.KapanmaTarihi = null;
                siparis.KapanmaTarihi = null;
            }
            
            if (siparis.Id == 0)
            {
                int bagId = siparis.BaglantiId;
                tblSiparisler sipTip = db.GetGeneric<tblSiparisler>(c => c.Id == bagId).FirstOrDefault();

                tblBelgeNo belgeNo = tblBelgeNo.BelgeNoGetir(sipTip == null ? "" : sipTip.BelgeTuru);
                string strNumara = (belgeNo.SonBelgeNo + 1).ToString("00000");
                string belgeNoStr = belgeNo.Tipi + " " + DateTime.Now.ToString("yy") + "/" + strNumara;

                siparis.SozlesmeNo = belgeNoStr;
                tblSip.SozlesmeNo = belgeNoStr;
                belgeNo.SonBelgeNo = belgeNo.SonBelgeNo + 1;
                belgeNo.Yil = DateTime.Now.Year;
                tblBelgeNo.BelgeNoKaydet(belgeNo);

                bool snc = db.SaveGeneric<tblSiparisler>(ref tblSip);
                siparis.Id = tblSip.Id;

                return snc;
            }

            else return db.UpdateGeneric<tblSiparisler>(tblSip);
        }

        public tblFiyatListeleri TipFiyatiGetir(int musteriId, string tipNo)
        {
            ////müşteriye ilişkin prosess fiyatları getirilir.
            //List<tblFiyatListeleri> musteriProsesFiyatlari = db.GetGeneric<tblFiyatListeleri>(c => c.Tip == tipNo && c.ProcessId != null && c.MusteriId == musteriId);
            
            ////müşteriye ilişik proses fiyatları dışında tipe uygulanan başka prosesslerde varsa(sabit fiyatlar) fiyata eklenir.
            //var query = PredicateBuilder.True<tblFiyatListeleri>();
            //query = query.And(c => c.Tip == tipNo && c.ProcessId != null && c.MusteriId == null);
            //foreach (tblFiyatListeleri item in musteriProsesFiyatlari) query = query.And(c => c.ProcessId != item.ProcessId); //müşteriye özgü prosesslerin tekrar fiyata eklenmesi önleniyor.
            //List<tblFiyatListeleri> sabitProsessFiyatlari = db.GetGeneric<tblFiyatListeleri>(query);

            double processToplamFiyati = 0;
            //processToplamFiyati = musteriProsesFiyatlari.Sum(s => s.Fiyat) + sabitProsessFiyatlari.Sum(s => s.Fiyat);
            tblFiyatListeleri tipFiyati = null;

            List<tblFiyatListeleri> fiyatlar = db.GetGeneric<tblFiyatListeleri>(c => c.AktifMi == true && c.Tip == tipNo && c.MusteriId == musteriId && c.Yil == DateTime.Now.Year /*&& c.Ay == DateTime.Now.Month --şükrü 11.04.2016*/);
            if (fiyatlar != null && fiyatlar.Count > 0) tipFiyati = fiyatlar.OrderByDescending(o => o.Tarih).FirstOrDefault();

            fiyatlar = db.GetGeneric<tblFiyatListeleri>(c => c.AktifMi == true && c.Tip == tipNo && c.MusteriId == null && c.Yil == DateTime.Now.Year /*&& c.Ay == DateTime.Now.Month*/);
            if (fiyatlar != null && fiyatlar.Count > 0) tipFiyati = fiyatlar.OrderByDescending(o => o.Tarih).FirstOrDefault();

            if (tipFiyati != null) tipFiyati.Fiyat += processToplamFiyati;

            return tipFiyati;
        }

        public string OrderNoGetir(tblFirmalar musteri, int siparisId = 0)
        {
            if (musteri == null) return "";
            string strSayi = "", orderNo = "";
            if (siparisId == 0)
            {
                strSayi = db.GetGenericWithSQLQuery<string>("select top 1 cast(OrderSayi as varchar) Sonuc from tblSiparisler where FirmaId = " + musteri.Id.ToString() + " order by OrderSayi desc", new string[0]).FirstOrDefault();
                if (musteri.Adi != "") orderNo = musteri.Adi.Substring(0, 3) + "-" + (Convert.ToInt32(strSayi) + 1).ToString();
            }
            else
            {
                tblSiparisler tmp = db.GetGeneric<tblSiparisler>(c => c.FirmaId == musteri.Id && c.Id == siparisId).FirstOrDefault(); //db.GetGenericWithSQLQuery<string>("select OrderNo from tblSiparisler FirmaId = " + musteri.Id.ToString() + " where Id = " + siparisId.ToString(), new string[0]).FirstOrDefault();
                if (tmp == null || tmp.OrderNo == null)
                {
                    strSayi = db.GetGenericWithSQLQuery<string>("select top 1 cast(OrderSayi as varchar) Sonuc from tblSiparisler where FirmaId = " + musteri.Id.ToString() + " order by OrderSayi desc", new string[0]).FirstOrDefault();
                    orderNo = musteri.Adi.Substring(0, 3) + "-" + (Convert.ToInt32(strSayi) + 1).ToString();
                }
                else orderNo = tmp.OrderNo;
            }

            return orderNo;
        }

        public string SevkAdresiGetir(tblFirmalar firma)
        {
            if (firma == null) return "";
            string sevkAdresi = db.GetGenericWithSQLQuery<string>("select top 1 SevkYeri from tblSiparisler where FirmaId  = " + firma.Id.ToString() + " order by Tarih desc", new string[0]).FirstOrDefault();
            if (sevkAdresi == null) sevkAdresi = firma.Adres == null ? "" : firma.Adres;

            return sevkAdresi;
        }

        public List<vSiparisMamulleri> SiparisMamulleriGetir(int siparisId)
        {
            return db.GetGeneric<vSiparisMamulleri>(c => c.SiparisId == siparisId);
        }

        public bool SevkiyatEmriOlustur(vSiparisler siparis, int olusturanId)
        {
            tblSevk sevkBelge = new tblSevk()
            {
                FarkliSiparisOkut = false,
                LogoAktarildiMi = false,
                MusteriId = siparis.FirmaId,
                SevkEdenId = olusturanId,
                SiparisId = siparis.Id,
                Tarih = DateTime.Today,
                TipRenkKontrolDevreDisi = false
            };

            if (db.SaveGeneric<tblSevk>(sevkBelge))
            {
                tblSiparisler sipTbl = vSiparisler.ViewToTable(siparis);
                sipTbl.SevkEdilebilirMi = true;
                return db.UpdateGeneric<tblSiparisler>(sipTbl);
            }

            return false;
        }

        public List<vMamulKumaslar> SevkEmriMamulleriGetir(int siparisSatirId)
        {
            return db.GetGeneric<vMamulKumaslar>(c => c.SevkSiparisActId == siparisSatirId);
        }

        public List<vMamulKumaslar> SevkEmriMamulStoklariGetir()
        {
            return db.GetGeneric<vMamulKumaslar>(c => (c.Durum == "Mamul" || c.Durum == "ReUretim" || c.Durum == "Iade" || c.Durum == "Ticari" || c.Durum =="Fason") 
                                                        && c.SevkId == 0 && (c.SevkSiparisActId == null || c.SevkSiparisActId == 0) && c.SevkEdilebilir == true);
        }

        public bool SiparisSatiriSevkEmriMamulleriSec(List<vMamulKumaslar> mamuller, int siparisSatirId)
        {
            List<tblMamulKumaslar> tbl = vMamulKumaslar.ViewToTable(mamuller);
            tbl.ForEach(c => c.SevkSiparisActId = siparisSatirId);

            string idStr = "";
            foreach (var item in tbl) idStr += item.Id.ToString() + ",";
            idStr = idStr.TrimEnd(',');

            List<tblMamulKumaslar> sevkEdilenler = db.GetGenericWithSQLQuery<tblMamulKumaslar>("select * from tblMamulKumaslar where Id in (" + idStr + ") and SevkId <> 0", new object[0]);
            if (sevkEdilenler != null && sevkEdilenler.Count != 0)
            {
                string barkodStrs = "";
                foreach (var item in sevkEdilenler) barkodStrs += item.Barkod + "\n";
                throw new Exception("Aşağıdaki barkodlar sevk edilmiş.\n\nBarkodlar eklenemedi..!\n\n" + barkodStrs);
            }

            return db.UpdateGeneric<tblMamulKumaslar>(tbl);
        }

        public bool SevkEmriMamuluKaldir(vMamulKumaslar mamul)
        {
            tblMamulKumaslar tbl = db.GetGeneric<tblMamulKumaslar>(c => c.Id == mamul.Id).FirstOrDefault();
            if (tbl.SevkId.HasValue && tbl.SevkId.Value != 0) throw new Exception("Mamul sevk edilmiş.\n\nSevk emrinden silinemez..!");

            tbl.SevkSiparisActId = null;
            return db.UpdateGeneric<tblMamulKumaslar>(tbl);
        }

        #region Statics

        public static tblKumas KumasTipGetir(string kodu)
        {
            tblKumas kumas = null;

            if (kodu.Substring(0, 4) == "03.J")
            {
                string tipNo = kodu.Substring(5, 5);
                string varyant = kodu.Substring(11,3);
                kumas = new DBEvents().GetGeneric<tblKumas>(c => c.TipNo == tipNo && c.Varyant == varyant).FirstOrDefault();
            }
            else if (kodu.Substring(0, 4) == "03.Ö")
            {
                string tipNo = kodu.Substring(5, 6);                
                kumas = new DBEvents().GetGeneric<tblKumas>(c => c.TipNo == tipNo).FirstOrDefault();
            }
            else kumas = new DBEvents().GetGeneric<tblKumas>(c => c.TipNo == kodu.Substring(5, 4)).FirstOrDefault();

            //if (kumas == null) kumas = new DBEvents().GetGeneric<tblKumas>(c => c.TipNo == kodu.Substring(5, 7)).FirstOrDefault();

            return kumas;
        }

        public static tblKumasRenk KumasRenkGetir(string kodu)
        {
            if (kodu.Substring(0, 4) == "03.Ö")
            {
                string RenkNo = kodu.Substring(16, 5);                 
                return new DBEvents().GetGeneric<tblKumasRenk>(c => c.Kodu == RenkNo).FirstOrDefault();
            }
            else
            return new DBEvents().GetGeneric<tblKumasRenk>(c => c.Kodu == kodu.Substring(17, 4)).FirstOrDefault();
        }

        public static tblProsesGrup KumasFinishGetir(string kodu)
        {
            return new DBEvents().GetGeneric<tblProsesGrup>(c => c.Adi == kodu.Substring(22, 2)).FirstOrDefault();
        }

        public static List<vMamulKumaslar> RezerveyeUygunMamulGetir(int tipId)
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.TipId == tipId && (c.SevkId == null || c.SevkId == 0) && c.RezerveSiparisActId == null);
        }

        public static List<vMamulKumaslar> RezerveleriGetir()
        {
            List<vMamulKumaslar> list = new DBEvents().GetGeneric<vMamulKumaslar>(c => c.RezerveSiparisActId != null && (c.SevkId == null || c.SevkId == 0));

            foreach (vMamulKumaslar item in list)
            {
                vSiparisAct sip = new DBEvents().GetGeneric<vSiparisAct>(c => c.Id == item.RezerveSiparisActId).FirstOrDefault();
                if (sip != null) item.RezerveMusterisi = sip.FirmaAdi;
            }

            return list;
        }

        public static bool RezerveEt(vMamulKumaslar mamul, int siparisActId)
        {
            mamul.RezerveSiparisActId = siparisActId;

            return new DBEvents().UpdateGeneric<tblMamulKumaslar>(mamul.ViewToTable());
        }

        public static bool RezerveIptalEt(vMamulKumaslar mamul)
        {
            tblMamulKumaslar tbl = mamul.ViewToTable();
            tbl.RezerveSiparisActId = null;

            return new DBEvents().UpdateGeneric<tblMamulKumaslar>(tbl);
        }

        public static List<tblPersoneller> PersonelleriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 7 && c.AktifMi==true);
        }

        public static List<tblMalzemeler> SiparisMamulleriGetir()
        {
            return new DBEvents().GetGeneric<tblMalzemeler>(c => c.BaglantiId == 41 || c.BaglantiId == 43).OrderBy(o => o.Kodu).ToList();
        }



        #endregion //Statics
    }
}

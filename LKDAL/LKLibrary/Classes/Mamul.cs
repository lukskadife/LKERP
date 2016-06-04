using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Mamul
    {
        private DBEvents db = new DBEvents();

        public Mamul(vMamulKumaslar mamul = null)
        {
            Hatalar = new List<tblMamulHatalari>();

            if (mamul == null) YeniMamulKumas = new vMamulKumaslar() { Tarih = DateTime.Today };
        }

        public vMamulKumaslar YeniMamulKumas { get; set; }
        /// <summary>
        /// Okutulan parti barkoduna uygun ham kumaşlar
        /// </summary>
        public List<vHamKumaslar> PartiHamKumaslari { get; set; }

        public List<vReProcessBarkodlari> RePartiKumaslari { get; set; }

        /// <summary>
        /// Okutulan parti barkodundaki mamüller
        /// </summary>
        public List<vMamulKumaslar> MamulKumaslar { get; set; }
        /// <summary>
        /// Mamule girilen mamul hataları
        /// </summary>
        public List<tblMamulHatalari> Hatalar { get; set; }

        #region statics

        public static List<vHataTanim> MamulHatalariGetir()
        {
            return new DBEvents().GetGeneric<vHataTanim>(c => c.AktifMi == true && c.Kodu != "Sebep" && c.Kodu != "Yer").GroupBy(o => o.Adi).Select(s => s.First()).OrderBy(c => c.Kodu).ToList();
        }

        public static List<vMamulKumaslar> MamulleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih && (c.Durum == "Mamul" || c.Durum == "ReUretim" || c.Durum == "Fason" || c.Durum == "Kartela"));
        }

        public static List<tblPersoneller> KalitecileriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 14 && c.AktifMi == true);
        }

        public static tblMamulKumaslar AciklamaBarkodOkut(string barkod)
        {
            return new DBEvents().GetGeneric<tblMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();
        }

        public static tblMamulKumaslar AciklamaEkle(tblMamulKumaslar mamul, string mesaj)
        {
            if (mamul == null) return null;
            tblMamulKumaslar tempMamul = mamul;

            if (tempMamul.Aciklama == null) tempMamul.Aciklama = "";
            if (tempMamul.Aciklama != "" && tempMamul.Aciklama.Substring(tempMamul.Aciklama.Length - 1, 1) != "\n") tempMamul.Aciklama += "\n";

            tempMamul.Aciklama += mesaj + "\n";
            if (new DBEvents().UpdateGeneric<tblMamulKumaslar>(tempMamul)) return tempMamul;
            else return mamul;
        }

        public static List<vMamulHataHaritasi> MamulHataHaritasiGetir(int mamulId)
        {
            return new DBEvents().GetGeneric<vMamulHataHaritasi>(c => c.MamulId == mamulId);
        }

        public static bool FasonaSevkEdilmisMi(int partiId)
        {
            tblMamulKumaslar mamul = new DBEvents().GetGeneric<tblMamulKumaslar>(c => c.PartiId == partiId && c.SevkId > 0).FirstOrDefault();

            if (mamul == null)
                return false;
            else return true;
        }

        public static List<tblMakinalar> TezgahlariGetir()
        {
            return new DBEvents().GetGeneric<tblMakinalar>(c => c.BaglantiId == 1 && c.AktifMi == true);
        }

        public static List<tblAyarlar> MamulEtiketleriGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.BaglantiId == 104);
        }

        public static IQueryable<vMamulKumaslar> MamulStokGetir()
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.SevkId == 0 && (c.Durum == "Mamul" || c.Durum == "ReUretim")).AsQueryable();
        }

        public static bool MamulDuzelt(tblMamulKumaslar mamul)
        {
            List<tblMamulHatalari> Hatalar = new DBEvents().GetGeneric<tblMamulHatalari>(c => c.MamulId == mamul.Id);
            double? hataMt = 0;
            if (Hatalar != null) hataMt = Math.Round(Hatalar.Sum(c => c.Uzunluk), 2);
            if (mamul.KaliteAdet == "I.KALİTE" && mamul.Metre != 0) mamul.NetMetre = mamul.Metre - (hataMt.HasValue ? hataMt.Value : 0) - 0.30;
            else mamul.NetMetre = mamul.Metre;

            return new DBEvents().UpdateGeneric<tblMamulKumaslar>(mamul);
        }

        public static List<vMamulKumaslar> TicariMamulleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.Durum == "Ticari" && ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static bool TicariMamulKaydet(vMamulKumaslar ticari)
        {
            if (ticari == null) return false;

            if (string.IsNullOrEmpty(ticari.Durum) || ticari.Durum == "Ticari")
            {
                tblMamulKumaslar tbl = ticari.ViewToTable();
                if (tbl.Id == 0)
                {
                    tblMamulKumaslar kontrol = new DBEvents().GetGeneric<tblMamulKumaslar>(c => c.Barkod == ticari.Barkod).FirstOrDefault();
                    if (kontrol != null) throw new Exception("Barkod çakışması var..!");
                    tbl.Durum = "Ticari";
                    tbl.PartiId = null;
                    tbl.Tarih = DateTime.Today;
                    tbl.NetMetre = tbl.Metre;
                    tbl.KalitePuan = tbl.KaliteAdet;
                    tbl.SevkId = 0;
                    tbl.SevkEdilebilir = true;
                    tbl.Parca = "TAM";
                    return new DBEvents().SaveGeneric<tblMamulKumaslar>(tbl);
                }
                else return new DBEvents().UpdateGeneric<tblMamulKumaslar>(tbl);
            }
            return false;
        }

        public static bool TicariMamulSil(vMamulKumaslar ticari)
        {
            if (ticari == null || ticari.Durum != "Ticari") return false;

            tblMamulKumaslar tbl = new DBEvents().GetGeneric<tblMamulKumaslar>(c => c.Id == ticari.Id).FirstOrDefault();

            return new DBEvents().DeleteGeneric<tblMamulKumaslar>(tbl);
        }

        public static vMamulKumaslar BarkodGetir(string barkod)
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();
        }

        public static vMamulKumaslar MamulBarkodSorgula(string barkod)
        {
            DBEvents db = new DBEvents();
            vMamulKumaslar mamul = db.GetGeneric<vMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();
            if (mamul != null && mamul.SevkSiparisActId.HasValue == true)
            {
                vSiparisAct sevkEmri = db.GetGeneric<vSiparisAct>(c => c.Id == mamul.SevkSiparisActId.Value).FirstOrDefault();
                if (sevkEmri != null) mamul.SevkEmri = sevkEmri.SozlesmeNo + " - " + sevkEmri.FirmaAdi;
            }
            return mamul;
        }

        #endregion 

        public vMamuleHazirPartiler Partisi;
        public bool FasonamiGidecek = false;
        public bool BarkodOkut(string barkod)
        {
            vMamuleHazirPartiler okutulan = db.GetGeneric<vMamuleHazirPartiler>(c => c.PartiNo == barkod).FirstOrDefault();
            Partisi = okutulan;

            if (okutulan == null || okutulan.ProcessId != 60)
            {
                // paketleme prosesi okutulmuşsa normal mamul barkod alınır durum sütunundaki ayracı 'Mamul'dür. 
                // Paketleme prosesi okutulmamış ve fason proses okutulmuş ise _FasonamiGidecek değişkeni true olur
                // ve kumaş kesme esnasında bu değişken kontrol edilerek mamul barkodun Durum sütunundaki ayracı 'Fason' olarak işaretlenir.
                vBoyahaneProcess paketleme = db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == barkod && c.ProcessId == 60).FirstOrDefault();
                if (paketleme == null)
                {
                    vBoyahaneProcess fasonProcess = db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == barkod).OrderByDescending(c=>c.Sira).FirstOrDefault();
                   
                    if (fasonProcess.FasonMu == false)
                    {
                        FasonamiGidecek = false;
                        throw new Exception("Paketleme prosesi okutulmamış..!");
                    }
                    else if (fasonProcess.FasonMu == true)
                    {
                        vMamulKumaslar kumaslar = db.GetGeneric<vMamulKumaslar>(c => c.PartiNo == barkod && c.Durum == "Fason").FirstOrDefault();
                        if (kumaslar == null)
                            FasonamiGidecek = true;
                        else
                        {
                            FasonamiGidecek = false;
                            throw new Exception("Fasondan kumaş gelmiş. Şimdi paketleme processini okutun!");
                        }
                    }
                }
            }

            if (okutulan == null)
            {
                YeniMamulKumas = null;
                throw new Exception("Partilenmiş barkod bulunamadı..!");
            }

            YeniMamulKumas.TipId = okutulan.TipId;
            YeniMamulKumas.TipNo = okutulan.TipNo;
            YeniMamulKumas.RenkNo = okutulan.RenkNo;
            YeniMamulKumas.TopMetre = okutulan.TopMetre;
            YeniMamulKumas.DyeBatchNo = barkod;
            YeniMamulKumas.RefRenkAdi = okutulan.RefRenkAdi;             
            YeniMamulKumas.RefRenkNo=okutulan.RefRenkNo;
            YeniMamulKumas.RefTipNo=okutulan.RefTipNo;
            YeniMamulKumas.SozlesmeNo = okutulan.SozlesmeNo;            
            YeniMamulKumas.SevkiyatNotu = (db.GetGeneric<vPartiler>(f=> f.PartiNo==okutulan.PartiNo).FirstOrDefault()).BoyaNotu;


            if (okutulan.RePartiMi)
                RePartiKumaslari = db.GetGenericWithSQLQuery<vReProcessBarkodlari>("execute dbo.spRePartiBarkodlariGetir " + okutulan.PartiId.ToString(), new string[0]);
            else PartiHamKumaslari = db.GetGeneric<vHamKumaslar>(c => c.PartiId == okutulan.PartiId);
            
            MamulKumaslar = db.GetGeneric<vMamulKumaslar>(c => c.PartiId == okutulan.PartiId);

            if ((okutulan.RePartiMi == false) && (PartiHamKumaslari == null || PartiHamKumaslari.Count == 0))
                throw new Exception("Partide ham kumaş yok.\n\nBarkod okutulamaz..!");
            if (MamulKumaslar == null) MamulKumaslar = new List<vMamulKumaslar>();

            YeniMamulKumas = new vMamulKumaslar()
            {
                MusteriId = okutulan.MusteriId,
                MusteriKodu = okutulan.MusteriKodu,
                MusteriAdi = okutulan.MusteriAdi,
                PartiId = okutulan.PartiId,
                PartiNo = okutulan.PartiNo.ToString(),
                SozlesmeNo = okutulan.SozlesmeNo,
                ToplamKalan = Math.Round((PartiHamKumaslari == null ? 0 : PartiHamKumaslari.Sum(c => c.Metre)) - MamulKumaslar.Sum(c => c.Metre), 2),
                Tarih = DateTime.Today,
                TopMetre = okutulan.TopMetre,
                Finish = okutulan.FinishNo,
                TipId = okutulan.TipId,
                TipNo = okutulan.TipNo,
                RenkNo = okutulan.RenkNo,
                RenkVaryant = okutulan.RenkVaryant,
                TipVaryant = okutulan.TipVaryant,
                DyeBatchNo = barkod,
                RefRenkAdi=okutulan.RefRenkAdi,
                RefRenkNo=okutulan.RefRenkNo,
                RefTipNo=okutulan.RefTipNo,
                SevkiyatNotu = (db.GetGeneric<vPartiler>(f => f.PartiNo == okutulan.PartiNo).FirstOrDefault()).BoyaNotu
            };

            return true;
        }

        public double HamKalanMiktarGetir(int hamId)
        {
            if (this.YeniMamulKumas == null) return 0;

            double hamMiktar = PartiHamKumaslari == null ? 0 : PartiHamKumaslari.Where(c=>c.Id == hamId).Sum(c => c.Metre);
            double mamulMiktar = MamulKumaslar.Where(c => c.HamId == hamId && c.Durum=="Mamul").Sum(t => t.Metre);

            return Math.Round(hamMiktar - mamulMiktar);
        }

        protected bool MamulBarkodAl(ref tblMamulKumaslar mamul)
        {
            //mamul kumaş barkodu oluşturuluyor
            mamul.Barkod = ('M' + mamul.Id.ToString()).PadLeft(10, '0');
            if (mamul.AnaMamulId == 0 && mamul.HamId != null) mamul.AnaMamulId = mamul.Id;
            bool barkodAlindiMi = db.UpdateGeneric<tblMamulKumaslar>(mamul);
            if (!barkodAlindiMi) //eğer barkod alınamaz ise kayıt silinir ve kaydedilmedi olarak fonksiyon false dönderir.
            {
                db.DeleteGeneric<tblMamulKumaslar>(mamul);
                return false;
            }

            return true;
        }

        public bool KumasKes(bool parcaMi)
        {
            if (YeniMamulKumas == null) throw new Exception("Barkod girişi olmadan kesme yapılamaz..!");

            if (parcaMi) YeniMamulKumas.Parca = "PARÇA";
            else YeniMamulKumas.Parca = "TAM";

            //_FasonamiGidecek : barkodOkut fonksiyonunda değeri set edildi.
            if (FasonamiGidecek) YeniMamulKumas.Durum = "Fason";
            else YeniMamulKumas.Durum = "Mamul";

            if (YeniMamulKumas.SevkId == null) YeniMamulKumas.SevkId = 0;

           // ParcaKgHesapla();
           

            HataPuanlariHesapla();
            KaliteHesapla();
            tblMamulKumaslar tblMamul = YeniMamulKumas.ViewToTable();
            
            //reprocess kumaşı ise
            if (this.Partisi.RePartiMi)
            {
                try
                {
                    vReProcessBarkodlari reBarkodu = YeniMamulKumas.SecilenBarkod as vReProcessBarkodlari;

                    tblMamulKumaslar ReMamul = db.GetGeneric<tblMamulKumaslar>(c => c.Id == reBarkodu.Id).FirstOrDefault();
                    tblMamul.HamId = null;
                    tblMamul.PartiId = ReMamul.RePartiId.Value;
                    tblMamul.AnaMamulId = ReMamul.AnaMamulId;
                    YeniMamulKumas.Durum = "ReUretim";
                    tblMamul.Durum = "ReUretim";
                    //ReMamul.Durum = "";
                    //if (db.UpdateGeneric<tblMamulKumaslar>(ReMamul) == false) throw new Exception();
                }
                catch
                {
                    throw new Exception("Hata oluştu.\n\nKumaş kesilemedi.REPROC");
                }
            }
            else
            {
                tblMamul.HamId = (YeniMamulKumas.SecilenBarkod as vHamKumaslar).Id;
                YeniMamulKumas.HamId = tblMamul.HamId;
                YeniMamulKumas.HamBarkod = (YeniMamulKumas.SecilenBarkod as vHamKumaslar).Barkod;
            }

            if (db.SaveGeneric<tblMamulKumaslar>(ref tblMamul) == true)
            {
                YeniMamulKumas.Id = tblMamul.Id;

                if (this.Hatalar != null && this.Hatalar.Count > 0)
                {
                    bool hataKaydedildiMi = HatalariKaydet();
                    if (!hataKaydedildiMi)
                    {
                        db.DeleteGeneric<tblMamulKumaslar>(tblMamul);
                        throw new Exception("Hata oluştu.\n\nKumaş kesilemedi..!");
                    }
                }

                double hataMt = 0;
                if (Hatalar != null) hataMt = Math.Round(Hatalar.Sum(c => c.Uzunluk), 2);
                if (tblMamul.KaliteAdet == "I.KALİTE") tblMamul.NetMetre = tblMamul.Metre - hataMt - 0.30;
                else tblMamul.NetMetre = tblMamul.Metre;

                if (MamulBarkodAl(ref tblMamul) == false) return false;

                YeniMamulKumas.Barkod = tblMamul.Barkod;
                YeniMamulKumas.NetMetre = tblMamul.NetMetre;
                YeniMamulKumas.AnaMamulId = tblMamul.AnaMamulId;
            }
            else return false; //mamul kumaş kaydedilemez ise

            MamulKumaslar.Add(YeniMamulKumas);

            vMamulKumaslar tmp = YeniMamulKumas;
            YeniMamulKumas = new vMamulKumaslar()
            {
                Aciklama = tmp.Aciklama,
                FasonTipi = tmp.FasonTipi,
                Finish = tmp.Finish,
                KaliteciAdi = tmp.KaliteciAdi,
                KaliteciId = tmp.KaliteciId,
                KaliteciKodu = tmp.KaliteciKodu,
                MusteriAdi = tmp.MusteriAdi,
                MusteriId = tmp.MusteriId,
                MusteriKodu = tmp.MusteriKodu,
                PartiId = tmp.PartiId,
                PartiNo = tmp.PartiNo,
                RenkNo = tmp.RenkNo,
                RenkVaryant = tmp.RenkVaryant,
                SevkId = tmp.SevkId,
                SevkiyatNotu = tmp.SevkiyatNotu,
                SiparisId = tmp.SiparisId,
                SozlesmeNo = tmp.SozlesmeNo,
                Tarih = DateTime.Today,
                TipAdi = tmp.TipAdi,
                TipId = tmp.TipId,
                TipNo = tmp.TipNo,
                TipVaryant = tmp.TipVaryant,
                ToplamKalan = tmp.ToplamKalan - tmp.Metre,
                TopMetre = tmp.TopMetre,
                En = tmp.En,
                SecilenBarkod = tmp.SecilenBarkod,
                DyeBatchNo = tmp.DyeBatchNo,
                RefRenkAdi = tmp.RefRenkAdi,
                RefRenkNo = tmp.RefRenkNo,
                RefTipNo = tmp.RefTipNo
            };
             
            Hatalar.Clear();
            return true;
        }

        public bool MamulSil(vMamulKumaslar silinecek)
        {
            bool snc = db.DeleteGeneric<tblMamulKumaslar>(silinecek.ViewToTable());
            if (snc)
            {
                List<tblMamulHatalari> hatalar = db.GetGeneric<tblMamulHatalari>(c => c.MamulId == silinecek.Id);
                db.DeleteGeneric<tblMamulHatalari>(hatalar);
            }

            if (snc && MamulKumaslar != null) MamulKumaslar.RemoveAll(c=>c.Id == silinecek.Id);

            return snc;
        }

        private void HataPuanlariHesapla(tblMamulHatalari mamulHata)
        {
            if (mamulHata.Uzunluk > 2)
            {
                this.YeniMamulKumas.HataAdet += Convert.ToInt32(mamulHata.Uzunluk);
                this.YeniMamulKumas.HataPuan += mamulHata.Uzunluk * 4;
            }

            else
            {
                tblHataTanim hataPuan = db.GetGeneric<tblHataTanim>(c => c.Kodu == mamulHata.HataKodu && c.PuanAralik1 <= mamulHata.Uzunluk && mamulHata.Uzunluk <= c.PuanAralik2).FirstOrDefault();

                this.YeniMamulKumas.HataAdet += 1;
                this.YeniMamulKumas.HataPuan += (hataPuan != null && hataPuan.Puan != null) ? hataPuan.Puan : 0;
            }
        }

        public void HataPuanlariHesapla()
        {
            YeniMamulKumas.HataAdet = 0;
            YeniMamulKumas.HataPuan = 0;

            foreach (tblMamulHatalari item in this.Hatalar) HataPuanlariHesapla(item);

        }
        public void ParcaKgHesapla()
        {
            
            double TeorikKg = 0;
            double Kg_1,Kg_2,KumasKg;
          
            tblKumas tip = db.GetGeneric<tblKumas>(c => c.Id == YeniMamulKumas.TipId).FirstOrDefault();
            if (tip == null && tip.MamulAgirlik == null) return;
            if (YeniMamulKumas.Tur == "Alt")
            {
                if (tip != null && tip.MamulAgirlik != null) TeorikKg = Math.Round((tip.MamulAgirlik.Value * YeniMamulKumas.Metre / 1000), 2);
            }
            if (YeniMamulKumas.Tur == "Ust")
            {
                if (tip != null && tip.MamulAgirlik != null) TeorikKg = Math.Round((tip.MamulAgirlikUst.Value * YeniMamulKumas.Metre / 1000), 2);
            }
            Kg_1 = Math.Round((TeorikKg*2/ 100) + TeorikKg,2);  Kg_2 = Math.Round(TeorikKg - (TeorikKg*4 / 100),2);
            KumasKg=((double)YeniMamulKumas.Kg-1.5);
            if ((Kg_2 <= KumasKg && KumasKg <= Kg_1)==false) throw new Exception("Tartılan Ağırlık ile Mamül Ağırlığı Arasında Fark Var.\n\nKontrol Ediniz."); 
        }

        public bool KaliteHesapla()
        {
            //kalite adet hesaplanıyor
            if (YeniMamulKumas.Metre != null && YeniMamulKumas.Metre != 0) YeniMamulKumas.KaliteAdetDeger = Math.Round((YeniMamulKumas.HataAdet / YeniMamulKumas.Metre), 3);
            else YeniMamulKumas.KaliteAdetDeger = 0;
            tblKaliteTanim adetKalite = db.GetGeneric<tblKaliteTanim>(c => c.AdetAralik1 <= YeniMamulKumas.KaliteAdetDeger && YeniMamulKumas.KaliteAdetDeger <= c.AdetAralik2).FirstOrDefault();
            YeniMamulKumas.KaliteAdet = (adetKalite == null || adetKalite.Adi == null) ? "" : adetKalite.Adi;

            //kalite puan hesaplanıyor
            if (YeniMamulKumas.Metre != null && YeniMamulKumas.Metre != 0) YeniMamulKumas.KalitePuanDeger = Math.Round((YeniMamulKumas.HataPuan * 100 / YeniMamulKumas.Metre), 3);
            else YeniMamulKumas.KalitePuanDeger = 0;
            tblKaliteTanim puanKalite = db.GetGeneric<tblKaliteTanim>(c => c.PuanAralik1 <= YeniMamulKumas.KalitePuanDeger && YeniMamulKumas.KalitePuanDeger <= c.PuanAralik2).FirstOrDefault();
            YeniMamulKumas.KalitePuan = (puanKalite == null || puanKalite.Adi == null) ? "" : puanKalite.Adi;

            return true;
        }

        public bool HataEkle(tblMamulHatalari hata)
        {
            try
            {
                if (hata == null) return false;
                hata.Uzunluk /= 100;
                Hatalar.Add(hata);
                HataPuanlariHesapla(hata);
                KaliteHesapla();
                return true;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "Mamul HataEkle(tblMamulHatalari hata) fonksiyonu", 0);
                return false;
            }
        }

        private bool HatalariKaydet()
        {
            if (YeniMamulKumas == null || YeniMamulKumas.Id == 0) return false;

            this.Hatalar.ForEach(c => c.MamulId = YeniMamulKumas.Id);

            List<tblMamulHatalari> toSave = this.Hatalar.FindAll(c => c.Id == 0);
            List<tblMamulHatalari> toUpd = this.Hatalar.FindAll(c => c.Id != 0);

            if (toSave.Count>0 && db.SaveGeneric<tblMamulHatalari>(ref toSave) == false) return false;
            if (toUpd.Count > 0 && db.UpdateGeneric<tblMamulHatalari>(toUpd) == false)
            {
                db.DeleteGeneric<tblMamulHatalari>(toSave);
                return false;
            }

            this.Hatalar.Clear();
            this.Hatalar.AddRange(toSave);
            this.Hatalar.AddRange(toUpd);
            this.Hatalar = this.Hatalar.OrderBy(c => c.Id).ToList();

            return true;
        }

        public bool HataSil(tblMamulHatalari hata)
        {
            if (hata.Id != 0 && db.DeleteGeneric<tblMamulHatalari>(hata) == false) return false;
            this.Hatalar.Remove(hata);

            HataPuanlariHesapla();
            KaliteHesapla();
            return true;
        }

        public vMamulKumaslar BirlestirilenKumas;

        /// <summary>
        /// Partisi, tipi, eni ve kalitesi aynı olan mamül kumaşları tek bir kayıt olarak kaydeder. Birleştirilenleri siler.
        /// </summary>
        /// <param name="kumaslar">Birleştirilecek kumaslar</param>
        /// <returns></returns>
        public bool BarkodBirlestir()
        {
            List<vMamulKumaslar> kumaslar = this.MamulKumaslar.FindAll(c => c.BarkodBirlestirmeSecim == true);
            if (kumaslar == null || kumaslar.Count <= 1) throw new Exception("Birden fazla kumaş seçilmelidir..!");

            tblMamulKumaslar yeniKumas = kumaslar[0].ViewToTable();
            if (kumaslar.Exists(c => c.KaliteAdet != yeniKumas.KaliteAdet || c.TipId != kumaslar[0].TipId || c.PartiId != yeniKumas.PartiId))
            {
                throw new Exception("Birleştirileceklerin partisi, tipi ve kalitesi aynı olmalıdır..!");
            }

            yeniKumas.Tarih = DateTime.Today;
            yeniKumas.Metre = kumaslar.Sum(s => s.Metre);
            yeniKumas.NetMetre = kumaslar.Sum(s => s.NetMetre);
            yeniKumas.Kg = kumaslar.Sum(s => s.Kg);
            yeniKumas.AnaMamulId = 0;

            //Birleştirilen kumaşların hataları yeni kumaşa atanıyor.
            List<tblMamulHatalari> hatalar = new List<tblMamulHatalari>();
            string parcaMetre = "";
            foreach (vMamulKumaslar item in kumaslar)
            {
                parcaMetre += item.Metre.ToString() + " + ";
                List<tblMamulHatalari> itemHatalari = db.GetGeneric<tblMamulHatalari>(c => c.MamulId == item.Id);
                if (itemHatalari != null && itemHatalari.Count > 0) hatalar.AddRange(itemHatalari);
            }
            parcaMetre = parcaMetre.TrimEnd(new char[] { ' ', '+' });
            yeniKumas.ParcaMetre = parcaMetre;

            if (db.SaveGeneric<tblMamulKumaslar>(ref yeniKumas) == false) return false;
            if (MamulBarkodAl(ref yeniKumas) == false) return false;

            //Birleştirilen kumaşların hataları yeni kumaşın hataları olarak güncelleniyor.
            hatalar.ForEach(f => f.MamulId = yeniKumas.Id);
            if (db.SaveGeneric<tblMamulHatalari>(hatalar) == false)
            {
                db.DeleteGeneric<tblMamulKumaslar>(yeniKumas);
                return false;
            }
            else
            {
                List<tblMamulKumaslar> tblkumaslar = new List<tblMamulKumaslar>();
                foreach (vMamulKumaslar item in kumaslar)
                {
                    tblMamulKumaslar tbl = db.GetGeneric<tblMamulKumaslar>(c=>c.Id == item.Id).FirstOrDefault();
                    tblkumaslar.Add(tbl);
                }
                //db.DeleteGeneric<tblMamulKumaslar>(vMamulKumaslar.ViewToTable(kumaslar));
                db.DeleteGeneric<tblMamulKumaslar>(tblkumaslar);
                MamulKumaslar = db.GetGeneric<vMamulKumaslar>(c => c.PartiId == yeniKumas.PartiId);
                BirlestirilenKumas = MamulKumaslar.Find(c => c.Id == yeniKumas.Id);
                return true;
            }
        }

        
    }

    public class MamulKesim
    {
        public vMamulKumaslar AnaMamul { get; set; }

        private vMamulKumaslar _ParcaMamul;
        public vMamulKumaslar ParcaMamul
        {
            get
            { 
                return _ParcaMamul; 
            }
            set
            {
                if (value != null)
                {
                    _ParcaMamul = value;

                    if (value.Metre != 0)
                    {
                        tblKumas tip = db.GetGeneric<tblKumas>(c => c.Id == AnaMamul.TipId).FirstOrDefault();
                        if (tip != null && tip.MamulAgirlik != null) _ParcaMamul.Kg = Math.Round((tip.MamulAgirlik.Value * ParcaMamul.Metre / 1000), 2);
                    }
                    else _ParcaMamul.Kg = 0;
                }
            }
        }

        DBEvents db = new DBEvents();

        public MamulKesim()
        {
            this.AnaMamul = null;
            this._ParcaMamul = null;
        }

        public void BarkodOkut(string barkod)
        {
            AnaMamul = db.GetGeneric<vMamulKumaslar>(c => c.Barkod == barkod && c.SevkId == 0).FirstOrDefault();
            if (AnaMamul == null) throw new Exception("Barkod bulunamadı..!");
            _ParcaMamul = AnaMamul.CopyToNewObject();
            _ParcaMamul.AnaMamulId = this.AnaMamul.Id;
            _ParcaMamul.Durum = "Kesilen";
            _ParcaMamul.Metre = 0;
            _ParcaMamul.Kg = 0;
            _ParcaMamul.Barkod = "";
        }

        public bool Parcala()
        {
            if (this.AnaMamul == null || this._ParcaMamul == null) throw new Exception("Barkod okutulmamış..!");
            if (this._ParcaMamul.Metre <= 0) throw new Exception("Kesilen metre 0'dan büyük olmalıdır.");
            if (this._ParcaMamul.Kg <= 0) throw new Exception("Kg 0'dan büyük olmalıdır.");
            if (this._ParcaMamul.Metre > this.AnaMamul.Metre) throw new Exception("Fazla metre kesilemez..!\n\nMamul metresi : " + this.AnaMamul.Metre.ToString());
            if (this._ParcaMamul.Kg > this.AnaMamul.Kg) throw new Exception("Kesilenin kg'ı barkoddan fazla olamaz..!\n\nMamul kg : " + this.AnaMamul.Kg.ToString());

            if (AnaMamul.Metre == _ParcaMamul.Metre)
            {
                AnaMamul.Durum = "Kesilen";
                return db.UpdateGeneric<tblMamulKumaslar>(AnaMamul.ViewToTable());
            }

            else
            {
                _ParcaMamul.NetMetre = _ParcaMamul.Metre;
                AnaMamul.Metre = Math.Round((AnaMamul.Metre - _ParcaMamul.Metre), 2);
                AnaMamul.NetMetre = Math.Round((AnaMamul.NetMetre - _ParcaMamul.NetMetre), 2);
                AnaMamul.Kg = Math.Round((AnaMamul.Kg.Value - _ParcaMamul.Kg.Value), 2);

                if (db.UpdateGeneric<tblMamulKumaslar>(AnaMamul.ViewToTable()))
                {
                    return db.SaveGeneric<tblMamulKumaslar>(this._ParcaMamul.ViewToTable());
                }
            }

            return false;
        }
    }
}

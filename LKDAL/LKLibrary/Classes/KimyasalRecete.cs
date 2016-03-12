using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class KimyasalRecete
    {
        public vKimyasalRecete Recete { get; set; }
        public List<vKimyasalRecetePartileri> ReceteninPartileri { get; set; }
        public List<vPartiler> UygunPartiler { get; set; }
        public string RenkAciklama { get; set; }

        private enum Tipler { Kimyasal, Boya, Apre, OnIslem, Yikama, Kasar }
        private DBEvents db = new DBEvents();

        int KId;
        string mesaj;

        public KimyasalRecete(vKimyasalRecete recete = null)
        {
            if (recete != null)
            {
                this.Recete = recete;
                this.ReceteninPartileri = db.GetGeneric<vKimyasalRecetePartileri>(c => c.ReceteId == recete.Id);
                
                if (Recete.TopluRecete) //Toplu kasar yada apre
                this.UygunPartiler = db.GetGenericWithSQLQuery<vPartiler>("select * from vPartiler where PaketlendiMi=0 order by PartiNo", new object[0]);  //renkten bağımsız boyanmamış tüm partiler ekrana gelecek.
                else
                    this.UygunPartiler = db.GetGenericWithSQLQuery<vPartiler>("select * from vPartiler where CHARINDEX(RenkNo, '" + this.Recete.RenkKodu + "') > 0 and PaketlendiMi=0 order by PartiNo", new object[0]);

                tblKumasRenk renk = db.GetGeneric<tblKumasRenk>(c => c.Id == recete.RenkId).FirstOrDefault();
                this.RenkAciklama = (renk == null || renk.Aciklama == null) ? "" : "Renk Notu : " + renk.Aciklama;
                if (string.IsNullOrEmpty(this.Recete.Aciklama) == false) this.RenkAciklama += "\n\n< Diğer Notlar >\n" + this.Recete.Aciklama;
            }
            else
            {
                this.Recete = new vKimyasalRecete() { Tarih = DateTime.Today };
                this.ReceteninPartileri = new List<vKimyasalRecetePartileri>();
            }
        }

        private string YeniReceteNoVer()
        {
            tblBelgeNo receteNo = tblBelgeNo.BelgeNoGetir("KR");
            string strNo = "";
            if (receteNo != null && receteNo.SonBelgeNo != null) strNo = receteNo.Tipi + " " + receteNo.Yil.ToString().Substring(2, 2) + "/" + (receteNo.SonBelgeNo + 1).ToString();
            else strNo = "KR " + DateTime.Now.Year.ToString("YY") + "/" + "1";

            receteNo.SonBelgeNo = receteNo.SonBelgeNo + 1;
            if (tblBelgeNo.BelgeNoKaydet(receteNo) == false) throw new Exception("Reçete numarası almada hata oluştu.\n\nKaydedilemedi..!");

            return strNo;
        }

        public bool PartiEkle(vPartiler parti)
        {
            if (this.Recete == null || this.Recete.Id == 0) throw new Exception("Reçete seçili değil");

            List<vKimyasalRecetePartileri> OncedenEklenmis = this.ReceteninPartileri.FindAll(c => c.PartiId == parti.Id);

            if (OncedenEklenmis.Count > 0)
            {
                return false;
            }
            else
            {

                this.ReceteninPartileri.Add(new vKimyasalRecetePartileri()
                {
                    PartiId = parti.Id,
                    PartiNo = parti.PartiNo,
                    ReceteId = this.Recete.Id,
                    TartilanKg = parti.TartilanKg,
                    AcilmisMetre = parti.PartiMetre
                });

                return true;
            }
        }

        public bool RecetePartileriKaydet()
        {
            bool snc = true;
            this.ReceteninPartileri.ForEach(c => c.ReceteId = Recete.Id);

            List<vKimyasalRecetePartileri> saveView = ReceteninPartileri.FindAll(c => c.Id == 0);
            List<vKimyasalRecetePartileri> updView = ReceteninPartileri.FindAll(c => c.Id != 0);

            List<tblKimyasalRecetePartileri> toSave = vKimyasalRecetePartileri.ViewToTbl(saveView);
            List<tblKimyasalRecetePartileri> toUpd = vKimyasalRecetePartileri.ViewToTbl(updView);

            if (toSave.Count > 0 && db.SaveGeneric<tblKimyasalRecetePartileri>(ref toSave) == true)
                for (int i = 0; i < toSave.Count; i++) saveView[i].Id = toSave[i].Id;
            else snc = false;

            if (toUpd.Count > 0 && db.UpdateGeneric<tblKimyasalRecetePartileri>(toUpd) == false) snc = false;

            this.ReceteninPartileri = new List<vKimyasalRecetePartileri>();
            this.ReceteninPartileri.AddRange(saveView);
            this.ReceteninPartileri.AddRange(updView);

            return snc;
        }

        public bool RecetePartisiSil(vKimyasalRecetePartileri silinecek)
        {
            if (silinecek.Id != 0)
            {
                if (db.DeleteGeneric<tblKimyasalRecetePartileri>(silinecek.ViewToTbl()) == false) return false;
            }

            this.ReceteninPartileri.Remove(silinecek);
            return true;
        }

        public bool ReceteKaydet()
        {
            bool snc = true;

            this.Recete.ReceteNo = YeniReceteNoVer();
            tblKimyasalRecete tblRecete = this.Recete.ViewToTbl();
            if (Recete.Id == 0)
            {
                if (db.SaveGeneric<tblKimyasalRecete>(ref tblRecete)) Recete.Id = tblRecete.Id;
                else snc = false;
            }
            else snc = db.UpdateGeneric<tblKimyasalRecete>(Recete.ViewToTbl());

            return snc;
        }

        //Gökhan 12.05.2014
        public bool ReceteKaydet(int makinaId)
        {
            bool snc = true;

            if (this.Recete.ReceteNo == null)
            {
                this.Recete.ReceteNo = YeniReceteNoVer();
            }
            
            tblKimyasalRecete tblRecete = this.Recete.ViewToTbl();
            if (Recete.Id == 0)
            {
                
                tblRecete.MakinaId = makinaId;
                if (db.SaveGeneric<tblKimyasalRecete>(ref tblRecete)) Recete.Id = tblRecete.Id;
                else snc = false;
            }
            else
            {
                Recete.MakinaId = makinaId;
                snc = db.UpdateGeneric<tblKimyasalRecete>(Recete.ViewToTbl());
            }

            return snc;
        }

        public bool ReceteSil(int personelId)
        {
            bool snc = db.DeleteGeneric<tblKimyasalRecetePartileri>(vKimyasalRecetePartileri.ViewToTbl(this.ReceteninPartileri));
            if (snc)
            {
                //bool de = db.DeleteGeneric<tblKimyasalRecete>(this.Recete.ViewToTbl());

                List<tblKimyasalReceteAct> silinecekKRAct = db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == this.Recete.ViewToTbl().Id).ToList();
                bool kimyasalreceteActSilme = db.DeleteGeneric<tblKimyasalReceteAct>(silinecekKRAct);

                if (ReceteOnayaDusmusMu(Recete.Id))
                {
                    List<tblKimyasalReceteActLog> personelEklenecekListe = db.GetGeneric<tblKimyasalReceteActLog>(c => c.ReceteId == Recete.Id).ToList();

                    foreach (tblKimyasalReceteActLog item in personelEklenecekListe)
                    {
                        item.PersonelIdY = personelId;
                    }

                    db.UpdateGeneric<tblKimyasalReceteActLog>(personelEklenecekListe);
                }

                if (kimyasalreceteActSilme)
                {
                    List<tblKimyasalRecete> silinecekKR = db.GetGeneric<tblKimyasalRecete>(c => c.Id == this.Recete.ViewToTbl().Id).ToList();
                    return db.DeleteGeneric<tblKimyasalRecete>(silinecekKR);
                }

                return kimyasalreceteActSilme;
            }

            return snc;
        }

        public bool SiparisleriBoyandiIsaretle()
        {
            List<tblSiparisAct> siparisler = new List<tblSiparisAct>();
            bool snc = true;
            foreach (vKimyasalRecetePartileri item in this.ReceteninPartileri)
            {
                tblPartiler parti = db.GetGeneric<tblPartiler>(c => c.Id == item.PartiId).FirstOrDefault();
                tblSiparisAct siparis = db.GetGeneric<tblSiparisAct>(c => c.Id == parti.SiparisActId).FirstOrDefault();
                
                if (siparis != null)
                {
                    siparis.BoyandiMi = true;
                    //siparisler.Add(siparis);
                    if (db.UpdateGeneric<tblSiparisAct>(siparis) == false) snc = false;
                }
            }

            if (siparisler.Count > 0) return db.UpdateGeneric<tblSiparisAct>(siparisler);

            return snc;
        }

        public static List<tblKumasRenk> KumasRenkleriGetir(bool sadeceAktifler = false)
        {
            if (sadeceAktifler) return new DBEvents().GetGeneric<tblKumasRenk>(c => c.AktifMi == true).OrderBy(c => c.Kodu).ToList();
            return new DBEvents().GetGeneric<tblKumasRenk>().OrderBy(c => c.Kodu).ToList();
        }

        public static List<tblKumasRenkAct> KumasRenkKimyasallariGetir(int renkId)
        {
            return new DBEvents().GetGeneric<tblKumasRenkAct>(c => c.RenkId == renkId);
        }

        public static List<vPartiler> TumPartileriGetir()
        {
            return new DBEvents().GetGeneric<vPartiler>();
        }

        public static List<vKimyasalRecete> ReceteleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vKimyasalRecete>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        //Gökhan 16.05.2014
        public static List<vKimyasalRecete> ReceteleriGetir()
        {
            return new DBEvents().GetGeneric<vKimyasalRecete>(c => c.AktifMi == true);
        }

        //Gökhan 12.05.2014
        public static List<tblMakinalar> MakinalariGetir()
        {
            return new DBEvents().GetGeneric<tblMakinalar>(c => c.BaglantiId == 2 && c.AktifMi == true && c.Id != 177).OrderBy(o=>o.Adi).ToList();
        }

        public static List<tblPersoneller> PersonelleriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 13);
        }

        public static string ReceteAciklamaEkle(vKimyasalRecete recete, string aciklama)
        {
            if (recete == null) return null;

            if (recete.Aciklama == null) recete.Aciklama = "";
            recete.Aciklama += aciklama + "\n";
            if (new DBEvents().UpdateGeneric<tblKimyasalRecete>(recete.ViewToTbl())) return recete.Aciklama;
            else return null;
        }

        List<tblMalzemeler> ListKimyasallar;

        private void KimyasalSifirla(vKimyasalReceteAct kimyasal)
        {
            kimyasal.Id = 0;
            kimyasal.ReceteId = Recete.Id;
            kimyasal.ListKimyasallar = ListKimyasallar;
            kimyasal.Miktar = 0;
        }

        private void AktifReceteYukle()
        {
            tblKimyasalRecete recete = db.GetGeneric<tblKimyasalRecete>(c => c.AktifMi == true && c.RenkId == Recete.RenkId).FirstOrDefault();

            Kasarlar = new List<vKimyasalReceteAct>();
            OnIslemler = new List<vKimyasalReceteAct>();
            Boyalar = new List<vKimyasalReceteAct>();
            Kimyasallar = new List<vKimyasalReceteAct>();
            Apreler = new List<vKimyasalReceteAct>();
            Yikamalar = new List<vKimyasalReceteAct>();

            if (recete != null)
            {
                Kasarlar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.Kasar.ToString());
                OnIslemler = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.OnIslem.ToString());
                Boyalar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.Boya.ToString());
                Kimyasallar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.Kimyasal.ToString());
                Apreler = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.Apre.ToString());
                Yikamalar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == recete.Id && c.Tip == Tipler.Yikama.ToString());
            }

            Kasarlar.ForEach(KimyasalSifirla);
            OnIslemler.ForEach(KimyasalSifirla);
            Boyalar.ForEach(KimyasalSifirla);
            Kimyasallar.ForEach(KimyasalSifirla);
            Apreler.ForEach(KimyasalSifirla);
            Yikamalar.ForEach(KimyasalSifirla);
        }

        public void ReceteleriYukle(bool aktifReceteYuklensinMi)
        {
            ListKimyasallar = db.GetGeneric<tblMalzemeler>(c => c.BaglantiId == 44).OrderBy(o=>o.Adi).ToList();

            if (this.Recete.Id == 0)
            {
                if (aktifReceteYuklensinMi) AktifReceteYukle();
                else
                {
                    Kasarlar = new List<vKimyasalReceteAct>();
                    OnIslemler = new List<vKimyasalReceteAct>();
                    Boyalar = new List<vKimyasalReceteAct>();
                    Kimyasallar = new List<vKimyasalReceteAct>();
                    Apreler = new List<vKimyasalReceteAct>();
                    Yikamalar = new List<vKimyasalReceteAct>();
                }
            }
            else
            {
                Kasarlar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.Kasar.ToString());
                Kasarlar.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                OnIslemler = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.OnIslem.ToString());
                OnIslemler.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                Boyalar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.Boya.ToString());
                Boyalar.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                Kimyasallar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.Kimyasal.ToString());
                Kimyasallar.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                Apreler = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.Apre.ToString());
                Apreler.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                Yikamalar = db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == Recete.Id && c.Tip == Tipler.Yikama.ToString());
                Yikamalar.ForEach(c => c.ListKimyasallar = ListKimyasallar);

                if (Kasarlar.Count == 0 && OnIslemler.Count == 0 && Boyalar.Count == 0 && Kimyasallar.Count == 0 && Apreler.Count == 0 && Yikamalar.Count == 0 && aktifReceteYuklensinMi)
                    AktifReceteYukle();
                else
                {
                    KasarFlote = (Kasarlar != null && Kasarlar.Count > 0) ? Kasarlar[0].Flote : 0;
                    if (Boyalar != null && Boyalar.Count != 0) BoyaKimyasalFlote = Boyalar[0].Flote;
                    if (BoyaKimyasalFlote == 0 && Kimyasallar != null && Kimyasallar.Count != 0) BoyaKimyasalFlote = Kimyasallar[0].Flote;
                    if (Apreler != null && Apreler.Count != 0) ApreFlote = Apreler[0].Flote;
                    if (Yikamalar != null && Yikamalar.Count != 0) YikamaFlote = Yikamalar[0].Flote;
                }
            }
        }

        public double KasarFlote { get; set; }
        public List<vKimyasalReceteAct> Kasarlar { get; set; }

        public double BoyaKimyasalFlote { get; set; }
        public List<vKimyasalReceteAct> Boyalar { get; set; }
        public List<vKimyasalReceteAct> Kimyasallar { get; set; }

        public double ApreFlote { get; set; }
        public List<vKimyasalReceteAct> Apreler { get; set; }

        public double YikamaFlote { get; set; }
        public List<vKimyasalReceteAct> Yikamalar { get; set; }

        public List<vKimyasalReceteAct> OnIslemler { get; set; }

        public void KasarEkle()
        {
            this.Kasarlar.Add(new vKimyasalReceteAct()
            {
                ListKimyasallar = this.ListKimyasallar,
                ReceteId = this.Recete.Id,
                Tip = Tipler.Kasar.ToString()
            });
        }

        public bool KasarOranlariHesapla()
        {
            double tartilan = this.ReceteninPartileri.Sum(c => c.TartilanKg);

            foreach (vKimyasalReceteAct item in Kasarlar)
            {
                if (item.PersonelId != KId)
                {
                    item.PersonelId = KId;
                }
                item.Flote = KasarFlote;
                if (item.GrLtOran == "Gr/Lt")
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * KasarFlote, 2);
                else if (item.GrLtOran == "Oran(%)")
                {
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * tartilan * 10, 2);
                }
            }

            return true;
        }

        public void BoyaKimyasalYukleMuadiklReceteden(int muadilReceteId)
        {
            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.Boya.ToString()))
            {
                this.Boyalar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Boya.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar

                });
            }

            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.Kimyasal.ToString()))
            {
                this.Kimyasallar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Kimyasal.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.OnIslem.ToString()))
            {
                this.OnIslemler.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.OnIslem.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.Kasar.ToString()))
            {
                this.Kasarlar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Kasar.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.Yikama.ToString()))
            {
                this.Yikamalar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Yikama.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            foreach (vKimyasalReceteAct item in db.GetGeneric<vKimyasalReceteAct>(c => c.ReceteId == muadilReceteId && c.Tip == Tipler.Apre.ToString()))
            {
                this.Apreler.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrLtOran,
                    Oran = item.Oran,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Apre.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

        }

        public bool BoyaKimyasalYukle()
        {
            foreach (vKumasRenkAct item in db.GetGeneric<vKumasRenkAct>(c => c.RenkId == this.Recete.RenkId && c.BoyaKimya == "BOYA"))
            {
                this.Boyalar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrOran,
                    Oran = item.Miktar,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Boya.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                    
                });
            }

            foreach (vKumasRenkAct item in db.GetGeneric<vKumasRenkAct>(c => c.RenkId == this.Recete.RenkId && c.BoyaKimya == "KİMYASAL"))
            {
                this.Kimyasallar.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrOran,
                    Oran = item.Miktar,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Kimyasal.ToString(),
                    OnayBirTarih = item.OnayBirTarih,
                    OnayIkiTarih = item.OnayIkiTarih,
                    OnayBirPersonelId = item.OnayBirPersonelId,
                    OnayIkiPersonelId = item.OnayIkiPersonelId,
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            return true;
        }

        public void KimyasalEkle()
        {
            this.Kimyasallar.Add(new vKimyasalReceteAct()
            {
                ListKimyasallar = this.ListKimyasallar,
                ReceteId = this.Recete.Id,
                Tip = Tipler.Kimyasal.ToString()
            });
        }

        public bool BoyaKimyasalOranlariHesapla()
        {
            double tartilan = this.ReceteninPartileri.Sum(c => c.TartilanKg);

            foreach (vKimyasalReceteAct item in Boyalar)
            {
                if (item.PersonelId != KId)
                {
                    item.PersonelId = KId;
                }
                item.Flote = BoyaKimyasalFlote;
                if (item.GrLtOran == "Gr/Lt")
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * BoyaKimyasalFlote, 2);
                else if (item.GrLtOran == "Oran")
                {
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * tartilan * 10, 2);
                }
            }

            foreach (vKimyasalReceteAct item in Kimyasallar)
            {
                if (item.PersonelId != KId)
                {
                    item.PersonelId = KId;
                }
                item.Flote = BoyaKimyasalFlote;
                if (item.GrLtOran == "Gr/Lt")
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * BoyaKimyasalFlote, 2);
                else if (item.GrLtOran == "Oran")
                {
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * tartilan * 10, 2);
                }
            }

            return true;
        }

        public void ApreEkle()
        {
            this.Apreler.Add(new vKimyasalReceteAct()
            {
                ListKimyasallar = this.ListKimyasallar,
                ReceteId = this.Recete.Id,
                Tip = Tipler.Apre.ToString()
            });
        }

        public bool ApreYukle(int apreId)
        {
            foreach (vKumasRenkAct item in db.GetGeneric<vKumasRenkAct>(c => c.RenkId == apreId))
            {
                this.Apreler.Add(new vKimyasalReceteAct()
                {
                    GrLtOran = item.GrOran,
                    Oran = item.Miktar,
                    KimyasalAdi = item.KimyasalAdi,
                    KimyasalId = item.KimyasalId,
                    KimyasalKodu = item.KimyasalKodu,
                    ReceteId = this.Recete.Id,
                    Tip = Tipler.Apre.ToString(),
                    ListKimyasallar = this.ListKimyasallar
                });
            }

            return true;
        }

        public static List<tblKumasRenk> ApreRenkleriGetir()
        {
            return new DBEvents().GetGeneric<tblKumasRenk>(c => c.BoyarMadde == "Apre" && c.AktifMi == true).ToList();
        }

        public bool ApreOranlariHesapla()
        {
            double tartilan = this.ReceteninPartileri.Sum(c => c.TartilanKg);


            foreach (vKimyasalReceteAct item in Apreler)
            {
                if (item.PersonelId != KId)
                {
                    item.PersonelId = KId;
                }
                item.Flote = ApreFlote;
                if (item.GrLtOran == "Gr/Lt")
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * ApreFlote, 2);
            }

            return true;
        }

        public void YikamaEkle()
        {
            this.Yikamalar.Add(new vKimyasalReceteAct()
            {
                ListKimyasallar = this.ListKimyasallar,
                ReceteId = this.Recete.Id,
                Tip = Tipler.Yikama.ToString()
            });
        }

        public bool YikamaOranlariHesapla()
        {
            double tartilan = this.ReceteninPartileri.Sum(c => c.TartilanKg);

            foreach (vKimyasalReceteAct item in Yikamalar)
            {
                if (item.PersonelId != KId)
                {
                    item.PersonelId = KId;
                }
                item.Flote = YikamaFlote;
                if (item.GrLtOran == "Gr/Lt")
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * YikamaFlote, 2);
                else if (item.GrLtOran == "Oran(%)")
                {
                    item.Miktar = Math.Round((item.Oran.HasValue ? item.Oran.Value : 0) * tartilan * 10, 2);
                }
            }

            return true;
        }

        public void OnIslemEkle()
        {
            this.OnIslemler.Add(new vKimyasalReceteAct()
            {
                ListKimyasallar = this.ListKimyasallar,
                ReceteId = this.Recete.Id,
                Tip = Tipler.OnIslem.ToString()
            });
        }

        private List<vKimyasalReceteAct> ListKaydet(List<vKimyasalReceteAct> recete, ref bool snc)
        {
            List<vKimyasalReceteAct> saveView = recete.FindAll(c => c.Id == 0);
            List<tblKimyasalReceteAct> saveTbl = vKimyasalReceteAct.ViewToTable(saveView);
            List<vKimyasalReceteAct> updView = recete.FindAll(c => c.Id != 0); ;

            snc = true;
            if (saveTbl.Count > 0)
            {
                if (db.SaveGeneric<tblKimyasalReceteAct>(ref saveTbl)) for (int i = 0; i < saveTbl.Count; i++) saveView[i].Id = saveTbl[i].Id;
                else
                {
                    snc = false;
                    return recete;
                }
            }

            if (updView.Count > 0) snc = db.UpdateGeneric<tblKimyasalReceteAct>(vKimyasalReceteAct.ViewToTable(updView));

            recete = new List<vKimyasalReceteAct>();
            if (updView.Count > 0) recete.AddRange(updView);

            if (saveView.Count > 0) recete.AddRange(saveView);

            return recete;
        }

        //Gökhan 16.05.2014
        private List<vKimyasalReceteAct> ListKaydetNew(List<vKimyasalReceteAct> recete, ref bool snc)
        {
            List<vKimyasalReceteAct> saveView = recete.FindAll(c => c.Id == 0);
            List<tblKimyasalReceteAct> saveTbl = vKimyasalReceteAct.ViewToTable(saveView);
            List<vKimyasalReceteAct> updView = recete.FindAll(c => c.Id != 0); ;

            snc = true;
            if (saveTbl.Count > 0)
            {
                if (db.SaveGeneric<tblKimyasalReceteAct>(ref saveTbl)) for (int i = 0; i < saveTbl.Count; i++) saveView[i].Id = saveTbl[i].Id;
                else
                {
                    snc = false;
                    return recete;
                }
            }

            if (updView.Count > 0)
            {
                int tumSayi = updView.Count;
                snc = db.UpdateGeneric<tblKimyasalReceteAct>(vKimyasalReceteAct.ViewToTable(updView.Where(c => c.OnayBirPersonelId.HasValue == true && c.OnayIkiPersonelId.HasValue == true).ToList()));

                if (tumSayi != updView.Where(c => c.OnayBirPersonelId.HasValue == true && c.OnayIkiPersonelId.HasValue == true).ToList().Count)
                {
                    mesaj += "Onay bekleyen işlemleriniz varsa kayıt edilmemiştir. ";
                }

            }

            recete = new List<vKimyasalReceteAct>();
            if (updView.Count > 0) recete.AddRange(updView);

            if (saveView.Count > 0) recete.AddRange(saveView);

            return recete;
        }

        public void NuanslariGuncelledim()
        {
            Recete.NuanslariGuncelledim = true;
            bool snc = db.UpdateGeneric<tblKimyasalRecete>(Recete.ViewToTbl());               
        }

        public bool Onayla()
        {
            List<tblKimyasalRecete> aktifler = db.GetGeneric<tblKimyasalRecete>(c => c.RenkId == Recete.RenkId && c.AktifMi == true);
            aktifler.ForEach(c => c.AktifMi = false);

            Recete.AktifMi = true;

            if (db.UpdateGeneric<tblKimyasalRecete>(Recete.ViewToTbl()))
                return db.UpdateGeneric<tblKimyasalRecete>(aktifler);

            return false;
        }

        public bool Kaydet()
        {
            bool snc = true;
            string msj = "";
            mesaj = "";
            if (db.UpdateGeneric<tblKimyasalRecete>(this.Recete.ViewToTbl()) == false) msj += "Açıklama kaydedilemedi.\n";

            ApreOranlariHesapla();
            this.Apreler = ListKaydet(Apreler, ref snc); //ListKaydetNew(Apreler, ref snc); onaylama mekanizması iş yükü oluşturduğu için kapatıldı.
            if (!snc) msj += "Apre kimyasalları kaydedilemedi.\n";
            snc = true;

            BoyaKimyasalOranlariHesapla();
            this.Boyalar = ListKaydet(Boyalar, ref snc); //ListKaydetNew(Boyalar, ref snc);
            if (!snc) msj += "Boya kimyasalları kaydedilemedi.\n";
            snc = true;

            this.Kimyasallar = ListKaydet(Kimyasallar, ref snc); //ListKaydetNew(Kimyasallar, ref snc);
            if (!snc) msj += "Kimyasallar kaydedilemedi.\n";
            snc = true;

            KasarOranlariHesapla();
            this.Kasarlar = ListKaydet(Kasarlar, ref snc); //ListKaydetNew(Kasarlar, ref snc);
            if (!snc) msj += "Boya kasarı kimyasalları kaydedilemedi.\n";
            snc = true;

            this.OnIslemler = ListKaydet(OnIslemler, ref snc); //ListKaydetNew(OnIslemler, ref snc);
            if (!snc) msj += "Ön işlemler kimyasalları kaydedilemedi.\n";
            snc = true;

            YikamaOranlariHesapla();
            this.Yikamalar = ListKaydet(Yikamalar, ref snc);  //ListKaydetNew(Yikamalar, ref snc);
            if (!snc) msj += "Yıkama kimyasalları kaydedilemedi.\n";
            snc = true;

            //Gökhan 16.05.2014 if blogunu
            if (mesaj != "")
            {
                if (msj == "")
                {
                    throw new Exception("bos");
                }
            }

            if (msj != "") throw new Exception(msj);
            else return true;
        }

        //Gökhan 02.06.2014
        public bool OnaysizKimyasalVarMiRenkKartlari(int renkId)
        {
            return db.GetGeneric<vKimyasalReceteActLog>(c => c.logTuru == 1 && (c.OnayBirPersonelId == null || c.OnayIkiPersonelId == null) && c.RenkId == renkId).Any();
        }

        //Gökhan 16.05.2014
        public void KaydetNew(int Personel)
        {
            KId = Personel;
            Kaydet();
        }

        public bool KasarSil(vKimyasalReceteAct silinecek)
        {
            if (silinecek.Id == 0)
            {
                Kasarlar.Remove(silinecek);
                return true;
            }

            else
            {
                if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
                {
                    Kasarlar.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool KasarSilVeritabanindan(vKimyasalReceteAct silinecek)
        {

            if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
            {
                return true;
            }
            else
                return false;           

        }

        public bool OnIslemSil(vKimyasalReceteAct silinecek)
        {
            if (silinecek.Id == 0)
            {
                OnIslemler.Remove(silinecek);
                return true;
            }

            else
            {
                if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
                {
                    OnIslemler.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool KimyasalSil(vKimyasalReceteAct silinecek)
        {
            if (silinecek.Id == 0)
            {
                Kimyasallar.Remove(silinecek);
                return true;
            }

            else
            {
                if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
                {
                    Kimyasallar.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool ApreSil(vKimyasalReceteAct silinecek)
        {
            if (silinecek.Id == 0)
            {
                Apreler.Remove(silinecek);
                return true;
            }

            else
            {
                if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
                {
                    Apreler.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool YikamaSil(vKimyasalReceteAct silinecek)
        {
            if (silinecek.Id == 0)
            {
                Yikamalar.Remove(silinecek);
                return true;
            }

            else
            {
                if (db.DeleteGeneric<tblKimyasalReceteAct>(silinecek.ViewToTable()))
                {
                    Yikamalar.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool KimyasalStokCikisiYap()
        {
            List<tblMalzemeCikis> cikislar = new List<tblMalzemeCikis>();
            string stoktaOlmayanlar = "";

            foreach (var item in this.Apreler)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            foreach (var item in this.Boyalar)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            foreach (var item in this.Kasarlar)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            foreach (var item in this.Kimyasallar)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            foreach (var item in this.OnIslemler)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            foreach (var item in this.Yikamalar)
            {
                double stok = Convert.ToDouble(db.GetGenericWithSQLQuery<string>("select dbo.fKimyasalStokMiktariGetir({0})", new object[] { item.KimyasalId }).FirstOrDefault().Replace('.', ','));
                if (stok < (double)(item.Miktar / 1000)) stoktaOlmayanlar += (item.KimyasalKodu + " - " + item.KimyasalAdi) + "\n";
            }

            if (stoktaOlmayanlar != "") throw new Exception("Stokta olmayan kimyasallar var.\nStok çıkışı yapılamaz..!\n\n" + stoktaOlmayanlar);

            string snc = db.GetGenericWithSQLQuery<string>("exec dbo.spKimyasalStokCikisYap " + this.Recete.Id.ToString() + "," + this.Recete.PersonelId.ToString(), new string[0]).FirstOrDefault();

            bool yapildiMi = string.Equals(snc, "1");
            if (yapildiMi)
            {
                this.Recete.StoktanDusulduMu = true;
                tblKimyasalRecete tbl = this.Recete.ViewToTbl();
                return db.UpdateGeneric<tblKimyasalRecete>(tbl);
            }

            return yapildiMi;
        }

        public List<vKimyasalRecetePartileri> RecetePartiBilgileriGetir()
        {
            foreach (vKimyasalRecetePartileri item in this.ReceteninPartileri)
            {
                vPartiler parti = new DBEvents().GetGeneric<vPartiler>(c => c.Id == item.PartiId).FirstOrDefault();
                if (parti == null) continue;

                item.RenkNo = parti.RenkNo;
                item.TipNo = parti.TipNo;
                item.SiparisNo = parti.SozlesmeNo;
                item.Firma = parti.MusteriAdi;
            }

            return this.ReceteninPartileri;
        }

        //Gökhan 16.05.2014
        public bool SilenPersonelKaydet(int silinecekId, int KullaniciId)
        {
            tblKimyasalReceteActLog tempKRL = db.GetGeneric<tblKimyasalReceteActLog>(c => c.SilinenId == silinecekId && c.Turu == 2).LastOrDefault();
            if (tempKRL != null)
            {
                tempKRL.PersonelIdY = KullaniciId;
                return new DBEvents().UpdateGeneric<tblKimyasalReceteActLog>(tempKRL);
            }
            else
            {
                return false;
            }

        }

        //Gökhan 06.06.2014
        public bool ReceteOnayaDusmusMu(int receteId)
        {
            return db.GetGeneric<tblKimyasalReceteActLog>(c => c.ReceteId == receteId && (c.OnayBirTarih.HasValue == false || c.OnayIkiTarih.HasValue == false)).Any();
        }

        //Gökhan 16.05.2014
        public bool OnaylanmamisMi(int receteId)
        {
            return db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == receteId && (c.OnayBir.HasValue == false || c.OnayIki.HasValue == false)).Any();
        }

        //Gökhan 16.05.2014
        public bool KimyasalDegisiklikleriniOnayla(List<vKimyasalReceteActLog> onaylanacaklar, int personID)
        {
            List<vKimyasalReceteAct> tempKRdL = new List<vKimyasalReceteAct>();
            List<vKumasRenkAct> RenkListesi = new List<vKumasRenkAct>();
            
            foreach (vKimyasalReceteActLog item in onaylanacaklar)
            {
                if (item.OnayBirPersonelId.HasValue == false)
                {
                    item.OnayBirPersonelId = personID;
                    item.OnayBirTarih = DateTime.Now;
                    
                }
                else
                {
                    item.OnayIkiPersonelId = personID;
                    item.OnayIkiTarih = DateTime.Now;
                }
                tempKRdL.AddRange(db.GetGeneric<vKimyasalReceteAct>(c => c.Id == item.SilinenId));

                if (item.logTuru == 1)
                {
                    RenkListesi.AddRange(db.GetGeneric<vKumasRenkAct>(c => c.Id == item.SilinenId));
                }
            }

            foreach (vKimyasalReceteAct item in tempKRdL)
            {
                if (item.OnayBirPersonelId.HasValue == false)
                {
                    item.OnayBirPersonelId = personID;
                    item.OnayBirTarih = DateTime.Now;
                }
                else
                {
                    item.OnayIkiPersonelId = personID;
                    item.OnayIkiTarih = DateTime.Now;
                }
            }

            foreach (vKumasRenkAct item in RenkListesi)
            {
                if (item.OnayBirPersonelId.HasValue == false)
                {
                    item.OnayBirPersonelId = personID;
                    item.OnayBirTarih = DateTime.Now;
                }
                else
                {
                    item.OnayIkiPersonelId = personID;
                    item.OnayIkiTarih = DateTime.Now;
                }
            }

            bool returnValue = false;

            if (RenkListesi.Count == 0)
            {
                if (db.UpdateGeneric(vKimyasalReceteActLog.ViewToTable(onaylanacaklar)) == true && db.UpdateGeneric(vKimyasalReceteAct.ViewToTable(tempKRdL)) == true) returnValue = true;
            }
            else
            {
                if (db.UpdateGeneric(vKimyasalReceteActLog.ViewToTable(onaylanacaklar)) == true && db.UpdateGeneric(vKimyasalReceteAct.ViewToTable(tempKRdL)) == true && db.UpdateGeneric(vKumasRenkAct.ViewToTbl(RenkListesi)) == true) returnValue = true;
            }

            return returnValue;
        }

        //Gökhan 16.05.2014
        public bool OnayKontrolKimyasal(int receteId)
        {
            return db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == receteId && (c.OnayBir.HasValue == false || c.OnayIki.HasValue == false)).Any();
        }

        //Gökhan 16.05.2014
        public bool OnayKontrol(int receteId)
        {
            return db.GetGeneric<tblKimyasalRecete>(c => c.Id == receteId && c.AktifMi == true).Any();
        }

        //Gökhan 16.05.2014
        public bool ReceteKayitliMi(int receteId)
        {
            return db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == receteId).Any();
        }

        //Gökhan 16.05.2014
        public bool SilinecekVarMi(int receteId)
        {
            List<tblKimyasalReceteAct> tempKRA = db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == receteId); 
            int toplamkayit = tempKRA.Count;
            int toplamkayitNull = tempKRA.Where(c => c.OnayBir.HasValue == true && c.OnayIki.HasValue == true).ToList().Count;
            if (toplamkayit == toplamkayitNull)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Gökhan 16.05.2014
        public bool receteAyarla(int receteId,int personelId, bool onay)
        {
            List<tblKimyasalReceteActLog> tempKRAL = new List<tblKimyasalReceteActLog>();
            List<tblKimyasalReceteAct> tempKRA = db.GetGeneric<tblKimyasalReceteAct>(c => c.ReceteId == receteId).ToList();
            tblKimyasalReceteActLog temp; 



            if (!onay)
            {
                //Onaysızı Onaylama

                foreach (tblKimyasalReceteAct item in tempKRA)
                {
                    temp = new tblKimyasalReceteActLog();
                    temp.SilinenId = item.Id;
                    temp.ReceteId = item.ReceteId;
                    temp.OranE = null;
                    temp.OranY = item.Oran;
                    temp.GrLtOranE = null;
                    temp.GrLtOranY = item.GrLtOran;
                    temp.MiktarE = null;
                    temp.MiktarY = item.Miktar;
                    temp.TipE = null;
                    temp.TipY = item.Tip;
                    temp.FloteE = null;
                    temp.FloteY = item.Flote;
                    temp.Turu = 4;//New
                    temp.Tarih = DateTime.Now;
                    temp.Saat = DateTime.Now;
                    temp.PersonelIdE = null;
                    temp.PersonelIdY = personelId;
                    temp.KimyasalIdE = null;
                    temp.KimyasalIdY = item.KimyasalId;
                    temp.OnayBir = null;
                    temp.OnayBirTarih = null;
                    temp.OnayIki = null;
                    temp.OnayIkiTarih = null;
                    temp.logTuru = 0;
                    
                    tempKRAL.Add(temp);
                }
                
                return db.SaveGeneric<tblKimyasalReceteActLog>(tempKRAL);
            }
            else
            {
                //Onaylının onayını kaldırma
                foreach (tblKimyasalReceteAct item in tempKRA)
                {
                    item.OnayBir = null;
                    item.OnayBirTarih = null;
                    item.OnayIki = null;
                    item.OnayIkiTarih = null;

                }
                return (db.UpdateGeneric<tblKimyasalReceteAct>(tempKRA) && db.DeleteGeneric<tblKimyasalReceteActLog>(db.GetGeneric<tblKimyasalReceteActLog>(c => c.ReceteId == receteId && c.OnayBir.HasValue == false && c.OnayIki.HasValue == false).ToList()));
            }
        }

        //Gökhan 16.05.2014 
        public bool temizle(int receteId)
        {
            tblKimyasalRecete tempKR = db.GetGeneric<tblKimyasalRecete>(c => c.Id == receteId).LastOrDefault();
            if (tempKR.RenkId == null)
            {
                return true;//O renk yeniyse
            }

            List<tblKimyasalRecete> tempKR1 = db.GetGeneric<tblKimyasalRecete>(c => c.RenkId == tempKR.RenkId && c.AktifMi == true).ToList();

            if (tempKR1.Count > 0)
            {
                List<tblKimyasalReceteActLog> liste = db.GetGeneric<tblKimyasalReceteActLog>(c => c.ReceteId == tempKR1.ElementAt(0).Id);
                int toplamKayit = liste.Count;
                int doluKayit = liste.Where(c => c.OnayBir.HasValue == true && c.OnayIki.HasValue == true).Count();
                int bosKayit = liste.Where(c => c.OnayBir.HasValue == false && c.OnayIki.HasValue == false).Count();
                if ((toplamKayit - doluKayit) == bosKayit)
                {
                    return db.DeleteGeneric<tblKimyasalReceteActLog>(liste.Where(c => c.OnayBir.HasValue == false && c.OnayIki.HasValue == false).ToList());
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return true;//O renkte reçete ilk ise
            }
        }

        public void ReceteRename(int receteId)
        {
            tblKimyasalRecete tempKR = db.GetGeneric<tblKimyasalRecete>(c => c.Id == receteId).LastOrDefault();
            if (tempKR == null)
            {
                return;//O renk yeniyse
            }
            string temps = tempKR.ReceteNo;

            List<tblKimyasalRecete> tempKR1 = db.GetGeneric<tblKimyasalRecete>(c => c.RenkId == tempKR.RenkId && c.AktifMi == true).ToList();
            
            if (tempKR1.Count > 0)
            {
                int tempi1 = temps.IndexOf("-");
                if (tempi1 == -1)
                {
                    temps = temps + "-1";
                }
                else
                {
                    string numara1 = temps.Substring(tempi1 + 1);
                    int temp = Convert.ToInt32(numara1);
                    temp += 1;
                    temps = temps.Substring(0, tempi1) + "-" + temp;
                }

                tblKimyasalRecete instance = db.GetGeneric<tblKimyasalRecete>(c => c.Id == tempKR1.ElementAt(0).Id).LastOrDefault();
                instance.ReceteNo = temps;
                db.UpdateGeneric<tblKimyasalRecete>(instance);
                return;

            }
            else
            {
                return;//O renkte reçete ilk ise
            }

        }

        public int OnaylininId(int receteId)
        {
            tblKimyasalRecete tempKR = db.GetGeneric<tblKimyasalRecete>(c => c.Id == receteId).LastOrDefault();
            List<tblKimyasalRecete> tempKR1 = db.GetGeneric<tblKimyasalRecete>(c => c.RenkId == tempKR.RenkId && c.AktifMi == true).ToList();
            if (tempKR1.Count > 0)
            {
                return tempKR1.ElementAt(0).Id;
            }
            else
            {
                return 0;
            }
            
        }

        public static List<vKimyasalReceteMuadiller> MuadilReceteleriGetir(int renkId)
        {
            return new DBEvents().GetGeneric<vKimyasalReceteMuadiller>(c => c.RenkId == renkId).OrderByDescending(o => o.Tarih).ToList();
        }

    }
}

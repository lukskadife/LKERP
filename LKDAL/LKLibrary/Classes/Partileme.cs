using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Partileme
    {
        public class RefakatKartRapor
        {
            private DBEvents db = new DBEvents();

            public RefakatKartRapor(int partiId)
            {
                _Parti = db.GetGeneric<vPartiler>(c => c.Id == partiId);
                _Siparis = db.GetGeneric<tblSiparisAct>(c => c.Id == _Parti[0].SiparisActId);
                _PartiProcesses = db.GetGeneric<vPartiProcessleri>(c => c.PartiId == partiId).OrderBy(o => o.Sira).ToList();
                _PlanlananKumaslar = db.GetGeneric<vHamKumaslar>(c => c.PartiIdPlanlanan == partiId);
                if (_Parti[0].ApreId != null)
                {
                    _ApreRenk = db.GetGeneric<tblKumasRenk>(c => c.Id == _Parti[0].ApreId);
                    _ApreKimyasallari = db.GetGeneric<vKumasRenkAct>(c => c.RenkId == _Parti[0].ApreId.Value);
                }
                else
                {
                    _ApreRenk = new List<tblKumasRenk>();
                    _ApreKimyasallari = new List<vKumasRenkAct>();
                }
            }

            private List<vPartiler> _Parti;
            public List<vPartiler> Parti { get { return _Parti; } }

            private List<tblSiparisAct> _Siparis;
            public List<tblSiparisAct> Siparis { get { return _Siparis; } }

            private List<vPartiProcessleri> _PartiProcesses;
            public List<vPartiProcessleri> PartiProcesses { get { return _PartiProcesses; } }

            private List<vKumasRenkAct> _ApreKimyasallari;
            public List<vKumasRenkAct> ApreKimyasallari { get { return _ApreKimyasallari; } }

            private List<tblKumasRenk> _ApreRenk;
            public List<tblKumasRenk> ApreRenk { get { return _ApreRenk; } }

            private List<vHamKumaslar> _PlanlananKumaslar;
            public List<vHamKumaslar> PlanlananKumaslar { get { return _PlanlananKumaslar; } }

        }

        private DBEvents db = new DBEvents();

        #region statics

        public static bool PartiDuzelt(vPartiler parti)
        {
            if (parti == null) return false;
            return new DBEvents().UpdateGeneric<tblPartiler>(parti.ViewToTbl());
        }

        public static List<vGrupProcess> GrupProcessleriGetir()
        {
            return new DBEvents().GetGeneric<vGrupProcess>(c => c.AktifMi == true);
        }

        public static List<vPartiler> PartileriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vPartiler>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static List<tblProses> ProcessleriGetir()
        {
            return new DBEvents().GetGeneric<tblProses>(c => c.AktifMi == true);
        }

        public static List<vSiparisler> MusterileriGetir()
        {
            List<vSiparisler> list = new DBEvents().GetGeneric<vSiparisler>(c => c.Durum == "Açık" && c.BaglantiId != 3 );//&& c.BaglantiId != 4);
            if (list == null) return null;
            return list.GroupBy(c => c.FirmaId).Select(s => s.First()).OrderBy(o => o.FirmaAdi).ToList();
        }

        public static List<tblPersoneller> PlanlamaPersonelleriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 6 && c.AktifMi == true).OrderBy(c => c.Adi).ToList();
        }

        public static List<tblPersoneller> BoyaPersonelleriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 12 && c.AktifMi == true).OrderBy(c => c.Adi).ToList();
        }

        public static List<vSiparisler> MusteriSiparisleriGetir(int musteriId)
        {
            return new DBEvents().GetGeneric<vSiparisler>(c => c.FirmaId == musteriId && c.Durum == "Açık" && c.BaglantiId != 3);// && c.BaglantiId != 4);
        }

        public static List<vSiparisAct> SiparisTipleriGetir(int siparisId)
        {
            List<vSiparisAct> sipTipleri = new DBEvents().GetGeneric<vSiparisAct>(c => c.SiparisId == siparisId);
            foreach (vSiparisAct item in sipTipleri)
            {
                tblProsesGrup finish = new DBEvents().GetGeneric<tblProsesGrup>(c => c.Id == item.FinishGrupId).FirstOrDefault();
                item.FinishAdi = finish == null ? "" : finish.Adi;
                List<tblPartiler> partilenenler = new DBEvents().GetGeneric<tblPartiler>(c => c.SiparisActId == item.Id);
                item.PartilenenMetre = partilenenler == null ? 0 : partilenenler.Sum(s => s.PartiMetre);
            }

            return sipTipleri;
        }

        public static List<tblKumasRenk> ApreleriGetir()
        {
            return new DBEvents().GetGeneric<tblKumasRenk>(c => c.BoyarMadde == "Apre").OrderBy(o => o.Kodu).ToList();
        }

        public static List<tblFinish> FinishKartlariGetir()
        {
            return new DBEvents().GetGeneric<tblFinish>().OrderBy(o => o.Kodu).ToList();
        }

        public static vPartiler PartiGetir(int partiId)
        {
            return new DBEvents().GetGeneric<vPartiler>(c => c.Id == partiId).FirstOrDefault();
        }
        //3 Mayıs 2015 Şükrü
        public static vBoyaProgramiSukru BoyaProgramiGetir(int partiId)
        {
            return new DBEvents().GetGeneric<vBoyaProgramiSukru>(c => c.PartiId == partiId).FirstOrDefault();
        }

        #endregion

        public Partileme(vPartiler parti, bool barkodlarGetirilsinMi = false)
        {
            if (parti.PartiNo == null)
            {
                Parti = parti;
                string sonKayit = "";
                if (parti.RePartiMi == false)
                    sonKayit = new DBEvents().GetGenericWithSQLQuery<string>("select dbo.fSonPartiNumarasiGetir(0) as Sonuc", new string[0]).FirstOrDefault();
                else sonKayit = new DBEvents().GetGenericWithSQLQuery<string>("select dbo.fSonPartiNumarasiGetir(1) as Sonuc", new string[0]).FirstOrDefault();
                this.Parti.PartiNo = (sonKayit != null) ? sonKayit : "1";
                this.Barkodlar = new List<vHamKumaslar>();
                this.ReBarkodlar = new List<vReProcessBarkodlari>();
            }
            else
            {
                Parti = parti;
                if (barkodlarGetirilsinMi)
                {
                    if (parti.RePartiMi) this.ReBarkodlar = db.GetGenericWithSQLQuery<vReProcessBarkodlari>("execute dbo.spRePartiBarkodlariGetir " + parti.Id.ToString(), new string[0]);
                    else
                    {
                        this.Barkodlar = db.GetGeneric<vHamKumaslar>(c => c.PartiId == parti.Id);
                        this.PlanlananBarkodlar = db.GetGeneric<vHamKumaslar>(c => c.PartiIdPlanlanan == parti.Id);
                    }
                }
                this._Processler = db.GetGeneric<vPartiProcessleri>(c => c.PartiId == parti.Id).OrderBy(o=>o.Sira).ToList();
            }

            Parti.FinishKodChanged += new EventHandler(Parti_FinishKodChanged);
        }

        private void Parti_FinishKodChanged(object sender, EventArgs e)
        {
            //List<vGrupProcess> grup = db.GetGeneric<vGrupProcess>(c => c.GrupId == this.Parti.FinishKodId && c.AktifMi == true);

            //this._Processler = vGrupProcess.GrupProcessToPartiProcess(grup, Parti.Id);
        }

        public vPartiler Parti { get; set; }

        private List<vPartiProcessleri> _Processler = new List<vPartiProcessleri>();
        public List<vPartiProcessleri> Processler
        {
            get
            {
                return _Processler;
            }
            set
            {
                _Processler = value;
            }
        }
        public List<vHamKumaslar> Barkodlar { get; set; }
        public List<vHamKumaslar> PlanlananBarkodlar { get; set; }
        public List<vReProcessBarkodlari> ReBarkodlar { get; set; }

        public string PartiKullanilmisMi()
        {
            if (this.Parti == null) return "";
            tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.PartiId == this.Parti.Id).FirstOrDefault();
            if (mamul != null) return "Partiden mamul kumaş kesilmiş.\n\nSiparişi değiştirilemez..!";

            tblBoyahaneProcess process = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id).FirstOrDefault();
            if (process != null) return "Parti boyahanede işlem görmüş.\n\nSiparişi değiştirilemez..!";

            return "";
        }

        private bool HamBarkoduEkle(string barkod)
        {
            //if (Parti.BoyahaneOnay == false) throw new Exception("Boyahane onayı yok.\n\nBarkod okutulamaz..!");

            vHamKumaslar kumas = db.GetGeneric<vHamKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (kumas == null) throw new Exception("Barkod hatalı.!");

            if (kumas.PartiId != null && kumas.PartiId != 0) throw new Exception("Bu barkod ham stokta değil!");

            if (kumas.PartiIdPlanlanan != Parti.Id) throw new Exception("Planda yok. Yanlış barkot!");

            //if (!Parti.FarkliSiparisKabul && Parti.MusteriId != kumas.MusteriId) throw new Exception("Bu tip farklı müşteri siparişidir.\n\nKabul edilemez..!\n\nOkutulan tip no : " + kumas.TipNo);

            vSiparisAct tip = db.GetGeneric<vSiparisAct>(c => c.Id == Parti.SiparisActId).FirstOrDefault();

            //Ham Kumaş plan kontrolü yapılmalı.



            //Eski versiyonda ki kontroldür. Artık ham kumaş planlaması yapılıyor.
            //if (tip.TipMalzemeKodu != null && tip.TipMalzemeKodu.StartsWith("03.J"))
            //{
            //    tblPartiler prt = db.GetGeneric<tblPartiler>(c => c.Id == Parti.Id).FirstOrDefault();
            //    if (kumas.Varyant != prt.TipVaryant || (tip.TipNo != kumas.TipNo && Parti.DigerTipNo1 != kumas.TipNo && Parti.DigerTipNo2 != kumas.TipNo && Parti.DigerTipNo3 != kumas.TipNo))
            //        throw new Exception("Bu tip ve varyant, bu parti için kabul edilemez..!\n\nOkutulan tip no : " + kumas.TipNo + "\nVaryant : " + kumas.Varyant);
            //}

            //else if (tip.TipNo != kumas.TipNo && Parti.DigerTipNo1 != kumas.TipNo && Parti.DigerTipNo2 != kumas.TipNo && Parti.DigerTipNo3 != kumas.TipNo)// && kumas.Varyant != tip.Varyant)
            //    throw new Exception("Bu tip, bu parti için kabul edilemez..!\n\nOkutulan tip no : " + kumas.TipNo);// + "\nVaryant : " + kumas.Varyant);

            tblHamKumaslar tblKumas = kumas.ViewToTbl();
            tblKumas.PartiId = Parti.Id;
            if (db.UpdateGeneric<tblHamKumaslar>(tblKumas)) kumas.Id = Parti.Id;

            if (this.Barkodlar == null) this.Barkodlar = new List<vHamKumaslar>();

            this.Barkodlar.Add(kumas);

            return true;
        }

        private bool ReProcessBarkoduEkle(string barkod)
        {
            if (Parti.BoyahaneOnay == false) throw new Exception("Boyahane onayı yok.\n\nBarkod okutulamaz..!");

            vReProcessBarkodlari reBarkod = db.GetGenericWithSQLQuery<vReProcessBarkodlari>("exec spReProcSepetiGetir", new string[0]).Find(c => c.Barkod == barkod);

            if (reBarkod == null)
            {
                tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

                if (mamul.Durum != "BoyaSepeti") throw new Exception("Boyahane sepetine atılmamış.\n\nOkutulamaz..!");

                throw new Exception("Reprocess kaydı bulunamadı veya önceden okutuldu..!");
            }

            //if (reBarkod.PartiId != null && kumas.PartiId != 0) throw new Exception("Bu barkod önceden okutuldu..!");

            tblSiparisAct tip = db.GetGeneric<tblSiparisAct>(c => c.Id == Parti.SiparisActId).FirstOrDefault();
            if (tip.TipId != reBarkod.TipId && Parti.DigerTipNo1 != reBarkod.TipNo && Parti.DigerTipNo2 != reBarkod.TipNo && Parti.DigerTipNo3 != reBarkod.TipNo)
                throw new Exception("Bu tip, bu parti için kabul edilemez..!\n\nOkutulan tip no : " + reBarkod.TipNo);

            if (this.ReBarkodlar == null) this.ReBarkodlar = new List<vReProcessBarkodlari>();

            if (reBarkod.Ayirac == "Mamul")
            {
                tblMamulKumaslar mamulBarkod = db.GetGeneric<tblMamulKumaslar>(c => c.Id == reBarkod.Id).FirstOrDefault();
                mamulBarkod.Durum = "ReProcess";
                mamulBarkod.RePartiId = this.Parti.Id;
                if (db.UpdateGeneric<tblMamulKumaslar>(mamulBarkod))
                {
                    this.ReBarkodlar.Add(reBarkod);
                    return true;
                }
                else return false;
            }

            else if (reBarkod.Ayirac == "Iade")
            {
                tblIadeler iadeBarkod = db.GetGeneric<tblIadeler>(c => c.Id == reBarkod.Id).FirstOrDefault();
                iadeBarkod.PartiId = this.Parti.Id;
                iadeBarkod.Durum = "ReProcess";
                reBarkod.PartiId = this.Parti.Id;
                if (db.UpdateGeneric<tblIadeler>(iadeBarkod))
                {
                    this.ReBarkodlar.Add(reBarkod);
                    return true;
                }
                else return false;
            }

            return true;
        }

        public bool BarkodEkle(string okutulanBarkod)
        {
            if (Parti.RePartiMi == false) return HamBarkoduEkle(okutulanBarkod);
            else return ReProcessBarkoduEkle(okutulanBarkod);            
        }

        public bool BarkodSil(vHamKumaslar silinecekBarkod)
        {
            tblHamKumaslar ham = db.GetGeneric<tblHamKumaslar>(c => c.Id == silinecekBarkod.Id).FirstOrDefault();
            if (ham == null) return false;
            ham.PartiId = null;
            if (db.UpdateGeneric<tblHamKumaslar>(ham))
            {
                this.Barkodlar.Remove(silinecekBarkod);
                return true;
            }
            return false;
        }

        public bool PlanSil(vHamKumaslar silinecekBarkod)
        {
            tblHamKumaslar ham = db.GetGeneric<tblHamKumaslar>(c => c.Id == silinecekBarkod.Id).FirstOrDefault();
            if (ham == null) return false;
            ham.PartiIdPlanlanan = null;
            if (db.UpdateGeneric<tblHamKumaslar>(ham))
            {                
                this.PlanlananBarkodlar.Remove(silinecekBarkod);
                return true;
            }
            return false;
        }

        public List<vHamKumaslar> PlanlanmamisTipBazliHamKumaslariGetir(string tipNo, string tipVaryant, string DigerTipNo1, string DigerTipNo2, string DigerTipNo3)
        {
            List<vHamKumaslar> kumaslar = new List<vHamKumaslar>();
            List<vHamKumaslar> Diger1 = new List<vHamKumaslar>();
            List<vHamKumaslar> Diger2 = new List<vHamKumaslar>();
            List<vHamKumaslar> Diger3 = new List<vHamKumaslar>();
            
            kumaslar = new DBEvents().GetGeneric<vHamKumaslar>(c => c.TipNo == tipNo && c.PartiId== null).ToList();
            kumaslar = kumaslar.Where(c => c.PartiIdPlanlanan == 0 | c.PartiIdPlanlanan == null).ToList();

            if (tipVaryant.Length > 0)
                kumaslar = kumaslar.Where(c => c.Varyant == tipVaryant).ToList();

            if (DigerTipNo1 != null)
            {
                Diger1 = new DBEvents().GetGeneric<vHamKumaslar>(c => c.TipNo.Equals(DigerTipNo1.ToString()) && c.PartiId== null).ToList();
                Diger1 = Diger1.Where(c => c.PartiIdPlanlanan == 0 | c.PartiIdPlanlanan == null).ToList();
                foreach (var item in Diger1)
                {
                    kumaslar.Add(item);
                }
            }

            if (DigerTipNo2 != null)
            {
                Diger2 = new DBEvents().GetGeneric<vHamKumaslar>(c => c.TipNo.Equals(DigerTipNo2.ToString()) && c.PartiId == null).ToList();
                Diger2 = Diger2.Where(c => c.PartiIdPlanlanan == 0 | c.PartiIdPlanlanan == null).ToList();
                foreach (var item in Diger2)
                {
                    kumaslar.Add(item);
                }
            }

            if (DigerTipNo3 != null)
            {
                Diger3 = new DBEvents().GetGeneric<vHamKumaslar>(c => c.TipNo.Equals(DigerTipNo3.ToString()) && c.PartiId == null).ToList();
                Diger3 = Diger3.Where(c => c.PartiIdPlanlanan == 0 | c.PartiIdPlanlanan == null).ToList();
                foreach (var item in Diger3)
                {
                    kumaslar.Add(item);
                }
            }

            return kumaslar;
        }       

        public bool BarkodSil(vReProcessBarkodlari silinecekReBarkod)
        {
            tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Id == silinecekReBarkod.Id).FirstOrDefault();
            mamul.RePartiId = null;
            mamul.Durum = "BoyaSepeti";
            if (db.UpdateGeneric<tblMamulKumaslar>(mamul))
            {
                this.ReBarkodlar.Remove(silinecekReBarkod);
                return true;
            }
            return false;
        }

        public bool ProcessEkle(vGrupProcess process)
        {
            _Processler.Add(process.GrupProcessToPartiProcess(Parti.Id));
            return true;
        }

        public bool ProcessEkle(tblProses process)
        {
            if (_Processler == null) _Processler = new List<vPartiProcessleri>();

            _Processler.Add(new vPartiProcessleri()
            {
                PartiId = Parti.Id,
                ProcessAdi = process.Adi,
                ProcessId = process.Id,
                ProcessKodu = process.Kodu,
                Sira = _Processler.Count + 1
            });

            return true;
        }

        public void GrupProsessleriEkle(tblProsesGrup grup)
        {
            List<tblProsesGrupAct> grupProsesleri = db.GetGeneric<tblProsesGrupAct>(c => c.GrupId == grup.Id);
            if (grupProsesleri != null) grupProsesleri = grupProsesleri.OrderBy(o => o.Sira).ToList();

            foreach (tblProsesGrupAct item in grupProsesleri)
            {
                tblProses proses = db.GetGeneric<tblProses>(c=>c.Id == item.ProcessId).FirstOrDefault();
                if (proses != null && proses.AktifMi) this.ProcessEkle(proses);
            }
        }

        public bool ProcessSil(vPartiProcessleri process)
        {
            if (process.Id == 0)
            {
                this._Processler.Remove(process);
                return true;
            }
            else
            {
                if (db.DeleteGeneric<tblPartiProsesleri>(process.ViewToTbl()))
                {
                    this._Processler.Remove(process);
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeYenilensinMi">processler kaydedildikten sonra id'lerini almış olarak yenilensin mi ?</param>
        /// <returns></returns>
        private bool ProcessleriKaydet(bool listeYenilensinMi)
        {
            this._Processler.ForEach(c => c.PartiId = this.Parti.Id);

            //önceden partiye kayıtlı olan prosesler db'den silinir ve tekrar insert çalışır.
            List<tblPartiProsesleri> silinecekler = db.GetGeneric<tblPartiProsesleri>(c => c.PartiId == this.Parti.Id);
            if (db.DeleteGeneric<tblPartiProsesleri>(silinecekler) == false) return false;
            List<tblPartiProsesleri> toSaveTbl = vPartiProcessleri.ViewToTbl(_Processler);

            if (_Processler.Count > 0)
            {
                if (db.SaveGeneric<tblPartiProsesleri>(ref toSaveTbl) == true)
                    for (int i = 0; i < _Processler.Count; i++)
                        _Processler[i].Id = toSaveTbl[i].Id;
                else return false;
            }

            return true;
        }

        //Parti ve processleri kaydeder. Processler kaydedilemez ise exception atar.
        public bool PartiKaydet()
        {
            if (Parti == null) return false;

            tblPartiler parti = this.Parti.ViewToTbl();
            if (Parti.Id == 0)
            {
                tblPartiler partiNoKontrol = db.GetGeneric<tblPartiler>(c => c.PartiNo == Parti.PartiNo).FirstOrDefault();
                if (partiNoKontrol != null) throw new Exception("Parti No daha önce kullanılmıştır..!");

                if (db.SaveGeneric<tblPartiler>(ref parti) == false) return false;
            }
            if (parti.Id != 0 && db.UpdateGeneric<tblPartiler>(parti) == false) return false;
            this.Parti.Id = parti.Id;

            if (ProcessleriKaydet(false) == false) throw new Exception("Processler kaydedilemedi..!");

            return true;
        }

        public bool PartiSil()
        {
            tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.PartiId == this.Parti.Id).FirstOrDefault();
            if (mamul != null) throw new Exception("Partiden mamul kumaş kesilmiş.\n\nSilinemez..!");

            tblBoyahaneProcess process = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id).FirstOrDefault();
            if (process != null) throw new Exception("Parti boyahanede işlem görmüş.\n\nSilinemez..!");

            tblPartiler tbl = this.Parti.ViewToTbl();
            bool snc = true;            

            if (this.Barkodlar != null && this.Barkodlar.Count > 0)
            {
                List<tblHamKumaslar> barkodlar = vHamKumaslar.ViewToTbl(this.Barkodlar);
                barkodlar.ForEach(c => c.PartiId = null);

                snc = db.UpdateGeneric<tblHamKumaslar>(barkodlar);
            }
            
            if (snc == true && db.DeleteGeneric<tblPartiProsesleri>(vPartiProcessleri.ViewToTbl(this.Processler))) snc = db.DeleteGeneric<tblPartiler>(tbl);

            return false;
        }

        public bool RefakatKartiCikartildi()
        {
            tblPartiler parti = this.Parti.ViewToTbl();
            parti.PartilendiMi = true;
            parti.PartilendiTarihi = DateTime.Today;
            return db.UpdateGeneric<tblPartiler>(parti);
        }

        public bool PlanlananOlarakIsaretle(List<vHamKumaslar> Planlananlar)
        {
            List<tblHamKumaslar> hamKumaslar = new List<tblHamKumaslar>();
            foreach (vHamKumaslar item in Planlananlar)
            {
                tblHamKumaslar ham = db.GetGeneric<tblHamKumaslar>(c => c.Id == item.Id).FirstOrDefault();
                ham.PartiIdPlanlanan = this.Parti.Id;
                hamKumaslar.Add(ham);
                if (this.PlanlananBarkodlar == null) this.PlanlananBarkodlar = new List<vHamKumaslar>();
                this.PlanlananBarkodlar.Add(item);
            }

            if (db.UpdateGeneric<tblHamKumaslar>(hamKumaslar) == false) throw new Exception("Planlama yapılamadı!");

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Boyahane
    {
        public vPartiler Parti { get; set; }
        public List<vPartiProcessleri> PartiProcessleri { get; set; }

        private DBEvents db = new DBEvents();

        private vBoyahaneProcess _SecilenProcess = new vBoyahaneProcess();
        public vBoyahaneProcess SecilenProcess
        {
            get { return _SecilenProcess; }
            set { _SecilenProcess = value; }
        }

        #region Statics

        public static bool ProcessSil(vBoyahaneProcess silinecek)
        {
            DBEvents db = new DBEvents();
            if (db.DeleteGeneric<tblBoyahaneProcess>(silinecek.ViewToTbl()))
            {
                if (silinecek.ReProcess.HasValue && silinecek.ReProcess.Value) //silinecek process reprocess ise parti duruma göre tekrar set edilmelidir.
                {
                    List<tblBoyahaneProcess> list = db.GetGeneric<tblBoyahaneProcess>(c => c.ReProcess == true);
                    if (list == null || list.Count == 0)
                    {
                        tblPartiler parti = db.GetGeneric<tblPartiler>(c => c.Id == silinecek.PartiId).FirstOrDefault();
                        parti.ReProcessVarMi = false;
                        db.UpdateGeneric<tblPartiler>(parti);
                    }
                }

                return true;
            }

            return false;
        }

        public static List<vBoyahaneProcess> BoyahaneProcessleriGetir(DateTime tarihIlk, DateTime tarihSon)
        {
            return new DBEvents().GetGeneric<vBoyahaneProcess>(c => tarihIlk <= c.Tarih && c.Tarih <= tarihSon);
        }

        public static vOkutulanProcess OkutulanProcessSiraGetir(int partiId,string processKodu)
        {
            return new DBEvents().GetGeneric<vOkutulanProcess>(c => c.PartiId == partiId && c.Kodu == processKodu).FirstOrDefault();
        }

        //Gökhan 16.05.2014
        public static List<vBoyahaneProcess> BoyahaneProcessleriGetir(string partiNo)
        {
            return new DBEvents().GetGeneric<vBoyahaneProcess>(c => c.PartiNo == partiNo);
        }

        //Gökhan 16.05.2014
        public static List<tblTezgahArizaGrupAct> ArizalariGetir()
        {
            return new DBEvents().GetGeneric<tblTezgahArizaGrupAct>(c => c.GrupId == 19);
        }

        //Gökhan 16.05.2014
        public static List<vPartiProcessleri> SecilenProcessGetir(string pPartiNo, string processKodu, int Sira)
        {
            return new DBEvents().GetGeneric<vPartiProcessleri>(c => c.PartiNo == pPartiNo && c.ProcessKodu == processKodu && c.Sira == Sira);
        }

        //Gökhan 22.09.2014
        public static int SonOkutulanProcessSiraGetir(string pPartiNo)
        {
            return new DBEvents().GetGeneric<vBoyahaneProcess>(c => c.PartiNo == pPartiNo && c.Silindi == false).OrderBy(e => e.Sira).LastOrDefault().Sira;
        }

        //Gökhan 16.05.2014
        public static List<vPartiProcessleri> SecilenProcessGetir(string pPartiNo)
        {
            return new DBEvents().GetGeneric<vPartiProcessleri>(c => c.PartiNo == pPartiNo);
        }

        //Gökhan 16.05.2014
        public static List<vFasonKumasMaliyet> FasonKumasMaliyetleriGetir()
        {
            return new DBEvents().GetGeneric<vFasonKumasMaliyet>(c => c.AktifMi == true && c.FasonMu == true);
        }

        //Gökhan 16.05.2014
        public static List<vBoyahaneProcess> CalisanProcessleriGetir()
        {
            return new DBEvents().GetGeneric<vBoyahaneProcess>(c => c.CikisTarih.HasValue == false && c.Durdu == false);
        }

        public static List<tblPersoneller> PersonelleriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.AktifMi == true && c.BolumId == 13);
        }

        public static List<vMamulKumaslar> BoyahaneSepetiKumaslariGetir()
        {
            //return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.Durum == "BoyaSepeti");
            return new DBEvents().GetGenericWithSQLQuery<vMamulKumaslar>("exec spReProcSepetiGetir", new string[0]);
        }

        public static List<vBoyaProgrami> BoyaProgramiGetir(bool terminlerGuncellensin = false)
        {
            List<vBoyaProgrami> boyaProgrami = new DBEvents().GetGenericWithSQLQuery<vBoyaProgrami>("exec spBoyaProgramiGetir", new string[0]).OrderBy(o => o.PartiNo).ToList();

            if (terminlerGuncellensin)
            {
                List<vBoyaProgrami> guncellenecekler = boyaProgrami.FindAll(c => c.TmpDokumaTerminiGeciktiMi != c.DokumaTerminiGeciktiMi || c.TmpBoyaTerminiGeciktiMi != c.BoyamaTerminiGeciktiMi);
                foreach (vBoyaProgrami item in guncellenecekler)
                {
                    tblSiparisAct siparis = new DBEvents().GetGeneric<tblSiparisAct>(c => c.Id == item.SiparisActId).FirstOrDefault();

                    siparis.DokumaTerminiGeciktiMi = item.TmpDokumaTerminiGeciktiMi;
                    if (item.TmpDokumaTerminiGeciktiMi == false) siparis.DokumaTerminiGecikmeTarihi = null;
                    else siparis.DokumaTerminiGecikmeTarihi = DateTime.Now;

                    siparis.BoyamaTerminiGeciktiMi = item.TmpBoyaTerminiGeciktiMi;
                    if (item.TmpBoyaTerminiGeciktiMi == false) siparis.BoyamaTerminiGecikmeTarihi = null;
                    else siparis.BoyamaTerminiGecikmeTarihi = DateTime.Now;

                    new DBEvents().UpdateGeneric<tblSiparisAct>(siparis);
                }
            }

            return boyaProgrami;
        }

        public static List<vBoyaProgramiSukru> BoyaPrograminaAlinanPartileriGetir()
        {
            return new DBEvents().GetGeneric<vBoyaProgramiSukru>(c => c.BoyaProgramiBoyandiMi == false).OrderByDescending(o => o.TerminYili).ThenByDescending(o => o.TerminHaftasi).ToList();
            //List<vBoyaProgramiSukru> boyaPrograminaAlinanlar = new List<vBoyaProgramiSukru>();
            //return boyaPrograminaAlinanlar;
        }


        #endregion

        public void BoyahaneProcessSec(vPartiProcessleri partiProcess)
        {
            vBoyahaneProcess process = null;
            if (partiProcess != null) process = db.GetGeneric<vBoyahaneProcess>(c => c.PartiId == partiProcess.PartiId && c.ProcessId == partiProcess.ProcessId && c.Sira == partiProcess.Sira).LastOrDefault();
            if (process != null) _SecilenProcess = process;
            else if (partiProcess != null)
            {
                _SecilenProcess = new vBoyahaneProcess()
                {
                    PartiId = partiProcess.PartiId,
                    ProcessId = partiProcess.ProcessId,
                    Sira = partiProcess.Sira
                };
            }
            else if (partiProcess == null) _SecilenProcess = new vBoyahaneProcess();
        }

        public bool ProsesDahaOnceOkutulduMu(vPartiProcessleri process)
        {
            tblBoyahaneProcess prc = db.GetGeneric<tblBoyahaneProcess>(c => c.ProcessId == process.ProcessId && c.PartiId == process.PartiId && c.Sira == process.Sira && c.CikisTarih.HasValue == true && c.Silindi == false).FirstOrDefault();

            if (prc == null) return false;
            else return true;
        }

        public bool BarkodOkut(string barkod)
        {
            this.Parti = db.GetGeneric<vPartiler>(c => c.PartiNo == barkod).FirstOrDefault();
            if (this.Parti == null) throw new Exception("Parti no bulunamadı..!");

            this.PartiProcessleri = db.GetGeneric<vPartiProcessleri>(c => c.PartiId == this.Parti.Id).OrderBy(e => e.Sira).ToList();
            if (this.PartiProcessleri == null || this.PartiProcessleri.Count == 0) throw new Exception("Processler girilmemiş..!");

            if (this.Parti.AcilmisMetre > this.Parti.PartiMetre * 1.10) throw new Exception("Fazla kumaş açılmış..!");
            if (this.Parti.AcilmisMetre < this.Parti.PartiMetre * 0.90) throw new Exception("Eksik kumaş açılmış..!");

            return true;
        }

        private bool ReProcessIsaretle()
        {
            tblBoyahaneProcess reProVarMi = db.GetGeneric<tblBoyahaneProcess>(c => c.ProcessId == _SecilenProcess.ProcessId && c.CikisTarih.HasValue == true).FirstOrDefault();
            if (reProVarMi != null)
            {
                tblPartiler parti = this.Parti.ViewToTbl();
                parti.ReProcessVarMi = true;
                if (db.UpdateGeneric<tblPartiler>(parti))
                {
                    this.Parti.ReProcessVarMi = true;
                    this._SecilenProcess.ReProcess = true;

                    return true;
                }

                return false;
            }
            return true;
        }

        private bool CabukOkutmaIsaretle()
        {
            tblPartiler parti = Parti.ViewToTbl();
            parti.ProcessOkumaHizliMi = true;

            if (db.UpdateGeneric<tblPartiler>(parti))
            {
                Parti.ProcessOkumaHizliMi = true;
                return true;
            }

            return false;
        }

        public bool ProcessKaydet(vPartiProcessleri partiProcess)
        {
            if (PartiProcessleri.Exists(c => c.ProcessId == _SecilenProcess.ProcessId))
            {
                //List<tblBoyahaneProcess> listBoyahane = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == Parti.Id);

                if (SecilenProcess.Metre < Parti.PartiMetre * 0.90) throw new Exception("Az kumaş açılmış..!");

                if (SecilenProcess.Metre > Parti.PartiMetre * 1.10) throw new Exception("Fazla kumaş açılmış..!");

                vBoyahaneProcess sonOkutulan = db.GetGeneric<vBoyahaneProcess>(c => c.PartiId == this.Parti.Id).OrderBy(e => e.Sira).LastOrDefault();

                if (sonOkutulan != null)
                {
                    if (sonOkutulan.FasonMu) throw new Exception("Fason gönderime hazır.\n\nMamul kaliteye geçmelisiniz..!");

                    if (DateTime.Now.TimeOfDay.Subtract(sonOkutulan.Saat.TimeOfDay) < new TimeSpan(0, 15, 0)) //process okutma hızı kontrolü
                        if (CabukOkutmaIsaretle() == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!\nHiZ");

                    tblBoyahaneProcess cikisiOkutulmayan = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Id != _SecilenProcess.Id).FirstOrDefault();
                    if (cikisiOkutulmayan != null) throw new Exception("Önceki process'in çıkış okutması yapılmamış.\n\nEklenemez..!");

                    int ind = PartiProcessleri.FindIndex(c => c.Sira == sonOkutulan.Sira && c.ProcessId == sonOkutulan.ProcessId);
                    if (PartiProcessleri[ind + 1].Sira < partiProcess.Sira) throw new Exception("Daha önceki process okutulmamış.\n\nEklenemez..!");

                    if (ReProcessIsaretle() == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!\nReP");
                }
                else
                {
                    if (this.PartiProcessleri[0].ProcessId != this._SecilenProcess.ProcessId) throw new Exception("Daha önceki process okutulmamış.\n\nEklenemez..!");
                }
            }
            else throw new Exception("Process bulunamadı..!");

            tblBoyahaneProcess tblProcess = _SecilenProcess.ViewToTbl();
            tblProcess.PartiId = Parti.Id;
            if (tblProcess.Id == 0)
            {
                tblProcess.Tarih = DateTime.Today;
                tblProcess.Saat = DateTime.Now;
            }
            else
            {
                if (tblProcess.CikisTarih.HasValue == false)
                {
                    tblProcess.CikisTarih = DateTime.Today;
                    tblProcess.CikisSaat = DateTime.Now;
                }
                else
                {
                    tblProcess.Id = 0;
                    tblProcess.CikisTarih = null;
                    tblProcess.CikisSaat = null;
                    tblProcess.Tarih = DateTime.Today;
                    tblProcess.Saat = DateTime.Now;
                }
            }
            tblProcess.Sira = partiProcess.Sira;

            if (tblProcess.Id == 0) if (db.SaveGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
            if (tblProcess.Id != 0) if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");

            return true;
        }

        //Gökhan 16.05.2014
        public bool ProcessKaydet(vPartiProcessleri partiProcess, string btn, string comment, double metre)
        {
            if (PartiProcessleri.Exists(c => c.ProcessId == _SecilenProcess.ProcessId))
            {
                if (btn == "Bitir")
                {
                    if (SecilenProcess.Metre < Parti.PartiMetre * 0.90) throw new Exception("Az kumaş açılmış..!");

                    if (SecilenProcess.Metre > Parti.PartiMetre * 1.10) throw new Exception("Fazla kumaş açılmış..!");
                }
             
                vBoyahaneProcess sonOkutulan = db.GetGeneric<vBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.Silindi == false).OrderBy(e => e.Sira).LastOrDefault();                

                if (sonOkutulan != null)
                {
                    //25.06.15 Mehmet
                    //Fasona sevk edildi mi kontrolü yapılacak.
                    bool FasonaSevkEdilmisMi = Mamul.FasonaSevkEdilmisMi(Parti.Id);

                    if (sonOkutulan.FasonMu && FasonaSevkEdilmisMi==false) throw new Exception("Fason gönderime hazır.\n\nMamul kaliteye geçmelisiniz..!");

                    if (DateTime.Now.TimeOfDay.Subtract(sonOkutulan.Saat.TimeOfDay) < new TimeSpan(0, 15, 0)) //process okutma hızı kontrolü
                        if (CabukOkutmaIsaretle() == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!\nHiZ");

                    tblBoyahaneProcess cikisiOkutulmayan = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Silindi == false && c.Id != _SecilenProcess.Id).FirstOrDefault();
                    tblBoyahaneProcess durmusVarMi = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Durdu == true).FirstOrDefault();
                    if (cikisiOkutulmayan != null)
                    {
                        if (cikisiOkutulmayan.ProcessId != this._SecilenProcess.ProcessId)
                        {
                            throw new Exception("Devam eden Process var.\n\nEklenemez..!");
                        }
                    }

                    int ind = PartiProcessleri.FindIndex(c => c.Sira == sonOkutulan.Sira && c.ProcessId == sonOkutulan.ProcessId);
                    if (PartiProcessleri.Count() != (ind + 1))
                    {
                        if (PartiProcessleri[ind + 1].Sira < partiProcess.Sira) throw new Exception("Daha önceki process okutulmamış.\n\nEklenemez...!");
                    }

                }
                else
                {
                    if (this.PartiProcessleri[0].ProcessId != this._SecilenProcess.ProcessId) throw new Exception("Daha önceki process okutulmamış.\n\nEklenemez..!");
                }
            }
            else throw new Exception("Process bulunamadı..!");

            tblBoyahaneProcess tblProcess = _SecilenProcess.ViewToTbl();
            tblProcess.PartiId = Parti.Id;
            int tempControl = -1;
            if (tblProcess.Id == 0)
            {
                tblProcess.Tarih = DateTime.Today;
                tblProcess.Saat = DateTime.Now;
            }
            else
            {
                tblBoyahaneProcess iptalEdilecek;
                try
                {
                    iptalEdilecek = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id).OrderBy(e => e.Id).Last();
                    if (iptalEdilecek.CikisTarih.HasValue != false)
                    {
                        iptalEdilecek = null;
                    }
                }
                catch (Exception)
                {
                    iptalEdilecek = null;
                }
                tblBoyahaneProcess cikisiOkutulmayan = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Durdu != true).OrderBy(e => e.Sira).LastOrDefault();
                tblBoyahaneProcess ikinciBaslami = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Durdu != false).OrderBy(e => e.Sira).LastOrDefault();

                if (btn == "Başla")
                {
                    if (cikisiOkutulmayan == null)
                    {
                        tblProcess.Id = 0;
                        tblProcess.CikisTarih = null;
                        tblProcess.CikisSaat = null;
                        tblProcess.Tarih = DateTime.Today;
                        tblProcess.Saat = DateTime.Now;
                        tblProcess.Durdu = false;
                        tblProcess.Silindi = false;
                        tblProcess.Aciklama = null;
                        tblProcess.ArizaId = null;
                        tempControl = 0;
                    }
                    else
                    {
                        if (ikinciBaslami != null)
                        {
                            tblProcess.CikisTarih = DateTime.Today;
                            tblProcess.CikisSaat = DateTime.Now;
                            tblProcess.Durdu = true;
                            tempControl = 3;
                        }
                        else
                        {
                            throw new Exception("Devam eden process var!...");
                        }
                    }
                }
                else if (btn == "Bitir")
                {
                    if (cikisiOkutulmayan != null)
                    {
                        if (ikinciBaslami == null)
                        {
                            tblProcess = cikisiOkutulmayan;
                            tempControl = 2;
                            tblProcess.CikisTarih = DateTime.Today;
                            tblProcess.CikisSaat = DateTime.Now;
                            tblProcess.Durdu = false;
                        }
                        else
                        {
                            throw new Exception("Devam eden process var!...");
                        }
                    }
                    else
                    {
                        throw new Exception("Başlatılmış process yok!...");
                    }
                }
                else if (btn == "Dur")
                {
                    if (cikisiOkutulmayan != null)
                    {
                        if (ikinciBaslami == null)
                        {
                            tblProcess.Id = 0;
                            tblProcess.CikisTarih = null;
                            tblProcess.CikisSaat = null;
                            tblProcess.Tarih = DateTime.Today;
                            tblProcess.Saat = DateTime.Now;
                            tblProcess.Durdu = true;
                            tblProcess.ArizaId = Convert.ToInt32(comment.Substring(0, comment.IndexOf("?")));
                            if ((comment.Length - comment.IndexOf("?") > 1))
                            {
                                tblProcess.Aciklama = comment.Substring(comment.IndexOf("?") + 1, (comment.Length - comment.IndexOf("?") - 1));
                            }
                            else
                            {
                                tblProcess.Aciklama = null;
                            }
                            tblProcess.Silindi = false;
                            tempControl = 1;
                        }
                        else
                        {
                            throw new Exception("Durdurulmuş işlem var!...");
                        }
                    }
                    else
                    {
                        throw new Exception("Başlatılmış process yok!...");
                    }
                }
                else if (btn == "Iptal")
                {
                    if (iptalEdilecek != null)
                    {
                        tblProcess = iptalEdilecek;
                        tempControl = 3;
                        tblProcess.Silindi = true;
                        tblProcess.CikisTarih = DateTime.Today;
                        tblProcess.CikisSaat = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("İptal edilebilecek process yok!...");
                    }
                }
                else if (btn == "Not")
                {
                    tblProcess.Aciklama = comment;
                    tempControl = 4;
                }
            }

            tblProcess.Sira = partiProcess.Sira;
            tblProcess.Metre = metre; //txtmetre alanından geliyor.
            if (btn == "Başla")
            {
                if (tblProcess.Id == 0)
                {
                    tblProcess.Durdu = false;
                    tblProcess.Silindi = false;
                    if (tblProcess.Id == 0)
                    {
                        if (tblProcess.ProcessId == 60)
                        {
                            tblProcess.CikisSaat = DateTime.Now;
                            tblProcess.CikisTarih = DateTime.Now;
                        }
                        if (db.SaveGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                    }
                    if (tblProcess.Id != 0) if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                }
                else
                {
                    if (tempControl == 0)
                    {

                        if (db.SaveGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                    }
                    else
                    {
                        if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                    }
                }
            }
            else if (btn == "Dur")
            {
                if (tempControl == 1)
                {
                    if (db.SaveGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                }
                else
                {
                    throw new Exception("Başlamamış Processi duraklatamazsın..!");
                }
            }
            else if (btn == "Bitir")
            {
                if (tempControl == 2)
                {
                    if (BesDakikaOlduMu(tblProcess.PartiId, tblProcess.ProcessId))
                    {
                        if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                    }
                    else
                    {
                        throw new Exception("5 dakika dolmadan process bitirilemez..!");
                    }
                }
                else
                {
                    throw new Exception("Önce Processi başlatmalısınız..!");
                }
            }
            else if (btn == "Iptal")
            {
                if (tempControl == 3)
                {
                    if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                }
                else
                {
                    throw new Exception("İptal edilecek Process bulunamadı..!");
                }
            }
            else
            {
                if (tempControl == 4)
                {
                    if (db.UpdateGeneric<tblBoyahaneProcess>(tblProcess) == false) throw new Exception("Hata oluştu.\n\nKaydedilemedi..!");
                }
                else
                {
                    throw new Exception("not edilecek Process bulunamadı..!");
                }
            }

            return true;
        }

        //Gökhan 15.05.2014
        public bool PartiVarMi(string partiNo)
        {
            return db.GetGeneric<tblPartiler>(c => c.PartiNo == partiNo).Any();
        }

        //Gökhan 15.05.2014
        public bool ProcessVarMi(string partiNo, string processKodu)
        {
            return db.GetGeneric<vPartiProcessleri>(c => c.PartiNo == partiNo && c.ProcessKodu == processKodu).Any();
        }

        //Gökhan 15.05.2014
        public int PartiIdGetir(string partiNo)
        {
            return db.GetGeneric<tblPartiler>(c => c.PartiNo == partiNo).FirstOrDefault().Id;
        }

        //Gökhan 15.05.2014
        public void Paketleme(string partiNo, int siraParti, int partiId)
        {
            tblBoyahaneProcess instance = new tblBoyahaneProcess();
            instance.ArizaId = null;
            instance.Aciklama = null;
            instance.CikisSaat = DateTime.Now;
            instance.CikisTarih = DateTime.Now;
            instance.Durdu = false;
            instance.MakinaId = 177;
            instance.Metre = 0;
            instance.PartiId = partiId;
            instance.PersonelId = 482;
            instance.ProcessId = 60;
            instance.ReProcess = false;
            instance.Saat = DateTime.Now;
            instance.Silindi = false;
            instance.Sira = siraParti;
            instance.Tarih = DateTime.Now;
            db.SaveGeneric<tblBoyahaneProcess>(instance);
        }

        //Gökhan 16.05.2014
        public bool ButonTextAyarla()
        {
            tblBoyahaneProcess cikisiOkutulmayan = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == this.Parti.Id && c.CikisTarih.HasValue == false && c.Durdu == true).OrderBy(e => e.Sira).LastOrDefault();
            if (cikisiOkutulmayan != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Gökhan 16.05.2014
        public bool FasonKumasKaydet(List<vFasonKumasMaliyet> liste)
        {
            bool result = false;
            
            List<vFasonKumasMaliyet> listeSave = liste.Where(c => c.Id == 0).ToList();
            List<vFasonKumasMaliyet> listeUpdate = liste.Where(c => c.Id != 0).ToList();

            if (listeSave.Count > 0)
            {
                return db.SaveGeneric<tblFasonKumasMaliyet>(vFasonKumasMaliyet.ViewToTbl(listeSave));
            }

            if (listeUpdate.Count > 0)
            {
                return db.UpdateGeneric<tblFasonKumasMaliyet>(vFasonKumasMaliyet.ViewToTbl(listeUpdate));
            }

            return result;
        }

        //Gökhan 16.05.2014
        public string SureyiHesapla(string partiNo)
        {
            vBoyahaneProcess instance = db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == partiNo && c.CikisTarih.HasValue == false).LastOrDefault();
            if (instance != null)
            {
                TimeSpan ts = DateTime.Now - instance.Saat;
                return ts.ToString();
            }
            else
            {
                return null;
            }
        }

        //Gökhan 16.05.2014
        public DateTime TarihiGetir(string partiNo)
        {
            vBoyahaneProcess instance = db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == partiNo && c.CikisSaat.HasValue == false).LastOrDefault();
            if (instance == null)
            {
                return DateTime.Now;
            }
            else
            {
                return instance.Saat;
            }

        }

        //Gökhan 16.05.2014 
        public bool BesDakikaOlduMu(string partiNo, string processKodu)
        {
            vBoyahaneProcess instance = new vBoyahaneProcess();
            instance = db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == partiNo && c.ProcessKodu == processKodu && c.CikisTarih.HasValue == false && c.Durdu != true).LastOrDefault();
            if (instance != null)
            {
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - instance.Saat;
                if (ts.Minutes >= 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Gökhan 16.05.2014 
        public bool BesDakikaOlduMu(int partiId, int processId)
        {
            tblBoyahaneProcess instance = new tblBoyahaneProcess();
            instance = db.GetGeneric<tblBoyahaneProcess>(c => c.PartiId == partiId && c.ProcessId == processId && c.CikisTarih.HasValue == false && c.Durdu != true).LastOrDefault();
            if (instance != null)
            {
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - instance.Saat;
                if (ts.Days >0 || ts.Hours >0 || ts.Minutes >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Gökhan 16.05.2014
        public vBoyahaneProcess verileriGetir(string partiNo, string processKodu)
        {
            return db.GetGeneric<vBoyahaneProcess>(c => c.PartiNo == partiNo && c.ProcessKodu == processKodu && c.CikisSaat.HasValue == false).LastOrDefault();
        }

        //Gökhan 16.05.2014
        public tblMakinalar makinayiGetir(string makinaAdi, int baglantiAdi)
        {
            return db.GetGeneric<tblMakinalar>(c => c.Adi == makinaAdi && c.BaglantiId == baglantiAdi).LastOrDefault();
        }

        public class RenkKartlari
        {
            public RenkKartlari(tblKumasRenk renk)
            {
                Renk = renk;
            }

            private DBEvents db = new DBEvents();

            private tblKumasRenk _Renk;
            public tblKumasRenk Renk
            {
                get
                {
                    return _Renk;
                }
                set
                {
                    _Renk = value;
                    RenkKimyasallari = db.GetGeneric<vKumasRenkAct>(c => c.RenkId == value.Id);
                }
            }
            public List<vKumasRenkAct> RenkKimyasallari;

            public bool ReceteItemKaydet(vKumasRenkAct kimyasal)
            {
                kimyasal.RenkId = _Renk.Id;
                tblKumasRenkAct kimyasalTbl = kimyasal.ViewToTable();
                if (kimyasalTbl.Id == 0 && db.SaveGeneric<tblKumasRenkAct>(ref kimyasalTbl))
                {
                    kimyasal.Id = kimyasalTbl.Id;
                    tblMalzemeler malzeme = tblMalzemeler.MalzemeGetir(kimyasal.KimyasalId);
                    kimyasal.KimyasalAdi = malzeme.Adi;
                    kimyasal.KimyasalKodu = malzeme.Kodu;
                    this.RenkKimyasallari.Add(kimyasal);
                    return true;
                }

                else if (kimyasalTbl.Id != 0) return db.UpdateGeneric<tblKumasRenkAct>(kimyasalTbl);

                return false;
            }

            //Gökhan 16.05.2014
            public bool ReceteItemKaydet(vKumasRenkAct kimyasal, int kullaniciId)
            {
                kimyasal.RenkId = _Renk.Id;
                tblKumasRenkAct kimyasalTbl = kimyasal.ViewToTable();
                kimyasalTbl.PersonelId = kullaniciId;
                if (kimyasalTbl.Id == 0 && db.SaveGeneric<tblKumasRenkAct>(ref kimyasalTbl))
                {
                    kimyasal.Id = kimyasalTbl.Id;
                    tblMalzemeler malzeme = tblMalzemeler.MalzemeGetir(kimyasal.KimyasalId);
                    kimyasal.KimyasalAdi = malzeme.Adi;
                    kimyasal.KimyasalKodu = malzeme.Kodu;
                    this.RenkKimyasallari.Add(kimyasal);
                    return true;
                }

                else if (kimyasalTbl.Id != 0) return db.UpdateGeneric<tblKumasRenkAct>(kimyasalTbl);

                return false;
            }

            //Gökhan 16.05.2014
            public bool ReceteItemSil(vKumasRenkAct kimyasal, int kullaniciAdi)
            {
                tblKumasRenkAct kimyasalTbl = kimyasal.ViewToTable();
                int silinenId = kimyasalTbl.Id;
                if (db.DeleteGeneric<tblKumasRenkAct>(kimyasalTbl))
                {
                    this.RenkKimyasallari.Remove(kimyasal);
                    SilenPersonelKaydet(silinenId, kullaniciAdi);
                    return true;
                }

                return false;
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

            //Gökhan 16.05.2014
            public bool RenkKartiAktifMi(int renkId)
            {
                return db.GetGeneric<tblKumasRenk>(c => c.Id == renkId).Any();
            }

            //Gökhan 16.05.2014
            public bool Logla(int renkId, int personelId)
            {
                List<tblKumasRenkAct> tempL = new List<tblKumasRenkAct>();
                tempL = db.GetGeneric<tblKumasRenkAct>(c => c.RenkId == renkId);
                List<tblKimyasalReceteActLog> temp = new List<tblKimyasalReceteActLog>();
                tblKimyasalReceteActLog tempT;

                foreach (tblKumasRenkAct item in tempL)
                {
                    tempT = new tblKimyasalReceteActLog();
                    tempT.SilinenId = item.Id;
                    tempT.ReceteId = null;
                    tempT.OranE = null;
                    tempT.OranY = null;
                    tempT.GrLtOranE = null;
                    tempT.GrLtOranY = item.GrOran;
                    tempT.MiktarE = null;
                    tempT.MiktarY = item.Miktar;
                    tempT.TipE = null;
                    tempT.TipY = null;
                    tempT.FloteE = null;
                    tempT.FloteY = null;
                    tempT.Turu = 5;
                    tempT.Tarih = DateTime.Now;
                    tempT.Saat = DateTime.Now;
                    tempT.PersonelIdE = null;
                    tempT.PersonelIdY = personelId;
                    tempT.KimyasalIdE = null;
                    tempT.KimyasalIdY = item.KimyasalId;
                    tempT.OnayBir = item.OnayBir;
                    tempT.OnayIki = item.OnayIki;
                    tempT.OnayBirTarih = item.OnayBirTarih;
                    tempT.OnayIkiTarih = item.OnayIkiTarih;
                    tempT.RAciklamaE = null;
                    tempT.RAciklamaY = item.Aciklama;
                    tempT.RBoyaKimyaE = null;
                    tempT.RBoyaKimyaY = item.BoyaKimya;
                    tempT.logTuru = 1;
                    tempT.RenkId = item.RenkId;

                    temp.Add(tempT);
                }
                return db.SaveGeneric<tblKimyasalReceteActLog>(temp);

            }

            public bool ReceteItemSil(vKumasRenkAct kimyasal)
            {
                tblKumasRenkAct kimyasalTbl = kimyasal.ViewToTable();
                if (db.DeleteGeneric<tblKumasRenkAct>(kimyasalTbl))
                {
                    this.RenkKimyasallari.Remove(kimyasal);
                    return true;
                }

                return false;
            }

            public bool RenkKartiSil()
            {
                if (db.DeleteGeneric<tblKumasRenkAct>(vKumasRenkAct.ViewToTbl(this.RenkKimyasallari)))
                {
                    this.RenkKimyasallari.Clear();
                    if (db.DeleteGeneric<tblKumasRenk>(this._Renk))
                    {
                        this._Renk = null;
                        return true;
                    }
                }

                return false;
            }

            public bool RenkKaydet()
            {
                if (this._Renk.Id == 0)
                {
                    return db.SaveGeneric<tblKumasRenk>(ref this._Renk);
                }
                else return db.UpdateGeneric<tblKumasRenk>(this._Renk);
            }

            

        }
    }
}




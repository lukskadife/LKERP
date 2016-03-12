using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.Interfaces;
using LKLibrary.DbClasses;
using System.IO;

namespace LKLibrary.Classes
{
    public class MalzemeTalep : AbsMalzemeTalep
    {
        private DBEvents db = new DBEvents();

        public enum Belge { Irsaliye, Fatura }

        public List<vModulDurumlari> ListTalepDurumlari; 
        public int TalepTamamId;
        public int TalepIptalId;

        public MalzemeTalep() 
        {
            ListTalepDurumlari = db.GetGeneric<vModulDurumlari>(c => c.AyarUstAdi == "Talep").OrderBy(s => s.Sira).ToList();

            try { TalepTamamId = Convert.ToInt32(db.GetGeneric<tblAyarlar>(c => c.Adi == "TalepTamamId").FirstOrDefault().Deger); } catch {}
            try { TalepIptalId = Convert.ToInt32(db.GetGeneric<tblAyarlar>(c => c.Adi == "TalepIptalId").FirstOrDefault().Deger); } catch {}
        }

        public override bool TalepKaydet(List<tblTalepler> lstTalep)
        {
            try
            {
                int talepIlkDurumId = this.ListTalepDurumlari[0].DurumId;
                lstTalep.ForEach(c => c.DurumId = talepIlkDurumId);
            }
            catch (Exception)
            {
                return false;
            }
            return db.SaveGeneric<tblTalepler>(lstTalep);
        }

        public bool TalepGuncelle(List<tblTalepler> lstTalep)
        {
            return db.UpdateGeneric<tblTalepler>(lstTalep);
        }

        /// <summary>
        /// Durumuna göre talepleri getirir.
        /// </summary>
        /// <param name="durumId">tblDurumlar id'si</param>
        /// <returns></returns>
        public List<vTalepMalzemeler> TalepAra(int durumId)
        {
            return db.GetGeneric<vTalepMalzemeler>(c => c.DurumId == durumId);
        }

        public List<vTalepMalzemeler> TalepAra(int durumId, int talepEdenId)
        {
            return db.GetGeneric<vTalepMalzemeler>(c => c.DurumId == durumId && c.TalepEdenId == talepEdenId);
        }

        /// <summary>
        /// İlgili durumda içinde geçen harflere göre talepleri arar
        /// </summary>
        /// <param name="durumId">ilgili durumun idsi</param>
        /// <param name="malzemeFiltre">malzemenin içinde geçen harfler</param>
        /// <returns></returns>
        public List<vTalepMalzemeler> TalepAra(int durumId, string malzemeFiltre)
        {
            return db.GetGeneric<vTalepMalzemeler>(c => c.DurumId == durumId && c.MalzemeAdi.Contains(malzemeFiltre));
        }

        public List<vTalepMalzemeler> TalepAraWithTalepId(int talepId)
        {
            return db.GetGeneric<vTalepMalzemeler>(c => c.TalepId == talepId);
        }

        public bool TalepSil(int talepId)
        {
            tblTalepler talep = db.GetGeneric<tblTalepler>(c=>c.Id == talepId).FirstOrDefault();
            if (talep == null) return false;
            return db.DeleteGeneric<tblTalepler>(talep);
        }

        /// <summary>
        /// İlgili malzemenin firmalardaki ortalama fiyatlarını getirir.
        /// </summary>
        /// <param name="malzemeId">Ortalama fiyatları istenen malzemenin malzemeId 'si</param>
        /// <returns></returns>
        public List<vMalzemeOrtFiyatlar> MalzemeFirmaFiyatlariGetir(int malzemeId)
        {
            return db.GetGeneric<vMalzemeOrtFiyatlar>(c => c.MalzemeId == malzemeId);
        }

        /// <summary>
        /// İlgili malzemenin firmalardaki ortalama fiyatlarını sayfalayarak getirir.
        /// </summary>
        /// <param name="malzemeId">Ortalama fiyatları istenen malzemenin malzemeId 'si</param>
        /// <param name="sonFirmaId">Daha önce çekilen son firmanın id'si</param>
        /// <returns></returns>
        public List<vMalzemeOrtFiyatlar> MalzemeFirmaFiyatlariGetir(int malzemeId, int sonFirmaId)
        {
            return db.GetGenericWithSQLQuery<vMalzemeOrtFiyatlar>("exec dbo.spMalzemeFirmaPaging " + sonFirmaId.ToString() + ", " + malzemeId.ToString(), new string[0]).ToList();
        }

        public List<vTalepKarsilama> KarsilamaFormlariGetir(int formId)
        {
            return db.GetGeneric<vTalepKarsilama>(c => c.Id == formId);
        }

        public List<vTalepKarsilama> KarsilamaFormlariGetir()
        {
            return db.GetGeneric<vTalepKarsilama>();
        }

        public List<vTalepKarsilama> KarsilamaFormlariGetirWithDurum(int durumId)
        {
            return db.GetGeneric<vTalepKarsilama>(c=>c.DurumId == durumId);
        }

        public List<vTalepKarsilamaAct> KarsilananMalzemeleriGetir(int talepKarsilamaId)
        {
            return db.GetGeneric<vTalepKarsilamaAct>(c => c.TalepKarsilamaId == talepKarsilamaId);
        }

        public bool TalepleriGuncelle(List<vTalepMalzemeler> listTalepler)
        {
            try
            {
                bool sonuc = true;
                foreach (vTalepMalzemeler item in listTalepler)
                {
                    tblTalepler talep = db.GetGeneric<tblTalepler>(c=>c.Id == item.TalepId).FirstOrDefault();
                    
                    talep.BirimId = item.BirimId;
                    talep.Detay = item.Detay;
                    talep.DurumId = item.DurumId;
                    talep.MalzemeId = item.MalzemeId;
                    talep.Miktar = item.Miktar;
                    talep.TalepEdenId = item.TalepEdenId;
                    talep.Tarih = item.Tarih;

                    sonuc = db.UpdateGeneric<tblTalepler>(talep);
                }

                return sonuc;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "TalepleriGuncelle", 0);
                return false;
            }

            
        }

        public int YeniDurumGetir(int mevcutDurum)
        {
            int sonrakiDurumIndex = this.ListTalepDurumlari.FindIndex(c => c.DurumId == mevcutDurum);
            if (sonrakiDurumIndex + 1 < this.ListTalepDurumlari.Count) sonrakiDurumIndex++;
            int yeniDurumId = this.ListTalepDurumlari[sonrakiDurumIndex].DurumId;

            return yeniDurumId;
        }

        /// <summary>
        /// Verilen taleblerin durumunu günceller.
        /// </summary>
        /// <param name="listvTalepler">Durumları güncellenecek olan taleplerin listesi</param>
        /// <param name="mevcutDurum">İlgili taleplerin mevcut durumu</param>
        /// <returns>Durum güncelleme başarılı ise yeni durumun id'sini döner. Aksi durumda dönüş değeri -1'dir</returns>
        public int TalepDurumlariGuncelle(List<vTalepMalzemeler> listvTalepler, int mevcutDurum)
        {
            int yeniDurumId = YeniDurumGetir(mevcutDurum);
            listvTalepler.ForEach(c => c.DurumId = yeniDurumId);

            if (TalepleriGuncelle(listvTalepler.FindAll(c => c.HepsiKarsilandiMi))) return yeniDurumId;
            else return -1;
        }

        public bool TalepDurumlariGuncelle(List<vTalepKarsilamaAct> listvTalepler, int yeniDurumId)
        {
            bool sonuc = true;
            try
            {
                foreach (vTalepKarsilamaAct item in listvTalepler)
                {
                    tblTalepler talep = db.GetGeneric<tblTalepler>(c => c.Id == item.TalepId).FirstOrDefault();
                    talep.DurumId = yeniDurumId;
                    if (!db.UpdateGeneric<tblTalepler>(talep)) sonuc = false;
                }
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "TalepDurumlariGuncelle", 0);
                sonuc = false;
            }
            

            return sonuc;
        }

        public int TalepDurumlariDegistir(List<vTalepMalzemeler> listvTalepler, int yeniDurumId)
        {
            listvTalepler.ForEach(c => c.DurumId = yeniDurumId);

            if (TalepleriGuncelle(listvTalepler)) return yeniDurumId;
            else return -1;
        }

        public bool TalepFormDurumuGuncelle(vTalepKarsilama vForm, int guncellenecekDurumId)
        {
            try
            {
                tblTalepKarsilama tblForm = db.GetGeneric<tblTalepKarsilama>(c => c.Id == vForm.Id).FirstOrDefault();
                tblForm.DurumId = guncellenecekDurumId;

                return db.UpdateGeneric<tblTalepKarsilama>(tblForm);
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "TalepFormDurumuGuncelle", 0);
                return false;
            }
        }

        /// <summary>
        /// Karşılanan talepleri kaydeder.
        /// </summary>
        /// <param name="ustForm">Karşılanacak taleplerin formu</param>
        /// <param name="listKarsilananlar">Karşılanacak taleplerin listesi</param>
        /// <returns></returns>
        public bool KarsilamaKaydet(tblTalepKarsilama ustForm = null, List<tblTalepKarsilamaAct> listKarsilananlar = null)
        {
            if (ustForm == null && listKarsilananlar == null) return false;
            try
            {
                if (ustForm != null)
                {
                    if (db.SaveGeneric<tblTalepKarsilama>(ref ustForm) == false) return false;
                    for (int i = 0; i < listKarsilananlar.Count; i++)
                    {
                        listKarsilananlar[i].TalepKarsilamaId = ustForm.Id;
                    }
                }

                if (listKarsilananlar.Exists(c => c.TalepKarsilamaId == 0)) return false;

                if (listKarsilananlar != null) if (db.SaveGeneric<tblTalepKarsilamaAct>(listKarsilananlar) == false) return false;
                return true;
            }
            catch (Exception e)
            {
                string str = e.Message;
                return false;
            }
        }

        public bool KarsilamaMalzemeSil(int karsActId)
        {
            tblTalepKarsilamaAct tbl = db.GetGeneric<tblTalepKarsilamaAct>(c => c.Id == karsActId).FirstOrDefault();

            return db.DeleteGeneric<tblTalepKarsilamaAct>(tbl);
        }

        public bool KarsilamaGuncelle(vTalepKarsilama form)
        {
            return db.UpdateGeneric<tblTalepKarsilama>(form.ViewToTable());
        }

        /// <summary>
        /// Satın alma belgeleri için dosya ekler
        /// </summary>
        /// <param name="dosyaTamYolu">dosyanın local'deki tam yolu</param>
        /// <param name="satinAlmaId">dosyanın ekleneceği satınalma id'si</param>
        /// <returns></returns>
        public bool KarsilamaBelgesiEkle(string dosyaTamYolu, int satinAlmaId, Belge belgeTuru)
        {
            try
            {
                if (!File.Exists(dosyaTamYolu)) return false;

                string dosyaAdi = dosyaTamYolu.Substring(dosyaTamYolu.LastIndexOf('\\') + 1);
                DateTime zaman = DateTime.Now;
                dosyaAdi = dosyaAdi.Insert(dosyaAdi.LastIndexOf('.'), zaman.Year.ToString() + zaman.Month.ToString() + zaman.Day.ToString() + 
                    zaman.Hour.ToString() + zaman.Minute.ToString() + zaman.Second.ToString() + zaman.Millisecond.ToString());
                string dosyaServerTamAdi = "D:\\ISD\\Belgeler\\" + dosyaAdi;

                byte[] dosyaByte = ExtensionMethods.FileToByteArray(dosyaTamYolu);

                DosyaServisi.FileOperationServicesClient client = new DosyaServisi.FileOperationServicesClient();
                string srvSonuc = client.SaveFile(new DosyaServisi.SenfoniFiles()
                {
                    FileByteArray = dosyaByte,
                    FileName = dosyaServerTamAdi
                });

                tblTalepKarsilamaBelgeleri belge = new tblTalepKarsilamaBelgeleri()
                {
                    DosyaAdi = dosyaAdi,
                    DosyaTamAdi = dosyaServerTamAdi,
                    KarsilamaId = satinAlmaId,
                    Turu = belgeTuru.ToString()
                };

                if (!db.SaveGeneric<tblTalepKarsilamaBelgeleri>(ref belge)) return false;
                return true;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "SatinAlmaBelgeEkle", 0);
                return false;
            }
        }

        public bool KarsilamaBelgesiGetir(int belgeId, string dosyaKayitDizini = "")
        {
            try
            {
                dosyaKayitDizini = dosyaKayitDizini == "" ? System.Environment.GetEnvironmentVariable("TEMP") : dosyaKayitDizini;
                if (!Directory.Exists(dosyaKayitDizini)) return false;

                tblTalepKarsilamaBelgeleri belge = db.GetGeneric<tblTalepKarsilamaBelgeleri>(c => c.Id == belgeId).FirstOrDefault();
                if (belge == null) return false;

                DosyaServisi.FileOperationServicesClient client = new DosyaServisi.FileOperationServicesClient();
                DosyaServisi.SenfoniFiles file = client.GetFile(belge.DosyaTamAdi);

                ExtensionMethods.ByteArrayToFile(dosyaKayitDizini + '\\' + file.FileName, file.FileByteArray);
                return  true;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "KarsilamaBelgesiGetir", 0);
                return false;
            }
        }

        public bool TaratilanBelgeKontrolu(int satinAlmaId)
        {
            if (db.GetGeneric<tblTalepKarsilamaBelgeleri>(c => c.KarsilamaId == satinAlmaId && c.Turu == Belge.Irsaliye.ToString()).Count == 0)
                return false;
            else return true;
        }
    }
}

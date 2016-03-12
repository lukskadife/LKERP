using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;
using System.IO;

namespace LKLibrary.Classes
{
    public class SatinAlma
    {
        #region statics

        public static List<vTalepKarsilama> BelgeleriGetir(int grupId, string durum)
        {
            if (grupId == 0) return new DBEvents().GetGeneric<vTalepKarsilama>(c => c.Durum == durum);

            return new DBEvents().GetGeneric<vTalepKarsilama>(c=>c.TalepGrupId == grupId && c.Durum == durum);
        }

        public static bool BelgeSil(vTalepKarsilama talep)
        {
            return new DBEvents().DeleteGeneric<tblTalepKarsilama>(talep.ViewToTable());
        }

        public static List<LogoOdeme> OdemeSekilleriGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<LogoOdeme>("exec spLogoOdemeSekilleri", new object[0]);
        }

        #endregion

        public class LogoOdeme
        {
            public int LogoId { get; set; }
            public string OdemeSekli { get; set; }
        }

        public enum BelgeEnumu { Irsaliye, Fatura }

        private vTalepKarsilama _Belge;
        public vTalepKarsilama Belge
        {
            get { return _Belge; }
            set { _Belge = value; }
        }

        private List<vTalepKarsilamaAct> _Talepler = new List<vTalepKarsilamaAct>();
        public List<vTalepKarsilamaAct> Talepler
        {
            get { return _Talepler; }
            set {
                _Talepler = value; 
            }
        }

        private int _IslemYapanPersonelId;
        private DBEvents db = new DBEvents();

        private List<tblMalzemeler> _GrupMalzemeleri;
        private List<vPersonelBolumleri> _PersonelBolumleri;
        private List<tblRenkler> _IplikRenkleri;
        private List<tblAyarlar> _Dovizler;

        public SatinAlma(int grup, int talepEdenPersonelId, vTalepKarsilama satinAlmaBelgesi = null)
        {
            _PersonelBolumleri = new vPersonelBolumleri().PersonelBolumleriGetir();
            if (grup == 39) _IplikRenkleri = new DBEvents().GetGeneric<tblRenkler>(c => c.AktifMi == true);
            _Dovizler = db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 31);

            if (satinAlmaBelgesi != null)
            {
                _GrupMalzemeleri = new vMalzemeler().GruptakiMalzemeleriGetir(satinAlmaBelgesi.TalepGrupId.Value);
                _Belge = satinAlmaBelgesi;
                _Talepler = db.GetGeneric<vTalepKarsilamaAct>(c => c.TalepKarsilamaId == satinAlmaBelgesi.Id);
                foreach (var item in _Talepler)
                {
                    item.Malzemeler = _GrupMalzemeleri;
                    item.Bolumler = _PersonelBolumleri;
                    item.MalzemeBirimleri = db.GetGeneric<vMalzemeBirimleri>(c => c.MalzemeId == item.MalzemeId);
                    item.IplikRenkleri = _IplikRenkleri;
                    item.MevcutStok = new Stok().StokMiktariGetir(item.MalzemeId);
                    item.GelecekStok = new Stok().StokGelecekMiktariGetir(item.MalzemeId);
                    item.Dovizler = _Dovizler;
                    if (_Belge.Durum == "Onay") item.OnayMiktar = item.Miktar;
                }
            }
            else
            {
                _Belge = new vTalepKarsilama() { Durum = "Amir Onayı", PersonelId = talepEdenPersonelId, TalepGrupId = grup, Tarih = DateTime.Now, DurumId = 0 };
                _GrupMalzemeleri = new vMalzemeler().GruptakiMalzemeleriGetir(grup);
            }

            _IslemYapanPersonelId = talepEdenPersonelId;
        }

        public bool Kaydet()
        {
            if (_Belge.Durum != "Amir Onayı" && _Belge.Durum != "Satın Al" && _Belge.TedarikciId.HasValue == false) throw new Exception("Tedarikçi boş geçilemez..!");

            tblTalepKarsilama belge = _Belge.ViewToTable();

            if (_Belge.Id == 0)
            {
                tblBelgeNo belgeNo = tblBelgeNo.BelgeNoGetir("SA");
                string strNumara = (belgeNo.SonBelgeNo + 1).ToString("0000");
                string belgeNoStr = belgeNo.Tipi + " " + DateTime.Now.ToString("yy") + "/" + strNumara;

                belge.No = belgeNoStr;
                _Belge.No = belgeNoStr;

                if (db.SaveGeneric<tblTalepKarsilama>(ref belge))
                {
                    _Belge.Id = belge.Id;
                    belgeNo.SonBelgeNo++;
                    tblBelgeNo.BelgeNoKaydet(belgeNo);
                }
                else return false;
            }
            else if (db.UpdateGeneric<tblTalepKarsilama>(belge) == false) return false;

            vTalepKarsilamaAct depokontrol = _Talepler.Find(c => c.DepoGirisMiktar > c.OnayMiktar);
            if (depokontrol != null) throw new Exception("Depo girişi onaylanan miktardan fazla olamaz.\n\nMalzeme : " + depokontrol.MalzemeAdi +
                "\nOnay Miktarı : " + depokontrol.OnayMiktar.ToString() + "\nDepo Girişi : " + depokontrol.DepoGirisMiktar.ToString());

            List<tblTalepKarsilamaAct> saveList = vTalepKarsilamaAct.ViewToTbl(this._Talepler.FindAll(f => f.Id == 0));
            saveList.ForEach(c => c.TalepKarsilamaId = _Belge.Id);
            List<tblTalepKarsilamaAct> updList = vTalepKarsilamaAct.ViewToTbl(this._Talepler.FindAll(f => f.Id != 0));

            if (db.SaveGeneric<tblTalepKarsilamaAct>(saveList) == false) throw new Exception("Hata oluştu.\n\nKaydetme başarısız..!");
            if (db.UpdateGeneric<tblTalepKarsilamaAct>(updList) == false) throw new Exception("Hata oluştu.\n\nKaydetme başarısız..!");

            this._Talepler = db.GetGeneric<vTalepKarsilamaAct>(c => c.TalepKarsilamaId == _Belge.Id);

            if (_Belge.Durum == "Depo" || _Belge.Durum == "Tamamlandı") if (StokGirisleriYap() == false) throw new Exception("Stok girişleri yapılamadı..!");

            return true;
        }

        private bool StokGirisleriYap()
        {
            List<tblMalzemeGiris> list = new List<tblMalzemeGiris>();

            string ids = "";
            foreach (var item in _Talepler) ids += item.Id.ToString() + ",";
            ids = ids.TrimEnd(',');
            string snc = db.GetGenericWithSQLQuery<string>("delete from tblMalzemeGiris where KarsilamaActId in (" + ids + ")", new object[0]).FirstOrDefault();

            if (db.GetGeneric<tblTalepKarsilamaBelgeleri>(c => c.KarsilamaId == _Belge.Id && c.Turu == BelgeEnumu.Irsaliye.ToString()).Count == 0)
                throw new Exception("İrsaliye eklenmemiş.\n\nStok girişleri yapılamaz..!");

            foreach (vTalepKarsilamaAct giris in _Talepler.FindAll(c => c.DepoGirisMiktar.HasValue == true && c.DepoGirisMiktar.Value != 0))
            {
                tblMalzemeGiris malzemeGiris;

                malzemeGiris = new tblMalzemeGiris()
                {
                    BirimId = giris.BirimId,
                    MalzemeId = giris.MalzemeId,
                    Miktar = giris.DepoGirisMiktar,
                    PersonelId = _IslemYapanPersonelId,
                    SaticiId = _Belge.TedarikciId.Value,
                    KarsilamaActId = giris.Id,
                    Tarih = DateTime.Now,
                    GirisTanim = "SatinAlma"
                };

                if (giris.MalzemeBagId == 39)
                {
                    malzemeGiris.Ambalaj = giris.Ambalaj;
                    malzemeGiris.LotNo = giris.LotNo;
                    malzemeGiris.BobinSayisi = giris.BobinSayisi;
                    malzemeGiris.BrutKg = giris.DepoGirisMiktar;
                    malzemeGiris.NetKg = giris.DepoGirisMiktar;
                    malzemeGiris.RenkId = giris.RenkId;
                    malzemeGiris.Tarih = DateTime.Now;
                }
                list.Add(malzemeGiris);
            }

            return db.SaveGeneric<tblMalzemeGiris>(list);
        }

        public void SatirEkle()
        {
            if (this._Talepler.Exists(c => c.MalzemeId == 0)) return;
            if (this._Belge.Durum != "Amir Onayı") return;

            vTalepKarsilamaAct satir = new vTalepKarsilamaAct() { Tarih = DateTime.Now };
            satir.Malzemeler = _GrupMalzemeleri;
            satir.TalepKarsilamaId = this._Belge.Id;
            satir.Bolumler = _PersonelBolumleri;
            satir.IplikRenkleri = _IplikRenkleri;
            satir.Dovizler = _Dovizler;
            this._Talepler.Add(satir);
        }

        public bool SatirSil(vTalepKarsilamaAct satir)
        {
            if (satir.Id == 0)
            {
                _Talepler.Remove(satir);
                return true;
            }

            if (db.DeleteGeneric<tblTalepKarsilamaAct>(satir.ViewToTbl()))
            {
                _Talepler.Remove(satir);
                return true;
            }
            return false;
        }

        public void SatirGuncelle(vTalepKarsilamaAct satir)
        {
            satir.MalzemeBirimleri = db.GetGeneric<vMalzemeBirimleri>(c => c.MalzemeId == satir.MalzemeId);
            tblMalzemeler malzeme = satir.Malzemeler.Find(c => c.Id == satir.MalzemeId);
            satir.MalzemeKodu = malzeme.Kodu;
            satir.MalzemeAdi = malzeme.Adi;
            satir.MevcutStok = new Stok().StokMiktariGetir(malzeme.Id);
            satir.GelecekStok = new Stok().StokGelecekMiktariGetir(malzeme.Id);
        }

        public List<tblTalepKarsilamaBelgeleri> DijitalBelgeleriGetir()
        {
            return db.GetGeneric<tblTalepKarsilamaBelgeleri>(c => c.KarsilamaId == this._Belge.Id);
        }

        /// <summary>
        /// Satın alma belgeleri için dosya ekler
        /// </summary>
        /// <param name="dosyaTamYolu">dosyanın local'deki tam yolu</param>
        /// <param name="belgeTuru">irsaliye, fatura, vb.</param>
        /// <returns></returns>
        public bool DijitalBelgeEkle(string dosyaTamYolu, BelgeEnumu belgeTuru)
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
                    KarsilamaId = this._Belge.Id,
                    Turu = belgeTuru.ToString(),
                    Tarih = DateTime.Now
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

        public void DijitalBelgeAc(tblTalepKarsilamaBelgeleri dijitalBelge)
        {
            DosyaServisi.SenfoniFiles file;
            try
            {
                LKLibrary.DosyaServisi.FileOperationServicesClient servis = new LKLibrary.DosyaServisi.FileOperationServicesClient();
                file = servis.GetFile(dijitalBelge.DosyaTamAdi);
            }
            catch { return; }

            if (file != null)
            {
                if (File.Exists(dijitalBelge.DosyaAdi)) File.Delete(dijitalBelge.DosyaAdi);
                string path = System.Environment.GetEnvironmentVariable("TEMP") + @"\" + @"LKERP";
                if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                string tempDosyaYolu = path + "\\" + dijitalBelge.DosyaAdi;
                ExtensionMethods.ByteArrayToFile(tempDosyaYolu, file.FileByteArray);
                System.Diagnostics.Process.Start(tempDosyaYolu);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class HamKumas
    {
        public List<tblHamHatalari> Hatalar { get; set; }

        private tblHamKumaslar _KumasAlt;
        public tblHamKumaslar KumasAlt
        {
            get
            {
                return _KumasAlt;
            }
            set
            {
                _KumasAlt = value;
            }
        }

        private tblHamKumaslar _KumasUst;
        public tblHamKumaslar KumasUst
        {
            get
            {
                return _KumasUst;
            }
            set
            {
                _KumasUst = value;
            }
        }

        private int? _TipId = null;
        public int? TipId
        {
            get
            {
                return _TipId;
            }
            set
            {
                _TipId = value.Value;
                _KumasAlt.TipId = value.Value;
                _KumasUst.TipId = value.Value;
                if (_TezgahId != null && value != null) KumasSetEt();
            }
        }

        private int? _TezgahId = null;
        public int? TezgahId
        {
            get
            {
                return _TezgahId;
            }
            set
            {
                _TezgahId = value.Value;
                _KumasAlt.TezgahId = value.Value;
                _KumasUst.TezgahId = value.Value;
                if (_TipId != null && value != null) KumasSetEt();
            }
        }

        private int? _DokumaciId;
        public int? DokumaciId
        {
            get
            {
                return _DokumaciId;
            }
            set
            {
                _DokumaciId = value;
                _KumasAlt.DokumaciId = value;
                _KumasUst.DokumaciId = value;
            }
        }

        private int? _KaliteciId;
        public int? KaliteciId
        {
            get
            {
                return _KaliteciId;
            }
            set
            {
                _KaliteciId = value;
                _KumasAlt.KaliteciId = value;
                _KumasUst.KaliteciId = value;
            }
        }

        private DateTime _Tarih;
        public DateTime Tarih
        {
            get
            {
                return _Tarih;
            }
            set
            {
                _Tarih = value;
                _KumasAlt.Tarih = value;
                _KumasUst.Tarih = value;
            }
        }

        private string _Aciklama;
        public string Aciklama
        {
            get
            {
                return _Aciklama;
            }
            set
            {
                _Aciklama = value;
                _KumasAlt.Aciklama = value;
                _KumasUst.Aciklama = value;
            }
        }

        private string _Varyant;
        public string Varyant
        {
            get
            {
                return _Varyant;
            }
            set
            {
                _Varyant = value;
                _KumasAlt.Varyant = value;
                _KumasUst.Varyant = value;
            }
        }

        public vLeventHareket AltZeminLevent;
        public vLeventHareket UstZeminLevent;
        public vLeventHareket HavLevent;

        private void KumasSetEt()
        {
            vPlanlama plan = _Db.GetGeneric<vPlanlama>(c => c.TezgahId == _TezgahId && c.TipId == _TipId && c.Tarih.Value == DateTime.Today).FirstOrDefault();

            if (plan == null)
            {
                this._KumasAlt.SiparisId = null;
                this._KumasUst.SiparisId = null;
            }
            else
            {
                _KumasUst.SiparisId = plan.SiparisId;
                _KumasAlt.SiparisId = plan.SiparisId;
            }

            List<vLeventHareket> leventler = _Db.GetGeneric<vLeventHareket>(c => c.TezgahId == this._TezgahId && c.TipId == this._TipId && c.Durum == 21);

            vLeventHareket altZeminLevent = leventler.Find(c => c.Cozgu == "Alt Zemin");
            vLeventHareket ustZeminLevent = leventler.Find(c => c.Cozgu == "Üst Zemin");
            vLeventHareket havLevent = leventler.Find(c => c.Cozgu == "Hav");

            if (altZeminLevent != null)
            {
                _KumasAlt.ZeminAltLeventId = altZeminLevent.Id;
                _KumasUst.ZeminAltLeventId = altZeminLevent.Id;
                this.AltZeminLevent = altZeminLevent;
            }
            else
            {
                _KumasAlt.ZeminAltLeventId = null;
                _KumasUst.ZeminAltLeventId = null;
                this.AltZeminLevent = null;
            }

            if (ustZeminLevent != null)
            {
                _KumasAlt.ZeminUstLeventId = ustZeminLevent.Id;
                _KumasUst.ZeminUstLeventId = ustZeminLevent.Id;
                this.UstZeminLevent = ustZeminLevent;
            }
            else
            {
                _KumasAlt.ZeminUstLeventId = null;
                _KumasUst.ZeminUstLeventId = null;
                this.UstZeminLevent = null;
            }

            if (havLevent != null)
            {
                _KumasAlt.HavLeventId = havLevent.Id;
                _KumasUst.HavLeventId = havLevent.Id;
                this.HavLevent = havLevent;
            }
            else
            {
                _KumasAlt.HavLeventId = null;
                _KumasUst.HavLeventId = null;
                this.HavLevent = null;
            }
        }

        #region Statics

        public static List<vHamHataHaritasi> HataHaritasiGetir(vHamKumaslar ham)
        {
            if (ham.Tur == "Ust") return new DBEvents().GetGeneric<vHamHataHaritasi>(c => c.UstId == ham.Id);
            else if (ham.Tur == "Alt") return new DBEvents().GetGeneric<vHamHataHaritasi>(c => c.AltId == ham.Id);
            else return new DBEvents().GetGeneric<vHamHataHaritasi>(c => c.AltId == ham.Id);
            
        }

        public static bool HamKumasDuzelt(tblHamKumaslar kumas)
        {
            List<tblHamHatalari> hatalar = new List<tblHamHatalari>();

            if (kumas.Tur == "Ust") hatalar = new DBEvents().GetGeneric<tblHamHatalari>(c => c.UstId == kumas.Id);
            else if (kumas.Tur == "Alt") hatalar = new DBEvents().GetGeneric<tblHamHatalari>(c => c.AltId == kumas.Id);
            else hatalar = new DBEvents().GetGeneric<tblHamHatalari>(c => c.AltId == kumas.Id);

            double? hataMt = 0;
            if (hatalar != null) hataMt = Math.Round(hatalar.Sum(s => s.Uzunluk).Value, 2);
            kumas.NetMetre = kumas.Metre; //- (hataMt.HasValue ? hataMt.Value : 0); Feramin bey hamda brüt net farkı istemiyor. 24.06.2015 mail attı.


            return new DBEvents().UpdateGeneric<tblHamKumaslar>(kumas);
        }

        public static List<tblPersoneller> DokumacilariGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 12 && c.AktifMi == true);
        }

        public static List<tblPersoneller> KalitecileriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 14 && c.AktifMi == true);
        }

        public static List<tblPersoneller> OrmeKalitecileriGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 23 && c.AktifMi == true);
        }

        public static List<tblKumas> TipleriGetir()
        {
            return new DBEvents().GetGeneric<tblKumas>(c=>c.AktifMi==true && c.KumasCinsi != 396).OrderBy(o => o.TipNo).ToList();
        }

        public static List<tblKumas> OrmeTipleriGetir()
        {
            return new DBEvents().GetGeneric<tblKumas>(c => c.AktifMi == true && c.KumasCinsi == 396).OrderBy(o => o.TipNo).ToList();
        }

        public static List<tblMakinalar> TezgahlariGetir()
        {
            return new DBEvents().GetGeneric<tblMakinalar>(c => c.BaglantiId == 1 && c.AktifMi == true);
        }

        public static List<tblMakinalar> OrmeTezgahlariGetir()
        {
            return new DBEvents().GetGeneric<tblMakinalar>(c => c.BaglantiId == 3 && c.AktifMi == true);
        }

        public static List<vHataTanim> HataTanimlariGetir()
        {
            return new DBEvents().GetGeneric<vHataTanim>(c => (c.HataYerBagId == 1 || c.HataYerBagId == 3) && c.AktifMi == true).GroupBy(g => g.Adi).Select(s => s.First()).OrderBy(o => o.Kodu).ToList();
        }

        public static List<vHataTanim> OrmeHataTanimlariGetir()
        {
            return new DBEvents().GetGeneric<vHataTanim>(c => (c.HataYerBagId == 266) && c.AktifMi == true).GroupBy(g => g.Adi).Select(s => s.First()).OrderBy(o => o.Kodu).ToList();
        }

        public static List<vHamKumaslar> HamKayitlariGetir(DateTime basTarih, DateTime sonTarih)
        {
            List<vHamKumaslar> kumaslar = new DBEvents().GetGeneric<vHamKumaslar>(c => basTarih <= c.Tarih && c.Tarih <= sonTarih);
            return kumaslar.Where(c => c.KumasCinsiId != 396).ToList();
        }

        public static List<vHamKumaslar> OrmeKayitlariGetir(DateTime basTarih, DateTime sonTarih)
        {
            List<vHamKumaslar> kumaslar = new DBEvents().GetGeneric<vHamKumaslar>(c => basTarih <= c.Tarih && c.Tarih <= sonTarih);
            return kumaslar.Where(c => c.KumasCinsiId == 396).ToList();
        }

        public static vHamKumaslar HamKumasGetir(int kumasId, bool lotNoGetirilsin = false)
        {
            vHamKumaslar kumas = new DBEvents().GetGeneric<vHamKumaslar>(c => c.Id == kumasId).FirstOrDefault();

            if (lotNoGetirilsin)
            {
                tblLeventHareket levent = new DBEvents().GetGeneric<tblLeventHareket>(c => c.Id == kumas.HavLeventId).FirstOrDefault();
                if (levent == null) levent = new DBEvents().GetGeneric<tblLeventHareket>(c => c.Id == kumas.ZeminUstLeventId).FirstOrDefault();
                if (levent == null) levent = new DBEvents().GetGeneric<tblLeventHareket>(c => c.Id == kumas.ZeminAltLeventId).FirstOrDefault();

                if (levent != null)
                {
                    tblMalzemeCikis cikis = new DBEvents().GetGeneric<tblMalzemeCikis>(c => c.SetId == levent.SetId).FirstOrDefault();
                    if (cikis != null) kumas.LotNo = cikis.LotNo;
                }
            }

            return kumas;
        }

        public static List<vHamHataHaritasi> HamHatalariGetir(int hamId, bool ustHataMi)
        {
            if (ustHataMi) return new DBEvents().GetGeneric<vHamHataHaritasi>(c => c.UstId == hamId);
            else return new DBEvents().GetGeneric<vHamHataHaritasi>(c => c.AltId == hamId);
        }

        public static List<tblAyarlar> HamEtiketleriGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.BaglantiId == 113);
        }

        public static IQueryable<vHamKumaslar> HamStoklariGetir()
        {
            return new DBEvents().GetGeneric<vHamKumaslar>(c => c.PartiId == null).AsQueryable();
        }

        public static List<vHamKumaslar> HamStoklariGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vHamKumaslar>(c => c.PartiId == null && ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static IQueryable<vHamRapor> HamHareketleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vHamRapor>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih && c.PartiId != null).AsQueryable();
        }

        public static vHamKumaslar HamBarkodSorgula(string barkod)
        {
            DBEvents db = new DBEvents();

            vHamKumaslar ham = db.GetGeneric<vHamKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (ham != null && ham.PartiId.HasValue == true)
            {
                tblPartiler parti = db.GetGeneric<tblPartiler>(c => c.Id == ham.PartiId).FirstOrDefault();
                if (parti != null) ham.PartiNo = parti.PartiNo;
            }

            return ham;
        }

        #endregion

        public HamKumas(string kumasCinsi,vHamKumaslar duzeltilecekKumas = null)
        {
            Hatalar = new List<tblHamHatalari>();

            if (kumasCinsi == "Ham")
            {
                _KumasAlt = new tblHamKumaslar() { HavLeventId = null, ZeminAltLeventId = null, ZeminUstLeventId = null, SiparisId = null, Tur = "Alt", DepoId = 400 };
                _KumasUst = new tblHamKumaslar() { HavLeventId = null, ZeminAltLeventId = null, ZeminUstLeventId = null, SiparisId = null, Tur = "Ust", DepoId = 400 };

                if (duzeltilecekKumas != null)
                {
                    if (duzeltilecekKumas.Tur == "Ust")
                    {
                        _KumasAlt = _Db.GetGeneric<tblHamKumaslar>(c => c.TipId == duzeltilecekKumas.TipId && c.TezgahId == duzeltilecekKumas.TezgahId && c.Tarih == duzeltilecekKumas.Tarih && c.DokumaciId == duzeltilecekKumas.DokumaciId && c.Tur == "Alt").FirstOrDefault();
                        _KumasUst = duzeltilecekKumas.ViewToTbl();
                    }
                    if (duzeltilecekKumas.Tur == "Alt")
                    {
                        _KumasUst = _Db.GetGeneric<tblHamKumaslar>(c => c.TipId == duzeltilecekKumas.TipId && c.TezgahId == duzeltilecekKumas.TezgahId && c.Tarih == duzeltilecekKumas.Tarih && c.DokumaciId == duzeltilecekKumas.DokumaciId && c.Tur == "Ust").FirstOrDefault();
                        _KumasAlt = duzeltilecekKumas.ViewToTbl();
                    }

                    this._TipId = duzeltilecekKumas.TipId;
                    this._Aciklama = duzeltilecekKumas.Aciklama;
                    this._DokumaciId = duzeltilecekKumas.DokumaciId;
                    this._KaliteciId = duzeltilecekKumas.KaliteciId;
                    this._Tarih = duzeltilecekKumas.Tarih;
                    this._TezgahId = duzeltilecekKumas.TezgahId;
                    this._Varyant = duzeltilecekKumas.Varyant;
                }
            }
            else
            {
                //Örme kumaş
                _KumasAlt = new tblHamKumaslar() { HavLeventId = null, ZeminAltLeventId = null, ZeminUstLeventId = null, SiparisId = null, DepoId = 401 };
                _KumasUst = new tblHamKumaslar(); // Örme de kullanmıyoruz.
                if (duzeltilecekKumas != null)
                {                    
                    _KumasAlt = duzeltilecekKumas.ViewToTbl();                    
                    this._TipId = duzeltilecekKumas.TipId;
                    this._Aciklama = duzeltilecekKumas.Aciklama;
                    this._DokumaciId = duzeltilecekKumas.DokumaciId;
                    this._KaliteciId = duzeltilecekKumas.KaliteciId;
                    this._Tarih = duzeltilecekKumas.Tarih;
                    this._TezgahId = duzeltilecekKumas.TezgahId;
                    this._Varyant = duzeltilecekKumas.Varyant;
                }              
            }
        }

        

        private void HataPuanlariHesapla(tblHamHatalari hamHata)
        {
            if (hamHata.Uzunluk > 2)
            {
                if (hamHata.HataUstVarMi)
                {
                    this._KumasUst.HataAdet += Convert.ToInt32(hamHata.Uzunluk);
                    this._KumasUst.HataPuan += hamHata.Uzunluk.Value * 4;
                }
                if (hamHata.HataAltVarMi)
                {
                    this._KumasAlt.HataAdet += Convert.ToInt32(hamHata.Uzunluk);
                    this._KumasAlt.HataPuan += hamHata.Uzunluk.Value * 4;
                }
            }

            else
            {
                int hataId = hamHata.HataId.Value;
                tblHataTanim hataPuan = _Db.GetGeneric<tblHataTanim>(c => c.Kodu == hamHata.HataKodu && c.PuanAralik1 <= hamHata.Uzunluk && hamHata.Uzunluk <= c.PuanAralik2).FirstOrDefault();
                if (hamHata.HataUstVarMi)
                {
                    this._KumasUst.HataAdet += 1;
                    this._KumasUst.HataPuan += (hataPuan != null && hataPuan.Puan != null) ? hataPuan.Puan : 0;
                }
                if (hamHata.HataAltVarMi)
                {
                    this._KumasAlt.HataAdet += 1;
                    this._KumasAlt.HataPuan += (hataPuan != null && hataPuan.Puan != null) ? hataPuan.Puan : 0;
                }
            }
        }

        private void HataPuanlariHesapla()
        {
            _KumasAlt.HataAdet = 0;
            _KumasAlt.HataPuan = 0;

            _KumasUst.HataAdet = 0;
            _KumasUst.HataPuan = 0;

            foreach (tblHamHatalari item in this.Hatalar) HataPuanlariHesapla(item);
        }

        private DBEvents _Db = new DBEvents();
        public bool HataEkle(tblHamHatalari yeniHata)
        {
            if (yeniHata == null) return false;
            else
            {
                yeniHata.Uzunluk /= 100;

                HataPuanlariHesapla(yeniHata);
                GramajHesapla();
                KaliteHesapla();

                this.Hatalar.Add(yeniHata);
                return true;
            }
        }

        public bool HataSil(tblHamHatalari hata)
        {
            bool snc = true;
            if (hata.Id == 0)
            {
                Hatalar.Remove(hata);
                snc = true;
            }
            else
            {
                if (_Db.DeleteGeneric<tblHamHatalari>(hata))
                {
                    Hatalar.Remove(hata);
                    snc = true;
                }
                else snc = false;
            }

            if (snc)
            {
                HataPuanlariHesapla();
                GramajHesapla();
                KaliteHesapla();
            }
            return snc;
        }

        private bool HatalariKaydet()
        {
            foreach (tblHamHatalari item in this.Hatalar.FindAll(c => c.HataAltVarMi == true)) item.AltId = _KumasAlt.Id;
            foreach (tblHamHatalari item in this.Hatalar.FindAll(c => c.HataUstVarMi)) item.UstId = _KumasUst.Id;

            List<tblHamHatalari> toSave = this.Hatalar.FindAll(c => c.Id == 0);
            List<tblHamHatalari> toUpd = this.Hatalar.FindAll(c => c.Id != 0);

            bool snc = true;
            this.Hatalar = new List<tblHamHatalari>();
            if (toSave.Count > 0) if (_Db.SaveGeneric<tblHamHatalari>(ref toSave) == false) snc = false;
            if (toUpd.Count > 0) if (_Db.UpdateGeneric<tblHamHatalari>(toUpd) == false) snc = false;

            this.Hatalar.AddRange(toSave);
            this.Hatalar.AddRange(toUpd);

            return snc;
        }

        public bool HamKumasKaydet()
        {
            //if (this._KumasAlt.SiparisId == null || this._KumasUst.SiparisId == null)
            //    throw new Exception("Çözgüde sipariş bulunamadı..!");
            if (this._KumasAlt.HavLeventId.HasValue == false || this._KumasAlt.ZeminAltLeventId.HasValue == false || this._KumasAlt.ZeminUstLeventId.HasValue == false)
                throw new Exception("Leventler bulunamadı..!");
            
            HataPuanlariHesapla();
            GramajHesapla();
            KaliteHesapla();

            //alt kumaş kaydediliyor
            if (this._KumasAlt.Id == 0)
            {
                if (_Db.SaveGeneric<tblHamKumaslar>(ref _KumasAlt) == false) return false;
                else //Kumaş kaydedildikten sonra alınan id'ye göre barkod kaydedilir.
                {
                    _KumasAlt.Barkod = ('H' + _KumasAlt.Id.ToString()).PadLeft(10, '0');
                    double? altHataMetre = Hatalar.FindAll(c=>c.HataAltVarMi == true).Sum(s=>s.Uzunluk);
                    _KumasAlt.NetMetre = _KumasAlt.Metre;// -(altHataMetre.HasValue ? altHataMetre.Value : 0);
                    if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasAlt) == false) return false;
                }
            }
            else if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasAlt) == false) return false;

            //üst kumaş kaydediliyor
            if (this._KumasUst.Id == 0) 
            {
                if (_Db.SaveGeneric<tblHamKumaslar>(ref _KumasUst) == false) return false;
                else //Kumaş kaydedildikten sonra alınan id'ye göre barkod kaydedilir.
                {
                    _KumasUst.Barkod = ('H' + _KumasUst.Id.ToString()).PadLeft(10, '0');
                    double? ustHataMetre = Hatalar.FindAll(c => c.HataUstVarMi == true).Sum(s => s.Uzunluk);
                    _KumasUst.NetMetre = _KumasUst.Metre;// -(ustHataMetre.HasValue ? ustHataMetre.Value : 0);
                    if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasUst) == false) return false;
                }
            }
            else if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasUst) == false) return false;

            return HatalariKaydet(); // Hataların kaydedilmesi son adımdır. Buradan dönen sonuç tam, doğru bir şekilde ham kumaşın kaydedilip kaydedilmediği sonucudur.
        }

        public bool OrmeKumasKaydet()
        {           
            HataPuanlariHesapla();
            GramajHesapla();
            KaliteHesapla();

            //alt kumaş kaydediliyor
            if (this._KumasAlt.Id == 0)
            {
                if (_Db.SaveGeneric<tblHamKumaslar>(ref _KumasAlt) == false) return false;
                else //Kumaş kaydedildikten sonra alınan id'ye göre barkod kaydedilir.
                {
                    _KumasAlt.Barkod = ('R' + _KumasAlt.Id.ToString()).PadLeft(10, '0');
                    double? altHataMetre = Hatalar.FindAll(c => c.HataAltVarMi == true).Sum(s => s.Uzunluk);
                    _KumasAlt.NetMetre = _KumasAlt.Metre; // -(altHataMetre.HasValue ? altHataMetre.Value : 0);
                    if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasAlt) == false) return false;
                }
            }
            else if (_Db.UpdateGeneric<tblHamKumaslar>(_KumasAlt) == false) return false;

            return HatalariKaydet(); // Hataların kaydedilmesi son adımdır. Buradan dönen sonuç tam, doğru bir şekilde ham kumaşın kaydedilip kaydedilmediği sonucudur.
        }

        public static bool HamKumasSil(vHamKumaslar kumas)
        {
            tblHamKumaslar silinecek = new DBEvents().GetGeneric<tblHamKumaslar>(c => c.Id == kumas.Id).FirstOrDefault();

            return new DBEvents().DeleteGeneric<tblHamKumaslar>(silinecek);
        }

        private bool KaliteHesapla()
        {
            //kalite adet alt kumaş için hesaplanıyor
            if (_KumasAlt.Metre != null && _KumasAlt.Metre != 0) _KumasAlt.KaliteAdetDeger = Math.Round((_KumasAlt.HataAdet / _KumasAlt.Metre), 3);
            else _KumasAlt.KaliteAdetDeger = 0;
            tblKaliteTanim adetKalite = _Db.GetGeneric<tblKaliteTanim>(c => c.AdetAralik1 <= _KumasAlt.KaliteAdetDeger && _KumasAlt.KaliteAdetDeger <= c.AdetAralik2).FirstOrDefault();
            _KumasAlt.KaliteAdet = (adetKalite == null || adetKalite.Adi == null) ? "" : adetKalite.Adi;

            //kalite puan alt kumaş için hesaplanıyor
            if (_KumasAlt.Metre != null && _KumasAlt.Metre != 0) _KumasAlt.KalitePuanDeger = Math.Round((_KumasAlt.HataPuan * 100 / _KumasAlt.Metre), 3);
            else _KumasAlt.KalitePuanDeger = 0;
            tblKaliteTanim puanKalite = _Db.GetGeneric<tblKaliteTanim>(c => c.PuanAralik1 <= _KumasAlt.KalitePuanDeger && _KumasAlt.KalitePuanDeger <= c.PuanAralik2).FirstOrDefault();
            _KumasAlt.KalitePuan = (puanKalite == null || puanKalite.Adi == null) ? "" : puanKalite.Adi;

            //kalite adet üst kumaş için hesaplanıyor
            if (_KumasUst.Metre != null && _KumasUst.Metre != 0) _KumasUst.KaliteAdetDeger = Math.Round((_KumasUst.HataAdet / _KumasUst.Metre), 3);
            else _KumasUst.KaliteAdetDeger = 0;
            adetKalite = _Db.GetGeneric<tblKaliteTanim>(c => c.AdetAralik1 <= _KumasUst.KaliteAdetDeger && _KumasUst.KaliteAdetDeger <= c.AdetAralik2).FirstOrDefault();
            _KumasUst.KaliteAdet = (adetKalite == null || adetKalite.Adi == null) ? "" : adetKalite.Adi;

            //kalite puan üst kumaş için hesaplanıyor
            if (_KumasUst.Metre != null && _KumasUst.Metre != 0) _KumasUst.KalitePuanDeger = Math.Round((_KumasUst.HataPuan * 100 / _KumasUst.Metre), 3);
            else _KumasUst.KalitePuanDeger = 0;
            puanKalite = _Db.GetGeneric<tblKaliteTanim>(c => c.PuanAralik1 <= _KumasUst.KalitePuanDeger && _KumasUst.KalitePuanDeger <= c.PuanAralik2).FirstOrDefault();
            _KumasUst.KalitePuan = (puanKalite == null || puanKalite.Adi == null) ? "" : puanKalite.Adi;

            return true;
        }

        private bool GramajHesapla()
        {
            double dara = 0.96;

            if (_KumasAlt.Metre != null && _KumasAlt.Metre != 0) _KumasAlt.Gramaj = Math.Round((((_KumasAlt.Kg - dara) / _KumasAlt.Metre) * 1000), 2);
            else _KumasAlt.Gramaj = 0;

            if (_KumasUst.Metre != null && _KumasUst.Metre != 0) _KumasUst.Gramaj = Math.Round((((_KumasUst.Kg - dara) / _KumasUst.Metre) * 1000), 2);
            else _KumasUst.Gramaj = 0;

            return true;
        }
    }

    public class HamKesim
    {
        public vHamKumaslar AnaKumas { get; set; }

        public vHamKumaslar ParcaKumas { get; set; }

        DBEvents db = new DBEvents();

        public HamKesim()
        {
            AnaKumas = null;
            ParcaKumas = null;
        }

        public void BarkodOkut(string barkod)
        {
            AnaKumas = db.GetGeneric<vHamKumaslar>(c => c.Barkod == barkod && c.PartiId.HasValue == false).FirstOrDefault();
            if (AnaKumas == null) throw new Exception("Barkod bulunamadı..!");
            ParcaKumas = AnaKumas.CopyToNewObject();
            ParcaKumas.Metre = 0;
            ParcaKumas.Kg = 0;
            ParcaKumas.Id = 0;
            ParcaKumas.NetMetre = 0;
            ParcaKumas.Barkod = "";
        }

        public double ParcaKgHesapla()
        {
            tblKumas tip = db.GetGeneric<tblKumas>(c => c.Id == ParcaKumas.TipId).FirstOrDefault();
            if (tip != null && tip.MamulAgirlik != null) ParcaKumas.Kg = Math.Round((tip.MamulAgirlik.Value * ParcaKumas.Metre / 1000), 2);

            return ParcaKumas.Kg;
        }

        public bool Parcala()
        {
            if (this.AnaKumas == null || this.ParcaKumas == null) throw new Exception("Barkod okutulmamış..!");
            if (this.ParcaKumas.Metre <= 0) throw new Exception("Kesilen metre 0'dan büyük olmalıdır.");
            if (this.ParcaKumas.Kg <= 0) throw new Exception("Kg 0'dan büyük olmalıdır.");
            if (this.ParcaKumas.Metre > this.AnaKumas.Metre) throw new Exception("Fazla metre kesilemez..!\n\nMamul metresi : " + this.AnaKumas.Metre.ToString());
            if (this.ParcaKumas.Kg > this.AnaKumas.Kg) throw new Exception("Kesilenin kg'ı barkoddan fazla olamaz..!\n\nMamul kg : " + this.AnaKumas.Kg.ToString());

            ParcaKumas.NetMetre = ParcaKumas.Metre;
            AnaKumas.Metre = Math.Round((AnaKumas.Metre - ParcaKumas.Metre), 2);
            AnaKumas.NetMetre = Math.Round((AnaKumas.NetMetre - ParcaKumas.NetMetre), 2);
            AnaKumas.Kg = Math.Round((AnaKumas.Kg - ParcaKumas.Kg), 2);

            if (db.UpdateGeneric<tblHamKumaslar>(AnaKumas.ViewToTbl()))
            {
                tblHamKumaslar hamtbl = this.ParcaKumas.ViewToTbl();
                db.SaveGeneric<tblHamKumaslar>(ref hamtbl);
                this.ParcaKumas.Id = hamtbl.Id;
                this.ParcaKumas.Barkod = ('H' + hamtbl.Id.ToString()).PadLeft(10, '0');
                return db.UpdateGeneric<tblHamKumaslar>(this.ParcaKumas.ViewToTbl());
            }

            return false;
        }
    }
}

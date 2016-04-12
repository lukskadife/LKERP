using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace LKLibrary.DbClasses
{
    [Table(Name = "vHamRapor")]
    public class vHamRapor : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int? SiparisId { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string TezgahNo { get; set; }

        [Column]
        public int? DokumaciId { get; set; }

        [Column]
        public string DokumaciAdi { get; set; }

        [Column]
        public int? KaliteciId { get; set; }

        [Column]
        public string KaliteciAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double Kg { get; set; }

        [Column]
        public double Gramaj { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public int? HavLeventId { get; set; }

        [Column]
        public int? ZeminAltLeventId { get; set; }

        [Column]
        public int? ZeminUstLeventId { get; set; }

        [Column]
        public int HataAdet { get; set; }

        [Column]
        public double HataPuan { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public double KaliteAdetDeger { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public double KalitePuanDeger { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public int? SonProcessId { get; set; }

        [Column]
        public string SonProcessKodu { get; set; }

        [Column]
        public string SonProcessAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vSatisSiparisRaporu : IDisposable
    {
        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkKodu { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string KarsiReferansNo { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public DateTime? SatirTerminTarihi { get; set; }

        [Column]
        public double? SevkMiktar { get; set; }

        [Column]
        public string SatirDurum { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string FinishKodu { get; set; }

        [Column]
        public DateTime? UstTerminTarihi { get; set; }

        [Column]
        public DateTime? SiparisTarihi { get; set; }

        [Column]
        public double? IadeMiktar { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public double? PartilenenMetre { get; set; }

        [Column]
        public string SiparisDurum { get; set; }

        [Column]
        public DateTime? SevkTarihi { get; set; }

        [Column]
        public string Hazirlayan { get; set; }

        [Column]
        public string KoleksiyonAdi { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public int? TerminHaftasi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vIplikCikisRaporu : IDisposable
    {
        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public string CikisTanim { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string FasonAdi { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vIplikGirisRaporu : IDisposable
    {
        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public double? BrutKg { get; set; }

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public string GirisTanim { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string IrsaliyeNo { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public string FasonAdi { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKaliteDagilimTip : IDisposable
    {
        [Column(Name = "TIP_NO")]
        public string TIP_NO { get; set; }

        [Column(Name = "RENK_NO")]
        public string RENK_NO { get; set; }

        [Column(Name = "URETIM")]
        public double? URETIM { get; set; }

        [Column(Name = "BIRKALITE")]
        public double? BIRKALITE { get; set; }

        [Column(Name = "IKIKALITE")]
        public double? IKIKALITE { get; set; }

        [Column(Name = "HATALI")]
        public double? HATALI { get; set; }

        [Column(Name = "ORAN1")]
        public double? ORAN1 { get; set; }

        [Column(Name = "ORAN2")]
        public double? ORAN2 { get; set; }

        [Column(Name = "ORANHATALI")]
        public double? ORANHATALI { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKaliteDagilimMusteri : IDisposable
    {
        [Column]
        public string MUSTERI_ADI { get; set; }

        [Column(Name = "RENK_NO")]
        public string RENK_NO { get; set; }

        [Column(Name = "URETIM")]
        public double? URETIM { get; set; }

        [Column(Name = "BIRKALITE")]
        public double? BIRKALITE { get; set; }

        [Column(Name = "IKIKALITE")]
        public double? IKIKALITE { get; set; }

        [Column(Name = "HATALI")]
        public double? HATALI { get; set; }

        [Column(Name = "ORAN1")]
        public double? ORAN1 { get; set; }

        [Column(Name = "ORAN2")]
        public double? ORAN2 { get; set; }

        [Column(Name = "ORANHATALI")]
        public double? ORANHATALI { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKaliteDagilimTezgah : IDisposable
    {
        [Column]
        public string TEZGAH_NO { get; set; }

        [Column]
        public string TIP_NO { get; set; }

        [Column(Name = "URETIM")]
        public double? URETIM { get; set; }

        [Column(Name = "BIRKALITE")]
        public double? BIRKALITE { get; set; }

        [Column(Name = "IKIKALITE")]
        public double? IKIKALITE { get; set; }

        [Column(Name = "HATALI")]
        public double? HATALI { get; set; }

        [Column(Name = "ORAN1")]
        public double? ORAN1 { get; set; }

        [Column(Name = "ORAN2")]
        public double? ORAN2 { get; set; }

        [Column(Name = "ORANHATALI")]
        public double? ORANHATALI { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vMamulKumasUretimRaporu : IDisposable
    {
        [Column]
        public string Barkod { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public double? NetMetre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public int? HataAdet { get; set; }

        [Column]
        public double? HataPuan { get; set; }

        [Column]
        public double? KaliteAdetDeger { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public double? KalitePuanDeger { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string KaliteciAdi { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SevkiyatNotu { get; set; }

        [Column]
        public string Finish { get; set; }

        [Column]
        public string HamBarkod { get; set; }

        [Column]
        public string FasonTipi { get; set; }

        [Column]
        public string Parca { get; set; }

        [Column]
        public string ParcaMetre { get; set; }

        [Column]
        public double? Kg { get; set; }

        [Column]
        public string HataList { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string DyeBatchNo { get; set; }

        [Column]
        public DateTime? Saat { get; set; }

        [Column]
        public string HamTezgahKodu { get; set; }

        [Column]
        public string HamTezgahAdi { get; set; }

        [Column]
        public string KumasinFasonBilgisi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }
    public class vTezgahRandimanRapor : IDisposable
    {
        [Column]
        public string TezgahNo { get; set; }
        [Column]
        public string Model { get; set; }
        [Column]
        public string TipNo { get; set; }
        [Column]
        public string Postasi { get; set; }
        [Column]
        public DateTime Tarih { get; set; }
        [Column]
        public int AtkiSayisi { get; set; }
        [Column]
        public double? Devir { get; set; }
        [Column]
        public double? AtkiSiklik { get; set; }
        [Column]
        public double? MaksDevir { get; set; }
        [Column]
        public string Dokumaci { get; set; }
        [Column]
        public double? DurussuzRandiman { get; set; }
        [Column]
        public double? DurusluRandiman { get; set; }
        [Column]
        public double? DurusSuresi { get; set; }
        [Column]
        public double? DokunanMetre { get; set; }
        [Column]
        public double? TeorikAtki { get; set; }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    public class vMamulSevkiyatRaporu  : IDisposable
    {
        [Column]
        public string BelgeNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SevkEdenAdi { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string KarsiReferansNo { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public double? BrutMetre { get; set; }

        [Column]
        public double? NetMetre { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string ParcaMetre { get; set; }

        [Column]
        public string Parca { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string DyeBatchNo { get; set; }

        [Column]
        public string KoleksiyonAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    public class vKimyasalGirisRaporu : IDisposable
    {
        [Column]
        public string MalzemeKodu { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public string PersonelKodu { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string IrsaliyeNo { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column]
        public double? Miktar { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    public class vKimyasalCikisRaporu : IDisposable
    {
        [Column]
        public string MalzemeKodu { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column]
        public double? Miktar { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vHataDagilimHamTipRaporu : IDisposable
    {
        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? Uretim { get; set; }

        [Column]
        public double? Uzunluk { get; set; }

        [Column]
        public string HataAdi { get; set; }

        [Column]
        public string HataYeri { get; set; }

        [Column]
        public int Adet { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    public class vHataDagilimHamTezgahRaporu : IDisposable
    {
        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? Uretim { get; set; }

        [Column]
        public double? Uzunluk { get; set; }

        [Column]
        public string HataAdi { get; set; }

        [Column]
        public int Adet { get; set; }

        [Column]
        public string TezgahNo { get; set; }

        [Column]
        public string HataYeri { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vHataDagilimMamulRaporu : IDisposable
    {
        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? Uretim { get; set; }

        [Column]
        public double? Uzunluk { get; set; }

        [Column]
        public string HataAdi { get; set; }

        [Column]
        public string HataYeri { get; set; }

        [Column]
        public int Adet { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vHamUretimRaporu : IDisposable
    {
        [Column]
        public string Barkod { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TezgahNo { get; set; }

        [Column]
        public string DokumaciAdi { get; set; }

        [Column]
        public string KaliteciAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public double BrutMetre { get; set; }

        [Column]
        public double BrutKg { get; set; }

        [Column]
        public double Gramaj { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public double KaliteAdetDeger { get; set; }

        [Column]
        public int HataAdet { get; set; }

        [Column]
        public string HataList { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public double KalitePuanDeger { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string SonProcessAdi { get; set; }

        [Column]
        public string HavLeventNo { get; set; }

        [Column]
        public string ZeminAltLeventNo { get; set; }

        [Column]
        public string ZeminUstLeventNo { get; set; }

        [Column]
        public DateTime? TarihSaat { get; set; }

        [Column]
        public string KumasCinsi { get; set; }

        [Column]
        public string Depo { get; set; }

        [Column]
        public DateTime? Saat { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKimyasalStokRaporu : IDisposable
    {
        [Column]
        public string Adi { get; set; }

        //[Column]
        //public double? GirisKg { get; set; }

        //[Column]
        //public double? CikisKg { get; set; }

        [Column]
        public double? KalanKg { get; set; }

        [Column]
        public DateTime AlimTarih { get; set; }

        [Column]
        public DateTime SKT { get; set; }

        [Column]
        public int BeklemeGun { get; set; }

        [Column]
        public int KalanGun { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table]
    public class vBoyaPlani : IDisposable
    {
        [Column]
        public string Makina { get; set; }

        [Column]
        public double? PartiMetre { get; set; }

        [Column]
        public double? JarzMetraji { get; set; }

        [Column]
        public int? GunlukJarz { get; set; }

        [Column]
        public double? GunlukMetre { get; set; }

        [Column]
        public int? UretimDakika { get; set; }

        [Column]
        public string UretimZamani { get; set; }

        [Column]
        public int? JarzSayisi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        public string Gunluk
        {
            get
            {
                string snc = (GunlukMetre.HasValue ? GunlukMetre.ToString() : "") + " mt./ ";
                snc += (JarzSayisi.HasValue ? GunlukJarz.ToString() : "") + " jarz";
                return snc;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //public class vKonsolKalanSiparislerRaporu : IDisposable
    //{
    //    [Column]
    //    public string MusteriAdi { get; set; }

    //    [Column]
    //    public string TipNo { get; set; }

    //    [Column]
    //    public string RenkNo { get; set; }

    //    [Column]
    //    public double? SiparisMetre { get; set; }

    //    [Column]
    //    public double? SevkMetre { get; set; }

    //    [Column]
    //    public double? Kalan { get; set; }

    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(this);
    //    }
    
    //    #endregion
    //}

    public class vKonsolKumasRaporu : IDisposable
    {
        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double? NetMetre { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public int? BeklemeGunu { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string KumasCinsi { get; set; }

        [Column]
        public string Depo { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKonsolSiparisRaporu : IDisposable
    {
        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double? SiparisMetre { get; set; }

        [Column]
        public double? SevkMetre { get; set; }

        [Column]
        public double? Kalan { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public string KumasCinsi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKonsolDuruslarRaporu : IDisposable
    {
        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public string ArizaAdi { get; set; }

        [Column]
        public string Fark { get; set; }

        [Column]
        public int? FarkDakika { get; set; }

        [Column]
        public string Aciklama { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKonsolBoyaSiparisRaporu : IDisposable
    {
        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? PartiMetre { get; set; }

        [Column]
        public string Makina { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vKimyasalSarfiyatTipBazli : IDisposable
    {
        [Column]
        public int KimyasalId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double HarcananBoyaKg { get; set; }

        [Column]
        public double HarcananKimyasalKg { get; set; }

        [Column]
        public double TutarBoya { get; set; }

        [Column]
        public double TutarKimyasal { get; set; }       

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    public class vSiparisTerminRaporu : IDisposable
    {
        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }
        
        [Column]
        public string Durum { get; set; }

        [Column]
        public int Gecikme { get; set; }
        
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vSiparisSatirTerminRaporu : IDisposable
    {
        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? Gecikme { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vBoyahaneHareketRaporu : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public DateTime Saat { get; set; }

        [Column]
        public DateTime? CikisTarih { get; set; }

        [Column]
        public DateTime? CikisSaat { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public string ProcessKodu { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string PersonelKodu { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double AcilmisMetre { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool? ReProcess { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public bool FasonMu { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string Musteri { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string MakinaAdi { get; set; }

        [Column]
        public string KalanProcessler { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 29.05.2014
    public class vSiparisKaliteRaporu : IDisposable
    {
        [Column]
        public double BrutMetre { get; set; }

        [Column]
        public string Kalitesi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 02.07.2014
    public class vSiparisParcaRaporu : IDisposable
    {
        [Column]
        public double ParcaMetre { get; set; }

        [Column]
        public string Parca { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 29.05.2014
    public class vDovizKarsiliginiHesapla : IDisposable
    {
        [Column]
        public double tutarDoviz { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 18.06.2014
    public class vBukumCozgumMaliyet : IDisposable
    {
        [Column]
        public int IplikId { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public double TekKatIplikFiyati { get; set; }

        [Column]
        public double CiftKatIplikFiyati { get; set; }

        [Column]
        public double FiiliUretim { get; set; }

        [Column]
        public double ToplamUretim { get; set; }

        [Column]
        public double Katsayi { get; set; }

        [Column]
        public double ToplamKatsayi { get; set; }

        [Column]
        public double IscilikGideri { get; set; }

        [Column]
        public double ElektrikGideri { get; set; }

        [Column]
        public double DogalGazGideri { get; set; }

        [Column]
        public double AtikSuGideri { get; set; }

        [Column]
        public double AmortismanGideri { get; set; }

        [Column]
        public double FasonGideri { get; set; }

        [Column]
        public double NakliyeBakimGideri { get; set; }

        [Column]
        public double IsletmeMalzemeGideri { get; set; }

        [Column]
        public double ToplamBukumluIplikMaliyeti { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public string Ayırac { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 18.06.2014
    public class vDokumaTumMaliyet : IDisposable
    {

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public string Cinsi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public double? GecenMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public double? DirekIscilik { get; set; }

        [Column]
        public double? DogalGaz { get; set; }

        [Column]
        public double? Elektrik { get; set; }

        [Column]
        public double? AtikSu { get; set; }

        [Column]
        public double? Amortisman { get; set; }

        [Column]
        public double? IsletmeMalzele { get; set; }

        [Column]
        public double? NakliyeBakimGideri { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        [Column]
        public double? Kimyasal { get; set; }

        [Column]
        public double? EnDirekIscilik { get; set; }

        [Column]
        public double? Ambalaj { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 18.06.2014
    public class vIplikTumMaliyet : IDisposable
    {
        [Column]
        public int IplikId { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public double UstuBuluGr { get; set; }

        [Column]
        public double KullanilanIplikKg { get; set; }

        [Column]
        public double IplikBirimFiyati { get; set; }

        [Column]
        public double KullanilanIplikFiyati { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double TopMetre { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 11.05.2014
    public class vProcessMaliyetleri : IDisposable
    {
        [Column]
        public int PartiId { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int MakinaId { get; set; }

        [Column]
        public bool FasonMu { get; set; }

        [Column]
        public double GecenMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public bool ReProcess { get; set; }

        [Column]
        public int ProcessNetCalismaDk { get; set; }

        [Column]
        public int MakinaAylikNetCalismaDk { get; set; }

        [Column]
        public double MaliyetDirekIscilik { get; set; }

        [Column]
        public double MaliyetDogalGaz { get; set; }

        [Column]
        public double MaliyetElektrik { get; set; }

        [Column]
        public double MaliyetAtikSu { get; set; }

        [Column]
        public double MaliyetAmortisman { get; set; }

        [Column]
        public double MaliyetIsletmeMalzeme { get; set; }

        [Column]
        public double MaliyetNakliyeBakımGideri { get; set; }

        [Column]
        public double MaliyetEndirekIscilik { get; set; }

        [Column]
        public double MaliyetKimyasal { get; set; }

        [Column]
        public double MaliyetFason { get; set; }

        [Column]
        public double MaliyetAmbalaj { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public string MakinaAdi { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public double ToplamMaliyet { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public int Sira { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }


     //Gökhan 27.06.2014
    public class vTumMaliyetlerGrup : IDisposable
    {
        [Column]
        public string PartiNo { get; set; }

        [Column]
        public double? UretimMetresi { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public string TipNo { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vMaliyetTipBazinda :IDisposable
    {
        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string FirmaAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int Id { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public double MamulMetre { get; set; }

        [Column]
        public double SevkMetre { get; set; }

        [Column]
        public double IKalite { get; set; }

        [Column]
        public double IIKalite { get; set; }

        [Column]
        public double Hatali { get; set; }

        [Column]
        public double Parca { get; set; }

        [Column]
        public double ReProcess { get; set; }

        [Column]
        public double SatisTutari { get; set; }

        [Column]
        public double IplikMaliyet { get; set; }

        [Column]
        public double BukumMaliyet { get; set; }

        [Column]
        public double CozguMaliyet { get; set; }

        [Column]
        public double DokumaMaliyet { get; set; }

        [Column]
        public double BoyahaneMaliyet { get; set; }

        [Column]
        public double MaliyetToplami { get; set; }

        [Column]
        public string Pound { get; set; }

        [Column]
        public string Dolar { get; set; }

        [Column]
        public string Euro { get; set; }

        [Column]
        public string TL { get; set; }

        [Column]
        public string KarZarar { get; set; }

        [Column]
        public string Yuzde { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    //Gökhan 08.08.2014
    public class vMaliyetTumunuGoster : IDisposable
    {
        [Column]
        public string Cinsi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public double GecenMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public double DirekIscilik { get; set; }

        [Column]
        public double Dogalgaz { get; set; }

        [Column]
        public double Elektrik { get; set; }

        [Column]
        public double AtikSu { get; set; }

        [Column]
        public double Amortisman { get; set; }

        [Column]
        public double IsletmeMalzeme { get; set; }

        [Column]
        public double NakliyeBakimGideri { get; set; }

        [Column]
        public double Kimyasal { get; set; }

        [Column]
        public double EnDirekIscilik { get; set; }

        [Column]
        public double Ambalaj { get; set; }

        [Column]
        public double SatirToplami { get; set; }

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public double KullanilanIplikKg { get; set; }

        [Column]
        public double KullanilanIplikFiyati { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double FasonProcess { get; set; }

        [Column]
        public double FasonBukum { get; set; }

        [Column]
        public double FasonBoyali { get; set; }

        [Column]
        public double FasonRenkDuzeltme { get; set; }

        [Column]
        public double FasonFikse { get; set; }

        [Column]
        public int MaliyetSira { get; set; }

        [Column]
        public double Boyasal { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 20.08.2014
    public class vReProcessRaporu : IDisposable
    {  

        [Column]
        public int Id { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public DateTime? Saat { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public bool ReProcess { get; set; }

        [Column]
        public DateTime? CikisTarih { get; set; }

        [Column]
        public DateTime? CikisSaat { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string MakinaAdi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    

    //Gökhan 25.08.2014
    [Table(Name = "vSiradakiProcessRaporu")]
    public class vSiradakiProcessRaporu : IDisposable
    {
        [Column]
        public int PartiId { get; set; }

        [Column]
        public string Process { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public string FirmaAdi { get; set; }

        [Column]
        public double Metre { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    //[Table(Name = "")]
    //public class  : IDisposable
    //{
    //    [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
    //    public int Id { get; set; }

    //    [Column]
    //    public string  { get; set; }

    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(this);
    //    }
    //
    //    #endregion
    //}
}

using System;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace ETSevk.Classes
{
    [Table(Name = "tblAyarlar")]
    public class tblAyarlar : IDisposable
    {
        const string AlanAdi = "sss";
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "BaglantiId", CanBeNull = false)]
        public int BaglantiId { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column(Name = "Deger")]
        public string Deger { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Aciklama2 { get; set; }

        [Column]
        public bool? GosterilsinMi { get; set; }

        [Column]
        public int? Sira { get; set; }

        [Column]
        public bool? BosGecilebilirMi { get; set; }

        [Column]
        public bool? KontrolMu { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblExceptionLog")]
    class tblExceptionLog
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "CompanyId")]
        public int CompanyId { get; set; }

        [Column(Name = "FunctionName")]
        public string FunctionName { get; set; }

        [Column(Name = "Message")]
        public string Message { get; set; }

        [Column(Name = "InnerMessage")]
        public string InnerMessage { get; set; }

        [Column(Name = "RecordDate")]
        public DateTime RecordDate { get; set; }

        [Column(Name = "Source")]
        public string Source { get; set; }

        public bool SaveException(tblExceptionLog exception)
        {
            return new DBEvents().SaveGeneric<tblExceptionLog>(ref exception);
        }
    }

    [Table(Name = "tblDurumlar")]
    public class tblDurumlar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public int AyarId { get; set; }

        [Column]
        public string DurumAdi { get; set; }

        [Column]
        public string AcilacakSayfa { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool KontrolMu { get; set; }

        [Column]
        public string Tanim { get; set; }

        public tblDurumlar DurumGetir(int durumId)
        {
            return new DBEvents().GetGeneric<tblDurumlar>(c => c.Id == durumId)[0];
        }

        public static tblDurumlar DurumGetir(string tanim)
        {
            return new DBEvents().GetGeneric<tblDurumlar>(c => c.Tanim == tanim).FirstOrDefault();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMalzemeler")]
    public class tblMalzemeler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public bool? AktifMi { get; set; }

        [Column]
        internal int? EntId { get; set; }

        [Column]
        public bool? LeventteGor { get; set; }

        [Column]
        public int? IplikNo { get; set; }

        [Column]
        public string Cins { get; set; }

        [Column(Name = "MinStok")]
        private double? _MinStok;

        public double? MinStok
        {
            get
            {
                return _MinStok;
            }
            set
            {
                _MinStok = value;
                MinStokDegistiMi = true;
            }
        }

        public bool MinStokDegistiMi = false;

        public static tblMalzemeler MalzemeGetir(int malzemeId)
        {
            return new DBEvents().GetGeneric<tblMalzemeler>(c => c.Id == malzemeId).FirstOrDefault();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTalepler")]
    public class tblTalepler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public int DurumId { get; set; }

        [Column]
        public int TalepEdenId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Detay { get; set; }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public int? RenkId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTalepKarsilamaAct")]
    public class tblTalepKarsilamaAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TalepKarsilamaId { get; set; }

        [Column]
        public int TalepId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public double Fiyat { get; set; }

        [Column]
        public int TedarikciId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public double Kur { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTalepKarsilama")]
    public class tblTalepKarsilama : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public string No { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int DurumId { get; set; }

        [Column]
        public string OdemeSekli { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFirmalar")]
    public class tblFirmalar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        internal int EntId { get; set; }

        [Column]
        public string Adres { get; set; }

        public static List<tblFirmalar> MusterileriGetir()
        {
            return new DBEvents().GetGeneric<tblFirmalar>(c => c.Kodu.StartsWith("M") || c.Kodu.StartsWith("YM") || c.Kodu == "T380000").OrderBy(o => o.Adi).ToList();
        }

        public static List<tblFirmalar> FirmalariGetir(int baglantiId = 0)
        {
            var query = PredicateBuilder.True<tblFirmalar>();

            if (baglantiId != 0) query = query.And<tblFirmalar>(c => c.BaglantiId == baglantiId);

            return new DBEvents().GetGeneric<tblFirmalar>(query).OrderBy(c => c.Adi).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPersoneller")]
    public class tblPersoneller : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int? BolumId { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        internal int EntId { get; set; }

        public static List<tblPersoneller> PersonelleriGetir(bool sadeceAktifler = true)
        {
            var query = PredicateBuilder.True<tblPersoneller>();
            if (sadeceAktifler) query = query.And<tblPersoneller>(c => c.AktifMi == true);
            return new DBEvents().GetGeneric<tblPersoneller>(query).OrderBy(o => o.Adi).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPersonelBolumleri")]
    public class tblPersonelBolumleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        internal int EntId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTalepKarsilamaBelgeleri")]
    public class tblTalepKarsilamaBelgeleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string DosyaAdi { get; set; }

        [Column]
        public string DosyaTamAdi { get; set; }

        [Column]
        public int KarsilamaId { get; set; }

        [Column]
        public string Turu { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBirimler")]
    public class tblBirimler : IDisposable
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double BirimCarpan { get; set; }

        [Column]
        public double AnaCarpan { get; set; }

        [Column]
        public int EntId { get; set; }

        [Column]
        public string BirimAdi { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMalzemeGiris")]
    public class tblMalzemeGiris : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int? BirimId { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public int? SaticiId { get; set; }

        [Column]
        public int? KarsilamaActId { get; set; }

        [Column]
        public string GirisTanim { get; set; }

        [Column]
        public int? GirisTanimId { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public double? BrutKg { get; set; }

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string IrsaliyeNo { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public double? Metre { get; set; }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBakimOnarim")]
    internal class tblBakimOnarim : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MakinaId { get; set; }

        [Column]
        public DateTime BasTarih { get; set; }

        [Column]
        public DateTime BitisTarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Vardiya { get; set; }

        [Column]
        public double HarcananSure { get; set; }

        [Column]
        public string Turu { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public DateTime OlusturmaTarihi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBakimOnarimAct")]
    internal class tblBakimOnarimAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BakimOnarimId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public DateTime OlusturmaTarihi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMakinalar")]
    public class tblMakinalar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? BaglantiId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int? BakimPeriyodu { get; set; }

        [Column(Name = "GunlukDokuma")]
        public double? DevirSayisi { get; set; }

        public string KodAd { get { return this.Kodu + " - " + this.Adi; } }

        [Column]
        public bool? AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSayaclar")]
    public class tblSayaclar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int? MaxAltDal { get; set; }

        [Column]
        public int Derinlik { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMalzemeCikis")]
    public class tblMalzemeCikis : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int? BirimId { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string CikisTanim { get; set; }

        [Column]
        public int? CikisTanimId { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public double? BrutKg { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public int? SaticiId { get; set; }

        [Column]
        public Int64? SetId { get; set; }

        [Column]
        public int? SetHareketId { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSayacGirisleri")]
    public class tblSayacGirisleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SayacId { get; set; }

        [Column]
        public double? kwh { get; set; }

        [Column]
        public double? ton { get; set; }

        [Column]
        public double? m3 { get; set; }

        [Column]
        public double? sm3 { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFiyatListeleri")]
    public class tblFiyatListeleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public int? ProcessId { get; set; }

        [Column]
        public string Tip { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public double Fiyat { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Grup { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public double? Gr { get; set; }

        [Column]
        public string Kompozisyon { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int OlusturanPersonelId { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSayacBirimFiyatlari")]
    public class tblSayacBirimFiyatlari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SayacId { get; set; }

        [Column]
        public double Fiyat { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public DateTime OlusturmaTarihi { get; set; }

        [Column]
        public int OlusturanPersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSiparisler")]
    public class tblSiparisler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int? FirmaId { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public string KarsiReferansNo { get; set; }

        [Column]
        public string SipVeren { get; set; }

        [Column]
        public string SevkYeri { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public string BelgeTuru { get; set; }

        [Column]
        public int? OlusturanPersId { get; set; }

        [Column]
        public string SipOnaylayan { get; set; }

        [Column]
        public int? GuncelleyenPersId { get; set; }

        [Column]
        public string OrderNo { get; set; }

        [Column]
        public int? OrderSayi { get; set; }

        [Column]
        public string FaturaAdresi { get; set; }

        [Column]
        public string KargoUcreti { get; set; }

        [Column]
        public string FaturaNo { get; set; }

        [Column]
        public string SevkiyatSekli { get; set; }

        [Column]
        public string OdemeSekli { get; set; }

        [Column]
        public bool? SevkEdilebilirMi { get; set; }

        [Column]
        public string IadeSipNo { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBelgeNo")]
    public class tblBelgeNo : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Tipi { get; set; }

        [Column]
        public int SonBelgeNo { get; set; }

        [Column]
        public int Yil { get; set; }

        internal static tblBelgeNo BelgeNoGetir(string tipi)
        {
            tblBelgeNo belgeNo = new DBEvents().GetGeneric<tblBelgeNo>(c => c.Tipi == tipi && c.Yil == DateTime.Now.Year).FirstOrDefault();

            if (belgeNo == null) belgeNo = new tblBelgeNo()
            {
                SonBelgeNo = 0,
                Tipi = tipi,
                Yil = DateTime.Now.Year
            };

            return belgeNo;
        }

        internal static bool BelgeNoKaydet(tblBelgeNo belgeNo)
        {
            if (belgeNo.Id == 0) return new DBEvents().SaveGeneric<tblBelgeNo>(belgeNo);
            else return new DBEvents().UpdateGeneric<tblBelgeNo>(belgeNo);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSiparisAct")]
    public class tblSiparisAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int? TipMalzemeId { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public string ParcaliTop { get; set; }

        [Column]
        public string LabRenkNo { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public string FinishOzellikleri { get; set; }

        [Column]
        public string SevkDurumu { get; set; }

        [Column]
        public double? SevkMiktar { get; set; }

        [Column]
        public string BoyaNotu { get; set; }

        [Column]
        public string DokumaNotu { get; set; }

        [Column]
        public string SevkiyatNotu { get; set; }

        [Column]
        public string PlanlamaNotu { get; set; }

        [Column]
        public string IcTicaretNotu { get; set; }

        [Column]
        public string DisTicaretNotu { get; set; }

        [Column]
        public string MuhasebeNotu { get; set; }

        [Column]
        public string NumuneNotu { get; set; }

        [Column]
        public string TerminNotu { get; set; }

        [Column]
        public bool? NorApre { get; set; }

        [Column]
        public bool? SuYagApre { get; set; }

        [Column]
        public bool? SirtApre { get; set; }

        [Column]
        public bool? NorYanmazApre { get; set; }

        [Column]
        public bool? YumApre { get; set; }

        [Column]
        public bool? AntiStatik { get; set; }

        [Column]
        public bool? Apresiz { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public int? FinishGrupId { get; set; }

        [Column]
        public int? TestId { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public bool? BoyandiMi { get; set; }

        [Column]
        public string RefTipNo { get; set; }

        [Column]
        public string RefTipAdi { get; set; }

        [Column]
        public string RefRenkNo { get; set; }

        [Column]
        public string RefRenkAdi { get; set; }

        [Column]
        public string RefBarkod { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblProses")]
    public class tblProses : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        public static List<tblProses> ProsesleriGetir(bool yalnizcaAktifler = false)
        {
            if (yalnizcaAktifler) return new DBEvents().GetGeneric<tblProses>(c => c.AktifMi == true);
            return new DBEvents().GetGeneric<tblProses>();
        }

        public static bool ProsesKaydet(ref tblProses proses)
        {
            if (proses.Id == 0)
            {
                string kodu = proses.Kodu;
                if (new DBEvents().GetGeneric<tblProses>(c => c.Kodu == kodu).FirstOrDefault() != null)
                    throw new Exception("Bu kod numarası kullanımda..!");

                return new DBEvents().SaveGeneric<tblProses>(ref proses);
            }
            else return new DBEvents().UpdateGeneric<tblProses>(proses);
        }

        public static bool ProsesSil(tblProses process)
        {
            return new DBEvents().DeleteGeneric<tblProses>(process);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumas")]
    public class tblKumas : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public string NumuneTipNo { get; set; }

        [Column]
        public double? KumasEn { get; set; }

        [Column]
        public double? KumasAgirlik { get; set; }

        [Column]
        public double? KumasAgirlik2 { get; set; }

        [Column]
        public string HavKomp { get; set; }

        [Column]
        public string TotalKomp { get; set; }

        [Column]
        public string KulAlani { get; set; }

        [Column]
        public string Martindale { get; set; }

        [Column]
        public bool? Yik1 { get; set; }

        [Column]
        public bool? Yik2 { get; set; }

        [Column]
        public bool? Yik3 { get; set; }

        [Column]
        public bool? Yik4 { get; set; }

        [Column]
        public bool? Yik5 { get; set; }

        [Column]
        public string YikamaTalNot { get; set; }

        [Column]
        public string MukavemetAtki { get; set; }

        [Column]
        public string MukavemetCözgü { get; set; }

        [Column]
        public string DikisSiyrikAtki { get; set; }

        [Column]
        public string DikSiyrikCozgu { get; set; }

        [Column]
        public string RenkHaslikAcik { get; set; }

        [Column]
        public string RenkHaslikOrta { get; set; }

        [Column]
        public string RenkHaslikKoyu { get; set; }

        [Column]
        public string SurtmeHaslikYasAcik { get; set; }

        [Column]
        public string SurtmeHaslikYasOrta { get; set; }

        [Column]
        public string SurtmeHaslikYasKoyu { get; set; }

        [Column]
        public string SurtmeHaslikKuruAcik { get; set; }

        [Column]
        public string SurtmeHaslikKuruOrta { get; set; }

        [Column]
        public string SurtmeHaslikKuruKoyu { get; set; }

        [Column]
        public double? Fiyat { get; set; }

        [Column]
        public int? DovizId { get; set; }

        [Column]
        public int? EntId { get; set; }

        [Column]
        public bool? Dosemelik { get; set; }

        [Column]
        public bool? Perdelik { get; set; }

        [Column]
        public bool? Elbiselik { get; set; }

        [Column]
        public bool? Likrali { get; set; }

        [Column]
        public double? HavSevki { get; set; }

        [Column]
        public double? ZeminSevki { get; set; }

        [Column]
        public double? AtkiSiklik { get; set; }

        [Column]
        public double? CozguSiklik { get; set; }

        [Column]
        public double? ZeminCozguTel { get; set; }

        [Column]
        public double? KenarCozguTel { get; set; }

        [Column]
        public double? HavCozguTel { get; set; }

        [Column]
        public double? SpigerCozguTel { get; set; }

        [Column]
        public string TarakNo { get; set; }

        [Column]
        public double? TarakEni { get; set; }

        [Column]
        public double? HamEn { get; set; }

        [Column]
        public double? MamulEn { get; set; }

        [Column]
        public string HHavYuksek { get; set; }

        [Column]
        public string MHavYuksek { get; set; }

        [Column]
        public string ZeminOrgu { get; set; }

        [Column]
        public string HavOrgu { get; set; }

        [Column]
        public string HavBaglanti { get; set; }

        [Column]
        public int? Atki1 { get; set; }

        [Column]
        public int? Atki2 { get; set; }

        [Column]
        public int? Atki3 { get; set; }

        [Column]
        public int? Atki4 { get; set; }

        [Column]
        public double? Atki1UstuGr { get; set; }

        [Column]
        public double? Atki2UstuGr { get; set; }

        [Column]
        public double? Atki3UstuGr { get; set; }

        [Column]
        public double? Atki4UstuGr { get; set; }

        [Column]
        public double? Atki1HamGr { get; set; }

        [Column]
        public double? Atki2HamGr { get; set; }

        [Column]
        public double? Atki3HamGr { get; set; }

        [Column]
        public double? Atki4HamGr { get; set; }

        [Column]
        public int? Zemin1 { get; set; }

        [Column]
        public int? Zemin2 { get; set; }

        [Column]
        public int? Zemin3 { get; set; }

        [Column]
        public int? Zemin4 { get; set; }

        [Column]
        public double? Zemin1UstuGr { get; set; }

        [Column]
        public double? Zemin2UstuGr { get; set; }

        [Column]
        public double? Zemin3UstuGr { get; set; }

        [Column]
        public double? Zemin4UstuGr { get; set; }

        [Column]
        public double? Zemin1HamGr { get; set; }

        [Column]
        public double? Zemin2HamGr { get; set; }

        [Column]
        public double? Zemin3HamGr { get; set; }

        [Column]
        public double? Zemin4HamGr { get; set; }

        [Column]
        public int? Hav1 { get; set; }

        [Column]
        public int? Hav2 { get; set; }

        [Column]
        public int? Hav3 { get; set; }

        [Column]
        public int? Hav4 { get; set; }

        [Column]
        public double? Hav1UstuGr { get; set; }

        [Column]
        public double? Hav2UstuGr { get; set; }

        [Column]
        public double? Hav3UstuGr { get; set; }

        [Column]
        public double? Hav4UstuGr { get; set; }

        [Column]
        public double? Hav1HamGr { get; set; }

        [Column]
        public double? Hav2HamGr { get; set; }

        [Column]
        public double? Hav3HamGr { get; set; }

        [Column]
        public double? Hav4HamGr { get; set; }

        [Column]
        public int? Kenar { get; set; }

        [Column]
        public double? KenarUstuGr { get; set; }

        [Column]
        public double? KenarHamGr { get; set; }

        [Column]
        public string Spiger { get; set; }

        [Column]
        public double? SpigerUstuGr { get; set; }

        [Column]
        public double? SpigerHamGr { get; set; }

        [Column]
        public string Kompozisyon { get; set; }

        [Column]
        public double? HamAgirlik { get; set; }

        [Column]
        public double? MamulAgirlik { get; set; }

        [Column]
        public double? HamM2Agirlik { get; set; }

        [Column]
        public double? MamulM2Agirlik { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblYetkiler")]
    public class tblYetkiler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? BolumId { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public int YetkiId { get; set; }

        [Column]
        public bool YetkiVarMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPlanlama")]
    public class tblPlanlama : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int TipId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarBaskilar")]
    public class tblFuarBaskilar : IDisposable
    {

        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string BaskiAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarKombin")]
    public class tblFuarKombin : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string KombinNo { get; set; }

        [Column]
        public string KombinAdi { get; set; }

        [Column]
        public string ResimUrl { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarKombinKumaslar")]
    public class tblFuarKombinKumaslar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public int? BaskiId { get; set; }

        [Column]
        public int? KombinId { get; set; }

        [Column]
        public double? Fiyat { get; set; }

        [Column]
        public int? DovizId { get; set; }

        public List<vAyarlar> Dovizler { get; set; }
        public List<tblFuarBaskilar> Baskilar { get; set; }
        public List<vKumas> Kumaslar { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblCozgu")]
    public class tblCozgu : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int PersId { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public DateTime OlusturmaTarihi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblRenkler")]
    public class tblRenkler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblLeventHareket")]
    public class tblLeventHareket : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public long SetId { get; set; }

        [Column]
        public string LeventNo { get; set; }

        [Column]
        public int CekenPersonelId { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string Cozgu { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public double? LeventEni { get; set; }

        [Column]
        public double? LeventCapi { get; set; }

        [Column]
        public double? TelAdedi { get; set; }

        [Column]
        public double? BobinAdedi { get; set; }

        [Column]
        public double? BobinMetre { get; set; }

        [Column]
        public double? ResteMetre { get; set; }

        [Column]
        public double? BantSayisi { get; set; }

        [Column]
        public int Durum { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblHamKumaslar")]
    public class tblHamKumaslar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int? SiparisId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public int? DokumaciId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public int? KaliteciId { get; set; }

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
        public int? SayimIndisi { get; set; }

        //Gökhan 21.08.2014
        [Column]
        public int? KafesId { get; set; }

        [Column]
        public string KafesDikeyKodu { get; set; }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblHamHatalari")]
    public class tblHamHatalari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public double? Uzunluk { get; set; }

        [Column]
        public double Metresi { get; set; }

        [Column]
        public int? HataId { get; set; }

        public bool HataUstVarMi { get; set; }
        public bool HataAltVarMi { get; set; }

        [Column]
        public int? UstId { get; set; }

        [Column]
        public int? AltId { get; set; }

        public string HataKodu { get; set; }

        public string HataAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblHataTanim")]
    public class tblHataTanim : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int HataYerBagId { get; set; }

        [Column]
        public int HataSebepBagId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public double Puan { get; set; }

        [Column]
        public double PuanAralik1 { get; set; }

        [Column]
        public double PuanAralik2 { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKaliteTanim")]
    public class tblKaliteTanim : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public double AdetAralik1 { get; set; }

        [Column]
        public double AdetAralik2 { get; set; }

        [Column]
        public double PuanAralik1 { get; set; }

        [Column]
        public double PuanAralik2 { get; set; }

        public static List<tblKaliteTanim> KaliteleriGetir()
        {
            return new DBEvents().GetGeneric<tblKaliteTanim>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblProsesGrup")]
    public class tblProsesGrup : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        public static List<tblProsesGrup> ProcessGruplariGetir()
        {
            return new DBEvents().GetGeneric<tblProsesGrup>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblProsesGrup")]
    public class tblProsesGrupAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int GrupId { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int Sira { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPartiler")]
    public class tblPartiler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int PlanlayanId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public string DigerTipNo1 { get; set; }

        [Column]
        public string DigerTipNo2 { get; set; }

        [Column]
        public string DigerTipNo3 { get; set; }

        [Column]
        public double PartiMetre { get; set; }

        [Column]
        public string Tipi { get; set; }

        [Column]
        public string Makina { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public int? OnaylayanId { get; set; }

        [Column]
        public DateTime? BoyamaTarihi { get; set; }

        [Column]
        public bool BoyahaneOnay { get; set; }

        [Column]
        public string BoyahaneNot { get; set; }

        [Column]
        public bool FarkliSiparisKabul { get; set; }

        [Column]
        public bool? ReProcessVarMi { get; set; }

        [Column]
        public bool? ProcessOkumaHizliMi { get; set; }

        [Column]
        public int FinishKodId { get; set; }

        [Column]
        public int? ApreId { get; set; }

        [Column]
        public bool RePartiMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPartiProsesleri")]
    public class tblPartiProsesleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool? ReProcess { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBoyahaneProcess")]
    public class tblBoyahaneProcess : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public DateTime Saat { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool? ReProcess { get; set; }

        [Column]
        public string Aciklama { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasRenk")]
    public class tblKumasRenk : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string BoyarMadde { get; set; }

        public string KodAd
        {
            get
            {
                return Kodu + " - " + Adi;
            }
        }

        public static List<tblKumasRenk> KumasRenkleriGetir(bool sadeceAktifler = true)
        {
            if (sadeceAktifler) return new DBEvents().GetGeneric<tblKumasRenk>(c => c.AktifMi == true).OrderBy(o => o.Kodu).ToList();
            return new DBEvents().GetGeneric<tblKumasRenk>().OrderBy(o => o.Kodu).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasRenkAct")]
    public class tblKumasRenkAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string BoyaKimya { get; set; }

        [Column]
        public string GrOran { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int KimyasalId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKimyasalRecete")]
    public class tblKimyasalRecete : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string ReceteNo { get; set; }

        [Column]
        public string Makina { get; set; }

        [Column]
        public string Program { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public bool StoktanDusulduMu { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKimyasalRecetePartileri")]
    public class tblKimyasalRecetePartileri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int ReceteId { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public double TartilanKg { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKimyasalReceteAct")]
    public class tblKimyasalReceteAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int ReceteId { get; set; }

        [Column]
        public double? Oran { get; set; }

        [Column]
        public int KimyasalId { get; set; }

        [Column]
        public string GrLtOran { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public string Tip { get; set; }

        [Column]
        public double Flote { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSiparisTestleri")]
    public class tblSiparisTestleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public bool EnVarMi { get; set; }

        [Column]
        public string EnKriter { get; set; }

        [Column]
        public string EnSonuc { get; set; }

        [Column]
        public bool GramVarMi { get; set; }

        [Column]
        public string GrMKriter { get; set; }

        [Column]
        public string GrMSonuc { get; set; }

        [Column]
        public string GrM2Kriter { get; set; }

        [Column]
        public string GrM2Sonuc { get; set; }

        [Column]
        public bool SurtmeVarMi { get; set; }

        [Column]
        public string SYasKriter { get; set; }

        [Column]
        public string SYasSonuc { get; set; }

        [Column]
        public string SKuruKriter { get; set; }

        [Column]
        public string SKuruSonuc { get; set; }

        [Column]
        public bool BoyutStabilVarMi { get; set; }

        [Column]
        public string BStabilAtkiKriter { get; set; }

        [Column]
        public string BStabilAtkiSonuc { get; set; }

        [Column]
        public string BStabilCozguKriter { get; set; }

        [Column]
        public string BStabilCozguSonuc { get; set; }

        [Column]
        public bool MartinDaleVarMi { get; set; }

        [Column]
        public string MartinDaleKriter { get; set; }

        [Column]
        public string MartinDaleSonuc { get; set; }

        [Column]
        public bool PillingVarMi { get; set; }

        [Column]
        public string PillingKriter { get; set; }

        [Column]
        public string PillingSonuc { get; set; }

        [Column]
        public bool KopMukVarMi { get; set; }

        [Column]
        public string KopMukAtkiKriter { get; set; }

        [Column]
        public string KopMukAtkiSonuc { get; set; }

        [Column]
        public string KopMukCozguKriter { get; set; }

        [Column]
        public string KopMukCozguSonuc { get; set; }

        [Column]
        public bool YirtMukVarMi { get; set; }

        [Column]
        public string YirtMukAtkiKriter { get; set; }

        [Column]
        public string YirtMukAtkiSonuc { get; set; }

        [Column]
        public string YirtMukCozguKriter { get; set; }

        [Column]
        public string YirtMukCozguSonuc { get; set; }

        [Column]
        public bool DikisKayVarMi { get; set; }

        [Column]
        public string DikisKayAtkiKriter { get; set; }

        [Column]
        public string DikisKayAtkiSonuc { get; set; }

        [Column]
        public string DikisKayCozguKriter { get; set; }

        [Column]
        public string DikisKayCozguSonuc { get; set; }

        [Column]
        public bool KuruTemizVarMi { get; set; }

        [Column]
        public string KuruTemizAtkiKriter { get; set; }

        [Column]
        public string KuruTemizAtkiSonuc { get; set; }

        [Column]
        public string KuruTemizCozguKriter { get; set; }

        [Column]
        public string KuruTemizCozguSonuc { get; set; }

        [Column]
        public bool TerHaslikVarMi { get; set; }

        [Column]
        public string TerHaslikKriter { get; set; }

        [Column]
        public string TerHaslikSonuc { get; set; }

        [Column]
        public bool YikamaHaslikVarMi { get; set; }

        [Column]
        public string YikamaHaslikKriter { get; set; }

        [Column]
        public string YikamaHaslikSonuc { get; set; }

        [Column]
        public bool IsikHaslikVarMi { get; set; }

        [Column]
        public string IsikHaslikKriter { get; set; }

        [Column]
        public string IsikHaslikSonuc { get; set; }

        [Column]
        public bool HavKaybiVarMi { get; set; }

        [Column]
        public string HavKaybiKriter { get; set; }

        [Column]
        public string HavKaybiSonuc { get; set; }

        [Column]
        public bool YanmazlikVarMi { get; set; }

        [Column]
        public string KriterAciklama { get; set; }

        [Column]
        public string SonucAciklama { get; set; }

        [Column]
        public bool TestYapildiMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMamulHatalari")]
    public class tblMamulHatalari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public double Uzunluk { get; set; }

        [Column]
        public double Metresi { get; set; }

        [Column]
        public int HataId { get; set; }

        [Column]
        public string HataKodu { get; set; }

        [Column]
        public int MamulId { get; set; }

        public string HataAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblMamulKumaslar")]
    public class tblMamulKumaslar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public int HataAdet { get; set; }

        [Column]
        public double HataPuan { get; set; }

        [Column]
        public double KaliteAdetDeger { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public double KalitePuanDeger { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int KaliteciId { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SevkiyatNotu { get; set; }

        [Column]
        public string Finish { get; set; }

        [Column]
        public int? HamId { get; set; }

        [Column]
        public int? SevkId { get; set; }

        [Column]
        public int? RezerveSiparisActId { get; set; }

        [Column]
        public string Parca { get; set; }

        [Column]
        public string ParcaMetre { get; set; }

        [Column]
        public int? KutuId { get; set; }

        [Column]
        public double? Kg { get; set; }

        [Column]
        public bool SevkEdilebilir { get; set; }

        [Column]
        public int? RePartiId { get; set; }

        [Column]
        public int AnaMamulId { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public int? IadeSipId { get; set; }

        [Column]
        public int? SayimIndisi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblSevk")]
    public class tblSevk : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string BelgeNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public int SevkEdenId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public bool FarkliSiparisOkut { get; set; }

        [Column]
        public bool LogoAktarildiMi { get; set; }

        [Column]
        public bool TipRenkKontrolDevreDisi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblCozguIsEmri")]
    public class tblCozguIsEmri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string Cozgu { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double DokumaMetre { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public long Islem { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPlanRezerve")]
    public class tblPlanRezerve : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public double RezerveMetre { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblIadeler")]
    public class tblIadeler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? SipId { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string Kalite { get; set; }

        [Column]
        public string IadeSebebi { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int OlusturanId { get; set; }

        [Column]
        public bool SevkEdilebilir { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? SevkId { get; set; }

        [Column]
        public int? KutuId { get; set; }

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

    //    #endregion
    //}

}
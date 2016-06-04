using System;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Linq;
using LKLibrary.DbClasses;

namespace LKLibrary.DbClasses
{
    [Table(Name = "tblNumuneTalepleri")]
    public class tblNumuneTalepleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? NumuneId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public string YeniMusteriAdi { get; set; }

        [Column]
        public int? FuarId { get; set; }

        [Column] 
        public DateTime? TalepTarihi { get; set; }

        [Column]
        public int? TalepEdenKullaniciId { get; set; }

        [Column]
        public bool? GonderildiMi { get; set; }

        [Column]
        public DateTime? GonderimTarihi { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "tblNumuneKumaslarBarkodlu")]
    public class tblNumuneKumaslarBarkodlu : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? MamulId { get; set; }

        [Column]
        public int? BirimId { get; set; }

        [Column] //450-metrelik kumas, 451-kitap, 452-Şelale, 453-Aski --tblayarlar
        public int? TrCode { get; set; }

        [Column]
        public int? FuarId { get; set; }

        [Column]
        public int? EkleyenKullaniciId { get; set; }

        [Column]
        public int? SonDegistirenKullaniciId { get; set; }

        [Column]
        public DateTime? EklenmeTarihi { get; set; }

        [Column]
        public string KafesNo { get; set; }

        [Column]
        public string KafesAltNo { get; set; }

        [Column]
        public string KafesSiraNo { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Finish { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
    }


    [Table(Name = "tblNumuneKumaslar")]
    public class tblNumuneKumaslar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public int? MamulId { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column] //desen, baskı, plise vs.
        public string FasonIslemKodu { get; set; }

        [Column]
        public string Koleksiyon { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int? BirimId { get; set; }

        [Column] //450-metrelik kumas, 451-kitap, 452-Şelale, 453-Aski --tblayarlar
        public int? TrCode { get; set; }

        [Column]
        public int? FuarId { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public int? EkleyenKullaniciId { get; set; }

        [Column]
        public int? SonDegistirenKullaniciId { get; set; }

        [Column]
        public DateTime? EklenmeTarihi { get; set; }

        [Column]
        public string KafesNo { get; set; }

        [Column]
        public string KafesAltNo { get; set; }

        [Column]
        public string KafesSiraNo { get; set; }

        [Column]
        public string Finish { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }

    [Table(Name = "tblFuarlar")]
    public class tblFuarlar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string FuarTarihleri { get; set; }

        [Column]
        public DateTime? FuarBaslangicTarihi { get; set; }

        [Column]
        public int? FuarYili { get; set; }

        [Column]
        public string HallNo { get; set; }

        [Column]
        public string StandNo { get; set; }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }



    [Table(Name = "tblBoyaProgrami")]
    public class tblBoyaProgrami : IDisposable       
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public int? BoyanacakHafta { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public bool? Boyandi { get; set; }

        [Column]
        public bool? Tahsis { get; set; }

        [Column]
        public bool? HamKumas { get; set; }

        [Column]
        public bool? HamFirca { get; set; }

        [Column]
        public bool? Fikse { get; set; }

        [Column]
        public bool? Kasar { get; set; }

        [Column]
        public bool? Boya { get; set; }

        [Column]
        public bool? Recete { get; set; }

        [Column]
        public Nullable<DateTime> BoyaPrograminaAlinmaTarihi { get; set; }

        [Column]
        public bool? BoyaProgaminaAlindiMi { get; set; }

        [Column]
        public int BoyamaSayisi { get; set; }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }



    [Table(Name = "tblAmbarAct")]
    public class tblAmbarAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int AmbarUstId { get; set; }

        [Column]
        public int HamBarkodId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int DepoId { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "tblAmbar")]
    public class tblAmbar : IDisposable
    {        
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public bool LogoAktarildiMi { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


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

        public static List<tblAyarlar> KumasCinsleriniGetir()
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 393);
            }
        }

        public static List<tblAyarlar> KumasGruplariniGetir()
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 406).OrderBy(o=>o.Sira).ToList();
            }
        }

        public static List<tblAyarlar> BoyahaneKumasTipleriGetir()
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 183);
            }
        }

        public static List<tblAyarlar> TezgahVersiyonlari()
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<tblAyarlar>(c => c.BaglantiId == 224);
            }
        }

        public static List<tblAyarlar> AyarAgaciGetir(string ustAyarAdi)
        {
            using (var db = new DBEvents())
            {
                tblAyarlar ustTanim = db.GetGeneric<tblAyarlar>(c => c.Adi == ustAyarAdi).FirstOrDefault();
                List<tblAyarlar> records = new List<tblAyarlar>();
                if (ustTanim != null) records = db.GetGeneric<tblAyarlar>(c => c.BaglantiId == ustTanim.Id);

                return records;
            }
        }

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

    [Table(Name = "tblBaskiDesenUrunAgaci")]
    public class tblBaskiDesenUrunAgaci : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaskiDesenGrupTanimId { get; set; }        

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Varyant { get; set; }

        public tblBaskiDesenUrunAgaci Duzelt(int id)
        {
            return new DBEvents().GetGeneric<tblBaskiDesenUrunAgaci>(c => c.Id == id)[0];
        }       

        public static List<tblAyarlar> GrupAdiGetir()
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.BaglantiId == 389).OrderBy(o=>o.Adi).ToList();
        }

        public static List<vBaskiDesenUrunAgaci> UrunAgaciniGetir()
        {
            return new DBEvents().GetGeneric<vBaskiDesenUrunAgaci>().ToList();
        }

        public static bool Kaydet(ref tblBaskiDesenUrunAgaci item)
        {
            if (item.Id == 0)
            {
                string kodu = item.Kodu;
                if (new DBEvents().GetGeneric<tblBaskiDesenUrunAgaci>(c => c.Kodu == kodu).FirstOrDefault() != null)
                    throw new Exception("Bu kod numarası kullanımda..!");

                return new DBEvents().SaveGeneric<tblBaskiDesenUrunAgaci>(ref item);
            }
            else return new DBEvents().UpdateGeneric<tblBaskiDesenUrunAgaci>(item);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
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

        public static List<tblDurumlar> DurumlariGetir(int ayarId)
        {
            return new DBEvents().GetGeneric<tblDurumlar>(c => c.AyarId == ayarId);
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
        public double? IplikNo { get; set; }

        [Column]
        public string Cins { get; set; }      

        [Column(Name="MinStok")]
        private double? _MinStok;

        [Column]
        public double? BukumIscilikFaktoru { get; set; }

        [Column]
        public double? BukumGenelUretimFaktoru { get; set; }

        [Column]
        public double? Devir { get; set; }

        [Column]
        public double? Tur { get; set; }

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

        [Column]
        public int? IplikGrubId { get; set; }

        public List<tblBukumDagitimAnahtari> ListIplikGrubu { get; set; }

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
        public int? TalepId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public double Fiyat { get; set; }

        [Column]
        public int? TedarikciId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public double Kur { get; set; }

        [Column]
        public int? BolumId { get; set; }

        [Column]
        public double? OnayMiktar { get; set; }

        [Column]
        public string OnayNotu { get; set; }

        [Column]
        public string Detay { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public double? DepoGirisMiktar { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public DateTime? SonKullanmaTarihi { get; set; }

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
        public int? FirmaId { get; set; }

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

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? TalepGrupId { get; set; }

        [Column]
        public string IlkOnay { get; set; }

        [Column]
        public string IkinciOnay { get; set; }

        [Column]
        public string Nakliye { get; set; }

        [Column]
        public int? OdemeLogoId { get; set; }

        [Column]
        public string IrsaliyeNo { get; set; }

        [Column]
        public DateTime? IrsaliyeTarihi { get; set; }

        [Column]
        public string CezaiSart { get; set; }

        [Column]
        public string FaturaNo { get; set; }

        [Column]
        public DateTime? FaturaTarihi { get; set; }

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
            return new DBEvents().GetGeneric<tblFirmalar>(c => (c.Kodu.StartsWith("M") || c.Kodu.StartsWith("YM") || c.Kodu == "T380000") && c.AktifMi == true).OrderBy(o=>o.Adi).ToList();
        }

        public static List<tblFirmalar> FirmalariGetir(int baglantiId = 0)
        {
            var query = PredicateBuilder.True<tblFirmalar>();

            if (baglantiId != 0) query = query.And<tblFirmalar>(c => c.BaglantiId == baglantiId && c.AktifMi == true);

            return new DBEvents().GetGeneric<tblFirmalar>(query).OrderBy(c => c.Adi).ToList();
        }

        public static List<tblFirmalar> TedarikcileriGetir()
        {
            return new DBEvents().GetGeneric<tblFirmalar>(c => (c.Kodu.StartsWith("T") || c.Kodu.StartsWith("YT") || c.Kodu == "T380000") && c.AktifMi == true).OrderBy(o => o.Adi).ToList();
        }

        public static List<tblFirmalar> TedarikcileriGetirWithBaglantiId()
        {
            return new DBEvents().GetGeneric<tblFirmalar>(c => (c.BaglantiId == 1 || c.BaglantiId == 2) && c.AktifMi == true).OrderBy(o => o.Adi).ToList();
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

        [Column]
        public DateTime Tarih { get; set; }

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
        [Column(Name = "Id", IsPrimaryKey=true, IsDbGenerated=true)]
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

        [Column(Name="GunlukDokuma")]
        public double? DevirSayisi { get; set; }

        public string KodAd { get { return this.Kodu + " - " + this.Adi; } }

        [Column]
        public bool? AktifMi { get; set; }

        [Column]
        public int? GunlukJarz { get; set; }
        [Column]
        public double? MaksDevir { get; set; }

        public List<tblMakinalar> Kategoriler { get; set; }

        [Column]
        public int? TezgahVersiyonId { get; set; }

        public List<tblAyarlar> TezgahVersiyonlari { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public double DogalGaz { get; set; }

        [Column]
        public double Elektirik { get; set; }

        [Column]
        public double AtikSu { get; set; }

        [Column]
        public double Genel { get; set; }        

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

        [Column]
        public string ProformaNo { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }

        [Column]
        public string KarZarar { get; set; }

        [Column]
        public int? Yuzde { get; set; }

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

        [Column]
        public bool? DokumaTerminiGeciktiMi { get; set; }

        [Column]
        public DateTime? DokumaTerminiGecikmeTarihi { get; set; }

        [Column]
        public bool? BoyamaTerminiGeciktiMi { get; set; }

        [Column]
        public DateTime? BoyamaTerminiGecikmeTarihi { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }

        [Column]
        public int FinishId { get; set; }

        [Column]
        public string KoleksiyonAdi { get; set; }



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

        [Column]
        public bool FasonMu { get; set; }

        [Column]
        public bool Boya { get; set; }

        [Column]
        public bool Kimyasal { get; set; }

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
        public double? MamulAgirlikUst { get; set; }

        [Column]
        public double? HamM2Agirlik { get; set; }

        [Column]
        public double? MamulM2Agirlik { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public int? KumasCinsi { get; set; }

        [Column]
        public string TaharNo { get; set; }

        [Column]
        public double? MaySiklik { get; set; }

        [Column]
        public double? Zemin1CozguTel { get; set; }

        [Column]
        public double? Zemin2CozguTel { get; set; }

        [Column]
        public double? TostHavBoyu { get; set; }

        [Column]
        public double? TarakAdedi { get; set; }

        [Column]
        public string Zemin1TarakTahar { get; set; }

        [Column]
        public string Zemin2TarakTahar { get; set; }

        [Column]
        public string Zemin5TarakTahar { get; set; }

        [Column]
        public string Zemin6TarakTahar { get; set; }

        [Column]
        public string Hav3TarakTahar { get; set; }

        [Column]
        public string Hav4TarakTahar { get; set; }

        [Column]
        public double? ZeminAltOrmeAgirlik { get; set; }

        [Column]
        public double? ZeminUstOrmeAgirlik { get; set; }

        [Column]
        public double? HavOrmeAgirlik { get; set; }

        [Column]
        public double? ZeminAltRack { get; set; }

        [Column]
        public double? ZeminUstRack { get; set; }

        [Column]
        public double? HavRack { get; set; }

        [Column]
        public double? ZeminAltTostAgirlik { get; set; }

        [Column]
        public double? ZeminUstTostAgirlik { get; set; }

        [Column]
        public double? HavTostAgirlik { get; set; }

        [Column]
        public double? ToplamTostAgirlik { get; set; }

        [Column]
        public int? KumasGrup { get; set; }

        public bool MusteriSatisHakkiVarMi(int musteriId)
        {
            DBEvents db = new DBEvents();
            List<tblKumasSatisHakki> satisHaklari = db.GetGeneric<tblKumasSatisHakki>(c => c.TipId == this.Id);
            if (satisHaklari == null || satisHaklari.Count == 0) return true;

            if (satisHaklari.Find(f => f.MusteriId == musteriId) == null) return false;
            else return true;
        }

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
        public int SiparisId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int? AtkiOtelemesi { get; set; }

        [Column]
        public string SiparisNo  { get; set; }

        [Column]
        public double? EksikDokuma { get; set; }

        [Column]
        public bool? OtelemeKontrol { get; set; }

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

        [Column]
        public string ImgData { get; set; }

        [Column]
        public string ImgThumbData { get; set; }        

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
        public int FuarKumasId { get; set; }

        [Column]
        public int KombinId { get; set; }

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
        public int SiparisId { get; set; }

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

        [Column]
        public DateTime? CozguTarihi { get; set; }

        [Column]
        public DateTime? DugumTarihi { get; set; }
        
        [Column]
        public DateTime? TamamlanmaTarihi { get; set; }

        [Column]
        public bool IadeMi { get; set; }

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

        //Gökhan 21.08.2014
        [Column]
        public int? KafesId { get; set; }

        [Column]
        public int? PartiIdPlanlanan { get; set; }

        [Column]
        public int? DepoId { get; set; }

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

        public static bool FinishKaydet(ref tblProsesGrup finish)
        {
            if (finish.Id == 0)
            {                                
                return new DBEvents().SaveGeneric<tblProsesGrup>(ref finish);
            }
            else return new DBEvents().UpdateGeneric<tblProsesGrup>(finish);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblProsesGrupAct")]
    public class tblProsesGrupAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int GrupId{ get; set; }

        [Column]
        public int ProcessId{ get; set; }

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

        [Column]
        public bool PaketlendiMi { get; set; }

        [Column]
        public bool BoyandiMi { get; set; }

        [Column]
        public bool? BoyaProgIptal { get; set; }

        [Column]
        public string BoyaProgIptalNedeni { get; set; }

        [Column]
        public int? MakinaId { get; set; }

        [Column]
        public bool PartilendiMi { get; set; }

        [Column]
        public DateTime? PartilendiTarihi { get; set; }

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

        [Column]
        public DateTime? CikisTarih { get; set; }

        [Column]
        public DateTime? CikisSaat { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public int? ArizaId { get; set; }

        [Column]
        public int? MakinaId { get; set; }

        [Column]
        public bool? Durdu { get; set; }

        [Column]
        public bool? Silindi { get; set; }

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

        [Column]
        public string AOK { get; set; }

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

        //Gökhan 16.05.2014
        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public int? OnayBir { get; set; }

        [Column]
        public int? OnayIki { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }

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

        [Column]
        public int? MakinaId { get; set; }//Eski makinalar bos ondan yaptım

        [Column]
        public bool TopluRecete { get; set; }  //Topluca kasar, yada apre işlemleri yapmak için kullanılacak. True ise Toplu reçetedir.

        [Column]
        public bool NuanslariGuncelledim { get; set; } // Boyama sonrası nüans farklılıkları olduğunda bunu sisteme aktarıldığını takip etmek için kullanılan bir işaret.
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

        //Gökhan 16.05.2014
        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public int? OnayBir { get; set; }

        [Column]
        public int? OnayIki { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 16.05.2014
    [Table(Name = "tblKimyasalReceteActLog")]
    public class tblKimyasalReceteActLog : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SilinenId { get; set; }

        [Column]
        public int? ReceteId { get; set; }

        [Column]
        public double? OranE { get; set; }

        [Column]
        public double? OranY { get; set; }

        [Column]
        public int? KimyasalIdE { get; set; }

        [Column]
        public int? KimyasalIdY { get; set; }
        
        [Column]
        public string GrLtOranE { get; set; }

        [Column]
        public string GrLtOranY { get; set; }

        [Column]
        public double? MiktarE { get; set; }

        [Column]
        public double? MiktarY { get; set; }

        [Column]
        public string TipE { get; set; }

        [Column]
        public string TipY { get; set; }

        [Column]
        public double? FloteE { get; set; }

        [Column]
        public double? FloteY { get; set; }

        [Column]
        public int? PersonelIdE { get; set; }

        [Column]
        public int? PersonelIdY { get; set; }

        [Column]
        public int? Turu { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public DateTime Saat { get; set; }

        [Column]
        public int? OnayBir { get; set; }

        [Column]
        public int? OnayIki { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }

        [Column]
        public string RAciklamaE { get; set; }

        [Column]
        public string RAciklamaY { get; set; }

        [Column]
        public string RBoyaKimyaE { get; set; }

        [Column]
        public string RBoyaKimyaY { get; set; }

        [Column]
        public int? logTuru { get; set; }

        [Column]
        public int? RenkId { get; set; }

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
        public string DesenNo { get; set; }

        [Column]
        public string IadeAciklama { get; set; }

        [Column]
        public string DyeBatchNo { get; set; }

        [Column]
        public int? SevkSiparisActId { get; set; }

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

    [Table(Name = "tblFinish")]
    public class tblFinish : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        public static List<vTipBazliFinishler> FinishKartlariGetir(string tipNo)
        {
            return new DBEvents().GetGeneric<vTipBazliFinishler>(c=>c.TipNo.Contains(tipNo)).OrderBy(o => o.FinishAdi).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTezgahArizaGrupAct")]
    public class tblTezgahArizaGrupAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int GrupId { get; set; }

        public string KodAd { get { return this.Kodu + " - " + this.Adi; } }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTezgahArizalari")]
    public class tblTezgahArizalari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public int ArizaId { get; set; }

        [Column]
        public DateTime? BaslangicTarihi { get; set; }

        [Column]
        public DateTime? BitisTarihi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Postasi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblTezgahAtkiGiris")]
    public class tblTezgahAtkiGiris : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int AtkiBaslangic { get; set; }

        [Column]
        public int AtkiBitis { get; set; }

        [Column]
        public string Postasi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public bool PlanOteledi { get; set; }
        [Column]
        public int? DokumaciId { get; set; }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarKumas")]
    public class tblFuarKumas : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int? KategoriId { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string BaskiNo { get; set; }

        [Column]
        public string NakisNo { get; set; }

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
        public bool? Dosemelik { get; set; }

        [Column]
        public bool? Perdelik { get; set; }

        [Column]
        public bool? Elbiselik { get; set; }

        [Column]
        public bool? Likrali { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public double? MamulTL { get; set; }

        [Column]
        public double? MamulDolar { get; set; }

        [Column]
        public double? MamulEuro { get; set; }

        [Column]
        public double? DesenTL { get; set; }

        [Column]
        public double? DesenDolar { get; set; }

        [Column]
        public double? DesenEuro { get; set; }

        [Column]
        public double? BaskiTL { get; set; }

        [Column]
        public double? BaskiDolar { get; set; }

        [Column]
        public double? BaskiEuro { get; set; }

        [Column]
        public double? NakisTL { get; set; }

        [Column]
        public double? NakisDolar { get; set; }

        [Column]
        public double? NakisEuro { get; set; }

        [Column]
        public double? HamTL { get; set; }

        [Column]
        public double? HamDolar { get; set; }

        [Column]
        public double? HamEuro { get; set; }

        [Column]
        public DateTime? KaydetmeTarihi { get; set; }

        [Column]
        public string ImgData { get; set; }

        [Column]
        public string ImgThumbData { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarProses")]
    public class tblFuarProses : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public double? Fiyati { get; set; }

        public static List<tblFuarProses> ProsesleriGetir()
        {
            return new DBEvents().GetGeneric<tblFuarProses>();
        }

        public bool Kaydet()
        {
            return new DBEvents().SaveGeneric<tblFuarProses>(this);
        }

        public bool Sil()
        {
            tblFuarKumasProsesleri knt = new DBEvents().GetGeneric<tblFuarKumasProsesleri>(c => c.FuarProcessId == this.Id).FirstOrDefault();
            if (knt != null) return false;
            return new DBEvents().DeleteGeneric<tblFuarProses>(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBukumDagitimAnahtari")]
    public class tblBukumDagitimAnahtari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public double Iscilik_Katsayisi { get; set; }

        [Column]
        public double Genel_Uretim_Katsayisi { get; set; }

        public static List<tblBukumDagitimAnahtari> BukumDagitimAnahtariGetir()
        {
            return new DBEvents().GetGeneric<tblBukumDagitimAnahtari>();
        }
      
        public bool Kaydet(List<tblBukumDagitimAnahtari> ListAnahtar)
        {
            if (ListAnahtar == null) return false;
            List<tblBukumDagitimAnahtari> savelist = ListAnahtar.FindAll(c => c.Id == 0);
            List<tblBukumDagitimAnahtari> updatelist = ListAnahtar.FindAll(c => c.Id > 0);

            bool sonuc = true;
            if (savelist.Count > 0) if (new DBEvents().SaveGeneric<tblBukumDagitimAnahtari>(savelist) == false) sonuc = false;
            if (updatelist.Count > 0) if (new DBEvents().UpdateGeneric<tblBukumDagitimAnahtari>(updatelist) == false) sonuc = false;

            return sonuc;            
        }
        public bool Sil()
        {
            return new DBEvents().DeleteGeneric<tblBukumDagitimAnahtari>(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarKumasKategori")]
    public class tblFuarKumasKategori : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        public static List<tblFuarKumasKategori> KategorileriGetir()
        {
            return new DBEvents().GetGeneric<tblFuarKumasKategori>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblFuarKumasProsesleri")]
    public class tblFuarKumasProsesleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int FuarKumasId { get; set; }

        [Column]
        public int FuarProcessId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblPackAliases")]
    public class tblPackAliases : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SevkId { get; set; }

        [Column]
        public int MamulId { get; set; }

        [Column]
        public string TipAlias { get; set; }

        [Column]
        public string RenkAlias { get; set; }

        [Column]
        public string RollAlias { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasResimleri")]
    public class tblKumasResimleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string Resim { get; set; }

        public bool Ekle()
        {
            tblKumasResimleri ekle = this;
            if (new DBEvents().SaveGeneric<tblKumasResimleri>(ref ekle))
            {
                this.Id = ekle.Id;
                return true;
            }
            else return false;
        }

        public static bool Sil(int id)
        {
            List<string> snc = new DBEvents().GetGenericWithSQLQuery<string>("delete from tblKumasResimleri where Id = {0}", new object[] { id });
            if (snc == null) return false;
            else return true;
        }

        public static List<tblKumasResimleri> TipResimleriGetir(int tipId)
        {
            return new DBEvents().GetGeneric<tblKumasResimleri>(c => c.TipId == tipId);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasSatisHakki")]
    public class tblKumasSatisHakki : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasFinishHakki")]
    public class tblKumasFinishHakki : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int ProcessGrupId { get; set; }       

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumasBelgeleri")]
    public class tblKumasBelgeleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string DosyaYolu { get; set; }

        [Column]
        public string Adi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBoyahaneUrunAgaci")]
    public class tblBoyahaneUrunAgaci : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int ProsesGrupId { get; set; }

        [Column]
        public int KumasTipiAyarId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblBoyahaneUrunAgaciAct")]
    public class tblBoyahaneUrunAgaciAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int UrunAgaciId { get; set; }
        
        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int IslemSayisi { get; set; }

        [Column]
        public string MakinaDevri { get; set; }

        [Column]
        public string MakinaSicaklik { get; set; }

        [Column]
        public string GirisEni { get; set; }

        [Column]
        public string CikisEni { get; set; }

        [Column]
        public string MakinadaKalmaSuresi { get; set; }

        [Column]
        public string ApreKodu { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public short Sira { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKullanicilar")]
    public class tblKullanicilar : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string Sifre { get; set; }

        [Column]
        public int PersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 16.05.2014
    [Table(Name = "tblFasonKumasMaliyet")]
    public class tblFasonKumasMaliyet : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int PartiProcessActId { get; set; }

        [Column]
        public double? FaturaTutari { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int? ProcessId { get; set; }

        [Column]
        public int? PartiId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 26.06.2014
    [Table(Name = "tblTmpSiparisMaliyet")]
    public class tblTmpSiparisMaliyet : IDisposable
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
        public double DogalGaz { get; set; }

        [Column]
        public double Elektrik { get; set; }

        [Column]
        public double AtikSu { get; set; }

        [Column]
        public double Amortisman { get; set; }
        
        [Column]
        public double IsletmeMalzele { get; set; }

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
        public DateTime Tarih { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double? FasonProcess { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyali { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }
        
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 03.07.2014
    [Table(Name = "tblFasonIplikMaliyet")]
    public class tblFasonIplikMaliyet : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int IplikId { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public double FaturaTutari { get; set; }

        [Column]
        public double IplikKg { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public DateTime FaturaTarihi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 07.07.2014
    [Table(Name = "tblMaliyetBoyahaneDetay")]
    public class tblMaliyetBoyahaneDetay : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public string Cinsi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public double? DirekIscilik { get; set; }

        [Column]
        public double? DogalGaz { get; set; }

        [Column]
        public double? AtikSu { get; set; }

        [Column]
        public double? Elektrik { get; set; }

        [Column]
        public double? Amortisman { get; set; }

        [Column]
        public double? IsletmeMalzeme { get; set; }

        [Column]
        public double? NakliyeBakimGideri { get; set; }

        [Column]
        public double? Kimyasal { get; set; }

        [Column]
        public double? EnDirekIscilik { get; set; }

        [Column]
        public double? Ambalaj { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public int? ProcessCalismaDk { get; set; }

        [Column]
        public int? MakinaToplamCalismaDk { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public string Process { get; set; }

        [Column]
        public int MakinaId { get; set; }

        [Column]
        public string Makina { get; set; }

        [Column]
        public int? Sira { get; set; }

        [Column]
        public bool? FasonMu { get; set; }
        
        [Column]
        public string RenkNo { get; set; }

        [Column]
        public bool? ReProcess { get; set; }

        [Column]
        public double? FasonMaliyet { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public double? Boyasal { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyali { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 07.07.2014
    [Table(Name = "tblMaliyetIplikDetay")]
    public class tblMaliyetIplikDetay : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public double? HavZeminSevki { get; set; }

        [Column]
        public int IplikId { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public double? UstuBuluGr { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public double? LogoFiyati { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        [Column]
        public double? DirekIscilik { get; set; }

        [Column]
        public double? Dogalgaz { get; set; }

        [Column]
        public double? Elektrik { get; set; }

        [Column]
        public double? AtikSu { get; set; }

        [Column]
        public double? Amortisman { get; set; }

        [Column]
        public double? IsletmeMalzeme { get; set; }

        [Column]
        public double? NakliyeBakim { get; set; }

        [Column]
        public double? Kimyasal { get; set; }

        [Column]
        public double? EnDirekIscilik { get; set; }

        [Column]
        public double? Ambalaj { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public string Cinsi { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double? FasonProcess { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyali { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 07.07.2014
    [Table(Name = "tblMaliyetBukumDetay")]
    public class tblMaliyetBukumDetay : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public int IplikId { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public string KullanimAlani { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? DirekIscilik { get; set; }

        [Column]
        public double? Dogalgaz { get; set; }

        [Column]
        public double? Elektrik { get; set; }

        [Column]
        public double? AtikSu { get; set; }

        [Column]
        public double? Amortisman { get; set; }

        [Column]
        public double? IsletmeMalzeme { get; set; }

        [Column]
        public double? NakliyeBakim { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyama { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }

        [Column]
        public double? BukumIscilikFaktoru { get; set; }

        [Column]
        public double? BukumGenelUretimFaktoru { get; set; }

        [Column]
        public double? Katsayi { get; set; }

        [Column]
        public double? ToplamKatsayi { get; set; }

        [Column]
        public double? FiiliUretim { get; set; }

        [Column]
        public double? ToplamUretim { get; set; }

        [Column]
        public string Levent { get; set; }

        [Column]
        public double? HavSevki { get; set; }

        [Column]
        public double? TelAdedi { get; set; }

        [Column]
        public double? CozguToplamKg { get; set; }
        
        [Column]
        public string LotNo { get; set; }
        
        [Column]
        public Int64 SetId { get; set; }
        
        [Column]
        public double? ZeminSevki { get; set; }
        
        [Column]
        public double? IplikNo { get; set; }
        
        [Column]
        public DateTime? SonUretimTarihi { get; set; }
        
        [Column]
        public string Cinsi { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double? FasonProcess { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 07.07.2014
    [Table(Name = "tblMaliyetCozguDetay")]
    public class tblMaliyetCozguDetay : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }

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
        public double? TopMetre { get; set; }

        [Column]
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public double? DirekIscilik { get; set; }

        [Column]
        public double? DogalGaz { get; set; }

        [Column]
        public double? AtikSu { get; set; }

        [Column]
        public double? Elektrik { get; set; }

        [Column]
        public double? Amortisman { get; set; }

        [Column]
        public double? IsletmeMalzeme { get; set; }

        [Column]
        public double? NakliyeBakimGideri { get; set; }

        [Column]
        public double? Kimyasal { get; set; }

        [Column]
        public double? EnDirekIscilik { get; set; }

        [Column]
        public double? Ambalaj { get; set; }

        [Column]
        public double? SatirToplami { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public double? HavZeminSevki { get; set; }

        [Column]
        public double? LogoFiyat { get; set; }

        [Column]
        public double? DaireCozguToplamMetre { get; set; }

        [Column]
        public double? MtIscilik { get; set; }
        
        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double? FasonProcess { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyali { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 07.07.2014
    [Table(Name = "tblMaliyetDokumaDetay")]
    public class tblMaliyetDokumaDetay : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SiparisId { get; set; }
        
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
        public double? TopMetre { get; set; }
        
        [Column]
        public int Ay { get; set; }
        
        [Column]
        public int Yil { get; set; }
        
        [Column]
        public double? DirekIscilik { get; set; }
        
        [Column]
        public double? DogalGaz { get; set; }
        
        [Column]
        public double? AtikSu { get; set; }
        
        [Column]
        public double? Elektrik { get; set; }
        
        [Column]
        public double? Amortisman { get; set; }
        
        [Column]
        public double? IsletmeMalzeme { get; set; }
        
        [Column]
        public double? NakliyeBakimGideri { get; set; }
        
        [Column]
        public double? Kimyasal { get; set; }
        
        [Column]
        public double? EnDirekIscilik { get; set; }
        
        [Column]
        public double? Ambalaj { get; set; }
        
        [Column]
        public double? SatirToplami { get; set; }
        
        [Column]
        public int? TezgahId { get; set; }
        
        [Column]
        public double? HavZeminSevki { get; set; }
        
        [Column]
        public double? Katsayi { get; set; }

        [Column]
        public double? ToplamKatsayi { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public double? FasonProcess { get; set; }

        [Column]
        public double? FasonBukum { get; set; }

        [Column]
        public double? FasonBoyali { get; set; }

        [Column]
        public double? FasonRenkDuzeltme { get; set; }

        [Column]
        public double? FasonFikse { get; set; }

        [Column]
        public double? KullanilanIplikKg { get; set; }

        [Column]
        public double? KullanilanIplikFiyati { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    
}

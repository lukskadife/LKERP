using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using LKLibrary.Classes;
using System.IO;

namespace LKLibrary.DbClasses
{

    [Table(Name = "vBoyaProgramiSukru")]
    public class vBoyaProgramiSukru : IDisposable
    {
        [Column]
        public Nullable<int> PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public Nullable<int> SiparisId { get; set; }

        [Column]
        public Nullable<int> SiparisActId { get; set; }

        [Column]
        public Nullable<int> TipId { get; set; }

        [Column]
        public Nullable<DateTime> PartilemeTarihi { get; set; }

        [Column]
        public Nullable<int> MusteriId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string DigerTipNo1 { get; set; }

        [Column]
        public string DigerTipNo2 { get; set; }

        [Column]
        public double? PartiMetre { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string AOK { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public string PartiAciklama { get; set; }

        [Column]
        public string BoyahaneNot { get; set; }

        [Column]
        public double? AcilmisMetre { get; set; }

        [Column]
        public bool? ReProcessVarMi { get; set; }

        [Column]
        public Nullable<DateTime> TerminTarihi { get; set; }

        [Column]
        public Nullable<int> ApreId { get; set; }

        [Column]
        public bool? RePartiMi { get; set; }

        [Column]
        public bool? PaketlendiMi { get; set; }

        [Column]
        public bool? BoyandiMi { get; set; }

        [Column]
        public bool? BoyaProgIptal { get; set; }

        [Column]
        public string BoyaProgIptalNedeni { get; set; }

        [Column]
        public Nullable<int> MakinaId { get; set; }

        [Column]
        public string Makina { get; set; }

        [Column]
        public bool? PartilendiMi { get; set; }

        [Column]
        public double? SevkMetre { get; set; }

        [Column]
        public double? SiparisMetre { get; set; }

        [Column]
        public double? MamuldenCikanBrutMetre { get; set; }

        [Column]
        public Nullable<int> TerminYili { get; set; }

        [Column]
        public Nullable<int> TerminHaftasi { get; set; }

        [Column]
        public bool? BoyaProgaminaAlindiMi { get; set; }

        [Column]
        public Nullable<int> BoyanacakHafta { get; set; }

        [Column]
        public string BoyaProgAciklama { get; set; }

        [Column]
        public bool? BoyaProgramiBoyandiMi { get; set; }

        [Column]
        public bool? Tahsis { get; set; }

        [Column]
        public bool? HamKumasHazirMi { get; set; }

        [Column]
        public bool? HamFircadanGectimi { get; set; }

        [Column]
        public bool? FiksedenGectimi { get; set; }

        [Column]
        public bool? KasardanGectimi { get; set; }

        [Column]
        public bool? BoyadanGectimi { get; set; }

        [Column]
        public bool? ReceteHazirmi { get; set; }

        [Column]
        public Nullable<DateTime> TahsisTarihi { get; set; }

        [Column]
        public Nullable<DateTime> HamKumasTarihi { get; set; }

        [Column]
        public Nullable<DateTime> HamFircaTarihi { get; set; }

        [Column]
        public Nullable<DateTime> FikseTarihi { get; set; }

        [Column]
        public Nullable<DateTime> KasarTarihi { get; set; }

        [Column]
        public Nullable<DateTime> BoyanmaTarihi { get; set; }

        [Column]
        public Nullable<DateTime> ReceteTarihi { get; set; }

        [Column]
        public Nullable<DateTime> BoyaPrograminaAlinmaTarihi { get; set; }

        [Column]
        public Nullable<int> BoyamaSayisi { get; set; }

        //public bool DahaOnceBoyaPrograminaAlindiMi()
        //{
        //    if (this.BoyaProgaminaAlindiMi.HasValue == true && this.BoyaProgaminaAlindiMi.Value) return true;

        //    DBEvents db = new DBEvents();
        //    tblBoyaProgrami boyaProgrami = db.GetGeneric<tblBoyaProgrami>(x => x.PartiId == this.PartiId).FirstOrDefault();
        //    if (boyaProgrami != null) return true;
        //    else return false;
        //}
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }



    [Table(Name = "vAmbarAct")]
    public class vAmbarAct : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int AmbarUstId { get; set; }

        [Column]
        public int HamBarkodId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double Kg { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public int DepoId { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public int BirimFiyat { get; set; }

        [Column(Name = "DovizKuruTL")]
        public int DovizKuruTL { get; set; }

        [Column(Name = "DovizCinsiLogo")]
        public int DovizCinsiLogo { get; set; }

        [Column(Name = "TLFiyati")]
        public int TLFiyati { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "vAmbarUst")]
    public class vAmbarUst : IDisposable
    {
        [Column(Name = "Adi")]
        public string Adi { get; set; }

        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column(Name = "Tarih")]
        public DateTime Tarih { get; set; }

        [Column(Name = "LogoAktarildiMi")]
        public bool LogoAktarildiMi { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vFasonSepeti")]
    public class vFasonSepeti : IDisposable
    {
        [Column(Name = "ProcessAdi")]
        public string ProcessAdi { get; set; }

        [Column(Name = "PartiNo")]
        public string PartiNo { get; set; }

        [Column(Name = "SozlesmeNo")]
        public string SozlesmeNo { get; set; }

        [Column(Name = "Musteri")]
        public string Musteri { get; set; }

        [Column(Name = "TipNo")]
        public string TipNo { get; set; }

        [Column(Name = "RenkNo")]
        public string RenkNo { get; set; }

        [Column(Name = "PartiMetre")]
        public double? PartiMetre { get; set; }

        [Column(Name = "AcilmisHamKumas")]
        public double? AcilmisHamKumas { get; set; }

        [Column(Name = "FasonaSevkMetre")]
        public double? FasonaSevkMetre { get; set; }

        [Column(Name = "FasondanGelenMetre")]
        public double? FasondanGelenMetre { get; set; }

        [Column(Name = "TipVaryant")]
        public string TipVaryant { get; set; }

        [Column(Name = "RenkVaryant")]
        public string RenkVaryant { get; set; }

        [Column(Name = "Fasoncu")]
        public string Fasoncu { get; set; }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "vTipBazliFinishler")]
    public class vTipBazliFinishler : IDisposable
    {
        [Column(Name = "FinishId")]
        public int FinishId { get; set; }

        [Column(Name = "FinishAdi")]
        public string FinishAdi { get; set; }

        [Column(Name = "TipNo")]
        public string TipNo { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vBaskiDesenUrunAgaci")]
    public class vBaskiDesenUrunAgaci : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "Varyant")]
        public String Varyant { get; set; }

        [Column(Name = "GrupAdi")]
        public String GrupAdi { get; set; }

        [Column(Name = "BaskiDesenGrupTanimId")]
        public int BaskiDesenGrupTanimId { get; set; }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "vKimyasalReceteMuadiller")]
    public class vKimyasalReceteMuadiller : IDisposable
    {
        [Column]
        public string ReceteNo { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Makine { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string Musteri { get; set; }

        [Column]
        public int ReceteId { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public bool NuanslariGuncelledim { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class vBoyahaneProcessTotal : IDisposable
    {                       
        public string Process { get; set; }

        public double CalismaDk { get; set; }

        public double SatirToplami { get; set; }

        public int Sira { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vKimyasalSarfFisi")]
    public class vKimyasalSarfFisi : IDisposable
    {
        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimAdi")]
        public string BirimAdi { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vIplikCiftKatGiris")]
    public class vIplikCiftKatGiris : IDisposable
    {
        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vIplikCozguSarf")]
    public class vIplikCozguSarf : IDisposable
    {
        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "vIplikAtkiSarf")]
    public class vIplikAtkiSarf : IDisposable
    {
        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vIplikTekKatSarf")]
    public class vIplikTekKatSarf : IDisposable
    {       
        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vLogoIplikGirisTest")]
    public class vLogoIplikGirisTest : IDisposable
    {
        [Column(Name = "MalzemeId")]
        public int MalzemeId { get; set; }

        [Column(Name = "NetKg")]
        public double NetKg { get; set; }

        [Column(Name = "MalzemeKodu")]
        public string MalzemeKodu { get; set; }

        [Column(Name = "MusteriKodu")]
        public string MusteriKodu { get; set; }

        [Column(Name = "IrsaliyeNo")]
        public string IrsaliyeNo { get; set; }

        [Column(Name = "GirisTanim")]
        public string GirisTanim { get; set; }

        [Column(Name = "LotNo")]
        public string LotNo { get; set; }

        [Column(Name = "Tarih")]
        public DateTime Tarih { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        [Column(Name = "TLFiyati")]
        public double TLFiyati { get; set; }

        [Column(Name = "SatinAlmaTalepId")]
        public int SatinAlmaTalepId { get; set; }
        

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vHamKumasSevkGrup")]
    public class vHamKumasSevkGrup : IDisposable
    {
        [Column(Name = "PartiId")]
        public int PartiId { get; set; }

        [Column(Name = "NetMetre")]
        public double NetMetre { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public int BirimFiyat { get; set; }

        [Column(Name = "DovizKuruTL")]
        public int DovizKuruTL { get; set; }

        [Column(Name = "DovizCinsiLogo")]
        public int DovizCinsiLogo { get; set; }

        [Column(Name = "TLFiyati")]
        public int TLFiyati { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vKimyasalSevkGrup")]
    public class vKimyasalSevkGrup : IDisposable
    {
        [Column(Name = "ReceteId")]
        public int ReceteId { get; set; }

        [Column(Name = "Miktar")]
        public double Miktar { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public int BirimFiyat { get; set; }

        [Column(Name = "DovizKuruTL")]
        public int DovizKuruTL { get; set; }

        [Column(Name = "DovizCinsiLogo")]
        public int DovizCinsiLogo { get; set; }

        [Column(Name = "TLFiyati")]
        public int TLFiyati { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Table(Name = "vSevkGrup")]
    public class vSevkGrup : IDisposable
    {
        [Column(Name = "SevkId")]
        public int SevkId { get; set; }

        [Column(Name = "NetMetre")]
        public double NetMetre { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        [Column(Name = "BirimFiyat")]
        public double BirimFiyat { get; set; }

        [Column(Name = "DovizKuruTL")]
        public double DovizKuruTL { get; set; }

        [Column(Name = "DovizCinsiLogo")]
        public int DovizCinsiLogo { get; set; }

        [Column(Name = "TLFiyati")]
        public double TLFiyati { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vOkutulanProcess")]
    public class vOkutulanProcess : IDisposable
    {
        [Column(Name = "Sira")]
        public int Sira { get; set; }

        [Column(Name = "PartiId")]
        public int PartiId { get; set; }

        [Column(Name = "Kodu")]
        public string Kodu { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    [Table(Name = "vDurumCount")]
    public class  vDurumCount : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column(Name = "Adi")]
        public string Adi { get; set; } 

        [Column(Name = "DurumCount")]
        public int DurumCount { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vTalepMalzemeler")]
    public class vTalepMalzemeler : IDisposable
    {
        [Column(Name = "TalepId")]
        public int TalepId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public string BirimAdi { get; set; }

        [Column]
        public string MalzemeKodu { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public int DurumId { get; set; }

        [Column]
        public int TalepEdenId { get; set; }

        [Column]
        public string TalepEdenKodu { get; set; }

        [Column]
        public string TalepEdenAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Detay { get; set; }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public string FabrikaBolumAdi { get; set; }

        public bool HepsiKarsilandiMi { get; set; }

        [Column]
        public double? KarsilananMiktar { get; set; }

        [Column]
        public int? KarsilananBirimId { get; set; }

        [Column]
        public string KarsilananBirim { get; set; }

        [Column]
        public string RenkAdi { get; set; }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMalzemeOrtFiyatlar")]
    public class vMalzemeOrtFiyatlar : IDisposable
    {
        [Column]
        public int TedarikciId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string TedarikciAdi { get; set; }

        [Column]
        public double OrtFiyat { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vTalepKarsilama")]
    public class vTalepKarsilama : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string No { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int? TedarikciId { get; set; }

        [Column]
        public string TedarikciKodu { get; set; }

        [Column]
        public string TedarikciAdi { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string PersonelKodu { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

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
        public string Grubu { get; set; }

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

        public tblTalepKarsilama ViewToTable()
        {
            return new tblTalepKarsilama()
            {
                DurumId = this.DurumId,
                FirmaId = this.TedarikciId,
                Id = this.Id,
                No = this.No,
                PersonelId = this.PersonelId,
                Tarih = this.Tarih,
                OdemeSekli = this.OdemeSekli,
                TerminTarihi = this.TerminTarihi,
                Durum = this.Durum,
                TalepGrupId = this.TalepGrupId,
                IkinciOnay = this.IkinciOnay,
                IlkOnay = this.IlkOnay,
                Nakliye = this.Nakliye,
                OdemeLogoId = this.OdemeLogoId,
                CezaiSart = this.CezaiSart,
                IrsaliyeNo = this.IrsaliyeNo,
                IrsaliyeTarihi = this.IrsaliyeTarihi,
                FaturaNo = this.FaturaNo,
                FaturaTarihi = this.FaturaTarihi
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vTalepKarsilamaAct")]
    public class vTalepKarsilamaAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TalepKarsilamaId { get; set; }

        [Column]
        public int? TalepId { get; set; }

        //[Column]
        public int? DurumId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public string BirimAdi { get; set; }

        [Column]
        public double Fiyat { get; set; }

        [Column]
        public int? TedarikciId { get; set; }

        [Column]
        public string MalzemeKodu { get; set; }
        
        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public string TedarikciKodu { get; set; }

        [Column]
        public string TedarikciAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public string Doviz { get; set; }

        [Column]
        public double Kur { get; set; }

        public double FiyatTL
        {
            get {return Fiyat * Kur; }
        }

        public double Tutar
        {
            get { return Fiyat * (Kur == 0 ? 1 : Kur) * (OnayMiktar.HasValue ? OnayMiktar.Value : 0); }
        }

        public double DovizTutari
        {
            get { return Fiyat * Miktar; }
        }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public string BolumAdi { get; set; }

        [Column]
        public int MalzemeBagId { get; set; }

        [Column]
        public string RenkKodu { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public double? OnayMiktar { get; set; }

        [Column]
        public string OnayNotu { get; set; }

        [Column]
        public string Detay { get; set; }

        [Column]
        public double? DepoGirisMiktar { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public DateTime? SonKullanmaTarihi { get; set; }

        public List<tblMalzemeler> Malzemeler { get; set; }
        public List<vMalzemeBirimleri> MalzemeBirimleri { get; set; }
        public List<vPersonelBolumleri> Bolumler { get; set; }
        public List<tblRenkler> IplikRenkleri { get; set; }
        public List<tblAyarlar> Dovizler { get; set; }

        public tblTalepKarsilamaAct ViewToTbl()
        {
            return new tblTalepKarsilamaAct()
            {
                BirimId = this.BirimId,
                DovizId = this.DovizId,
                Fiyat = this.Fiyat,
                Id = this.Id,
                Kur = this.Kur,
                MalzemeId = this.MalzemeId,
                Miktar = this.Miktar,
                TalepId = this.TalepId,
                TalepKarsilamaId = this.TalepKarsilamaId,
                Tarih = this.Tarih,
                TedarikciId = this.TedarikciId,
                BolumId = this.BolumId,
                Detay = this.Detay,
                OnayMiktar = this.OnayMiktar,
                OnayNotu = this.OnayNotu,
                Ambalaj = this.Ambalaj,
                BobinSayisi = this.BobinSayisi,
                LotNo = this.LotNo,
                RenkId = this.RenkId,
                DepoGirisMiktar = this.DepoGirisMiktar,
                TerminTarihi = this.TerminTarihi,
                SonKullanmaTarihi = this.SonKullanmaTarihi
            };
        }

        public static List<tblTalepKarsilamaAct> ViewToTbl(List<vTalepKarsilamaAct> view)
        {
            List<tblTalepKarsilamaAct> tbl = new List<tblTalepKarsilamaAct>();

            foreach (var item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        public double MevcutStok { get; set; }
        public string GelecekStok { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMalzemeBirimleri")]
    public class vMalzemeBirimleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public string BirimAdi { get; set; }

        [Column]
        public double BirimCarpan { get; set; }

        [Column]
        public double AnaCarpan { get; set; }

        [Column]
        public bool AnaBirimMi { get; set; }

        [Column]
        public int BirimAyarId { get; set; }

        public vMalzemeBirimleri GetMalzemeAnaBirim(int malId)
        {
            return new DBEvents().GetGeneric<vMalzemeBirimleri>(c => c.AnaBirimMi == true && c.MalzemeId == malId).First();
        }

        public List<vMalzemeBirimleri> GetMalzemeBirimleri(int malId)
        {
            return new DBEvents().GetGeneric<vMalzemeBirimleri>(c => c.MalzemeId == malId).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vModulDurumlari")]
    public class vModulDurumlari : IDisposable
    {
        [Column]
        public int AyarUstId { get; set; }

        [Column]
        public string AyarUstAdi { get; set; }

        [Column]
        public int DurumId { get; set; }

        [Column]
        public string DurumAdi { get; set; }

        [Column]
        public int Sira { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vMalzemeler : tblMalzemeler
    {
        private DBEvents db = new DBEvents();
        public double MevcutStok { get; set; }
        public string GelecekStok { get; set; }

        public List<tblMalzemeler> GruptakiMalzemeleriGetir(int GrupID, bool birimlerGelsinMi = false)
        {
            List<tblMalzemeler> list = db.GetGeneric<tblMalzemeler>(c => c.BaglantiId == GrupID && c.AktifMi == true).OrderBy(o=>o.Adi).ToList();

            return list;
        }

        public List<vMalzemeler> ArananMalzemeGetir(int malzemeId)
        {
            List<tblMalzemeler> list = db.GetGeneric<tblMalzemeler>(c => c.Id == malzemeId && c.AktifMi == true);

            List<vMalzemeler> vList = list.ConvertAll(new Converter<tblMalzemeler, vMalzemeler>(vMalzemeler.tblMalzemelerTovMalzemeler));

            return vList;
        }

        public enum AramaKriteri { KodaGore, AdaGore, None }
        public List<tblMalzemeler> ArananMalzemeleriGetir(string Filtre, int GrupId, AramaKriteri eGore)
        {
            List<tblMalzemeler> list = new List<tblMalzemeler>();

            if (eGore == AramaKriteri.AdaGore) 
                list = db.GetGeneric<tblMalzemeler>(c => c.Adi.ToUpper().Contains(Filtre.ToUpper()) && c.BaglantiId == GrupId && c.AktifMi == true);
            else if (eGore == AramaKriteri.KodaGore)
                list = db.GetGeneric<tblMalzemeler>(c => c.Kodu.ToUpper().Contains(Filtre.ToUpper()) && c.BaglantiId == GrupId && c.AktifMi == true);
            else if (eGore == AramaKriteri.None)
                list = db.GetGeneric<tblMalzemeler>(c => c.BaglantiId == GrupId && c.AktifMi == true);
            return list;
        }

        public List<tblMalzemeler> MalzemeGruplariGetir()
        {
            return db.GetGeneric<tblMalzemeler>(c => c.BaglantiId == -1);
        }

        public static vMalzemeler tblMalzemelerTovMalzemeler(tblMalzemeler malzeme)
        {
            return new vMalzemeler()
            {
                Adi = malzeme.Adi,
                BaglantiId = malzeme.BaglantiId,
                Birimleri = new DBEvents().GetGeneric<vMalzemeBirimleri>(c => c.MalzemeId == malzeme.Id),
                Id = malzeme.Id,
                Kodu = malzeme.Kodu,
                MevcutStok = new Stok().StokMiktariGetir(malzeme.Id),
                GelecekStok = new Stok().StokGelecekMiktariGetir(malzeme.Id)
            };

        }

        public static vMalzemeler tblMalzemelerTovMalzemeler(tblMalzemeler malzeme, bool stokHesaplamaz)
        {
            return new vMalzemeler()
            {
                Adi = malzeme.Adi,
                BaglantiId = malzeme.BaglantiId,
                Birimleri = new DBEvents().GetGeneric<vMalzemeBirimleri>(c => c.MalzemeId == malzeme.Id),
                Id = malzeme.Id,
                Kodu = malzeme.Kodu
            };

        }

        public static List<vMalzemeler> tblMalzemelerTovMalzemeler(List<tblMalzemeler> malzemeler, bool stokMiktarlariHesaplansin = false)
        {
            List<vMalzemeler> newList = new List<vMalzemeler>();
            foreach (tblMalzemeler item in malzemeler)
            {
                if (stokMiktarlariHesaplansin) newList.Add(vMalzemeler.tblMalzemelerTovMalzemeler(item));
                else newList.Add(vMalzemeler.tblMalzemelerTovMalzemeler(item, true));
            }

            return newList;
        }

        public List<tblMalzemeler> IplikleriGetir()
        {
            return GruptakiMalzemeleriGetir(39);
        }

        public tblMalzemeler IplikGetir(int iplikId)
        {
            return db.GetGeneric<tblMalzemeler>(c => c.Id == iplikId).FirstOrDefault();
        }

        /// <summary>
        /// Verilen list'deki malzemeleri kaydeder. Listenin içinde id'si 0 olan kayıtlar insert edilirken id'si 0'dan farklı olan kayıtlar update edilir.
        /// Dönüş true ise kaydetme başarılıdır. false ise kaydetme başarısızdır.
        /// </summary>
        /// <param name="malzemeler"></param>
        /// <returns></returns>
        public bool MalzemeKaydet(List<tblMalzemeler> malzemeler)
        {
            bool snc = true;
            List<tblMalzemeler> toSaveList = malzemeler.FindAll(c => c.Id == 0);
            List<tblMalzemeler> toUpdList = malzemeler.FindAll(c => c.Id != 0);

            if (toSaveList.Count > 0) if (db.SaveGeneric<tblMalzemeler>(toSaveList) == false) snc = false;
            if (toUpdList.Count > 0) if (db.UpdateGeneric<tblMalzemeler>(toUpdList) == false) snc = false;

            return snc;
        }

        public List<vMalzemeBirimleri> Birimleri { get; set; }

    }

    [Table(Name = "vAyarlar")]
    public class vAyarlar : tblAyarlar
    {
        DBEvents db = new DBEvents();

        public vAyarlar AyarGetir(int id)
        {
            return db.GetGeneric<tblAyarlar>(c => c.Id == id).ConvertAll<vAyarlar>(new Converter<tblAyarlar, vAyarlar>(tblAyarlarToVAyarlar)).FirstOrDefault();
        }

        public List<vAyarlar> DovizleriGetir()
        {
            tblAyarlar dovizTanim = db.GetGeneric<tblAyarlar>(c=>c.Adi == "Doviz").FirstOrDefault();
            return dovizTanim != null ? db.GetGeneric<tblAyarlar>(c=>c.BaglantiId == dovizTanim.Id).ConvertAll<vAyarlar>(new Converter<tblAyarlar,vAyarlar>(tblAyarlarToVAyarlar))
                                      : new List<vAyarlar>();
        }

        public static vAyarlar tblAyarlarToVAyarlar(tblAyarlar ayar)
        {
            return new vAyarlar()
            {
                Aciklama = ayar.Aciklama,
                Adi = ayar.Adi,
                BaglantiId = ayar.BaglantiId,
                BosGecilebilirMi = ayar.BosGecilebilirMi,
                Deger = ayar.Deger,
                GosterilsinMi = ayar.GosterilsinMi,
                Id = ayar.Id, 
                KontrolMu = ayar.KontrolMu,
                Sira = ayar.Sira
            };
        }


    }

    [Table(Name = "tblPersonelBolumleri")]
    public class vPersonelBolumleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        public List<vPersonelBolumleri> PersonelBolumleriGetir()
        {
            List<vPersonelBolumleri> list = new DBEvents().GetGeneric<vPersonelBolumleri>().ToList();
            return list == null ? new List<vPersonelBolumleri>() : list;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMalzemeStokDurum")]
    public class vMalzemeStokDurum : IDisposable
    {
        [Column(Name = "MalzemeId", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MalzemeId { get; set; }

        [Column]
        public int MalzemeBagId { get; set; }        

        [Column]
        public string MalzemeKodu { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public double MinStok { get; set; }

        [Column]
        public double MevcutStok { get; set; }

        public double GelecekStok { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKullanicilar")]
    public class vKullanicilar : IDisposable
    {
        [Column]
        public int Id { get; set; }
        
        [Column]
        public string KulAdi { get; set; }

        [Column]
        public string Sifre { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public int? BolumId { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        public static List<vKullanicilar> KullanicilariGetir()
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<vKullanicilar>();
            }
        }

        public bool KullaniciKaydet(string eskiSifre)
        {
            using (var db = new DBEvents())
            {
                if (this.ExistKullaniciAdi())
                    throw new Exception("Girdiğiniz kullanıcı adı kullanımda.\n\nFarklı bir kullanıcı adı belirleyiniz..!");

                if (this.Id != 0)
                {
                    vKullanicilar sifreKontrol = db.GetGeneric<vKullanicilar>(c => c.Id == this.Id && c.Sifre == eskiSifre).FirstOrDefault();
                    if (sifreKontrol == null) throw new Exception("Eski şifre doğru değil..!");
                }

                if (this.Id == 0) return db.SaveGeneric<tblKullanicilar>(this.ViewToTbl());
                else return db.UpdateGeneric<tblKullanicilar>(this.ViewToTbl());
            }
        }

        public bool ExistKullaniciAdi()
        {
            using (var db = new DBEvents())
            {
                var record = db.GetGeneric<vKullanicilar>(c => c.Id != this.Id && c.KulAdi == this.KulAdi).FirstOrDefault();
                return record != null;
            }
        }

        internal tblKullanicilar ViewToTbl()
        {
            return new tblKullanicilar
            {
                Adi = this.KulAdi,
                PersonelId = this.PersonelId,
                Sifre = this.Sifre,
                Id = this.Id
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vBakimOnarim")]
    public class vBakimOnarim : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MakinaId { get; set; }

        [Column]
        public string MakinaAdi { get; set; }

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

        internal static tblBakimOnarim ViewToTable(vBakimOnarim view)
        {
            return new tblBakimOnarim()
            {
                Aciklama = view.Aciklama,
                BasTarih = view.BasTarih,
                BitisTarih = view.BitisTarih,
                HarcananSure = view.HarcananSure,
                Id = view.Id,
                MakinaId = view.MakinaId,
                OlusturmaTarihi = view.OlusturmaTarihi == null ? DateTime.Now : view.OlusturmaTarihi,
                PersonelId = view.PersonelId,
                Turu = view.Turu,
                Vardiya = view.Vardiya
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vBakimOnarimAct")]
    public class vBakimOnarimAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BakimOnarimId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string MalzemeAdi { get; set; }

        [Column]
        public string MalzemeKodu { get; set; }
        
        [Column]
        public double Miktar { get; set; }

        [Column]
        public int BirimId { get; set; }

        [Column]
        public string BirimAdi { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public DateTime OlusturmaTarihi { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        internal static tblBakimOnarimAct ViewToTable(vBakimOnarimAct view)
        {
            return new tblBakimOnarimAct()
            {
                BakimOnarimId = view.BakimOnarimId,
                BirimId = view.BirimId,
                Id = view.Id,
                MalzemeId = view.MalzemeId,
                Miktar = view.Miktar,
                OlusturmaTarihi = view.OlusturmaTarihi,
                PersonelId = view.PersonelId    
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMakinaBakimTarihleri")]
    public class vMakinaBakimTarihleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int BakimPeriyodu { get; set; }

        [Column]
        public DateTime? SonrakiBakimTarihi { get; set; }

        [Column]
        public int KalanGun { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSayacGirisleriDgaz")]
    public class vSayacGirisleriDgaz : IDisposable
    {
        [Column(Name = "SayacGirisId", IsDbGenerated = true, IsPrimaryKey = true)]
        public int SayacGirisId { get; set; }

        [Column]
        public int SarfiyatId { get; set; }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public string BolumAdi { get; set; }

        [Column]
        public int SayacId { get; set; }

        [Column]
        public string SayacAdi { get; set; }

        [Column]
        public double m3 { get; set; }

        [Column]
        public double sm3 { get; set; }

        [Column]
        public double kwh { get; set; }        

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public double Maliyet { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSayacGirisleriSu")]
    public class vSayacGirisleriSu : IDisposable
    {
        [Column(Name = "SayacGirisId", IsDbGenerated = true, IsPrimaryKey = true)]
        public int SayacGirisId { get; set; }

        [Column]
        public int SarfiyatId { get; set; }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public string BolumAdi { get; set; }

        [Column]
        public int SayacId { get; set; }

        [Column]
        public string SayacAdi { get; set; }

        [Column]
        public double ton { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public double Maliyet { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSayacGirisleriElk")]
    public class vSayacGirisleriElk : IDisposable
    {
        [Column(Name = "SayacGirisId", IsDbGenerated = true, IsPrimaryKey = true)]
        public int SayacGirisId { get; set; }

        [Column]
        public int SarfiyatId { get; set; }

        [Column]
        public int BolumId { get; set; }

        [Column]
        public string BolumAdi { get; set; }

        [Column]
        public int SayacId { get; set; }

        [Column]
        public string SayacAdi { get; set; }

        [Column]
        public double kwh { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public double Maliyet { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vSayacGiris
    {
        public int BolumId { get; set; }
        public double kwh { get; set; }
        public double ton { get; set; }
        public double m3 { get; set; }
        public double sm3 { get; set; }
        public string BolumAdi { get; set; }
        public DateTime Tarih { get; set; }
        public double BirimFiyat { get; set; }
        public double Maliyet { get; set; }
        public int PersonelId { get; set; }
    }

    [Table(Name = "vFiyatListeleri")]
    public class vFiyatListeleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public int? ProcessId { get; set; }

        [Column]
        public string MusteriKodu { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

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
        public int Ay { get; set; }

        [Column]
        public int Yil { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public int OlusturanPersonelId { get; set; }

        public List<vAyarlar> Dovizler { get; set; }
        public List<tblProses> Prosesler { get; set; }

        public static tblFiyatListeleri ViewToTable(vFiyatListeleri fiyat)
        {
            return new tblFiyatListeleri()
                {
                    Aciklama = fiyat.Aciklama,
                    AktifMi = fiyat.AktifMi,
                    Ay = fiyat.Ay,
                    DovizId = fiyat.DovizId,
                    En = fiyat.En,
                    Fiyat = fiyat.Fiyat,
                    Gr = fiyat.Gr,
                    Grup = fiyat.Grup,
                    Id = fiyat.Id,
                    Kompozisyon = fiyat.Kompozisyon,
                    MusteriId = fiyat.MusteriId,
                    OlusturanPersonelId = fiyat.OlusturanPersonelId,
                    ProcessId = fiyat.ProcessId,
                    Tarih = fiyat.Tarih,
                    Tip = fiyat.Tip,
                    Yil = fiyat.Yil
                };
        }

        public static List<tblFiyatListeleri> ViewToTable(List<vFiyatListeleri> fiyatListesi)
        {
            List<tblFiyatListeleri> tblList = new List<tblFiyatListeleri>();

            foreach (vFiyatListeleri viewItem in fiyatListesi)
            {
                tblList.Add(ViewToTable(viewItem));
            }

            return tblList;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKur")]
    public class vKur : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }
        
        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string DovizAdi { get; set; }

        [Column]
        public string DovizSimge { get; set; }

        [Column]
        public Int16 LogoDovizId { get; set; }

        [Column]
        public int LogoKurId { get; set; }

        [Column]
        public double LogoDovizAlis { get; set; }
        
        [Column]
        public double LogoDovizSatis { get; set; }

        [Column]
        public double LogoEfektifAlis { get; set; }

        [Column]
        public double LogoEfektifSatis { get; set; }

        [Column]
        public DateTime LogoTarih { get; set; }

        public vKur DovizKuruGetir(int dovizId)
        {
            return new DBEvents().GetGeneric<vKur>(c => c.Id == dovizId).FirstOrDefault();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSiparisler")]
    public class vSiparisler : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public string FirmaAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string SipTipi { get; set; }

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
        public int OlusturanPersId { get; set; }

        [Column]
        public string OlusturanPersAdi { get; set; }

        [Column]
        public string SipOnaylayan { get; set; }

        [Column]
        public int? GuncelleyenPersId { get; set; }

        [Column]
        public string GuncelleyenPersAdi { get; set; }

        [Column]
        public double TopMiktar { get; set; }

        [Column]
        public string OrderNo { get; set; }

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

        public string IadeSipNo { get; set; }

        [Column]
        public string ProformaNo { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }

        [Column]
        public string KarZarar { get; set; }

        [Column]
        public int? Yuzde { get; set; }

        [Column]
        public DateTime? SevkTarihi { get; set; }

        [Column]
        public int? TerminHaftasi { get; set; }

        public void ProformaNumarasiAl()
        {
            if (this.ProformaNo == null || this.ProformaNo == "")
            {
                tblBelgeNo belgeNo = tblBelgeNo.BelgeNoGetir("Proforma");
                string strNumara = (belgeNo.SonBelgeNo + 1).ToString();
                string belgeNoStr = DateTime.Now.ToString("yyyy") + "/" + strNumara;


                if (this.Id != 0)
                {
                    DBEvents db = new DBEvents();
                    tblSiparisler tbl = db.GetGeneric<tblSiparisler>(c => c.Id == this.Id).FirstOrDefault();
                    tbl.ProformaNo = belgeNoStr;
                    if (db.UpdateGeneric<tblSiparisler>(tbl) == false) return;
                }

                belgeNo.SonBelgeNo++;
                tblBelgeNo.BelgeNoKaydet(belgeNo);
                this.ProformaNo = belgeNoStr;
            }
        }

        public bool SevkEmriVarMi()
        {
            if (this.SevkEdilebilirMi.HasValue == true && this.SevkEdilebilirMi.Value) return true;

            DBEvents db = new DBEvents();

            tblSevk sevk = db.GetGeneric<tblSevk>(c => c.SiparisId == this.Id && c.Tarih == DateTime.Today).FirstOrDefault();
            if (sevk != null) return true;
            else return false;
        }

        public static tblSiparisler ViewToTable(vSiparisler view)
        {
            return new tblSiparisler()
                {
                    BaglantiId = view.BaglantiId,
                    BelgeTuru = view.BelgeTuru,
                    Durum = view.Durum,
                    FirmaId = view.FirmaId,
                    GuncelleyenPersId = view.GuncelleyenPersId,
                    Id = view.Id,
                    KarsiReferansNo = view.KarsiReferansNo,
                    OlusturanPersId = view.OlusturanPersId,
                    SipOnaylayan = view.SipOnaylayan,
                    SevkYeri = view.SevkYeri,
                    SipVeren = view.SipVeren,
                    SozlesmeNo = view.SozlesmeNo,
                    Tarih = view.Tarih,
                    TerminTarihi = view.TerminTarihi,
                    OrderNo = view.OrderNo,
                    FaturaAdresi = view.FaturaAdresi,
                    FaturaNo = view.FaturaNo,
                    KargoUcreti = view.KargoUcreti,
                    OdemeSekli = view.OdemeSekli,
                    SevkiyatSekli = view.SevkiyatSekli,
                    SevkEdilebilirMi = view.SevkEdilebilirMi,
                    IadeSipNo = view.IadeSipNo,
                    ProformaNo = view.ProformaNo,
                    KapanmaTarihi = view.KapanmaTarihi
                };
        }

        public static List<tblSiparisler> ViewToTable(List<vSiparisler> view)
        {
            List<tblSiparisler> tbl = new List<tblSiparisler>();
            foreach (vSiparisler viewItem in view)
            {
                tbl.Add(ViewToTable(viewItem));    
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSiparisAct")]
    public class vSiparisAct : IDisposable
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
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public int DovizId { get; set; }

        [Column]
        public string Doviz { get; set; }

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
        public double SevkMiktar { get; set; }

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
        public string SozlesmeNo { get; set; }

        [Column]
        public int? FinishGrupId { get; set; }

        [Column]
        public int FinishId { get; set; }

        public string FinishAdi { get; set; }

        [Column]
        public string SecilenFinishAdi { get; set; }

        [Column]
        public int? TestId { get; set; }

        [Column]
        public double? RezerveMetre { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public string FirmaKodu { get; set; }

        [Column]
        public string FirmaAdi { get; set; }

        [Column]
        public string TipAcikAdi { get; set; }
        
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

        public double PartilenenMetre { get; set; }

        [Column]
        public DateTime? KapanmaTarihi { get; set; }

        [Column]
        public string TipMalzemeKodu { get; set; }

        //Gökhan 14.05.2014
        public string SatirKodu { get; set; }

        public List<vAyarlar> ListDoviz { get; set; }
        public List<tblMalzemeler> ListMamuller { get; set; }

        public double? PartiMetresi { get; set; }

        public double? AcilanMetre { get; set; }

        public double? PartiBirimMaliyet { get; set; }

        public double? DovizBirimMaliyet { get; set; }

        public double? SatirMaliyeti { get; set; }

        public double? TutarDoviz { get; set; }
        
        [Column]
        public string  PartiListesi { get; set; }

        [Column]
        public string KoleksiyonAdi { get; set; }

        public tblSiparisTestleri Testler { get; set; }

        public static tblSiparisAct ViewToTable(vSiparisAct view)
        {
            return new tblSiparisAct()
            {
                AntiStatik = view.AntiStatik == null ? false : view.AntiStatik.Value,
                Apresiz = view.Apresiz == null ? false : view.Apresiz.Value,
                BirimFiyat = view.BirimFiyat,
                BoyaNotu = view.BoyaNotu,
                DisTicaretNotu = view.DisTicaretNotu,
                DokumaNotu = view.DokumaNotu,
                DovizId = view.DovizId,
                FinishOzellikleri = view.FinishOzellikleri,
                IcTicaretNotu = view.IcTicaretNotu,
                Id = view.Id,
                LabRenkNo = view.LabRenkNo,
                Miktar = view.Miktar,
                MuhasebeNotu = view.MuhasebeNotu,
                NorApre = view.NorApre == null ? false : view.NorApre.Value,
                NorYanmazApre = view.NorYanmazApre == null ? false : view.NorYanmazApre.Value,
                NumuneNotu = view.NumuneNotu,
                ParcaliTop = view.ParcaliTop,
                PlanlamaNotu = view.PlanlamaNotu,
                SevkDurumu = view.SevkDurumu,
                SevkiyatNotu = view.SevkiyatNotu,
                SevkMiktar = view.SevkMiktar,
                SiparisId = view.SiparisId,
                SirtApre = view.SirtApre == null ? false : view.SirtApre.Value,
                SuYagApre = view.SuYagApre == null ? false : view.SuYagApre.Value,
                TerminNotu = view.TerminNotu,
                TerminTarihi = view.TerminTarihi,
                TopMetre = view.TopMetre == null ? 0 : view.TopMetre.Value,
                TipId = view.TipId,
                YumApre = view.YumApre == null ? false : view.YumApre.Value,
                Sira = view.Sira,
                FinishGrupId = view.FinishGrupId,
                TestId = view.TestId,
                RenkId = view.RenkId,
                TipMalzemeId = view.TipMalzemeId,
                Durum = view.Durum,
                RefBarkod = view.RefBarkod,
                RefRenkAdi = view.RefRenkAdi,
                RefRenkNo = view.RefRenkNo,
                RefTipAdi = view.RefTipAdi,
                RefTipNo = view.RefTipNo,
                KapanmaTarihi = view.KapanmaTarihi,
                FinishId = view.FinishId,
                KoleksiyonAdi = view.KoleksiyonAdi                
            };
        }

        public static List<tblSiparisAct> ViewToTable(List<vSiparisAct> view)
        {
            List<tblSiparisAct> tbl = new List<tblSiparisAct>();

            foreach (vSiparisAct item in view)
            {
                tbl.Add(ViewToTable(item));
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "tblKumas")]
    public class vKumas : IDisposable
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
        public int KumasCinsi { get; set; }

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

        public List<vAyarlar> Dovizler { get; set; }

        public static tblKumas ViewToTable(vKumas view)
        {
            return new tblKumas()
            {
                DikisSiyrikAtki = view.DikisSiyrikAtki,
                DikSiyrikCozgu = view.DikSiyrikCozgu,
                DovizId = view.DovizId,
                EntId = view.EntId,
                Fiyat = view.Fiyat,
                HavKomp = view.HavKomp,
                Id = view.Id,
                KulAlani = view.KulAlani,
                KumasAgirlik = view.KumasAgirlik,
                KumasAgirlik2 = view.KumasAgirlik2,
                KumasEn = view.KumasEn,
                Martindale = view.Martindale,
                MukavemetAtki = view.MukavemetAtki,
                MukavemetCözgü = view.MukavemetCözgü,
                RenkHaslikAcik = view.RenkHaslikAcik,
                RenkHaslikKoyu = view.RenkHaslikKoyu,
                RenkHaslikOrta = view.RenkHaslikOrta,
                SurtmeHaslikKuruAcik = view.SurtmeHaslikKuruAcik,
                SurtmeHaslikKuruKoyu = view.SurtmeHaslikKuruKoyu,
                SurtmeHaslikKuruOrta = view.SurtmeHaslikKuruOrta,
                SurtmeHaslikYasAcik = view.SurtmeHaslikYasAcik,
                SurtmeHaslikYasKoyu = view.SurtmeHaslikYasKoyu,
                SurtmeHaslikYasOrta = view.SurtmeHaslikYasOrta,
                TipAdi = view.TipAdi,
                TipNo = view.TipNo,
                TotalKomp = view.TotalKomp,
                Yik1 = view.Yik1,
                Yik2 = view.Yik2,
                Yik3 = view.Yik3,
                Yik4 = view.Yik4,
                Yik5 = view.Yik5,
                YikamaTalNot = view.YikamaTalNot,
                Dosemelik = view.Dosemelik,
                Perdelik = view.Perdelik,
                Elbiselik = view.Elbiselik,
                Likrali = view.Likrali,
                AktifMi = view.AktifMi,
                Atki1 = view.Atki1,
                Atki1HamGr = view.Atki1HamGr,
                Atki1UstuGr = view.Atki1UstuGr,
                Atki2 = view.Atki2,
                Atki2HamGr = view.Atki2HamGr,
                Atki2UstuGr = view.Atki2UstuGr,
                Atki3 = view.Atki3,
                Atki3HamGr = view.Atki3HamGr,
                Atki3UstuGr = view.Atki3UstuGr,
                Atki4 = view.Atki4,
                Atki4HamGr = view.Atki4HamGr,
                Atki4UstuGr = view.Atki4UstuGr,
                AtkiSiklik = view.AtkiSiklik,
                CozguSiklik = view.CozguSiklik,
                HamAgirlik = view.HamAgirlik,
                HamEn = view.HamEn,
                HamM2Agirlik = view.HamM2Agirlik,
                Hav1 = view.Hav1,
                Hav1HamGr = view.Hav1HamGr,
                Hav1UstuGr = view.Hav1UstuGr,
                Hav2 = view.Hav2,
                Hav2HamGr = view.Hav2HamGr,
                Hav2UstuGr = view.Hav2UstuGr,
                Hav3 = view.Hav3,
                Hav3HamGr = view.Hav3HamGr,
                Hav3UstuGr = view.Hav3UstuGr,
                Hav4 = view.Hav4,
                Hav4HamGr = view.Hav4HamGr,
                Hav4UstuGr = view.Hav4UstuGr,
                HavBaglanti = view.HavBaglanti,
                HavCozguTel = view.HavCozguTel,
                HavOrgu = view.HavOrgu,
                HavSevki = view.HavSevki,
                HHavYuksek = view.HHavYuksek,
                Kenar = view.Kenar,
                KenarCozguTel = view.KenarCozguTel,
                KenarHamGr = view.KenarHamGr,
                KenarUstuGr = view.KenarUstuGr,
                Kompozisyon = view.Kompozisyon,
                MamulAgirlik = view.MamulAgirlik,
                MamulEn = view.MamulEn,
                MamulM2Agirlik = view.MamulM2Agirlik,
                MHavYuksek = view.MHavYuksek,
                NumuneTipNo = view.NumuneTipNo,
                Spiger = view.Spiger,
                SpigerCozguTel = view.SpigerCozguTel,
                SpigerHamGr = view.SpigerHamGr,
                SpigerUstuGr = view.SpigerUstuGr,
                TarakEni = view.TarakEni,
                TarakNo = view.TarakNo,
                Zemin1 = view.Zemin1,
                Zemin1HamGr = view.Zemin1HamGr,
                Zemin1UstuGr = view.Zemin1UstuGr,
                Zemin2 = view.Zemin2,
                Zemin2HamGr = view.Zemin2HamGr,
                Zemin2UstuGr = view.Zemin2UstuGr,
                Zemin3 = view.Zemin3,
                Zemin3HamGr = view.Zemin3HamGr,
                Zemin3UstuGr = view.Zemin3UstuGr,
                Zemin4 = view.Zemin4,
                Zemin4HamGr = view.Zemin4HamGr,
                Zemin4UstuGr = view.Zemin4UstuGr,
                ZeminCozguTel = view.ZeminCozguTel,
                ZeminOrgu = view.ZeminOrgu,
                ZeminSevki = view.ZeminSevki,
                Varyant = view.Varyant    ,
                MamulAgirlikUst=view.MamulAgirlikUst,
                KumasCinsi = view.KumasCinsi,
                TaharNo = view.TaharNo,
                MaySiklik = view.MaySiklik,
                Zemin1CozguTel = view.Zemin1CozguTel,
                Zemin2CozguTel = view.Zemin2CozguTel,
                TostHavBoyu = view.TostHavBoyu,
                TarakAdedi = view.TarakAdedi,
                Zemin1TarakTahar = view.Zemin1TarakTahar,
                Zemin2TarakTahar = view.Zemin2TarakTahar,
                Zemin5TarakTahar = view.Zemin5TarakTahar,
                Zemin6TarakTahar = view.Zemin6TarakTahar,
                Hav3TarakTahar = view.Hav3TarakTahar,
                Hav4TarakTahar = view.Hav4TarakTahar,
                ZeminAltOrmeAgirlik = view.ZeminAltOrmeAgirlik,
                ZeminUstOrmeAgirlik = view.ZeminUstOrmeAgirlik,
                HavOrmeAgirlik = view.HavOrmeAgirlik,
                ZeminAltRack = view.ZeminAltRack,
                ZeminUstRack = view.ZeminUstRack,
                HavRack = view.HavRack,
                ZeminAltTostAgirlik = view.ZeminAltTostAgirlik,
                ZeminUstTostAgirlik = view.ZeminUstTostAgirlik,
                HavTostAgirlik = view.HavTostAgirlik,
                ToplamTostAgirlik = view.ToplamTostAgirlik,
                KumasGrup = view.KumasGrup
            };
        }
        

        public List<vMusteriTipSatisHakkı> SatisHaklariGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vMusteriTipSatisHakkı>("exec spMusteriTipSatisHaklariGetir {0}", new object[] { this.Id });
        }

        public List<vTipFinishHakkı> FinishHaklariGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vTipFinishHakkı>("exec spTipFinishHaklariGetir {0}", new object[] { this.Id });
        }

        public bool SatisHaklariKaydet(List<vMusteriTipSatisHakkı> haklar)
        {
            DBEvents db = new DBEvents();

            List<string> silindiMi = db.GetGenericWithSQLQuery<string>("DELETE FROM [LKDB].[dbo].[tblKumasSatisHakki] WHERE TipId = {0}", new object[] { this.Id });
            if (silindiMi == null) return false;

            List<tblKumasSatisHakki> yeniHaklar = new List<tblKumasSatisHakki>();

            foreach (var item in haklar.FindAll(f=>f.SatisHakkiVarMi == true))
            {
                yeniHaklar.Add(new tblKumasSatisHakki()
                {
                     TipId = this.Id, MusteriId = item.MusteriId
                });
            }

            return db.SaveGeneric<tblKumasSatisHakki>(yeniHaklar);
        }

        public bool FinishHaklariKaydet(List<vTipFinishHakkı> haklar)
        {
            DBEvents db = new DBEvents();

            List<string> silindiMi = db.GetGenericWithSQLQuery<string>("DELETE FROM [LKDB].[dbo].[tblKumasFinishHakki] WHERE TipId = {0}", new object[] { this.Id });
            if (silindiMi == null) return false;

            List<tblKumasFinishHakki> yeniHaklar = new List<tblKumasFinishHakki>();

            foreach (var item in haklar.FindAll(f => f.FinishHakkiVarMi == true))
            {
                yeniHaklar.Add(new tblKumasFinishHakki()
                {
                    TipId = this.Id,
                    ProcessGrupId = item.ProcessGrupId
                });
            }

            return db.SaveGeneric<tblKumasFinishHakki>(yeniHaklar);
        }

        public List<tblKumasBelgeleri> TakdirNameleriGetir()
        {
            DBEvents db = new DBEvents();
            return db.GetGeneric<tblKumasBelgeleri>(c => c.TipId == this.Id);
        }

        public bool TakdirNameEkle(string localPath)
        {
            if (!File.Exists(localPath)) return false;

            DBEvents db = new DBEvents();
            tblAyarlar serverPath = db.GetGeneric<tblAyarlar>(c=>c.Adi == "TakdirNameBelgePath").FirstOrDefault();
            if (serverPath == null || serverPath.Deger == null) return false;

            string dosyaAdi = localPath.Substring(localPath.LastIndexOf('\\') + 1);
            tblKumasBelgeleri belge = new tblKumasBelgeleri()
            {
                Adi = dosyaAdi,
                DosyaYolu = serverPath.Deger + "\\" + dosyaAdi.Insert(dosyaAdi.LastIndexOf('.'), DateTime.Now.ToString("'_'yyMMddHHmmmss")),
                TipId = this.Id
            };

            string serverSnc = "true";
            try
            {
                DosyaServisi.FileOperationServicesClient client = new DosyaServisi.FileOperationServicesClient();
                serverSnc = client.SaveFile(new DosyaServisi.SenfoniFiles() { FileByteArray = ExtensionMethods.FileToByteArray(localPath), FileName = belge.DosyaYolu });
            }
            catch { return false; }

            if (serverSnc == "true") return db.SaveGeneric<tblKumasBelgeleri>(belge);

            return false;
        }

        public static List<tblKumas> ViewToTable(List<vKumas> view)
        {
            List<tblKumas> tbl = new List<tblKumas>();

            foreach (vKumas item in view) tbl.Add(ViewToTable(item));

            return tbl;
        }

        public static vKumas TipGetir(int id)
        {
            return new DBEvents().GetGeneric<vKumas>(c => c.Id == id).FirstOrDefault();
        }

        public static List<vKumas> KumaslariGetir(bool sadeceAktifler = false)
        {
            if (sadeceAktifler) return new DBEvents().GetGeneric<vKumas>(c => c.AktifMi == true).OrderBy(o => o.TipNo).ToList();
            return new DBEvents().GetGeneric<vKumas>().OrderBy(o => o.TipNo).ToList();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vYetkiTanim")]
    public class vYetkiTanim : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MenuId { get; set; }

        [Column]
        public int MenuBagId { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        public List<vYetkiTanim> AltYetkiler { get; set; }
        private bool? _YetkiliMi = true;
        public bool? YetkiliMi
        {
            get
            {
                return _YetkiliMi;
            }
            set
            {
                _YetkiliMi = value;
            }
        }

        //private static List<vYetkiTanim> YetkiTanimlariGetir()
        //{
        //    List<vYetkiTanim> yetkiler = new DBEvents().GetGeneric<vYetkiTanim>();
        //    List<vYetkiTanim> ToReturnList = new List<vYetkiTanim>();

        //    foreach (vYetkiTanim item in yetkiler.FindAll(c=>c.MenuBagId == 1))
        //    {
        //        item.AltYetkiler = yetkiler.FindAll(c=>c.MenuBagId == item.MenuId);
        //        ToReturnList.Add(item);
        //    }
        //    return ToReturnList;
        //}

        //public static List<vYetkiTanim> YetkileriGetir(int bolumId = 0, int kullaniciId = 0)
        //{
        //    if (bolumId == 0 && kullaniciId == 0) return null;

        //    var query = PredicateBuilder.True<tblYetkiler>();
        //    query = query.And(c => c.BolumId == bolumId);
        //    if (kullaniciId != 0) query = query.Or(c => c.KullaniciId == kullaniciId);

        //    List<tblYetkiler> kullaniciYetki = new DBEvents().GetGeneric<tblYetkiler>(query);
        //    //mevcut tüm yetkiler getiriliyor
        //    List<vYetkiTanim> yetkiler = YetkiTanimlariGetir();
            
        //    //bölümlerin ya da kullanıcıların yetkileri atanıyor
        //    for (int i = 0; i < yetkiler.Count; i++)
        //    {
        //        if (yetkiler[i].AltYetkiler.Count == 0)
        //        {
        //            //kullanıcıya özgü yetki varsa yetki kullanıcının yetkisinden alınır.
        //            tblYetkiler yetki = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].Id && c.KullaniciId == kullaniciId);
        //            //kullanıcıya özgü yetki yoksa, yetki kulanıcının bölümünün yetkisinden alınır.
        //            if (yetki == null)
        //            {
        //                yetki = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].Id && c.BolumId == bolumId);
        //            }
        //            yetkiler[i].YetkiliMi = yetki == null ? true : yetki.YetkiVarMi;
        //        }

        //        else
        //        {
        //            for (int j = 0; j < yetkiler[i].AltYetkiler.Count; j++)
        //            {
        //                tblYetkiler yetkiAlt = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].AltYetkiler[j].Id && c.KullaniciId == kullaniciId);
        //                if (yetkiAlt == null) yetkiAlt = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].AltYetkiler[j].Id && c.BolumId == bolumId);
        //                yetkiler[i].AltYetkiler[j].YetkiliMi = yetkiAlt == null ? true : yetkiAlt.YetkiVarMi;
        //            }

        //            if (yetkiler[i].AltYetkiler.Count == yetkiler[i].AltYetkiler.Count(c => c.YetkiliMi == true)) yetkiler[i].YetkiliMi = true;
        //            else yetkiler[i].YetkiliMi = null;
        //        }
        //    }

        //    return yetkiler;
        //}

        //private static bool YetkiKaydet(List<tblYetkiler> yetkiler)
        //{
        //    List<tblYetkiler> toSaveList = yetkiler.FindAll(c => c.Id == 0);
        //    List<tblYetkiler> toUpdList = yetkiler.FindAll(c => c.Id != 0);

        //    bool sonuc = true;
        //    if (toSaveList.Count > 0) if (new DBEvents().SaveGeneric<tblYetkiler>(toSaveList) == false) sonuc = false;
        //    if (toUpdList.Count > 0) if (new DBEvents().UpdateGeneric<tblYetkiler>(toUpdList) == false) sonuc = false;

        //    return sonuc;
        //}

        //public static bool YetkiKaydet(List<vYetkiTanim> yetkiler, int bolumId = 0, int kullaniciId = 0)
        //{
        //    List<tblYetkiler> yetkiTbl = new List<tblYetkiler>();

        //    //Bölüm için yetki tanımlama
        //    if (bolumId != 0)
        //    {
        //        foreach (vYetkiTanim yetkiTanim in yetkiler)
        //        {
        //            tblYetkiler yetki = new DBEvents().GetGeneric<tblYetkiler>(c => c.BolumId == bolumId && c.YetkiId == yetkiTanim.Id).FirstOrDefault();
        //            if (yetki == null)
        //            {
        //                yetki = new tblYetkiler()
        //                {
        //                    BolumId = bolumId,
        //                    KullaniciId = null,
        //                    YetkiId = yetkiTanim.Id,
        //                    YetkiVarMi = yetkiTanim.YetkiliMi.Value
        //                };
        //            }
        //            else yetki.YetkiVarMi = yetkiTanim.YetkiliMi.Value;
        //            yetkiTbl.Add(yetki);
        //        }
        //    }
        //    //kullanıcı için yetki tanımlama
        //    else
        //    {
        //        foreach (vYetkiTanim yetkiTanim in yetkiler)
        //        {
        //            tblYetkiler yetki = new DBEvents().GetGeneric<tblYetkiler>(c => c.KullaniciId == kullaniciId && c.YetkiId == yetkiTanim.Id).FirstOrDefault();
        //            if (yetki == null)
        //            {
        //                yetki = new tblYetkiler()
        //                {
        //                    KullaniciId = kullaniciId,
        //                    BolumId = null,
        //                    YetkiId = yetkiTanim.Id,
        //                    YetkiVarMi = yetkiTanim.YetkiliMi.Value
        //                };
        //            }
        //            yetkiTbl.Add(yetki);
        //        }
        //    }

        //    return vYetkiTanim.YetkiKaydet(yetkiTbl);
        //}

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vTezgahPlanlama")]
    public class vTezgahPlanlama : IDisposable
    {
        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string Tezgah { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string Tarih1 { get; set; }

        [Column]
        public string Tarih2 { get; set; }

        [Column]
        public string Tarih3 { get; set; }

        [Column]
        public string Tarih4 { get; set; }

        [Column]
        public string Tarih5 { get; set; }

        [Column]
        public string Tarih6 { get; set; }

        [Column]
        public string Tarih7 { get; set; }

        [Column]
        public string Tarih8 { get; set; }

        [Column]
        public string Tarih9 { get; set; }

        [Column]
        public string Tarih10 { get; set; }

        [Column]
        public string Tarih11 { get; set; }

        [Column]
        public string Tarih12 { get; set; }

        [Column]
        public string Tarih13 { get; set; }

        [Column]
        public string Tarih14 { get; set; }

        [Column]
        public string Tarih15 { get; set; }

        [Column]
        public string Tarih16 { get; set; }

        [Column]
        public string Tarih17 { get; set; }

        [Column]
        public string Tarih18 { get; set; }

        [Column]
        public string Tarih19 { get; set; }

        [Column]
        public string Tarih20 { get; set; }

        [Column]
        public string Tarih21 { get; set; }

        [Column]
        public string Tarih22 { get; set; }

        [Column]
        public string Tarih23 { get; set; }

        [Column]
        public string Tarih24 { get; set; }

        [Column]
        public string Tarih25 { get; set; }

        [Column]
        public string Tarih26 { get; set; }

        [Column]
        public string Tarih27 { get; set; }

        [Column]
        public string Tarih28 { get; set; }

        [Column]
        public string Tarih29 { get; set; }

        [Column]
        public string Tarih30 { get; set; }

        [Column]
        public string Tarih31 { get; set; }

        [Column]
        public string Tarih32 { get; set; }

        [Column]
        public string Tarih33 { get; set; }

        [Column]
        public string Tarih34 { get; set; }

        [Column]
        public string Tarih35 { get; set; }

        [Column]
        public string Tarih36 { get; set; }

        [Column]
        public string Tarih37 { get; set; }

        [Column]
        public string Tarih38 { get; set; }

        [Column]
        public string Tarih39 { get; set; }

        [Column]
        public string Tarih40 { get; set; }

        [Column]
        public string Tarih41 { get; set; }

        [Column]
        public string Tarih42 { get; set; }

        [Column]
        public string Tarih43 { get; set; }

        [Column]
        public string Tarih44 { get; set; }

        [Column]
        public string Tarih45 { get; set; }

        [Column]
        public string Tarih46 { get; set; }

        [Column]
        public string Tarih47 { get; set; }

        [Column]
        public string Tarih48 { get; set; }

        [Column]
        public string Tarih49 { get; set; }

        [Column]
        public string Tarih50 { get; set; }

        [Column]
        public string Tarih51 { get; set; }

        [Column]
        public string Tarih52 { get; set; }

        [Column]
        public string Tarih53 { get; set; }

        [Column]
        public string Tarih54 { get; set; }

        [Column]
        public string Tarih55 { get; set; }

        [Column]
        public string Tarih56 { get; set; }

        [Column]
        public string Tarih57 { get; set; }

        [Column]
        public string Tarih58 { get; set; }

        [Column]
        public string Tarih59 { get; set; }

        [Column]
        public string Tarih60 { get; set; }

        [Column]
        public string Tarih61 { get; set; }

        [Column]
        public string Tarih62 { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanlama")]
    public class vPlanlama : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public double? Miktar { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int? AtkiOtelemesi { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        public double? ToplamPlan { get; set; }
        public double? ToplamDokunan { get; set; }

        public static List<vPlanlama> SonrakiPlanlariGetir()
        {
            return new DBEvents().GetGeneric<vPlanlama>(c => c.Tarih >= DateTime.Today);
        }

        internal tblPlanlama ViewToTbl()
        {
            return new tblPlanlama()
            {
                Id = this.Id,
                Miktar = this.Miktar,
                SiparisId = this.SiparisId,
                Tarih = this.Tarih.Value,
                TezgahId = this.TezgahId,
                TipId = this.TipId,
                AtkiOtelemesi = this.AtkiOtelemesi,
                SiparisNo = this.SozlesmeNo
            };
        }

        internal List<tblPlanlama> ViewToTbl(List<vPlanlama> view)
        {
            List<tblPlanlama> tbl = new List<tblPlanlama>();

            foreach (vPlanlama vItem in view)
            {
                tbl.Add(vItem.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vCozgu")]
    public class vCozgu : IDisposable
    {
        [Column]
        public int TipId { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public string Tip { get; set; }

        [Column]
        public string Varyant { get; set; }

        //public tblCozgu ViewToTbl()
        //{
        //    return new tblCozgu()
        //    {
        //        Miktar = this.Miktar,
        //        TipId = this.TipId
        //    };
        //}

        //public List<tblCozgu> ViewToTbl(List<vCozgu> view)
        //{
        //    List<tblCozgu> tbl = new List<tblCozgu>();

        //    foreach (vCozgu item in view) tbl.Add(item.ViewToTbl());

        //    return tbl;
        //}

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vIplikStok")]
    public class vIplikStok : IDisposable
    {
        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column(Name = "KalanNetKg")]
        public double NetKg { get; set; }

        [Column(Name = "KalanBobinSayisi")]
        public double BobinSayisi { get; set; }

        [Column]
        public bool? LeventteGor { get; set; }

        [Column]
        public double? IplikNo { get; set; }

        [Column]
        public int SaticiId { get; set; }

        [Column]
        public string Satici { get; set; }

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

    public static class vRenkler
    {
        public static List<tblRenkler> RenkleriGetir(bool sadeceAktifler = true)
        {
            if (sadeceAktifler == false) return new DBEvents().GetGeneric<tblRenkler>();
            return new DBEvents().GetGeneric<tblRenkler>(c => c.AktifMi == true);
        }
    }

    [Table(Name = "vLeventHareket")]
    public class vLeventHareket : IDisposable
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
        public string CekenPersonel { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public string Cozgu { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int RenkId { get; set; }

        //[Column]
        public string SaticiAdi { get; set; }

        [Column]
        public double? LeventEni { get; set; }

        [Column]
        public double? LeventCapi { get; set; }

        [Column(Name="TelAdedi")]
        private double? _TelAdedi;
        
        public double? TelAdedi
        {
            get
            {
                return _TelAdedi;
            }
            set
            {
                _TelAdedi = value;
                if (BobinAdedi.HasValue && BobinAdedi.Value != 0)
                {
                    BantSayisi = Math.Round((1 + (value.Value / BobinAdedi.Value)), 2);
                    if (BantSayisiChanged != null) BantSayisiChanged();
                }
                else BantSayisi = null;
            }
        }

        #region Events
        public delegate void BantSayisiEvent();
        public event BantSayisiEvent BantSayisiChanged;        
        #endregion

        [Column(Name="BobinAdedi")]
        private double? _BobinAdedi;

        public double? BobinAdedi
        {
            get
            {
                return _BobinAdedi;
            }
            set
            {
                _BobinAdedi = value;
                if (TelAdedi.HasValue && TelAdedi.Value != 0)
                {
                    BantSayisi = Math.Round((1 + (TelAdedi.Value / value.Value)), 2);
                    if (BantSayisiChanged != null) BantSayisiChanged();
                }
                else BantSayisi = null;
            }
        }

        [Column]
        public double? BobinMetre { get; set; }

        [Column]
        public double? ResteMetre { get; set; }

        [Column]
        public double? BantSayisi { get; set; }

        [Column]
        public int Durum { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? KullanilanMetre { get; set; }

        [Column]
        public string RenkKodu { get; set; }

        [Column]
        public double? DokunanMetre { get; set; }

        public double KalanMetre
        {
            get
            {
                double kalanMetre = (Metre.HasValue == false ? 0 : Metre.Value) - (KullanilanMetre.HasValue == false ? 0 : KullanilanMetre.Value);
                return Math.Round(kalanMetre, 2);
            }
        }

        [Column]
        public DateTime? CozguTarihi { get; set; }

        [Column]
        public DateTime? DugumTarihi { get; set; }

        [Column]
        public DateTime? TamamlanmaTarihi { get; set; }

        [Column]
        public bool IadeMi { get; set; }

        public vLeventHareket CopyToNewObject()
        {
            return (vLeventHareket)this.MemberwiseClone();
        }

        internal tblLeventHareket TblLevent()
        {
            return new tblLeventHareket()
            {
                BantSayisi = this.BantSayisi,
                BobinAdedi = this.BobinAdedi,
                BobinMetre = this.BobinMetre,
                CekenPersonelId = this.CekenPersonelId,
                Cozgu = this.Cozgu,
                Durum = this.Durum,
                Id = this.Id,
                LeventEni = this.LeventEni,
                LeventNo = this.LeventNo,
                Metre = this.Metre,
                RenkId = this.RenkId,
                ResteMetre = this.ResteMetre,
                SetId = this.SetId,
                TelAdedi = this.TelAdedi,
                TezgahId = this.TezgahId,
                TipId = this.TipId,
                Aciklama = this.Aciklama,
                Tarih = this.Tarih,
                LeventCapi = this.LeventCapi,
                CozguTarihi = this.CozguTarihi,
                DugumTarihi = this.DugumTarihi,
                TamamlanmaTarihi = this.TamamlanmaTarihi,
                IadeMi = this.IadeMi
            };
        }

        public static List<tblLeventHareket> ViewToTbl(List<vLeventHareket> view)
        {
            List<tblLeventHareket> tbl = new List<tblLeventHareket>();

            foreach (vLeventHareket item in view) tbl.Add(item.TblLevent());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLeventIplik")]
    public class vLeventIplik : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public long SetId { get; set; }

        [Column]
        public int? SetHareketId { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public double BobinSayisi { get; set; }

        [Column]
        public double NetKg { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public int SaticiId { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public string Satici { get; set; }

        public tblMalzemeCikis TblMalzemeCikis
        {
            get
            {
                return new tblMalzemeCikis()
                {
                    Ambalaj = this.Ambalaj,
                    BirimId = null,
                    BobinSayisi = this.BobinSayisi,
                    CikisTanim = "CCC",
                    CikisTanimId = null,
                    Id = this.Id,
                    LotNo = this.LotNo,
                    MalzemeId = this.MalzemeId,
                    Miktar = null,
                    NetKg = this.NetKg,
                    PersonelId = this.PersonelId,
                    RenkId = this.RenkId,
                    SaticiId = this.SaticiId,
                    SetHareketId = this.SetHareketId,
                    SetId = this.SetId,
                    Tarih = DateTime.Now
                };
            }
        }

        public static List<tblMalzemeCikis> ViewToTblCikis(List<vLeventIplik> view)
        {
            List<tblMalzemeCikis> yeniList = new List<tblMalzemeCikis>();

            foreach (vLeventIplik item in view) yeniList.Add(item.TblMalzemeCikis);

            return yeniList;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vIplikGiris")]
    public class vIplikGiris : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        
        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public int? SaticiId { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column(Name="BobinSayisi")]
        private double? _BobinSayisi;

        public double? BobinSayisi
        {
            get
            {
                return _BobinSayisi;
            }
            set
            {
                _BobinSayisi = value;
                if (this.Metre != null)
                {
                    double? deger = 0;
                    tblMalzemeler malz = new DBEvents().GetGeneric<tblMalzemeler>(c => c.Id == this.MalzemeId).FirstOrDefault();
                    if (malz != null && malz.IplikNo != null) deger = (this.BobinSayisi == null ? 0 : this.BobinSayisi.Value) * (this.Metre == null ? 0 : this.Metre.Value) / malz.IplikNo.Value / 1.693;
                    this.NetKg = deger;
                    NetKgChanged();
                }
            }
        }        

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public double? BrutKg { get; set; }

        #region Events
        public delegate void NetKgEvent();
        public event NetKgEvent NetKgChanged;
        #endregion

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public string GirisTanim { get; set; }

        [Column]
        public int? GirisTanimId { get; set; }

        [Column]
        public string FasonAdi { get; set; }

        [Column]
        public int? PersonelId { get; set; }

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

        [Column(Name = "Metre")]
        private double? _Metre;

        public double? Metre
        {
            get
            {
                return _Metre;
            }
            set
            {
                _Metre = value;
                if (this.BobinSayisi != null)
                {
                    double? deger = 0;
                    tblMalzemeler malz = new DBEvents().GetGeneric<tblMalzemeler>(c => c.Id == this.MalzemeId).FirstOrDefault();
                    if (malz != null && malz.IplikNo != null) deger = (this.BobinSayisi == null ? 0 : this.BobinSayisi.Value) * ((this.Metre == null ? 0 : this.Metre.Value) / (malz.IplikNo.Value * 1.693 * 1000));
                    this.NetKg = deger;
                    this.BrutKg = deger * 1.025;
                    NetKgChanged();
                }
            }
        }

        public tblMalzemeGiris ViewToTbl()
        {
            return new tblMalzemeGiris()
            {
                Ambalaj = this.Ambalaj,
                BirimId = null,
                BobinSayisi = this.BobinSayisi,
                BrutKg = this.BrutKg,
                GirisTanim = this.GirisTanim,
                GirisTanimId = this.GirisTanimId,
                Id = this.Id,
                KarsilamaActId = null,
                LotNo = this.LotNo,
                MalzemeId = this.MalzemeId,
                Miktar = null,
                NetKg = this.NetKg,
                PersonelId = this.PersonelId.Value,
                RenkId = this.RenkId,
                Aciklama = this.Aciklama,
                Tarih = this.Tarih,
                Metre = this.Metre,
                IrsaliyeNo = this.IrsaliyeNo,
                 SaticiId = this.SaticiId
            };
        }

        public static List<tblMalzemeGiris> ViewToTbl(List<vIplikGiris> view)
        {
            List<tblMalzemeGiris> tbl = new List<tblMalzemeGiris>();

            foreach (vIplikGiris item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vIplikCikis")]
    public class vIplikCikis : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int MalzemeId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string LotNo { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public double? BobinSayisi { get; set; }

        [Column]
        public string Ambalaj { get; set; }

        [Column]
        public double? BrutKg { get; set; }

        [Column]
        public double? NetKg { get; set; }

        [Column]
        public int? SaticiId { get; set; }

        [Column]
        public string SaticiAdi { get; set; }

        [Column]
        public string CikisTanim { get; set; }

        [Column]
        public int? CikisTanimId { get; set; }

        [Column]
        public string FasonAdi { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public string PersonelKodu { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        public tblMalzemeCikis ViewToTbl()
        {
            return new tblMalzemeCikis()
            {
                Ambalaj = this.Ambalaj,
                BirimId = null,
                BobinSayisi = this.BobinSayisi,
                BrutKg = this.BrutKg,
                CikisTanim = this.CikisTanim,
                CikisTanimId = this.CikisTanimId,
                Id = this.Id,
                LotNo = this.LotNo,
                MalzemeId = this.MalzemeId,
                Miktar = null,
                NetKg = this.NetKg,
                PersonelId = this.PersonelId,
                RenkId = this.RenkId,
                Aciklama = this.Aciklama,
                Tarih = this.Tarih,
                SaticiId = this.SaticiId
            };
        }

        public static List<tblMalzemeCikis> ViewToTbl(List<vIplikCikis> view)
        {
            List<tblMalzemeCikis> tbl = new List<tblMalzemeCikis>();

            foreach (vIplikCikis item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vHamHatalar : IDisposable
    {
        public int Id { get; set; }

        public string HataKodu { get; set; }

        public string HataAdi { get; set; }

        public double Uzunluk { get; set; }

        public double Metresi { get; set; }

        public int TurId { get; set; }

        public double UstMu { get; set; }

        public double AltMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vHamKumaslarOrmeStok")]
    public class vHamKumaslarOrmeStok : IDisposable
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
        public string TezgahAdi { get; set; }

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

        public string LotNo { get; set; }

        [Column]
        public string HataList { get; set; }

        //Gökhan 21.08.2014
        [Column]
        public int? KafesId { get; set; }

        [Column]
        public string KafesAdi { get; set; }

        [Column]
        public string PlanlananPartiNo { get; set; }

        [Column]
        public int? PartiIdPlanlanan { get; set; }

        [Column]
        public int? KumasCinsiId { get; set; }

        [Column]
        public string KumasCinsi { get; set; }

        [Column]
        public string Depo { get; set; }

        [Column]
        public int? DepoId { get; set; }

        public string TezgahNoAdi
        {
            get
            {
                return (TezgahNo == null ? "" : TezgahNo) + " - " + (TezgahAdi == null ? "" : TezgahAdi);
            }
        }

        public string PartiNo { get; set; }

        public int BeklemeGunu
        {
            get
            {
                return DateTime.Today.Subtract(this.Tarih).Days;
            }
        }

        public vHamKumaslar CopyToNewObject()
        {
            return (vHamKumaslar)this.MemberwiseClone();
        }

        public tblHamKumaslar ViewToTbl()
        {
            return new tblHamKumaslar()
            {
                Aciklama = this.Aciklama,
                Barkod = this.Barkod,
                DokumaciId = this.DokumaciId,
                Gramaj = this.Gramaj,
                HataAdet = this.HataAdet,
                HataPuan = this.HataPuan,
                HavLeventId = this.HavLeventId,
                Id = this.Id,
                KaliteAdet = this.KaliteAdet,
                KaliteAdetDeger = this.KaliteAdetDeger,
                KaliteciId = this.KaliteciId,
                KalitePuan = this.KalitePuan,
                KalitePuanDeger = this.KalitePuanDeger,
                Kg = this.Kg,
                Metre = this.Metre,
                NetMetre = this.NetMetre,
                SiparisId = this.SiparisId,
                Tarih = this.Tarih,
                TezgahId = this.TezgahId,
                TipId = this.TipId,
                Tur = this.Tur,
                Varyant = this.Varyant,
                ZeminAltLeventId = this.ZeminAltLeventId,
                ZeminUstLeventId = this.ZeminUstLeventId,
                PartiId = this.PartiId,
                KafesId = this.KafesId,
                PartiIdPlanlanan = this.PartiIdPlanlanan,
                DepoId = this.DepoId
            };
        }

        public static List<tblHamKumaslar> ViewToTbl(List<vHamKumaslar> view)
        {
            List<tblHamKumaslar> tbl = new List<tblHamKumaslar>();

            foreach (vHamKumaslar item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }


    [Table(Name = "vHamKumaslar")]
    public class vHamKumaslar : IDisposable
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
        public string TezgahAdi { get; set; }

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

        public string LotNo { get; set; }

        [Column]
        public string HataList { get; set; }

        //Gökhan 21.08.2014
        [Column]
        public int? KafesId { get; set; }

        [Column]
        public string KafesAdi { get; set; }

        [Column]
        public string PlanlananPartiNo { get; set; }

        [Column]
        public int? PartiIdPlanlanan { get; set; }

        [Column]
        public int? KumasCinsiId { get; set; }

        [Column]
        public string KumasCinsi { get; set; }

        [Column]
        public string Depo { get; set; }

        [Column]
        public int? DepoId { get; set; }

        [Column]
        public string PlanDurum { get; set; }

        [Column]
        public string KafesDikeyKodu { get; set; }

        [Column]
        public string Kafes_Dikey_Birlikte { get; set; }

        public string TezgahNoAdi
        {
            get
            {
                return (TezgahNo == null ? "" : TezgahNo) + " - " + (TezgahAdi == null ? "" : TezgahAdi);
            }
        }

        public string PartiNo { get; set; }

        public int BeklemeGunu
        {
            get
            {
                return DateTime.Today.Subtract(this.Tarih).Days;
            }
        }

        public vHamKumaslar CopyToNewObject()
        {
            return (vHamKumaslar)this.MemberwiseClone();
        }

        public tblHamKumaslar ViewToTbl()
        {
            return new tblHamKumaslar()
            {
                Aciklama = this.Aciklama,
                Barkod = this.Barkod,
                DokumaciId = this.DokumaciId,
                Gramaj = this.Gramaj,
                HataAdet = this.HataAdet,
                HataPuan = this.HataPuan,
                HavLeventId = this.HavLeventId,
                Id = this.Id,
                KaliteAdet = this.KaliteAdet,
                KaliteAdetDeger = this.KaliteAdetDeger,
                KaliteciId = this.KaliteciId,
                KalitePuan = this.KalitePuan,
                KalitePuanDeger = this.KalitePuanDeger,
                Kg = this.Kg,
                Metre = this.Metre,
                NetMetre = this.NetMetre,
                SiparisId = this.SiparisId,
                Tarih = this.Tarih,
                TezgahId = this.TezgahId,
                TipId = this.TipId,
                Tur = this.Tur,
                Varyant = this.Varyant,
                ZeminAltLeventId = this.ZeminAltLeventId,
                ZeminUstLeventId = this.ZeminUstLeventId,
                PartiId = this.PartiId,
                KafesId = this.KafesId,
                PartiIdPlanlanan = this.PartiIdPlanlanan,
                DepoId = this.DepoId
            };
        }

        public static List<tblHamKumaslar> ViewToTbl(List<vHamKumaslar> view)
        {
            List<tblHamKumaslar> tbl = new List<tblHamKumaslar>();

            foreach (vHamKumaslar item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vGrupProcess")]
    public class vGrupProcess : IDisposable
    {
        [Column]
        public int GrupId { get; set; }

        [Column]
        public int GrupActId { get; set; }

        [Column]
        public string GrupAdi { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool? AktifMi { get; set; }

        [Column]
        public bool FasonMu { get; set; }

        public List<tblProses> Processler { get; set; }

        public static List<vGrupProcess> GrupProcessleriGetir(int grupId)
        {
            return new DBEvents().GetGeneric<vGrupProcess>(c => c.GrupId == grupId && c.AktifMi == true).OrderBy(o=>o.Sira).ToList();
        }

        public static bool GrupProcesSil(vGrupProcess process)
        {
            return new DBEvents().DeleteGeneric<tblProsesGrupAct>(process.ViewToTbl());
        }

        public tblProsesGrupAct ViewToTbl()
        {
            return new tblProsesGrupAct()
            {
                GrupId = this.GrupId,
                Id = this.GrupActId,
                ProcessId = this.ProcessId,
                Sira = this.Sira
            };
        }

        public List<tblProsesGrupAct> ViewToTbl(List<vGrupProcess> view)
        {
            List<tblProsesGrupAct> tbl = new List<tblProsesGrupAct>();
            foreach (vGrupProcess item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        public vPartiProcessleri GrupProcessToPartiProcess(int partiId)
        {
            return new vPartiProcessleri()
            {
                PartiId = partiId,
                ProcessAdi = this.ProcessAdi,
                ProcessId = this.ProcessId,
                Sira = this.Sira
            };
        }

        public static List<vPartiProcessleri> GrupProcessToPartiProcess(List<vGrupProcess> grupProcess, int partiId)
        {
            List<vPartiProcessleri> partiProc = new List<vPartiProcessleri>();

            foreach (vGrupProcess item in grupProcess) partiProc.Add(item.GrupProcessToPartiProcess(partiId));

            return partiProc;
        }

        public static bool GrupProcessleriKaydet(List<vGrupProcess> grupProcessleri)
        {
            vGrupProcess fason = grupProcessleri.Find(c => c.FasonMu == true);
            if (fason != null)
            {
                if (grupProcessleri.Exists(c => c.Sira > fason.Sira))
                    throw new Exception("Fason proses sırasından sonra proses eklenemez..!");
            }

            List<tblProsesGrupAct> saveTbl = new vGrupProcess().ViewToTbl(grupProcessleri.FindAll(c => c.GrupActId == 0));
            List<tblProsesGrupAct> updTbl = new vGrupProcess().ViewToTbl(grupProcessleri.FindAll(c => c.GrupActId != 0));

            bool snc = true;

            if (new DBEvents().SaveGeneric<tblProsesGrupAct>(saveTbl) == false) snc = false;
            if (new DBEvents().UpdateGeneric<tblProsesGrupAct>(updTbl) == false) snc = false;

            return snc;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPartiProcessleri")]
    public class vPartiProcessleri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        
        [Column]
        public int PartiId { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public string ProcessKodu { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public int Sira { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public string ProcessBarkod { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public string PartiNo { get; set; }

        public tblPartiProsesleri ViewToTbl()
        {
            return new tblPartiProsesleri()
            {
                Id = this.Id,
                PartiId = this.PartiId,
                ProcessId = this.ProcessId,
                Sira = this.Sira
            };
        }

        public static List<tblPartiProsesleri> ViewToTbl(List<vPartiProcessleri> view)
        {
            List<tblPartiProsesleri> tbl = new List<tblPartiProsesleri>();

            foreach (vPartiProcessleri item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPartiler")]
    public class vPartiler : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int PlanlayanId { get; set; }

        [Column]
        public string PlanlayanAdi { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

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
        public string OnaylayanAdi { get; set; }

        [Column]
        public DateTime? BoyamaTarihi { get; set; }

        [Column]
        public bool BoyahaneOnay { get; set; }

        [Column]
        public string BoyahaneNot { get; set; }

        [Column]
        public bool FarkliSiparisKabul { get; set; }

        [Column]
        public bool RePartiMi { get; set; }

        [Column]
        public bool PaketlendiMi { get; set; }

        [Column]
        public bool BoyandiMi { get; set; }

        public event EventHandler FinishKodChanged;

        [Column(Name = "FinishKodId")]
        private int _FinishKodId;

        public int FinishKodId
        {
            get { return _FinishKodId; }
            set
            {
                _FinishKodId = value;
                if (FinishKodChanged != null) FinishKodChanged(this, EventArgs.Empty);
            }
        }

        [Column]
        public string FinishKodu { get; set; }

        [Column]
        public double AcilmisMetre { get; set; }

        [Column]
        public double TartilanKg { get; set; }

        [Column]
        public bool? ReProcessVarMi { get; set; }

        [Column]
        public bool? ProcessOkumaHizliMi { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public int? ApreId { get; set; }

        [Column]
        public string En { get; set; }

        [Column]
        public bool? BoyaProgIptal { get; set; }

        [Column]
        public string BoyaProgIptalNedeni { get; set; }

        [Column]
        public int? MakinaId { get; set; }
        [Column]
        public string BoyaNotu { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public string ApreKodu { get; set; }

        public tblPartiler ViewToTbl()
        {
            return new tblPartiler()
            {
                Aciklama = this.Aciklama,
                BoyahaneNot = this.BoyahaneNot,
                BoyahaneOnay = this.BoyahaneOnay,
                BoyamaTarihi = this.BoyamaTarihi,
                DigerTipNo1 = this.DigerTipNo1,
                DigerTipNo2 = this.DigerTipNo2,
                DigerTipNo3 = this.DigerTipNo3,
                FarkliSiparisKabul = this.FarkliSiparisKabul,
                FinishKodId = this.FinishKodId,
                Id = this.Id,
                MusteriId = this.MusteriId,
                OnaylayanId = this.OnaylayanId,
                PartiMetre = this.PartiMetre,
                PartiNo = this.PartiNo,
                PlanlayanId = this.PlanlayanId,
                RenkNo = this.RenkNo,
                RenkVaryant = this.RenkVaryant,
                //SiparisId = this.SiparisId,
                Tarih = this.Tarih,
                Tipi = this.Tipi,
                //TipId = this.TipId,
                TipVaryant = this.TipVaryant,
                ProcessOkumaHizliMi = this.ProcessOkumaHizliMi,
                ReProcessVarMi = this.ReProcessVarMi,
                SiparisActId = this.SiparisActId,
                ApreId = this.ApreId,
                RePartiMi = this.RePartiMi,
                BoyandiMi = this.BoyandiMi,
                PaketlendiMi = this.PaketlendiMi,
                BoyaProgIptal = this.BoyaProgIptal,
                BoyaProgIptalNedeni = this.BoyaProgIptalNedeni,
                MakinaId = this.MakinaId
            };
        }

        

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vBoyahaneProcess")]
    public class vBoyahaneProcess : IDisposable
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

        //Gökhan 16.05.2014
        [Column]
        public int? ArizaId { get; set; }

        [Column]
        public int? MakinaId { get; set; }

        [Column]
        public bool? Durdu { get; set; }

        [Column]
        public bool? Silindi { get; set; }

        [Column]
        public string SilindiMi { get; set; }

        [Column]
        public string ReProcessMi { get; set; }

        [Column]
        public string DurduMu { get; set; }

        [Column]
        public string ArizaAdi { get; set; }

        [Column]
        public string MakinaAdi { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? ProcessDk { get; set; }
        
        public tblBoyahaneProcess ViewToTbl()
        {
            return new tblBoyahaneProcess()
            {
                Id = this.Id,
                Metre = this.Metre,
                PartiId = this.PartiId,
                PersonelId = this.PersonelId,
                ProcessId = this.ProcessId,
                Saat = this.Saat,
                Sira = this.Sira,
                Tarih = this.Tarih,
                ReProcess = this.ReProcess,
                Aciklama = this.Aciklama,
                CikisTarih = this.CikisTarih,
                CikisSaat = this.CikisSaat,
                ArizaId = this.ArizaId,
                MakinaId = this.MakinaId,
                Durdu = this.Durdu,
                Silindi = this.Silindi
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKimyasalRecete")]
    public class vKimyasalRecete : IDisposable
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
        public string PersonelAdi { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string RenkKodu { get; set; }

        [Column]
        public string RenkAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public bool AktifMi { get; set; }

        [Column]
        public bool StoktanDusulduMu { get; set; }

        [Column]
        public int? MakinaId { get; set; }

        [Column]
        public bool TopluRecete { get; set; }

        [Column]
        public bool NuanslariGuncelledim { get; set; }      


        public tblKimyasalRecete ViewToTbl()
        {
            return new tblKimyasalRecete()
            {
                Id = this.Id,
                Makina = this.Makina,
                PersonelId = 110, //this.PersonelId, //this.Program, Kullanıcı rastgele isim seçtiği için iş yüküne dönüşmüş. Bu yüzden kaldırıldı.
                Program = "1",//this.Program, Kullanıcı rastgele isim seçtiği için iş yüküne dönüşmüş. Bu yüzden kaldırıldı.
                ReceteNo = this.ReceteNo,
                RenkId = this.RenkId,
                Tarih = this.Tarih,
                Aciklama = this.Aciklama,
                AktifMi = this.AktifMi,
                StoktanDusulduMu = this.StoktanDusulduMu,
                MakinaId = this.MakinaId,
                TopluRecete = this.TopluRecete,
                NuanslariGuncelledim = this.NuanslariGuncelledim
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKimyasalRecetePartileri")]
    public class vKimyasalRecetePartileri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        
        [Column]
        public int ReceteId { get; set; }

        [Column]
        public int PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public double TartilanKg { get; set; }

        [Column]
        public double AcilmisMetre { get; set; }

        [Column]
        public string MusteriKodu { get; set; }

        public string RenkNo { get; set; }
        public string TipNo { get; set; }
        public string SiparisNo { get; set; }
        public string Firma { get; set; }

        public tblKimyasalRecetePartileri ViewToTbl()
        {
            return new tblKimyasalRecetePartileri()
            {
                Id = this.Id,
                PartiId = this.PartiId,
                ReceteId = this.ReceteId,
                TartilanKg = this.TartilanKg
            };
        }

        public static List<tblKimyasalRecetePartileri> ViewToTbl(List<vKimyasalRecetePartileri> view)
        {
            List<tblKimyasalRecetePartileri> tbl = new List<tblKimyasalRecetePartileri>();

            foreach (vKimyasalRecetePartileri item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKimyasalReceteAct")]
    public class vKimyasalReceteAct : IDisposable
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
        public string KimyasalKodu { get; set; }

        [Column]
        public string KimyasalAdi { get; set; }

        [Column]
        public string Tip { get; set; }

        [Column]
        public double Flote { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public int? OnayBirPersonelId { get; set; }

        [Column]
        public int? OnayIkiPersonelId { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }

        [Column]
        public string OnayBirPersonelAdi { get; set; }

        [Column]
        public string OnayIkiPersonelAdi { get; set; }

        public List<tblMalzemeler> ListKimyasallar { get; set; }

        public tblKimyasalReceteAct ViewToTable()
        {
            return new tblKimyasalReceteAct()
            {
                GrLtOran = this.GrLtOran,
                Id = this.Id,
                KimyasalId = this.KimyasalId,
                Miktar = this.Miktar,
                Oran = this.Oran,
                ReceteId = this.ReceteId,
                Tip = this.Tip,
                Flote = this.Flote,
                PersonelId = this.PersonelId,
                //OnayBir = this.OnayBirPersonelId,
                OnayBir = (this.OnayBirPersonelId==null)?3:this.OnayBirPersonelId, //Onaya düşmesini engellemek için yaptım.
                //OnayIki = this.OnayIkiPersonelId,
                OnayIki = (this.OnayIkiPersonelId==null)?3:this.OnayIkiPersonelId,
                OnayBirTarih = this.OnayBirTarih,
                OnayIkiTarih = this.OnayIkiTarih
            };
        }

        public static List<tblKimyasalReceteAct> ViewToTable(List<vKimyasalReceteAct> view)
        {
            List<tblKimyasalReceteAct> table = new List<tblKimyasalReceteAct>();

            foreach (vKimyasalReceteAct item in view)
            {
                table.Add(item.ViewToTable());
            }

            return table;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 16.05.2014
    [Table(Name = "vKimyasalReceteActLog")]
    public class vKimyasalReceteActLog : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string ReceteNo { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Islem { get; set; }

        [Column]
        public double? OranE { get; set; }

        [Column]
        public double? OranY { get; set; }

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
        public DateTime Tarih { get; set; }

        [Column]
        public DateTime Saat { get; set; }

        [Column]
        public string PersonelE { get; set; }

        [Column]
        public string PersonelY { get; set; }

        [Column]
        public string KimyasalE { get; set; }

        [Column]
        public string KimyasalY { get; set; }

        [Column]
        public int? OnayBirPersonelId { get; set; }

        [Column]
        public int? OnayIkiPersonelId { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }

        [Column]
        public string OnayBirPersonelAdi { get; set; }

        [Column]
        public string OnayIkiPersonelAdi { get; set; }

        [Column]
        public int SilinenId { get; set; }

        [Column]
        public int? ReceteId { get; set; }

        [Column]
        public int? PersonelIdE { get; set; }

        [Column]
        public int? PersonelIdY { get; set; }

        [Column]
        public int? KimyasalIdE { get; set; }

        [Column]
        public int? KimyasalIdY { get; set; }

        [Column]
        public int? Turu { get; set; }

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
        public string logTuruAdi { get; set; }

        [Column]
        public int? RenkId { get; set; }

        [Column]
        public string KumasRenkKodu { get; set; }

        //public List<tblMalzemeler> ListKimyasallar { get; set; }

        public tblKimyasalReceteActLog ViewToTable()
        {
            return new tblKimyasalReceteActLog()
            {
                Id = this.Id,
                SilinenId = this.SilinenId,
                ReceteId = this.ReceteId,
                OranE = this.OranE,
                OranY=this.OranY,
                GrLtOranE=this.GrLtOranE,
                GrLtOranY=this.GrLtOranY,
                MiktarE = this.MiktarE,
                MiktarY = this.MiktarY,
                TipE=this.TipE,
                TipY=this.TipY,
                FloteE= this.FloteE,
                FloteY=this.FloteY,
                Turu = this.Turu,
                Tarih = this.Tarih,
                Saat = this.Saat,
                PersonelIdE = this.PersonelIdE,
                PersonelIdY = this.PersonelIdY,
                KimyasalIdE = this.KimyasalIdE,
                KimyasalIdY = this.KimyasalIdY,
                OnayBir = this.OnayBirPersonelId,
                OnayIki= this.OnayIkiPersonelId,
                OnayBirTarih = this.OnayBirTarih,
                OnayIkiTarih= this.OnayIkiTarih,
                RAciklamaE = this.RAciklamaE,
                RAciklamaY = this.RAciklamaY,
                RBoyaKimyaE = this.RBoyaKimyaE,
                RBoyaKimyaY = this.RBoyaKimyaY,
                logTuru = this.logTuru,
                RenkId = this.RenkId
            };
        }

        public static List<tblKimyasalReceteActLog> ViewToTable(List<vKimyasalReceteActLog> view)
        {
            List<tblKimyasalReceteActLog> table = new List<tblKimyasalReceteActLog>();

            foreach (vKimyasalReceteActLog item in view)
            {
                table.Add(item.ViewToTable());
            }

            return table;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vKumasRenkAct")]
    public class vKumasRenkAct : IDisposable
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

        [Column]
        public string KimyasalKodu { get; set; }

        [Column]
        public string KimyasalAdi { get; set; }

        //Gökhan 16.05.2014
        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public int? OnayBirPersonelId { get; set; }

        [Column]
        public int? OnayIkiPersonelId { get; set; }

        [Column]
        public string OnayBirPersonelAdi { get; set; }

        [Column]
        public string OnayIkiPersonelAdi { get; set; }

        [Column]
        public DateTime? OnayBirTarih { get; set; }

        [Column]
        public DateTime? OnayIkiTarih { get; set; }

        public tblKumasRenkAct ViewToTable()
        {
            return new tblKumasRenkAct()
            {
                Aciklama = this.Aciklama,
                BoyaKimya = this.BoyaKimya,
                GrOran = this.GrOran,
                Id = this.Id,
                KimyasalId = this.KimyasalId,
                Miktar = this.Miktar,
                RenkId = this.RenkId,
                OnayBir = this.OnayBirPersonelId,
                OnayIki = this.OnayIkiPersonelId,
                OnayBirTarih = this.OnayBirTarih,
                OnayIkiTarih = this.OnayIkiTarih,
                PersonelId = this.PersonelId                
            };
        }

        public static List<tblKumasRenkAct> ViewToTbl(List<vKumasRenkAct> view)
        {
            List<tblKumasRenkAct> tbl = new List<tblKumasRenkAct>();

            foreach (vKumasRenkAct item in view)
            {
                tbl.Add(item.ViewToTable());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLaboratuvarTest")]
    public class vLaboratuvarTest : IDisposable
    {        
        [Column]
        public int PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public int TestId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public bool TestYapildiMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMamulKumaslar")]
    public class vMamulKumaslar : IDisposable
    {
        [Column]
        public int Id { get; set; }
        
        [Column]
        public string Barkod { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public string MusteriKodu { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string Finish { get; set; }

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
        public string KaliteciKodu { get; set; }

        [Column]
        public string KaliteciAdi { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string SevkiyatNotu { get; set; }

        [Column]
        public int? HamId { get; set; }

        [Column]
        public string HamBarkod { get; set; }

        [Column]
        public string FasonTipi { get; set; }

        [Column]
        public int? SiparisId { get; set; }

        [Column]
        public int? SiparisActId { get; set; }

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
        public int? IadeSipId { get; set; }

        [Column]
        public bool SevkEdilebilir { get; set; }

        [Column]
        public int? RePartiId { get; set; }

        [Column]
        public int AnaMamulId { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string IadeAciklama { get; set; }

        [Column]
        public string DyeBatchNo { get; set; }

        [Column]
        public string HataList { get; set; }

        [Column]
        public int? SevkSiparisActId { get; set; }
        [Column]
        public string RefTipNo { get; set; }
        [Column]
        public string RefRenkAdi { get; set; }
        [Column]
        public string RefRenkNo { get; set; }
        [Column]
        public string TurEng { get; set; }
        [Column]
        public string KumasinFasonBilgisi { get; set; }
        [Column]
        public string KoleksiyonAdi { get; set; }
       
        public double ToplamKalan { get; set; }
        public double HamKalan { get; set; }
        public string RezerveMusterisi { get; set; }
        public bool BarkodBirlestirmeSecim { get; set; }
        public string LotNo { get; set; }
        public bool SevkEdildi
        {
            get
            {
                if (this.SevkId.HasValue == false || this.SevkId.Value == 0) return false;

                else return true;
            }
        }
        public string SevkEmri { get; set; }

        public int BeklemeGunu
        {
            get
            {
                return DateTime.Today.Subtract(this.Tarih).Days;
            }
        }

        /// <summary>
        /// Ham kumaş barkodu da olabilir,reprocess barkodu da
        /// </summary>
        public object SecilenBarkod { get; set; }

        public tblMamulKumaslar ViewToTable()
        {
            return new tblMamulKumaslar()
            {
                Barkod = this.Barkod,
                HataAdet = this.HataAdet,
                HataPuan = this.HataPuan,
                Id = this.Id,
                KaliteAdet = this.KaliteAdet,
                KaliteAdetDeger = this.KaliteAdetDeger,
                KalitePuan = this.KalitePuan,
                KalitePuanDeger = this.KalitePuanDeger,
                Metre = this.Metre,
                PartiId = this.PartiId,
                KaliteciId = this.KaliteciId,
                Tarih = this.Tarih,
                Tur = this.Tur,
                Aciklama = this.Aciklama,
                SevkiyatNotu = this.SevkiyatNotu,
                Finish = this.Finish,
                HamId = this.HamId,
                En = this.En, 
                NetMetre = this.NetMetre,
                RezerveSiparisActId = this.RezerveSiparisActId,
                SevkId = this.SevkId,
                Parca = this.Parca,
                ParcaMetre = this.ParcaMetre,
                KutuId = this.KutuId,
                Kg = this.Kg,
                TipId = this.TipId,
                RenkNo = this.RenkNo,
                AnaMamulId = this.AnaMamulId,
                Durum = this.Durum,
                RePartiId = this.RePartiId,
                SevkEdilebilir = this.SevkEdilebilir,
                IadeSipId = this.IadeSipId,
                DesenNo = this.DesenNo,
                DyeBatchNo = this.DyeBatchNo,
                SevkSiparisActId = this.SevkSiparisActId
            };
        }

        public static List<tblMamulKumaslar> ViewToTable(List<vMamulKumaslar> view)
        {
            List<tblMamulKumaslar> tbl = new List<tblMamulKumaslar>();

            foreach (vMamulKumaslar item in view)
            {
                tbl.Add(item.ViewToTable());
            }

            return tbl;
        }

        public vMamulKumaslar CopyToNewObject()
        {
            return (vMamulKumaslar)this.MemberwiseClone();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMamuleHazirPartiler")]
    public class vMamuleHazirPartiler : IDisposable
    {
        [Column]
        public int PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriKodu { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public int? FinishKodId { get; set; }

        [Column]
        public string FinishNo { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public bool RePartiMi { get; set; }

        [Column]
        public double? TopMetre { get; set; }
        [Column]
        public string RefTipNo { get; set; }
        [Column]
        public string RefRenkAdi { get; set; }
        [Column]
        public string RefRenkNo { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vHataTanim")]
    public class vHataTanim : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int HataYerBagId { get; set; }

        [Column]
        public int HataSebepBagId { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        public string KodAd { get { return Kodu + " - " + Adi; } }

        [Column]
        public bool AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSevk")]
    public class vSevk : IDisposable
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
        public string SevkEdenKodu { get; set; }

        [Column]
        public string SevkEdenAdi { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriKodu { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public bool FarkliSiparisOkut { get; set; }

        [Column]
        public bool LogoAktarildiMi { get; set; }

        [Column]
        public bool TipRenkKontrolDevreDisi { get; set; }

        public tblSevk ViewToTbl()
        {
            return new tblSevk()
            {
                Aciklama = this.Aciklama,
                FarkliSiparisOkut = this.FarkliSiparisOkut,
                Id = this.Id,
                LogoAktarildiMi = this.LogoAktarildiMi,
                MusteriId = this.MusteriId,
                SevkEdenId = this.SevkEdenId,
                SiparisId = this.SiparisId,
                Tarih = this.Tarih,
                BelgeNo = this.BelgeNo,
                TipRenkKontrolDevreDisi = this.TipRenkKontrolDevreDisi
            };
        }

        public static List<tblSevk> ViewToTbl(List<vSevk> view)
        {
            List<tblSevk> tbl = new List<tblSevk>();

            foreach (vSevk item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vYetkiMenu")]
    public class vYetkiMenu : IDisposable
    {
        [Column]
        public int Id { get; set; }
        
        [Column]
        public int BaglantiId { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string Deger { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public bool? GosterilsinMi { get; set; }

        [Column]
        public bool BosGecilebilirMi { get; set; }

        [Column]
        public int Sira { get; set; }

        [Column]
        public bool? KontrolMu { get; set; }

        [Column]
        public int? BolumId { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public int YetkiId { get; set; }

        [Column]
        public int MenuId { get; set; }

        [Column]
        public bool YetkiVarMi { get; set; }

        public List<vYetkiMenu> MenuItems { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vCozguIsEmri")]
    public class vCozguIsEmri : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public DateTime Tarih { get; set; }
        
        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public string Cozgu { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double DokumaMetre { get; set; }

        [Column]
        public int PersonelId { get; set; }

        [Column]
        public int? IplikId { get; set; }

        [Column]
        public double? IplikTelAdedi { get; set; }

        [Column]
        public string IplikKodu { get; set; }

        [Column]
        public string IplikAdi { get; set; }

        [Column]
        public long Islem { get; set; }

        public double HavMetre { get; set; }
        public double AltZeminMetre { get; set; }
        public double UstZeminMetre { get; set; }

        public List<tblMakinalar> Tezgahlar { get; set; }

        public tblCozguIsEmri ViewToTbl()
        {
            return new tblCozguIsEmri()
            {
                Cozgu = this.Cozgu,
                Id = this.Id,
                Metre = this.Metre,
                TipId = this.TipId,
                Tarih = this.Tarih,
                TezgahId = this.TezgahId,
                DokumaMetre =this.DokumaMetre,
                PersonelId = this.PersonelId,
                Islem = this.Islem
            };
        }

        public static List<tblCozguIsEmri> ViewToTbl(List<vCozguIsEmri> view)
        {
            List<tblCozguIsEmri> tbl = new List<tblCozguIsEmri>();

            foreach (vCozguIsEmri item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanRezerveUygunlar")]
    public class vPlanRezerveUygunlar : IDisposable
    {
        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public double Metre { get; set; }

        private string _Firma = "LK";
        public string Firma { get { return _Firma; } }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanRezerveler")]
    public class vPlanRezerveler : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public double RezerveMetre { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        public tblPlanRezerve ViewToTbl()
        {
            return new tblPlanRezerve()
            {
                Id = this.Id,
                KalitePuan = this.KalitePuan,
                MusteriId = this.MusteriId,
                RezerveMetre = this.RezerveMetre,
                Tarih = this.Tarih,
                TipId = this.TipId
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanlananTipMiktarlari")]
    public class vPlanlananTipMiktarlari : IDisposable
    {
        [Column]
        public int TipId { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public double PlanMiktar { get; set; }

        public string Firma { get { return "LK"; } }

        [Column]
        public string TipNo { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanSiparisleri")]
    public class vPlanSiparisleri : IDisposable
    {
        [Column]
        public int SiparisActId { get; set; }
        
        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string FirmaAdi { get; set; }

        [Column]
        public int FirmaId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double Miktar { get; set; }

        [Column]
        public double CozguMetre { get; set; }

        [Column]
        public double PlanMetre { get; set; }

        [Column]
        public double RezerveMetre { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        public double PlanSiparisMetre
        {
            get
            {
                if (this.RezerveMetre == null) return Miktar;
                else return Miktar - RezerveMetre;
            }
        }

        [Column]
        public bool PartilendiMi { get; set; }

        [Column]
        public bool PaketlendiMi { get; set; }

        [Column]
        public bool? BoyandiMi { get; set; }

        [Column]
        public string BoyaNotu { get; set; }

        [Column]
        public string DokumaNotu { get; set; }

        [Column]
        public string PlanlamaNotu { get; set; }

        [Column]
        public string NumuneNotu { get; set; }

        [Column]
        public string TerminNotu { get; set; }

        /// <summary>
        /// Sipariş planlama esnasında kalan miktarın hesaplanmasında kullanılıyor.
        /// </summary>
        public double KalanMetre;

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanSiparisleri2")]
    public class vPlanSiparisleri2 : IDisposable
    {
        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public string Musteri { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public double PlanMetre { get; set; }
            
        [Column]
        public double DokunanMetre { get; set; }

        [Column]
        public double CozguMetre { get; set; }

        public double KalanMetre;

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vHamHataHaritasi")]
    public class vHamHataHaritasi : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public double Uzunluk { get; set; }

        [Column]
        public int HataId { get; set; }

        [Column]
        public double Metresi { get; set; }

        [Column]
        public int? UstId { get; set; }

        [Column]
        public int? AltId { get; set; }

        [Column]
        public string HataKodu { get; set; }

        [Column]
        public string HataAdi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vMamulHataHaritasi")]
    public class vMamulHataHaritasi : IDisposable
    {
        [Column]
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
        public string HataAdi { get; set; }

        [Column]
        public int MamulId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPaketListesi")]
    public class vPaketListesi : IDisposable
    {
        [Column]
        public int Id { get; set; }
        
        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAdi { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public int SevkId { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public string OrderNo { get; set; }

        [Column]
        public double? TopMetre { get; set; }

        [Column]
        public int? KutuId { get; set; }

        [Column]
        public double Kg { get; set; }

        public double BrutKg { get; set; }

        [Column]
        public double? NetAgirlik { get; set; }

        [Column]
        public double? BrutAgirlik { get; set; }

        [Column]
        public double? En { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vSiparisMamulleri")]
    public class vSiparisMamulleri : IDisposable
    {
        [Column]
        public int SiparisId { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public double MamulMetre { get; set; }

        [Column]
        public double NetAgirlik { get; set; }

        [Column]
        public double BrutAgirlik { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string TipAcikAdi { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double BirimFiyat { get; set; }

        [Column]
        public string Doviz { get; set; }

        public double Tutar { get { return Math.Round(BirimFiyat * MamulMetre, 2); } }

        [Column]
        public int MamulAdet { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vIadeler")]
    public class vIadeler : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int? SipId { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public int RenkId { get; set; }

        [Column]
        public string RenkNo { get; set; }

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
        public string PersonelAdi { get; set; }

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

        public tblIadeler ViewToTbl()
        {
            return new tblIadeler()
            {
                Barkod = this.Barkod,
                En = this.En,
                IadeSebebi = this.IadeSebebi,
                Id = this.Id,
                Kalite = this.Kalite,
                Metre = this.Metre,
                OlusturanId = this.OlusturanId,
                PersonelId = this.PersonelId,
                RenkId = this.RenkId,
                SipId = this.SipId,
                Tarih = this.Tarih,
                TipId = this.TipId,
                Varyant = this.Varyant,
                SevkEdilebilir = this.SevkEdilebilir,
                PartiId = this.PartiId,
                Durum = this.Durum, 
                SevkId = this.SevkId,
                KutuId = this.KutuId
                  
            };
        }

        public static List<tblIadeler> ViewToTbl(List<vIadeler> view)
        {
            List<tblIadeler> tbl = new List<tblIadeler>();

            foreach (vIadeler item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vMamulOnay : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public double? Kg { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string Kalite { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public bool SevkEdilebilir { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public string HataList { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int SevkId { get; set; }

        [Column]
        public string Ayirac { get; set; }

        [Column]
        public string IadeAciklama { get; set; }

        [Column]
        public string HamTezgahKodu { get; set; }

        [Column]
        public string HamTezgahAdi { get; set; }

        public tblIadeler MamulOnayToIade()
        {
            return new DBEvents().GetGeneric<tblIadeler>(c => c.Id == this.Id).FirstOrDefault();
        }

        public static List<tblIadeler> MamulToIade(List<vMamulOnay> view)
        {
            List<tblIadeler> tbl = new List<tblIadeler>();

            foreach (vMamulOnay item in view)
            {
                tbl.Add(item.MamulOnayToIade());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vReProcessBarkodlari : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public string IadeSebebi { get; set; }

        [Column]
        public int? PersonelId { get; set; }

        [Column]
        public string PersonelAdi { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int OlusturanId { get; set; }

        [Column]
        public bool SevkEdilebilir { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public int? SevkId { get; set; }

        [Column]
        public string Ayirac { get; set; }

        [Column]
        public string Tur { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vSevkiyatBarkodlari : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public string KalitePuan { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public string Tur { get; set; }

        [Column]
        public string Parca { get; set; }

        [Column]
        public string ParcaMetre { get; set; }

        [Column]
        public double? Kg { get; set; }

        [Column]
        public string Durum { get; set; }

        [Column]
        public int? KutuId { get; set; }

        [Column]
        public string Ayirac { get; set; }

        [Column]
        public int? RezerveSiparisActId { get; set; }

        [Column]
        public string TipVaryant { get; set; }

        [Column]
        public string RenkVaryant { get; set; }

        [Column]
        public string FinishKodu { get; set; }

        [Column]
        public string DyeBatchNo { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public int? SevkSiparisActId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vPlanRapor")]
    public class vPlanRapor : IDisposable
    {
        [Column]
        public int? SiparisId { get; set; }

        [Column]
        public string SiparisNo { get; set; }

        [Column]
        public int? FirmaId { get; set; }

        [Column]
        public string Musteri { get; set; }

        [Column]
        public int? TezgahId { get; set; }

        [Column]
        public string Tezgah { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double? Planlanan { get; set; }

        [Column]
        public double? DokunanMetre { get; set; }

        [Column]
        public double? SiparisMetre { get; set; }

        public double DokunacakMetre
        {
            get
            {
                double sipMetre = SiparisMetre.HasValue ? SiparisMetre.Value : 0;
                double dokunan = DokunanMetre.HasValue ? DokunanMetre.Value : 0;

                return sipMetre - dokunan;
            }
        }

        [Column]
        public string Durum { get; set; }

        [Column]
        public DateTime SonPlanTarihi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vTezgahArizalari")]
    public class vTezgahArizalari : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int TezgahId { get; set; }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public int ArizaId { get; set; }

        [Column]
        public string ArizaKodu { get; set; }

        [Column]
        public string ArizaAdi { get; set; }

        [Column]
        public DateTime? BaslangicTarihi { get; set; }

        [Column]
        public DateTime? BitisTarihi { get; set; }

        [Column]
        public string Aciklama { get; set; }

        [Column]
        public string Postasi { get; set; }

        [Column]
        public string Fark { get; set; }

        [Column]
        public int? FarkDakika { get; set; }

        public tblTezgahArizalari ViewToTbl()
        {
            return new tblTezgahArizalari()
            {
                Aciklama = this.Aciklama,
                ArizaId = this.ArizaId,
                BaslangicTarihi = this.BaslangicTarihi,
                BitisTarihi = this.BitisTarihi,
                Id = this.Id, 
                Postasi = this.Postasi, 
                TezgahId = this.TezgahId
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vTezgahAtkiGiris")]
    public class vTezgahAtkiGiris : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column(Name = "TezgahId")]
        private int _TezgahId;

        public int TezgahId
        {
            get { return _TezgahId; }
            set
            {
                _TezgahId = value;
                tblTezgahAtkiGiris sonGiris = new DBEvents().GetGenericWithSQLQuery<tblTezgahAtkiGiris>("select * from tblTezgahAtkiGiris where TezgahId = " + _TezgahId.ToString() + " order by Tarih desc", new string[0]).FirstOrDefault();
                if (sonGiris != null) AtkiBaslangic = sonGiris.AtkiBitis;
                else AtkiBaslangic = 0;
            }
        }

        [Column]
        public string TezgahKodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int AtkiBaslangic { get; set; }

        [Column]
        public int AtkiBitis { get; set; }

        [Column]
        public int Fark { get; set; }

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
        [Column]
        public string Dokumaci { get; set; }

        public tblTezgahAtkiGiris ViewToTbl()
        {
            return new tblTezgahAtkiGiris()
            {
                Aciklama = this.Aciklama,
                AtkiBaslangic = this.AtkiBaslangic,
                AtkiBitis = this.AtkiBitis,
                Id = this.Id,
                Postasi = this.Postasi,
                Tarih = this.Tarih,
                TezgahId = this.TezgahId,
                TipId = this.TipId,
                PlanOteledi = this.PlanOteledi,
                DokumaciId=this.DokumaciId
            };
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table]
    public class vBoyaProgrami : IDisposable
    {
        [Column]
        public int PartiId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public DateTime Tarih { get; set; }

        [Column]
        public int PlanlayanId { get; set; }

        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public int SiparisId { get; set; }

        [Column]
        public string SozlesmeNo { get; set; }

        [Column]
        public int? TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

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
        public bool RePartiMi { get; set; }

        [Column]
        public bool PaketlendiMi { get; set; }

        [Column]
        public bool BoyandiMi { get; set; }

        [Column]
        public string FinishKodu { get; set; }

        [Column]
        public double AcilmisMetre { get; set; }

        [Column]
        public bool? ReProcessVarMi { get; set; }

        [Column]
        public bool? ProcessOkumaHizliMi { get; set; }

        [Column]
        public int SiparisActId { get; set; }

        [Column]
        public DateTime? TerminTarihi { get; set; }

        [Column]
        public int? ApreId { get; set; }

        [Column]
        public string En { get; set; }

        [Column]
        public bool? BoyaProgIptal { get; set; }

        [Column]
        public string BoyaProgIptalNedeni { get; set; }

        [Column]
        public int? MakinaId { get; set; }

        [Column]
        public bool? TmpDokumaTerminiGeciktiMi { get; set; }

        [Column]
        public bool? TmpBoyaTerminiGeciktiMi { get; set; }

        [Column]
        public bool? DokumaTerminiGeciktiMi { get; set; }

        [Column]
        public DateTime? DokumaTerminiGecikmeTarihi { get; set; }

        [Column]
        public bool? BoyamaTerminiGeciktiMi { get; set; }

        [Column]
        public DateTime? BoyamaTerminiGecikmeTarihi { get; set; }

        [Column]
        public bool? PartilendiMi { get; set; }

        [Column]
        public string AOK { get; set; }

        [Column]
        public string SimdikiProcessAdi { get; set; }

        [Column]
        public string SonrakiProcessAdi { get; set; }

        [Column]
        public DateTime? ProcessTarihi { get; set; }

        [Column]
        public double SevkMetre { get; set; }

        [Column]
        public double SiparisMetre { get; set; }

        [Column]
        public double? MamuldenCikanBrutMetre { get; set; }

        [Column]
        public int? TerminHaftasi { get; set; }

        //25 Mart 2016 Şükrü Öçal
        [Column]
        public bool? BoyaProgaminaAlindiMi { get; set; }

        public bool DahaOnceBoyaPrograminaAlindiMi()
        {
            if (this.BoyaProgaminaAlindiMi.HasValue == true && this.BoyaProgaminaAlindiMi.Value) return true;

            DBEvents db = new DBEvents();
            tblBoyaProgrami boyaProgrami = db.GetGeneric<tblBoyaProgrami>(x => x.PartiId == this.PartiId).FirstOrDefault();
            if (boyaProgrami != null) return true;
            else return false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSayimMamul")]
    public class vSayimMamul : IDisposable
    {
        [Column]
        public int MamulId { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public int SayimIndisi { get; set; }

        public static List<vSayimMamul> MamulSayimlariGetir()
        {
            return new DBEvents().GetGeneric<vSayimMamul>().OrderByDescending(o => o.SayimIndisi).ToList();
        }

        public static List<vSayimMamul> OkutulmayanlariGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSayimMamul>("exec spSayimdaOkutulmayanlariGetir 'Mamul'", new string[0]);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vSayimHam")]
    public class vSayimHam : IDisposable
    {
        [Column]
        public int HamId { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public string KaliteAdet { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public int SayimIndisi { get; set; }

        public static List<vSayimHam> HamSayimlariGetir()
        {
            return new DBEvents().GetGeneric<vSayimHam>().OrderByDescending(o => o.SayimIndisi).ToList();
        }

        public static List<vSayimHam> OkutulmayanlariGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSayimHam>("exec spSayimdaOkutulmayanlariGetir 'Ham'", new string[0]);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vFuarKumas")]
    public class vFuarKumas : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public int? KategoriId { get; set; }

        [Column]
        public string TipNo  { get; set; }

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

        private DBEvents db = new DBEvents();

        public List<vFuarKumasProsesleri> Prosesler
        {
            get
            {
                return db.GetGeneric<vFuarKumasProsesleri>(c => c.FuarKumasId == this.Id);
            }
        }

        public tblFuarKumas ViewToTbl()
        {
            return new tblFuarKumas()
            {
                AktifMi = this.AktifMi, 
                BaskiDolar = this.BaskiDolar, 
                BaskiEuro = this.BaskiEuro, 
                BaskiNo = this.BaskiNo, 
                BaskiTL = this.BaskiTL, 
                DesenDolar = this.DesenDolar,
                DesenEuro = this.DesenEuro, 
                DesenNo = this.DesenNo, 
                DesenTL = this.DesenTL, 
                DikisSiyrikAtki = this.DikisSiyrikAtki, 
                DikSiyrikCozgu = this.DikSiyrikCozgu,
                Dosemelik = this.Dosemelik, 
                DovizId = this.DovizId, 
                Elbiselik = this.Elbiselik, 
                Fiyat = this.Fiyat, 
                HamDolar = this.HamDolar, 
                HamEuro = this.HamEuro, 
                HamTL = this.HamTL,
                HavKomp = this.HavKomp, 
                Id = this.Id, 
                KaydetmeTarihi = this.KaydetmeTarihi, 
                KulAlani = this.KulAlani, 
                KumasAgirlik = this.KumasAgirlik, 
                KumasAgirlik2 = this.KumasAgirlik2,
                KumasEn = this.KumasEn, 
                Likrali = this.Likrali, 
                MamulDolar = this.MamulDolar, 
                MamulEuro = this.MamulEuro, 
                MamulTL = this.MamulTL, 
                Martindale = this.Martindale,
                MukavemetAtki = this.MukavemetAtki, 
                MukavemetCözgü = this.MukavemetCözgü, 
                NakisDolar = this.NakisDolar, 
                NakisEuro = this.NakisEuro, 
                NakisNo = this.NakisNo, 
                NakisTL = this.NakisTL, 
                Perdelik = this.Perdelik, 
                RenkHaslikAcik = this.RenkHaslikAcik, 
                RenkHaslikKoyu = this.RenkHaslikKoyu, 
                RenkHaslikOrta = this.RenkHaslikOrta,
                RenkNo = this.RenkNo, 
                SurtmeHaslikKuruAcik = this.SurtmeHaslikKuruAcik, 
                SurtmeHaslikKuruKoyu = this.SurtmeHaslikKuruKoyu, 
                SurtmeHaslikKuruOrta = this.SurtmeHaslikKuruOrta,
                SurtmeHaslikYasAcik = this.SurtmeHaslikYasAcik, 
                SurtmeHaslikYasKoyu = this.SurtmeHaslikYasKoyu, 
                SurtmeHaslikYasOrta = this.SurtmeHaslikYasOrta, 
                TipId = this.TipId,
                TotalKomp = this.TotalKomp, 
                Varyant = this.Varyant, 
                Yik1 = this.Yik1, 
                Yik2 = this.Yik2, 
                Yik3 = this.Yik3, 
                Yik4 = this.Yik4, 
                Yik5 = this.Yik5, 
                YikamaTalNot = this.YikamaTalNot,
                ImgData = this.ImgData,
                ImgThumbData = this.ImgThumbData,
                KategoriId = this.KategoriId
            };
        }

        public bool Kaydet()
        {
            tblFuarKumas tbl = this.ViewToTbl();
            tbl.KaydetmeTarihi = DateTime.Now;
            if (this.Id == 0)
            {
                bool snc = db.SaveGeneric<tblFuarKumas>(ref tbl);
                this.Id = tbl.Id;
                return snc;
            }
            else
            {
                db.GetGenericWithSQLQuery<string>("update tblFuarKumas set ImgData = null, ImgThumbData = null where Id = " + tbl.Id.ToString(), new string[0]);
                return db.UpdateGeneric<tblFuarKumas>(tbl);
            }
        }

        public bool Sil()
        {
            List<tblFuarKumasProsesleri> pro = db.GetGeneric<tblFuarKumasProsesleri>(c => c.FuarKumasId == this.Id);
            bool snc = true;
            if (pro.Count> 0) snc = db.DeleteGeneric<tblFuarKumasProsesleri>(pro);
            if (snc) db.GetGenericWithSQLQuery<tblFuarKumas>("DELETE FROM tblFuarKumas WHERE Id = " + this.Id.ToString(), new string[0]);

            return true;
        }

        public static List<vFuarKumas> FuarKumaslariGetir()
        {
            return new DBEvents().GetGeneric<vFuarKumas>();
        }

        public bool ProsesEkle(List<tblFuarProses> list)
        {
            List<tblFuarKumasProsesleri> eklenecekler = new List<tblFuarKumasProsesleri>();
            foreach (var item in list)
            {
                eklenecekler.Add(new tblFuarKumasProsesleri()
                {
                    FuarKumasId = this.Id,
                    FuarProcessId = item.Id
                });
            }

            return db.SaveGeneric<tblFuarKumasProsesleri>(eklenecekler);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vFuarKumasProsesleri")]
    public class vFuarKumasProsesleri : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string ProsesAdi { get; set; }

        [Column]
        public int FuarKumasId { get; set; }

        [Column]
        public int FuarProcessId { get; set; }

        public bool Sil()
        {
            DBEvents db = new DBEvents();
            tblFuarKumasProsesleri tbl = db.GetGeneric<tblFuarKumasProsesleri>(c=>c.Id == this.Id).FirstOrDefault();
            return db.DeleteGeneric<tblFuarKumasProsesleri>(tbl);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vFuarKombinKumaslar")]
    public class vFuarKombinKumaslar : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int FuarKumasId { get; set; }

        [Column]
        public int KombinId { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string DesenNo { get; set; }

        [Column]
        public string BaskiNo { get; set; }

        [Column]
        public string NakisNo { get; set; }

        [Column]
        public string Varyant { get; set; }

        [Column]
        public string ImgData { get; set; }

        [Column]
        public string ImgThumbData { get; set; }

        public tblFuarKombinKumaslar ViewToTbl()
        {
            return new tblFuarKombinKumaslar()
            {
                FuarKumasId = this.FuarKumasId,
                Id = this.Id,
                KombinId = this.KombinId
            };
        }

        public static List<tblFuarKombinKumaslar> ViewToTbl(List<vFuarKombinKumaslar> view)
        {
            List<tblFuarKombinKumaslar> list = new List<tblFuarKombinKumaslar>();

            if (view == null) return list;

            foreach (vFuarKombinKumaslar item in view)
            {
                list.Add(item.ViewToTbl());
            }

            return list;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vPackList")]
    public class vPackList : IDisposable
    {
        [Column]
        public int SevkId { get; set; }
        
        [Column]
        public int MamulId { get; set; }

        [Column]
        public string Barkod { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }
        
        [Column]
        public string RenkNo { get; set; }

        [Column]
        public string TipAlias { get; set; }

        [Column]
        public string RenkAlias { get; set; }

        [Column]
        public string RollAlias { get; set; }

        [Column]
        public double Metre { get; set; }

        [Column]
        public double NetMetre { get; set; }

        [Column]
        public string Tur { get; set; }
        
        [Column]
        public double Kg { get; set; }

        [Column]
        public double? NetAgirlik { get; set; }

        [Column]
        public double? BrutAgirlik { get; set; }

        [Column]
        public double? En { get; set; }

        [Column]
        public string OrderNo { get; set; }

        [Column]
        public string PartiNo { get; set; }
        
        public double BrutKg { get; set; }
        public int? KutuId { get; set; }

        public tblPackAliases ViewToTbl()
        {
            return new tblPackAliases()
            {
                MamulId = this.MamulId,
                RenkAlias = this.RenkAlias,
                RollAlias = this.RollAlias,
                SevkId = this.SevkId,
                TipAlias = this.TipAlias
            };
        }

        public static List<tblPackAliases> ViewToTbl(List<vPackList> view)
        {
            List<tblPackAliases> tbl = new List<tblPackAliases>();

            foreach (var item in view)
            {
                tbl.Add(item.ViewToTbl());
            }

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class vMusteriTipSatisHakkı : IDisposable
    {
        [Column]
        public int MusteriId { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public bool SatisHakkiVarMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    public class vTipFinishHakkı : IDisposable
    {
        [Column]
        public int ProcessGrupId { get; set; }

        [Column]
        public string ProcessGrupAdi { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public bool FinishHakkiVarMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }


    [Table(Name = "vBoyahaneUrunAgaci")]
    public class vBoyahaneUrunAgaci : IDisposable
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int TipId { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public int ProsesGrupId { get; set; }

        [Column]
        public string ProsesGrupAdi { get; set; }

        [Column]
        public int KumasTipiAyarId { get; set; }

        [Column]
        public string KumasTipi { get; set; }

        public bool Kaydet()
        {
            DBEvents db = new DBEvents();
            bool snc = false;

            if (this.Id == 0)
            {
                tblBoyahaneUrunAgaci tbl = this.ViewToTbl();
                snc = db.SaveGeneric<tblBoyahaneUrunAgaci>(ref tbl);
                this.Id = tbl.Id;
            }
            else snc = db.UpdateGeneric<tblBoyahaneUrunAgaci>(this.ViewToTbl());

            return snc;
        }

        public bool Sil()
        {
            return new DBEvents().DeleteGeneric<tblBoyahaneUrunAgaci>(this.ViewToTbl());
        }

        internal tblBoyahaneUrunAgaci ViewToTbl()
        {
            return new tblBoyahaneUrunAgaci()
            {
                Id = this.Id,
                KumasTipiAyarId = this.KumasTipiAyarId,
                ProsesGrupId = this.ProsesGrupId,
                TipId = this.TipId
            };
        }

        public static List<vBoyahaneUrunAgaci> UrunAgaciGetir()
        {
            return new DBEvents().GetGeneric<vBoyahaneUrunAgaci>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    [Table(Name = "vBoyahaneUrunAgaciAct")]
    public class vBoyahaneUrunAgaciAct : IDisposable
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int UrunAgaciId { get; set; }

        [Column]
        public int ProcessId { get; set; }

        [Column]
        public string ProsesAdi { get; set; }
        
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

        public bool Kaydet()
        {
            DBEvents db = new DBEvents();
            bool snc = false;

            if (this.Id == 0)
            {
                tblBoyahaneUrunAgaciAct tbl = this.ViewToTbl();
                snc = db.SaveGeneric<tblBoyahaneUrunAgaciAct>(ref tbl);
                this.Id = tbl.Id;
            }
            else snc = db.UpdateGeneric(this.ViewToTbl());

            return snc;
        }

        public bool Sil()
        {
            return new DBEvents().DeleteGeneric<tblBoyahaneUrunAgaciAct>(this.ViewToTbl());
        }

        public static List<vBoyahaneUrunAgaciAct> UrunAgaciProsesleriGetir(int urunAgacId)
        {
            return new DBEvents().GetGeneric<vBoyahaneUrunAgaciAct>(c => c.UrunAgaciId == urunAgacId).OrderBy(o => o.Sira).ToList();
        }

        internal tblBoyahaneUrunAgaciAct ViewToTbl()
        {
            return new tblBoyahaneUrunAgaciAct()
            {
                Aciklama = this.Aciklama,
                ApreKodu = this.ApreKodu,
                CikisEni = this.CikisEni,
                GirisEni = this.GirisEni,
                Id = this.Id,
                IslemSayisi = this.IslemSayisi,
                MakinadaKalmaSuresi = this.MakinadaKalmaSuresi,
                MakinaDevri = this.MakinaDevri,
                MakinaSicaklik = this.MakinaSicaklik,
                ProcessId = this.ProcessId,
                Sira = this.Sira,
                UrunAgaciId = this.UrunAgaciId
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vAnahtar")]
    public class vAnahtar : IDisposable
    {
        [Column]
        public int YilId { get; set; }

        [Column]
        public string Yil { get; set; }

        [Column]
        public int AyId { get; set; }

        [Column]
        public string Ay { get; set; }

        [Column]
        public string TipNo { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string TezgahAdi { get; set; }

        [Column]
        public double Devir { get; set; }

        [Column]
        public double NazariAtki { get; set; }

        [Column]
        public double TezgahSaati { get; set; }

        [Column]
        public int TezgahSayisi { get; set; }

        [Column]
        public double IscilikFaktoru { get; set; }

        [Column]
        public double DigerIscilikFaktoru { get; set; }

        [Column]
        public double IsciSaat { get; set; }

        [Column]
        public double ToplamIscilikFaktoru { get; set; }

        [Column]
        public double IsciDagitimKatsayi { get; set; }

        [Column]
        public double TezgahEnerjiKatsayi { get; set; }

        [Column]
        public double TezgahKWSaat { get; set; }

        [Column]
        public double DigerKWSaat { get; set; }

        [Column]
        public double ToplamKWSaat { get; set; }

        [Column]
        public double ToplamKWSaatBinAtki { get; set; }

        [Column]
        public double GenelUretimKatsayi { get; set; }

        [Column]
        public double FiiliUretimAtilanAtkiSayisi { get; set; }

        [Column]
        public double DaireRandimani { get; set; }

        [Column]
        public int ToplamTezgahSayisi { get; set; }

        [Column]
        public int CalisanToplamTezgahSayisi { get; set; }

        [Column]
        public double IscilikDagitimKatsayisi { get; set; }

        [Column]
        public double GenelUretimDagitimKatsayisi { get; set; }

        public static List<vAnahtar> Getir(int ayId, int yilId)
        {
            using (var db = new DBEvents())
            {
                return db.GetGeneric<vAnahtar>(c => c.AyId == ayId && c.YilId == yilId);
            }
        }

        public static void Hesapla(int ayId, int yilId)
        {
            using (var db = new DBEvents())
            {
                db.GetGenericWithSQLQuery<string>("EXEC [dbo].[spHamMaliyetHesaplama] @YilId = {0}, @AyId = {1}", new object[] { yilId, ayId });
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }

    //Gökhan 16.05.2014
    [Table(Name = "vFasonKumasMaliyet")]
    public class vFasonKumasMaliyet : IDisposable
    {
        //[Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true)]
        //public int? Id { get; set; }

        [Column]
        public int Id { get; set; }

        [Column]
        public double? Metre { get; set; }

        [Column]
        public double? FaturaTutari { get; set; }

        [Column]
        public int? PartiProcessActId { get; set; }

        [Column]
        public int? PartiId { get; set; }

        [Column]
        public int? ProcessId { get; set; }

        [Column]
        public string PartiNo { get; set; }

        [Column]
        public int? MusteriId { get; set; }

        [Column]
        public string ProcessKodu { get; set; }

        [Column]
        public string ProcessAdi { get; set; }

        [Column]
        public bool? FasonMu { get; set; }

        [Column]
        public bool? AktifMi { get; set; }

        [Column]
        public double? PartiMetre { get; set; }

        [Column]
        public string MusteriAdi { get; set; }

        [Column]
        public DateTime? Tarih { get; set; }

        [Column]
        public int ActId { get; set; }

        public tblFasonKumasMaliyet ViewToTbl()
        {
            return new tblFasonKumasMaliyet()
            {
                Id = this.Id,
                PartiProcessActId = this.ActId,
                FaturaTutari = this.FaturaTutari,
                Metre = this.Metre,
                Tarih = DateTime.Now,
                PartiId = this.PartiId,
                ProcessId = this.ProcessId
            };
        }

        public static List<tblFasonKumasMaliyet> ViewToTbl(List<vFasonKumasMaliyet> view)
        {
            List<tblFasonKumasMaliyet> tbl = new List<tblFasonKumasMaliyet>();

            foreach (vFasonKumasMaliyet item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    //Gökhan 03.07.2014
    [Table(Name = "vFasonIplikMaliyet")]
    public class vFasonIplikMaliyet : IDisposable
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

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        public List<tblMalzemeler> ListIplikler { get; set; }

        public tblFasonIplikMaliyet ViewToTbl()
        {
            return new tblFasonIplikMaliyet()
            {
                Id = this.Id,
                IplikId = this.IplikId,
                FaturaTarihi = this.FaturaTarihi,
                FaturaTutari = this.FaturaTutari,
                IplikKg = this.IplikKg,
                KullanimAlani = this.KullanimAlani,
                LotNo = this.LotNo,
                Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString())
            };
        }

        public static List<tblFasonIplikMaliyet> ViewToTbl(List<vFasonIplikMaliyet> view)
        {
            List<tblFasonIplikMaliyet> tbl = new List<tblFasonIplikMaliyet>();

            foreach (vFasonIplikMaliyet item in view) tbl.Add(item.ViewToTbl());

            return tbl;
        }

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
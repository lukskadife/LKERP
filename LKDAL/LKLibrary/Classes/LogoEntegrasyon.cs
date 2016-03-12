using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;
using System.Data.Linq.Mapping;
using UnityObjects;

namespace LKLibrary.Classes
{
    [Table(Name = "vLogoFirmalar")]
    internal class vLogoFirmalar : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column(Name = "Adi")]
        public string Adi { get; set; }

        [Column(Name = "Kod")]
        public string Kod { get; set; }

        [Column(Name = "Tip")]
        public int? Tip { get; set; }

        [Column(Name = "AktifMi")]
        public short AktifMi { get; set; }

        [Column(Name = "LogoId")]
        public int LogoId { get; set; }

        [Column]
        public string TamAdres { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLogoMalzemeler")]
    internal class vLogoMalzemeler : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public int? Tip { get; set; }

        [Column]
        public int LogoId { get; set; }

        [Column]
        public short AktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLogoPersoneller")]
    internal class vLogoPersoneller : IDisposable
    {
        [Column(Name = "Id")]
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
        public int LogoId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLogoPersonelBolumleri")]
    internal class vLogoPersonelBolumleri : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column]
        public string Kodu { get; set; }

        [Column]
        public string Adi { get; set; }

        [Column]
        public int LogoId { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    [Table(Name = "vLogoMalzemeBirimleri")]
    internal class vLogoMalzemeBirimleri : IDisposable
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column]
        public int lgMalzemeId { get; set; }

        /// <summary>
        /// LKDB'deki malzemeId si
        /// </summary>
        [Column]
        public int MalzemeId { get; set; }
        
        [Column]
        public int lgBirimId { get; set; }

        [Column]
        public string lgBirimAdi { get; set; }

        [Column]
        public bool lgAnaBirimMi { get; set; }

        [Column]
        public double lgCarpan { get; set; }

        [Column]
        public double lgAnaCarpan { get; set; }
        
        [Column]
        public short lgAktifMi { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class LogoEntegrasyon
    {
        DBEvents db = new DBEvents();

        private List<vLogoFirmalar> LogoFirmalariGetir()
        {
            List<vLogoFirmalar> list = db.GetGenericWithSQLQuery<vLogoFirmalar>("exec spLogoFirmalar", new string[0]);

            return list.FindAll(c => c.Tip != null).ToList();
        }

        public int FirmalariEntegreEt()
        {
            try
            {
                List<tblFirmalar> listToSave = new List<tblFirmalar>();
                List<tblFirmalar> listToUpdate = new List<tblFirmalar>();
                List<vLogoFirmalar> listLogo = LogoFirmalariGetir();

                foreach (vLogoFirmalar logoFirma in listLogo)
                {
                    tblFirmalar firma = new tblFirmalar()
                    {
                        Id = logoFirma.Id,
                        Adi = logoFirma.Adi,
                        AktifMi = logoFirma.AktifMi == 0 ? true : false,
                        BaglantiId = logoFirma.Tip.Value,
                        Kodu = logoFirma.Kod,
                        EntId = logoFirma.LogoId,
                         Adres = logoFirma.TamAdres
                    };

                    if (logoFirma.Id == 0) listToSave.Add(firma);
                    else listToUpdate.Add(firma);
                }

                bool sonuc = true;
                if (listToSave.Count > 0) if (db.SaveGeneric<tblFirmalar>(listToSave) == false) sonuc = false;
                if (listToUpdate.Count > 0 && sonuc) if (db.UpdateGeneric<tblFirmalar>(listToUpdate) == false) sonuc = false;

                if (sonuc) return listToSave.Count + listToUpdate.Count;
                else return -1;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "FirmalariEntegreEt", 0);
                return -1;
            }
        }

        public int MalzemeleriEntegreEt()
        {
            List<vLogoMalzemeler> listLogo = db.GetGenericWithSQLQuery<vLogoMalzemeler>("exec spLogoMalzemeler", new string[0]);
            List<tblMalzemeler> listToUpdate = new List<tblMalzemeler>();
            List<tblMalzemeler> listToSave = new List<tblMalzemeler>();

            List<tblMalzemeler> malzemeler = db.GetGeneric<tblMalzemeler>();

            foreach (vLogoMalzemeler logoMalzeme in listLogo)
            {
                tblMalzemeler malzeme = malzemeler.Find(c => c.Id == logoMalzeme.Id);

                if (malzeme == null) malzeme = new tblMalzemeler();

                malzeme.Adi = logoMalzeme.Adi;
                malzeme.AktifMi = logoMalzeme.AktifMi == 0 ? true : false;
                malzeme.BaglantiId = logoMalzeme.Tip.Value;
                malzeme.Id = logoMalzeme.Id;
                malzeme.EntId = logoMalzeme.LogoId;
                malzeme.Kodu = logoMalzeme.Kodu;

                if (malzeme.Id == 0) listToSave.Add(malzeme);
                else listToUpdate.Add(malzeme);
            }

            bool sonuc = true;
            if (listToSave.Count > 0) if (db.SaveGeneric<tblMalzemeler>(listToSave) == false) sonuc = false;
            if (listToUpdate.Count > 0 && sonuc) if (db.UpdateGeneric<tblMalzemeler>(listToUpdate) == false) sonuc = false;

            if (sonuc) return listToSave.Count + listToUpdate.Count;
            else return -1;
        }

        public int MalzemeBirimleriEntegreEt()
        {
            List<vLogoMalzemeBirimleri> listLogo = db.GetGenericWithSQLQuery<vLogoMalzemeBirimleri>("exec spLogoMalzemeBirimleri", new string[0]);
            List<tblBirimler> listToUpdate = new List<tblBirimler>();
            List<tblBirimler> listToSave = new List<tblBirimler>();

            foreach (vLogoMalzemeBirimleri logoBirim in listLogo)
            {
                tblBirimler birim = new tblBirimler()
                {
                    AktifMi = logoBirim.lgAktifMi == 0 ? true : false,
                    AnaCarpan = logoBirim.lgAnaCarpan,
                    BirimAdi = logoBirim.lgBirimAdi,
                    BirimCarpan = logoBirim.lgCarpan,
                    BirimId = logoBirim.lgBirimId,
                    EntId = logoBirim.lgBirimId,
                    Id = logoBirim.Id,
                    MalzemeId = logoBirim.MalzemeId
                };

                if (birim.Id == 0) listToSave.Add(birim);
                else listToUpdate.Add(birim);
            }

            bool sonuc = true;
            if (listToSave.Count > 0) if (db.SaveGeneric<tblBirimler>(listToSave) == false) sonuc = false;
            if (listToUpdate.Count > 0 && sonuc) if (db.UpdateGeneric<tblBirimler>(listToUpdate) == false) sonuc = false;

            if (sonuc) return listToSave.Count + listToUpdate.Count;
            else return -1;
        }

        public int PersonelleriEntegreEt()
        {
            List<vLogoPersoneller> listLogo = db.GetGenericWithSQLQuery<vLogoPersoneller>("exec spLogoPersoneller", new string[0]);
            List<tblPersoneller> listToUpdate = new List<tblPersoneller>();
            List<tblPersoneller> listToSave = new List<tblPersoneller>();

            foreach (vLogoPersoneller logoPers in listLogo)
            {
                tblPersoneller pers = new tblPersoneller()
                {
                    Adi = logoPers.Adi,
                    BolumId = logoPers.BolumId,
                    Id = logoPers.Id,
                    Kodu = logoPers.Kodu,
                    EntId = logoPers.LogoId,
                    AktifMi = logoPers.AktifMi
                };

                if (pers.Id == 0) listToSave.Add(pers);
                else listToUpdate.Add(pers);
            }

            bool sonuc = true;
            if (listToSave.Count > 0) if (db.SaveGeneric<tblPersoneller>(listToSave) == false) sonuc = false;
            if (listToUpdate.Count > 0 && sonuc) if (db.UpdateGeneric<tblPersoneller>(listToUpdate) == false) sonuc = false;

            if (sonuc) return listToSave.Count + listToUpdate.Count;
            else return -1;
        }

        public int PersonelBolumleriEntegreEt()
        {
            List<vLogoPersonelBolumleri> listLogo = db.GetGenericWithSQLQuery<vLogoPersonelBolumleri>("exec spLogoPersonelBolumleri", new string[0]);
            List<tblPersonelBolumleri> listToSave = new List<tblPersonelBolumleri>();
            List<tblPersonelBolumleri> listToUpdate = new List<tblPersonelBolumleri>();

            foreach (vLogoPersonelBolumleri logoBolum in listLogo)
            {
                tblPersonelBolumleri bolum = new tblPersonelBolumleri()
                {
                    Adi = logoBolum.Adi,
                    Id = logoBolum.Id,
                    EntId = logoBolum.LogoId,
                    Kodu = logoBolum.Kodu
                };

                if (bolum.Id == 0) listToSave.Add(bolum);
                else listToUpdate.Add(bolum);
            }

            bool sonuc = true;
            if (listToSave.Count > 0) if (db.SaveGeneric<tblPersonelBolumleri>(listToSave) == false) sonuc = false;
            if (listToUpdate.Count > 0 && sonuc) if (db.UpdateGeneric<tblPersonelBolumleri>(listToUpdate) == false) sonuc = false;

            if (sonuc) return listToSave.Count + listToUpdate.Count;
            else return -1;
        }
       
    }
}

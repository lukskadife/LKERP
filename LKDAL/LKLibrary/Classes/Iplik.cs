using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Iplik
    {
        private Enums.Hareketler _Hareket;

        public Iplik(Enums.Hareketler hareketTipi, dynamic hareket = null)
        {
            this._Hareket = hareketTipi;

            if (hareketTipi == Enums.Hareketler.IplikGiris)
            {
                GirisIplikleri = new List<vIplikGiris>();
                if (hareket != null && hareket is vIplikGiris) GirisIplikleri.Add(hareket);
            }
            else if (hareketTipi == Enums.Hareketler.IplikCikis)
            {
                CikisIplikleri = new List<vIplikCikis>();
                if (hareket != null && hareket is vIplikCikis) CikisIplikleri.Add(hareket);
            }
        }

        public Iplik()
        {
        }

        DBEvents db = new DBEvents();
        public enum IplikGirisTurleri { S, N, BU, FBU, RU, FBD, FBG, FBOG, FRDG, FFG, DII, None }
        public enum IplikCikisTurleri { DC, BC, FBC, FBUC, FBGC, FBOC, FDOC, FRDC, FFC, DRC, RS, RAC, SII, None }

        public List<vIplikGiris> GirisIplikleri { get; set; }
        public List<vIplikCikis> CikisIplikleri { get; set; }

        /// <summary>
        /// Verilen iplik yeni bir kayıtsa ekleme yapar, eski kayıtsa o kaydı düzeltir.
        /// </summary>
        /// <param name="iplik"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public bool IplikKaydet(dynamic iplik, string tip)
        {
            if (_Hareket == Enums.Hareketler.IplikGiris)
            {
                try
                {
                    Enum.Parse(typeof(IplikGirisTurleri), tip);
                    vIplikGiris giris = iplik as vIplikGiris;
                    giris.GirisTanim = tip;
                    if (giris.Id == 0) GirisIplikleri.Add(giris);
                    else GirisIplikleri[GirisIplikleri.FindIndex(c => c.Id == giris.Id)] = giris;
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
            else if (_Hareket == Enums.Hareketler.IplikCikis)
            {
                try
                {
                    Enum.Parse(typeof(IplikCikisTurleri), tip);
                    vIplikCikis cikis = iplik as vIplikCikis;
                    cikis.CikisTanim = tip;
                    if (cikis.Id == 0) CikisIplikleri.Add(iplik);
                    else CikisIplikleri[CikisIplikleri.FindIndex(c => c.Id == cikis.Id)] = cikis;
                    return true;
                }
                catch
                {
                    return false;
                }                
            }

            return false;
        }

        private bool CikisKaydet()
        {
            bool snc = true;

            List<tblMalzemeCikis> toSaveList = vIplikCikis.ViewToTbl(CikisIplikleri.FindAll(c => c.Id == 0));
            List<tblMalzemeCikis> toUpdList = vIplikCikis.ViewToTbl(CikisIplikleri.FindAll(c => c.Id != 0));

            if (db.SaveGeneric<tblMalzemeCikis>(toSaveList) == false) snc = false;
            if (db.UpdateGeneric<tblMalzemeCikis>(toUpdList) == false) snc = false;

            return snc;
        }

        private bool GirisKaydet()
        {
            bool snc = true;

            List<tblMalzemeGiris> toSaveList = vIplikGiris.ViewToTbl(GirisIplikleri.FindAll(c => c.Id == 0));
            List<tblMalzemeGiris> toUpdList = vIplikGiris.ViewToTbl(GirisIplikleri.FindAll(c => c.Id != 0));

            if (db.SaveGeneric<tblMalzemeGiris>(toSaveList) == false) snc = false;
            if (db.UpdateGeneric<tblMalzemeGiris>(toUpdList) == false) snc = false;

            return snc;
        }

        public bool HareketKaydet()
        {
            if (_Hareket == Enums.Hareketler.IplikGiris) return GirisKaydet();
            else if (_Hareket == Enums.Hareketler.IplikCikis) return CikisKaydet();

            return false;
        }

        public bool CikisSil(vIplikCikis cikis)
        {
            if (db.DeleteGeneric<tblMalzemeCikis>(cikis.ViewToTbl()))
            {
                if (this.CikisIplikleri.Contains(cikis)) this.CikisIplikleri.Remove(cikis);
                return true;
            }
            return false;
        }

        public bool GirisSil(vIplikGiris giris)
        {
            if (db.DeleteGeneric<tblMalzemeGiris>(giris.ViewToTbl()))
            {
                if (this.GirisIplikleri.Contains(giris)) this.GirisIplikleri.Remove(giris);
                return true;
            }
            return false;
        }

        public static List<vIplikCikis> IplikCikislariGetir(string tip, DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vIplikCikis>(c => c.CikisTanim == tip && ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static List<vIplikGiris> IplikGirisleriGetir(string tip, DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vIplikGiris>(c => c.GirisTanim == tip && ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static List<tblMalzemeler> IplikleriGetir()
        {
            //return new DBEvents().GetGeneric<tblMalzemeler>(c => c.AktifMi == true && c.BaglantiId == 39);
            return new DBEvents().GetGeneric<tblMalzemeler>(c => c.AktifMi == true && c.BaglantiId == 39).OrderBy(o => o.Adi).ToList();
        }

        //Gökhan 03.07.2014
        public static List<vFasonIplikMaliyet> FasonIplikMaliyetleriGetir()
        {
            return new DBEvents().GetGeneric<vFasonIplikMaliyet>();
        }

        //Gökhan 16.05.2014
        public bool FasonIplikKaydet(List<vFasonIplikMaliyet> liste)
        {
            bool result = false;

            List<vFasonIplikMaliyet> listeSave = liste.Where(c => c.Id == 0).ToList();
            List<vFasonIplikMaliyet> listeUpdate = liste.Where(c => c.Id != 0).ToList();

            if (listeSave.Count > 0)
            {
                return db.SaveGeneric<tblFasonIplikMaliyet>(vFasonIplikMaliyet.ViewToTbl(listeSave));
            }

            if (listeUpdate.Count > 0)
            {
                return db.UpdateGeneric<tblFasonIplikMaliyet>(vFasonIplikMaliyet.ViewToTbl(listeUpdate));
            }

            return result;
        }

        //Gökhan 04.07.2014
        //public bool FasonIplikSil(List<vFasonIplikMaliyet> liste)
        //{
        //    return db.DeleteGeneric<tblFasonIplikMaliyet>(vFasonIplikMaliyet.ViewToTbl(liste));
        //}

        //Gökhan 04.07.2014
        public bool FasonIplikSil(int id)
        {
            return db.DeleteGeneric<tblFasonIplikMaliyet>(db.GetGeneric<tblFasonIplikMaliyet>(c => c.Id == id));
        }

    }
}

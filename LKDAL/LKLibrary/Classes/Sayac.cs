using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Sayac
    {        
        DBEvents db = new DBEvents();

        public List<tblSayaclar> SayacTanimlariGetir()
        {
            return db.GetGeneric<tblSayaclar>(c => c.BaglantiId == -1 && c.AktifMi == true);
        }

        public List<tblSayaclar> SayacAltBasliklariGetir(int sayacTanimId)
        {
            return db.GetGeneric<tblSayaclar>(c => c.BaglantiId == sayacTanimId && c.AktifMi == true);
        }

        public bool SayacKaydet(tblSayaclar altBaslik)
        {
            if (altBaslik.Id == 0) return db.SaveGeneric<tblSayaclar>(ref altBaslik);
            else return db.UpdateGeneric<tblSayaclar>(altBaslik);
        }

        public bool SayacSil(tblSayaclar altBaslik)
        {
            return db.DeleteGeneric<tblSayaclar>(altBaslik);
        }

        public bool SayacGirisiKaydet(tblSayacGirisleri giris)
        {
            if (giris.Tarih == null) giris.Tarih = DateTime.Now;

            tblSayacGirisleri tmp = db.GetGeneric<tblSayacGirisleri>(c => c.SayacId == giris.SayacId && c.Tarih == giris.Tarih.Value.Date).FirstOrDefault();
            if (tmp != null) giris.Id = tmp.Id;

            if (giris.Id == 0) return db.SaveGeneric<tblSayacGirisleri>(ref giris);
            else if (giris.Id > 0) return db.UpdateGeneric<tblSayacGirisleri>(giris);

            return false;
        }

        public List<vSayacGirisleriDgaz> DgazSayacGirisiGetir(DateTime gun, int bolumId)
        {
            return db.GetGeneric<vSayacGirisleriDgaz>(c => c.Tarih.Date == gun && c.BolumId == bolumId);
        }

        public List<vSayacGiris> DgazSayacGirisiGetir(int yil, int ay, int bolumId)
        {
            List<vSayacGirisleriDgaz> listGaz = new Sayac().db.GetGeneric<vSayacGirisleriDgaz>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            var a = (from elk in listGaz
                     group elk by new { elk.BolumId } into elkGroup
                     select elkGroup).ToList();

            List<vSayacGiris> listGiris = new List<vSayacGiris>();
            foreach (var x in a)
            {
                listGiris.Add(new vSayacGiris
                {
                    BolumId = x.Select(c => c.BolumId).FirstOrDefault(),
                    kwh = x.Sum(c => c.kwh),
                    BolumAdi = x.Select(c => c.BolumAdi).FirstOrDefault(),
                    m3 = x.Sum(c => c.m3),
                    sm3 = x.Sum(c => c.sm3),
                    Tarih = x.Select(c => c.Tarih).FirstOrDefault(),
                    Maliyet = x.Sum(c => c.Maliyet),
                    BirimFiyat = x.Select(c => c.BirimFiyat).FirstOrDefault(),
                    PersonelId = x.Select(c => c.PersonelId).FirstOrDefault().Value
                });
            }
            return listGiris;
        }

        public List<vSayacGirisleriDgaz> DgazSayacGirisiGetir(int yil, int ay, int bolumId, bool gunGun)
        {
            List<vSayacGirisleriDgaz> listGaz = new Sayac().db.GetGeneric<vSayacGirisleriDgaz>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            return listGaz;
        }

        public List<vSayacGirisleriElk> ElkSayacGirisiGetir(DateTime gun, int bolumId)
        {
            return db.GetGeneric<vSayacGirisleriElk>(c => c.Tarih.Date == gun && c.BolumId == bolumId);
        }

        public List<vSayacGiris> ElkSayacGirisiGetir(int yil, int ay, int bolumId)
        {
            List<vSayacGirisleriElk> listElk = new Sayac().db.GetGeneric<vSayacGirisleriElk>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            var a = (from elk in listElk
                     group elk by new { elk.BolumId } into elkGroup
                     select elkGroup).ToList();

            List<vSayacGiris> listGiris = new List<vSayacGiris>();
            foreach (var x in a)
            {
                listGiris.Add(new vSayacGiris
                {
                    BolumId = x.Select(c => c.BolumId).FirstOrDefault(),
                    kwh = x.Sum(c => c.kwh),
                    BolumAdi = x.Select(c => c.BolumAdi).FirstOrDefault(),
                    Tarih = x.Select(c => c.Tarih).FirstOrDefault(),
                    Maliyet = x.Sum(c => c.Maliyet),
                    BirimFiyat = x.Select(c => c.BirimFiyat).FirstOrDefault(),
                    PersonelId = x.Select(c => c.PersonelId).FirstOrDefault().Value
                });
            }
            return listGiris;
        }

        public List<vSayacGirisleriElk> ElkSayacGirisiGetir(int yil, int ay, int bolumId, bool gunGun)
        {
            List<vSayacGirisleriElk> listElk = new Sayac().db.GetGeneric<vSayacGirisleriElk>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            return listElk;
        }

        public List<vSayacGirisleriSu> SuSayacGirisiGetir(DateTime gun, int bolumId)
        {
            return db.GetGeneric<vSayacGirisleriSu>(c => c.Tarih.Date == gun && c.BolumId == bolumId);
        }

        public List<vSayacGiris> SuSayacGirisiGetir(int yil, int ay, int bolumId)
        {
            List<vSayacGirisleriSu> listSu = new Sayac().db.GetGeneric<vSayacGirisleriSu>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            var a = (from elk in listSu
                     group elk by new { elk.BolumId } into elkGroup
                     select elkGroup).ToList();

            List<vSayacGiris> listGiris = new List<vSayacGiris>();
            foreach (var x in a)
            {
                listGiris.Add(new vSayacGiris
                {
                    BolumId = x.Select(c => c.BolumId).FirstOrDefault(),
                    BolumAdi = x.Select(c => c.BolumAdi).FirstOrDefault(),
                    ton = x.Sum(c => c.ton),
                    Tarih = x.Select(c=>c.Tarih).FirstOrDefault(),
                    Maliyet = x.Sum(c=>c.Maliyet),
                    BirimFiyat = x.Select(c=>c.BirimFiyat).FirstOrDefault(),
                    PersonelId = x.Select(c => c.PersonelId).FirstOrDefault().Value
                });
            }
            return listGiris;
        }

        public List<vSayacGirisleriSu> SuSayacGirisiGetir(int yil, int ay, int bolumId, bool gunGun)
        {
            List<vSayacGirisleriSu> listSu = new Sayac().db.GetGeneric<vSayacGirisleriSu>(c => c.Tarih.Year == yil && c.Tarih.Month == ay && c.BolumId == bolumId);
            return listSu;
        }

        public bool SayacBirimFiyatKaydet(tblSayacBirimFiyatlari fiyat)
        {
            if (fiyat.Id == 0) return db.SaveGeneric<tblSayacBirimFiyatlari>(fiyat);
            else return db.UpdateGeneric<tblSayacBirimFiyatlari>(fiyat);
        }

        public double SayacBirimFiyatGetir(int sayacId)
        {
            tblSayacBirimFiyatlari fiyat = db.GetGeneric<tblSayacBirimFiyatlari>(c => c.SayacId == sayacId).OrderByDescending(c => c.OlusturmaTarihi).FirstOrDefault();

            if (fiyat == null) return 0;
            else return fiyat.Fiyat;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfService1.Classes
{
    public class vTalepStokGiris : vTalepKarsilamaAct
    {
        public double AlinanMiktar { get; set; }
        public string Ambalaj { get; set; }
        public int MalzemeGirisId;

        public static vTalepStokGiris vTalepKarsilamaActTovTalepStokGiris(vTalepKarsilamaAct karsilama)
        {
            tblMalzemeGiris malzemeGirisi = new DBEvents().GetGeneric<tblMalzemeGiris>(c => c.KarsilamaActId == karsilama.Id).FirstOrDefault();

            return new vTalepStokGiris()
            {
                BirimAdi = karsilama.BirimAdi,
                BirimId = karsilama.BirimId,
                Id = karsilama.Id,
                MalzemeAdi = karsilama.MalzemeAdi,
                MalzemeId = karsilama.MalzemeId,
                MalzemeKodu = karsilama.MalzemeKodu,
                Miktar = karsilama.Miktar,
                TalepId = karsilama.TalepId,
                TalepKarsilamaId = karsilama.TalepKarsilamaId,
                Tarih = karsilama.Tarih,
                TedarikciAdi = karsilama.TedarikciAdi,
                TedarikciId = karsilama.TedarikciId,
                TedarikciKodu = karsilama.TedarikciKodu,
                MalzemeGirisId = malzemeGirisi == null ? 0 : malzemeGirisi.Id,
                AlinanMiktar = new Stok().KarsilamaStokGirisiGetir(karsilama.Id)
            };
        }
    }

    public class Stok
    {
        DBEvents db = new DBEvents();

        public bool StokGirisleriYap(List<vTalepStokGiris> listStokGirisleri, int personelId)
        {
            if (listStokGirisleri == null || listStokGirisleri.Count == 0) return false;

            try
            {
                List<tblMalzemeGiris> listToSave = new List<tblMalzemeGiris>();
                List<tblMalzemeGiris> listToUpdate = new List<tblMalzemeGiris>();

                foreach (vTalepStokGiris giris in listStokGirisleri)
                {
                    tblMalzemeGiris malzemeGiris;

                    //if (giris.MalzemeBagId == 39)
                    //    malzemeGiris = new tblMalzemeGiris()
                    //    {
                    //        Ambalaj = giris.Ambalaj,
                    //        GirisTanim = "SA"
                    //    };
                    //else
                        malzemeGiris = new tblMalzemeGiris()
                        {
                            Id = giris.MalzemeGirisId,
                            BirimId = giris.BirimId,
                            MalzemeId = giris.MalzemeId,
                            Miktar = giris.AlinanMiktar,
                            PersonelId = personelId,
                            KarsilamaActId = giris.Id
                        };

                    if (malzemeGiris.Id > 0) listToUpdate.Add(malzemeGiris);
                    else listToSave.Add(malzemeGiris);
                }

                bool sonuc = true;
                if (listToUpdate.Count > 0) if (db.UpdateGeneric<tblMalzemeGiris>(listToUpdate) == false) sonuc = false;
                if (listToSave.Count > 0) if (db.SaveGeneric<tblMalzemeGiris>(listToSave) == false) sonuc = false;

                return sonuc;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "StokGirisleriYap", 0);
                return false;
            }
        }

        internal bool StokCikislariYap(List<tblMalzemeCikis> listStokCikislari, int personelId, Enums.Bolumler bolum, int bolumId)
        {
            if (listStokCikislari == null || listStokCikislari.Count == 0) return false;

            try
            {
                List<tblMalzemeCikis> listToSave = listStokCikislari.FindAll(c=>c.Id == 0);
                List<tblMalzemeCikis> listToUpdate = listStokCikislari.FindAll(c => c.Id > 0);

                bool sonuc = true;
                if (listToUpdate.Count > 0) if (db.UpdateGeneric<tblMalzemeCikis>(listToUpdate) == false) sonuc = false;
                if (listToSave.Count > 0) if (db.SaveGeneric<tblMalzemeCikis>(listToSave) == false) sonuc = false;

                return sonuc;
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "StokCikislariYap", 0);
                return false;
            }
        }

        public List<vMalzemeStokDurum> StokMiktarlariGetir()
        {
            return db.GetGeneric<vMalzemeStokDurum>();
        }

        /// <summary>
        /// Malzemenin stoktaki mevcut miktarını ana biriminden dönderir.
        /// </summary>
        /// <param name="malzemeId">Stok durumu sorulan malzemenin id'si</param>
        /// <returns></returns>
        public double StokMiktariGetir(int malzemeId)
        {
            vMalzemeStokDurum stok = db.GetGeneric<vMalzemeStokDurum>(c => c.MalzemeId == malzemeId).FirstOrDefault();
            return stok != null ? stok.MevcutStok : 0;
        }

        /// <summary>
        /// Malzemenin satın alması yapılmış, depoya girmesi beklenen miktarını ana biriminden hesaplayarak dönderir.
        /// </summary>
        /// <param name="malzemeId">stok gelecek miktarı hesaplanacak malzemenin id'si</param>
        /// <returns></returns>
        public string StokGelecekMiktariGetir(int malzemeId)
        {
            return new DBEvents().GetGenericWithSQLQuery<string>("select dbo.fMalzemeGelecekMiktar(" + malzemeId + ")", new string[0]).FirstOrDefault();
        }

        public List<vMalzemeStokDurum> StokAlarmVerenleriGetir()
        {
            return db.GetGeneric<vMalzemeStokDurum>(c => c.MevcutStok <= c.MinStok && c.MinStok != 0);
        }

        public List<tblMalzemeler> MinStoklariGetir()
        {
            return db.GetGeneric<tblMalzemeler>(c=>c.BaglantiId > 0);
        }

        internal double KarsilamaStokGirisiGetir(int karsilamaActId)
        {
            tblMalzemeGiris stokGiris = db.GetGeneric<tblMalzemeGiris>(c => c.KarsilamaActId == karsilamaActId).FirstOrDefault();

            return ((stokGiris == null || stokGiris.Miktar.HasValue == false) ? 0 : stokGiris.Miktar.Value);
        }

        public bool MinStoklariGuncelle(List<tblMalzemeler> list)
        {
            try
            {
                return db.UpdateGeneric<tblMalzemeler>(list);
            }
            catch (Exception e)
            {
                DBEvents.LogException(e, "MinStoklariGuncelle", 0);
                return false;
            }
        }

        /// <summary>
        /// İplik stoğunu getirir.
        /// </summary>
        /// <param name="hepsiMiGelsin">true ise bütün iplik stoğunu(net kg'ı 0 dan büyük olanlar) getirir. false ise sadece leventte görülecekleri getirir</param>
        /// <returns></returns>
        public List<vIplikStok> IplikStoklariGetir(bool hepsiMiGelsin = true)
        {
            var query = PredicateBuilder.True<vIplikStok>();
            query = query.And<vIplikStok>(c => c.NetKg > 0);
            if (hepsiMiGelsin == false) query = query.And<vIplikStok>(c => c.LeventteGor.Value == true || c.LeventteGor.HasValue == false);
            return db.GetGeneric<vIplikStok>(query);
        }

    }
}

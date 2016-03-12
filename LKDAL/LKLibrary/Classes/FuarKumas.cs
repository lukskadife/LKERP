using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class FuarKumas
    {
        DBEvents db = new DBEvents();
        public List<vKumas> KumaslariGetir()
        {
            return db.GetGeneric<vKumas>().OrderBy(o => o.TipNo).ToList();
        }

        private int KumasKaydet(vKumas kumas)
        {
            tblKumas tbl = vKumas.ViewToTable(kumas);
            if (db.SaveGeneric<tblKumas>(ref tbl)) return tbl.Id;
            else return 0;
        }

        public bool KumasKaydet(List<vKumas> lstKumas)
        {
            List<tblKumas> toSave = vKumas.ViewToTable( lstKumas.FindAll(c => c.Id == 0));
            List<tblKumas> toUpd = vKumas.ViewToTable( lstKumas.FindAll(c => c.Id > 0));

            bool snc = true;
            if (toSave.Count > 0) if (db.SaveGeneric<tblKumas>(toSave) == false) snc = false;
            if (toUpd.Count > 0) if (db.UpdateGeneric<tblKumas>(toUpd) == false) snc = false;

            return snc;
        }

        public bool TipKaydet(vKumas kumas)
        {
            tblKumas tbl = vKumas.ViewToTable(kumas);

            if (kumas.Id == 0)
            {
                tblKumas kumasKontrol = db.GetGeneric<tblKumas>(c => c.TipNo == kumas.TipNo && c.Varyant == kumas.Varyant).FirstOrDefault();
                if (kumasKontrol != null) throw new Exception("Bu tip kumaş zaten var.\n\nEklenemez..!");

                return db.SaveGeneric<tblKumas>(tbl);
            }
            else return db.UpdateGeneric<tblKumas>(tbl);
        }

        public bool KumasSil(vKumas silinecek)
        {
            return db.DeleteGeneric<tblKumas>(vKumas.ViewToTable(silinecek));
        }

        public List<tblFuarBaskilar> BaskilariGetir()
        {
            return db.GetGeneric<tblFuarBaskilar>();
        }

        public bool KombinKaydet(tblFuarKombin kombin = null, List<tblFuarKombinKumaslar> kombinKumaslar = null)
        {
            bool snc = true;

            if (kombin != null && kombin.Id != 0)
            {
                db.GetGenericWithSQLQuery<string>("update tblFuarKombin set ImgData = null, ImgThumbData = null where Id = " + kombin.Id.ToString(), new string[0]);
                if (db.UpdateGeneric<tblFuarKombin>(kombin) == false) snc = false;
            }

            else if (kombin != null && kombin.Id == 0) //yeni kayıt kaydet
                snc = db.SaveGeneric<tblFuarKombin>(ref kombin);

            return snc;
        }

        public bool KombineKumasEkle(List<vFuarKumas> kumaslar, int kombinId)
        {
            List<tblFuarKombinKumaslar> list = new List<tblFuarKombinKumaslar>();
            foreach (vFuarKumas item in kumaslar)
            {
                if (item != null)
                    list.Add(new tblFuarKombinKumaslar()
                    {
                        FuarKumasId = item.Id, 
                        KombinId = kombinId
                    });
            }

            return db.SaveGeneric<tblFuarKombinKumaslar>(list);
        }

        public List<vFuarKombinKumaslar> KombinKumaslariGetir(int kombinId)
        {
            return db.GetGeneric<vFuarKombinKumaslar>(c => c.KombinId == kombinId);
        }

        public List<tblFuarKombin> KombinleriGetir()
        {
            return db.GetGeneric<tblFuarKombin>();
        }

        public bool KombinSil(tblFuarKombin kombin)
        {
            List<tblFuarKombinKumaslar> kombinKumaslar = vFuarKombinKumaslar.ViewToTbl(KombinKumaslariGetir(kombin.Id));

            if (db.DeleteGeneric<tblFuarKombinKumaslar>(kombinKumaslar))
            {
                try
                {
                    db.GetGenericWithSQLQuery<tblFuarKumas>("DELETE FROM tblFuarKombin WHERE Id = " + kombin.Id.ToString(), new string[0]);
                    return true;
                }
                catch(Exception exp)
                {
                    return false;
                }
            }

            return false;

        }

        public bool KombinKumasSil(vFuarKombinKumaslar kombinKumas)
        {
            tblFuarKombinKumaslar tbl = kombinKumas.ViewToTbl(); //db.GetGeneric<tblFuarKombinKumaslar>(c => c.Id == kombinKumas.Id).FirstOrDefault();
            return db.DeleteGeneric<tblFuarKombinKumaslar>(tbl);
        }

        public bool KombinNoVarMi(string kombinNo)
        {
            if (db.GetGeneric<tblFuarKombin>(c => c.KombinNo == kombinNo).Count > 0) return true;
            else return false;
        }
    }
}

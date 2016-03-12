using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Iade : Mamul
    {
        private DBEvents db = new DBEvents();

        public vSiparisler IadeSiparisi
        {
            get
            {
                return _IadeSiparisi;
            }
            set
            {
                _IadeSiparisi = value;
                IadeBarkodlari = db.GetGeneric<vMamulKumaslar>(c => c.IadeSipId == value.Id && c.Durum == "Iade");
            }
        }
        private vSiparisler _IadeSiparisi;

        public List<vMamulKumaslar> IadeBarkodlari { get; set; }

        #region statics

        public static List<vSiparisler> IadeSiparisleriGetir()
        {
            return new DBEvents().GetGeneric<vSiparisler>(c => c.Durum == "Açık" && c.BaglantiId == 5);
        }

        public static List<vMamulOnay> MamulKontrolListesiGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vMamulOnay>("EXEC spMamulOnayListesiGetir", new string[0]);
        }

        #endregion

        public List<vSiparisAct> IadeTipleriGetir()
        {
            return db.GetGeneric<vSiparisAct>(c => c.SiparisId == this._IadeSiparisi.Id);
        }

        public List<vSiparisAct> IadeRenkleriGetir(int tipId)
        {
            return db.GetGeneric<vSiparisAct>(c => c.SiparisId == this._IadeSiparisi.Id && c.TipId == tipId);
        }

        public bool IadeEkle(vMamulKumaslar iade)
        {
            iade.SevkEdilebilir = false;
            iade.IadeSipId = _IadeSiparisi.Id;
            iade.SevkId = 0;
            iade.Durum = "Iade";
            iade.PartiId = null;
            iade.KalitePuan = iade.KaliteAdet;
            iade.NetMetre = iade.Metre;

            tblMamulKumaslar iadeTbl = iade.ViewToTable();
            if (db.SaveGeneric<tblMamulKumaslar>(ref iadeTbl))
            {
                iadeTbl.AnaMamulId = iadeTbl.Id;
                if (base.MamulBarkodAl(ref iadeTbl) == false) return false;
                iade = db.GetGeneric<vMamulKumaslar>(c => c.Id == iadeTbl.Id).FirstOrDefault();
                IadeBarkodlari.Add(iade);
                return true;
            }
            else return false;
        }

        public bool IadeDuzelt(vMamulKumaslar iade)
        {
            if (db.UpdateGeneric<tblMamulKumaslar>(iade.ViewToTable()) == false) return false;

            IadeBarkodlari[IadeBarkodlari.FindIndex(c => c.Id == iade.Id)] = iade;
            return true;
        }

        public bool IadeSil(vMamulKumaslar silinecek)
        {
            tblMamulKumaslar satir = db.GetGeneric<tblMamulKumaslar>(c => c.Id == silinecek.Id).FirstOrDefault();
            if (satir == null) return false;
            if (db.DeleteGeneric<tblMamulKumaslar>(satir) == false) return false;
            IadeBarkodlari.Remove(silinecek);
            return true;
        }

        //private bool IadeBarkodAl(ref tblIadeler iadebarkod)
        //{
        //    //mamul kumaş barkodu oluşturuluyor
        //    iadebarkod.Barkod = ('I' + iadebarkod.Id.ToString()).PadLeft(10, '0');
        //    bool barkodAlindiMi = db.UpdateGeneric<tblIadeler>(iadebarkod);
        //    if (!barkodAlindiMi) //eğer barkod alınamaz ise kayıt silinir ve kaydedilmedi olarak fonksiyon false dönderir.
        //    {
        //        db.DeleteGeneric<tblIadeler>(iadebarkod);
        //        return false;
        //    }

        //    return true;
        //}

        public bool BoyaheneyeIadeEt(List<vMamulOnay> iadeEdilecekler)
        {
            List<tblMamulKumaslar> iadeMamulleri = new List<tblMamulKumaslar>();
            foreach (vMamulOnay item in iadeEdilecekler)
            {
                tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Id == item.Id).FirstOrDefault();
                mamul.Durum = "BoyaSepeti";
                mamul.IadeAciklama = item.IadeAciklama;
                iadeMamulleri.Add(mamul);
            }

            if (db.UpdateGeneric<tblMamulKumaslar>(iadeMamulleri) == false) throw new Exception("Boyahaneye iade edilemeyen mamuller var..!");

            return true;
        }

        public bool SevkEdilebilirIsaretle(List<vMamulOnay> iadeEdilecekler)
        {
            List<tblMamulKumaslar> iadeMamulleri = new List<tblMamulKumaslar>();
            foreach (vMamulOnay item in iadeEdilecekler)
            {
                tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Id == item.Id).FirstOrDefault();
                mamul.SevkEdilebilir = true;
                iadeMamulleri.Add(mamul);
            }

            if (db.UpdateGeneric<tblMamulKumaslar>(iadeMamulleri) == false) throw new Exception("Onaylanamayan mamuller var..!");

            return true;
        }
    }
}

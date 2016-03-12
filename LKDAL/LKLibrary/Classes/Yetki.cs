using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Yetki
    {
        int _PersonelId;
        public int PersonelId
        {
            get { return _PersonelId; }
            set
            {
                _PersonelId = value;
                BolumId = 0;
            }
        }

        int _BolumId;
        public int BolumId
        {
            get { return _BolumId; }
            set
            {
                _BolumId = value; 
                PersonelId = 0;
            }
        }

        DBEvents db = new DBEvents();

        public Yetki(int personelId = 0, int bolumId = 0)
        {
            this._BolumId = bolumId;
            this._PersonelId = personelId;
        }

        private List<vYetkiTanim> YetkiTanimlariGetir()
        {
            List<vYetkiTanim> yetkiler = db.GetGeneric<vYetkiTanim>();
            List<vYetkiTanim> ToReturnList = new List<vYetkiTanim>();

            foreach (vYetkiTanim item in yetkiler.FindAll(c => c.MenuBagId == 1))
            {
                item.AltYetkiler = yetkiler.FindAll(c => c.MenuBagId == item.MenuId);
                ToReturnList.Add(item);
            }
            return ToReturnList;
        }

        public List<vYetkiTanim> YetkileriGetir()
        {
            if (this._BolumId == 0 && this._PersonelId == 0) return null;

            var query = PredicateBuilder.True<tblYetkiler>();
            if (this.BolumId != 0) query = query.And(c => c.BolumId == this._BolumId);
            tblPersoneller kullaniciBolum = null;
            if (this._PersonelId != 0)
            {
                query = query.And(c => c.PersonelId == this._PersonelId);
                kullaniciBolum = db.GetGeneric<tblPersoneller>(c => c.Id == this._PersonelId).FirstOrDefault();
            }

            List<tblYetkiler> kullaniciYetki = db.GetGeneric<tblYetkiler>(query);
            //mevcut tüm yetkiler getiriliyor
            List<vYetkiTanim> yetkiler = YetkiTanimlariGetir();            

            //bölümlerin ya da kullanıcıların yetkileri atanıyor
            for (int i = 0; i < yetkiler.Count; i++)
            {
                if (yetkiler[i].AltYetkiler.Count == 0)
                {
                    //kullanıcıya özgü yetki varsa yetki kullanıcının yetkisinden alınır.
                    tblYetkiler yetki = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].Id && c.PersonelId == this._PersonelId);
                    //kullanıcıya özgü yetki yoksa, yetki kulanıcının bölümünün yetkisinden alınır.
                    if (yetki == null)
                    {
                        yetki = db.GetGeneric<tblYetkiler>(c => c.YetkiId == yetkiler[i].Id && c.BolumId == (kullaniciBolum == null ? this.BolumId : kullaniciBolum.BolumId)).FirstOrDefault();
                    }
                    yetkiler[i].YetkiliMi = yetki == null ? true : yetki.YetkiVarMi;
                }

                else
                {
                    for (int j = 0; j < yetkiler[i].AltYetkiler.Count; j++)
                    {
                        tblYetkiler yetkiAlt = kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].AltYetkiler[j].Id);
                        if (yetkiAlt == null) yetkiAlt = db.GetGeneric<tblYetkiler>(c => c.YetkiId == yetkiler[i].AltYetkiler[j].Id && c.BolumId == kullaniciBolum.BolumId).FirstOrDefault();//kullaniciYetki.Find(c => c.YetkiId == yetkiler[i].AltYetkiler[j].Id && c.BolumId == this._BolumId);
                        yetkiler[i].AltYetkiler[j].YetkiliMi = yetkiAlt == null ? true : yetkiAlt.YetkiVarMi;
                    }

                    if (yetkiler[i].AltYetkiler.Count == yetkiler[i].AltYetkiler.Count(c => c.YetkiliMi == true)) yetkiler[i].YetkiliMi = true;
                    else yetkiler[i].YetkiliMi = null;
                }
            }

            return yetkiler;
        }

        private bool YetkiKaydet(List<tblYetkiler> yetkiler)
        {
            List<tblYetkiler> toSaveList = yetkiler.FindAll(c => c.Id == 0);
            List<tblYetkiler> toUpdList = yetkiler.FindAll(c => c.Id != 0);

            bool sonuc = true;
            if (toSaveList.Count > 0) if (db.SaveGeneric<tblYetkiler>(toSaveList) == false) sonuc = false;
            if (toUpdList.Count > 0) if (db.UpdateGeneric<tblYetkiler>(toUpdList) == false) sonuc = false;

            return sonuc;
        }

        public bool YetkiKaydet(List<vYetkiTanim> yetkiler)
        {
            List<tblYetkiler> yetkiTbl = new List<tblYetkiler>();

            //Bölüm için yetki tanımlama
            if (this._BolumId != 0)
            {
                foreach (vYetkiTanim bYetkiTanim in yetkiler)
                {
                    tblYetkiler bolumYetki = db.GetGeneric<tblYetkiler>(c => c.BolumId == this._BolumId && c.YetkiId == bYetkiTanim.Id).FirstOrDefault();
                    if (bolumYetki == null)
                    {
                        bolumYetki = new tblYetkiler()
                        {
                            BolumId = this._BolumId,
                            PersonelId = null,
                            YetkiId = bYetkiTanim.Id,
                            YetkiVarMi = bYetkiTanim.YetkiliMi ?? true
                        };
                    }
                    else bolumYetki.YetkiVarMi = bYetkiTanim.YetkiliMi ?? true;
                    yetkiTbl.Add(bolumYetki);
                }
            }
            //kullanıcı için yetki tanımlama
            else
            {
                foreach (var kYetkiTanim in yetkiler)
                {
                    tblYetkiler kullaniciYetki = db.GetGeneric<tblYetkiler>(c => c.PersonelId == this._PersonelId && c.YetkiId == kYetkiTanim.Id).FirstOrDefault();
                    if (kullaniciYetki == null)
                    {
                        kullaniciYetki = new tblYetkiler()
                        {
                            PersonelId = this._PersonelId,
                            BolumId = null,
                            YetkiId = kYetkiTanim.Id,
                            YetkiVarMi = kYetkiTanim.YetkiliMi ?? true
                        };
                    }
                    else kullaniciYetki.YetkiVarMi = kYetkiTanim.YetkiliMi ?? true;
                    yetkiTbl.Add(kullaniciYetki);
                }
            }

            return YetkiKaydet(yetkiTbl);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Menu
    {
        //public int MenuId { get; set; }
        //public string Adi { get; set; }
        //public string Aciklama { get; set; }
        //public string AcilacakSayfa { get; set; }
        ///// <summary>
        ///// Açılacak sayfa page olarak mı açılacak yoksa childWindow gibi küçük bir ekranda mı açılacak
        ///// </summary>
        //public bool IsChildWindow { get; set; }
        //public bool GosterilsinMi { get; set; }
        public List<vYetkiMenu> ListMenuItem = new List<vYetkiMenu>(); 

        private DBEvents db = new DBEvents(); 

        public List<vYetkiMenu> GetMenuItems(int personelId, int bolumId)
        {
            List<vYetkiMenu> yetkiList = db.GetGeneric<vYetkiMenu>(c => c.YetkiVarMi == true && (c.PersonelId == personelId || c.BolumId == bolumId));
            List<vYetkiMenu> tmpList = new List<vYetkiMenu>(yetkiList);

            //foreach (vYetkiMenu item in tmpList) // bölüm yetkisi kullanıcı yetkisini eziyor
            //{
            //    if (yetkiList.Count(c => c.YetkiId == item.YetkiId) > 1)
            //    {
            //        vYetkiMenu bolumYetki = yetkiList.Find(c => c.YetkiId == item.YetkiId && c.BolumId != 0);
            //        yetkiList.Remove(bolumYetki);
            //    }
            //}

            foreach (vYetkiMenu item in yetkiList.FindAll(c=>c.BaglantiId == 1).OrderBy(o=>o.Sira).ToList())
            {
                item.MenuItems = yetkiList.FindAll(c => c.BaglantiId == item.Id).OrderBy(o => o.Sira).ToList();
                ListMenuItem.Add(item);
            }

            return ListMenuItem;
        }

        public List<vDurumCount> GetMenuItemCounts(string spIsmi, int talepEdenId, int durumAyarId)
        {
            try
            {
                if (string.IsNullOrEmpty(spIsmi)) return null;
                List<vDurumCount> ob = db.GetGenericWithSQLQuery<vDurumCount>("exec " + spIsmi + " {0}, {1}", new object[] { durumAyarId, talepEdenId }).ToList();

                return ob;
            }
            catch (Exception e) 
            {
                DBEvents.LogException(e, "GetMenuItemCounts", 0);
                return new List<vDurumCount>();
            }
        }

        public vKullanicilar GetKullanici(string kulAdi, string kulSifre)
        {
            return db.GetGeneric<vKullanicilar>(c => c.KulAdi == kulAdi && c.Sifre == kulSifre && c.AktifMi == true).FirstOrDefault();
        }

        
    }
}

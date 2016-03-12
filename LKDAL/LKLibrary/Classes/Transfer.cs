using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Transfer
    {
        private DBEvents db = new DBEvents();
        public vAmbarUst Ambar { get; set; }        

        public static void AmbarUstFisiOlustur()
        {
            tblAmbar ambarFisi = new tblAmbar();
            ambarFisi.FirmaId = 1432;
            ambarFisi.LogoAktarildiMi = false;
            ambarFisi.Tarih = DateTime.Now;

            bool snc = new DBEvents().SaveGeneric<tblAmbar>(ambarFisi);
        }

        public static bool UstBelgeSilinebilirMi(int ambarUstId)
        {
            bool snc = true;
            tblAmbarAct altKayitlar = new DBEvents().GetGeneric<tblAmbarAct>().Where(c => c.AmbarUstId == ambarUstId).FirstOrDefault();

            if (altKayitlar == null)
                snc = false; // silinebilir. Hiç kayıt yok.

            return snc;
        }

        public Transfer(vAmbarUst ambar)
        {
            this.Ambar = ambar;
        }

        public static void AmbarUstFisiSil(int ambarId)
        {
            tblAmbar ambar = new DBEvents().GetGeneric<tblAmbar>().Where(c => c.Id == ambarId).FirstOrDefault();
            bool snc = new DBEvents().DeleteGeneric<tblAmbar>(ambar);
        }

        public static List<vAmbarUst> AmbarUstBelgeleriGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vAmbarUst>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static List<vHamKumaslarOrmeStok> TransferEdilecekHamKumaslariGetir()
        {
            return new DBEvents().GetGeneric<vHamKumaslarOrmeStok>().ToList();
        }

        public static bool TransferEt(List<vHamKumaslarOrmeStok> secilenler, int ambarUstId)
        {
            bool snc = true;            
            List<tblAmbarAct> transferKumaslar = new List<tblAmbarAct>();
            
            foreach (vHamKumaslarOrmeStok item in secilenler)
            {
                tblHamKumaslar ham = new DBEvents().GetGeneric<tblHamKumaslar>(c => c.Id == item.Id).FirstOrDefault();
                ham.DepoId = 402;
                bool snc3 = new DBEvents().UpdateGeneric<tblHamKumaslar>(ham);
                              
                tblAmbarAct ambarAct = new tblAmbarAct();
                ambarAct.AmbarUstId = ambarUstId;
                ambarAct.HamBarkodId = ham.Id;
                ambarAct.Tarih = DateTime.Now;
                ambarAct.DepoId = 402;
                transferKumaslar.Add(ambarAct);                

            }          

            if (new DBEvents().SaveGeneric<tblAmbarAct>(transferKumaslar) == false)
            {
                snc = false;
                throw new Exception("Transfer edilemedi!");
            }

            return snc;
        }

        public static List<vAmbarAct> TransferEdilenleriGetir(int ambarUstId)
        {
            return new DBEvents().GetGeneric<vAmbarAct>().Where(c => c.AmbarUstId == ambarUstId && c.DepoId == 402).ToList();
        }

        public static List<vAmbarAct> DepoyaAlinanlariGetir(int ambarUstId)
        {
            return new DBEvents().GetGeneric<vAmbarAct>().Where(c => c.AmbarUstId == ambarUstId && c.DepoId == 400).ToList();
        }

        public static void HamBarkoduEkle(string barkod, int ambarUstId)
        {            
            vHamKumaslar kumas = new DBEvents().GetGeneric<vHamKumaslar>(c => c.Barkod == barkod).FirstOrDefault();

            if (kumas == null) throw new Exception("Barkod hatalı.!");

            if (kumas.DepoId != 402) throw new Exception("Bu barkod transfer edilmemiş!");

            tblAmbarAct ambarX = new DBEvents().GetGeneric<tblAmbarAct>().Where(c => c.AmbarUstId == ambarUstId && c.HamBarkodId == kumas.Id && c.DepoId == 402).FirstOrDefault();

            if (ambarX == null) throw new Exception("Bu barkod için transfer işlemi yapılmamış!");

            tblHamKumaslar tblKumas = kumas.ViewToTbl();
            tblKumas.DepoId = 400;
            if (new DBEvents().UpdateGeneric<tblHamKumaslar>(tblKumas))
            {
                tblAmbarAct ambarAct = new tblAmbarAct();
                ambarAct.AmbarUstId = ambarUstId;
                ambarAct.DepoId = 400;
                ambarAct.HamBarkodId = tblKumas.Id;
                ambarAct.Tarih = DateTime.Now;

                bool snc = new DBEvents().SaveGeneric<tblAmbarAct>(ambarAct);               
            }          
                       
        }

        public static void BarkodSilinebilirMi(int hamId)
        {
            tblHamKumaslar kumas = new DBEvents().GetGeneric<tblHamKumaslar>().Where(c => c.Id == hamId).FirstOrDefault();            
            if (kumas.DepoId == 400) throw new Exception("Kumaş merkezde. Silinemez!");

            if (kumas.DepoId == 402)
            {
                tblAmbarAct ambarAct = new DBEvents().GetGeneric<tblAmbarAct>().Where(c => c.HamBarkodId == hamId).FirstOrDefault();                
                vAmbarAct silinen = new DBEvents().GetGeneric<vAmbarAct>().Where(c => c.Id == ambarAct.Id).FirstOrDefault();
                bool snc = new DBEvents().DeleteGeneric<tblAmbarAct>(ambarAct);                            
            }            

        }
    }
}

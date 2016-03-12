using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;


namespace LKLibrary.Interfaces
{
   public abstract class AbsBakımPeriyot
    {
        public object GrupGetir()
        {
            return new DBEvents().GetGeneric<tblMalzemeler>(c => c.BaglantiId == -1).ToList<tblMalzemeler>();
        }
        public abstract object ArananUrunGetir(string Filtre, int GrupId);
        public abstract bool PeriyotKaydet();
    }
   public abstract class AbsMakinaBakım
   {
  public abstract object ArananUrunGetir(string GrubuFiltre, string AdiFiltre);
   }
   public abstract class AbsBakımOnarım
   {
       public abstract object PeriyotSıfırla(int MakinaId);
       public object GrupGetir()
       {
           return new DBEvents().GetGeneric<tblMalzemeler>(c => c.BaglantiId == -1).ToList<tblMalzemeler>();

       }
       public abstract object ArananUrunGetir(string KoduFiltre, int GrupId,string AdiFiltre);
       public abstract bool BakımOnarımKaydet(object BakımDetay);

   }
}

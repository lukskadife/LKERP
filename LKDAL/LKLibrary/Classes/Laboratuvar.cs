using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Laboratuvar
    {
        DBEvents db = new DBEvents();

        private int _TestId;

        public int TestId
        {
            get { return _TestId; }
            set 
            { 
                _TestId = value;
                Test = db.GetGeneric<tblSiparisTestleri>(c => c.Id == value).FirstOrDefault();
                if (Test == null) Test = new tblSiparisTestleri();
            }
        }

        public tblSiparisTestleri Test { get; set; }

        public bool TestKaydet()
        {
            Test.TestYapildiMi = true;

            if (Test.Id == 0) return db.SaveGeneric<tblSiparisTestleri>(Test);
            else return db.UpdateGeneric<tblSiparisTestleri>(Test);
        }

        public static List<vLaboratuvarTest> TumTestleriGetir()
        {
            return new DBEvents().GetGeneric<vLaboratuvarTest>();
        }

        public static List<vLaboratuvarTest> BekleyenTestleriGetir()
        {
            return new DBEvents().GetGeneric<vLaboratuvarTest>(c => c.TestYapildiMi == false);
        }
    }
}

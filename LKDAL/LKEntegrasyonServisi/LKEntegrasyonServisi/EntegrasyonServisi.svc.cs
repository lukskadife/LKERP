using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LKEntegrasyonServisi
{
    public class EntegrasyonServisi : IEntegrasyon  
    {
        private LKLibrary.Classes.LogoEntegrasyon _Islem = new LKLibrary.Classes.LogoEntegrasyon();

        public int MalzemeleriEntegreEt()
        {
            return _Islem.MalzemeleriEntegreEt();
        }

        public int MalzemeBirimleriEntegreEt()
        {
            return _Islem.MalzemeBirimleriEntegreEt();
        }

        public int FirmalariEntegreEt()
        {
            return _Islem.FirmalariEntegreEt();
        }

        public int PersonelleriEntegreEt()
        {
            return _Islem.PersonelleriEntegreEt();
        }

        public int PersonelBolumleriEntegreEt()
        {
            return _Islem.PersonelBolumleriEntegreEt();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1.Classes;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private LogoEntegrasyon _Islem = new LogoEntegrasyon();

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
            if (_Islem == null) return -1;
            return _Islem.PersonelleriEntegreEt();
        }

        public int PersonelBolumleriEntegreEt()
        {
            return _Islem.PersonelBolumleriEntegreEt();
        }
    }
}

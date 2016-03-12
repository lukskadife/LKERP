using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LKEntegrasyonServisi
{
    [ServiceContract]
    public interface IEntegrasyon
    {
        [OperationContract]
        int MalzemeleriEntegreEt();

        [OperationContract]
        int MalzemeBirimleriEntegreEt();

        [OperationContract]
        int FirmalariEntegreEt();

        [OperationContract]
        int PersonelleriEntegreEt();

        [OperationContract]
        int PersonelBolumleriEntegreEt();
    }
}

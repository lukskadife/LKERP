using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Interfaces
{
    public abstract class AbsMalzemeTalep 
    {
        public virtual object TalepFormuOlustur() { return null; }

        public abstract bool TalepKaydet(List<tblTalepler> lstTalep);
    }

    abstract class AbsSatınAlma
    {
        public abstract object TalepFiltrele(string Filtre, object lstFiltrelenecekData);
        //public object TalepFiltrele(string Filtre, object );

    }
}

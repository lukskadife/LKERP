using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Interfaces
{
    public abstract class AbsInfoBar
    {
        private DBEvents db = new DBEvents();

        public virtual object GetBarItems(int ayarId)
        {
            return db.GetGeneric<tblDurumlar>(c => c.AyarId == ayarId);
        }
    }
}

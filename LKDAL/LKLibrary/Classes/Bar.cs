using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.Interfaces;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Bar : AbsInfoBar
    {
        public override object GetBarItems(int ayarId)
        {
            return base.GetBarItems(ayarId);
        }
    }
}

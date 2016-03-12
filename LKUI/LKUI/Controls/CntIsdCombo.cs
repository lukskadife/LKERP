using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace LKUI.Controls
{
    public class CntIsdCombo : ComboBox
    {
        private Brush TmpBrush;

        public CntIsdCombo()
        {
            this.Loaded += (snd, ea) =>
            {
                TmpBrush = this.BorderBrush;
            };
        }

        public bool GirisYapildiMi
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    this.BorderBrush = Brushes.Red;
                    return false;
                }
                else
                {
                    this.BorderBrush = TmpBrush;
                    return true;
                }
            }
        }
    }
}

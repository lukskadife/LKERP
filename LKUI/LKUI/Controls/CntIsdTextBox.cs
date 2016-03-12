using System;
using System.Windows.Controls;

namespace LKUI.Controls
{
    class CntIsdTextBox : TextBox
    {
        public enum Tipi {None, Sayisal, Gerekli, ZorunsuzSayisal };
        public Tipi TxtTipi { get; set; }

        public int? IntTxt
        {
            get
            {
                if (this.TextGirisiDogruMu && (this.TxtTipi == Tipi.Sayisal || this.TxtTipi == Tipi.ZorunsuzSayisal))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(this.Text)) return null;
                        else return Convert.ToInt32(this.Text);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else return null;
            }
        }

        public double? DoubleTxt
        {
            get
            {
                if (this.TextGirisiDogruMu && (this.TxtTipi == Tipi.Sayisal || this.TxtTipi == Tipi.ZorunsuzSayisal))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(this.Text)) return null;
                        else return Convert.ToDouble(this.Text);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {

                    return null;
                }
            }
        }

        public CntIsdTextBox()
        {
            TextChanged += new TextChangedEventHandler(CntIsdTextBox_TextChanged);
            this.Loaded += (snd, ea) =>
            {
                TempBrush = this.BorderBrush;
                TempThick = this.BorderThickness;
                if (TxtTipi == Tipi.Sayisal && ToolTip == null)
                {
                    ToolTip = "Giriş sayısal olmalıdır..!";
                    _TextGirisiDogruMu = false;
                }
                else if (TxtTipi == Tipi.ZorunsuzSayisal && ToolTip == null)
                {
                    ToolTip = "Giriş sayısal olmalıdır..!";
                    _TextGirisiDogruMu = true;
                }
                else if (TxtTipi == Tipi.Gerekli && ToolTip == null)
                {
                    ToolTip = "Bu alan boş geçilemez..!";
                    _TextGirisiDogruMu = false;
                }
                if (TxtTipi == Tipi.None) TextGirisiDogruMu = true;
            };
        }

        void CntIsdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtTipi == Tipi.Sayisal) StringSayisalMi(this.Text);
            else if (TxtTipi == Tipi.ZorunsuzSayisal) StringSayisalMi(string.IsNullOrEmpty(this.Text) ? "0" : this.Text);
            else if (TxtTipi == Tipi.Gerekli) TextGirisiDogruMu = string.IsNullOrEmpty(Text) ? false : true;
        }

        private System.Windows.Media.Brush TempBrush;
        private System.Windows.Thickness TempThick;

        private void Ayarla()
        {
            if (_TextGirisiDogruMu == false)
            {
                BorderBrush = System.Windows.Media.Brushes.Red;
                this.BorderThickness = new System.Windows.Thickness(1.5);
            }
            else
            {
                BorderBrush = TempBrush;
                BorderThickness = TempThick;
            }
        }

        private bool _TextGirisiDogruMu;
        public bool TextGirisiDogruMu
        {
            get {
                Ayarla();
                return _TextGirisiDogruMu; 
            }
            set
            {
                _TextGirisiDogruMu = value;
                Ayarla();
            }
        }

        private void StringSayisalMi(string girisStr)
        {
            try
            {
                Convert.ToDouble(girisStr);
                _TextGirisiDogruMu = true;
                if (this.TxtTipi == Tipi.Sayisal && girisStr == "0") _TextGirisiDogruMu = false;
            }
            catch
            {
                TextGirisiDogruMu = false;
            }
        }

    }
}

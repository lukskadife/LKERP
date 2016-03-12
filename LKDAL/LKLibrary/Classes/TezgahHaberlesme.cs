using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Threading;

namespace LKLibrary.Classes
{
    public class TezgahHaberlesme
    {
        private SerialPort _TezgahPort;
        private DispatcherTimer _Thread;
        
        public string ReturnDeger;
        public bool HazirMi = false;

        #region Events
        public delegate void TezgahEvent();
        public event TezgahEvent TezgahHareketEtti;
        #endregion

        public TezgahHaberlesme()
        {
            _TezgahPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            _TezgahPort.ReadTimeout = 2000;
            if (_TezgahPort.IsOpen) _TezgahPort.Close();
            try
            {
                _TezgahPort.Open();
                this.HazirMi = true;
                _Thread = new DispatcherTimer();
                _Thread.Interval = new TimeSpan(0, 0, 0, 0, 500);
                _Thread.Tick += new EventHandler((snd, ea) => { TezgahSinyalGonder(); });
                _Thread.Start();
            }
            catch
            {
                if (_TezgahPort.IsOpen) _TezgahPort.Close();
                this.HazirMi = false;
                if (_Thread != null) _Thread.Stop();
            }
        }

        private int SinyalGondermeHataSayisi = 0;
        private void TezgahSinyalGonder()
        {
            byte[] c = new byte[]{
                58,
                48,
                49,
                48,
                52,
                48,
                48,
                48,
                49,
                48,
                48,
                48,
                50,
                70,
                56,
                13,
                10
            };

            _TezgahPort.Write(c, 0, c.Length);
            try
            {
                string veri = _TezgahPort.ReadLine();
                if (veri.IndexOf(':') == -1) return;
                TezgahOku(veri);
                this.SinyalGondermeHataSayisi = 0;
            }
            catch
            {
                this.SinyalGondermeHataSayisi ++;
                if (this.SinyalGondermeHataSayisi == 3)
                {
                    if (_TezgahPort.IsOpen) _TezgahPort.Close();
                    if (_Thread != null) _Thread.Stop();
                    this.HazirMi = false;
                }
            }
        }

        private void TezgahOku(string tezgahVerisi)
        {
            string tmpStr;
            tmpStr = tezgahVerisi;
            tmpStr = tmpStr.Substring(7, 8);
            tmpStr = int.Parse(tmpStr, System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string asil = tmpStr;

            if (asil.Length == 2)
            {
                tmpStr = '0' + tmpStr;
                tmpStr = tmpStr.Insert(2, ",");
            }
            if (asil.Length == 3) tmpStr = tmpStr.Insert(1, ",");
            if (asil.Length == 4) tmpStr = tmpStr.Insert(2, ",");
            if (asil.Length == 5) tmpStr = tmpStr.Insert(3, ",");
            if (asil.Length == 6) tmpStr = tmpStr.Insert(4, ",");
            this.ReturnDeger = tmpStr;
            if (TezgahHareketEtti != null) TezgahHareketEtti();
        }

        ~TezgahHaberlesme()
        {
            if (_TezgahPort.IsOpen) _TezgahPort.Close();
            if (_Thread != null) _Thread.Stop();
        }
    }
}

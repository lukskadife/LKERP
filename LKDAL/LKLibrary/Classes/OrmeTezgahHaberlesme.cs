using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;

namespace LKLibrary.Classes
{
    public class OrmeTezgahHaberlesme
    {
        public string OrmeOkunanMetreDegeri;
        public string OrmeOkunanKgDegeri;
        SerialPort sPort;
      //  private DispatcherTimer _Thread;
      //  public bool HazirMi = false;

       // SerialPort sPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
      

        public OrmeTezgahHaberlesme()
        {
            sPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            sPort.ReadTimeout = 2000;
            if (sPort.IsOpen) sPort.Close();
            try
            {
                sPort.Open();
                sPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortOku);

            }
            catch
            {
                 if (sPort.IsOpen) sPort.Close();
               
            }
                
               
                
           
        }


        
        public void SerialPortOku(object sender, SerialDataReceivedEventArgs e)
        {
            if(sPort.IsOpen==false) sPort.Open();
            sPort.WriteLine("R");
            OrmeOkunanMetreDegeri += sPort.ReadExisting();
        }
          



        


    }
}

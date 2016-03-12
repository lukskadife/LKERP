using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;
using LKLibrary.Classes;
using System.Diagnostics;

namespace ConAppLibTest
{
    class Program
    {
        private static List<T> Ara<T>(string aranan, List<T> ItemsSource) where T:class
        {
            List<T> list = new List<T>();
            foreach (T item in ItemsSource)
            {
                if (item.GetType().GetProperty("Adi").GetValue(item, null).ToString().Contains(aranan))
                    list.Add(item);
            }

            return list;
        }

        static void Main(string[] args)
        {
            string str = "Sametasdt";
            Stopwatch watch = new Stopwatch();

            str = "001234567890";

            string snc = str + string.Format("5", "0"); 

            watch.Start();

  

            
            //object obj = new DBEvents().GetGeneric<vBoyaProgrami>();
            //List<vPaketListesi> paket = (new Sevkiyat() { SevkBelge = new vSevk() { SiparisId = 21 } }).SevkiyatListesiGetir();

            //string ord = new Siparis().OrderNoGetir(new DBEvents().GetGeneric<tblFirmalar>(c => c.Id == 2510).FirstOrDefault());

            watch.Stop();

            //Console.WriteLine(str);
            //Console.WriteLine(DateTime.Now.AddYears(-3).ToShortDateString());
            //Console.WriteLine(1245.ToString("00000"));

            try
            {
                int a = 500 / 300;
                Console.WriteLine(a.ToString());
            }
            catch 
            {
                Console.WriteLine("floating");
                
            }

            //Console.WriteLine((Math.Ceiling(2.98)).ToString());

            Console.WriteLine("süresi : " + watch.ElapsedMilliseconds.ToString());
            //object obj = new Menu().GetKullanici("s", "1");
            //object obj = tblFirmalar.FirmalariGetir(1);
            //new tblLogoCariler().Yukle();

            //new Makina().BakimOnarimMalzemeleriGetir();
            //Console.WriteLine(obje.ToString());

            Console.ReadLine();
        }

        //public static List<T> Getir<T>(List<object> view) where T:class
        //{
        //    List<T> table = new List<T>();
        //    foreach (var item in view)
        //    {
        //        foreach (System.Reflection.PropertyInfo prop in item.GetType().GetProperties)
        //        {
                    
        //        }
        //    }
        //    return table;
        //}
    }
}

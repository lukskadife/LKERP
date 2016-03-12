using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Planlama
    {
        public int PlanAyi;
        public int PlanYili;
        public int PersonelId;

        internal List<vMalzemeStokDurum> TipIplikIhtiyaclariGetir(vKumas kumas)
        {
            List<vMalzemeStokDurum> iplikIhtiyac = new List<vMalzemeStokDurum>();

            //atkı için iplik ihtiyaç hesaplanıyor
            if (kumas.Atki1.HasValue)
            {
                tblMalzemeler iplik = new vMalzemeler().IplikGetir(kumas.Atki1.Value);
                iplikIhtiyac.Add(new vMalzemeStokDurum()
                {
                    MalzemeId = kumas.Atki1.Value,
                    MalzemeBagId = iplik.BaglantiId,
                    MalzemeAdi = iplik.Adi,
                    MalzemeKodu = iplik.Kodu,
                    MinStok = Math.Round((this.DokumaMetre * (kumas.Atki1UstuGr.HasValue ? kumas.Atki1UstuGr.Value : 0)), 4)
                });
            }

            //zemin için iplik ihtiyaç hesaplanıyor
            if (kumas.Zemin1.HasValue)
            {
                double zeminMetre = this.AltZeminMetre * ((kumas.ZeminCozguTel.HasValue ? kumas.ZeminCozguTel.Value : 0) + (kumas.KenarCozguTel.HasValue ? kumas.KenarCozguTel.Value : 0));
                tblMalzemeler iplik = new vMalzemeler().IplikGetir(kumas.Zemin1.Value);
                vMalzemeStokDurum ihtiyac = new vMalzemeStokDurum()
                {
                    MalzemeAdi = iplik.Adi,
                    MalzemeId = kumas.Zemin1.Value,
                    MalzemeKodu = iplik.Kodu,
                    MalzemeBagId = iplik.BaglantiId,
                };
                if (iplik == null || iplik.IplikNo.HasValue == false) ihtiyac.MinStok = 0;
                else
                {
                    switch (iplik.Cins)
                    {
                        case "Denye":
                            ihtiyac.MinStok = (zeminMetre / (9000 / iplik.IplikNo.Value)) / 1000;
                            break;
                        case "Dtex":
                            ihtiyac.MinStok = zeminMetre / (10000 / iplik.IplikNo.Value) / 1000;
                            break;
                        case "Nm":
                            ihtiyac.MinStok = (zeminMetre / iplik.IplikNo.Value) / 1000;
                            break;
                        case "Ne":
                            ihtiyac.MinStok = (zeminMetre / iplik.IplikNo.Value) / 1693;
                            break;
                        default:
                            ihtiyac.MinStok = 0;
                            break;
                    }
                    ihtiyac.MinStok = Math.Round(ihtiyac.MinStok, 4);
                }
                iplikIhtiyac.Add(ihtiyac);                
            }

            //hav için iplik ihtiyaç
            if (kumas.Hav1.HasValue)
            {
                double havMetre = this.HavMetre * (kumas.HavCozguTel.HasValue ? kumas.HavCozguTel.Value : 0);
                tblMalzemeler iplik = new vMalzemeler().IplikGetir(kumas.Hav1.Value);
                vMalzemeStokDurum ihtiyac = new vMalzemeStokDurum()
                {
                    MalzemeAdi = iplik.Adi,
                    MalzemeId = kumas.Hav1.Value,
                    MalzemeKodu = iplik.Kodu,
                    MalzemeBagId = iplik.BaglantiId,
                };
                if (iplik == null || iplik.IplikNo.HasValue == false) ihtiyac.MinStok = 0;
                else
                {
                    switch (iplik.Cins)
                    {
                        case "Denye":
                            ihtiyac.MinStok = (havMetre / (9000 / iplik.IplikNo.Value)) / 1000;
                            break;
                        case "Dtex":
                            ihtiyac.MinStok = havMetre / (10000 / iplik.IplikNo.Value) / 1000;
                            break;
                        case "Nm":
                            ihtiyac.MinStok = (havMetre / iplik.IplikNo.Value) / 1000;
                            break;
                        case "Ne":
                            ihtiyac.MinStok = (havMetre / iplik.IplikNo.Value) / 1693;
                            break;
                        default:
                            ihtiyac.MinStok = 0;
                            break;
                    }
                    ihtiyac.MinStok = Math.Round(ihtiyac.MinStok, 4);
                }
                iplikIhtiyac.Add(ihtiyac);
            }

            return iplikIhtiyac;
        }

        public vMalzemeStokDurum IplikStokGetir(int iplikId)
        {
            List<vTalepKarsilamaAct> talepler = db.GetGeneric<vTalepKarsilamaAct>(c => c.MalzemeId == iplikId && c.DurumId != 4 && c.DurumId != 14);
            vIplikStok stok = db.GetGeneric<vIplikStok>(c => c.MalzemeId == iplikId).FirstOrDefault();

            vTalepKarsilamaAct malzemeBilgi = talepler.FirstOrDefault();

            return new vMalzemeStokDurum()
            {
                GelecekStok = talepler.Sum(s=>s.Miktar),
                MalzemeAdi = malzemeBilgi.MalzemeAdi,
                MalzemeId = malzemeBilgi.MalzemeId,
                MalzemeBagId = malzemeBilgi.MalzemeBagId,
                MalzemeKodu = malzemeBilgi.MalzemeKodu,
                MevcutStok = stok.NetKg
            };
        }

        #region statics

        public static List<vPlanSiparisleri> SiparisUrunleriGetir()
        {
            return new DBEvents().GetGeneric<vPlanSiparisleri>(c => c.PaketlendiMi == false);
        }

        public static List<vPlanSiparisleri2> SiparisUrunleriGetir2()
        {
            return new DBEvents().GetGeneric<vPlanSiparisleri2>();
        }

        public static List<vPlanRezerveUygunlar> RezerveyeUygunlariGetir(int tipId)
        {
            return new DBEvents().GetGeneric<vPlanRezerveUygunlar>(c => c.TipId == tipId);
        }

        public static List<tblFirmalar> MusterileriGetir()
        {
            return new DBEvents().GetGeneric<tblFirmalar>(c => c.BaglantiId == 3 || c.BaglantiId == 4);
        }

        public static bool SiparisPlaniSil(vPlanSiparisleri2 planSiparisi)
        {
            DBEvents db = new DBEvents();
            List<tblPlanlama> silinecekler = db.GetGeneric<tblPlanlama>(c => c.SiparisId == planSiparisi.SiparisId && c.TipId == planSiparisi.TipId);

            bool snc = new DBEvents().DeleteGeneric<tblPlanlama>(silinecekler);
            if (snc)
            {
                List<tblCozgu> cozguler = db.GetGeneric<tblCozgu>(c => c.SiparisId == planSiparisi.SiparisId && c.TipId == planSiparisi.TipId);
                if (db.DeleteGeneric<tblCozgu>(cozguler) == false) throw new Exception("Çözgü silinemedi..!\n\nLütfen çözgüyü manuel siliniz..!");
            }

            return snc;
        }

        public static bool SiparisCozguSil(vPlanSiparisleri2 planSiparisi)
        {
            List<tblCozgu> silinecek = new DBEvents().GetGeneric<tblCozgu>(c => c.SiparisId == planSiparisi.SiparisId && c.TipId == planSiparisi.TipId);

            return new DBEvents().DeleteGeneric<tblCozgu>(silinecek);
        }

        public static List<vPlanRapor> PlanRaporuGetir()
        {
            return new DBEvents().GetGeneric<vPlanRapor>();
        }

        public static List<vPlanlananTipMiktarlari> PlanlananTipMiktarlariGetir(int tipId)
        {
            return new DBEvents().GetGeneric<vPlanlananTipMiktarlari>(c => c.TipId == tipId);
        }

        #endregion

        public void IhtiyacHesapla(double dokumaMetre, int tipId)
        {
            vKumas kumas = vKumas.TipGetir(tipId);

            DokumaMetre = dokumaMetre;
            HavMetre = dokumaMetre * (kumas.HavSevki == null ? 1 : kumas.HavSevki.Value);
            AltZeminMetre = dokumaMetre * (kumas.ZeminSevki == null ? 1 : kumas.ZeminSevki.Value) / 2;
            UstZeminMetre = AltZeminMetre;

            IplikIhtiyaclari = TipIplikIhtiyaclariGetir(kumas);
        }

        public List<vMalzemeStokDurum> IplikIhtiyaclari;
        public double DokumaMetre;
        public double HavMetre;
        public double AltZeminMetre;
        public double UstZeminMetre;

        public List<vTezgahPlanlama> ListTezgahPlanlari;

        private DBEvents db = new DBEvents();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ay">Planı yapılacak ay. 0 verilirse planlama verilerini default'da yüklemez. Aksi halde girilen ayın verileri default olarak yüklenir</param>
        /// <param name="yil">Planı yapılacak yıl. Değeri 0 verilirse içinde bulunulan yıl default'dur</param>
        public Planlama(int personelId, int ay = 0, int yil = 0)
        {
            this.PlanAyi = ay;
            this.PlanYili = yil == 0 ? DateTime.Now.Year : yil;
            this.PersonelId = personelId;

            if (ay != 0) this.ListTezgahPlanlari = TezgahPlanAylikGetir(yil, ay);
        }

        public List<DateTime> YilAyTarihleriVer(int yil, int ay)
        {
            List<DateTime> tarihler = new List<DateTime>();
            int tmp = 1;
            for (int i = 1; i <= 31; i++)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(i.ToString() + "." + ay.ToString() + "." + yil.ToString());
                    tarihler.Add(date);
                }
                catch (FormatException f)
                {
                    tarihler.Add(new DateTime(1900,01,tmp));
                    tmp++;
                }
            }
            return tarihler;
        }

        internal List<vTezgahPlanlama> TezgahPlanAylikGetir(int yil, int ay)
        {
            string dateStr = "";
            string yilStr = yil.ToString();
            string ayStr = ay.ToString();
            int sonrakiYil = yil, sonrakiAy = ay + 1;

            //Planı getirilecek ay Aralık'tan başlanıyorsa sonraki ay Ocak olarak set edilmeli. Değilse ay 1 artırılarak set edilmeli.
            if (ay == 12)
            {
                yilStr += "," + (yil + 1).ToString();
                ayStr += ", 1";
                sonrakiYil = yil + 1;
                sonrakiAy = 1;
            }
            else ayStr += "," + (ay + 1).ToString();

            foreach (DateTime date in YilAyTarihleriVer(yil, ay))
                dateStr += date.Year + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00") + ",";

            foreach (DateTime date in YilAyTarihleriVer(sonrakiYil, sonrakiAy))
                dateStr += date.Year + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00") + ",";

            return db.GetGenericWithSQLQuery<vTezgahPlanlama>("exec dbo.spTezgahPlanlariAylikGetir '" + yilStr + "' ,'" + ayStr + "', '" + dateStr + "'", new object[0]).ToList();
        }

        public vPlanlama TezgahPlanDetayGetir(int tezgahId, DateTime date)
        {
            vPlanlama plan = db.GetGeneric<vPlanlama>(c => c.TezgahId == tezgahId && c.Tarih == date).FirstOrDefault();

            if (plan != null)
            {
                plan.ToplamDokunan = db.GetGeneric<tblHamKumaslar>(c => c.SiparisId == plan.SiparisId && c.TipId == plan.TipId).Sum(s => s.Metre);
                plan.ToplamPlan = db.GetGeneric<tblPlanlama>(c => c.SiparisId == plan.SiparisId && c.TipId == plan.TipId).Sum(s => s.Miktar);
            }
            
            return plan;
        }

        private List<int> _OtelenenSiparisler;

        /// <summary>
        /// Tezgah planlarını öteler/geri çeker. Return; 1 ise başarılı bir öteleme/geri çekme, 0 ise öteleme/geri çekme yapılamamış, -1 ise geri çekilecek bir önceki gün müsait olmadığından geri çekme yapılamamıştır.
        /// </summary>
        /// <param name="tezgahId">öteleme/geri çekme yapılacak tezgahın id'si</param>
        /// <param name="tarih">verilen tarihten sonraki planlar ötelenecek/geri çekilecek</param>
        /// <param name="gunSayisi">Pozitif ise öteleme, negatif ise geri çekme yapılacaktır.</param>
        /// <returns></returns>
        public int TezgahPlanOtele(int tezgahId, DateTime tarih, int gunSayisi, bool atkiOtelemesiMi = false)
        {
            List<tblPlanlama> listPlan;

            if (gunSayisi < 0)
            {
                int gunCount = -1;
                if (tarih.AddDays(-1).DayOfWeek == DayOfWeek.Sunday) gunCount--;
                if (db.GetGeneric<tblPlanlama>(c => c.TezgahId == tezgahId && c.Tarih == tarih.AddDays(gunCount)).FirstOrDefault() != null) return -1;
            }

            listPlan = db.GetGeneric<tblPlanlama>(c => c.TezgahId == tezgahId && c.Tarih >= tarih).OrderBy(o=>o.Tarih).ToList();
            if (listPlan == null) return 0;

            listPlan.ForEach(c => c.Tarih = c.Tarih.AddDays(gunSayisi));

            foreach (tblPlanlama item in listPlan)
            {
                if (item.Tarih.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (gunSayisi > 0) item.Tarih = item.Tarih.AddDays(1);
                    else item.Tarih = item.Tarih.AddDays(-1);

                    if (listPlan.Exists(c => c != item && c.Tarih == item.Tarih))
                    {
                        if (gunSayisi > 0) listPlan.FindAll(f => f != item && f.Tarih >= item.Tarih).ForEach(c => c.Tarih = c.Tarih.AddDays(1));
                        else listPlan.FindAll(f => f != item && f.Tarih >= item.Tarih).ForEach(c => c.Tarih = c.Tarih.AddDays(-1));
                    }
                }
                if (atkiOtelemesiMi) item.OtelemeKontrol = true;
            }

            int snc = (db.UpdateGeneric<tblPlanlama>(listPlan)) == true ? 1 : 0;
            if (atkiOtelemesiMi && snc == 1)
            {
                 //daha sonra bu değişken kullanılarak atkı ötelemesi ile ötelenen siparişlerin öteleme sayısı update edilecek.
                foreach (var item in listPlan.GroupBy(g=>g.SiparisId)) _OtelenenSiparisler.Add(item.Key);
            }

            return snc;
        }

        /// <summary>
        /// Atkı girişleri tezgah planlarını öteler.
        /// </summary>
        /// <param name="atkiGiris">Atkı girişi, ilgili tarihteki plan metresinin yarısının %20 fazlasından az ise tezgahtaki planlar 1 gün ötelenir.</param>
        /// <returns></returns>
        public int TezgahPlanOtele(tblTezgahAtkiGiris atkiGiris)
        {
            tblPlanlama plan = db.GetGeneric<tblPlanlama>(c => c.TezgahId == atkiGiris.TezgahId && c.Tarih == atkiGiris.Tarih && c.TipId == atkiGiris.TipId).FirstOrDefault();
            if (plan == null) return 1;
            tblKumas tip = db.GetGeneric<tblKumas>(c=>c.Id == atkiGiris.TipId).FirstOrDefault();

            double? dokMetre = null;
            int fark = atkiGiris.AtkiBitis - atkiGiris.AtkiBaslangic;
            if (tip.AtkiSiklik.HasValue) dokMetre = fark * 1000 * 2 / (tip.AtkiSiklik.Value * 100);

            if (dokMetre.HasValue == false) return 0;

            if (dokMetre < (plan.Miktar / 2) + (plan.Miktar * 0.2))
            {
                int snc = this.TezgahPlanOtele(atkiGiris.TezgahId, atkiGiris.Tarih.Value, 1, atkiOtelemesiMi: true);
                if (snc == 1)
                {
                    atkiGiris.PlanOteledi = true;
                    db.UpdateGeneric<tblTezgahAtkiGiris>(atkiGiris);
                }
                return snc;
            }

            return 1;
        }

        public int TezgahPlanOtele()
        {
            DateTime atkiSonTarih = Makina.TezgahAtkiSonGirisTarihiGetir();
            List<tblPlanlama> planlar = db.GetGeneric<tblPlanlama>(c => c.Tarih == atkiSonTarih);

            foreach (var plan in planlar)
            {
                tblKumas tip = db.GetGeneric<tblKumas>(c => c.Id == plan.TipId).FirstOrDefault();
                List<tblTezgahAtkiGiris> atkiGirisleri = db.GetGeneric<tblTezgahAtkiGiris>(c => c.Tarih == plan.Tarih && c.TezgahId == plan.TezgahId && c.TipId == plan.TipId);


                double? dokMetre = null;
                int fark = atkiGirisleri == null ? 0 : atkiGirisleri.Sum(c=>c.AtkiBitis - c.AtkiBaslangic);
                if (tip.AtkiSiklik.HasValue) dokMetre = fark * 1000 * 2 / (tip.AtkiSiklik.Value * 100);

                if (dokMetre.HasValue == false) return 0;
                double? kontMetre = (plan.Miktar / 2) + (plan.Miktar * 0.2);
                kontMetre = Math.Round(kontMetre.Value, 2);

                if (dokMetre < kontMetre)
                {
                    plan.OtelemeKontrol = false;
                    plan.EksikDokuma = kontMetre - dokMetre;

                    if (atkiGirisleri != null && atkiGirisleri.Count != 0)//&& snc == 1)
                    {
                        atkiGirisleri.ForEach(f => f.PlanOteledi = true);
                        db.UpdateGeneric<tblTezgahAtkiGiris>(atkiGirisleri);
                    }
                }
            }

            if (db.UpdateGeneric<tblPlanlama>(planlar))
            {
                planlar = db.GetGeneric<tblPlanlama>(c => c.OtelemeKontrol == false);

                var otelemeList = planlar.GroupBy(g => new { g.SiparisId, g.TezgahId, g.TipId, g.Miktar }).Select(s => new
                {
                    ToplamEksik = s.Sum(f => f.EksikDokuma),
                    TezgahId = s.Key.TezgahId,
                    PlanMiktar =s.Key.Miktar,
                    Tarih = s.FirstOrDefault().Tarih
                });

                _OtelenenSiparisler = new List<int>();
                foreach (var item in otelemeList)
                {
                    if (((item.PlanMiktar / 2) + (item.PlanMiktar * 0.2)) <= item.ToplamEksik)
                    {
                        this.TezgahPlanOtele(item.TezgahId, item.Tarih, 1, true);
                    }
                }

                if (_OtelenenSiparisler != null && _OtelenenSiparisler.Count != 0)
                {
                    string idStr = "";
                    foreach (var item in _OtelenenSiparisler) idStr += item.ToString() + ",";
                    idStr = idStr.TrimEnd(',');
                    //ötelenen tezgahların üzerlerindeki diğer siparişlerde ötelenmiş olduğundan bu siparişler de 1 er gün ötelendi olarak update edilmeli.
                    db.GetGenericWithSQLQuery<string>("update tblPlanlama set AtkiOtelemesi = ISNULL(AtkiOtelemesi, 0) + 1 where SiparisId in (" + idStr + ")", new object[0]);
                }
            }

            return 0;
        }

        private DateTime? GunSec(int tezgahId, List<DateTime> gunler)
        {
            foreach (DateTime gun in gunler)
            {
                tblPlanlama plan = db.GetGeneric<tblPlanlama>(c => c.TezgahId == tezgahId && c.Tarih == gun).FirstOrDefault();
                if (plan == null) return gun;
            }

            return null;
        }

        public bool TezgahPlanTasi(int tezgahId, List<DateTime> gunler, List<int> listTasinacaklar)
        {
            bool snc = true;
            for (int i = 0; i < listTasinacaklar.Count; i++)
            {
                DateTime? gun = GunSec(tezgahId, gunler);
                if (gun != null)
                {
                    tblPlanlama plan = db.GetGeneric<tblPlanlama>(c => c.Id == listTasinacaklar[i]).FirstOrDefault();
                    plan.TezgahId = tezgahId;
                    plan.Tarih = gun.Value;
                    if (db.UpdateGeneric<tblPlanlama>(plan) == false) snc = false;
                }
                else snc = false;
            }

            return snc;
        }

        /// <summary>
        /// Yeni bir planlama kaydedilecekse plan.Id 'si 0'dır. Sistem buna göre yeni kaydı açar. Bunun için de girilen plan.Tarih ve  plan.Tarih + gunSayisi arasındaki günlerde herhangi bir plan olmamalıdır.
        /// Eğer bu günler arasında bir plan varsa plan kaydedilmez ve fonksiyon -1 dönderir. 
        /// 
        /// Eski bir planlama üzerinde update yapılacaksa plan.Id'si 0'dan farklıdır ve buna göre verilen plan update edilir.
        /// </summary>
        /// <param name="plan">vPlanlama türünde veridir.</param>
        /// <param name="gunSayisi">plan.Tarih'inden itibaren kaç güne plan atılacağını belirler. Her bir güne dokunacak olan bilgi plan.Miktar içerisinde saklıdır.</param>
        /// <returns></returns>
        public int PlanlamaKaydet(vPlanlama plan, int gunSayisi)
        {
            if (plan.Id == 0)
            {
                if (db.GetGeneric<tblPlanlama>(c => c.TezgahId == plan.TezgahId && c.Tarih >= plan.Tarih.Value && c.Tarih < plan.Tarih.Value.AddDays(gunSayisi)).Count > 0) return -1;

                List<tblPlanlama> listPlan = new List<tblPlanlama>();

                int tarihIndisi = 0;
                for (int i = 0; i < gunSayisi; i++)
                {
                    tblPlanlama tblPlan = plan.ViewToTbl();
                    if (tblPlan.Tarih.AddDays(tarihIndisi).DayOfWeek != DayOfWeek.Sunday)
                    {
                        tblPlan.Tarih = tblPlan.Tarih.AddDays(tarihIndisi);
                        listPlan.Add(tblPlan);
                    }
                    else gunSayisi++; //plan pazara geliyorsa o gün atlanmalı ve gün sayısı 1 artırılmalı.
                    tarihIndisi++;
                }

                return (db.SaveGeneric<tblPlanlama>(listPlan) == true) ? 1 : 0;
            }
            else return (db.UpdateGeneric<tblPlanlama>(plan.ViewToTbl()) == true) ? 1 : 0;
        }

        public bool TezgahPlanKaydet(tblPlanlama plan)
        {
            return db.SaveGeneric<tblPlanlama>(plan);
        }

        public bool TezgahPlaniSil(vPlanlama plan)
        {
            if (plan == null) return false;

            bool snc = true;
            tblCozgu cozgu = db.GetGeneric<tblCozgu>(c => c.SiparisId == plan.SiparisId && c.TipId == plan.TipId).FirstOrDefault();

            //eğer silinen plan çözgüyü sıfırlıyorsa o çözgü kaydı silinmelidir. Aksi halde miktarı düşürülerek update edilmelidir.
            if (cozgu.Miktar - plan.Miktar <= 0) snc = db.DeleteGeneric<tblCozgu>(cozgu);
            else
            {
                cozgu.Miktar -= plan.Miktar.Value;
                snc = db.UpdateGeneric<tblCozgu>(cozgu);
            }

            if (snc == false) return false;

            return db.DeleteGeneric<tblPlanlama>(plan.ViewToTbl());
        }

        public DateTime TezgahMusaitTarihiGetir(int tezgahId)
        {
            DateTime? tarih = null;
            try
            {
                tarih = db.GetGeneric<tblPlanlama>(c => c.TezgahId == tezgahId && c.Tarih >= DateTime.Today).Max(t => t.Tarih);
            }
            catch (Exception) { }
            
            if (tarih == null) return DateTime.Today.AddDays(1);
            else return tarih.Value.AddDays(1);
        }

        /// <summary>
        /// Çözgü sepetini doldurur. Eğer verilen sipariş daha önce çözgü sepetine atılmış ise Exception verir.
        /// </summary>
        public vCozgu CozguyeAt(int tipId, double miktar, int siparisId)
        {
            if (miktar == 0) throw new Exception("Çözgüye atılacak miktar 0 olamaz..!");

            vPlanSiparisleri2 kontrol = db.GetGeneric<vPlanSiparisleri2>(c => c.SiparisId == siparisId).FirstOrDefault();
            if (kontrol != null && (kontrol.CozguMetre > kontrol.PlanMetre )) throw new Exception("Çözgü metresi plan metresinden fazla olamaz..!");

            tblCozgu tblCozgu = new tblCozgu() { TipId = tipId, Miktar = miktar, OlusturmaTarihi = DateTime.Now, PersId = this.PersonelId, SiparisId = siparisId };
            if (db.SaveGeneric<tblCozgu>(ref tblCozgu))
                return db.GetGeneric<vCozgu>(c => c.TipId == tblCozgu.TipId).FirstOrDefault();
            else return null;
        }

        public static List<vCozgu> CozguSepetleriGetir()
        {
            return new DBEvents().GetGeneric<vCozgu>(c=>c.Miktar > 0);
        }

        public vCozguIsEmri CozguIsEmriHesapla(double dokumaMetre)
        {
            vKumas kumas = vKumas.TipGetir(_CozguIsEmriTipId);
            vCozguIsEmri hesap = new vCozguIsEmri();
            hesap.DokumaMetre = dokumaMetre;
            hesap.HavMetre = dokumaMetre * (kumas.HavSevki == null ? 1 : kumas.HavSevki.Value);
            hesap.AltZeminMetre = dokumaMetre * (kumas.ZeminSevki == null ? 1 : kumas.ZeminSevki.Value) / 2;
            hesap.UstZeminMetre = hesap.AltZeminMetre;
            hesap.TipId = _CozguIsEmriTipId;
            hesap.TipNo = kumas.TipNo;

            return hesap;
        }

        private int _CozguIsEmriTipId;
        public void CozguIsEmirleriYukle(int tipId)
        {
            _CozguIsEmriTipId = tipId;
            //CozguIsEmirleri = db.GetGeneric<vCozguIsEmri>(c => c.TipId == tipId);

            //CozguIsEmirleri.ForEach(t => t.Tezgahlar = new Makina().MakinalariGetir(8));
        }

        public List<vCozguIsEmri> CozguIsEmirleri = new List<vCozguIsEmri>();
        public void CozguIsEmriEkle(vCozguIsEmri hesaplar)
        {
            vCozgu cozgusu = db.GetGeneric<vCozgu>(c => c.TipId == _CozguIsEmriTipId).FirstOrDefault();
            if ((CozguIsEmirleri.Count > 0 ? CozguIsEmirleri.FindAll(f => f.Cozgu == "Hav").Sum(s => s.DokumaMetre) : 0) + hesaplar.DokumaMetre > (cozgusu == null ? 0 : cozgusu.Miktar))
            {
                throw new Exception("Toplam iş emri çözgü metresinden fazla olamaz..!");
            }

            hesaplar.Islem = Convert.ToInt64(DateTime.Now.ToString("yyMMddhhmmssfff"));

            List<tblMakinalar> tezgahlar = new Makina().MakinalariGetir(1);
            vKumas kumas = vKumas.TipGetir(_CozguIsEmriTipId);
            tblMalzemeler havIplik = db.GetGeneric<tblMalzemeler>(c => c.Id == kumas.Hav1).FirstOrDefault();
            tblMalzemeler zeminIplik = db.GetGeneric<tblMalzemeler>(c => c.Id == kumas.Zemin1).FirstOrDefault();

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Hav",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.HavMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = havIplik == null ? 0 : havIplik.Id,
                IplikKodu = havIplik == null ? null : havIplik.Kodu,
                IplikAdi = havIplik == null ? null : havIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Alt Zemin",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.AltZeminMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = zeminIplik == null ? 0 : zeminIplik.Id,
                IplikKodu = zeminIplik == null ? null : zeminIplik.Kodu,
                IplikAdi = zeminIplik == null ? null : zeminIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Üst Zemin",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.UstZeminMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = zeminIplik == null ? 0 : zeminIplik.Id,
                IplikKodu = zeminIplik == null ? null : zeminIplik.Kodu,
                IplikAdi = zeminIplik == null ? null : zeminIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });
        }

        public bool CozguIsEmriSil(vCozguIsEmri silinecek)
        {
            if (silinecek.Id == 0)
            {
                CozguIsEmirleri.Remove(silinecek);
                return true;
            }
            else if (db.DeleteGeneric<tblCozguIsEmri>(silinecek.ViewToTbl()))
            {
                CozguIsEmirleri.Remove(silinecek);
                return true;
            }

            return false;
        }

        public bool CozguIsEmirleriKaydet()
        {
            if (CozguIsEmirleri.Exists(c => c.TezgahId == 0)) throw new Exception("Tezgahı seçilmeyen iş emri var.\n\nKaydedilemez..!");

            List<vCozguIsEmri> viewToSave = CozguIsEmirleri.FindAll(c => c.Id == 0);
            List<vCozguIsEmri> viewToUpd = CozguIsEmirleri.FindAll(c => c.Id != 0);

            if (db.UpdateGeneric<tblCozguIsEmri>(vCozguIsEmri.ViewToTbl(viewToUpd)) == false) return false;

            List<tblCozguIsEmri> tblToSave = vCozguIsEmri.ViewToTbl(viewToSave);
            bool snc = true;
            if (db.SaveGeneric<tblCozguIsEmri>(ref tblToSave))
            {
                for (int i = 0; i < viewToSave.Count; i++) viewToSave[i].Id = tblToSave[i].Id;

                CozguIsEmirleri.Clear();
                CozguIsEmirleri.AddRange(viewToSave);
                CozguIsEmirleri.AddRange(viewToUpd);
                CozguIsEmirleri = CozguIsEmirleri.OrderBy(o => o.Id).ToList();
            }
            else snc = false;

            return snc;
        }

        public bool RezerveEt(int musteriId, int tipId, string kalitePuan, double metre)
        {
            tblPlanRezerve planRezerve = db.GetGeneric<tblPlanRezerve>(c => c.MusteriId == musteriId && c.TipId == tipId && c.KalitePuan == kalitePuan).FirstOrDefault();
            if (planRezerve != null)
            {
                planRezerve.RezerveMetre += metre;
                planRezerve.Tarih = DateTime.Now;
                return db.UpdateGeneric<tblPlanRezerve>(planRezerve);
            }
            else
            {
                planRezerve = new tblPlanRezerve()
                {
                    KalitePuan = kalitePuan,
                    MusteriId = musteriId,
                    RezerveMetre = metre,
                    Tarih = DateTime.Now,
                    TipId = tipId
                };

                return db.SaveGeneric<tblPlanRezerve>(planRezerve);
            }
        }

        public List<vPlanRezerveler> RezerveleriGetir(int tipId, string kalitePuan)
        {
            return db.GetGeneric<vPlanRezerveler>(c => c.TipId == tipId && c.KalitePuan == kalitePuan && c.RezerveMetre > 0);
        }

        public bool RezerveSil(vPlanRezerveler rezerve)
        {
            return db.DeleteGeneric<tblPlanRezerve>(rezerve.ViewToTbl());
        }

        public bool SiparisTerminVer(vPlanSiparisleri siparis, DateTime? yeniTermin)
        {
            tblSiparisAct siparisAct = db.GetGeneric<tblSiparisAct>(c => c.Id == siparis.SiparisActId).FirstOrDefault();
            siparisAct.TerminTarihi = yeniTermin;
            tblSiparisler siparisUst = db.GetGeneric<tblSiparisler>(c => c.Id == siparisAct.SiparisId).FirstOrDefault();
            if (siparisUst.TerminTarihi == null || siparisUst.TerminTarihi < yeniTermin)
            {
                siparisUst.TerminTarihi = yeniTermin;
                if (db.UpdateGeneric<tblSiparisler>(siparisUst) == false) return false;
            }
            return db.UpdateGeneric<tblSiparisAct>(siparisAct);
        }
    }

    public class CozguIsEmri
    {
        public CozguIsEmri(int tipId)
        {
            _CozguIsEmriTipId = tipId;
        }

        public vCozguIsEmri CozguIsEmriHesapla(double dokumaMetre)
        {
            vKumas kumas = vKumas.TipGetir(_CozguIsEmriTipId);
            vCozguIsEmri hesap = new vCozguIsEmri();
            hesap.DokumaMetre = dokumaMetre;
            hesap.HavMetre = dokumaMetre * (kumas.HavSevki == null ? 1 : kumas.HavSevki.Value);
            hesap.AltZeminMetre = dokumaMetre * (kumas.ZeminSevki == null ? 1 : kumas.ZeminSevki.Value) / 2;
            hesap.UstZeminMetre = hesap.AltZeminMetre;
            hesap.TipId = _CozguIsEmriTipId;
            hesap.TipNo = kumas.TipNo;

            return hesap;
        }

        private int _CozguIsEmriTipId;
        private DBEvents db = new DBEvents();

        public void CozguIsEmirleriYukle(int tipId)
        {
            _CozguIsEmriTipId = tipId;
            //CozguIsEmirleri = db.GetGeneric<vCozguIsEmri>(c => c.TipId == tipId);

            //CozguIsEmirleri.ForEach(t => t.Tezgahlar = new Makina().MakinalariGetir(8));
        }

        public List<vCozguIsEmri> CozguIsEmirleri = new List<vCozguIsEmri>();
        public void CozguIsEmriEkle(vCozguIsEmri hesaplar)
        {
            vCozgu cozgusu = db.GetGeneric<vCozgu>(c => c.TipId == _CozguIsEmriTipId).FirstOrDefault();
            if ((CozguIsEmirleri.Count > 0 ? CozguIsEmirleri.FindAll(f => f.Cozgu == "Hav").Sum(s => s.DokumaMetre) : 0) + hesaplar.DokumaMetre > (cozgusu == null ? 0 : cozgusu.Miktar))
            {
                throw new Exception("Toplam iş emri çözgü metresinden fazla olamaz..!");
            }

            hesaplar.Islem = Convert.ToInt64(DateTime.Now.ToString("yyMMddhhmmssfff"));

            List<tblMakinalar> tezgahlar = new Makina().MakinalariGetir(1);
            vKumas kumas = vKumas.TipGetir(_CozguIsEmriTipId);
            tblMalzemeler havIplik = db.GetGeneric<tblMalzemeler>(c => c.Id == kumas.Hav1).FirstOrDefault();
            tblMalzemeler zeminIplik = db.GetGeneric<tblMalzemeler>(c => c.Id == kumas.Zemin1).FirstOrDefault();

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Hav",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.HavMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = havIplik == null ? 0 : havIplik.Id,
                IplikKodu = havIplik == null ? null : havIplik.Kodu,
                IplikAdi = havIplik == null ? null : havIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Alt Zemin",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.AltZeminMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = zeminIplik == null ? 0 : zeminIplik.Id,
                IplikKodu = zeminIplik == null ? null : zeminIplik.Kodu,
                IplikAdi = zeminIplik == null ? null : zeminIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });

            CozguIsEmirleri.Add(new vCozguIsEmri()
            {
                Cozgu = "Üst Zemin",
                DokumaMetre = hesaplar.DokumaMetre,
                Metre = hesaplar.UstZeminMetre,
                Id = 0,
                PersonelId = hesaplar.PersonelId,
                Tarih = DateTime.Now,
                Tezgahlar = tezgahlar,
                TipId = hesaplar.TipId,
                TipNo = hesaplar.TipNo,
                IplikId = zeminIplik == null ? 0 : zeminIplik.Id,
                IplikKodu = zeminIplik == null ? null : zeminIplik.Kodu,
                IplikAdi = zeminIplik == null ? null : zeminIplik.Adi,
                IplikTelAdedi = kumas == null ? null : kumas.HavCozguTel,
                Islem = hesaplar.Islem
            });
        }

        public bool CozguIsEmriSil(vCozguIsEmri silinecek)
        {
            if (silinecek.Id == 0)
            {
                CozguIsEmirleri.Remove(silinecek);
                return true;
            }
            else if (db.DeleteGeneric<tblCozguIsEmri>(silinecek.ViewToTbl()))
            {
                CozguIsEmirleri.Remove(silinecek);
                return true;
            }

            return false;
        }

        public bool CozguIsEmirleriKaydet()
        {
            if (CozguIsEmirleri.Exists(c => c.TezgahId == 0)) throw new Exception("Tezgahı seçilmeyen iş emri var.\n\nKaydedilemez..!");

            List<vCozguIsEmri> viewToSave = CozguIsEmirleri.FindAll(c => c.Id == 0);
            List<vCozguIsEmri> viewToUpd = CozguIsEmirleri.FindAll(c => c.Id != 0);

            if (db.UpdateGeneric<tblCozguIsEmri>(vCozguIsEmri.ViewToTbl(viewToUpd)) == false) return false;

            List<tblCozguIsEmri> tblToSave = vCozguIsEmri.ViewToTbl(viewToSave);
            bool snc = true;
            if (db.SaveGeneric<tblCozguIsEmri>(ref tblToSave))
            {
                for (int i = 0; i < viewToSave.Count; i++) viewToSave[i].Id = tblToSave[i].Id;

                CozguIsEmirleri.Clear();
                CozguIsEmirleri.AddRange(viewToSave);
                CozguIsEmirleri.AddRange(viewToUpd);
                CozguIsEmirleri = CozguIsEmirleri.OrderBy(o => o.Id).ToList();
            }
            else snc = false;

            return snc;
        }

    }
}

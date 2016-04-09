using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;
using System.IO;

namespace LKLibrary.Classes
{
    public class Rapor
    {
        public string RaporTamAdi;
        public string RaporAdi;
        public bool RaporGuncelMi;

        private tblAyarlar RaporAyar;
        private string RaporServerFullPath;

        public Rapor(string raporAdi)
        {
            this.RaporAdi = raporAdi;
            RaporAyar = new DBEvents().GetGeneric<tblAyarlar>(c => c.Adi == raporAdi).FirstOrDefault();
            
            if (RaporAyar == null || string.IsNullOrEmpty(RaporAyar.Aciklama))
            {
                this.RaporGuncelMi = false;
                return;
            }

            //this.RaporDataSetName = this.RaporAyar.Deger; //ReportViewer üzerindeki dataset adı
            //if (RaporAyar.BaglantiId == -1) 
                RaporServerFullPath = RaporAyar.Aciklama + "\\" + this.RaporAdi + ".rdlc";
            //else
            //{
            //    tblAyarlar ayarUst = new DBEvents().GetGeneric<tblAyarlar>(c => c.Id == RaporAyar.BaglantiId).FirstOrDefault();
            //    this.RaporServerFullPath = ayarUst.Aciklama + "\\" + this.RaporAdi + ".rdlc";
            //}

            RaporGuncelle(raporAdi);
        }

        private void RaporCopyToLocal()
        {
            try
            {
                LKLibrary.DosyaServisi.FileOperationServicesClient servis = new LKLibrary.DosyaServisi.FileOperationServicesClient();

                LKLibrary.DosyaServisi.SenfoniFiles file = servis.GetFile(this.RaporServerFullPath);

                if (file != null)
                {
                    if (File.Exists(this.RaporTamAdi)) File.Delete(this.RaporTamAdi);
                    ExtensionMethods.ByteArrayToFile(this.RaporTamAdi, file.FileByteArray);
                    this.RaporGuncelMi = true;
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
                this.RaporGuncelMi = false;
            }
        }

        private void RaporGuncelle(string raporAdi)
        {
            string tempDosyaYolu = System.Environment.GetEnvironmentVariable("TEMP") + @"\" + @"LKERP\" + raporAdi + ".rdlc";
            this.RaporTamAdi = tempDosyaYolu;
            string path = tempDosyaYolu.Substring(0, tempDosyaYolu.LastIndexOf('\\'));
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
            if (File.Exists(tempDosyaYolu))
            {
                tblAyarlar raporGuncelMi = Rapor.RaporGetir(raporAdi);
                if (raporGuncelMi != null && raporGuncelMi.Deger != null)
                {
                    try
                    {
                        if (Convert.ToDateTime(raporGuncelMi.Deger) <= File.GetLastWriteTime(tempDosyaYolu)) RaporGuncelMi = true;
                        else RaporCopyToLocal();
                    }
                    catch (Exception e)
                    {
                        string str = e.Message;
                    }
                }
            }
            else RaporCopyToLocal();
        }

        #region statics

        public static tblAyarlar RaporGetir(string ayarAdi)
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.Adi == ayarAdi).FirstOrDefault();
        }

        public static tblAyarlar EtiketGetir(int raporId)
        {
            return new DBEvents().GetGeneric<tblAyarlar>(c => c.BaglantiId == raporId && c.GosterilsinMi == true).FirstOrDefault();
        }

        public static List<vSatisSiparisRaporu> SatisSiparisRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSatisSiparisRaporu>("EXEC spSatisSiparisRaporu", new string[0]);
        }

        public static List<vIplikCikisRaporu> IplikCikisRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vIplikCikisRaporu>("EXEC spIplikCikisRaporu '"+ ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vIplikGirisRaporu> IplikGirisRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vIplikGirisRaporu>("EXEC spIplikGirisRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vKaliteDagilimTip> TipBazliHamKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimTip>("EXEC spKaliteDagilim '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "', 'ham'", new string[0]);
        }

        public static List<vKaliteDagilimTip> TipBazliMamulKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimTip>("EXEC spKaliteDagilim '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "', 'mamul'", new string[0]);
        }

        public static List<vKaliteDagilimTip> TipBazliRenksizMamulKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimTip>("EXEC spKaliteDagilim '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "', 'MamulRenksiz'", new string[0]);
        }

        public static List<vKaliteDagilimMusteri> MusteriBazliHamKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimMusteri>("EXEC spKaliteDagilimMusteriRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "', 'ham'", new string[0]);
        }

        public static List<vKaliteDagilimMusteri> MusteriBazliMamulKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimMusteri>("EXEC spKaliteDagilimMusteriRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "', 'mamul'", new string[0]);
        }

        public static List<vKaliteDagilimTezgah> TezgahBazliKaliteDagilimiGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKaliteDagilimTezgah>("EXEC spKaliteDagilimTezgahRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vMamulKumasUretimRaporu> MamulUretimRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vMamulKumasUretimRaporu>("EXEC spMamulKumasUretimRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vMamulSevkiyatRaporu> MamulSevkiyatRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vMamulSevkiyatRaporu>("EXEC spMamulKumasSevkiyatRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }
        public static List<vMamulSevkiyatRaporu> FasonSevkiyatRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vMamulSevkiyatRaporu>("EXEC spFasonSevkiyatRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }
        public static List<vTezgahRandimanRapor> TezgahRandimanRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vTezgahRandimanRapor>("EXEC spTezgahRandiman '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }
        public static List<vKimyasalGirisRaporu> KimyasalGirisRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKimyasalGirisRaporu>("EXEC spKimyasalGirisRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vKimyasalCikisRaporu> KimyasalCikisRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKimyasalCikisRaporu>("EXEC spKimyasalCikisRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vHataDagilimHamTipRaporu> HataDagilimHamTipRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vHataDagilimHamTipRaporu>("EXEC spHataDagilimHamTip '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vHataDagilimHamTezgahRaporu> HataDagilimHamTezgahRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vHataDagilimHamTezgahRaporu>("EXEC spHataDagilimHamTezgah '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vHataDagilimMamulRaporu> HataDagilimMamulRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vHataDagilimMamulRaporu>("EXEC spHataDagilimiMamul '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vHamUretimRaporu> HamUretimRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vHamUretimRaporu>("EXEC spHamKumasUretimRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        //Gökhan 20.08.2014
        public static List<vReProcessRaporu> ReProcessRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vReProcessRaporu>("EXEC spReProcessRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        public static List<vIplikStok> IplikStokRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vIplikStok>("EXEC spIplikStok", new string[0]);
        }

        public static List<vMamulKumaslar> MamulStokRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vMamulKumaslar>("EXEC spMamulStokRaporu", new string[0]);
        }

        public static List<vMamulKumaslar> FasonaGonderilecekMamulleriGetir()
        {
            return new DBEvents().GetGeneric<vMamulKumaslar>(c => c.SevkId == 0 && c.Durum == "Fason");
        }

        public static List<vKimyasalStokRaporu> KimyasalStokRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vKimyasalStokRaporu>("EXEC spKimyasalStokRaporu", new string[0]);
        }

        public static List<vBoyaPlani> BoyaPlaniGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vBoyaPlani>("EXEC spBoyaPlaniGetir", new string[0]);
        }

        public static List<T> YoneticiKonsolRaporuGetir<T>(string raporIsmi) where T : class
        {
            return new DBEvents().GetGenericWithSQLQuery<T>("exec spYoneticiKonsolu '" + raporIsmi + "'", new string[0]);
        }

        public static List<vKimyasalSarfiyatTipBazli> KimyasalSarfiyatlariTipBazliGetir(int yil, int ay)
        {
            return new DBEvents().GetGenericWithSQLQuery<vKimyasalSarfiyatTipBazli>("exec spKimyasalSarfiyatlariAylikTipBazli " + yil.ToString() + "," + ay.ToString(), new string[0]);
        }

        public static List<vSiparisTerminRaporu> SiparisBazliTerminRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiparisTerminRaporu>("exec spTerminYonetimRaporu 'siparis'", new string[0]);
        }

        public static List<vSiparisSatirTerminRaporu> SiparisSatirBazliTerminRaporuGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiparisSatirTerminRaporu>("exec spTerminYonetimRaporu 'satir'", new string[0]);
        }

        public static List<vBoyahaneHareketRaporu> BoyahaneHareketRaporuGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGenericWithSQLQuery<vBoyahaneHareketRaporu>("EXEC spBoyahaneHareketRaporu '" + ilkTarih.Date.ToDateString() + "','" + sonTarih.Date.ToDateString() + "'", new string[0]);
        }

        //Gökhan 16.05.2014
        public static List<vKimyasalReceteActLog> OnayliReceteDegisiklikleriRaporuGetir()
        {
            return new DBEvents().GetGeneric<vKimyasalReceteActLog>();
        }

        //Gökhan 26.08.2014
        public static List<vSiradakiProcessRaporu> SiradakiProcessGetir()
        {
            return new DBEvents().GetGenericWithSQLQuery<vSiradakiProcessRaporu>("EXEC spSiradakiProcessRaporu ", new string[0]);
        }

        #endregion
    }
}

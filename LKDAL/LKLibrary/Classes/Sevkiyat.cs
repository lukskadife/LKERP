using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKLibrary.DbClasses;

namespace LKLibrary.Classes
{
    public class Sevkiyat
    {
        private DBEvents db = new DBEvents();
        private vSevk _SevkBelge;

        public vSevk SevkBelge
        {
            get { return _SevkBelge; }
            set
            {
                _SevkBelge = value;

                if (value != null && value.Id != 0)
                {
                    Okutulanlar = db.GetGenericWithSQLQuery<vSevkiyatBarkodlari>("EXEC	spSevkiyatBarkodlariGetir " + value.Id.ToString(), new string[0]);
                    if (Okutulanlar != null) Okutulanlar = Okutulanlar.OrderByDescending(o => o.KutuId).ToList();
                    Siparisleri = db.GetGeneric<vSiparisAct>(c => c.SiparisId == value.SiparisId);
                    if (Siparisleri != null) Siparisleri = Siparisleri.OrderBy(c => c.Sira).ToList();
                }

                if (Okutulanlar == null || value == null) Okutulanlar = new List<vSevkiyatBarkodlari>();
                if (Siparisleri == null || value == null) Siparisleri = new List<vSiparisAct>();
            }
        }

        public List<vSevkiyatBarkodlari> Okutulanlar { get; set; }
        public List<vSiparisAct> Siparisleri { get; set; }

        public void BarkodOkut(string barkod)
        {
            if (_SevkBelge == null) throw new Exception("Belge seçili değil.\n\nOkutulamaz..!");

            vSevkiyatBarkodlari okutulanMamul = db.GetGenericWithSQLQuery<vSevkiyatBarkodlari>("exec spSevkiyataHazirBarkodGetir '" + barkod + "'", new string[0]).FirstOrDefault();

            if (okutulanMamul == null)
            {
                tblMamulKumaslar mamul = db.GetGeneric<tblMamulKumaslar>(c => c.Barkod == barkod).FirstOrDefault();
                if (mamul == null) throw new Exception("Barkod bulunamadı..!");

                if (mamul.SevkEdilebilir == false) throw new Exception("Mamul onayı gerekli.\n\nOkutulamaz..!");

                if (mamul.SevkId != null && mamul.SevkId != 0) throw new Exception("Daha önce sevk edilmiş..!");

                throw new Exception("Barkod bulunamadı..!");
            }

            if (okutulanMamul.SevkSiparisActId.HasValue == false) throw new Exception("Barkod sevk emrinde bulunamadı..!");

            if (okutulanMamul.RezerveSiparisActId != null && okutulanMamul.RezerveSiparisActId != 0) throw new Exception("Rezerve edilmiş..!");

            vSiparisAct siparisSatiri = Siparisleri.Find(f => f.Id == okutulanMamul.SevkSiparisActId);
            if (siparisSatiri == null) throw new Exception("Barkod sevk emrinde bulunamadı..!");
            if (siparisSatiri != null && siparisSatiri.Durum == "Kapalı") throw new Exception("Sipariş kapalı.\n\nEklenemez..!");

            bool snc = false;

            if (okutulanMamul.Ayirac == "Mamul")
            {
                tblMamulKumaslar tblMamul = db.GetGeneric<tblMamulKumaslar>(c => c.Id == okutulanMamul.Id).FirstOrDefault();
                if (this.SevkBelge.SozlesmeNo.StartsWith("FS")) tblMamul.Durum = "Fason";
                tblMamul.KutuId = Okutulanlar.Count + 1;
                okutulanMamul.KutuId = tblMamul.KutuId;
                tblMamul.SevkId = _SevkBelge.Id;
                snc = db.UpdateGeneric<tblMamulKumaslar>(tblMamul);
            }
            else if (okutulanMamul.Ayirac == "Iade")
            {
                tblIadeler iadeMamul = db.GetGeneric<tblIadeler>(c => c.Id == okutulanMamul.Id).FirstOrDefault();
                iadeMamul.KutuId = Okutulanlar.Count + 1;
                okutulanMamul.KutuId = iadeMamul.KutuId;
                iadeMamul.SevkId = _SevkBelge.Id;
                snc = db.UpdateGeneric<tblIadeler>(iadeMamul);
            }

            if (snc)
            {
                Okutulanlar.Add(okutulanMamul);
                Okutulanlar = Okutulanlar.OrderByDescending(o => o.KutuId).ToList();
            }
            else throw new Exception("Hata oluştu.\n\nOkutulamadı..!");
        }

        public bool BarkodSil(vSevkiyatBarkodlari silinecek)
        {
            if (silinecek.Ayirac == "Mamul")
            {
                tblMamulKumaslar tbl = db.GetGeneric<tblMamulKumaslar>(c => c.Id == silinecek.Id).FirstOrDefault();
                tbl.SevkId = 0;
                if (db.UpdateGeneric<tblMamulKumaslar>(tbl))
                {
                    Okutulanlar.Remove(silinecek);
                    return true;
                }
            }

            else if (silinecek.Ayirac == "Iade")
            {
                tblIadeler tbl = db.GetGeneric<tblIadeler>(c => c.Id == silinecek.Id).FirstOrDefault();
                tbl.SevkId = 0;
                if (db.UpdateGeneric<tblIadeler>(tbl))
                {
                    Okutulanlar.Remove(silinecek);
                    return true;
                }
            }

            return false;
        }

        public bool SevkiyatKaydet()
        {
            tblSevk tblSevk = SevkBelge.ViewToTbl();

            if (SevkBelge.Id == 0)
            {
                if (db.SaveGeneric<tblSevk>(ref tblSevk) == false) return false;
                SevkBelge.Id = tblSevk.Id;
            }
            else if (db.UpdateGeneric<tblSevk>(tblSevk) == false) return false;

            return true;
        }

        public bool TekEtiketMi = false;

        public bool SevkiyatSil()
        {
            //List<tblMamulKumaslar> tblOkutulanlar = vMamulKumaslar.ViewToTable(Okutulanlar);
            //tblOkutulanlar.ForEach(c => c.SevkId = null);

            string iptalMi = db.GetGenericWithSQLQuery<string>("execute spMamulSevkiyatlariIptalEt " + this.SevkBelge.Id.ToString(), new string[0]).FirstOrDefault();
            
            if (iptalMi == "1")
            {
                bool snc = db.DeleteGeneric<tblSevk>(this.SevkBelge.ViewToTbl());
                if (snc) SevkBelge = new vSevk();
                return snc;
            }

            return false;
        }

        private List<vPaketListesi> SevkiyatBrutleriHesapla_PaleteGore(List<vPaketListesi> paketListesi, SevkiyatPaketTip paketTipi)
        {
            double kutuAgirlik = 0, paletAgirlik = 0;

            //kutu ağırlığı getiriliyor.
            if (paketTipi == SevkiyatPaketTip.Palet12Kucuk || paketTipi == SevkiyatPaketTip.Palet9Kucuk)
            {
                tblAyarlar kucukKutu = db.GetGeneric<tblAyarlar>(c => c.Adi == "KucukKutu").FirstOrDefault();
                kutuAgirlik = (kucukKutu == null || kucukKutu.Deger == null || kucukKutu.Deger.StringSayisalMi() == false) ? 0 : Convert.ToDouble(kucukKutu.Deger);
            }
            else if (paketTipi == SevkiyatPaketTip.Palet12Buyuk || paketTipi == SevkiyatPaketTip.Palet9Buyuk)
            {
                tblAyarlar buyukKutu = db.GetGeneric<tblAyarlar>(c => c.Adi == "BuyukKutu").FirstOrDefault();
                kutuAgirlik = (buyukKutu == null || buyukKutu.Deger == null || buyukKutu.Deger.StringSayisalMi() == false) ? 0 : Convert.ToDouble(buyukKutu.Deger);
            }

            //palet ağırlığı getiriliyor.
            string tip = "";
            if (paketTipi == SevkiyatPaketTip.Palet9Buyuk || paketTipi == SevkiyatPaketTip.Palet9Kucuk) tip = "Palet9";
            else if (paketTipi == SevkiyatPaketTip.Palet12Buyuk || paketTipi == SevkiyatPaketTip.Palet12Kucuk) tip = "Palet12";
            tblAyarlar palet = db.GetGeneric<tblAyarlar>(c => c.Adi == tip).FirstOrDefault();
            paletAgirlik = (palet == null || palet.Deger == null || palet.Deger.StringSayisalMi() == false) ? 0 : Convert.ToDouble(palet.Deger);

            int ind = 0;
            List<vPaketListesi> donusListesi = new List<vPaketListesi>();
            while (ind < paketListesi.Count)
            {
                List<vPaketListesi> tmpList= new List<vPaketListesi>();
                if (paketTipi == SevkiyatPaketTip.Palet9Buyuk || paketTipi == SevkiyatPaketTip.Palet9Kucuk)
                {
                    tmpList = paketListesi.Skip(ind).Take(9).ToList();
                    double brutKutuAgirlik = Math.Round((double)((paletAgirlik + tmpList.Count * kutuAgirlik) / tmpList.Count), 2);
                    tmpList.ForEach(c => c.BrutAgirlik = Math.Round((c.BrutAgirlik.Value + brutKutuAgirlik), 2));
                    donusListesi.AddRange(tmpList);
                    ind += 9;
                }

                if (paketTipi == SevkiyatPaketTip.Palet12Buyuk || paketTipi == SevkiyatPaketTip.Palet12Kucuk)
                {
                    tmpList = paketListesi.Skip(ind).Take(12).ToList();
                    double brutKutuAgirlik = Math.Round((double)((paletAgirlik + tmpList.Count * kutuAgirlik) / tmpList.Count),2);
                    tmpList.ForEach(c => c.BrutAgirlik = Math.Round(c.BrutAgirlik.Value + brutKutuAgirlik));
                    donusListesi.AddRange(tmpList);
                    ind += 12;
                }
            }           

            return donusListesi;
        }

        public enum SevkiyatPaketTip { Sandik160, Sandik180, Palet9Kucuk, Palet9Buyuk, Palet12Kucuk, Palet12Buyuk, KucukKutu, BuyukKutu };
        public List<vPaketListesi> SevkiyatListesiGetir(SevkiyatPaketTip secilenTip)
        {
            List<vPaketListesi> list = db.GetGeneric<vPaketListesi>(c => c.SevkId == this._SevkBelge.Id);
            if (secilenTip == SevkiyatPaketTip.Sandik160 || secilenTip == SevkiyatPaketTip.Sandik180 || secilenTip == SevkiyatPaketTip.KucukKutu || secilenTip == SevkiyatPaketTip.BuyukKutu)
            {
                tblAyarlar sandik = db.GetGeneric<tblAyarlar>(c => c.Adi == secilenTip.ToString()).FirstOrDefault();
                double sandikAgirlik = (sandik == null || sandik.Deger == null || sandik.Deger.StringSayisalMi() == false) ? 0 : Convert.ToDouble(sandik.Deger);
                list.ForEach(c => c.BrutAgirlik = Math.Round((c.BrutAgirlik.Value + sandikAgirlik), 2));
            }
            else return SevkiyatBrutleriHesapla_PaleteGore(list, secilenTip);
            
            return list;
        }

        public vPaketListesi KutuGetir(int mamulID)
        {
            return db.GetGeneric<vPaketListesi>(c => c.Id == mamulID).FirstOrDefault();
        }

        public bool MamulBarkodGuncelle(vMamulKumaslar mamul)
        {
            return db.UpdateGeneric<tblMamulKumaslar>(mamul.ViewToTable());
        }

        public List<vMamulKumaslar> SevkEmriMamulleriGetir()
        {
            string idStr = "";
            foreach (var item in this.Siparisleri) idStr += item.Id.ToString() + ",";
            idStr = idStr.TrimEnd(',');

            return db.GetGenericWithSQLQuery<vMamulKumaslar>("select * from vMamulKumaslar where SevkId = 0 and SevkSiparisActId in (" + idStr + ")", new object[0]);
        }     

        #region statics

        public static List<vSevk> SevkiyatlariGetir(DateTime ilkTarih, DateTime sonTarih)
        {
            return new DBEvents().GetGeneric<vSevk>(c => ilkTarih <= c.Tarih && c.Tarih <= sonTarih);
        }

        public static List<vSiparisler> MusterileriGetir()
        {
            List<vSiparisler> list = new DBEvents().GetGeneric<vSiparisler>(c => c.Durum == "Açık");
            if (list == null) return null;
            return list.GroupBy(c => c.FirmaId).Select(s => s.First()).OrderBy(o => o.FirmaAdi).ToList();
        }

        public static List<tblPersoneller> SevkPersoneliGetir()
        {
            return new DBEvents().GetGeneric<tblPersoneller>(c => c.BolumId == 18 && c.AktifMi == true);
        }

        public static List<vSiparisler> MusteriSiparisleriGetir(int musteriId)
        {
            List<vSiparisler> list = new DBEvents().GetGeneric<vSiparisler>(c => c.FirmaId == musteriId && c.Durum == "Açık");
            //return list.GroupBy(c => c.SozlesmeNo).Select(s => s.First()).ToList();
            return list.OrderBy(o => o.SozlesmeNo).ToList();
        }

        #endregion
    }
}

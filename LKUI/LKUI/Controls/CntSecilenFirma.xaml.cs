using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LKLibrary.DbClasses;
using LKLibrary.Classes;

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntSecilenFirma.xaml
    /// </summary>
    public partial class CntSecilenFirma : UserControl
    {
        public CntSecilenFirma(vTalepKarsilama talepKarsilamaFormu, List<vTalepKarsilamaAct> listTalepKarsilama = null)
        {
            InitializeComponent();

            this.TalepKarsilamaFormu = talepKarsilamaFormu;

            _TblKarsilamaFormu = new tblTalepKarsilama()
            {
                DurumId = talepKarsilamaFormu.DurumId,
                FirmaId = talepKarsilamaFormu.TedarikciId,
                Id = talepKarsilamaFormu.Id,
                No = talepKarsilamaFormu.No,
                PersonelId = talepKarsilamaFormu.PersonelId,
                Tarih = talepKarsilamaFormu.Tarih,
                OdemeSekli = talepKarsilamaFormu.OdemeSekli,
                TerminTarihi = talepKarsilamaFormu.TerminTarihi

            };

            if (listTalepKarsilama != null) Add(listTalepKarsilama);
            else this.ListTalepKarsilananlar = listTalepKarsilama;

            this.DataContext = talepKarsilamaFormu;
        }

        public vTalepKarsilama TalepKarsilamaFormu;
        public List<vTalepKarsilamaAct> ListTalepKarsilananlar = new List<vTalepKarsilamaAct>();

        private tblTalepKarsilama _TblKarsilamaFormu;
        private List<tblTalepKarsilamaAct> _ListTblTalepKarsilananlar = new List<tblTalepKarsilamaAct>();

        private MalzemeTalep talep = new MalzemeTalep();

        public bool SaveKarsilama()
        {
            _TblKarsilamaFormu.OdemeSekli = TalepKarsilamaFormu.OdemeSekli;
            _TblKarsilamaFormu.TerminTarihi = TalepKarsilamaFormu.TerminTarihi;
            bool sonuc = talep.KarsilamaKaydet(ustForm: _TblKarsilamaFormu, listKarsilananlar: _ListTblTalepKarsilananlar);
            this.TalepKarsilamaFormu = talep.KarsilamaFormlariGetir(_TblKarsilamaFormu.Id).FirstOrDefault();
            return sonuc;
        }

        public bool Add(List<vTalepKarsilamaAct> newKarsilamaList)
        {
            try
            {
                foreach (vTalepKarsilamaAct item in newKarsilamaList)
                {
                    tblTalepKarsilamaAct karsila = new tblTalepKarsilamaAct() { 
                       BirimId = item.BirimId,
                       Fiyat = item.Fiyat,
                       Id = item.Id,
                       MalzemeId = item.MalzemeId,
                       Miktar = item.Miktar,
                       TalepId = item.TalepId,
                       TalepKarsilamaId = this.TalepKarsilamaFormu.Id,
                       Tarih = item.Tarih,
                       TedarikciId = item.TedarikciId,
                       DovizId = item.DovizId,
                       Kur = item.Kur                       
                    };

                    _ListTblTalepKarsilananlar.Add(karsila);
                }

                this.ListTalepKarsilananlar.AddRange(newKarsilamaList);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.CornflowerBlue;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = null;
        }
    }
}

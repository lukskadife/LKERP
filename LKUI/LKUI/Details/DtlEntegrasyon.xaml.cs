using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using LKLibrary.Classes;
using LKLibrary.DbClasses;

namespace LKUI.Details
{
    /// <summary>
    /// Interaction logic for DtlEntegrasyon.xaml
    /// </summary>
    public partial class DtlEntegrasyon : UserControl
    {
        string _EntEdilecekKayit;

        public DtlEntegrasyon()
        {
            InitializeComponent();
            _EntEdilecekKayit = new vAyarlar().AyarGetir(App.ClickedMenuItemId).Adi;
        }

        private void Baslat()
        {
            ImgEntegrasyon.Height = 0;
            Spinner.Visibility = System.Windows.Visibility.Visible;
            //TxtDurum.Text = "Entegrasyon devam ediyor...";
            switch (_EntEdilecekKayit)
            {
                case "MalzemeEnt":
                    TxtDurum.Text = "Malzemeler entegre ediliyor...";
                    break;
                case "FirmaEnt":
                    TxtDurum.Text = "Müşteriler ve tedarikçiler entegre ediliyor...";
                    break;
                case "PersonelEnt":
                    TxtDurum.Text = "Personeller entegre ediliyor...";
                    break;
                case "BolumEnt":
                    TxtDurum.Text = "Personel bölümleri entegre ediliyor...";
                    break;
                case "MalzemeBirimEnt":
                    TxtDurum.Text = "Malzeme birimleri entegre ediliyor...";
                    break;
                default:
                    break;
            }
            BackgroundWorker bgWork = new BackgroundWorker();
            bgWork.DoWork += new DoWorkEventHandler(bgWork_DoWork);
            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWork_RunWorkerCompleted);

            bgWork.RunWorkerAsync();
        }

        void bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bitir();
        }

        void bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //EntegrasyonServisi.EntegrasyonClient client = new EntegrasyonServisi.EntegrasyonClient();
                
                switch (_EntEdilecekKayit)
                {
                    case "MalzemeEnt":
                        kayitSayisi =  new LogoEntegrasyon().MalzemeleriEntegreEt();
                        break;
                    case "FirmaEnt":
                        kayitSayisi = new LogoEntegrasyon().FirmalariEntegreEt();
                        break;
                    case "PersonelEnt":
                        kayitSayisi = new LogoEntegrasyon().PersonelleriEntegreEt();
                        break;
                    case "BolumEnt":
                        kayitSayisi = new LogoEntegrasyon().PersonelBolumleriEntegreEt();
                        break;
                    case "MalzemeBirimEnt":
                        kayitSayisi = new LogoEntegrasyon().MalzemeBirimleriEntegreEt();
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception exc)
            {
                kayitSayisi = 0;
                string str = exc.Message;
            }
        }

        int kayitSayisi;

        private void Bitir()
        {
            if (kayitSayisi > 0)
            {
                ImgOk.Height = 40;
                Spinner.Visibility = System.Windows.Visibility.Hidden;
                TxtDurum.Text = kayitSayisi.ToString() + " kayıt başarıyla alındı...";
            }
            else
            {
                ImgError.Height = 40;
                Spinner.Visibility = System.Windows.Visibility.Hidden;
                TxtDurum.Text = "Kayıtlar alınamadı..!";
            }
        }

        private void ImgEntegrasyon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Baslat();
        }
    }
}

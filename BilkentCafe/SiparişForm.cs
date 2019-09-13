using BilkentKafe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilkentCafe
{
    public partial class SiparişForm : Form
    {
        KafeVeri db;
        Siparis siparis;
        BindingList<SiparisDetay> blSiparisDetaylar;

        public SiparişForm(KafeVeri kafeveri, Siparis siparis)
        {
            db = kafeveri;
            this.siparis = siparis; //Aynı isime sahip olduğu için this kullanıldı.This'in anlamı bu class.
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);

            blSiparisDetaylar.ListChanged += BlSiparisDetaylar_ListChanged;

            InitializeComponent();

            Text = "Masa " + siparis.MasaNo;
            lblMasaNo.Text = string.Format("{0:00}", siparis.MasaNo);

            cboUrunler.DataSource = db.Urunler;
            dgvSiparisDetaylar.DataSource = blSiparisDetaylar;
            lblOdemeTutari.Text = siparis.ToplamTutarTL;
        }

        private void BlSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            lblOdemeTutari.Text = siparis.ToplamTutarTL;
        }

        private void btnSiparisDetayEkle_Click(object sender, EventArgs e)
        {

            Urun secili = (Urun)cboUrunler.SelectedItem;

            SiparisDetay sd = new SiparisDetay
            {
                UrunAd = secili.UrunAd,
                BirimFiyat = secili.BirimFiyat,
                Adet = (int)nudUrunAdet.Value
            };

            blSiparisDetaylar.Add(sd);



        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
             //Bu pencereyi kapat.
            Close();
        }
    }
}

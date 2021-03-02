using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace Odev1Deneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Game game = new Game();
        Button button = new Button();
        Button tas = new Button();
        Button eskiTasButonu = new Button();
        Button yeniTasButonu = new Button();
        public bool tasSecim = false;
        public bool gameover = false;
        public bool gameStart = false;
        public int tasSayac = 0;
        public int tasSayisi = 7;
        public int[,] tasKonumlari = new int[7, 2];
        public int kX;
        public int kY;
        private void Form1_Load(object sender, EventArgs e)
        {
            button.Click += new EventHandler(button_click);
            tas.Click += new EventHandler(tas_click);
            formYenile();
        }

        private void button_click(object sender, EventArgs e)
        {
            button = (Button)sender;

            //OYUN BAŞLANGICI TAŞ YERLEŞTİRME
            if (tasSecim == true && tas.ImageKey == "blueBall.png")
            {
                
                if (!(button.ImageList == imageList1))
                {
                    tasKonumlari[tasSayac, 0] = Convert.ToInt32(button.Name.Substring(7, 1));
                    tasKonumlari[tasSayac, 1] = Convert.ToInt32(button.Name.Substring(6, 1));
                    button.Text = game.uzaklikBelirleme(Convert.ToInt32(button.Name.Substring(6, 1)), Convert.ToInt32(button.Name.Substring(7, 1)), kX, kY).ToString();
                    this.button.ImageList = imageList1;
                    this.button.ImageKey = tas.ImageKey;
                    tas.Visible = false;
                    tasSayac++;
                }
                tasSecim = false;
                if(tasSayac == tasSayisi)
                {
                    btnBasla.Enabled = true;
                }
            }
            else if (tasSecim == true && tas.ImageKey == "redBall.png")
            {
                btnBlue1.Enabled = true;
                btnBlue2.Enabled = true;
                btnBlue3.Enabled = true;
                btnBlue4.Enabled = true;
                btnBlue5.Enabled = true;
                btnBlue6.Enabled = true;
                btnBlue7.Enabled = true;

                if (tasSayisi == 8)
                {
                    btnBlue8.Enabled = true;
                }
                else if (tasSayisi == 9)
                {
                    btnBlue8.Enabled = true;
                    btnBlue9.Enabled = true;
                }

                btnRed.Visible = false;
                

                this.button.ImageList = imageList1;
                this.button.ImageKey = tas.ImageKey;
                this.button.Text = "";
                kY = Convert.ToInt32(button.Name.Substring(6, 1));
                kX = Convert.ToInt32(button.Name.Substring(7, 1));
                tasSecim = false;
            }



            //OYUN İÇİ HAREKET

            else if (button.ImageKey == "blueBall.png" && tasSecim == false && gameStart == true)
            {
                eskiTasButonu = button;
            }
            else if (button.ImageKey != "blueBall.png" && eskiTasButonu.ImageKey == "blueBall.png" && tasSecim == false && gameStart == true)
            {
                if (hamleKontrol(button, eskiTasButonu))
                {
                    yeniTasButonu = button;

                    if (yeniTasButonu.ImageKey == "redBall.png" && Convert.ToInt32(eskiTasButonu.Text) - 1 == 0)
                    {
                        tasSayisi--;
                        eskiTasButonu.ImageList = null;
                        eskiTasButonu.ImageKey = null;
                        eskiTasButonu.Text = "";
                        if (tasSayisi == 0)
                        {
                            lblGame.Text = "Oyunu Kazandın!";
                            lblGame.ForeColor = System.Drawing.Color.Green;
                            btnBasla.Text = "Yeniden Oyna!";
                            gameStart = false;
                            tasSayisi = 7;
                        }


                    }
                    else if (yeniTasButonu.ImageKey != "redBall.png" && Convert.ToInt32(eskiTasButonu.Text) - 1 <= 0)
                    {
                        gameover = true;
                        yeniTasButonu.Text = (Convert.ToInt32(eskiTasButonu.Text) - 1).ToString();
                        yeniTasButonu.ImageList = imageList1;
                        yeniTasButonu.ImageKey = "blueBall.png";
                        eskiTasButonu.ImageList = null;
                        eskiTasButonu.ImageKey = null;
                        eskiTasButonu.Text = "";
                        lblGame.Text = "Oyunu Kaybettin!";
                        lblGame.ForeColor = System.Drawing.Color.Red;
                        btnBasla.Text = "Yeniden Oyna!";
                        gameStart = false;
                    }
                    else
                    {

                        yeniTasButonu.Text = (Convert.ToInt32(eskiTasButonu.Text) - 1).ToString();
                        yeniTasButonu.ImageList = imageList1;
                        yeniTasButonu.ImageKey = "blueBall.png";
                        eskiTasButonu.ImageList = null;
                        eskiTasButonu.ImageKey = null;
                        eskiTasButonu.Text = "";
                    }
                    for (int i = 0; i < tasKonumlari.Length / 2; i++)
                    {
                        if (tasKonumlari[i, 0] == Convert.ToInt32(eskiTasButonu.Name.Substring(7, 1)) &&
                            tasKonumlari[i, 1] == Convert.ToInt32(eskiTasButonu.Name.Substring(6, 1)))
                        {
                            tasKonumlari[i, 0] = Convert.ToInt32(yeniTasButonu.Name.Substring(7, 1));
                            tasKonumlari[i, 1] = Convert.ToInt32(yeniTasButonu.Name.Substring(6, 1));
                        }

                    }
                }
            }
            button = null;

        }
        private bool hamleKontrol(Button eskiKonum, Button yeniKonum)
        {
            int hamleUzakligi = game.uzaklikBelirleme(Convert.ToInt32(yeniKonum.Name.Substring(6, 1)), Convert.ToInt32(yeniKonum.Name.Substring(7, 1)),
                                                      Convert.ToInt32(eskiKonum.Name.Substring(7, 1)), Convert.ToInt32(eskiKonum.Name.Substring(6, 1)));
            if (hamleUzakligi > 1) { return false; }
            else { return true; }
        }
        private void tas_click(object sender, EventArgs e)
        {
            tas = (Button)sender;
            tasSecim = true;
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            gameStart = true;
            if (btnBasla.Text == "Yeniden Oyna!")
            {
                formYenile();
            }
            else
            {
                lblGame.Text = "Oyun Başladı!";
            }
                
            
        }


        public void formYenile()
        {
            btnBasla.Enabled = false;
            btnBlue1.Enabled = false;
            btnBlue2.Enabled = false;
            btnBlue3.Enabled = false;
            btnBlue4.Enabled = false;
            btnBlue5.Enabled = false;
            btnBlue6.Enabled = false;
            btnBlue7.Enabled = false;
            btnBlue8.Enabled = false;
            btnBlue9.Enabled = false;
            btnBlue1.Visible = true;
            btnBlue2.Visible = true;
            btnBlue3.Visible = true;
            btnBlue4.Visible = true;
            btnBlue5.Visible = true;
            btnBlue6.Visible = true;
            btnBlue7.Visible = true;
            btnBlue8.Visible = true;
            btnBlue9.Visible = true;
            btnRed.Enabled = true;
            btnRed.Visible = true;
            btnBasla.Text = "Başla";
            lblGame.Text = "";
            Control ctnRed = this.Controls["button" + kY + kX];

            for (int i = 0; i < tasKonumlari.Length / 2; i++)
            {
                Control ctnBlue = this.Controls["button" + tasKonumlari[i, 1].ToString() + tasKonumlari[i, 0].ToString()];
                game.oyunYenile(ctnBlue, ctnRed);
            }
            tasKonumlari = new int[tasSayisi, 2];
            tasSayac = 0;
            gameStart = false;
        }

        private void seviyeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tasSayisi = 7;
            formYenile();
        }

        private void seviyeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tasSayisi = 8;
            formYenile();
            
        }

        private void seviyeToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            tasSayisi = 9;
            formYenile();
        }


        private void yapanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HAZIRLAYANLAR\nAhmet ...\nSeyfeddin Emre Türkmen");
        }
    }
}

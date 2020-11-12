// 140201068 - Mustafa ALIN
// 150201167 - Selin Gizem Özkan
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;


namespace Puzzle2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static String puan1;
        int eb = 0;
        int puan = 100;
        List<Point> liste1 = new List<Point>();
        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("skor.txt", FileMode.Open, FileAccess.Read);
            StreamReader oku = new StreamReader(fs);
            string str;
            string str2;
            int enBuyuk_index = 0;
            int i = 0;
            int[] dizi = new int[100];
            while ((str = oku.ReadLine()) != null)
            {
                str2 = str.Substring(str.IndexOf("\t"));
                str2.Trim();
                dizi[i] = Convert.ToInt32(str2);
                i++;
            }
            oku.Close();

            for (int a = 0; a < dizi.Length; a++)
            {
                if (dizi[a] > eb)
                {
                    eb = dizi[a];
                    enBuyuk_index = a;
                }
            }
            oku = new StreamReader("skor.txt");
            for (i = 0; i < enBuyuk_index + 1; i++)
            {
                str = oku.ReadLine();
            }
            label2.Text = str.Substring(0, str.IndexOf("\t"));
            label3.Text = eb.ToString();
            oku.Close();
            button1.Visible = true;
            pictureBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Oyna();
        }

        private void degis(Button parca1, Button parca2)
        {
            Point konum1 = parca1.Location;
            Point konum2 = parca2.Location;
            parca1.Location = new Point(konum2.X, konum2.Y);
            parca2.Location = new Point(konum1.X, konum1.Y);
            parca1.FlatStyle = FlatStyle.Standard;
            parca2.FlatStyle = FlatStyle.Standard;
        }
        int sayac1 = 0, sayac2 = 0, sayac3 = 0;
        private void button_Click(object nesne, EventArgs e)
        {

            Button buton1 = (Button)nesne;
            buton1.FlatStyle = FlatStyle.Popup;

            var i = (from Button buton in this.pictureBox2.Controls.OfType<Button>()
                     where buton.FlatStyle == FlatStyle.Popup
                     select buton).ToList();
            if (i.Count() == 2)
            {
                degis(i[0], i[1]);
                if (i[0].Name == i[0].Location.ToString() && i[1].Name == i[1].Location.ToString())
                {
                    sayac1++;
                    i[0].Enabled = false;
                    i[1].Enabled = false;
                }
                else if (i[0].Name == i[0].Location.ToString() || i[1].Name == i[1].Location.ToString())
                {
                    sayac2++;
                    if (i[0].Name == i[0].Location.ToString())
                        i[0].Enabled = false;
                    else if (i[1].Name == i[1].Location.ToString())
                        i[1].Enabled = false;
                }
                else if (i[0].Name != i[0].Location.ToString() && i[1].Name != i[1].Location.ToString())
                    sayac3++;
            }


            liste1.Clear();
            Bitmap orj = (Bitmap)pictureBox1.Image;
            foreach (Button b in pictureBox2.Controls)
            {
                int sayac = 0;
                string str1;
                string str2;
                Bitmap ref1 = new Bitmap(b.Image);
                for (int k = 0; k < 90; k++)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        str1 = ref1.GetPixel(k, j).ToString();
                        str2 = orj.GetPixel(b.Location.X + k, b.Location.Y + j).ToString();
                        if (str1 != str2)
                            sayac++;
                    }
                }
                if (sayac == 0)
                    liste1.Add(b.Location);

            }
            if (liste1.Count == pictureBox2.Controls.Count)
            {
                foreach (Button button in pictureBox2.Controls)
                {
                    button.Enabled = true;
                }
                DialogResult secim;
                puan = 100 - (sayac1 + sayac2 * 2 + sayac3 * 3);
                if (puan < 0)
                    puan = 0;
                if (puan > eb)
                    secim = MessageBox.Show("EN YÜKSEK SKOR !!!!! TEBRİKLER !!!!!\nPuanınız: " + puan + " Kaydetmek ister misiniz?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    secim = MessageBox.Show("Tebrikler! Puanınız: " + puan + " Kaydetmek ister misiniz?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                puan1 = puan.ToString();
                if (secim == DialogResult.Yes)
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                }
            }

        }
        private void Oyna()
        {
            liste1.Clear();
            pictureBox2.Controls.Clear();
            Bitmap orj = (Bitmap)pictureBox1.Image;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Rectangle kare = new Rectangle(90 * i, 90 * j, 90, 90);
                    Bitmap parca = orj.Clone(kare, orj.PixelFormat);
                    Button buton = new Button();
                    buton.Size = new Size(90, 90);
                    buton.Image = parca;
                    buton.FlatStyle = FlatStyle.Standard;
                    buton.Location = new Point(90 * i, 90 * j);
                    buton.Name = buton.Location.ToString();
                    liste1.Add(buton.Location);
                    this.pictureBox2.Controls.Add(buton);
                    buton.Click += new System.EventHandler(this.button_Click);

                }
            }
            Random rnd = new Random();
            int a;
            int sayac4 = 0;
            foreach (Button b in pictureBox2.Controls)
            {
                int x = 0;
                a = rnd.Next(0, liste1.Count);
                b.Location = liste1[a];
                Bitmap ref1 = (Bitmap)b.Image;
                string str1;
                string str2;
                for (int i = 0; i < 90; i++)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        str1 = ref1.GetPixel(i, j).ToString();
                        str2 = orj.GetPixel(i + b.Location.X, j + b.Location.Y).ToString();
                        if (str1 != str2)
                        {
                            x++;
                        }
                    }
                }
                if (x == 0)
                {
                    sayac4++;
                    b.Enabled = false;
                }

                liste1.RemoveAt(a);
            }
            if (sayac4 > 0)
            {
                pictureBox2.Enabled = true;
                MessageBox.Show("" + sayac4 + " tane parçanın yeri doğru. Şimdi oynayabilirsiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (sayac4 == 0)
            {
                pictureBox2.Enabled = false;
                MessageBox.Show(" En az bir parçanın yeri doğru olana kadar Karıştır butonuna basmaya devam ediniz", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya_ac = new OpenFileDialog();
            dosya_ac.Filter = "Resim Dosyasi |" + "*.jpg;*.bmp;*.gif;*.png;*.wmf; *.tif";
            if (dosya_ac.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dosya_ac.FileName);
                Bitmap bitmap = new Bitmap(pictureBox1.Image, 360, 360);
                pictureBox1.Image = bitmap;
            }

        }
    }
}

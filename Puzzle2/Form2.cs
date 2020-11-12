using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Puzzle2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.puan1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dosya_yolu = @"skor.txt";
            //FileStream file = File.Create(dosya_yolu);
            StreamWriter yaz = File.AppendText(dosya_yolu);
            yaz.WriteLine(textBox1.Text + "\t\t" + label3.Text);
            yaz.Close();
            this.Close();
        }
    }
}

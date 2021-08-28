using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diploma
{
    public partial class PasInd : Form
    {
        public PasInd()
        {
            InitializeComponent();
        }
        public string foto= @"E:\Диплом\Diploma\Diploma\Diploma\bin\Debug\asddsa.jpg";
        private void PasInd_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = foto;
        }

        private string FotoSetFormat(string s)
        {
            string[] arr = s.Split('\\');
            s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                s += arr[i] + "\\\\";
            }
            s = s.Remove(s.Length - 2);
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Foto(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foto = openFileDialog1.FileName;
                pictureBox1.ImageLocation = foto;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (((Form2)this.Tag).fl == 1)
            {
                ((Form2)this.Tag).fotopas = FotoSetFormat(foto);
            }
            
            if (((Form2)this.Tag).fl == 0)
            {
                ((Form2)this.Tag).fotoind = FotoSetFormat(foto);
            }
            
            MessageBox.Show("Сохранено!");
        }
    }
}

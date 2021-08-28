using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Diploma
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        Form2 a = new Form2();

        public string constring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\EMPTY FOLDER\dipl2.mdf;
        Integrated Security=True;Connect Timeout=30";
        public string quer;
        public DataSet dbdataset;

        private void GetTable(string queryString, string nameTable)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(queryString, conDataBase);
            cmdDataBase.CommandText = queryString;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmdDataBase;
            dbdataset = new DataSet();
            conDataBase.Open();
            sda.Fill(dbdataset, nameTable);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                quer = @"select id_пользователя from usr where Пароль = '" + maskedTextBox1.Text + "' and Логин = '" + textBox1.Text + "'";
                GetTable(quer, "usr");
                listBox1.DataSource = dbdataset.Tables[0].DefaultView;
                listBox1.DisplayMember = "usr";
                listBox1.ValueMember = "id_пользователя";
                quer = @"Insert Into journal (id_пользователя, Дата) Values (" + int.Parse(listBox1.Text) + ", { fn NOW() })";
                GetTable(quer, "journal");
                this.Close();
            }
            catch { MessageBox.Show("Неверный пароль или логин!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

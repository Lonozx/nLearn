using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Diploma
{
    public partial class Kid3 : Form
    {
        public Kid3()
        {
            InitializeComponent();
        }
        public int n;
        Numarray[] num;
        TextBoxs[] txb;

        public string constring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Диплом\Diploma\Diploma\Diploma\dipl2.mdf;
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
        public DataTable SetTable(string queryString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand com = new SqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows) dt.Load(dr);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
            return dt;
        }

        public void Show(int n)
        {
            num = new Numarray[n];
            txb = new TextBoxs[n];

            panel1.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                txb[i] = new TextBoxs();
                txb[i].Size = new Size(120, 20);
                txb[i].Location = new Point(3, 3 + i * 30);
                txb[i].Pozition = new Point(3, i);
                panel1.Controls.Add(txb[i]);

                num[i] = new Numarray();
                num[i].Size = new Size(120, 20);
                num[i].Location = new Point(200, 3 + i * 30);
                num[i].Pozition = new Point(200, i);
                panel1.Controls.Add(num[i]);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (((Form2)this.Tag).flag == 0)
                n = Convert.ToInt32(numericUpDown1.Value);
            Show(n);
            button2.Visible = true;

        }

        private void txbPress(Object sender, KeyPressEventArgs e)
        {
 
        }
        private void numPress(Object sender, KeyPressEventArgs e)
        {

        }

        private void Kid3_Load(object sender, EventArgs e)
        {
            if (((Form2)this.Tag).flag != 0)
            {
                n = ((Form2)this.Tag).flag;
                numericUpDown1.Value = n;
                Show(n);
                quer = @"select * from Deti where id_пользователя=" + ((Form2)this.Tag).textBox1.Text;
                dataGridView1.DataSource = SetTable(quer);
                for (int i = 0; i < n; i++)
                {
                    txb[i].Text = dataGridView1[2, i].Value.ToString();
                    num[i].Text = dataGridView1[3, i].Value.ToString();
                }
                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                quer = @"select * from Deti where id_пользователя=" + ((Form2)this.Tag).textBox1.Text;
                GetTable(quer, "ob");
                listBox1.DataSource = dbdataset.Tables[0].DefaultView;
                listBox1.DisplayMember = "ob";
                listBox1.ValueMember = "id_пользователя";
                ((Form2)this.Tag).flag = listBox1.Items.Count;

                if (((Form2)this.Tag).flag == 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        quer = @"insert into Deti (id_deti,id_пользователя, ФИО, Возраст) Values (" + i + ", " + ((Form2)this.Tag).textBox1.Text + ",'" + txb[i].Text + "'," + num[i].Text + ")";
                        dataGridView1.DataSource = SetTable(quer);
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        quer = @"UPDATE Deti SET ФИО='" + txb[i].Text + "', Возраст=" + num[i].Text +
                                                            " where id_пользователя=" + ((Form2)this.Tag).textBox1.Text + " and id_deti=" + i + ";";
                        dataGridView1.DataSource = SetTable(quer);
                    }
                }
                MessageBox.Show("Сохранено!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                quer = @"select Фамилия, id_пользователя from ob";
                GetTable(quer, "ob");
                ((Form2)this.Tag).listBox2.DataSource = dbdataset.Tables[0].DefaultView;
                ((Form2)this.Tag).listBox2.DisplayMember = "ob";
                ((Form2)this.Tag).listBox2.ValueMember = "Фамилия";

                ((Form2)this.Tag).listBox3.DataSource = dbdataset.Tables[0].DefaultView;
                ((Form2)this.Tag).listBox3.DisplayMember = "ob";
                ((Form2)this.Tag).listBox3.ValueMember = "id_пользователя";
                this.Close();
            }
            catch
            { 
                 MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

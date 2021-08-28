
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace Diploma
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string constring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\EMPTY FOLDER\dipl2.mdf;
        Integrated Security=True;Connect Timeout=30";
        public string quer;
        public DataSet dbdataset;
        public string fotopas = "1.jpg", fotoind = "1.jpg";
        public int fl;
        public int flag = 0;
        private void GetTable(string queryString, string nameTable)
        {
            try
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
            catch (Exception e) { MessageBox.Show(e.Message); }
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
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                con.Close();
            }
            return dt;
        }

        //***************************************************************
        public void Form2_Load(object sender, EventArgs e)
        {
            //---------ВХОД--------------------------------------------------
            quer = @"select id_пользователя from journal order by Дата desc";
            GetTable(quer, "journal");
            listBox1.DataSource = dbdataset.Tables[0].DefaultView;
            listBox1.DisplayMember = "journal";
            listBox1.ValueMember = "id_пользователя";

            //----------СОТРУДНИКИ--------------------------------------------
            quer = @"select * from ob where id_пользователя = " + int.Parse(listBox1.Text);
            GetTable(quer, "ob");
            listBox4.DataSource = dbdataset.Tables[0].DefaultView;
            listBox4.DisplayMember = "ob";
            listBox4.ValueMember = "Фамилия";
            toolStripStatusLabel1.Text = listBox4.Text;

            quer = @"select Фамилия, id_пользователя from ob";
            GetTable(quer, "ob");
            listBox2.DataSource = dbdataset.Tables[0].DefaultView;
            listBox2.DisplayMember = "ob";
            listBox2.ValueMember = "Фамилия";

            listBox3.DataSource = dbdataset.Tables[0].DefaultView;
            listBox3.DisplayMember = "ob";
            listBox3.ValueMember = "id_пользователя";

            Start_Form();
            //---------НАЧИСЛЕНИЯ---------------------------------------------
            quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество,Дата, Вид_начисления, Начисления from ob inner join Nachisl on ob.id_пользователя=Nachisl.id_пользователя";
            dataGridView2.DataSource = SetTable(quer);
            //---------УДЕРЖАНИЯ---------------------------------------------
            quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество,Дата, Вид_удержания, Удержание from ob inner join Uderzan on ob.id_пользователя=Uderzan.id_пользователя";
            dataGridView3.DataSource = SetTable(quer);
            //---------ВЫЧИСЛЕНИЯ---------------------------------------------
            //quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество,Дата, К_выплате, Выплаты from ob inner join viplati on ob.id_пользователя=viplati.id_пользователя";
            //dataGridView3.DataSource = SetTable(quer);
            quer = @"select * from ob";
            GetTable(quer, "ob");
            listBox1.DataSource = dbdataset.Tables[0].DefaultView;
            listBox1.DisplayMember = "ob";
            listBox1.ValueMember = "Имя";
        }

        //**********************СОТРУДНИКИ**********************************************
        private void Start_Form()
        {
            textBox1.Enabled = false;           textBox1.Text = "";
            textBox2.Enabled = false;           textBox2.Text = "";
            textBox3.Enabled = false;           textBox3.Text = "";
            textBox4.Enabled = false;           textBox4.Text = "";
            radioButton1.Enabled = false;       radioButton1.Checked = false;
            radioButton2.Enabled = false;       radioButton2.Checked = false;
            dateTimePicker1.Enabled = false;    dateTimePicker1.Value = dateTimePicker1.MinDate;
            
            textBox5.Enabled = false;           textBox5.Text = "";     
            textBox13.Enabled = false;          textBox13.Text = "";
            button27.Enabled = false;
            textBox14.Enabled = false;          textBox14.Text = "";
            button28.Enabled = false;
            textBox6.Enabled = false;           textBox6.Text = "";
            comboBox1.Enabled = false;          comboBox1.Text = "";
            button29.Enabled = false;
            
            comboBox2.Enabled = false;          comboBox2.Text = "";
            dateTimePicker2.Enabled = false;    dateTimePicker2.Value = DateTime.Now;
            textBox7.Enabled = false;           textBox7.Text = "";
            comboBox3.Enabled = false;          comboBox3.Text = "";
            comboBox4.Enabled = false;          comboBox4.Text = "";

            button4.Visible = true;             button4.Enabled = false;
            button6.Visible = true;             button6.Enabled = false;
            button7.Visible = true;             button7.Enabled = true;
            button1.Visible = true;
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            string[] s = { "Неполное среднее", "Среднее", "Среднее специальное", "Неполное высшее", "Высшее" };
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(s);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Женат");
            comboBox1.Items.Add("Не женат");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Замужем");
            comboBox1.Items.Add("Не замужем");
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            quer = @"select Название from Doljnost";
            GetTable(quer, "Doljnost");
            comboBox3.DataSource = dbdataset.Tables[0].DefaultView;
            comboBox3.DisplayMember = "Doljnost";
            comboBox3.ValueMember = "Название";
        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
            string[] s = { "Ставка", "Почасовая", "Совместитель" };
            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(s);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox7.Text = (DateTime.Now.Year - dateTimePicker2.Value.Year).ToString();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8) e.Handled = true;

        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)                       //LIST
        {
            quer = @"select id_пользователя from ob where Фамилия = '" + listBox2.Text + "'";
            GetTable(quer, "ob");
            listBox3.DataSource = dbdataset.Tables[0].DefaultView;
            listBox3.DisplayMember = "ob";
            listBox3.ValueMember = "id_пользователя";

            quer = @"select * from ob where id_пользователя = " + Convert.ToInt32(listBox3.Text);
            dataGridView1.DataSource = SetTable(quer);

            quer = @"select * from Doljnost where id_dol = " + Convert.ToInt32(dataGridView1[13, 0].Value);
            GetTable(quer, "Doljnost");
            listBox1.DataSource = dbdataset.Tables[0].DefaultView;
            listBox1.DisplayMember = "Doljnost";
            listBox1.ValueMember = "Название";

            string passport = dataGridView1[6, 0].Value.ToString();

            textBox1.Text = dataGridView1[0, 0].Value.ToString();
            textBox2.Text = dataGridView1[1, 0].Value.ToString();
            textBox3.Text = dataGridView1[2, 0].Value.ToString();
            textBox4.Text = dataGridView1[3, 0].Value.ToString();
            if (dataGridView1[4, 0].Value.ToString() == "Мужчина") radioButton1.Checked = true;
            if (dataGridView1[4, 0].Value.ToString() == "Женщина") radioButton2.Checked = true;
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1[5, 0].Value);

            textBox5.Text = passport.Remove(2);
            textBox13.Text = passport.Remove(0, 2);
            textBox14.Text = dataGridView1[8, 0].Value.ToString();
            textBox6.Text = dataGridView1[10, 0].Value.ToString();
            comboBox1.Text = dataGridView1[11, 0].Value.ToString();

            comboBox2.Text = dataGridView1[12, 0].Value.ToString();
            comboBox3.Text = listBox1.Text;
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1[14, 0].Value);
            textBox7.Text = (DateTime.Now.Year - dateTimePicker2.Value.Year).ToString();
            comboBox4.Text = dataGridView1[15, 0].Value.ToString();

            DataGridView dGV = new DataGridView();
            quer = @"select * from usr where id_пользователя = " + Convert.ToInt32(listBox3.Text);
            dGV.DataSource = SetTable(quer);

            button3.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = true;
        }
        
        private void button27_Click(object sender, EventArgs e)  //pasport
        {
            fl = 1;
            PasInd p = new PasInd();
            p.Tag = this;
            p.ShowDialog();
        }

        private void button28_Click(object sender, EventArgs e)  //indecs
        {
            fl = 2;
            PasInd p = new PasInd();
            p.Tag = this;
            p.ShowDialog();
        }
        
        private void button29_Click(object sender, EventArgs e)             //deti
        {
            flag = 0;
            quer = @"select id_пользователя from Deti where id_пользователя=" + textBox1.Text;
            GetTable(quer, "ob");
            listBox8.DataSource = dbdataset.Tables[0].DefaultView;
            listBox8.DisplayMember = "ob";
            listBox8.ValueMember = "id_пользователя";
            flag = listBox8.Items.Count;

            Kid3 f2 = new Kid3();
            f2.Tag = this;
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)              //DELETE
        {
            DialogResult result = MessageBox.Show(" Удалить данные? ", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        quer = @"delete from ob where id_пользователя=" + textBox1.Text;
                        dataGridView1.DataSource = SetTable(quer);
                        MessageBox.Show(" Данные удалены!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case DialogResult.No:
                    {
                        MessageBox.Show("Удаление отменено!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
            quer = @"select Фамилия, id_пользователя from ob";
            GetTable(quer, "ob");
            listBox2.DataSource = dbdataset.Tables[0].DefaultView;
            listBox2.DisplayMember = "ob";
            listBox2.ValueMember = "Фамилия";
        }

        private void button2_Click(object sender, EventArgs e)              //ADD
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox13.Text != "" &&
               textBox14.Text != "" && textBox6.Text != "" && textBox7.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" &&
               comboBox3.Text != "" && comboBox4.Text != "" && (radioButton1.Checked == true || radioButton2.Checked == true) &&
               fotopas != "" && fotoind != "")
            {
                quer = @"select * from Doljnost where Название = '" + comboBox3.Text + "'";
                GetTable(quer, "Doljnost");
                listBox4.DataSource = dbdataset.Tables[0].DefaultView;
                listBox4.DisplayMember = "Doljnost";
                listBox4.ValueMember = "id_dol";

                DateTime d1 = Convert.ToDateTime(dateTimePicker1.Text), d2 = Convert.ToDateTime(dateTimePicker2.Text);

                quer = @"insert into ob (id_пользователя, Фамилия, Имя, Отчество, Дата_рождения, Паспорт,ind_kod, Адрес, 
                                        Семейное_положение, Образование, Стаж, id_dol, Занятость, Пол, Фото_паспорта, Фото_indkod) Values ("
                    +  textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','"
                    + d1.Date.Year.ToString() + "-" + d1.Date.Month.ToString() + "-" + d1.Date.Day.ToString() + "','"
                    + textBox5.Text + textBox13.Text + "','"+textBox14.Text+"','" + textBox6.Text + "','"
                    + comboBox1.Text + "','" + comboBox2.Text + "','"
                    + d2.Date.Year.ToString() + "-" + d2.Date.Month.ToString() + "-" + d2.Date.Day.ToString() + "',"
                    + Convert.ToInt32(listBox4.Text) + ",'" + comboBox3.Text+ "','" + comboBox4.Text+ "','";
                if (radioButton1.Checked == true)  quer += "Мужчина";
                if (radioButton2.Checked == true)  quer += "Женщина";
                quer += "','" +fotopas + "','" + fotoind + "')";

                dataGridView1.DataSource = SetTable(quer);

                MessageBox.Show("Данные добавлены!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                quer = @"select Фамилия, id_пользователя from ob";
                GetTable(quer, "ob");
                listBox2.DataSource = dbdataset.Tables[0].DefaultView;
                listBox2.DisplayMember = "ob";
                listBox2.ValueMember = "Фамилия";

                button7.Visible = true;
                button8.Visible = false;

                Start_Form();
            }
            else
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button7_Click(object sender, EventArgs e)              //ADD
        {
            quer = @"select * from ob order by id_пользователя desc";
            dataGridView1.DataSource = SetTable(quer);

            textBox1.Enabled = false;           textBox1.Text = (Convert.ToInt32(dataGridView1[0, 0].Value) + 1).ToString(); ;
            textBox2.Enabled = true;            textBox2.Text = "";
            textBox3.Enabled = true;            textBox3.Text = "";
            textBox4.Enabled = true;            textBox4.Text = "";
            radioButton1.Enabled = true;        radioButton1.Checked = false;
            radioButton2.Enabled = true;        radioButton2.Checked = false;
            dateTimePicker1.Enabled = true;     dateTimePicker1.Value = dateTimePicker1.MinDate;

            textBox5.Enabled = true;            textBox5.Text = "";
            textBox13.Enabled = true;           textBox13.Text = "";
            button27.Enabled = true;
            textBox14.Enabled = true;           textBox14.Text = "";
            button28.Enabled = true;
            textBox6.Enabled = true;            textBox6.Text = "";
            comboBox1.Enabled = true;           comboBox1.Text = "";
            button29.Enabled = true;

            comboBox2.Enabled = true;           comboBox2.Text = "";
            dateTimePicker2.Enabled = true;     dateTimePicker2.Value = DateTime.Now;
            textBox7.Enabled = false;           textBox7.Text = "";
            comboBox3.Enabled = true;           comboBox3.Text = "";
            comboBox4.Enabled = true;           comboBox4.Text = "";
          
            button7.Visible = false;
            button8.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)              //EDIT
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox13.Text != "" &&
               textBox14.Text != "" && textBox6.Text != "" && textBox7.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" &&
               comboBox3.Text != "" && comboBox4.Text != "" && (radioButton1.Checked == true || radioButton2.Checked == true)&&
               fotopas!="" && fotoind!="")
            {
                DateTime d = Convert.ToDateTime(dateTimePicker1.Text), d2 = Convert.ToDateTime(dateTimePicker2.Text);

                quer = @"select * from Doljnost where Название = '" + comboBox3.Text + "'";
                GetTable(quer, "Doljnost");
                listBox4.DataSource = dbdataset.Tables[0].DefaultView;
                listBox4.DisplayMember = "Doljnost";
                listBox4.ValueMember = "id_dol";

                quer = @"UPDATE ob SET Фамилия='" + textBox2.Text + "' , Имя='" + textBox3.Text + "' , Отчество='" + textBox4.Text
                         + "', Дата_рождения= '" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString()
                         + "', Паспорт= '" + textBox5.Text + textBox13.Text + "', ind_kod = '"+ textBox14.Text +"', Адрес= '" + textBox6.Text
                         + "', Семейное_положение = '" + comboBox1.Text +"', Образование = '" + comboBox2.Text
                         + "', Стаж= '" + d2.Date.Year.ToString() + "-" + d2.Date.Month.ToString() + "-" + d2.Date.Day.ToString()
                         + "', id_dol= " + Convert.ToInt32(listBox4.Text) + ", Занятость='" + comboBox4.Text + "', Пол = '"; 
                		if (radioButton1.Checked == true)  quer += "Мужчина";
                		if (radioButton2.Checked == true)  quer += "Женщина";
                        quer += "',Фото_паспорта='" +fotopas + "', Фото_indkod = '" + fotoind +
					  "' WHERE id_пользователя =" + Convert.ToInt32(textBox1.Text.Trim());
                dataGridView1.DataSource = SetTable(quer);
               

		    MessageBox.Show("Данные изменины!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                quer = @"select Фамилия, id_пользователя from ob";
                GetTable(quer, "ob");
                listBox2.DataSource = dbdataset.Tables[0].DefaultView;
                listBox2.DisplayMember = "ob";
                listBox2.ValueMember = "Фамилия";
                button6.Visible = true;
                button8.Visible = false;
                Start_Form();
            }
            else
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button6_Click(object sender, EventArgs e)                  //EDIT
        {
            textBox1.Enabled = false;
            textBox2.Enabled = true;	textBox3.Enabled = true;	textBox4.Enabled = true;
            radioButton1.Enabled = true;	radioButton2.Enabled = true;
            dateTimePicker1.Enabled = true;

            textBox5.Enabled = true;	textBox13.Enabled = true;	button27.Enabled = true;
            textBox14.Enabled = true;	button28.Enabled = true;
            textBox6.Enabled = true;
            comboBox1.Enabled = true;	button29.Enabled = true;

            comboBox2.Enabled = true;
            dateTimePicker2.Enabled = true;	textBox7.Enabled = false;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;

            button6.Visible = false;
            button8.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)                     //OTMEHA
        {
            Start_Form();
        }

        private void button1_Click(object sender, EventArgs e)                  //POISK
        {
            if (textBoxPoisk.Text != "")
            {
                quer = @"select * from ob where Фамилия like '%" + textBoxPoisk.Text + "%'";
                GetTable(quer, "ob");
                listBox2.DataSource = dbdataset.Tables[0].DefaultView;
                listBox2.DisplayMember = "ob";
                listBox2.ValueMember = "Фамилия";
                button1.Visible = false;
            }
            else
                MessageBox.Show("Данные не заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (listBox2.Items.Count == 0)
                MessageBox.Show("Данные не найдены!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //****************************************************************************************

        //****************НАЧИСЛЕНИЯ**************************************************************
        private void comboBox5_Click(object sender, EventArgs e)
        {
            string[] s = { "Оклад", "Аванс", "Отпускные", "Материальная помощь" };
            comboBox5.Items.Clear();
            comboBox5.Items.AddRange(s);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            DateTime d = Convert.ToDateTime(dateTimePicker3.Text);
            quer = @"select N_начисл from Nachisl where Дата = '" +
                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() + "' and Вид_начисления= '" + comboBox5.Text + "'";
            GetTable(quer, "Nachisl");
            listBox5.DataSource = dbdataset.Tables[0].DefaultView;
            listBox5.DisplayMember = "Nachisl";
            listBox5.ValueMember = "N_начисл";
            if (listBox5.Items.Count == 0)
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество from ob";
                dataGridView2.DataSource = SetTable(quer);
                 dataGridView2.Columns.Add("Начисления", "Начисления");
            }
            else
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество, Начисления from ob inner join Nachisl on
                        ob.id_пользователя=Nachisl.id_пользователя where N_начисл = " + listBox5.Text;
                dataGridView2.DataSource = SetTable(quer);

            }
            dataGridView2.Columns[0].Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DateTime d = Convert.ToDateTime(dateTimePicker3.Text);
            string N_ved;
            if (listBox5.Items.Count != 0)
            {
                N_ved = listBox5.Text;
                for (int i = 0; i < dataGridView2.RowCount-1; i++)
                {
                    if (dataGridView2[4, i].Value != null)
                        quer = @"UPDATE  Nachisl SET id_пользователя =" + dataGridView2[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' , Вид_начисления='" + comboBox5.Text + "', Начисления= " + dataGridView2[4, i].Value.ToString() +
                                " WHERE N_начисл =" + N_ved + ";";
                    else
                        quer = @"UPDATE  Nachisl SET id_пользователя =" + dataGridView2[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' , Вид_начисления='" + comboBox5.Text + "', Начисления= 0" +
                                " WHERE N_начисл =" + N_ved + ";";
                    GetTable(quer, "Nachisl");
                }
            }
            else
            {
                quer = @"select * from Nachisl order by N_начисл desc";
                dataGridView1.DataSource = SetTable(quer);
                if (dataGridView1.RowCount != 0)
                    N_ved = (Convert.ToInt32(dataGridView1[0, 0].Value) + 1).ToString();
                else
                    N_ved = "1";
                for (int i = 0; i < dataGridView2.RowCount-1; i++)
                {
                    if (dataGridView2[4, i].Value != null)
                        quer = @"insert into Nachisl(N_начисл, id_пользователя, Дата, Вид_начисления, Начисления) values (" +
                                N_ved + ", " + dataGridView2[0, i].Value.ToString() + ", '" +
                                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' ,'" + comboBox5.Text + "', " + dataGridView2[4, i].Value.ToString() + ");";
                    else
                        quer = @"insert into Nachisl(N_начисл, id_пользователя, Дата, Вид_начисления, Начисления) values (" +
                                 N_ved + "," + dataGridView2[0, i].Value.ToString() + ",'" +
                                 d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                 "' ,'" + comboBox5.Text + "', 0);";
                    GetTable(quer, "Nachisl");
                }
            }

        }

        private void comboBox6_Click(object sender, EventArgs e)
        {
            string[] s = { "Алименты", "Военный сбор", "Налоги" };
            comboBox6.Items.Clear();
            comboBox6.Items.AddRange(s);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView3.Columns.Clear();
            DateTime d = Convert.ToDateTime(dateTimePicker4.Text);
            quer = @"select N_удерж from Uderzan where Дата = '" +
                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() + "' and Вид_удержания= '" + comboBox6.Text + "'";
            GetTable(quer, "Uderzan");
            listBox6.DataSource = dbdataset.Tables[0].DefaultView;
            listBox6.DisplayMember = "Uderzan";
            listBox6.ValueMember = "N_удерж";
            if (listBox6.Items.Count == 0)
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество from ob";
                dataGridView3.DataSource = SetTable(quer);
                 dataGridView3.Columns.Add("Удержание", "Удержание");
            }
            else
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество, Удержание, Uderzan.Дата from ob inner join Uderzan on
                        ob.id_пользователя=Uderzan.id_пользователя where N_удерж = " + listBox6.Text;
                dataGridView3.DataSource = SetTable(quer);

            }
           dataGridView3.Columns[0].Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
        DateTime d = Convert.ToDateTime(dateTimePicker4.Text);
            string N_ud;
            if (listBox6.Items.Count != 0)
            {
                N_ud = listBox6.Text;
                for (int i = 0; i < dataGridView3.RowCount-1; i++)
                {
                    if (dataGridView3[4, i].Value != null)
                        quer = @"UPDATE  Uderzan SET id_пользователя =" + dataGridView3[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' , Вид_удержания='" + comboBox6.Text + "', Удержание= " + dataGridView3[4, i].Value.ToString() +
                                " WHERE N_удерж =" + N_ud + ";";
                    else
                        quer = @"UPDATE  Uderzan SET id_пользователя =" + dataGridView3[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' , Вид_удержания='" + comboBox6.Text + "', Удержание= 0" +
                                " WHERE N_удерж =" + N_ud + ";";
                    GetTable(quer, "Uderzan");
                }
            }
            else
            {
                quer = @"select * from Uderzan order by N_удерж desc";
                dataGridView1.DataSource = SetTable(quer);
                if (dataGridView1.RowCount != 0)
                    N_ud = (Convert.ToInt32(dataGridView1[0, 0].Value) + 1).ToString();
                else
                    N_ud = "1";
                for (int i = 0; i < dataGridView3.RowCount-1; i++)
                {
                    if (dataGridView3[4, i].Value != null)
                        quer = @"insert into Uderzan(N_удерж, id_пользователя, Дата, Вид_удержания, Удержание) values (" +
                                N_ud + ", " + dataGridView3[0, i].Value.ToString() + ", '" +
                                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' ,'" + comboBox6.Text + "', " + dataGridView3[4, i].Value.ToString() + ");";
                    else
                        quer = @"insert into Uderzan(N_удерж, id_пользователя, Дата, Вид_удержания, Удержание) values (" +
                                 N_ud + "," + dataGridView3[0, i].Value.ToString() + ",'" +
                                 d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                 "' ,'" + comboBox6.Text + "', 0);";
                    GetTable(quer, "Uderzan");
                }
            }

        
        }

        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView4.Columns.Clear();
            DateTime d = Convert.ToDateTime(dateTimePicker5.Text);
            quer = @"select К_выплате from viplati where Дата = '" +
                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() + "'";
            GetTable(quer, "viplati");
            listBox7.DataSource = dbdataset.Tables[0].DefaultView;
            listBox7.DisplayMember = "viplati";
            listBox7.ValueMember = "К_выплате";
            if (listBox7.Items.Count == 0)
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество from ob";
                dataGridView4.DataSource = SetTable(quer);
                dataGridView4.Columns.Add("Выплачено", "Выплачено");
            }
            else
            {
                quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество, Выплачено, viplati.Дата from ob inner join viplati on
                        ob.id_пользователя=viplati.id_пользователя where К_выплате = " + listBox7.Text;
                dataGridView4.DataSource = SetTable(quer);

            }
            dataGridView4.Columns[0].Visible = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DateTime d = Convert.ToDateTime(dateTimePicker4.Text);
            string N_ud;
            if (listBox7.Items.Count != 0)
            {
                N_ud = listBox7.Text;
                for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                {
                    if (dataGridView4[4, i].Value != null)
                        quer = @"UPDATE  viplati SET id_пользователя =" + dataGridView4[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                 "', К_выплате= " + dataGridView4[4, i].Value.ToString() +
                                " WHERE Выплачено =" + N_ud + ";";
                    else
                        quer = @"UPDATE  viplati SET id_пользователя =" + dataGridView4[0, i].Value.ToString() +
                                ", Дата ='" + d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                 "', К_выплате= 0" +
                                " WHERE Выплачено =" + N_ud + ";";
                    GetTable(quer, "viplati");
                }
            }
            else
            {
                quer = @"select * from viplati order by id_пользователя desc";
                dataGridView1.DataSource = SetTable(quer);
                if (dataGridView1.RowCount != 0)
                    N_ud = (Convert.ToInt32(dataGridView1[0, 0].Value) + 1).ToString();
                else
                    N_ud = "1";
                for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                {
                    if (dataGridView4[4, i].Value != null)
                        quer = @"insert into viplati(id_пользователя, Дата, К_выплате, Выплачено, Удержано_налоги) values (" +
                                N_ud + ", " + dataGridView4[0, i].Value.ToString() + ", '" +
                                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' ," + dataGridView4[0, i].Value.ToString() + ", " + dataGridView3[4, i].Value.ToString() + ");";
                    else
                      quer = @"insert into viplati(id_пользователя, Дата, К_выплате, Выплачено, Удержано_налоги) values (" +
                                N_ud + ", " + dataGridView4[0, i].Value.ToString() + ", '" +
                                d.Date.Year.ToString() + "-" + d.Date.Month.ToString() + "-" + d.Date.Day.ToString() +
                                "' ," + dataGridView4[0, i].Value.ToString() + ", " + dataGridView3[4, i].Value.ToString() + "'0);";
                    GetTable(quer, "viplati");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
                if (dataGridView2[1, i].FormattedValue.ToString().
                    Contains(textBox10.Text.Trim()))
                {
                    dataGridView2.CurrentCell = dataGridView2[0, i];
                    return;
                }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.RowCount; i++)
                if (dataGridView3[1, i].FormattedValue.ToString().
                    Contains(textBox11.Text.Trim()))
                {
                    dataGridView3.CurrentCell = dataGridView3[0, i];
                    return;
                }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView4.RowCount; i++)
                if (dataGridView4[1, i].FormattedValue.ToString().
                    Contains(textBox12.Text.Trim()))
                {
                    dataGridView4.CurrentCell = dataGridView4[0, i];
                    return;
                }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество,Дата, Вид_начисления, Начисления from ob inner join Nachisl on ob.id_пользователя=Nachisl.id_пользователя";
            dataGridView2.DataSource = SetTable(quer);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            quer = @"select ob.id_пользователя, Фамилия, Имя, Отчество,Дата, Вид_удержания, Удержание from ob inner join Uderzan on ob.id_пользователя=Uderzan.id_пользователя";
            dataGridView3.DataSource = SetTable(quer);
        }

       


   }
}
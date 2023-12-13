using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Chooses : Form
    {
        public Chooses()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "-";
        }
        public Chooses (string Filters)
        {
            InitializeComponent();
            comboBox1.SelectedItem = "-";
            result = Filters;
            string[] strings = Filters.Split(',');
            for (int i = 0; i < strings.Length; i++) 
            {
                if (strings[i] == "st")
                {
                    costSt = int.Parse(strings[i+1]);
                    textBox1.Text = strings[i+1];
                }
                if (strings[i] == "fin")
                {
                    costFin = int.Parse(strings[i+1]);
                    textBox2.Text = strings[i+1];
                }
                if (strings[i] == "type")
                {
                    type = strings[i+1];
                    comboBox1.Text = type;
                }
                if ((strings[i] == "hot"))
                {
                    hotel = int.Parse(strings[i+1]);
                    textBox3.Text = strings[i+1];
                }

            }
        }
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;
        int costSt = 0;
        int costFin = 10000;
        string type = "";
        int hotel = 0;
        string result;
        public string resultString = "";
        private void Chooses_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            resultString = result;
            this.Close();
        }
        private new void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from rooms";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        bool isAnd = false;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                result = "";
                isAnd = false;
                conn.Open();
                sql = @"select * from rooms where r_status='Свободен'";
                if (textBox1.Text == "" && textBox2.Text == "" && comboBox1.Text == "-" && textBox3.Text == "")
                { Select(); }
                else
                {
                    sql += " and";
                }
                if (textBox1.Text != "")
                {
                    costSt = int.Parse(textBox1.Text);
                    sql += $" r_cost::int >{costSt}";
                    result += $"st,{costSt}";
                }
                if (textBox2.Text == "" && comboBox1.Text == "-" && textBox3.Text == "")
                {
                    cmd = new NpgsqlCommand(sql, conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    conn.Close();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    return;
                }
                else
                {
                    if (isAnd == false)
                    {
                        sql += " and";
                        isAnd = true;
                    }
                }
                if (textBox2.Text != "")
                {
                    costFin = int.Parse(textBox2.Text);
                    sql += $" r_cost::int <{costFin}";
                    result += $",fin,{costFin}";
                    isAnd = false;
                }
                if (comboBox1.Text == "-" && textBox3.Text == "")
                {
                    cmd = new NpgsqlCommand(sql, conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    conn.Close();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    return;
                }
                else
                {
                    if (isAnd == false)
                    {
                        sql += " and";
                        isAnd = true;
                    }
                }

                if (comboBox1.Text == "-")
                {
                    type = "";
                }
                else if (comboBox1.Text == "Эконом")
                {
                    type = "Эконом";
                    sql += " r_type = 'Эконом'";
                    result += $",type,{type}";
                    isAnd = false;
                }
                else if (comboBox1.Text == "Стандарт")
                {
                    type = "Стандарт";
                    sql += " r_type = 'Стандарт'";
                    result += $",type,{type}";
                    isAnd = false;
                }
                else if (comboBox1.Text == "Люкс")
                {
                    type = "Люкс";
                    sql += " r_type = 'Люкс'";
                    result += $",type,{type}";
                    isAnd = false;
                }
                if (textBox3.Text == "")
                {
                    cmd = new NpgsqlCommand(sql, conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    conn.Close();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    return;
                }
                else
                {
                    if (isAnd == false)
                    {
                        sql += " and";
                        isAnd = true;
                    }
                    hotel = int.Parse(textBox3.Text);
                    sql += $" r_idhotel = {hotel}";
                    result += $",hot,{hotel}";
                }
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                return;

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }

}

        
    }
}

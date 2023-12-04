using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CursachSUBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private new void Select()
        {
            try 
            {
                conn.Open();
                sql = @"select * from clients";
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

        private void button1_Click(object sender, EventArgs e)
        {
            int result;
            ClientRedact insert = new ClientRedact();
            insert.ShowDialog();
            conn.Open();
            sql = @"select * from cl_insert(:_firstname, :_midname, :_lastname, :_phonenumber, :_idchoose, :_passportseries, :_passportnumber)";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("_firstname", insert.Firstname);
            cmd.Parameters.AddWithValue("_midname", insert.Midname);
            cmd.Parameters.AddWithValue("_lastname", insert.Lastname);
            cmd.Parameters.AddWithValue("_phonenumber", insert.Phonenumber);
            cmd.Parameters.AddWithValue("_idchoose", insert.IdChoose);
            cmd.Parameters.AddWithValue("_passportseries", insert.PassportSeries);
            cmd.Parameters.AddWithValue("_passportnumber", insert.PassportNumber);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
            {
                MessageBox.Show("Inserted new client successfully");
                Select();
            }
            else
            {
                MessageBox.Show("Inserted fail");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Chooses chos = new Chooses();
            chos.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Rooms rooms = new Rooms();
            rooms.ShowDialog();
        }
    }
}

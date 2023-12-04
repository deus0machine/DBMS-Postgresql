using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
        }

        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;

        private void Rooms_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
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

        private void button1_Click(object sender, EventArgs e)
        {
            int result;
            RoomRedact insert = new RoomRedact();
            insert.ShowDialog();
            conn.Open();
            sql = @"select * from r_insert(:_status, :_type, :_cost, :_adress, :_idhotel)";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("_status", insert.Status);
            cmd.Parameters.AddWithValue("_type", insert.Type);
            cmd.Parameters.AddWithValue("_cost", insert.Cost);
            cmd.Parameters.AddWithValue("_adress", insert.Adress);
            cmd.Parameters.AddWithValue("_idhotel", int.Parse(insert.IdHotel));
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
            {
                MessageBox.Show("Inserted new room successfully");
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
    }
}

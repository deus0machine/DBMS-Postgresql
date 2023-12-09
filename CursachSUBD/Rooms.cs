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

        private void button1_Click(object sender, EventArgs e) // Добавление записи о номере
        {
            int result;
            RoomRedact insert = new RoomRedact();
            insert.ShowDialog();
            if (insert.res == 1)
            {
                conn.Open();
                sql = @"select * from r_insert(:_status, :_type, :_cost, :_adress, :_idhotel)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_status", insert.Status);
                cmd.Parameters.AddWithValue("_type", insert.Type);
                cmd.Parameters.AddWithValue("_cost", insert.Cost);
                cmd.Parameters.AddWithValue("_adress", insert.Adress);
                cmd.Parameters.AddWithValue("_idhotel", insert.IdHotel);
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
        }

        private void button2_Click(object sender, EventArgs e) // Изменение информации о номере
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose room to update");
                return;
            }
            int result;
            RoomRedact update = new RoomRedact(dataGridView1.Rows[rowIndex].Cells["r_status"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["r_type"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["r_cost"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["r_adress"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["r_idhotel"].Value.ToString());
            update.ShowDialog();
            try
            {
                if (update.res == 1)
                {
                    conn.Open();
                    sql = @"select * from r_update(:_id, :_status, :_type, :_cost, :_adress, :_idhotel)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["r_id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_status", update.Status);
                    cmd.Parameters.AddWithValue("_type", update.Type);
                    cmd.Parameters.AddWithValue("_cost", update.Cost);
                    cmd.Parameters.AddWithValue("_adress", update.Adress);
                    cmd.Parameters.AddWithValue("_idhotel", update.IdHotel);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Updated successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Updated failed");
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Uodated fail. Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose room to delete");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from r_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["r_id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete room successfully");
                    rowIndex = -1;
                    conn.Close();
                    Select();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Deleted fail. Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                /*                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["firstname"].Value.ToString();
                                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["midname"].Value.ToString();
                                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["lastname"].Value.ToString();*/
            }
        }
    }
}

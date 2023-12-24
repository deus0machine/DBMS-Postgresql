using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace CursachSUBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string login)
        {
            InitializeComponent();
            if (login == "user")
            {
                button6.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
            }
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
        private new void Select() // Ререндер данных таблицы
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

        private void button1_Click(object sender, EventArgs e) //Открытие формы добавления клиента
        {
            int result;
            ClientRedact insert = new ClientRedact();
            insert.ShowDialog();
            if (insert.res == 1)
            {
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
        }

        private void button2_Click(object sender, EventArgs e) // Изменение информации о клиенте
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose client to update");
                return;
            }
            int result;
            ClientRedact update = new ClientRedact(dataGridView1.Rows[rowIndex].Cells["cl_firstname"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_midname"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_lastname"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_phonenumber"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_idchoose"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_passportseries"].Value.ToString(),
                dataGridView1.Rows[rowIndex].Cells["cl_passportnumber"].Value.ToString());
            update.ShowDialog();
            try
            {
                if (update.res == 1)
                {
                    conn.Open();
                    sql = @"select * from cl_update(:_id, :_firstname, :_midname, :_lastname, :_phonenumber, :_idchoose, :_passportseries, :_passportnumber)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["cl_id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_firstname", update.Firstname);
                    cmd.Parameters.AddWithValue("_midname", update.Midname);
                    cmd.Parameters.AddWithValue("_lastname", update.Lastname);
                    cmd.Parameters.AddWithValue("_phonenumber", update.Phonenumber);
                    cmd.Parameters.AddWithValue("_idchoose", update.IdChoose);
                    cmd.Parameters.AddWithValue("_passportseries", update.PassportSeries);
                    cmd.Parameters.AddWithValue("_passportnumber", update.PassportNumber);
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

        private void button3_Click(object sender, EventArgs e) // Удаление записи о клиенте
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose client to delete");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from cl_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["cl_id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete client successfully");
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose client to see the selection");
                return;
            }
            Chooses chos = new Chooses(dataGridView1.Rows[rowIndex].Cells["cl_idchoose"].Value.ToString());
            chos.ShowDialog();
            if (chos.resultString != "")
            {
                int result;
                conn.Open();
                sql = @"select * from cl_update(:_id, :_idchoose)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["cl_id"].Value.ToString()));
                cmd.Parameters.AddWithValue("_idchoose", chos.resultString);
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

        private void button5_Click(object sender, EventArgs e)
        {
            Rooms rooms = new Rooms();
            rooms.ShowDialog();
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

        private void button6_Click(object sender, EventArgs e)
        {
            Agreements ag  = new Agreements();
            ag.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hotels hotels = new Hotels();
            hotels.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Grids grids = new Grids();
            grids.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

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
    public partial class Agreements : Form
    {
        public Agreements()
        {
            InitializeComponent();
        }
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;
        private void Agreements_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private new void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from agreements";
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

        private void button1_Click(object sender, EventArgs e) //Добавление соглашения
        {
            int result;
            AgreementsRedact insert = new AgreementsRedact();
            insert.ShowDialog();
            if (insert.res == 1)
            {
                conn.Open();
                sql = @"select * from ag_insert(:_idclient, :_idroom, :_idhotel, :_resstart, :_resend)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_idclient", insert.idClient);
                cmd.Parameters.AddWithValue("_idroom", insert.idRoom);
                cmd.Parameters.AddWithValue("_idhotel", insert.idHotel);
                cmd.Parameters.AddWithValue("_resstart", NpgsqlTypes.NpgsqlDbType.Date, insert.dateSt);
                cmd.Parameters.AddWithValue("_resend", NpgsqlTypes.NpgsqlDbType.Date, insert.dateFin);
                result = (int)cmd.ExecuteScalar();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Inserted new agreement successfully");
                    Select();
                }
                else
                {
                    MessageBox.Show("Inserted fail");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Обновление данных
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose agreement to update");
                return;
            }
            int result;
            AgreementsRedact update = new AgreementsRedact(int.Parse(dataGridView1.Rows[rowIndex].Cells["ag_idclient"].Value.ToString()),
                int.Parse(dataGridView1.Rows[rowIndex].Cells["ag_idroom"].Value.ToString()),
                int.Parse(dataGridView1.Rows[rowIndex].Cells["ag_idhotel"].Value.ToString()),
                (DateTime)dataGridView1.Rows[rowIndex].Cells["ag_resstart"].Value,
                (DateTime)dataGridView1.Rows[rowIndex].Cells["ag_resend"].Value);
            update.ShowDialog();
            try
            {
                if (update.res == 1)
                {
                    conn.Open();
                    sql = @"select * from ag_update(:_id, :_idclient, :_idroom, :_idhotel, :_resstart, :_resend)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["ag_id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_idclient", update.idClient);
                    cmd.Parameters.AddWithValue("_idroom", update.idRoom);
                    cmd.Parameters.AddWithValue("_idhotel", update.idHotel);
                    cmd.Parameters.AddWithValue("_resstart", NpgsqlTypes.NpgsqlDbType.Date, update.dateSt);
                    cmd.Parameters.AddWithValue("_resend", NpgsqlTypes.NpgsqlDbType.Date, update.dateFin);
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
                MessageBox.Show("Choose agreement to delete");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from ag_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["ag_id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete agreement successfully");
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
            }
        }
    }
}

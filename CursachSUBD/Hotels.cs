using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Hotels : Form
    {
        public Hotels()
        {
            InitializeComponent();
        }
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;
        private new void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from hotels";
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

        private void Hotels_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private void button1_Click(object sender, EventArgs e) //Добавление записи
        {
            int result;
            HotelRedact insert = new HotelRedact();
            insert.ShowDialog();
            if (insert.res == 1)
            {
                conn.Open();
                sql = @"select * from h_insert(:_name, :_owner, :_adress, :_rating, :_img)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", insert.name);
                cmd.Parameters.AddWithValue("_owner", insert.owner);
                cmd.Parameters.AddWithValue("_adress", insert.adress);
                cmd.Parameters.AddWithValue("_rating", insert.rating);
                cmd.Parameters.AddWithValue("_img", NpgsqlDbType.Bytea, 1000000, insert.img);
                result = (int)cmd.ExecuteNonQuery();
                conn.Close();
                if (result >= 0)
                {
                    MessageBox.Show("Inserted new hotel successfully");
                    Select();
                }
                else
                {
                    MessageBox.Show("Inserted fail");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                label1.Text = dataGridView1.Rows[e.RowIndex].Cells["h_name"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["h_img"].Value is DBNull)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    byte[] imageData = (byte[])dataGridView1.CurrentRow.Cells["h_img"].Value;
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Bitmap bitmapImage = new Bitmap(ms);
                        pictureBox1.Image = bitmapImage;
                    }
                }
                /*                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["firstname"].Value.ToString();
                                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["midname"].Value.ToString();
                                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["lastname"].Value.ToString();*/
            }
        }

        private void button2_Click(object sender, EventArgs e) // Обновление записи
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Choose hotel to update");
                return;
            }
            int result;
            string name = "";
            string owner = "";
            string adress = "";
            int rating = 0;
            byte[] img = { };
            if (dataGridView1.Rows[rowIndex].Cells["h_name"].Value.ToString() != null)
                name = dataGridView1.Rows[rowIndex].Cells["h_name"].Value.ToString();
            if (dataGridView1.Rows[rowIndex].Cells["h_owner"].Value.ToString() != null)
                owner = dataGridView1.Rows[rowIndex].Cells["h_owner"].Value.ToString();
            if (dataGridView1.Rows[rowIndex].Cells["h_adress"].Value.ToString() != null)
                adress = dataGridView1.Rows[rowIndex].Cells["h_adress"].Value.ToString();
            bool ratin = int.TryParse(dataGridView1.Rows[rowIndex].Cells["h_rating"].Value.ToString(), out rating);
            if (dataGridView1.Rows[rowIndex].Cells["h_img"].Value is DBNull) { }
                
            else
            {
                        img = (byte[])dataGridView1.Rows[rowIndex].Cells["h_img"].Value;
                    }
            HotelRedact update = new HotelRedact(name, owner, adress, rating, img);
            update.ShowDialog();
            try
            {
                if (update.res == 1)
                {
                    conn.Open();
                    sql = @"select * from h_update(:_id, :_name, :_owner, :_adress, :_rating, :_img)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["h_id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_name", update.name);
                    cmd.Parameters.AddWithValue("_owner", update.owner);
                    cmd.Parameters.AddWithValue("_adress", update.adress);
                    cmd.Parameters.AddWithValue("_rating", update.rating);
                    cmd.Parameters.AddWithValue("_img", update.img);
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
                MessageBox.Show("Choose hotel to delete");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from h_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dataGridView1.Rows[rowIndex].Cells["h_id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete hotel successfully");
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
    }
}

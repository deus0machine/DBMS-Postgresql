using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Autoriz : Form
    {
        string login = "";
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        public Autoriz()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            string password = textBox2.Text;
            conn = new NpgsqlConnection(connstring);
            conn.Open();
            sql = $@"SELECT login, pass FROM users WHERE login = '{login}' AND pass = '{password}';";
            cmd = new NpgsqlCommand(sql, conn);
            if (cmd.ExecuteScalar()!= null)
            {

                if (textBox1.Text == "postgres")
                {
                    this.Hide();
                    login = "admin";
                    Form1 form = new Form1(login);
                    form.ShowDialog();
                }
                else if (textBox1.Text == "user" )
                {
                    this.Hide();
                    login = "user";
                    Form1 form = new Form1(login);
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

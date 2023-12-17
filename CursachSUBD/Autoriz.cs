using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Autoriz : Form
    {
        string login = "";
        public Autoriz()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "postgres" && textBox2.Text == "admin")
            {
                this.Hide();
                login = "admin";
                Form1 form = new Form1(login);
                form.ShowDialog();
            }
            else if (textBox1.Text == "user" && textBox2.Text == "user")
            {
                this.Hide();
                login = "user";
                Form1 form = new Form1(login);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

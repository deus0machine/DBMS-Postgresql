using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Autoriz : Form
    {
        public Autoriz()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "postgres" && textBox2.Text == "18201905")
            {
                this.Hide();
                Form1 form = new Form1();
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

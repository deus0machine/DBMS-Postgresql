using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class ClientRedact : Form
    {
        public string Firstname;
        public string Midname;
        public string Lastname;
        public string Phonenumber;
        public string IdChoose;
        public string PassportSeries;
        public string PassportNumber;
        public ClientRedact()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox7.Text != "")
            {
                Firstname = textBox1.Text;
                Midname = textBox2.Text;
                Lastname = textBox3.Text;
                Phonenumber = textBox4.Text;
                IdChoose = textBox5.Text;
                PassportSeries = textBox6.Text;
                PassportNumber = textBox7.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Пасспортные данные не заполнены!");
            }
    }
    }
}

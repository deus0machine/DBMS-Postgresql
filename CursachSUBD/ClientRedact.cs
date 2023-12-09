using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class ClientRedact : Form
    {
        public int res = 0;
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
        public ClientRedact(string fn, string md, string ln, string phone,string idchoose, string pS, string pN)
        {
            InitializeComponent();
            textBox1.Text = fn;
            textBox2.Text = md;
            textBox3.Text = ln;
            textBox4.Text = phone;
            textBox5.Text = idchoose;
            textBox6.Text = pS;
            textBox7.Text = pN;
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
                res = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Пасспортные данные не заполнены!");
            }
    }
    }
}

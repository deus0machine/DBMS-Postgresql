using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class RoomRedact : Form
    {
        public int res = 0;
        public string Status;
        public string Type;
        public string Cost;
        public string Adress;
        public int IdHotel;
        public RoomRedact()
        {
            InitializeComponent();
        }
        public RoomRedact(string st, string tp, string cst, string adr, string idh)
        {
            InitializeComponent();
            comboBox1.Text = st;
            comboBox2.Text = tp;
            textBox3.Text = cst;
            textBox1.Text = adr;
            textBox2.Text = idh;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && textBox3.Text != "" && comboBox2.Text != "" && textBox2.Text != "")
            {
                Status = comboBox1.Text;
                Type = comboBox2.Text;
                Cost = textBox3.Text;
                Adress = textBox1.Text;
                IdHotel = int.Parse(textBox2.Text);
                res = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Данные о номере не заполнены!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

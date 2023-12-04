using System;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class RoomRedact : Form
    {
        public string Status;
        public string Type;
        public string Cost;
        public string Adress;
        public string IdHotel;
        public RoomRedact()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && textBox3.Text != "" && comboBox2.Text != "" && textBox2.Text != "")
            {
                Status = comboBox1.Text;
                Type = comboBox2.Text;
                Cost = textBox3.Text;
                Adress = textBox1.Text;
                IdHotel = textBox2.Text;
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

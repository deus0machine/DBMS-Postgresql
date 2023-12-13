using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CursachSUBD
{
    public partial class HotelRedact : Form
    {
        public int res = 0;
        public string name;
        public string owner;
        public string adress;
        public int rating;
        public byte[] img;
        public HotelRedact()
        {
            InitializeComponent();
        }
        public HotelRedact(string nm, string own, string adr, int rat, byte [] img)
        {
            InitializeComponent();
            textBox1.Text = nm;
            textBox2.Text = own;
            textBox3.Text = adr;
            textBox4.Text = rat.ToString();
            this.img = img;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
            openFileDialog.ShowDialog(); 
            if (openFileDialog.FileName != "")
            {
                img = File.ReadAllBytes(openFileDialog.FileName);
                textBox5.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox3.Text != "" && textBox2.Text != "")
            {
                name = textBox1.Text;
                owner = textBox2.Text;
                adress = textBox3.Text;
                rating = int.Parse(textBox4.Text);
                res = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Данные о отеле не заполнены!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

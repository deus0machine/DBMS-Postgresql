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
    public partial class AgreementsRedact : Form
    {
        public AgreementsRedact()
        {
            InitializeComponent();
        }
        public AgreementsRedact(int idCl, int IdR, int IdH, DateTime ds, DateTime df)
        {
            InitializeComponent();
            textBox1.Text = idCl.ToString();
            textBox2.Text = IdR.ToString();
            textBox3.Text = IdH.ToString();
            dateTimePicker1.Value = ds;
            dateTimePicker2.Value = df;
        }
        public int res = 0;
        public int idClient;
        public int idRoom;
        public int idHotel;
        public DateTime dateSt;
        public DateTime dateFin;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && dateTimePicker1.Text != "" && dateTimePicker2.Text != "")
            {
                idClient = int.Parse(textBox1.Text);
                idRoom = int.Parse(textBox2.Text);
                idHotel = int.Parse(textBox3.Text);
                dateSt = dateTimePicker1.Value.Date;
                dateFin = dateTimePicker2.Value.Date;
                res = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Данные о соглашении не заполнены!");
            }
        }
    }
}

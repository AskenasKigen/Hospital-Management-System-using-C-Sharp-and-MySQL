using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            SetDefaultWindowSize();
            SetNewSize();

        }
        private void SetDefaultWindowSize()
        {
            int sizeW, sizeH;
            sizeW = 2300;
            sizeH = 1200;

            var size = new Size(sizeW, sizeH);

            Size = size;
            MinimumSize = size;
        }

        private void SetNewSize()
        {
            Size = new Size(Width, 30);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Channel c = new Channel();
            c.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doctor d = new Doctor();
            d.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Registration r = new Registration();
            r.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

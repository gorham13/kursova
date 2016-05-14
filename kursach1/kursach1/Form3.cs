using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach1
{
    public partial class Form3 : Form
    {
        Form1 f1;
        public Form3(Form1 f)
        {
            InitializeComponent();
            f1 = f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(f1);
            f2.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.open();
        }
    }
}

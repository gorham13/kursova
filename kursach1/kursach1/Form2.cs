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

namespace kursach1
{
    public partial class Form2 : Form
    {
        Form1 f;
        public Form2(Form1 F)
        {
            InitializeComponent();
            f = F;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.setTree(textBox1.Text);
            f.path += textBox1.Text;
            Directory.CreateDirectory(f.path);
            Directory.CreateDirectory(f.path+"\\"+textBox1.Text);
            this.Close();
        }
    }
}

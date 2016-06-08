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
    public partial class Form4 : Form
    {
        Form1 f1;
        public Form4(Form1 f, string str)
        {
            InitializeComponent();
            f1 = f;
            this.richTextBox1.Size = new Size(this.Width, this.Height - 60);
            this.richTextBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.richTextBox1.Text = str;
        }

        private void Form4_Resize(object sender, EventArgs e)
        {
            this.richTextBox1.Size = new Size(this.Width, this.Height - 60);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.setRich(richTextBox1.Text);
            File.WriteAllText(f1.path + "\\" + f1.getNode()+"\\text.txt", richTextBox1.Text);
            this.Close();
        }
    }
}

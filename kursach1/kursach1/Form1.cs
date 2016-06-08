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
using System.Xml;

namespace kursach1
{
    public partial class Form1 : Form
    {
        public string path = "Дерева\\";
        public Form1()
        {
            InitializeComponent();
            this.button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.button3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.button4.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.button5.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.textBox1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.treeView1.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.pictureBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.richTextBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            Form3 f = new Form3(this);
            f.ShowDialog();
        }
        public void setTree(string text)
        {
            treeView1.Nodes.Add(text);
        }
        public void setRich(string text)
        {
            richTextBox1.Text = text;
        }
        public string getNode()
        {
            return treeView1.SelectedNode.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    treeView1.SelectedNode.Nodes.Add(textBox1.Text);
                    Directory.CreateDirectory(path + "\\" + textBox1.Text);
                }
                catch(System.NullReferenceException)
                {
                    MessageBox.Show("Оберіть елемент");
                }
            }
            else
                MessageBox.Show("Введіть ім'я");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Nodes.Remove(treeView1.SelectedNode);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Text = textBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path1 = "";
            OpenFileDialog fbd = new OpenFileDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                path1 = fbd.FileName;
                File.Copy(path1, path + "\\" + treeView1.SelectedNode.Text + "\\foto.jpg");
                pictureBox1.Image = new Bitmap(path + "\\" + getNode() + "\\foto.jpg");
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.treeView1.Size = new Size(this.Width/3, this.Height - 95);
            this.treeView1.Top = 20;
            this.richTextBox1.Size = new Size((this.Width / 3) * 2 - 40, (this.Height / 3) - 15);
            this.richTextBox1.Top = (this.Height / 3) * 2 - 60;
            this.richTextBox1.Left = this.Width / 3 + 20;
            this.pictureBox1.Size = new Size((this.Width / 3) * 2 - 40, (this.Height / 3) * 2 - 85);
            this.pictureBox1.Top = 20;
            this.pictureBox1.Left = this.Width / 3 + 20;
            this.textBox1.Size = new Size(this.button5.Left - 20, this.textBox1.Height);
        }

        private void зберегтиФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlTextWriter xw = new XmlTextWriter(path+"\\"+path.Substring(path.LastIndexOf("\\"))+".xml", null);
            xw.Formatting = Formatting.Indented;
            TreeViewToXML.Write(xw, treeView1.Nodes);
            xw.Close();
            MessageBox.Show("Збережено");
        }

        public void open()
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.SelectedPath = @"C:\Users\igor\Documents\Visual Studio 2015\Projects\kursach1\kursach1\bin\Debug\Дерева";
                DialogResult dr = fbd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                }
                treeView1.Nodes.Clear();
                XmlTextReader xr = new XmlTextReader(path + path.Substring(path.LastIndexOf("\\")) + ".xml");
                TreeViewToXML.Read(xr, treeView1.Nodes);
                pictureBox1.Image = new Bitmap("camera_200.png");
            }
            catch(System.IO.FileNotFoundException)
            {
                MessageBox.Show("Оберіть коректну папку і спробуйте знову відкрити файл");
            }
        }

        private void відкритиФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            richTextBox1.Clear();
            if (File.Exists(path + "\\" + getNode() + "\\text.txt"))
            {
                string[] s1 = File.ReadAllLines(path + "\\" + getNode() + "\\text.txt");
                foreach (string s in s1)
                    richTextBox1.Text += (s+"\n");
            }
            if (File.Exists(path + "\\" + getNode() + "\\foto.jpg"))
            {
                pictureBox1.Image = new Bitmap(path + "\\" + getNode() + "\\foto.jpg");
            }
            else
                pictureBox1.Image = new Bitmap("camera_200.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this, richTextBox1.Text);
            f4.Show();
        }
    }
    public class TreeViewToXML
    {
        public static void Write(XmlWriter w, TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                w.WriteStartElement("node");
                w.WriteAttributeString("text", tn.Text);
                Write(w, tn.Nodes);
                w.WriteEndElement();
            }
        }

        public static void Read(XmlReader xr, TreeNodeCollection nodes)
        {
            while (xr.Read())
            {
                if (xr.NodeType != XmlNodeType.Element)
                    continue;

                TreeNode nn = new TreeNode();
                StringBuilder sb = new StringBuilder();
                if (xr.HasAttributes)
                {
                    xr.MoveToAttribute("text");
                    sb.Append(xr.Value);
                    xr.MoveToElement();
                }

                nn.Text = sb.ToString();
                nodes.Add(nn);
                XmlReader nxr = xr.ReadSubtree();
                if (nxr.Read())
                    Read(nxr, nn.Nodes);
            }
        }
    }
}

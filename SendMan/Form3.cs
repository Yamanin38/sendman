using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SendMan
{
    public partial class Form3 : Form
    {
        private string classroomlabel;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void titlepictureBox_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            titlepictureBox.Parent = barpictureBox;
            titlepictureBox.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 画面を非表示
            this.Visible = false;
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete("temp.txt");
            File.Delete("temp2.txt");
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class.modeFlag = "folder";
            // 画面を非表示
            this.Visible = false;
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Class.modeFlag = "file";
            // 画面を非表示
            this.Visible = false;
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}

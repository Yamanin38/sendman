using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace SendMan
{

    public partial class Form5 : Form
    {
        private Hashtable ip = new Hashtable
        {
            ["5A"] = 35,
            ["5B"] = 36,
            ["5C"] = 38,
            ["5D1"] = 46,
            ["5D2"] = 46,
            ["7A"] = 39,
            ["7B"] = 40,
            ["7C"] = 45,
            ["8B"] = 42,
            ["8C"] = 43,
            ["8D"] = 44,
        };
        private string classroomlabel;
        private Form1 f1 = new Form1();
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // テキストファイルを作成し、1行目に教室名2行目にipアドレスを記載する
            StreamWriter sw = File.CreateText(@"temp.txt");
            sw.WriteLine(classroomlabel);
            sw.WriteLine(ip[classroomlabel]);
            sw.Close();
            //次画面を非表示
            this.Visible = false;
            Form3 f3 = new Form3();
            f3.Show();

        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles("*", System.IO.SearchOption.AllDirectories);
            try
            {
                this.Close();
                
            }
            catch(Exception e)
            {
                if (e.Message.Contains("アクセスが拒否"))
                    MessageBox.Show("ITINSで実行してください(;´･ω･)", "アクセス拒否", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void titlepictureBox_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string srcfolder = "SOURCE";
            if (!Directory.Exists(srcfolder))
                Directory.CreateDirectory(srcfolder);
            titlepictureBox.Parent = barpictureBox;
            titlepictureBox.BackColor = Color.Transparent;
            button1.Enabled = false;
            button1.BackColor = Color.White;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete("temp1.txt");
            File.Delete("temp2.txt");
            Application.Exit();
        }

        private void labelClassroom_Click(object sender, EventArgs e)
        {

        }

        private void button5A_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "5A";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "5A";
        }

        private void button5B_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "5B";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "5B";
        }

        private void button5C_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "5C";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "5C";
        }

        private void button5D1_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "5D1";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "5D1";
        }

        private void button5D2_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "5D2";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "5D2";
        }

        private void button7A_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "7A";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "7A";
        }

        private void button7B_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "7B";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "7B";
        }

        private void button7C_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "7C";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "7C";
        }

        private void button8A_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "8A";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "8A";
        }

        private void button8B_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "8B";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "8B";
        }

        private void button8C_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "8C";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "8C";
        }

        private void button8D_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "8D";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "8D";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelClassroom.Text = "IMCSTU2";
            button1.Enabled = true;
            button1.BackColor = Color.MediumSeaGreen;
            classroomlabel = "IMCSTU2";
        }
    }
}

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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string classroomlabel;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value)
            {
                MessageBox.Show("最大値が最小値を下回っています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // temp2の1行目と2行目にそれぞれ最小値と最大値を、3行目にIPアドレスの完成形を記載する
                StreamWriter sw1 = File.CreateText(@"temp2.txt");
                sw1.WriteLine(numericUpDown1.Value);
                sw1.WriteLine(numericUpDown2.Value);
                sw1.WriteLine(@"172.24." + File.ReadLines(@"temp.txt").Skip(1).First() + @".");
                sw1.Close();
                // 画面を非表示
                this.Visible = false;
                Form6 f6 = new Form6();
                f6.Show();
            }
        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles("*", System.IO.SearchOption.AllDirectories);
            try
            {
                // 画面を非表示
                this.Visible = false;
                Form2 f2 = new Form2();
                f2.Show();

                // pingを送り、返事があった端末のみに送信する処理をここに書く

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
            titlepictureBox.Parent = barpictureBox;
            titlepictureBox.BackColor = Color.Transparent;
            classroomlabel = File.ReadLines(@"temp.txt").Skip(0).First();
            labelClassroom.Text = classroomlabel;
            label5.Text = classroomlabel;
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
    }
}

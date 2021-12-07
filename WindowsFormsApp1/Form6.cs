﻿using System;
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
    public partial class Form6 : Form
    {
        private string classroomlabel;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dstpath = @"C:\" + textBox1.Text;
            if (!Directory.Exists(dstpath))
            {
                MessageBox.Show("今確認してきましたがそのようなディレクトリは存在しませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //CopyFiles(@"C:\Users\yamanin-Note\Desktop\ゼミナール教室URL.txt", @"\\172.24.44.123\C$\Users\b8067\Desktop");//@"\\172.24.44.124\C:\Users\b8067\Desktop\destination"
                File.Copy(@"C:\Users\yamanin-Note\Desktop\ゼミナール教室URL.txt", @"\\172.24.44.123\C$\Users\b8067\Desktop\ゼミナール教室URL.txt", true);
                // 画面を非表示
                this.Visible = false;
                Form2 f2 = new Form2();
                f2.Show();
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
            textBox1.Text = "Users/stuadmin/Desktop/";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 画面を非表示
            this.Visible = false;
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete("temp.txt");
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

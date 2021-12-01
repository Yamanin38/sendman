using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CopyFiles(@"C:\Users\b8067\Desktop\copy", @"\\172.24.44.123\C$\Users\stuadmin\Desktop");//@"\\172.24.44.124\C:\Users\b8067\Desktop\destination"
            //次画面を非表示
            this.Visible = false;

            //Form2を表示
            Form2 f2 = new Form2();
            f2.Show();
        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles("*", System.IO.SearchOption.AllDirectories);
            try
            {
                //次画面を非表示
                this.Visible = false;

                //Form2を表示
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //次画面を非表示
            this.Visible = false;

            //Form2を表示
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

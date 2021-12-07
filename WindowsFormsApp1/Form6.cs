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
    public partial class Form6 : Form
    {

        private string classroomlabel;
        private string dstpath_min;
        private string classroom_ip_min;
        private string classroom_ip_max;
        private string classroom_ip;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.EndsWith(@"\"))
                textBox1.Text = textBox1.Text + @"\";
            classroom_ip_min = File.ReadLines(@"temp2.txt").Skip(0).First();
            classroom_ip_max = File.ReadLines(@"temp2.txt").Skip(1).First();
            classroom_ip = File.ReadLines(@"temp2.txt").Skip(2).First();
            if (int.Parse(classroom_ip_min) < 10)
                dstpath_min = @"\\" + classroom_ip + "10" + classroom_ip_min + @"\C$\" + textBox1.Text;
            else
                dstpath_min = @"\\" + classroom_ip + "1" + classroom_ip_min + @"\C$\" + textBox1.Text;
            if (!Directory.Exists(dstpath_min))
            {
                MessageBox.Show("そのようなディレクトリは存在しないか、権限がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CopyFiles(@"SOURCE", dstpath_min);//@"\\172.24.44.124\C:\Users\b8067\Desktop\destination"
                // File.Copy(@"C:\Users\b8067\Desktop\copy\test.txt", dstpath_min + "test.txt", true);
            }
        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles("*", System.IO.SearchOption.AllDirectories);


            // 最小値から最大値までのPC番号にpingを送り、返事がなかった場合その端末のみループから除外する処理を書く


            for (int i = int.Parse(classroom_ip_min); i <= int.Parse(classroom_ip_max); i++)
            {
                if (i < 10)
                    dstPath = @"\\" + classroom_ip + "10" + i + @"\C$\" + textBox1.Text;
                else
                    dstPath = @"\\" + classroom_ip + "1" + classroom_ip_min + @"\C$\" + textBox1.Text;
                foreach (var file in files)
                {
                    // ログファイルを作り、失敗したPCを記載する処理
                    string dst = dstPath + file.Name;
                    File.Copy(file.FullName, dst, true);
                }
            }

            // 画面を非表示
            this.Visible = false;
            Form2 f2 = new Form2();
            f2.Show();


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
            textBox1.Text = @"Users\b8067\Desktop\";
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
            File.Delete("temp2.txt");
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

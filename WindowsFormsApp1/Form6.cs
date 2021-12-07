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
using System.Net.NetworkInformation;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        Ping sender = new Ping();
        private StreamWriter sw;
        private string classroomlabel;
        private string dstpath_min;
        private string classroom_ip_min;
        private string classroom_ip_max;
        private string classroom_ip;
        private string ipaddress;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.EndsWith(@"\"))
                textBox1.Text = textBox1.Text + @"\";
                classroomlabel = File.ReadLines(@"temp.txt").Skip(0).First();
                classroom_ip_min = File.ReadLines(@"temp2.txt").Skip(0).First();
                classroom_ip_max = File.ReadLines(@"temp2.txt").Skip(1).First();
                classroom_ip = File.ReadLines(@"temp2.txt").Skip(2).First();
            if (int.Parse(classroom_ip_min) < 10)
                dstpath_min = @"\\" + classroom_ip + "10" + classroom_ip_min + @"\C$\" + textBox1.Text;
            else
                dstpath_min = @"\\" + classroom_ip + "1" + classroom_ip_min + @"\C$\" + textBox1.Text;
            if (!Directory.Exists(dstpath_min))
            {
                MessageBox.Show("そのようなディレクトリは存在しないか、アクセス権限がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sw = File.CreateText(@"failedlog.txt");
                sw.WriteLine("---コピーに失敗したPC---");
                CopyFiles(@"SOURCE", dstpath_min);//@"\\172.24.44.124\C:\Users\b8067\Desktop\destination"
                // File.Copy(@"C:\Users\b8067\Desktop\copy\test.txt", dstpath_min + "test.txt", true);
            }
        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            PingReply reply;
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

            for (int i = int.Parse(classroom_ip_min); i <= int.Parse(classroom_ip_max); i++)
            {
                if (i < 10)
                {
                    dstPath = @"\\" + classroom_ip + "10" + i + @"\C$\" + textBox1.Text;
                    ipaddress = classroom_ip + "10" + i;
                }
                else
                {
                    dstPath = @"\\" + classroom_ip + "1" + classroom_ip_min + @"\C$\" + textBox1.Text;
                    ipaddress = classroom_ip + "1" + i;
                }
                    foreach (var file in files)
                {
                    string dst = dstPath + file.Name;
                    try
                    {
                        reply = sender.Send(ipaddress);
                        if (reply.Status != IPStatus.Success)
                            throw new Exception();
                        else
                            File.Copy(file.FullName, dst, true);
                    }
                    catch
                    {
                       // ログファイルに失敗したPC名を記載する処理
                        sw.WriteLine(classroomlabel + i);
                    }
                }
            }

            // 画面を非表示
            this.Visible = false;
            Form2 f2 = new Form2();
            f2.Show();
            sw.Close();

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

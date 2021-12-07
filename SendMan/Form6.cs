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

namespace SendMan
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
            label4.Visible = true;
            label4.Update();
            // 他のボタンを使えなくする
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;
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
                label4.Visible = false;
                label4.Update();
                // 他のボタンを使えるようにする
                button1.Enabled = true;
                button2.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                // 失敗ログを作成
                sw = File.CreateText(@"failedlog.txt");
                sw.WriteLine("---コピーに失敗したPC---");
                // ファイルコピーメソッド実行
                CopyFiles("SOURCE", dstpath_min);
            }
        }

        public void CopyFiles(string srcPath, string dstPath)
        {
            label4.Visible = false;
            label4.Update();
            PingReply reply;
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            // 他のボタンを使えなくする
            label3.Visible = true;
            progressBar1.Visible = true;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;

            //コントロールを初期化する
            progressBar1.Minimum = int.Parse(classroom_ip_min);
            progressBar1.Maximum = int.Parse(classroom_ip_max);
            progressBar1.Value = 0;
            label3.Text = "コピー開始";
            //label3を再描画する
            label3.Update();

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

                //ProgressBar1の値を変更する
                progressBar1.Value = i;
                //Label1のテキストを変更する
                label3.Text = classroomlabel + i + "にコピー中...";

                //Label1を再描画する
                label3.Update();

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
                            break;
                        }
                    }
            }
            // ファイルを閉じる
            sw.Close();

            // 結果を報告する
            label3.Text = "完了しました。";

            // ダイアログ表示
            MessageBox.Show("コピーが完了しました、失敗したPCはfailedlog.txtへ出力されます。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // 画面を切り替え
            this.Visible = false;
            Form5 f5 = new Form5();
            f5.Show();

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

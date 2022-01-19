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
using System.Diagnostics;

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
        private string dstDrive;
        private string ipPlus;
        private string ipPlus2;
        readonly Process p = new Process();
        public Form6()
        {
            InitializeComponent();
        }

    private void button1_Click(object sender, EventArgs e)
        {
            // exeファイルの階層に「FileCopy.bat」がなければメッセージボックスを表示
            string batfile = "FileCopy.bat";
            if (!File.Exists(batfile))
                MessageBox.Show("\"FileCopy.bat\"がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // ドライブのコンボボックスがカラだったら警告
                if (comboBox1.SelectedItem == null || string.IsNullOrEmpty(comboBox1.Text) == true)
                    MessageBox.Show("送信先のドライブを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // 設定ファイルの第4オクテットが空白でなければ値を代入する
                    if (Class.plusIP != "")
                    {
                        ipPlus = Class.plusIP.Substring(0, 2);
                        ipPlus2 = Class.plusIP.Substring(0, 1);
                    }

                    // 他のボタンを使えなくする
                    button1.Enabled = false;
                    button2.Enabled = false;
                    textBox1.Enabled = false;

                    // ファイルパスの最後に\を付ける
                    if (!textBox1.Text.EndsWith(@"\"))
                        textBox1.Text = textBox1.Text + @"\";

                    // tempファイルに教室名、IP最小値、IP最大値、第3オクテットを記述
                    classroomlabel = File.ReadLines(@"temp.txt").Skip(0).First();
                    classroom_ip_min = File.ReadLines(@"temp2.txt").Skip(0).First();
                    classroom_ip_max = File.ReadLines(@"temp2.txt").Skip(1).First();
                    classroom_ip = File.ReadLines(@"temp2.txt").Skip(2).First();

                    // コンボボックスからドライブ名をdstDriveに代入
                    dstDrive = comboBox1.Text;

                    // PC番号が10以前か以降で違う処理をする(dstpath_minに送り先の最小PCのIPアドレス+パスを代入する)
                    if (int.Parse(classroom_ip_min) < 10)
                        dstpath_min = @"\\" + classroom_ip + ipPlus + classroom_ip_min + @"\" + dstDrive + @"$\" + textBox1.Text;
                    else
                        dstpath_min = @"\\" + classroom_ip + ipPlus2 + classroom_ip_min + @"\" + dstDrive + @"$\" + textBox1.Text;

                    // 最初のPCでディレクトリが存在するかを確認
                    if (!Directory.Exists(dstpath_min))
                    {
                        MessageBox.Show("そのようなディレクトリは存在しないか、アクセス権限がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // 他のボタンを使えるようにする
                        button1.Enabled = true;
                        button2.Enabled = true;
                        textBox1.Enabled = true;
                    }
                    else
                    {
                        // 失敗ログファイルを作成
                        sw = File.CreateText(@"failedlog.txt");
                        sw.WriteLine("---コピーに失敗したPC---");

                        Class.dst_min = dstpath_min;
                        // ファイルコピーメソッド実行(バックグラウンド操作の実行を開始する)
                        this.backgroundWorker1.RunWorkerAsync();
                    }
                }
            }
        }
        //public void CopyDirectory(string srcPath, string dstPath)
        //{
        //    // PING用の宣言
        //    PingReply reply;

        //    // 他のボタンを使えなくする
        //    label3.Visible = true;
        //    progressBar1.Visible = true;
        //    button1.Enabled = false;
        //    button2.Enabled = false;
        //    textBox1.Enabled = false;

        //    // プログレスバーのコントロールを初期化する
        //    progressBar1.Minimum = int.Parse(classroom_ip_min);
        //    progressBar1.Maximum = int.Parse(classroom_ip_max) + 1;
        //    progressBar1.Value = int.Parse(classroom_ip_min);
        //    label3.Text = "コピー開始";

        //    // プログレスバー上のlabel3を再描画する
        //    label3.Update();

        //    // PCの最小から最大までの回数ループする
        //    for (int i = int.Parse(classroom_ip_min); i <= int.Parse(classroom_ip_max); i++)
        //    {
        //        if (i < 10)
        //        {
        //            dstPath = @"\\" + classroom_ip + ipPlus + i + @"\" + dstDrive + @"$\" + textBox1.Text;
        //            ipaddress = classroom_ip + ipPlus + i;
        //        }
        //        else
        //        {
        //            dstPath = @"\\" + classroom_ip + ipPlus2 + i + @"\" + dstDrive + @"$\" + textBox1.Text;
        //            ipaddress = classroom_ip + ipPlus2 + i;
        //            //dstPath→\\172.24.oo.ooo\o$\ooo\
        //        }

        //        //ProgressBar1の値を変更する
        //        progressBar1.Value = i + 1;
        //        //Label1のテキストを変更する
        //        label3.Text = classroomlabel + i + "にコピー中...";

        //        //Label1を再描画する
        //        label3.Update();

        //        // PINGを送って生存確認(なければスルーし失敗PCに記述
        //        reply = sender.Send(ipaddress);
        //        if (reply.Status != IPStatus.Success)
        //            sw.WriteLine(classroomlabel + i);
        //        else
        //        {
        //            // バッチファイル名を指定
        //            p.StartInfo.FileName = "FileCopy.bat";

        //            // dstPath = @"\\192.168.11.33\C$\Users\やまにん\Desktop\"; // デバッグ用パス

        //            // xcopy SOURCE dstPath\ /E /Yとなっている
        //            p.StartInfo.Arguments = "SOURCE " + dstPath;

        //            // コンソールウインドウを非表示にする
        //            p.StartInfo.CreateNoWindow = true;

        //            // 標準出力を有効にする
        //            p.StartInfo.RedirectStandardOutput = true;

        //            // これをしないとエラーが出る
        //            p.StartInfo.UseShellExecute = false;

        //            // 管理者として実行する場合
        //            p.StartInfo.Verb = "RunAs";

        //            // 実行
        //            p.Start();

        //            // batファイルの返り値をresultに代入
        //            Class.results = p.StandardOutput.ReadToEnd();

        //            // resultの数字だけ切り抜く
        //            // Class.results = Class.results.Substring(0, 1);

        //            // 終わるまで待つ処理
        //            p.WaitForExit();

        //            // 閉じる
        //            p.Close();

        //            if (!Class.results.Contains("コピーしました")) // 成功が含まれていなかったら
        //            {
        //                // ログファイルに失敗したPC名を記載する処理
        //                sw.WriteLine(classroomlabel + i);
        //            }
        //        }
        //    }
        //    // 失敗ログファイルを閉じる
        //    sw.Close();

        //    // 結果を報告する
        //    label3.Text = "完了しました。";

        //    // ダイアログ表示
        //    MessageBox.Show("コピーが完了しました、コピーに失敗したPCはfailedlog.txtへ出力されます。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    // 他のボタンを使えるようにする
        //    button1.Enabled = true;
        //    button2.Enabled = true;
        //    textBox1.Enabled = true;

        //    // 画面を切り替え
        //    this.Visible = false;
        //    Form5 f5 = new Form5();
        //    f5.Show();
        //}

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
            comboBox1.SelectedIndex = 0;
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

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // 変数の代入
            string dstPath = Class.dst_min;
            string srcPath = "SOURCE";

            // PING用の宣言
            PingReply reply;
            Ping sender2 = new Ping();

            // 他のボタンを使えなくする
            label3.Visible = true;
            progressBar1.Visible = true;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;

            // フォームのコントロールを無効化
            this.ControlBox = !this.ControlBox;

            // プログレスバーのコントロールを初期化する
            progressBar1.Minimum = int.Parse(classroom_ip_min);
            progressBar1.Maximum = int.Parse(classroom_ip_max) + 1;
            progressBar1.Value = int.Parse(classroom_ip_min);
            label3.Text = "コピー開始";

            // プログレスバー上のlabel3を再描画する
            label3.Update();

            // PCの最小から最大までの回数ループする
            for (int i = int.Parse(classroom_ip_min); i <= int.Parse(classroom_ip_max); i++)
            {
                if (i < 10)
                {
                    dstPath = @"\\" + classroom_ip + ipPlus + i + @"\" + dstDrive + @"$\" + textBox1.Text;
                    ipaddress = classroom_ip + ipPlus + i;
                }
                else
                {
                    dstPath = @"\\" + classroom_ip + ipPlus2 + i + @"\" + dstDrive + @"$\" + textBox1.Text;
                    ipaddress = classroom_ip + ipPlus2 + i;
                    //dstPath→\\172.24.oo.ooo\o$\ooo\
                }

                //ProgressBar1の値を変更する
                // progressBar1.Value = i + 1;

                //進捗状況の報告
                this.backgroundWorker1.ReportProgress(i + 1);
                System.Threading.Thread.Sleep(100);

                //Label1のテキストを変更する
                label3.Text = classroomlabel + i + "にコピー中...";

                //Label1を再描画する
                label3.Update();

                // PINGを送って生存確認(なければスルーし失敗PCに記述
                reply =sender2.Send(ipaddress);
                if (reply.Status != IPStatus.Success)
                    sw.WriteLine(classroomlabel + i);
                else
                {
                    // バッチファイル名を指定
                    p.StartInfo.FileName = "FileCopy.bat";

                    // dstPath = @"\\192.168.11.33\C$\Users\やまにん\Desktop\"; // デバッグ用パス

                    // xcopy SOURCE dstPath\ /E /Yとなっている
                    p.StartInfo.Arguments = "SOURCE " + dstPath;

                    // コンソールウインドウを非表示にする
                    p.StartInfo.CreateNoWindow = true;

                    // 標準出力を有効にする
                    p.StartInfo.RedirectStandardOutput = true;

                    // これをしないとエラーが出る
                    p.StartInfo.UseShellExecute = false;

                    // 管理者として実行する場合
                    p.StartInfo.Verb = "RunAs";

                    // 実行
                    p.Start();

                    // batファイルの返り値をresultに代入
                    Class.results = p.StandardOutput.ReadToEnd();

                    // resultの数字だけ切り抜く
                    // Class.results = Class.results.Substring(0, 1);

                    // 終わるまで待つ処理
                    p.WaitForExit();

                    // 閉じる
                    p.Close();

                    if (!Class.results.Contains("コピーしました")) // 成功が含まれていなかったら
                    {
                        // ログファイルに失敗したPC名を記載する処理
                        sw.WriteLine(classroomlabel + i);
                    }
                }
            }
            // 失敗ログファイルを閉じる
            sw.Close();

            // 結果を報告する
            label3.Text = "完了しました。";

            // ダイアログ表示
            MessageBox.Show("コピーが完了しました、コピーに失敗したPCはfailedlog.txtへ出力されます。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 他のボタンを使えるようにする
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = true;

            // フォームのコントロールを有効化
            this.ControlBox = !this.ControlBox;

            // 画面を切り替え
            this.Visible = false;
            Form5 f5 = new Form5();
            f5.Show();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //進捗状況をプログレスバーに表示
            this.progressBar1.Value = e.ProgressPercentage;
        }
    }
}

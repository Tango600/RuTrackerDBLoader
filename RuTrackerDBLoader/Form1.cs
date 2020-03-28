using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RuTrackerDBLoader.Reader;
using Microsoft.Win32;
using System.Diagnostics;

namespace RuTrackerDBLoader
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellation;
        private DateTime timeStart;

        public Form1()
        {
            InitializeComponent();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.LastFile = tbPath.Text;
            Properties.Settings.Default.Database = tbDatabase.Text;
            Properties.Settings.Default.Save();
        }

        private string ConvertSpanToString(TimeSpan span)
        {
            string view = "";
            if (span.TotalDays > 1)
            {
                view += ((int)span.TotalDays).ToString() + " d. ";
            }
            view += span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString();
            return view;
        }

        private decimal BolcksPerSecond(int blocks, double seconds)
        {
            if (seconds > 0 && blocks > 0)
            {
                return Convert.ToDecimal(blocks / seconds);
            }
            else
                return 0;
        }

        private void OnProgress(int Progress)
        {
            if (Progress % 1000 == 0)
            {
                var sp = DateTime.Now - timeStart;
                lbProgress.Text = Progress.ToString("0,0");
                lbTime.Text = ConvertSpanToString(sp);
                lbSpeed.Text = BolcksPerSecond(Progress, sp.TotalSeconds).ToString("0.00") + " блок./сек.";
            }
        }

        private void OnErrorBlock(int ErrorBlocks)
        {
            lbBadBlocks.Text = ErrorBlocks.ToString("0,0");
        }

        private void OnComplite(int TotalBlocks)
        {
            lbProgress.Text = TotalBlocks.ToString("0,0") + " Готово!";
            lbTime.Text = (DateTime.Now - timeStart).ToString();

            if (chShutdownPC.Checked)
            {
                Process.Start("shutdown", "-s -t 300 -f");
            }
        }

        private void LockInterface(bool lockMode)
        {
            btOpen.Enabled = !lockMode;
            btLoad.Enabled = !lockMode;
            btGetCount.Enabled = !lockMode;
            btUnpack.Enabled = !lockMode;

            tbDatabase.Enabled = !lockMode;
            tbPath.Enabled = !lockMode;

            chTestMode.Enabled = !lockMode;
            chUpdateMode.Enabled = !lockMode;
            chNoContent.Enabled = !lockMode;
            chClearDB.Enabled = !lockMode;
            chNoLoadFiles.Enabled = !lockMode;
        }

        private void UnLockInterface()
        {
            LockInterface(false);
        }

        private async void BtLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbPath.Text))
            {
                if (!chTestMode.Checked && chClearDB.Checked)
                {
                    if (MessageBox.Show("Очистить базу?", "Очистка базы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        await DataReder.ClearDB(tbDatabase.Text, ConfigReader.Config.UserName, ConfigReader.Config.Password, ConfigReader.Config.Cogepage);
                    }
                }

                cancellation = new CancellationTokenSource();

                SaveSettings();
                DataReder.OnProgressEvent += OnProgress;
                DataReder.OnErrorEvent += OnErrorBlock;
                DataReder.OnCompliteEvent += OnComplite;

                timeStart = DateTime.Now;
                try
                {
                    LockInterface(true);

                    await DataReder.Load(tbPath.Text, tbDatabase.Text, ConfigReader.Config.UserName, ConfigReader.Config.Password, ConfigReader.Config.Cogepage,
                        chTestMode.Checked, chUpdateMode.Checked, chNoContent.Checked, chNoLoadFiles.Checked, chNotDeleted.Checked, cancellation);
                }
                catch
                {
                    //
                }
                UnLockInterface();
            }
        }

        private void BtOpen_Click(object sender, EventArgs e)
        {
            if (ofdXML.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = ofdXML.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbPath.Text = Properties.Settings.Default.LastFile;
            tbDatabase.Text = Properties.Settings.Default.Database;

            if (!File.Exists(ConfigReader.ConfigFileName))
            {
                ConfigReader.InitConfig(ConfigReader.ConfigFileName);
            }
            ConfigReader.LoadConfig(ConfigReader.ConfigFileName);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void BtTestDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbDatabase.Text.Contains(":"))
                {
                    var ds = new FBDataSet(tbDatabase.Text, ConfigReader.Config.UserName, ConfigReader.Config.Password, ConfigReader.Config.Cogepage);
                    ds.Dispose();
                    MessageBox.Show("Соединение с базой успешно!", "Тест соединения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Строка соединения некорректна!" + Environment.NewLine + "Не указан сервер", "Параметры соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сбой соединения с базой!" + Environment.NewLine + ex.Message, "Тест соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btUnpack_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbPath.Text))
            {
                LockInterface(true);

                if (MessageBox.Show("Распаковать архив с xml?", "Распаковка", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var z7Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\7-zip");
                    if (z7Key.ValueCount > 0)
                    {
                        string z7Path = "";

                        if (z7Key.GetValue("Path64") != null)
                        {
                            z7Path = z7Key.GetValue("Path64").ToString();
                        }
                        else
                        {
                            if (z7Key.GetValue("Path") != null)
                            {
                                z7Path = z7Key.GetValue("Path").ToString();
                            }
                        }

                        string z7EXE = z7Path + "7zG.exe";
                        if (!File.Exists(z7EXE))
                        {
                            z7EXE = z7Path + "7z.exe";
                        }
                        if (File.Exists(z7EXE))
                        {
                            string outpudDir = Path.GetDirectoryName(tbPath.Text) + Path.DirectorySeparatorChar;
                            var process = Process.Start(z7EXE, $"e -y -o\"{ outpudDir }\" \"{ tbPath.Text }\"");
                            process.WaitForExit();

                            if (process.ExitCode == 0)
                            {
                                var files = Directory.GetFiles(outpudDir, "rutracker-*.xml");
                                if (files.Any())
                                {
                                    tbPath.Text = files.First();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("7 zip не найден и не установлен!", "Распаковка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                UnLockInterface();
            }
        }

        private async void btGetCount_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbPath.Text))
            {
                LockInterface(true);

                lbTotalBlocks.Text = "<подсчёт ...>";
                cancellation = new CancellationTokenSource();
                int c = await DataReder.GetCount(tbPath.Text, cancellation);
                lbTotalBlocks.Text = c.ToString("0,0");
                UnLockInterface();
            }
        }
    }
}

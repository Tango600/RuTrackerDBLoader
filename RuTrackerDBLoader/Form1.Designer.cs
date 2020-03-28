namespace RuTrackerDBLoader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chNotDeleted = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.chShutdownPC = new System.Windows.Forms.CheckBox();
            this.chNoLoadFiles = new System.Windows.Forms.CheckBox();
            this.chUpdateMode = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbBadBlocks = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTotalBlocks = new System.Windows.Forms.Label();
            this.btGetCount = new System.Windows.Forms.Button();
            this.btUnpack = new System.Windows.Forms.Button();
            this.chNoContent = new System.Windows.Forms.CheckBox();
            this.chClearDB = new System.Windows.Forms.CheckBox();
            this.lbTime = new System.Windows.Forms.Label();
            this.chTestMode = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbProgress = new System.Windows.Forms.Label();
            this.btTestDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.btOpen = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btLoad = new System.Windows.Forms.Button();
            this.ofdXML = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chNotDeleted);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbSpeed);
            this.groupBox1.Controls.Add(this.chShutdownPC);
            this.groupBox1.Controls.Add(this.chNoLoadFiles);
            this.groupBox1.Controls.Add(this.chUpdateMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbBadBlocks);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbTotalBlocks);
            this.groupBox1.Controls.Add(this.btGetCount);
            this.groupBox1.Controls.Add(this.btUnpack);
            this.groupBox1.Controls.Add(this.chNoContent);
            this.groupBox1.Controls.Add(this.chClearDB);
            this.groupBox1.Controls.Add(this.lbTime);
            this.groupBox1.Controls.Add(this.chTestMode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbProgress);
            this.groupBox1.Controls.Add(this.btTestDB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbDatabase);
            this.groupBox1.Controls.Add(this.btOpen);
            this.groupBox1.Controls.Add(this.tbPath);
            this.groupBox1.Controls.Add(this.btLoad);
            this.groupBox1.Location = new System.Drawing.Point(10, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 264);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chNotDeleted
            // 
            this.chNotDeleted.AutoSize = true;
            this.chNotDeleted.Location = new System.Drawing.Point(106, 151);
            this.chNotDeleted.Name = "chNotDeleted";
            this.chNotDeleted.Size = new System.Drawing.Size(136, 17);
            this.chNotDeleted.TabIndex = 24;
            this.chNotDeleted.Text = "Только не удалённые";
            this.chNotDeleted.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Скорость:";
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSize = true;
            this.lbSpeed.Location = new System.Drawing.Point(279, 206);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(19, 13);
            this.lbSpeed.TabIndex = 22;
            this.lbSpeed.Text = "<>";
            // 
            // chShutdownPC
            // 
            this.chShutdownPC.AutoSize = true;
            this.chShutdownPC.Location = new System.Drawing.Point(10, 238);
            this.chShutdownPC.Name = "chShutdownPC";
            this.chShutdownPC.Size = new System.Drawing.Size(181, 17);
            this.chShutdownPC.TabIndex = 21;
            this.chShutdownPC.Text = "Выключить ПК по завершении";
            this.chShutdownPC.UseVisualStyleBackColor = true;
            // 
            // chNoLoadFiles
            // 
            this.chNoLoadFiles.AutoSize = true;
            this.chNoLoadFiles.Location = new System.Drawing.Point(271, 151);
            this.chNoLoadFiles.Name = "chNoLoadFiles";
            this.chNoLoadFiles.Size = new System.Drawing.Size(176, 17);
            this.chNoLoadFiles.TabIndex = 20;
            this.chNoLoadFiles.Text = "Не загружать список файлов";
            this.chNoLoadFiles.UseVisualStyleBackColor = true;
            // 
            // chUpdateMode
            // 
            this.chUpdateMode.AutoSize = true;
            this.chUpdateMode.Location = new System.Drawing.Point(106, 128);
            this.chUpdateMode.Name = "chUpdateMode";
            this.chUpdateMode.Size = new System.Drawing.Size(88, 17);
            this.chUpdateMode.TabIndex = 19;
            this.chUpdateMode.Text = "Обновление";
            this.chUpdateMode.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Всего блоков:";
            // 
            // lbBadBlocks
            // 
            this.lbBadBlocks.AutoSize = true;
            this.lbBadBlocks.Location = new System.Drawing.Point(80, 206);
            this.lbBadBlocks.Name = "lbBadBlocks";
            this.lbBadBlocks.Size = new System.Drawing.Size(19, 13);
            this.lbBadBlocks.TabIndex = 17;
            this.lbBadBlocks.Text = "<>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Ошибочных:";
            // 
            // lbTotalBlocks
            // 
            this.lbTotalBlocks.AutoSize = true;
            this.lbTotalBlocks.Location = new System.Drawing.Point(392, 180);
            this.lbTotalBlocks.Name = "lbTotalBlocks";
            this.lbTotalBlocks.Size = new System.Drawing.Size(19, 13);
            this.lbTotalBlocks.TabIndex = 15;
            this.lbTotalBlocks.Text = "<>";
            // 
            // btGetCount
            // 
            this.btGetCount.Location = new System.Drawing.Point(9, 130);
            this.btGetCount.Name = "btGetCount";
            this.btGetCount.Size = new System.Drawing.Size(75, 23);
            this.btGetCount.TabIndex = 14;
            this.btGetCount.Text = "Подсчитать";
            this.btGetCount.UseVisualStyleBackColor = true;
            this.btGetCount.Click += new System.EventHandler(this.btGetCount_Click);
            // 
            // btUnpack
            // 
            this.btUnpack.Location = new System.Drawing.Point(417, 73);
            this.btUnpack.Name = "btUnpack";
            this.btUnpack.Size = new System.Drawing.Size(57, 23);
            this.btUnpack.TabIndex = 13;
            this.btUnpack.Text = "unpack";
            this.btUnpack.UseVisualStyleBackColor = true;
            this.btUnpack.Click += new System.EventHandler(this.btUnpack_Click);
            // 
            // chNoContent
            // 
            this.chNoContent.AutoSize = true;
            this.chNoContent.Location = new System.Drawing.Point(271, 128);
            this.chNoContent.Name = "chNoContent";
            this.chNoContent.Size = new System.Drawing.Size(168, 17);
            this.chNoContent.TabIndex = 12;
            this.chNoContent.Text = "Не грузить содерж. форума";
            this.chNoContent.UseVisualStyleBackColor = true;
            // 
            // chClearDB
            // 
            this.chClearDB.AutoSize = true;
            this.chClearDB.Location = new System.Drawing.Point(271, 105);
            this.chClearDB.Name = "chClearDB";
            this.chClearDB.Size = new System.Drawing.Size(161, 17);
            this.chClearDB.TabIndex = 11;
            this.chClearDB.Text = "Очистить перед загрузкой";
            this.chClearDB.UseVisualStyleBackColor = true;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(185, 180);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(19, 13);
            this.lbTime.TabIndex = 10;
            this.lbTime.Text = "<>";
            // 
            // chTestMode
            // 
            this.chTestMode.AutoSize = true;
            this.chTestMode.Location = new System.Drawing.Point(106, 105);
            this.chTestMode.Name = "chTestMode";
            this.chTestMode.Size = new System.Drawing.Size(113, 17);
            this.chTestMode.TabIndex = 9;
            this.chTestMode.Text = "Тестовый режим";
            this.chTestMode.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Обработано:";
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(80, 180);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(19, 13);
            this.lbProgress.TabIndex = 7;
            this.lbProgress.Text = "<>";
            // 
            // btTestDB
            // 
            this.btTestDB.Location = new System.Drawing.Point(424, 30);
            this.btTestDB.Name = "btTestDB";
            this.btTestDB.Size = new System.Drawing.Size(50, 23);
            this.btTestDB.TabIndex = 6;
            this.btTestDB.Text = "Тест";
            this.btTestDB.UseVisualStyleBackColor = true;
            this.btTestDB.Click += new System.EventHandler(this.BtTestDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "XML или XZ файл";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Путь к БД (host:DBPath)";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(9, 32);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(409, 20);
            this.tbDatabase.TabIndex = 3;
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(384, 73);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(27, 23);
            this.btOpen.TabIndex = 2;
            this.btOpen.Text = "...";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.BtOpen_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(9, 75);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(369, 20);
            this.tbPath.TabIndex = 1;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(9, 101);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 0;
            this.btLoad.Text = "Загрузить";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.BtLoad_Click);
            // 
            // ofdXML
            // 
            this.ofdXML.DefaultExt = "xml";
            this.ofdXML.Filter = "XML|*.xml|Архивы|*.7z;*.xz;*.zip";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 268);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RuTracked DB Loader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.OpenFileDialog ofdXML;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btTestDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.CheckBox chTestMode;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.CheckBox chClearDB;
        private System.Windows.Forms.CheckBox chNoContent;
        private System.Windows.Forms.Button btUnpack;
        private System.Windows.Forms.Button btGetCount;
        private System.Windows.Forms.Label lbTotalBlocks;
        private System.Windows.Forms.Label lbBadBlocks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chUpdateMode;
        private System.Windows.Forms.CheckBox chNoLoadFiles;
        private System.Windows.Forms.CheckBox chShutdownPC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.CheckBox chNotDeleted;
    }
}


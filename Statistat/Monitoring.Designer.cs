namespace Statistat
{
    partial class Monitoring
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.swDisplay = new System.Windows.Forms.PictureBox();
            this.Switcher = new System.Windows.Forms.Button();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.UpdateTimeBox = new System.Windows.Forms.TextBox();
            this.clock = new System.Windows.Forms.Label();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.sBar = new System.Windows.Forms.StatusStrip();
            this.Message = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.finishLot = new System.Windows.Forms.TextBox();
            this.startLot = new System.Windows.Forms.TextBox();
            this.finishSwitch = new System.Windows.Forms.CheckBox();
            this.startSwitch = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.minRN = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.swDisplay)).BeginInit();
            this.sBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.swDisplay);
            this.groupBox1.Controls.Add(this.Switcher);
            this.groupBox1.Controls.Add(this.TimerLabel);
            this.groupBox1.Controls.Add(this.UpdateTimeBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 512);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры мониторинга";
            this.groupBox1.Visible = false;
            // 
            // swDisplay
            // 
            this.swDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.swDisplay.Image = global::Statistat.Properties.Resources.off;
            this.swDisplay.InitialImage = global::Statistat.Properties.Resources.off;
            this.swDisplay.Location = new System.Drawing.Point(140, 0);
            this.swDisplay.Name = "swDisplay";
            this.swDisplay.Size = new System.Drawing.Size(15, 15);
            this.swDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swDisplay.TabIndex = 5;
            this.swDisplay.TabStop = false;
            this.swDisplay.WaitOnLoad = true;
            // 
            // Switcher
            // 
            this.Switcher.Location = new System.Drawing.Point(49, 84);
            this.Switcher.Name = "Switcher";
            this.Switcher.Size = new System.Drawing.Size(66, 23);
            this.Switcher.TabIndex = 4;
            this.Switcher.Text = "Пуск";
            this.Switcher.UseVisualStyleBackColor = true;
            this.Switcher.Click += new System.EventHandler(this.Switcher_Click);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Location = new System.Drawing.Point(10, 26);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(132, 13);
            this.TimerLabel.TabIndex = 3;
            this.TimerLabel.Text = "Время обновления (мин)";
            // 
            // UpdateTimeBox
            // 
            this.UpdateTimeBox.Location = new System.Drawing.Point(49, 58);
            this.UpdateTimeBox.Name = "UpdateTimeBox";
            this.UpdateTimeBox.Size = new System.Drawing.Size(66, 20);
            this.UpdateTimeBox.TabIndex = 0;
            this.UpdateTimeBox.Text = "100";
            this.UpdateTimeBox.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // clock
            // 
            this.clock.AutoSize = true;
            this.clock.BackColor = System.Drawing.Color.Transparent;
            this.clock.Font = new System.Drawing.Font("Times New Roman", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clock.ForeColor = System.Drawing.Color.Maroon;
            this.clock.Location = new System.Drawing.Point(12, 9);
            this.clock.Name = "clock";
            this.clock.Size = new System.Drawing.Size(103, 38);
            this.clock.TabIndex = 1;
            this.clock.Text = "label1";
            // 
            // clockTimer
            // 
            this.clockTimer.Enabled = true;
            this.clockTimer.Interval = 200;
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            // 
            // sBar
            // 
            this.sBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Message});
            this.sBar.Location = new System.Drawing.Point(0, 553);
            this.sBar.Name = "sBar";
            this.sBar.Size = new System.Drawing.Size(945, 22);
            this.sBar.TabIndex = 2;
            // 
            // Message
            // 
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(0, 17);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "off.png");
            this.imgList.Images.SetKeyName(1, "On.png");
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(189, 250);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(744, 300);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.finishLot);
            this.groupBox2.Controls.Add(this.startLot);
            this.groupBox2.Controls.Add(this.finishSwitch);
            this.groupBox2.Controls.Add(this.startSwitch);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.minRN);
            this.groupBox2.Location = new System.Drawing.Point(12, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 178);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры отбора";
            // 
            // finishLot
            // 
            this.finishLot.Enabled = false;
            this.finishLot.Location = new System.Drawing.Point(13, 140);
            this.finishLot.Name = "finishLot";
            this.finishLot.Size = new System.Drawing.Size(74, 20);
            this.finishLot.TabIndex = 12;
            // 
            // startLot
            // 
            this.startLot.Enabled = false;
            this.startLot.Location = new System.Drawing.Point(13, 93);
            this.startLot.Name = "startLot";
            this.startLot.Size = new System.Drawing.Size(74, 20);
            this.startLot.TabIndex = 11;
            // 
            // finishSwitch
            // 
            this.finishSwitch.AutoSize = true;
            this.finishSwitch.Location = new System.Drawing.Point(13, 125);
            this.finishSwitch.Name = "finishSwitch";
            this.finishSwitch.Size = new System.Drawing.Size(78, 17);
            this.finishSwitch.TabIndex = 10;
            this.finishSwitch.Text = "Партия до";
            this.finishSwitch.UseVisualStyleBackColor = true;
            // 
            // startSwitch
            // 
            this.startSwitch.AutoSize = true;
            this.startSwitch.Location = new System.Drawing.Point(13, 78);
            this.startSwitch.Name = "startSwitch";
            this.startSwitch.Size = new System.Drawing.Size(77, 17);
            this.startSwitch.TabIndex = 9;
            this.startSwitch.Text = "Партия от";
            this.startSwitch.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Мин разр. нагр.";
            // 
            // minRN
            // 
            this.minRN.Location = new System.Drawing.Point(13, 40);
            this.minRN.Name = "minRN";
            this.minRN.Size = new System.Drawing.Size(74, 20);
            this.minRN.TabIndex = 0;
            this.minRN.Text = "200";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 39);
            this.button1.TabIndex = 6;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Monitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Statistat.Properties.Resources.StatimatLogo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(945, 575);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sBar);
            this.Controls.Add(this.clock);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Monitoring";
            this.Text = "Monitoring";
            this.Load += new System.EventHandler(this.Monitoring_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.swDisplay)).EndInit();
            this.sBar.ResumeLayout(false);
            this.sBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label clock;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.TextBox UpdateTimeBox;
        private System.Windows.Forms.StatusStrip sBar;
        private System.Windows.Forms.ToolStripStatusLabel Message;
        private System.Windows.Forms.PictureBox swDisplay;
        private System.Windows.Forms.Button Switcher;
        private System.Windows.Forms.Label TimerLabel;
        public System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minRN;
        private System.Windows.Forms.TextBox finishLot;
        private System.Windows.Forms.TextBox startLot;
        private System.Windows.Forms.CheckBox finishSwitch;
        private System.Windows.Forms.CheckBox startSwitch;
        private System.Windows.Forms.Button button1;
    }
}

namespace Cursovaya
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
            this.components = new System.ComponentModel.Container();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Stop = new System.Windows.Forms.Button();
            this.Step = new System.Windows.Forms.Button();
            this.switchOnInfo = new System.Windows.Forms.RadioButton();
            this.switchOnRealm = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(31, 32);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(726, 406);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseClick);
            this.picDisplay.MouseLeave += new System.EventHandler(this.picDisplay_MouseLeave);
            this.picDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(577, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(180, 202);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(224, 444);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(191, 56);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.scrollSpeedParticles_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(21, 445);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(93, 39);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Step
            // 
            this.Step.Location = new System.Drawing.Point(125, 445);
            this.Step.Name = "Step";
            this.Step.Size = new System.Drawing.Size(93, 39);
            this.Step.TabIndex = 4;
            this.Step.Text = "Step";
            this.Step.UseVisualStyleBackColor = true;
            this.Step.Click += new System.EventHandler(this.Step_Click);
            // 
            // switchOnInfo
            // 
            this.switchOnInfo.AutoSize = true;
            this.switchOnInfo.Checked = true;
            this.switchOnInfo.Location = new System.Drawing.Point(445, 454);
            this.switchOnInfo.Name = "switchOnInfo";
            this.switchOnInfo.Size = new System.Drawing.Size(88, 21);
            this.switchOnInfo.TabIndex = 5;
            this.switchOnInfo.TabStop = true;
            this.switchOnInfo.Text = "watchInfo";
            this.switchOnInfo.UseVisualStyleBackColor = true;
            this.switchOnInfo.CheckedChanged += new System.EventHandler(this.switchOnInfo_CheckedChanged);
            // 
            // switchOnRealm
            // 
            this.switchOnRealm.AutoSize = true;
            this.switchOnRealm.Location = new System.Drawing.Point(558, 454);
            this.switchOnRealm.Name = "switchOnRealm";
            this.switchOnRealm.Size = new System.Drawing.Size(105, 21);
            this.switchOnRealm.TabIndex = 6;
            this.switchOnRealm.Text = "watchRealm";
            this.switchOnRealm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(399, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 496);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.switchOnRealm);
            this.Controls.Add(this.switchOnInfo);
            this.Controls.Add(this.Step);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.picDisplay);
            this.MaximumSize = new System.Drawing.Size(826, 543);
            this.MinimumSize = new System.Drawing.Size(826, 543);
            this.Name = "Form1";
            this.Text = "Курсовая (частицы)";
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Step;
        private System.Windows.Forms.RadioButton switchOnInfo;
        private System.Windows.Forms.RadioButton switchOnRealm;
        private System.Windows.Forms.Label label1;
    }
}


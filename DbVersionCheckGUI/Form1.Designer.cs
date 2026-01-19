namespace DbVersionCheckGUI
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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.btnSessions = new System.Windows.Forms.Button();
            this.rbDisconnect = new System.Windows.Forms.RadioButton();
            this.rbConnect = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.SystemColors.Window;
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtResult.Location = new System.Drawing.Point(31, 12);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(596, 296);
            this.txtResult.TabIndex = 1;
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnGetVersion.Location = new System.Drawing.Point(202, 397);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(227, 47);
            this.btnGetVersion.TabIndex = 0;
            this.btnGetVersion.Text = "Узнать версию!";
            this.btnGetVersion.UseVisualStyleBackColor = false;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // btnSessions
            // 
            this.btnSessions.BackColor = System.Drawing.SystemColors.Control;
            this.btnSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSessions.Location = new System.Drawing.Point(202, 463);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(227, 74);
            this.btnSessions.TabIndex = 0;
            this.btnSessions.Text = "Узнать об открытых подключениях к БД";
            this.btnSessions.UseVisualStyleBackColor = false;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // rbDisconnect
            // 
            this.rbDisconnect.AutoSize = true;
            this.rbDisconnect.BackColor = System.Drawing.Color.DarkSalmon;
            this.rbDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbDisconnect.Location = new System.Drawing.Point(135, 21);
            this.rbDisconnect.Name = "rbDisconnect";
            this.rbDisconnect.Size = new System.Drawing.Size(141, 29);
            this.rbDisconnect.TabIndex = 2;
            this.rbDisconnect.TabStop = true;
            this.rbDisconnect.Text = "Отключено";
            this.rbDisconnect.UseVisualStyleBackColor = false;
            // 
            // rbConnect
            // 
            this.rbConnect.AutoSize = true;
            this.rbConnect.BackColor = System.Drawing.Color.GreenYellow;
            this.rbConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbConnect.Location = new System.Drawing.Point(303, 21);
            this.rbConnect.Name = "rbConnect";
            this.rbConnect.Size = new System.Drawing.Size(151, 29);
            this.rbConnect.TabIndex = 2;
            this.rbConnect.TabStop = true;
            this.rbConnect.Text = "Подключено";
            this.rbConnect.UseVisualStyleBackColor = false;
            this.rbConnect.CheckedChanged += new System.EventHandler(this.rbConnect_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDisconnect);
            this.groupBox1.Controls.Add(this.rbConnect);
            this.groupBox1.Location = new System.Drawing.Point(31, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 63);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 560);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.btnGetVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Получаем версию БД быстро и просто!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosed);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.Button btnSessions;
        private System.Windows.Forms.RadioButton rbDisconnect;
        private System.Windows.Forms.RadioButton rbConnect;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSessions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.btnConnect.Location = new System.Drawing.Point(34, 58);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(196, 47);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.SystemColors.Window;
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtResult.Location = new System.Drawing.Point(293, 58);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(506, 332);
            this.txtResult.TabIndex = 1;
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.btnGetVersion.Location = new System.Drawing.Point(34, 141);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(196, 47);
            this.btnGetVersion.TabIndex = 0;
            this.btnGetVersion.Text = "Get Version";
            this.btnGetVersion.UseVisualStyleBackColor = false;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.btnDisconnect.Location = new System.Drawing.Point(34, 226);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(196, 47);
            this.btnDisconnect.TabIndex = 0;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnSessions
            // 
            this.btnSessions.BackColor = System.Drawing.SystemColors.Control;
            this.btnSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.btnSessions.Location = new System.Drawing.Point(34, 343);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(196, 47);
            this.btnSessions.TabIndex = 0;
            this.btnSessions.Text = "GetActiveSessions";
            this.btnSessions.UseVisualStyleBackColor = false;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 438);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnGetVersion);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Get Version Prototype";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSessions;
    }
}


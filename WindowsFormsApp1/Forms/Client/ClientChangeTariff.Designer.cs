
namespace WindowsFormsApp1.Forms.Client
{
    partial class ClientChangeTariff
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
            this.listBox1PhoneNumber = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCurremtTariffInfo = new System.Windows.Forms.Label();
            this.listBox2Tarif = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1PhoneNumber
            // 
            this.listBox1PhoneNumber.FormattingEnabled = true;
            this.listBox1PhoneNumber.Location = new System.Drawing.Point(15, 43);
            this.listBox1PhoneNumber.Name = "listBox1PhoneNumber";
            this.listBox1PhoneNumber.Size = new System.Drawing.Size(161, 69);
            this.listBox1PhoneNumber.TabIndex = 0;
            this.listBox1PhoneNumber.SelectedIndexChanged += new System.EventHandler(this.listBox1PhoneNumber_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер телефона:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelCurremtTariffInfo
            // 
            this.labelCurremtTariffInfo.AutoSize = true;
            this.labelCurremtTariffInfo.Location = new System.Drawing.Point(209, 43);
            this.labelCurremtTariffInfo.Name = "labelCurremtTariffInfo";
            this.labelCurremtTariffInfo.Size = new System.Drawing.Size(0, 13);
            this.labelCurremtTariffInfo.TabIndex = 3;
            // 
            // listBox2Tarif
            // 
            this.listBox2Tarif.FormattingEnabled = true;
            this.listBox2Tarif.Location = new System.Drawing.Point(15, 182);
            this.listBox2Tarif.Name = "listBox2Tarif";
            this.listBox2Tarif.Size = new System.Drawing.Size(161, 69);
            this.listBox2Tarif.TabIndex = 4;
            this.listBox2Tarif.SelectedIndexChanged += new System.EventHandler(this.listBox2Tarif_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 37);
            this.button2.TabIndex = 5;
            this.button2.Text = "Изменить тариф";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Доступные тарифы:";
            // 
            // ClientChangeTariff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox2Tarif);
            this.Controls.Add(this.labelCurremtTariffInfo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1PhoneNumber);
            this.Name = "ClientChangeTariff";
            this.Text = "ClientChangeTariff";
            this.Load += new System.EventHandler(this.ClientChangeTariff_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1PhoneNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelCurremtTariffInfo;
        private System.Windows.Forms.ListBox listBox2Tarif;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
    }
}
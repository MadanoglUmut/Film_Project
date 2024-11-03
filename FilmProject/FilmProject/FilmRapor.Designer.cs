namespace _220601002_donemOdevi
{
    partial class FilmRapor
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.puanYkskBtn = new System.Windows.Forms.Button();
            this.puanDskBtn = new System.Windows.Forms.Button();
            this.degerlendirilenBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(325, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(631, 240);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(366, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "DEĞERLENDİRME PUANINA GÖRE SIRALANMIŞ FİLMLER";
            // 
            // puanYkskBtn
            // 
            this.puanYkskBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.puanYkskBtn.Location = new System.Drawing.Point(301, 378);
            this.puanYkskBtn.Name = "puanYkskBtn";
            this.puanYkskBtn.Size = new System.Drawing.Size(196, 36);
            this.puanYkskBtn.TabIndex = 2;
            this.puanYkskBtn.Text = "Puanı Yüksek Filmler";
            this.puanYkskBtn.UseVisualStyleBackColor = true;
            this.puanYkskBtn.Click += new System.EventHandler(this.puanYkskBtn_Click);
            // 
            // puanDskBtn
            // 
            this.puanDskBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.puanDskBtn.Location = new System.Drawing.Point(503, 378);
            this.puanDskBtn.Name = "puanDskBtn";
            this.puanDskBtn.Size = new System.Drawing.Size(196, 36);
            this.puanDskBtn.TabIndex = 3;
            this.puanDskBtn.Text = "Puanı Düşük Filmler";
            this.puanDskBtn.UseVisualStyleBackColor = true;
            this.puanDskBtn.Click += new System.EventHandler(this.puanDskBtn_Click);
            // 
            // degerlendirilenBtn
            // 
            this.degerlendirilenBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.degerlendirilenBtn.Location = new System.Drawing.Point(705, 378);
            this.degerlendirilenBtn.Name = "degerlendirilenBtn";
            this.degerlendirilenBtn.Size = new System.Drawing.Size(281, 36);
            this.degerlendirilenBtn.TabIndex = 4;
            this.degerlendirilenBtn.Text = "En Çok Değerlendirilen Filmler";
            this.degerlendirilenBtn.UseVisualStyleBackColor = true;
            this.degerlendirilenBtn.Click += new System.EventHandler(this.degerlendirilenBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(32, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 330);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FilmRapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(1076, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.degerlendirilenBtn);
            this.Controls.Add(this.puanDskBtn);
            this.Controls.Add(this.puanYkskBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FilmRapor";
            this.Text = "FilmRapor";
            this.Load += new System.EventHandler(this.FilmRapor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button puanYkskBtn;
        private System.Windows.Forms.Button puanDskBtn;
        private System.Windows.Forms.Button degerlendirilenBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
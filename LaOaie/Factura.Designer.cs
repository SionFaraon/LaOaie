namespace LaOaie
{
    partial class Factura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Factura));
            this.pictureBoxFactura = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFactura
            // 
            this.pictureBoxFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(37)))), ((int)(((byte)(42)))));
            this.pictureBoxFactura.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxFactura.Name = "pictureBoxFactura";
            this.pictureBoxFactura.Size = new System.Drawing.Size(750, 739);
            this.pictureBoxFactura.TabIndex = 0;
            this.pictureBoxFactura.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(37)))), ((int)(((byte)(42)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(193)))));
            this.buttonSave.Location = new System.Drawing.Point(12, 757);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(210, 74);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(37)))), ((int)(((byte)(42)))));
            this.buttonPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(193)))));
            this.buttonPrint.Location = new System.Drawing.Point(552, 757);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(210, 74);
            this.buttonPrint.TabIndex = 5;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // Factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(774, 858);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.pictureBoxFactura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Factura";
            this.Text = "Factura";
            this.Load += new System.EventHandler(this.Factura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFactura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFactura;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonPrint;
    }
}
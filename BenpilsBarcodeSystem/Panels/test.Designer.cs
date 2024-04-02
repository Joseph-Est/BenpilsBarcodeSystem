namespace BenpilsBarcodeSystem.Panels
{
    partial class test
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
            this.ProjectedProfitLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProjectedProfitLbl
            // 
            this.ProjectedProfitLbl.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectedProfitLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ProjectedProfitLbl.Location = new System.Drawing.Point(20, 18);
            this.ProjectedProfitLbl.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ProjectedProfitLbl.Name = "ProjectedProfitLbl";
            this.ProjectedProfitLbl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.ProjectedProfitLbl.Size = new System.Drawing.Size(214, 30);
            this.ProjectedProfitLbl.TabIndex = 10;
            this.ProjectedProfitLbl.Text = "₱ 0.00";
            this.ProjectedProfitLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label6.Location = new System.Drawing.Point(20, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(214, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Projected Profit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 615);
            this.Controls.Add(this.ProjectedProfitLbl);
            this.Controls.Add(this.label6);
            this.Name = "test";
            this.Text = "test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ProjectedProfitLbl;
        private System.Windows.Forms.Label label6;
    }
}
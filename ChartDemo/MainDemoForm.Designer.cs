namespace ChartDemo
{
    partial class MainDemoForm
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
            this.ucSuperSimpleChart1 = new ChartDemo.ucSuperSimpleChart();
            this.ucMultipleGraphsAndAxes1 = new ChartDemo.ucMultipleGraphsAndAxes();
            this.ucSimpleProgressChart1 = new ChartDemo.ucSimpleProgressChart();
            this.SuspendLayout();
            // 
            // ucSuperSimpleChart1
            // 
            this.ucSuperSimpleChart1.Location = new System.Drawing.Point(2, 2);
            this.ucSuperSimpleChart1.Name = "ucSuperSimpleChart1";
            this.ucSuperSimpleChart1.Size = new System.Drawing.Size(1047, 503);
            this.ucSuperSimpleChart1.TabIndex = 0;
            // 
            // ucMultipleGraphsAndAxes1
            // 
            this.ucMultipleGraphsAndAxes1.Location = new System.Drawing.Point(2, 536);
            this.ucMultipleGraphsAndAxes1.Name = "ucMultipleGraphsAndAxes1";
            this.ucMultipleGraphsAndAxes1.Size = new System.Drawing.Size(1000, 600);
            this.ucMultipleGraphsAndAxes1.TabIndex = 1;
            // 
            // ucSimpleProgressChart1
            // 
            this.ucSimpleProgressChart1.Location = new System.Drawing.Point(907, 2);
            this.ucSimpleProgressChart1.Name = "ucSimpleProgressChart1";
            this.ucSimpleProgressChart1.Size = new System.Drawing.Size(1000, 226);
            this.ucSimpleProgressChart1.TabIndex = 2;
            // 
            // MainDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1302, 421);
            this.Controls.Add(this.ucSimpleProgressChart1);
            this.Controls.Add(this.ucMultipleGraphsAndAxes1);
            this.Controls.Add(this.ucSuperSimpleChart1);
            this.Name = "MainDemoForm";
            this.Text = "Demo Simple2DCharts";
            this.ResumeLayout(false);

        }

        #endregion

        private ucSuperSimpleChart ucSuperSimpleChart1;
        private ucMultipleGraphsAndAxes ucMultipleGraphsAndAxes1;
        private ucSimpleProgressChart ucSimpleProgressChart1;

    }
}


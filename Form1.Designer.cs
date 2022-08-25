namespace Esuli.NBodyDemo
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nBodyCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nBodySeed = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownGravity = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFPS = new System.Windows.Forms.TextBox();
            this.radioButtonSingleThreaded = new System.Windows.Forms.RadioButton();
            this.radioButtonMultiThreaded = new System.Windows.Forms.RadioButton();
            this.radioButtonOpenCL = new System.Windows.Forms.RadioButton();
            this.checkBoxDraw = new System.Windows.Forms.CheckBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBodyCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBodySeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox);
            this.splitContainer1.Size = new System.Drawing.Size(708, 424);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonStartStop);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.nBodyCount);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.nBodySeed);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDownScale);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDownGravity);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.textBoxFPS);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonSingleThreaded);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonMultiThreaded);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonOpenCL);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDraw);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 420);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonStartStop
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.buttonStartStop, true);
            this.buttonStartStop.Location = new System.Drawing.Point(3, 3);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(120, 23);
            this.buttonStartStop.TabIndex = 0;
            this.buttonStartStop.Text = "Start-Stop";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "# particles:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nBodyCount
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.nBodyCount, true);
            this.nBodyCount.Location = new System.Drawing.Point(68, 32);
            this.nBodyCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nBodyCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nBodyCount.Name = "nBodyCount";
            this.nBodyCount.Size = new System.Drawing.Size(76, 20);
            this.nBodyCount.TabIndex = 1;
            this.nBodyCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nBodyCount.ValueChanged += new System.EventHandler(this.nBodyCount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "seed:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nBodySeed
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.nBodySeed, true);
            this.nBodySeed.Location = new System.Drawing.Point(42, 58);
            this.nBodySeed.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.nBodySeed.Minimum = new decimal(new int[] {
            2000000000,
            0,
            0,
            -2147483648});
            this.nBodySeed.Name = "nBodySeed";
            this.nBodySeed.Size = new System.Drawing.Size(76, 20);
            this.nBodySeed.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "gravity factor:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownGravity
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numericUpDownGravity, true);
            this.numericUpDownGravity.Location = new System.Drawing.Point(80, 110);
            this.numericUpDownGravity.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownGravity.Name = "numericUpDownGravity";
            this.numericUpDownGravity.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownGravity.TabIndex = 12;
            this.numericUpDownGravity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGravity.ValueChanged += new System.EventHandler(this.numericUpDownGravity_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "FPS:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFPS
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.textBoxFPS, true);
            this.textBoxFPS.Location = new System.Drawing.Point(39, 136);
            this.textBoxFPS.Name = "textBoxFPS";
            this.textBoxFPS.ReadOnly = true;
            this.textBoxFPS.Size = new System.Drawing.Size(76, 20);
            this.textBoxFPS.TabIndex = 3;
            // 
            // radioButtonSingleThreaded
            // 
            this.radioButtonSingleThreaded.AutoSize = true;
            this.radioButtonSingleThreaded.Checked = true;
            this.radioButtonSingleThreaded.Location = new System.Drawing.Point(3, 162);
            this.radioButtonSingleThreaded.Name = "radioButtonSingleThreaded";
            this.radioButtonSingleThreaded.Size = new System.Drawing.Size(97, 17);
            this.radioButtonSingleThreaded.TabIndex = 4;
            this.radioButtonSingleThreaded.TabStop = true;
            this.radioButtonSingleThreaded.Text = "single threaded";
            this.radioButtonSingleThreaded.UseVisualStyleBackColor = true;
            this.radioButtonSingleThreaded.CheckedChanged += new System.EventHandler(this.radioButtonSingleThreaded_CheckedChanged);
            // 
            // radioButtonMultiThreaded
            // 
            this.radioButtonMultiThreaded.AutoSize = true;
            this.radioButtonMultiThreaded.Location = new System.Drawing.Point(3, 185);
            this.radioButtonMultiThreaded.Name = "radioButtonMultiThreaded";
            this.radioButtonMultiThreaded.Size = new System.Drawing.Size(91, 17);
            this.radioButtonMultiThreaded.TabIndex = 5;
            this.radioButtonMultiThreaded.TabStop = true;
            this.radioButtonMultiThreaded.Text = "multi threaded";
            this.radioButtonMultiThreaded.UseVisualStyleBackColor = true;
            this.radioButtonMultiThreaded.CheckedChanged += new System.EventHandler(this.radioButtonThreadPool_CheckedChanged);
            // 
            // radioButtonOpenCL
            // 
            this.radioButtonOpenCL.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.radioButtonOpenCL, true);
            this.radioButtonOpenCL.Location = new System.Drawing.Point(3, 208);
            this.radioButtonOpenCL.Name = "radioButtonOpenCL";
            this.radioButtonOpenCL.Size = new System.Drawing.Size(62, 17);
            this.radioButtonOpenCL.TabIndex = 6;
            this.radioButtonOpenCL.TabStop = true;
            this.radioButtonOpenCL.Text = "openCL";
            this.radioButtonOpenCL.UseVisualStyleBackColor = true;
            this.radioButtonOpenCL.CheckedChanged += new System.EventHandler(this.radioButtonOpenCL_CheckedChanged);
            // 
            // checkBoxDraw
            // 
            this.checkBoxDraw.AutoSize = true;
            this.checkBoxDraw.Checked = true;
            this.checkBoxDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.flowLayoutPanel1.SetFlowBreak(this.checkBoxDraw, true);
            this.checkBoxDraw.Location = new System.Drawing.Point(3, 231);
            this.checkBoxDraw.Name = "checkBoxDraw";
            this.checkBoxDraw.Size = new System.Drawing.Size(91, 17);
            this.checkBoxDraw.TabIndex = 8;
            this.checkBoxDraw.Text = "draw particles";
            this.checkBoxDraw.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(534, 420);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "scale factor:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownScale
            // 
            this.numericUpDownScale.DecimalPlaces = 1;
            this.flowLayoutPanel1.SetFlowBreak(this.numericUpDownScale, true);
            this.numericUpDownScale.Location = new System.Drawing.Point(74, 84);
            this.numericUpDownScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownScale.Name = "numericUpDownScale";
            this.numericUpDownScale.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownScale.TabIndex = 14;
            this.numericUpDownScale.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownScale.ValueChanged += new System.EventHandler(this.numericUpDownScale_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 424);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Bouncing N-Body Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBodyCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBodySeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.NumericUpDown nBodyCount;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown nBodySeed;
        private System.Windows.Forms.TextBox textBoxFPS;
        private System.Windows.Forms.RadioButton radioButtonSingleThreaded;
        private System.Windows.Forms.RadioButton radioButtonMultiThreaded;
        private System.Windows.Forms.RadioButton radioButtonOpenCL;
        private System.Windows.Forms.CheckBox checkBoxDraw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownGravity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
    }
}


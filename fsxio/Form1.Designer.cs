namespace Managed_Data_Request
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
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.richResponse = new System.Windows.Forms.RichTextBox();
            this.labelOmegaX = new System.Windows.Forms.Label();
            this.labelOmegaY = new System.Windows.Forms.Label();
            this.labelOmegaZ = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonUDP = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelAX = new System.Windows.Forms.Label();
            this.checkBoxAil = new System.Windows.Forms.CheckBox();
            this.checkBoxEle = new System.Windows.Forms.CheckBox();
            this.checkBoxThrottle = new System.Windows.Forms.CheckBox();
            this.checkBoxRudder = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownNoise = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownBiasDrift = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownBiasLimit = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelRxFreq = new System.Windows.Forms.Label();
            this.labelAY = new System.Windows.Forms.Label();
            this.labelAZ = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBiasDrift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBiasLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(27, 13);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(110, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect to FSX";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(27, 42);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(110, 23);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // richResponse
            // 
            this.richResponse.Location = new System.Drawing.Point(18, 206);
            this.richResponse.Name = "richResponse";
            this.richResponse.Size = new System.Drawing.Size(284, 153);
            this.richResponse.TabIndex = 3;
            this.richResponse.Text = "";
            // 
            // labelOmegaX
            // 
            this.labelOmegaX.AutoSize = true;
            this.labelOmegaX.Location = new System.Drawing.Point(183, 37);
            this.labelOmegaX.Name = "labelOmegaX";
            this.labelOmegaX.Size = new System.Drawing.Size(13, 13);
            this.labelOmegaX.TabIndex = 4;
            this.labelOmegaX.Text = "0";
            // 
            // labelOmegaY
            // 
            this.labelOmegaY.AutoSize = true;
            this.labelOmegaY.Location = new System.Drawing.Point(183, 61);
            this.labelOmegaY.Name = "labelOmegaY";
            this.labelOmegaY.Size = new System.Drawing.Size(13, 13);
            this.labelOmegaY.TabIndex = 5;
            this.labelOmegaY.Text = "0";
            // 
            // labelOmegaZ
            // 
            this.labelOmegaZ.AutoSize = true;
            this.labelOmegaZ.Location = new System.Drawing.Point(183, 85);
            this.labelOmegaZ.Name = "labelOmegaZ";
            this.labelOmegaZ.Size = new System.Drawing.Size(13, 13);
            this.labelOmegaZ.TabIndex = 6;
            this.labelOmegaZ.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonUDP
            // 
            this.buttonUDP.Location = new System.Drawing.Point(191, 365);
            this.buttonUDP.Name = "buttonUDP";
            this.buttonUDP.Size = new System.Drawing.Size(120, 23);
            this.buttonUDP.TabIndex = 7;
            this.buttonUDP.Text = "Send to IP:Port";
            this.buttonUDP.UseVisualStyleBackColor = true;
            this.buttonUDP.Click += new System.EventHandler(this.buttonUDP_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 367);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "192.168.1.5:49003";
            // 
            // labelAX
            // 
            this.labelAX.AutoSize = true;
            this.labelAX.Location = new System.Drawing.Point(183, 134);
            this.labelAX.Name = "labelAX";
            this.labelAX.Size = new System.Drawing.Size(13, 13);
            this.labelAX.TabIndex = 9;
            this.labelAX.Text = "0";
            // 
            // checkBoxAil
            // 
            this.checkBoxAil.AutoSize = true;
            this.checkBoxAil.Checked = true;
            this.checkBoxAil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAil.Location = new System.Drawing.Point(18, 414);
            this.checkBoxAil.Name = "checkBoxAil";
            this.checkBoxAil.Size = new System.Drawing.Size(100, 17);
            this.checkBoxAil.TabIndex = 10;
            this.checkBoxAil.Text = "Reverse aileron";
            this.checkBoxAil.UseVisualStyleBackColor = true;
            // 
            // checkBoxEle
            // 
            this.checkBoxEle.AutoSize = true;
            this.checkBoxEle.Checked = true;
            this.checkBoxEle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEle.Location = new System.Drawing.Point(18, 437);
            this.checkBoxEle.Name = "checkBoxEle";
            this.checkBoxEle.Size = new System.Drawing.Size(107, 17);
            this.checkBoxEle.TabIndex = 11;
            this.checkBoxEle.Text = "Reverse elevator";
            this.checkBoxEle.UseVisualStyleBackColor = true;
            // 
            // checkBoxThrottle
            // 
            this.checkBoxThrottle.AutoSize = true;
            this.checkBoxThrottle.Location = new System.Drawing.Point(18, 461);
            this.checkBoxThrottle.Name = "checkBoxThrottle";
            this.checkBoxThrottle.Size = new System.Drawing.Size(101, 17);
            this.checkBoxThrottle.TabIndex = 12;
            this.checkBoxThrottle.Text = "Reverse throttle";
            this.checkBoxThrottle.UseVisualStyleBackColor = true;
            // 
            // checkBoxRudder
            // 
            this.checkBoxRudder.AutoSize = true;
            this.checkBoxRudder.Checked = true;
            this.checkBoxRudder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRudder.Location = new System.Drawing.Point(18, 485);
            this.checkBoxRudder.Name = "checkBoxRudder";
            this.checkBoxRudder.Size = new System.Drawing.Size(99, 17);
            this.checkBoxRudder.TabIndex = 13;
            this.checkBoxRudder.Text = "Reverse rudder";
            this.checkBoxRudder.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Gyro rates (rad/sec; XYZ)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Body-frame accelerations (m/s/s)";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(27, 100);
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(110, 20);
            this.numericUpDownInterval.TabIndex = 16;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownInterval.ValueChanged += new System.EventHandler(this.numericUpDownHandler);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Data send interval (ms)";
            // 
            // numericUpDownNoise
            // 
            this.numericUpDownNoise.DecimalPlaces = 4;
            this.numericUpDownNoise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownNoise.Location = new System.Drawing.Point(376, 35);
            this.numericUpDownNoise.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNoise.Name = "numericUpDownNoise";
            this.numericUpDownNoise.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownNoise.TabIndex = 18;
            this.numericUpDownNoise.Value = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.numericUpDownNoise.ValueChanged += new System.EventHandler(this.numericUpDownHandler);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(376, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Gyro noise (rads/sec, 1sd)";
            // 
            // numericUpDownBiasDrift
            // 
            this.numericUpDownBiasDrift.DecimalPlaces = 4;
            this.numericUpDownBiasDrift.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownBiasDrift.Location = new System.Drawing.Point(376, 107);
            this.numericUpDownBiasDrift.Name = "numericUpDownBiasDrift";
            this.numericUpDownBiasDrift.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBiasDrift.TabIndex = 20;
            this.numericUpDownBiasDrift.Value = new decimal(new int[] {
            35,
            0,
            0,
            196608});
            this.numericUpDownBiasDrift.ValueChanged += new System.EventHandler(this.numericUpDownHandler);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(376, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 30);
            this.label8.TabIndex = 21;
            this.label8.Text = "Gyro bias instability (rads/min, 1sd)";
            // 
            // numericUpDownBiasLimit
            // 
            this.numericUpDownBiasLimit.DecimalPlaces = 4;
            this.numericUpDownBiasLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownBiasLimit.Location = new System.Drawing.Point(376, 176);
            this.numericUpDownBiasLimit.Name = "numericUpDownBiasLimit";
            this.numericUpDownBiasLimit.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBiasLimit.TabIndex = 22;
            this.numericUpDownBiasLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numericUpDownBiasLimit.ValueChanged += new System.EventHandler(this.numericUpDownHandler);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(376, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 30);
            this.label9.TabIndex = 23;
            this.label9.Text = "Gyro bias limit (rads/sec)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Receive frequency:";
            // 
            // labelRxFreq
            // 
            this.labelRxFreq.AutoSize = true;
            this.labelRxFreq.Location = new System.Drawing.Point(124, 143);
            this.labelRxFreq.Name = "labelRxFreq";
            this.labelRxFreq.Size = new System.Drawing.Size(13, 13);
            this.labelRxFreq.TabIndex = 25;
            this.labelRxFreq.Text = "0";
            // 
            // labelAY
            // 
            this.labelAY.AutoSize = true;
            this.labelAY.Location = new System.Drawing.Point(183, 160);
            this.labelAY.Name = "labelAY";
            this.labelAY.Size = new System.Drawing.Size(13, 13);
            this.labelAY.TabIndex = 26;
            this.labelAY.Text = "0";
            // 
            // labelAZ
            // 
            this.labelAZ.AutoSize = true;
            this.labelAZ.Location = new System.Drawing.Point(183, 183);
            this.labelAZ.Name = "labelAZ";
            this.labelAZ.Size = new System.Drawing.Size(13, 13);
            this.labelAZ.TabIndex = 27;
            this.labelAZ.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 514);
            this.Controls.Add(this.labelAZ);
            this.Controls.Add(this.labelAY);
            this.Controls.Add(this.labelRxFreq);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDownBiasLimit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownBiasDrift);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownNoise);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxRudder);
            this.Controls.Add(this.checkBoxThrottle);
            this.Controls.Add(this.checkBoxEle);
            this.Controls.Add(this.checkBoxAil);
            this.Controls.Add(this.labelAX);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonUDP);
            this.Controls.Add(this.labelOmegaZ);
            this.Controls.Add(this.labelOmegaY);
            this.Controls.Add(this.labelOmegaX);
            this.Controls.Add(this.richResponse);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "FSXIO";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBiasDrift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBiasLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.RichTextBox richResponse;
        private System.Windows.Forms.Label labelOmegaX;
        private System.Windows.Forms.Label labelOmegaY;
        private System.Windows.Forms.Label labelOmegaZ;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonUDP;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelAX;
        private System.Windows.Forms.CheckBox checkBoxAil;
        private System.Windows.Forms.CheckBox checkBoxEle;
        private System.Windows.Forms.CheckBox checkBoxThrottle;
        private System.Windows.Forms.CheckBox checkBoxRudder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownNoise;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownBiasDrift;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownBiasLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelRxFreq;
        private System.Windows.Forms.Label labelAY;
        private System.Windows.Forms.Label labelAZ;
    }
}


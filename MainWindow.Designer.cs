using System;

namespace ColorPicker
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bVal = new System.Windows.Forms.TrackBar();
            this.gVal = new System.Windows.Forms.TrackBar();
            this.rVal = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.profileSelector = new System.Windows.Forms.ComboBox();
            this.redValue = new System.Windows.Forms.Label();
            this.greenValue = new System.Windows.Forms.Label();
            this.blueValue = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "R";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "G";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "B";
            // 
            // bVal
            // 
            this.bVal.Location = new System.Drawing.Point(134, 72);
            this.bVal.Maximum = 255;
            this.bVal.Name = "bVal";
            this.bVal.Size = new System.Drawing.Size(315, 45);
            this.bVal.TabIndex = 9;
            this.bVal.Text = "0";
            this.bVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.bVal.ValueChanged += new System.EventHandler(this.rgbValChanged);
            // 
            // gVal
            // 
            this.gVal.Location = new System.Drawing.Point(134, 44);
            this.gVal.Maximum = 255;
            this.gVal.Name = "gVal";
            this.gVal.Size = new System.Drawing.Size(315, 45);
            this.gVal.TabIndex = 8;
            this.gVal.Text = "0";
            this.gVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.gVal.ValueChanged += new System.EventHandler(this.rgbValChanged);
            // 
            // rVal
            // 
            this.rVal.Location = new System.Drawing.Point(134, 15);
            this.rVal.Maximum = 255;
            this.rVal.Name = "rVal";
            this.rVal.Size = new System.Drawing.Size(315, 45);
            this.rVal.TabIndex = 7;
            this.rVal.Text = "0";
            this.rVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.rVal.ValueChanged += new System.EventHandler(this.rgbValChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(12, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(95, 95);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // profileSelector
            // 
            this.profileSelector.FormattingEnabled = true;
            this.profileSelector.Location = new System.Drawing.Point(12, 112);
            this.profileSelector.Name = "profileSelector";
            this.profileSelector.Size = new System.Drawing.Size(95, 21);
            this.profileSelector.TabIndex = 11;
            this.profileSelector.Text = "Profile 1";
            // 
            // redValue
            // 
            this.redValue.AutoSize = true;
            this.redValue.Location = new System.Drawing.Point(455, 18);
            this.redValue.Name = "redValue";
            this.redValue.Size = new System.Drawing.Size(13, 13);
            this.redValue.TabIndex = 13;
            this.redValue.Text = "0";
            // 
            // greenValue
            // 
            this.greenValue.AutoSize = true;
            this.greenValue.Location = new System.Drawing.Point(455, 47);
            this.greenValue.Name = "greenValue";
            this.greenValue.Size = new System.Drawing.Size(13, 13);
            this.greenValue.TabIndex = 14;
            this.greenValue.Text = "0";
            // 
            // blueValue
            // 
            this.blueValue.AutoSize = true;
            this.blueValue.Location = new System.Drawing.Point(455, 76);
            this.blueValue.Name = "blueValue";
            this.blueValue.Size = new System.Drawing.Size(13, 13);
            this.blueValue.TabIndex = 15;
            this.blueValue.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Save Profile";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Reset Profile";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(134, 128);
            this.trackBar1.Maximum = 4;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(169, 45);
            this.trackBar1.TabIndex = 18;
            this.trackBar1.Value = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Brightness";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(209, 168);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "gamer";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 200);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.blueValue);
            this.Controls.Add(this.greenValue);
            this.Controls.Add(this.redValue);
            this.Controls.Add(this.profileSelector);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.bVal);
            this.Controls.Add(this.gVal);
            this.Controls.Add(this.rVal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Tongfang RGB Keyboard";
            ((System.ComponentModel.ISupportInitialize)(this.bVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar bVal;
        private System.Windows.Forms.TrackBar gVal;
        private System.Windows.Forms.TrackBar rVal;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox profileSelector;
        private System.Windows.Forms.Label redValue;
        private System.Windows.Forms.Label greenValue;
        private System.Windows.Forms.Label blueValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
    }
}


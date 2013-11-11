namespace FindingCommunicationRoutes
{
    partial class CommunicationRoutesGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationRoutesGui));
            this.upperMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startBusStopComboBox = new System.Windows.Forms.ComboBox();
            this.destinationBusStopComboBox = new System.Windows.Forms.ComboBox();
            this.directConnectionsCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.typeOfDayComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hourTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minuteTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.departureRadioButton = new System.Windows.Forms.RadioButton();
            this.arrivalRadioButton = new System.Windows.Forms.RadioButton();
            this.searchButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.upperMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hourTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteTimeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // upperMenuStrip
            // 
            this.upperMenuStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.upperMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.upperMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.upperMenuStrip.Name = "upperMenuStrip";
            this.upperMenuStrip.Size = new System.Drawing.Size(481, 24);
            this.upperMenuStrip.TabIndex = 1;
            this.upperMenuStrip.Text = "upperMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To:";
            // 
            // startBusStopComboBox
            // 
            this.startBusStopComboBox.FormattingEnabled = true;
            this.startBusStopComboBox.Location = new System.Drawing.Point(90, 43);
            this.startBusStopComboBox.Name = "startBusStopComboBox";
            this.startBusStopComboBox.Size = new System.Drawing.Size(121, 21);
            this.startBusStopComboBox.TabIndex = 4;
            // 
            // destinationBusStopComboBox
            // 
            this.destinationBusStopComboBox.FormattingEnabled = true;
            this.destinationBusStopComboBox.Location = new System.Drawing.Point(90, 73);
            this.destinationBusStopComboBox.Name = "destinationBusStopComboBox";
            this.destinationBusStopComboBox.Size = new System.Drawing.Size(121, 21);
            this.destinationBusStopComboBox.TabIndex = 5;
            // 
            // directConnectionsCheckBox
            // 
            this.directConnectionsCheckBox.AutoSize = true;
            this.directConnectionsCheckBox.Checked = true;
            this.directConnectionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.directConnectionsCheckBox.Enabled = false;
            this.directConnectionsCheckBox.Location = new System.Drawing.Point(12, 199);
            this.directConnectionsCheckBox.Name = "directConnectionsCheckBox";
            this.directConnectionsCheckBox.Size = new System.Drawing.Size(137, 17);
            this.directConnectionsCheckBox.TabIndex = 6;
            this.directConnectionsCheckBox.Text = "Only direct connections";
            this.directConnectionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Type of day:";
            // 
            // typeOfDayComboBox
            // 
            this.typeOfDayComboBox.FormattingEnabled = true;
            this.typeOfDayComboBox.Location = new System.Drawing.Point(90, 100);
            this.typeOfDayComboBox.Name = "typeOfDayComboBox";
            this.typeOfDayComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeOfDayComboBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = ":";
            // 
            // hourTimeNumericUpDown
            // 
            this.hourTimeNumericUpDown.Location = new System.Drawing.Point(90, 127);
            this.hourTimeNumericUpDown.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hourTimeNumericUpDown.Name = "hourTimeNumericUpDown";
            this.hourTimeNumericUpDown.Size = new System.Drawing.Size(32, 20);
            this.hourTimeNumericUpDown.TabIndex = 14;
            // 
            // minuteTimeNumericUpDown
            // 
            this.minuteTimeNumericUpDown.Location = new System.Drawing.Point(144, 127);
            this.minuteTimeNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minuteTimeNumericUpDown.Name = "minuteTimeNumericUpDown";
            this.minuteTimeNumericUpDown.Size = new System.Drawing.Size(32, 20);
            this.minuteTimeNumericUpDown.TabIndex = 16;
            // 
            // departureRadioButton
            // 
            this.departureRadioButton.AutoSize = true;
            this.departureRadioButton.Location = new System.Drawing.Point(88, 153);
            this.departureRadioButton.Name = "departureRadioButton";
            this.departureRadioButton.Size = new System.Drawing.Size(72, 17);
            this.departureRadioButton.TabIndex = 18;
            this.departureRadioButton.TabStop = true;
            this.departureRadioButton.Text = "Departure";
            this.departureRadioButton.UseVisualStyleBackColor = true;
            // 
            // arrivalRadioButton
            // 
            this.arrivalRadioButton.AutoSize = true;
            this.arrivalRadioButton.Location = new System.Drawing.Point(88, 176);
            this.arrivalRadioButton.Name = "arrivalRadioButton";
            this.arrivalRadioButton.Size = new System.Drawing.Size(54, 17);
            this.arrivalRadioButton.TabIndex = 19;
            this.arrivalRadioButton.TabStop = true;
            this.arrivalRadioButton.Text = "Arrival";
            this.arrivalRadioButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(101, 227);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 20;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Results";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Line:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(259, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Time of departure:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Time of arrival:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(358, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(47, 20);
            this.textBox1.TabIndex = 25;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(358, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(47, 20);
            this.textBox2.TabIndex = 26;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(358, 122);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(47, 20);
            this.textBox3.TabIndex = 27;
            // 
            // CommunicationRoutesGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 262);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.arrivalRadioButton);
            this.Controls.Add(this.departureRadioButton);
            this.Controls.Add(this.minuteTimeNumericUpDown);
            this.Controls.Add(this.hourTimeNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.typeOfDayComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.directConnectionsCheckBox);
            this.Controls.Add(this.destinationBusStopComboBox);
            this.Controls.Add(this.startBusStopComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upperMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommunicationRoutesGui";
            this.Text = "Communication Routes";
            this.upperMenuStrip.ResumeLayout(false);
            this.upperMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hourTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteTimeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip upperMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox startBusStopComboBox;
        private System.Windows.Forms.ComboBox destinationBusStopComboBox;
        private System.Windows.Forms.CheckBox directConnectionsCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox typeOfDayComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown hourTimeNumericUpDown;
        private System.Windows.Forms.NumericUpDown minuteTimeNumericUpDown;
        private System.Windows.Forms.RadioButton departureRadioButton;
        private System.Windows.Forms.RadioButton arrivalRadioButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;

    }
}


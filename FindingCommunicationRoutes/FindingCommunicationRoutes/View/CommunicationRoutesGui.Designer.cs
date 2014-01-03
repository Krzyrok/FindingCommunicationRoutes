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
            foreach (System.Threading.Thread thread in _threadsList)
            {
                if (thread.ThreadState == System.Threading.ThreadState.Running)
                {
                    thread.Abort();
                }
            }

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
            this.updateScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startBusStopComboBox = new System.Windows.Forms.ComboBox();
            this.destinationBusStopComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.lineDirectResultTextBox = new System.Windows.Forms.TextBox();
            this.timeOfDepartureDirectResultTextBox = new System.Windows.Forms.TextBox();
            this.timeOfArrivalDirectResultTextBox = new System.Windows.Forms.TextBox();
            this.informationAboutActualizationProgressBar = new System.Windows.Forms.ProgressBar();
            this.dateOfJourneyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.informationLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.totalTravelTimeDirectResultTextBox = new System.Windows.Forms.TextBox();
            this.indirectConnectionDetailsListView = new System.Windows.Forms.ListView();
            this.startBusStopColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endBusStopColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.totalTravelTimeInderectResultTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.departureDateDirectResultTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.departureDateIndirectResultTextBox = new System.Windows.Forms.TextBox();
            this.timeOfArrivalIndirectResultTextBox = new System.Windows.Forms.TextBox();
            this.timeOfDepartureIndirectResultTextBox = new System.Windows.Forms.TextBox();
            this.lineIndirectResultTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
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
            this.upperMenuStrip.Size = new System.Drawing.Size(824, 24);
            this.upperMenuStrip.TabIndex = 1;
            this.upperMenuStrip.Text = "upperMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateScheduleToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // updateScheduleToolStripMenuItem
            // 
            this.updateScheduleToolStripMenuItem.Name = "updateScheduleToolStripMenuItem";
            this.updateScheduleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.updateScheduleToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.updateScheduleToolStripMenuItem.Text = "&Update schedule";
            this.updateScheduleToolStripMenuItem.Click += new System.EventHandler(this.updateScheduleToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To:";
            // 
            // startBusStopComboBox
            // 
            this.startBusStopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startBusStopComboBox.FormattingEnabled = true;
            this.startBusStopComboBox.Location = new System.Drawing.Point(53, 39);
            this.startBusStopComboBox.Name = "startBusStopComboBox";
            this.startBusStopComboBox.Size = new System.Drawing.Size(148, 21);
            this.startBusStopComboBox.TabIndex = 4;
            this.startBusStopComboBox.SelectedIndexChanged += new System.EventHandler(this.startBusStopComboBox_SelectedIndexChanged);
            // 
            // destinationBusStopComboBox
            // 
            this.destinationBusStopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destinationBusStopComboBox.FormattingEnabled = true;
            this.destinationBusStopComboBox.Location = new System.Drawing.Point(53, 69);
            this.destinationBusStopComboBox.Name = "destinationBusStopComboBox";
            this.destinationBusStopComboBox.Size = new System.Drawing.Size(148, 21);
            this.destinationBusStopComboBox.TabIndex = 5;
            this.destinationBusStopComboBox.SelectedIndexChanged += new System.EventHandler(this.destinationBusStopComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = ":";
            // 
            // hourTimeNumericUpDown
            // 
            this.hourTimeNumericUpDown.Location = new System.Drawing.Point(54, 123);
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
            this.minuteTimeNumericUpDown.Location = new System.Drawing.Point(108, 123);
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
            this.departureRadioButton.Checked = true;
            this.departureRadioButton.Location = new System.Drawing.Point(53, 149);
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
            this.arrivalRadioButton.Location = new System.Drawing.Point(53, 172);
            this.arrivalRadioButton.Name = "arrivalRadioButton";
            this.arrivalRadioButton.Size = new System.Drawing.Size(54, 17);
            this.arrivalRadioButton.TabIndex = 19;
            this.arrivalRadioButton.Text = "Arrival";
            this.arrivalRadioButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(50, 218);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 20;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Direct connection:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Line:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(228, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Time of departure:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(245, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Time of arrival:";
            // 
            // lineDirectResultTextBox
            // 
            this.lineDirectResultTextBox.Enabled = false;
            this.lineDirectResultTextBox.Location = new System.Drawing.Point(327, 65);
            this.lineDirectResultTextBox.Name = "lineDirectResultTextBox";
            this.lineDirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.lineDirectResultTextBox.TabIndex = 25;
            // 
            // timeOfDepartureDirectResultTextBox
            // 
            this.timeOfDepartureDirectResultTextBox.Enabled = false;
            this.timeOfDepartureDirectResultTextBox.Location = new System.Drawing.Point(327, 92);
            this.timeOfDepartureDirectResultTextBox.Name = "timeOfDepartureDirectResultTextBox";
            this.timeOfDepartureDirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.timeOfDepartureDirectResultTextBox.TabIndex = 26;
            // 
            // timeOfArrivalDirectResultTextBox
            // 
            this.timeOfArrivalDirectResultTextBox.Enabled = false;
            this.timeOfArrivalDirectResultTextBox.Location = new System.Drawing.Point(327, 118);
            this.timeOfArrivalDirectResultTextBox.Name = "timeOfArrivalDirectResultTextBox";
            this.timeOfArrivalDirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.timeOfArrivalDirectResultTextBox.TabIndex = 27;
            // 
            // informationAboutActualizationProgressBar
            // 
            this.informationAboutActualizationProgressBar.Location = new System.Drawing.Point(207, 218);
            this.informationAboutActualizationProgressBar.Name = "informationAboutActualizationProgressBar";
            this.informationAboutActualizationProgressBar.Size = new System.Drawing.Size(95, 23);
            this.informationAboutActualizationProgressBar.TabIndex = 28;
            this.informationAboutActualizationProgressBar.Value = 100;
            // 
            // dateOfJourneyDateTimePicker
            // 
            this.dateOfJourneyDateTimePicker.Location = new System.Drawing.Point(54, 97);
            this.dateOfJourneyDateTimePicker.Name = "dateOfJourneyDateTimePicker";
            this.dateOfJourneyDateTimePicker.Size = new System.Drawing.Size(147, 20);
            this.dateOfJourneyDateTimePicker.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(204, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Information:";
            // 
            // informationLabel
            // 
            this.informationLabel.AutoSize = true;
            this.informationLabel.Location = new System.Drawing.Point(271, 195);
            this.informationLabel.Name = "informationLabel";
            this.informationLabel.Size = new System.Drawing.Size(31, 13);
            this.informationLabel.TabIndex = 31;
            this.informationLabel.Text = "none";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(259, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Travel time:";
            // 
            // totalTravelTimeDirectResultTextBox
            // 
            this.totalTravelTimeDirectResultTextBox.Enabled = false;
            this.totalTravelTimeDirectResultTextBox.Location = new System.Drawing.Point(327, 170);
            this.totalTravelTimeDirectResultTextBox.Name = "totalTravelTimeDirectResultTextBox";
            this.totalTravelTimeDirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.totalTravelTimeDirectResultTextBox.TabIndex = 33;
            // 
            // indirectConnectionDetailsListView
            // 
            this.indirectConnectionDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.startBusStopColumnHeader,
            this.endBusStopColumnHeader});
            this.indirectConnectionDetailsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.indirectConnectionDetailsListView.HideSelection = false;
            this.indirectConnectionDetailsListView.Location = new System.Drawing.Point(572, 63);
            this.indirectConnectionDetailsListView.MultiSelect = false;
            this.indirectConnectionDetailsListView.Name = "indirectConnectionDetailsListView";
            this.indirectConnectionDetailsListView.Size = new System.Drawing.Size(241, 97);
            this.indirectConnectionDetailsListView.TabIndex = 34;
            this.indirectConnectionDetailsListView.UseCompatibleStateImageBehavior = false;
            this.indirectConnectionDetailsListView.View = System.Windows.Forms.View.Details;
            // 
            // startBusStopColumnHeader
            // 
            this.startBusStopColumnHeader.Text = "Start Bus Stop";
            this.startBusStopColumnHeader.Width = 127;
            // 
            // endBusStopColumnHeader
            // 
            this.endBusStopColumnHeader.Text = "End Bus Stop";
            this.endBusStopColumnHeader.Width = 107;
            // 
            // totalTravelTimeInderectResultTextBox
            // 
            this.totalTravelTimeInderectResultTextBox.Enabled = false;
            this.totalTravelTimeInderectResultTextBox.Location = new System.Drawing.Point(765, 173);
            this.totalTravelTimeInderectResultTextBox.Name = "totalTravelTimeInderectResultTextBox";
            this.totalTravelTimeInderectResultTextBox.Size = new System.Drawing.Size(47, 20);
            this.totalTravelTimeInderectResultTextBox.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(697, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Travel time:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(569, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "Indirect connection:";
            // 
            // departureDateDirectResultTextBox
            // 
            this.departureDateDirectResultTextBox.Enabled = false;
            this.departureDateDirectResultTextBox.Location = new System.Drawing.Point(327, 144);
            this.departureDateDirectResultTextBox.Name = "departureDateDirectResultTextBox";
            this.departureDateDirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.departureDateDirectResultTextBox.TabIndex = 38;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(228, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Date of departure:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(404, 147);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 13);
            this.label15.TabIndex = 49;
            this.label15.Text = "Date of departure:";
            // 
            // departureDateIndirectResultTextBox
            // 
            this.departureDateIndirectResultTextBox.Enabled = false;
            this.departureDateIndirectResultTextBox.Location = new System.Drawing.Point(503, 144);
            this.departureDateIndirectResultTextBox.Name = "departureDateIndirectResultTextBox";
            this.departureDateIndirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.departureDateIndirectResultTextBox.TabIndex = 48;
            // 
            // timeOfArrivalIndirectResultTextBox
            // 
            this.timeOfArrivalIndirectResultTextBox.Enabled = false;
            this.timeOfArrivalIndirectResultTextBox.Location = new System.Drawing.Point(502, 118);
            this.timeOfArrivalIndirectResultTextBox.Name = "timeOfArrivalIndirectResultTextBox";
            this.timeOfArrivalIndirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.timeOfArrivalIndirectResultTextBox.TabIndex = 45;
            // 
            // timeOfDepartureIndirectResultTextBox
            // 
            this.timeOfDepartureIndirectResultTextBox.Enabled = false;
            this.timeOfDepartureIndirectResultTextBox.Location = new System.Drawing.Point(502, 92);
            this.timeOfDepartureIndirectResultTextBox.Name = "timeOfDepartureIndirectResultTextBox";
            this.timeOfDepartureIndirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.timeOfDepartureIndirectResultTextBox.TabIndex = 44;
            // 
            // lineIndirectResultTextBox
            // 
            this.lineIndirectResultTextBox.Enabled = false;
            this.lineIndirectResultTextBox.Location = new System.Drawing.Point(502, 65);
            this.lineIndirectResultTextBox.Name = "lineIndirectResultTextBox";
            this.lineIndirectResultTextBox.Size = new System.Drawing.Size(63, 20);
            this.lineIndirectResultTextBox.TabIndex = 43;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(420, 121);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 13);
            this.label17.TabIndex = 42;
            this.label17.Text = "Time of arrival:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(403, 95);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Time of departure:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(466, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(30, 13);
            this.label19.TabIndex = 40;
            this.label19.Text = "Line:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(428, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "Indirect connection details:";
            // 
            // CommunicationRoutesGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 269);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.departureDateIndirectResultTextBox);
            this.Controls.Add(this.timeOfArrivalIndirectResultTextBox);
            this.Controls.Add(this.timeOfDepartureIndirectResultTextBox);
            this.Controls.Add(this.lineIndirectResultTextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.departureDateDirectResultTextBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.totalTravelTimeInderectResultTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.indirectConnectionDetailsListView);
            this.Controls.Add(this.totalTravelTimeDirectResultTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.informationLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dateOfJourneyDateTimePicker);
            this.Controls.Add(this.informationAboutActualizationProgressBar);
            this.Controls.Add(this.timeOfArrivalDirectResultTextBox);
            this.Controls.Add(this.timeOfDepartureDirectResultTextBox);
            this.Controls.Add(this.lineDirectResultTextBox);
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
            this.Controls.Add(this.label3);
            this.Controls.Add(this.destinationBusStopComboBox);
            this.Controls.Add(this.startBusStopComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upperMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
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
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.TextBox lineDirectResultTextBox;
        private System.Windows.Forms.TextBox timeOfDepartureDirectResultTextBox;
        private System.Windows.Forms.TextBox timeOfArrivalDirectResultTextBox;
        private System.Windows.Forms.ProgressBar informationAboutActualizationProgressBar;
        private System.Windows.Forms.ToolStripMenuItem updateScheduleToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateOfJourneyDateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label informationLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox totalTravelTimeDirectResultTextBox;
        private System.Windows.Forms.ListView indirectConnectionDetailsListView;
        private System.Windows.Forms.ColumnHeader startBusStopColumnHeader;
        private System.Windows.Forms.ColumnHeader endBusStopColumnHeader;
        private System.Windows.Forms.TextBox totalTravelTimeInderectResultTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox departureDateDirectResultTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox departureDateIndirectResultTextBox;
        private System.Windows.Forms.TextBox timeOfArrivalIndirectResultTextBox;
        private System.Windows.Forms.TextBox timeOfDepartureIndirectResultTextBox;
        private System.Windows.Forms.TextBox lineIndirectResultTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;

    }
}


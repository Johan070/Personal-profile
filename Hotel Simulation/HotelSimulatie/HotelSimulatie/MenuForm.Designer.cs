namespace HotelSimulatie
{
    /// <summary>
    /// The Status Menu Form
    /// </summary>
    partial class MenuForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSettingsPage = new System.Windows.Forms.TabPage();
            this.Movie_Runtime_TXT = new System.Windows.Forms.TextBox();
            this.Eating_Speed_TXT = new System.Windows.Forms.TextBox();
            this.Movie_RunTime_LBL = new System.Windows.Forms.Label();
            this.Eating_Speed_LBL = new System.Windows.Forms.Label();
            this.lblStairsSpeed = new System.Windows.Forms.Label();
            this.lblSimulationSpeed = new System.Windows.Forms.Label();
            this.cmbStairsSpeed = new System.Windows.Forms.ComboBox();
            this.cmbHTE_Time = new System.Windows.Forms.ComboBox();
            this.tabRoomStatusPage = new System.Windows.Forms.TabPage();
            this.listBoxRoomStatus = new System.Windows.Forms.ListBox();
            this.tabCustomerStatusPage = new System.Windows.Forms.TabPage();
            this.lblCustomerCount = new System.Windows.Forms.Label();
            this.listBoxCustomerStatus = new System.Windows.Forms.ListBox();
            this.tabCleanerStatusPage = new System.Windows.Forms.TabPage();
            this.listBoxCleanerStatus = new System.Windows.Forms.ListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.Cleaning_Speed_LBL = new System.Windows.Forms.Label();
            this.Cleaning_Speed_TXT = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabSettingsPage.SuspendLayout();
            this.tabRoomStatusPage.SuspendLayout();
            this.tabCustomerStatusPage.SuspendLayout();
            this.tabCleanerStatusPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSettingsPage);
            this.tabControl.Controls.Add(this.tabRoomStatusPage);
            this.tabControl.Controls.Add(this.tabCustomerStatusPage);
            this.tabControl.Controls.Add(this.tabCleanerStatusPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(726, 515);
            this.tabControl.TabIndex = 0;
            // 
            // tabSettingsPage
            // 
            this.tabSettingsPage.BackColor = System.Drawing.Color.DimGray;
            this.tabSettingsPage.Controls.Add(this.Cleaning_Speed_TXT);
            this.tabSettingsPage.Controls.Add(this.Cleaning_Speed_LBL);
            this.tabSettingsPage.Controls.Add(this.Movie_Runtime_TXT);
            this.tabSettingsPage.Controls.Add(this.Eating_Speed_TXT);
            this.tabSettingsPage.Controls.Add(this.Movie_RunTime_LBL);
            this.tabSettingsPage.Controls.Add(this.Eating_Speed_LBL);
            this.tabSettingsPage.Controls.Add(this.lblStairsSpeed);
            this.tabSettingsPage.Controls.Add(this.lblSimulationSpeed);
            this.tabSettingsPage.Controls.Add(this.cmbStairsSpeed);
            this.tabSettingsPage.Controls.Add(this.cmbHTE_Time);
            this.tabSettingsPage.Location = new System.Drawing.Point(4, 22);
            this.tabSettingsPage.Name = "tabSettingsPage";
            this.tabSettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettingsPage.Size = new System.Drawing.Size(718, 489);
            this.tabSettingsPage.TabIndex = 0;
            this.tabSettingsPage.Text = "Settings";
            // 
            // Movie_Runtime_TXT
            // 
            this.Movie_Runtime_TXT.Location = new System.Drawing.Point(78, 289);
            this.Movie_Runtime_TXT.Name = "Movie_Runtime_TXT";
            this.Movie_Runtime_TXT.Size = new System.Drawing.Size(118, 20);
            this.Movie_Runtime_TXT.TabIndex = 9;
            // 
            // Eating_Speed_TXT
            // 
            this.Eating_Speed_TXT.Location = new System.Drawing.Point(78, 242);
            this.Eating_Speed_TXT.Name = "Eating_Speed_TXT";
            this.Eating_Speed_TXT.Size = new System.Drawing.Size(118, 20);
            this.Eating_Speed_TXT.TabIndex = 8;
            // 
            // Movie_RunTime_LBL
            // 
            this.Movie_RunTime_LBL.AutoSize = true;
            this.Movie_RunTime_LBL.Location = new System.Drawing.Point(75, 273);
            this.Movie_RunTime_LBL.Name = "Movie_RunTime_LBL";
            this.Movie_RunTime_LBL.Size = new System.Drawing.Size(78, 13);
            this.Movie_RunTime_LBL.TabIndex = 7;
            this.Movie_RunTime_LBL.Text = "Movie Runtime";
            this.Movie_RunTime_LBL.Click += new System.EventHandler(this.label1_Click);
            // 
            // Eating_Speed_LBL
            // 
            this.Eating_Speed_LBL.AutoSize = true;
            this.Eating_Speed_LBL.Location = new System.Drawing.Point(75, 220);
            this.Eating_Speed_LBL.Name = "Eating_Speed_LBL";
            this.Eating_Speed_LBL.Size = new System.Drawing.Size(71, 13);
            this.Eating_Speed_LBL.TabIndex = 6;
            this.Eating_Speed_LBL.Text = "Eating Speed";
            this.Eating_Speed_LBL.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblStairsSpeed
            // 
            this.lblStairsSpeed.AutoSize = true;
            this.lblStairsSpeed.Location = new System.Drawing.Point(75, 168);
            this.lblStairsSpeed.Name = "lblStairsSpeed";
            this.lblStairsSpeed.Size = new System.Drawing.Size(67, 13);
            this.lblStairsSpeed.TabIndex = 3;
            this.lblStairsSpeed.Text = "Stairs Speed";
            // 
            // lblSimulationSpeed
            // 
            this.lblSimulationSpeed.AutoSize = true;
            this.lblSimulationSpeed.Location = new System.Drawing.Point(75, 115);
            this.lblSimulationSpeed.Name = "lblSimulationSpeed";
            this.lblSimulationSpeed.Size = new System.Drawing.Size(89, 13);
            this.lblSimulationSpeed.TabIndex = 2;
            this.lblSimulationSpeed.Text = "Simulation Speed";
            // 
            // cmbStairsSpeed
            // 
            this.cmbStairsSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStairsSpeed.FormattingEnabled = true;
            this.cmbStairsSpeed.Location = new System.Drawing.Point(75, 188);
            this.cmbStairsSpeed.Name = "cmbStairsSpeed";
            this.cmbStairsSpeed.Size = new System.Drawing.Size(121, 21);
            this.cmbStairsSpeed.TabIndex = 1;
            // 
            // cmbHTE_Time
            // 
            this.cmbHTE_Time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHTE_Time.FormattingEnabled = true;
            this.cmbHTE_Time.Location = new System.Drawing.Point(75, 134);
            this.cmbHTE_Time.Name = "cmbHTE_Time";
            this.cmbHTE_Time.Size = new System.Drawing.Size(121, 21);
            this.cmbHTE_Time.TabIndex = 0;
            // 
            // tabRoomStatusPage
            // 
            this.tabRoomStatusPage.Controls.Add(this.listBoxRoomStatus);
            this.tabRoomStatusPage.Location = new System.Drawing.Point(4, 22);
            this.tabRoomStatusPage.Name = "tabRoomStatusPage";
            this.tabRoomStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoomStatusPage.Size = new System.Drawing.Size(718, 489);
            this.tabRoomStatusPage.TabIndex = 1;
            this.tabRoomStatusPage.Text = "RoomStatus";
            this.tabRoomStatusPage.UseVisualStyleBackColor = true;
            // 
            // listBoxRoomStatus
            // 
            this.listBoxRoomStatus.FormattingEnabled = true;
            this.listBoxRoomStatus.Location = new System.Drawing.Point(-4, 2);
            this.listBoxRoomStatus.Name = "listBoxRoomStatus";
            this.listBoxRoomStatus.Size = new System.Drawing.Size(722, 485);
            this.listBoxRoomStatus.TabIndex = 0;
            // 
            // tabCustomerStatusPage
            // 
            this.tabCustomerStatusPage.Controls.Add(this.lblCustomerCount);
            this.tabCustomerStatusPage.Controls.Add(this.listBoxCustomerStatus);
            this.tabCustomerStatusPage.Location = new System.Drawing.Point(4, 22);
            this.tabCustomerStatusPage.Name = "tabCustomerStatusPage";
            this.tabCustomerStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomerStatusPage.Size = new System.Drawing.Size(718, 489);
            this.tabCustomerStatusPage.TabIndex = 2;
            this.tabCustomerStatusPage.Text = "CustomerStatus";
            this.tabCustomerStatusPage.UseVisualStyleBackColor = true;
            // 
            // lblCustomerCount
            // 
            this.lblCustomerCount.AutoSize = true;
            this.lblCustomerCount.Location = new System.Drawing.Point(546, 473);
            this.lblCustomerCount.Name = "lblCustomerCount";
            this.lblCustomerCount.Size = new System.Drawing.Size(88, 13);
            this.lblCustomerCount.TabIndex = 1;
            this.lblCustomerCount.Text = "Customer Count: ";
            // 
            // listBoxCustomerStatus
            // 
            this.listBoxCustomerStatus.FormattingEnabled = true;
            this.listBoxCustomerStatus.Location = new System.Drawing.Point(-4, 0);
            this.listBoxCustomerStatus.Name = "listBoxCustomerStatus";
            this.listBoxCustomerStatus.Size = new System.Drawing.Size(722, 498);
            this.listBoxCustomerStatus.TabIndex = 0;
            // 
            // tabCleanerStatusPage
            // 
            this.tabCleanerStatusPage.Controls.Add(this.listBoxCleanerStatus);
            this.tabCleanerStatusPage.Location = new System.Drawing.Point(4, 22);
            this.tabCleanerStatusPage.Name = "tabCleanerStatusPage";
            this.tabCleanerStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabCleanerStatusPage.Size = new System.Drawing.Size(718, 489);
            this.tabCleanerStatusPage.TabIndex = 3;
            this.tabCleanerStatusPage.Text = "CleanerStatus";
            this.tabCleanerStatusPage.UseVisualStyleBackColor = true;
            // 
            // listBoxCleanerStatus
            // 
            this.listBoxCleanerStatus.FormattingEnabled = true;
            this.listBoxCleanerStatus.Location = new System.Drawing.Point(-4, 2);
            this.listBoxCleanerStatus.Name = "listBoxCleanerStatus";
            this.listBoxCleanerStatus.Size = new System.Drawing.Size(722, 498);
            this.listBoxCleanerStatus.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(744, 274);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseOnClick);
            // 
            // Cleaning_Speed_LBL
            // 
            this.Cleaning_Speed_LBL.AutoSize = true;
            this.Cleaning_Speed_LBL.Location = new System.Drawing.Point(75, 317);
            this.Cleaning_Speed_LBL.Name = "Cleaning_Speed_LBL";
            this.Cleaning_Speed_LBL.Size = new System.Drawing.Size(82, 13);
            this.Cleaning_Speed_LBL.TabIndex = 10;
            this.Cleaning_Speed_LBL.Text = "Cleaning Speed";
            // 
            // Cleaning_Speed_TXT
            // 
            this.Cleaning_Speed_TXT.Location = new System.Drawing.Point(78, 333);
            this.Cleaning_Speed_TXT.Name = "Cleaning_Speed_TXT";
            this.Cleaning_Speed_TXT.Size = new System.Drawing.Size(118, 20);
            this.Cleaning_Speed_TXT.TabIndex = 11;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 539);
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.tabControl.ResumeLayout(false);
            this.tabSettingsPage.ResumeLayout(false);
            this.tabSettingsPage.PerformLayout();
            this.tabRoomStatusPage.ResumeLayout(false);
            this.tabCustomerStatusPage.ResumeLayout(false);
            this.tabCustomerStatusPage.PerformLayout();
            this.tabCleanerStatusPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSettingsPage;
        private System.Windows.Forms.TabPage tabRoomStatusPage;
        private System.Windows.Forms.TabPage tabCustomerStatusPage;
        private System.Windows.Forms.TabPage tabCleanerStatusPage;
        private System.Windows.Forms.ComboBox cmbStairsSpeed;
        private System.Windows.Forms.ComboBox cmbHTE_Time;
        private System.Windows.Forms.ListBox listBoxRoomStatus;
        private System.Windows.Forms.ListBox listBoxCustomerStatus;
        private System.Windows.Forms.ListBox listBoxCleanerStatus;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label lblStairsSpeed;
        private System.Windows.Forms.Label lblSimulationSpeed;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.Label Movie_RunTime_LBL;
        private System.Windows.Forms.Label Eating_Speed_LBL;
        private System.Windows.Forms.TextBox Movie_Runtime_TXT;
        private System.Windows.Forms.TextBox Eating_Speed_TXT;
        private System.Windows.Forms.TextBox Cleaning_Speed_TXT;
        private System.Windows.Forms.Label Cleaning_Speed_LBL;
    }
}
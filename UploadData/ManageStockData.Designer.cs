namespace UploadData
{
    partial class ManageStockData
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("USA");
            this.btnUploadData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.success = new System.Windows.Forms.Label();
            this.textDataServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textDatabase = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textOutputPath = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnBreakEven = new System.Windows.Forms.Button();
            this.btnMarketCalendar = new System.Windows.Forms.Button();
            this.btnlnkWeb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadData.Location = new System.Drawing.Point(16, 391);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(204, 43);
            this.btnUploadData.TabIndex = 0;
            this.btnUploadData.Text = "Upload stock data";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(195, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stock Data Manager";
            // 
            // success
            // 
            this.success.AutoSize = true;
            this.success.Location = new System.Drawing.Point(47, 456);
            this.success.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.success.Name = "success";
            this.success.Size = new System.Drawing.Size(173, 17);
            this.success.TabIndex = 2;
            this.success.Text = "Data loaded successfully..";
            // 
            // textDataServer
            // 
            this.textDataServer.Location = new System.Drawing.Point(164, 174);
            this.textDataServer.Margin = new System.Windows.Forms.Padding(4);
            this.textDataServer.Name = "textDataServer";
            this.textDataServer.Size = new System.Drawing.Size(185, 22);
            this.textDataServer.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 179);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "DataServer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 234);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Database";
            // 
            // textDatabase
            // 
            this.textDatabase.Location = new System.Drawing.Point(164, 229);
            this.textDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Size = new System.Drawing.Size(185, 22);
            this.textDatabase.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 289);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Input";
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(164, 284);
            this.folderPath.Margin = new System.Windows.Forms.Padding(4);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(185, 22);
            this.folderPath.TabIndex = 7;
            this.folderPath.Click += new System.EventHandler(this.FolderPath_Click);
            this.folderPath.Enter += new System.EventHandler(this.FolderPath_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 340);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output";
            // 
            // textOutputPath
            // 
            this.textOutputPath.Location = new System.Drawing.Point(164, 330);
            this.textOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.textOutputPath.Name = "textOutputPath";
            this.textOutputPath.Size = new System.Drawing.Size(185, 22);
            this.textOutputPath.TabIndex = 9;
            this.textOutputPath.Click += new System.EventHandler(this.TextOutputPath_Click);
            this.textOutputPath.Enter += new System.EventHandler(this.TextOutputPath_Enter);
            // 
            // listView1
            // 
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(164, 108);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(185, 30);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AllowDrop = true;
            this.label6.Location = new System.Drawing.Point(8, 108);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 46);
            this.label6.TabIndex = 12;
            this.label6.Text = "Select Country - To be implemented";
            this.label6.UseCompatibleTextRendering = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(456, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 31);
            this.button2.TabIndex = 13;
            this.button2.Text = "Data CleanUp";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(456, 252);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(185, 31);
            this.btnCalc.TabIndex = 16;
            this.btnCalc.Text = "Calculator";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.BtnCalc_Click);
            // 
            // btnBreakEven
            // 
            this.btnBreakEven.Location = new System.Drawing.Point(456, 172);
            this.btnBreakEven.Name = "btnBreakEven";
            this.btnBreakEven.Size = new System.Drawing.Size(185, 31);
            this.btnBreakEven.TabIndex = 17;
            this.btnBreakEven.Text = "BreakEven";
            this.btnBreakEven.UseVisualStyleBackColor = true;
            this.btnBreakEven.Click += new System.EventHandler(this.btnBreakEven_Click);
            // 
            // btnMarketCalendar
            // 
            this.btnMarketCalendar.Location = new System.Drawing.Point(456, 326);
            this.btnMarketCalendar.Name = "btnMarketCalendar";
            this.btnMarketCalendar.Size = new System.Drawing.Size(185, 31);
            this.btnMarketCalendar.TabIndex = 18;
            this.btnMarketCalendar.Text = "Market Calendar";
            this.btnMarketCalendar.UseVisualStyleBackColor = true;
            // 
            // btnlnkWeb
            // 
            this.btnlnkWeb.Location = new System.Drawing.Point(456, 391);
            this.btnlnkWeb.Name = "btnlnkWeb";
            this.btnlnkWeb.Size = new System.Drawing.Size(185, 31);
            this.btnlnkWeb.TabIndex = 19;
            this.btnlnkWeb.Text = "Links to websites";
            this.btnlnkWeb.UseVisualStyleBackColor = true;
            // 
            // ManageStockData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(690, 472);
            this.Controls.Add(this.btnlnkWeb);
            this.Controls.Add(this.btnMarketCalendar);
            this.Controls.Add(this.btnBreakEven);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textOutputPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDataServer);
            this.Controls.Add(this.success);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUploadData);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ManageStockData";
            this.Text = "Deepika Consulting Services";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label success;
        private System.Windows.Forms.TextBox textDataServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDatabase;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textOutputPath;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnBreakEven;
        private System.Windows.Forms.Button btnMarketCalendar;
        private System.Windows.Forms.Button btnlnkWeb;
    }
}


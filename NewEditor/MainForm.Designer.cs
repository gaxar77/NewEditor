namespace Gaxar77.NewEditor
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvFileTypes = new System.Windows.Forms.ListView();
            this.btnModifyFileType = new System.Windows.Forms.Button();
            this.btnRemoveFileType = new System.Windows.Forms.Button();
            this.btnAddFileType = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvFileTypes);
            this.groupBox1.Controls.Add(this.btnModifyFileType);
            this.groupBox1.Controls.Add(this.btnRemoveFileType);
            this.groupBox1.Controls.Add(this.btnAddFileType);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(555, 458);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Types";
            // 
            // lvFileTypes
            // 
            this.lvFileTypes.HideSelection = false;
            this.lvFileTypes.Location = new System.Drawing.Point(7, 27);
            this.lvFileTypes.MultiSelect = false;
            this.lvFileTypes.Name = "lvFileTypes";
            this.lvFileTypes.Size = new System.Drawing.Size(420, 410);
            this.lvFileTypes.TabIndex = 4;
            this.lvFileTypes.UseCompatibleStateImageBehavior = false;
            this.lvFileTypes.View = System.Windows.Forms.View.Details;
            this.lvFileTypes.SelectedIndexChanged += new System.EventHandler(this.lvFileTypes_SelectedIndexChanged);
            // 
            // btnModifyFileType
            // 
            this.btnModifyFileType.Enabled = false;
            this.btnModifyFileType.Location = new System.Drawing.Point(434, 74);
            this.btnModifyFileType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModifyFileType.Name = "btnModifyFileType";
            this.btnModifyFileType.Size = new System.Drawing.Size(112, 35);
            this.btnModifyFileType.TabIndex = 3;
            this.btnModifyFileType.Text = "Modify";
            this.btnModifyFileType.UseVisualStyleBackColor = true;
            this.btnModifyFileType.Click += new System.EventHandler(this.btnModifyFileType_Click);
            // 
            // btnRemoveFileType
            // 
            this.btnRemoveFileType.Enabled = false;
            this.btnRemoveFileType.Location = new System.Drawing.Point(434, 118);
            this.btnRemoveFileType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveFileType.Name = "btnRemoveFileType";
            this.btnRemoveFileType.Size = new System.Drawing.Size(112, 35);
            this.btnRemoveFileType.TabIndex = 2;
            this.btnRemoveFileType.Text = "Remove";
            this.btnRemoveFileType.UseVisualStyleBackColor = true;
            this.btnRemoveFileType.Click += new System.EventHandler(this.btnRemoveFileType_Click);
            // 
            // btnAddFileType
            // 
            this.btnAddFileType.Location = new System.Drawing.Point(434, 29);
            this.btnAddFileType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddFileType.Name = "btnAddFileType";
            this.btnAddFileType.Size = new System.Drawing.Size(112, 35);
            this.btnAddFileType.TabIndex = 1;
            this.btnAddFileType.Text = "Add";
            this.btnAddFileType.UseVisualStyleBackColor = true;
            this.btnAddFileType.Click += new System.EventHandler(this.btnAddFileType_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(460, 502);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(112, 35);
            this.btnAbout.TabIndex = 3;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 552);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Windows Explorer New Menu - Current User";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveFileType;
        private System.Windows.Forms.Button btnAddFileType;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnModifyFileType;
        private System.Windows.Forms.ListView lvFileTypes;
    }
}


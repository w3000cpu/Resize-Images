namespace ResizeImagesApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadBtn = new System.Windows.Forms.Button();
            this.browseLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthTxtBox = new System.Windows.Forms.TextBox();
            this.heightTxtBox = new System.Windows.Forms.TextBox();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resolutionTxtBox = new System.Windows.Forms.TextBox();
            this.resolutionBorder = new System.Windows.Forms.Label();
            this.heightBorder = new System.Windows.Forms.Label();
            this.widthBorder = new System.Windows.Forms.Label();
            this.selectedImagesLabel = new System.Windows.Forms.Label();
            this.saveImagesPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resolutionLabel = new System.Windows.Forms.Label();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // loadBtn
            // 
            resources.ApplyResources(this.loadBtn, "loadBtn");
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            this.loadBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoadBtn_KeyDown);
            this.loadBtn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoadBtn_KeyPress);
            // 
            // browseLabel
            // 
            resources.ApplyResources(this.browseLabel, "browseLabel");
            this.browseLabel.Name = "browseLabel";
            // 
            // widthLabel
            // 
            resources.ApplyResources(this.widthLabel, "widthLabel");
            this.widthLabel.Name = "widthLabel";
            // 
            // heightLabel
            // 
            resources.ApplyResources(this.heightLabel, "heightLabel");
            this.heightLabel.Name = "heightLabel";
            // 
            // widthTxtBox
            // 
            this.widthTxtBox.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.widthTxtBox, "widthTxtBox");
            this.widthTxtBox.Name = "widthTxtBox";
            this.widthTxtBox.TextChanged += new System.EventHandler(this.WidthTxtBox_TextChanged);
            this.widthTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WidthTxtBox_KeyPress);
            // 
            // heightTxtBox
            // 
            this.heightTxtBox.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.heightTxtBox, "heightTxtBox");
            this.heightTxtBox.Name = "heightTxtBox";
            this.heightTxtBox.TextChanged += new System.EventHandler(this.HeightTxtBox_TextChanged);
            this.heightTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HeightTxtBox_KeyPress);
            // 
            // confirmBtn
            // 
            resources.ApplyResources(this.confirmBtn, "confirmBtn");
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // SaveBtn
            // 
            resources.ApplyResources(this.SaveBtn, "SaveBtn");
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.resolutionTxtBox);
            this.panel1.Controls.Add(this.resolutionBorder);
            this.panel1.Controls.Add(this.heightTxtBox);
            this.panel1.Controls.Add(this.heightBorder);
            this.panel1.Controls.Add(this.widthTxtBox);
            this.panel1.Controls.Add(this.widthBorder);
            this.panel1.Controls.Add(this.selectedImagesLabel);
            this.panel1.Controls.Add(this.saveImagesPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.resolutionLabel);
            this.panel1.Controls.Add(this.percentageLabel);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.loadBtn);
            this.panel1.Controls.Add(this.SaveBtn);
            this.panel1.Controls.Add(this.browseLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.widthLabel);
            this.panel1.Controls.Add(this.confirmBtn);
            this.panel1.Controls.Add(this.heightLabel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // resolutionTxtBox
            // 
            this.resolutionTxtBox.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.resolutionTxtBox, "resolutionTxtBox");
            this.resolutionTxtBox.Name = "resolutionTxtBox";
            this.resolutionTxtBox.TextChanged += new System.EventHandler(this.ResolutionTxtBox_TextChanged);
            this.resolutionTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ResolutionTxtBox_KeyPress);
            // 
            // resolutionBorder
            // 
            this.resolutionBorder.BackColor = System.Drawing.Color.Red;
            this.resolutionBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.resolutionBorder, "resolutionBorder");
            this.resolutionBorder.Name = "resolutionBorder";
            // 
            // heightBorder
            // 
            this.heightBorder.BackColor = System.Drawing.Color.Red;
            this.heightBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.heightBorder, "heightBorder");
            this.heightBorder.Name = "heightBorder";
            // 
            // widthBorder
            // 
            this.widthBorder.BackColor = System.Drawing.Color.Red;
            this.widthBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.widthBorder, "widthBorder");
            this.widthBorder.Name = "widthBorder";
            // 
            // selectedImagesLabel
            // 
            resources.ApplyResources(this.selectedImagesLabel, "selectedImagesLabel");
            this.selectedImagesLabel.ForeColor = System.Drawing.Color.Red;
            this.selectedImagesLabel.Name = "selectedImagesLabel";
            // 
            // saveImagesPath
            // 
            this.saveImagesPath.BackColor = System.Drawing.Color.CornflowerBlue;
            this.saveImagesPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.saveImagesPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.saveImagesPath, "saveImagesPath");
            this.saveImagesPath.ForeColor = System.Drawing.Color.Red;
            this.saveImagesPath.HideSelection = false;
            this.saveImagesPath.Name = "saveImagesPath";
            this.saveImagesPath.ReadOnly = true;
            this.saveImagesPath.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // resolutionLabel
            // 
            resources.ApplyResources(this.resolutionLabel, "resolutionLabel");
            this.resolutionLabel.Name = "resolutionLabel";
            // 
            // percentageLabel
            // 
            resources.ApplyResources(this.percentageLabel, "percentageLabel");
            this.percentageLabel.BackColor = System.Drawing.Color.Transparent;
            this.percentageLabel.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.percentageLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.percentageLabel.Name = "percentageLabel";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Label browseLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.TextBox widthTxtBox;
        private System.Windows.Forms.TextBox heightTxtBox;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label percentageLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label resolutionLabel;
        private System.Windows.Forms.TextBox resolutionTxtBox;
        private System.Windows.Forms.TextBox saveImagesPath;
        private System.Windows.Forms.Label selectedImagesLabel;
        private System.Windows.Forms.Label widthBorder;
        private System.Windows.Forms.Label resolutionBorder;
        private System.Windows.Forms.Label heightBorder;
    }
}


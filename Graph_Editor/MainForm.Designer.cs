namespace Graph_Editor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CoordsText = new System.Windows.Forms.Label();
            this.ModeButton = new System.Windows.Forms.Button();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.VertexContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdjacencyMatrixButton = new System.Windows.Forms.Button();
            this.ScreenshotButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ModeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.VertexContextMenu.SuspendLayout();
            this.ModeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.Location = new System.Drawing.Point(12, 12);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(75, 23);
            this.AddVertexButton.TabIndex = 0;
            this.AddVertexButton.Text = "Add vertex";
            this.AddVertexButton.UseVisualStyleBackColor = true;
            this.AddVertexButton.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // CoordsText
            // 
            this.CoordsText.AutoSize = true;
            this.CoordsText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CoordsText.Location = new System.Drawing.Point(0, 435);
            this.CoordsText.Name = "CoordsText";
            this.CoordsText.Size = new System.Drawing.Size(0, 15);
            this.CoordsText.TabIndex = 2;
            // 
            // ModeButton
            // 
            this.ModeButton.Location = new System.Drawing.Point(12, 41);
            this.ModeButton.Name = "ModeButton";
            this.ModeButton.Size = new System.Drawing.Size(75, 23);
            this.ModeButton.TabIndex = 3;
            this.ModeButton.Text = "Connect";
            this.ModeButton.UseVisualStyleBackColor = true;
            this.ModeButton.Click += new System.EventHandler(this.ModeButton_Click);
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPictureBox.Location = new System.Drawing.Point(102, 0);
            this.MainPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(698, 450);
            this.MainPictureBox.TabIndex = 4;
            this.MainPictureBox.TabStop = false;
            // 
            // VertexContextMenu
            // 
            this.VertexContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexToolStripMenuItem});
            this.VertexContextMenu.Name = "VertexContextMenu";
            this.VertexContextMenu.Size = new System.Drawing.Size(143, 26);
            this.VertexContextMenu.Opened += new System.EventHandler(this.VertexContextMenu_Opened);
            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteVertexToolStripMenuItem.Text = "Delete vertex";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);
            // 
            // AdjacencyMatrixButton
            // 
            this.AdjacencyMatrixButton.Location = new System.Drawing.Point(12, 70);
            this.AdjacencyMatrixButton.Name = "AdjacencyMatrixButton";
            this.AdjacencyMatrixButton.Size = new System.Drawing.Size(75, 61);
            this.AdjacencyMatrixButton.TabIndex = 5;
            this.AdjacencyMatrixButton.Text = "Get adjacency matrix";
            this.AdjacencyMatrixButton.UseVisualStyleBackColor = true;
            this.AdjacencyMatrixButton.Click += new System.EventHandler(this.AdjacencyMatrixButton_Click);
            // 
            // ScreenshotButton
            // 
            this.ScreenshotButton.Location = new System.Drawing.Point(12, 137);
            this.ScreenshotButton.Name = "ScreenshotButton";
            this.ScreenshotButton.Size = new System.Drawing.Size(75, 39);
            this.ScreenshotButton.TabIndex = 6;
            this.ScreenshotButton.Text = "Save screenshot";
            this.ScreenshotButton.UseVisualStyleBackColor = true;
            this.ScreenshotButton.Click += new System.EventHandler(this.ScreenshotButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(762, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // ModeContextMenu
            // 
            this.ModeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.ModeContextMenu.Name = "ModeContextMenu";
            this.ModeContextMenu.Size = new System.Drawing.Size(134, 70);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScreenshotButton);
            this.Controls.Add(this.AdjacencyMatrixButton);
            this.Controls.Add(this.ModeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddVertexButton);
            this.Controls.Add(this.CoordsText);
            this.Controls.Add(this.MainPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "MainForm";
            this.Text = "GraphED";
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.VertexContextMenu.ResumeLayout(false);
            this.ModeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddVertexButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CoordsText;
        private System.Windows.Forms.Button ModeButton;
        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.ContextMenuStrip VertexContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexToolStripMenuItem;
        private System.Windows.Forms.Button AdjacencyMatrixButton;
        private System.Windows.Forms.Button ScreenshotButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip ModeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
    }
}

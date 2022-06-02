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
            this.CoordsText = new System.Windows.Forms.Label();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModeToolstripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolstripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolstripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjacencyMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortestDistanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortestRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serachInDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VertexContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitter = new System.Windows.Forms.Splitter();
            this.PictureBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexPictureBoxToolstripMenu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.VertexContextMenu.SuspendLayout();
            this.PictureBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
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
            // MainPictureBox
            // 
            this.MainPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPictureBox.Location = new System.Drawing.Point(0, 24);
            this.MainPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.MainPictureBox.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(800, 426);
            this.MainPictureBox.TabIndex = 4;
            this.MainPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4\r\n";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(762, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.HighlightText;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem1,
            this.calculationsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.TabIndex = 12;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveScreenshotToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveScreenshotToolStripMenuItem
            // 
            this.saveScreenshotToolStripMenuItem.Name = "saveScreenshotToolStripMenuItem";
            this.saveScreenshotToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveScreenshotToolStripMenuItem.Text = "Save Screenshot";
            this.saveScreenshotToolStripMenuItem.Click += new System.EventHandler(this.saveScreenshotToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem1.Text = "Edit";
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addVertexToolStripMenuItem.Text = "Add vertex";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editModeToolstripItem,
            this.connectToolstripItem,
            this.disconnectToolstripItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.modeToolStripMenuItem.Text = "Connect";
            // 
            // editModeToolstripItem
            // 
            this.editModeToolstripItem.Name = "editModeToolstripItem";
            this.editModeToolstripItem.Size = new System.Drawing.Size(133, 22);
            this.editModeToolstripItem.Text = "Edit";
            this.editModeToolstripItem.Click += new System.EventHandler(this.editModeToolstripItem_Click);
            // 
            // connectToolstripItem
            // 
            this.connectToolstripItem.Name = "connectToolstripItem";
            this.connectToolstripItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolstripItem.Text = "Connect";
            this.connectToolstripItem.Click += new System.EventHandler(this.connectToolstripItem_Click);
            // 
            // disconnectToolstripItem
            // 
            this.disconnectToolstripItem.Name = "disconnectToolstripItem";
            this.disconnectToolstripItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolstripItem.Text = "Disconnect";
            this.disconnectToolstripItem.Click += new System.EventHandler(this.disconnectToolstripItem_Click);
            // 
            // calculationsToolStripMenuItem
            // 
            this.calculationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adjacencyMatrixToolStripMenuItem,
            this.shortestDistanceToolStripMenuItem,
            this.shortestRouteToolStripMenuItem,
            this.serachInDepthToolStripMenuItem});
            this.calculationsToolStripMenuItem.Name = "calculationsToolStripMenuItem";
            this.calculationsToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.calculationsToolStripMenuItem.Text = "Calculations";
            // 
            // adjacencyMatrixToolStripMenuItem
            // 
            this.adjacencyMatrixToolStripMenuItem.Name = "adjacencyMatrixToolStripMenuItem";
            this.adjacencyMatrixToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.adjacencyMatrixToolStripMenuItem.Text = "Adjacency matrix";
            this.adjacencyMatrixToolStripMenuItem.Click += new System.EventHandler(this.adjacencyMatrixToolStripMenuItem_Click);
            // 
            // shortestDistanceToolStripMenuItem
            // 
            this.shortestDistanceToolStripMenuItem.Name = "shortestDistanceToolStripMenuItem";
            this.shortestDistanceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.shortestDistanceToolStripMenuItem.Text = "Shortest distance";
            this.shortestDistanceToolStripMenuItem.Click += new System.EventHandler(this.shortestDistanceToolStripMenuItem_Click);
            // 
            // shortestRouteToolStripMenuItem
            // 
            this.shortestRouteToolStripMenuItem.Name = "shortestRouteToolStripMenuItem";
            this.shortestRouteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.shortestRouteToolStripMenuItem.Text = "Shortest route";
            this.shortestRouteToolStripMenuItem.Click += new System.EventHandler(this.shortestRouteToolStripMenuItem_Click);
            // 
            // serachInDepthToolStripMenuItem
            // 
            this.serachInDepthToolStripMenuItem.Name = "serachInDepthToolStripMenuItem";
            this.serachInDepthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.serachInDepthToolStripMenuItem.Text = "Serach in depth";
            this.serachInDepthToolStripMenuItem.Click += new System.EventHandler(this.serachInDepthToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteVertexToolStripMenuItem.Text = "Delete vertex";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);
            // 
            // VertexContextMenu
            // 
            this.VertexContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexToolStripMenuItem});
            this.VertexContextMenu.Name = "VertexContextMenu";
            this.VertexContextMenu.Size = new System.Drawing.Size(143, 26);
            this.VertexContextMenu.Opened += new System.EventHandler(this.VertexContextMenu_Opened);
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Enabled = false;
            this.splitter.Location = new System.Drawing.Point(0, 24);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(800, 3);
            this.splitter.TabIndex = 13;
            this.splitter.TabStop = false;
            // 
            // PictureBoxContextMenuStrip
            // 
            this.PictureBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexPictureBoxToolstripMenu});
            this.PictureBoxContextMenuStrip.Name = "PictureBoxContextMenuStrip";
            this.PictureBoxContextMenuStrip.Size = new System.Drawing.Size(132, 26);
            // 
            // addVertexPictureBoxToolstripMenu
            // 
            this.addVertexPictureBoxToolstripMenu.Name = "addVertexPictureBoxToolstripMenu";
            this.addVertexPictureBoxToolstripMenu.Size = new System.Drawing.Size(131, 22);
            this.addVertexPictureBoxToolstripMenu.Text = "Add vertex";
            this.addVertexPictureBoxToolstripMenu.Click += new System.EventHandler(this.addVertexPictureBoxToolstripMenu_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CoordsText);
            this.Controls.Add(this.MainPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "MainForm";
            this.Text = "GraphED";
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.VertexContextMenu.ResumeLayout(false);
            this.PictureBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CoordsText;
        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScreenshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editModeToolstripItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolstripItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolstripItem;
        private System.Windows.Forms.ToolStripMenuItem calculationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjacencyMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortestDistanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortestRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip VertexContextMenu;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.ContextMenuStrip PictureBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addVertexPictureBoxToolstripMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serachInDepthToolStripMenuItem;
    }
}

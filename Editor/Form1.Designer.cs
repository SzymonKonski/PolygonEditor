
namespace Editor
{
    partial class MainWindow
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.editPolygonGroupBox = new System.Windows.Forms.GroupBox();
            this.stycznaCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteRelationRadioButton = new System.Windows.Forms.RadioButton();
            this.deleteCircleRadioButton = new System.Windows.Forms.RadioButton();
            this.deleteVertexRadioButton = new System.Windows.Forms.RadioButton();
            this.deletePolygonRadioButton = new System.Windows.Forms.RadioButton();
            this.addCircleRadioButton = new System.Windows.Forms.RadioButton();
            this.addPolygonRadioButton = new System.Windows.Forms.RadioButton();
            this.changeRadiusRadioButton = new System.Windows.Forms.RadioButton();
            this.divideEdgeRadioButton = new System.Windows.Forms.RadioButton();
            this.moveRadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.equalLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parallelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lockCircleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lockRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tangentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.editPolygonGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14551F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.85449F));
            this.tableLayoutPanel1.Controls.Add(this.editPolygonGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1292, 785);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // editPolygonGroupBox
            // 
            this.editPolygonGroupBox.Controls.Add(this.stycznaCheckBox);
            this.editPolygonGroupBox.Controls.Add(this.deleteRelationRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.deleteCircleRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.deleteVertexRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.deletePolygonRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.addCircleRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.addPolygonRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.changeRadiusRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.divideEdgeRadioButton);
            this.editPolygonGroupBox.Controls.Add(this.moveRadioButton);
            this.editPolygonGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPolygonGroupBox.Location = new System.Drawing.Point(3, 3);
            this.editPolygonGroupBox.Name = "editPolygonGroupBox";
            this.editPolygonGroupBox.Size = new System.Drawing.Size(137, 779);
            this.editPolygonGroupBox.TabIndex = 1;
            this.editPolygonGroupBox.TabStop = false;
            this.editPolygonGroupBox.Text = "Edit";
            // 
            // stycznaCheckBox
            // 
            this.stycznaCheckBox.AutoSize = true;
            this.stycznaCheckBox.Location = new System.Drawing.Point(10, 370);
            this.stycznaCheckBox.Name = "stycznaCheckBox";
            this.stycznaCheckBox.Size = new System.Drawing.Size(64, 17);
            this.stycznaCheckBox.TabIndex = 41;
            this.stycznaCheckBox.Text = "Styczna";
            this.stycznaCheckBox.UseVisualStyleBackColor = true;
            this.stycznaCheckBox.CheckedChanged += new System.EventHandler(this.stycznaCheckBox_CheckedChanged);
            // 
            // deleteRelationRadioButton
            // 
            this.deleteRelationRadioButton.AutoSize = true;
            this.deleteRelationRadioButton.Location = new System.Drawing.Point(9, 286);
            this.deleteRelationRadioButton.Name = "deleteRelationRadioButton";
            this.deleteRelationRadioButton.Size = new System.Drawing.Size(93, 17);
            this.deleteRelationRadioButton.TabIndex = 39;
            this.deleteRelationRadioButton.TabStop = true;
            this.deleteRelationRadioButton.Text = "Delete relation";
            this.deleteRelationRadioButton.UseVisualStyleBackColor = true;
            // 
            // deleteCircleRadioButton
            // 
            this.deleteCircleRadioButton.AutoSize = true;
            this.deleteCircleRadioButton.Location = new System.Drawing.Point(9, 253);
            this.deleteCircleRadioButton.Name = "deleteCircleRadioButton";
            this.deleteCircleRadioButton.Size = new System.Drawing.Size(84, 17);
            this.deleteCircleRadioButton.TabIndex = 38;
            this.deleteCircleRadioButton.TabStop = true;
            this.deleteCircleRadioButton.Text = "Delete circle";
            this.deleteCircleRadioButton.UseVisualStyleBackColor = true;
            // 
            // deleteVertexRadioButton
            // 
            this.deleteVertexRadioButton.AutoSize = true;
            this.deleteVertexRadioButton.Location = new System.Drawing.Point(9, 219);
            this.deleteVertexRadioButton.Name = "deleteVertexRadioButton";
            this.deleteVertexRadioButton.Size = new System.Drawing.Size(88, 17);
            this.deleteVertexRadioButton.TabIndex = 37;
            this.deleteVertexRadioButton.TabStop = true;
            this.deleteVertexRadioButton.Text = "Delete vertex";
            this.deleteVertexRadioButton.UseVisualStyleBackColor = true;
            // 
            // deletePolygonRadioButton
            // 
            this.deletePolygonRadioButton.AutoSize = true;
            this.deletePolygonRadioButton.Location = new System.Drawing.Point(9, 186);
            this.deletePolygonRadioButton.Name = "deletePolygonRadioButton";
            this.deletePolygonRadioButton.Size = new System.Drawing.Size(96, 17);
            this.deletePolygonRadioButton.TabIndex = 36;
            this.deletePolygonRadioButton.TabStop = true;
            this.deletePolygonRadioButton.Text = "Delete polygon";
            this.deletePolygonRadioButton.UseVisualStyleBackColor = true;
            // 
            // addCircleRadioButton
            // 
            this.addCircleRadioButton.AutoSize = true;
            this.addCircleRadioButton.Location = new System.Drawing.Point(9, 153);
            this.addCircleRadioButton.Name = "addCircleRadioButton";
            this.addCircleRadioButton.Size = new System.Drawing.Size(72, 17);
            this.addCircleRadioButton.TabIndex = 35;
            this.addCircleRadioButton.TabStop = true;
            this.addCircleRadioButton.Text = "Add circle";
            this.addCircleRadioButton.UseVisualStyleBackColor = true;
            // 
            // addPolygonRadioButton
            // 
            this.addPolygonRadioButton.AutoSize = true;
            this.addPolygonRadioButton.Location = new System.Drawing.Point(9, 119);
            this.addPolygonRadioButton.Name = "addPolygonRadioButton";
            this.addPolygonRadioButton.Size = new System.Drawing.Size(84, 17);
            this.addPolygonRadioButton.TabIndex = 34;
            this.addPolygonRadioButton.TabStop = true;
            this.addPolygonRadioButton.Text = "Add polygon";
            this.addPolygonRadioButton.UseVisualStyleBackColor = true;
            // 
            // changeRadiusRadioButton
            // 
            this.changeRadiusRadioButton.AutoSize = true;
            this.changeRadiusRadioButton.Location = new System.Drawing.Point(9, 83);
            this.changeRadiusRadioButton.Name = "changeRadiusRadioButton";
            this.changeRadiusRadioButton.Size = new System.Drawing.Size(93, 17);
            this.changeRadiusRadioButton.TabIndex = 33;
            this.changeRadiusRadioButton.TabStop = true;
            this.changeRadiusRadioButton.Text = "Change radius";
            this.changeRadiusRadioButton.UseVisualStyleBackColor = true;
            // 
            // divideEdgeRadioButton
            // 
            this.divideEdgeRadioButton.AutoSize = true;
            this.divideEdgeRadioButton.Location = new System.Drawing.Point(9, 51);
            this.divideEdgeRadioButton.Name = "divideEdgeRadioButton";
            this.divideEdgeRadioButton.Size = new System.Drawing.Size(82, 17);
            this.divideEdgeRadioButton.TabIndex = 32;
            this.divideEdgeRadioButton.TabStop = true;
            this.divideEdgeRadioButton.Text = "Divide edge";
            this.divideEdgeRadioButton.UseVisualStyleBackColor = true;
            // 
            // moveRadioButton
            // 
            this.moveRadioButton.AutoSize = true;
            this.moveRadioButton.Location = new System.Drawing.Point(9, 19);
            this.moveRadioButton.Name = "moveRadioButton";
            this.moveRadioButton.Size = new System.Drawing.Size(52, 17);
            this.moveRadioButton.TabIndex = 31;
            this.moveRadioButton.TabStop = true;
            this.moveRadioButton.Text = "Move";
            this.moveRadioButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(146, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1143, 779);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equalLengthToolStripMenuItem,
            this.setLengthToolStripMenuItem,
            this.parallelToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(159, 70);
            // 
            // equalLengthToolStripMenuItem
            // 
            this.equalLengthToolStripMenuItem.Name = "equalLengthToolStripMenuItem";
            this.equalLengthToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.equalLengthToolStripMenuItem.Text = "Równa długość";
            this.equalLengthToolStripMenuItem.Click += new System.EventHandler(this.equalLengthToolStripMenuItem_Click);
            // 
            // setLengthToolStripMenuItem
            // 
            this.setLengthToolStripMenuItem.Name = "setLengthToolStripMenuItem";
            this.setLengthToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.setLengthToolStripMenuItem.Text = "Zadana długość";
            this.setLengthToolStripMenuItem.Click += new System.EventHandler(this.setLengthToolStripMenuItem_Click);
            // 
            // parallelToolStripMenuItem
            // 
            this.parallelToolStripMenuItem.Name = "parallelToolStripMenuItem";
            this.parallelToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.parallelToolStripMenuItem.Text = "Równoległe";
            this.parallelToolStripMenuItem.Click += new System.EventHandler(this.parallelToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockCircleToolStripMenuItem1,
            this.lockRadiusToolStripMenuItem,
            this.unlockCircleToolStripMenuItem,
            this.unlockRadiusToolStripMenuItem,
            this.tangentToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(172, 114);
            // 
            // lockCircleToolStripMenuItem1
            // 
            this.lockCircleToolStripMenuItem1.Name = "lockCircleToolStripMenuItem1";
            this.lockCircleToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.lockCircleToolStripMenuItem1.Text = "Zablokuj";
            this.lockCircleToolStripMenuItem1.Click += new System.EventHandler(this.lockCircleToolStripMenuItem1_Click);
            // 
            // lockRadiusToolStripMenuItem
            // 
            this.lockRadiusToolStripMenuItem.Name = "lockRadiusToolStripMenuItem";
            this.lockRadiusToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.lockRadiusToolStripMenuItem.Text = "Zablokuj promień";
            this.lockRadiusToolStripMenuItem.Click += new System.EventHandler(this.lockRadiusToolStripMenuItem_Click);
            // 
            // unlockCircleToolStripMenuItem
            // 
            this.unlockCircleToolStripMenuItem.Name = "unlockCircleToolStripMenuItem";
            this.unlockCircleToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.unlockCircleToolStripMenuItem.Text = "Odblokuj";
            this.unlockCircleToolStripMenuItem.Click += new System.EventHandler(this.unlockCircleToolStripMenuItem_Click);
            // 
            // unlockRadiusToolStripMenuItem
            // 
            this.unlockRadiusToolStripMenuItem.Name = "unlockRadiusToolStripMenuItem";
            this.unlockRadiusToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.unlockRadiusToolStripMenuItem.Text = "Odblokuj promień";
            this.unlockRadiusToolStripMenuItem.Click += new System.EventHandler(this.unlockRadiusToolStripMenuItem_Click);
            // 
            // tangentToolStripMenuItem
            // 
            this.tangentToolStripMenuItem.Name = "tangentToolStripMenuItem";
            this.tangentToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.tangentToolStripMenuItem.Text = "Styczne";
            this.tangentToolStripMenuItem.Click += new System.EventHandler(this.tangentToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 785);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainWindow";
            this.Text = "Editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.editPolygonGroupBox.ResumeLayout(false);
            this.editPolygonGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem equalLengthToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem lockCircleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lockRadiusToolStripMenuItem;
        private System.Windows.Forms.GroupBox editPolygonGroupBox;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripMenuItem setLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parallelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tangentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockCircleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockRadiusToolStripMenuItem;
        private System.Windows.Forms.RadioButton moveRadioButton;
        private System.Windows.Forms.RadioButton divideEdgeRadioButton;
        private System.Windows.Forms.RadioButton changeRadiusRadioButton;
        private System.Windows.Forms.RadioButton addPolygonRadioButton;
        private System.Windows.Forms.RadioButton addCircleRadioButton;
        private System.Windows.Forms.RadioButton deletePolygonRadioButton;
        private System.Windows.Forms.RadioButton deleteVertexRadioButton;
        private System.Windows.Forms.RadioButton deleteCircleRadioButton;
        private System.Windows.Forms.RadioButton deleteRelationRadioButton;
        private System.Windows.Forms.CheckBox stycznaCheckBox;
    }
}


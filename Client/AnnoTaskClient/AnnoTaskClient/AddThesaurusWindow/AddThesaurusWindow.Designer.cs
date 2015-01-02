namespace AnnoTaskClient
{
    partial class AddThesaurusWindow
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
			this.ConceptFromComboBox = new System.Windows.Forms.ComboBox();
			this.ConceptToComboBox = new System.Windows.Forms.ComboBox();
			this.MetaOntologyComboBox = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.linkedList = new System.Windows.Forms.ListBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.addThesaurusButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.ConceptToTextBox = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ConceptFromComboBox
			// 
			this.ConceptFromComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.ConceptFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ConceptFromComboBox.FormattingEnabled = true;
			this.ConceptFromComboBox.Location = new System.Drawing.Point(38, 45);
			this.ConceptFromComboBox.Name = "ConceptFromComboBox";
			this.ConceptFromComboBox.Size = new System.Drawing.Size(206, 21);
			this.ConceptFromComboBox.TabIndex = 0;
			this.ConceptFromComboBox.SelectedIndexChanged += new System.EventHandler(this.ConceptFromComboBox_SelectedIndexChanged);
			// 
			// ConceptToComboBox
			// 
			this.ConceptToComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this.ConceptToComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.ConceptToComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ConceptToComboBox.FormattingEnabled = true;
			this.ConceptToComboBox.Location = new System.Drawing.Point(38, 123);
			this.ConceptToComboBox.Name = "ConceptToComboBox";
			this.ConceptToComboBox.Size = new System.Drawing.Size(206, 21);
			this.ConceptToComboBox.TabIndex = 1;
			this.ConceptToComboBox.SelectionChangeCommitted += new System.EventHandler(this.ConceptToComboBox_SelectionChangeCommitted);
			// 
			// MetaOntologyComboBox
			// 
			this.MetaOntologyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MetaOntologyComboBox.FormattingEnabled = true;
			this.MetaOntologyComboBox.Items.AddRange(new object[] {
            " ",
            "Agent",
            "Knowledge",
            "Event",
            "Task",
            "Resource",
            "Location",
            "Organization",
            "Action",
            "Role",
            "Unknown"});
			this.MetaOntologyComboBox.Location = new System.Drawing.Point(263, 122);
			this.MetaOntologyComboBox.Name = "MetaOntologyComboBox";
			this.MetaOntologyComboBox.Size = new System.Drawing.Size(200, 21);
			this.MetaOntologyComboBox.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.linkedList);
			this.panel1.Location = new System.Drawing.Point(38, 192);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(206, 210);
			this.panel1.TabIndex = 3;
			// 
			// linkedList
			// 
			this.linkedList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkedList.FormattingEnabled = true;
			this.linkedList.Location = new System.Drawing.Point(0, 0);
			this.linkedList.Name = "linkedList";
			this.linkedList.Size = new System.Drawing.Size(206, 210);
			this.linkedList.TabIndex = 0;
			this.linkedList.Click += new System.EventHandler(this.linkedList_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(263, 192);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
			this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
			this.splitContainer1.Size = new System.Drawing.Size(477, 210);
			this.splitContainer1.SplitterDistance = 203;
			this.splitContainer1.TabIndex = 4;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(203, 210);
			this.treeView1.TabIndex = 0;
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
			this.richTextBox1.Location = new System.Drawing.Point(3, 4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(264, 203);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// addThesaurusButton
			// 
			this.addThesaurusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.addThesaurusButton.Location = new System.Drawing.Point(263, 419);
			this.addThesaurusButton.Name = "addThesaurusButton";
			this.addThesaurusButton.Size = new System.Drawing.Size(200, 48);
			this.addThesaurusButton.TabIndex = 5;
			this.addThesaurusButton.Text = "시소러스 추가하기";
			this.addThesaurusButton.UseVisualStyleBackColor = true;
			this.addThesaurusButton.Click += new System.EventHandler(this.addThesaurusButton_Click_1);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(38, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "ConceptFrom";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(38, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "ConceptTo";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(263, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "Meta Ontology";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(38, 173);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "Linked List";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label5.Location = new System.Drawing.Point(263, 173);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 15);
			this.label5.TabIndex = 10;
			this.label5.Text = "문서리스트";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label6.Location = new System.Drawing.Point(470, 173);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 15);
			this.label6.TabIndex = 11;
			this.label6.Text = "기사전문";
			// 
			// ConceptToTextBox
			// 
			this.ConceptToTextBox.Location = new System.Drawing.Point(38, 124);
			this.ConceptToTextBox.Name = "ConceptToTextBox";
			this.ConceptToTextBox.Size = new System.Drawing.Size(206, 20);
			this.ConceptToTextBox.TabIndex = 12;
			this.ConceptToTextBox.TextChanged += new System.EventHandler(this.ConceptToTextBox_TextChanged);
			this.ConceptToTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ConceptToTextBox_KeyUp);
			// 
			// AddThesaurusWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(786, 485);
			this.Controls.Add(this.ConceptToTextBox);
			this.Controls.Add(this.ConceptToComboBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.addThesaurusButton);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.MetaOntologyComboBox);
			this.Controls.Add(this.ConceptFromComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddThesaurusWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Add Thesaurus Window";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddThesaurusWindow_FormClosed);
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox ConceptFromComboBox;
        public System.Windows.Forms.ComboBox ConceptToComboBox;
        public System.Windows.Forms.ComboBox MetaOntologyComboBox;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button addThesaurusButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ListBox linkedList;
		public System.Windows.Forms.TextBox ConceptToTextBox;
    }
}

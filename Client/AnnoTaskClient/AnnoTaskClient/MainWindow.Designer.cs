using AnnoTaskClient.Logic;

namespace AnnoTaskClient
{
	partial class MainWindow
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnJobStart = new System.Windows.Forms.Button();
            this.docCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.termCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.로그인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.로그아웃ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wordList1 = new System.Windows.Forms.DataGridView();
            this.check1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.빈도수 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.출현문서수 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.docList1 = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.article1 = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.wordList2 = new System.Windows.Forms.DataGridView();
            this.check2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Term2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.빈도수2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.출현문서수2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.docList2 = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.article2 = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.vScrollBar5 = new System.Windows.Forms.VScrollBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.wordList3 = new System.Windows.Forms.DataGridView();
            this.check3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Term3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.빈도수3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.출현문서수3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.docList3 = new System.Windows.Forms.TreeView();
            this.label12 = new System.Windows.Forms.Label();
            this.article3 = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.vScrollBar9 = new System.Windows.Forms.VScrollBar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.wordList4 = new System.Windows.Forms.DataGridView();
            this.check4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Term4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.빈도수4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.출현문서수4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.docList4 = new System.Windows.Forms.TreeView();
            this.label15 = new System.Windows.Forms.Label();
            this.article4 = new System.Windows.Forms.RichTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.vScrollBar12 = new System.Windows.Forms.VScrollBar();
            this.openAddThesaurusWindowButton = new System.Windows.Forms.Button();
            this.addDeleteListButton = new System.Windows.Forms.Button();
            this.frequencyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnJobStart
            // 
            this.btnJobStart.Enabled = false;
            this.btnJobStart.Location = new System.Drawing.Point(14, 46);
            this.btnJobStart.Name = "btnJobStart";
            this.btnJobStart.Size = new System.Drawing.Size(176, 23);
            this.btnJobStart.TabIndex = 16;
            this.btnJobStart.Text = "작업 시작하기";
            this.btnJobStart.UseVisualStyleBackColor = true;
            this.btnJobStart.Click += new System.EventHandler(this.btnJobStart_Click);
            // 
            // docCount
            // 
            this.docCount.AutoSize = true;
            this.docCount.Cursor = System.Windows.Forms.Cursors.Default;
            this.docCount.Location = new System.Drawing.Point(850, 51);
            this.docCount.Name = "docCount";
            this.docCount.Size = new System.Drawing.Size(13, 13);
            this.docCount.TabIndex = 22;
            this.docCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(792, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "총 문서수:";
            // 
            // termCount
            // 
            this.termCount.AutoSize = true;
            this.termCount.Location = new System.Drawing.Point(936, 51);
            this.termCount.Name = "termCount";
            this.termCount.Size = new System.Drawing.Size(13, 13);
            this.termCount.TabIndex = 20;
            this.termCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(879, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "총 단어수:";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.progressLabel.Location = new System.Drawing.Point(739, 49);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(25, 15);
            this.progressLabel.TabIndex = 18;
            this.progressLabel.Text = "0%";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(196, 46);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(537, 23);
            this.progressBar.TabIndex = 17;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(968, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.로그인ToolStripMenuItem,
            this.로그아웃ToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileMenuItem.Text = "파일";
            // 
            // 로그인ToolStripMenuItem
            // 
            this.로그인ToolStripMenuItem.Name = "로그인ToolStripMenuItem";
            this.로그인ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.로그인ToolStripMenuItem.Text = "로그인";
            this.로그인ToolStripMenuItem.Click += new System.EventHandler(this.로그인ToolStripMenuItem_Click);
            // 
            // 로그아웃ToolStripMenuItem
            // 
            this.로그아웃ToolStripMenuItem.Name = "로그아웃ToolStripMenuItem";
            this.로그아웃ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.로그아웃ToolStripMenuItem.Text = "로그아웃";
            this.로그아웃ToolStripMenuItem.Click += new System.EventHandler(this.로그아웃ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(14, 93);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(952, 440);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.vScrollBar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(944, 414);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "1-gram";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wordList1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(938, 411);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 1;
            // 
            // wordList1
            // 
            this.wordList1.AllowUserToAddRows = false;
            this.wordList1.AllowUserToOrderColumns = true;
            this.wordList1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.wordList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordList1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check1,
            this.Term,
            this.빈도수,
            this.출현문서수});
            this.wordList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wordList1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wordList1.Location = new System.Drawing.Point(0, 22);
            this.wordList1.Name = "wordList1";
            this.wordList1.Size = new System.Drawing.Size(326, 385);
            this.wordList1.TabIndex = 1;
            this.wordList1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wordList1_CellClick);
            // 
            // check1
            // 
            this.check1.HeaderText = " ";
            this.check1.Name = "check1";
            this.check1.Width = 40;
            // 
            // Term
            // 
            this.Term.HeaderText = "Term";
            this.Term.Name = "Term";
            this.Term.ReadOnly = true;
            this.Term.Width = 60;
            // 
            // 빈도수
            // 
            this.빈도수.HeaderText = "빈도수";
            this.빈도수.Name = "빈도수";
            this.빈도수.ReadOnly = true;
            this.빈도수.Width = 90;
            // 
            // 출현문서수
            // 
            this.출현문서수.HeaderText = "출현문서수";
            this.출현문서수.Name = "출현문서수";
            this.출현문서수.ReadOnly = true;
            this.출현문서수.Width = 90;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "단어리스트";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.docList1);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.article1);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Size = new System.Drawing.Size(604, 411);
            this.splitContainer2.SplitterDistance = 291;
            this.splitContainer2.TabIndex = 0;
            // 
            // docList1
            // 
            this.docList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.docList1.Location = new System.Drawing.Point(0, 22);
            this.docList1.Name = "docList1";
            this.docList1.Size = new System.Drawing.Size(287, 385);
            this.docList1.TabIndex = 1;
            this.docList1.DoubleClick += new System.EventHandler(this.docList1_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "문서리스트";
            // 
            // article1
            // 
            this.article1.AcceptsTab = true;
            this.article1.BackColor = System.Drawing.SystemColors.Window;
            this.article1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.article1.Location = new System.Drawing.Point(0, 22);
            this.article1.Name = "article1";
            this.article1.ReadOnly = true;
            this.article1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.article1.Size = new System.Drawing.Size(305, 385);
            this.article1.TabIndex = 1;
            this.article1.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "기사전문";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(1380, 40);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 412);
            this.vScrollBar1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Controls.Add(this.vScrollBar5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(944, 414);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "2-gram";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.CausesValidation = false;
            this.splitContainer3.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.wordList2);
            this.splitContainer3.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(938, 411);
            this.splitContainer3.SplitterDistance = 330;
            this.splitContainer3.TabIndex = 11;
            // 
            // wordList2
            // 
            this.wordList2.AllowUserToAddRows = false;
            this.wordList2.AllowUserToOrderColumns = true;
            this.wordList2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.wordList2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordList2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check2,
            this.Term2,
            this.빈도수2,
            this.출현문서수2});
            this.wordList2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wordList2.Location = new System.Drawing.Point(0, 22);
            this.wordList2.Name = "wordList2";
            this.wordList2.Size = new System.Drawing.Size(326, 385);
            this.wordList2.TabIndex = 1;
            this.wordList2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wordList2_CellClick);
            // 
            // check2
            // 
            this.check2.HeaderText = " ";
            this.check2.Name = "check2";
            this.check2.Width = 40;
            // 
            // Term2
            // 
            this.Term2.HeaderText = "Term";
            this.Term2.Name = "Term2";
            this.Term2.Width = 60;
            // 
            // 빈도수2
            // 
            this.빈도수2.HeaderText = "빈도수";
            this.빈도수2.Name = "빈도수2";
            this.빈도수2.Width = 90;
            // 
            // 출현문서수2
            // 
            this.출현문서수2.HeaderText = "출현문서수";
            this.출현문서수2.Name = "출현문서수2";
            this.출현문서수2.Width = 90;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(4, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "단어리스트";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.docList2);
            this.splitContainer4.Panel1.Controls.Add(this.label9);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.article2);
            this.splitContainer4.Panel2.Controls.Add(this.label10);
            this.splitContainer4.Size = new System.Drawing.Size(604, 411);
            this.splitContainer4.SplitterDistance = 291;
            this.splitContainer4.TabIndex = 0;
            // 
            // docList2
            // 
            this.docList2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.docList2.Location = new System.Drawing.Point(0, 22);
            this.docList2.Name = "docList2";
            this.docList2.Size = new System.Drawing.Size(287, 385);
            this.docList2.TabIndex = 1;
            this.docList2.DoubleClick += new System.EventHandler(this.docList2_DoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "문서리스트";
            // 
            // article2
            // 
            this.article2.AcceptsTab = true;
            this.article2.BackColor = System.Drawing.SystemColors.Window;
            this.article2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.article2.Location = new System.Drawing.Point(0, 22);
            this.article2.Name = "article2";
            this.article2.ReadOnly = true;
            this.article2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.article2.Size = new System.Drawing.Size(305, 385);
            this.article2.TabIndex = 1;
            this.article2.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "기사전문";
            // 
            // vScrollBar5
            // 
            this.vScrollBar5.Location = new System.Drawing.Point(1380, 40);
            this.vScrollBar5.Name = "vScrollBar5";
            this.vScrollBar5.Size = new System.Drawing.Size(17, 412);
            this.vScrollBar5.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer5);
            this.tabPage3.Controls.Add(this.vScrollBar9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(944, 414);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "3-gram";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.CausesValidation = false;
            this.splitContainer5.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.wordList3);
            this.splitContainer5.Panel1.Controls.Add(this.label11);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(938, 411);
            this.splitContainer5.SplitterDistance = 330;
            this.splitContainer5.TabIndex = 12;
            // 
            // wordList3
            // 
            this.wordList3.AllowUserToAddRows = false;
            this.wordList3.AllowUserToOrderColumns = true;
            this.wordList3.BackgroundColor = System.Drawing.SystemColors.Window;
            this.wordList3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordList3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check3,
            this.Term3,
            this.빈도수3,
            this.출현문서수3});
            this.wordList3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wordList3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wordList3.Location = new System.Drawing.Point(0, 22);
            this.wordList3.Name = "wordList3";
            this.wordList3.Size = new System.Drawing.Size(326, 385);
            this.wordList3.TabIndex = 1;
            this.wordList3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wordList3_CellClick);
            // 
            // check3
            // 
            this.check3.HeaderText = " ";
            this.check3.Name = "check3";
            this.check3.Width = 40;
            // 
            // Term3
            // 
            this.Term3.HeaderText = "Term";
            this.Term3.Name = "Term3";
            this.Term3.Width = 60;
            // 
            // 빈도수3
            // 
            this.빈도수3.HeaderText = "빈도수";
            this.빈도수3.Name = "빈도수3";
            this.빈도수3.Width = 90;
            // 
            // 출현문서수3
            // 
            this.출현문서수3.HeaderText = "출현문서수";
            this.출현문서수3.Name = "출현문서수3";
            this.출현문서수3.Width = 90;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(4, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "단어리스트";
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.docList3);
            this.splitContainer6.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.article3);
            this.splitContainer6.Panel2.Controls.Add(this.label13);
            this.splitContainer6.Size = new System.Drawing.Size(604, 411);
            this.splitContainer6.SplitterDistance = 291;
            this.splitContainer6.TabIndex = 0;
            // 
            // docList3
            // 
            this.docList3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.docList3.Location = new System.Drawing.Point(0, 22);
            this.docList3.Name = "docList3";
            this.docList3.Size = new System.Drawing.Size(287, 385);
            this.docList3.TabIndex = 1;
            this.docList3.DoubleClick += new System.EventHandler(this.docList3_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(3, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 15);
            this.label12.TabIndex = 0;
            this.label12.Text = "문서리스트";
            // 
            // article3
            // 
            this.article3.AcceptsTab = true;
            this.article3.BackColor = System.Drawing.SystemColors.Window;
            this.article3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.article3.Location = new System.Drawing.Point(0, 22);
            this.article3.Name = "article3";
            this.article3.ReadOnly = true;
            this.article3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.article3.Size = new System.Drawing.Size(305, 385);
            this.article3.TabIndex = 1;
            this.article3.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "기사전문";
            // 
            // vScrollBar9
            // 
            this.vScrollBar9.Location = new System.Drawing.Point(1380, 40);
            this.vScrollBar9.Name = "vScrollBar9";
            this.vScrollBar9.Size = new System.Drawing.Size(17, 412);
            this.vScrollBar9.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer7);
            this.tabPage4.Controls.Add(this.vScrollBar12);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(944, 414);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "4-gram";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer7
            // 
            this.splitContainer7.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer7.CausesValidation = false;
            this.splitContainer7.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer7.Location = new System.Drawing.Point(3, 3);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.wordList4);
            this.splitContainer7.Panel1.Controls.Add(this.label14);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer7.Size = new System.Drawing.Size(938, 411);
            this.splitContainer7.SplitterDistance = 330;
            this.splitContainer7.TabIndex = 12;
            // 
            // wordList4
            // 
            this.wordList4.AllowUserToAddRows = false;
            this.wordList4.AllowUserToOrderColumns = true;
            this.wordList4.BackgroundColor = System.Drawing.SystemColors.Window;
            this.wordList4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordList4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check4,
            this.Term4,
            this.빈도수4,
            this.출현문서수4});
            this.wordList4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wordList4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wordList4.Location = new System.Drawing.Point(0, 22);
            this.wordList4.Name = "wordList4";
            this.wordList4.Size = new System.Drawing.Size(326, 385);
            this.wordList4.TabIndex = 1;
            this.wordList4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wordList4_CellClick);
            // 
            // check4
            // 
            this.check4.HeaderText = " ";
            this.check4.Name = "check4";
            this.check4.Width = 40;
            // 
            // Term4
            // 
            this.Term4.HeaderText = "Term";
            this.Term4.Name = "Term4";
            this.Term4.Width = 90;
            // 
            // 빈도수4
            // 
            this.빈도수4.HeaderText = "빈도수";
            this.빈도수4.Name = "빈도수4";
            // 
            // 출현문서수4
            // 
            this.출현문서수4.HeaderText = "출현문서수";
            this.출현문서수4.Name = "출현문서수4";
            this.출현문서수4.Width = 90;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(4, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 15);
            this.label14.TabIndex = 0;
            this.label14.Text = "단어리스트";
            // 
            // splitContainer8
            // 
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.docList4);
            this.splitContainer8.Panel1.Controls.Add(this.label15);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.article4);
            this.splitContainer8.Panel2.Controls.Add(this.label16);
            this.splitContainer8.Size = new System.Drawing.Size(604, 411);
            this.splitContainer8.SplitterDistance = 291;
            this.splitContainer8.TabIndex = 0;
            // 
            // docList4
            // 
            this.docList4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.docList4.Location = new System.Drawing.Point(0, 22);
            this.docList4.Name = "docList4";
            this.docList4.Size = new System.Drawing.Size(287, 385);
            this.docList4.TabIndex = 1;
            this.docList4.DoubleClick += new System.EventHandler(this.docList4_DoubleClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(3, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 15);
            this.label15.TabIndex = 0;
            this.label15.Text = "문서리스트";
            // 
            // article4
            // 
            this.article4.AcceptsTab = true;
            this.article4.BackColor = System.Drawing.SystemColors.Window;
            this.article4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.article4.Location = new System.Drawing.Point(0, 22);
            this.article4.Name = "article4";
            this.article4.ReadOnly = true;
            this.article4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.article4.Size = new System.Drawing.Size(305, 385);
            this.article4.TabIndex = 1;
            this.article4.Text = "";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.Location = new System.Drawing.Point(3, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "기사전문";
            // 
            // vScrollBar12
            // 
            this.vScrollBar12.Location = new System.Drawing.Point(1380, 40);
            this.vScrollBar12.Name = "vScrollBar12";
            this.vScrollBar12.Size = new System.Drawing.Size(17, 412);
            this.vScrollBar12.TabIndex = 10;
            // 
            // openAddThesaurusWindowButton
            // 
            this.openAddThesaurusWindowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.openAddThesaurusWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openAddThesaurusWindowButton.Location = new System.Drawing.Point(14, 539);
            this.openAddThesaurusWindowButton.Name = "openAddThesaurusWindowButton";
            this.openAddThesaurusWindowButton.Size = new System.Drawing.Size(155, 37);
            this.openAddThesaurusWindowButton.TabIndex = 25;
            this.openAddThesaurusWindowButton.Text = "시소러스 추가";
            this.openAddThesaurusWindowButton.UseVisualStyleBackColor = true;
            this.openAddThesaurusWindowButton.Click += new System.EventHandler(this.openAddThesaurusWindowButton_Click);
            // 
            // addDeleteListButton
            // 
            this.addDeleteListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.addDeleteListButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addDeleteListButton.Location = new System.Drawing.Point(193, 539);
            this.addDeleteListButton.Name = "addDeleteListButton";
            this.addDeleteListButton.Size = new System.Drawing.Size(156, 37);
            this.addDeleteListButton.TabIndex = 26;
            this.addDeleteListButton.Text = "불용어 추가";
            this.addDeleteListButton.UseVisualStyleBackColor = true;
            this.addDeleteListButton.Click += new System.EventHandler(this.addDeleteListButton_Click);
            // 
            // frequencyBindingSource
            // 
            this.frequencyBindingSource.DataSource = typeof(AnnoTaskClient.Logic.Frequency);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 579);
            this.Controls.Add(this.addDeleteListButton);
            this.Controls.Add(this.openAddThesaurusWindowButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.docCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.termCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnJobStart);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnnoTask Phase 1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wordList1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wordList2)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wordList3)).EndInit();
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wordList4)).EndInit();
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.frequencyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
		//private System.Windows.Forms.ToolStripMenuItem 불러오기ToolStripMenuItem;
		public System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.VScrollBar vScrollBar5;
        public System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.VScrollBar vScrollBar9;
        public System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.VScrollBar vScrollBar12;
        public System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.RichTextBox article1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.SplitContainer splitContainer6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.SplitContainer splitContainer7;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.SplitContainer splitContainer8;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		public System.Windows.Forms.DateTimePicker startDate;
		public System.Windows.Forms.DateTimePicker endDate;
		public System.Windows.Forms.CheckBox bNate;
		public System.Windows.Forms.CheckBox bDaum;
		public System.Windows.Forms.CheckBox bNaver;
		public System.Windows.Forms.Label docCount;
		public System.Windows.Forms.Label termCount;
		public System.Windows.Forms.ProgressBar progressBar;
		public System.Windows.Forms.DataGridView wordList1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn check1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Term;
		private System.Windows.Forms.DataGridViewTextBoxColumn 빈도수;
		private System.Windows.Forms.DataGridViewTextBoxColumn 출현문서수;
		private System.Windows.Forms.BindingSource frequencyBindingSource;
		public System.Windows.Forms.TreeView docList1;
		public System.Windows.Forms.Label progressLabel;
		public System.Windows.Forms.DataGridView wordList2;
		public System.Windows.Forms.TreeView docList2;
        public System.Windows.Forms.RichTextBox article2;
		public System.Windows.Forms.DataGridView wordList3;
		public System.Windows.Forms.TreeView docList3;
        public System.Windows.Forms.RichTextBox article3;
		public System.Windows.Forms.TabControl tabControl1;
		public System.Windows.Forms.TreeView docList4;
        public System.Windows.Forms.RichTextBox article4;
		public System.Windows.Forms.DataGridView wordList4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Term2;
		private System.Windows.Forms.DataGridViewTextBoxColumn 빈도수2;
		private System.Windows.Forms.DataGridViewTextBoxColumn 출현문서수2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Term3;
		private System.Windows.Forms.DataGridViewTextBoxColumn 빈도수3;
		private System.Windows.Forms.DataGridViewTextBoxColumn 출현문서수3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Term4;
		private System.Windows.Forms.DataGridViewTextBoxColumn 빈도수4;
		private System.Windows.Forms.DataGridViewTextBoxColumn 출현문서수4;
		public System.Windows.Forms.Button btnJobStart;
        public System.Windows.Forms.Button openAddThesaurusWindowButton;
        private System.Windows.Forms.Button addDeleteListButton;
        private System.Windows.Forms.ToolStripMenuItem 로그인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 로그아웃ToolStripMenuItem;
	}
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using AnnoTaskClient.UIController;
using AnnoTaskClient.Logic;
using System.Collections;

namespace AnnoTaskClient
{
	public partial class MainWindow : Form
	{
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
		private Thread mainLogicWorker;
        public LoginPage loginPage
        {
            get;
            set;
        }
        
        private MainLogic logic = UIHandler.Instance.logic;
        public MainLogic getMainLogic()
        {
            return this.logic;
        }

		public MainWindow()
		{
			InitializeComponent();

			this.Disposed += exitWindow;

			mainLogicWorker = new Thread(new ThreadStart(logic.doWork));
			mainLogicWorker.Name = "MainLogic";
			mainLogicWorker.Start();
			UIHandler.Instance.runUIHandler(this);

            // (기흥) Login Page 뛰우기
            startLogin();

		}

		// 종료 이벤트 핸들러
		private void exitWindow(object sender, EventArgs e)
		{
			// 메인로직 쓰레드 끊어줌.
			logic.KillLogicThread();
			logic = null;
		}

		private void clearUIContents()
		{
			this.wordList1.Rows.Clear();
			this.wordList2.Rows.Clear();
			this.wordList3.Rows.Clear();
			this.wordList4.Rows.Clear();

			this.docList1.Nodes.Clear();
			this.docList2.Nodes.Clear();
			this.docList3.Nodes.Clear();
			this.docList4.Nodes.Clear();

			this.article1.Text = "";
			this.article2.Text = "";
			this.article3.Text = "";
			this.article4.Text = "";
		}

		/// <summary>
		/// 작성자 : 이승철
		/// 작성일 : 20141231
		/// 메인 윈도우 모든 버튼을 활성화로
		/// </summary>
		public void AllButtonEnable()
		{
			this.btnJobStart.Enabled = true;
			this.openAddThesaurusWindowButton.Enabled = true;
			this.addDeleteListButton.Enabled = true;
		}

		/// <summary>
		/// 작성자 : 이승철
		/// 작성일 : 20141231
		/// 메인 윈도우 모든 버튼을 비활성화로
		/// </summary>
		public void AllButtonDisable()
		{
			this.btnJobStart.Enabled = false;
			this.openAddThesaurusWindowButton.Enabled = false;
			this.addDeleteListButton.Enabled = false;
		}

        /// <summary>
        /// 작성자 : 박기흥
        /// 작성일 : 20150122
        /// AnnoTask 실행 시 로그인 프로시져 부터 동작하도록...
        /// </summary>
        private void startLogin()
        {
            loginPage = new LoginPage();
            loginPage.Owner = this;
            this.Enabled = false;

            loginPage.Show();
        }

		private void btnJobStartHandler()
		{
			clearUIContents();
			logic.clickedJobStart();
		}

        // (기흥) "작업 시작하기" 버튼 클릭 시
		private void btnJobStart_Click(object sender, EventArgs e)
		{
			// 이승철 추가, 20141231, 시작버튼 눌렀을때 정말 할껀지 안내창 띄워줌.
			// 사용자 선택후 진행.
			string msg = "새로운 작업을 시작하시겠습니까?\r\n" +
						 "('예'를 클릭하시면 화면에 보이는 모든 사항이 초기화 됩니다.)";
			DialogResult result = MessageBox.Show(msg, "새로운 작업 시작", MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes)
			{
				// (기흥) btnImportDoc -> btnJobStart 수정함.
				AllButtonDisable();
				btnJobStartHandler();
			}
			else
			{
				// 아무작업안함.
			}
		}

		/// <summary>
		/// 수정자 : 이승철
		/// 수정내용 : 시소러스 추가 창을 열기전 선택된 Tab check, 선택된 term 인자로 전달.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void openAddThesaurusWindowButton_Click(object sender, EventArgs e)
        {
			// 메인 윈도우 비활성화
			this.Enabled = false;

			int selectedTab = -1;
            List<string> selectedTerms = new List<string>();
            if (this.tabControl1.SelectedTab == this.tabPage1)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram1.getMainWindow().wordList1.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
				selectedTab = 1;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram2.getMainWindow().wordList2.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
				selectedTab = 2;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram3.getMainWindow().wordList3.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
				selectedTab = 3;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage4)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram4.getMainWindow().wordList4.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
				selectedTab = 4;
            }
          
            // 20141219 이승철 수정.
			if (selectedTerms.Count == 0)
			{
				MessageBox.Show("선택된 단어가 없습니다.\r\n" +
								"단어 선택 후 Thesaurus 추가를 진행해주십시오.");
				return;
			}

			addThesaurusWindow(selectedTerms, selectedTab);
			//logic.OpenThesaurusWindow(selectedTerms);
        }

		/// <summary>
		/// 시소러스 추가 창을 열어줌.
		/// </summary>
		private void addThesaurusWindow(List<string> selectedTerms, int selectedTab)
		{
			// 이승철 추가 20141219
			AddThesaurusWindow addThesaurus = new AddThesaurusWindow(selectedTerms, selectedTab);
			addThesaurus.Owner = this;
			
			// 시소러스 창 위치 지정.			
			// 창 겹치는 부분에서의 내부 옵셋 구함.
			int x = (this.Size.Width - addThesaurus.Size.Width) / 2;
			int y = (this.Size.Height - addThesaurus.Size.Height) / 2;
			// 메인창 위치를 기준으로 위에서 구한 옵셋 더함.
			x += this.Location.X;
			y += this.Location.Y;
			addThesaurus.Location = new Point(x, y);

			// 자식창 쓰레드 분리 지점.
			addThesaurus.Show();
		}

		private void cellClickHandler(string cellValue, int tabNumber)
		{
			// TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
			logic.DocListUpdate(cellValue, tabNumber);
		}

		private void checkHandler(int cellRow, int cellCol, int tabNum)
        {
            // TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
            logic.cellContentCheck(cellRow, cellCol, tabNum);
        }

      	private void wordList1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			int cellRow = e.RowIndex;
			int cellCol = e.ColumnIndex;

			_wordList_CellClick(dataGridView, cellRow, cellCol, 1);			
		}

		private void wordList2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			int cellRow = e.RowIndex;
			int cellCol = e.ColumnIndex;

			_wordList_CellClick(dataGridView, cellRow, cellCol, 2);
		}

		private void wordList3_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			int cellRow = e.RowIndex;
			int cellCol = e.ColumnIndex;

			_wordList_CellClick(dataGridView, cellRow, cellCol, 3);
		}

		private void wordList4_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			int cellRow = e.RowIndex;
			int cellCol = e.ColumnIndex;

			_wordList_CellClick(dataGridView, cellRow, cellCol, 4);
		}

		/// <summary>
		/// 동작방식 리펙토링.
		/// 이승철, 20141220
		/// </summary>
		/// <param name="dataGridView"></param>
		/// <param name="cellRow"></param>
		/// <param name="cellCol"></param>
		/// <param name="tabNum"></param>
		private void _wordList_CellClick(DataGridView dataGridView, int cellRow, int cellCol, int tabNum)
		{
			// 이승철 추가, 20141220, 컬럼이름 필드 클릭시 이벤트 걸림방지.
			if (cellRow < 0)
			{
				return;
			}

			if (cellCol == 1)
			{
				cellClickHandler((string)dataGridView.Rows[cellRow].Cells[1].Value, tabNum);
			}
			else if (cellCol == 0)
			{
				checkHandler(cellRow, cellCol, tabNum);
			}
		}

		// TODO : 아래 4개 이벤트들 쓰레드 안돌려도 된다? 작업이 짧아서?
		private void docList1_DoubleClick(object sender, EventArgs e)
		{
			UIHandler.Instance.UtilForUI.AfterDocListClickHandler((sender as TreeView).SelectedNode, this.article1);
		}
        
		private void docList2_DoubleClick(object sender, EventArgs e)
		{
			UIHandler.Instance.UtilForUI.AfterDocListClickHandler((sender as TreeView).SelectedNode, this.article2);
		}

		private void docList3_DoubleClick(object sender, EventArgs e)
		{
			UIHandler.Instance.UtilForUI.AfterDocListClickHandler((sender as TreeView).SelectedNode, this.article3);
		}

		private void docList4_DoubleClick(object sender, EventArgs e)
		{
			UIHandler.Instance.UtilForUI.AfterDocListClickHandler((sender as TreeView).SelectedNode, this.article4);
		}

        private void addDeleteListButton_Click(object sender, EventArgs e)
        {
			AllButtonDisable();

            List<string> selectedTerms = new List<string>();
            int tabNum = 0;

            if (this.tabControl1.SelectedTab == this.tabPage1)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram1.getMainWindow().wordList1.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
                tabNum = 1;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram2.getMainWindow().wordList2.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
                tabNum = 2;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram3.getMainWindow().wordList3.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
                tabNum = 3;
            }
            else if (this.tabControl1.SelectedTab == this.tabPage4)
            {
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram4.getMainWindow().wordList4.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
                tabNum = 4;
            }


            logic.clickedAddDeleteList(selectedTerms, tabNum);
        }

        private void 로그인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // (기흥) Login Page 뛰우기
            startLogin();
        }

        private void 로그아웃ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 서버 연결 끊기

            // 메인 윈도우 초기화

            // 메인 윈도우 사용자 이름 지우기

            // 로그인 창 새로 띄우기
            startLogin();
        }
	}
}

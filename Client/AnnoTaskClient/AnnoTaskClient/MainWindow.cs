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

namespace AnnoTaskClient
{
	public partial class MainWindow : Form
	{        
		private Thread mainLogicWorker;
     
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

			mainLogicWorker.Start();
			UIHandler.Instance.runUIHandler(this);
		}

		// 종료 이벤트 핸들러
		private void exitWindow(object sender, EventArgs e)
		{
			// 메인로직 쓰레드 끊어줌. 
			//mainLogicWorker.Abort();
			logic = null;
		}

		private void clearUIContents()
		{
			this.wordList1.Rows.Clear();
			this.wordList2.Rows.Clear();
			this.wordList3.Rows.Clear();
			this.wordList4.Rows.Clear();

			this.article1.Text = "";
			this.article2.Text = "";
			this.article3.Text = "";
			this.article4.Text = "";
		}

		
		private string getArticle(object sender)
		{
			TreeNode node = (sender as TreeView).SelectedNode;

			if (node.Level != 2)
			{
				return null;
			}

			string title = node.Text;
			string category = node.Parent.Text.Substring(0, node.Parent.Text.IndexOf('('));
			string term = node.Parent.Parent.Text;

			return logic.loadArticle(term, category, title);
		}

		private void btnImportHandler()
		{
			btnImportDoc.Enabled = false;
			clearUIContents();

			logic.clickedImportDoc();
		}

		private void btnImportDoc_Click(object sender, EventArgs e)
		{
			this.btnImportDoc.Enabled = false;

			btnImportHandler();
		}

        private void openAddThesaurusWindowButton_Click(object sender, EventArgs e)
        {
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
            }
          
            AddThesaurusWindow addThesaurus = new AddThesaurusWindow();
            addThesaurus.Show();

            logic.getConceptToList();
            logic.getTermList(selectedTerms);
            this.openAddThesaurusWindowButton.Enabled = false;
        }

		private void cellClickHandler(string cellValue, int tabNumber)
		{
			// TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
			logic.cellContentDoubleClick(cellValue, tabNumber);
		}

        private void checkHandler(DataGridViewCellEventArgs e, int tabNumber)
        {
            // TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
            logic.cellContentCheck(e, tabNumber);
        }

      	private void wordList1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);

            int cellRow = e.RowIndex;
            int cellCol = e.ColumnIndex;

            if (cellCol == 1)
            {
                cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[1].Value, 1);
            }
            else if (cellCol == 0)
            {
                checkHandler(e, 1);
            }
			
		}

		private void wordList2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);

            int cellRow = e.RowIndex;
            int cellCol = e.ColumnIndex;

            if (cellCol == 1)
            {
                cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[1].Value, 2);
            }
            else if (cellCol == 0)
            {
                checkHandler(e, 2);
            }
		}

		private void wordList3_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);

            int cellRow = e.RowIndex;
            int cellCol = e.ColumnIndex;

            if (cellCol == 1)
            {
                cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[1].Value, 3);
            }
            else if (cellCol == 0)
            {
                checkHandler(e, 3);
            }
		}

		private void wordList4_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
            int cellRow = e.RowIndex;
            int cellCol = e.ColumnIndex;

            if (cellCol == 1)
            {
                cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[1].Value, 4);
            }
            else if (cellCol == 0)
            {
                checkHandler(e, 4);
            }
		}

		// TODO : 아래 4개 이벤트들 쓰레드 안돌려도 된다? 작업이 짧아서?
		private void docList1_DoubleClick(object sender, EventArgs e)
		{
			string article = getArticle(sender);
			if (article != null)
			{
                logic.getColoredArticle(article, sender, this.article1);    
			}
		}
        
		private void docList2_DoubleClick(object sender, EventArgs e)
		{
            string article = getArticle(sender);
            if (article != null)
            {
                logic.getColoredArticle(article, sender, this.article2);
            }
		}

		private void docList3_DoubleClick(object sender, EventArgs e)
		{
            string article = getArticle(sender);
            if (article != null)
            {
                logic.getColoredArticle(article, sender, this.article3);
            }
		}

		private void docList4_DoubleClick(object sender, EventArgs e)
		{
            string article = getArticle(sender);
            if (article != null)
            {
                logic.getColoredArticle(article, sender, this.article4);
            }
		}

        private void addDeleteListButton_Click(object sender, EventArgs e)
        {
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
            }


            logic.clickedAddDeleteList(selectedTerms);
        }
	}
}

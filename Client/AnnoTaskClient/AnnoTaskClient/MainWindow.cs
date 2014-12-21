﻿using System;
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
			logic.KillLogicThread();
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

		private void btnJobStartHandler()
		{
			btnJobStart.Enabled = false;
			clearUIContents();
			logic.clickedJobStart();
		}

        // (기흥) "작업 시작하기" 버튼 클릭 시
		private void btnJobStart_Click(object sender, EventArgs e)
		{
            // (기흥) btnImportDoc -> btnJobStart 수정함.
			this.btnJobStart.Enabled = false;
			btnJobStartHandler();
		}

        private void openAddThesaurusWindowButton_Click(object sender, EventArgs e)
        {
            List<string> selectedTerms = new List<string>();
            if (this.tabControl1.SelectedTab == this.tabPage1)
            {
                logic.setTabNumber(1);
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
                logic.setTabNumber(2);
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
                logic.setTabNumber(3);
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
                logic.setTabNumber(4);
                DataGridViewRowCollection dataGridViewSelection = UIHandler.Instance.NGram4.getMainWindow().wordList4.Rows;

                foreach (DataGridViewRow selectedRow in dataGridViewSelection)
                {
                    if (Convert.ToBoolean(selectedRow.Cells[0].Value))
                    {
                        selectedTerms.Add((string)selectedRow.Cells[1].Value);
                    }
                }
            }
          
            // 20141219 이승철 수정.
			addThesaurusWindow();
			logic.OpenThesaurusWindow(selectedTerms);
        }

		/// <summary>
		/// 시소러스 추가 창을 열어줌.
		/// </summary>
		private void addThesaurusWindow()
		{
			// 이승철 추가 20141219
			AddThesaurusWindow addThesaurus = new AddThesaurusWindow();
			addThesaurus.Owner = this;
			this.Enabled = false;

			// 자식창 쓰레드 분리 지점.
			addThesaurus.Show();
		}

		private void cellClickHandler(string cellValue, int tabNumber)
		{
			// TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
			logic.cellContentDoubleClick(cellValue, tabNumber);
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
			if (cellRow < 1)
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
	}
}

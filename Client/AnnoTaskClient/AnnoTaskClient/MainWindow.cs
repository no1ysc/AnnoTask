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
		private MainLogic logic = new MainLogic();

		public MainWindow()
		{
			InitializeComponent();

			Thread worker = new Thread(new ThreadStart(logic.ConnectServer));

			worker.Start();
			
			UIHandler.Instance.runUIHandler(this);
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

		private void cellClickHandler(string cellValue, int tabNumber)
		{
			// TODO : 쓰레드는 안돌려도 된다? 작업이 짧아서?
			logic.cellContentDoubleClick(cellValue, tabNumber);
		}

		private void wordList1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[0].Value, 1);
		}

		private void wordList2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[0].Value, 2);
		}

		private void wordList3_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[0].Value, 3);
		}

		private void wordList4_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (sender as DataGridView);
			cellClickHandler((string)dataGridView.Rows[e.RowIndex].Cells[0].Value, 4);
		}

		// TODO : 아래 4개 이벤트들 쓰레드 안돌려도 된다? 작업이 짧아서?
		private void docList1_DoubleClick(object sender, EventArgs e)
		{
			string article = getArticle(sender);
			if (article != null)
			{
				this.article1.Text = article;
			}
		}

		private void docList2_DoubleClick(object sender, EventArgs e)
		{
			string article = getArticle(sender);
			if (article != null)
			{
				this.article2.Text = article;
			}
		}

		private void docList3_DoubleClick(object sender, EventArgs e)
		{
			string article = getArticle(sender);
			if (article != null)
			{
				this.article3.Text = article;
			}
		}

		private void docList4_DoubleClick(object sender, EventArgs e)
		{
			string article = getArticle(sender);
			if (article != null)
			{
				this.article4.Text = article;
			}
		}

	}
}

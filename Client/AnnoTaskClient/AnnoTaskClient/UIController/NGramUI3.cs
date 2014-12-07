using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoTaskClient.Logic;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
	class NGramUI3
	{
		private MainWindow mainWindow;

        public MainWindow getMainWindow()
        {
            return this.mainWindow;
        }

		public NGramUI3(MainWindow mainWindow)
		{
			this.mainWindow = mainWindow;
		}

		// Delegates
		private delegate void WordListAdd(Frequency freq);
		

		public void AddTermList(Frequency termFreq)
		{
			WordListAdd wordListAdd = new WordListAdd(addTermList);
			mainWindow.Invoke(wordListAdd, new object[] { termFreq });			
		}
		private void addTermList(Frequency termFreq)
		{
			mainWindow.wordList3.Rows.Add(1);
			mainWindow.wordList3.Rows[mainWindow.wordList3.RowCount-1].Cells[1].Value = termFreq.Term;
			mainWindow.wordList3.Rows[mainWindow.wordList3.RowCount-1].Cells[2].Value = termFreq.TotalTermFreq;
			mainWindow.wordList3.Rows[mainWindow.wordList3.RowCount-1].Cells[3].Value = termFreq.FreqInDocument;
		}

		private delegate void DocListRefresh(Frequency freq);
		public void RefreshDocList(Frequency term)
		{
			DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
			mainWindow.Invoke(docListRefresh, new object[] { term });
		}
		private void refreshDocList(Frequency term)
		{
			mainWindow.docList3.Nodes.Clear();

			TreeNode root = new TreeNode(term.Term);

			foreach (string category in term.Category.Keys)
			{
				string content = category + "(" + term.Category[category].Count.ToString() + ")";
				
				TreeNode child = root.Nodes.Add(content);
				foreach (int docID in term.Category[category].Keys)
				{
					child.Nodes.Add(term.Category[category][docID]);
				}
			}

			mainWindow.docList3.Nodes.Add(root);
		}


        private delegate void CheckboxRefresh(DataGridViewCellEventArgs e);
        public void RefreshCheckbox(DataGridViewCellEventArgs e)
        {
            CheckboxRefresh checkboxRefresh = new CheckboxRefresh(refreshCheckbox);
            mainWindow.Invoke(checkboxRefresh, new object[] { e });
        }
        private void refreshCheckbox(DataGridViewCellEventArgs e)
        {
            bool cellStatus = false;
            if (mainWindow.wordList3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                cellStatus = (bool)mainWindow.wordList3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }

            if (cellStatus)
            {
                mainWindow.wordList3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
            }
            else
            {
                mainWindow.wordList3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoTaskClient.Logic;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
	class NGramUI4
	{
		private MainWindow mainWindow;

        public MainWindow getMainWindow()
        {
            return this.mainWindow;
        }

		public NGramUI4(MainWindow mainWindow)
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
			mainWindow.wordList4.Rows.Add(1);
			mainWindow.wordList4.Rows[mainWindow.wordList4.RowCount-1].Cells[1].Value = termFreq.Term;
			mainWindow.wordList4.Rows[mainWindow.wordList4.RowCount-1].Cells[2].Value = termFreq.TotalTermFreq;
			mainWindow.wordList4.Rows[mainWindow.wordList4.RowCount-1].Cells[3].Value = termFreq.FreqInDocument;
		}

		private delegate void DocListRefresh(Frequency freq);
		public void RefreshDocList(Frequency term)
		{
			DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
			mainWindow.Invoke(docListRefresh, new object[] { term });
		}
		private void refreshDocList(Frequency term)
		{
			mainWindow.docList4.Nodes.Clear();

			TreeNode root = new TreeNode(term.Term);

            root.ExpandAll();

			foreach (string category in term.Category.Keys)
			{
				string content = category + "(" + term.Category[category].Count.ToString() + ")";
				
				TreeNode child = root.Nodes.Add(content);
				foreach (int docID in term.Category[category].Keys)
				{
					child.Nodes.Add(term.Category[category][docID]);
				}
			}

			mainWindow.docList4.Nodes.Add(root);
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
            if (mainWindow.wordList4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                cellStatus = (bool)mainWindow.wordList4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }

            if (cellStatus)
            {
                mainWindow.wordList4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
            }
            else
            {
                mainWindow.wordList4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
        }

        internal void RefreshTermList(List<string> updateList)
        {
            for (int indexOfWordList = 0; indexOfWordList < mainWindow.wordList4.RowCount; indexOfWordList++)
            {
                foreach (String term in updateList)
                {
                    if (mainWindow.wordList4.Rows[indexOfWordList].Cells[1].ToString() == term)
                    {
                        mainWindow.wordList4.Rows.RemoveAt(indexOfWordList);
                    }
                }
            }
            mainWindow.wordList4.Refresh();
        }
	}
}

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

        // (기흥) 트리뷰 생성하는 동작 수정
        private delegate void DocListRefresh(string term, Dictionary<string, Dictionary<int, string>> docMeta);
        public void RefreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
        {
            DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
            mainWindow.Invoke(docListRefresh, new object[] { str, docMeta });
        }
        private void refreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
        {
            mainWindow.docList3.Nodes.Clear();

            TreeNode root = new TreeNode(str);
            TreeNode child = new TreeNode();
            root.ExpandAll();

            foreach (string category in docMeta.Keys)
            {
                string content = category + "(" + docMeta[category].Count + ")";

                child = root.Nodes.Add(content);
                foreach (int doc_id in docMeta[category].Keys)
                {
                    child.Nodes.Add(docMeta[category][doc_id]);
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

        delegate void RefreshTermListDelegate(List<string> updateList);

        internal void RefreshTermList(List<string> updateList)
        {
            if (mainWindow.InvokeRequired)
            {
                RefreshTermListDelegate RefreshTermListDelegate = new RefreshTermListDelegate(RefreshTermList);
                mainWindow.Invoke(RefreshTermListDelegate, new object[] { updateList });
            }
            else
            {
                for (int indexOfWordList = 0; indexOfWordList < mainWindow.wordList3.RowCount; indexOfWordList++)
                {
                    foreach (String term in updateList)
                    {
                        if ((String)mainWindow.wordList3.Rows[indexOfWordList].Cells[1].Value == term)
                        {
                            mainWindow.wordList3.Rows.RemoveAt(indexOfWordList);
                        }
                    }
                }
                mainWindow.wordList3.Refresh();
            }
        }
	}
}

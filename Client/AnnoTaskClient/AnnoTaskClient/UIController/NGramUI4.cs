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

        // (기흥) 트리뷰 생성하는 동작 수정
        private delegate void DocListRefresh(string term, Dictionary<string, Dictionary<int, string>> docMeta);
        public void RefreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
        {
            DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
            mainWindow.Invoke(docListRefresh, new object[] { str, docMeta });
        }
        private void refreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
        {
            mainWindow.docList4.Nodes.Clear();

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

            mainWindow.docList4.Nodes.Add(root);
        }


		// 이승철 수정, 20141220
		private delegate void CheckboxRefresh(int cellRow, int cellCol);
		public void RefreshCheckbox(int cellRow, int cellCol)
		{
			CheckboxRefresh checkboxRefresh = new CheckboxRefresh(refreshCheckbox);
			mainWindow.Invoke(checkboxRefresh, new object[] { cellRow, cellCol });
		}
		private void refreshCheckbox(int cellRow, int cellCol)
		{
			bool cellStatus = false;
			if (mainWindow.wordList4.Rows[cellRow].Cells[cellCol].Value != null)
			{
				cellStatus = (bool)mainWindow.wordList4.Rows[cellRow].Cells[cellCol].Value;
			}

			if (cellStatus)
			{
				mainWindow.wordList4.Rows[cellRow].Cells[cellCol].Value = false;
			}
			else
			{
				mainWindow.wordList4.Rows[cellRow].Cells[cellCol].Value = true;
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
                for (int indexOfWordList = 0; indexOfWordList < mainWindow.wordList4.RowCount; indexOfWordList++)
                {
                    foreach (String term in updateList)
                    {
                        if ((String)mainWindow.wordList4.Rows[indexOfWordList].Cells[1].Value == term)
                        {
                            mainWindow.wordList4.Rows.RemoveAt(indexOfWordList);
                        }
                    }
                }
                mainWindow.wordList4.Refresh();
            }
        }
	}
}

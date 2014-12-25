using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoTaskClient.Logic;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
	class NGramUI1
	{
		private MainWindow mainWindow;

        public MainWindow getMainWindow()
        {
            return this.mainWindow;
        }

		public NGramUI1(MainWindow mainWindow)
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
			mainWindow.wordList1.Rows.Add(1);
			mainWindow.wordList1.Rows[mainWindow.wordList1.RowCount-1].Cells[1].Value = termFreq.Term;
			mainWindow.wordList1.Rows[mainWindow.wordList1.RowCount-1].Cells[2].Value = termFreq.TotalTermFreq;
			mainWindow.wordList1.Rows[mainWindow.wordList1.RowCount-1].Cells[3].Value = termFreq.FreqInDocument;
		}

        /// <summary>
		/// 작성자 : 박기흥
		/// // (기흥) 트리뷰 생성하는 동작 수정
		/// 수정자 : 이승철
		/// 수정내용 : 재사용을 위해 공통부를 UIUtil로 이동.
		/// </summary>
		/// <param name="term"></param>
		/// <param name="docMeta"></param>
		private delegate void DocListRefresh(string term, Dictionary<string, Dictionary<int, string>> docMeta);
        public void RefreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
		{
			DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
			mainWindow.Invoke(docListRefresh, new object[] { str, docMeta });
		}
        private void refreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
		{
			UIHandler.Instance.UtilForUI.RefreshDocList(str, docMeta, mainWindow.docList1);
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
			if (mainWindow.wordList1.Rows[cellRow].Cells[cellCol].Value != null)
            {
				cellStatus = (bool)mainWindow.wordList1.Rows[cellRow].Cells[cellCol].Value;
            }

            if (cellStatus)
            {
				mainWindow.wordList1.Rows[cellRow].Cells[cellCol].Value = false;
            }
            else
            {
				mainWindow.wordList1.Rows[cellRow].Cells[cellCol].Value = true;
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
                for (int indexOfWordList = 0; indexOfWordList < mainWindow.wordList1.RowCount; indexOfWordList++)
                {
                    foreach (String term in updateList)
                    {
                        if ((String)mainWindow.wordList1.Rows[indexOfWordList].Cells[1].Value == term)
                        {
                            mainWindow.wordList1.Rows.RemoveAt(indexOfWordList);
                        }
                    }
                }
                mainWindow.wordList1.Refresh();
            }

        }
    }
}

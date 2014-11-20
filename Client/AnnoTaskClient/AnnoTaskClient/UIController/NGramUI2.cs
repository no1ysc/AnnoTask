﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoTaskClient.Logic;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
	class NGramUI2
	{
		private MainWindow mainWindow;

		public NGramUI2(MainWindow mainWindow)
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
			mainWindow.wordList2.Rows.Add(1);
			mainWindow.wordList2.Rows[mainWindow.wordList2.RowCount-1].Cells[0].Value = termFreq.Term;
			mainWindow.wordList2.Rows[mainWindow.wordList2.RowCount-1].Cells[1].Value = termFreq.TotalTermFreq;
			mainWindow.wordList2.Rows[mainWindow.wordList2.RowCount-1].Cells[2].Value = termFreq.FreqInDocument;
		}

		private delegate void DocListRefresh(Frequency freq);
		public void RefreshDocList(Frequency term)
		{
			DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
			mainWindow.Invoke(docListRefresh, new object[] { term });
		}
		private void refreshDocList(Frequency term)
		{
			mainWindow.docList2.Nodes.Clear();

			TreeNode root = new TreeNode(term.Term);

			foreach (string category in term.Category.Keys)
			{
				string content = category + " (" + term.Category[category].Count.ToString() + ")";
				
				TreeNode child = root.Nodes.Add(content);
				foreach (int docID in term.Category[category].Keys)
				{
					child.Nodes.Add(term.Category[category][docID]);
				}
			}

			mainWindow.docList2.Nodes.Add(root);
		}
	}
}

using AnnoTaskClient.UIController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace AnnoTaskClient.Logic
{
	public class MainLogic : IDisposable
	{
		private bool running = true;

		private ClientWormHole clientWormHole = new ClientWormHole();
		private Dictionary<string, Frequency> termNFreq = new Dictionary<string, Frequency>(); // Term, Freq
        private List<String> deleteList;
        private List<string> termList = new List<string>();
        private ConceptTo[] conceptList;
        private LinkedList[] linkedList;
        private String conceptToTerm;
        private int selectedTabNumber;

		public MainLogic()
		{
			
		}


		#region 종료시 연결중이었던 쓰레드, 스트림 등 안끊김
		// 종료시 연결중이었던 쓰레드, 스트림 등 안끊김, ///
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
			
		}
		bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				// Free any other managed objects here.
				//
				running = false;
				clientWormHole = null;
			}
			running = false;
			clientWormHole = null;
			// Free any unmanaged objects here.
			//
			disposed = true;
		}

		~MainLogic()
		{
			running = false;
			clientWormHole = null;
		}
		#endregion





		private LinkedList<string> commandQ = new LinkedList<string>();

		public  void doWork()
		{
			if (clientWormHole.Connect())
			{
				// 서버 연결 안내문.
				//MessageBox.Show("서버 연결완료");
				UIHandler.Instance.CommonUI.ButtonEnable = true;
			}
			else
			{
				// 서버 연결 실패, 지연됨.
				MessageBox.Show("서버 연결실패");
			}

			while (running)
			{
				while(commandQ.Count != 0)
				{
					CommandParser();
				}
				Thread.Sleep(10);
			}
		}

		private void CommandParser()
		{
			string command = commandQ.First.Value;
		
			lock (commandQ) 
			{
				commandQ.RemoveFirst();
			}

			switch(command)
			{
				case "JobStart":
					jobStart();
					UIHandler.Instance.CommonUI.ButtonEnable = true;
					break;
                case "AddDeleteList":
                    addDeleteList();
                    break;
                case "AddThesaurus":
                    addThesaurus();
                    break;
                case "GetConceptToList":
                    importConceptToList();
                    break;
                case "GetLinkedList":
                    importGetLinkedList();
                    break;
				default:
					break;
			}

		}

        // (기흥) phase2.5 
		private void jobStart()
		{
			clear();

            TermFreqByDoc[] result = clientWormHole.JobStart();

			if (result == null)
			{
                MessageBox.Show("더이상 가져올 문서가 없습니다.");
				return;
			}

            // (기흥) 가져온 term들을 화면에 뿌려주는 로직.
            foreach (TermFreqByDoc termByDoc in result)
            {
                Frequency wordListEntry = new Frequency(termByDoc.Term, termByDoc.Ngram, termByDoc.TermFreq4RequestedCorpus, termByDoc.Terms.Count);
                switch (termByDoc.Ngram)
                {
                    case 1:
                        UIHandler.Instance.NGram1.AddTermList(wordListEntry);
                        break;
                    case 2:
                        UIHandler.Instance.NGram2.AddTermList(wordListEntry);
                        break;
                    case 3:
                        UIHandler.Instance.NGram3.AddTermList(wordListEntry);
                        break;
                    case 4:
                        UIHandler.Instance.NGram4.AddTermList(wordListEntry);
                        break;
                    default:
                        break;
                }
            }
			UIHandler.Instance.CommonUI.ProgressBar = 100;
		}

        private void importConceptToList()
        {
            
            conceptList = clientWormHole.ImportConceptToList();

            List<String> list = new List<string>();

            if (termList == null && termList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < conceptList.Count(); ++i)
            {
                string temp = conceptList[i].conceptToTerms;
                list.Add(temp);                
            }
            list.Sort();
            UIHandler.Instance.ThesaurusUI.RefreshConceptToCombo(list);

            if (conceptList == null)
            {
                MessageBox.Show("ConcepTo list가 없습니다.");
                return;
            }
        }

        private void importGetLinkedList()
        {
            string term = conceptToTerm;
            string conceptToId = "";
            for (int i = 0; i < conceptList.Count(); ++i)
            {
                ConceptTo temp = conceptList[i];
                if (temp.conceptToTerms.Equals(term))
                {
                    conceptToId = temp.conceptToIds;
                }
            }

            if (conceptToId.Equals(""))
            {
                return;
            }


            linkedList = clientWormHole.ImportGetLinkedList(conceptToId);
            UIHandler.Instance.ThesaurusUI.RefreshMeta(linkedList);
        }

		private void clear()
		{
			// TODO : 메모리 누수 가능성 점검해야함.
			termNFreq = new Dictionary<string, Frequency>();

			UIHandler.Instance.CommonUI.TermCount = 0;
			UIHandler.Instance.CommonUI.DocCount = 0;
			UIHandler.Instance.CommonUI.ProgressBar = 0;
		}

		internal void clickedJobStart()
		{
			commandQ.AddLast("JobStart");
		}

        internal void getConceptToList()
        {     
            commandQ.AddLast("GetConceptToList");
        }
                
        internal void getLinkedList(String term)
       {
           conceptToTerm = term;
            UIHandler.Instance.ThesaurusUI.conceptTo = term;
            commandQ.AddLast("GetLinkedList");
        }

		internal void cellContentDoubleClick(string p, int tabNumber)
		{
			// 트리뷰를 생성.
			switch (tabNumber)
			{
				case 1:
					UIHandler.Instance.NGram1.RefreshDocList(termNFreq[p]);
					break;
				case 2:
					UIHandler.Instance.NGram2.RefreshDocList(termNFreq[p]);
					break;
				case 3:
					UIHandler.Instance.NGram3.RefreshDocList(termNFreq[p]);
					break;
				case 4:
					UIHandler.Instance.NGram4.RefreshDocList(termNFreq[p]);
					break;
				default:
					break;
			}
		}

        internal void cellContentCheck(DataGridViewCellEventArgs e, int tabNumber)
		{
			// set checkbox
			switch (tabNumber)
			{
                case 1:
                    UIHandler.Instance.NGram1.RefreshCheckbox(e);
					break;
				case 2:
                    UIHandler.Instance.NGram2.RefreshCheckbox(e);
					break;
				case 3:
                    UIHandler.Instance.NGram3.RefreshCheckbox(e);
					break;
				case 4:
                    UIHandler.Instance.NGram4.RefreshCheckbox(e);
					break;
				default:
					break;
			}
		}

        internal void updateTermList(List<String> updateList, int tabNumber)
        {
            switch (tabNumber)
            {
                case 1:
                    UIHandler.Instance.NGram1.RefreshTermList(updateList);
                    break;
                case 2:
                    UIHandler.Instance.NGram2.RefreshTermList(updateList);
                    break;
                case 3:
                    UIHandler.Instance.NGram3.RefreshTermList(updateList);
                    break;
                case 4:
                    UIHandler.Instance.NGram4.RefreshTermList(updateList);
                    break;
            }
        }

		internal string loadArticle(string term, string category, string title)
		{
			// find DOC ID
			int targetID = -1;
			foreach (int docID in termNFreq[term].Category[category].Keys)
			{
				if (termNFreq[term].Category[category][docID].Contains(title))
				{
					targetID = docID;
				}
			}

			return clientWormHole.getDocBodyFromID(targetID);
		}

        // (기흥) 불용어 추가 버튼 클릭시.
        internal void clickedAddDeleteList(List<string> selectedTerm, int tabNum)
        {
            this.deleteList = selectedTerm;
            commandQ.AddLast("AddDeleteList");
            updateTermList(this.deleteList, tabNum); //불용어에 추가할 단어들은 현재 단어 리스트에서 제거하는 동작
        }

        private void addDeleteList()
        {
            clientWormHole.sendDeleteList(this.deleteList);
            //MessageBox.Show("불용어 추가를 진행하였습니다.");
        }

        internal void clickedAddThesaurus()
        {
            commandQ.AddLast("AddThesaurus");
            MessageBox.Show("시소러스에 단어 추가를 진행하였습니다.");
        }

        private void addThesaurus()
        {
            String conceptFrom = UIHandler.Instance.ThesaurusUI.GetConceptFrom();
            String metaOntology = UIHandler.Instance.ThesaurusUI.GetMetaOntology();
            List<string> temp = new List<string>();

            clientWormHole.AddThesaurus(conceptFrom, conceptToTerm, metaOntology);
            if (this.termList.Contains(conceptFrom))
            {
                this.termList.Remove(conceptFrom);
                this.termList.Sort();
                temp = this.termList;
                UIHandler.Instance.ThesaurusUI.RefreshConceptFromCombo(temp);
            }
            List<string> selectedConceptFrom = new List<string>();
            selectedConceptFrom.Add(conceptFrom);
            updateTermList(selectedConceptFrom, this.selectedTabNumber);
        }

        public void setTabNumber(int value)
        {
            this.selectedTabNumber = value;
        }

        public void emptyTermList()
        {
            this.termList.Clear();
        }

        internal void getTermList(List<string> selectedTerm)
        {
            if (selectedTerm != null && selectedTerm.Count != 0)
            {
                for (int i = 0; i < selectedTerm.Count; ++i)
                    termList.Add(selectedTerm[i]);

                selectedTerm.Sort();

                UIHandler.Instance.ThesaurusUI.RefreshConceptFromCombo(termList);
                return;
            }

            MessageBox.Show("단어를 선택해 주세요.");
        }

        internal void treeViewForThesaurusWindow(string p)
        {
            UIHandler.Instance.ThesaurusUI.RefreshDocList(termNFreq[p]);         
        }

        internal void getColoredArticle(String article, object sender, RichTextBox articleView)
        {
            articleView.Clear();

            TreeNode node = (sender as TreeView).SelectedNode;
            string term = node.Parent.Parent.Text;

            int Ncnt = 1;
            int line = 0;
            bool lineF = false;
            for (int i = 0; i < term.Length; ++i)
            {
                char temp = term[i];
                if (temp.Equals(' '))
                {
                    Ncnt++;
                }
            }

            String[] lineTokenList = article.Split('\n');
            string word = "";
            string word1 = "";
            string word2 = "";
            ArrayList list = new ArrayList();
            foreach (string lineToken in lineTokenList)
            {
                String[] wordtokenList = lineToken.Split(' ');

                foreach (string wordToken in wordtokenList)
                {
                    if (word.Contains(term))
                    {
                        if (!lineF)
                        {
                            String[] lineList = word.Split('\n');
                            line = lineList.Count();
                            lineF = true;
                        }
                        
                        String[] result = word.Split(' ');

                        for (int i = 0; i < (result.Count() - Ncnt); ++i)
                        {
                            word1 += result[i] + " ";
                        }

                        for (int j = (result.Count() - Ncnt); j < result.Count(); ++j)
                        {
                            word2 += result[j] + " ";
                        }

                        articleView.SelectionColor = System.Drawing.Color.Black;
                        articleView.SelectionBackColor = System.Drawing.Color.White;
                        articleView.SelectedText = word1;
                        
                        articleView.SelectionColor = System.Drawing.Color.Black;
                        articleView.SelectionBackColor = System.Drawing.Color.Yellow;
                        articleView.SelectedText = word2;

                        word = "";
                        word1 = "";
                        word2 = "";
                    }
                    word += " " + wordToken;
                }
                word += "\n";
            }

            if (word.Contains(term))
            {
                if (!lineF)
                {
                    String[] lineList = word.Split('\n');
                    line = lineList.Count();
                    lineF = true;
                }
                word1 = "";
                word2 = "";

                String[] result = word.Split(' ');
                for (int i = 0; i < (result.Count() - Ncnt); ++i)
                {
                    word1 += result[i] + " ";
                }

                for (int j = (result.Count() - Ncnt); j < result.Count(); ++j)
                {
                    word2 += result[j] + " ";
                }

                articleView.SelectionColor = System.Drawing.Color.Black;
                articleView.SelectionBackColor = System.Drawing.Color.White;
                articleView.SelectedText = word1;

                articleView.SelectionColor = System.Drawing.Color.Black;
                articleView.SelectionBackColor = System.Drawing.Color.Yellow;
                articleView.SelectedText = word2;

                articleView.SelectionStart = line;
                articleView.ScrollToCaret();

                return;           
            }

            articleView.SelectionColor = System.Drawing.Color.Black;
            articleView.SelectionBackColor = System.Drawing.Color.White;
            articleView.SelectedText = word;

            articleView.SelectionStart = line;
            articleView.ScrollToCaret();
        }        
    }
}

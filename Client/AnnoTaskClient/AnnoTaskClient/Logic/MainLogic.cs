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
        private List<string> termList = new List<string>();
        private ConceptTo[] conceptList;
        private LinkedList[] linkedList;
        private int selectedTabNumber;

        //
        private DocMeta documentMeta = new DocMeta(null, null, null);
        private Dictionary<string, Dictionary<int, string>> dMeta = new Dictionary<string, Dictionary<int, string>>();
        private Command.Server2Client.DocMeta docMeta = new Command.Server2Client.DocMeta();
        private TermFreqByDoc[] resultFromServer;

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





		private LinkedList<InternalCommand> commandQ = new LinkedList<InternalCommand>();

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
			InternalCommand internalCommand = commandQ.First.Value;
			string command = internalCommand.Command;
		
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
					AddDeleteList addDeleteListParam = (AddDeleteList)internalCommand;
					addDeleteList(addDeleteListParam.SelectedTerms);
					updateTermList(addDeleteListParam.SelectedTerms, addDeleteListParam.TabNum); //불용어에 추가할 단어들은 현재 단어 리스트에서 제거하는 동작
                    break;
                case "AddThesaurus":
					//logic.getConceptToList();
					AddThesaurus addThesaurusParam = (AddThesaurus)internalCommand;
					importConceptToList();
					addThesaurus(addThesaurusParam.ConceptFrom, addThesaurusParam.ConceptTo, addThesaurusParam.MetaOntology);
                    break;
				case "GetConceptToList":
					importConceptToList();
					break;
                case "GetLinkedList":
					GetLinkedList getLinkedList = (GetLinkedList)internalCommand;
					importGetLinkedList(getLinkedList.Term);
					UIHandler.Instance.ThesaurusUI.ConceptTo = getLinkedList.Term;
                    break;
				case "OpenThesaurusWindow":
					importConceptToList();
					getTermList(((OpenThesaurusWindow)internalCommand).SelectedTerms);
					break;
				default:
					break;
			}

		}

        // (기흥) phase2.5 
		private void jobStart()
		{
			clear();

            resultFromServer = clientWormHole.JobStart();

			if (resultFromServer == null)
			{
                MessageBox.Show("더이상 가져올 문서가 없습니다.");
				return;
			}

            // (기흥) 가져온 term들을 화면에 뿌려주는 로직.
            foreach (TermFreqByDoc termByDoc in resultFromServer)
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

        private void importGetLinkedList(string term)
        {
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
			commandQ.AddLast(new JobStart());
		}

        internal void getConceptToList()
        {     
            commandQ.AddLast(new GetConceptToList());
        }

        internal void getLinkedList(String term)
		{
            commandQ.AddLast(new GetLinkedList(term));
        }

        // 단어 선택 시
		internal void cellContentDoubleClick(string p, int tabNumber)
		{

            foreach (TermFreqByDoc termByDoc in resultFromServer)
            {
                if (termByDoc.Term.Equals(p))
                {
                    // 서버에 doc_id 리스트를 보내야함.
                    List<int> termLinkedDocIds = new List<int>(termByDoc.Terms.Keys);
                    // 트리뷰를 생성.
                    switch (tabNumber)
                    {
                        case 1:
                            documentMeta = clientWormHole.getDocMeta(termLinkedDocIds);
                            dMeta.Clear();
                            for (int index = 0; index < documentMeta.Category.Count; index++)
                            {
                                string cat = documentMeta.Category[index];
                                Dictionary<int, string> temp = new Dictionary<int,string>();
                                temp.Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                if (!dMeta.ContainsKey(cat))
                                {
                                    dMeta.Add(cat, temp);
                                }
                                else
                                {
                                    dMeta[cat].Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                }
                            }
                            UIHandler.Instance.NGram1.RefreshDocList(p, dMeta);
                            break;
                        
                        case 2:
                            documentMeta = clientWormHole.getDocMeta(termLinkedDocIds);
                            dMeta.Clear();
                            for (int index = 0; index < documentMeta.Category.Count; index++)
                            {
                                string cat = documentMeta.Category[index];
                                Dictionary<int, string> temp = new Dictionary<int,string>();
                                temp.Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                if (!dMeta.ContainsKey(cat))
                                {
                                    dMeta.Add(cat, temp);
                                }
                                else
                                {
                                    dMeta[cat].Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                }
                            }
                            UIHandler.Instance.NGram2.RefreshDocList(p, dMeta);
                            break;

                        case 3:
                            documentMeta = clientWormHole.getDocMeta(termLinkedDocIds);
                            dMeta.Clear();
                            for (int index = 0; index < documentMeta.Category.Count; index++)
                            {
                                string cat = documentMeta.Category[index];
                                Dictionary<int, string> temp = new Dictionary<int,string>();
                                temp.Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                if (!dMeta.ContainsKey(cat))
                                {
                                    dMeta.Add(cat, temp);
                                }
                                else
                                {
                                    dMeta[cat].Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                }
                            }
                            UIHandler.Instance.NGram3.RefreshDocList(p, dMeta);
                            break;

                        case 4:
                            documentMeta = clientWormHole.getDocMeta(termLinkedDocIds);
                            dMeta.Clear();
                            for (int index = 0; index < documentMeta.Category.Count; index++)
                            {
                                string cat = documentMeta.Category[index];
                                Dictionary<int, string> temp = new Dictionary<int,string>();
                                temp.Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                if (!dMeta.ContainsKey(cat))
                                {
                                    dMeta.Add(cat, temp);
                                }
                                else
                                {
                                    dMeta[cat].Add(documentMeta.DocIDList[index], documentMeta.Title[index]);
                                }
                            }
                            UIHandler.Instance.NGram4.RefreshDocList(p, dMeta);
                            break;
                        default:
                            break;
                    }
                }
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
            foreach (int docID in dMeta[category].Keys)
            {
                if (dMeta[category][docID].Contains(title))
                {
                    targetID = docID;
                }
            }

			return clientWormHole.getDocBodyFromID(targetID);
		}

        // (기흥) 불용어 추가 버튼 클릭시.
        internal void clickedAddDeleteList(List<string> selectedTerm, int tabNum)
        {
			commandQ.AddLast(new AddDeleteList(selectedTerm, tabNum));
        }

		private void addDeleteList(List<string> deleteList)
        {
			List<ReturnFromServer> returnValues = clientWormHole.sendDeleteList(deleteList);

			// 이승철 추가. 20141220
			// 상황대처
			foreach (ReturnFromServer returnValue in returnValues)
			{
				switch (returnValue.ReturnValue)
				{
					case "ExistThesaurusTable":
						string msg = "이미 Thesaurus Table에 추가된 단어입니다. \r\n" +
									"DeleteList에 추가하시겠습니까? \r\n" +
									"(추가하시면, Thesaurus Table에서는 삭제됩니다.)\r\n" +
									returnValue.Message;
						// 사용자 선택후 진행.
						DialogResult result = MessageBox.Show(msg, "DeleteList 추가", MessageBoxButtons.OKCancel);
						if (result == DialogResult.Yes)
						{
							// 강제로 다시 불용어 리스트에 넣기.
							clientWormHole.sendDeleteListForce(deleteList);
						}
						else
						{
							// 그냥 사전에 두기
						}

						break;
					case "ExistDeleteTable":
						// 사용자에게 겹쳤다라고 알려줌.
						MessageBox.Show("이전에 이미 추가된 단어 입니다.");
						break;
					case "Normal":
						MessageBox.Show("불용어 추가를 진행하였습니다.");
						break;
				}
			}
        }

		internal void clickedAddThesaurus(string conceptFrom, string conceptToTerm, string metaOntology)
        {
            commandQ.AddLast(new AddThesaurus(conceptFrom, conceptToTerm, metaOntology));
        }

		private void addThesaurus(string conceptFrom, string conceptToTerm, string metaOntology)
        {
			// 이승철 추가, 선택권은 사용자에게.
			ReturnFromServer returnValue = clientWormHole.AddThesaurus(conceptFrom, conceptToTerm, metaOntology);
			
			switch (returnValue.ReturnValue)
			{
				case "ExistThesaurusTable":
					{
						string msg = "이미 Thesaurus Table에 추가된 단어입니다. \r\n" +
									"업데이트 하시겠습니까? \r\n" +
									"(추가하시면, 기존입력사항은 삭제됩니다.)\r\n" +
									returnValue.Message;
						// 사용자 선택후 진행.
						DialogResult result = MessageBox.Show(msg, "시소러스 추가", MessageBoxButtons.OKCancel);
						if (result == DialogResult.Yes)
						{
							// 강제로 테이블에 넣기.
							clientWormHole.AddThesaurusForce(conceptFrom, conceptToTerm, metaOntology);
						}
						else
						{
							// 그냥 사전에 두기
						}
					}
					break;
				case "ExistDeleteTable":
					{
						string msg = "이미 Delete List에 추가된 단어입니다. \r\n" +
									"Thesaurus Table에 추가하시겠습니까? \r\n" +
									"(추가하시면, Delete List에서는 삭제됩니다.)\r\n" +
									returnValue.Message;
						// 사용자 선택후 진행.
						DialogResult result = MessageBox.Show(msg, "시소러스 추가", MessageBoxButtons.OKCancel);
						if (result == DialogResult.Yes)
						{
							// 강제로 테이블에 넣기.
							clientWormHole.AddThesaurusForce(conceptFrom, conceptToTerm, metaOntology);
						}
						else
						{
							// 그냥 사전에 두기
						}
					}
					break;
				case "Normal":
					MessageBox.Show("시소러스에 단어 추가를 진행하였습니다.");
					break;
			}


			List<string> temp = new List<string>();
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

		internal void OpenThesaurusWindow(List<string> paramSelectedTerms)
		{
			commandQ.AddLast(new OpenThesaurusWindow(paramSelectedTerms));
		}
	}
}

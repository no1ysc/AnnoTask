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

		//private DocMeta documentMeta = new DocMeta(null, null, null);
		//private Dictionary<string, Dictionary<int, string>> dMeta = new Dictionary<string, Dictionary<int, string>>(); //
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

		// 새로 구현
		public void KillLogicThread()
		{
			this.running = false;
		}
		#endregion





		private LinkedList<InternalCommand> commandQ = new LinkedList<InternalCommand>();

		public  void doWork()
		{
			if (clientWormHole.Connect())
			{
				// 서버 연결 안내문.
				//MessageBox.Show("서버 연결완료");
				UIHandler.Instance.CommonUI.AllButtonEnabledInMainWindow = true;
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

			// 종료시 처리.
			clientWormHole.Destroy();
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
					UIHandler.Instance.CommonUI.AllButtonEnabledInMainWindow = true;
					break;
                case "AddDeleteList":
					AddDeleteList addDeleteListParam = (AddDeleteList)internalCommand;
					addDeleteList(addDeleteListParam.SelectedTerms);
					updateTermList(addDeleteListParam.SelectedTerms, addDeleteListParam.TabNum); //불용어에 추가할 단어들은 현재 단어 리스트에서 제거하는 동작
					UIHandler.Instance.CommonUI.AllButtonEnabledInMainWindow = true;
                    break;
                case "AddThesaurus":
					//logic.getConceptToList();
					AddThesaurus addThesaurusParam = (AddThesaurus)internalCommand;
					//importConceptToList();
					addThesaurus(addThesaurusParam.ConceptFrom, addThesaurusParam.ConceptTo, addThesaurusParam.MetaOntology);
                    break;
				case "GetConceptToList":
					GetConceptToList getConceptToList = (GetConceptToList)internalCommand;
					importConceptToList(getConceptToList.InputConceptTo);
					break;
                case "GetLinkedList":
					GetLinkedList getLinkedList = (GetLinkedList)internalCommand;
					importGetLinkedList(getLinkedList.Term);
					//UIHandler.Instance.ThesaurusUI.ConceptTo = getLinkedList.Term;
                    break;
				//case "OpenThesaurusWindow":
				//	importConceptToList();
				//	getTermList(((OpenThesaurusWindow)internalCommand).SelectedTerms);
				//	break;
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

		/// <summary>
		/// 작성자 : 신효정
		/// 수정자 : 이승철, 20141222
		/// 수정내용 : 동작방식 변경, 데이터저장소 이동
		/// </summary>
		private void importConceptToList(string inputConceptTo)
        {
			List<ConceptTo> conceptToList = new List<ConceptTo>();

			conceptToList = clientWormHole.ImportConceptToList(inputConceptTo);

			//if (conceptToList.Count == 0)
			//{
			//	// 승철 : 메시지 박스 찍어야함?
			//	//MessageBox.Show("ConcepTo list가 없습니다.");
			//	return;
			//}
          
			// 승철 : 소팅이 꼭 필요한가?
            //list.Sort();
            
			//UIHandler.Instance.ThesaurusUI.RefreshConceptToCombo(list);
			//UIHandler.Instance.ThesaurusUI.ConceptToList = null;
			UIHandler.Instance.ThesaurusUI.ConceptToList = conceptToList;
			//UIHandler.Instance.ThesaurusUI.ConceptTo = inputConceptTo;	// 승철, 이렇게 줄 수 밖에 없나.. 클린 이후에 뭘해야하지?
        }

		private void importGetLinkedList(string conceptToTerm)
        {
			int termID = UIHandler.Instance.ThesaurusUI.findConceptToID(conceptToTerm);
			UIHandler.Instance.ThesaurusUI.LinkedListBoxList = null;
			UIHandler.Instance.ThesaurusUI.LinkedListBoxList = clientWormHole.ImportGetLinkedList(termID.ToString());
			//UIHandler.Instance.ThesaurusUI.RefreshMeta(linkedList);
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

        internal void getConceptToList(string term)
        {     
            commandQ.AddLast(new GetConceptToList(term));
        }

        internal void getLinkedList(String term)
		{
            commandQ.AddLast(new GetLinkedList(term));
        }

        
		/// <summary>
		/// 작성자 : 박기흥
		/// // 단어 선택 시
		/// 수정자 : 이승철, 20141223
		/// 수정내용 : 단위 동작으로 변경.
		/// 트리뷰 업데이트 하는 코드 UI Util로 이동.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="tabNumber"></param>
		internal void DocListUpdate(string p, int tabNumber)
		{
			Dictionary<string, Dictionary<int, string>> dMeta = new Dictionary<string, Dictionary<int, string>>(); //

			foreach (TermFreqByDoc termByDoc in resultFromServer)
            {
                if (termByDoc.Term.Equals(p))
                {
                    // 서버에 doc_id 리스트를 보내야함.
                    List<int> termLinkedDocIds = new List<int>(termByDoc.Terms.Keys);
					DocMeta documentMeta = clientWormHole.getDocMeta(termLinkedDocIds);
				
					for (int index = 0; index < documentMeta.Category.Count; index++)
					{
						string cat = documentMeta.Category[index];
						Dictionary<int, string> temp = new Dictionary<int, string>();
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

					// 트리뷰를 생성.
                    switch (tabNumber)
                    {
                        case 1:
							UIHandler.Instance.NGram1.RefreshDocList(p, dMeta);
                            break;
                        
                        case 2:
                            UIHandler.Instance.NGram2.RefreshDocList(p, dMeta);
                            break;

                        case 3:
                            UIHandler.Instance.NGram3.RefreshDocList(p, dMeta);
                            break;

                        case 4:
                            UIHandler.Instance.NGram4.RefreshDocList(p, dMeta);
                            break;
						case 0:	// Thesaurus
							UIHandler.Instance.ThesaurusUI.RefreshDocList(p, dMeta);
							break;
                        default:
                            break;
                    }
				}
            }
		}

		// 이승철 수정 20141220
		internal void cellContentCheck(int cellRow, int cellCol, int tabNumber)
		{
			// set checkbox
			switch (tabNumber)
			{
                case 1:
                    UIHandler.Instance.NGram1.RefreshCheckbox(cellRow, cellCol);
					break;
				case 2:
                    UIHandler.Instance.NGram2.RefreshCheckbox(cellRow, cellCol);
					break;
				case 3:
                    UIHandler.Instance.NGram3.RefreshCheckbox(cellRow, cellCol);
					break;
				case 4:
					UIHandler.Instance.NGram4.RefreshCheckbox(cellRow, cellCol);
					break;
				default:
					break;
			}
		}

		
		/// <summary>
		/// 작성자 : ???
		/// 수정자 : 이승철, 20141223
		/// CommonUI 로 옮김.
		/// 이승철, 의견 : Term 관리하는데에서는 안지우나?
		/// </summary>
		/// <param name="updateList"></param>
		/// <param name="tabNumber"></param>
        internal void updateTermList(List<String> updateList, int tabNumber)
        {
			UIHandler.Instance.CommonUI.UpdateTermList(updateList, tabNumber);
        }

		//internal string loadArticle(string term, string category, string title)
		//{
		//	// find DOC ID
		//	int targetID = -1;
		//	foreach (int docID in dMeta[category].Keys)
		//	{
		//		if (dMeta[category][docID].Contains(title))
		//		{
		//			targetID = docID;
		//			break;
		//		}
		//	}

		//	return clientWormHole.getDocBodyFromID(targetID);
		//}

		/// <summary>
		/// 작성자 : 이승철, 20141223
		/// ID로 DOC 바로 가져오기.
		/// </summary>
		/// <param name="term"></param>
		/// <param name="category"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		internal string loadArticle(int docID)
		{
			return clientWormHole.getDocBodyFromID(docID);
		}

        // (기흥) 불용어 추가 버튼 클릭시.
        internal void clickedAddDeleteList(List<string> selectedTerm, int tabNum)
        {
			commandQ.AddLast(new AddDeleteList(selectedTerm, tabNum));
        }

		private void addDeleteList(List<string> deleteList)
        {
			List<ReturnFromServer> returnValues = clientWormHole.sendDeleteList(deleteList);

			int normalCount = 0;	// 정상 동작
			int dupleCount = 0;		// 딜리트 겹친것
			int exceptionCount = 0;	// 시소러스에서 딜리트로 옮긴것.
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
						DialogResult result = MessageBox.Show(msg, "DeleteList 추가", MessageBoxButtons.YesNo);
						if (result == DialogResult.Yes)
						{
							// 강제로 다시 불용어 리스트에 넣기.
							clientWormHole.sendDeleteListForce(deleteList);
							exceptionCount++;
						}
						else
						{
							// 그냥 사전에 두기
						}

						break;
					case "ExistDeleteTable":
						// 사용자에게 겹쳤다라고 알려줌.
						//MessageBox.Show("이전에 이미 추가된 단어 입니다.");
						dupleCount++;
						break;
					case "Normal":
						normalCount++;
						//MessageBox.Show("불용어 추가를 진행하였습니다.");
						break;
				}
			}

			int procCount = normalCount + dupleCount + exceptionCount;
			string message = "선택된 단어 " + returnValues.Count.ToString() + "개 중 " + procCount.ToString() + "개를 불용어사전에 반영하였습니다.\r\n"
							+"(새로추가 : " + normalCount.ToString() + "개,\r\n"
							+"이미 추가된 단어 : " + dupleCount.ToString() + "개,\r\n"
							+"시소러스에서 옮긴 단어 : " + exceptionCount.ToString() + "개,\r\n"
							+"처리하지 않은 단어 : " + (returnValues.Count - procCount).ToString() + "개)"
			
			MessageBox.Show(message);
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
						DialogResult result = MessageBox.Show(msg, "시소러스 추가", MessageBoxButtons.YesNo);
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
						DialogResult result = MessageBox.Show(msg, "시소러스 추가", MessageBoxButtons.YesNo);
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

			// 시소러스창 Refresh,
			UIHandler.Instance.ThesaurusUI.RefreshAfterAddThesaurus();
        }

		//public void setTabNumber(int value)
		//{
		//	this.selectedTabNumber = value;
		//}

		//internal void getTermList(List<string> selectedTerm)
		//{
		//	if (selectedTerm != null && selectedTerm.Count != 0)
		//	{
		//		for (int i = 0; i < selectedTerm.Count; ++i)
		//			termList.Add(selectedTerm[i]);

		//		selectedTerm.Sort();

		//		UIHandler.Instance.ThesaurusUI.RefreshConceptFromCombo(termList);
		//		return;
		//	}

		//	MessageBox.Show("단어를 선택해 주세요.");
		//}

		/// <summary>
		/// 작성자 : 이승철
		/// Thesaurus 윈도우의 트리뷰를 업데이트 하기 위함......
		/// DB쿼리작업이라서, 이종류의 작업들 커멘드 처리해야되나,,,,,
		/// 조금 시간이 오래걸리면 응답없음이 뜰 수도,
		/// </summary>
		/// <param name="p"></param>
        internal void treeViewForThesaurusWindow(string term)
        {
			DocListUpdate(term, 0);
        }

		//internal void OpenThesaurusWindow(List<string> paramSelectedTerms)
		//{
		//	commandQ.AddLast(new OpenThesaurusWindow(paramSelectedTerms));
		//}
	}
}

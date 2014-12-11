using AnnoTaskClient;
using AnnoTaskClient.UIController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnnoTaskClient.Logic
{
	class ClientWormHole
	{
		~ClientWormHole()
		{
			m_ns.Close();
			m_Reader.Close();
			m_Writer.Close();
			m_client.Close();
		}

		internal bool Connect()
		{
			//Thread worker = new Thread(new ThreadStart(connectServer));
			//worker.Start();

			int failCount = 0;

			while (true)
			{
				if (connectServer())
				{
					//MessageBox.Show("서버에 연결 되었습니다.");
					return true;
				}

				if (failCount > Configure.Instance.ConnectionWaitTimeS)
				{
					MessageBox.Show("서버에 연결하지 못했습니다.");
					return false;
				}
				Thread.Sleep(1000);
				failCount++;
			}			
		}

		private TcpClient m_client;
		private NetworkStream m_ns;
		private StreamReader m_Reader;
		private StreamWriter m_Writer;
		private bool connectServer()
		{
			try
			{
				m_client = new TcpClient(Configure.Instance.ServerIP, Configure.Instance.ServerPort);
				if (!m_client.Connected)
				{
					return false;
				}
			}
			catch (SocketException e)
			{
				return false;
			}

			m_ns = m_client.GetStream();
			//m_Reader = new StreamReader(m_ns);
			m_Writer = new StreamWriter(m_ns);
			m_Reader = new StreamReader(m_ns, Encoding.UTF8);
			m_ns.ReadTimeout = 300000;
			
			//m_Writer = new StreamWriter(m_ns, Encoding.UTF8);

			return true;
		}

        // (기흥) phase2.5 start
        internal TermFreqByDoc[] JobStart()
        {
            // (기흥) bRequestAnnoTaskWork에 true를 넣어 서버에 보내면 작업 대상의 doc_id들을 서버가 보내줄 것임.
            Command.Client2Server.RequestAnnoTaskWork data = new Command.Client2Server.RequestAnnoTaskWork();
            data.bRequestAnnoTaskWork = true;
            string json_RequestAnnoTaskWork = new JsonConverter<Command.Client2Server.RequestAnnoTaskWork>().Object2Json(data);
            m_Writer.WriteLine(json_RequestAnnoTaskWork);
            m_Writer.Flush();

            // (기흥) 서버에서 보내온 작업량을 받아서...
            string json_ReceiveDocCount = m_Reader.ReadLine();
            Command.Server2Client.SendDocumentCount docCount = new JsonConverter<Command.Server2Client.SendDocumentCount>().Json2Object(json_ReceiveDocCount);
            if (docCount.doucumentCount != 0)
            {
                UIHandler.Instance.CommonUI.DocCount = docCount.doucumentCount; // (기흥) "총 문서수" 업데이트
            }
            else
            {
                return null; // (기흥) 더이상 가져온게 없으면 return null.
            }

            // (기흥) 이제 본격적으로 단어들을 보내달라고 서버에게 요청.
            Command.Client2Server.RequestTermTransfer requestTermTransfer = new Command.Client2Server.RequestTermTransfer();
            requestTermTransfer.bTransfer = true;
            string json_requestTermTransfer = new JsonConverter<Command.Client2Server.RequestTermTransfer>().Object2Json(requestTermTransfer);
            m_Writer.WriteLine(json_requestTermTransfer);
            m_Writer.Flush();

            // (기흥) 서버는 먼저 보내올 총 단어 갯수를 Client에게 보내고, Client는 Progress Bar를 총 단어 갯수로 업데이트 준비함.
            string json_TotalTermCount = null;
            json_TotalTermCount = m_Reader.ReadLine();
            Command.Server2Client.SendTermCount totalTermCount = new JsonConverter<Command.Server2Client.SendTermCount>().Json2Object(json_TotalTermCount);
            //UIHandler.Instance.CommonUI.TermCount = totalTermCount.totalTermCount;
            int totalTermsToReceive = totalTermCount.totalTermCount;


            // (기흥) 서버에서 단어 하나하나 보내오면 Client쪽에서는 차곡차곡 받아서 progress bar 업데이트!!
            TermFreqByDoc[] termFreqByDoc = new TermFreqByDoc[totalTermsToReceive];
            for (int transferCount = 0; transferCount < totalTermsToReceive; transferCount++)
            {
                string json_transferedTerm = null;
                try
                {
                    json_transferedTerm = m_Reader.ReadLine();
                    if (json_transferedTerm == null)
                    {
                        Console.WriteLine("서버로부터 받은 단어가 없습니다.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    goto EndOfInstance;
                }

                // (기흥) 읽어들인 term을 자료구조에 맞게 넣어주는 작업...
                Command.Server2Client.TermTransfer term = new JsonConverter<Command.Server2Client.TermTransfer>().Json2Object(json_transferedTerm);
                termFreqByDoc[transferCount] = new TermFreqByDoc();
                termFreqByDoc[transferCount].Term = term.term;
                termFreqByDoc[transferCount].TermFreq4RequestedCorpus = term.termFreq4RequestedCorpus;
                termFreqByDoc[transferCount].Ngram = term.ngram;
                termFreqByDoc[transferCount].Terms = new JsonConverter<Dictionary<int, int>>().Json2Object(term.termsJson);
                
                // ProgressBar 업데이트
                UIHandler.Instance.CommonUI.ProgressBar = (transferCount / totalTermsToReceive) * 100;
                UIHandler.Instance.CommonUI.TermCount = transferCount;
            }

            string json_isEndTransfer = m_Reader.ReadLine();
            Command.Server2Client.NotifyTransferEnd end = new JsonConverter<Command.Server2Client.NotifyTransferEnd>().Json2Object(json_isEndTransfer);

            EndOfInstance: //완료...

            return termFreqByDoc;
        }


        // phase2 코드
//        internal DocByTerm[] ImportDoc(string startDate, string endDate, bool naver, bool daum, bool nate)
//        {
//            Command.Client2Server.RequestByDate data = new Command.Client2Server.RequestByDate();
//            data.startDate = startDate;
//            data.endDate = endDate;
//            data.bNaver = naver;
//            data.bDaum = daum;
//            data.bNate = nate;

//            // 1-1 보냄
//            string json1_1 = new JsonConverter<Command.Client2Server.RequestByDate>().Object2Json(data);
//            m_Writer.WriteLine(json1_1);
//            m_Writer.Flush();

//            // 1-2 받음.
//            string json1_2 = m_Reader.ReadLine();
//            Command.Server2Client.SendDocumentCount docCount = new JsonConverter<Command.Server2Client.SendDocumentCount>().Json2Object(json1_2);

//            // 나중 고치자,
//            UIHandler.Instance.CommonUI.DocCount = docCount.doucumentCount / 4;
//            if (docCount.doucumentCount > Configure.Instance.LimitDocumentCount)
//            {
//                // 분석제한.
//                return null;
//            }
//            if (docCount.doucumentCount == 0)
//            {
//                // 분석 대상 없음.
//                return null;
//            }

//            // 1-3 보냄.
//            Command.Client2Server.RequestTermTransfer requestTermTransfer = new Command.Client2Server.RequestTermTransfer();
//            requestTermTransfer.bTransfer = true;
//            string json1_3 = new JsonConverter<Command.Client2Server.RequestTermTransfer>().Object2Json(requestTermTransfer);
//            m_Writer.WriteLine(json1_3);
//            m_Writer.Flush();

//            // 1-4 연속 받음.
//            DocByTerm[]	docByTerms = new DocByTerm[docCount.doucumentCount];
//            for (int transferCount = 0; transferCount < docCount.doucumentCount; transferCount++ )
//            {
//                string json1_4 = null;
//                try
//                {
//                    json1_4 = m_Reader.ReadLine();
//                    if (json1_4 == null)
//                    {
//                        // 타임아웃 걸리면 이쪽으로. 사용자에게 인지해 줄 필요가 있나?
//                        // 후행처리 필요, 서버와의 재연결이 필요함, 시점은 언제가 좋을지?
//                        UIHandler.Instance.CommonUI.DocCount = transferCount / 4;
//                        goto EndOfInstance;
//                    }
//                }
//                catch (IOException e)
//                {
//                    // 서버쪽에서 어떠한 이유든 병목걸리면 이 익셉션 뱃을 수 있음.
//                    // 이거 뜨면 연결 끊어진거라 보면되고, 프로그램이 종료될 익셉션임.
//                    // 여기서 잡아주면 여지껏 받아온 텀들은 보여줄 수는 있음.
//                    // 단, 후행작업(Thesaurus 처리 등)을 하기 위해서는 재커넥션 하는 로직이 필요함.
//                    UIHandler.Instance.CommonUI.DocCount = transferCount / 4;
//                    goto EndOfInstance;
//                }
				

//                Command.Server2Client.TermTransfer term = new JsonConverter<Command.Server2Client.TermTransfer>().Json2Object(json1_4);
//                docByTerms[transferCount] = new DocByTerm();
//                docByTerms[transferCount].DocID = term.docID;
//                docByTerms[transferCount].Title = term.docTitle;
//                docByTerms[transferCount].DocCategory = (term.docCategory != null)?term.docCategory:"null";
//                docByTerms[transferCount].Ngram = term.ngram;
//                docByTerms[transferCount].Terms = new JsonConverter<Dictionary<string, int>>().Json2Object(term.termsJson);

//                UIHandler.Instance.CommonUI.ProgressBar = (transferCount/docCount.doucumentCount) * 50;
//                UIHandler.Instance.CommonUI.TermCount += docByTerms[transferCount].Terms.Count;
//            }

//            string json1_5 = m_Reader.ReadLine();
//            Command.Server2Client.NotifyTransferEnd end = new JsonConverter<Command.Server2Client.NotifyTransferEnd>().Json2Object(json1_5);
//EndOfInstance:
//            UIHandler.Instance.CommonUI.ProgressBar = 50;
//            // 완료.....

//            return docByTerms;
//        }

        internal LinkedList[] ImportGetLinkedList(string conceptToId)
		{
            Command.Client2Server.RequestLinkedList data = new Command.Client2Server.RequestLinkedList();
            data.conceptToId = conceptToId;
            string json1_1 = new JsonConverter<Command.Client2Server.RequestLinkedList>().Object2Json(data);
            
            m_Writer.WriteLine(json1_1);
            m_Writer.Flush();
            
            string json1_2 = m_Reader.ReadLine();
            Command.Server2Client.LinkedListCount linkedCount = new JsonConverter<Command.Server2Client.LinkedListCount>().Json2Object(json1_2);

            LinkedList[] linkedList = new LinkedList[linkedCount.linkedListCount];
            for (int transferCount = 0; transferCount < linkedCount.linkedListCount; transferCount++)
            {
                string json1_4 = null;
                try
                {
                    json1_4 = m_Reader.ReadLine();
                    if (json1_4 == null)
                    {
                        // 타임아웃 걸리면 이쪽으로. 사용자에게 인지해 줄 필요가 있나?
                        // 후행처리 필요, 서버와의 재연결이 필요함, 시점은 언제가 좋을지?
                       // goto EndOfInstance;
                    }
                }
                catch (IOException e)
                {
                    // 서버쪽에서 어떠한 이유든 병목걸리면 이 익셉션 뱃을 수 있음.
                    // 이거 뜨면 연결 끊어진거라 보면되고, 프로그램이 종료될 익셉션임.
                    // 여기서 잡아주면 여지껏 받아온 텀들은 보여줄 수는 있음.
                    // 단, 후행작업(Thesaurus 처리 등)을 하기 위해서는 재커넥션 하는 로직이 필요함.
                   // goto EndOfInstance;
                }

                Command.Server2Client.LinkedListResponse linked = new JsonConverter<Command.Server2Client.LinkedListResponse>().Json2Object(json1_4);

                linkedList[transferCount] = new LinkedList();
                linkedList[transferCount].linkedTerms = linked.linkedListTerm;
              }

            string json1_5 = m_Reader.ReadLine();
            Command.Server2Client.LinkedListResponse temp = new JsonConverter<Command.Server2Client.LinkedListResponse>().Json2Object(json1_5);

            if (temp.metaInfo == null || linkedCount.linkedListCount == 0)
            {
                return linkedList;
            }

            linkedList[0].metaInfos = temp.metaInfo;

            return linkedList;
		}


        internal ConceptTo[] ImportConceptToList()
        {
            Command.Client2Server.RequestConcetpToList data = new Command.Client2Server.RequestConcetpToList();
            string json1_1 = new JsonConverter<Command.Client2Server.RequestConcetpToList>().Object2Json(data);
            m_Reader = new StreamReader(m_ns, Encoding.UTF8);
            m_Writer.WriteLine(json1_1);
            m_Writer.Flush();

            string json1_2 = m_Reader.ReadLine();
            Command.Server2Client.ConceptToCount conceptToCount = new JsonConverter<Command.Server2Client.ConceptToCount>().Json2Object(json1_2);

            ConceptTo[] conceptToList = new ConceptTo[conceptToCount.conceptToCount];
            for (int transferCount = 0; transferCount < conceptToCount.conceptToCount; transferCount++)
            {
                string json1_4 = null;
                try
                {
                    json1_4 = m_Reader.ReadLine();
                    if (json1_4 == null)
                    {
                        // 타임아웃 걸리면 이쪽으로. 사용자에게 인지해 줄 필요가 있나?
                        // 후행처리 필요, 서버와의 재연결이 필요함, 시점은 언제가 좋을지?
                        // goto EndOfInstance;
                    }
                }
                catch (IOException e)
                {
                    // 서버쪽에서 어떠한 이유든 병목걸리면 이 익셉션 뱃을 수 있음.
                    // 이거 뜨면 연결 끊어진거라 보면되고, 프로그램이 종료될 익셉션임.
                    // 여기서 잡아주면 여지껏 받아온 텀들은 보여줄 수는 있음.
                    // 단, 후행작업(Thesaurus 처리 등)을 하기 위해서는 재커넥션 하는 로직이 필요함.
                    // goto EndOfInstance;
                }

                Command.Server2Client.ConceptToListResponse conceptTo = new JsonConverter<Command.Server2Client.ConceptToListResponse>().Json2Object(json1_4);

                conceptToList[transferCount] = new ConceptTo();
                conceptToList[transferCount].conceptToIds = conceptTo.conceptToId;
                conceptToList[transferCount].conceptToTerms = conceptTo.ConceptToTerm;
            }


            return conceptToList;
        }

		internal string getDocBodyFromID(int targetID)
		{
			Command.Client2Server.DocumentRequest docReq = new Command.Client2Server.DocumentRequest();
			docReq.documentID = targetID;
			Command.Server2Client.DocumentResponse document;

			try
			{
				// 2-1 보냄.
				string json = new JsonConverter<Command.Client2Server.DocumentRequest>().Object2Json(docReq);
				m_Writer.WriteLine(json);
				m_Writer.Flush();

				// 2-2 받음.
				string jsonRes = m_Reader.ReadLine();
				document = new JsonConverter<Command.Server2Client.DocumentResponse>().Json2Object(jsonRes);
				
				return document.body;
			}
			catch (Exception e)
			{
				return null;
			}
		}

        internal void sendDeleteList(List<string> list)
        {
            Command.Client2Server.RequestAddDeleteList deleteListReq = new Command.Client2Server.RequestAddDeleteList();
            deleteListReq.addDeleteList = list;
            string json_addDeleteList = new JsonConverter<Command.Client2Server.RequestAddDeleteList>().Object2Json(deleteListReq);
            m_Writer.WriteLine(json_addDeleteList);
            m_Writer.Flush();
        }

        internal bool AddThesaurus(string conceptFrom, string conceptTo, string metaOntology)
        {
            Command.Client2Server.RequestAddThesaurus entry = new Command.Client2Server.RequestAddThesaurus();
            entry.conceptFrom = conceptFrom;
            entry.conceptTo = conceptTo;
            entry.metaOntology = metaOntology;
            try
            {
                string json_addThesaurus = new JsonConverter<Command.Client2Server.RequestAddThesaurus>().Object2Json(entry);

                System.Diagnostics.Debug.WriteLine(json_addThesaurus);

                m_client = new TcpClient(Configure.Instance.ServerIP, Configure.Instance.ServerPort);
                m_ns = m_client.GetStream();
                m_Writer = new StreamWriter(m_ns);

                Console.WriteLine(json_addThesaurus);
                Console.WriteLine(json_addThesaurus.Length);

                if (json_addThesaurus != null)
                {
                    m_Writer.WriteLine(json_addThesaurus);
                    m_Writer.Flush();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}

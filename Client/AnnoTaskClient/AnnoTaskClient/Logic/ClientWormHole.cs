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
					MessageBox.Show("서버에 연결 되었습니다.");
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
			m_ns.ReadTimeout = 30000;
			
			//m_Writer = new StreamWriter(m_ns, Encoding.UTF8);

			return true;
		}

		internal DocByTerm[] ImportDoc(string startDate, string endDate, bool naver, bool daum, bool nate)
		{
			Command.Client2Server.RequestByDate data = new Command.Client2Server.RequestByDate();
			data.startDate = startDate;
			data.endDate = endDate;
			data.bNaver = naver;
			data.bDaum = daum;
			data.bNate = nate;

			// 1-1 보냄
			string json1_1 = new JsonConverter<Command.Client2Server.RequestByDate>().Object2Json(data);
			m_Writer.WriteLine(json1_1);
			m_Writer.Flush();

			// 1-2 받음.
			string json1_2 = m_Reader.ReadLine();
			Command.Server2Client.SendDocumentCount docCount = new JsonConverter<Command.Server2Client.SendDocumentCount>().Json2Object(json1_2);

			// 나중 고치자,
			UIHandler.Instance.CommonUI.DocCount = docCount.doucumentCount / 4;
			if (docCount.doucumentCount > Configure.Instance.LimitDocumentCount)
			{
				// 분석제한.
				return null;
			}
			if (docCount.doucumentCount == 0)
			{
				// 분석 대상 없음.
				return null;
			}

			// 1-3 보냄.
			Command.Client2Server.RequestTermTransfer requestTermTransfer = new Command.Client2Server.RequestTermTransfer();
			requestTermTransfer.bTransfer = true;
			string json1_3 = new JsonConverter<Command.Client2Server.RequestTermTransfer>().Object2Json(requestTermTransfer);
			m_Writer.WriteLine(json1_3);
			m_Writer.Flush();

			// 1-4 연속 받음.
			DocByTerm[]	docByTerms = new DocByTerm[docCount.doucumentCount];
			for (int transferCount = 0; transferCount < docCount.doucumentCount; transferCount++ )
			{
				string json1_4 = null;
				try
				{
					json1_4 = m_Reader.ReadLine();
					if (json1_4 == null)
					{
						// 타임아웃 걸리면 이쪽으로. 사용자에게 인지해 줄 필요가 있나?
						// 후행처리 필요, 서버와의 재연결이 필요함, 시점은 언제가 좋을지?
						UIHandler.Instance.CommonUI.DocCount = transferCount / 4;
						goto EndOfInstance;
					}
				}
				catch (IOException e)
				{
					// 서버쪽에서 어떠한 이유든 병목걸리면 이 익셉션 뱃을 수 있음.
					// 이거 뜨면 연결 끊어진거라 보면되고, 프로그램이 종료될 익셉션임.
					// 여기서 잡아주면 여지껏 받아온 텀들은 보여줄 수는 있음.
					// 단, 후행작업(Thesaurus 처리 등)을 하기 위해서는 재커넥션 하는 로직이 필요함.
					UIHandler.Instance.CommonUI.DocCount = transferCount / 4;
					goto EndOfInstance;
				}
				

				Command.Server2Client.TermTransfer term = new JsonConverter<Command.Server2Client.TermTransfer>().Json2Object(json1_4);
				docByTerms[transferCount] = new DocByTerm();
				docByTerms[transferCount].DocID = term.docID;
				docByTerms[transferCount].Title = term.docTitle;
				docByTerms[transferCount].DocCategory = (term.docCategory != null)?term.docCategory:"null";
				docByTerms[transferCount].Ngram = term.ngram;
				docByTerms[transferCount].Terms = new JsonConverter<Dictionary<string, int>>().Json2Object(term.termsJson);

				UIHandler.Instance.CommonUI.ProgressBar = (transferCount/docCount.doucumentCount) * 50;
				UIHandler.Instance.CommonUI.TermCount += docByTerms[transferCount].Terms.Count;
			}

			string json1_5 = m_Reader.ReadLine();
			Command.Server2Client.NotifyTransferEnd end = new JsonConverter<Command.Server2Client.NotifyTransferEnd>().Json2Object(json1_5);
EndOfInstance:
			UIHandler.Instance.CommonUI.ProgressBar = 50;
			// 완료.....

			return docByTerms;
		}



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
                string json_AddThesaurus = new JsonConverter<Command.Client2Server.RequestAddThesaurus>().Object2Json(entry);

                System.Diagnostics.Debug.WriteLine(json_AddThesaurus);

                m_client = new TcpClient(Configure.Instance.ServerIP, Configure.Instance.ServerPort);
                m_ns = m_client.GetStream();
                m_Writer = new StreamWriter(m_ns);

                Console.WriteLine(json_AddThesaurus);
                Console.WriteLine(json_AddThesaurus.Length);

                if (json_AddThesaurus != null)
                {
                    m_Writer.WriteLine(json_AddThesaurus);
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

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

namespace AnnoTaskClient.Logic
{
	class ClientWormHole
	{

		internal bool Connect()
		{
			//Thread worker = new Thread(new ThreadStart(connectServer));
			//worker.Start();

			int failCount = 0;

			while (true)
			{
				if (connectServer())
				{
					return true;
				}

				if (failCount > Configure.Instance.ConnectionWaitTimeS)
				{
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
				string json1_4 = m_Reader.ReadLine();
				Command.Server2Client.TermTransfer term = new JsonConverter<Command.Server2Client.TermTransfer>().Json2Object(json1_4);
				docByTerms[transferCount] = new DocByTerm();
				docByTerms[transferCount].DocID = term.docID;
				docByTerms[transferCount].Title = term.docTitle;
				docByTerms[transferCount].DocCategory = term.docCategory;
				docByTerms[transferCount].Ngram = term.ngram;
				docByTerms[transferCount].Terms = new JsonConverter<Dictionary<string, int>>().Json2Object(term.termsJson);

				UIHandler.Instance.CommonUI.ProgressBar = (transferCount/docCount.doucumentCount) * 50;
				UIHandler.Instance.CommonUI.TermCount += docByTerms[transferCount].Terms.Count;
			}

			string json1_5 = m_Reader.ReadLine();
			Command.Server2Client.NotifyTransferEnd end = new JsonConverter<Command.Server2Client.NotifyTransferEnd>().Json2Object(json1_5);

			UIHandler.Instance.CommonUI.ProgressBar = 50;
			// 완료.....

			return docByTerms;
		}

		internal string getDocBodyFromID(int targetID)
		{
			Command.Client2Server.DocumentRequest docReq = new Command.Client2Server.DocumentRequest();
			docReq.documentID = targetID;

			// 2-1 보냄.
			string json = new JsonConverter<Command.Client2Server.DocumentRequest>().Object2Json(docReq);
			m_Writer.WriteLine(json);
			m_Writer.Flush();

			// 2-2 받음.
			string jsonRes = m_Reader.ReadLine();
			Command.Server2Client.DocumentResponse document = new JsonConverter<Command.Server2Client.DocumentResponse>().Json2Object(jsonRes);

			return document.body;
		}
	}
}

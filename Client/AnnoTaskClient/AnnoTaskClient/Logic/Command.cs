﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	class Command
	{
		public class Server2Client
		{
			public class SendDocumentCount
			{
				// 문서갯수 이만큼임.1-2
				public int doucumentCount;
			}

			public class TermTransfer
			{
				// 텀 1개씩 전송	1-4(반복)
				public int docID;
				public string docCategory;
				public string docTitle;
				public int ngram;
				public string termsJson;	// 추후 Map으로 다시 변환.<Term, Freq>
			}

			public class NotifyTransferEnd
			{
				// 텀 전송 끝		1-5
				public bool flagEnd;
			}

			public class DocumentResponse
			{
				// 문서원본 전송, Document 그대로 전송해도 됨.	2-2
				public int documentID;
				public string collectDate;
				public string newsDate;
				public string siteName;
				public string pressName;
				public string url;
				public string category;
				public string title;
				public string body;
				public string comment;
				public string crawlerVersion;
			}

            public class ConceptToCount
            {
                public int conceptToCount;
            }

            public class LinkedListCount
            {
                public int linkedListCount=0;
            }


            public class ConceptToListResponse
            {   
                public string conceptToId;
                public string ConceptToTerm;
            }

            public class LinkedListResponse
            {
                public string linkedListTerm;
                public string metaInfo;
            }
		}

		public class Client2Server
		{
			public class RequestByDate
			{
				// 요청날짜 문서 1-1
				public string startDate;
				public string endDate;
				public bool bNaver;
				public bool bDaum;
				public bool bNate;
			}


			public class RequestTermTransfer
			{
				// 텀 전송해주삼 1-3
				public bool bTransfer;
			}


			public class DocumentRequest
			{
				// 문서 원본 요청 2-1
				public int documentID;
			}

            public class RequestAddDeleteList
            {
                public List<String> addDeleteList;
            }

            public class RequestAddThesaurus
            {
                public String conceptFrom;
                public String conceptTo;
                public String metaOntology;
            }
			
			public class RequestConcetpToList
            {
                public string cmd = "RequestConceptToList";
            }

            public class RequestLinkedList
            {
                public string cmd = "RequestLinkedList";
                public string conceptToId;
            }
		}
	}
}

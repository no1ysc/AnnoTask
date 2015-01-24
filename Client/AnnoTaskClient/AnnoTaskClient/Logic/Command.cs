using System;
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
            public class IsDuplicateUserID
            {
                public bool isDuplicate;
            }

            public class UserInfo
            {
                public string userId;
                public string userName;
                public bool isLoginSuccess;
            }

			public class SendDocumentCount
			{
				// 문서갯수 이만큼임.1-2
				public int doucumentCount;
			}

            // (기흥) 단어 총 갯수
            public class SendTermCount
            {
                public int totalTermCount;
            }

			public class TermTransfer
			{
				// 텀 1개씩 전송	1-4(반복)
				public string term;
                public int termFreq4RequestedCorpus;
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

            public class DocMeta
            {
                public string category;
                public string docIdList;
                public string title;

                // ArrayList
                //public List<string> category;
                //public List<int> docIdList;
                //public List<string> title;
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
				public string MetaOntology;		// 이승철 추가, 20150101
            }

            public class LinkedListResponse
            {
                public string linkedListTerm;
                public string metaInfo;		// 이거 필요한가요? (이승철), 20150101
            }

			public class ReturnAddDeleteList
			{
				public string term;
				public string returnValue;
				public string message;
			}

			public class ReturnAddThesaurus
			{
				public string term;
				public string returnValue;
				public string message;
			}
		}

		public class Client2Server
		{
            public class CheckUserID
            {
                public string checkUserID;
            }

            public class RegisterUserAccount
            {
                public string userName;
                public string userID; // email address
                public string password;
            }

            public class UserLogin
            {
                public string userID; // email address
                public string password;
            }

            //phase2.5 JobStart
            public class RequestAnnoTaskWork
            {
                public bool bRequestAnnoTaskWork;
            }

			public class RequestByDate
			{
				// 요청날짜 문서 1-1
				public string startDate;
				public string endDate;
				public bool bNaver;
				public bool bDaum;
				public bool bNate;
			}

            public class RequestDocMeta
            {
                public List<int> termLinkedDocIds;
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
				public RequestAddDeleteList(bool forced)
				{
					isForced = forced;
				}
				public bool isForced;
                public List<String> addDeleteList;
            }

            public class RequestAddThesaurus
            {
				public RequestAddThesaurus(bool forced)
				{
					isForced = forced;
				}
				public bool isForced;
                public string conceptFrom;
                public string conceptTo;
                public string metaOntology;
            }
			

			/// <summary>
			/// 작성자 : ??????
			/// 수정자 : 이승철
			/// 파라미터 추가
			/// </summary>
			public class RequestConcetpToList
            {
				public string cmd = "RequestConceptToList";
				public string term;
            }

            public class RequestLinkedList
            {
                public string cmd = "RequestLinkedList";
                public string conceptToId;
            }

			/// <summary>
			/// 작성자 : 이승철
			/// Idle Time에 서버로 보낼 HeartBeat,
			/// 서버단에서는 Check 할 필요 없음. 시간기록용.
			/// </summary>
			public class HeartBeat
			{
				public string cmd = "HeartBeat";
			}
		}
	}
}

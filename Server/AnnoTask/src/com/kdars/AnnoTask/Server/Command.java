package com.kdars.AnnoTask.Server;

public class Command {
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
			public String docCategory;
			public int ngram;
			public String termsJson;	// 추후 Map으로 다시 변환.<Term, Freq>
		}

		public class NotifyTransferEnd
		{
			// 텀 전송 끝		1-5
			public boolean flagEnd;
		}

		public class DocumentResponse
		{
			// 문서원본 전송, Document 그대로 전송해도 됨.	2-2
			public int documentID;
			public String collectDate;
			public String newsDate;
			public String siteName;
			public String pressName;
			public String url;
			public String category;
			public String title;
			public String body;
			public String comment;
			public String crawlerVersion;
		}
	}

	public class Client2Server
	{
		public class RequestByDate
		{
			// 요청날짜 문서 1-1
			public String startDate;
			public String endDate;
			public boolean bNaver;
			public boolean bDaum;
			public boolean bNate;
		}


		public class RequestTermTransfer
		{
			// 텀 전송해주삼 1-3
			public boolean bTransfer;
		}


		public class DocumentRequest
		{
			// 문서 원본 요청 2-1
			public int documentID;
		}
	}
}

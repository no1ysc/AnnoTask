using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient
{
	public class Configure
	{
		private static Configure thisClass = new Configure();
		public static Configure Instance
		{
			get { return thisClass; }
		}

		private string serverIP = "128.2.213.162";
		private int serverPort = 50000;
		private int connectionWaitTimeS = 10;
		private int limitDocumentCount = 5000;
		private int heartBeatDurationS = 10;	// HeartBeat 보내는 주기.

		public string ServerIP
		{
			get { return serverIP; }
		}
		public int ServerPort
		{
			get { return serverPort; }
		}
		public int ConnectionWaitTimeS
		{
			get { return connectionWaitTimeS; }
		}
		public int LimitDocumentCount
		{
			get { return limitDocumentCount; }
		}
		public int HeartBeatDurationS
		{
			get { return heartBeatDurationS; }
		}
	}
}

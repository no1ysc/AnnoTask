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

		private string serverIP = "192.168.1.12";
		private int serverPort = 5000;
		private int connectionWaitTimeS = 10;
		private int limitDocumentCount = 5000;


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
	}
}

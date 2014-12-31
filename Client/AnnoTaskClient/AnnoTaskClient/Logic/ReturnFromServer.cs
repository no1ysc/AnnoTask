using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	class ReturnFromServer
	{
		private string term;
		private string returnValue;
		private string msg;

		public string Term
		{
			get { return term; }
		}
		public string ReturnValue
		{
			get { return returnValue; }
		}
		public string Message
		{
			get { return msg; }
		}

		public ReturnFromServer(string term, string returnValue, string msg)
		{
			this.term = term;
			this.returnValue = returnValue;
			this.msg = msg;
		}
	}
}

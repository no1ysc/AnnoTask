using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient
{
	class ConceptTo
	{
		public ConceptTo(string conceptTo, string conceptToId)
		{
			conceptToTerm = conceptTo;
			this.conceptToId = conceptToId;
		}

		private string conceptToId;
		private string conceptToTerm;

		public string ConceptToID
		{
			get { return conceptToId; }
			set { conceptToId = value; }
		}
		public string ConceptToTerm
		{
			get { return conceptToTerm; }
			set { conceptToTerm = value; }
		}

		public override string ToString()
		{
			return conceptToTerm;
		}
	}
}

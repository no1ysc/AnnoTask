using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
    class ConceptTo
    {
        private string conceptToId;
        private string conceptToTerm;

        public string conceptToIds
        {
            get { return conceptToId; }
            set { conceptToId = value; }
        }
        public string conceptToTerms
        {
            get { return conceptToTerm; }
            set { conceptToTerm = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
    class LinkedList
    {
        private string linkedTerm;
        private string metaInfo;

        public string linkedTerms
        {
            get { return linkedTerm; }
            set { linkedTerm = value; }
        }

        public string metaInfos
        {
            get { return metaInfo; }
            set { metaInfo = value; }
        }
    }
}

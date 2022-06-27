using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Books.FB2
{
    internal class Chapter
    {
        private bool hasChildren;
        public bool HasChildren
        {
            get { return hasChildren; }
        }

        private Chapter[] children;
        public Chapter[] Children
        {
            get { return children; }
        }
    }
}

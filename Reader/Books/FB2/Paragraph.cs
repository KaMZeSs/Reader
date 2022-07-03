using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Books.FB2
{
    internal class Paragraph : FB2Component
    {
        private string text;
        public String Text { get { return text; } }

        public Paragraph(string text)
        {
            this.text = text;
        }
    }
}

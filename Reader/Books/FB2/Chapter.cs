using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Books.FB2
{
    internal class Chapter : FB2Component, IEnumerable
    {
        public bool isHasChildren
        {
            get { return children.Count is not 0; }
        }

        private string title;
        public string Title
        {
            get { return title; }
        }

        private List<FB2Component> children;
        public FB2Component[] Children
        {
            get { return children.ToArray(); }
        }

        public Chapter()
        {
            this.title = string.Empty;
            this.children = new();
        }
        public Chapter(string title) : this()
        {
            this.title = title;
        }
        public Chapter(string title, List<FB2Component> children) : this(title)
        {
            this.children = children;
        }

        public Chapter(XmlNode node)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)children).GetEnumerator();
        }

        public void AddComponent(FB2Component component)
        {
            children.Add(component);
        }
    }
}

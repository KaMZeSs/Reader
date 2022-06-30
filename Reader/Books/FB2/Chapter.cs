using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Books.FB2
{
    internal class Chapter
    {
        private bool hasChildren;
        public bool HasChildren
        {
            get { return hasChildren; }
        }
        /*
        Все говно
        Лучше хранить все в одном массиве
        Ладно название
        Но весь текст, дочерние главы (так как название), изображения в одном массиве
        Для этого нужно сделать интерфейс
        public interface Obj {
            что-то (можно забить и сделать ничего, а можно текст (название картинки, главы, текст абзаца))
            можно еще сделать перечисление (запоминать тип для удобного сохранения)
        }
        а потом наследовать его для всех элементов
        List<Obj> a = new()
        a.Add(new Chapter);
        a.Add(new Illustration);
        и тд
        */
        private Chapter[] children;
        public Chapter[] Children
        {
            get { return children; }
        }

        private string title;
        public string Title
        {
            get { return title; }
        }

        public Chapter()
        {
            hasChildren = false;
            children = new Chapter[0];
        }

        public Chapter(XmlNode node)
        {

        }
    }
}

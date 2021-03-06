using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Books.FB2
{
    internal class Illustration : FB2Component
    {
        public struct Img
        {
            public string Type;
            public byte[] Data;
        }

        private Img image;
        public Img Image { get { return image; } }

        private string name;
        public string Name { get { return name; } }

        public Illustration(Img image, string name)
        {
            this.image = image;
            this.name = name;
        }

        public static Img GetIllustrationByName(Illustration[] illustrations, string name)
        {
            var found = from res in illustrations.AsParallel() 
                        where res.name.Equals(name) select res.image;
            if (found is null)
            {
                throw new KeyNotFoundException($"There is no illustration which name is {name}");
            }
            return found.First();
        }
    }
}

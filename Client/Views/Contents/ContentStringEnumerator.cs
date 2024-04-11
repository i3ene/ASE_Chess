using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public class ContentStringEnumerator : IEnumerator<ContentCharacter>
    {
        private int position = -1;
        private readonly ContentString content;

        public ContentStringEnumerator(ContentString content)
        {
            this.content = content;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < content.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ContentCharacter Current
        {
            get { return content[position]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}

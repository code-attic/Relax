using System.Collections.Generic;
using Relax;

namespace RelaxDemo
{
    public class PagingDataLoader
    {
        private IDocumentRepository _couch;
        private int _page = 0;

        public IList<TestDocument> GetNext3Documents()
        {
            return _couch.GetAll<TestDocument>(3, ++_page);
        }

        public PagingDataLoader(IDocumentRepository couch)
        {
            _couch = couch;
        }
    }
}
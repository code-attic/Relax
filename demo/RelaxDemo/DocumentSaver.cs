using Relax;

namespace RelaxDemo
{
    public class DocumentSaver
    {
        private IDocumentRepository _couch;

        public void Save(TestDocument document)
        {
            _couch.Save(document);
        }

        public DocumentSaver(IDocumentRepository couch)
        {
            _couch = couch;
        }
    }
}
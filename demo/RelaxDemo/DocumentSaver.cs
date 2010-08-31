using System.Diagnostics;
using Relax;
using Symbiote.Core.Extensions;

namespace RelaxDemo
{
    public class DocumentSaver
    {
        private IDocumentRepository _couch;

        public void Save(TestDocument document)
        {
            var watch = Stopwatch.StartNew();
            _couch.Save(document);
            watch.Stop();
            "Save completed in {0}"
                .ToInfo<ChangeWatcher>(watch.ElapsedMilliseconds);
        }

        public DocumentSaver(IDocumentRepository couch)
        {
            _couch = couch;
        }
    }
}
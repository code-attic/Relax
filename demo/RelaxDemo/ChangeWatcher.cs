using Relax;
using Symbiote.Core.Extensions;

namespace RelaxDemo
{
    public class ChangeWatcher
    {
        private IDocumentRepository _couch;

        public void Start()
        {
            _couch.HandleUpdates<TestDocument>(0, Update, null);
        }

        private void Update(string database, ChangeRecord obj)
        {
            "An update was posted to couch: \r\n\t {0}"
                .ToInfo<ChangeWatcher>(obj.Document);
        }

        public void Stop()
        {
            _couch.StopChangeStreaming<TestDocument>();
        }

        public ChangeWatcher(IDocumentRepository couch)
        {
            _couch = couch;
        }
    }
}
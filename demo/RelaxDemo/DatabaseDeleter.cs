using Relax;

namespace RelaxDemo
{
    public class DatabaseDeleter
    {
        private ICouchServer _couch;

        public void Nuke()
        {
            _couch.DeleteDatabase<TestDocument>();
        }

        public DatabaseDeleter(ICouchServer couch)
        {
            _couch = couch;
        }
    }
}
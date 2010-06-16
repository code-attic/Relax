using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class ViewQuery
    {
        protected CouchUri _uri;

        public ViewQuery AllowStale()
        {
            _uri.StaleOk();
            return this;
        }

        public ViewQuery DescendingOrder()
        {
            _uri.Descending();
            return this;
        }

        public ViewQuery EndingWith<TKey>(TKey key)
        {
            _uri.EndKey(key);
            return this;
        }
        
        public ViewQuery Group()
        {
            _uri.Group();
            return this;
        }

        public ViewQuery Group(int groupLevel)
        {
            _uri.Group(groupLevel);
            return this;
        }

        public ViewQuery LimitTo(int count)
        {
            _uri.Limit(count);
            return this;
        }

        public ViewQuery NoReduce()
        {
            _uri.NoReduce();
            return this;
        }

        public ViewQuery PagedBy(int pageNumber, int pageSize)
        {
            int number = (pageNumber - 1) * pageSize;
            _uri.Skip(number < 0 ? 0 : number).Limit(pageSize);
            return this;
        }

        public ViewQuery Skip(int count)
        {
            _uri.Skip(count);
            return this;
        }

        public ViewQuery StartingWith<TKey>(TKey key)
        {
            _uri.StartKey(key);
            return this;
        }

        public ViewQuery WithKey<TKey>(TKey key)
        {
            _uri.Key(key);
            return this;
        }

        public ViewQuery WithKeyAndRevision<TKey,TRev>(TKey key, TRev revision)
        {
            _uri.KeyAndRev(key, revision);
            return this;
        }
        
        public ViewQuery(CouchUri uri)
        {
            _uri = uri;
        }
    }
}

using System.Collections.Generic;
using Symbiote.Core;
using Symbiote.Core.Persistence;

namespace Relax.Impl {
    
    /// <summary>
    /// This class provides generic abstractions that Symbiote's
    /// Actor system requires in order to handle caching and
    /// persistence concerns. Not recommended for use outside
    /// of this use case unless you truly understand the
    /// underlying calls this makes to the IRedisClient API
    /// </summary>
    public class CouchProxy :
        IKeyValueStore,
        IRepository
    {
        public IKeyAccessor KeyAccessor { get; set; }
        public IDocumentRepository Repository { get; set; }

        public bool Delete<T>( string key )
        {
            return Repository.DeleteDocument<T>( key );
        }

        public T Get<T>( string key )
        {
            return Repository.Get<T>( key );
        }

        public IEnumerable<T> GetAll<T>()
        {
            return Repository.GetAll<T>();
        }

        public bool Persist<T>( string key, T instance )
        {
            return Repository.Save( key, instance );
        }

        public bool Delete<T>( T instance ) where T : class
        {
            return Repository.DeleteDocument<T>( KeyAccessor.GetId( instance ) );
        }

        public bool Persist<T>( T instance ) where T : class
        {
            return Repository.Save( KeyAccessor.GetId(instance), instance );
        }

        public CouchProxy( IKeyAccessor keyAccessor, IDocumentRepository repository )
        {
            KeyAccessor = keyAccessor;
            Repository = repository;
        }
    }
}

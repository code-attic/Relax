using Machine.Specifications;
using Moq;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public abstract class with_couch_cache_provider : with_cache_provider
    {
        protected static Mock<ICacheKeyBuilder> cacheKeyBuilderMock;
        protected static Mock<IKeyAssociationManager> keyAssociationManagerMock;

        protected static ICacheKeyBuilder CacheKeyBuilder { get { return cacheKeyBuilderMock.Object; } }
        protected static IKeyAssociationManager KeyAssociationManager { get { return keyAssociationManagerMock.Object; } }

        protected static CouchCacheProvider CouchCacheProvider 
        { 
            get 
            { 
                return new CouchCacheProvider(
                    CacheProvider, 
                    KeyAssociationManager,
                    CacheKeyBuilder);
            }
        }

        private Establish context = () =>
                                        {
                                            cacheKeyBuilderMock = new Mock<ICacheKeyBuilder>();
                                            keyAssociationManagerMock = new Mock<IKeyAssociationManager>();
                                        };
    }
}
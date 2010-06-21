using System;
using System.Collections.Generic;
using System.Text;
using Machine.Specifications;
using Moq;
using Relax.Impl.Cache;
using Symbiote.Core.Cache;

namespace Relax.Tests.Caching
{
    public abstract class with_cache_provider
    {
        protected static Mock<ICacheProvider> cacheProviderMock;
        protected static ICacheProvider CacheProvider { get { return cacheProviderMock.Object; } }

        private Establish context = () =>
                                        {
                                            cacheProviderMock = new Mock<ICacheProvider>();
                                        };
    }

    public abstract class with_couch_cache_provider : with_cache_provider
    {
        protected static Mock<ICacheKeyBuilder> cacheKeyBuilderMock;
        protected static ICacheKeyBuilder CacheKeyBuilder { get { return cacheKeyBuilderMock.Object; } }

        protected static CouchCacheProvider CouchCacheProvider { get { return new CouchCacheProvider(CacheProvider, CacheKeyBuilder);}}

        private Establish context = () =>
                                        {
                                            cacheKeyBuilderMock = new Mock<ICacheKeyBuilder>();
                                        };
    }

    public class when_adding_cross_reference : with_couch_cache_provider
    {
        
    }
}

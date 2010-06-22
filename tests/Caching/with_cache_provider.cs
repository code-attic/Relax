using System;
using System.Collections.Generic;
using System.Text;
using Machine.Specifications;
using Moq;
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
}

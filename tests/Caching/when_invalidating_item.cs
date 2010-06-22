using Machine.Specifications;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public class when_invalidating_item : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;

        private Because of = () =>
                                 {
                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("item_1"))
                                         .Returns(new string[] {"cache_key1", "cache_key2"});

                                     cacheProviderMock
                                         .Setup(x => x.Remove("cache_key1"))
                                         .AtMostOnce();

                                     cacheProviderMock
                                         .Setup(x => x.Remove("cache_key2"))
                                         .AtMostOnce();

                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.InvalidateItem("item_1");
                                 };

        private It should_get_associations_from_key_association_manager = () => keyAssociationManagerMock.VerifyAll();
        private It should_remove_associated_keys_from_the_cache = () => cacheProviderMock.VerifyAll();
    }
}
using Machine.Specifications;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public class when_deleting_item_by_key : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;

        private Because of = () =>
                                 {
                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>("item_1"))
                                         .Returns("TestDocument_item_1");

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_item_1"))
                                         .Returns(new string[] { "cache_key1", "cache_key2" });

                                     cacheProviderMock
                                         .Setup(x => x.Remove("cache_key1"))
                                         .AtMostOnce();

                                     cacheProviderMock
                                         .Setup(x => x.Remove("cache_key2"))
                                         .AtMostOnce();

                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.Delete<TestDocument>("item_1", x => called_delegate = true);
                                 };

        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
        private It should_get_associations_from_key_association_manager = () => keyAssociationManagerMock.VerifyAll();
        private It should_remove_associated_keys_from_the_cache = () => cacheProviderMock.VerifyAll();
    }
}
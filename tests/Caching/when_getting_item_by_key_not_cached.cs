using Machine.Specifications;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public class when_getting_item_by_key_not_cached : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;
        protected static TestDocument document;
        protected static TestDocument result;

        private Because of = () =>
                                 {
                                     document = new TestDocument() {Message ="Testing"};

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>("item_1"))
                                         .Returns("TestDocument_item_1");

                                     cacheProviderMock
                                         .Setup(x => x.Get<TestDocument>("TestDocument_item_1"))
                                         .Returns(default(TestDocument));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_item_1", document));

                                     couchCacheProvider = CouchCacheProvider;
                                     result = couchCacheProvider.Get("item_1", x =>
                                                                                   {
                                                                                       called_delegate = true;
                                                                                       return document;
                                                                                   });
                                 };

        private It should_create_cache_key = () => cacheKeyBuilderMock.VerifyAll();
        private It should_request_item_from_cache_and_store_item = () => cacheProviderMock.VerifyAll();
        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
        private It should_return_instance = () => result.ShouldEqual(document);
    }
}
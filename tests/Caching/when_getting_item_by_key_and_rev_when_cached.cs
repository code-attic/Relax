using Machine.Specifications;
using Moq;
using Relax.Impl.Cache;
using It = Machine.Specifications.It;

namespace Relax.Tests.Caching
{
    public class when_getting_item_by_key_and_rev_when_cached : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;
        protected static TestDocument document;
        protected static TestDocument result;

        private Because of = () =>
                                 {
                                     document = new TestDocument() { Message = "Testing" };

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>("item_1", "rev_1"))
                                         .Returns("TestDocument_item_1_rev_1");

                                     cacheProviderMock
                                         .Setup(x => x.Get<TestDocument>("TestDocument_item_1_rev_1"))
                                         .Returns(document);

                                     couchCacheProvider = CouchCacheProvider;
                                     result = couchCacheProvider.Get("item_1", "rev_1", (x,y) =>
                                                                                            {
                                                                                                called_delegate = true;
                                                                                                return document;
                                                                                            });
                                 };

        private It should_create_cache_key = () => cacheKeyBuilderMock.VerifyAll();
        private It should_request_item_from_cache = () => cacheProviderMock.VerifyAll();

        private It should_not_store_item =
            () => cacheProviderMock.Verify(x => x.Store("TestDocument_item_1_rev_1", document), Times.Never());
        private It should_not_call_delegate = () => called_delegate.ShouldBeFalse();
        private It should_return_instance = () => result.ShouldEqual(document);
    }
}
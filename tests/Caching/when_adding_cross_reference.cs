using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using Relax.Impl.Cache;
using It = Machine.Specifications.It;

namespace Relax.Tests.Caching
{
    public class when_adding_cross_reference : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;

        private Because of = () =>
                                 {
                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("item_1", "cache_key"));
                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.AddCrossReference("item_1", "cache_key");
                                 };

        private It calling_add_cross_reference_should_add_association_to_key_manager = () => 
                                                                                       keyAssociationManagerMock.Verify();
    }

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

    public class when_deleting_item_by_key_and_rev : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;

        private Because of = () =>
        {
            cacheKeyBuilderMock
                .Setup(x => x.GetKey<TestDocument>("item_1","rev_1"))
                .Returns("TestDocument_item_1_rev_1");

            keyAssociationManagerMock
                .Setup(x => x.GetAssociations("TestDocument_item_1_rev_1"))
                .Returns(new string[] { "cache_key1", "cache_key2" });

            cacheProviderMock
                .Setup(x => x.Remove("cache_key1"))
                .AtMostOnce();

            cacheProviderMock
                .Setup(x => x.Remove("cache_key2"))
                .AtMostOnce();

            couchCacheProvider = CouchCacheProvider;
            couchCacheProvider.Delete<TestDocument>("item_1","rev_1", (x,y) => called_delegate = true);
        };

        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
        private It should_get_associations_from_key_association_manager = () => keyAssociationManagerMock.VerifyAll();
        private It should_remove_associated_keys_from_the_cache = () => cacheProviderMock.VerifyAll();
    }

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

    public class when_getting_item_by_key_when_cached : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;
        protected static TestDocument document;
        protected static TestDocument result;

        private Because of = () =>
        {
            document = new TestDocument() { Message = "Testing" };

            cacheKeyBuilderMock
                .Setup(x => x.GetKey<TestDocument>("item_1"))
                .Returns("TestDocument_item_1");

            cacheProviderMock
                .Setup(x => x.Get<TestDocument>("TestDocument_item_1"))
                .Returns(document);

            couchCacheProvider = CouchCacheProvider;
            result = couchCacheProvider.Get("item_1", x =>
            {
                called_delegate = true;
                return document;
            });
        };

        private It should_create_cache_key = () => cacheKeyBuilderMock.VerifyAll();
        private It should_request_item_from_cache = () => cacheProviderMock.VerifyAll();

        private It should_not_store_item =
            () => cacheProviderMock.Verify(x => x.Store("TestDocument_item_1", document), Times.Never());
        private It should_not_call_delegate = () => called_delegate.ShouldBeFalse();
        private It should_return_instance = () => result.ShouldEqual(document);
    }
}
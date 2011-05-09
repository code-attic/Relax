using System;
using System.Collections.Generic;
using Machine.Specifications;
using Relax.Impl.Cache;
using Symbiote.Core;

namespace Relax.Tests.Caching
{
    public class when_saving_a_collection : with_couch_cache_provider
    {
        protected static CouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;
        protected static IList<TestDocument> documents;
        protected static IList<TestDocument> result;
        protected static Guid _documentId1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        protected static Guid _documentId2 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        protected static TestDocument document1;
        protected static TestDocument document2;

        private Because of = () =>
                                 {
                                     Assimilate.Initialize();
                                     document1 = new TestDocument
                                                     {
                                                         Message = "Doc 1",
                                                         DocumentId = _documentId1,
                                                         DocumentRevision = "1"
                                                     };
                                     document2 = new TestDocument
                                                     {
                                                         Message = "Doc 2",
                                                         DocumentId = _documentId2,
                                                         DocumentRevision = "1"
                                                     };
                                     documents = new List<TestDocument>(){ document1, document2 };

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId1))
                                         .Returns("TestDocument_1");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId1, "1"))
                                         .Returns("TestDocument_1_1");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId2))
                                         .Returns("TestDocument_2");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId2, "1"))
                                         .Returns("TestDocument_2_1");

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_1"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_1_1"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_2"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_2_1"));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_1", document1));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_1_1", document1));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_2", document2));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_2_1", document2));

                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.SaveAll(documents, x => called_delegate = true);
                                 };

        private It should_update_cache = () => cacheProviderMock.VerifyAll();
        private It should_get_key_associations = () => keyAssociationManagerMock.VerifyAll();
        private It should_call_key_builder = () => cacheKeyBuilderMock.VerifyAll();
        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
    }
}
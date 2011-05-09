using System;
using System.Collections.Generic;
using Machine.Specifications;
using Relax.Impl.Cache;
using Symbiote.Core;

namespace Relax.Tests.Caching
{
    public class when_getting_paged_list_uncached : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;
        protected static bool called_delegate;
        protected static IList<TestDocument> documents;
        protected static IList<TestDocument> result;
        protected static Guid _documentId1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        protected static Guid _documentId2 = Guid.Parse("00000000-0000-0000-0000-000000000002");

        private Because of = () =>
                                 {
                                     Assimilate.Initialize();

                                     documents = new List<TestDocument>()
                                                     {
                                                         new TestDocument
                                                             {
                                                                 Message = "Doc 1", 
                                                                 DocumentId = _documentId1, 
                                                                 DocumentRevision = "1"
                                                             },
                                                         new TestDocument
                                                             {
                                                                 Message = "Doc 2", 
                                                                 DocumentId = _documentId2,
                                                                 DocumentRevision = "1"
                                                             }
                                                     };

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetListKey<TestDocument>(3, 10))
                                         .Returns("TestDocument_list_10_3");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId1))
                                         .Returns("TestDocument_Doc 1");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId1, "1"))
                                         .Returns("TestDocument_Doc 1_1");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId2))
                                         .Returns("TestDocument_Doc 2");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId2, "1"))
                                         .Returns("TestDocument_Doc 2_1");

                                     cacheProviderMock
                                         .Setup(x => x.Get<IList<TestDocument>>("TestDocument_list_10_3"))
                                         .Returns(default(List<TestDocument>));

                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("TestDocument_Doc 1", "TestDocument_list_10_3"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("TestDocument_Doc 1_1", "TestDocument_list_10_3"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("TestDocument_Doc 2", "TestDocument_list_10_3"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("TestDocument_Doc 2_1", "TestDocument_list_10_3"));

                                     couchCacheProvider = CouchCacheProvider;
                                     result = couchCacheProvider.GetAllPaged(10, 3, (x, y) =>
                                                                                        {
                                                                                            called_delegate = true;
                                                                                            return documents;
                                                                                        });
                                 };

        private It should_build_cache_keys_correctly = () => cacheKeyBuilderMock.VerifyAll();
        private It should_request_list_from_cache_provider_and_store_result = () => cacheProviderMock.VerifyAll();
        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
        private It should_have_correct_result = () => result.ShouldEqual(documents);
    }
}
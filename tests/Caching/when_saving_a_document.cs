using System;
using Machine.Specifications;
using Relax.Impl.Cache;
using Symbiote.Core;

namespace Relax.Tests.Caching
{
    public class when_saving_a_document : with_couch_cache_provider
    {
        protected static CouchCacheProvider couchCacheProvider;
        protected static TestDocument document;
        protected static bool called_delegate;
        private static Guid _documentId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        private Because of = () =>
                                 {
                                     Assimilate.Initialize();
                                     document = new TestDocument() {DocumentId = _documentId, DocumentRevision = "1"};

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId))
                                         .Returns("TestDocument_1");

                                     cacheKeyBuilderMock
                                         .Setup(x => x.GetKey<TestDocument>(_documentId, "1"))
                                         .Returns("TestDocument_1_1");

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_1"));

                                     keyAssociationManagerMock
                                         .Setup(x => x.GetAssociations("TestDocument_1_1"));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_1", document));

                                     cacheProviderMock
                                         .Setup(x => x.Store("TestDocument_1_1", document));

                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.Save(document, x => called_delegate = true);
                                 };

        private It should_update_cache = () => cacheProviderMock.VerifyAll();
        private It should_get_key_associations = () => keyAssociationManagerMock.VerifyAll();
        private It should_call_key_builder = () => cacheKeyBuilderMock.VerifyAll();
        private It should_call_delegate = () => called_delegate.ShouldBeTrue();
    }
}
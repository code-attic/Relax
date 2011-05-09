using Machine.Specifications;
using Relax.Impl.Http;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Tests.Commands.SaveCommand
{
    public class when_saving_poco_list_to_database : with_poco_list_persistence
    {
        protected static string url;
        protected static string response;
        protected static IProvideDocumentMetadata MetadataProvider;

        private Because of = () =>
            {
                url = @"http://localhost:5984/relax/_bulk_docs";

                response = @"[{ ok:true, id:""1"", rev: ""2""}, { ok:true, id:""2"", rev: ""2""}, { ok:true, id:""3"", rev: ""2""}]";

                MetadataProvider = Assimilate.GetInstanceOf<IProvideDocumentMetadata>();
                MetadataProvider.Clear();

                mockAction
                    .Setup( x => x
                                     .Post( Moq.It.Is<CouchUri>( i => url.ToString() == url ),
                                            Moq.It.IsAny<string>() ) )
                    .Returns( response )
                    .AtMostOnce();

                command = factory.CreateSaveDocumentsCommand();
                command.SaveAll( "Couch", testDocs );
            };
        
        private It should_call_action = () => mockAction.Verify();
        private It should_update_revision = () =>
            {
                MetadataProvider.GetMetadata( "1" )._rev.ShouldEqual( "2" );
                MetadataProvider.GetMetadata( "2" )._rev.ShouldEqual( "2" );
                MetadataProvider.GetMetadata( "3" )._rev.ShouldEqual( "2" );
            };
    }
}
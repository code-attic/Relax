using Machine.Specifications;
using Relax.Impl.Http;

namespace Relax.Tests.Commands.SaveCommand
{
    public class when_saving_list_to_database : with_serialized_bulk_persist
    {
        protected static string url;
        protected static string response;

        private Because of = () =>
                                 {
                                     url = @"http://localhost:5984/relax/_bulk_docs";

                                     response = @"[{ ok:true, id:""doc 1"", rev: ""2""}]";

                                     mockAction
                                         .Setup(x => x.Post(Moq.It.Is<CouchUri>(i => url.ToString() == url), Moq.It.IsAny<string>()))
                                         .Returns(response)
                                         .AtMostOnce();
                                     command = factory.CreateSaveDocumentsCommand();
                                     command.SaveAll("Couch", new object[] {testDoc});
                                 };
        
        private It should_call_action = () => mockAction.Verify();
        private It should_update_revision = () => testDoc.DocumentRevision.ShouldEqual("2");
    }
}
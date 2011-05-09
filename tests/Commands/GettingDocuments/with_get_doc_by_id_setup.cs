using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Http;

namespace Relax.Tests.Commands.GettingDocuments
{
    public abstract class with_get_doc_by_id_setup : with_command_factory
    {
        protected static string response;
        protected static string url;
        protected static GetDocumentCommand command;
        
        private Establish context = () =>
                                        {
                                            url = @"http://localhost:5984/relax/1";
                                            response = @"{""$type"":""Relax.Tests.Commands.TestDoc,Relax.Tests"",""_id"":""1"",""_rev"":""1"",""Message"":""Test""}";
                                            mockAction
                                                .Setup(x => x.Get(Moq.It.Is<CouchUri>(i => i.ToString() == url)))
                                                .Returns(response)
                                                .AtMostOnce();
                                            command = factory.CreateGetDocumentCommand();
                                        };
    }
}
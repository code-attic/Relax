using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;

namespace Relax.Tests.Repository
{
    public abstract class with_from_view_command : with_configuration
    {
        protected static string jsonResult;
        protected static Mock<IHttpAction> commandMock;
        protected static CouchUri uri;
        protected static IDocumentRepository repository;
        protected static CouchUri couchUri 
        {
            get
            {
                return Moq.It.Is<CouchUri>(u => u.ToString().Equals(uri.ToString()));
            }
        }

        private Establish context = () =>
                                        {
                                            repository = Assimilate.GetInstanceOf<DocumentRepository>();
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .Design("test")
                                                .View("test")
                                                .NoReduce();

                                            jsonResult = @"
{
    total_rows: 2,
    offset: 0,
    rows:
        [
            {
                doc: { _id: ""test"", _rev: ""1"", Message: ""Hi"" }
            },
            {
                doc: { _id: ""_design/mydesigndoc"" }
            }
        ]
}
";
                                            commandMock = new Mock<IHttpAction>();
                                            commandMock
                                                .Setup(x => x.Get( couchUri ))
                                                .Returns(jsonResult);

                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
using Machine.Specifications;
using Relax.Impl.Http;

namespace Relax.Tests.Server
{
    public abstract class with_create_delete_database_command : with_couch_server
    {
        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984, "relax");
                                            commandMock.Setup(x => x.Delete(couchUri));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
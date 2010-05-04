using Machine.Specifications;
using Relax.Impl;

namespace Relax.Tests.Server
{
    public abstract class with_list_databases_command : with_couch_server
    {
        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984, "_all_dbs");
                                            commandMock.Setup(x => x.Get(couchUri))
                                                .Returns("[ \"one\", \"two\", \"three\" ]");
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
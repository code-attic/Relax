using Machine.Specifications;
using Relax.Impl;

namespace Relax.Tests.Server
{
    public abstract class with_view_cleanup_command : with_couch_server
    {
        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984, "testdocument")
                                                .CleanupViews();
                                            commandMock.Setup(x => x.Post(couchUri));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
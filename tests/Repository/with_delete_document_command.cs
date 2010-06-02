using System;
using Machine.Specifications;
using Relax.Impl;
using Relax.Impl.Http;

namespace Relax.Tests.Repository
{
    public abstract class with_delete_document_command : with_document_repository
    {
        protected static Guid id;

        private Establish context = () =>
                                        {
                                            id = Guid.NewGuid();
                                            uri = new CouchUri("http", "localhost", 5984, "testdocument").IdAndRev(id, "1");
                                            commandMock.Setup(x => x.Delete(couchUri));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
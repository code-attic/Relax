using System;
using Machine.Specifications;
using Relax.Impl;
using Relax.Impl.Http;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Repository
{
    public abstract class with_get_all_documents_command : with_document_repository
    {
        protected static Guid id;

        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984, "testdocument")
                                                .ListAll()
                                                .IncludeDocuments();
                                            commandMock.Setup(x => x.Get(couchUri))
                                                .Returns("{{ offset: \"0\", total_rows: \"1\", rows : [ {{ id : \"{0}\", key : \"{0}\", doc : {{_id : \"{0}\", _rev : \"2\", Message : \"Hello\" }} }} ] }}".AsFormat(id));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
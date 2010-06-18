using Machine.Specifications;
using Relax.Impl;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Server
{
    public abstract class with_continuous_replication : with_couch_server
    {
        protected static CouchUri targetUri;
        protected static CouchUri sourceUri;
        protected static string replication;

        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984).Replicate();
                                            sourceUri = new CouchUri("http", "localhost", 5984, "relax");
                                            targetUri = new CouchUri("admin", "password", "http", "remotehost", 5984);

                                            replication = ReplicationCommand.Continuous(sourceUri, targetUri).ToJson(false);

                                            commandMock
                                                .Setup(x => x.Post(couchUri, replication));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
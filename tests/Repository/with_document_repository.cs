using Machine.Specifications;
using Moq;
using Relax.Impl;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using StructureMap;
using Symbiote.Core;

namespace Relax.Tests.Repository
{
    public abstract class with_document_repository : with_configuration
    {
        protected static IDocumentRepository repository;
        protected static CouchUri uri;
        protected static Mock<IHttpAction> commandMock;
        protected static CouchUri couchUri 
        {
            get
            {
                return Moq.It.Is<CouchUri>(u => u.ToString().Equals(uri.ToString()));
            }
        }
        
        private Establish context = () =>
                                        {
                                            commandMock = new Mock<IHttpAction>();
                                            repository = ObjectFactory.GetInstance<DocumentRepository>();
                                        };
    }


}
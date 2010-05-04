using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public abstract class with_test_document : with_document_repository
    {
        protected static TestDocument document;
        private Establish context = () => { document = new TestDocument() {Message = "hi"}; };
    }
}
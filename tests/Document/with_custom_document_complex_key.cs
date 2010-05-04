using Machine.Specifications;

namespace Relax.Tests.Document
{
    public class with_custom_document_complex_key : with_custom_document_simple_key
    {
        protected static Driver driver;

        private Establish context = () => driver = new Driver(person);
    }
}
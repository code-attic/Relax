using Machine.Specifications;
using Symbiote.Core;
using Symbiote.StructureMap;

namespace Relax.Tests.Document
{
    public class with_custom_document_complex_key : with_custom_document_simple_key
    {
        protected static Driver driver;

        private Establish context = () =>
                                        {
                                            Assimilate.Core<StructureMapAdapter>();
                                            driver = new Driver(person);
                                        };
    }
}
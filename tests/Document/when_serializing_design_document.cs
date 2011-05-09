using Machine.Specifications;
using Symbiote.Core.Serialization;

namespace Relax.Tests.Document
{
    public class when_serializing_design_document : with_design_document
    {
        protected static string json;

        private Because of = () =>
                                 {
                                     json = doc.ToJson(false);
                                 };

        private It should_not_be_empty = () => 
            json.ShouldNotBeEmpty();
    }
}

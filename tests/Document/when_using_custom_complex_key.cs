using Machine.Specifications;
using Symbiote.Core.Serialization;

namespace Relax.Tests.Document
{
    [Subject("custom document")]
    public class when_using_custom_complex_key : with_custom_document_complex_key
    {
        private static string jsonKey;
        private Because of = () =>
                                 {
                                     jsonKey = person.ToJson(false);
                                 };

        private It should_have_expected_key = () => driver.DocumentId.ShouldEqual(person);
        private It should_have_correct_json = () => driver.GetDocumentIdAsJson().ShouldEqual(jsonKey);
    }
}
using Machine.Specifications;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Document
{
    [Subject("custom document")]
    public class when_using_custom_key : with_custom_document_simple_key
    {
        private It should_have_expected_key = () => person.DocumentId.ShouldEqual("{0}, {1} born {2}".AsFormat(person.LastName, person.FirstName, person.DateOfBirth));
    }
}
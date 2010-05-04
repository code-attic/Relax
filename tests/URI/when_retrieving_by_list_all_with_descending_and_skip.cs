using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_list_all_with_descending_and_skip : with_basic_uri
    {
        private Because of = () => uri.ListAll().Descending().Skip(5);

        private It should_append_all_docs_and_descending_and_skip_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/_all_docs?descending=true&skip=5");
    }
}
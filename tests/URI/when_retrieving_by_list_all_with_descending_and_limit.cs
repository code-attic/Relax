using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_list_all_with_descending_and_limit : with_basic_uri
    {
        private Because of = () => uri.ListAll().Descending().Limit(100);

        private It should_append_all_docs_and_descending_and_limit_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/_all_docs?descending=true&limit=100");
    }
}
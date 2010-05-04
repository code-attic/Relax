using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_list_all : with_basic_uri
    {
        private Because of = () => uri.ListAll();

        private It should_append_all_docs_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/_all_docs");
    }
}
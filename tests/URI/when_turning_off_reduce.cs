using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_turning_off_reduce : with_basic_uri
    {
        private Because of = () => uri.NoReduce();

        private It should_append_reduce_false
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax?reduce=false");
    }
}
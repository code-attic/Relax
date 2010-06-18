using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_creating_baseline_uri : with_basic_uri
    {
        private It should_have_proper_format = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax");    
    }
}
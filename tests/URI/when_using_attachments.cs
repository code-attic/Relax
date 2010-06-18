using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_using_attachments : with_basic_uri
    {
        private Because of = () => uri.Attachment("foo.txt");

        private It should_append_attachment
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/foo.txt");
    }
}
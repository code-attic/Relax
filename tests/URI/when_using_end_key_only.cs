using System;
using Machine.Specifications;
using Symbiote.Core.Extensions;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_using_end_key_only : with_basic_uri
    {
        private static DateTime date = DateTime.Now;
        private static string complexKey;

        private Because of = () =>
                                 {
                                     complexKey = new object[] { "id", 1}.ToJson(false);
                                     complexKey = Uri.EscapeUriString(complexKey);
                                     uri.EndKey(complexKey);
                                 };

        private It should_append_reduce_false
            //= () => uri.ToString().ShouldEqual("http://localhost:5984/test?endkey=%5B%22id%22,1%5D");
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test?endkey=""%5B%22id%22,1%5D""");
    }
}
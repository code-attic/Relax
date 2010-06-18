using System;
using Machine.Specifications;
using Symbiote.Core.Extensions;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_using_start_key_only : with_basic_uri
    {
        private static DateTime date = DateTime.Now;
        private static string complexKey;

        private Because of = () =>
                                 {
                                     complexKey = "test+10";
                                     uri.StartKey(complexKey);
                                 };

        private It should_append_reduce_false
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax?startkey=""test%2b10""");
    }
}
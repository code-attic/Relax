using Machine.Specifications;

namespace Relax.Tests.URI.ViewQuery
{
    public class when_testing_view_query_scenario_2 : with_couch_uri
    {
        private Because of = () =>
                                 {
                                     uriMock
                                         .Setup(x => x.Group(2))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Skip(20))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Limit(10))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Key("doc 1"))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.KeyAndRev("doc 1", "2"))
                                         .Returns(uriMock.Object);
            
                                     query
                                         .Group(2)
                                         .PagedBy(3, 10)
                                         .StartingWith("doc 1")
                                         .WithKey("doc 1")
                                         .WithKeyAndRevision("doc 1", "2");
                                 };

        private Machine.Specifications.It should_call_all_expected_uri_methods = () => uriMock.VerifyAll();
    }
}
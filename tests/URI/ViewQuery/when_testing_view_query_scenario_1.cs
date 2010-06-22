using Machine.Specifications;

namespace Relax.Tests.URI.ViewQuery
{
    public class when_testing_view_query_scenario_1 : with_couch_uri
    {
        private Because of = () =>
                                 {
                                     uriMock
                                         .Setup(x => x.StaleOk())
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Descending())
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Group())
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.IncludeDocuments())
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Limit(10))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.NoReduce())
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.Skip(10))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.StartKey("doc 1"))
                                         .Returns(uriMock.Object);

                                     uriMock
                                         .Setup(x => x.EndKey("doc 100"))
                                         .Returns(uriMock.Object);

                                     query
                                         .AllowStale()
                                         .DescendingOrder()
                                         .Group()
                                         .UseDocumentsInsteadOfValue()
                                         .LimitTo(10)
                                         .NoReduce()
                                         .Skip(10)
                                         .StartingWith("doc 1")
                                         .EndingWith("doc 100");
                                 };

        private Machine.Specifications.It should_call_all_expected_uri_methods = () => uriMock.VerifyAll();
    }
}
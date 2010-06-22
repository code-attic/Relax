using Machine.Specifications;

namespace Relax.Tests.Commands.Querying
{
    public class when_querying_relax_lucene : with_relax_query
    {
        protected static object[] result;

        private Because of = () =>
                                 {
                                     result = command.GetDocumentIdsForQuery<TestDoc>(x => x.Message.StartsWith("Hi"));
                                 };
        
        private It should_get_results = () => result[0].ShouldEqual("1");
    }
}
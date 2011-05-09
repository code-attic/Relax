using System.Diagnostics;
using Machine.Specifications;
using Relax.Impl.Commands;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_doc_by_id_timed : with_get_doc_by_id_setup
    {
        protected static CommandResult result;
        protected static TestDoc model;
        protected static string json;
        protected static Stopwatch watch;

        private Because of = () =>
            {
                watch = Stopwatch.StartNew();

                result = command.GetDocument<TestDoc>("1");

                for( int i = 0; i < 10000; i++ )
                {
                    model = command.Deserialize<TestDoc>(result.Json);    
                }
                                     
                watch.Stop();
            };

        private It should_take_less_than_1_second = () => watch.ElapsedMilliseconds.ShouldBeLessThan( 1100 );
    }
}
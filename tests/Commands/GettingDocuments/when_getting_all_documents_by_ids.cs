using System.Diagnostics;
using System.Linq;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_all_documents_by_ids : with_get_all_docs_by_ids
    {
        protected static CommandResult result;
        protected static ViewResult<TestDoc> viewResult;
        protected static string json;

        private Because of = () =>
                                 {
                                     result = command.GetDocuments<TestDoc>(new object[] {"1","2"});
                                     viewResult = command.DeserializeView<TestDoc>( result.Json );
                                     json = result.Json.Replace("\r\n", "").Replace(" ", "");
                                 };

        private It should_produce_expected_json = () => json.ShouldEqual(response);

        private It should_have_two_rows = () => ShouldExtensionMethods.ShouldEqual(viewResult.GetList().Count(), 2);

        private It should_create_valid_instances = () =>
                                                       {
                                                           var firstDoc = viewResult.GetList().ToList()[0];
                                                           firstDoc.DocumentId.ShouldEqual("1");
                                                           firstDoc.DocumentRevision.ShouldEqual("1");
                                                           firstDoc.Message.ShouldEqual("Test1");

                                                           var secondDoc = viewResult.GetList().ToList()[1];
                                                           secondDoc.DocumentId.ShouldEqual("2");
                                                           secondDoc.DocumentRevision.ShouldEqual("1");
                                                           secondDoc.Message.ShouldEqual("Test2");
                                                       };

        private It should_call_action = () => mockAction.Verify();
    }

    public class when_getting_all_documents_by_ids_timed : with_get_all_docs_by_ids
    {
        protected static CommandResult result;
        protected static ViewResult<TestDoc> viewResult;
        protected static string json;
        protected static Stopwatch watch;

        private Because of = () =>
                                 {
                                     result = command.GetDocuments<TestDoc>(new object[] {"1","2"});
                                     watch = Stopwatch.StartNew();

                                     for( int i = 0; i < 10000; i++ )
                                     {
                                        viewResult = command.DeserializeView<TestDoc>( result.Json );    
                                     }

                                     watch.Stop();
                                 };

        private It should_not_take_longer_than_1_second = () => watch.ElapsedMilliseconds.ShouldBeLessThan( 3000 );
    }
}
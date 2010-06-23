using System.Linq;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;

namespace Relax.Tests.Commands.FromView
{
    public class when_getting_from_view : with_get_from_view_command
    {
        protected static ViewResult<TestDoc> expectedResult;
        protected static CommandResult result;
        protected static TestDoc document;

        private Because of = () =>
                                 {
                                     result = command.GetFromView<TestDoc>("test", "test",
                                                                           x => x.AllowStale().NoReduce());
                                     expectedResult = result.GetResultAs<ViewResult<TestDoc>>();
                                     document = expectedResult.Rows.First().Model;
                                 };

        private It should_only_have_one_record = () => expectedResult.Rows.Length.ShouldEqual(1);
        private It should_have_correct_id = () => document.DocumentId.ShouldEqual("test");
        private It should_have_correct_rev = () => document.DocumentRevision.ShouldEqual("1");
        private It should_have_correct_message = () => document.Message.ShouldEqual("Hi");
    }
}
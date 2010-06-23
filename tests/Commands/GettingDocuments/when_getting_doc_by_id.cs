using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Relax.Impl.Commands;
using It = Machine.Specifications.It;

namespace Relax.Tests.Commands
{
    public class when_getting_doc_by_id : with_get_doc_by_id_setup
    {
        protected static CommandResult result;
        protected static TestDoc model;
        protected static string json;

        private Because of = () =>
                                 {
                                     result = command.GetDocument<TestDoc>("1");
                                     model = result.GetResultAs<TestDoc>();
                                     json = result.Json.Replace("\r\n", "").Replace(" ", "");
                                 };

        private It should_produce_expected_json = () => json.ShouldEqual(response);

        private It should_create_valid_instance = () =>
                                                      {
                                                          model.DocumentId.ShouldEqual("1");
                                                          model.DocumentRevision.ShouldEqual("1");
                                                          model.Message.ShouldEqual("Test");
                                                      };
        private It should_call_action = () => mockAction.Verify();
    }
}

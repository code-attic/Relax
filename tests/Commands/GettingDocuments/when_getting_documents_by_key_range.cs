using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_documents_by_key_range : with_get_doc_by_range
    {
        protected static CommandResult result;
        protected static ViewResult<TestDoc> viewResult;
        protected static string json;

        private Because of = () =>
                                 {
                                     result = command.GetDocumentsInRange<TestDoc>("doc 1", "doc 2");
                                     viewResult = result.GetResultAs<ViewResult<TestDoc>>();
                                 };

        private It should_have_one_document = () => viewResult.TotalRows.ShouldEqual(1);
    }
}
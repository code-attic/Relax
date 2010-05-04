using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public class when_getting_all_docuemnts_with_paging : with_get_all_documents_paged_command
    {
        private static Exception exception = null;
        private static IList<TestDocument> records;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => records = repository.GetAll<TestDocument>(10, 1)
                                         );
                                 };

        private It should_get_documents_without_exception = () => exception.ShouldBeNull();
        private It should_get_one_record = () => records.Count.ShouldEqual(1);
        private It should_get_right_record = () =>
                                                 {
                                                     records[0].DocumentId.ShouldEqual(id);
                                                     records[0].Message.ShouldEqual("Hello");
                                                     records[0].DocumentRevision.ShouldEqual("2");
                                                 };
        private It should_call_get_correctly = () => commandMock.Verify(x => x.Get(couchUri));
    }
}
using System;
using System.Collections.Generic;
using Machine.Specifications;
using Relax.Tests.Commands;

namespace Relax.Tests.Repository
{
    public class when_getting_documents_by_range : with_get_documents_by_range
    {
        protected static IList<TestDoc> records;
        protected static Exception exception;

        private Because of = () =>
                                 {
                                     exception =
                                         Catch.Exception(
                                             () => records = repository.GetAllBetweenKeys<TestDoc>("1","2"));
                                 };

        private It should_get_documents_without_exception = () => exception.ShouldBeNull();
        private It should_get_one_record = () => records.Count.ShouldEqual(1);
        private It should_get_right_record = () =>
                                                 {
                                                     records[0].DocumentId.ShouldEqual("1");
                                                     records[0].Message.ShouldEqual("Hello");
                                                     records[0].DocumentRevision.ShouldEqual("2");
                                                 };
        private It should_call_get_correctly = () => commandMock.VerifyAll();
    }
}
using System;
using System.Collections.Generic;
using Machine.Specifications;
using Relax.Tests.Commands;

namespace Relax.Tests.Repository
{
    public class when_getting_from_view : with_from_view_command
    {
        protected static IList<TestDoc> result;
        private static Exception exception = null;

        private Because of = () =>
                                 {
                                     exception =
                                         Catch.Exception( () =>
                                             {
                                                 result = repository.FromView<TestDoc>( "test", "test", x => x.NoReduce() );
                                             } );
                                 };
        
        private It should_get_view_result_without_exception = () => exception.ShouldBeNull();
        private It should_call_get_action = () => commandMock.VerifyAll();
        private It should_get_one_record = () => result.Count.ShouldEqual(1);
    }
}
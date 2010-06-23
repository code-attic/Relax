using System;
using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public class when_saving_a_document : with_save_model_command
    {
        private static Exception exception = null;
        
        private Because of = () => {
            exception = Catch.Exception(() => 
                repository.Save(document)); 
        };

        private It should_save_document_without_exception = () => exception.ShouldBeNull();
        private It should_update_revision = () => document.DocumentRevision.ShouldEqual("3");
        private It should_call_put_correctly = () => commandMock.Verify(x => x.Put(couchUri, json));
    }
}
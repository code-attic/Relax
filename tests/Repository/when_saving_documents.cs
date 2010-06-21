using System;
using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public class when_saving_documents : with_save_models_command
    {
        private static Exception exception = null;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => repository.SaveAll(new [] {document})
                                         );
                                 };
        
        private It should_save_documents_without_exception = () => exception.ShouldBeNull();
        private It should_update_revision = () => document.DocumentRevision.ShouldEqual("3");
        private It should_call_post_correctly = () => commandMock.Verify(x => x.Post(couchUri, bulkSaveJson));
    }
}
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_poco_list_persistence : with_poco_list
    {
        protected static BulkPersist persist;
        protected static string json;
        protected static ISaveDocuments command;

        private Establish context = () =>
            {
                persist = new BulkPersist(true, false, testDocs);
                command = factory.CreateSaveDocumentsCommand();
            };
    }
}
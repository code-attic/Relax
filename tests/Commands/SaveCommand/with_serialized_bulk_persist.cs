using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_serialized_bulk_persist : with_single_parent_document
    {
        protected static BulkPersist persist;
        protected static string json;
        protected static ISaveDocuments command;

        private Establish context = () =>
                                        {
                                            persist = new BulkPersist(true, false, new [] {testDoc});
                                            json = persist.ToJson();
                                            command = factory.CreateSaveDocumentsCommand();
                                        };
    }
}

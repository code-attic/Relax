using System.Collections.Generic;
using Relax.Config;
using Relax.Impl.Http;
using System.Linq;

namespace Relax.Impl.Commands
{
    public class SaveDocumentGraphListCommand : 
        BaseSaveDocumentCollection,
        ISaveDocuments
    {
        public virtual CommandResult SaveAll<TModel>(IEnumerable<TModel> models)
        {
            var databaseName = configuration.GetDatabaseNameForType<TModel>();
            return SaveAll(databaseName, models.Cast<object>());
        }

        public virtual CommandResult SaveAll(string database, IEnumerable<object> models)
        {
            CreateUri(database)
                .BulkInsert();

            var documents = models.GetDocmentsFromGraph();
            return SaveEnumerable(documents);
        }

        public SaveDocumentGraphListCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
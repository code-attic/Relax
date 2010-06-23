using System.Collections.Generic;
using System.Linq;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class SaveDocumentListCommand : 
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

            return SaveEnumerable(models);
        }

        public SaveDocumentListCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
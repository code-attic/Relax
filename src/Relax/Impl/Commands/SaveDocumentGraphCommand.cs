using System;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class SaveDocumentGraphCommand : 
        BaseSaveDocumentCollection, 
        ISaveDocument
    {
        public virtual CommandResult Save<TModel>(TModel model)
        {
            var databaseName = configuration.GetDatabaseNameForType<TModel>();
            return Save(databaseName, model);
        }

        public virtual CommandResult Save(string databaseName, object model)
        {
            try
            {
                CreateUri(databaseName)
                    .Id(model.GetDocumentId());

                var documents = model.GetDocmentsFromGraph();
                var result = Save(documents);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}",
                    model.GetType().FullName,
                    Uri.ToString(),
                    ex
                    );
            }
        }

        public SaveDocumentGraphCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
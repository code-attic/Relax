using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class SaveDocumentCommand : BaseCouchCommand
    {
        public CommandResult<SaveResponse> Save<TModel>(TModel model)
        {
            try
            {
                CreateUri<TModel>()
                    .Id(model.GetDocumentId());

                var body = model.ToJson(configuration.IncludeTypeSpecification);
                var result = Put<SaveResponse>(body);
                model.SetDocumentRevision(result.Json);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}",
                    typeof(TModel).FullName,
                    Uri.ToString()
                    );
            }
        }

        public CommandResult<SaveResponse[]> SaveAll<TModel>(IEnumerable<TModel> models)
        {
            CreateUri<TModel>()
                .BulkInsert();

            return SaveAll(models.Cast<object>());
        }

        public CommandResult<SaveResponse[]> SaveAll(string database, IEnumerable<object> models)
        {
            CreateUri(database)
                .BulkInsert();

            return SaveAll(models);
        }

        protected CommandResult<SaveResponse[]> SaveAll(IEnumerable<object> models)
        {
            try
            {
                var list = new BulkPersist(true, false, models);
                var body = list.ToJson(configuration.IncludeTypeSpecification);
                body = ScrubBulkPersistOfTypeTokens(body);

                return Post<SaveResponse[]>(body);
            }
            catch (Exception ex)
            {
                throw Exception(
                        ex,
                        "An exception occurred trying to save a collection documents at {1}. \r\n\t {2}",
                        Uri.ToString()
                    );
            }
        }

        public virtual string ScrubBulkPersistOfTypeTokens(string body)
        {
            var jBlob = JObject.Parse(body);

            var hasTypes = jBlob.Children().OfType<JProperty>().FirstOrDefault(x => x.Name == "$type") != null;
            if (hasTypes)
            {
                var allOrNothing = jBlob["all_or_nothing"];
                var nonAtomic = jBlob["non_atomic"];
                var docs = jBlob["docs"]["$values"];

                var newBlob = new JObject(
                    new JProperty("all_or_nothing", allOrNothing),
                    new JProperty("non_atomic", nonAtomic),
                    new JProperty("docs", docs)
                    );

                body = newBlob.ToString();
            }
            return body;
        }

        public SaveDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
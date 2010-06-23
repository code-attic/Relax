using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public abstract class BaseSaveDocumentCollection : BaseCouchCommand
    {
        protected CommandResult SaveEnumerable(IEnumerable<object> models)
        {
            try
            {
                var list = new BulkPersist(true, false, models);
                var body = list.ToString();

                var result = Post(body);
                var updates = result.GetResultAs<SaveResponse[]>().ToDictionary(x => x.Id, x => x.Revision);
                models
                    .ForEach(x =>
                                 {
                                     var documentId = UtilityExtensions.GetDocumentIdAsJson(x);
                                     string newRev = null;
                                     if (updates.TryGetValue(documentId, out newRev))
                                     {
                                         UtilityExtensions.SetDocumentRevision(x, newRev);
                                     }
                                 });
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to save a collection documents at {0}. \r\n\t {1}",
                    Uri.ToString(),
                    ex
                    );
            }
        }

        public virtual string ScrubBulkPersistOfTypeTokens(string body)
        {
            var jBlob = JObject.Parse(body);
            var docs = jBlob["docs"]["$values"];
            jBlob.Property("docs").Value = docs;
            return jBlob.ToString();
        }

        protected BaseSaveDocumentCollection(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
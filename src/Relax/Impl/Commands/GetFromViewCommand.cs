using System;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;

namespace Relax.Impl.Commands
{
    public class GetFromViewCommand : BaseCouchCommand
    {
        public CommandResult GetFromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query)
        {
            try
            {
                CreateUri<TModel>()
                    .Design(designDocument)
                    .View(viewName);

                var viewQuery = new ViewQuery(Uri);
                query(viewQuery);
                return Get();
            }
            catch (Exception ex)
            {
                throw Exception(
                        ex,
                        "An exception occurred trying to retrieve a documents of type {0} from design document {1} and view {2} at {3}. \r\n\t {4}",
                        typeof(TModel).FullName,
                        designDocument,
                        viewName,
                        Uri.ToString(),
                        ex
                    );
            }
        }

        public GetFromViewCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
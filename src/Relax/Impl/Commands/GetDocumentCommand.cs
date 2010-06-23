using System;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class GetDocumentCommand : BaseCouchCommand
    {
        public CommandResult GetDocument<TModel>(object id)
        {
            try
            {
                CreateUri<TModel>()
                    .Id(id);
                return Get();
            }
            catch (Exception ex)
            {
                var couchEx = Exception(ex,
                        "An exception occurred trying to retrieve a document of type {0} with id {1} at {2}. \r\n\t {3}",
                        typeof(TModel).FullName,
                        id,
                        Uri.ToString(),
                        ex
                    );

                if (configuration.Throw404Exceptions)
                    throw couchEx;

                return new CommandResult("");
            }
        }

        public CommandResult GetDocument<TModel>(object id, object rev)
        {
            try
            {
                CreateUri<TModel>()
                    .IdAndRev(id, rev);
                return Get();
            }
            catch (Exception ex)
            {
                var couchEx = Exception(ex,
                        "An exception occurred trying to retrieve a document of type {0} with id {1} and rev {2} at {3}. \r\n\t {4}",
                        typeof(TModel).FullName,
                        id,
                        rev,
                        Uri.ToString(),
                        ex
                    );

                if (configuration.Throw404Exceptions)
                    throw couchEx;

                return new CommandResult("");
            }
        }

        public GetDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
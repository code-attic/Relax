using System;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class GetDocumentsByIdsCommand : BaseCouchCommand
    {
        public CommandResult GetDocuments<TModel>(object[] ids)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                var keys = new KeyList() { keys = ids };
                var jsonKeyList = keys.ToJson(false);

                var commandResult = Post(jsonKeyList);
                commandResult.ApplyDesignDocumentFilter();
                return commandResult;
            }
            catch (Exception ex)
            {
                var couchEx = Exception(ex,
                                        "An exception occurred trying to retrieve a list of documents of type {0} by keys at {1}. \r\n\t {2}",
                                        typeof(TModel).FullName,
                                        Uri.ToString(),
                                        ex
                    );

                if (configuration.Throw404Exceptions)
                    throw couchEx;

                return new CommandResult("");
            }
        }

        public GetDocumentsByIdsCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
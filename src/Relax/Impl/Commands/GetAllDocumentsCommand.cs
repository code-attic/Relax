using System;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class GetAllDocumentsCommand : BaseCouchCommand
    {
        public CommandResult GetDocuments<TModel>()
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                var commandResult = Get();
                commandResult.ApplyDesignDocumentFilter();
                return commandResult;
            }
            catch (Exception ex)
            {
                var couchEx = Exception(
                    ex,
                    "An exception occurred trying to retrieve all documents of type {0} at {1}. \r\n\t {2}",
                    typeof(TModel).FullName,
                    Uri.ToString(),
                    ex
                    );

                if (configuration.Throw404Exceptions)
                    throw couchEx;

                return new CommandResult("");
            }
        }

        public GetAllDocumentsCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
using System;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class GetDocumentsPagedCommand : BaseCouchCommand
    {
        public CommandResult GetDocumentsPaged<TModel>(int pageSize, int pageNumber)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments()
                    .Skip((pageNumber - 1) * pageSize)
                    .Limit(pageSize);

                var commandResult = Get();
                commandResult.ApplyDesignDocumentFilter();
                return commandResult;
            }
            catch (Exception ex)
            {
                var couchEx = Exception(ex,
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

        public GetDocumentsPagedCommand(IHttpAction action, ICouchConfiguration configuration)
            : base(action, configuration)
        {
        }
    }
}
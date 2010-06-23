using System;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class GetDocumentsInRangeCommand : BaseCouchCommand
    {
        public CommandResult GetDocumentsInRange<TModel>(object startingWith, object endingWith)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments()
                    .StartKey(startingWith)
                    .EndKey(endingWith);

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

        public GetDocumentsInRangeCommand(IHttpAction action, ICouchConfiguration configuration)
            : base(action, configuration)
        {
        }
    }
}
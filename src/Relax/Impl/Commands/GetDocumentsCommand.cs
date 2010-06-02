using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class GetDocumentsCommand : BaseCouchCommand
    {
        public CommandResult<ViewResult<TModel>> GetDocuments<TModel>()
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                return Get<ViewResult<TModel>>();
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

                return new CommandResult<ViewResult<TModel>>("");
            }
        }

        public CommandResult<ViewResult<TModel>> GetDocuments<TModel>(int pageSize, int pageNumber)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments()
                    .Skip((pageNumber - 1)*pageSize)
                    .Limit(pageSize);

                return Get<ViewResult<TModel>>();
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

                return new CommandResult<ViewResult<TModel>>("");
            }
        }

        public CommandResult<ViewResult<TModel>> GetDocuments<TModel>(object[] ids)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                var keys = new KeyList() { keys = ids };
                var jsonKeyList = keys.ToJson(false);
                return Post<ViewResult<TModel>>(jsonKeyList);
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

                return new CommandResult<ViewResult<TModel>>("");
            }
        }

        public GetDocumentsCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
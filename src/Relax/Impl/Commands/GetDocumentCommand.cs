using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

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

        public CommandResult GetDocuments<TModel>()
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                return Get();
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

        public CommandResult GetDocuments<TModel>(int pageSize, int pageNumber)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments()
                    .Skip((pageNumber - 1) * pageSize)
                    .Limit(pageSize);

                return Get();
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

        public CommandResult GetDocuments<TModel>(object[] ids)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                var keys = new KeyList() { keys = ids };
                var jsonKeyList = keys.ToJson(false);
                return Post(jsonKeyList);
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
        
        public CommandResult GetDocuments<TModel>(object startingWith, object endingWith)
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments()
                    .StartKey(startingWith)
                    .EndKey(endingWith);

                return Get();
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

        public GetDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
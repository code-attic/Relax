using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class ServerCommand : BaseCouchCommand
    {
        public void CleanViews<TModel>()
        {
            CreateUri<TModel>()
                .CleanupViews();
            Post();
        }

        public void CompactDatabase<TModel>()
        {
            CreateUri<TModel>()
                .Compact();
            Post();
        }

        public void CompactView<TModel>(string view)
        {
            CreateUri<TModel>()
                .CompactView(view);
            Post();
        }

        public void CopyDatabase<TModel>(CouchUri targetUri)
        {
            try
            {
                CreateUri().Replicate();
                var sourceUri = configuration.NewUri<TModel>();
                var request = ReplicationCommand.Once(sourceUri, targetUri);
                var body = request.ToJson(false);
                Post(body);
            }
            catch (WebException ex)
            {
                //do nothing, it's a timeout
                if (!ex.Message.Contains("timed out"))
                    throw;
            }
        }

        public void CopyDatabase(CouchUri sourceUri, CouchUri targetUri)
        {
            try
            {
                CreateUri().Replicate();
                var request = ReplicationCommand.Once(sourceUri, targetUri);
                var body = request.ToJson(false);
                Post(body);
            }
            catch (WebException ex)
            {
                //do nothing, it's a timeout
                if (!ex.Message.Contains("timed out"))
                    throw;
            }
        }

        public void CreateDatabase<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            CreateDatabase(database);
        }

        public void CreateDatabase(string database)
        {
            Uri = new CouchUri(configuration.Protocol, configuration.Server, configuration.Port, database);
            Put();
        }

        public bool DatabaseExists<TModel>()
        {
            return DatabaseExists(configuration.GetDatabaseNameForType<TModel>());
        }

        public bool DatabaseExists(string database)
        {
            try
            {
                Uri = new CouchUri(configuration.Protocol, configuration.Server, configuration.Port, database);
                var response = action.Get(Uri);
                return !string.IsNullOrEmpty(response) && !response.StartsWith("{\"error\"");
            }
            catch (Exception ex)
            {
                throw Exception(
                    ex,
                    "An exception occurred checking for the existence of database {0} at {1}. \r\n\t {2}",
                    database,
                    Uri,
                    ex
                    );
            }
        }

        public IList<string> GetDatabaseList()
        {
            CreateUri("_all_dbs");
            var result = Get();
            return result.GetResultAs<string[]>().ToList();
        }

        public void DeleteDatabase<TModel>()
        {
            CreateUri<TModel>();
            Delete();
        }

        public void Replicate<TModel>(CouchUri targetUri)
        {
            CreateUri().Replicate();
            var sourceUri = configuration.NewUri<TModel>();
            var request = ReplicationCommand.Continuous(sourceUri, targetUri);
            var body = request.ToJson(false);
            Post(body);
        }

        public void Replicate(CouchUri sourceUri, CouchUri targetUri)
        {
            CreateUri().Replicate();
            var request = ReplicationCommand.Continuous(sourceUri, targetUri);
            var body = request.ToJson(false);
            Post(body);
        }

        public ServerCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}

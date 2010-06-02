using System;
using System.Collections.Concurrent;
using System.Net;
using Relax.Impl.Commands;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Model;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public abstract class BaseCouchDbController
    {
        protected ICouchConfiguration _configuration;
        protected CouchCommandFactory _commandFactory;
        protected ConcurrentDictionary<string, bool> _databaseExists = new ConcurrentDictionary<string, bool>();

        protected virtual void EnsureDatabaseExists<TModel>()
        {
            var dbCreated = false;
            var shouldCheckCouch = false;
            ServerCommand command = null;
            var database = _configuration.GetDatabaseNameForType<TModel>();
            try
            {
                shouldCheckCouch = !_databaseExists.TryGetValue(database, out dbCreated);
                if (shouldCheckCouch && !dbCreated)
                {
                    command = _commandFactory.GetServerCommand();
                    command.CreateDatabase<TModel>();
                    _databaseExists[database] = true;
                }
            }
            catch (WebException webEx)
            {
                if (webEx.Message.Contains("(412) Precondition Failed"))
                {
                    _databaseExists[database] = true;
                }
                else
                {
                    "An exception occurred while trying to check for the existence of database {0} at uri {1}. \r\n\t {2}"
                        .ToError<IDocumentRepository>(database, command.Uri, webEx);
                    throw;
                }

            }
            catch (Exception ex)
            {
                "An exception occurred while trying to check for the existence of database {0} at uri {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(database, command.Uri, ex);
                throw;
            }
        }

        public virtual void CreateDatabase<TModel>()
        {
                var database = _configuration.GetDatabaseNameForType<TModel>();
                var command = _commandFactory.GetServerCommand();
                command.CreateDatabase<TModel>();
                _databaseExists[database] = true;
        }

        public virtual bool DatabaseExists<TModel>()
        {
            var command = _commandFactory.GetServerCommand();
            var exists = command.DatabaseExists<TModel>();
            _databaseExists[command.Uri.DatabaseName] = exists;
            return exists;
        }
    }
}
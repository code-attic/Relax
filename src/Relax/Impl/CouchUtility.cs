using System;
using System.Collections.Concurrent;
using System.Net;
using Relax.Config;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;
using Relax.Impl;

namespace Relax.Impl
{
    public class CouchUtility
    {
        protected ICouchConfiguration configuration { get; set; }
        protected CouchCommandFactory commandFactory { get; set; }
        protected ConcurrentDictionary<string, bool> _databaseExists = new ConcurrentDictionary<string, bool>();

        public virtual void EnsureDatabaseExists<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            EnsureDatabaseExists(database);
        }

        public virtual void CreateDatabase<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            CreateDatabase(database);
        }

        public virtual bool DatabaseExists<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            return DatabaseExists(database);
        }

        public virtual void EnsureDatabaseExists(string database)
        {
            if (string.IsNullOrEmpty(database))
                return;

            var dbCreated = false;
            var shouldCheckCouch = false;
            ServerCommand command = null;
            try
            {
                shouldCheckCouch = !_databaseExists.TryGetValue(database, out dbCreated);
                if (shouldCheckCouch && !dbCreated)
                {
                    command = commandFactory.CreateServerCommand();
                    command.CreateDatabase(database);
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

        public virtual void CreateDatabase(string database)
        {
            var command = commandFactory.CreateServerCommand();
            command.CreateDatabase(database);
            _databaseExists[database] = true;
        }

        public virtual bool DatabaseExists(string database)
        {
            var command = commandFactory.CreateServerCommand();
            var exists = command.DatabaseExists(database);
            _databaseExists[command.Uri.DatabaseName] = exists;
            return exists;
        }

        public virtual CouchUri NewUri()
        {
            var uri = configuration.Preauthorize
                              ? CouchUri.Build(
                                  configuration.User,
                                  configuration.Password,
                                  configuration.Protocol,
                                  configuration.Server,
                                  configuration.Port)
                              : CouchUri.Build(
                                  configuration.Protocol,
                                  configuration.Server,
                                  configuration.Port);
            return uri;
        }
        
        public virtual CouchUri NewUri<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            return NewUri(database);
        }
        
        public virtual CouchUri NewUri(string database)
        {
            var uri = configuration.Preauthorize ?
                                CouchUri.Build(
                                    configuration.User,
                                    configuration.Password,
                                    configuration.Protocol,
                                    configuration.Server,
                                    configuration.Port,
                                    database)
                              : CouchUri.Build(
                                  configuration.Protocol,
                                  configuration.Server,
                                  configuration.Port,
                                  database);
            EnsureDatabaseExists(database);
            return uri;
        }

        public virtual string GetDatabaseForType<TModel>()
        {
            return configuration.GetDatabaseNameForType<TModel>();
        }

        public CouchUtility(ICouchConfiguration couchConfiguration)
        {
            configuration = couchConfiguration;
            commandFactory = new CouchCommandFactory(couchConfiguration);
        }
    }
}
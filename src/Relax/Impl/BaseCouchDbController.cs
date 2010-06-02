using System;
using System.Collections.Concurrent;
using System.Net;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public abstract class BaseCouchDbController
    {
        protected ICouchConfiguration _configuration;
        protected CouchCommandFactory _commandFactory;
        protected ConcurrentDictionary<string, bool> _databaseExists = new ConcurrentDictionary<string, bool>();

        protected virtual CouchUri BaseURI<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var database = _configuration.GetDatabaseNameForType<TModel>();
            var baseURI = _configuration.Preauthorize ?
                                                          CouchUri.Build(
                                                              _configuration.User,
                                                              _configuration.Password,
                                                              _configuration.Protocol,
                                                              _configuration.Server,
                                                              _configuration.Port)
                              : CouchUri.Build(
                                  _configuration.Protocol,
                                  _configuration.Server,
                                  _configuration.Port,
                                  database);
            EnsureDatabaseExists<TModel>(database, baseURI);
            return baseURI;
        }

        protected virtual CouchUri BaseURI()
        {
            var baseURI = _configuration.Preauthorize ?
                                                          CouchUri.Build(
                                                              _configuration.User,
                                                              _configuration.Password,
                                                              _configuration.Protocol,
                                                              _configuration.Server,
                                                              _configuration.Port)
                              : CouchUri.Build(
                                  _configuration.Protocol,
                                  _configuration.Server,
                                  _configuration.Port);
            return baseURI;
        }

        protected virtual void EnsureDatabaseExists<TModel>(string database, CouchUri baseURI)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var dbCreated = false;
            var shouldCheckCouch = false;
            try
            {
                var command = _commandFactory.GetCommand();
                shouldCheckCouch = !_databaseExists.TryGetValue(database, out dbCreated);
                if (shouldCheckCouch && !dbCreated)
                {
                    command.Put(baseURI);
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
                        .ToError<IDocumentRepository>(database, baseURI.ToString(), webEx);
                    throw;
                }

            }
            catch (Exception ex)
            {
                "An exception occurred while trying to check for the existence of database {0} at uri {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(database, baseURI.ToString(), ex);
                throw;
            }
        }

        public virtual void CreateDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            string database = "";
            var uri = BaseURI<TModel>();
            try
            {
                database = _configuration.GetDatabaseNameForType<TModel>();
                var command = _commandFactory.GetCommand();
                command.Put(uri);
                _databaseExists[database] = true;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to create the database {0} at uri {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(database, uri.ToString(), ex);
                throw;
            }
        }

        public virtual bool DatabaseExists<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>();
            var database = "";
            var exists = false;
            try
            {
                database = _configuration.GetDatabaseNameForType<TModel>();
                var command = _commandFactory.GetCommand();
                var response = command.Get(uri);
                exists = !string.IsNullOrEmpty(response) && !response.StartsWith("{\"error\"");
                _databaseExists[database] = exists;
                return exists;
            }
            catch (Exception ex)
            {
                "An exception occurred checking for the existence of database {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(database, uri.ToString(), ex);
                throw;
            }
        }
    }
}
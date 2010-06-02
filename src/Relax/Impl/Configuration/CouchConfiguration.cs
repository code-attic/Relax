using System;
using System.Collections.Generic;
using Relax.Impl.Http;
using Relax.Impl.Serialization;

namespace Relax.Impl.Configuration
{
    public class CouchConfiguration : ICouchConfiguration
    {
        protected Dictionary<Type, string> _databaseForType = new Dictionary<Type, string>();

        public string GetDatabaseNameForType<T>()
        {
            var type = typeof (T);
            var dbname = "";
            _databaseForType.TryGetValue(type, out dbname);
            dbname = dbname ?? type.Name.ToLower();
            return dbname;
        }

        public void SetDatabaseNameForType<T>(string databaseName)
        {
            _databaseForType[typeof (T)] = databaseName.ToLower();
        }

        public string Protocol { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool Preauthorize { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int TimeOut { get; set; }
        public DocumentConventions Conventions { get; set; }
        public bool Cache { get; set; }
        public DateTime CacheExpiration { get; set; }
        public TimeSpan CacheLimit { get; set; }
        public bool Throw404Exceptions { get; set; }
        public bool IncludeTypeSpecification { get; set; }

        public virtual CouchUri NewUri()
        {
            var baseURI = Preauthorize
                              ? CouchUri.Build(
                                  User,
                                  Password,
                                  Protocol,
                                  Server,
                                  Port)
                              : CouchUri.Build(
                                  Protocol,
                                  Server,
                                  Port);
            return baseURI;
        }
        public virtual CouchUri NewUri<TModel>()
        {
            var database = GetDatabaseNameForType<TModel>();
            return NewUri(database);
        }
        public virtual CouchUri NewUri(string database)
        {
            return Preauthorize ?
                                CouchUri.Build(
                                    User,
                                    Password,
                                    Protocol,
                                    Server,
                                    Port,
                                    database)
                              : CouchUri.Build(
                                  Protocol,
                                  Server,
                                  Port,
                                  database);
        }

        public CouchConfiguration()
        {
            Protocol = "http";
            Server = "localhost";
            Port = 5984;
            Preauthorize = false;
            TimeOut = 6000;
            IncludeTypeSpecification = true;
            Conventions = new DocumentConventions();
        }
    }
}
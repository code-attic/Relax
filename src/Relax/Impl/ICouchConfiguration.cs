using System;

namespace Relax.Impl
{
    public interface ICouchConfiguration
    {
        int Port { get; set; }
        bool Preauthorize { get; set; }
        int TimeOut { get; set; }
        string Protocol { get; set; }
        string Server { get; set; }
        string User { get; set; }
        string Password { get; set; }
        bool Cache { get; set; }
        DateTime CacheExpiration { get; set; }
        TimeSpan CacheLimit { get; set; }
        string GetDatabaseNameForType<T>();
        bool Throw404Exceptions { get; set; }
        bool IncludeTypeSpecification { get; set; }
        void SetDatabaseNameForType<T>(string databaseName);
    }
}
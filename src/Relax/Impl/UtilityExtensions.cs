using Relax.Config;
using Relax.Impl.Http;
using StructureMap;

namespace Relax.Impl
{
    public static class UtilityExtensions
    {
        private static CouchUtility _utility;
        private static CouchUtility Utility
        {
            get
            {
                _utility = _utility ?? ObjectFactory.GetInstance<CouchUtility>();
                return _utility;
            }
        }

        public static string GetDocumentIdAsJson(this object instance)
        {
            return Utility.GetDocumentIdAsJson(instance);
        }

        public static object GetDocumentId(this object instance)
        {
            return Utility.GetDocumentId(instance);
        }

        public static string GetDocumentRevision(this object instance)
        {
            return Utility.GetDocumentRevision(instance);
        }
        
        public static void SetDocumentRevision(this object instance, string revision)
        {
            Utility.SetDocumentRevision(revision, instance);
        }

        public static bool DoesDatabaseExist(this CouchUri uri)
        {
            return Utility.DatabaseExists(uri.DatabaseName);
        }

        public static bool DoesDatabaseExist<TModel>()
        {
            return Utility.DatabaseExists<TModel>();
        }

        public static bool DoesDatabaseExist(string database)
        {
            return Utility.DatabaseExists(database);
        }

        public static void CreateDatabase(this CouchUri uri)
        {
            Utility.CreateDatabase(uri.DatabaseName);
        }

        public static void CreateDatabase<TModel>()
        {
            Utility.CreateDatabase<TModel>();
        }

        public static void CreateDatabase(string database)
        {
            Utility.CreateDatabase(database);
        }

        public static void EnsureDatabaseExists(this CouchUri uri)
        {
            Utility.EnsureDatabaseExists(uri.DatabaseName);
        }

        public static void EnsureDatabaseExists<TModel>()
        {
            Utility.EnsureDatabaseExists<TModel>();
        }

        public static void EnsureDatabaseExists(string database)
        {
            Utility.EnsureDatabaseExists(database);
        }

        public static object[] GetDocmentsFromGraph(this object instance)
        {
            return Utility.GetDocumentGraph(instance);
        }

        public static string GetDatabaseForType<TModel>()
        {
            return Utility.GetDatabaseForType<TModel>();
        }

        public static CouchUri NewUri(this ICouchConfiguration configuration)
        {
            return Utility.NewUri();
        }

        public static CouchUri NewUri<TModel>(this ICouchConfiguration configuration)
        {
            return Utility.NewUri<TModel>();
        }

        public static CouchUri NewUri(this ICouchConfiguration configuration, string database)
        {
            return Utility.NewUri(database);
        }
    }
}
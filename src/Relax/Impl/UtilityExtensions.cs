using Microsoft.Practices.ServiceLocation;
using Relax.Config;
using Relax.Impl.Http;

namespace Relax.Impl
{
    public static class UtilityExtensions
    {
        private static CouchUtility _couchUtility;
        private static DocumentUtility _documentUtility;
        private static CouchUtility CouchUtility
        {
            get
            {
                _couchUtility = _couchUtility ?? ServiceLocator.Current.GetInstance<CouchUtility>();
                return _couchUtility;
            }
        }
        private static DocumentUtility DocumentUtility
        {
            get
            {
                _documentUtility = _documentUtility ?? ServiceLocator.Current.GetInstance<DocumentUtility>();
                return _documentUtility;
            }
        }

        public static string GetDocumentIdAsJson(this object instance)
        {
            return DocumentUtility.GetDocumentIdAsJson(instance);
        }

        public static object GetDocumentId(this object instance)
        {
            return DocumentUtility.GetDocumentId(instance);
        }

        public static string GetDocumentRevision(this object instance)
        {
            return DocumentUtility.GetDocumentRevision(instance);
        }
        
        public static void SetDocumentRevision(this object instance, string revision)
        {
            DocumentUtility.SetDocumentRevision(revision, instance);
        }

        public static object[] GetDocmentsFromGraph(this object instance)
        {
            return DocumentUtility.GetDocumentGraph(instance);
        }

        public static CouchUri NewUri(this ICouchConfiguration configuration)
        {
            return CouchUtility.NewUri();
        }

        public static CouchUri NewUri<TModel>(this ICouchConfiguration configuration)
        {
            return CouchUtility.NewUri<TModel>();
        }

        public static CouchUri NewUri(this ICouchConfiguration configuration, string database)
        {
            return CouchUtility.NewUri(database);
        }
    }
}
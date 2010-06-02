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

        public static string GetDocumentId(this object instance)
        {
            return Utility.GetDocumentId(instance);
        }

        public static string GetDocumentRevision(this object instance)
        {
            return Utility.GetDocumentRevision(instance);
        }

        public static void SetDocumentId(this object instance, string json)
        {
            Utility.SetDocumentId(json, instance);
        }

        public static void SetDocumentRevision(this object instance, string json)
        {
            Utility.SetDocumentRevision(json, instance);
        }
    }
}
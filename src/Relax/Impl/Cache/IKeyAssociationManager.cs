using System.Collections.Generic;

namespace Relax.Impl.Cache
{
    public interface IKeyAssociationManager
    {
        void AddKeyAssociation(string key, string cacheKey);
        IEnumerable<string> GetAllKeys();
        IEnumerable<string> GetAssociations(string key);
    }
}
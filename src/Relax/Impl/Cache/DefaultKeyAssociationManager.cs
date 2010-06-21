using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Relax.Impl.Cache
{
    public class DefaultKeyAssociationManager : IKeyAssociationManager
    {
        protected ConcurrentDictionary<string, ConcurrentStack<string>> KeyAssociations { get; set; }
        
        public DefaultKeyAssociationManager()
        {
            KeyAssociations = new ConcurrentDictionary<string, ConcurrentStack<string>>();
        }

        public void AddKeyAssociation(string key, string cacheKey)
        {
            ConcurrentStack<string> associations = null;
            if (!KeyAssociations.TryGetValue(key, out associations))
            {
                associations = new ConcurrentStack<string>();
                KeyAssociations[key] = associations;
            }
            associations.Push(cacheKey);
        }

        public IEnumerable<string> GetAllKeys()
        {
            return KeyAssociations.Keys;
        }

        public IEnumerable<string> GetAssociations(string key)
        {
            ConcurrentStack<string> associations = null;
            if (KeyAssociations.TryGetValue(key, out associations))
            {
                string associatedKey = null;
                while (associations.TryPop(out associatedKey))
                {
                    yield return associatedKey;
                }
            }
            yield break;
        }
    }
}
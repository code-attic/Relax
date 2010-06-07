using System;
using Newtonsoft.Json.Serialization;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Serialization
{
    public class ChildDocumentResolverStrategy : IContractResolverStrategy
    {
        public bool ResolverAppliesForSerialization(Type type)
        {
            return true;
        }

        public bool ResolverAppliesForDeserialization(Type type)
        {
            return false;
        }

        public IContractResolver Resolver
        {
            get { return new ChildDocumentContractResolver(); }
        }
    }
}

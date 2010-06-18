using System;
using Newtonsoft.Json.Serialization;
using Relax.Impl.Json;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Serialization
{
    public class ConventionSerializationContractResolverStrategy : IContractResolverStrategy
    {
        public bool ResolverAppliesForSerialization(Type type)
        {
            return type.GetInterface("ICouchDocument`1") == null;
        }

        public bool ResolverAppliesForDeserialization(Type type)
        {
            return false;
        }

        public IContractResolver Resolver
        {
            get { return ObjectFactory.GetInstance<ConventionSerializationContractResolver>(); }
        }
    }
}
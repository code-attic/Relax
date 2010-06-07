using System;
using Newtonsoft.Json.Serialization;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Serialization
{
    public class ConventionDeserializerContractResolverStrategy : IContractResolverStrategy
    {
        public bool ResolverAppliesForSerialization(Type type)
        {
            return false;
        }

        public bool ResolverAppliesForDeserialization(Type type)
        {
            if (type.IsGenericType)
                if (type.GetGenericArguments()[0].GetInterface("ICouchDocument`2") == null)
                    return true;

            return type.GetInterface("ICouchDocument`2") == null;
        }

        public IContractResolver Resolver
        {
            get { return ObjectFactory.GetInstance<ConventionDeserializationContractResolver>(); }
        }
    }
}
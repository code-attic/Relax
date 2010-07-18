using System;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Serialization;
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
                if (type.GetGenericArguments()[0].GetInterface("ICouchDocument`1") == null)
                    return true;

            return type.GetInterface("ICouchDocument`1") == null;
        }

        public IContractResolver Resolver
        {
            get { return ServiceLocator.Current.GetInstance<ConventionDeserializationContractResolver>(); }
        }
    }
}
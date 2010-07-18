using System;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Serialization;
using Relax.Impl.Json;
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
            get { return ServiceLocator.Current.GetInstance<ConventionSerializationContractResolver>(); }
        }
    }
}
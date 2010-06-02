using System;
using Newtonsoft.Json.Serialization;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public class ConventionContractResolverStrategy : IContractResolverStrategy
    {
        public bool ResolverApplies(Type type)
        {
            return type.GetInterface("ICouchDocument`2") == null;
        }

        public IContractResolver Resolver
        {
            get { return ObjectFactory.GetInstance<ConventionContractResolver>(); }
        }
    }
}
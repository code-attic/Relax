using System;
using Newtonsoft.Json.Serialization;
using Relax.Impl.Configuration;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Serialization
{
    public class ChildDocumentResolverStrategy : IContractResolverStrategy
    {
        protected ICouchConfiguration configuration { get; set; }

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
            get { return new ChildDocumentContractResolver(configuration); }
        }

        public ChildDocumentResolverStrategy(ICouchConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}

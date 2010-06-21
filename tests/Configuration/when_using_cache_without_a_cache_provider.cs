using System;
using Machine.Specifications;
using Relax.Config;
using StructureMap;
using Symbiote.Core.Cache;

namespace Relax.Tests.Configuration
{
    public class when_using_cache_without_a_cache_provider : with_couch_configuration
    {
        private static Exception exception;

        private Because of = () =>
                                 {
                                     configurator
                                         .Https()
                                         .Port(1234)
                                         .Server("couchdb")
                                         .TimeOut(1000)
                                         .Cache(TimeSpan.FromMinutes(15));

                                     ObjectFactory.EjectAllInstancesOf<ICacheProvider>();

                                     exception = Catch.Exception(() => RelaxConfiguration.Configure(configuration));
                                 };

        private It should_produce_relax_configuration_exception = () => exception.ShouldBe(typeof(RelaxConfigurationException));
        private It should_have_clear_exception_message = () => exception.Message.ShouldEqual("You must have an implementation of ICacheProvider configured to use caching in Relax. Consider referencing Symbiote.Eidetic and adding the .Eidetic() call before this in your assimilation to utilize memcached or memcachedb as the cache provider for Relax.");
    }
}
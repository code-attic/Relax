using System;
using Machine.Specifications;
using Relax.Tests.Caching;
using Symbiote.Core;
using Symbiote.Core.Cache;

namespace Relax.Tests.Configuration
{
    public class when_using_cache_without_a_cache_provider : with_couch_cache_provider
    {
        private static Exception exception;

        private Because of = () =>
                                 {
                                     var assimilate = Assimilate.Initialize();
                                     Assimilate.Dependencies( x => x.For<ICacheProvider>().Use( null ) );
                                     exception = Catch.Exception(() => assimilate.Couch( x => x.Https().Port( 1234 ).Server( "couchdb" ).TimeOut( 1000 ).Cache() ));
                                     
                                     //Assimilate.Dependencies( x => x.For<ICacheProvider>().Use( cacheProviderMock.Object ) );
                                 };

        //private It should_not_have_cache_provider =
        //    () => Assimilate.Assimilation.DependencyAdapter.HasPluginFor<ICacheProvider>().ShouldBeFalse();
        //private It should_produce_Couch_configuration_exception = () => exception.ShouldBe(typeof(CouchConfigurationException));
        //private It should_have_clear_exception_message = () => exception.Message.ShouldEqual("You must have an implementation of ICacheProvider configured to use caching in Couch. Consider referencing Symbiote.Eidetic and adding the .Eidetic() call before this in your assimilation to utilize memcached or memcachedb as the cache provider for Couch.");
    }
}
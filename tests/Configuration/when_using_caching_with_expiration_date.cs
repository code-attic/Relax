using System;
using Machine.Specifications;

namespace Relax.Tests.Configuration
{
    [Subject("Couch Configuration")]
    public class when_using_caching_with_expiration_date : with_couch_configuration
    {
        private Because of = () => configurator
                                       .Https()
                                       .Port(1234)
                                       .Preauthorize("admin", "test")
                                       .Server("couchdb")
                                       .TimeOut(1000)
                                       .Cache(DateTime.Now.AddDays(1));

        private It should_use_http_as_protocol = () => configuration.Protocol.ShouldEqual("https");
        private It should_use_localhost_as_server = () => configuration.Server.ShouldEqual("couchdb");
        private It should_use_port_5984 = () => configuration.Port.ShouldEqual(1234);
        private It should_not_require_preauthorization = () => configuration.Preauthorize.ShouldBeTrue();
        private It should_have_username = () => configuration.User.ShouldEqual("admin");
        private It should_have_password = () => configuration.Password.ShouldEqual("test");
        private It should_have_six_second_timeout = () => configuration.TimeOut.ShouldEqual(1000);
        private It should_not_use_cache = () => configuration.Cache.ShouldBeTrue();
        private It should_not_have_cache_timeout = () => configuration.CacheLimit.ShouldEqual(TimeSpan.Zero);
        private It should_expire_tomorrow = () => configuration.CacheExpiration.ToShortDateString().ShouldEqual(DateTime.Now.AddDays(1).ToShortDateString());
    }
}
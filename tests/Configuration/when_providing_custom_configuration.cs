using Machine.Specifications;

namespace Relax.Tests.Configuration
{
    [Subject("Couch Configuration")]
    public class when_providing_custom_configuration : with_couch_configuration
    {
        private Because of = () => configurator
                                       .Https()
                                       .Port(1234)
                                       .Preauthorize("admin","test")
                                       .Server("couchdb")
                                       .TimeOut(1000)
                                       .UseForType<TestDocument>("TeStIngDoC");

        private It should_use_http_as_protocol = () => configuration.Protocol.ShouldEqual("https");
        private It should_use_localhost_as_server = () => configuration.Server.ShouldEqual("couchdb");
        private It should_use_port_5984 = () => configuration.Port.ShouldEqual(1234);
        private It should_require_preauthorization = () => configuration.Preauthorize.ShouldBeTrue();
        private It should_have_username = () => configuration.User.ShouldEqual("admin");
        private It should_have_password = () => configuration.Password.ShouldEqual("test");
        private It should_have_six_second_timeout = () => configuration.TimeOut.ShouldEqual(1000);
        private It should_not_use_cache = () => configuration.Cache.ShouldBeFalse();
        private It should_change_database_names_to_lower_case = 
            () => configuration
                .GetDatabaseNameForType<TestDocument>()
                .ShouldEqual("testingdoc");
    }
}
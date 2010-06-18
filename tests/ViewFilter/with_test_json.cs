using Machine.Specifications;

namespace Relax.Tests.ViewFilter
{
    public abstract class with_test_json
    {
        protected static string json;

        private Establish context = () =>
                                        {
                                            json = @"{""total_rows"":6,""offset"":0,""rows"":[
{""id"":""10c8208b-9575-4c13-be9f-3355b547c287"",""key"":""10c8208b-9575-4c13-be9f-3355b547c287"",""value"":{""rev"":""1-cead7a9977aa56695fc287c5050b676f""},""doc"":{""_id"":""10c8208b-9575-4c13-be9f-3355b547c287"",""_rev"":""1-cead7a9977aa56695fc287c5050b676f"",""Message"":""Test""}},
{""id"":""_design/requests"",""key"":""_design/requests"",""value"":{""rev"":""1-1ff1c1551a076eaed3ccebc516a5e77e""},""doc"":{""_id"":""_design/requests"",""_rev"":""1-1ff1c1551a076eaed3ccebc516a5e77e"",""language"":""javascript"",""views"":{""by_user"":{""map"":""function(doc) { emit(doc.Requestor, doc) }""}}}},
]}";
                                        };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Impl;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Tests.ViewFilter
{
    public class Request : CouchDocument
    {
        public virtual string BusinessJustification { get; set; }
        public virtual decimal CapitalCostEstimate { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual DateTime? DateModified { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual decimal? EstimatedDuration { get; set; }
        public virtual int? Rank { get; set; }
        public virtual string Requestor { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual string Title { get; set; }

        public Request()
        {
        }
    }

    public abstract class with_test_json
    {
        protected static string json;

        private Establish context = () =>
                                        {
                                            json = @"{""total_rows"":6,""offset"":0,""rows"":[
{""id"":""10c8208b-9575-4c13-be9f-3355b547c287"",""key"":""10c8208b-9575-4c13-be9f-3355b547c287"",""value"":{""rev"":""1-cead7a9977aa56695fc287c5050b676f""},""doc"":{""_id"":""10c8208b-9575-4c13-be9f-3355b547c287"",""_rev"":""1-cead7a9977aa56695fc287c5050b676f"",""BusinessJustification"":""Just Because"",""CapitalCostEstimate"":0.0,""CostType"":{""Id"":0,""Name"":""OneTime""},""DateCreated"":""/Date(1271455719700-0500)/"",""DateModified"":""/Date(1271455719700-0500)/"",""Description"":""Something Cool"",""DueDate"":""/Date(1272319719700-0500)/"",""DurationType"":{""Id"":0,""Name"":""Days""},""EndDate"":""/Date(1272319719701-0500)/"",""EstimatedDuration"":10.0,""Priority"":{""Id"":0,""Name"":""Low""},""Rank"":0,""Requestor"":""testperson"",""Resources"":[{""Id"":0,""Name"":""Computer 1"",""ResourceType"":{""Id"":0,""Name"":""Equipment""}}],""Status"":{""Id"":0,""Name"":""Unranked""},""Title"":""Test Project""}},
{""id"":""1a1b74c7-251a-4442-bd43-1f42c76cff35"",""key"":""1a1b74c7-251a-4442-bd43-1f42c76cff35"",""value"":{""rev"":""1-fc0a6b12cb090bdf6c1e5a63d790f7f4""},""doc"":{""_id"":""1a1b74c7-251a-4442-bd43-1f42c76cff35"",""_rev"":""1-fc0a6b12cb090bdf6c1e5a63d790f7f4"",""BusinessJustification"":""testing"",""CapitalCostEstimate"":234234.0,""Description"":""testing"",""Title"":""testing""}},
{""id"":""49a75675-e699-4f95-9e16-c711af17c955"",""key"":""49a75675-e699-4f95-9e16-c711af17c955"",""value"":{""rev"":""1-ccd8747a7c129f703f177f2d99e13932""},""doc"":{""_id"":""49a75675-e699-4f95-9e16-c711af17c955"",""_rev"":""1-ccd8747a7c129f703f177f2d99e13932"",""BusinessJustification"":""Just Because"",""CapitalCostEstimate"":0.0,""CostType"":{""Id"":0,""Name"":""OneTime""},""DateCreated"":""/Date(1271455717141-0500)/"",""DateModified"":""/Date(1271455717141-0500)/"",""Description"":""Something Cool"",""DueDate"":""/Date(1272319717141-0500)/"",""DurationType"":{""Id"":0,""Name"":""Days""},""EndDate"":""/Date(1272319717142-0500)/"",""EstimatedDuration"":10.0,""Priority"":{""Id"":0,""Name"":""Low""},""Rank"":0,""Requestor"":""testperson"",""Resources"":[{""Id"":0,""Name"":""Computer 1"",""ResourceType"":{""Id"":0,""Name"":""Equipment""}}],""Status"":{""Id"":0,""Name"":""Unranked""},""Title"":""Test Project""}},
{""id"":""53721607-e705-4a70-91f8-cef7ecdbe40e"",""key"":""53721607-e705-4a70-91f8-cef7ecdbe40e"",""value"":{""rev"":""1-6d51fc1fa3ce0738cf3d89ef08ac4498""},""doc"":{""_id"":""53721607-e705-4a70-91f8-cef7ecdbe40e"",""_rev"":""1-6d51fc1fa3ce0738cf3d89ef08ac4498"",""BusinessJustification"":""asdf"",""CapitalCostEstimate"":23423.0,""Description"":""asdf"",""EndDate"":""/Date(1271221200000-0500)/"",""StartDate"":""/Date(1271653200000-0500)/"",""Title"":""adsf""}},
{""id"":""_design/requests"",""key"":""_design/requests"",""value"":{""rev"":""1-1ff1c1551a076eaed3ccebc516a5e77e""},""doc"":{""_id"":""_design/requests"",""_rev"":""1-1ff1c1551a076eaed3ccebc516a5e77e"",""language"":""javascript"",""views"":{""by_user"":{""map"":""function(doc) { emit(doc.Requestor, doc) }""}}}},
{""id"":""fe00f3b1-880c-4da6-a007-d70ec4d432c8"",""key"":""fe00f3b1-880c-4da6-a007-d70ec4d432c8"",""value"":{""rev"":""1-5d76288b350999f8bcd0f41c86ab0a3e""},""doc"":{""_id"":""fe00f3b1-880c-4da6-a007-d70ec4d432c8"",""_rev"":""1-5d76288b350999f8bcd0f41c86ab0a3e"",""BusinessJustification"":""Because this is working :D"",""CapitalCostEstimate"":0.0,""Description"":""Testing"",""DueDate"":""/Date(1271660400000-0700)/"",""EndDate"":""/Date(1271574000000-0700)/"",""EstimatedDuration"":2.0,""StartDate"":""/Date(1271487600000-0700)/"",""Title"":""Elijah Is Awesome""}}
]}";
                                        };
    }

    public class when_filtering_design_documents_from_results : with_test_json
    {
        protected static List<Request> requests;

        private Because of = () =>
                                 {
                                     var jsonDoc = JObject.Parse(json);
                                     jsonDoc["rows"]
                                         .Children()
                                         .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design"))
                                         .ForEach(x => x.Remove());
                                     json = jsonDoc.ToString();

                                     var view = json.FromJson<ViewResult<Request>>();
                                     requests = view.GetList().ToList();
                                 };

        private It should_have_only_5_results = () => requests.Count.ShouldEqual(5);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public class when_getting_cache_keys_with_default_cache_key_builder
    {
        protected static ICacheKeyBuilder keyBuilder;
        protected static int pageIndex = 1;
        protected static int pageSize = 10;
        protected static string id = "1";
        protected static string rev = "B";
        protected static object startKey = new object[] {"A", 1};
        protected static object endKey = new object[] {"C", 4};
        protected static object key = new { property="value"};

        protected static string expected_id_key = "Relax.Tests.TestDocument_1";
        protected static string expected_id_and_rev_key = "Relax.Tests.TestDocument_1_B";
        protected static string expected_list_key = "Relax.Tests.TestDocument_list";
        protected static string expected_paged_list_key = "Relax.Tests.TestDocument_1_10";
        protected static string expected_range_key = "Relax.Tests.TestDocument_[\"A\",1]_[\"C\",4]";
        protected static string expected_object_key = "Relax.Tests.TestDocument_{\"property\":\"value\"}";

        private Establish context = () => keyBuilder = new CacheKeyBuilder();

        private It should_produce_id_key = () => keyBuilder.GetKey<TestDocument>(id).ShouldEqual(expected_id_key);
        private It should_produce_id_and_rev_key = () => keyBuilder.GetKey<TestDocument>(id, rev).ShouldEqual(expected_id_and_rev_key);
        private It should_produce_list_key = () => keyBuilder.GetListKey<TestDocument>().ShouldEqual(expected_list_key);
        private It should_produce_paging_list_key = () => keyBuilder.GetListKey<TestDocument>(pageIndex, pageSize).ShouldEqual(expected_paged_list_key);
        private It should_produce_object_key = () => keyBuilder.GetKey<TestDocument>(key).ShouldEqual(expected_object_key);
        private It should_produce_range_key =
            () => keyBuilder.GetRangeKey<TestDocument>(startKey, endKey).ShouldEqual(expected_range_key);
    }
}

namespace Relax.Tests.Serialization
{
    public class with_complex_view_result
    {
        protected static string json =
            @"{
  ""total_rows"": 10,
  ""offset"": 0,
  ""rows"": [
    {
      ""id"": ""0b29aa9a-589e-467e-869b-864968aa9532"",
      ""key"": ""0b29aa9a-589e-467e-869b-864968aa9532"",
      ""value"": {
        ""rev"": ""1-39759d4fdfa5e162acec9551b741c82e""
      },
      ""doc"": {
        ""_id"": ""0b29aa9a-589e-467e-869b-864968aa9532"",
        ""_rev"": ""1-39759d4fdfa5e162acec9551b741c82e"",
        ""$id"": ""7"",
        ""$type"": ""Relax.Tests.Serialization.ComplexDocument, Relax.Tests"",
        ""Message"": ""Document 3"",
        ""Time"": ""2010-06-02T14:07:35.2683188-05:00"",
        ""$doc_type"": ""TestDocument"",
        ""$doc_relatedIds"": {
          ""$id"": ""8"",
          ""$type"": ""System.Collections.Generic.Dictionary`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib""
        }
      }
    },
    {
      ""id"": ""5039f0f3-e752-439e-98ba-4149c85592b5"",
      ""key"": ""5039f0f3-e752-439e-98ba-4149c85592b5"",
      ""value"": {
        ""rev"": ""1-b2d8cd2294f69edf8f48888d3e549e82""
      },
      ""doc"": {
        ""_id"": ""5039f0f3-e752-439e-98ba-4149c85592b5"",
        ""_rev"": ""1-b2d8cd2294f69edf8f48888d3e549e82"",
        ""$id"": ""15"",
        ""$type"": ""Relax.Tests.Serialization.ComplexDocument, Relax.Tests"",
        ""Message"": ""Document 7"",
        ""Time"": ""2010-06-02T14:07:35.2683188-05:00"",
        ""$doc_type"": ""TestDocument"",
        ""$doc_relatedIds"": {
          ""$id"": ""16"",
          ""$type"": ""System.Collections.Generic.Dictionary`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib""
        }
      }
    }]
}
";
    }
}
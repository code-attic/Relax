using Symbiote.Core;

namespace Relax.Tests.Commands
{
    public class TestDoc : CouchDocument
    {
        public virtual string Message { get; set; }
    }

    public class MyPocoKeyAccessor : IKeyAccessor<MyPoco>
    {
        public string GetId( MyPoco actor )
        {
            return actor.MyId;
        }

        public void SetId<TKey>( MyPoco actor, TKey key )
        {
            actor.MyId = key.ToString();
        }
    }


    public class MyPoco
    {
        public string MyId { get; set; }
        public string Message { get; set; }
    }
}
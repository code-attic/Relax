using System;
using System.Linq.Expressions;
using Symbiote.Restfully;

namespace Relax.Tests.Commands.Querying
{
    public class MockProxy : IRemoteProxy<IRelaxQueryService>
    {
        protected string[] result;
        protected string requiredQuery;

        public void Call(Expression<Action<IRelaxQueryService>> call)
        {
            
        }

        public R Call<R>(Expression<Func<IRelaxQueryService, R>> call)
        {
            var query = Expression.Lambda((call.Body as MethodCallExpression).Arguments[1]).Compile().DynamicInvoke().ToString();
            if(query.Equals(requiredQuery))
            {
                object r = result;
                return (R) r;
            }
            return default(R);
        }

        public MockProxy(TestDoc doc, string expectedQuery)
        {
            result = new string[] {doc.DocumentId};
            requiredQuery = expectedQuery;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Machine.Specifications;
using Moq;
using Relax.Impl;
using Relax.Impl.Commands;

namespace Relax.Tests.Commands.Querying
{
    public abstract class with_relax_query : with_command_factory
    {
        protected static string expectedQuery;
        protected static MockProxy proxy;
        protected static Expression<Func<TestDocument, bool>> queryPredicate;
        protected static TestDoc document;
        protected static RelaxQueryCommand command;

        private Establish context = () =>
                                        {
                                            document = new TestDoc() {DocumentId = "1", Message = "Hi, I'm a document"};
                                            SetPredicate(x => x.Message.StartsWith("Hi"));
                                            expectedQuery = ExpressionTreeProcessor.TranslateExpression(queryPredicate);
                                            proxy = new MockProxy(document, expectedQuery);
                                            command = new RelaxQueryCommand(proxy);
                                        };

        protected static void SetPredicate(Expression<Func<TestDocument, bool>> predicate)
        {
            queryPredicate = predicate;
        }
    }
}

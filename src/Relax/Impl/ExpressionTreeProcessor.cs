using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Symbiote.Core;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public class ExpressionTreeProcessor
    {
        #region "Don't look at this, it'll break your eyes"
        protected static Dictionary<ExpressionType, Action<Expression, DelimitedBuilder>> processors =
            new Dictionary<ExpressionType, Action<Expression, DelimitedBuilder>>()
                {
                    {ExpressionType.Add, ProcessAddition},                
                    {ExpressionType.AddAssign, DoNothing},                
                    {ExpressionType.AddAssignChecked, DoNothing},                
                    {ExpressionType.AddChecked, ProcessAddition},                
                    {ExpressionType.And, ProcessAnd},                
                    {ExpressionType.AndAlso, ProcessAnd},                
                    {ExpressionType.AndAssign, DoNothing},                
                    {ExpressionType.ArrayIndex, DoNothing},                
                    {ExpressionType.Assign, DoNothing},
                    {ExpressionType.Block, DoNothing},
                    {ExpressionType.Call, ProcessCall},
                    {ExpressionType.Coalesce, DoNothing},
                    {ExpressionType.Conditional, DoNothing},
                    {ExpressionType.Constant, ProcessConstant},
                    {ExpressionType.Convert, DoNothing},
                    {ExpressionType.ConvertChecked, DoNothing},
                    {ExpressionType.DebugInfo, DoNothing},                
                    {ExpressionType.Decrement, DoNothing},                
                    {ExpressionType.Default, DoNothing},                
                    {ExpressionType.Divide, ProcessDivision},                
                    {ExpressionType.DivideAssign, DoNothing},                
                    {ExpressionType.Dynamic, DoNothing},                
                    {ExpressionType.Equal, ProcessEquals},                
                    {ExpressionType.ExclusiveOr, DoNothing},                
                    {ExpressionType.ExclusiveOrAssign, DoNothing},                
                    {ExpressionType.Extension, DoNothing},                
                    {ExpressionType.Goto, DoNothing},                
                    {ExpressionType.GreaterThan, ProcessGreaterThan},                
                    {ExpressionType.GreaterThanOrEqual, ProcessGreaterThanEqualTo},                
                    {ExpressionType.Increment, DoNothing},                
                    {ExpressionType.Index, DoNothing},
                    {ExpressionType.Invoke, DoNothing},
                    {ExpressionType.IsFalse, DoNothing},
                    {ExpressionType.IsTrue, DoNothing},
                    {ExpressionType.Label, DoNothing},
                    {ExpressionType.Lambda, ProcessLambda},
                    {ExpressionType.LeftShift, DoNothing},
                    {ExpressionType.LeftShiftAssign, DoNothing},
                    {ExpressionType.LessThan, ProcessLessThan},
                    {ExpressionType.LessThanOrEqual, ProcessLessThanEqualTo},
                    {ExpressionType.ListInit, DoNothing},
                    {ExpressionType.Loop, DoNothing},
                    {ExpressionType.MemberAccess, ProcessMemberAccess},
                    {ExpressionType.MemberInit, DoNothing},
                    {ExpressionType.Modulo, DoNothing},
                    {ExpressionType.ModuloAssign, DoNothing},
                    {ExpressionType.Multiply, ProcessMultiplication},
                    {ExpressionType.MultiplyAssign, DoNothing},
                    {ExpressionType.MultiplyAssignChecked, DoNothing},
                    {ExpressionType.MultiplyChecked, ProcessMultiplication},
                    {ExpressionType.Negate, DoNothing},
                    {ExpressionType.NegateChecked, DoNothing},
                    {ExpressionType.New, DoNothing},
                    {ExpressionType.NewArrayBounds, DoNothing},
                    {ExpressionType.NewArrayInit, DoNothing},
                    {ExpressionType.Not, DoNothing},
                    {ExpressionType.NotEqual, DoNothing},
                    {ExpressionType.OnesComplement, DoNothing},
                    {ExpressionType.Or, ProcessOr},
                    {ExpressionType.OrAssign, DoNothing},
                    {ExpressionType.OrElse, ProcessOr},
                    {ExpressionType.Parameter, ProcessParameter},
                    {ExpressionType.PostDecrementAssign, DoNothing},
                    {ExpressionType.PostIncrementAssign, DoNothing},
                    {ExpressionType.Power, DoNothing},
                    {ExpressionType.PowerAssign, DoNothing},
                    {ExpressionType.PreDecrementAssign, DoNothing},
                    {ExpressionType.PreIncrementAssign, DoNothing},
                    {ExpressionType.Quote, DoNothing},
                    {ExpressionType.RightShift, DoNothing},
                    {ExpressionType.RightShiftAssign, DoNothing},
                    {ExpressionType.RuntimeVariables, DoNothing},
                    {ExpressionType.Subtract, ProcessSubtraction},
                    {ExpressionType.SubtractAssign, DoNothing},
                    {ExpressionType.SubtractAssignChecked, DoNothing},
                    {ExpressionType.SubtractChecked, ProcessSubtraction},
                    {ExpressionType.Switch, DoNothing},
                    {ExpressionType.Throw, DoNothing},
                    {ExpressionType.Try, DoNothing},
                    {ExpressionType.TypeAs, DoNothing},
                    {ExpressionType.TypeEqual, DoNothing},
                    {ExpressionType.TypeIs, DoNothing},
                    {ExpressionType.UnaryPlus, DoNothing},
                    {ExpressionType.Unbox, DoNothing},
                };

        #endregion

        private static void ProcessMemberAccess(Expression expr, DelimitedBuilder builder)
        {
            var symbols = new[] { "{", "TO ", ":", "[", @"\+", @"\-", @"\\", @"\*" };
            var memberExpr = expr as MemberExpression;
            var temp = builder.ToString();
            var last = new string(temp.Skip(temp.Length - 3).Take(3).ToArray());
            if (ConstantExpected(builder))
            {
                var memberValue = Expression<Action<object>>.Lambda(expr).Compile().DynamicInvoke();
                builder.Append(HandleFormatting(memberValue));
            }
            else
            {
                builder.Append(GetMemberName(memberExpr));
            }
        }

        private static bool ConstantExpected(DelimitedBuilder builder)
        {
            var symbols = new[] { "{", "TO ", ":", "[", @"\+", @"\-", @"\\", @"\*" };
            var temp = builder.ToString();
            var last = new string(temp.Skip(temp.Length - 3).Take(3).ToArray());
            return symbols.Any(x => last.EndsWith(x));
        }

        private static string GetMemberName(MemberExpression expr)
        {
            if (expr.Expression is MemberExpression)
            {
                return "{0}.{1}".AsFormat(
                    GetMemberName(expr.Expression as MemberExpression),
                    expr.Member.Name);
            }
            else if (expr.Expression is MethodCallExpression)
            {
                var methodCallExpression = expr.Expression as MethodCallExpression;
                return "{0}.{1}".AsFormat(
                    GetMemberName(methodCallExpression.Object as MemberExpression),
                    expr.Member.Name);
            }
            else
            {
                return expr.Member.Name;
            }
        }

        private static void ProcessLambda(Expression expr, DelimitedBuilder builder)
        {
            var lambda = expr as LambdaExpression;

            Process(lambda.Body, builder);
        }

        private static void ProcessParameter(Expression expr, DelimitedBuilder builder)
        {
            var parameterExpr = expr as ParameterExpression;

            if (parameterExpr.Name.Length > 1)
            {
                var paramValue = Expression<Action<object>>.Lambda(parameterExpr).Compile().DynamicInvoke();
                builder.Append(HandleFormatting(paramValue));
            }
        }

        public static void Process(Expression expr, DelimitedBuilder builder)
        {
            processors[expr.NodeType](expr, builder);
        }

        private static void ProcessAddition(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, @"\+");
        }

        private static void ProcessAnd(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, " AND ");
        }

        private static void ProcessOr(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, " OR ");
        }

        private static void ProcessGreaterThan(Expression expr, DelimitedBuilder builder)
        {
            var binaryExpr = expr as BinaryExpression;
            Process(binaryExpr.Left, builder);
            builder.Append(":{");
            Process(binaryExpr.Right, builder);
            builder.Append(" TO *}");
        }

        private static void ProcessGreaterThanEqualTo(Expression expr, DelimitedBuilder builder)
        {
            var binaryExpr = expr as BinaryExpression;
            Process(binaryExpr.Left, builder);
            builder.Append(":[");
            Process(binaryExpr.Right, builder);
            builder.Append(" TO *]");
        }

        private static void ProcessLessThan(Expression expr, DelimitedBuilder builder)
        {
            var binaryExpr = expr as BinaryExpression;
            Process(binaryExpr.Left, builder);
            builder.Append(":{* TO ");
            Process(binaryExpr.Right, builder);
            builder.Append("}");
        }

        private static void ProcessLessThanEqualTo(Expression expr, DelimitedBuilder builder)
        {
            var binaryExpr = expr as BinaryExpression;
            Process(binaryExpr.Left, builder);
            builder.Append(":[* TO ");
            Process(binaryExpr.Right, builder);
            builder.Append("]");
        }

        private static void ProcessEquals(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, ":");
        }

        private static void ProcessDivision(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, @"\\");
        }

        private static void ProcessMultiplication(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, @"\*");
        }

        private static void ProcessSubtraction(Expression expr, DelimitedBuilder builder)
        {
            ProcessSimpleBinaryExpression(expr, builder, @"\-");
        }

        private static void ProcessCall(Expression expr, DelimitedBuilder builder)
        {
            var call = expr as MethodCallExpression;
            var name = call.Method.Name;

            if (ConstantExpected(builder))
            {
                var constValue = Expression<Action<object>>.Lambda(expr).Compile().DynamicInvoke();
                builder.Append(HandleFormatting(constValue));
            }
            else
            {
                switch (name)
                {
                    case "StartsWith":
                        Process(call.Object, builder);
                        builder.Append(":");
                        call.Arguments.ForEach(x => Process(x, builder));
                        builder.Append("*");
                        break;
                    case "EndsWith":
                        Process(call.Object, builder);
                        builder.Append(":");
                        builder.Append("*");
                        call.Arguments.ForEach(x => Process(x, builder));
                        break;
                    case "Contains":
                        Process(call.Object, builder);
                        builder.Append(":");
                        builder.Append("*");
                        call.Arguments.ForEach(x => Process(x, builder));
                        builder.Append("*");
                        break;
                    default:
                        call.Arguments.ForEach(x => Process(x, builder));
                        break;
                }
            }
        }

        private static void ProcessConstant(Expression expr, DelimitedBuilder builder)
        {
            var constantExpr = expr as ConstantExpression;
            builder.Append(HandleFormatting(constantExpr.Value));
        }

        private static void ProcessSimpleBinaryExpression(Expression expr, DelimitedBuilder builder, string token)
        {
            var binaryExpr = expr as BinaryExpression;
            Process(binaryExpr.Left, builder);
            builder.Append(token);
            Process(binaryExpr.Right, builder);
        }

        private static string HandleFormatting(object value)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToShortDateString();
            }
            else
            {
                return value.ToString();
            }
        }

        private static void DoNothing(Expression expr, DelimitedBuilder builder)
        {
            var nodeType = expr.NodeType;
            builder.AppendFormat("No handler for node type {0}", nodeType.ToString());
        }
    }
}

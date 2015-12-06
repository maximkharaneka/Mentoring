using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ExpressionTransformation
{
    [TestClass]
    public class VisitExpressionTest
    {
        public class TraceExpressionVisitor : ExpressionVisitor
        {
            public int indent = 0;

            public Dictionary<string, int> replacers;

            public Expression Visit(Expression node, Dictionary<string, int> dict)
            {
                if (node == null)
                    return base.Visit(node);

                replacers = dict;
                Expression result = base.Visit(node);

                return result;
            }
            public override Expression Visit(Expression node)
            {
                if (node == null)
                    return base.Visit(node);

                indent++;
                Expression result = base.Visit(node);
                Console.WriteLine("{0}{1} - {2}", new String(' ', indent * 4), result.NodeType, result.GetType());

                indent--;

                return result;
            }
            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node == null)
                    return base.VisitBinary(node);

                if (node.Right.NodeType == ExpressionType.Constant && node.NodeType == ExpressionType.Add && node.Right.ToString() == "1")
                {
                    Expression newNode = Expression.Increment(node.Left);
                    return base.Visit(newNode);
                }
                if (node.Right.NodeType == ExpressionType.Constant && node.NodeType == ExpressionType.Subtract && node.Right.ToString() == "1")
                {
                    Expression newNode = Expression.Decrement(node.Left);
                    return base.Visit(newNode);
                }
                if (node.Right.NodeType == ExpressionType.Parameter || node.Left.NodeType == ExpressionType.Parameter)
                {
                    var newLeft = replaceParameter(node.Left);
                    var newRight = replaceParameter(node.Right);
                    if (newLeft != node.Left || newRight != node.Right)
                    {
                        var newNode = Expression.MakeBinary(node.NodeType, newRight, newLeft);
                        return base.VisitBinary(newNode);
                    }                  
                }

                return base.VisitBinary(node);
            }
            public Expression replaceParameter(Expression parameter)
            {
                if (replacers != null)
                {
                    var par = parameter as ParameterExpression;
                    var val = replacers[par.Name];
                    if (val != 0)
                    {
                        return Expression.Constant(val);
                    }
                }
                return parameter;
            }
        }

        [TestMethod]
        public void Visit()
        {            
            Expression<Func<int, int>> exp1 = (a) => 3 * a - 1 + 1;
            Expression<Func<int, int>> changed = (Expression<Func<int, int>>) new TraceExpressionVisitor().Visit(exp1);

            Console.WriteLine("{0} : ", changed);
            Console.WriteLine("{0} : ", exp1);
            Console.WriteLine("{0} : ", changed.Compile().Invoke(3));
            Console.WriteLine("{0} : ", exp1.Compile().Invoke(3));    

            Expression<Func<int, int, int>> replace = (a, b) => b * a +5;
            Console.WriteLine("{0} : ", replace.Compile().Invoke(3,3));

            var dict = new Dictionary<string, int>();
            dict.Add("a", 5); dict.Add("b", 4);

            Expression<Func<int, int, int>> replaced = 
                (Expression<Func<int, int, int>>)new TraceExpressionVisitor().Visit(replace, dict);
            Console.WriteLine("{0} : ", replaced.Compile().Invoke(3, 3));
        }
    }
}








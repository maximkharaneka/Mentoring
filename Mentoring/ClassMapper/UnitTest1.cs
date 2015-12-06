using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassMapper
{
    [TestClass]
    public class UnitTest1
    {
        public class Mapper<TSource, TDestination>
        {
            Func<TSource, TDestination> mapFunction;
            internal Mapper(Func<TSource, TDestination> func)
            {
                mapFunction = func;
            }
            public TDestination Map(TSource source)
            {
                return mapFunction(source);
            }
        }
        public class MappingGenerator
        {
            public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
            {
                var sourceParam = Expression.Parameter(typeof(TSource));
                var destParam = Expression.Parameter(typeof(TDestination));
                var typeDest = typeof(TDestination);

                var newExpression = Expression.New(typeDest);
                var list = new List<MemberBinding>();
                var propertyInfos = typeDest.GetProperties();
                //var propertyInfos = typeDest.GetProperties(BindingFlags.Instance |
                //                    BindingFlags.Public |
                //                    BindingFlags.SetProperty);
                foreach (var propertyInfo in propertyInfos)
                {
                    Expression call;
                    //= Expression.Call(
                    //                       typeof(DictionaryExtension),
                    //                       "GetValue", new[] { propertyInfo.PropertyType },
                    //                       new Expression[]
                    //                         {
                    //                 dictParam,
                    //                 Expression.Constant(propertyInfo.Name)
                    //                         });
                    call = Expression.Field(
            Expression.Constant(destParam),
            propertyInfo.Name);
                    MemberBinding mb = Expression.Bind(propertyInfo.GetSetMethod(), call);
                    list.Add(mb);
                }

                //var ex = Expression.Lambda<Func<Dictionary<string, object>, T>>(
                //                                  Expression.MemberInit(newExpression, list),
                //                                  new[] { dictParam });
                var mapFunction =
                    Expression.Lambda<Func<TSource, TDestination>>(
                      Expression.MemberInit(newExpression, list),
                    sourceParam
                    );
                var compiled = mapFunction.Compile();
                return new Mapper<TSource, TDestination>(compiled);
            }
        }

        public class Foo
        {
            public int i;
            public string s;
        }
        public class Bar
        {
            public int i;
            public string s;
        }

        [TestMethod]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var source = new Foo();
            source.i = 3;
            source.s = "ss";
            var dest = mapper.Map(source);
            Assert.AreEqual(source.i,dest.i);
            Assert.AreEqual(source.s,dest.s);
        }
    }
}

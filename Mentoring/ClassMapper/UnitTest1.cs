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
            private Func<TSource, TDestination> mapFunction;

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
                var sourceParam = Expression.Parameter(typeof (TSource));

                var sourceProperties = typeof (TSource).GetFields();
                var destProperties = typeof (TDestination).GetFields();

                var newExpression = Expression.New(typeof (TDestination));

                var list = new List<MemberBinding>();

                foreach (var destProperty in destProperties)
                {
                    Expression fieldProperty = Expression.Field(sourceParam,
                        sourceProperties.FirstOrDefault(x => x.Name.Equals(destProperty.Name)));
                    MemberBinding mb = Expression.Bind(destProperty, fieldProperty);
                    list.Add(mb);
                }

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
            public int i; // { get; set; }
            public string s; //{ get; set; }
        }

        public class Bar
        {
            public int i; //{ get; set; }
            public string s; //{ get; set; }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var source = new Foo();
            source.i = 3;
            source.s = "abc";
            var dest = mapper.Map(source);
            Assert.AreEqual(source.i, dest.i);
            Assert.AreEqual(source.s, dest.s);
        }
    }
}
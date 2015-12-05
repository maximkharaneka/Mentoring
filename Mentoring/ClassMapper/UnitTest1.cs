using System;
using System.Linq.Expressions;
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
                var mapFunction =
                    Expression.Lambda<Func<TSource, TDestination>>(
                    Expression.New(typeof(TDestination)),
                    sourceParam
                    );

                return new Mapper<TSource, TDestination>(mapFunction.Compile());
            }
        }
        public class Foo { }
        public class Bar { }

        [TestMethod]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
        }
    }
}

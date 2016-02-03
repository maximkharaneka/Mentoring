using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        private Northwind dbContext;

        [TestMethod]
        public void IDataContractSurrogate()
        {
            var orders = dbContext.Orders.ToArray();
            var knownTypes = new List<Type>
            {
                typeof (Order)
            };

            IDataContractSurrogate surrogate = new OrderSurrogate();
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(
                new DataContractSerializer(typeof(IEnumerable<Order>), knownTypes, Int32.MaxValue, false, true,
                    surrogate),
                true);

            tester.SerializeAndDeserialize(orders);
        }

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ProxyCreationEnabled = false;
        }

        [TestMethod]
        public void ISerializable()
        {
            var products =
                dbContext.Products.Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .Include(x => x.Order_Details)
                    .ToList();

            var knownTypes = new List<Type>
            {
                typeof (HashSet<Order_Detail>),
                typeof (Supplier),
                typeof (Category)
            };

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Product>>(
                    new DataContractSerializer(typeof(IEnumerable<Product>)
                        , knownTypes, int.MaxValue, false, true, null), true);

            tester.SerializeAndDeserialize(products);
        }

        [TestMethod]
        public void ISerializationSurrogate()
        {
            var orderDetails = dbContext.Order_Details.Include(x => x.Product).ToArray();

            var selector = new SurrogateSelector();

            selector.AddSurrogate(
                typeof(Order_Detail),
                new StreamingContext(StreamingContextStates.Persistence, null),
                new SerializationSurrogate());

            var tester =
                new SoapFormatterTester<IEnumerable<Order_Detail>>(new SoapFormatter(selector, new StreamingContext()),
                    true);

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void SerializationEvents()
        {
            var categories = dbContext.Categories.Include(x => x.Products).ToList();

            var knownTypes = new List<Type>
            {
                typeof (HashSet<Order_Detail>)
            };

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Category>>(
                    new DataContractSerializer(typeof(IEnumerable<Category>)
                        , knownTypes, int.MaxValue, false, true, null), true);

            tester.SerializeAndDeserialize(categories);
        }
    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Task.DB
{
    public class OrderSurrogate : IDataContractSurrogate
    {
        #region Not implemented
        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }
        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }
        #endregion

        public Type GetDataContractType(Type type)
        {
            if (typeof(Order).IsAssignableFrom(type))
            {
                return typeof(OrderSurrogated);
            }
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order)
            {
                OrderSurrogated orderSurrogated = new OrderSurrogated
                {
                    CustomerID = ((Order)obj).CustomerID,
                    EmployeeID = ((Order)obj).EmployeeID,
                    Freight = ((Order)obj).Freight,
                    OrderDate = ((Order)obj).OrderDate,
                    OrderID = ((Order)obj).OrderID,
                    RequiredDate = ((Order)obj).RequiredDate,
                    ShipAddress = ((Order)obj).ShipAddress,
                    ShipCity = ((Order)obj).ShipCity,
                    ShipCountry = ((Order)obj).ShipCountry,
                    ShipName = ((Order)obj).ShipName,
                    ShipPostalCode = ((Order)obj).ShipPostalCode,
                    ShipRegion = ((Order)obj).ShipRegion,
                    ShipVia = ((Order)obj).ShipVia,
                    ShippedDate = ((Order)obj).ShippedDate
                };
                return orderSurrogated;
            }
            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj is OrderSurrogated)
            {
                Order order = new Order
                {
                    CustomerID = ((OrderSurrogated)obj).CustomerID,
                    EmployeeID = ((OrderSurrogated)obj).EmployeeID,
                    Freight = ((OrderSurrogated)obj).Freight,
                    OrderDate = ((OrderSurrogated)obj).OrderDate,
                    OrderID = ((OrderSurrogated)obj).OrderID,
                    RequiredDate = ((OrderSurrogated)obj).RequiredDate,
                    ShipAddress = ((OrderSurrogated)obj).ShipAddress,
                    ShipCity = ((OrderSurrogated)obj).ShipCity,
                    ShipCountry = ((OrderSurrogated)obj).ShipCountry,
                    ShipName = ((OrderSurrogated)obj).ShipName,
                    ShipPostalCode = ((OrderSurrogated)obj).ShipPostalCode,
                    ShipRegion = ((OrderSurrogated)obj).ShipRegion,
                    ShipVia = ((OrderSurrogated)obj).ShipVia,
                    ShippedDate = ((OrderSurrogated)obj).ShippedDate
                };
                return order;
            }
            return obj;
        }
    }

    [DataContract(Name = "Order")]
    class OrderSurrogated
    {
        [DataMember]
        public int OrderID { get; set; }

        [StringLength(5)]
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? RequiredDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }

        [DataMember]
        public int? ShipVia { get; set; }

        [DataMember]
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [DataMember]
        [StringLength(40)]
        public string ShipName { get; set; }

        [DataMember]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipRegion { get; set; }

        [DataMember]
        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipCountry { get; set; }

        [IgnoreDataMember]
        public virtual Customer Customer { get; set; }

        [IgnoreDataMember]
        public virtual Employee Employee { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        [IgnoreDataMember]
        public virtual Shipper Shipper { get; set; }
    }
}
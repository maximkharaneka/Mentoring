using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Details")]
    public partial class Order_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }


    public class SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info,
            StreamingContext context)
        {
            var f = (Order_Detail)obj;
            info.AddValue("OrderID", f.OrderID);
            info.AddValue("ProductID", f.ProductID);
            info.AddValue("UnitPrice", f.UnitPrice);
            info.AddValue("Quantity", f.Quantity);
            info.AddValue("Discount", f.Discount);
            info.AddValue("Order", f.Order);
            f.Product.Order_Details = null;
            info.AddValue("Product", f.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info,
            StreamingContext context, ISurrogateSelector selector)
        {
            var f = (Order_Detail)obj;
            f.OrderID = info.GetInt32("OrderID");
            f.ProductID = info.GetInt32("ProductID");
            f.UnitPrice = info.GetDecimal("UnitPrice");
            f.Quantity = info.GetInt16("Quantity");
            f.Discount = info.GetSingle("Discount");
            f.Order = (Order)info.GetValue("Order", typeof(Order));
            f.Product = (Product)info.GetValue("Product", typeof(Product));
            return f;
        }
    }
}

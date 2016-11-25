using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [DataContract]
    [KnownType(typeof(DiscountDTO[]))]
    public class ProductDTO : DataTransferObjectBase
    {
        [DataMember]
        public List<DiscountDTO> Discounts { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal Price { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Title { get; set; }
    }
}
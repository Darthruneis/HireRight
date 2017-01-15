using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class OrderDetailsDTO : DataTransferObjectBase
    {
        [DataMember]
        public NotesPositionsDTO NotesAndPositions { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        public OrderDetailsDTO()
        {
            NotesAndPositions = new NotesPositionsDTO();
        }
    }
}
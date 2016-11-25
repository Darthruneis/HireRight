using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class CompanyDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO BillingAddress { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
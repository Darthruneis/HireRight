using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class LocationDTO : DataTransferObjectBase
    {
        public LocationDTO(CompanyLocation item)
        {
        }
    }
}
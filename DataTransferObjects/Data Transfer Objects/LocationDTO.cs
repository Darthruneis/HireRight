using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;

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
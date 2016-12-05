using HireRight.EntityFramework.CodeFirst.Models;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingLocations
    {
        ICollection<CompanyLocation> Locations { get; set; }
    }
}
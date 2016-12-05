using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingAddress
    {
        Address Address { get; set; }
    }
}
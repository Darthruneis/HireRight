using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public class AddressViewModel
    {

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ZIP { get; set; }

        public string GetFullAddress()
        {
            return Address +", " + State + " " + ZIP;
        }
    }
}

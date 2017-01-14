using DataTransferObjects.Data_Transfer_Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HireRight.Models
{
    public class CustomSolutionsViewModel
    {
        public IList<JobAnalysisCategoryViewModel> Categories { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public ContactDTO Contact { get; set; }

        [Required]
        public string Notes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<string> Positions => PositionsToFill.Split(',').Select(x => x.Trim()).ToList();

        [Required]
        [Display(Name = "Positions to fill (separated by commas)")]
        public string PositionsToFill { get; set; }

        public CustomSolutionsViewModel()
        {
            Categories = new List<JobAnalysisCategoryViewModel>();
            Contact = new ContactDTO();
        }
    }
}
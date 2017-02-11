using System;
using DataTransferObjects.Data_Transfer_Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DataTransferObjects;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Models
{
    public class CustomSolutionsViewModel
    {
        public IList<JobAnalysisCategoryViewModel> Categories { get; set; }

        public IList<UndisplayedCategory> CategoriesFromOtherPages { get; set; }

        public CategoryFilter CategoryFilter { get; set; }

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
            CategoryFilter = new CategoryFilter(1, 10);
            CategoriesFromOtherPages = new List<UndisplayedCategory>();
        }
    }

    public class UndisplayedCategory
    {
        public Guid Id { get; set; }
        public string Importance { get; set; }
    }
}
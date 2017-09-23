using System;
using System.Collections;
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
        public IList<UndisplayedCategory> CategoriesFromOtherPages { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public ContactDTO Contact { get; set; }
        public CategoryFilterViewModel Filter { get; set; }

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
            Filter = new CategoryFilterViewModel();
            CategoriesFromOtherPages = new List<UndisplayedCategory>();
        }

        public CustomSolutionsViewModel(IEnumerable<JobAnalysisCategoryViewModel> categories, CategoryFilterViewModel filter) : this()
        {
            Categories = categories.ToList();
            Filter = filter;
        }

        public SubmitCardsDTO CreateSubmitCardsDTO()
        {
            SubmitCardsDTO dto = new SubmitCardsDTO();
            dto.Categories = Categories.Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance, IsInTopTwelve = x.IsInTopTwelve, Id = x.Id }).ToList();
            dto.CompanyName = CompanyName;
            dto.Positions = Positions;
            dto.Contact = Contact;
            dto.Notes = Notes;

            return dto;
        }
    }

    public class UndisplayedCategory
    {
        public Guid Id { get; set; }
        public string Importance { get; set; }
    }
}
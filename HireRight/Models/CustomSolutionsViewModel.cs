using System;
using System.Collections;
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

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public ContactDTO Contact { get; set; }

        [Required]
        public string Notes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<string> Positions => (PositionsToFill ?? "").Split(',').Select(x => x.Trim()).ToList();

        [Required]
        [Display(Name = "Positions to fill (separated by commas)")]
        public string PositionsToFill { get; set; }

        public List<IndustryDTO> Industries { get; set; }

        private CustomSolutionsViewModel()
        {
            Categories = new List<JobAnalysisCategoryViewModel>();
            Contact = new ContactDTO();
            Industries = new List<IndustryDTO>();
        }

        public CustomSolutionsViewModel(IEnumerable<JobAnalysisCategoryViewModel> categories, IEnumerable<IndustryDTO> industries) : this()
        {
            Categories = categories.ToList();
            Industries = industries.ToList();
        }

        public SubmitCardsDTO CreateSubmitCardsDTO()
        {
            SubmitCardsDTO dto = new SubmitCardsDTO();
            dto.Categories = Categories.Where(x => x.Importance != CategoryImportance.Irrelevant)
                                       .Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance, RowGuid = x.Id })
                                       .ToList();
            dto.CompanyName = CompanyName;
            dto.Positions = Positions;
            dto.Contact = Contact;
            dto.Notes = Notes;

            return dto;
        }
    }
}
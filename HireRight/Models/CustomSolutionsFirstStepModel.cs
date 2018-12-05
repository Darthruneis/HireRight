using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic;

namespace HireRight.Models
{
    public class CustomSolutionsFirstStepModel
    {
        [Required(ErrorMessage = "The Company Name field is required.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<string> Positions => (PositionsToFill ?? "").Split(',').Select(x => x.Trim()).ToList();

        [Required(ErrorMessage = "At least one position is required.")]
        [Display(Name = "Positions to fill (separated by commas)")]
        public string PositionsToFill { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "The Last Name field is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Personal Phone")]
        [RegularExpression(RegularExpressions.PhoneNumber, 
            ErrorMessage = "Invalid format. Please use the format '### ### ####'.")]
        public string CellNumber { get; set; }

        [RegularExpression(RegularExpressions.PhoneNumber,
            ErrorMessage = "Invalid format. Please use the format '### ### ####'.")]
        [Display(Name = "Office Phone")]
        public string OfficeNumber { get; set; }
        
        [Display(Name = "Email")]
        [RegularExpression(RegularExpressions.EmailAddress,
            ErrorMessage = "Invalid format. Please provide a valid email.")]
        public string Email { get; set; }
        
        public string Notes { get; set; }
    }
}
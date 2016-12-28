using DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public class JobAnalysisCategoryViewModel
    {
        public string Description { get; set; }
        public CategoryImportance Importance { get; set; }
        public string Title { get; set; }

        public JobAnalysisCategoryViewModel()
        {
            Importance = CategoryImportance.Irrelevant;
        }
    }
}
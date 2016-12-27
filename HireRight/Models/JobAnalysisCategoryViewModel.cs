using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public enum CategoryImportance
    {
        [Display(Name = "Not Important")]
        Irrelevant,

        [Display(Name = "Low Importance")]
        LowImportance,

        [Display(Name = "Normal Importance")]
        NormalImportance,

        [Display(Name = "High Importance")]
        HighImportance,
    }

    public class JobAnalysisCategoryViewModel
    {
        public string Description { get; set; }
        public CategoryImportance Importance { get; set; }
        public string Title { get; set; }

        public JobAnalysisCategoryViewModel()
        {
            Importance = CategoryImportance.NormalImportance;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects
{
    public enum CategoryImportance
    {
        [Display(Name = "Not Important")]
        Irrelevant,

        [Display(Name = "Nice To Have")]
        LowImportance,

        [Display(Name = "Critical for Success")]
        HighImportance,
    }
}
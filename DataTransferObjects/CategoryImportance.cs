using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects
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
}
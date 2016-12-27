using HireRight.EntityFramework.CodeFirst.Abstract;
using System.ComponentModel.DataAnnotations;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    public class ScaleCategory : PocoBase
    {
        /// <summary>
        /// The description for this scale category.
        /// </summary>
        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        public ScaleCategory()
        {
        }

        public ScaleCategory(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
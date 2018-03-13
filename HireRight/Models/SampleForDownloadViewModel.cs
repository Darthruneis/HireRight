using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireRight.Models
{
    public class SampleForDownloadViewModel
    {
        public string ActionName { get; set; }
        public string LinkText { get; set; }
        public string Description { get; set; }
        public bool IsAlternate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public SampleForDownloadViewModel(string linkText, string actionName, string description)
        {
            ActionName = actionName;
            LinkText = linkText;
            Description = description;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferObjects;

namespace HireRight.Models
{
    public class JobAnalysisCategoryViewModel
    {
        public string Description { get; set; }
        public Guid Id { get; set; }
        public CategoryImportance Importance { get; set; }
        public string Title { get; set; }
        public List<long> Industries { get; set; }

        public JobAnalysisCategoryViewModel()
        {
            Importance = CategoryImportance.LowImportance;
        }

        public JobAnalysisCategoryViewModel(Guid id, CategoryImportance importance)
        {
            Id = id;
            Importance = importance;
        }

        public JobAnalysisCategoryViewModel(string description, string title, Guid id, ICollection<long> industries) : this()
        {
            Description = description;
            Title = title;
            Id = id;
            Industries = industries.ToList();
        }

        public override string ToString()
        {
            return $"{Id},{Title},{Description}";
        }
    }
}
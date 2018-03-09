using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;

namespace HireRight.Models
{
    public class JobAnalysisCategoryViewModel
    {
        public string Description { get; set; }
        public Guid Id { get; set; }
        public CategoryImportance Importance { get; set; }
        public string Title { get; set; }
        public List<long> RelevantIndustries { get; set; }
        public List<IndustryDTO> AllIndustries { get; set; }

        public JobAnalysisCategoryViewModel()
        {
            Importance = CategoryImportance.LowImportance;
        }

        public JobAnalysisCategoryViewModel(Guid id, CategoryImportance importance)
        {
            Id = id;
            Importance = importance;
        }

        public JobAnalysisCategoryViewModel(string description, string title, Guid id, List<long> industries, List<IndustryDTO> allIndustries) : this()
        {
            Description = description;
            Title = title;
            Id = id;
            RelevantIndustries = industries.ToList();
            AllIndustries = allIndustries.ToList();
        }

        public override string ToString()
        {
            return $"{Id},{Title},{Description}";
        }
    }
}
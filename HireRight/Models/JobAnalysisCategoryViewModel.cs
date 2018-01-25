using System;
using DataTransferObjects;

namespace HireRight.Models
{
    public class JobAnalysisCategoryViewModel
    {
        public string Description { get; set; }
        public Guid Id { get; set; }
        public CategoryImportance Importance { get; set; }
        public bool IsInTopTwelve { get; set; }
        public string Title { get; set; }

        public JobAnalysisCategoryViewModel()
        {
            Importance = CategoryImportance.Irrelevant;
        }

        public JobAnalysisCategoryViewModel(Guid id, CategoryImportance importance)
        {
            Id = id;
            Importance = importance;
        }

        public JobAnalysisCategoryViewModel(string description, string title, Guid id) : this()
        {
            Description = description;
            Title = title;
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id},{Title},{Description}";
        }
    }
}
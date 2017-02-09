using System;
using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using System.ComponentModel.DataAnnotations;

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
    }
}
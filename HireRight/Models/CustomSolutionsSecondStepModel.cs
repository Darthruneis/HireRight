﻿using DataTransferObjects.Data_Transfer_Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DataTransferObjects;

namespace HireRight.Models
{
    public class CustomSolutionsSecondStepModel
    {
        public CustomSolutionsFirstStepModel PreviousInformation { get; set; }

        public IList<JobAnalysisCategoryViewModel> Categories { get; set; }

        public List<IndustryDTO> Industries { get; set; }

        public CustomSolutionsSecondStepModel()
        {
            Categories = new List<JobAnalysisCategoryViewModel>();
            PreviousInformation = new CustomSolutionsFirstStepModel();
            Industries = new List<IndustryDTO>();
        }

        public CustomSolutionsSecondStepModel(CustomSolutionsFirstStepModel previousInformation, IEnumerable<JobAnalysisCategoryViewModel> categories, IEnumerable<IndustryDTO> industries) : this()
        {
            PreviousInformation = previousInformation;
            Categories = categories.ToList();
            Industries = industries.ToList();
        }

        public SubmitCardsDTO CreateSubmitCardsDTO()
        {
            SubmitCardsDTO dto = new SubmitCardsDTO();
            dto.Categories = Categories.Where(x => x.Importance != CategoryImportance.Irrelevant)
                                       .Select(x => new CategoryDTO(x.Title, x.Description) { Importance = x.Importance, RowGuid = x.Id })
                                       .ToList();
            dto.CompanyName = PreviousInformation.CompanyName;
            dto.Positions = PreviousInformation.Positions;
            dto.Contact = PreviousInformation.Contact;
            dto.Notes = PreviousInformation.Notes;

            return dto;
        }

        public void RefreshModel(ICollection<IndustryDTO> industries, ICollection<CategoryDTO> categories)
        {
            Industries = industries.ToList();
            foreach (JobAnalysisCategoryViewModel jobAnalysisCategoryViewModel in Categories)
            {
                var dto = categories.SingleOrDefault(x => x.RowGuid == jobAnalysisCategoryViewModel.Id);
                if (dto == null)
                    continue;

                jobAnalysisCategoryViewModel.RelevantIndustries = dto.Industries;
                jobAnalysisCategoryViewModel.Description = dto.Description;
                jobAnalysisCategoryViewModel.Title = dto.Title;
                jobAnalysisCategoryViewModel.AllIndustries = Industries;
            }
        }
    }
}
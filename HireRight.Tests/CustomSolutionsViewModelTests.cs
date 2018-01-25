using System;
using System.Collections.Generic;
using DataTransferObjects.Filters.Concrete;
using FluentAssertions;
using HireRight.Models;
using NUnit.Framework;

namespace HireRight.Tests
{
    [TestFixture]
    public class CustomSolutionsViewModelTests
    {
        [Test]
        public void ToFilterReturnsCorrectPageSoThatAllCategoriesAreReturned()
        {
            var filterModel = new CategoryFilterViewModel() {Filter = new CategoryFilter(1, 10), TotalMatchingResults = 10};
            var jobAnalysisCategoryViewModels = new List<JobAnalysisCategoryViewModel>();
            jobAnalysisCategoryViewModels.Add(new JobAnalysisCategoryViewModel("desc", "title", Guid.NewGuid()));
            var model = new CustomSolutionsViewModel(jobAnalysisCategoryViewModels, filterModel);

            var result = model.ToFilter();

            result.PageNumber.Should().Be(1);
        }

        [Test]
        public void ToFilterAddsAllCategoriesToGuidList()
        {
            var filterModel = new CategoryFilterViewModel() { Filter = new CategoryFilter(1, 10), TotalMatchingResults = 10 };
            var jobAnalysisCategoryViewModels = new List<JobAnalysisCategoryViewModel>();
            jobAnalysisCategoryViewModels.Add(new JobAnalysisCategoryViewModel("desc", "title", Guid.NewGuid()));
            var model = new CustomSolutionsViewModel(jobAnalysisCategoryViewModels, filterModel);

            var result = model.ToFilter();

            result.ItemGuids.Count.Should().Be(jobAnalysisCategoryViewModels.Count);
        }

        [Test]
        public void CreateSubmitCardsDtoIncludesAllCategories()
        {
            var filterModel = new CategoryFilterViewModel() { Filter = new CategoryFilter(1, 10), TotalMatchingResults = 10 };
            var jobAnalysisCategoryViewModels = new List<JobAnalysisCategoryViewModel>();
            jobAnalysisCategoryViewModels.Add(new JobAnalysisCategoryViewModel("desc", "title", Guid.NewGuid()));
            var model = new CustomSolutionsViewModel(jobAnalysisCategoryViewModels, filterModel);

            var result = model.CreateSubmitCardsDTO();

            result.Categories.Count.Should().Be(jobAnalysisCategoryViewModels.Count);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.EntityFramework.CodeFirst.Seeds;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace HireRight.Repository.Tests
{
    [TestFixture, Explicit]
    public class ExportCategoriesToCsv
    {
        [Test]
        public void ExportAllCategoriesToCsvFile()
        {
            var seed = ScaleCategorySeed.Seed();

            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("\"ID\",\"Date Created\",\"Last Updated\",\"Owner\",\"Title\",\"Description\",\"StaticId\"");
            foreach (ScaleCategory scaleCategory in seed)
            {
                csvBuilder.AppendLine($"\"{Guid.NewGuid()}\",{DateTime.UtcNow:s}Z,{DateTime.UtcNow:s}Z,\"6edc4944-e501-4a45-b29c-b7599b1a96f6\",\"{scaleCategory.Title}\",\"desc\",\"{scaleCategory.StaticId}\"");
            }

            File.WriteAllText(@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\HireRight.Repository.Tests\ScaleCategories.csv", csvBuilder.ToString());
        }

        [Test]
        public void ExportAllIndustriesToCsvFile()
        {
            var seed = IndustrySeed.Seed;

            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("\"Name\",\"StaticId\"");
            foreach (var industry in seed)
            {
                csvBuilder.AppendLine($"\"{industry.Name}\",\"{industry.StaticId}\"");
            }

            File.WriteAllText(@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\HireRight.Repository.Tests\Industries.csv", csvBuilder.ToString());
        }

        [Test]
        public void ExportAllRelationshipsBetweenCategoryAndIndustryToCsvFile()
        {
            var seed = IndustryScaleCategorySeed.CreateRelationshipsUsingStaticIds(new HireRightDbContext());

            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("\"RandomGuid\",\"StaticIndustryId\",\"StaticCategoryId\"");
            foreach (var industry in seed)
            {
                csvBuilder.AppendLine($"\"{Guid.NewGuid()}\",\"{industry.IndustryId}\",\"{industry.CategoryId}\"");
            }

            File.WriteAllText(@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\HireRight.Repository.Tests\IndustryScaleCategoryBinders.csv", csvBuilder.ToString());
        }

        [Test]
        public void UpdateCategoryDescriptionsFromJson()
        {
            var json = @"[
  {
    ""ID"": ""f0589ed5-621f-4dfc-81f8-5f31442fa7c5"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""OCEAN - Extraversion"",
    ""Description"": ""desc"",
    ""StaticId"": 51
  },
  {
    ""ID"": ""8eb7f4eb-4bd7-4feb-a61b-489d0970ccee"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""OCEAN - Non-Negativity"",
    ""Description"": ""desc"",
    ""StaticId"": 52
  },
  {
    ""ID"": ""a7c53213-ab7d-4fa8-bae3-8066aa96cc50"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""OCEAN - Openness"",
    ""Description"": ""desc"",
    ""StaticId"": 53
  },
  {
    ""ID"": ""f85c8c6f-84d6-4e7d-b5a6-e45432b03cd9"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""PAP: Dedication"",
    ""Description"": ""desc"",
    ""StaticId"": 54
  },
  {
    ""ID"": ""ccb80e8c-a5fa-44ce-9507-767711eba82d"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""PAP: Initiative"",
    ""Description"": ""desc"",
    ""StaticId"": 55
  },
  {
    ""ID"": ""f8729373-8e4f-4093-90b5-0e007de95816"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""PAP: Open Mindedness"",
    ""Description"": ""desc"",
    ""StaticId"": 56
  },
  {
    ""ID"": ""a40dd74f-6b97-420c-8e6c-f269974e7594"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""PAP: Optimism"",
    ""Description"": ""desc"",
    ""StaticId"": 57
  },
  {
    ""ID"": ""80e7c308-76d8-4b18-a798-77fab14d85bf"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Problem Solving Interest"",
    ""Description"": ""desc"",
    ""StaticId"": 58
  },
  {
    ""ID"": ""88c120b1-4e26-4f56-a20b-12203e198890"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Realistic"",
    ""Description"": ""desc"",
    ""StaticId"": 59
  },
  {
    ""ID"": ""8594cb5b-3d05-4750-845c-abe365818348"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Reasoning"",
    ""Description"": ""desc"",
    ""StaticId"": 60
  },
  {
    ""ID"": ""5a17a7d9-46bc-4666-9017-5e79a98aead3"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Reliability"",
    ""Description"": ""desc"",
    ""StaticId"": 61
  },
  {
    ""ID"": ""b8cbf3b6-d82d-43e7-aaf9-7da5281c937e"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Responsibility"",
    ""Description"": ""desc"",
    ""StaticId"": 62
  },
  {
    ""ID"": ""4bd79cba-75c2-40a2-a094-18420b8e2a49"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Rules Compliance"",
    ""Description"": ""desc"",
    ""StaticId"": 63
  },
  {
    ""ID"": ""ac9879c3-1d68-4a33-a290-ef036e9a6e88"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Safety"",
    ""Description"": ""desc"",
    ""StaticId"": 64
  },
  {
    ""ID"": ""28609758-962b-4ecd-8278-963f038cacc0"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Safety Profile"",
    ""Description"": ""desc"",
    ""StaticId"": 65
  },
  {
    ""ID"": ""43f738e4-2152-491c-8eb1-44f11ac2ee45"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Self Confidence"",
    ""Description"": ""desc"",
    ""StaticId"": 66
  },
  {
    ""ID"": ""48409ee9-a6e0-4dd5-8c90-ab4b329fed03"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Self Control"",
    ""Description"": ""desc"",
    ""StaticId"": 67
  },
  {
    ""ID"": ""78a78f25-a9ab-4bd9-bfd6-37d0fd18bf08"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Service"",
    ""Description"": ""desc"",
    ""StaticId"": 68
  },
  {
    ""ID"": ""193bf4d8-9ee7-45c0-ba82-9f857a4580f7"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Management - Communication"",
    ""Description"": ""desc"",
    ""StaticId"": 69
  },
  {
    ""ID"": ""3781d3bf-bb16-4bef-a31b-d68f9b1d80a7"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Management - Decision Making"",
    ""Description"": ""desc"",
    ""StaticId"": 70
  },
  {
    ""ID"": ""b0f4e94a-1786-4dce-8b29-0cba6e503879"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Management - Delegation"",
    ""Description"": ""desc"",
    ""StaticId"": 71
  },
  {
    ""ID"": ""4cde3774-c7a9-4f9f-a088-af056ed32d86"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Management - Employee Relations"",
    ""Description"": ""desc"",
    ""StaticId"": 72
  },
  {
    ""ID"": ""a01fb9ae-2adf-478f-852e-b12daab6f935"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Sales - Customer Focus"",
    ""Description"": ""desc"",
    ""StaticId"": 73
  },
  {
    ""ID"": ""cad6b8d9-d1e2-4bae-9a82-b988415d3c5c"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Sales - Drive and Persistence"",
    ""Description"": ""desc"",
    ""StaticId"": 74
  },
  {
    ""ID"": ""199e48cf-74d3-471d-a844-0262280e38aa"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Sales - Listening Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 75
  },
  {
    ""ID"": ""78f84979-e592-4c6b-b8eb-cc2498c07d30"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Sales - Sales Strategies"",
    ""Description"": ""desc"",
    ""StaticId"": 76
  },
  {
    ""ID"": ""9c6db98b-de0f-4901-8586-bf0e762922da"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Service - Conscientiousness"",
    ""Description"": ""desc"",
    ""StaticId"": 77
  },
  {
    ""ID"": ""c0480fcd-6370-4866-bc02-32ad6f5227a8"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Service - Interpersonal Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 78
  },
  {
    ""ID"": ""178a0f54-b702-4f50-983d-0727d6bc2010"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Service - Listening Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 79
  },
  {
    ""ID"": ""b581c27a-e845-4574-8c4b-5cebeb909104"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Service - Service-Orientation"",
    ""Description"": ""desc"",
    ""StaticId"": 80
  },
  {
    ""ID"": ""79beb3b7-fbdc-480b-81eb-66c924e0d3d6"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Supervisor - Communication"",
    ""Description"": ""desc"",
    ""StaticId"": 81
  },
  {
    ""ID"": ""fadd33ba-547b-40f1-afbf-2299675b08ca"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Supervisor - Conscientiousness"",
    ""Description"": ""desc"",
    ""StaticId"": 82
  },
  {
    ""ID"": ""201e36aa-265f-40db-91ff-63e12d2f902a"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Supervisor - Motivation"",
    ""Description"": ""desc"",
    ""StaticId"": 83
  },
  {
    ""ID"": ""c5da28fd-b2e0-4f7d-ba15-6da1fcbcf379"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Supervisor - Team Orientation"",
    ""Description"": ""desc"",
    ""StaticId"": 84
  },
  {
    ""ID"": ""824790fa-4302-427d-8ba2-5727abf466a2"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Team - Confidence"",
    ""Description"": ""desc"",
    ""StaticId"": 85
  },
  {
    ""ID"": ""7b729b33-19aa-446e-b4eb-84e712e31447"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Team - Flexibility"",
    ""Description"": ""desc"",
    ""StaticId"": 86
  },
  {
    ""ID"": ""89553951-afb1-47be-87f7-9632a7ce5f22"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Team - Team Spirit"",
    ""Description"": ""desc"",
    ""StaticId"": 87
  },
  {
    ""ID"": ""33360fab-b369-42eb-b0fc-94bea2803919"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""SJT - Team - Trust"",
    ""Description"": ""desc"",
    ""StaticId"": 88
  },
  {
    ""ID"": ""cf5e3159-7ffe-438d-a9c0-5c27367422bd"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Social"",
    ""Description"": ""desc"",
    ""StaticId"": 89
  },
  {
    ""ID"": ""1cae000c-7bc2-4844-b2f2-67516bbe3862"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Spatial Reasoning"",
    ""Description"": ""desc"",
    ""StaticId"": 90
  },
  {
    ""ID"": ""cf08ed55-131d-4764-8b78-6f1ddc41467d"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Stress Management"",
    ""Description"": ""desc"",
    ""StaticId"": 91
  },
  {
    ""ID"": ""d0753145-60f9-4de8-bef2-66968f3e93d1"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Supervision"",
    ""Description"": ""desc"",
    ""StaticId"": 92
  },
  {
    ""ID"": ""3b58bd42-5d1b-4043-906b-df6f9d0a73e5"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Tables"",
    ""Description"": ""desc"",
    ""StaticId"": 93
  },
  {
    ""ID"": ""991847c6-c67a-4f69-a792-7ddf01a1c2a9"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Team Care"",
    ""Description"": ""desc"",
    ""StaticId"": 94
  },
  {
    ""ID"": ""f6b3e829-dde1-44c1-8a5d-df0cc0c849a3"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Team Player"",
    ""Description"": ""desc"",
    ""StaticId"": 95
  },
  {
    ""ID"": ""b739a845-5dd1-4325-876a-45e74802b543"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Trustworthiness"",
    ""Description"": ""desc"",
    ""StaticId"": 96
  },
  {
    ""ID"": ""7f99947b-f576-492b-82fd-d68c2caad4b1"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Verbal Reasoning"",
    ""Description"": ""desc"",
    ""StaticId"": 97
  },
  {
    ""ID"": ""554a2e6c-6778-4d75-ad82-be7774cccbe1"",
    ""Date Created"": ""2018-03-24T21:05:58Z"",
    ""Last Updated"": ""2018-03-24T21:05:58Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Work Ethic"",
    ""Description"": ""desc"",
    ""StaticId"": 98
  },
  {
    ""ID"": ""9c3fae25-9461-49bf-8b04-06a25b9aee29"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Achievement Drive"",
    ""Description"": ""desc"",
    ""StaticId"": 1
  },
  {
    ""ID"": ""a3049dea-99cb-40bc-b3ca-dde8e93f28f6"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Artistic"",
    ""Description"": ""desc"",
    ""StaticId"": 2
  },
  {
    ""ID"": ""3172b448-e4f3-40d4-965b-0886068ac5fa"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Assertiveness"",
    ""Description"": ""desc"",
    ""StaticId"": 3
  },
  {
    ""ID"": ""054028e6-c684-4dc4-91e1-f800a08f3496"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Attention to Detail"",
    ""Description"": ""desc"",
    ""StaticId"": 4
  },
  {
    ""ID"": ""4064e940-7f0c-4181-b251-fea95b69aa54"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""C5 Business: Commitment"",
    ""Description"": ""desc"",
    ""StaticId"": 5
  },
  {
    ""ID"": ""5126714c-1d10-4d53-b164-93eb3fc2e560"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""C5 Business: Competitiveness"",
    ""Description"": ""desc"",
    ""StaticId"": 6
  },
  {
    ""ID"": ""86d8d5a1-2ab3-4361-9b44-dfc4b55257e0"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""C5 Business: Conscientiousness"",
    ""Description"": ""desc"",
    ""StaticId"": 7
  },
  {
    ""ID"": ""51dad018-1aaf-4a09-bd72-0dddfeb0990b"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""C5 Business: Control"",
    ""Description"": ""desc"",
    ""StaticId"": 8
  },
  {
    ""ID"": ""fbb00a86-8ce7-405e-9ade-777de074ed24"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""C5 Business: Cooperativeness"",
    ""Description"": ""desc"",
    ""StaticId"": 9
  },
  {
    ""ID"": ""977bf62a-7327-405b-8ce2-9cfcf7e60319"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Achievement Drive"",
    ""Description"": ""desc"",
    ""StaticId"": 10
  },
  {
    ""ID"": ""56b392f2-15ff-4aa4-a8e9-06f0a08ed0b1"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Assertiveness"",
    ""Description"": ""desc"",
    ""StaticId"": 11
  },
  {
    ""ID"": ""582d8ac5-b779-4e89-97af-afcc2229b33e"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Positive Attitude"",
    ""Description"": ""desc"",
    ""StaticId"": 12
  },
  {
    ""ID"": ""822ec328-09a4-4afa-8a32-0773a7d05ec1"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Reliability"",
    ""Description"": ""desc"",
    ""StaticId"": 13
  },
  {
    ""ID"": ""1d218f86-449b-4370-8a60-c9751cfb52ac"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Self Confidence"",
    ""Description"": ""desc"",
    ""StaticId"": 14
  },
  {
    ""ID"": ""9f166490-9d93-44bf-906d-fb46435f212d"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Sales) Service Ability"",
    ""Description"": ""desc"",
    ""StaticId"": 15
  },
  {
    ""ID"": ""07682fb3-f981-4166-927b-e6fa0d27ddc6"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Service) Customer Relations"",
    ""Description"": ""desc"",
    ""StaticId"": 16
  },
  {
    ""ID"": ""166d7a25-69e7-4698-ae6c-fc7f142880c2"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Service) Stress Management"",
    ""Description"": ""desc"",
    ""StaticId"": 17
  },
  {
    ""ID"": ""e4850467-c5e9-40ae-b019-6dd96ce7e24d"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Service) Team Player"",
    ""Description"": ""desc"",
    ""StaticId"": 18
  },
  {
    ""ID"": ""7531f5e1-ac54-43c7-afe6-2ef4a30a782a"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Call Center (Service) Willingness to Help"",
    ""Description"": ""desc"",
    ""StaticId"": 19
  },
  {
    ""ID"": ""fa846aef-91fa-4f18-ab48-ec51796fbcad"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Candidness"",
    ""Description"": ""desc"",
    ""StaticId"": 20
  },
  {
    ""ID"": ""1d474ed2-841d-4a6f-b31a-0e4607fb02bc"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Can-Do Attitude"",
    ""Description"": ""desc"",
    ""StaticId"": 21
  },
  {
    ""ID"": ""dc73a870-3d8b-4cf1-a437-00f8c5d2faaa"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Conventional"",
    ""Description"": ""desc"",
    ""StaticId"": 22
  },
  {
    ""ID"": ""daa50aba-f050-4acb-b4d9-3fe0457cae02"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Creativity"",
    ""Description"": ""desc"",
    ""StaticId"": 23
  },
  {
    ""ID"": ""460e97d7-9d1d-435d-af6a-8ce1de673cb8"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Customer Care"",
    ""Description"": ""desc"",
    ""StaticId"": 24
  },
  {
    ""ID"": ""ad56cbd4-56ae-40b3-a2c5-7f3928585aee"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Drug Free Attitudes"",
    ""Description"": ""desc"",
    ""StaticId"": 25
  },
  {
    ""ID"": ""e60c2c6c-7f7d-409c-852d-175e39e596fb"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Energy"",
    ""Description"": ""desc"",
    ""StaticId"": 26
  },
  {
    ""ID"": ""4cb2c79c-f691-497e-841a-f896f14af68c"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Enterprising"",
    ""Description"": ""desc"",
    ""StaticId"": 27
  },
  {
    ""ID"": ""a47cc1b5-3134-4bc2-b2f2-59da6fccbbc4"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Extraversion"",
    ""Description"": ""desc"",
    ""StaticId"": 28
  },
  {
    ""ID"": ""7c919039-b461-40bd-97cf-8c6d6dc94305"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Flexibility"",
    ""Description"": ""desc"",
    ""StaticId"": 29
  },
  {
    ""ID"": ""1ce9ff4b-cee0-4e33-af2b-9fb27b4ebb34"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Goal Focus"",
    ""Description"": ""desc"",
    ""StaticId"": 30
  },
  {
    ""ID"": ""4eeb364d-6e48-416a-96d5-62df212b99c0"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Go-Getter Attitude"",
    ""Description"": ""desc"",
    ""StaticId"": 31
  },
  {
    ""ID"": ""3951aac3-339d-4d9f-abb5-75c1b4f4803d"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Good Citizen"",
    ""Description"": ""desc"",
    ""StaticId"": 32
  },
  {
    ""ID"": ""ed070fde-0880-41d8-9537-1d5541fd7466"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Healthcare - Compassion"",
    ""Description"": ""desc"",
    ""StaticId"": 33
  },
  {
    ""ID"": ""8edc2634-3d39-4bf5-a727-a102713ed372"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Healthcare - Patient Relations"",
    ""Description"": ""desc"",
    ""StaticId"": 34
  },
  {
    ""ID"": ""a27f0ffb-cc5c-4677-b014-bb199eda7ef0"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Healthcare - Stress Tolerance"",
    ""Description"": ""desc"",
    ""StaticId"": 35
  },
  {
    ""ID"": ""c3b5a1fb-a476-422e-ae29-a404a3eff34d"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Healthcare - Team Player"",
    ""Description"": ""desc"",
    ""StaticId"": 36
  },
  {
    ""ID"": ""a12bfe16-bf37-44f1-a96e-5b257b72cfd6"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Helping Disposition"",
    ""Description"": ""desc"",
    ""StaticId"": 37
  },
  {
    ""ID"": ""ab4fe1e7-2be9-4943-9ace-a83ff7001cf7"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Inspection"",
    ""Description"": ""desc"",
    ""StaticId"": 38
  },
  {
    ""ID"": ""5d0847b2-fa76-43e9-bb94-6ff8c7d3d387"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Interpersonal Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 39
  },
  {
    ""ID"": ""029abfd5-8eec-4a2b-9578-fde88daf4145"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Investigative"",
    ""Description"": ""desc"",
    ""StaticId"": 40
  },
  {
    ""ID"": ""716af619-202a-47c8-a443-59be7cd84d9e"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Kindness"",
    ""Description"": ""desc"",
    ""StaticId"": 41
  },
  {
    ""ID"": ""37518422-ea1f-4573-9d59-0269d0967a09"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Language Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 42
  },
  {
    ""ID"": ""b39c196c-71fb-4523-8061-487362ba7386"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Leadership"",
    ""Description"": ""desc"",
    ""StaticId"": 43
  },
  {
    ""ID"": ""0d19930c-3a9d-4df2-a8b6-2912abeb7b9f"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Light Industrial Math"",
    ""Description"": ""desc"",
    ""StaticId"": 44
  },
  {
    ""ID"": ""08e3b985-f1c3-4687-ad78-630495d3d7ca"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Math Skills"",
    ""Description"": ""desc"",
    ""StaticId"": 45
  },
  {
    ""ID"": ""95cb3f87-e346-40b1-a0a8-455b038dab13"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Mathematical and Logical Reasoning"",
    ""Description"": ""desc"",
    ""StaticId"": 46
  },
  {
    ""ID"": ""896245bf-edf3-4754-a58d-aaec7750a3af"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Mechanical Aptitude Profile -one test can not be part of custom profile"",
    ""Description"": ""desc"",
    ""StaticId"": 47
  },
  {
    ""ID"": ""42c392dc-df1b-418f-bb52-272b2a4ec92a"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""Non-Violent Attitudes"",
    ""Description"": ""desc"",
    ""StaticId"": 48
  },
  {
    ""ID"": ""ceb97007-ed92-4e33-a7d7-21bcf481f029"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""OCEAN - Agreeableness"",
    ""Description"": ""desc"",
    ""StaticId"": 49
  },
  {
    ""ID"": ""bf907a66-c30e-4c05-abf4-76009fbeb60e"",
    ""Date Created"": ""2018-03-24T21:05:57Z"",
    ""Last Updated"": ""2018-03-24T21:05:57Z"",
    ""Owner"": ""6edc4944-e501-4a45-b29c-b7599b1a96f6"",
    ""Title"": ""OCEAN - Conscientiousness"",
    ""Description"": ""desc"",
    ""StaticId"": 50
  }
]";
            var seed = ScaleCategorySeed.Seed();
            var parsed = JsonConvert.DeserializeObject<WixScaleCategory[]>(json);
            foreach (WixScaleCategory wixScaleCategory in parsed)
            {
                wixScaleCategory.Description = seed.Single(x => x.StaticId == wixScaleCategory.StaticId).Description;
            }

            for (int i = 0; i <= parsed.Length / 10; i++)
            {
                //take 10 categories per file
                StringBuilder csvBuilder = new StringBuilder();
                csvBuilder.AppendLine($"\"{nameof(WixScaleCategory.ID)}\",\"{nameof(WixScaleCategory.Owner)}\",\"Date Created\",\"Last Updated\",\"{nameof(WixScaleCategory.Title)}\",\"{nameof(WixScaleCategory.Description)}\",\"{nameof(WixScaleCategory.StaticId)}\"");
                var subSet = parsed.Skip(10 * i).Take(10);
                foreach (WixScaleCategory wixScaleCategory in subSet)
                {
                    csvBuilder.AppendLine($"\"{wixScaleCategory.ID}\",\"{wixScaleCategory.Owner}\",{wixScaleCategory.DateCreated},{DateTime.UtcNow:u},\"{wixScaleCategory.Title}\",\"{wixScaleCategory.Description.Replace("\"", "'")}\",\"{wixScaleCategory.StaticId}\"");
                }
                if(subSet.Any())
                    File.WriteAllText($@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\HireRight.Repository.Tests\UpdatedCategoriesWithDescriptions-{i}.csv", csvBuilder.ToString());
            }
        }

        public class WixScaleCategory
        {
            public string ID { get; set; }
            public string Owner { get; set; }
            [JsonProperty(PropertyName = "Date Created")]
            public string DateCreated { get; set; }
            [JsonProperty(PropertyName = "Last Updated")]
            public string LastUpdated { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public long StaticId { get; set; }
        }
    }
}
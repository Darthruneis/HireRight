using System.Collections.Generic;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using HireRight.Models;

namespace HireRight.Controllers
{
    public partial class ReportsController : Controller
    {
        [HttpGet]
        public PartialViewResult Categories() => PartialView("ExampleCategoriesBreakdownSectionPartial");

        [HttpGet]
        public PartialViewResult CategoryBreakdown() => PartialView("ExampleCategoryDetailedBreakdownSectionPartial");

        public ViewResult Index() => View();

        [HttpGet]
        public PartialViewResult InterviewGuide() => PartialView("ExampleInterviewGuideSectionPartial");

        [HttpGet]
        public PartialViewResult Introduction() => PartialView("SampleReportsIntroSectionPartial");

        [HttpGet]
        public PartialViewResult ManagementStrategies() => PartialView("ExampleManagementStrategiesSectionPartial");

        [HttpGet]
        public PartialViewResult OverallScore() => PartialView("ExampleOverallScoreSectionPartial");

        [HttpGet]
        public ViewResult Samples()
        {
            return View(SamplesList);
        }
    }

    /// <summary>
    /// This part of the controller handles all of the downloads
    /// </summary>
    public partial class ReportsController
    {
        private readonly string _filePathBase = System.Web.HttpContext.Current.Server.MapPath("~") + "/Content/ProfileSamples/";

        [HttpGet]
        public ActionResult EliteCareProfileSample() => DownloadFromFileName("EliteCareProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteCharacterProfileSample() => DownloadFromFileName("EliteCharacterProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteHealthcareProfileSample() => DownloadFromFileName("EliteHealthcareProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteIndustrialProfileSample() => DownloadFromFileName("EliteIndustrialProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteIntellectProfileSample() => DownloadFromFileName("EliteIntellectProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteManagerProfileSample() => DownloadFromFileName("EliteManagerProfileSample.pdf");

        [HttpGet]
        public ActionResult ElitePersonalityProfileSample() => DownloadFromFileName("ElitePersonalityProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteSalesProfileSample() => DownloadFromFileName("EliteSalesProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteSkillsProfileSample() => DownloadFromFileName("EliteSkillsProfileSample.pdf");

        [HttpGet]
        public ActionResult EliteTransportationProfileSample() => DownloadFromFileName("EliteTransportationProfileSample.pdf");

        [HttpGet]
        public ActionResult EQProfileSample() => DownloadFromFileName("EQProfileSample.pdf");

        [HttpGet]
        public ActionResult MechanicalAptitudeTestProfileSample() => DownloadFromFileName("MechanicalAptitudeTestSample.pdf");

        [HttpGet]
        public ActionResult SalesHunterProfileTestSample() => DownloadFromFileName("SalesHunterProfileSampleReport.pdf");

        [HttpGet]
        public ActionResult SituationalJudgementTestSalesSample() => DownloadFromFileName("SJTSalesSample.pdf");

        [HttpGet]
        public ActionResult SituationalJudgementTestSupervisorSample() => DownloadFromFileName("SJTSupervisorSample.pdf");

        [HttpGet]
        public ActionResult SituationalJudgementTestTeamsSample() => DownloadFromFileName("SJTTeamsSample.pdf");

        [HttpGet]
        public ActionResult WorkplaceAptitudeTestSample() => DownloadFromFileName("WATSample.pdf");
        
        [HttpGet]
        public ActionResult EliteGritProfileSample() => DownloadFromFileName("EliteGritProfileSample.pdf");
    }

    /// <summary>
    /// Helper methods
    /// </summary>
    public partial class ReportsController
    {
        private static List<SampleForDownloadViewModel> SamplesList => new List<SampleForDownloadViewModel>()
        {
            new SampleForDownloadViewModel(nameof(EliteCareProfileSample), "Elite Care Profile"),
            new SampleForDownloadViewModel(nameof(EliteCharacterProfileSample), "Elite Character Profile"),
            new SampleForDownloadViewModel(nameof(EliteHealthcareProfileSample), "Elite Healthcare Profile"),
            new SampleForDownloadViewModel(nameof(EliteIndustrialProfileSample), "Elite Industrial Profile"),
            new SampleForDownloadViewModel(nameof(EliteIntellectProfileSample), "Elite Intellect Profile"),
            new SampleForDownloadViewModel(nameof(EliteManagerProfileSample), "Elite Manager Profile"),
            new SampleForDownloadViewModel(nameof(ElitePersonalityProfileSample), "Elite Personality Profile"),
            new SampleForDownloadViewModel(nameof(EliteSalesProfileSample), "Elite Sales Profile"),
            new SampleForDownloadViewModel(nameof(EliteSkillsProfileSample), "Elite Skills Profile"),
            new SampleForDownloadViewModel(nameof(EliteTransportationProfileSample), "Elite Transportation Profile"),
            new SampleForDownloadViewModel(nameof(EQProfileSample), "EQ (Emotional Intelligence) Profile"),
            new SampleForDownloadViewModel(nameof(MechanicalAptitudeTestProfileSample), "Mechanical Aptitude Test"),
            new SampleForDownloadViewModel(nameof(SalesHunterProfileTestSample), "Sales Hunter Profile Test"),
            new SampleForDownloadViewModel(nameof(SituationalJudgementTestSalesSample), "Situational Judgement Test - Sales"),
            new SampleForDownloadViewModel(nameof(SituationalJudgementTestSupervisorSample), "Situational Judgement Test - Supervisor"),
            new SampleForDownloadViewModel(nameof(SituationalJudgementTestTeamsSample), "Situational Judgement Test - Teams"),
            new SampleForDownloadViewModel(nameof(WorkplaceAptitudeTestSample), "Workplace Aptitude Test"),
            new SampleForDownloadViewModel(nameof(EliteGritProfileSample), "Elite Grit Profile"),
        };

        private ActionResult Download(string filePath)
        {
            return File(filePath, MimeMapping.GetMimeMapping(filePath));
        }

        private ActionResult DownloadFromFileName(string fileName)
        {
            string testFilePath = _filePathBase + fileName;

            return Download(testFilePath);
        }
    }
}
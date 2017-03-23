using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public partial class ReportsController : Controller
    {
        [HttpGet]
        public PartialViewResult Categories()
        {
            return PartialView("ExampleCategoriesBreakdownSectionPartial");
        }

        [HttpGet]
        public PartialViewResult CategoryBreakdown()
        {
            return PartialView("ExampleCategoryDetailedBreakdownSectionPartial");
        }

        public ViewResult Index() => View();

        [HttpGet]
        public PartialViewResult InterviewGuide()
        {
            return PartialView("ExampleInterviewGuideSectionPartial");
        }

        [HttpGet]
        public PartialViewResult Introduction()
        {
            return PartialView("SampleReportsIntroSectionPartial");
        }

        [HttpGet]
        public PartialViewResult ManagementStrategies()
        {
            return PartialView("ExampleManagementStrategiesSectionPartial");
        }

        [HttpGet]
        public PartialViewResult OverallScore()
        {
            return PartialView("ExampleOverallScoreSectionPartial");
        }

        [HttpGet]
        public ViewResult Samples()
        {
            return View();
        }
    }

    /// <summary>
    /// This part of the controller handles all of the downloads
    /// </summary>
    public partial class ReportsController
    {
        private readonly string _filePathBase = System.Web.HttpContext.Current.Server.MapPath("~") + "/Content/ProfileSamples/";

        [HttpGet]
        public FileContentResult EliteCareProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteCareProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteCharacterProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteCharacterProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteHealthcareProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteHealthcareProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteIndustrialProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteIndustrialProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteIntellectProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteIntellectProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteManagerProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteManagerProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult ElitePersonalityProfileSample(bool inline = false)
        {
            return DownloadFromFileName("ElitePersonalityProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteSalesProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteSalesProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteSkillsProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteSkillsProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EliteTransportationProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EliteTransportationProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult EQProfileSample(bool inline = false)
        {
            return DownloadFromFileName("EQProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult MechanicalAptitudeTestProfileSample(bool inline = false)
        {
            return DownloadFromFileName("MechanicalAptitudeTestProfileSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult SituationalJudgementTestSalesSample(bool inline = false)
        {
            return DownloadFromFileName("SJTSalesSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult SituationalJudgementTestSupervisorSample(bool inline = false)
        {
            return DownloadFromFileName("SJTSupervisorSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult SituationalJudgementTestTeamsSample(bool inline = false)
        {
            return DownloadFromFileName("SJTTeamsSample.pdf", inline);
        }

        [HttpGet]
        public FileContentResult WorkplaceAptitudeTestSample(bool inline = false)
        {
            return DownloadFromFileName("WATSample.pdf", inline);
        }

        private FileContentResult Download(string filePath, string fileName, bool inline)
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            string contentType = MimeMapping.GetMimeMapping(filePath);

            ContentDisposition contentDisposition = new ContentDisposition
            {
                FileName = fileName,
                Size = fileData.LongLength,
                Inline = inline,
            };

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());

            return File(fileData, contentType);
        }

        private FileContentResult DownloadFromFileName(string fileName, bool inline)
        {
            string testFilePath = _filePathBase + fileName;

            return Download(testFilePath, fileName, inline);
        }
    }
}
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
            return View("Samples", SamplesList);
        }

        private static readonly List<SampleForDownloadViewModel> SamplesList =
            new List<SampleForDownloadViewModel>()
            {
                new SampleForDownloadViewModel("Elite Care Profile", nameof(EliteCareProfileSample),
                                               "The Elite Care Profile is a general indicator of the individual’s ability to engage in care-oriented behaviors across a wide range of care-focused environments (e.g. healthcare, long-term care, in home care). The profile measures a wide range of characteristics that center around being kind and caring while exhibiting conscientious and compliant behaviors."),
                new SampleForDownloadViewModel("Elite Character Profile", nameof(EliteCharacterProfileSample),
                                               "The Elite Character Profile is a general indicator of the individual's ability to refrain from participating in counterproductive behaviors by being trustworthy, drug-free, non-violent and compliant. This battery is appropriate for most jobs."),
                new SampleForDownloadViewModel("Elite Healthcare Profile", nameof(EliteHealthcareProfileSample),
                                               "The Elite Healthcare Profile is a general indicator of the individual's ability to engage in service-oriented behaviors within the Healthcare environment. This profile is appropriate for healthcare professionals who interact with patients (e.g., nurses, doctors, therapists, healthcare technicians, etc.)."),
                new SampleForDownloadViewModel("Elite Industrial Profile", nameof(EliteIndustrialProfileSample),
                                               "The Elite Industrial Profile is a general indicator of the individual's ability to perform the basic skills that underlie most entry-level through supervisory positions within a manufacturing or light industrial setting, such as basic math, assembly, and inspection. Other key characteristics assessed are safety, responsibility and reliable work habits."),
                new SampleForDownloadViewModel("Elite Intellect Profile", nameof(EliteIntellectProfileSample),
                                               "Overall Intellect; Is a general indicator of the individual's ability to think quickly and solve problems. It is also a valid indicator of an individual's training potential."),
                new SampleForDownloadViewModel("Elite Manager Profile", nameof(EliteManagerProfileSample),
                                               "The Elite Management Profile is a general indicator of the individual's ability to lead and manage others. Adding the Elite Intellect Profile helps identify those who can also problem solve, learn and think quickly."),
                new SampleForDownloadViewModel("Elite Personality Profile", nameof(ElitePersonalityProfileSample),
                                               "The Elite Personality Profile is a general indicator of the individual's strength or weakness on ten personality dimensions generally perceived to be important for a wide range of occupations. The individual scale scores offer detailed insights with respect to the applicant's personality and potential job fit."),
                new SampleForDownloadViewModel("Elite Sales Profile", nameof(EliteSalesProfileSample),
                                               "The Elite Sales Profile is a general indicator of the individual's ability to persuade prospects and existing customers to purchase specific products and/or services. This battery is appropriate for most sales-related jobs."),
                new SampleForDownloadViewModel("Elite Skills Profile", nameof(EliteSkillsProfileSample),
                                               "The Elite Skills Profile is a general indicator of the individual's ability to perform the basic skills that underlie most entry-level through supervisory positions (i.e., math skills, attention to detail, grammar and reading tables)."),
                new SampleForDownloadViewModel("Elite Transportation Profile", nameof(EliteTransportationProfileSample),
                                               "The Elite Transportation Profile is a general indicator of the individual's ability to behave responsibly, be safety conscious and follow rules and procedures. This profile is ideal for drivers and warehouse packers and shippers."),
                new SampleForDownloadViewModel("Mechanical Aptitude Test", nameof(MechanicalAptitudeTestProfileSample),
                                               "An objective measure of an individual’s knowledge of general mechanical concepts. Consists of Electrical, Mechanical Movements, Physical Properties, Spatial Reasoning & Measurement"),
                new SampleForDownloadViewModel("Situational Judgement Test - Sales", nameof(SituationalJudgementTestSalesSample),
                                               "The Situational Judgment Test - Sales consists of attitudinal, behavioral and situational questions aimed at assessing the candidate's ability to problem solve and use appropriate judgment while performing the sales function. The test is a general indicator of the individual's ability to persuade prospects and existing customers to purchase specific products and/or services."),
                new SampleForDownloadViewModel("Situational Judgement Test - Supervisor", nameof(SituationalJudgementTestSupervisorSample),
                                               "The Situational Judgment Test – Supervisor consists of attitudinal, behavioral and situational questions aimed at assessing the candidate's ability to problem solve and use appropriate judgment while performing the supervisory function. The test is designed to help your business succeed by identifying those individuals with strong supervisory skills, as well as identifying potential areas for development among your management staff."),
                new SampleForDownloadViewModel("Situational Judgement Test - Teams", nameof(SituationalJudgementTestTeamsSample),
                                               "The Situational Judgment Test – Teams consists of attitudinal, behavioral and situational questions aimed at assessing the candidate's ability to problem solve and use appropriate judgment in team environments. The test is designed to help identify those individuals who are more likely to be productive in work environments where teamwork is critical for success."),
                new SampleForDownloadViewModel("Workplace Aptitude Test", nameof(WorkplaceAptitudeTestSample),
                                               "The Workplace Aptitude Test measures the degree to which the individual has the ability to solve typical problems encountered at work; this includes solving problems that require the use of math and reasoning and the use of basic vocabulary one might encounter in the workplace. This scale is timed, therefore quickness of thought is also important."),
                new SampleForDownloadViewModel("EQ (Emotional Intelligence) Profile", nameof(EQProfileSample), 
                                               "The EQ Profile is a general indicator of the individual's ability to recognize and regulate emotions within themselves and in others. These abilities have been found to be of great importance in various aspects of career success. The Emotional Intelligence literature, including the pioneering work of Dr. Daniel Goleman, suggests that Emotional Intelligence can be broken down into four domains: Self-Awareness, Social-Awareness, Self Management and Relationship Management. The EQ Profile is a reliable measure of these four characteristics."),
                new SampleForDownloadViewModel("Sales Hunter Profile", nameof(SalesHunterProfileTestSample),
                                               "The Sales Hunter Profile is a general indicator of the individual's ability and desire to be achievement driven, outgoing, confident and an overall go-getter. These characteristics have been scientifically proven to be predictive of top sales and management performance."),
                new SampleForDownloadViewModel("Elite Grit Profile", nameof(EliteGritProfileSample),
                                               "The Elite Grit Profile is a general indicator of the individual's ability to work hard and persevere even in the face of setbacks and to stay focused on set goals regardless of any interference they may encounter."),
            };
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
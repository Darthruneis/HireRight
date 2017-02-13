namespace HireRight.Models
{
    public class SectionWithTogglesAndTitleViewModel
    {
        public string ActionText { get; set; }
        public string HiddenContentId { get; set; }
        public string HideButtonId { get; set; }
        public string ShowButtonId { get; set; }
        public string ShownContentId { get; set; }
        public string TitleText { get; set; }
        public object ViewModelForContentView { get; set; }
        public string ViewNameWithPath { get; set; }

        public SectionWithTogglesAndTitleViewModel()
        {
            ActionText = "show the content again.";
            ViewModelForContentView = null;
        }

        public SectionWithTogglesAndTitleViewModel(string actionText, string hiddenContentId, string hideButtonId, string showButtonId, string shownContentId, string viewNameWithPath, string titleText)
        {
            ActionText = actionText;
            HiddenContentId = hiddenContentId;
            HideButtonId = hideButtonId;
            ShowButtonId = showButtonId;
            ShownContentId = shownContentId;
            ViewNameWithPath = viewNameWithPath;
            TitleText = titleText;
        }
    }
}
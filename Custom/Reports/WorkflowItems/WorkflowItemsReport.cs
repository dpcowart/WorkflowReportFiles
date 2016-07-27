using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Custom.Reports.WorkflowItems
{
    /// <summary>
    /// Class used to create custom page widget
    /// </summary>
    /// <remarks>
    /// If this widget is a part of a Sitefinity module,
    /// you can register it in the site's toolbox by adding this to the module's Install/Upgrade method(s):
    /// initializer.Installer
    ///     .Toolbox(CommonToolbox.PageWidgets)
    ///         .LoadOrAddSection(SectionName)
    ///             .SetTitle(SectionTitle) // When creating a new section
    ///             .SetDescription(SectionDescription) // When creating a new section
    ///             .LoadOrAddWidget<WorkflowItemsReport>("WorkflowItemsReport")
    ///                 .SetTitle("WorkflowItemsReport")
    ///                 .SetDescription("WorkflowItemsReport")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner))]
    public class WorkflowItemsReport : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        // +++++++++++++++ Page Selector
        //public Guid SelectedPageID { get; set; }
        /// <summary>
        /// integer representing the number of 
        /// days that represent a medium level notice
        /// </summary>
        public int DaysTillMediumNotice { get; set; }
        /// <summary>
        /// integer for the number of 
        /// days that represent a high level notice
        /// </summary>
        public int DaysTillUrgentNotice { get; set; }

        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return WorkflowItemsReport.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Label MessageLabel
        {
            get
            {
                return this.Container.GetControl<Label>("MessageLabel", true);
            }
        }

        // +++++++++++++++ Page Selector
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        //protected virtual HiddenField DetailsPageUrl
        //{
        //    get
        //    {
        //        return this.Container.GetControl<HiddenField>("DetailsPageUrl", true);
        //    }
        //}
        
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HiddenField DaysTillYellow
        {
            get
            {
                return this.Container.GetControl<HiddenField>("DaysTillYellow", true);
            }
        }
        
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HiddenField DaysTillRed
        {
            get
            {
                return this.Container.GetControl<HiddenField>("DaysTillRed", true);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            Label messageLabel = this.MessageLabel;
            if (string.IsNullOrEmpty(this.Message))
            {
                messageLabel.Text = "<h2>Workflow Items Report</h2>";
            }
            else
            {
                messageLabel.Text = this.Message;
            }

            // +++++++++++++++ Page Selector
            //if (SelectedPageID != Guid.Empty)
            //    DetailsPageUrl.Value = GetPageUrl(SelectedPageID);

            if (DaysTillMediumNotice > 0)
                DaysTillYellow.Value = DaysTillMediumNotice.ToString();

            if (DaysTillUrgentNotice > 0)
                DaysTillRed.Value = DaysTillUrgentNotice.ToString();
        }

        // +++++++++++++++ Page Selector
        //public string GetPageUrl(Guid pageID)
        //{
        //    var mgr = PageManager.GetManager();
        //    var pageNode = mgr.GetPageNode(pageID);
        //    string pageUrl = String.Empty;

        //    if (pageNode.GetFullUrl().Substring(0, 2) == "~/")
        //    {
        //        pageUrl = pageNode.GetFullUrl().Substring(2, pageNode.GetFullUrl().Length - 2);
        //    }
        //    else
        //    {
        //        pageUrl = pageNode.GetFullUrl();
        //    }
        //    return pageUrl;
        //}
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Custom/Reports/WorkflowItems/WorkflowItemsReport.ascx";
        #endregion
    }
}

using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.Custom.Reports.WorkflowItems.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.Custom.Reports.WorkflowItems.WorkflowItemsReport"/> widget
    /// </summary>
    public class WorkflowItemsReportDesigner : ControlDesignerBase
    {
        #region Properties
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
                    return WorkflowItemsReportDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        // +++++++++++++++++++++++++ Page Selector
        /// <summary>
        /// Gets the page selector control.
        /// </summary>
        /// <value>The page selector control.</value>
        //protected internal virtual PagesSelector PageSelectorSelectedPageID
        //{
        //    get
        //    {
        //        return this.Container.GetControl<PagesSelector>("pageSelectorSelectedPageID", true);
        //    }
        //}

        /// <summary>
        /// Gets the selector tag.
        /// </summary>
        /// <value>The selector tag.</value>
        //public HtmlGenericControl SelectorTagSelectedPageID
        //{
        //    get
        //    {
        //        return this.Container.GetControl<HtmlGenericControl>("selectorTagSelectedPageID", true);
        //    }
        //}
        // ++++++++++++++++++++++++ End Page Selector

        /// <summary>
        /// Gets the control that is bound to the DaysTillMediumNotice property
        /// </summary>
        protected virtual Control DaysTillMediumNotice
        {
            get
            {
                return this.Container.GetControl<Control>("DaysTillMediumNotice", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the DaysTillUrgentNotice property
        /// </summary>
        protected virtual Control DaysTillUrgentNotice
        {
            get
            {
                return this.Container.GetControl<Control>("DaysTillUrgentNotice", true);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
            // ++++++++++++++++++++++++ Page Selector
            //if (this.PropertyEditor != null)
            //{
            //    var uiCulture = this.PropertyEditor.PropertyValuesCulture;
            //    this.PageSelectorSelectedPageID.UICulture = uiCulture;
            //}
            // ++++++++++++++++++++++++ End Page Selector
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();
            // ++++++++++++++++++++++++ Page Selector
            //descriptor.AddComponentProperty("pageSelectorSelectedPageID", this.PageSelectorSelectedPageID.ClientID);
            //descriptor.AddElementProperty("selectorTagSelectedPageID", this.SelectorTagSelectedPageID.ClientID);
            // ++++++++++++++++++++++++ End Page Selector
            descriptor.AddElementProperty("daysTillMediumNotice", this.DaysTillMediumNotice.ClientID);
            descriptor.AddElementProperty("daysTillUrgentNotice", this.DaysTillUrgentNotice.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(WorkflowItemsReportDesigner.scriptReference));
            return scripts;
        }

        /// <summary>
        /// Gets the required by the control, core library scripts predefined in the <see cref="ScriptRef"/> enum.
        /// </summary>
        protected override ScriptRef GetRequiredCoreScripts()
        {
            return ScriptRef.JQuery | ScriptRef.JQueryUI;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Custom/Reports/WorkflowItems/Designer/WorkflowItemsReportDesigner.ascx";
        public const string scriptReference = "~/Custom/Reports/WorkflowItems/Designer/WorkflowItemsReportDesigner.js";
        #endregion
    }
}
 

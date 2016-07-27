using SitefinityWebApp.Custom.Reports.Helpers.WorkflowItemInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model;
using SitefinityWebApp.Custom.Reports.Helpers;

namespace SitefinityWebApp.Custom.Services.Models
{
    public class WorkflowItemsModel
    {
        public WorkflowItemsModel() { }

        public Guid WorkflowItemId { get; set; }
        public Guid ItemId { get; set; }
        public string DateCreated { get; set; }
        public string ItemType { get; set; }
        public string ItemTitle { get; set; }
        public string ItemCreator { get; set; }
        public string ItemCreatorAvatar { get; set; }
        public string ItemLanguage { get; set; }
        public string ItemURL { get; set; }
        public string DaysAwaitingApproval { get; set; }
        public string WorkflowTitle { get; set; }
        public string WorkflowSteps { get; set; }
        public string WorkflowApproverName { get; set; }
        public string WorkflowApproverEmail { get; set; }
        public string WorkflowApproverType { get; set; }
        public string WorkflowItemStatus { get; set; }

        public WorkflowItemsModel(WorkflowItemInfo workflowItemInfo)
        {
            this.WorkflowItemId = workflowItemInfo.WorkflowItemId;
            this.ItemId = workflowItemInfo.ItemId;
            this.DateCreated = workflowItemInfo.DateCreated;
            this.ItemType = workflowItemInfo.ItemType;
            this.ItemTitle = workflowItemInfo.ItemTitle;
            this.ItemCreator = workflowItemInfo.ItemCreator;
            this.ItemURL = workflowItemInfo.ItemURL;
            this.DaysAwaitingApproval = workflowItemInfo.DaysAwaitingApproval;
            this.WorkflowTitle = workflowItemInfo.WorkflowTitle;
            this.WorkflowSteps = workflowItemInfo.WorkflowSteps;
            this.WorkflowApproverName = workflowItemInfo.WorkflowApproverName;
            this.WorkflowApproverEmail = workflowItemInfo.WorkflowApproverEmail;
            this.WorkflowApproverType = workflowItemInfo.WorkflowApproverType;
            this.WorkflowItemStatus = workflowItemInfo.WorkflowItemStatus;
            this.ItemLanguage = workflowItemInfo.ItemLanguage;
            this.ItemCreatorAvatar = workflowItemInfo.ItemCreatorAvatar;

        }
    }
}
using System;
using System.Linq;

namespace SitefinityWebApp.Custom.Reports.Helpers.WorkflowItemInfo
{
    public class WorkflowItemInfo
    {
        public WorkflowItemInfo()
        {
        }

        /// <summary>
        /// Id of the Workflow 
        /// </summary>
        private Guid workflowItemId;
        public Guid WorkflowItemId
        {
            get
            {
                return workflowItemId;
            }
            set
            {
                workflowItemId = value;
            }
        }

        /// <summary>
        /// Id of the content Item
        /// </summary>
        private Guid itemId;
        public Guid ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }
        
        /// <summary>
        /// Date workflow item was submitted for approval.
        /// </summary>
        private string dateCreated;
        public string DateCreated 
        {
            get
            {
                return dateCreated;
            }
            set
            {
                dateCreated = value;
            }
        }

        /// <summary>
        /// The content type of the workflow item
        /// </summary>
        private string itemType;
        public string ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }

        /// <summary>
        /// The content title of the workflow item 
        /// </summary>
        private string itemTitle;
        public string ItemTitle
        {
            get
            {
                return itemTitle;
            }
            set
            {
                itemTitle = value;
            }
        }

        /// <summary>
        /// Person who create the content item
        /// </summary>
        private string itemCreator;
        public string ItemCreator
        {
            get
            {
                return itemCreator;
            }
            set
            {
                itemCreator = value;
            }
        }

        /// <summary>
        /// Person who create the content item
        /// </summary>
        private string itemCreatorAvatar;
        public string ItemCreatorAvatar
        {
            get
            {
                return itemCreatorAvatar;
            }
            set
            {
                itemCreatorAvatar = value;
            }
        }

        /// <summary>
        /// Person who create the content item
        /// </summary>
        private string itemURL;
        public string ItemURL
        {
            get
            {
                return itemURL;
            }
            set
            {
                itemURL = value;
            }
        }

        /// <summary>
        /// Number of days since submission for approval
        /// </summary>
        private string daysAwaitingApproval;
        public string DaysAwaitingApproval
        {
            get
            {
                return daysAwaitingApproval;
            }
            set
            {
                daysAwaitingApproval = value;
            }
        }
        
        /// <summary>
        /// Associated workflow title
        /// </summary>
        private string workflowTitle;
        public string WorkflowTitle
        {
            get
            {
                return workflowTitle;
            }
            set
            {
                workflowTitle = value;
            }
        }
        
        /// <summary>
        /// Number of steps in workflow process
        /// </summary>
        private string workflowSteps;
        public string WorkflowSteps
        {
            get
            {
                return workflowSteps;
            }
            set
            {
                workflowSteps = value;
            }
        }

        /// <summary>
        /// Workflow Approvers, role and/or username
        /// </summary>
        private string workflowApproverName;
        public string WorkflowApproverName
        {
            get
            {
                return workflowApproverName;
            }
            set
            {
                workflowApproverName = value;
            }
        }

        /// <summary>
        /// Workflow Approvers, role and/or username
        /// </summary>
        private string workflowApproverEmail;
        public string WorkflowApproverEmail
        {
            get
            {
                return workflowApproverEmail;
            }
            set
            {
                workflowApproverEmail = value;
            }
        }

        /// <summary>
        /// Workflow Approvers, role and/or username
        /// </summary>
        private string workflowApproverType;
        public string WorkflowApproverType
        {
            get
            {
                return workflowApproverType;
            }
            set
            {
                workflowApproverType = value;
            }
        }

        /// <summary>
        /// Workflow item's status
        /// </summary>
        private string workflowItemStatus;
        public string WorkflowItemStatus
        {
            get
            {
                return workflowItemStatus;
            }
            set
            {
                workflowItemStatus = value;
            }
        }

        /// <summary>
        /// Workflow item's status
        /// </summary>
        private string itemLanguage;
        public string ItemLanguage
        {
            get
            {
                return itemLanguage;
            }
            set
            {
                itemLanguage = value;
            }
        }
    }
}
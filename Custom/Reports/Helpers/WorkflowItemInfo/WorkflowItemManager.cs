using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Model.ContentLinks;
using System.Globalization;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.GenericContent.Model;

namespace SitefinityWebApp.Custom.Reports.Helpers.WorkflowItemInfo
{
    public static class WorkflowItemManager
    {
        #region members
        private const string BlogPostType = "Telerik.Sitefinity.Blogs.Model.BlogPost";
        private const string ContentBlockType = "Telerik.Sitefinity.GenericContent.Model.ContentItem";
        private const string DocumentType = "Telerik.Sitefinity.Libraries.Model.Document";
        private const string DynamicModuleType = "Telerik.Sitefinity.DynamicTypes.Model";
        private const string ProductType = "sf_ec_prdct";
        private const string EventType = "Telerik.Sitefinity.Events.Model.Event";
        private const string ForumPostType = "Telerik.Sitefinity.Forums.Model.ForumPost";
        private const string ForumThreadType = "Telerik.Sitefinity.Forums.Model.ForumThread";
        private const string ImageType = "Telerik.Sitefinity.Libraries.Model.Image";
        private const string ListType = "Telerik.Sitefinity.Lists.Model.ListItem";
        private const string NewsType = "Telerik.Sitefinity.News.Model.NewsItem";
        private const string PageType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string VideoType = "Telerik.Sitefinity.Libraries.Model.Video";
        private const string SitefinityContent = "/sitefinity/content/";
        private const string AwaitingApprovalState = "AwaitingApproval";
        private const string AwaitingPublishingState = "AwaitingPublishing";
        private const string Approval = "Approval";
        private const string Publishing = "Publishing";
        private const string StandardOneStep = "StandardOneStep";
        private const string StandardTwoStep = "StandardTwoStep";
        private const string OneStep = "One";
        private const string TwoStep = "Two";
        private const string ActionNameAwaitingApprovalState = "approve";
        private const string ActionNameAwaitingPublishingState = "publish";
        private const string EmptyString = "";
        private const string ProviderName = "OpenAccessProvider";
        #endregion members

        /// <summary>
        /// Gets a collection of workflow items
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        public static List<WorkflowItemInfo> GetWorkflowItems(WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            var workflowItemInfoList = new List<WorkflowItemInfo>();

            try
            {
                if (workflowScope.ContentType.Contains(ProductType))
                {
                    GetProductWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                    return workflowItemInfoList;
                }
                else if (workflowScope.ContentType.Contains(DynamicModuleType))
                {
                    GetDynamicModuleWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                    return workflowItemInfoList;
                }
                else
                {
                    switch (workflowScope.ContentType)
                    {
                        case NewsType:
                            GetNewsWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        case EventType:
                            GetEventWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        case BlogPostType:
                            GetBlogPostWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        case ImageType:
                        case VideoType:
                        case DocumentType:
                            GetMediaWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        case PageType:
                            GetPageWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        case ListType:
                            GetListWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                            return workflowItemInfoList;
                        //case ContentBlockType:
                        //    GetContentBlockWorkflowItemsByState(workflowItemInfoList, workflowPermission, workflowScope);
                        //    return workflowItemInfoList;
                        //if (workflowScope.ContentType.Contains(DynamicModuleType))
                        default:
                            break;
                    }
                }
            }
            catch { Exception ex; }
            return workflowItemInfoList;
        }

        #region workflow methods

        /// <summary>
        /// Gets a collection of news items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetNewsWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList, 
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            NewsManager newsManager = NewsManager.GetManager();

            try
            {
                foreach (var newsItem in newsManager.GetNewsItems()
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower()) &&
                        i.Status == ContentLifecycleStatus.Master).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, newsItem.Id,
                        newsItem.DateCreated, newsItem.Title, "", newsItem.ApprovalWorkflowState,
                        newsItem.Content.CurrentLanguage.ToString(), newsItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of event items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetEventWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList, 
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            EventsManager eventsManager = EventsManager.GetManager();

            try
            {
                foreach (var eventsItem in eventsManager.GetEvents()
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, eventsItem.Id,
                        eventsItem.DateCreated, eventsItem.Title, "", eventsItem.ApprovalWorkflowState,
                        eventsItem.Content.CurrentLanguage.ToString(), eventsItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of dynamic module items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetDynamicModuleWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            DynamicModuleManager dynamicModuleManager = null;
            var provider = new MultisiteContext().CurrentSite.GetDefaultProvider(workflowScope.ContentType);
            
            if (provider != null)
                dynamicModuleManager = DynamicModuleManager.GetManager(provider.ProviderName);
            else
                dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName);

            Type dynamicContentType = TypeResolutionService.ResolveType(workflowScope.ContentType);

            try
            {
                foreach (var dynamicItem in dynamicModuleManager.GetDataItems(dynamicContentType)
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, dynamicItem.Id,
                        dynamicItem.DateCreated, dynamicItem.GetValue<Lstring>("Title"), EmptyString,
                        dynamicItem.ApprovalWorkflowState, dynamicItem.UrlName.CurrentLanguage.ToString(), dynamicItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of product items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetProductWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            CatalogManager catalogManager = CatalogManager.GetManager();

            var itemClrType = TypeResolutionService.ResolveType(workflowScope.ContentType);

            try
            {
                foreach (var product in catalogManager.GetProducts(itemClrType.FullName)
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, product.Id,
                        product.DateCreated, product.Title, EmptyString,
                        product.ApprovalWorkflowState, product.UrlName.CurrentLanguage.ToString(), product.Owner));
                }
            }
            catch { Exception ex; }
        }
        
        /// <summary>
        /// Gets a collection of blog post items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetBlogPostWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            BlogsManager blogsManager = BlogsManager.GetManager();

            try
            {
                foreach (var blogItem in blogsManager.GetBlogPosts()
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, blogItem.Id,
                        blogItem.DateCreated, blogItem.Title, blogItem.Parent.ItemDefaultUrl, blogItem.ApprovalWorkflowState,
                        blogItem.Content.CurrentLanguage.ToString(), blogItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of list items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetListWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            ListsManager listsManager = ListsManager.GetManager();

            try
            {
                foreach (var listItem in listsManager.GetListItems()
                    .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, listItem.Id,
                        listItem.DateCreated, listItem.Title, listItem.Parent.UrlName, listItem.ApprovalWorkflowState,
                        listItem.Content.CurrentLanguage.ToString(), listItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of pages that are in an awaiting
        /// approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowItemInfoList"></param>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        private static void GetPageWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            PageManager pageManager = PageManager.GetManager();

            try
            {
                foreach (var pageItem in pageManager.GetPageDataList()
                    .Where(i => i.NavigationNode.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                {
                    workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, pageItem.Id,
                        pageItem.DateCreated, pageItem.NavigationNode.Title, EmptyString, pageItem.NavigationNode.ApprovalWorkflowState,
                        pageItem.Culture, pageItem.Owner));
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Gets a collection of content blocks that are in 
        /// an awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowItemInfoList"></param>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        //private static void GetContentBlockWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
        //    WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        //{
        //    ContentManager contentManager = ContentManager.GetManager();

        //    try
        //    {
        //        foreach (var contentBlockItem in contentManager.GetContent()
        //            .Where(i => i.Status.ToString() == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
        //        {
        //            workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, contentBlockItem.Id,
        //                contentBlockItem.DateCreated, contentBlockItem.Title, EmptyString, contentBlockItem.Status.ToString(),
        //                "", contentBlockItem.Id));
        //        }
        //    }
        //    catch { Exception ex; }
        //}

        /// <summary>
        /// Gets a collection of media items that are in an 
        /// awaiting approval or awaiting publishing state
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <returns></returns>
        private static void GetMediaWorkflowItemsByState(List<WorkflowItemInfo> workflowItemInfoList,
            WorkflowPermission workflowPermission, WorkflowScope workflowScope)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            try
            {
                switch (workflowScope.ContentType)
                {
                    case ImageType:
                        foreach (var imageItem in librariesManager.GetImages()
                            .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                        {
                            workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, imageItem.Id,
                                imageItem.DateCreated, imageItem.Title, imageItem.Album.ItemDefaultUrl, imageItem.ApprovalWorkflowState,
                                EmptyString, imageItem.Owner));
                        }
                        break;
                    case VideoType:
                        foreach (var videoItem in librariesManager.GetVideos()
                            .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                        {
                            workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, videoItem.Id,
                                videoItem.DateCreated, videoItem.Title, videoItem.Library.ItemDefaultUrl, videoItem.ApprovalWorkflowState,
                                EmptyString, videoItem.Owner));
                        }
                        break;
                    case DocumentType:
                        foreach (var documentItem in librariesManager.GetDocuments()
                            .Where(i => i.ApprovalWorkflowState == SetWorkflowState(workflowPermission.ActionName.ToLower())).ToList())
                        {
                            workflowItemInfoList.Add(SetWorkflowItemProperties(workflowPermission, workflowScope, documentItem.Id,
                                documentItem.DateCreated, documentItem.Title, documentItem.Library.ItemDefaultUrl, documentItem.ApprovalWorkflowState,
                                EmptyString, documentItem.Owner));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Utility method to set the properties for all Workflow Items
        /// </summary>
        /// <param name="workflowPermission"></param>
        /// <param name="workflowScope"></param>
        /// <param name="itemId"></param>
        /// <param name="dateCreated"></param>268f8745-3b5f-425f-a206-3731adacea20 
        /// <param name="itemTitle"></param>
        /// <param name="itemWorkflowStatus"></param>
        /// <param name="itemLanguage"></param>
        /// <param name="itemOwnerId"></param>
        /// <returns></returns>
        private static WorkflowItemInfo SetWorkflowItemProperties(WorkflowPermission workflowPermission,
            WorkflowScope workflowScope, Guid itemId, DateTime dateCreated, string itemTitle,
            string itemUrlName, string itemWorkflowStatus, string itemLanguage, Guid itemOwnerId)
        {
            var workflowItemInfo = new WorkflowItemInfo();
            try
            {
                workflowItemInfo.ItemId = itemId;
                workflowItemInfo.WorkflowItemId = workflowPermission.Definition.Id; 
                workflowItemInfo.DateCreated = dateCreated.ToShortDateString();
                workflowItemInfo.DaysAwaitingApproval = GetDaysSinceItemSubmission(DateTime.Now.Subtract(dateCreated));
                workflowItemInfo.ItemType = GetItemTypeFriendName(workflowScope.ContentType);
                workflowItemInfo.ItemTitle = itemTitle;
                workflowItemInfo.ItemLanguage = itemLanguage;
                workflowItemInfo.ItemURL = GetItemBackendUrl(workflowScope.ContentType, itemUrlName);

                SitefinityProfile profile = GetSubmitterProfile(itemOwnerId);
                if (profile != null)
                {
                    workflowItemInfo.ItemCreator = SetSubmitterUserName(profile, true);
                    workflowItemInfo.ItemCreatorAvatar = SetSubmitterAvatar(profile);
                    workflowItemInfo.WorkflowApproverEmail = SetSubmitterEmail(profile);
                }

                workflowItemInfo.WorkflowItemStatus = GetItemWorkflowStatusFriendlyName(itemWorkflowStatus);
                workflowItemInfo.WorkflowTitle = workflowScope.Definition.Title;
                workflowItemInfo.WorkflowSteps = SetWorkflowSteps(workflowScope.Definition.WorkflowType.ToString());
                workflowItemInfo.WorkflowApproverName = workflowPermission.PrincipalName;
                workflowItemInfo.WorkflowApproverType = workflowPermission.PrincipalType.ToString();
            }
            catch { Exception ex; }

            return workflowItemInfo;
        }

        /// <summary>
        /// Sets the workflow state to filter items by
        /// </summary>
        /// <param name="workflowState"></param>
        /// <returns></returns>
        private static string SetWorkflowState(string workflowState)
        {
            if (workflowState == ActionNameAwaitingApprovalState)
                return AwaitingApprovalState;
            else if (workflowState == ActionNameAwaitingPublishingState)
                return AwaitingPublishingState;
            else 
                return workflowState;
        }

        /// <summary>
        /// Sets the workflow state to filter items by
        /// </summary>
        /// <param name="workflowState"></param>
        /// <returns></returns>
        private static string SetWorkflowSteps(string workflowSteps)
        {
            if (workflowSteps == StandardOneStep)
                return OneStep;
            else if (workflowSteps == StandardTwoStep)
                return TwoStep;
            else
                return workflowSteps;
        }

        /// <summary>
        /// Gets the report friendly name of the item's workflow state
        /// </summary>
        /// <param name="workflowStatus"></param>
        /// <returns></returns>
        private static string GetItemWorkflowStatusFriendlyName(string workflowStatus)
        {
            if (workflowStatus.ToLower().Contains(Approval.ToLower()))
                return Approval;
            else if (workflowStatus.ToLower().Contains(Publishing.ToLower()))
                return Publishing;
            else
                return workflowStatus;
        }

        #endregion workflow methods

        #region submitter profile methods
        /// <summary>
        /// Returns the item submitter's profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static SitefinityProfile GetSubmitterProfile(Guid userId)
        {
            UserProfileManager profileManager = UserProfileManager.GetManager();
            UserManager userManager = UserManager.GetManager();
            User user = userManager.GetUser(userId);
            SitefinityProfile profile = null;

            try
            {    
                if (user != null)
                {
                    profile = profileManager.GetUserProfile<SitefinityProfile>(user);
                }

            }
            catch { }
            return profile;
        }

        /// <summary>
        /// Gets either the item submitter's full name or nickname
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="isFullName"></param>
        /// <returns></returns>
        private static string SetSubmitterUserName(SitefinityProfile profile, bool isFullName)
        {
            if (isFullName)
                return profile.FirstName + " " + profile.LastName;
            else
                return profile.Nickname;
        }

        /// <summary>
        /// Get the profile's avatar image
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private static string SetSubmitterAvatar(SitefinityProfile profile)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            ContentLink avatarLink = profile.Avatar;
            var imageId = avatarLink.ChildItemId;
            Image avatar = librariesManager.GetImage(imageId);

            if (avatar != null)
                return avatar.Url;
            else
                return "";
        }

        /// <summary>
        /// Get the profile's avatar image
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private static string SetSubmitterEmail(SitefinityProfile profile)
        {
            if (profile != null)
                return profile.User.Email;
            else
                return "";
        }

        #endregion submitter profile methods

        #region item info methods

        /// <summary>
        /// Gets the number of days an item has been in an awaiting approval or awaiting publish state
        /// </summary>
        /// <param name="submittedDate"></param>
        /// <returns></returns>
        private static string GetDaysSinceItemSubmission(TimeSpan submittedDate)
        {
            return submittedDate.TotalDays.ToString("F0", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the item's backend Url
        /// TODO: get the item's backend url... :/
        /// TODO: add things like forum thread and blog post titles
        /// </summary>
        /// <param name="newsItem"></param>
        /// <returns>string ItemURL</returns>
        private static string GetItemBackendUrl(string itemType, string itemUrlHelper)
        {
            if (itemType.Contains(ProductType))
            {
                return "/Sitefinity/Ecommerce/Products";
            }
            else if (itemType.Contains(DynamicModuleType))
            {
                return SitefinityContent + GetDynamicModuleFriendlyName(itemType, 2);
            }
            else
            {
                switch (itemType)
                {
                    case BlogPostType:
                        return SitefinityContent + "Blogs/BlogPosts" + itemUrlHelper;
                    case DocumentType:
                        return SitefinityContent + "Documents/LibraryDocuments" + itemUrlHelper;
                    case EventType:
                        return SitefinityContent + "Events";
                    case ImageType:
                        return SitefinityContent + "Images/LibraryImages" + itemUrlHelper;
                    case ListType:
                        return SitefinityContent + "Lists/ListItems/" + itemUrlHelper;
                    case NewsType:
                        return SitefinityContent + "News";
                    case PageType:
                        return "/Sitefinity/Pages";
                    case VideoType:
                        return SitefinityContent + "Videos/LibraryVideos" + itemUrlHelper;
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// Returns a friendly name for various content types
        /// </summary>
        /// <param name="fullTypeName"></param>
        /// <returns></returns>
        private static string GetItemTypeFriendName(string contentType)
        {
            if (contentType.Contains(ProductType))
            {
                return "Product";
            }
            else if (contentType.Contains(DynamicModuleType))
            {
                return GetDynamicModuleFriendlyName(contentType, 1);
            }
            else
            {
                switch (contentType)
                {
                    case BlogPostType:
                        return "Blog Post";
                    case ContentBlockType:
                        return "Content Item";
                    case DocumentType:
                        return "Document";
                    case EventType:
                        return "Event";
                    case ForumPostType:
                        return "Forum Post";
                    case ForumThreadType:
                        return "Forum Thread";
                    case ImageType:
                        return "Image";
                    case ListType:
                        return "List Item";
                    case NewsType:
                        return "News";
                    case PageType:
                        return "Page";
                    case VideoType:
                        return "Video";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// Gets the dynamic module's friendly name 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetDynamicModuleFriendlyName(string contentType, int index)
        {
            char[] delimiterChars = { '.' };
            string[] nameParts = contentType.Split(delimiterChars);

            if (nameParts.Count() > 0)
            {
                return nameParts[nameParts.Count() - index];
            }
            else
                return "";
        }
        #endregion item info methods
    }
}
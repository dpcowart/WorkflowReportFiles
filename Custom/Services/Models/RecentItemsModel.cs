using System;
using System.Linq;
using System.Runtime.Serialization;
using SitefinityWebApp.Custom.Reports.Helpers;
using Telerik.Sitefinity.Dashboard.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security;
using SitefinityWebApp.Custom.Reports.Helpers.ItemsInfo;

namespace SitefinityWebApp.Custom.Services.Models
{
    [DataContractAttribute]
    public class RecentItemsModel
    {
        [DataMemberAttribute]
        public Guid Id { get; set; }
        [DataMemberAttribute]
        public Guid UserId { get; set; }
        [DataMemberAttribute]
        public string UserFullName { get; set; }
        [DataMemberAttribute]
        public string UserName { get; set; }
        [DataMemberAttribute]
        public string Title { get; set; }
        [DataMemberAttribute]
        public string ItemTypeFull { get; set; }
        [DataMemberAttribute]
        public string ItemTypeFriendly { get; set; }
        [DataMemberAttribute]
        public string ItemUrl { get; set; }
        [DataMemberAttribute]
        public string Language { get; set; }
        [DataMemberAttribute]
        public string Status { get; set; }
        [DataMemberAttribute]
        public string LastModified { get; set; }
        [DataMemberAttribute]
        public string DateCreated { get; set; }
        [DataMemberAttribute]
        public string Provider { get; set; }
        
        public RecentItemsModel(DashboardLogEntry item)
        {
            ItemInfo itemInfo = new ItemInfo();
            this.Id = new Guid(item.ItemId);
            this.UserId = item.UserId;
            this.UserFullName = GetUserName(UserId, true);
            this.UserName = GetUserName(UserId, false);
            this.Title = item.Title;
            this.ItemTypeFull = item.ItemType; // This is the full item type ex. Telerik.Sitefinity.GenericContent.Model.ContentItem
            itemInfo = ItemManager.GetItemInfo(item);
            this.ItemTypeFriendly = itemInfo.ItemTypeFriendly; //This is the type friendly name for reports: ex. Content Item
            this.LastModified = itemInfo.LastModified;
            this.DateCreated = itemInfo.DateCreated;
            this.ItemUrl = itemInfo.ItemUrl;
            this.Language = item.Language;
            this.Status = item.Status;
            this.Provider = item.ItemProvider;
        }

        private string GetUserName(Guid userId, bool isFullName)
        {
            UserProfileManager profileManager = UserProfileManager.GetManager();
            UserManager userManager = UserManager.GetManager();
            string name = String.Empty;
            User user = userManager.GetUser(userId);

            try
            {
                if (isFullName)
                {
                    SitefinityProfile profile = null;
                    if (user != null)
                    {
                        profile = profileManager.GetUserProfile<SitefinityProfile>(user);
                        name = profile.FirstName + " " + profile.LastName;
                    }
                }
                else
                    name = user.UserName;
            }
            catch { }
            return name;
        }
    }
}
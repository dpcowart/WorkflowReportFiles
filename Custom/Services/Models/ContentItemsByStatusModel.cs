using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Dashboard.Data;
using Telerik.Sitefinity.Dashboard.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security;
using System.Runtime.Serialization;

namespace SitefinityWebApp.Custom.Services.Models
{
    [DataContractAttribute]
    public class ContentItemsByStatusModel
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
        public string ItemName { get; set; }
        [DataMemberAttribute]
        public string ItemType { get; set; }
        [DataMemberAttribute]
        public string Language { get; set; }
        [DataMemberAttribute]
        public string Status { get; set; }
        [DataMemberAttribute]
        public string Timestamp { get; set; }

        //List<ItemCounts> myCounts { get; set; }

        public ContentItemsByStatusModel(DashboardLogEntry item)
        {
            this.Id = item.Id;
            this.UserId = item.UserId;
            this.UserFullName = GetUserName(UserId, true);
            this.UserName = GetUserName(UserId, false);
            this.Title = item.Title;
            this.ItemName = item.ItemType.Substring(item.ItemType.LastIndexOf('.') + 1);
            this.ItemType = item.ItemType;
            this.Language = item.Language;
            this.Status = item.Status;
            this.Timestamp = item.Timestamp.ToShortDateString();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SitefinityWebApp.Custom.Services.Models;
using Telerik.Sitefinity.Dashboard.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security;

namespace SitefinityWebApp.Custom.Services.Api.Controllers
{
    public class RecentItemsController : ApiController
    {
        private DashboardManager _manager = null;

        public class ItemCounts
        {
            public string FullName = String.Empty;
            public string UserName = String.Empty;
            public string ContentType = String.Empty;
            public int ContentCount = 0;
        }

        public DashboardManager Manager
        {
            get
            {
                return _manager ??
                    (_manager = DashboardManager.GetManager());
            }
        }

        // GET api/<controller>
        public IEnumerable<ItemCounts> Get()
        {
            var myCollection = new List<RecentItemsModel>();
            var myCounts = new List<ItemCounts>();
            try
            {
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-1))
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }

            myCounts.AddRange(SetCountsByUserAndItemType(myCollection));
            return myCounts;
        }

        // GET api/<controller>/5
        public IEnumerable<RecentItemsModel> Get(string id)
        {
            var myCollection = new List<RecentItemsModel>();
            UserManager userManager = UserManager.GetManager();
            User user = userManager.GetUser(id);

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3) && r.UserId == user.Id)
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }

            return myCollection;
        }

        // GET api/<controller>/5?type=published
        public IEnumerable<RecentItemsModel> Get(string id, string status)
        {
            var myCollection = new List<RecentItemsModel>();
            User user = GetUser(id);

            if (String.IsNullOrEmpty(status))
                status = "";

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3) &&
                        r.UserId == user.Id && r.Status == status)
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }

            return myCollection;
        }

        public IEnumerable<RecentItemsModel> Get(string id, string type, string status)
        {
            List<RecentItemsModel> myCollection = new List<RecentItemsModel>();

            try
            {
                if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(type) && String.IsNullOrEmpty(status))
                    myCollection = GetItemsCollection();
                else if (!String.IsNullOrEmpty(id) && String.IsNullOrEmpty(type) && String.IsNullOrEmpty(status))
                    myCollection = GetItemsCollectionByUser(id);
                else if (!String.IsNullOrEmpty(id) && String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(status))
                    myCollection = GetItemsCollectionByUserByStatus(id, status);
                else if (String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(type) && String.IsNullOrEmpty(status))
                    myCollection = GetItemsCollectionByType(type);
            }
            catch { Exception ex; }

            return myCollection;
        }

        private IEnumerable<ItemCounts> SetCountsByUserAndItemType(List<RecentItemsModel> items)
        {
            var myCounts = new List<ItemCounts>();
            int itemTypeCount = 0;
            Guid userId = new Guid();
            string itemType = String.Empty;
            string userFullName = String.Empty;
            string userName = String.Empty;
            ItemCounts currentItem = null;
            foreach (var item in items)
            {
                currentItem = new ItemCounts();

                //initial loop - nothing set yet
                if (userId == Guid.Empty && String.IsNullOrEmpty(itemType))
                {
                    userId = item.UserId;
                    itemType = item.ItemTypeFriendly;
                    userFullName = item.UserFullName;
                    itemTypeCount = 1;
                    userName = item.UserName;
                }
                //same user and same item type, increment count
                else if (userId == item.UserId && itemType == item.ItemTypeFriendly)
                {
                    itemTypeCount = itemTypeCount + 1;
                }
                //new user or new content type, add to collection and reset values
                else if (userId != item.UserId || itemType != item.ItemTypeFriendly)
                {
                    currentItem.FullName = userFullName;
                    currentItem.UserName = userName;
                    currentItem.ContentType = itemType;
                    currentItem.ContentCount = itemTypeCount;
                    myCounts.Add(currentItem);

                    //reset values
                    userId = item.UserId;
                    itemType = item.ItemTypeFriendly;
                    userFullName = item.UserFullName;
                    userName = item.UserName;
                    itemTypeCount = 1;
                }
            }

            currentItem = new ItemCounts();
            currentItem.FullName = userFullName;
            currentItem.ContentType = itemType;
            currentItem.ContentCount = itemTypeCount;
            myCounts.Add(currentItem);

            return myCounts;
        }

        /// <summary>
        /// Get the user object by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUser(string username)
        {
            UserManager userManager = UserManager.GetManager();

            User user = userManager.GetUser(username);

            return user;
        }

        /// <summary>
        /// Get a full list of content items with no filter
        /// </summary>
        /// <returns></returns>
        private List<RecentItemsModel> GetItemsCollection()
        {
            var myCollection = new List<RecentItemsModel>();

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3))
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }
            return myCollection;
        }

        /// <summary>
        /// Get a full list of content items by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<RecentItemsModel> GetItemsCollectionByUser(string id)
        {
            User user = GetUser(id);
            var myCollection = new List<RecentItemsModel>();

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3) &&
                        r.UserId == user.Id)
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }
            return myCollection;
        }

        /// <summary>
        /// Get a full list of content items by Content Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<RecentItemsModel> GetItemsCollectionByType(string type)
        {
            var myCollection = new List<RecentItemsModel>();

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3) &&
                        r.ItemType == type)
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }
            return myCollection;
        }

        /// <summary>
        /// Get a full list of content items by User Id and Content Item Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private List<RecentItemsModel> GetItemsCollectionByUserByStatus(string id, string status)
        {
            User user = GetUser(id);
            var myCollection = new List<RecentItemsModel>();

            try
            {
                //r => r.Timestamp > DateTime.UtcNow.AddMonths(-12) &&
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-3) &&
                        r.UserId == user.Id && r.Status == status)
                    .OrderByDescending(r => r.UserId)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new RecentItemsModel(r)));
            }
            catch { Exception ex; }
            return myCollection;
        }
    }
}
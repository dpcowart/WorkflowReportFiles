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
    public class ContentItemsByStatusController : ApiController
    {
        private DashboardManager _manager = null;
        
        public class ItemCounts
        {
            public string FullName = String.Empty;
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
        public IEnumerable<ContentItemsByStatusModel> Get()
        {
            var myCollection = new List<ContentItemsByStatusModel>();
            try
            {
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-1))
                    .OrderByDescending(r => r.ItemType)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new ContentItemsByStatusModel(r)));
            }
            catch { Exception ex; }
            return myCollection;
        }

        // GET api/<controller>/5
        public IEnumerable<ContentItemsByStatusModel> Get(string id)
        {
            var myCollection = new List<ContentItemsByStatusModel>();
            UserManager userManager = UserManager.GetManager();
            User user = userManager.GetUser(id);

            try
            {
                this.Manager.GetDashboardLogEntries()
                    .Where(r => r.Timestamp > DateTime.UtcNow.AddMonths(-1) &&
                        r.ItemType == id)
                    .OrderByDescending(r => r.ItemType)
                    .ThenBy(r => r.ItemType)
                    .ToList()
                    .ForEach(r => myCollection.Add(new ContentItemsByStatusModel(r)));
            }
            catch { Exception ex; }

            return myCollection;
        }
    }
}
using System;
using System.Linq;

namespace SitefinityWebApp.Custom.Reports.Helpers
{
    public class ItemInfo
    {
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

        private string itemUrl;
        public string ItemUrl
        {
            get
            {
                return itemUrl;
            }
            set
            {
                itemUrl = value;
            }
        }

        private string itemTypeFriendly;
        public string ItemTypeFriendly
        {
            get
            {
                return itemTypeFriendly;
            }
            set
            {
                itemTypeFriendly = value;
            }
        }

        private string lastModified;
        public string LastModified
        {
            get
            {
                return lastModified;
            }
            set
            {
                lastModified = value;
            }
        }

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

        private string provider;
        public string Provider
        {
            get
            {
                return provider;
            }
            set
            {
                provider = value;
            }
        }
    }
}
using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Dashboard.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Forums.Model;
using Telerik.Sitefinity.Forums;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.DynamicModules.Model;
using System.Globalization;
//using Telerik.Sitefinity.DynamicModules.Builder;
//using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace SitefinityWebApp.Custom.Reports.Helpers
{
    public static class ItemManager
    {
        private const string BlogPostType = "Telerik.Sitefinity.Blogs.Model.BlogPost";
        private const string ContentItemPostType = "Telerik.Sitefinity.GenericContent.Model.ContentItem";
        private const string DocumentType = "Telerik.Sitefinity.Libraries.Model.Document";
        private const string DynamicModuleType = "Telerik.Sitefinity.DynamicTypes.Model";
        private const string EventType = "Telerik.Sitefinity.Events.Model.Event";
        private const string ForumPostType = "Telerik.Sitefinity.Forums.Model.ForumPost";
        private const string ForumThreadType = "Telerik.Sitefinity.Forums.Model.ForumThread";
        private const string ImageType = "Telerik.Sitefinity.Libraries.Model.Image";
        private const string ListType = "Telerik.Sitefinity.Lists.Model.ListItem";
        private const string NewsType = "Telerik.Sitefinity.News.Model.NewsItem";
        private const string PageType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string VideoType = "Telerik.Sitefinity.Libraries.Model.Video";
        private const string SitefinityContent = "/sitefinity/content/";

        public static ItemInfo GetItemInfo(DashboardLogEntry item)
        {
            ItemInfo itemInfo = new ItemInfo();

            itemInfo.Provider = item.ItemProvider;
            GetItemTypeFriendName(item, itemInfo);
            GetContentItemUrl(item, itemInfo);

            return itemInfo;
        }

        /// <summary>
        /// Returns a friendly name for various content types
        /// </summary>
        /// <param name="fullTypeName"></param>
        /// <returns></returns>
        private static void GetItemTypeFriendName(DashboardLogEntry item, ItemInfo itemInfo)
        {

            switch (item.ItemType)
            {
                case BlogPostType:
                    itemInfo.ItemTypeFriendly = "Blog Post";
                    break;
                case ContentItemPostType:
                    itemInfo.ItemTypeFriendly = "Content Item";
                    break;
                case DocumentType:
                    itemInfo.ItemTypeFriendly = "Library Document";
                    break;
                case EventType:
                    itemInfo.ItemTypeFriendly = "Event Item";
                    break;
                case ForumPostType:
                    itemInfo.ItemTypeFriendly = "Forum Post";
                    break;
                case ForumThreadType:
                    itemInfo.ItemTypeFriendly = "Forum Thread";
                    break;
                case ImageType:
                    itemInfo.ItemTypeFriendly = "Library Image";
                    break;
                case ListType:
                    itemInfo.ItemTypeFriendly = "List Item";
                    break;
                case NewsType:
                    itemInfo.ItemTypeFriendly = "News Item";
                    break;
                case PageType:
                    itemInfo.ItemTypeFriendly = "Page Node";
                    break;
                case VideoType:
                    itemInfo.ItemTypeFriendly = "Library Video";
                    break;
                default:
                    itemInfo.ItemTypeFriendly = GetTypeFriendlyName(item);
                    break;
            }
        }

        private static string GetTypeFriendlyName(DashboardLogEntry item)
        {
            char[] delimiterChars = { '.' };
            string[] nameParts = item.ItemType.Split(delimiterChars);

            if (nameParts.Count() > 0)
            {
                if (item.ItemType.Contains(DynamicModuleType))
                {
                    return nameParts[nameParts.Count() - 2];
                }
                else
                {
                    return nameParts[nameParts.Count() - 1];
                }
            }
            else
                return "";
        }

        private static void GetContentItemUrl(DashboardLogEntry item, ItemInfo itemInfo)
        {
            if (item.ItemType.Contains(DynamicModuleType))
                GetDynamicContentItemById(item, itemInfo);
            else
            {
                switch (item.ItemType)
                {
                    case BlogPostType:
                        GetBlogPostById(item, itemInfo);
                        break;
                    case ContentItemPostType:
                        GetGenericContentItemById(item, itemInfo);
                        break;
                    case DocumentType:
                        GetDocumentById(item, itemInfo);
                        break;
                    case EventType:
                        GetEventById(item, itemInfo);
                        break;
                    case ForumPostType:
                        GetForumPostById(item, itemInfo);
                        break;
                    case ForumThreadType:
                        GetForumThreadById(item, itemInfo);
                        break;
                    case ImageType:
                        GetImageById(item, itemInfo);
                        break;
                    case ListType:
                        GetListItemById(item, itemInfo);
                        break;
                    case NewsType:
                        GetNewsItemById(item, itemInfo);
                        break;
                    case PageType:
                        FindPageNodeById(item, itemInfo);
                        break;
                    case VideoType:
                        GetVideoById(item, itemInfo);
                        break;
                    default:
                        itemInfo.DateCreated = "";
                        itemInfo.LastModified = "";
                        itemInfo.ItemUrl = "";
                        break;
                }
            }
        }

        /// <summary>
        /// Get Blogpost Url by Id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private static void GetBlogPostById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    BlogsManager blogsManager = BlogsManager.GetManager();

                    //Get the master version.
                    BlogPost blogPost = blogsManager.GetBlogPosts().Where(b => b.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (blogPost != null)
                    {
                        //Get the live version.
                        blogPost = blogsManager.Lifecycle.GetLive(blogPost) as BlogPost;
                    }

                    var contentLocationService = SystemManager.GetContentLocationService();
                    var contentLocation = contentLocationService.GetItemDefaultLocation(blogPost);

                    itemInfo.DateCreated = blogPost.DateCreated.ToShortDateString();
                    itemInfo.LastModified = blogPost.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = contentLocation == null ? "" : contentLocation.ItemAbsoluteUrl;
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Get the page node url
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private static void FindPageNodeById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    PageManager pageManager = PageManager.GetManager();
                    PageNode node = pageManager.GetPageNodes().Where(n => n.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    itemInfo.DateCreated = node.DateCreated.ToShortDateString();
                    itemInfo.LastModified = node.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = RouteHelper.GetAbsoluteUrl(node.GetFullUrl());
                }
            }
            catch { Exception ex; }
        }

        /// <summary>
        /// Get the generic content item url
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private static void GetGenericContentItemById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    ContentManager manager = ContentManager.GetManager();
                    ContentItem contentItem = manager.GetContent().Where(cI => cI.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    itemInfo.DateCreated = contentItem.DateCreated.ToShortDateString();
                    itemInfo.LastModified = contentItem.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = SitefinityContent + "Content%20blocks";
                }
            }
            catch { Exception ex; }
        }

        private static void GetDocumentById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    LibrariesManager librariesManager = LibrariesManager.GetManager();
                    Document document = librariesManager.GetDocuments().Where(d => d.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (document != null)
                    {
                        document = librariesManager.Lifecycle.GetLive(document) as Document;
                    }

                    itemInfo.DateCreated = document.DateCreated.ToShortDateString();
                    itemInfo.LastModified = document.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = document.Url;
                }
            }
            catch { Exception ex; }
        }

        private static void GetImageById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    LibrariesManager librariesManager = LibrariesManager.GetManager();
                    Image image = librariesManager.GetImages().Where(i => i.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (image != null)
                    {
                        image = librariesManager.Lifecycle.GetLive(image) as Image;
                    }

                    itemInfo.DateCreated = image.DateCreated.ToShortDateString();
                    itemInfo.LastModified = image.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = image.Url;
                }
            }
            catch { Exception ex; }
        }

        private static void GetVideoById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    LibrariesManager librariesManager = LibrariesManager.GetManager();
                    Video video = librariesManager.GetVideos().Where(d => d.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (video != null)
                    {
                        video = librariesManager.Lifecycle.GetLive(video) as Video;
                    }

                    itemInfo.DateCreated = video.DateCreated.ToShortDateString();
                    itemInfo.LastModified = video.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = video.Url;
                }
            }
            catch { Exception ex; }
        }

        private static void GetEventById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    EventsManager eventsManager = EventsManager.GetManager();
                    Event eventItem = eventsManager.GetEvents().Where(ev => ev.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (eventItem != null)
                    {
                        eventItem = eventsManager.Lifecycle.GetLive(eventItem) as Event;
                    }

                    var contentLocationService = SystemManager.GetContentLocationService();
                    var contentLocation = contentLocationService.GetItemDefaultLocation(eventItem);

                    itemInfo.DateCreated = eventItem.DateCreated.ToShortDateString();
                    itemInfo.LastModified = eventItem.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = contentLocation == null ? "" : contentLocation.ItemAbsoluteUrl;
                }
            }
            catch { Exception ex; }
        }

        private static void GetForumPostById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    ForumsManager forumsManager = ForumsManager.GetManager();

                    ForumPost post = forumsManager.GetPost(CreateGuid(item.ItemId));

                    var contentLocationService = SystemManager.GetContentLocationService();
                    var contentLocation = contentLocationService.GetItemDefaultLocation(post);

                    itemInfo.DateCreated = post.DateCreated.ToShortDateString();
                    itemInfo.LastModified = post.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = contentLocation == null ? "" : contentLocation.ItemAbsoluteUrl;
                }
            }
            catch { Exception ex; }
        }

        private static void GetForumThreadById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    ForumsManager forumsManager = ForumsManager.GetManager();

                    ForumThread thread = forumsManager.GetThread(CreateGuid(item.ItemId));

                    var contentLocationService = SystemManager.GetContentLocationService();
                    var contentLocation = contentLocationService.GetItemDefaultLocation(thread);

                    itemInfo.DateCreated = thread.DateCreated.ToShortDateString();
                    itemInfo.LastModified = thread.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = contentLocation == null ? "" : contentLocation.ItemAbsoluteUrl;
                }
            }
            catch { Exception ex; }
        }

        private static void GetListItemById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    ListsManager listsManager = ListsManager.GetManager();

                    ListItem listItem = listsManager.GetListItems().Where(i => i.Id == CreateGuid(item.ItemId)).FirstOrDefault();

                    if (listItem != null)
                    {
                        listItem = listsManager.Lifecycle.GetLive(listItem) as ListItem;
                    }

                    itemInfo.DateCreated = listItem.DateCreated.ToShortDateString();
                    itemInfo.LastModified = listItem.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = SitefinityContent + "lists/listItems/" + listItem.Parent.UrlName + "/?provider" + listItem.Provider + "&lang=";
                }
            }
            catch { Exception ex; }
        }

        private static void GetNewsItemById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    NewsManager newsManager = NewsManager.GetManager();

                    NewsItem newsItem = newsManager.GetNewsItems().Where(ni => ni.Id == CreateGuid(item.ItemId)).FirstOrDefault();
                    if (newsItem != null)
                    {
                        newsItem = newsManager.Lifecycle.GetLive(newsItem) as NewsItem;
                    }

                    var contentLocationService = SystemManager.GetContentLocationService();
                    var contentLocation = contentLocationService.GetItemDefaultLocation(newsItem);
                    itemInfo.ItemUrl = contentLocation == null ? "" : contentLocation.ItemAbsoluteUrl;

                    itemInfo.DateCreated = newsItem.DateCreated.ToShortDateString();
                    itemInfo.LastModified = newsItem.LastModified.ToShortDateString();
                }
            }
            catch { Exception ex; }
        }

        private static void GetDynamicContentItemById(DashboardLogEntry item, ItemInfo itemInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(item.ItemId))
                {
                    var providerName = item.ItemProvider;
                    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
                    Type dynamicContentType = TypeResolutionService.ResolveType(item.ItemType);

                    DynamicContent dynamicContentItem = dynamicModuleManager.GetDataItem(dynamicContentType, CreateGuid(item.ItemId));

                    itemInfo.DateCreated = dynamicContentItem.DateCreated.ToShortDateString();
                    itemInfo.LastModified = dynamicContentItem.LastModified.ToShortDateString();
                    itemInfo.ItemUrl = SitefinityContent + GetTypeFriendlyName(item);
                }
            }
            catch { Exception ex; }
        }

        private static Guid CreateGuid(string itemId)
        {
            Guid newGuid;
            if (Guid.TryParse(itemId, out newGuid))
                return newGuid;
            else
                return Guid.Empty;
        }
    }
}
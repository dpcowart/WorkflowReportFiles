using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SitefinityWebApp.Custom.Services.Models;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Security;
using SitefinityWebApp.Custom.Reports.Helpers.WorkflowItemInfo;

namespace SitefinityWebApp.Custom.Services.Api.Controllers
{
    public class WorkflowItemsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<WorkflowItemsModel> Get()
        {
            var myCollection = new List<WorkflowItemsModel>();
            WorkflowManager workflowManager = new WorkflowManager();
            RoleManager roleManager = RoleManager.GetManager();
            RoleManager appRoleManager = RoleManager.GetManager("AppRoles");
            var identity = ClaimsManager.GetCurrentIdentity(); 
            Guid userId = identity.UserId;

            try
            {
                var workflowPermissions = workflowManager.GetWorkflowPermissions().Where(w => w.Definition.IsActive == true).ToList();

                foreach (var workflowPermission in workflowPermissions)
                {
                    if ((workflowPermission.PrincipalType.ToString() == "Role" &&
                        (roleManager.IsUserInRole(userId, workflowPermission.PrincipalName) ||
                        appRoleManager.IsUserInRole(userId, workflowPermission.PrincipalName))) ||
                        (workflowPermission.PrincipalType.ToString() == "User" && 
                        identity.Name == workflowPermission.PrincipalName))
                    {
                        foreach (var workflowScope in workflowPermission.Definition.WorkflowScopes)
                        {
                            foreach (var workflowItem in WorkflowItemManager.GetWorkflowItems(workflowPermission, workflowScope))
                            {
                                myCollection.Add(new WorkflowItemsModel(workflowItem));
                            }
                        }
                    }
                }
            }
            catch { Exception ex; }
            return myCollection;
        }

        //// GET api/<controller>/5
        //public IEnumerable<WorkflowItemsModel> Get(string id)
        //{
        //    var myCollection = new List<WorkflowItemsModel>();
        //    WorkflowManager workflowManager = new WorkflowManager();
        //    RoleManager roleManager = RoleManager.GetManager();
        //    RoleManager appRoleManager = RoleManager.GetManager("AppRoles");
        //    var identity = ClaimsManager.GetCurrentIdentity();
        //    Guid userId = identity.UserId;

        //    try
        //    {
        //        var workflowPermissions = workflowManager.GetWorkflowPermissions().Where(w => w.Definition.IsActive == true).ToList();

        //        foreach (var workflowPermission in workflowPermissions)
        //        {
        //            if ((workflowPermission.PrincipalType.ToString() == "Role" &&
        //                (roleManager.IsUserInRole(userId, workflowPermission.PrincipalName) ||
        //                appRoleManager.IsUserInRole(userId, workflowPermission.PrincipalName))) ||
        //                (workflowPermission.PrincipalType.ToString() == "User" &&
        //                identity.Name == workflowPermission.PrincipalName))
        //            {
        //                foreach (var workflowScope in workflowPermission.Definition.WorkflowScopes)
        //                {
        //                    foreach (var workflowItem in WorkflowItemManager.GetWorkflowItems(workflowPermission, workflowScope))
        //                    {
        //                        myCollection.Add(new WorkflowItemsModel(workflowItem));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch { Exception ex; }
        //    return myCollection;
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}
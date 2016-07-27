<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<%--<link href="/custom/styles/kendo/css/kendo.common.min.css" rel="stylesheet" />
<link href="/custom/styles/kendo/css/kendo.default.min.css" rel="stylesheet" />--%>
<link href="/custom/styles/reports/css/reports.css" rel="stylesheet" />

<sf:ResourceLinks ID="resourcesLinks" runat="server"  UseEmbeddedThemes="True" Theme="Default">
    <sf:ResourceFile JavaScriptLibrary="KendoAll" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js" Static="True" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />    
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css" Static="true" />
 </sf:ResourceLinks>

<div id="example" class="k-content">
    <div id="clientsDb">
        <asp:Label ID="MessageLabel" Text="Text" runat="server"/>
        <div id="workflowGrid"></div>
        <script type="text/x-kendo-template" id="template">
            <div class="tabstrip">
                <ul>
                    <li class="k-state-active tabborder">
                        Workflow Details
                    </li>
                </ul>
                <div class="wrap tabborder">
                    <div class='workflow-details'>
                        <table>
                            <tr>
                                <td class="workflow-details-info" style="width:15%;">
                                    #if(ItemCreatorAvatar) {# <img src=#=ItemCreatorAvatar# class="avatar roundedges"> #}#
                                </td>
                                <td width="18%" class="workflow-details-info" style="text-align:right;">
                                    <ul>
                                        <li><label>Submitter:</label></li>
                                        <li><label>Workflow:</label></li>
                                        <li><label>Steps:</label></li>
                                        <li><label>Approver:</label></li>
                                        <li><label>Language:</label></li>
                                    <ul>
                                </td>
                                <td class="workflow-details-info" style="text-align:left;">
                                    <ul>
                                        #if(WorkflowApproverEmail){# <li><a class="user-email" href='mailto:#: WorkflowApproverEmail #'>#: ItemCreator#</a></li> #}else{# <li>ItemCreator</li> #}#
                                        #if(WorkflowTitle){# <li>#: WorkflowTitle #</li> #}else{# <li>N/A</li> #}#                            
                                        #if(WorkflowSteps){# <li>#: WorkflowSteps #</li> #}else{# <li>N/A</li> #}#   
                                        #if(WorkflowApproverName){# <li>#: WorkflowApproverName #</li> #}else{# <li>N/A</li> #}#   
                                        #if(ItemLanguage){# <li>#: ItemLanguage #</li> #}else{# <li>N/A</li> #}#
                                    <ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </script>
    </div>

    <script type="text/javascript">
        //Sets the background color of the Days column
        //Red = Urgent notice
        //Yellow = Medium notice
        //Green = No rush
        //Values for red and yellow notice are set in the widget's designer
        function Getvalue(value) {
            var daysTillYellow = document.getElementById('<%= DaysTillYellow.ClientID %>').value;
            var daysTillRed = document.getElementById('<%= DaysTillRed.ClientID %>').value;
            
            if (value && value !== null && (value >= Number(daysTillYellow) && value < Number(daysTillRed)))
                return "<div class='roundedges yellowbackground'>" + value + "</div>";
            else if (value && value !== null && value >= Number(daysTillRed))
                return "<div class='roundedges redbackground'>" + value + "</div>";
            else
                return "<div class='roundedges greenbackground'>" + value + "</div>";
        }

        $(document).ready(function () {

            $("#workflowGrid").kendoGrid({
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read: "/api/workflowitems/get"
                    },
                    pageSize: 5,
                }),
                groupable: true,
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                },
                detailTemplate: kendo.template($("#template").html()),
                detailInit: detailInit,
                columns: [
                {
                    width: 100,
                    attributes: { style: "text-align: left; border: 0 !important; padding:15px!important;" },
                    field: "DateCreated",
                    title: "Date"
                },
                {
                    attributes: { style: "border: 0 !important; padding:15px!important;" },
                    field: "ItemTitle",
                    title: "Title",
                    template: '<a class="links" target="_blank" href=#=ItemURL#>#=ItemTitle#</a>'
                },
                {
                    width: 150,
                    attributes: { style: "border: 0 !important; padding:15px!important;" },
                    field: "ItemType",
                    title: "Type"
                },
                {
                    width: 50,
                    attributes: { style: "text-align: center; border: 0 !important;" },
                    field: "DaysAwaitingApproval",
                    title: "Days",
                    template: '#=Getvalue(DaysAwaitingApproval)#'
                },                
                {
                    width: 100,
                    attributes: { style: "text-align: left; border: 0 !important; padding:15px!important;" },
                    field: "WorkflowItemStatus",
                    title: "Status"
                }
                ]
            });
        });

        function detailInit(e) {
            var detailRow = e.detailRow;

            detailRow.find(".tabstrip").kendoTabStrip({
                animation: {
                    open: { effects: "fadeIn" }
                },
            });
        }
    </script>
</div>
<div><br /></div>
<asp:HiddenField id="DaysTillYellow" value="" runat="server"/>
<asp:HiddenField id="DaysTillRed" value="" runat="server"/>
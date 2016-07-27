<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
    <sitefinity:ResourceFile Name="Styles/jQuery/jquery.ui.core.css" />
    <sitefinity:ResourceFile Name="Styles/jQuery/jquery.ui.dialog.css" />
    <sitefinity:ResourceFile Name="Styles/jQuery/jquery.ui.theme.sitefinity.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto; ">
<ol>        
    <%--++++++++++++++++++++++++ Page Selector--%>
    <%--<li class="sfFormCtrl">
    <label class="sfTxtLbl" for="selectedSelectedPageIDLabel">Details Page Selector</label>
    <span style="display: none;" class="sfSelectedItem" id="selectedSelectedPageIDLabel">
        <asp:Literal runat="server" Text="" />
    </span>
    <span class="sfLinkBtn sfChange">
        <a href="javascript: void(0)" class="sfLinkBtnIn" id="pageSelectButtonSelectedPageID">
            <asp:Literal runat="server" Text="<%$Resources:Labels, SelectDotDotDot %>" />
        </a>
    </span>
    <div id="selectorTagSelectedPageID" runat="server" style="display: none;">
        <sf:PagesSelector runat="server" ID="pageSelectorSelectedPageID" 
            AllowExternalPagesSelection="false" AllowMultipleSelection="false" />
    </div>
    <div class="sfExample">Select the page where you display an item's details...</div>
    </li>--%>
    <%--++++++++++++++++++++++++ End Page Selector--%>
    
    <li class="sfFormCtrl">
    <asp:Label runat="server" AssociatedControlID="DaysTillMediumNotice" CssClass="sfTxtLbl">Days Till Medium Notice</asp:Label>
    <asp:TextBox ID="DaysTillMediumNotice" runat="server" CssClass="sfTxt" />
    <div class="sfExample">Designate the number of days before an item will be of medium notice (i.e. turn yellow)...</div>
    </li>
    
    <li class="sfFormCtrl">
    <asp:Label runat="server" AssociatedControlID="DaysTillUrgentNotice" CssClass="sfTxtLbl">Days Till Urgent Notice</asp:Label>
    <asp:TextBox ID="DaysTillUrgentNotice" runat="server" CssClass="sfTxt" />
    <div class="sfExample">Designate the number of days before an item will be of urgent notice (i.e. turn red)...</div>
    </li>
    
</ol>
</div>

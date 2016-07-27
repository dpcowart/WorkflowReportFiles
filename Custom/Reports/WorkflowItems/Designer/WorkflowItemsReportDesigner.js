Type.registerNamespace("SitefinityWebApp.Custom.Reports.WorkflowItems.Designer");

SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner = function (element) {

    /*++++++++++++++++++++++ Page Selector*/
    /* Initialize SelectedPageID fields */
    //this._pageSelectorSelectedPageID = null;
    //this._selectorTagSelectedPageID = null;
    //this._SelectedPageIDDialog = null;
 
    //this._showPageSelectorSelectedPageIDDelegate = null;
    //this._pageSelectedSelectedPageIDDelegate = null;
    /*++++++++++++++++++++++ End Page Selector*/

    /* Initialize DaysTillMediumNotice fields */
    this._daysTillMediumNotice = null;
    
    /* Initialize DaysTillUrgentNotice fields */
    this._daysTillUrgentNotice = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner.callBaseMethod(this, 'initialize');

        /*++++++++++++++++++++++ Page Selector*/
        ///* Initialize SelectedPageID */
        //this._showPageSelectorSelectedPageIDDelegate = Function.createDelegate(this, this._showPageSelectorSelectedPageIDHandler);
        //$addHandler(this.get_pageSelectButtonSelectedPageID(), "click", this._showPageSelectorSelectedPageIDDelegate);

        //this._pageSelectedSelectedPageIDDelegate = Function.createDelegate(this, this._pageSelectedSelectedPageIDHandler);
        //this.get_pageSelectorSelectedPageID().add_doneClientSelection(this._pageSelectedSelectedPageIDDelegate);

        //if (this._selectorTagSelectedPageID) {
        //    this._SelectedPageIDDialog = jQuery(this._selectorTagSelectedPageID).dialog({
        //        autoOpen: false,
        //        modal: false,
        //        width: 395,
        //        closeOnEscape: true,
        //        resizable: false,
        //        draggable: false,
        //        zIndex: 5000
        //    });
        //}
        /*++++++++++++++++++++++ End Page Selector*/
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner.callBaseMethod(this, 'dispose');

        /*++++++++++++++++++++++ Page Selector*/
        ///* Dispose SelectedPageID */
        //if (this._showPageSelectorSelectedPageIDDelegate) {
        //    $removeHandler(this.get_pageSelectButtonSelectedPageID(), "click", this._showPageSelectorSelectedPageIDDelegate);
        //    delete this._showPageSelectorSelectedPageIDDelegate;
        //}

        //if (this._pageSelectedSelectedPageIDDelegate) {
        //    this.get_pageSelectorSelectedPageID().remove_doneClientSelection(this._pageSelectedSelectedPageIDDelegate);
        //    delete this._pageSelectedSelectedPageIDDelegate;
        //}
        /*++++++++++++++++++++++ End Page Selector*/
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /*++++++++++++++++++++++ Page Selector*/
        /* RefreshUI SelectedPageID */
        //if (controlData.SelectedPageID && controlData.SelectedPageID !== "00000000-0000-0000-0000-000000000000") {
        //    var pagesSelectorSelectedPageID = this.get_pageSelectorSelectedPageID().get_pageSelector();
        //    var selectedPageLabelSelectedPageID = this.get_selectedSelectedPageIDLabel();
        //    var selectedPageButtonSelectedPageID = this.get_pageSelectButtonSelectedPageID();
        //    pagesSelectorSelectedPageID.add_selectionApplied(function (o, args) {
        //        var selectedPage = pagesSelectorSelectedPageID.get_selectedItem();
        //        if (selectedPage) {
        //            selectedPageLabelSelectedPageID.innerHTML = selectedPage.Title.Value;
        //            jQuery(selectedPageLabelSelectedPageID).show();
        //            selectedPageButtonSelectedPageID.innerHTML = '<span>Change</span>';
        //        }
        //    });
        //    pagesSelectorSelectedPageID.set_selectedItems([{ Id: controlData.SelectedPageID}]);
        //}        
        /*++++++++++++++++++++++ End Page Selector*/

        /* RefreshUI DaysTillMediumNotice */
        jQuery(this.get_daysTillMediumNotice()).val(controlData.DaysTillMediumNotice);

        /* RefreshUI DaysTillUrgentNotice */
        jQuery(this.get_daysTillUrgentNotice()).val(controlData.DaysTillUrgentNotice);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges SelectedPageID */

        /* ApplyChanges DaysTillMediumNotice */
        controlData.DaysTillMediumNotice = jQuery(this.get_daysTillMediumNotice()).val();

        /* ApplyChanges DaysTillUrgentNotice */
        controlData.DaysTillUrgentNotice = jQuery(this.get_daysTillUrgentNotice()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /*++++++++++++++++++++++ Page Selector*/
    /* SelectedPageID private methods */
    //_showPageSelectorSelectedPageIDHandler: function (selectedItem) {
    //    var controlData = this._propertyEditor.get_control();
    //    var pagesSelector = this.get_pageSelectorSelectedPageID().get_pageSelector();
    //    if (controlData.SelectedPageID) {
    //        pagesSelector.set_selectedItems([{ Id: controlData.SelectedPageID }]);
    //    }
    //    this._SelectedPageIDDialog.dialog("open");
    //    jQuery("#designerLayoutRoot").hide();
    //    this._SelectedPageIDDialog.dialog().parent().css("min-width", "355px");
    //    dialogBase.resizeToContent();
    //},

    //_pageSelectedSelectedPageIDHandler: function (items) {
    //    var controlData = this._propertyEditor.get_control();
    //    var pagesSelector = this.get_pageSelectorSelectedPageID().get_pageSelector();
    //    this._SelectedPageIDDialog.dialog("close");
    //    jQuery("#designerLayoutRoot").show();
    //    dialogBase.resizeToContent();
    //    if (items == null) {
    //        return;
    //    }
    //    var selectedPage = pagesSelector.get_selectedItem();
    //    if (selectedPage) {
    //        this.get_selectedSelectedPageIDLabel().innerHTML = selectedPage.Title.Value;
    //        jQuery(this.get_selectedSelectedPageIDLabel()).show();
    //        this.get_pageSelectButtonSelectedPageID().innerHTML = '<span>Change</span>';
    //        controlData.SelectedPageID = selectedPage.Id;
    //    }
    //    else {
    //        jQuery(this.get_selectedSelectedPageIDLabel()).hide();
    //        this.get_pageSelectButtonSelectedPageID().innerHTML = '<span>Select...</span>';
    //        controlData.SelectedPageID = "00000000-0000-0000-0000-000000000000";
    //    }
    //},

    ///* --------------------------------- properties -------------------------------------- */

    ///* SelectedPageID properties */
    //get_pageSelectButtonSelectedPageID: function () {
    //    if (this._pageSelectButtonSelectedPageID == null) {
    //        this._pageSelectButtonSelectedPageID = this.findElement("pageSelectButtonSelectedPageID");
    //    }
    //    return this._pageSelectButtonSelectedPageID;
    //},
    //get_selectedSelectedPageIDLabel: function () {
    //    if (this._selectedSelectedPageIDLabel == null) {
    //        this._selectedSelectedPageIDLabel = this.findElement("selectedSelectedPageIDLabel");
    //    }
    //    return this._selectedSelectedPageIDLabel;
    //},
    //get_pageSelectorSelectedPageID: function () {
    //    return this._pageSelectorSelectedPageID;
    //},
    //set_pageSelectorSelectedPageID: function (val) {
    //    this._pageSelectorSelectedPageID = val;
    //},
    //get_selectorTagSelectedPageID: function () {
    //    return this._selectorTagSelectedPageID;
    //},
    //set_selectorTagSelectedPageID: function (value) {
    //    this._selectorTagSelectedPageID = value;
    //},
    /*++++++++++++++++++++++ End Page Selector*/

    /* DaysTillMediumNotice properties */
    get_daysTillMediumNotice: function () { return this._daysTillMediumNotice; }, 
    set_daysTillMediumNotice: function (value) { this._daysTillMediumNotice = value; },

    /* DaysTillUrgentNotice properties */
    get_daysTillUrgentNotice: function () { return this._daysTillUrgentNotice; }, 
    set_daysTillUrgentNotice: function (value) { this._daysTillUrgentNotice = value; }
}

SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner.registerClass('SitefinityWebApp.Custom.Reports.WorkflowItems.Designer.WorkflowItemsReportDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

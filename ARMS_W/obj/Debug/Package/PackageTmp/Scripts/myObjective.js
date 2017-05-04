/*
*
Author: Hervie T. Inoc
Date Created: April 2013
Modified Date: NA
Discription: For calendar events
*
*/
var ns4;

//Collection Button
var btn_add = null;
var btn_cancel = null;

//FOR CALL REPORT DECLARATION COLLECTION
//var tbl_CRCollection = null;

//Hide Button in call report collection
var btn_saveEdit = null;
//hidden field call report collection
var hdn_evntId = null;
//Buttons in Callreport Collection
var btn_edit = null;
//tab collection
var cr_collectiontab = null;

//Table UNDER CALL REPORT MERCHANDISE TAB
var tbl_mse_callrepProductPres = null;
//NEW CALL REPORT CODE
var tbl_collection_crlist = null;

/* New Code Declaration  */
//Header Fields
var txt_acctcode = null;
var txt_accountClass = null;
var txt_accountName = null;
var txt_accountAddress = null;
var txt_contactPerson = null;
var txt_ContactNumber = null;

//------For Collection Coverage------
// Collection Field
var txt_collectBrand = null;
var txt_collectAmount = null;

//Collection Table
var tbl_collection_dtls = null;
//------For Merchandising Coverage
//Merchandising Fields
var txt_storechecking = null;
//------For Sales Coverage
//Sales Fields
var txt_salesBrand = null;
var txt_salesAmount = null;
//Sales Tables
var tbl_sales_dtls = null;
//------For Customer Service
var txt_issue_concern = null;
//CALL REPORT
//collection here
var txt_cr_datecheck = null;
var txt_cr_postdatedcheck = null;
var txt_cr_totalcollection = null;
var txt_cr_colremarks = null;

//Merchandising here
var txt_cr_pstorecheck = null;

//Sales

//Customer Service here
var txt_cs_callrepIssue = null;

//call report CS checkbox
var chk_cs_callrepOntime = null;
var chk_cs_callrepDelay = null;
var chk_cs_callrepcomplete = null;
var chk_cs_callrepincomplete = null;

//call report CS text Area
var txt_cs_callrepSummarylacking = null;
var txt_cs_callrepRemarks = null;
var txt_cs_callrepRecommend = null;
var txt_cs_callrepTimetable = null;

//BUTTON FOR ALL COVERAGE
var btn_saveDraft = null;
var btn_saveChanges = null;
var btn_coverageEdit = null;

//Header Fields
var txt_cr_accountCode = null;
var txt_cr_accountclass = null;
var txt_cr_accountname = null;
var txt_cr_accountaddress = null;
var txt_cr_contactperson = null;
var txt_cr_contactpersonNo = null;
var txt_freqvisit = null;
var txt_timein = null;
var txt_timeout = null;
//checkbox in header
var chk_isunPlanned = null;

//Call report Hidden Fields
var txt_hiden_cr_eventId = null;
var txt_hidden_cr_month = null;
var txt_hidden_cr_day = null;
var txt_hidden_cr_year = null;
//var txt_hidden_cr_Linenum = null;
var txt_hidden_cr_soId = null;
var txt_hidden_cr_linenum = null;

//Button
var btn_updatecallrep = null;

//collection 
var chk_Fcollection =null;
var chk_Pcollection = null;
var chk_Ncollection = null;

//text fields
var txt_cr_brand = null;
var txt_cr_pAmt = null;
var txt_cr_aAmt = null;
var txt_cr_collectRemarks = null;

//Merchandising
var txt_cr_storecheckingres = null;
var txt_cr_productpresented = null;
var txt_cr_counterclerk = null;
var txt_cr_mobileno = null;
var txt_cr_remarks = null;


//SALES
var txt_cr_competitorsAct = null;
var chk_yes = null;
var chk_no = null;
var txt_cr_nextcalldate = null;
var txt_cr_otherInfo = null;
var txt_cr_salesBrand = null;
var txt_cr_salesEstimatedAmt = null;
var txt_cr_salesActualAmt = null;
var txt_cr_salesremarks = null;
var txt_timein = null;
var txt_timeout = null;
var txt_freqvisit = null;

//hotel Name coverage
var txt_hotelname = null;
var txt_hotel_contact = null;
var txt_cr_hotelname = null;
var txt_cr_hotelcontact = null;

var Edit_fields = null;
var div_saveDraft = null;
var div_edit = null;
var div_update = null;
var div_delete = null;
var div_callreport = null;

var f_eventId = null;
var chk_delete = null;

var btn_delete = null;

var txt_hidden_linenum = null;
var txt_hidden_eventId = null;

//changes in coverage Merchandise
var txt_mse_productpresented = null;
var txt_mse_counterclerk = null;
var txt_mse_mobileno = null;

//attachment
var lnk_attach = null;
var lnk_view_attach = null;
var txt_FileAttachment = null;

//Second Attachmemt
var lnk_attach2 = null;
var lnk_view_attach2 = null;
var txt_FileAttachment2 = null;

var spn_dateholder = null;
var spn_callreport_status = null;
var _monthname = null;

var time_in = null;
var time_out = null;
var location_in = null;
var location_out = null;
var isPlanned = null;
var cls_excel_file = null;
var listOfItems = null;
var btn_cancelcallreport = null;
var btn_cancelDraft = null;
var btn_cancelChanges = null;
var txt_total_collection = null;
var txt_total_sales = null;
var txt_cr_colestAmount = null;
var txt_cr_colactAmount = null;
var txt_cr_total_salesEstAmt = null;
var txt_cr_total_salesActtAmt = null;
var txt_salesRemarks = null;
var txt_cr_details = null;

var isShowSaveDraftButton = false;

$(function () {
    // Button under collection tab
    // assigning variables to button name in page
    btn_add = $("#btn_add");
    btn_cancel = $("#btn_cancel");
    cr_collectiontab = $("#callreporttab-1");

    //TABLE CALL REPORT DECLARATION MERCHANDISE
    tbl_mse_callrepProductPres = $("#tbl_mse_callrepProductPres");
    txt_cs_callrepIssue = $("#txt_cs_callrepIssue");

    txt_hidden_obj = $("#txt_hidden_obj");

    //NEW CALL REPORT CODE
    tbl_collection_crlist = $("#tbl_collection_crlist");

    $("#Logchanges").hide();
    $("#routeChanges").hide();
    $("#VisitLogs").hide();

    //New Code Declaration and Assignment

    //collection Field
    txt_acctcode = $("#txt_acctcode");
    txt_accountClass = $("#txt_accountClass");
    txt_accountName = $("#txt_accountName");
    txt_accountAddress = $("#txt_accountAddress");
    txt_contactPerson = $("#txt_contactPerson");
    txt_ContactNumber = $("#txt_ContactNumber");
    txt_collectBrand = $("#txt_collectBrand");
    txt_collectAmount = $("#txt_collectAmount");
    //Collection Table
    tbl_collection_dtls = $("#tbl_collection_dtls");

    //Merchandising Field
    txt_storechecking = $("#txt_storechecking");

    //Sales Field
    txt_salesBrand = $("#txt_salesBrand");
    txt_salesAmount = $("#txt_salesAmount");
    //Sales Table
    tbl_sales_dtls = $("#tbl_sales_dtls");
    btn_saveDraft = $("#btn_saveDraft");
    btn_saveChanges = $("#btn_saveChanges");
    btn_coverageEdit = $("#btn_coverageEdit");

    //Customer Service fields
    txt_issue_concern = $("#txt_issue_concern");

    spn_callreport_status = $("#spn_callreport_status");
    //Call report

    //Header Feilds
    txt_cr_accountCode = $("#txt_cr_accountCode");
    txt_cr_accountclass = $("#txt_cr_accountclass");
    txt_cr_accountname = $("#txt_cr_accountname");
    txt_cr_accountaddress = $("#txt_cr_accountaddress");
    txt_cr_contactperson = $("#txt_cr_contactperson");
    txt_cr_contactpersonNo = $("#txt_cr_contactpersonNo");
    txt_freqvisit = $("#txt_freqvisit");
    txt_timein = $("#txt_timein");
    txt_timeout = $("#txt_timeout");
    //checkbox in header
    chk_isunPlanned = $("#chk_isunPlanned");

    //Call report Hidden Fields
    txt_hiden_cr_eventId = $("#txt_hiden_cr_eventId");
    txt_hidden_cr_month = $("#txt_hidden_cr_month");
    txt_hidden_cr_day = $("#txt_hidden_cr_day");
    txt_hidden_cr_year = $("#txt_hidden_cr_year");
    //txt_hidden_cr_Linenum = $("#txt_hidden_cr_Linenum");
    txt_hidden_cr_soId = $("#txt_hidden_cr_soId");
    txt_hidden_cr_linenum = $("#txt_hidden_cr_linenum");

    //button
    btn_updatecallrep = $("#btn_updatecallrep");

    //call report Collection here

    txt_cr_datecheck = $("#txt_cr_datecheck");
    txt_cr_postdatedcheck = $("#txt_cr_postdatedcheck");
    txt_cr_totalcollection = $("#txt_cr_totalcollection");
    txt_cr_colremarks = $("#txt_cr_colremarks");

    //check Box
    chk_Fcollection = $("#chk_Fcollection");
    chk_Pcollection = $("#chk_Pcollection");
    chk_Ncollection = $("#chk_Ncollection");

    //text fields
    txt_cr_brand = $("#txt_cr_brand");
    txt_cr_pAmt = $("#txt_cr_pAmt");
    txt_cr_aAmt = $("#txt_cr_aAmt");
    txt_cr_collectRemarks = $("#txt_cr_collectRemarks");

    //call report merchandise here

    //merchandising Field
    txt_cr_pstorecheck = $("#txt_cr_pstorecheck");
    txt_cr_storecheckingres = $("#txt_cr_storecheckingres");
    txt_cr_productpresented = $("#txt_cr_productpresented"); ;
    txt_cr_counterclerk = $("#txt_cr_counterclerk");
    txt_cr_mobileno = $("#txt_cr_mobileno");
    txt_cr_remarks = $("#txt_cr_remarks");

    //Customer SERVICE HERE
    chk_cs_callrepOntime = $("#chk_cs_callrepOntime");
    chk_cs_callrepDelay = $("#chk_cs_callrepDelay");
    chk_cs_callrepcomplete = $("#chk_cs_callrepcomplete");
    chk_cs_callrepcomplete = $("#chk_cs_callrepcomplete");
    txt_cs_callrepSummarylacking = $("#txt_cs_callrepSummarylacking");
    txt_cs_callrepRemarks = $("#txt_cs_callrepRemarks");
    txt_cs_callrepRecommend = $("#txt_cs_callrepRecommend");
    txt_cs_callrepTimetable = $("#txt_cs_callrepTimetable");
    txt_cs_hiddenEventId = $("#txt_cs_hiddenEventId");

    //Sales 
    txt_cr_competitorsAct = $("#txt_cr_competitorsAct");
    chk_yes = $("#chk_yes");
    chk_no = $("#chk_no");
    txt_cr_nextcalldate = $("#txt_cr_nextcalldate");
    txt_cr_otherInfo = $("#txt_cr_otherInfo");
    txt_cr_salesBrand = $("#txt_cr_salesBrand");
    txt_cr_salesEstimatedAmt = $("#txt_cr_salesEstimatedAmt");
    txt_cr_salesActualAmt = $("#txt_cr_salesActualAmt");
    txt_cr_salesremarks = $("#txt_cr_salesremarks");
    txt_timein = $("#txt_timein");
    txt_timeout = $("#txt_timeout");
    txt_freqvisit = $("#txt_freqvisit");

    //hotel Name
    txt_hotelname = $("#txt_hotelname");
    txt_hotel_contact = $("#txt_hotel_contact");
    txt_cr_hotelname = $("#txt_cr_hotelname");
    txt_cr_hotelcontact = $("#txt_cr_hotelcontact");

    //trapping control variables
    Edit_fields = $(".Edit_fields");
    div_saveDraft = $("#div_saveDraft");
    div_update = $("#div_update");
    div_edit = $("#div_edit");
    div_delete = $("#div_delete");
    div_callreport = $("#div_callreport");

    chk_delete = $("#chk_delete");

    btn_delete = $("#btn_delete");

    txt_hidden_linenum = $("#txt_hidden_linenum");
    txt_hidden_eventId = $("#txt_hidden_eventId");

    //changes in coverage Merchandise
    txt_mse_productpresented = $("#txt_mse_productpresented");
    txt_mse_counterclerk = $("#txt_mse_counterclerk");
    txt_mse_mobileno = $("#txt_mse_mobileno");

    lnk_attach = $("#lnk_attach");
    lnk_view_attach = $("#lnk_view_attach");
    txt_FileAttachment = $("#txt_FileAttachment");

    //secon Attachment Assign
    lnk_attach2 = $("#lnk_attach2");
    lnk_view_attach2 = $("#lnk_view_attach2");
    txt_FileAttachment2 = $("#txt_FileAttachment2");

    spn_dateholder = $("#spn_dateholder");

    btn_cancelcallreport = $("#btn_cancelcallreport");
    btn_cancelDraft = $("#btn_cancelDraft");
    btn_cancelChanges = $("#btn_cancelChanges");

    /* added by billy jay delima  */
    time_in = $("#time_in");
    time_out = $("#time_out");
    location_in = $("#location_in");
    location_out = $("#location_out");
    cls_excel_file = $(".cls_excel_file");
    txt_total_collection = $("#txt_total_collection");
    txt_total_sales = $("#txt_total_sales");
    txt_cr_colestAmount = $("#txt_cr_colestAmount");
    txt_cr_colactAmount = $("#txt_cr_colactAmount");
    txt_cr_total_salesEstAmt = $("#txt_cr_total_salesEstAmt");
    txt_cr_total_salesActtAmt = $("#txt_cr_total_salesActtAmt");
    txt_salesRemarks = $("#txt_salesRemarks");
    txt_cr_details = $("#txt_cr_details");

    //hide link for upload and download template coverage plan
    if (allowEdit()) {
        cls_excel_file.show();
    }
    else {
        cls_excel_file.hide();
    }
    /* end */

    /** A work around function that generates the output of MM DD YY ex : August 20,2013*/
    getMonthName(Eventmonth);

    var mmddyyFormat = _monthname + " " + Eventday + ", " + Eventyear;
    spn_dateholder.html(mmddyyFormat);

    $('input.auto').autoNumeric();
    //NEW CODE IMPLEMENTATION 
    //Hide Trappings
    div_update.hide();
    div_edit.hide();
    div_saveDraft.hide();
    div_delete.hide();
    Edit_fields.hide();
    //not to be hide the div_callreport 
    //requested by pearl 
    //date of request: July 22, 2013 
    LoadTrappings();

    //TimeValidation();
    getEventId(soId, Eventmonth, Eventyear);

    //Getobjectivecode();

    if (qrystring_acctCode != "") {
        autodisplay_account_info(qrystring_acctCode);
    }
    //Hidden implementation for force applied coverage
    $("#tbl_collection_dtls").find(".hiddenTd").hide();
    $("#tbl_sales_dtls").find(".hiddenTd").hide();

    txt_cr_nextcalldate.datepicker();

    lnk_attach.uploadlink(
        baseurl + "Calendar/UploadFileAttachment",
        "txt_FileAttachment",
        "TESTING"
    );

    lnk_attach2.uploadlink(
        baseurl + "Calendar/UploadFileAttachment",
        "txt_FileAttachment2",
        "TESTING"
    );


    $("#lnk_upload_excel_data").uploadlink2(
        baseUrl + "Calendar/UploadExcelDataCoveragePlan?EventMonth=" + Eventmonth + "&EventDay=" + Eventday + "&EventYear=" + Eventyear,
        "txt_FileAttachment",
        "TESTING",
        function (res) {
            window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
        }
        );

    $("#lnk_attach").uploadlink(
        baseurl + "Calendar/UploadFileAttachment",
        "txt_FileAttachment",
        "TESTING"
    );

    //UNDER TAB COVERAGE

    //when button add is click under collection details
    //with field Brand and Amount
    $("#tbl_collection_dtls").find(".btn_add").click(function () {
        //Local variable declartion that can be access only inside this function
        var brand = null;
        var amount = null;
        //Local variable is being assigned to specific text field under collection Coverage
        brand = txt_collectBrand.val();
        amount = txt_collectAmount.val();

        //Check and validate first if the value is not null or a blank string
        // for the purpose to control not to add a blank information to the growth table
        if (brand != null && brand != "" && amount != null && amount != "") {

            var c_totalamt = 0;

            $("#tbl_collection_dtls .last_row").before('<tr class="AddedRow">' +
            '<td class="hiddenTd"><input type="text" value="Collection"/></td>' +
            '<td><input type="text" readonly="readonly" value="' + brand + '"/></td>' +
            '<td><input type="text" readonly="readonly" value="' + amount + '"/></td>' +
            '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
            '</tr>').prev().find('.btn_delete').click(function () {
                row = $(this).parent().parent();
                var deducAmt = parseFloat(undoAddComma(row.find('td:eq(2) input').attr("value")));

                txt_total_collection.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(c_totalamt)) - parseFloat(deducAmt)).toFixed(2)));
                $(this).parent().parent().remove();

            });
            // PopulateonMemodtlsifDraft("Collection", brand, amount, "", "", "", "", "");

            c_totalamt = parseFloat(undoAddComma(txt_total_collection.val())) + parseFloat(undoAddComma(txt_collectAmount.val()));
            txt_total_collection.attr("value", ReplaceNumberWithCommas(parseFloat(c_totalamt).toFixed(2)));

            txt_collectAmount.attr("value", "");
            txt_collectBrand.attr("value", "");

            $("#tbl_collection_dtls").find(".hiddenTd").hide();
        }
        else { alert("Fields Cannot be empty"); }
    });

    //when button add is click under sales details
    //with field Brand and Amount
    $("#tbl_sales_dtls").find(".btn_add").click(function () {
        //Local variable declartion that can be access only inside this function
        var brand = null;
        var amount = null;

        //Local variable is being assigned to specific text field under collection Coverage
        brand = txt_salesBrand.val();
        amount = txt_salesAmount.val();
        dtlsrmkrs = txt_salesRemarks.val();
       

        if (!isNotDuplicate(brand)) {
            return false;
        }

        //Check and validate first if the value is not null or a blank string
        // for the purpose to control not to add a blank information to the growth table
        if (brand != null && brand != "" && amount != null && amount != "") {

            var s_totalamt = 0;
            $("#tbl_sales_dtls .last_row").before('<tr class="AddedRow">' +
                                                      '<td class="hiddenTd"><input type="text" value="Sales"/></td>' +
                                                      '<td><input type="text" value="' + brand + '" readonly="readonly"/></td>' +
            //'<td><input type="text" value="' + ReplaceNumberWithCommas(parseFloat(amount).toFixed(2)) + '"/></td>' +
                                                      '<td><input type="text" value="' + amount + '" readonly="readonly"/></td>' +
                                                      '<td><input type="text" value="' + dtlsrmkrs + '"/></td>' +
                                                      '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {

                                                              row = $(this).parent().parent();
                                                              var deducAmts = parseFloat(undoAddComma(row.find('td:eq(2) input').attr("value")));
                                                              var total = undoAddComma(txt_total_sales.attr("value"));

                                                              var display_total = parseFloat(total) - deducAmts;

                                                              txt_total_sales.attr("value", addCommas(display_total));
                                                              //  txt_total_sales.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(s_totalamt)) - parseFloat(deducAmts)).toFixed(2)));
                                                              $(this).parent().parent().remove();

                                                          });
            s_totalamt = parseFloat(undoAddComma(txt_total_sales.val())) + parseFloat(undoAddComma(txt_salesAmount.val()));
            txt_total_sales.attr("value", ReplaceNumberWithCommas(parseFloat(s_totalamt).toFixed(2)));

            txt_salesBrand.attr("value", "");
            txt_salesAmount.attr("value", "");
            txt_salesRemarks.attr("value", "");
            $("#tbl_sales_dtls").find(".hiddenTd").hide();
        }
        else { alert("Fields Cannot be empty"); }
    });

    $("#tbl_mse_details").find(".btn_add").click(function () {
        //Local variable declartion that can be access only inside this function
        var Brand = null;
        var Cclerk = null;
        var cclerknum = null;

        //Local variable is being assigned to specific text field under collection Coverage
        Brand = txt_mse_productpresented.val();
        Cclerk = txt_mse_counterclerk.val();
        cclerknum = txt_mse_mobileno.val();
        //Check and validate first if the value is not null or a blank string
        // for the purpose to control not to add a blank information to the growth table

        if (Brand != null && Brand != "" && Cclerk != null && Cclerk != "" && cclerknum != null && cclerknum != "") {

            $("#tbl_mse_details .last_row").before('<tr class="AddedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="M"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Cclerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + cclerknum + '"/></td>' +
                                                  '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              $(this).parent().parent().remove();

                                                          });

            $("#tbl_mse_details").find(".hiddenTd").hide();
            //clear fields
            txt_mse_productpresented.attr("value", "");
            txt_mse_counterclerk.attr("value", "");
            txt_mse_mobileno.attr("value", "");
        }

        else { alert("Fields Cannot be empty"); }
    });

    //Button for Save as Draft
    btn_saveDraft.click(function () {
        var curr_date = new Date();
        var cur_day = curr_date.getDate();
        var cur_month = curr_date.getMonth();
        var cur_year = curr_date.getYear();
        var pcurr_date = (parseInt(cur_month) + 1) + "/" + cur_day + "/" + cur_year;
        var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
        var p_date = new Date(p_forceformateddate);
        var fcur_date = new Date(pcurr_date);

        var days = 24 * 60 * 60 * 1000;


        var datediff = p_date.getTime() - curr_date.getTime();
        var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));


        if (interval >= 2) {
            if (CheckHeaderRequiredFields() == false) { return; }
            else { SaveCoveragePlan(""); }
        }
        else {
            alert("You can't edit this date");
        }
    });

    $(".collection").keyup(function () {
        var postdate = txt_cr_postdatedcheck.val() == "" ? 0 : undoAddComma(txt_cr_postdatedcheck.val());
        var datecheck = txt_cr_datecheck.val() == "" ? 0 : undoAddComma(txt_cr_datecheck.val());
        var total = parseFloat(datecheck) + parseFloat(postdate);

        txt_cr_totalcollection.attr("value", numberWithCommas(total));
    });
    $(".collection").click(function () {
        $(this).select();
    });


    //Soft Delete
    $("#btn_softdelete").click(function () {
        SaveCoveragePlanChanges("", "T");
    });
    //Button for Save changes

    btn_saveChanges.click(function () {

        var curr_date = new Date();
        var cur_day = curr_date.getDate();
        var cur_month = curr_date.getMonth();
        var cur_year = curr_date.getYear();
        var pcurr_date = cur_month + "/" + cur_day + "/" + cur_year;
        var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
        var p_date = new Date(p_forceformateddate);
        var fcur_date = new Date(pcurr_date);

        var days = 24 * 60 * 60 * 1000;

        var datediff = p_date.getTime() - curr_date.getTime();
        var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

        if (interval >= 2) {

            if (CheckHeaderRequiredFields() == false) { return; }
            else { /**SaveCoveragePlan("");*/SaveCoveragePlanChanges("", ""); }

        }
        else {
            alert("You can't edit this date");
        }
    });

    btn_delete.click(function () {

        if (txt_acctcode.val() == "" | txt_acctcode.val() == null) {
            alert("Select Account Code");
            return false;
        }
        var ok = confirm("Are you sure you want to delete?");
        var account = txt_acctcode.val();
        var EventId = txt_hidden_eventId.val();
        var Linenum = txt_hidden_linenum.val();
        if (ok) {
            DeleteAccount(account, Eventmonth, Eventday, Eventyear, soId, EventId, Linenum);

        }

    });

    btn_cancelcallreport.click(function () {
        var ok = confirm("Are you sure you want cancel?");
        if (ok) {
            location.reload();
        }
    });

    btn_cancelChanges.click(function () {
        var ok = confirm("Are you sure you want cancel?");
        if (ok) {
            location.reload();
        }

    });

    btn_cancelDraft.click(function () {
        var ok = confirm("Are you sure you want cancel?");

        if (ok) {
            location.reload();
        }
    });

    chk_isunPlanned.change(function () {

        if (txt_cr_accountCode.attr("value") == "") {
            spn_callreport_status.text("Choose account name...");
            isPlanned = chk_isunPlanned.is(":checked") ? "T" : "F";
            div_callreport.hide();
            if (isPlanned == "T") {
                div_callreport.show();

            }
            ClearFieldsCallreport();
        }
        else if (confirm("Changing the value of this checkbox will clear all fields. Would you like to proceed? ")) {
            spn_callreport_status.text("Choose account name...");
            isPlanned = chk_isunPlanned.is(":checked") ? "T" : "F";
            div_callreport.hide();
            if (isPlanned == "T") {
                div_callreport.show();
            }
            ClearFieldsCallreport();
        }
        this.checked = isPlanned == "T" ? true : false;

    });


    //CheckBoxes under Call report
    //Collecetion
    $("#chk_Fcollection").change(function () {
        $("#chk_Pcollection").removeAttr('checked');
        $("#chk_Ncollection").removeAttr('checked');
    });

    $("#chk_Pcollection").change(function () {
        $("#chk_Fcollection").removeAttr('checked');
        $("#chk_Ncollection").removeAttr('checked');
    });

    $("#chk_Ncollection").change(function () {
        $("#chk_Pcollection").removeAttr('checked');
        $("#chk_Fcollection").removeAttr('checked');
    });

    //Merchandising
    //Sales
    //Customer Service
    $("#chk_no").change(function () {
        $("#chk_yes").removeAttr('checked');
    });

    $("#chk_yes").change(function () {
        $("#chk_no").removeAttr('checked');
    });

    btn_updatecallrep.click(function () {
        var c_date = new Date();
        // var p_date = new Date(Eventdate);

        //var datediff = p_date.getTime() - curr_date.getTime();
        //var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

        var curr_date = c_date.getMonth() + 1 + "/" + c_date.getDate() + "/" + c_date.getFullYear();
        var pass_date = Eventmonth + "/" + Eventday + "/" + Eventyear;
        var p_date = new Date(pass_date);
        var c_date = new Date(curr_date);
        var datediff = p_date.getTime() - c_date.getTime();
        var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

        //this code is commented upon request by 
        //Requestor Name: Pearl P. Pua
        //Request date: July 22, 2013
        //Reason: Can create a call report even beyond and within the date.

        if ($("#time_in").text() == "" | $("#time_in").text() == "(For Check In)") {
            alert("Required to check in");
            return;
        }
        else if ($("#time_out").text() == "" | $("#time_out").text() == "(For Check Out)") {
            alert("Required to check out");
            return;
        }

        if (interval <= 0) {
            if (CheckHeaderRequiredFieldsCR() == false) { return; }
            else {
                var msg = check_empty_fields();
                if (msg == "OK")
                    UpdateForCallReport();
                else
                    alert(msg);
            }
        }
        else { alert("Call report is not yet applicable for this date"); }
    });

    //$(".numeric").number_format({ precision: 2, decimal: ',', thousands: '.' });
    txt_collectAmount.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_salesAmount.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_cr_pAmt.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_cr_aAmt.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_cr_salesEstimatedAmt.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_cr_salesActualAmt.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });

    txt_cr_mobileno.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    txt_mse_mobileno.forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });

    txt_cr_hotelcontact.forceNumeric();
    txt_ContactNumber.forceNumeric();
    txt_hotel_contact.forceNumeric();
    txt_cr_contactpersonNo.forceNumeric();
    txt_freqvisit.forceNumeric();

    //Hide TD under call reports
    //collection grow table
    $("#tbl_cr_collection").find(".hiddenTd").hide();
    //merchandising groe table
    $("#tbl_cr_msdedtl").find(".hiddenTd").hide();
    //sales grow table
    $("#tbl_cr_salesdtl").find(".hiddenTd").hide();

    //Unplanned add item on collection List

    $("#tbl_cr_collection").find(".btn_add").click(function () {
        var brand = txt_cr_brand.val();
        var pAmt = txt_cr_pAmt.val();
        var aAmt = txt_cr_aAmt.val();
        var rmrks = txt_cr_collectRemarks.val();
        var pcollection = chk_Pcollection.is(":checked") ? "T" : "F";
        var ncollection = chk_Ncollection.is(":checked") ? "T" : "F";
        //chk_isunPlanned.is(":checked") ? "T" : "F";

        if (pcollection == "T" || ncollection == "T") {
            if (CheckcallreportRequiredFields("1") == false) { return; }
            else { AddCollectionList(brand, pAmt, aAmt, rmrks); }
        }
        else {
            AddCollectionList(brand, pAmt, aAmt, rmrks);
        }
    });

    $("#tbl_cr_msdedtl").find(".btn_add").click(function () {
        var brand = txt_cr_productpresented.val();
        var cclerk = txt_cr_counterclerk.val();
        var cclerknum = txt_cr_mobileno.val();
        var Remarks = txt_cr_remarks.val();

        AddMsdeOnList(brand, cclerk, cclerknum, "CALLREPORT", Remarks);
    });


    /** $("#tbl_mse_details").find(".btn_add").click(function () {

    var brand = txt_mse_productpresented.val();
    var cclerk = txt_mse_counterclerk.val();
    var cclerknum = txt_mse_mobileno.val();



    AddMsdeOnList(brand, cclerk, cclerknum, "COVERAGE");

    });   **/
    $("#tbl_cr_salesdtl").find(".btn_add").click(function () {
        var brand = txt_cr_salesBrand.val();
        var pAmt = txt_cr_salesEstimatedAmt.val();
        var aAmt = txt_cr_salesActualAmt.val();
        var rmrks = txt_cr_salesremarks.val();
        var details = txt_cr_details.val();

        AddSalesList(brand, pAmt, aAmt, rmrks, details);
    });

    $("#txt_Eventday").attr("value", Eventday);
    $("#txt_Eventmonth").attr("value", Eventmonth);
    $("#txt_Eventyear").attr("value", Eventyear);

    //Button add under objective>>collection tab
    $("#tbl_trans_info .btn_add").click(function () {
        var Brand = txt_brand.val();
        var Amount = txt_amount.val();

        if (Brand != "" && Amount != "") {
            $("#tbl_trans_info .last_row").before('<tr class="AddedRow"><td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                          '<td><input type="text" readonly="readonly" value="' + Amount + '"/></td>' +
                                                          '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              $(this).parent().parent().remove();

                                                          });
        }

        else { alert("Please Fill in feilds"); }


        //clear the input fields
        txt_brand.attr("value", "");
        txt_amount.attr("value", "");
    });

    txt_accountName.lookupTextField("");
    txt_cr_accountname.lookupTextField(isPlanned);



    $("#txt_collectBrand").lookdown(
    { "url": baseurl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
    { "": "M" },
    function (res) { return res; },
    function (res, all) {
        var _brand = all[1] == "null" ? "" : all[1];
        txt_collectBrand.attr("value", _brand);
    });

    $("#txt_cr_brand").lookdown(
    { "url": baseurl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
    { "": "M" },
    function (res) { return res; },
    function (res, all) {
        var _brand = all[1] == "null" ? "" : all[1];
        txt_cr_brand.attr("value", _brand);
    });

    $("#txt_salesBrand").lookdown(
    { "url": baseurl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
    { "": "M" },
    function (res) { return res; },
    function (res, all) {
        var _brand = all[1] == "null" ? "" : all[1];
        txt_salesBrand.attr("value", _brand);
    });

    $("#txt_cr_salesBrand").lookdown(
    { "url": baseurl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
    { "": "M" },
    function (res) { return res; },
    function (res, all) {
        var _brand = all[1] == "null" ? "" : all[1];
        txt_cr_salesBrand.attr("value", _brand);
    });

    $("#chk_cs_callrepOntime").change(function () {
        $("#chk_cs_callrepDelay").removeAttr('checked');
    });

    $("#chk_cs_callrepDelay").change(function () {
        $("#chk_cs_callrepOntime").removeAttr('checked');
    });

    $("#chk_cs_callrepcomplete").change(function () {
        $("#chk_cs_callrepincomplete").removeAttr('checked');
    });

    GetEventByDate();
    GetForCallReport();
});


function autodisplay_account_info(qrystring_acctCode) {
    var isReadonly = true;
    DisplayPreloader();
    var new_obj = {
        EventId: f_eventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId: soId,
        acctCode: qrystring_acctCode
    };
    div_callreport.hide();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST",
        url: baseUrl + "Calendar/getSpecificAccoutCodeInfo_Revise",
        success: function (res) {

            $("#tdModBtn").hide();
            


            if (!res.data.coverage.hasCallreport) {
                div_callreport.show();
                spn_callreport_status.text("For Update");
                isReadonly = false;
                txt_freqvisit.attr("value", res.data.number_of_visits);

                $("#changePOAttachment").show();
                $("#tdModBtn").show();

            }
            else {
                isReadonly = true;
                spn_callreport_status.text("Visited");
                var freqvisit = res.data.coverage.freqVisit != null ? res.data.coverage.freqVisit : "";
                txt_freqvisit.attr("value", freqvisit);
                // $("#btn_checkin").hide();
                // $("#btn_checkout").hide();
            }

            //added by francis 1/25/2017
            $("#tdDownloadBtn").hide();
            if (res.data.coverage.Attachment != undefined || res.data.coverage.Attachment != null) {
                $("#changePOAttachment").show();
                $("#tdDownloadBtn").show();
                $("#tdUploadBtn").hide();
                $("#DwnldFile").attr("href", baseurl + "Calendar/getAttachment?EventId=" + res.data.coverage.EventID + "&LineNum=" + res.data.coverage.LineNum);
                $("#filename").val(res.data.coverage.Attachment);

            }
            else {
                $("#changePOAttachment").hide();
            }
            //end

            //added by Francis 1/25/2017
            $("#cancelEdit").hide();
            $("#changePOAttachment").click(function () {
                $("#tdDownloadBtn").hide();
                $("#tdUploadBtn").show();
                $("#changePOAttachment").hide();
                $("#cancelEdit").show();
            });

            $("#cancelEdit").click(function () {
                $("#tdDownloadBtn").show();
                $("#tdUploadBtn").hide();
                $("#changePOAttachment").show();
                $("#cancelEdit").hide();
            });
            //end



            EnableDisableTextField(isReadonly);
            ClearFieldsCoverage();
            if (res.data.inventory_objective.hasForInventoryCount) {
                $("#create_inventory_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + qrystring_acctCode + '&doctype=newIC" target="new_tab">here</a> </div>');
                $("#inventory").addClass("tabcolor");
            }
            if (res.data.inventory_objective.inventoryCountId != "") {
                $("#view_inventory_count_link").html('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + res.data.inventory_objective.inventoryCountId + '&doctype=invcount" target="new_tab">here</a> </div>');
                $("#inventory").addClass("tabcolor");
            }
            $("#spn_delete").show();
            //  LoadTrappings();
            //     if (res.data.coverage != 0) {

            //IF CALENDAR IS DRAFT - hide CALL REPORT TAB
            // if (res.data.coverage.DocumentStatusId <= 0)
            //     $("#header_tab").find("ul > li:nth-child(2)").remove();

            txt_acctcode.attr("value", res.data.accountinfo.AccountCode);
            txt_accountName.attr("value", res.data.accountinfo.AccountName);
            txt_accountClass.attr("value", res.data.accountinfo.AccountClass);
            txt_accountAddress.attr("value", res.data.accountinfo.AccountAddress);
            txt_hotelname.attr("value", res.data.coverage.HotelName);
            txt_hotel_contact.attr("value", res.data.coverage.HotelNum);


            txt_storechecking.attr("value", res.data.coverage.StoreChecking);
            txt_issue_concern.attr("value", res.data.coverage.IssuesAndConcerns);

            if (res.data.coverage.StoreChecking)
                $("#crmerchandise").addClass("tabcolor");
            if (res.data.coverage.IssuesAndConcerns)
                $("#crcs").addClass("tabcolor");

            txt_total_sales.attr("value", addCommas(res.data.coverage.totalcoveragesales));

            $("#txt_totalcollection").attr("value", addCommas(res.data.total_collection_objective_lookupfromsap));
            var hasRow = false;
            $(res.data.collection_objective_lookupfromsap).each(function (index, elem) {
                $("#tbl_collection_dtls .last_row").before('<tr clone="true">' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.docnum + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.actDelDate + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.DueDate + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" class="fld_amount" value="' + addCommas(elem.balance) + '" /></td>' +
                                                           '</tr>');
                hasRow = true;
            });
            if (!hasRow) {
                $("#tbl_collection_dtls .last_row").before('<tr clone="true">' +
                                                           '<td colspan="6" style="width:100%; text-align:center"><input type="text" style="width:98%; text-align:center" value="No Data" /></td>' +
                                                           '</tr>');
            }

            //checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, qrystring_acctCode);


            txt_contactPerson.attr("value", res.data.coverage.ContactPerson);
            txt_ContactNumber.attr("value", res.data.coverage.ContactPersonNo);

            //call report Header details
            txt_cr_accountCode.attr("value", res.data.coverage.AccountCode);
            txt_cr_accountaddress.attr("value", res.data.coverage.AccountAddress);
            txt_cr_accountclass.attr("value", res.data.coverage.AccountClass);
            txt_cr_accountname.attr("value", res.data.coverage.AccountName);
            txt_cr_contactperson.attr("value", res.data.coverage.ContactPerson);
            txt_cr_contactpersonNo.attr("value", res.data.coverage.ContactPersonNo);
            txt_cr_hotelname.attr("value", res.data.coverage.HotelName);
            txt_cr_hotelcontact.attr("value", res.data.coverage.HotelNum);
            txt_cs_callrepIssue.attr("value", res.data.coverage.IssuesAndConcerns);

            //tracker details
            time_in.text(res.data.coverage.CheckInTime == null || res.data.coverage.CheckInTime == "" ? "(For Check In)" : res.data.coverage.CheckInTime).css("color", time_in.text() == "(For Check In)" ? "#ff0000" : "#000000");
            time_out.text(res.data.coverage.CheckOutTime == null || res.data.coverage.CheckOutTime == "" ? "(For Check Out)" : res.data.coverage.CheckOutTime).css("color", time_out.text() == "(For Check Out)" ? "#ff0000" : "#000000");
            location_in.text(res.data.coverage.CheckInAddress == null ? "(For Check In)" : res.data.coverage.CheckInAddress).css("color", location_in.text() == "(For Check In)" ? "#ff0000" : "#000000");
            location_out.text(res.data.coverage.CheckOutAddress == null ? "(For Check Out)" : res.data.coverage.CheckOutAddress).css("color", location_out.text() == "(For Check Out)" ? "#ff0000" : "#000000");

            //populating hidden Fields
            txt_hidden_cr_day.attr("value", res.data.coverage.Day);
            txt_hidden_cr_month.attr("value", res.data.coverage.Month);
            txt_hiden_cr_eventId.attr("value", res.data.coverage.EventID);
            txt_hidden_cr_year.attr("value", res.data.coverage.Year);
            //txt_hidden_cr_Linenum.attr("value", item.LineNum);
            txt_hidden_cr_soId.attr("value", res.data.coverage.EmpIdNo);
            txt_hidden_cr_linenum.attr("value", res.data.coverage.LineNum);
            $('#Logchanges a').attr('href', baseurl + "Calendar/getLogChangesAccount?LineNum=" + res.data.coverage.LineNum);
            $("#Logchanges").show();

            $('#VisitLogs a').attr('href', baseurl + "Calendar/getVisitLogsByAccount?LineNum=" + res.data.coverage.LineNum);
            $("#VisitLogs").show();
            //Collection here

            txt_cr_otherInfo.attr('value', res.data.coverage.OtherInformation);

            $("#txt_cr_postdatedcheck").attr("value", numberWithCommas(res.data.coverage.ColPostDatedCheck));
            $("#txt_cr_datecheck").attr("value", numberWithCommas(res.data.coverage.ColDatedCheck));
            $("#txt_cr_totalcollection").attr("value", numberWithCommas(res.data.coverage.ColTotal));
            $("#txt_cr_colremarks").attr("value", res.data.coverage.ColRemarks);

            //var freqvisit = res.data.coverage.freqVisit != null ? res.data.coverage.freqVisit : "";
            // txt_freqvisit.attr("value", freqvisit);
            if (res.data.coverage.cFullCollection == "T") {
                chk_Fcollection.attr("checked", true);
                chk_Pcollection.removeAttr('checked');
                chk_Ncollection.removeAttr('checked');
            }

            if (res.data.coverage.cPartialCollection == "T") {
                chk_Pcollection.attr("checked", true);
                chk_Fcollection.removeAttr('checked');
                chk_Ncollection.removeAttr('checked');
            }

            if (res.data.coverage.cNoCollection == "T") {
                chk_Ncollection.attr("checked", true);
                chk_Pcollection.removeAttr('checked');
                chk_Fcollection.removeAttr('checked');
            }

            //Customer Service
            if (res.data.coverage.Orders == "T") {
                chk_cs_callrepcomplete.attr("checked", true);
                //  chk_cs_callrepincomplete.removeAttr('checked');
                $("#chk_cs_callrepincomplete").attr("checked", false);
            }
            if (res.data.coverage.Orders == "F") {
                $("#chk_cs_callrepincomplete").attr("checked", true);
                // chk_cs_callrepcomplete.removeAttr('checked');
                $("#chk_cs_callrepcomplete").attr("checked", false);
            }

            if (res.data.coverage.WithOrder == "T") {
                chk_yes.attr("checked", true);
                chk_no.attr("checked", false);
            }
            else {
                chk_yes.attr("checked", false);
                chk_no.attr("checked", true);
            }

            if (res.data.coverage.Delivery == "T") {
                chk_cs_callrepOntime.attr("checked", true);
                // chk_cs_callrepDelay.removeAttr('checked');
                $("#chk_cs_callrepDelay").attr("checked", false);
            }
            if (res.data.coverage.Delivery == "F") {
                chk_cs_callrepDelay.attr("checked", true);
                //  chk_cs_callrepOntime.removeAttr('checked');
                $("#chk_cs_callrepOntime").attr("checked", false);
            }



            txt_cr_nextcalldate.attr("value", res.data.coverage.NextCallDate);
            txt_cr_competitorsAct.attr("value", res.data.coverage.CompetitorActivities);

            txt_cs_callrepSummarylacking.attr("value", res.data.coverage.SummaryLackingItems);
            txt_cs_callrepRemarks.attr("value", res.data.coverage.Remarks);
            txt_cs_callrepRecommend.attr("value", res.data.coverage.Recommendation);

            txt_cs_callrepTimetable.text(res.data.coverage.TimeTable);

            //Merchandising here
            txt_cr_pstorecheck.attr("value", res.data.coverage.StoreChecking);
            var check_res = res.data.coverage.StoreCheckingResult != "" ? res.data.coverage.StoreCheckingResult : "";
            txt_cr_storecheckingres.attr("value", check_res);

            if (res.data.coverage.Attachment != "" && res.data.coverage.Attachment != null && res.data.coverage.Attachment != "null") {
                lnk_view_attach2.attr("href", baseurl + "Calendar/DownLoadFile?filename=" + encodeURIComponent(res.data.coverage.Attachment) + "&DoctypeId=" + res.data.coverage.DoctypeId)

            }
            $(res.data.coverage.Sub_coverage).each(function (index, itm) {
                /**Converts the string into floating point and reduces decimal into two places**/
                var p_amount = itm.PlannedAmount == "" ? "0" : parseFloat(itm.PlannedAmount).toFixed(2);
                var a_amount = itm.ActualAmount == "" ? "0" : parseFloat(itm.ActualAmount).toFixed(2);

                if (res.data.coverage.DocumentStatusId != "0") {
                    if (itm.ObjectiveCode.toUpperCase() == "C") {
                        $("#txt_total_planned").attr("value", p_amount);

                        //call report
                        $("#txt_cr_planned_amount").attr("value", numberWithCommas(p_amount));
                        $('input.auto').autoNumeric();

                        $("#crcollection").addClass("tabcolor");
                    }


                    if (itm.ObjectiveCode.toUpperCase() == "S") {

                        PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        //PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_sales_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/


                        //CALL REPORT
                        PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        $('input.auto').autoNumeric();
                        //PopulateDate(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_cr_salesdtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/

                        $("#crsales").addClass("tabcolor");
                    }

                    if (itm.ObjectiveCode.toUpperCase() == "M") {
                        if (itm.ProductPresented != null & itm.CounterClerk != null & itm.Brand != null) {
                            PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        }
                        // PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_mse_details").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/


                        //CALL REPORT
                        PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        //PopulateDate(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_cr_msdedtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/

                        $("#crmerchandise").addClass("tabcolor");
                    }

                    //if (itm.ObjectiveCode.toUpperCase() == "INV") {
                    //     $("#view_inventory_count_link").append('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + itm.inventoryCountID + '&doctype=invcount" target="new_tab">' + itm.inventoryCountID + '</a> </div>');
                    //     $("#inventory").addClass("tabcolor");
                    // }
                }
                else {
                    if (itm.ObjectiveCode.toUpperCase() == "C") {
                        //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        $("#txt_total_planned").attr("value", p_amount);
                        $('input.auto').autoNumeric();
                        // $("#tbl_collection_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                    }

                    if (itm.ObjectiveCode.toUpperCase() == "S") {
                        PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        $('input.auto').autoNumeric();
                        //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_sales_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/

                    }

                    if (itm.ObjectiveCode.toUpperCase() == "M") {
                        PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                        $('input.auto').autoNumeric();
                        //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                        $("#tbl_mse_details").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/

                    }

                    if (itm.ObjectiveCode.toUpperCase() == "INV") {
                        $("#cr_inventorycount_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + item.AccountCode + '&doctype=invcount" target="dfdsfds">here</a> </div>');

                        $("#crinventory").addClass("tabcolor");
                    }
                }


            });

            $(res.data.coverage.Total_dtls).each(function (index, itn) {
                var c_actualAmount = itn.estActualAmount == "" ? "0" : ReplaceNumberWithCommas(parseFloat(itn.estActualAmount).toFixed(2));
                if (itn.ObjectiveCode == "C") {
                    txt_cr_colestAmount.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                    txt_cr_colactAmount.attr("value", c_actualAmount);
                }
                if (itn.ObjectiveCode == "S") {
                    txt_cr_total_salesEstAmt.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                    txt_cr_total_salesActtAmt.attr("value", c_actualAmount);
                }
            });

            $("#spn_eventstatus").text("Account already exists in itinerary");
            // if (isShowSaveDraftButton == false) {
            //        alert(isShowSaveDraftButton);
            //      div_update.show();
            //      $("#spn_delete").show();
            //      checkbuttonOK();


            //   }
            // else {
            // $("#div_delete").show();
            //}

            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}


function isNotDuplicate(brand) {
    var message = "brand is already in the list";
    var Error = false;
    $('#tbl_sales_dtls tr[class="AddedRow"]').each(function () {
        if ($(this).find('td:eq(1) input').val() == brand) {
            Error = true;
        }
    });

    if (Error) { alert(message); }

    return !Error;
}

function CheckHeaderRequiredFields() {
    var lacking_fields = "";
    if (txt_acctcode.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    if (txt_contactPerson.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Person"; } else { lacking_fields = "Contact Person"; }
    }

    if (txt_ContactNumber.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Number"; } else { lacking_fields = "Contact Number"; }
    }
    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }
}


function CheckcallreportRequiredFields(val) {
    var lacking_fields = "";
    if (val == "1") {
       
        if (txt_cr_brand.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Brand"; } else { lacking_fields = "Brand"; }
        }

        if (txt_cr_pAmt.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Estimated Amount"; } else { lacking_fields = "Estimated Amount"; }
        }

        if (txt_cr_aAmt.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Actual Amount"; } else { lacking_fields = "Actual Amount"; }
        }

        if (txt_cr_collectRemarks.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Remarks"; } else { lacking_fields = "Remarks"; }
        }


        if (lacking_fields != "") {
            alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
            return false;
        }
    }

    if (val = "2") {

        if (txt_cr_brand.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Brand"; } else { lacking_fields = "Brand"; }
        }
        if (txt_cr_pAmt.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Estimated Amount"; } else { lacking_fields = "Estimated Amount"; }
        }
        if (txt_cr_aAmt.attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Actual Amount"; } else { lacking_fields = "Actual Amount"; }
        }
        if (lacking_fields != "") {
            alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
            return false;
        } 
    }
}

function SavePlanEvents(act_type) 
{
    var EventId;
    var collection = new Array();
    var msde = new Array();
    var sales = new Array();
    var customersrvc = new Array();

    var row = $(this).parent().parent();

    //extracting the value from  collection
    $('#tbl_memodetails .AddedRow').each(function () {
        collection.push({
            Objdesc: $(this).find('td:eq(0) input').attr('value'),
            AccountCode: $(this).find('td:eq(1) input').attr('value'),
            AccountName: $(this).find('td:eq(2) input').attr('value'),
            AccountClass: $(this).find('td:eq(3) input').attr('value'),
            AccountAddress: $(this).find('td:eq(4) input').attr('value'),
            ContactPerson: $(this).find('td:eq(5) input').attr('value'),
            ContactNumber: $(this).find('td:eq(5) input').attr('value'),
            ObjectiveCode: $(this).find('td:eq(6) input').attr('value'),
            Brand: $(this).find('td:eq(7) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(8) input').attr('value'))
        });
    });

    //extracting the value from merchandising
    $('#tbl_msde_details .AddedRow').each(function () {
        msde.push({
            Objdesc: $(this).find('td:eq(0) input').attr('value'),
            AccountCode: $(this).find('td:eq(1) input').attr('value'),
            AccountName: $(this).find('td:eq(2) input').attr('value'),
            AccountClass: $(this).find('td:eq(3) input').attr('value'),
            AccountAddress: $(this).find('td:eq(4) input').attr('value'),
            ContactPerson: $(this).find('td:eq(5) input').attr('value')
        });
    });

    //extracting the value from sales
    $('#tbl_sales_details .AddedRow').each(function () {
        sales.push({
            Objdesc: $(this).find('td:eq(0) input').attr('value'),
            AccountCode: $(this).find('td:eq(1) input').attr('value'),
            AccountName: $(this).find('td:eq(2) input').attr('value'),
            AccountClass: $(this).find('td:eq(3) input').attr('value'),
            AccountAddress: $(this).find('td:eq(4) input').attr('value'),
            ContactPerson: $(this).find('td:eq(5) input').attr('value'),
            ContactNumber: $(this).find('td:eq(5) input').attr('value'),
            ObjectiveCode: $(this).find('td:eq(6) input').attr('value'),
            Brand: $(this).find('td:eq(7) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(8) input').attr('value'))
        });
    });

    //extracting the value from customer service
    $('#tbl_customerservice_dtls .AddedRow').each(function () {
        customersrvc.push({
            Objdesc: $(this).find('td:eq(0) input').attr('value'),
            AccountCode: $(this).find('td:eq(1) input').attr('value'),
            AccountName: $(this).find('td:eq(2) input').attr('value'),
            AccountClass: $(this).find('td:eq(3) input').attr('value'),
            AccountAddress: $(this).find('td:eq(4) input').attr('value'),
            ContactPerson: $(this).find('td:eq(5) input').attr('value'),
            ContactPersonNo: $(this).find('td:eq(6) input').attr('value')
        });
    });

    var params = {
        EventID: EventId,
        action_type: act_type,
        Year: Eventyear,
        Month: Eventmonth,
        Day: Eventday,
        collection_list: collection,
        msde_list: msde,
        sales_list: sales,
        customersrvc_list: customersrvc,
        EmpIdNo: userId
    }

  $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        type: "POST", url: baseurl + "Calendar/SavePlanEvents",
        success: function (res) {
            alert("Success!");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });  
}


function GetEventByDate() {
    var new_obj = 
    {
      Eventday : Eventday,
      Eventmonth : Eventmonth,
      Eventyear : Eventyear,
      soId : soId
  }

  $.ajax({
      dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
      type: "POST", url: baseurl + "Calendar/GeteventInfobyDate",
      success: function (res) {
          if (res.data.events.length != undefined) {
              $(res.data.events).each(function (index, item) {
                  if (item.ObjectiveCode.toUpperCase() == "C") {
                      AddItemonCollection(item.AccountCode, item.AccountName, item.AccountClass, item.AccountAddress, item.ContactPerson, item.ContactPersonNo, item.brand, item.Amount)
                  }
                  if (item.ObjectiveCode.toUpperCase() == "M") {
                      AddItemonMerchandise(item.AccountCode, item.AccountName, item.AccountAddress, item.ContactPerson);
                  }
                  if (item.ObjectiveCode.toUpperCase() == "S") {
                      AddItemonSales(item.AccountCode, item.AccountName, item.AccountAddress, item.AccountClass, item.ContactPerson, item.ContactPersonNo, item.brand, item.Amount)
                  }
                  if (item.ObjectiveCode.toUpperCase() == "CS") {
                      AddItemonCustomerSrvc(item.AccountCode, item.AccountName, item.AccountAddress, item.AccountClass, item.ContactPerson, item.ContactPersonNo)
                  }
              });

              $("#doc_stat_msg").html(res.data.docstatus);
              $('#txt_EventId').attr("value", res.data.EventId);
          } else {

          }
      },
      error: function (xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
  });
}

//display the collection list
function AddItemonCollection(Accountcode,Accountname,Accountclass,Accountaddress,Contactperson,Contactnumber,Brand,Amount) 
{
    $("#tbl_memodetails .last_row").before('<tr clone="true"><td><input type="text" readonly="readonly" value="' + Accountcode + '"/></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Accountname + '" /></td>' +
                          '<td><input type="text"readonly="readonly" value="' + Accountclass + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Accountaddress + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactperson + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactnumber + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Brand + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Amount + '" /></td>'+
                          '</tr>');
}
function AddItemonMerchandise(Accountcode, Accountname, Accountaddress, Contactperson) {
    $("#tbl_msde_details .last_row").before('<tr clone="true"><td><input type="text" readonly="readonly" value="' + Accountcode + '"/></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Accountname + '" /></td>' +
                          '<td><input type="text"readonly="readonly" value="' + Accountaddress + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactperson + '" /></td>' +
                          '</tr>');
}

function AddItemonSales(Accountcode, Accountname, Accountaddress, Accountclass, Contactperson, Contactnumber, Brand, Amount) {
    $("#tbl_sales_details .last_row").before('<tr clone="true"><td><input type="text" readonly="readonly" value="' + Accountcode + '"/></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Accountname + '" /></td>' +
                          '<td><input type="text"readonly="readonly" value="' + Accountaddress + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Accountclass + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactperson + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactnumber + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Brand + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Amount + '" /></td>'+
                          '</tr>');

}

function AddItemonCustomerSrvc(AccountCode, AccountName, AccountAddress, AccountClass, ContactPerson, Contactnumber) {
    $("#tbl_customerservice_dtls .last_row").before('<tr clone="true"><td><input type="text" readonly="readonly" value="' + AccountCode + '"/></td>' +
                          '<td><input type="text" readonly="readonly" value="' + AccountName + '" /></td>' +
                          '<td><input type="text"readonly="readonly" value="' + AccountAddress + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + AccountClass + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + ContactPerson + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactnumber + '" /></td>'+
                          '</tr>');

}

//Displaying values under call report
//CR stands for Call Report
function AddItemCRCollection(EventID, ObjectiveCode,AccountCode, AccountName, AccountClass, AccountAddress, ContactPerson, Contactnumber, Brand, Amount, cFullCollection, cPartialCollection, cNoCollection) 
{
    $("#tbl_CRCollection .last_row").before('<tr clone="true" class="AddedRow"><td class="hdn_evntId"><input type="text" readonly="readonly" value="' + EventID + '"/></td>' +
                          '<td class="hdn_evntId"><input type="text" readonly="readonly" value="' + ObjectiveCode + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + AccountCode + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + AccountName + '" /></td>' +
                          '<td><input type="text"readonly="readonly" value="' + AccountClass + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + AccountAddress + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + ContactPerson + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Contactnumber + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Brand + '" /></td>' +
                          '<td><input type="text" readonly="readonly" value="' + Amount + '" /></td>' +
                          '<td align="center"><input type="checkbox" disabled="disabled" ' + (cFullCollection == "T" ? 'checked="checked"' : '') + ' /></td>' +
                          '<td align="center"><input type="checkbox" disabled="disabled" ' + (cPartialCollection == "T" ? 'checked="checked"' : '') + ' /></td>' +
                          '<td align="center"><input type="checkbox" disabled="disabled" ' + (cNoCollection == "T" ? 'checked="checked"' : '') + ' /></td>' +
                          '<td class="td_btn_edit"><img class="btn_edit" src="' + baseurl + 'Images/edit.png" readonly="readonly" /></td>' +
                          '<td class="td_btn_saveEdit"><img class="btn_saveEdit" src="' + baseurl + 'Images/check_icon.png" readonly="readonly" /></td>'+
                          '</tr>');

  hdn_evntId = $("#tbl_CRCollection .AddedRow").find(".hdn_evntId");
  hdn_evntId.hide();
}

function AddPresentorItemOnList(Product,CounterClerk) {
    $("#tbl_mse_callrepProductPres .last_row").before('<tr class="Addedrow"><td><input type="text" value="' + Product + '"/></td>' +
                                                   '<td><td><input type="text" value="' + CounterClerk + '"/></td>' +
                                                   '<td><img class="btn_delete" scr="' + baseurl + 'Images/delete.png"/></td>' +
                                                   '</tr>').prev().find('.btn_delete').click(function () {
                                                       $(this).parent().parent().remove();
                                                   });
                                               }

function GetForCallReport() {

    var new_obj =
    {
        Eventday: Eventday,
        Eventmonth: Eventmonth,
        Eventyear: Eventyear,
        soId: soId,
        ObjectiveCode: "C"
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/GetForCallReport",
        success: function (res) {
            if (res.data.info.length != undefined) {

                $(res.data.info).each(function (index, item) {

                    if (item.ObjectiveCode.toUpperCase() == "C") {
                        var ischecked = "checked";
                        var unchecked = "false";
                        AddItemCRCollection(item.EventID, item.ObjectiveCode, item.AccountCode, item.AccountName, item.AccountClass, item.AccountAddress, item.ContactPerson, item.ContactPersonNo, item.brand, item.Amount, item.cFullCollection, item.cPartialCollection, item.cNoCollection);
                    }
                    if (item.ObjectiveCode.toUpperCase() == "M") { }
                    if (item.ObjectiveCode.toUpperCase() == "S") { }
                    if (item.ObjectiveCode.toUpperCase() == "CS") {  }
                });
                $('.td_btn_saveEdit').hide();

                $('.btn_edit').click(function () {
                    var msge = confirm("Are you sure you want to edit this line?");
                    if (msge == true) {
                        var row = $(this).parent().parent();
                        //hide button edit when click
                        btn_edit = row.find('.td_btn_edit').hide();
                        row.find("input[type=checkbox]").removeAttr('disabled');

                        //change in checkbox
                        row.find('td:eq(10) input').change(function () {
                            if ($(this).is(":checked") == true) {
                                row.find('td:eq(11) input').removeAttr('checked');
                                row.find('td:eq(12) input').removeAttr('checked');
                            }
                        });

                        row.find('td:eq(11) input').change(function () {
                            if ($(this).is(":checked") == true) {
                                row.find('td:eq(10) input').removeAttr('checked');
                                row.find('td:eq(12) input').removeAttr('checked');
                            }
                        });

                        row.find('td:eq(12) input').change(function () {
                            if ($(this).is(":checked") == true) {
                                row.find('td:eq(10) input').removeAttr('checked');
                                row.find('td:eq(11) input').removeAttr('checked');
                            }
                        });

                        btn_saveEdit = row.find('.td_btn_saveEdit').show().find('.btn_saveEdit').click(function () {
                            var selected_id = row.find('td:eq(0) input').attr('value');
                            var selected_objectiveCode = row.find('td:eq(1) input').attr('value');
                            var selected_acctcode = row.find('td:eq(2) input').attr('value');
                            var cFullCollection = row.find('td:eq(10) input').is(':checked') == true ? "T" : "F";
                            var cPartialCollection = row.find('td:eq(11) input').is(':checked') == true ? "T" : "F";
                            var cNoCollection = row.find('td:eq(12) input').is(':checked') == true ? "T" : "F";

                            SaveCallreport(selected_id, selected_objectiveCode, selected_acctcode, cFullCollection, cPartialCollection, cNoCollection);

                            row.find('.td_btn_saveEdit').hide();
                            row.find('.td_btn_edit').show();
                            row.find('input[type=checkbox]').attr('disabled', true);
                        });
                    }
                    else { alert("Canceled"); }
                });
            } else { }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function SaveCallreport(event_id,selected_objectiveCode, AccountCode,cFullCollection, cPartialCollection, cNoCollection) 
{
    var params = {
        EventID: event_id,
        ObjectiveCode: selected_objectiveCode,
        AccountCode: AccountCode,
        Year: Eventyear,
        Month: Eventmonth,
        Day: Eventday,
        cFullCollection: cFullCollection,
        cPartialCollection: cPartialCollection,
        cNoCollection: cNoCollection
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        type: "POST", url: baseurl + "Calendar/SaveCallReport",
        success: function (res) {
            alert("Success!");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function GetForCallReportDtls(LineNum) {
    var new_obj = { LineNum: LineNum }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/getEventDtlList",
        success: function (res) {
            if (res != undefined) {
                $(res).each(function (index, item) {
                    if (item[5].toUpperCase() == "M") {
                        Display_mse_frmDtl1(item[2], item[4]);
                    }

                    if (item[5].toUpperCase() == "S") {
                        Display_sales_frmDtl1(item[2], item[3]);
                    }
                });
                $("#tbl_sales_callrepdetails .last_row").remove();
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function Display_mse_frmDtl1(Brand, Counterclerk) {
    $("#tbl_mse_callrep .last_row").before('<tr class="AddedRow"><td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                  '<td><input type="text"  readonly="readonly" value="' + Counterclerk + '"/></td>' +
                                  '<td><img class="btn_delete" scr="' + baseurl + 'Images/delete.png"/></td>' +
                                                   '</tr>').prev().find('.btn_delete').click(function () {
                                                       $(this).parent().parent().remove();
                                                   });
                                               }

function Display_sales_frmDtl1(Brand, Amount) {
    $("#tbl_sales_callrepdetails .last_row").before('<tr class="AddedRow"><td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                  '<td><input type="text"  readonly="readonly" value="' + Amount + '"/></td>' +
                                   '</tr>');
}

//New Code for Coverage
function SaveCoveragePlan(act_type) {
    var EventId;

    //Declaration of Array for grouping porpuses
    var collection = new Array();
    var msde = new Array();
    var sales = new Array();
    var customersrvc = new Array();
    //Declaration of Header Variable
    var AccountCode = null;
    var ContactPerson = null;
    var ContactPersonNo = null;
    var isPlanned = null;
    var store_check = null;
    var IssuesAndConcerns = null;
    var hotelName = null;
    var hotelContact = null;
    var Attachment = null;
    var casted_msde = new Array();

    //Assiging of Values to header variables
    AccountCode = txt_acctcode.val();
    ContactPerson = txt_contactPerson.val();
    ContactPersonNo = txt_ContactNumber.val();
    store_check = txt_storechecking.val();
    IssuesAndConcerns = txt_issue_concern.val();
    hotelName = txt_hotelname.val();
    hotelContact = txt_hotel_contact.val();
    Attachment = txt_FileAttachment.val(); 
    
    var row = $(this).parent().parent();

    //extracting the value from  collection
   // if ($("#txt_total_planned").attr("value") != "" && $("#txt_total_planned").attr("value") != null) {
        collection.push({
            Objdesc: "Collection"//,
           // Amount: undoAddComma($("#txt_total_planned").attr("value"))
        });
  //  }
    //this msde Array was force implementation of having merchandising as array
    //to facilitate and to integrate the db design to the users deired design 
    $("#tbl_mse_details .AddedRow").each(function () {
        msde.push({
            Objdesc: "Merchandising",
            counterclerk: $(this).find('td:eq(2) input').attr('value'),
            Productpresented: $(this).find('td:eq(1) input').attr('value'),
            CounterClerkNo: $(this).find('td:eq(3) input').attr('value')
        });
    });

    $("#tbl_sales_dtls .AddedRow").each(function () {
        sales.push({
            Objdesc:"Sales",
            Brand: $(this).find('td:eq(1) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
            dtlsRrmks:$(this).find('td:eq(3) input').attr('value')
        });
    });

    customersrvc.push({
        Objdesc: "Customer Service",
        Brand: "",
        Amount: ""
    });

    if (msde.length != 0) {
        casted_msde = msde;
    }
    else {
        if (store_check != null && store_check != "") {
            casted_msde.push({
                Objdesc: "Merchandising",
                Brand: "",
                Amount: ""
            });
        }
    }

    var params = {
        EventID: EventId,
        action_type: act_type,
        Year: Eventyear,
        Month: Eventmonth,
        Day: Eventday,
        EmpIdNo: soId,
        AccountCode: AccountCode,
        ContactPerson: ContactPerson,
        ContactPersonNo: ContactPersonNo,
        collection_list: collection,
        merchandising_list: casted_msde,
        customersrv_list: customersrvc,
        sales_list: sales,
        StoreChecking: store_check,
        IssuesAndConcerns: IssuesAndConcerns,
        HotelName: hotelName,
        HotelContactNum: hotelContact,
        Attachment: Attachment,
        ColPastDueAmount:$("#txt_pastdue_amount").attr("value"),
        CollectibleAmount:$("#txt_collectible_amount").attr("value"),
        ColDueForMonth:$("#txt_dueforthemonth_amount").attr("value"),
        ColSalesAR:$("#txt_salesar_amount").attr("value")
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        type: "POST", url: baseurl + "Calendar/SaveCoveragePlan",
        success: function (res) {
            alert("Success!");
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function undoAddComma(str) {
    var amount = new String(str);
    for (var i = 0; i < amount.length - 1; i++) {
        if (amount.indexOf(",") != -1) {
            amount = amount.replace(",", "");
        }
        else
            break;
    }
    return amount;
}


function numberWithCommas(x) {
    x = Math.round(x == undefined ? 0 : x);
    return x == undefined ? "0" : x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
//get details of the account
function GetCoverageDtls(EventId, Eventmonth, Eventday, Eventyear, soId, accoutcode) {
    var isReadonly = false;
    var new_obj =
    {
        EventId: EventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId:soId,
        accoutcode: accoutcode
    }
    div_callreport.hide();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/GetCoverageInfo",
        success: function (res) {
            if (res != undefined) {
                if (res.data.inventory_objective.hasForInventoryCount)
                    $("#cr_create_inventory_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + accoutcode + '&doctype=newIC" target="new_tab">here</a> </div>');
                if (res.data.inventory_objective.inventoryCountId != "")
                    $("#cr_view_inventory_count_link").html('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + res.data.inventory_objective.inventoryCountId + '&doctype=invcount" target="new_tab">here</a> </div>');



                $(res.data.coverage).each(function (index, item) {
                    if (item.hasCallreport == false) {
                        div_callreport.show();
                        spn_callreport_status.text("For Update");
                        isReadonly = false;
                        //$(".mapcanvas11 > div").show();
                    }
                    else {
                        isReadonly = true;
                        spn_callreport_status.text("Visited");

                        //   $(".mapcanvas11 > div").hide();
                        //   $("#btn_checkin").hide();
                        //   $("#btn_checkout").hide();
                    }
                    EnableDisableTextField(isReadonly);
                    //call report Header details
                    txt_cr_accountaddress.attr("value", item.AccountAddress);
                    txt_cr_accountclass.attr("value", item.AccountClass);
                    txt_cr_accountname.attr("value", item.AccountName);
                    txt_cr_contactperson.attr("value", item.ContactPerson);
                    txt_cr_contactpersonNo.attr("value", item.ContactPersonNo);
                    txt_cs_callrepIssue.attr("value", item.IssuesAndConcerns);

                    var colpastdue = item.ColPastDueAmount == "" || item.ColPastDueAmount == null ? 0 : parseFloat(item.ColPastDueAmount);
                    var collectamount = item.CollectibleAmount == "" || item.ColPastDueAmount == null ? 0 : parseFloat(item.CollectibleAmount);
                    var coldue = item.ColDueForMonth == "" || item.ColPastDueAmount == null ? 0 : parseFloat(item.ColDueForMonth);
                    var colsalesar = item.ColSalesAR == "" || item.ColPastDueAmount == null ? 0 : parseFloat(item.ColSalesAR);

                    $("#txt_cr_postdatedcheck").attr("value", numberWithCommas(item.ColPostDatedCheck));
                    $("#txt_cr_datecheck").attr("value", numberWithCommas(item.ColDatedCheck));
                    $("#txt_cr_totalcollection").attr("value", numberWithCommas(item.ColTotal));
                    $("#txt_cr_colremarks").attr("value", numberWithCommas(item.ColRemarks));

                    $("#txt_cr_total_amount").attr("value", numberWithCommas(colpastdue + collectamount + coldue + colsalesar));

                    //tracker details
                    time_in.text(item.CheckInTime == "" ? "(For Check In)" : item.CheckInTime).css("color", time_in.text() == "(For Check In)" ? "#ff0000" : "#000000");
                    time_out.text(item.CheckOutTime == "" ? "(For Check Out)" : item.CheckOutTime).css("color", time_out.text() == "(For Check Out)" ? "#ff0000" : "#000000");
                    location_in.text(item.CheckInAddress == null ? "(For Check In)" : item.CheckInAddress).css("color", location_in.text() == "(For Check In)" ? "#ff0000" : "#000000");
                    location_out.text(item.CheckOutAddress == null ? "(For Check Out)" : item.CheckOutAddress).css("color", location_out.text() == "(For Check Out)" ? "#ff0000" : "#000000");

                    //populating hidden Feilds

                    txt_hidden_cr_day.attr("value", item.Day);
                    txt_hidden_cr_month.attr("value", item.Month);
                    txt_hiden_cr_eventId.attr("value", item.EventID);
                    txt_hidden_cr_year.attr("value", item.Year);
                    //txt_hidden_cr_Linenum.attr("value", item.LineNum);
                    txt_hidden_cr_soId.attr("value", item.EmpIdNo);
                    txt_hidden_cr_linenum.attr("value", item.LineNum);

               //     $('#callreporttab').tabs('select', 1);
                    $("#callreporttab").tabs({
                        active: 0
                    });
                    $('#Logchanges a').attr('href', baseurl + "Calendar/getLogChangesAccount?LineNum=" + item.LineNum);

                    $('#VisitLogs a').attr('href', baseurl + "Calendar/getVisitLogsByAccount?LineNum=" + item.LineNum);
                    //$("#VisitLogs a")[0].click();
                    //$("#VisitLogs").show();
                    //Collection here

                    var freqvisit = item.freqVisit != null ? item.freqVisit : "";
                    txt_freqvisit.attr("value", freqvisit);

                    //                    if (item.cFullCollection == "T") {
                    //                        chk_Fcollection.attr("checked", true);
                    //                        chk_Pcollection.removeAttr('checked');
                    //                        chk_Ncollection.removeAttr('checked');

                    //                    }
                    //                    if (item.cPartialCollection == "T") {
                    //                        chk_Pcollection.attr("checked", true);
                    //                        chk_Fcollection.removeAttr('checked');
                    //                        chk_Ncollection.removeAttr('checked');

                    //                    }
                    //                    if (item.cNoCollection == "T") {
                    //                        chk_Ncollection.attr("checked", true);
                    //                        chk_Pcollection.removeAttr('checked');
                    //                        chk_Fcollection.removeAttr('checked');
                    //                    }

                    //Customer Service
                    if (item.Orders == "T") {
                        chk_cs_callrepcomplete.attr("checked", true);
                        chk_cs_callrepincomplete.removeAttr('checked');
                    }
                    if (item.Orders == "F") {
                        chk_cs_callrepincomplete.attr("checked", true);
                        chk_cs_callrepcomplete.removeAttr('checked');
                    }

                    if (item.Delivery == "T") {
                        chk_cs_callrepOntime.attr("checked", true);
                        chk_cs_callrepDelay.removeAttr('checked');
                    }
                    if (item.Delivery == "F") {
                        chk_cs_callrepDelay.attr("checked", true);
                        chk_cs_callrepOntime.removeAttr('checked');
                    }

                    txt_cs_callrepSummarylacking.attr("value", item.SummaryLackingItems);
                    txt_cs_callrepRemarks.attr("value", item.Remarks);
                    txt_cs_callrepRecommend.attr("value", item.Recommendation);
                    txt_cs_callrepTimetable.attr("value", item.TimeTable);

                    //Merchandising here
                    txt_cr_pstorecheck.attr("value", item.StoreChecking);
                    var check_res = item.StoreCheckingResult != "" ? item.StoreCheckingResult : "";
                    txt_cr_storecheckingres.attr("value", check_res);

                    if (item.Attachment != "" && item.Attachment != null && item.Attachment != "null") {
                        lnk_view_attach2.attr("href", baseurl + "Calendar/DownLoadFile?filename=" + encodeURIComponent(item.Attachment) + "&DoctypeId=" + item.DoctypeId)
                    }

                    $(item.Sub_coverage).each(function (index, itm) {
                        /**Converts the string into floating point and reduces decimal into two places**/
                        var p_amount = itm.PlannedAmount == "" ? "0" : parseFloat(itm.PlannedAmount).toFixed(2);
                        var a_amount = itm.ActualAmount == "" ? "0" : parseFloat(itm.ActualAmount).toFixed(2);

                        if (itm.ObjectiveCode.toUpperCase() == "C") {
                            //PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            $("#txt_cr_planned_amount").attr("value", numberWithCommas(p_amount));
                            $('input.auto').autoNumeric();
                            //$("#tbl_cr_collection").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                        }
                        if (itm.ObjectiveCode.toUpperCase() == "S") {
                            PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            $('input.auto').autoNumeric();
                            //PopulateDate(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                            $("#tbl_cr_salesdtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                        }
                         
                        if (itm.ObjectiveCode.toUpperCase() == "M") {
                            if (itm.ProductPresented != null & itm.CounterClerk != null & itm.Brand != null) {
                                PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            }
                            //PopulateDate(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                            $("#tbl_cr_msdedtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                        }

                        if (itm.ObjectiveCode.toUpperCase() == "INV") {
                            $("#cr_inventorycount_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + item.AccountCode + '&doctype=invcount" target="dfdsfds">here</a> </div>');
                        }
                    });

                    $(item.Total_dtls).each(function (index, itn) {
                        var c_actualAmount = itn.estActualAmount == "" ? "0" : ReplaceNumberWithCommas(parseFloat(itn.estActualAmount).toFixed(2));
                        if (itn.ObjectiveCode == "C") {
                            txt_cr_colestAmount.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                            txt_cr_colactAmount.attr("value", c_actualAmount);
                        }
                        if (itn.ObjectiveCode == "S") {
                            txt_cr_total_salesEstAmt.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                            txt_cr_total_salesActtAmt.attr("value", c_actualAmount);
                        }
                    });
                });
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

//Coverage details Populator
function PopulateDate(objective, Brand, eAmt, aAmt, remarks, Product, cclerk, cclerkno, dtlsrmks) {
    var EstimatedAmt = eAmt == "" ? "0" : eAmt;
    var ActualAmt = aAmt == "" ? "0" : aAmt;
    var rmrks = remarks == null ? "" : remarks;
    var clerknum = cclerkno == null ? "" : cclerkno;
    var dtls = dtlsrmks == null ? "" : dtlsrmks;

    if (objective.toUpperCase() == "C") {
        $("#tbl_cr_collection .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                  '<td><input type="text" class="auto"  value="' + ReplaceNumberWithCommas(ActualAmt) + '"/></td>' +
                                                  '<td><input type="text"  value="' + rmrks + '"/><td>' +
                                                  '</tr>');

        getElem_lastRowInputted("tbl_cr_collection").find("td:nth-child(4)").find("input[type=text]").forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    }

    if (objective.toUpperCase() == "S") {
        $("#tbl_cr_salesdtl .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;" class="auto"  value="' + ReplaceNumberWithCommas(ActualAmt) + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;" readonly="readonly" value="' + dtls + '"/></td>' +
                                                  '<td><input type="text" style="width:96%;"  value="' + rmrks + '"/></td>' +
                                                  '</tr>');

        getElem_lastRowInputted("tbl_cr_salesdtl").find("td:nth-child(4)").find("input[type=text]").forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    }


    if (objective.toUpperCase() == "M") {
        $("#tbl_cr_msdedtl .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Product + '"/></td>' +
                                                  '<td><input type="text"  value="' + cclerk + '"/></td>' +
                                                  '<td><input type="text"  value="' + clerknum + '"/></td>' +
                                                  '<td><input type="text"  value="' + rmrks + '" readonly="readonly"/></td>' +
                                                  '<td><img class="btn_edit" src="' + baseurl + 'Images/edit.png"/></td>' +
                                                  '</tr>').prev().find('.btn_edit').click(function () {
                                                      var row = $(this).parent().parent();
                                                      var ok = confirm("Are you sure you want to edit this line?");
                                                      if (ok) {
                                                          row.find('td:eq(4) input').removeAttr('readonly');
                                                      }
                                                      else { alert("cancelled"); }
                                                  });
    }
}

//Coverage details Populator
function PopulateonMemodtls(objective, Brand, eAmt, aAmt, remarks, Product, cclerk, cclerkno, dtlsrmks) {
    var EstimatedAmt = eAmt == "" ? "0" : eAmt;
    var ActualAmt = aAmt == "" ? "0" : aAmt;
    var rmrks = remarks == null ? "" : remarks;
    var clerknum = cclerkno == null ? "" : cclerkno;
    var dtls = dtlsrmks == null ? "" : dtlsrmks;


    if (objective.toUpperCase() == "C") {
        $("#tbl_collection_dtls .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                  '</tr>');
    }

    if (objective.toUpperCase() == "S") {
        $("#tbl_sales_dtls .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + dtls + '"/></td>' +
                                                  '</tr>');
    }

    if (objective.toUpperCase() == "M") {
        $("#tbl_mse_details .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Product + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + cclerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + clerknum + '"/></td>' +
                                                  '</tr>');
    }
}

function PopulateonMemodtlsifDraft(objective, Brand, eAmt, aAmt, remarks, Product, cclerk, cclerkno,dtlsrmks) {

    var EstimatedAmt = eAmt == "" ? "0" : eAmt;
    var ActualAmt = aAmt == "" ? "0" : aAmt;
    var rmrks = remarks == null ? "" : remarks;
    var clerknum = cclerkno == null ? "" : cclerkno;

    if (objective.toUpperCase() == "C") {
        $("#tbl_collection_dtls .last_row").before('<tr class="AddedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" class="auto"  value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                   '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              var c_totalamt = parseFloat(undoAddComma(txt_total_collection.val()));
                                                             
                                                              row = $(this).parent().parent();
                                                              var deducAmt = parseInt(undoAddComma(row.find('td:eq(2) input').attr("value")));
                                                              txt_total_collection.attr("value", ReplaceNumberWithCommas(parseInt(parseInt(undoAddComma(c_totalamt)) - parseInt(deducAmt)).toFixed(2))); 
                                                                                                              
                                                              $(this).parent().parent().remove();
                                                          });
    }

    if (objective.toUpperCase() == "S") {
        $("#tbl_sales_dtls .last_row").before('<tr class="AddedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" class="auto"  value="' + ReplaceNumberWithCommas(EstimatedAmt) + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + dtlsrmks + '"/></td>' +
                                                   '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {

                                                              var c_totalamt = parseFloat(undoAddComma(txt_total_sales.val()));

                                                              row = $(this).parent().parent();
                                                              var deducAmt = parseInt(undoAddComma(row.find('td:eq(2) input').attr("value")));
                                                              txt_total_sales.attr("value", ReplaceNumberWithCommas(parseInt(parseInt(undoAddComma(c_totalamt)) - parseInt(deducAmt)).toFixed(2))); 
                                                              $(this).parent().parent().remove();
                                                          });
    }

    if (objective.toUpperCase() == "M") {
        $("#tbl_mse_details .last_row").before('<tr class="AddedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Product + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + cclerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + clerknum + '"/></td>' +
                                                   '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              $(this).parent().parent().remove();
                                                          });

    }
}

//this function Adds on collection list of brands and amount
//if there will be situation that their are collections
//for particular brands was not included on the coverage

function AddCollectionList(brand, pAmt, aAmt, remarks) {

    if (brand != null && brand != "" && pAmt != null && pAmt != "" && aAmt != null && aAmt != "") {
        $("#tbl_cr_collection .last_row").before('<tr class="UnPlanned"><td class="hiddenTd"><input type="text" readonly="readonly" value="C"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + pAmt + '"/></td>' +
                                                  '<td><input type="text"  value="' + aAmt + '"/></td>' +
                                                  '<td><input type="text"  value="' + remarks + '"/></td>' +
                                                  '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              var row = $(this).parent().parent();
                                                              var EstimatedAmt = row.find('td:eq(2) input').attr("value");
                                                              var ActaulAmt = row.find('td:eq(3) input').attr("value");

                                                              txt_cr_colestAmount.attr("value", ReplaceNumberWithCommas(parseFloat(undoAddComma(txt_cr_colestAmount.val())) - parseFloat(undoAddComma(EstimatedAmt))));
                                                              txt_cr_colactAmount.attr("value", ReplaceNumberWithCommas(parseFloat(undoAddComma(txt_cr_colactAmount.val())) - parseFloat(undoAddComma(ActaulAmt))));
                                                              $(this).parent().parent().remove();
                                                          });

                                                          txt_cr_colestAmount.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_colestAmount.val())) + parseFloat(undoAddComma(pAmt))).toFixed(2)));
                                                          txt_cr_colactAmount.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_colactAmount.val())) + parseFloat(undoAddComma(aAmt))).toFixed(2)));

                                                          $("#tbl_cr_collection").find(".hiddenTd").hide();
        //clear fields
        txt_cr_aAmt.attr("value", "");
        txt_cr_brand.attr("value", "");
        txt_cr_pAmt.attr("value", "");
        txt_cr_collectRemarks.attr("value", "");

        getElem_lastRowInputted("tbl_cr_collection").find("td:nth-child(4)").find("input[type=text]").forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    }
    else {
        if (CheckcallreportRequiredFields("2") == false) { return; }
    }
}

function AddMsdeOnList(Brand, Cclerk, cclerknum, actype,Remarks) {

    if (actype == "CALLREPORT") {
        if (Brand != null && Brand != "" && Cclerk != null && Cclerk != "" && cclerknum != null && cclerknum != "") {
            $("#tbl_cr_msdedtl .last_row").before('<tr class="unPlanned"><td class="hiddenTd"><input type="text" readonly="readonly" value="M"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Cclerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + cclerknum + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Remarks + '"/></td>' +
                                                  '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              $(this).parent().parent().remove();
                                                          });
            $("#tbl_cr_msdedtl").find(".hiddenTd").hide();
            //clear fields
            txt_cr_productpresented.attr("value", "");
            txt_cr_counterclerk.attr("value", "");
            txt_cr_mobileno.attr("value", "");
            txt_cr_remarks.attr("value", "");
        }
    }

    if (actype == "COVERAGE") {
        if (Brand != null && Brand != "" && Cclerk != null && Cclerk != "" && cclerknum != null && cclerknum != "") {
            $("#tbl_mse_details .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="M"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Cclerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + cclerknum + '"/></td>' +
                                                  '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {
                                                              $(this).parent().parent().remove();
                                                          });

                                                          $("#tbl_mse_details").find(".hiddenTd").hide();

            //clear fields
            txt_mse_productpresented.attr("value", "");
            txt_mse_counterclerk.attr("value", "");
            txt_mse_mobileno.attr("value", "");
        }
    }
}

function AddSalesList(brand, pAmt, aAmt, remarks,details) {
    if (brand != null && brand != "" && pAmt != null && pAmt != "" && aAmt != null && aAmt != "") {
        $("#tbl_cr_salesdtl .last_row").before('<tr class="UnPlanned"><td class="hiddenTd"><input type="text" readonly="readonly" value="S"/></td>' +
                                                  '<td><input type="text" style="width:98%;" readonly="readonly" value="' + brand + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;"  readonly="readonly" value="' + pAmt + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;"  value="' + aAmt + '"/></td>' +
                                                  '<td><input type="text" style="width:98%;"  value="' + details + '"/></td>' +
                                                  '<td><input type="text" style="width:96%;"  value="' + remarks + '"/></td>' +
                                                  '<td><img class="btn_delete" src ="' + baseurl + 'Images/delete.png"/></td>' +
                                                          '</tr>').prev().find('.btn_delete').click(function () {

                                                              var row = $(this).parent().parent();
                                                              var EstimatedAmt = row.find('td:eq(2) input').attr("value");
                                                              var ActaulAmt = row.find('td:eq(3) input').attr("value");

                                                              txt_cr_total_salesActtAmt.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_total_salesActtAmt.val())) - parseFloat(undoAddComma(ActaulAmt))).toFixed(2)));
                                                              txt_cr_total_salesEstAmt.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_total_salesEstAmt.val())) - parseFloat(undoAddComma(EstimatedAmt))).toFixed(2)));

                                                              $(this).parent().parent().remove();

                                                          });

                                                          $("#tbl_cr_salesdtl").find(".hiddenTd").hide();

                                                          txt_cr_total_salesEstAmt.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_total_salesEstAmt.val())) + parseFloat(undoAddComma(pAmt))).toFixed(2)));
                                                          txt_cr_total_salesActtAmt.attr("value", ReplaceNumberWithCommas(parseFloat(parseFloat(undoAddComma(txt_cr_total_salesActtAmt.val())) + parseFloat(undoAddComma(aAmt))).toFixed(2)));
        //clear fields
        txt_cr_salesBrand.attr("value", "");
        txt_cr_salesEstimatedAmt.attr("value", "");
        txt_cr_salesActualAmt.attr("value", "");
        txt_cr_salesremarks.attr("value", "");
        txt_cr_details.attr("value", "");

        getElem_lastRowInputted("tbl_cr_salesdtl").find("td:nth-child(4)").find("input[type=text]").forceNumeric({ 'allowNegative': true, 'allowDecimal': true, 'decimalPlaces': 2 });
    }
    else {
        alert("Fields cannot be empty");
    }
}

function getElem_lastRowInputted(tableId) {
    return $('#' + tableId).find("tr:gt(\"" + $('#' + tableId + ' tbody').find('tbody tr:gt(1)').length + "\")");
}

function UpdateForCallReport() {
    var cFullCollection = $("#chk_Fcollection").is(":checked") == true ? "T" : "F";
    var cPartialCollection = $("#chk_Pcollection").is(":checked") == true ? "T" : "F";
    var cNoCollection = $("#chk_Ncollection").is(":checked") == true ? "T" : "F";
    var mos = txt_hidden_cr_month.val();
    var day = txt_hidden_cr_day.val();
    var year = txt_hidden_cr_year.val();
    var emp_id = txt_hidden_cr_soId.val();
    var event_id = txt_hiden_cr_eventId.val();
    var AccountCode = txt_cr_accountCode.val();
    var Linenum = txt_hidden_cr_linenum.val();
    var StoreCheckingResult = txt_cr_storecheckingres.val();
    var SumLacking = txt_cs_callrepSummarylacking.val();
    var CustomerRemarks = txt_cs_callrepRemarks.val();
    var Recomendation = txt_cs_callrepRecommend.val();
    var Timetbl = txt_cs_callrepTimetable.val();
    var CompActivity = txt_cr_competitorsAct.val();
    var nextcalldate = txt_cr_nextcalldate.val();
    var otherInfo = txt_cr_otherInfo.val();
    var StoreChecking = txt_cr_pstorecheck.val();
    var IssuesAndConcerns = txt_cs_callrepIssue.val();
    var freqvisit = txt_freqvisit.val();
    var timein = txt_timein.val();
    var timeout = txt_timeout.val();

    var ContactPerson = txt_cr_contactperson.val();
    var ContactPersonNo = txt_cr_contactpersonNo.val();

    var datecheck = undoAddComma(txt_cr_datecheck.val());
    var postdatedcheck = undoAddComma(txt_cr_postdatedcheck.val());
    var totalcollection = undoAddComma(txt_cr_totalcollection.val());
    var colremarks = txt_cr_colremarks.val();

   
    var deliveryVal = "";
    var ordrStatus = "";
    var withorder = "";

    //Check Box Under Customer Service CallReport:

    //cehck if checkbox on time is check
    if ($("#chk_cs_callrepOntime").is(":checked") == true) {deliveryVal = "T";}
    //cehck if checkbox Delay is check
    else if ($("#chk_cs_callrepDelay").is(":checked") == true) { deliveryVal = "F"; }
    //set Null value or blank value no chosen checkbox
    else { deliveryVal = ""; }

    //cehck if checkbox Complete is check
    if ($("#chk_cs_callrepcomplete").is(":checked") == true) {ordrStatus = "T";}
    //cehck if checkbox Incomplete is check
    else if ($("#chk_cs_callrepincomplete").is(":checked") == true) { ordrStatus = "F"; }
    //set Null value or blank value no chosen checkbox
    else { ordrStatus = ""; }

    //Check box under Sales   Call report
    if ($("#chk_yes").is(":checked") == true) {withorder = "T";}

    else if ($("#chk_no").is(":checked") == true) {withorder = "F";}              
    else { withorder = ""; }
    
    var collection_list = new Array();
    var uncollection_list = new Array();
    var mse_present = new Array();
    var unmse_present = new Array();
    var unsales = new Array();
    var sales_list = new Array();
    var customersrvc = new Array();
    var hotelName = null;
    var hotelContact = null;
    var casted_unmse = new Array();

//    $("#tbl_cr_collection .UnPlanned").each(function () {
//        uncollection_list.push({
//            ObjectiveCode: $(this).find('td:eq(0) input').attr('value'),
//            Brand: $(this).find('td:eq(1) input').attr('value'),
//            Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
//            ActualAmount: undoAddComma($(this).find('td:eq(3) input').attr('value')),
//            Remarks:$(this).find('td:eq(4) input').attr('value')
//        });
    //    });

    if (($("#txt_cr_datecheck").attr("value") != "" && $("#txt_cr_datecheck").attr("value") != null) || ($("#txt_cr_postdatedcheck").attr("value") != "" && $("#txt_cr_postdatedcheck").attr("value") != null)) {

           //uncollection_list.push({
        collection_list.push({
            ObjectiveCode: "C"//,
            // Brand: $(this).find('td:eq(1) input').attr('value'),
            //Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
            //ActualAmount: undoAddComma($("#txt_cr_colactual_amount").attr("value"))//$(this).find('td:eq(3) input').attr('value')),
            //Remarks: $(this).find('td:eq(4) input').attr('value')
        });
    }
//    else if ($("#txt_cr_colactual_amount").attr("value") != "" && $("#txt_cr_colactual_amount").attr("value") != null) {
//        collection_list.push({
//            ObjectiveCode: "C",
//            // Brand: $(this).find('td:eq(1) input').attr('value'),
//            //  Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
//            ActualAmount: undoAddComma($("#txt_cr_colactual_amount").attr("value"))//$(this).find('td:eq(3) input').attr('value')),
//            //  Remarks: $(this).find('td:eq(4) input').attr('value')
//        });
//    }
   // });

    $("#tbl_cr_msdedtl .addedRow").each(function () {
        mse_present.push({
            ObjectiveCode: $(this).find('td:eq(0) input').attr('value'),
            Productpresented: $(this).find('td:eq(1) input').attr('value'),
            counterclerk: $(this).find('td:eq(2) input').attr('value'),
            CounterClerkNo:$(this).find('td:eq(3) input').attr('value'),
            Remarks: $(this).find('td:eq(4) input').attr('value')
        });
    });

    $("#tbl_cr_msdedtl .unPlanned").each(function () {
        unmse_present.push({
            ObjectiveCode: $(this).find('td:eq(0) input').attr('value'),
            Productpresented: $(this).find('td:eq(1) input').attr('value'),
            counterclerk: $(this).find('td:eq(2) input').attr('value'),
            CounterClerkNo: $(this).find('td:eq(3) input').attr('value'),
            Remarks: $(this).find('td:eq(4) input').attr('value')
        });
    });

    $("#tbl_cr_salesdtl .addedRow").each(function () {
        sales_list.push({
            ObjectiveCode: $(this).find('td:eq(0) input').attr('value'),
            Brand: $(this).find('td:eq(1) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
            ActualAmount: undoAddComma($(this).find('td:eq(3) input').attr('value')),
            Remarks: $(this).find('td:eq(4) input').attr('value')
        });
    });

    $("#tbl_cr_salesdtl .UnPlanned").each(function () {
        unsales.push({
            ObjectiveCode: $(this).find('td:eq(0) input').attr('value'),
            Brand: $(this).find('td:eq(1) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
            ActualAmount: undoAddComma($(this).find('td:eq(3) input').attr('value')),
            dtlsRrmks: $(this).find('td:eq(4) input').attr('value'),
            Remarks: $(this).find('td:eq(5) input').attr('value')
        });
    });

    customersrvc.push({
        Objdesc: "Customer Service",
        Brand: "",
        Amount: ""
    });

    if (unmse_present.length != 0) {
        casted_unmse = unmse_present;
    }
    else {

        if ((StoreChecking != null || StoreCheckingResult != null) && (StoreChecking != "" || StoreCheckingResult != "")) {
            casted_unmse.push({
                Objdesc: "Merchandising",
                Brand: "",
                Amount: "",
                ObjectiveCode: "M"
            });
        }
    }

    var new_obj = {
        Month: Eventmonth, //mos,
        Year: Eventyear, //year,
        Day: Eventday, //day,
        EmpIdNo: soId,
        EventID: event_id,
        cFullCollection: cFullCollection,
        cPartialCollection: cPartialCollection,
        cNoCollection: cNoCollection,
        StoreCheckingResult: StoreCheckingResult,
        AccountCode: AccountCode,
        LineNum: Linenum,
        Delivery: deliveryVal,
        Orders: ordrStatus,
        StoreChecking: StoreChecking,
        IssuesAndConcerns: IssuesAndConcerns,
        SummaryLackingItems: SumLacking,
        Recommendation: Recomendation,
        TimeTable: Timetbl,
        Remarks: CustomerRemarks,
        CompetitorActivities: CompActivity,
        WithOrder: withorder,
        NextCallDate: nextcalldate,
        OtherInformation: otherInfo,
        Numvisit: freqvisit,
        collection_list: collection_list,
        uncollection_list: uncollection_list,
        merchandising_list: mse_present,
        unmerchandising_list: casted_unmse, //unmse_present,
        unsales_list: unsales,
        sales_list: sales_list,
        customersrv_list: customersrvc,
        HotelName: hotelName,
        HotelContactNum: hotelContact,
        ColPostDatedCheck: postdatedcheck,
        ColDatedCheck: datecheck,
        ColTotal: totalcollection,
        ColRemarks: colremarks,
        ContactPerson : ContactPerson,
        ContactPersonNo : ContactPersonNo
    };

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/UpdateCallreports",
        success: function (res) {

            alert("success");

            if ($('#txtAttachment').attr('value').replace(/\s/g, '') != "") {
                $('#formUpload').attr('action', baseUrl + "Calendar/UploadAttachment?EventId=" + event_id + "&LineNum=" + Linenum + "&empId=" + soId);
                $('#formUpload').submit();
            }

            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

//functiom load look ups
function LoadLookUps() {
    var isplanned = chk_isunPlanned.is(":checked") == true ? "T" : "F";

    if (isplanned == "T") {
        txt_cr_accountname.lookupTextField("T");
    }
    else {
        txt_cr_accountname.lookupTextField("F");
    }
}

$.fn.AutoComma = function () {
    $(this).keypress(function (event) {
        // skip for arrow keys
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }
        var $this = $(this);
        var num = $this.val().replace(/,/g, '');
        //var num = $this.val();
        // the following line has been simplified. Revision history contains original.
        //$this.val(num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
        $this.val(num.replace(/\B(?=(?:\d{3})+(?!\d))/g, ","));

    });
}

function getTRFfromSAP(f_eventId, soId, Eventday, Eventmonth, Eventyear, accoutcode) {
    var new_obj =
    {
        EventId: f_eventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId: soId,
        acctCode: accoutcode
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/getTRFfromSAP",
        success: function (res) {

            var hasRow = false;
            $("#tbl_collection_dtls tr[clone=\"true\"]").remove();
            $(res.data.collection_objective_lookupfromsap).each(function (index, elem) {
                $("#tbl_collection_dtls .last_row").before('<tr clone="true">' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.docnum + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.actDelDate + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" value="' + elem.DueDate + '" /></td>' +
                                                           '<td><input type="text" readonly="readonly" class="fld_amount" value="' + addCommas(elem.balance) + '" /></td>' +
                                                           '</tr>');
                hasRow = true;
            });
            if (!hasRow) {
                $("#tbl_collection_dtls .last_row").before('<tr clone="true">' +
                                                           '<td colspan="6" style="width:100%; text-align:center"><input type="text" style="width:98%; text-align:center" value="No Data" /></td>' +
                                                           '</tr>');
            }
            $("#txt_totalcollection").attr("value", addCommas(res.data.total_collection_objective_lookupfromsap));

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr); alert(thrownError);
        }
    });
}

function SaveCoveragePlanChanges(act_type, Deleted) {
    var EventId;

    //Declaration of Array for grouping porpuses
    var collection = new Array();
    var msde = new Array();
    var sales = new Array();
    var customersrvc = new Array();
    //Declaration of Header Variable
    var AccountCode = null;
    var ContactPerson = null;
    var ContactPersonNo = null;
    var isPlanned = null;
    var store_check = null;
    var IssuesAndConcerns = null;
    var hotelName = null;
    var hotelContact = null;
    //var isDeleted = chk_delete.is(':checked') == true ? "T" : "F";
    var isDeleted = Deleted;
    var attachment2 = null;
    var casted_msde = new Array();

    //Assiging of Values to header variables
    AccountCode = txt_acctcode.val();
    ContactPerson = txt_contactPerson.val();
    ContactPersonNo = txt_ContactNumber.val();
    store_check = txt_storechecking.val();
    IssuesAndConcerns = txt_issue_concern.val();
    hotelName = txt_hotelname.val();
    hotelContact = txt_hotel_contact.val();
    attachment2 = txt_FileAttachment2.val();

    var row = $(this).parent().parent();
    //extracting the value from  collection

    //$('#tbl_collection_dtls .AddedRow').each(function () {
   // if ($("#txt_total_planned").attr("value") != "" && $("#txt_total_planned").attr("value") != null) {
        collection.push({
            Objdesc: "Collection"//,
          //  Amount: undoAddComma($("#txt_total_planned").attr("value"))
        });
   // }
    //});

    //this msde Array was force implementation of having merchandising as array
    //to facilitate and to integrate the db design to the users deired design 

    $("#tbl_mse_details .AddedRow").each(function () {
        msde.push({
            Objdesc: "Merchandising",
            counterclerk: $(this).find('td:eq(2) input').attr('value'),
            Productpresented: $(this).find('td:eq(1) input').attr('value'),
            CounterClerkNo: $(this).find('td:eq(3) input').attr('value')

        });
    });

    $("#tbl_sales_dtls .AddedRow").each(function () {
        sales.push({
            Objdesc: $(this).find('td:eq(0) input').attr('value'),
            Brand: $(this).find('td:eq(1) input').attr('value'),
            Amount: undoAddComma($(this).find('td:eq(2) input').attr('value')),
            dtlsRrmks: $(this).find('td:eq(3) input').attr('value')

        });
    });

    customersrvc.push({
        Objdesc: "Customer Service",
        Brand: "",
        Amount: ""
    });

    if (msde.length != 0) {
        casted_msde = msde;
    }
    else {
        if (store_check != null && store_check != "") {
            casted_msde.push({
                Objdesc: "Merchandising",
                Brand: "",
                Amount: ""
            });
        }
    }
    var params = {
        EventID: EventId,
        action_type: act_type,
        Year: Eventyear,
        Month: Eventmonth,
        Day: Eventday,
        EmpIdNo: soId,
        AccountCode: AccountCode,
        ContactPerson: ContactPerson,
        ContactPersonNo: ContactPersonNo,
        collection_list: collection,
        merchandising_list: casted_msde,
        customersrv_list: customersrvc,
        sales_list: sales,
        StoreChecking: store_check,
        IssuesAndConcerns: IssuesAndConcerns,
        HotelName: hotelName,
        HotelContactNum: hotelContact,
        isDeleted: isDeleted,
        Attachment: attachment2,
        ColPastDueAmount:$("#txt_pastdue_amount").attr("value"),
        CollectibleAmount:$("#txt_collectible_amount").attr("value"),
        ColDueForMonth:$("#txt_dueforthemonth_amount").attr("value"),
        ColSalesAR:$("#txt_salesar_amount").attr("value")
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        type: "POST", url: baseurl + "Calendar/SaveCoveragePlanChanges",
        success: function (res) {
            alert("Success!");
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


function LoadTrappings() {
    var new_obj =
    {
        Eventmonth: Eventmonth,
        Eventyear: Eventyear,
        soId: soId
    }
    div_saveDraft.show();
    //$("#div_delete").hide();
    div_delete.hide();

    $("#spn_delete").hide();

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/getstatus",
        success: function (res) {


            if (res != undefined) {
                $(res.data.coveragestatus).each(function (index, item) {
                    // alert("Entering LoadTrappings" + item.stateDesc);

                    cls_excel_file.hide(); //added by billy jay delima
                    div_saveDraft.hide();
                    $("#div_delete").hide();

                    //                    if (item.stateDesc.toUpperCase() == "FOR ASM APPROVAL") {
                    //                        cls_excel_file.hide(); //added by billy jay delima
                    //                        div_saveDraft.hide();
                    //                        $("#div_delete").hide();
                    //                    }
                    //                    if (item.stateDesc.toUpperCase() == "DISAPPROVED") {
                    //                        div_saveDraft.hide();
                    //                        $("#div_delete").hide();
                    //                        cls_excel_file.hide(); //added by billy jay delima
                    //                    }
                    div_update.hide();
                    if (item.stateDesc.toUpperCase() == "RETURNED BY ASM" || item.stateDesc.toUpperCase() == "RETURNED BY CHANNEL MANAGER" || item.stateDesc.toUpperCase() == "RETURNED BY VP-SALES" || item.stateDesc.toUpperCase() == "RETURNED BY RSM") {
                        cls_excel_file.show(); //added by billy jay delima
                        div_saveDraft.show();
                        $("#div_delete").hide();
                    }

                    else if (item.stateDesc.toUpperCase() == "DRAFT") {
                        div_saveDraft.show();
                        $("#div_delete").show();
                        cls_excel_file.show(); //added by billy jay delima
                    }

                    else if (item.stateDesc.toUpperCase() == "APPROVED") {
                        //     cls_excel_file.hide(); //added by billy jay delima
                        //     div_saveDraft.hide();
                        //      $("#div_delete").hide();

                        if (CheckEditTimeAllowed() == true) {
                            div_update.show();
                            // div_edit.show();
                        }
                    }

                    else if (item.stateDesc.toUpperCase() == "AMENDED(DISAPPROVED)" || item.stateDesc.toUpperCase() == "AMENDED(APPROVED)" ||
                             item.stateDesc.toUpperCase() == "AMENDED(FOR ASM APPROVAL)" || item.stateDesc.toUpperCase() == "AMENDED(FOR CHANNEL MANAGER APPROVAL)" || item.stateDesc.toUpperCase() == "AMENDED (FOR RSM APPROVAL)" ||
                             item.stateDesc.toUpperCase() == "AMENDED (FOR VP-SALES APPROVAL)") {
                        //     cls_excel_file.hide(); //added by billy jay delima
                        //     div_saveDraft.hide();
                        //      $("#div_delete").hide();

                        if (CheckEditTimeAllowed() == true) {
                            div_update.show();
                            // div_edit.show();
                        }
                    }

                    //  if (item.stateDesc.toUpperCase() == "AMENDED(APPROVED)") {
                    //      div_saveDraft.hide();
                    //      $("#div_delete").hide();
                    //  isShowSaveDraftButton = false;
                    //  }
                    //    if (item.stateDesc.toUpperCase() == "AMENDED(FOR ASM APPROVAL)") {
                    //      div_saveDraft.hide();
                    //      $("#div_delete").hide();
                    ///       // isShowSaveDraftButton = false;
                    //  }
                });
            } //else {
            //  isShowSaveDraftButton = true;
            // }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });



}


//work around to get the eventId

function getEventId(soId, Eventmonth, Eventyear) {
    var new_obj =
    {
        Eventmonth: Eventmonth,
        Eventyear: Eventyear,
        soId: soId
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/getEventId",
        success: function (res) {
            if (res != undefined) {
                f_eventId = res;
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

//this function is a validation whether this particular date contains this particular account.
function checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, accoutcode) {
    var new_obj =
    {
        EventId: f_eventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId: soId,
        accoutcode: accoutcode
    }
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/GetCoverageInfo",
        success: function (res) {
            if (res != undefined) {
                if (res.data.inventory_objective.hasForInventoryCount) {
                    $("#create_inventory_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + accoutcode + '&doctype=newIC" target="new_tab">here</a> </div>');
                    $("#inventory").addClass("tabcolor");
                }
                if (res.data.inventory_objective.inventoryCountId != "") {
                    $("#view_inventory_count_link").html('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + res.data.inventory_objective.inventoryCountId + '&doctype=invcount" target="new_tab">here</a> </div>');
                    $("#inventory").addClass("tabcolor");
                }
                //  LoadTrappings();
                if (res.data.coverage != 0) {
                    //alert("Account already exist in itinerary!");

                    $("#spn_eventstatus").text("Account already exists in itinerary");
                    $("#spn_delete").show();
                    // if (isShowSaveDraftButton == false) {
                    //     div_update.show();
                    //     $("#spn_delete").show();
                    //     checkbuttonOK();
                    //  }

                    txt_freqvisit.attr("value", res.data.number_of_visits);

                    $(res.data.coverage).each(function (index, item) {
                        txt_storechecking.attr("value", item.StoreChecking);
                        txt_issue_concern.attr("value", item.IssuesAndConcerns);

                        txt_hidden_eventId.attr("value", item.EventID);
                        txt_hidden_linenum.attr("value", item.LineNum);
                        $('#routeChanges a').attr('href', baseurl + "Calendar/getRoutechangesbyAccount?LineNum=" + item.LineNum);

                        var rowtotal = 0;
                        $(item.Sub_coverage).each(function (index, itm) {
                            var p_amount = itm.PlannedAmount == "" ? "0" : parseFloat(itm.PlannedAmount).toFixed(2);
                            var a_amount = itm.ActualAmount == "" ? "0" : parseFloat(itm.ActualAmount).toFixed(2);
                            if (item.DocumentStatusId != "0") {

                                if (itm.ObjectiveCode.toUpperCase() == "C") {
                                    //  PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    // PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                                    $("#txt_total_planned").attr("value", p_amount);
                                    // $("#tbl_collection_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }
                                if (itm.ObjectiveCode.toUpperCase() == "S") {
                                    PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    //PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                                    $("#tbl_sales_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }

                                if (itm.ObjectiveCode.toUpperCase() == "M") {
                                    PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    // PopulateonMemodtls(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                                    $("#tbl_mse_details").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }

                                if (itm.ObjectiveCode.toUpperCase() == "INV") {
                                    $("#view_inventory_count_link").append('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + itm.inventoryCountID + '&doctype=invcount" target="new_tab">' + itm.inventoryCountID + '</a> </div>');
                                    $("#inventory").addClass("tabcolor");
                                }

                                $(item.Total_dtls).each(function (index, itn) {
                                    if (itn.ObjectiveCode == "C") {
                                        txt_total_collection.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                                    }
                                    if (itn.ObjectiveCode == "S") {
                                        txt_total_sales.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                                    }
                                });
                            }
                            else {

                                if (itm.ObjectiveCode.toUpperCase() == "C") {
                                    //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    $("#txt_total_planned").attr("value", p_amount);
                                    $('input.auto').autoNumeric();
                                    // $("#tbl_collection_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }

                                if (itm.ObjectiveCode.toUpperCase() == "S") {
                                    PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    $('input.auto').autoNumeric();
                                    //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                                    $("#tbl_sales_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }

                                if (itm.ObjectiveCode.toUpperCase() == "M") {
                                    PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                                    $('input.auto').autoNumeric();
                                    //PopulateonMemodtlsifDraft(itm.ObjectiveCode, itm.Brand, itm.PlannedAmount, itm.ActualAmount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo);
                                    $("#tbl_mse_details").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                                }

                                //this is a work around function just to display total per account under from what particular objective
                                $(item.Total_dtls).each(function (index, itn) {
                                    if (itn.ObjectiveCode == "C") {
                                        txt_total_collection.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                                    }
                                    if (itn.ObjectiveCode == "S") {
                                        txt_total_sales.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                                    }
                                });
                            }
                        });

                        if (item.DocumentStatusId == "0") {
                            div_delete.show();
                        }

                    });
                }
                else {

                    $("#spn_eventstatus").text("NEW");
                    $("#spn_delete").hide();
                    div_delete.hide();
                    // if (isShowSaveDraftButton == false) {
                    //     div_update.show();
                    //     $("#spn_delete").hide();
                    //      checkbuttonOK();
                    //  }
                }
            } else {
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


function DeleteAccount(Account, month, day, year,soId,EventId, Linenum) {
    var new_obj = {
       AccountCode: Account,
       Month: month,
       Day: day,
       Year: year,
       EventID: EventId,
       LineNum:Linenum,
       EmpIdNo: soId,
       action_type:"DELETE"
   }

   $.ajax({
       contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
       type: "POST", url: baseurl + "Calendar/DELETECoveragePlan",
       success: function (res) {
           alert("Success!");
           ClearFieldsCoverage();
          // location.reload();
       },
       error: function (xhr, ajaxOptions, thrownError) {
           alert(xhr.status); alert(thrownError);
       }
   });
}



function checkDaylapseForCallreport() {
    var curr_date = new Date();
    var cur_day = curr_date.getDate();
    var cur_month = curr_date.getMonth();
    var cur_year = curr_date.getYear();
    var pcurr_date = (parseInt(cur_month) + 1) + "/" + cur_day + "/" + cur_year;
    var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
    var p_date = new Date(p_forceformateddate);
    var fcur_date = new Date(pcurr_date);

    var days = 24 * 60 * 60 * 1000;

    var datediff = /**p_date.getTime()-*/fcur_date.getTime() - p_date.getTime();
    var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

    if (interval == 0) {
        return true;
    }
    else { return false; }

}

function CheckEditTimeAllowed() {
    var curr_date = new Date();
    var cur_day = curr_date.getDate();
    var cur_month = curr_date.getMonth();
    var cur_year = curr_date.getYear();
    var pcurr_date = (parseInt(cur_month) + 1) + "/" + cur_day + "/" + cur_year;
    var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
    var p_date = new Date(p_forceformateddate);
    var fcur_date = new Date(pcurr_date);

    var days = 24 * 60 * 60 * 1000;
    var datediff = /**p_date.getTime()-*/p_date.getTime() - fcur_date.getTime();
    var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

    if (interval >= 2) {
        return true;
    }
    else { return false; }

}

function CreateUploadingBox(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseurl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseurl + "Uploading/CcaUploadDialogBox\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBox('" + obj_id_to_position + "');\" />" +
		"</div>" +
		"</div>" +
		"";

    $("body").append(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content_upload").css('top', btnX + '' + 'px');
    $("#id_content_upload").css('left', btnY + '' + 'px');

    // show 
    $("#id_content_upload").show("fast");
    $("#id_bkg_upload").show();
}

function TimeValidation() {
    var curr_date = new Date();
    var cur_day = curr_date.getDate();
    var cur_month = curr_date.getMonth();
    var cur_year = curr_date.getYear();
    var pcurr_date = (parseInt(cur_month) + 1) + "/" + cur_day + "/" + cur_year;
    var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
    var p_date = new Date(p_forceformateddate);
    var fcur_date = new Date(pcurr_date);

    var days = 24 * 60 * 60 * 1000;

    var datediff = p_date.getTime() - fcur_date.getTime();
    var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

    //Display
    if (interval >= 2) {
        btn_saveDraft.show();
    }
    if (interval == 0) {
        btn_updatecallrep.show();
    }
}


function CheckHeaderRequiredFieldsCR() {
    var lacking_fields = "";
    if (txt_cr_accountCode.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    if (txt_cr_accountname.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    if (txt_cr_contactperson.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Person"; } else { lacking_fields = "Contact Person"; }
    }

    if (txt_cr_accountaddress.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Address"; } else { lacking_fields = "Account Address"; }
    }

    if (txt_cr_contactpersonNo.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Number"; } else { lacking_fields = "Contact Number"; }
    }

    if (txt_freqvisit.attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Frequency of Visit"; } else { lacking_fields = "Frequency of Visit"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }
}

function getMonthName(str) {
    switch (str) {
        case "1": _monthname = "January"; break;
        case "2": _monthname = "Febraury"; break;
        case "3": _monthname = "March"; break;
        case "4": _monthname = "April"; break;
        case "5": _monthname = "May"; break;
        case "6": _monthname = "June"; break;
        case "7": _monthname = "July"; break;
        case "8": _monthname = "August"; break;
        case "9": _monthname = "September"; break;
        case "10": _monthname = "October"; break;
        case "11": _monthname = "November"; break;
        case "12": _monthname = "December"; break;
    }
}

$.fn.forcecomma = function () {
    $(this).keypress(function (event) {
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }
        var num = $(this).val();
        //check num if has dot
        if (num.indexOf('.') !== -1) {
            alert("naa");
        }
        else {
            alert("wala");
        }
    });


}

/**Auto Search Look-up*/
function BindToLookUpLive(obj) {
    $("#" + obj).bind('keyup', function () {
        if ($("#" + obj).attr("value").length > 2) {
            $("#div_live_lookup").hide();
            LookUpLiveData(obj);
        } else {
            HideLiveLookUp();
        }
    });
}

function HideLiveLookUp() {
    $("#div_live_lookup").hide();
    $("#div_live_lookup").remove();
}

function HandleSuccessGET(obj, res) {
    var w = "<div id=\"div_live_lookup\" > ";
    w = w + res;
    w = w + "</div>";
    // append
    $("body").after(w);
    // bind
    $("#div_live_lookup div a").bind('click', function () {
        $("#" + obj).attr("value", $(this).attr("val2"));
        $("#" + obj).find("td:nth-child(2) input[type=text]").attr("value", $(this).attr("val1"));

        txt_accountClass.attr("value", $(this).attr("val5"));
        txt_accountName.attr("value", $(this).attr("val3"));
        txt_accountAddress.attr("value", $(this).attr("val4"));
        txt_contactPerson.attr("value", $(this).attr("val6"));
        txt_ContactNumber.attr("value", $(this).attr("val7"));
        checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, $(this).attr("val2"));

        HideLiveLookUp();
    });

    // set position
    var btnY = getElLeft(document.getElementById(obj));
    var btnX = getElTop(document.getElementById(obj));

    var txtboxheight = document.getElementById(obj).offsetHeight;

    btnX = txtboxheight + btnX;
    $("#div_live_lookup").css('top', btnX + '' + 'px');
    $("#div_live_lookup").css('left', btnY + '' + 'px');

    // show 
    $("#div_live_lookup").show("fast");
}

function HandleErrorGET(obj) {

}

function LookUpLiveData(obj) {
    var keyword = "";
    keyword = $("#" + obj).attr("value");

    var accountcode = "";
    accountcode = txt_acctcode.attr("value");

    var SoId = "";
    SoId = soId;

    var month = "";
    month = Eventmonth;

    var day = "";
    day = Eventday;

    var year = "";
    year = Eventyear;

    $.ajax({
        type: "POST", url: baseUrl + "Calendar/GetItemList",
        data: "_str_data=ListOfitemCode&keyword=" + keyword + "&_accountcode=" + accountcode + "&SoId=" + SoId + "&month=" + month + "&day=" + day+"&year="+year,
        success: function (res) { HandleSuccessGET(obj, res); },
        error: function (xhr, ajaxOptions, thrownError) { HandleErrorGET(obj); }
    });
}

/**END Autosearch look up**/



/**CHOSEN STYLE LOOK UP**/
$.fn.lookupTextField = function (isPlanned) {
    $(this).live({
        focus: function () {
           //$(this).unbind();
            create_dialog_box($(this).attr('id'), ($("#chk_isunPlanned").is(":checked")?"T":"F"));
        }
    });
}

function create_dialog_box(obj_id_to_position, isPlanned) {
    $('#div_last_element').append('<div id="id_bkg" class="dlg_box_bkg" onclick="javascript:hide_dialog_box();"></div>' +
    '<div id="id_content" class="dlg_box_content">' +
    '<table cellspacing="0" cellpadding="0" border="0">' +
    '<tr>' +
    '<td>' +
    '<select id="' + obj_id_to_position + '_value" style="width:' + ($("#" + obj_id_to_position).css("width")) + '; font-family:Arial; size:12px; outline:none;">' +
    '</select>' +
    '</td>' +
    '</tr>' +
    '<tr align="center">' +
    '<td><button onclick="javascript:setValueFromSelect(\'' + obj_id_to_position + '\',\''+ isPlanned +'\');" style="cursor:pointer;">Select</button></td>' +
    '</tr>' +
    '</table>' +
    '</div>');

    $.ajax({
        type: 'GET',
        url: baseUrl + "Calendar/lookUpAccount",
        data: {
            Eventmonth: Eventmonth,
            Eventday: Eventday,
            Eventyear: Eventyear,
            soId: soId,
            obj: obj_id_to_position,
            isplanned: isPlanned
        },
        success: function (res) {
            listOfItems = res;
            $('#' + obj_id_to_position + '_value').append(res).chosen().trigger("liszt:updated");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

    var x = getElLeft(document.getElementById(obj_id_to_position));
    var y = getElTop(document.getElementById(obj_id_to_position));

    $('#id_content').css({
        left: x + '' + 'px',
        top: y + '' + 'px'
    });

    $('#id_content').show('fast');
    $('#id_bkg').show();
    $('#div_last_element select').focus();
}

function hide_dialog_box() {
    $('#id_content').hide();
    $('#id_bkg').hide();
    $("#id_content").remove();
    $("#id_bkg").remove();
}

function setValueFromSelect(obj_id_to_position, isPlanned) {
   
    var Acctcode = $("#id_content select option:selected").attr("code");
    var name = $("#id_content select option:selected").attr("value");
    var address = $("#id_content select option:selected").attr("Addrs");
    var contactperson = $("#id_content select option:selected").attr("contactper");
    var phone = $("#id_content select option:selected").attr("Phone");
    var acctclxn = $("#id_content select option:selected").attr("classfxn");
    var hotelname = $("#id_content select option:selected").attr("HotelName");
    var hotelcontactno = $("#id_content select option:selected").attr("hotelContact");

    $("#btn_checkin").show();
    $("#btn_checkout").show();

    if (obj_id_to_position == "txt_accountName") {
        ClearFieldsCoverage();
        txt_acctcode.attr("value", Acctcode);
        txt_accountName.attr("value", name);
        txt_contactPerson.attr("value", contactperson);
        txt_accountClass.attr("value", acctclxn);
        txt_accountAddress.attr("value", address);
        txt_ContactNumber.attr("value", phone);
        txt_hotelname.attr("value", hotelname);
        txt_hotel_contact.attr("value", hotelcontactno);
        getTRFfromSAP(f_eventId, soId, Eventday, Eventmonth, Eventyear, Acctcode);

        checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, Acctcode);
        txt_contactPerson.unbind();
        txt_contactPerson.lookdown(
            { "url": baseurl + "Calendar/ListOfContactPerson", "index_value": "1", "display_rowindex": "1" },
            { Eventmonth: Eventmonth, Eventday: Eventday, Eventyear: Eventyear, soId: soId, acctCode: Acctcode },
            function (res) { return res; },
            function (res, all) {
                //  var _brand = all[1] == "null" ? "" : all[1];
                txt_contactPerson.attr("value", res);
                txt_ContactNumber.attr("value", all[1]);
            });

    }
    if (obj_id_to_position == "txt_cr_accountname") {
        if (isPlanned == "T") {
            ClearFieldsCallreport();

            txt_cr_accountCode.attr("value", Acctcode);
            txt_cr_accountname.attr("value", name);
            txt_cr_contactperson.attr("value", contactperson);
            txt_cr_accountclass.attr("value", acctclxn);
            txt_cr_accountaddress.attr("value", address);
            txt_cr_contactpersonNo.attr("value", contactperson);

            /*inserted by billyjaydelima*/
            $("#location_in").text('').css("color", "#000000")
            $("#time_in").text('').css("color", "#000000");
            $("#location_out").text('').css("color", "#000000");
            $("#time_out").text('').css("color", "#000000");

            txt_hidden_cr_linenum.attr("value", "");
            /*end*/
            txt_cr_contactpersonNo.attr("value", phone);

            spn_callreport_status.text("New unplanned");
            var isReadonly = false;
            EnableDisableTextField(isReadonly);
            div_callreport.show();
            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
        }
        else {
            ClearFieldsCallreport();
            txt_cr_accountCode.attr("value", Acctcode);
            txt_cr_accountname.attr("value", name);
            txt_cr_contactperson.attr("value", contactperson);
            txt_cr_accountclass.attr("value", acctclxn);
            txt_cr_accountaddress.attr("value", address);
            txt_cr_contactpersonNo.attr("value", phone);
            txt_cr_hotelname.attr("value", hotelname);
            txt_cr_hotelcontact.attr("value", hotelcontactno);

            GetCoverageDtls(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
            $("#Logchanges").show();
            $("#VisitLogs").show();
            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
        }
        txt_cr_contactperson.unbind();
        txt_cr_contactperson.lookdown(
            { "url": baseurl + "Calendar/ListOfContactPerson", "index_value": "1", "display_rowindex": "1" },
            { Eventmonth: Eventmonth, Eventday: Eventday, Eventyear: Eventyear, soId: soId, acctCode: Acctcode },
            function (res) { return res; },
            function (res, all) {
                //  var _brand = all[1] == "null" ? "" : all[1];
                txt_cr_contactperson.attr("value", res);
                txt_cr_contactpersonNo.attr("value", all[1]);
            });
    }
    hide_dialog_box();
}
/**END CHOSEN STYLE LOOK UP**/

/* added by billy jay delima */
function allowEdit() {
    var curr_date = new Date();
    var cur_day = curr_date.getDate();
    var cur_month = curr_date.getMonth();
    var cur_year = curr_date.getYear();
    var pcurr_date = (parseInt(cur_month) + 1) + "/" + cur_day + "/" + cur_year;
    var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
    var p_date = new Date(p_forceformateddate);
    var fcur_date = new Date(pcurr_date);

    var datediff = p_date.getTime() - curr_date.getTime();
    var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

    if (interval >= 2) {
        return true;
    }
    else {
        return false;
    }
    return res;
}
/* end */

/**get objective Code for Tab color**/

//get details of the account
function Getobjectivecode(EventId, month, day, year, soid, accountcode) {
    var new_obj =
    {
        EventId: EventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId: soId,
        Acctcode:accountcode
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "Calendar/getObjectiveCode",
        success: function (res) {
            if (res != undefined) {
                txt_freqvisit.attr("value", res.data.NumberOfVisits);
                $(res.data.Objectives).each(function (index, item) {
                    if (item.ObjectiveCode == "C") {
                        $("#crcollection").addClass("tabcolor");
                    }

                    if (item.ObjectiveCode == "S") {
                        $("#crsales").addClass("tabcolor");
                    }

                    if (item.ObjectiveCode == "CS") {
                        $("#crcs").addClass("tabcolor");
                    }

                    if (item.ObjectiveCode == "M") {
                        $("#crmerchandise").addClass("tabcolor");
                    }

                    if (item.ObjectiveCode == "INV") {
                        $("#crinventory").addClass("tabcolor");
                    }

                });
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}
/**end tab color**/

function ReplaceNumberWithCommas(yourNumber) {
    //Seperates the components of the number
    var n = yourNumber.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}

function ClearFieldsCoverage() {
    //header in coverage
    txt_acctcode.attr("value", "");
    txt_accountClass.attr("value", "");
    txt_accountName.attr("value", "");
    txt_accountAddress.attr("value", "");
    txt_contactPerson.attr("value", "");
    txt_ContactNumber.attr("value", "");
    txt_hotelname.attr("value", "");
    txt_hotel_contact.attr("value", "");

    //merchandising
    txt_storechecking.attr("value", "");
    //Customer Service
    txt_issue_concern.attr("value", "");

    txt_total_collection.attr("value", "");
    txt_total_collection.attr("value", "0");

    txt_total_sales.attr("value", "");
    txt_total_sales.attr("value", "0");

    //details
    $("#tbl_collection_dtls").find(".AddedRow").remove();
    $("#tbl_mse_details").find(".AddedRow").remove();
    $("#tbl_sales_dtls").find(".AddedRow").remove();

    $("#tbl_collection_dtls").find(".addedRow").remove();
    $("#tbl_mse_details").find(".addedRow").remove();
    $("#tbl_sales_dtls").find(".addedRow").remove();

    //inventory tab
    $("#create_inventory_count_link").find("div").remove();
    $("#view_inventory_count_link").find("div").remove();

    $("#spn_eventstatus").text("");
}

function ClearFieldsCallreport() {
//header
    txt_cr_accountCode.attr("value", "");
    txt_cr_accountclass.attr("value", "");
    txt_freqvisit.attr("value", "");
    txt_cr_accountname.attr("value", "");
    txt_cr_accountaddress.attr("value", "");
    txt_cr_contactperson.attr("value", "");
    txt_cr_contactpersonNo.attr("value", "");
    txt_cr_hotelname.attr("value", "");
    txt_cr_hotelcontact.attr("value", "");

    $("#tbl_cr_collection").find(".addedRow").remove();
    $("#tbl_cr_collection").find(".UnPlanned").remove();

    // msde
    txt_cr_pstorecheck.attr("value", "");
    txt_cr_storecheckingres.attr("value", "");
    $("#tbl_cr_msdedtl").find(".addedRow").remove();
    $("#tbl_cr_msdedtl").find(".UnPlanned").remove();

    //Sales
    $("#tbl_cr_salesdtl").find(".addedRow").remove();
    $("#tbl_cr_salesdtl").find(".UnPlanned").remove();

    txt_cr_competitorsAct.attr("value", "");

    txt_cr_nextcalldate.attr("value", "");
    txt_cr_otherInfo.attr("value", "");
    $("#txtAttachment").attr("value", "");

    chk_yes.attr("checked", false);
    chk_no.attr("checked", false);

    chk_cs_callrepOntime.attr("checked", false);
    chk_cs_callrepDelay.attr("checked", false);
    chk_cs_callrepcomplete.attr("checked", false);
    $("#chk_cs_callrepincomplete").attr("checked", false);

    //Customer Service
    txt_cs_callrepIssue.attr("value", "");
    txt_cs_callrepSummarylacking.attr("value", "");
    txt_cs_callrepRemarks.attr("value", "");
    txt_cs_callrepRecommend.attr("value", "");
    txt_cs_callrepTimetable.attr("value", "");

    $("#cr_create_inventory_count_link").find("div").remove();
    $("#cr_view_inventory_count_link").find("div").remove();

    txt_cr_colestAmount.attr("value", "");
    txt_cr_colestAmount.attr("value", "0");
    txt_cr_colactAmount.attr("value", "");
    txt_cr_colactAmount.attr("value", "0");

    txt_cr_total_salesEstAmt.attr("value", "");
    txt_cr_total_salesEstAmt.attr("value", "0");

    txt_cr_total_salesActtAmt.attr("value", "");
    txt_cr_total_salesActtAmt.attr("value", "0");

    $("#crcollection").removeClass("tabcolor");
    $("#crsales").removeClass("tabcolor");
    $("#crcs").removeClass("tabcolor");
    $("#crmerchandise").removeClass("tabcolor");
    $("#crinventory").removeClass("tabcolor");

    $("#location_in").text("");  //formatted address
    $("#time_in").text("");
    $("#location_out").text("");  //formatted address
    $("#time_out").text("");

    $("#txt_cr_datecheck").attr("value","");
    $("#txt_cr_postdatedcheck").attr("value","");
    $("#txt_cr_totalcollection").attr("value","");
    $("#txt_cr_colremarks").attr("value","");


    $("#Logchanges").hide();
    $("#VisitLogs").hide();
}

function EnableDisableTextField(isReadonly) {
    if (isReadonly) {
        //$('input[type=text]').attr("readonly", true);
       // $('textarea').attr("readonly", true);

        txt_freqvisit.attr("readonly", true);
        txt_cr_contactperson.attr("readonly", true);
        txt_cr_contactpersonNo.attr("readonly", true);
        txt_cr_hotelname.attr("readonly", true);
        txt_cr_hotelcontact.attr("readonly", true);

        // msde
        txt_cr_pstorecheck.attr("readonly", true);
        txt_cr_storecheckingres.attr("readonly", true);
        //Sales
        txt_cr_competitorsAct.attr("readonly", true);

        txt_cr_nextcalldate.attr("readonly", true);
        txt_cr_otherInfo.attr("readonly", true);
        $("#txtAttachment").attr("readonly", true);


        //Customer Service
        txt_cs_callrepIssue.attr("readonly", true);
        txt_cs_callrepSummarylacking.attr("readonly", true);
        txt_cs_callrepRemarks.attr("readonly", true);
        txt_cs_callrepRecommend.attr("readonly", true);
        txt_cs_callrepTimetable.attr("readonly", true);

        txt_cr_colestAmount.attr("readonly", true);
        txt_cr_colactAmount.attr("readonly", true);

        txt_cr_total_salesEstAmt.attr("readonly", true);

        txt_cr_total_salesActtAmt.attr("readonly", true);

        $("#txt_cr_datecheck").attr("readonly", true);
        $("#txt_cr_postdatedcheck").attr("readonly", true);
        $("#txt_cr_totalcollection").attr("readonly", true);
        $("#txt_cr_colremarks").attr("readonly", true);

        txt_cr_productpresented.attr("readonly", true);
        txt_cr_counterclerk.attr("readonly", true);
        txt_cr_mobileno.attr("readonly", true);
        txt_cr_remarks.attr("readonly", true);

        txt_cr_salesEstimatedAmt.attr("readonly", true);
        txt_cr_salesActualAmt.attr("readonly", true);
        txt_cr_salesremarks.attr("readonly", true);

        txt_cr_total_salesEstAmt.attr("readonly", true);
        txt_cr_total_salesActtAmt.attr("readonly", true);
    }
    else {
        txt_cr_contactperson.attr("readonly", false);
        txt_cr_contactpersonNo.attr("readonly", false);
        txt_cr_hotelname.attr("readonly", false);
        txt_cr_hotelcontact.attr("readonly", false);

        // msde
        txt_cr_pstorecheck.attr("readonly", false);
        txt_cr_storecheckingres.attr("readonly", false);
        //Sales
        txt_cr_competitorsAct.attr("readonly", false);

        txt_cr_nextcalldate.attr("readonly", false);
        txt_cr_otherInfo.attr("readonly", false);
        $("#txtAttachment").attr("readonly", false);


        //Customer Service
        txt_cs_callrepIssue.attr("readonly", false);
        txt_cs_callrepSummarylacking.attr("readonly", false);
        txt_cs_callrepRemarks.attr("readonly", false);
        txt_cs_callrepRecommend.attr("readonly", false);
        txt_cs_callrepTimetable.attr("readonly", false);

        txt_cr_colestAmount.attr("readonly", false);
        txt_cr_colactAmount.attr("readonly", false);


        $("#txt_cr_datecheck").attr("readonly", false);
        $("#txt_cr_postdatedcheck").attr("readonly", false);
        //$("#txt_cr_totalcollection").attr("readonly", false);
        $("#txt_cr_colremarks").attr("readonly", false);

        txt_cr_productpresented.attr("readonly", false);
        txt_cr_counterclerk.attr("readonly", false);
        txt_cr_mobileno.attr("readonly", false);
        txt_cr_remarks.attr("readonly", false);

        txt_cr_salesEstimatedAmt.attr("readonly", false);
        txt_cr_salesActualAmt.attr("readonly", false);
        txt_cr_salesremarks.attr("readonly", false);
    }
}



function checkbuttonOK() {
    var curr_date = new Date();
    var cur_day = curr_date.getDate();
    var cur_month = curr_date.getMonth();
    var cur_year = curr_date.getYear();
    var pcurr_date = cur_month + "/" + cur_day + "/" + cur_year;
    var p_forceformateddate = Eventmonth + "/" + Eventday + "/" + Eventyear;
    var p_date = new Date(p_forceformateddate);
    var fcur_date = new Date(pcurr_date);
    var days = 24 * 60 * 60 * 1000;

    var datediff = p_date.getTime() - curr_date.getTime();
    var interval = Math.floor(datediff / (24 * 60 * 60 * 1000));

    if (interval >= 2) {
        document.getElementById("btn_softdelete").disabled = false;
        document.getElementById("btn_saveChanges").disabled = false;
        document.getElementById("btn_cancelChanges").disabled = false;
    }
    else {
        document.getElementById("btn_softdelete").disabled = true;
        document.getElementById("btn_saveChanges").disabled = true;
        document.getElementById("btn_cancelChanges").disabled = true;
    }
}

function addCommas(str) {
    var new_str = new String(str);
    var isNegative = false;

    if (new_str.indexOf("-") != -1) {
        str = new_str.replace("-", "");
        isNegative = true;
    }

    str = parseFloat(str).toFixed(2);
    var amount = new String(str);
    amount = amount.split("").reverse();
    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if (i != 2) {
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = ',' + output;
        }
    }

    if (isNegative) output = "-" + output;

    return output;
}

$.fn.addCommas = function () {
    var new_str = new String(this.attr("value"));
    var isNegative = false;

    if (new_str.indexOf("-") != -1) {
        str = new_str.replace("-", "");
        isNegative = true;
    }

    str = parseFloat(str).toFixed(2);
    var amount = new String(str);
    amount = amount.split("").reverse();
    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if (i != 2) {
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = "," + output;
        }
    }
}

function check_empty_fields() {

    if ((txt_cr_datecheck.attr("value") == "" || txt_cr_datecheck.attr("value") == "0" || txt_cr_datecheck.attr("value") == "0.00") &&
        (txt_cr_postdatedcheck.attr("value") == "" || txt_cr_postdatedcheck.attr("value") == "0" || txt_cr_postdatedcheck.attr("value") == "0.00") &&
        (txt_cr_totalcollection.attr("value") == "" || txt_cr_totalcollection.attr("value") == "0.00" || txt_cr_totalcollection.attr("value") == "0") &&
        txt_cr_colremarks.attr("value") == "" &&
        txt_cr_pstorecheck.attr("value") == "" &&
        txt_cr_storecheckingres.attr("value") == "" &&
        $("#tbl_cr_msdedtl").find("tr").length == 2 &&
        (txt_cr_total_salesActtAmt.attr("value") == "0" || txt_cr_total_salesActtAmt.attr("value") == "0.00") &&
        txt_cr_competitorsAct.attr("value") == "" &&
        txt_cr_nextcalldate.attr("value") == "" &&
        txt_cr_otherInfo.attr("value") == "" &&
        txt_cs_callrepIssue.attr("value") == "" &&
        txt_cs_callrepSummarylacking.attr("value") == "" &&
        txt_cs_callrepRemarks.attr("value") == "" &&
        txt_cs_callrepRecommend.attr("value") == "" &&
        txt_cs_callrepTimetable.attr("value") == "" &&
        chk_yes.is(":checked") == false && chk_no.is(":checked") == false &&
        chk_cs_callrepOntime.is(":checked") == false && chk_cs_callrepDelay.is(":checked") == false &&
        chk_cs_callrepcomplete.is(":checked") == false && $("#chk_cs_callrepincomplete").is(":checked") == false

        ) {

        return "Please fill atleast one(1) objective";

    }
    else
        return "OK";

//    if (chk_yes.is(":checked") == false && chk_no.is(":checked") == false)
//        return "Please fill atleast one(1) objective/s";
//    if (chk_cs_callrepOntime.is(":checked") == false && chk_cs_callrepDelay.is(":checked") == false)
//        return "Please fill atleast one(1) objective/s";
//    if (chk_cs_callrepcomplete.is(":checked") == false && chk_cs_callrepincomplete.is(":checked") == false)
//        return "Please fill atleast one(1) objective/s";

}
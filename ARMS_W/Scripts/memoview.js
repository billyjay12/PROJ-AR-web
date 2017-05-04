var ns4;
//under coverage header tab
var txt_vw_accountName = null;
var txt_vw_accountClass = null;
var txt_vw_accountAddress = null;
var txt_vw_contactPerson = null;
var txt_vw_ContactNumber = null;
var txt_vw_acctcode = null;
//under callreport header tab
var txt_vwcr_accountCode = null;
var txt_vwcr_accountclass = null;
var txt_vwcr_freqvisit = null;
var txt_vwcr_accountname = null;
var txt_vwcr_accountaddress = null;
var txt_vwcr_timein = null;
var txt_vwcr_contactperson = null;
var txt_vwcr_contactpersonNo = null;
var txt_vwcr_timeout = null;
var chk_vw_isunPlanned = null;

//coverage Merchandise Tab
var txt_vw_storechecking = null;

//coverage Header tab
var txt_wv_issue_concern = null;

//call report

//msde
var txt_vw_cr_pstorecheck = null;
var txt_vw_cr_storecheckingres = null;

//collection
var chk_vw_Fcollection = null;
var chk_vw_Pcollection = null;
var chk_vw_Ncollection = null;

//sales
var txt_cr_competitorsAct = null;
var chk_vw_yes = null;
var chk_vw_no = null;
var txt_vw_cr_nextcalldate = null;
var txt_vw_cr_otherInfo = null;

//cust srvc
var txt_vw_cs_callrepIssue = null;
var chk_vw_cs_callrepOntime = null;
var chk_vw_cs_callrepDelay = null;
var chk_vw_cs_callrepcomplete = null;
var chk_vw_cs_callrepincomplete = null;
var txt_vw_cs_callrepSummarylacking = null;
var txt_vw_cs_callrepRemarks = null;
var txt_vw_cs_callrepRecommend = null;
var txt_vw_cs_callrepTimetable = null;


//attachment view
//var lnk_attach2 = null;
var lnk_vw_view_attach2 = null;
var txt_vw_FileAttachment2 = null;



var time_in = null;
var time_out = null;
var location_in = null;
var location_out = null;

var txt_vwcr_hotelname = null;
var txt_vwcr_hotelcontact = null;

var txt_vw_hotelname = null;
var txt_vw_hotel_contact = null;

var txt_vw_collectionTotal = null;
var txt_vw_sales_total = null;
var txt_vw_total_collectionEst = null;
var txt_vw_total_collectionAct = null;
var txt_vw_sales_totalEst = null;
var txt_vw_sales_totalAct = null;

var txt_vw_details = null;




$(function () {
    var currentDate = new Date()
    var cday = currentDate.getDate()
    var cmonth = currentDate.getMonth() + 1
    var cyear = currentDate.getFullYear()


    //var pday = parseInt(Eventday) + 3;
    // var pmonth = parseInt(Eventmonth);
    // var pyear = parseInt(Eventyear);

    //coverage header tab
    txt_vw_accountName = $("#txt_vw_accountName");
    txt_vw_accountClass = $("#txt_vw_accountClass");
    txt_vw_accountAddress = $("#txt_vw_accountAddress");
    txt_vw_contactPerson = $("#txt_vw_contactPerson");
    txt_vw_ContactNumber = $("#txt_vw_ContactNumber");
    txt_vw_acctcode = $("#txt_vw_acctcode");

    //COVERAGE
    //coverage mse

    $("input").attr("readonly", true);
    $("textarea").attr("readonly", true);
    //$("textarea").css("back", true);

    txt_vw_storechecking = $("#txt_vw_storechecking");

    //covergae custservice
    txt_wv_issue_concern = $("#txt_wv_issue_concern");

    //msde
    txt_vw_cr_pstorecheck = $("#txt_vw_cr_pstorecheck");
    txt_vw_cr_storecheckingres = $("#txt_vw_cr_storecheckingres");

    //[call report header tab ]

    txt_vwcr_accountCode = $("#txt_vwcr_accountCode");
    txt_vwcr_accountclass = $("#txt_vwcr_accountclass");
    txt_vwcr_freqvisit = $("#txt_vwcr_freqvisit");
    txt_vwcr_accountname = $("#txt_vwcr_accountname");
    txt_vwcr_accountaddress = $("#txt_vwcr_accountaddress");
    txt_vwcr_timein = $("#txt_vwcr_timein");
    txt_vwcr_contactperson = $("#txt_vwcr_contactperson");
    txt_vwcr_contactpersonNo = $("#txt_vwcr_contactpersonNo");
    txt_vwcr_timeout = $("#txt_vwcr_timeout");
    chk_vw_isunPlanned = $("#chk_vw_isunPlanned");

    //[call report ]
    //collection 

    chk_vw_Fcollection = $("#chk_vw_Fcollection");
    chk_vw_Pcollection = $("#chk_vw_Pcollection");
    chk_vw_Ncollection = $("#chk_vw_Ncollection");

    //sales
    txt_cr_competitorsAct = $("#txt_cr_competitorsAct");
    chk_vw_yes = $("#chk_vw_yes");
    chk_vw_no = $("#chk_no");
    txt_vw_cr_nextcalldate = $("#txt_vw_cr_nextcalldate");
    txt_vw_cr_otherInfo = $("#txt_vw_cr_otherInfo");


    //cust srvc
    txt_vw_cs_callrepIssue = $("#txt_vw_cs_callrepIssue");
    chk_vw_cs_callrepOntime = $("#chk_vw_cs_callrepOntime");
    chk_vw_cs_callrepDelay = $("#chk_vw_cs_callrepDelay");
    chk_vw_cs_callrepcomplete = $("#chk_vw_cs_callrepcomplete");
    chk_vw_cs_callrepincomplete = $("#chk_vw_cs_callrepincomplete");
    txt_vw_cs_callrepSummarylacking = $("#txt_vw_cs_callrepSummarylacking");
    txt_vw_cs_callrepRemarks = $("#txt_vw_cs_callrepRemarks");
    txt_vw_cs_callrepRecommend = $("#txt_vw_cs_callrepRecommend");
    txt_vw_cs_callrepTimetable = $("#txt_vw_cs_callrepTimetable");


    //    lnk_attach2 = $("#lnk_attach2");
    lnk_vw_view_attach2 = $("#lnk_vw_view_attach2");
    txt_vw_FileAttachment2 = $("#txt_vw_FileAttachment2");



    time_in = $("#time_in");
    time_out = $("#time_out");
    location_in = $("#location_in");
    location_out = $("#location_out");

    txt_vwcr_hotelname = $("#txt_vwcr_hotelname");
    txt_vwcr_hotelcontact = $("#txt_vwcr_hotelcontact");

    txt_vw_hotelname = $("#txt_vw_hotelname");
    txt_vw_hotel_contact = $("#txt_vw_hotel_contact");


    txt_vw_collectionTotal = $("#txt_vw_collectionTotal");
    txt_vw_sales_total = $("#txt_vw_sales_total");
    txt_vw_total_collectionEst = $("#txt_vw_total_collectionEst");
    txt_vw_total_collectionAct = $("#txt_vw_total_collectionAct");
    txt_vw_sales_totalEst = $("#txt_vw_sales_totalEst");
    txt_vw_sales_totalAct = $("#txt_vw_sales_totalAct");

    txt_vw_details = $("#txt_vw_details");




    //alert(Eventdate);
    //alert(cday + "/" + cmonth + "/" + cyear);

    //if(pday>=cday && 

    /**
    $("#txt_vwcr_accountCode").lookdown(
    { "url": baseUrl + "Calendar/GetCoverageInfobyDates", "index_value": "1", "display_rowindex": "1" },
    { Eventmonth: Eventmonth, Eventday: Eventday, Eventyear: Eventyear, soId: soId },
    function (res) { return res; },
    function (res, all) {

    txt_vwcr_accountCode.attr("value", res);
    txt_vw_accountName.attr("value", "");



    }

    ); **/

    /**
    $("#txt_vw_acctcode").lookdown(
    { "url": baseUrl + "Calendar/GetCoverageInfobyDates", "index_value": "1", "display_rowindex": "1" },
    { Eventmonth: Eventmonth, Eventday: Eventday, Eventyear: Eventyear, soId: soId },
    function (res) { return res; },
    function (res, all) {

    txt_vw_acctcode.attr("value", res);


    //        txt_vw_accountName.attr("value", all[31]);
    //        txt_vw_accountClass.attr("value", all[33]);
    //        txt_vw_accountAddress.attr("value", all[32]);
    //        txt_vw_contactPerson.attr("value", all[9]);
    //        txt_vw_ContactNumber.attr("value", all[10]);

    //call report header Tab
    //        txt_vwcr_accountCode.attr("value", res);
    //        txt_vwcr_accountclass.attr("value", all[33]);
    //        txt_vwcr_freqvisit.attr("value", "");
    //        txt_vwcr_accountname.attr("value", all[31]);
    //        txt_vwcr_accountaddress.attr("value", all[32]);
    //        txt_vwcr_timein.attr("value", "");
    //        txt_vwcr_contactperson.attr("value", all[9]);
    //        txt_vwcr_contactpersonNo.attr("value", all[10]);
    //        txt_vwcr_timeout.attr("value", "");
    //        chk_vw_isunPlanned.attr("value", "");

    var EventId = all[5] != "" ? all[5] : "";
    var accountcode = all[0] != "" ? all[0] : "";


    GetCovegareInfo(EventId, Eventmonth, Eventday, Eventyear, soId, accountcode);
    }


    ); **/

    if (qrystring_acctCode != "") {
        GetCovegareInfo("AUTOLOOKUP", Eventmonth, Eventday, Eventyear, soId, qrystring_acctCode);
        txt_vw_acctcode.attr("value", qrystring_acctCode);
    }

    txt_vw_acctcode.lookupTextField();


});


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

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr); alert(thrownError);
        }
    });
}

function GetCovegareInfo(EventId, Eventmonth, Eventday, Eventyear, soId, accoutcode) {

    var new_obj =
    {
        EventId: EventId,
        Eventmonth: Eventmonth,
        Eventday: Eventday,
        Eventyear: Eventyear,
        soId: soId,
        accoutcode: accoutcode
    }
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetCoverageInfo",
        success: function (res) {
            if (res != undefined) {

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

                //                if (res.data.inventory_objective.hasForInventoryCount) {
                //                    $("#create_inventory_count_link").html('<div> To create inventory count click >> <a href="' + baseUrl + '?id=' + accoutcode + '&doctype=newIC" target="new_tab">here</a> </div>');
                //                    $("#vwinventory").addClass("tabcolor");
                //                }
                if (res.data.inventory_objective.inventoryCountId != "") {
                    $("#view_inventory_count_link").html('<div> To view inventory count click >> <a href="' + baseUrl + '?id=' + res.data.inventory_objective.inventoryCountId + '&doctype=invcount" target="new_tab">here</a> </div>');
                    $("#vwinventory").addClass("tabcolor");
                }

                $(res.data.coverage).each(function (index, item) {

                    //under coverage  header tab
                    txt_vw_accountName.attr("value", item.AccountName);
                    txt_vw_accountClass.attr("value", item.AccountClass);
                    txt_vw_accountAddress.attr("value", item.AccountAddress);
                    txt_vw_contactPerson.attr("value", item.ContactPerson);
                    txt_vw_ContactNumber.attr("value", item.ContactPersonNo);

                    //tracker details
                    time_in.text(item.CheckInTime == "" ? "(Not yet check in)" : item.CheckInTime).css("color", time_in.text() == "(No check in)" ? "#ff0000" : "#000000");
                    time_out.text(item.CheckOutTime == "" ? "(Not yet check out)" : item.CheckOutTime).css("color", time_out.text() == "(No check out)" ? "#ff0000" : "#000000");
                    location_in.text(item.CheckInAddress == null ? "(Not yet check in)" : item.CheckInAddress).css("color", location_in.text() == "(No check in)" ? "#ff0000" : "#000000");
                    location_out.text(item.CheckOutAddress == null ? "(Not yet check out)" : item.CheckOutAddress).css("color", location_out.text() == "(No check out)" ? "#ff0000" : "#000000");

                    txt_vwcr_accountCode.attr("value", item.AccountCode);
                    txt_vwcr_accountclass.attr("value", item.AccountClass);
                    txt_vwcr_freqvisit.attr("value", item.freqVisit);
                    txt_vwcr_accountname.attr("value", item.AccountName);
                    txt_vwcr_accountaddress.attr("value", item.AccountAddress);
                    txt_vwcr_timein.attr("value", item.TimeIn);
                    txt_vwcr_contactperson.attr("value", item.ContactPerson);
                    txt_vwcr_contactpersonNo.attr("value", item.ContactPersonNo);
                    txt_vwcr_timeout.attr("value", item.Timeout);
                    chk_vw_isunPlanned.attr("value", "");

                    $("#txt_cr_datecheck").attr("value", ReplaceNumberWithCommas(item.ColDatedCheck));
                    $("#txt_cr_postdatedcheck").attr("value", ReplaceNumberWithCommas(item.ColPostDatedCheck));
                    $("#txt_cr_totalcollection").attr("value", ReplaceNumberWithCommas(item.ColTotal));
                    $("#txt_cr_colremarks").attr("value", item.ColRemarks);
                    //coverage mse

                    txt_vw_storechecking.attr("value", item.StoreChecking);

                    //covergae custservice
                    txt_wv_issue_concern.attr("value", item.IssuesAndConcerns);

                    //call report
                    //msde
                    $('#VisitLogs a').attr('href', baseUrl + "Calendar/getVisitLogsByAccount?LineNum=" + item.LineNum);
                    $("#VisitLogs").show();

                    txt_vw_cr_pstorecheck.attr("value", item.StoreChecking);
                    txt_vw_cr_storecheckingres.attr("value", item.StoreCheckingResult);
                    //collection 
                    var partial = item.cPartialCollection != "T" ? "F" : "T";
                    var full = item.cFullCollection != "T" ? "F" : "T";
                    var without = item.cNoCollection != "T" ? "F" : "T";

                    if (partial == "T") {
                        // chk_vw_Pcollection.attr(':checked');
                        chk_vw_Pcollection.attr("checked", true);
                        chk_vw_Fcollection.removeAttr('checked');
                        chk_vw_Ncollection.removeAttr('checked');
                    }
                    if (full == "T") {
                        chk_vw_Fcollection.attr("checked", true);
                        chk_vw_Pcollection.removeAttr('checked');
                        chk_vw_Ncollection.removeAttr('checked');
                    }
                    if (without == "T") {
                        chk_vw_Ncollection.attr("checked", true);
                        chk_vw_Fcollection.removeAttr('checked');
                        chk_vw_Pcollection.removeAttr('checked');
                    }

                    txt_cr_competitorsAct.attr("value", item.CompetitorActivities);
                    txt_vw_cr_nextcalldate.attr("value", item.NextCallDate);
                    txt_vw_cr_otherInfo.attr("value", item.OtherInformation);
                    if (item.WithOrder == "T") {

                        chk_vw_yes.attr("checked", true);
                        chk_vw_no.removeAttr('checked');
                    }
                    if (item.WithOrder == "F") {

                        chk_vw_no.attr("checked", true);
                        chk_vw_yes.removeAttr('checked');
                    }
                    txt_vw_cs_callrepIssue.attr("value", item.IssuesAndConcerns);
                    txt_vw_cs_callrepSummarylacking.attr("value", item.SummaryLackingItems);
                    txt_vw_cs_callrepRemarks.attr("value", item.Remarks);
                    txt_vw_cs_callrepRecommend.attr("value", item.Recommendation);
                    txt_vw_cs_callrepTimetable.attr("value", item.TimeTable);

                    if (item.Delivery == "T") {
                        chk_vw_cs_callrepOntime.attr("checked", true);
                        chk_vw_cs_callrepDelay.removeAttr("checked");

                    }
                    else if (item.Delivery == "F") {
                        chk_vw_cs_callrepDelay.attr("checked", true);
                        chk_vw_cs_callrepOntime.removeAttr("checked");
                    }

                    if (item.Orders == "T") {
                        chk_vw_cs_callrepcomplete.attr("checked", true);
                        chk_vw_cs_callrepincomplete.removeAttr("checked");
                    }
                    else if (item.Orders == "F") {
                        chk_vw_cs_callrepincomplete.attr("checked", true);
                        chk_vw_cs_callrepcomplete.removeAttr("checked");
                    }

                    if (item.Attachment != "" && item.Attachment != null && item.Attachment != "null") {

                        lnk_vw_view_attach2.attr("href", baseUrl + "Calendar/DownLoadFile?filename=" + encodeURIComponent(item.Attachment) + "&DoctypeId=" + item.DoctypeId)

                    }

                    if (item.Attachment == "" || item.Attachment == null || item.Attachment == "null") {
                        //$('#divDownloadAttachment').html("No File Attached.");
                        lnk_vw_view_attach2.css("display", "none");
                        txt_vw_FileAttachment2.attr("value", "[No File Attached]");
                    }
                    else {
                        //  $('#divDownloadAttachment').html('<a href="' + baseUrl + "Calendar/getAttachment?EventId=" + item.EventID + "&LineNum=" + item.LineNum + '">Click to download attachment</a>');
                        txt_vw_FileAttachment2.attr("value", item.Attachment);
                        lnk_vw_view_attach2.attr("href", baseUrl + "Calendar/getAttachment?EventId=" + item.EventID + "&LineNum=" + item.LineNum);
                    }

                    $(item.Sub_coverage).each(function (index, itm) {

                        var p_amount = itm.PlannedAmount == "" ? "0" : parseFloat(itm.PlannedAmount).toFixed(2);
                        var a_amount = itm.ActualAmount == "" ? "0" : parseFloat(itm.ActualAmount).toFixed(2);

                        if (itm.ObjectiveCode.toUpperCase() == "C") {

                            PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            $("#tbl_vw_collection_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#tbl_vwcr_collection").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#vwcollection").addClass("tabcolor");
                            $("#vwcrcollection").addClass("tabcolor");
                        }

                        if (itm.ObjectiveCode.toUpperCase() == "S") {

                            PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            $("#tbl_vw_cr_salesdtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#tbl_vwsales_dtls").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#vwsales").addClass("tabcolor");
                            $("#vwcrsales").addClass("tabcolor");

                        }

                        if (itm.ObjectiveCode.toUpperCase() == "M") {

                            PopulateDate(itm.ObjectiveCode, itm.Brand, p_amount, a_amount, itm.Remarks, itm.ProductPresented, itm.CounterClerk, itm.CounterClerkNo, itm.dtlsrmks);
                            $("#tbl_vw_cr_msdedtl").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#tbl_vwmse_details").find(".hiddenTd").hide(); /*hide the td of the objectiveCode**/
                            $("#vwmsde").addClass("tabcolor");
                            $("#vwcrmsde").addClass("tabcolor");


                        }

                        if (itm.ObjectiveCode.toUpperCase() == "CS") {
                            $("#vwcs").addClass("tabcolor");
                            $("#vwcrcs").addClass("tabcolor");
                        }

                    });

                    $(item.Total_dtls).each(function (index, itn) {

                        var c_actualAmount = itn.estActualAmount == "" ? "0" : ReplaceNumberWithCommas(parseFloat(itn.estActualAmount).toFixed(2));
                        //var c_actualAmount = itn.estActualAmount == "" ? "0" : ReplaceNumberWithCommas(parseFloat(itn.estActualAmount).toFixed(2));

                        if (itn.ObjectiveCode == "C") {
                            txt_vw_collectionTotal.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));

                            txt_vw_total_collectionEst.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                            txt_vw_total_collectionAct.attr("value", c_actualAmount);
                        }
                        if (itn.ObjectiveCode == "S") {

                            txt_vw_sales_total.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));

                            txt_vw_total_collectionEst.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                            txt_vw_total_collectionAct.attr("value", c_actualAmount);

                            txt_vw_sales_totalEst.attr("value", ReplaceNumberWithCommas(parseFloat(itn.estPlannedAmount).toFixed(2)));
                            txt_vw_sales_totalAct.attr("value", c_actualAmount);
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


function PopulateDate(objective, Brand, eAmt, aAmt, remarks, Product, cclerk, cclerkno, dtlsrmks) {

    var EstimatedAmt = eAmt == "" ? "0" : eAmt;
    var ActualAmt = aAmt == "" ? "0" : aAmt;
    var rmrks = remarks == null ? "" : remarks;
    var clerknum = cclerkno == null ? "" : cclerkno;
    var dtls = dtlsrmks == null ? "" : dtlsrmks;

    if (objective.toUpperCase() == "C") {

        $("#tbl_vw_collection_dtls .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '" readonly="readonly"/></td>' +
                                                 
                                                  '</tr>');

        $("#tbl_vwcr_collection .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' +ReplaceNumberWithCommas(EstimatedAmt) + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' +ReplaceNumberWithCommas(ActualAmt) + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + rmrks + '" readonly="readonly"/><td>' +
                                                  '</tr>');

    }

    if (objective.toUpperCase() == "S") {

        $("#tbl_vwsales_dtls .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + ReplaceNumberWithCommas(EstimatedAmt) + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + dtls + '" readonly="readonly"/></td>' +
                                                  '</tr>');


        $("#tbl_vw_cr_salesdtl .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Brand + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' +ReplaceNumberWithCommas(EstimatedAmt) + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + ReplaceNumberWithCommas(ActualAmt) + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + dtls + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + rmrks + '" readonly="readonly"/></td>' +
                                                  '</tr>');
    }


    if (objective.toUpperCase() == "M") {

        $("#tbl_vwmse_details .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Product + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + cclerk + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + clerknum + '" readonly="readonly"/></td>');

        $("#tbl_vw_cr_msdedtl .last_row").before('<tr class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + objective + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + Product + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + cclerk + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + clerknum + '" readonly="readonly"/></td>' +
                                                  '<td><input type="text"  value="' + rmrks + '" readonly="readonly"/></td>' );

    }

}



function ReplaceNumberWithCommas(yourNumber) {
    if (yourNumber == null) return "";
    //Seperates the components of the number

    var n = yourNumber.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}



/**CHOSEN STYLE LOOK UP**/

$.fn.lookupTextField = function () {
    $(this).live({
        focus: function () {
            //$(this).unbind();
            create_dialog_box($(this).attr('id'));
        }
    });
}

function create_dialog_box(obj_id_to_position) {
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
    '<td><button onclick="javascript:setValueFromSelect(\'' + obj_id_to_position + '\');" style="cursor:pointer;">Select</button></td>' +
    '</tr>' +
    '</table>' +
    '</div>');

    $.ajax({
        type: 'GET',
        url: baseUrl + "Calendar/lookUpAccountASM",
        data: {
            Eventmonth: Eventmonth,
            Eventday: Eventday,
            Eventyear: Eventyear,
            soId: soId,
            obj: obj_id_to_position
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

function setValueFromSelect(obj_id_to_position) {

    var Acctcode = $("#id_content select option:selected").attr("value");
    var name = $("#id_content select option:selected").attr("name");
    var address = $("#id_content select option:selected").attr("Addrs");
    var contactperson = $("#id_content select option:selected").attr("contactper");
    var phone = $("#id_content select option:selected").attr("Phone");
    var acctclxn = $("#id_content select option:selected").attr("classfxn");
    var hotelname = $("#id_content select option:selected").attr("HotelName");
    var hotelcontactno = $("#id_content select option:selected").attr("hotelContact");

    var _month = $("#id_content select option:selected").attr("months");
    var _day = $("#id_content select option:selected").attr("day");
    var _year = $("#id_content select option:selected").attr("year");
    var _soid = $("#id_content select option:selected").attr("soId");
    var _eventId = $("#id_content select option:selected").attr("EventId");

    ClearFields();

    txt_vw_acctcode.attr("value", Acctcode);
    txt_vw_accountName.attr("value", name);
    txt_vw_contactPerson.attr("value", contactperson);
    txt_vw_hotelname.attr("value", hotelname);
    txt_vw_accountClass.attr("value", acctclxn);
    txt_vw_accountAddress.attr("value", address);
    txt_vw_ContactNumber.attr("value", phone);
    txt_vw_hotel_contact.attr("value", hotelcontactno);


    txt_vwcr_accountCode.attr("value", Acctcode);
    txt_vwcr_accountname.attr("value", name);
    txt_vwcr_contactperson.attr("value", contactperson);
    txt_vwcr_hotelname.attr("value", hotelname);
    txt_vwcr_accountclass.attr("value", acctclxn);
    txt_vwcr_accountaddress.attr("value", address);
    txt_vwcr_contactpersonNo.attr("value", phone);
    txt_vwcr_hotelcontact.attr("value", hotelcontactno);

    GetCovegareInfo(_eventId, _month, _day, _year, _soid, Acctcode);




    //    txt_itmcode.attr("value", code);
    //    txt_itmdesc.attr("value", desc);
    //    txt_brand.attr("value", brand);
    //    txt_prodgroup.attr("value", prodgrp);
    //    txt_hidAmt.attr("value", amt);
    //    txt_netSellIn.attr("value", sellin);

//    if (obj_id_to_position == "txt_acctcode") {
//        ClearFieldsCoverage();
//        txt_acctcode.attr("value", Acctcode);
//        txt_accountName.attr("value", name);
//        txt_contactPerson.attr("value", contactperson);
//        txt_accountClass.attr("value", acctclxn);
//        txt_accountAddress.attr("value", address);
//        txt_ContactNumber.attr("value", phone);
//        txt_hotelname.attr("value", hotelname);
//        txt_hotel_contact.attr("value", hotelcontactno);

//        checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, Acctcode);
//    }
//    if (obj_id_to_position == "txt_cr_accountCode") {

//        if (isPlanned == "T") {

//            ClearFieldsCallreport();

//            txt_cr_accountCode.attr("value", Acctcode);
//            txt_cr_accountname.attr("value", name);
//            txt_cr_contactperson.attr("value", contactperson);
//            txt_cr_accountclass.attr("value", acctclxn);
//            txt_cr_accountaddress.attr("value", address);
//            txt_cr_contactpersonNo.attr("value", phone);
//            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);

//        }

//        else {

//            ClearFieldsCallreport();
//            txt_cr_accountCode.attr("value", Acctcode);
//            txt_cr_accountname.attr("value", name);
//            txt_cr_contactperson.attr("value", contactperson);
//            txt_cr_accountclass.attr("value", acctclxn);
//            txt_cr_accountaddress.attr("value", address);
//            txt_cr_contactpersonNo.attr("value", phone);
//            txt_cr_hotelname.attr("value", hotelname);
//            txt_cr_hotelcontact.attr("value", hotelcontactno);

//            GetCoverageDtls(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
//            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);

//        }


//    }

    hide_dialog_box();
}



/**END CHOSEN STYLE LOOK UP**/



function ClearFields() {

    txt_vw_acctcode.attr("value", "");
    txt_vw_accountClass.attr("value", "");
    txt_vw_accountName.attr("value", "");
    txt_vw_accountAddress.attr("value", "");
    txt_vw_contactPerson.attr("value", "");
    txt_vw_ContactNumber.attr("value", "");
    txt_vw_hotelname.attr("value", "");
    txt_vw_hotel_contact.attr("value", "");
    txt_vw_storechecking.attr("value", "");
    txt_wv_issue_concern.attr("value", "");
    txt_vwcr_accountCode.attr("value", "");
    txt_vwcr_accountclass.attr("value", "");
    txt_vwcr_freqvisit.attr("value", "");
    txt_vwcr_accountname.attr("value", "");
    txt_vwcr_accountaddress.attr("value", "");
    txt_vwcr_timein.attr("value", "");
    txt_vwcr_contactperson.attr("value", "");
    txt_vwcr_contactpersonNo.attr("value", "");
    txt_vwcr_timeout.attr("value", "");
    txt_vwcr_hotelname.attr("value", "");
    txt_vwcr_hotelcontact.attr("value", "");

    txt_vw_cr_pstorecheck.attr("value", "");
    txt_vw_cr_storecheckingres.attr("value", "");
    txt_cr_competitorsAct.attr("value", "");

    txt_vw_cr_nextcalldate.attr("value", "");
    txt_vw_cr_otherInfo.attr("value", "");
    txt_vw_FileAttachment2.attr("value", "");
    txt_vw_cs_callrepIssue.attr("value", "");
    txt_vw_cs_callrepSummarylacking.attr("value", "");
    txt_vw_cs_callrepRemarks.attr("value", "");
    txt_vw_cs_callrepRecommend.attr("value", "");
    txt_vw_cs_callrepTimetable.attr("value", "");


    $("#tbl_vw_collection_dtls").find(".addedRow").remove();
    $("#tbl_vwcr_collection").find(".UnPlanned").remove();
    $("#tbl_vwsales_dtls").find(".addedRow").remove();
    $("#tbl_vw_cr_salesdtl").find(".UnPlanned").remove();
    $("#tbl_vwmse_details").find(".addedRow").remove();
    $("#tbl_vw_cr_msdedtl").find(".UnPlanned").remove();



    $("#vwcollection").removeClass("tabcolor");
    $("#vwmsde").removeClass("tabcolor");
    $("#vwsales").removeClass("tabcolor");
    $("#vwcs").removeClass("tabcolor");

    $("#vwcrcollection").removeClass("tabcolor");
    $("#vwcrmsde").removeClass("tabcolor");
    $("#vwcrsales").removeClass("tabcolor");
    $("#vwcrcs").removeClass("tabcolor");



}

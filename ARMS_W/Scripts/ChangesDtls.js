var ns4;
var txt_sales_id = null;
var txt_sales_name = null;
var txt_coveragetrackingno = null;
var txt_month = null;
var txt_year = null;
var txt_hidden_month = null;
var monthaname = null;
var btn_approve = null;
var btn_return = null;
var btn_disapprove = null;

$(function () {

    getMonthName(Eventmonth);
    txt_sales_id = $("#txt_sales_id");
    txt_sales_name = $("#txt_sales_name");
    txt_coveragetrackingno = $("#txt_coveragetrackingno");
    txt_month = $("#txt_month");
    txt_year = $("#txt_year");
    txt_hidden_month = $("#txt_hidden_month");
    btn_approve = $("#btn_approve");
    btn_return = $("#btn_return");
    btn_disapprove = $("#btn_disapprove");


    txt_sales_id.attr("value", soId);
    txt_sales_name.attr("value", SoName);
    txt_coveragetrackingno.attr("value", EventID);
    txt_month.attr("value", monthaname);
    txt_year.attr("value", Eventyear);

    $("#tbl_changesDtls").find(".hideTR").hide();

    LoadChangesdtls(soId, Eventmonth, Eventyear, EventID);



    btn_approve.click(function () { ApproveCoverageChanges("APPROVE"); });
    btn_return.click(function () { ApproveCoverageChanges("RETURN_TO_REQUESTOR"); });
    btn_disapprove.click(function () { ApproveCoverageChanges("DISAPPROVE"); });


});


function LoadChangesdtls(soId, Eventmonth, Eventyear, EventID) 
{
    var new_obj = {
        soId: soId,
        month: Eventmonth,
        year: Eventyear,
        EventId: EventID
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetChangesCoveragebySo",
        success: function (res) {

            if (res != undefined) {
                if (res.data.coverage.len != 0) {

                    $(res.data.coverage).each(function (index, itm) {
                        PopulatechangesDtls(itm.Day, itm.AccountCode);
                    });

                    $("#tbl_changesDtls").find(".hideTR").show();

                    $("#chk_selectall").change(function () {

                        if ($("#chk_selectall").is(":checked") == true) {
                            $("#tbl_changesDtls").find(".hideTR").show();
                            $("#tbl_changesDtls .AddedRow").each(function () {

                                var row = $(this);
                                row.find('td:eq(0) input[type=checkbox]').prop('checked', true);

                                row.find('td:eq(0) input[type=checkbox]').change(function () {
                                    $("#chk_selectall").removeAttr('checked');
                                });
                            });
                        }

                        else {

                            $("#tbl_changesDtls .AddedRow").each(function () {
                                var row = $(this);
                                row.find('td:eq(0) input[type=checkbox]').prop('checked', false);
                                $("#tbl_changesDtls").find(".hideTR").hide();
                            });
                        }
                        // $("#chk_selectall .AddedRow").find('td"eq(0) input[type=checkbox]').change(
                    });
                }
                else {
                    window.location = baseUrl + "calendar/ChangesForApproval";
                }
            } else {
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function getMonthName(Month) {
    switch (Month) {
        case "1":
            monthaname = "January";
            break;
        case "2":
            monthaname = "February";
            break;
        case "3":
            monthaname = "March";
            break;
        case "4":
            monthaname = "April";
            break;
        case "5":
            monthaname = "May";
            break;
        case "6":
            monthaname = "June";
            break;
        case "7":
            monthaname = "July";
            break;
        case "8":
            monthaname = "August";
            break;
        case "9":
            monthaname = "September";
            break;
        case "10":
            monthaname = "October";
            break;
        case "11":
            monthaname = "November";
            break;
            monthaname = "December";
        case "12":
            break;
    }
}


function PopulatechangesDtls(day,AccountCode) {
    $("#tbl_changesDtls .last_row").before('<tr class="AddedRow" align="left"><td style="width:40px;"><input type="checkbox"/></td>' +
                          '<td style="width:80px;"><input type="text" readonly="readonly" style="width:100%;" value="' + day + '"/></td>' +
                          '<td style="width:150px;"><input type="text" readonly="readonly" value="' + AccountCode + '"/></td>' +
                          '<td style="width:150px;"><input type="text"/></td>' +
                          '</tr>');
}


function ApproveCoverageChanges(act_type) {

    var accounts = new Array();
    var EventId = txt_coveragetrackingno.val();
    var soId = txt_sales_id.val();
    var Month = txt_hidden_month.val();
    var year = txt_year.val();
    var disregardedAccounts = new Array();

    if (act_type != "RETURN_TO_REQUESTOR") {

        //this block of code checked if parent check box or the checkall checkbox is checked
        if ($("#chk_selectall").is(":checked") == true) {

            //foreach function that gets the data from the from addedRow which checkbox is checke per account
            $("#tbl_changesDtls .AddedRow").each(function () {
                accounts.push({
                   // Day: $(this).find('td:eq(1)').text(),
                    //AccountCode: $(this).find('td:eq(2)').text()
                    Day: $(this).find('td:eq(1) input').val(),
                    AccountCode: $(this).find('td:eq(2) input').val(),
                    RmrkChanges: $(this).find('td:eq(3) input').val()
                });
            });
        }
        //excutes when the check all textbox is not check
        else {
            $("#tbl_changesDtls .AddedRow").each(function () {
                var row = $(this);
                if (row.find("td:eq(0) input[type=checkbox]").is(":checked") == true) {

                    accounts.push({

//                        Day: $(this).find('td:eq(1)').text(),
                        //                        AccountCode: $(this).find('td:eq(2)').text()
                        Day: $(this).find('td:eq(1) input').val(),
                        AccountCode: $(this).find('td:eq(2) input').val(),
                        RmrkChanges: $(this).find('td:eq(3) input').val()

                    });
                }
            });
        }
    }
    //executes when the action type is return
    else {
        if ($("#chk_selectall").is(":checked") == true) {

            $("#tbl_changesDtls .AddedRow").each(function () {
                var cur_date = new Date();
                var formatdate = Eventmonth + "/" + $(this).find('td:eq(1)').text() + "/" + Eventyear;
                var converteddate = new Date(formatdate);
                var days = 24 * 60 * 60 * 1000;
                var datediff = converteddate.getTime() - cur_date.getTime();
                var interval = Math.floor(datediff / days);

                if (interval >= 2) {
                    accounts.push({
//                        Day: $(this).find('td:eq(1)').text(),
                        //                        AccountCode: $(this).find('td:eq(2)').text()
                        Day: $(this).find('td:eq(1) input').val(),
                        AccountCode: $(this).find('td:eq(2) input').val(),
                        RmrkChanges: $(this).find('td:eq(3) input').val()
                    });
                }
                else {

                    disregardedAccounts.push({
                        //AccountCode: $(this).find('td:eq(2)').text()
                        AccountCode: $(this).find('td:eq(2) input').val()

                    });
                }
            });
        }
        else {

            $("#tbl_changesDtls .AddedRow").each(function () {

                var row = $(this);
                if (row.find("td:eq(0) input[type=checkbox]").is(":checked") == true) {

                    var formatdate = Eventmonth + "/" + $(this).find('td:eq(1)').text() + "/" + Eventyear;
                    var converteddate = new Date(formatdate);
                    var cur_date = new Date();
                    var cur_day = cur_date.getDay();
                    var cur_month = cur_date.getMonth();
                    var cur_year = cur_date.getFullYear();
                    var formated_cur_date = cur_month + "/" + cur_day + "/" + cur_year;
                    var today_date = new Date(formated_cur_date);
                    var days = 24 * 60 * 60 * 1000;
                    var datediff = converteddate.getTime() - cur_date.getTime();
                    var interval = Math.floor(datediff / days);

                    if (interval >= 2) {

                        accounts.push({
//                            Day: $(this).find('td:eq(1)').text(),
                            //                            AccountCode: $(this).find('td:eq(2)').text()
                            Day: $(this).find('td:eq(1) input').val(),
                            AccountCode: $(this).find('td:eq(2) input').val(),
                            RmrkChanges:$(this).fins('td:eq(3) input').val()
                        });
                    }

                    else {
                        disregardedAccounts.push({
                            //AccountCode: $(this).find('td:eq(2)').text()
                            AccountCode: $(this).find('td:eq(2) input').val()
                        });
                    }
                }
            });
        }
    }


    //var new obj includes all the value in textfield

    var new_obj = {
        EventId: EventId,
        EmpIdNo: soId,
        Month: Month,
        Year: year,
        Acct_dtls: accounts,
        action_type: act_type
    };

    if (disregardedAccounts.length > 0) {
        var string_to_join = "";
        for (var i = 0; i < disregardedAccounts.length; i++) {
            string_to_join += disregardedAccounts[i].AccountCode + "\n";
        }
        alert("The following accounts cannot be returned:" + "\n" + string_to_join);
    
    }
    //alert(disregardedAccounts.toString());
    //alert(JSON.stringify(disregardedAccounts));

    DisplayPreloader();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/UpdateChanges",
        success: function (res) {
            alert("success");
            location.reload();
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}









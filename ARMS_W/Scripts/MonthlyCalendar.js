var ns4;

var tbl_yearcalendar = null;
var tdjan = null;
var tdfeb = null;
var tdmar = null;
var tdapr = null;
var tdmay = null;
var tdjun = null;
var tdjul = null;
var tdaug = null;
var tdsep = null;
var tdoct = null;
var tdnov = null;
var tddec = null;
var month_holder = null;
var year_holder = null ;
var redirectlink = null;
var monthname = null;
var txt_chse_year = null;

$(function () {

    tbl_yearcalendar = $("#tbl_yearcalendar");
    tdjan = $("#tdjan");
    tdfeb = $("#tdfeb");
    tdmar = $("#tdmar");
    tdapr = $("#tdapr");
    tdmay = $("#tdmay");
    tdjun = $("#tdjun");
    tdjul = $("#tdjul");
    tdaug = $("#tdaug");
    tdsep = $("#tdsep");
    tdoct = $("#tdoct");
    tdnov = $("#tdnov");
    tddec = $("#tddec");
    txt_chse_year = $("#txt_chse_year");

    //    var $tiles = $("#tile1").liveTile({
    //        playOnHover: true,
    //        repeatCount: 0,
    //        delay: 0,
    //        startNow: false
    //    });

    var curdate = new Date();
    var cur_year = curdate.getFullYear();

    txt_chse_year.attr("value", cur_year);

    year_holder = txt_chse_year.val();
    //Getyearlookups();








    tdjan.click(function () {

        //monthname = $(this).text();

        monthname = "january";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdfeb.click(function () {

        //monthname = $(this).text();
        monthname = "february";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;
    });

    tdmar.click(function () {
        
        //monthname = $(this).text();
        monthname = "march";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdapr.click(function () {

        //monthname = $(this).text();
        monthname = "april";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdmay.click(function () {

        //monthname = $(this).text();
        monthname = "may";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdjun.click(function () {

        //monthname = $(this).text();
        monthname = "june"
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdjul.click(function () {

        //monthname = $(this).text();
        monthname = "july"
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdaug.click(function () {

        //monthname = $(this).text();
        monthname = "august";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdsep.click(function () {

        //monthname = $(this).text();
        monthname = "september";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdoct.click(function () {

        //monthname = $(this).text();
        monthname = "october";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tdnov.click(function () {

        //monthname = $(this).text();
        monthname = "november";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });

    tddec.click(function () {

        //monthname = $(this).text();
        monthname = "december";
        getCurrMonthVW(monthname);
        redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //top.location.href = redirectlink;
        window.location = baseUrl + 'calendar/' + redirectlink;

    });




    tbl_yearcalendar.find("td").live({

        //        click: function () {
        //            monthname = $(this).text();
        //            getCurrMonthVW(monthname);
        //	        alert(monthname);
        //            alert(month_holder);
        //            redirectlink = 'MyCalendar?soId=' + userId + '&month=' + month_holder + '&year=' + year_holder;
        //            //alert(top.location.href = redirectlink);
        //            top.location.href = baseUrl+"Calendar/"+redirectlink;



        //        },
        mouseover: function () { $(this).css("background-color", "#f7f5f5"); },
        mouseout: function () { $(this).css("background-color", "#eeeeee"); }

    });








});



function getCurrMonthVW(str) {
    //this regex detects number only without space from input
    var regexnum = /[A-Za-z ]+/g;
    //this regex detects alphabet only without space from input
    var regexletters = /[0-9 ]+/g; ;
    var month = "";
    //mynew=str.replace(regexnum, "")
    month = str.replace(regexletters, "");
    month = month.toLowerCase();

    switch (month) {
        case "january":
            month_holder = "0";
            //year = str.replace(regexnum, "");
            break;
        case "february":
            month_holder = "1";
            //year = str.replace(regexnum, "");
            break;

        case "march":
            month_holder = "2";
            //year = str.replace(regexnum, "");
            break;
        case "april":
            month_holder = "3";
            //year = str.replace(regexnum, "");
            break;
        case "may":
            month_holder = "4";
            //year = str.replace(regexnum, "");
            break;
        case "june":
            month_holder = "5";
            //year = str.replace(regexnum, "");
            break;
        case "july":
            month_holder = "6";
            //year = str.replace(regexnum, "");
            break;
        case "august":
            month_holder = "7";
            //year = str.replace(regexnum, "");
            break;
        case "september":
            month_holder = "8";
            //year = str.replace(regexnum, "");
            break;
        case "october":
            month_holder = "9";
           // year = str.replace(regexnum, "");
            break;
   
        case "november":
            month_holder = "10";
            //year = str.replace(regexnum, "");
            break;
        case "december":
            month_holder = "11";
            //year = str.replace(regexnum, "");
            break;


    }

}



function Getyearlookups() {

    var new_obj = {


    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        //type: "POST", url: baseUrl + "Calendar/SavePlanEvents",
        type: "POST", url: baseUrl + "Calendar/getyearincoverage",
        success: function (res) {

            if (res.length != null && res.length != 0 && res.length != undefined) {

                $(res).each(function (index, item) {
                    $("#txt_chse_year").append(res);
                });
                $("#txt_chse_year").chosen();

            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}

function LookUpData(obj_id_to_store, str_data, par1) {
    DisplayPreloader();

    if (par1 == undefined) {
        par1 = "";
    }

    $.ajax({
        type: "POST", url: baseUrl + "Calendar/GetFilteredList",
        data: "_str_data=" + str_data + "&par1=" + par1,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));

            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

    // show dialog box/window
}



function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    } else {
        return "true";
    }
}


function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[0] != null) {
            if (res_cols[0] != "") {
                w = w + "<option valterritory = \"" + res_cols[1] + "\" year_cur=\"" + res_cols[0] + "\" val_area=\"" + res_cols[2] + "\" val_region=\"" + res_cols[3] + "\" value=\"" + res_cols[0] + "\">" + res_cols[0] + "</option>";
            }

        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    // $("body").after(w);
    $("body").append(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'));
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();

    $("#id_content_upload").remove();
    $("#id_bkg_upload").remove();
}


function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", $("#id_content select option:selected").text());
    $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));
    if (obj == "txt_chse_year") {
        $("#txt_chse_year").attr("value", GetValue($("#id_content select option:selected").attr("year_cur")));
    }
    year_holder = txt_chse_year.val();
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function GetId(strVal) {
    var tmp_str = strVal.substring(0, strVal.indexOf('-'));
    return tmp_str.replace(/^\s*|\s*$/gi, "");
}

function GetValue(strVal) {
    if (strVal.indexOf('-') > -1) {
        return strVal.substring(strVal.indexOf('-') + 2, 200);
    } else {
        return strVal;
    }
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}
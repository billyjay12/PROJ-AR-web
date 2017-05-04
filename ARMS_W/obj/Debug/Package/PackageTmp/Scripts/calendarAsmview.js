/*
*  @Author:        Hervie T. Inoc
*  @Date:          March 14, 2013
*  @Description:   Generate Calendar with event user define function
*  @Source: FullCalendar v1.5.4 Google Calendar Plugin
*  @Source Author: Adam Shaw 
* 
*/


//variable declaration
var d = new Date();
var date1 = d.getDate();
var remarks = null;
var txt_vw_EventId = null;
var btn_test = null;
var vw_monthholder = null;
var vw_yearholder = null;
var vw_month = null;
var btn_saveandsend = null;
var txt_vw_so_id = null;
var vw_calendar = null;
var btn_vw_prev = null;
var btn_vw_next = null;
var txt_vw_hidden_id = null;
var wv_sub_menu_link_2 = null;
var wv_sub_menu_link_3 = null;
var curr_id = null;
//var wv_sub_menu_link_3 = null;

var resizelement;
var tbl_calendar_viewholder = null;

var btn_approvedCoverage = null;
var btn_returntosender = null;
var txt_apprvrRemarks = null;

$(function () {
    vw_calendar = $("#vw_calendar");
    wv_sub_menu_link_2 = $("#wv_sub_menu_link_2");
    wv_sub_menu_link_3 = $("#wv_sub_menu_link_3");
    txt_vw_EventId = $("#txt_vw_EventId");
    tbl_calendar_viewholder = $("#tbl_calendar_viewholder");

    btn_approvedCoverage = $("#btn_approvedCoverage");
    btn_returntosender = $("#btn_returntosender");
    txt_apprvrRemarks = $("#txt_apprvrRemarks");

    //hide(specific td)
    tbl_calendar_viewholder.find(".hiddenTD").hide();
    $(".hiddenTD").hide();

    btn_approvedCoverage.hide();
    btn_returntosender.hide();

    $(window).resize(function () {
        vw_calendar.fullCalendar('option', 'height', get_calendar_height());
    });

    //This function creates the calendar  from first call
    vw_calendar.CreateCalendar();

    $("#year").text(pyear);

    $("#btn_prev_year").click(
        function () {
            var year = $("#year").text();
            var res = parseInt(year) - 1;
            $("#year").text(res);
        }
    );

    $("#btn_next_year").click(
        function () {
            var year = $("#year").text();
            var res = parseInt(year) + 1;
            $("#year").text(res);
        }
    );

    appendLegend();
    appendDatePicker();

//    var isChrome = window.chrome;
//    var isFirefox = $.browser.mozilla;
//    if (isChrome) { //only chrome support html5 input type=month
//        appendDatePicker();
//    }

    //this line of code populates the events on the designated date of events
    //per account per user/so

    GetEventDetail(soId, pmonth, pyear);

    // GetEventDetail();

    var vw_month1 = $(this).find(".fc-header-title").text();
    //a function belows which understand or simply convert string into number form
    //with its corresponding value of the month
    //eg: if returns 1-january
    //eg: if returns 2- february
    //note in javascript assignment of month into numbers, starts from 0-11: 0-january and 11- december
    //but here in this function i forcibly assign dates as number which understand 1 as january and 12-december 
    getCurrMonthVW(vw_month1);

    //variables declaration after calendar is instantiated
    //btn_prev = vw_calendar.fullCalendar('prev');
    //btn_next = vw_calendar.fullCalendar('next');
    btn_prev = vw_calendar.find('.fc-button');

    //actions when previous button in calendar is click
    btn_prev.click(
        function () {

            var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();

            getCurrMonthVW(month);

            //calendar.fullCalendar('destroy');
            var dateHolder = vw_monthholder;
            //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
            vw_calendar.fullCalendar('removeEvents');
            GetEventDetail(soId, vw_monthholder, vw_yearholder);

        }
    );

    //    wv_sub_menu_link_2.click(function () {
    //        UpdatePlanEvents("APPROVE");
    //    });
    //    wv_sub_menu_link_3.click(function () {
    //        UpdatePlanEvents("RETURN_TO_REQUESTOR");
    //    });

    btn_approvedCoverage.click(
        function () {
            UpdatePlanEvents("APPROVE");
        }
    );

    btn_returntosender.click(
        function () {
            UpdatePlanEvents("RETURN_TO_REQUESTOR")
        }
    );

    $('.fc-button-basicWeek span > span').click(
        function () {
            vw_calendar.fullCalendar('option', 'height', 350);
        }
    );

    $('.fc-button-month span > span').click(
        function () {
            vw_calendar.fullCalendar('option', 'height', 750);
        }
    );

    $('.fc-button-basicDay span > span').click(
        function () {
            vw_calendar.fullCalendar('option', 'height', 350);
        }
    );

});


//this user define function calls the calendar plug in to 
//generate calendar view
$.fn.CreateCalendar = function (u_month, u_year) {


    //    $(this).fullCalendar({
    //        month: parseInt(pmonth) - 1,
    //        year: parseInt(pyear),
    //        theme:true,
    //        header: {
    ////            left: 'prev,next today,basicDay,basicWeek,month',
    ////            center: 'title',
    //        //            right: ''
    //            left: '', //'prev,next today,basicDay,basicWeek,month',
    //            center: 'title',
    //            right: 'prev,next today,basicDay,basicWeek,month'//'' //right header is being used by the legends 
    //      
    //        },

    //        //height: 820,

    //        dayClick: function (date, allDay, jsEvent, view) {

    //            //curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
    //            //ShowRemarkTextbox(date);

    //            var curr_date = date.getDate();
    //            var temp_month = date.getMonth();
    //            var curr_month = temp_month + 1;
    //            var curr_year = date.getFullYear();

    //            $.fancybox({
    //                openEffect: 'elastic',
    //                closeEffect: 'elastic',
    //                type: 'iframe',
    //                href: baseUrl + "Calendar/memoview?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + soId,
    //                'overlayShow': true,
    //                'showCloseButton': true//,
    ////                'afterClose': function () {
    ////                    var casted_month = parseInt(curr_month) - 1;
    ////                    var redirectlink = 'CalendarView?soId=' + soId + '&month=' + casted_month + '&year=' + curr_year +'&EventId='+ txt_vw_EventId.val();
    ////                    window.location = baseUrl + 'calendar/' + redirectlink;
    ////                    // alert(month);

    ////                }
    //            });

    //        }

    //    });
    $(this).fullCalendar({
        month: parseInt(pmonth) - 1,
        year: parseInt(pyear),
        theme: true,
        header: {
            left: '', //'prev,next today,basicDay,basicWeek,month',
            center: 'prev, title, next',
            right: 'today,basicDay,basicWeek,month'//'' //right header is being used by the legends 
        },
        eventColor: '#ededed',
        eventTextColor: 'black',
//        dayRender: function (date, cell) {
//        
//            var today = new Date();
//            var end = new Date();
//            end.setDate(today.getDate()+7);
//        
//            if (date.getDate() === today.getDate()) {
//                cell.css("background-color", "red");
//            }
//        
//            if(date > today && date <= end) {
//                cell.css("background-color", "yellow");
//            }
//      
//        }   ,
        //optional 
        // height: 820,
        dayClick: function (date, allDay, jsEvent, view) {

            // curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
            // alert(date);

            //get the month from the day click
            var _date = new Date(date);
            var _month = new Date();
            var _cmonthHolder;
            _month = parseInt(_date.getMonth()) + 1;
            var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
            _cmonthHolder = vw_monthholder;


            if (_month == _cmonthHolder) {
                var curr_date = date.getDate();
                var temp_month = date.getMonth();
                var curr_month = temp_month + 1;
                var curr_year = date.getFullYear();
                // ShowRemarkTextbox(date);

                $.fancybox({
                    openEffect: 'elastic',
                    closeEffect: 'elastic',
                    type: 'iframe',
                    //href: baseUrl + "Calendar/Memo?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id + "&acctCode=",
                    href: baseUrl + "Calendar/memoview?date=" + _date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + soId + "&acctCode=",
                    'overlayShow': true,
                    'showCloseButton': true,
                    // height: "500",
                    //'Monthvalue': curr_month,
                    //'Yearvalue': curr_year
                    'afterClose': function () {
                        // alert(curr_month);
                        var casted_month = parseInt(curr_month) - 1;
                        var getDate1 = $("#vw_calendar").fullCalendar('getDate');
                        var redirectlink = 'MyCalendar?soId=' + soId + '&month=' + casted_month + '&year=' + getDate1.getFullYear();
                        // window.location = baseUrl + 'calendar/' + redirectlink;


                        if (!is_processing) {

                            is_processing = true;
                            //                            var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
                            //                            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
                            //                            getCurrMonthVW(month);

                            //                            var dateHolder = vw_monthholder;
                            //                            calendar.fullCalendar('removeEvents');

                            //                            GetEventDetail(id, vw_monthholder, vw_yearholder);
                            //                            NxtInventorySked(vw_monthholder, vw_yearholder, userId);
                            var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();

                            getCurrMonthVW(month);


                            //calendar.fullCalendar('destroy');
                            var dateHolder = vw_monthholder;
                            //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
                            vw_calendar.fullCalendar('removeEvents');
                            GetEventDetail(soId, vw_monthholder, vw_yearholder);

                        }
                    },
                    // 'autoScale': true,

                    scrolling: 'no',
                    autoSize: false,
                    padding: 0,
                    helpers: {
                        overlay: {
                            css: { 'overflow': 'hidden' }
                        },
                        overlay: { closeClick: false }
                    }
                });
            }

        },

        eventClick: function (calEvent, jsEvent, view) {
            //curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
            //   alert(calEvent.start);
            //get the month from the day click
            var _date = new Date(calEvent.start);
            var _month = new Date();
            var _cmonthHolder;
            _month = parseInt(_date.getMonth()) + 1;
            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
            _cmonthHolder = vw_monthholder;


            if (_month == _cmonthHolder) {
                var curr_date = _date.getDate();
                var temp_month = _date.getMonth();
                var curr_month = temp_month + 1;
                var curr_year = _date.getFullYear();
                // ShowRemarkTextbox(date);

                $.fancybox({
                    openEffect: 'elastic',
                    closeEffect: 'elastic',
                    type: 'iframe',
                    // href: baseUrl + "Calendar/Memo?date=" + _date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id + "&acctCode=" + calEvent.title,
                    href: baseUrl + "Calendar/memoview?date=" + _date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + soId + "&acctCode=" + calEvent.title,
                    'overlayShow': true,
                    'showCloseButton': true,
                    scrolling: 'no',
                    autoSize: false,
                    //'Monthvalue': curr_month,
                    //'Yearvalue': curr_year
                    'afterClose': function () {
                        var casted_month = parseInt(curr_month) - 1;
                        var getDate1 = $("#vw_calendar").fullCalendar('getDate');
                        var redirectlink = 'MyCalendar?soId=' + soId + '&month=' + casted_month + '&year=' + getDate1.getFullYear();
                        //window.location = baseUrl + 'calendar/' + redirectlink;
                        // alert(month);


                        if (!is_processing) {

                            is_processing = true;
                            //                            var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
                            //                            var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
                            //                            getCurrMonthVW(month);

                            //                            var dateHolder = vw_monthholder;
                            //                            calendar.fullCalendar('removeEvents');

                            //                            GetEventDetail(id, vw_monthholder, vw_yearholder);
                            //                            NxtInventorySked(vw_monthholder, vw_yearholder, userId);
                            var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();

                            getCurrMonthVW(month);

                            //calendar.fullCalendar('destroy');
                            var dateHolder = vw_monthholder;
                            //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
                            vw_calendar.fullCalendar('removeEvents');
                            GetEventDetail(soId, vw_monthholder, vw_yearholder);

                        }

                    },
                    padding: 0,
                    helpers: {
                        overlay: {
                            css: { 'overflow': 'hidden' }
                        },
                        overlay: { closeClick: false }
                    },
                    afterShow: function () {
                        $(".fancybox-close").hide();
                        setTimeout(function () {
                            $(".fancybox-close").fadeIn();
                        }, 3000); // show close button after 3 seconds
                    }
                });
            }
        }
    });
}


//function GetEventDetail(soId, pmonth, pyear) {
function GetEventDetail(soid,month,year) {

    //var u_id = id == undefined ? userId : id;
    //var u_month = month

  //  var view_id = $("#vw_calendar").find('.fc-button.fc-state-active').find('span > span').text(); // <-- use this if calendar doesn't use theme ui
   var view_id = $("#vw_calendar").find('.fc-button.ui-state-active').find('span > span').text();// <-- use this if calendar uses theme ui

    var new_obj = {
        userId: soid, //soId, //userId
        month: month, //pmonth,
        year: year,//pyear,
        viewtype: view_id 
    }
    DisplayPreloader();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetCoverageByDate",
        success: function (res) {
            if (res.data.info.length != undefined) {
                vw_calendar.fullCalendar('addEventSource', res.data.info);
                //                $(res.data.info).each(function (index, item) {


                //                    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
                //                    //                    
                //                    var completeDate = new Date(completeDate1);
                //                    var vw_calendar = $('#vw_calendar');

                //                    /**var Bluecolor = '#5C9DDE';
                //                    var Redcolor = '#FF5F5F';
                //                    var Yellowcolor = '#E8E819';
                //                    var GreenColor = '#59DEBC';
                //                    var Graycolor = '#B1CACA';

                //                    //                    var colorholder = '99FF99';
                //                    //                    vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: colorholder }, true);

                //                    //Regular in Coverage
                //                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Bluecolor }, true); }

                //                    //DELETED ACCOUNT
                //                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }
                //                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "T")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }

                //                    // EDITED EITHER APPROVED, FOR ASM APPROVAL,RETURNED
                //                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus != "3")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Yellowcolor }, true); }

                //                    //UNPLANNED
                //                    if (item.isPlanned == "F" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: GreenColor }, true); }

                //                    //DISAPPROVED
                //                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus == "3")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Graycolor }, true); }
                //                    **/

                //                    var plan_color = '#92d050';
                //                    var deleted_color = '#f8f8f8';
                //                    var visited_color = '#ffc000'
                //                    var edited_color = '#ccffcc';
                //                    var unplanned = '#00BFFF';

                //                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.hasCallreport == "F")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: plan_color }, true); }
                //                    if (item.IsDeleted == "T")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: deleted_color }, true); }
                //                    if (item.hasCallreport == "T" && item.IsDeleted == "F" && item.isPlanned == "T")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: visited_color }, true); }
                //                    if (item.IsAnEdit == "T" && item.hasCallreport == "F" && item.AcctStatus != "7")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: edited_color }, true); }
                //                    if (item.isPlanned == "F")
                //                    { vw_calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: unplanned }, true); }
                //                    if (item.IsAnEdit == "T" && item.hasCallreport == "F" && item.AcctStatus == "7")
                //                    { vw_calendar.fullCalendar('renderEvent', { id: "disapproved", title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: edited_color }, true); }

                //                });

                $("#doc_stat2_msg").html(res.data.docstatus);
                $('#txt_vw_EventId').attr("value", res.data.EventId);

                btn_approvedCoverage.hide();
                btn_returntosender.hide();

                if (res.data.docstatus.toUpperCase() == "FOR ASM APPROVAL") {
                    btn_approvedCoverage.show();
                    btn_returntosender.show();
                }

                if (res.data.docstatus.toUpperCase() == "FOR CHANNEL MANAGER APPROVAL") {
                    btn_approvedCoverage.show();
                    btn_returntosender.show();
                }

                if (res.data.docstatus.toUpperCase() == "FOR RSM APPROVAL") {
                    btn_approvedCoverage.show();
                    btn_returntosender.show();
                }

                if (res.data.docstatus.toUpperCase() == "FOR VP-SALES APPROVAL") {
                    btn_approvedCoverage.show();
                    btn_returntosender.show();
                }

                //  HidePreloader();
                is_processing = false;

                HidePreloader();
            } else {
                HidePreloader();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);

            HidePreloader();
        }
    });
}

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
            vw_monthholder = "1";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "february":
            vw_monthholder = "2";
            vw_yearholder = str.replace(regexnum, "");
            break;

        case "march":
            vw_monthholder = "3";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "april":
            vw_monthholder = "4";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "may":
            vw_monthholder = "5";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "june":
            vw_monthholder = "6";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "july":
            vw_monthholder = "7";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "august":
            vw_monthholder = "8";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "september":
            vw_monthholder = "9";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "september":
            vw_monthholder = "9";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "october":
            vw_monthholder = "10";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "november":
            vw_monthholder = "11";
            vw_yearholder = str.replace(regexnum, "");
            break;
        case "december":
            vw_monthholder = "12";
            vw_yearholder = str.replace(regexnum, "");
            break;
    }
}

/*
*
Work around functions to get the values needed from calendar.
Data may also obtain from functions under the plug in functions
but here I made my own functions to caputure data that i need.
*
*/
function ShowRemarkTextbox(date) {
    var w = new Array();

    var curr_date = date.getDate();
    var temp_month = date.getMonth();
    var curr_month = temp_month + 1;
    var curr_year = date.getFullYear();

    w.push("<div id=\"remarks_bkg\" style=\"display:none; opacity: 0.60; filter:alpha(opacity=60); position:fixed; top:0; left:0; height:100%; width:100%; background:#323232;\"></div>");
    w.push("<div id=\"remarks_content\" style=\"display:none; position:absolute; top:0; left:0; height:100%; width:100%; \">");
    w.push("<table    oncontextmenu=\"return false\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"height:100%; width:100%; \" >");
    w.push("<tr>");
    w.push("<td valign=\"middle\" align=\"center\">");

    w.push("<table id=\"tbl_iframeHolder\" style=\"width:90%; height:100%; background-color:White;\">");
    w.push("<tr><td style=\"background:#ededed;\" colspan=\"3\" align=\"right\"><a command=\"close\" href=\"javascript:;\">Close</a></td></tr>");


    w.push("<tr>");
    w.push("<td>");

    w.push("<iframe id=\"frame_js\" src=\"" + baseUrl + "Calendar/memoview?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + soId + "\" target=\"myframe\" frameborder=\"1\" border-color=\"#c9ad97\"  style=\"width:100%; height:350px; background-color:#f7f4c5;\" >");
    w.push("</iframe>");

    w.push("</td>");
    w.push("</tr>");

    w.push("</table>");

    w.push("</td>");
    w.push("</tr>");
    w.push("</table>");
    w.push("</div>");

    $("body").append(w.join(""));

    var remarks_bkg = null;
    remarks_bkg = $("#remarks_bkg");

    var remarks_content = null;
    remarks_content = $("#remarks_content");

    var tbl_content_details = remarks_content.find(".tbl_details");

    var btn_proceed = null;

    var btn_proceed = $("#btn_proceed");

    var close_btn = remarks_content.find("a[command=close]");
    var collection_btn = remarks_content.find("a[command=collection]");
    var sales_btn = remarks_content.find("a[command=sales]");
    var visit_btn = remarks_content.find("a[command=visit]");

    close_btn.click(
        function () {
            var s = "";

            var current_row_no = remarks_content.find("input[type=hidden]").attr("value");

            if (current_row_no != "") {
                var remark_val = $("#txt_remarks").attr('value');
            }
            remarks_content.hide("fast", function () {
                remarks_bkg.hide();

                remarks_content.remove();
                remarks_bkg.remove();
            });

            location.reload();
        }
      );

    remarks_bkg.show();
    remarks_content.show("fast");
}


function UpdatePlanEvents(act_type) {
    var params =
     {
         EventID: txt_vw_EventId.val(),
         action_type: act_type,
         EmpIdNo: soId,
         Month: pmonth,
         Year: pyear,
         ApprvrRrmks: txt_apprvrRemarks.val()
     }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        //type: "POST", url: baseUrl + "Calendar/SavePlanEvents",
        type: "POST", url: baseUrl + "Calendar/SaveCoveragePlan",
        success: function (res) {
            alert("Success!");
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}

function doneResizing() {
    vw_calendar.fullCalendar('removeEvents');
    GetEventDetail(soId, pmonth, pyear);
}

function get_calendar_height() {
    return $(window).height() - 30;
}

function appendLegend() {
    var legend = '<table style="text-align:left;">' +
                   '<tr>' +
                       '<td><span style="font-weight:bold">Legend:</span></td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/darkgreenlegend.png" /> Plan</td> ' +
                       '<td><img alt="" src="' + baseUrl + 'Images/graylegend.png" /> Deleted</td> ' +
                       '<td><img alt="" src="' + baseUrl + 'Images/yellolegend.png" /> Visited</td>' +
                    '</tr>' +
                    '<tr>' +
                       '<td></td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/greenlegend.png" /> Edited</td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/bluelegend.png" /> Unplanned</td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/pinklegend.png" /> Unsaved Inventory</td>' +
                    '</tr>' +
                '</table>';

    $('.fc-header-left').append(legend);
}

//function appendDatePicker() {
//    var custom_buttons = '<td style="padding-left:10px; float:right; min-width:20px">' +
//                                    '<div>' +
//                                        '<span>' +
//                                            'Go to: <input type="month" id="gotodate" onchange="load_datepicker()" >' +
//                                        '</span>' +
//                                    '</div>' +
//                                '</td>';
//    $('.fc-header-title').parent('td').after(custom_buttons);

//}

//function load_datepicker() {
//        var d = $("#gotodate").attr("value");
//        var splitdate = d.split('-');

//        var month = new Array("", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12");
//        var int_month = parseInt(splitdate[1]);

//        $("#vw_calendar").fullCalendar('gotoDate', splitdate[0], month[int_month], splitdate[2]);

//        var month = $("#vw_calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
//        getCurrMonthVW(month);

//        //calendar.fullCalendar('destroy');
//        var dateHolder = vw_monthholder;
//        //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
//        vw_calendar.fullCalendar('removeEvents');
//        GetEventDetail(soId, vw_monthholder, vw_yearholder);
//}
/* end */

function appendDatePicker() {
//    var custom_buttons = '<td style="padding-left:10px; float:right; min-width:20px">' +
//                                    '<div>' +
//                                        '<span>' +
//                                            'Go to: <input type="date" id="gotodate" onchange="load_datepicker()" >' +
//                                        '</span>' +
//                                    '</div>' +
//                                '</td>';
    //    $('.fc-header-title').parent('td').after(custom_buttons);
    var custom_buttons = '<span style="vertical-align:center">' +
                            'Go to: <input type="text" id="gotodate" style="width:100px; vertical-align:center;"> &nbsp;' +
                        '</span>';
    $('.fc-header-right').find("span:eq(0)").before(custom_buttons);

    $('#gotodate').datepicker({
        //comment the beforeShow handler if you want to see the ugly overlay
        dateFormat: 'yy-mm-dd',
        beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 900);
            }, 0);
        },
        onSelect: function () {
            var day1 = $("#gotodate").datepicker('getDate').getDate();
            var month1 = $("#gotodate").datepicker('getDate').getMonth() + 1;
            var year1 = $("#gotodate").datepicker('getDate').getFullYear();
            var fullDate = year1 + "-" + month1 + "-" + day1;
            load_datepicker(year1, month1, day1);
        }
    });
}

function load_datepicker(year1, month1, day1) {

    if (!is_processing) {
        var fullDate = year1 + "-" + month1 + "-" + day1;

        vw_calendar.fullCalendar('changeView', 'basicDay');
        vw_calendar.fullCalendar('option', 'height', 350);

        vw_calendar.fullCalendar('gotoDate', year1, month1 - 1, day1);

        is_processing = true;
        //var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");

        vw_calendar.fullCalendar('removeEvents');

        getalldayevents(soId, month1, year1, day1);
    }
}

//function load_datepicker() {
//    if (!is_processing) {
//        var d = $("#gotodate").attr("value");
//        var splitdate = d.split('-');

//        var month = new Array("", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12");
//        var int_month = parseInt(splitdate[1]);
//        var day = parseInt(splitdate[2]);

//        vw_calendar.fullCalendar('changeView', 'basicDay');
//        vw_calendar.fullCalendar('option', 'height', 350);

//        vw_calendar.fullCalendar('gotoDate', splitdate[0], month[int_month], splitdate[2]);


//        is_processing = true;
//        //var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");

//        vw_calendar.fullCalendar('removeEvents');

//        getalldayevents(soId, int_month, splitdate[0], splitdate[2]);
//       // NxtInventorySked(int_month, splitdate[0], userId);

//    }
//}

function getalldayevents(id, month, year, day) {
    var new_obj = {
        userId: id,
        month: month,
        year: year,
        day: day
    }

    DisplayPreloader();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetAllDayEvents",
        success: function (res) {
            vw_calendar.fullCalendar('addEventSource', res.data.AllDayEvents);
            is_processing = false;

            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            is_processing = false;
            HidePreloader();
        }
    });
}

function gotodate(month, elem) {
    is_processing = true;
   // var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
    var dateHolder = vw_monthholder;
    vw_calendar.fullCalendar('removeEvents');

    vw_monthholder = parseInt(month) + 1;
    vw_yearholder = $("#year").text();

    vw_calendar.fullCalendar('gotoDate', $("#year").text(), month);
    GetEventDetail(soId, vw_monthholder, vw_yearholder);
}










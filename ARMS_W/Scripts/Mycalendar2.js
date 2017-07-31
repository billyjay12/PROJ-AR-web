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
var txt_EventId = null;
var btn_test = null;
var vw_monthholder = null;
var vw_yearholder = null;
var vw_month = null;
var btn_saveandsend = null;
var txt_so_id = null;
var calendar = null;
var btn_prev = null;
var btn_next = null;
var txt_hidden_id = null;
var sub_menu_link_2 = null;
var curr_id = null;
var sub_menu_link_3 = null;

var resizelement;
//table calendar holder
var tbl_calendarholder = null;
var btn_SaveAndSend = null;

//inserted by billy jay delima
var lnk_upload_excel = null;
var uploaded_data = null;

var btn_save_upload = null;
var btn_cancel_upload = null;

var counter_id_upload = null;

var is_processing = false;
//end


var selectedsoid = null;

$(document).ready(function () {

   
});


$(function () {

    //assigning variables to an object
    txt_EventId = $("#txt_EventId");
    btn_test = $("#btn_test");
    btn_saveandsend = $("#btn_saveandsend");
    txt_so_id = $("#txt_so_id");
    calendar = $('#calendar');
    txt_hidden_id = $('#txt_hidden_id');
    sub_menu_link_2 = $('#sub_menu_link_2');
    sub_menu_link_3 = $('#sub_menu_link_3');
    tbl_calendarholder = $("#tbl_calendarholder");

    //$(".hiddenTD").hide();

    tbl_calendarholder.find(".hiddenTD").hide();

    //This function instantiate the calendar from first call

    btn_SaveAndSend = $("#btn_SaveAndSend");

    btn_SaveAndSend.hide();

    //inserted by billy jay
    $("#tab_main").tabs();

    lnk_upload_excel = $("#lnk_upload_excel");

    btn_cancel_upload = $("#btn_cancel_upload");
    btn_save_upload = $("#btn_save_upload");
    //end

    $("#year").text(pyear);

    $(".hiddenTR").hide();

    $(".month").click(function () {
        $('.month').removeClass('selected');  // remove selected from any other item first
        $('.month').addClass('ui-state-default');
        $(this).removeClass('ui-state-default');
        $(this).addClass('selected');
    });

    $("#btn_prev_year").click(function () {

        var year = $("#year").text();
        var res = parseInt(year) - 1;
        $("#year").text(res);
    });

    $("#btn_next_year").click(function () {
        var year = $("#year").text();
        var res = parseInt(year) + 1;
        $("#year").text(res);
    });


    $("#soList").change(function () {
        /* inserted by billy jay delima */

        $("#lnk_upload_excel").unbind();
        $("#lnk_upload_excel").uploadlink2(
        //getDialog(
        baseUrl + "Calendar/UploadExcelDataCoveragePlan_Monthly?EventMonth=" + vw_monthholder + "&empIdNo=" + $(this).val() + "&EventYear=" + vw_yearholder,
                    "txt_FileAttachment",
                    "TESTING",
                    function (counterid) {
                        // window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
                        // ShowFramePreviewUpload();
                        displayUploaded(counterid);

                        counter_id_upload = counterid;
                    }
                );
        /* end */
    });

    // $("#radio").buttonset();
    /** OPTION 1 FOR WINDOW RESIZING METHOD PLS.DONT
    **  DELETE THIS COMMENTED CODE****  

    

    $(window).resize(function () {
    // $('#calendar').append('<div>Handler for .resize() called.</div>');
    //destroying calendar from first create
    calendar.fullCalendar('destroy');

    //creation of new calendar after being destroyed
    calendar.CreateCalendar();

    //removing events inside the calendar
    //assignment or populating events is done after the browser or windows 
    //done resizing. 
    calendar.fullCalendar('removeEvents');

    //assigning interval value to identify
    //whether the window pane is done refreshing or resizing.
    clearInterval(resizelement);

    //after interval is being set
    //it was refered as equal as timeout or delay before it 
    //populates the data or events back to the calendar pane
    resizelement = setTimeout(doneResizing, 100);
    //end-- resizing of calendar is done



    });

    **/


    $(window).resize(function () {
        //calendar.fullCalendar('option', 'height', get_calendar_height());
    });
    calendar.CreateCalendar();
    // calendar.fullCalendar('option', 'height', get_calendar_height());
    appendLegend();

    // var isChrome = window.chrome;
    // var isFirefox = $.browser.mozilla;
    //if (isChrome) { //only chrome support html5 input type=month
    appendDatePicker();
    //}
    //work arround implementation to get the current state(month, day, year)
    curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
    //get the current view month from text displayed above
    // and convert it to number form
    vw_month = $(this).find(".fc-header-title").text();
    //a function belows which understand or simply convert string into number form
    //with its corresponding value of the month
    //eg: if returns 1-january
    //eg: if returns 2- february
    //note in javascript assignment of month into numbers, starts from 0-11: 0-january and 11- december
    //but here in this function i forcibly assign dates as number which understand 1 as january and 12-december 
    getCurrMonthVW(vw_month);

    //selectedsoid = $("#xyz").attr('value');
    //    $('#soAccount').change(function () {
    //        selectedsoid = $('#soAccount option:selected').attr('value');
    //        alert(selectedsoid);
    //    });

    //Load if there are pre set inventory count schedule
    // CountInventorySchedule(vw_monthholder, vw_yearholder, userId);


    //This function add events from current log in.
    GetEventDetail(userId, vw_monthholder, vw_yearholder);
    NxtInventorySked(vw_monthholder, vw_yearholder, userId);

    $("#txt_so_id").lookdown(
    { "url": baseUrl + "Calendar/getSalesOfficerList", "index_value": "1", "display_rowindex": "1" },
    { userId: userId },
     function (res) { return res; },
    function (res, all) {
        $("#txt_so_id").attr("value", res);

        //destroy the calendar
        calendar.fullCalendar('destroy');

        //create a new calendar
        calendar.CreateCalendar();

        //this function load the event of every SO 
        GetEventDetail(all[1], vw_monthholder, vw_yearholder);

        //hidden id for SO selected.. use as reference
        txt_hidden_id.attr('value', all[1]);
        //    appendLegend();
        //appendDatePicker();
    });

    //variables declaration after calendar is instantiated
    //btn_prev = calendar.find('.fc-button-prev span > span > span'); //calendar.fullCalendar('prev');
    btn_next = calendar.find('.fc-button-next span > span > span'); //calendar.fullCalendar('next'); //$('.fc-button-prev span > span > span')
    btn_prev = calendar.find('.fc-button');



    //actions when previous button in calendar is click
    btn_prev.click(function (e) {

        if (!is_processing) {

            is_processing = true;
            var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
            getCurrMonthVW(month);

            //alert(calendar.find('.fc-button.fc-state-active').find('span > span').text());


            //calendar.fullCalendar('destroy');
            var dateHolder = vw_monthholder;
            //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
            calendar.fullCalendar('removeEvents');

            //Load if there are pre set inventory count schedule
            //CountInventorySchedule(vw_monthholder, vw_yearholder, userId);

            //inserted by billy jay delima
            //        var date = $("#calendar").fullCalendar('getDate');
            //        vw_monthholder = date.getMonth();
            //        vw_yearholder = date.getFullYear();
            //        if (vw_monthholder == 0) {
            //            vw_monthholder = 1;
            //            vw_yearholder = date.getFullYear() - 1;
            //        }
            //end

            NxtInventorySked(vw_monthholder, vw_yearholder, userId);
            GetEventDetail(id, vw_monthholder, vw_yearholder);

        }
    });

    //    //actions when previous button in calendar is click
    //    btn_next.click(function () {

    //        var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
    //        var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
    //        getCurrMonthVW(month);

    //        //calendar.fullCalendar('destroy');
    //        var dateHolder = vw_monthholder;
    //        //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
    //        calendar.fullCalendar('removeEvents');

    //        //Load if there are pre set inventory count schedule
    //        //CountInventorySchedule(vw_monthholder, vw_yearholder, userId);

    //        //inserted by billy jay delima
    ////        var date = $("#calendar").fullCalendar('getDate');
    ////        vw_monthholder = date.getMonth() + 2;
    ////        vw_yearholder = date.getFullYear();
    ////        if (vw_monthholder == 13) {
    ////            vw_monthholder = 1;
    ////            vw_yearholder = date.getFullYear() + 1;
    ////        }
    //        //end


    //        GetEventDetail(id, vw_monthholder, vw_yearholder);
    //        NxtInventorySked(vw_monthholder, vw_yearholder, userId);

    //    });




    $("#btnRefresh").click(function () {
        location.reload() = true;
    });



    //button Approved

    /** sub_menu_link_2.click(function () {

    UpdatePlanEvents("APPROVE");
    }); **/

    btn_SaveAndSend.click(function () {

        SaveNextinventoryCount(vw_monthholder, vw_yearholder, userId);
        // UpdatePlanEvents("APPROVE");
        UpdatePlanEvents("SaveAndSend");
    });



    sub_menu_link_3.click(function () {
        UpdatePlanEvents("RETURN_TO_REQUESTOR");

    });

    btn_cancel_upload.click(function () {
        if (confirm("Are you sure you want to cancel?")) {
            delete_uploaded_files();
        }
    });

    btn_save_upload.click(function () {
        var name = $("#soList option:selected").text();
        var date = $(".fc-header-title").text();
        var getDate = $("#calendar").fullCalendar('getDate');

        if (confirm("You are about to perform a batch upload for " + name + " \n with choosen month and year: " + date)) {
            if ($("#soList option:selected").val() != "") {
                if (confirm("Are you sure?")) {
                    save_uploaded_files();
                }
            }
            else {
                alert("Please select SO Account");
            }
        }
        $(this).hide();
    });


    $('.fc-button-basicWeek span > span').click(function () {
        calendar.fullCalendar('option', 'height', 350);

        // calendar.css("width", "100%");
    });
    $('.fc-button-month span > span').click(function () {
        calendar.fullCalendar('option', 'height', 750);

        // calendar.css("width", "100%");
    });
    $('.fc-button-basicDay span > span').click(function () {
        calendar.fullCalendar('option', 'height', 350);
        //calendar.parent().css("align", "center");
        // calendar.css("width", "1100px");
        calendar.fullCalendar('renderEvents');
    });

});


/** This generate a calendar with event triggered **/

$.fn.CreateCalendar = function () {

    var month = parseInt(pmonth);
    var year = parseInt(pyear);

    $(this).fullCalendar({
        month: month,
        year: year,
        theme: true,
        header: {
            left: '', //'prev,next today,basicDay,basicWeek,month',
            center: 'prev, title, next',
            right: 'today,basicDay,basicWeek,month'//'' //right header is being used by the legends 
        },
        eventColor: '#ededed',
        eventTextColor: 'black',
        //optional 
        // height: 820,
        dayClick: function (date, allDay, jsEvent, view) {
            curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
            // alert(date);

            //get the month from the day click
            var _date = new Date(date);
            var _month = new Date();
            var _cmonthHolder;
            _month = parseInt(_date.getMonth()) + 1;
            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
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
                    href: baseUrl + "Calendar/Memo?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id + "&acctCode=",
                    'overlayShow': true,
                    'showCloseButton': true,
                    // height: "500",
                    //'Monthvalue': curr_month,
                    //'Yearvalue': curr_year
                    'afterClose': function () {
                        // alert(curr_month);
                        var casted_month = parseInt(curr_month) - 1;
                        var getDate1 = $("#calendar").fullCalendar('getDate');
                        var redirectlink = 'MyCalendar?soId=' + soId + '&month=' + casted_month + '&year=' + getDate1.getFullYear();
                        // window.location = baseUrl + 'calendar/' + redirectlink;


                        if (!is_processing) {

                            is_processing = true;
                            var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
                            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
                            getCurrMonthVW(month);

                            var dateHolder = vw_monthholder;
                            calendar.fullCalendar('removeEvents');

                            NxtInventorySked(vw_monthholder, vw_yearholder, userId);
                            GetEventDetail(id, vw_monthholder, vw_yearholder);
                           
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
        eventRender: function (calEvent, element, monthView) {
            //if (event.className == "holiday") {
            //    $day = $date.getDate();
            $(this).css("background-color", "#ededed");

            var w = "";
            var has_row = false;
            // element.qtip({
            //     content: calEvent.title
            // });
            var view_id = $("#calendar").find('.fc-button.ui-state-active').find('span > span').text(); //<-- use this if calendar uses theme ui
           // alert(view_id);
            if (view_id == "Daily") {
                has_row = false;
                w = '<div>';
                w += '<table border="1" width="30%" style="background:#ededed;" class="ui-state-default" >';
                w += '<tr>';
                w += '<td style="font-weight: bold; text-align:center;">Brand</td>';
                w += '<td style="font-weight: bold; text-align:center;">Amount</td>';
                w += '</tr>';
                $(calEvent.salesdetails).each(function (i, e) {
                    w += '<tr>';
                    w += '<td>' + e.brand + '</td>';
                    w += '<td>' + addCommas(e.amount) + '</td>';
                    w += '</tr>';
                    has_row = true;
                });
                w += '<tr>';
                w += '<td style="text-align:right">Total:</td>';
                w += '<td>' + addCommas(calEvent.totalSales) + '</td>';
                w += '</tr>';
                w += '</table>';
                w += '</div>';
                w = has_row ? w : "";
                element.find("span").append(w);
            }
        },
       // eventAfterRender: function(event, element, view) {
                      //$element).css('width','px');
                  //  },
        eventClick: function (calEvent, jsEvent, view) {
            //curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
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
                var acctCode = "";
                // ShowRemarkTextbox(date);
                // var view_id = calendar.find('.fc-button.fc-state-active').find('span > span').text(); //<-- use this if calendar doesn't use theme ui
                var view_id = $("#calendar").find('.fc-button.ui-state-active').find('span > span').text(); //<-- use this if calendar uses theme ui

                acctCode = calEvent.title;
                if (view_id.toUpperCase() == "DAILY")
                    acctCode = calEvent.id;

                $.fancybox({
                    openEffect: 'elastic',
                    closeEffect: 'elastic',
                    type: 'iframe',
                    href: baseUrl + "Calendar/Memo?date=" + _date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id + "&acctCode=" + acctCode,
                    'overlayShow': true,
                    'showCloseButton': true,
                    scrolling: 'no',
                    autoSize: false,
                    //'Monthvalue': curr_month,
                    //'Yearvalue': curr_year
                    'afterClose': function () {
                        var casted_month = parseInt(curr_month) - 1;
                        var getDate1 = $("#calendar").fullCalendar('getDate');
                        var redirectlink = 'MyCalendar?soId=' + soId + '&month=' + casted_month + '&year=' + getDate1.getFullYear();
                        //window.location = baseUrl + 'calendar/' + redirectlink;
                        // alert(month);


                        if (!is_processing) {

                            is_processing = true;
                            var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
                            var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
                            getCurrMonthVW(month);

                            var dateHolder = vw_monthholder;
                            calendar.fullCalendar('removeEvents');

                            NxtInventorySked(vw_monthholder, vw_yearholder, userId);
                            GetEventDetail(id, vw_monthholder, vw_yearholder);

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
                        }, 1000); // show close button after 3 seconds
                    }
                });
            }
        }


    });


}

//$('#calendar').fullCalendar({
//    windowResize: function (view) {
//        alert('The calendar has adjusted to a window resize');
//    }
//});


function ShowRemarkTextbox(date) {
    var w = new Array();

    var curr_date = date.getDate();
    var temp_month = date.getMonth();
    var curr_month = temp_month + 1;
    var curr_year = date.getFullYear();

    w.push("<div id=\"remarks_bkg\" style=\"display:none; opacity: 0.60; filter:alpha(opacity=60); position:fixed; top:0; left:0; height:100%; width:100%; background:#323232; z-index:9;\"></div>");
    w.push("<div id=\"remarks_content\" style=\"display:none; position:absolute; top:0; left:0; height:100%; width:100%; z-index:9; \">");
    w.push("<table    oncontextmenu=\"return false\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"height:100%; width:100%; \" >");
    w.push("<tr>");
    w.push("<td valign=\"middle\" align=\"center\">");

    w.push("<table id=\"tbl_iframeHolder\" style=\"width:90%; height:100%; background-color:White;\">");
    w.push("<tr><td style=\"background:#ededed;\" colspan=\"3\" align=\"right\"><a command=\"close\" href=\"javascript:;\">Close</a></td></tr>");


    w.push("<tr>");
    w.push("<td>");

    w.push("<iframe id=\"frame_js\" src=\"" + baseUrl + "Calendar/Memo?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id + "\" target=\"myframe\" frameborder=\"1\" border-color=\"#c9ad97\"  style=\"width:100%; height:350px; background-color:#f7f4c5;\" >");
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


    //    btn_proceed.click(function () {

    //        if ($("#txt_remarks").attr('value') != "") {


    //            remarks = $("#txt_remarks").attr('value');
    //            var title = remarks;
    //            var calEvent = "renderEvent";
    //            $('#calendar').fullCalendar('renderEvent', { title: title, start: date, allDay: true }, true);
    //            remarks_content.hide("fast", function () {
    //                remarks_bkg.hide();

    //                remarks_content.remove();
    //                remarks_bkg.remove();
    //            });
    //        }


    //        else
    //            alert("Input remarks");
    //    });

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


function GetEventDetail(id, month, year) {

    var u_id = id == undefined ? userId : id;
    //var u_month = month

    var d = calendar.fullCalendar('getDate');
    //var view_id = calendar.find('.fc-button.fc-state-active').find('span > span').text(); //<-- use this if calendar doesn't use theme ui
    var view_id = $("#calendar").find('.fc-button.ui-state-active').find('span > span').text(); //<-- use this if calendar uses theme ui

    var new_obj = {
        userId: u_id, //userId
        month: month,
        year: year,
        viewtype: view_id,
        day: view_id == "Daily" ? d.getDate() : 1
    }

    /**
    $.ajax({
    dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
    type: "POST", url: baseUrl + "Calendar/GetEventByDate",
    success: function (res) {
    if (res.data.info.length != undefined) {

    $(res.data.info).each(function (index, item) {


    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
    //                    
    var completeDate = new Date(completeDate1);
    var calendar = $('#calendar');

    var colorholder = null;
    switch (item.ObjectiveCode.toUpperCase()) {
    case "C":
    colorholder = "Red";
    break;
    case "S":
    colorholder = "Green";
    break;
    case "CS":
    colorholder = "Yellow";
    break;
    case "M":
    colorholder = "Blue";
    break;

    default:
    colorholder = "None";
    break;


    }


    calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: colorholder }, true);

    }); **/


    //new code implementation Coverage
    DisplayPreloader();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetCoverageByDate",
        success: function (res) {

            getSOAccount();

            if (res.data.info.length != undefined && res.data.info.length != null) {

                calendar.fullCalendar('addEventSource', res.data.info);

                $('.month').removeClass('selected');  // remove selected from any other item first
                $('.month').addClass('ui-state-default');
                $("#" + month).removeClass('ui-state-default');
                $("#" + month).addClass('selected');
                $("#year").text(year);
                //////                $(res.data.info).each(function (index, item) {


                //////                    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
                //////                    //                    
                //////                    var completeDate = new Date(completeDate1);
                //////                    var calendar = $('#calendar');

                //////                    //var colorholder = '99FF99';




                //////                    //revisions of legend as to pearl request
                //////                    /**
                //////                    var Bluecolor = '#5C9DDE';
                //////                    var Redcolor = '#FF5F5F';
                //////                    var Yellowcolor = '#E8E819';
                //////                    var GreenColor = '#59DEBC';
                //////                    var Graycolor = '#B1CACA';

                //////                    //Regular in Coverage
                //////                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Bluecolor }, true); }

                //////                    //DELETED ACCOUNT
                //////                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }
                //////                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "T")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }

                //////                    // EDITED EITHER APPROVED, FOR ASM APPROVAL,RETURNED
                //////                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus != "3")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Yellowcolor }, true); }

                //////                    //UNPLANNED
                //////                    if (item.isPlanned == "F" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: GreenColor }, true); }

                //////                    //DISAPPROVED
                //////                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus == "3")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Graycolor }, true); }

                //////                    **/


                //////                    //calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: colorholder }, true);
                //////                    //alert(item.AccountCode);
                //////                   //new color holder
                //////                    var plan_color = '#92d050';
                //////                    var deleted_color = '#f8f8f8';
                //////                    var visited_color = '#ffc000'
                //////                    var edited_color = '#ccffcc';
                //////                    var unplanned = '#00BFFF';

                //////                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.hasCallreport == "F")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: plan_color }, true); }
                //////                    else if (item.IsDeleted == "T")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: deleted_color }, true); }
                //////                    else if (item.hasCallreport == "T" && item.IsDeleted == "F" && item.isPlanned == "T")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: visited_color }, true); }
                //////                    else if (item.IsAnEdit == "T" && item.hasCallreport == "F" && item.AcctStatus != "7")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: edited_color }, true); }
                //////                    else if (item.isPlanned == "F")
                //////                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: unplanned }, true); }
                //////                    else if (item.IsAnEdit == "T" && item.hasCallreport == "F" && item.AcctStatus == "7")
                //////                    { calendar.fullCalendar('renderEvent', { id: "disapproved", title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: edited_color }, true); }



                //////                });


                // calendar.fullCalendar('removeEvents', "disapproved");
                //calendar.fullCalendar
                $("#doc_stat_msg").html(res.data.docstatus);
                $("#sp_true").html(res.data.ntrue);
                $("#sp_false").html(res.data.allplanned);


                var xtrue = res.data.ntrue;
                var yfalse = res.data.allplanned;
                var avg = 0;
                if (xtrue != 0 && xtrue != null && xtrue != undefined && yfalse != 0 && yfalse != null && yfalse != undefined) {
                    avg = Math.floor(parseInt(xtrue) / parseInt(yfalse) * 100);
                }

                $("#sp_avg").html(avg + '%');

                $('#txt_EventId').attr("value", res.data.EventId);

                if (res.data.docstatus.toUpperCase() == "DRAFT" || res.data.docstatus.toUpperCase() == "FOR ASM APPROVAL" || res.data.docstatus.toUpperCase() == "FOR CHANNEL MANAGER APPROVAL") {

                    $(".hiddenTR").hide();

                }
                else {
                    $(".hiddenTR").show();
                }

                if (res.data.docstatus.toUpperCase() == "DRAFT" || res.data.docstatus.toUpperCase() == "RETURNED BY ASM" || res.data.docstatus.toUpperCase() == "RETURNED BY CHANNEL MANAGER" || res.data.docstatus.toUpperCase() == "RETURNED BY RSM" || res.data.docstatus.toUpperCase() == "RETURNED BY VP-SALES") {


                    btn_SaveAndSend.show();
                }
                else {

                    // if (res.data.docstatus.toUpperCase() == "" || res.data.docstatus.toUpperCase() == "APPROVED" || res.data.docstatus.toUpperCase() == "DISAPPROVED") {

                    btn_SaveAndSend.hide();
                }
                //}

                

                /* inserted by billy jay delima */
                $("#lnk_upload_excel").unbind();
                $("#lnk_upload_excel").uploadlink2(
                    //getDialog(
                    baseUrl + "Calendar/UploadExcelDataCoveragePlan_Monthly?EventMonth=" + vw_monthholder + "&empIdNo=" + userId + "&EventYear=" + vw_yearholder,
                   //linkurl(),
                    "txt_FileAttachment",
                    "TESTING",
                   
                    function (counterid) {
                        // window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
                        // ShowFramePreviewUpload();
                        displayUploaded(counterid);

                        counter_id_upload = counterid;
                    }
                );
                /* end */


                HidePreloader();
            } else {

            }
            is_processing = false;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
            is_processing = false;
        }
    });
}

function getSOAccount() {
    $.ajax({
        type: "POST",
        url: baseUrl + "Calendar/GetSoList",
        //data: { soid: soid },
        success: function (res) {
            $("#soList").html(res);
            $("#soList").chosen();
        }
    });
}

        
        
/*
*
Work around functions to get the values needed from calendar.
Data may also obtain from functions under the plug in functions
but here I made my own functions to caputure data that i need.
*
*/

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


function UpdatePlanEvents(act_type) {
    var params =
     {
         EventID: txt_EventId.val(),
         action_type: act_type,
         EmpIdNo: curr_id,
         Month: "",
         Year: "",
         ApprvrRrmks: $("#txt_apprvrRemarks").attr("value")
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
    calendar.fullCalendar('removeEvents');
    GetEventDetail(userId, vw_monthholder, vw_yearholder);
}



function get_calendar_height() {
    return $(window).height() - 30;
}


function CountInventorySchedule(month, year, soId) {

    var new_obj = {

        month: month,
        year: year,
        soId: soId

    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        //type: "POST", url: baseUrl + "Calendar/SavePlanEvents",
        type: "POST", url: baseUrl + "Calendar/GetInventoryperSo",
        success: function (res) {

            if (res.length != null && res.length != 0 && res.length != undefined) {
                var ok = confirm("You have scheduled inventory count under this month. Do you want to load it in your coverage plan?");

                if (ok) {

                    alert("reloading");
                }



            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}



function NxtInventorySked(month, year, soId) {

    var new_obj = {

        month: month,
        year: year,
        soId: soId

    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        //type: "POST", url: baseUrl + "Calendar/SavePlanEvents",
        type: "POST", url: baseUrl + "Calendar/GetNextInventoryCountGetInventoryperSo",
        success: function (res) {
            if (res.data.InvCoverage.length != undefined) {
                $(res.data.InvCoverage).each(function (index, itm) {

                    var SkeduledDate = new Date(itm.startCountDate);
                    var calendar = $('#calendar');
                    var pinkcolor = 'Pink';

                    calendar.fullCalendar('renderEvent', { title: itm.acctCode, start: SkeduledDate, allDay: true, backgroundColor: pinkcolor }, true);


                });
            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}

function SaveNextinventoryCount(month, year, soId) {
    var params =
     {

         soId: soId,
         month: month,
         year: year
     }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(params),
        //type: "POST", url: baseUrl + "Calendar/SavePlanEvents",
        type: "POST", url: baseUrl + "Calendar/SaveScheduledinventory",
        success: function (res) {
            //  alert("Success!");
            //  location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}

/* inserted by billy jay delima */
function eventDrag(node, ev, ui, evr) {
    var oldTD = dragTD;
    dragTD = dayTD(ev.pageX, ev.pageY);
    if (!dragStartTD) dragStartTD = dragTD;
    if (dragTD != oldTD) {
        if (evr != true && dragTD) {
            $(node).draggable('option', 'revert', dragTD == dragStartTD);
            dayOverlay.css({
                top: currTDY,
                left: currTDX,
                width: currTDW,
                height: currTDH,
                display: 'block'
            });
        } else {
            $(node).draggable('option', 'revert', true);
            dayOverlay.hide();
        }
    }
}

function displayUploaded(counterid) {

    var calendar_date1 = $("#calendar").fullCalendar('getDate');

    var new_obj = { counterid: counterid, eventMonth: calendar_date1.getMonth(), eventYear: calendar_date1.getFullYear() };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + 'Calendar/getTemporaryUploadData_Monthly',
        success: function (res) {
            uploaded_data = res.data.list;

            btn_SaveAndSend.hide();

            $("#doc_stat_msg").html('Upload Coverage Preview MODE');
            calendar.fullCalendar('destroy');
            calendar.fullCalendar({
                header: {
                    left: 'title',
                    right: 'month'
                },
                year: calendar_date1.getFullYear(),
                month: calendar_date1.getMonth(),
                //  height: 820,
                events: res.data.list,
                disableDragging: true,
                eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {

                    alert(revertFunc);

                    var day_all_events = null;
                    var revert = false;
                    var data = new Array();

                    day_all_events = calendar.fullCalendar('clientEvents');
                    $(day_all_events).each(function (i, e) {
                        if (e.start.toString() == event.start.toString() && e.title.toString() == event.title.toString())
                            data.push(e);
                    });
                    if (data.length > 1) {
                        revert = true;
                    }
                    if (revert == true) {
                        alert("Account already exist!..");
                        revertFunc();
                    }
                    else if (!confirm(event.title + " was moved " + dayDelta + " days. Are you sure about this change?")) {
                        revertFunc();
                    }
                },
                eventClick: function (calEvent, jsEvent, view) {
                    $('input.auto').autoNumeric();
                    var contactnum = calEvent.contact_person_no.split(" ");
                    var acctcode = calEvent.account_code.split(" ");
                    var acctname = calEvent.title.split(" ");


                    if (calEvent.contact_person == "(REQUIRED FIELD)") {
                        $("#txt_cntct_person").css("background-color", "orange");
                    }
                    else {
                        $("#txt_cntct_person").css("background-color", "#ededed");
                    }

                    if (calEvent.contact_person_no == "(REQUIRED FIELD)" || contactnum[0] == "invalid") {
                        $("#txt_cntct_person_no").css("background-color", "orange");
                    }
                    else {
                        $("#txt_cntct_person_no").css("background-color", "#ededed");
                    }


                    if (acctcode[0] == "invalid") {
                        $("#txt_acct_code").css("background-color", "orange");
                    }
                    else {
                        $("#txt_acct_code").css("background-color", "#ededed");
                    }

                    if (acctname[0] == "invalid") {
                        $("#txt_acct_name").css("background-color", "orange");
                    }
                    else {
                        $("#txt_acct_name").css("background-color", "#ededed");
                    }

                    $("#txt_date").text(calEvent.start);
                    $("#txt_acct_code").attr("value", calEvent.account_code);
                    $("#txt_acct_name").attr("value", calEvent.title);
                    $("#txt_cntct_person").attr("value", calEvent.contact_person);
                    $("#txt_cntct_person_no").attr("value", calEvent.contact_person_no);
                    $("#txt_hotel_name").attr("value", calEvent.hotel_name);
                    $("#txt_hotel_num").attr("value", calEvent.hotel_num);
                    $("#txt_storechecking").attr("value", calEvent.store_checking);
                    $("#txt_issue_concern").attr("value", calEvent.issues_and_concerns);

                    var obj_code_holder = "";
                    $("#tbl_objectives tr[clone=\"true\"]").remove();

                    $("#collection").removeClass("tabcolor");
                    $("#sales").removeClass("tabcolor");
                    $("#merchandise").removeClass("tabcolor");
                    $("#customerservice").removeClass("tabcolor");

                    $("#tbl_collection_dtls tr[class=\"addedRow\"]").remove();
                    $("#tbl_sales_dtls tr[class=\"addedRow\"]").remove();
                    $("#tbl_mse_details tr[class=\"addedRow\"]").remove();
                    $("#customerservice tr[class=\"addedRow\"]").remove();

                    if (calEvent.store_checking != "" && calEvent.store_checking != null)
                        $("#merchandise").addClass("tabcolor");
                    if (calEvent.issues_and_concerns != "" && calEvent.issues_and_concerns != null)
                        $("#customerservice").addClass("tabcolor");

                    $(calEvent.list_objectives).each(function (i, e) {
                        var brandsplit = e.brand.split(" ");
                        var amountsplit = e.planned_amount.split(" ");
                        var details = e.details_remark.split(" ");

                        //                        var brandbgcolorerror = null;
                        //                        var amountbgcolorerror = null;
                        //                        var detailsbccolorerror = null;

                        //                        if (brandsplit[0] == "invalid") {
                        //                            brandbgcolorerror = "orange";
                        //                        }
                        //                      
                        //                        else if (amountsplit[0] == "invalid") {
                        //                            amountbgcolorerror == "orange";
                        //                        }
                        //                       

                        //                        else if (details[0] == "invalid") {
                        //                            detailsbccolorerror == "orange";
                        //                        }
                        //                        else {
                        //                            detailsbccolorerror == "#ededed";   
                        //                        }

                        //alert(brandbgcolorerror + amountbgcolorerror + detailsbccolorerror);
                        //alert(brandsplit[0] + amountsplit[0] + details[0]);

                        if (e.objective_code == "C") {
                            $("#collection").addClass("tabcolor");
                            if (brandsplit[0] == "invalid") {
                                $("#tbl_collection_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                  '<td><input type="text"  value="' + e.planned_amount + '"/></td>' +
                                                  '</tr>');
                            }
                            else {
                                $("#tbl_collection_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                  '<td><input type="text"  value="' + e.planned_amount + '"/></td>' +
                                                  '</tr>');
                            }

                            $("#tbl_collection_dtls").attr("objectivecode", e.objective_code);
                        }
                        else if (e.objective_code == "S") {
                            $("#sales").addClass("tabcolor");
                            if (brandsplit[0] == "invalid") {
                                $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                                              '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.details_remark + '"/></td>' +
                                                                              '</tr>');
                            }
                            else if (amountsplit[0] == "invalid") {
                                $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                                              '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.details_remark + '"/></td>' +
                                                                              '</tr>');
                            }
                            else if (amountsplit[0] == "invalid" && brandsplit[0] == "invalid") {
                                $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                                              '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                                              '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.details_remark + '"/></td>' +
                                                                              '</tr>');
                            }
                            else if (details[0] == "invalid") {
                                $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                                              '<td><input type="text" style="background-color:orange;" readonly="readonly" value="' + e.details_remark + '"/></td>' +
                                                                              '</tr>');
                            }
                            else {
                                $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                                              '<td><input type="text" readonly="readonly" value="' + e.details_remark + '"/></td>' +
                                                                              '</tr>');
                            }

                        }
                        else if (e.objective_code == "M") {
                            $("#merchandise").addClass("tabcolor");
                            $("#tbl_mse_details .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.product_presented + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.counter_clerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.counter_clerk_no + '"/></td>' +
                                                  '</tr>');
                        }
                        else if (e.objective_code == "CS") {
                            $("#customerservice").addClass("tabcolor");
                        }

                        obj_code_holder = e.objective_code;
                    });

                    lookUp_function();
                    addButton_function();

                    $(".hiddenTd").hide();
                    display_dialogbox(calEvent);
                }
            });
            $("#tbl_footer").hide();
            $("#grp_upload_buttons").show();
            $(".fc-header-center").html("<h2>[UPLOAD PREVIEW MODE]</h2>").css("color", "red");

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function lookUp_function() {
    $("#txt_collectBrand").unbind();
    $("#txt_collectBrand").lookdown(
                        { "url": baseUrl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
                        { "": "M" },
                        function (res) { return res; },
                        function (res, all) {
                            var _brand = all[1] == "null" ? "" : all[1];
                            $("#txt_collectBrand").attr("value", _brand);
                        });

    $("#txt_salesBrand").unbind();
    $("#txt_salesBrand").lookdown(
                        { "url": baseUrl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
                        { "": "M" },
                        function (res) { return res; },
                        function (res, all) {
                            var _brand = all[1] == "null" ? "" : all[1];
                            $("#txt_salesBrand").attr("value", _brand);
                        });
}

function addButton_function() {
    $("#tbl_collection_dtls .btn_add").click(function () {
        if ($("#txt_collectBrand").val() == "" || $("#txt_collectAmount").val() == "") {
            alert('Fill up empty fields.');
        }
        else {
            $("#tbl_collection_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd" style=\"display:none;\"><input type="text" readonly="readonly" value="C"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_collectBrand").val() + '"/></td>' +
                                                  '<td><input type="text"  value="' + $("#txt_collectAmount").val() + '"/></td>' +
                                                  '</tr>');
            $("#txt_collectBrand").removeAttr("value");
            $("#txt_collectAmount").removeAttr("value");
        }
    });
    $("#tbl_sales_dtls .btn_add").click(function () {
        if ($("#txt_salesBrand").val() == "" || $("#txt_salesAmount").val() == "") {
            alert('Fill up empty fields.');
        } else {
            $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd" style=\"display:none;\"><input type="text" readonly="readonly" value="S"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_salesBrand").val() + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_salesAmount").val() + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_remarks").val() + '"/></td>' +
                                                  '</tr>');
            $("#txt_salesBrand").removeAttr("value");
            $("#txt_salesAmount").removeAttr("value");
            $("#txt_remarks").removeAttr("value");

        }
    });
    $("#tbl_mse_details .btn_add").click(function () {
        if ($("#txt_mse_productpresented").val() == "" || $("#txt_mse_counterclerk").val() == "" || $("#txt_mse_mobileno").val() == "") {
            alert('Fill up empty fields.');
        } else {
            $("#tbl_mse_details .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd" style=\"display:none;\"><input type="text" readonly="readonly" value="M"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_mse_productpresented").val() + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_mse_counterclerk").val() + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + $("#txt_mse_mobileno").val() + '"/></td>' +
                                                  '</tr>');

            $("#txt_mse_productpresented").removeAttr("value");
            $("#txt_mse_counterclerk").removeAttr("value");
            $("#txt_mse_mobileno").removeAttr("value");

        }
    });
}

function display_dialogbox(calEvent) {
    var originalContent;
    $("#dialog_box").dialog({
        title: 'Coverage Details', width: 720, height: 463, resizable: false, modal: true,
        buttons: {
            Delete: function () {
                if (confirm("Are you sure you want to delete this event?")) {
                    calendar.fullCalendar("removeEvents", calEvent.id);
                    calendar.fullCalendar("rerenderEvents");
                    $(this).dialog("close");
                }
            },
            Save: function () {
                calEvent.contact_person = $("#txt_cntct_person").attr("value");
                calEvent.contact_person_no = $("#txt_cntct_person_no").attr("value");
                calEvent.hotel_name = $("#txt_hotel_name").attr("value");
                calEvent.hotel_num = $("#txt_hotel_num").attr("value");
                calEvent.store_checking = $("#txt_storechecking").attr("value");
                calEvent.issues_and_concerns = $("#txt_issue_concern").attr("value");

                var data = new Array();
                $("#tbl_collection_dtls tr[clone=\"true\"]").each(function (i, elem) {
                    var curr_row = $(elem);
                    data.push({
                        account_code: null,
                        brand: curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"),
                        counter_clerk: null,
                        counter_clerk_no: null,
                        objective_code: curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value"),
                        planned_amount: curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value"),
                        product_presented: null

                    });
                });

                $('#tbl_sales_dtls tr[clone="true"]').each(function (i, elem) {
                    var curr_row = $(elem);
                    data.push({
                        account_code: null,
                        brand: curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"),
                        counter_clerk: null,
                        counter_clerk_no: null,
                        objective_code: curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value"),
                        planned_amount: curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value"),
                        details_remark: curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value"),
                        product_presented: null

                    });
                });

                $("#tbl_mse_details tr[clone=\"true\"]").each(function (i, elem) {
                    var curr_row = $(elem);
                    data.push({
                        account_code: null,
                        brand: null,
                        counter_clerk: curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value"),
                        counter_clerk_no: curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value"),
                        objective_code: curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value"),
                        planned_amount: null,
                        product_presented: curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value")
                    });
                });

                calEvent.list_objectives = data;
                calendar.fullCalendar('updateEvent', calEvent);
                clearFields();
                $(this).dialog("close");
            },
            Cancel: function () {
                clearFields();
                $(this).dialog("close");
            }
        },
        open: function (event, ui) {
            originalContent = $("#dialog_box").html();
        },
        close: function (event, ui) {
            $("#dialog_box").html(originalContent);
        }
    });

    $('.ui-button-text').each(function (i) {
        $(this).html($(this).parent().attr('text'))
    });

    $("#tab_main").tabs();
}

function clearFields() {
    $("#txt_date").text();
    $("#txt_acct_code").removeAttr("value");
    $("#txt_acct_name").removeAttr("value");
    $("#txt_cntct_person").removeAttr("value");
    $("#txt_cntct_person_no").removeAttr("value");
    $("#txt_hotel_name").removeAttr("value");
    $("#txt_hotel_num").removeAttr("value");
    $("#txt_storechecking").removeAttr("value");
    $("#txt_issue_concern").removeAttr("value");
}

function save_uploaded_files() {
    var soid = $("#soList option:selected").val();
    var data_header = new Array();
    var allEvents = $("#calendar").fullCalendar('clientEvents');
    var getDate = $("#calendar").fullCalendar('getDate');
    
    $(allEvents).each(function (i, e) {
        data_header.push({
            title: e.title,
            start: e.start,
            backgroundColor: e.backgroundColor,
            editable: e.editable,
            allday: e.allday,
            account_code: e.account_code,
            contact_person: e.contact_person,
            contact_person_no: e.contact_person_no,
            store_checking: e.storechecking,
            issues_and_concerns: e.issues_and_concerns,
            hotel_name: e.hotel_name,
            hotel_num: e.hotel_num,
            list_objectives: e.list_objectives
        });
    });

    var new_obj = {
        counter_id: counter_id_upload,
        EmpIdNo: soid,
        Year: getDate.getFullYear(),
        Month: getDate.getMonth(),
        list: data_header
    };


    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + 'Calendar/SaveUploadedMonthlyCoveragePlan',
        success: function (res) {
            if (!res.iserror) {
                alert("Successfully Uploaded.");
                location.reload();
            }
            else
                alert(res.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function delete_uploaded_files() {
    var new_obj = { counter_id: counter_id_upload }
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "Calendar/DeleteUploadedMonthlyCoveragePlan",
        success: function (res) {
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


function appendLegend() {
    var legend = '<table style="text-align:left;">' +
                   '<tr>' +
                       '<td><span style="font-weight:bold">Legend:</span></td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/darkgreenlegend.png" /> Planned</td> ' +
                       '<td><img alt="" src="' + baseUrl + 'Images/graylegend.png" /> Deleted</td> ' +
                       '<td><img alt="" src="' + baseUrl + 'Images/yellolegend.png" /> Visited</td>' +
                    '</tr>' +
                    '<tr>' +
                       '<td></td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/greenlegend.png" /> Edited</td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/bluelegend.png" /> Unplanned</td>' +
                       '<td><img alt="" src="' + baseUrl + 'Images/pinklegend.png" /> For Invty Count</td>' +
                    '</tr>' +
                '</table>';

    $('.fc-header-left').append(legend);
}

function appendDatePicker() {
    var custom_buttons = '<span style="vertical-align:center">' +
                            'Go to: <input type="text" id="gotodate" style="width:100px; vertical-align:center;"> &nbsp;' +
                        '</span>';
    $('.fc-header-right').find("span:eq(0)").before(custom_buttons);

    $('#gotodate').datepicker({
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

        calendar.fullCalendar('changeView', 'basicDay');
        calendar.fullCalendar('option', 'height', 350);

        calendar.fullCalendar('gotoDate', year1, month1 - 1, day1);

        is_processing = true;
        var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");

        calendar.fullCalendar('removeEvents');

        NxtInventorySked(month1, year1, userId);
        getalldayevents(id, month1, year1, day1);
    }
}

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
            if (!res.iserror) {
                calendar.fullCalendar('addEventSource', res.data.AllDayEvents);
                is_processing = false;
            }
            else {
                alert(res.message);
                location.reload();
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            is_processing = false;
            HidePreloader();
        }
    });
}
/* end */


function gotodate(month, elem) {
    is_processing = true;
    var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");

    var dateHolder = vw_monthholder;
    calendar.fullCalendar('removeEvents');

    vw_monthholder = parseInt(month) + 1;
    vw_yearholder = $("#year").text();



    //modified by francis 1/26/2017 ======== for getting the so id selected in dropdown list
    $("#soList").change(function () {
        is_processing = true;
        calendar.fullCalendar('removeEvents');
        /* inserted by billy jay delima */
        $("#lnk_upload_excel").unbind();
        $("#lnk_upload_excel").uploadlink2(
        //getDialog(
        baseUrl + "Calendar/UploadExcelDataCoveragePlan_Monthly?EventMonth=" + vw_monthholder + "&empIdNo=" + $(this).val() + "&EventYear=" + vw_yearholder,
                    "txt_FileAttachment",
                    "TESTING",
                                        function (counterid) {
                                            // window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
                                            // ShowFramePreviewUpload();
                                            displayUploaded(counterid);

                                            counter_id_upload = counterid;
                                        }
                );
        /* end */
    });

    calendar.fullCalendar('gotoDate', $("#year").text(), month);
    NxtInventorySked(vw_monthholder, vw_yearholder, userId);
    GetEventDetail($(this).val(), vw_monthholder, vw_yearholder);

    //end
}

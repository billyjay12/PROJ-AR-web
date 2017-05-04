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
        calendar.fullCalendar('option', 'height', get_calendar_height());
    });

    calendar.CreateCalendar();



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


    });

    //variables declaration after calendar is instantiated
    btn_prev = calendar.fullCalendar('prev');
    btn_next = calendar.fullCalendar('next');

    //actions when previous button in calendar is click
    btn_prev.click(function () {


        var id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");
        var month = $("#calendar").find(".fc-header").find(".fc-header-left").next(".fc-header-center").find(".fc-header-title").text();
        getCurrMonthVW(month);

        //calendar.fullCalendar('destroy');
        var dateHolder = vw_monthholder;
        //calendar.CreateCalendar(parseInt(dateHolder) - 1, vw_yearholder);
        calendar.fullCalendar('removeEvents');

        //Load if there are pre set inventory count schedule
        //CountInventorySchedule(vw_monthholder, vw_yearholder, userId);

        GetEventDetail(id, vw_monthholder, vw_yearholder);
        NxtInventorySked(vw_monthholder, vw_yearholder, userId);

    });








    //button Approved

    /** sub_menu_link_2.click(function () {

    UpdatePlanEvents("APPROVE");
    }); **/

    btn_SaveAndSend.click(function () {

        SaveNextinventoryCount(vw_monthholder, vw_yearholder, userId);
        UpdatePlanEvents("APPROVE");

    });



    sub_menu_link_3.click(function () {
        UpdatePlanEvents("RETURN_TO_REQUESTOR");

    });




});


/** This generate a calendar with event triggered **/

$.fn.CreateCalendar = function (u_month, u_year) {


    $(this).fullCalendar({
        month: u_month,
        year: u_year,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay'
        },
        //optional 
        height: 820,

        dayClick: function (date, allDay, jsEvent, view) {

            curr_id = txt_hidden_id.attr("value") == "" ? userId : txt_hidden_id.attr("value");

            var curr_date = date.getDate();
            var temp_month = date.getMonth();
            var curr_month = temp_month + 1;
            var curr_year = date.getFullYear();
            // ShowRemarkTextbox(date);

            $.fancybox({
                openEffect: 'elastic',
                closeEffect: 'elastic',
                type: 'iframe',
                href: baseUrl + "Calendar/Memo?date=" + date + "&day=" + curr_date + "&month=" + curr_month + "&year=" + curr_year + "&soId=" + curr_id,
                'overlayShow': true,
                'showCloseButton': true
            });

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


function GetEventDetail(id,month,year) 
{

    var u_id = id == undefined ? userId : id;
   //var u_month = month

    var new_obj = {
        userId: u_id, //userId
        month: month,
        year: year
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

            if (res.data.info.length != undefined) {

                $(res.data.info).each(function (index, item) {


                    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
                    //                    
                    var completeDate = new Date(completeDate1);
                    var calendar = $('#calendar');

                    //var colorholder = '99FF99';

                    var Bluecolor = '#5C9DDE';
                    var Redcolor = '#FF5F5F';
                    var Yellowcolor = '#E8E819';
                    var GreenColor = '#59DEBC';
                    var Graycolor = '#B1CACA';

                    //Regular in Coverage
                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Bluecolor }, true); }

                    //DELETED ACCOUNT
                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }
                    if (item.isPlanned == "T" && item.IsDeleted == "T" && item.IsAnEdit == "T")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Redcolor }, true); }

                    // EDITED EITHER APPROVED, FOR ASM APPROVAL,RETURNED
                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus != "3")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Yellowcolor }, true); }

                    //UNPLANNED
                    if (item.isPlanned == "F" && item.IsDeleted == "F" && item.IsAnEdit == "F" && item.AcctStatus != "3")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: GreenColor }, true); }

                    //DISAPPROVED
                    if (item.isPlanned == "T" && item.IsDeleted == "F" && item.IsAnEdit == "T" && item.AcctStatus == "3")
                    { calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: Graycolor }, true); }


                    //calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true, backgroundColor: colorholder }, true);

                });




                $("#doc_stat_msg").html(res.data.docstatus);

                $('#txt_EventId').attr("value", res.data.EventId);

                if (res.data.docstatus.toUpperCase() == "DRAFT" || res.data.docstatus.toUpperCase() == "RETURN BY ASM") {


                    btn_SaveAndSend.show();
                }
                else {

                    // if (res.data.docstatus.toUpperCase() == "" || res.data.docstatus.toUpperCase() == "APPROVED" || res.data.docstatus.toUpperCase() == "DISAPPROVED") {

                    btn_SaveAndSend.hide();
                }
                //}





                HidePreloader();
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
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


 function UpdatePlanEvents(act_type) 
 {
     var params =
     {
         EventID: txt_EventId.val(),
         action_type: act_type,
         EmpIdNo: curr_id,
         Month:"",
         Year:""
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
    return $(window).height()-30;
}


function CountInventorySchedule(month, year, soId) {

    var new_obj = {
    
    month:month,
    year:year,
    soId:soId

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

function SaveNextinventoryCount(month,year,soId) {
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
            alert("Success!");
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}












  

      

      
       
         
        







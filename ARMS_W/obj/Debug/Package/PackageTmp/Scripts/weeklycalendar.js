var date = new Date();
var d = date.getDate();
var m = date.getMonth();
var y = date.getFullYear();
var data1 = new Array();
var x;
var y;
var ns4;

$(document).ready(function () {
//    $('#calendar').fullCalendar({
//        header: {
//            left: 'prev,next today',
//            center: 'title'//,
//            //right: 'agendaWeek'
//        },
//        defaultView: 'agendaWeek',
//        month: 6,
//        editable: false, // set this false
//        events: [
//                    {
//                        title: 'This must be editable',
//                        start: date,
//                        editable: true,
//                        id: 'AFFs0011'
//                    },
//                    {
//                        title: 'This is non editable',
//                        start: date,
//                        end: date
//                    }
//                    ],
//        eventClick: function (calEvent, jsEvent, view) {
//            if (calEvent.editable == true) {
//                var title = prompt('Event Title:', calEvent.title, { buttons: { Ok: true, Cancel: false} });


//                if (title) {
//                    calEvent.title = title;
//                    calEvent.title = calEvent.id;
//                    $('#calendar').fullCalendar('updateEvent', { title: calEvent.id, start: calEvent.start, editable: false, allDay: true }, true);
//                }
//            }
//        }
//    });
});


$(function () {
    GetEventDetail1();
});

//function GetEventDetail() {


//    var new_obj = {
//        userId: userId
//    }

//    $.ajax({
//        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
//        type: "POST", url: baseUrl + "Calendar/GetEventByDate",
//        success: function (res) {
//            if (res.length != undefined) {

//                $(res).each(function (index, item) {

//                    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
//                    //                    
//                    var completeDate = new Date(completeDate1);
//                    var calendar = $('#calendar');

//                    calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true }, true);
//                    //$('#txt_EventId').attr("value", item.EventID);
//                });




//            } else {

//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status); alert(thrownError);
//        }
//    });


//}


function GetEventDetail1() {


    var new_obj = {
        userId: userId
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetEventByDate_Weekly",
        success: function (res) {

            if (res.data.fullcalendarEvents.length != undefined) {

                $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title'//,
                        //right: 'agendaWeek'
                    },
                    defaultView: 'agendaWeek',
                   // month: 4,
                    editable: false, // set this false
                    events: res.data.fullcalendarEvents,
                    disableDragging: true,
                    //                    eventSources: 
                    //                        // your event source
                    //                        {
                    //                           events: res.data.fullcalendarEvents,
                    //                            color: 'black',     // an option!
                    //                            textColor: 'yellow' // an option!
                    //                        },
                    eventClick: function (calEvent, jsEvent, view) {
                        if (calEvent.editable == true) {
                            var title = prompt('Event Title:', calEvent.title, { buttons: { Ok: true, Cancel: false} });


                            if (title) {
                                calEvent.title = title;
                                calEvent.title = calEvent.id;
                                $('#calendar').fullCalendar('updateEvent', { title: calEvent.id, start: calEvent.start, editable: false, allDay: true }, true);
                            }
                        }
                    }
                });

                //                $(res).each(function (index, item) {

                //                    var completeDate1 = item.Month + "/" + item.Day + "/" + item.Year;
                //                    //                    
                //                    var completeDate = new Date(completeDate1);
                //                    var calendar = $('#calendar');

                //                    calendar.fullCalendar('renderEvent', { title: item.AccountCode, start: completeDate, allDay: true }, true);
                //                    //$('#txt_EventId').attr("value", item.EventID);
                //                });


                //  $(".fc-event-inner .fc-event-skin").find("span").html();
                $(".fc-event-title").css("color", "red");
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });


}




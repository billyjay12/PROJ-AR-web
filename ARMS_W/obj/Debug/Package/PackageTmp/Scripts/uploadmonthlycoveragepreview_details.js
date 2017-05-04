$(function () {
    var ob = { aawts: "awts", wew: "wew" };
    var data1 = new Array();
    data1.push(ob);
    var new_obj = { a: "1", b: "2", aaa: data1 };

    //$(window).trigger("resize"); 
    $('#calendar').fullCalendar({
        header: {
            left: 'title',
            center: ' today prev,next',

            // right: 'month,agendaWeek,agendaDay'
            right: 'month,basicWeek,basicDay'
        },
        month: 6,
        editable: false, // set this false
        events: [
                            {
                                title: 'This must be editable',
                                start: '08/28/2013',
                                editable: true,
                                id: 'AFFs0011',
                                backgroundColor: 'green'//,
                                // awts:new_obj,
                            },
                            {
                                title: 'This is non editable',
                                start: '08/28/2013',
                                backgroundColor: 'red'
                            }
                            ],
        eventColor: '#ededed',
        eventRender: function (event, element, calEvent) {
            element.find(".fc-event-title").before($("<span class=\"fc-event-icons\"></span>").html("<img src=\"/images/bg.gif\" />"));
        },
        //        	select: function(start, end, allDay) {
        //                var title = prompt('Event Title:');
        //                if (title) {
        //                    calendar.fullCalendar('renderEvent',
        //                {
        //                title: title,
        //                start: start,
        //                end: end,
        //                allDay: allDay
        //                },
        //                true // make the event "stick"
        //                );
        //                }
        //                calendar.fullCalendar('unselect');
        //            },
        eventClick: function (calEvent, jsEvent, view) {
            if (calEvent.editable == true) {

                //                $.fancybox ({
                //                   // openEffect: 'elastic',
                //                   // closeEffect: 'elastic',
                //                  //  'showCloseButton': true,
                //                    type: 'iframe',
                //                    href: baseUrl + "Module1Page/ViewPage1",
                //                  //  'overlayShow': true
                //                });
                var title = prompt('Event Title:', calEvent.title, { buttons: { Ok: true, Cancel: false} });


                if (title) {
                    calEvent.title = title;
                    calEvent.title = calEvent.id;
                    $('#calendar').fullCalendar('updateEvent', { title: calEvent.id, start: calEvent.start, editable: false, allDay: true }, true);
                }
            }

        },
        dayClick: function (date, allDay, jsEvent, view) {
            alert("awts");
        }
    });
});
     
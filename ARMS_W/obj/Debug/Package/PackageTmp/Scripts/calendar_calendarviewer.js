var calendar = null;

var txt_year = null;

var lbl_calendarno = null;
var lbl_status = null;

var lbl_employeeid = null;
var lbl_employeename = null;
var lbl_NoOfVisit = null;
var lbl_noOfEvents = null;
var lbl_noOfPlannedEvents = null;
var lbl_noOfUnplannedEvents = null;
var lbl_editedevents = null;
var lbl_totalCallEffective = null;
var lbl_totalEvents = null;
var lbl_averageCallEffective = null;

var lbldateasof = null;
var lblGrossSales = null;
var lblCM = null;
var lblUnpostedSales = null;
var lblPostedSales = null;
var lblPending = null;
var lblBalanceOrder = null;
var lblNetPostedSales = null;
var lblNoTransactingAccts = null;

var btn_search_empId = null;
var is_processing = false;

$(function () {

    calendar = $("#calendar");
    btn_search_empId = $("#btn_search_empId");

    txt_year = $("#txt_year");

    lbl_calendarno = $("#lbl_calendarno");
    lbl_status = $("#lbl_status");
    lbl_employeeid = $("#lbl_employeeid");
    lbl_employeename = $("#lbl_employeename");
    lbl_NoOfVisit = $("#lbl_NoOfVisit");
    lbl_noOfEvents = $("#lbl_noOfEvents");
    lbl_noOfPlannedEvents = $("#lbl_noOfPlannedEvents");
    lbl_noOfUnplannedEvents = $("#lbl_noOfUnplannedEvents");
    lbl_editedevents = $("#lbl_editedevents");
    lbl_totalCallEffective = $("#lbl_totalCallEffective");
    lbl_totalEvents = $("#lbl_totalEvents");
    lbl_averageCallEffective = $("#lbl_averageCallEffective");

    lbldateasof = $("#lbldateasof");
    lblGrossSales = $("#lblGrossSales");
    lblCM = $("#lblCM");
    lblUnpostedSales = $("#lblUnpostedSales");
    lblPostedSales = $("#lblPostedSales");
    lblPending = $("#lblPending");
    lblBalanceOrder = $("#lblBalanceOrder");
    lblNetPostedSales = $("#lblNetPostedSales");
    lblNoTransactingAccts = $("#lblNoTransactingAccts");

    calendar.CreateCalendar();

    btn_search_empId.lookdown(
        { "url": baseUrl + "Calendar/lookUpSalesOfficerEmployee", "index_value": "1", "display_rowindex": "2" },
        {},
        function (res) {
            if (res.length == 0)
                alert("No Employee found.");
            return res;
        },
        function (res, all) {
            lbl_employeeid.text(res);
            lbl_employeename.text(all[1]);

            ClearAllData();
            if (!is_processing) {
                is_processing = true;
                getEvents();
            }
        }
    );

    calendar.find('.fc-button').click(
        function (e) {
            if (!is_processing) {

                is_processing = true;
                getEvents();
            }
        }
    );

    $('.choose_months td').click(
        function () {
            $('.choose_months td').removeClass("Active");
            $(this).addClass("Active");
            calendar.fullCalendar('gotoDate', txt_year.text(), $(this).attr("value"));
            getEvents();
        }
    );

    $('.arrow').click(
        function () {
            txt_year.text($(this).attr("id") == "btn_prev_year" ? parseInt(txt_year.text()) - 1 : parseInt(txt_year.text()) + 1);
        }
    );
});

function getEvents() {
    var new_obj = {
        userId: lbl_employeeid.text(),
        month: calendar.fullCalendar('getDate').getMonth() + 1,
        year: calendar.fullCalendar('getDate').getFullYear(),
        viewtype: "MONTHLY",
        day: 1
    }
    ClearAllData();
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "Calendar/GetEventDetails",
        success: function (res) {

            txt_year.text(calendar.fullCalendar('getDate').getFullYear());
            $('.choose_months td').removeClass("Active");
            $('.choose_months td[value="' + calendar.fullCalendar('getDate').getMonth() + '"]').addClass('Active');
            $('.field_amount').parent().css('text-align', 'right');

            // Clear Sales Info
            lbldateasof.text('N/A');
            lblGrossSales.text('N/A');
            lblCM.text('N/A');
            lblUnpostedSales.text('N/A');
            lblPostedSales.text('N/A');
            lblPending.text('N/A');
            lblBalanceOrder.text('N/A');
            lblNetPostedSales.text('N/A');
            lblNoTransactingAccts.text('N/A');

            if (!res.iserror) {

                // Sales Info
                if (res.data.sales_info.isNull == false) {
                    lbldateasof.text(res.data.sales_info.dateasof);
                    lblGrossSales.text(res.data.sales_info.gross);
                    lblCM.text(res.data.sales_info.cm);
                    lblUnpostedSales.text(res.data.sales_info.unposted);
                    lblPostedSales.text(res.data.sales_info.posted);
                    lblPending.text(res.data.sales_info.pending);
                    lblBalanceOrder.text(res.data.sales_info.balanceorder);
                    lblNetPostedSales.text(res.data.sales_info.netposted);
                    lblNoTransactingAccts.text(res.data.sales_info.noTransactingAccounts);
                }

                if (res.data.list_of_events.length != 0) {
                    lbl_calendarno.text(res.data.calendar_info.EventID);
                    lbl_status.text(res.data.calendar_info.Status);

                    // Calendar Info
                    lbl_noOfEvents.text(res.data.calendar_info.TotalEvents);
                    lbl_editedevents.text(res.data.calendar_info.TotalEdited);
                    lbl_noOfPlannedEvents.text(res.data.calendar_info.TotalPlanned);
                    lbl_noOfUnplannedEvents.text(res.data.calendar_info.TotalUnplanned);
                    lbl_NoOfVisit.text(res.data.calendar_info.TotalVisit);

                    lbl_totalCallEffective.text(res.data.calendar_info.TotalCallEffective);
                    lbl_totalEvents.text(res.data.calendar_info.TotalPlanned);
                    lbl_averageCallEffective.text(res.data.calendar_info.AverageCallEffective + '%');

                    calendar.fullCalendar('addEventSource', res.data.list_of_events);

                    HidePreloader();
                }
            }
            else {
                alert(res.message);
                ClearAllData();
            }
            is_processing = false;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            is_processing = false;
        }
    });
    NxtInventorySked(calendar.fullCalendar('getDate').getMonth() + 1, calendar.fullCalendar('getDate').getFullYear(), lbl_employeeid.text());
}


function NxtInventorySked(month, year, soId) {
    var new_obj = {
        month: month,
        year: year,
        soId: soId
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
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

function ClearAllData() {
    lbl_calendarno.text("N/A");
    lbl_status.text("N/A");
    lbl_noOfEvents.text("N/A");
    lbl_editedevents.text("N/A");
    lbl_noOfPlannedEvents.text("N/A");
    lbl_noOfUnplannedEvents.text("N/A");
    lbl_NoOfVisit.text("N/A");
    lbl_totalCallEffective.text()
    lbl_totalEvents.text()
    lbl_averageCallEffective.text()
    calendar.fullCalendar('removeEvents');
}

function getRouteChanges() {
    var getDate = calendar.fullCalendar('getDate');

    $.ajax({
        data: "EventMonth=" + getDate.getMonth() +
              "&EventYear=" + getDate.getFullYear() +
              "&EmpIDNo=" + lbl_employeeid.text(),
        type: 'POST', url: baseUrl + 'Calendar/getSOMonthlyCoverageRouteChanges',
        success: function (res) {
            $("#dialog_transaction_log_box").dialog({ title: 'Transaction log changes', width: 'inherit', height: '300', resizable: false, modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $('.ui-button-text').each(function (i) {
                $(this).html($(this).parent().attr('text'))
            });
            $("#dialog_transaction_log_box").find("table").remove();
            $("#dialog_transaction_log_box").append(res);
        },
        error: function (xhr, ajaxOption, throwError) {
            alert(xhr.status); alert(throwError);
        }
    });
}

$.fn.CreateCalendar = function () {
    $(this).fullCalendar({
        header: {
            left: 'title',
            center: 'prev,next, today',
            right: 'basicDay,basicWeek,month'
        },
        timeFormat: {
            // for agendaWeek and agendaDay do not display time in title (time already displayed in the view)
            agenda: '',

            // for all other views (19p)
            '': 'h:mmt{ - h:mm}t'
        },
        eventColor: '#ededed',
        eventTextColor: 'black',
        eventMouseover: function (event, jsEvent, view) {
            $(this).css("opacity", "1%");
        },
        eventClick: function (calEvent, jsEvent, view) {
          //  /**
            var acctCode = "";
            var view_id = calendar.find('.fc-button.fc-state-active').find('span > span').text(); //<-- use this if calendar doesn't use theme ui

            acctCode = view_id.toUpperCase() == "DAILY" ? calEvent.id : calEvent.title;
            $.fancybox({
                openEffect: 'elastic',
                closeEffect: 'elastic',
                type: 'iframe',
                href: baseUrl + "Calendar/Memoview?date=" + calEvent.start + "&day=" + calEvent.start.getDate() + "&month=" + (calendar.fullCalendar('getDate').getMonth() + 1) + "&year=" + calEvent.start.getFullYear() + "&soId=" + lbl_employeeid.text() + "&acctCode=" + acctCode,
                'overlayShow': true,
                'showCloseButton': true,
                scrolling: 'no',
                autoSize: false,
                'afterClose': function () {
                    if (!is_processing) {
                        is_processing = true;
                        getEvents();
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
          //  **/

        }
    });
}


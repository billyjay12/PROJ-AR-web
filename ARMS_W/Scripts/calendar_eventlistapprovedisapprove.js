var lnk_first = null;
var lnk_prev = null;
var lnk_next = null;
var lnk_last = null;

var total_rec = null;
var total_page = null;
var current_page = null;

var first_page_no = 1;
var prev_page_no = 0;
var next_page_no = 0;
var last_page_no = 0;

var tbl_listofso = null;

$(function () {

    lnk_first = $("#lnk_first");
    lnk_prev = $("#lnk_prev");
    lnk_next = $("#lnk_next");
    lnk_last = $("#lnk_last");

    total_rec = $("#total_rec");
    total_page = $("#total_page");
    current_page = $("#current_page");

    tbl_listofso = $("#tbl_listofso");

    GetListOfDocs(1);

    lnk_first.click(
        function () {
            GetListOfDocs(first_page_no);
        }
    );

    lnk_prev.click(
        function () {
            GetListOfDocs(prev_page_no);
        }
    );

    lnk_next.click(
        function () {
            GetListOfDocs(next_page_no);
        }
    );

    lnk_last.click(
        function () {
            GetListOfDocs(last_page_no);
        }
    );

    $("#txt_search").keyup(
    function (e) {
        //if (e.keyCode == 13) {
        GetListOfDocs(first_page_no);
        //}
    }
    );

});

function display_event_list() {
    var new_obj = { asm_id: userId, filter_by: "APPROVED_DISAPPROVED" };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8;', data:JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + 'Calendar/getListOfEvents',
        success: function (res) {

            if (res != undefined) {
                $(res).each(function (index, item) {
                    populateSoList(item[0], item[1], item[2], item[3], item[4], item[5]);
                });


            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function GetListOfDocs(page_no) {

    var DocStatusDescription = null;

    DocStatusDescription = ""; //txt_search.attr("value");

    var Keyword = $("#txt_search").attr("value"); //txt_search_member.attr("value");
    var record_per_page = $("#SelectOptionEntries option:selected").text();

    var new_obj = {
        COMMAND: "",
        PAGENO: page_no,
        SearchType: "Name",
        DocStatusDescription: "",
        rec_per_page: record_per_page,
        asm_id: userId,
        keyword: Keyword
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + 'Calendar/GetEventList?status=APPROVED_DISAPPROVED',
        success: function (res) {
            if (!res.iserror) {
                cached_data = res.data.list;
                number_of_page = res.data.int_total_pages;

                current_pageno = res.data.current_page_no;

                total_rec.text(res.data.total_records);
                current_page.text(current_pageno == 0 ? 1 : current_pageno);
                total_page.text(number_of_page == 0 ? 1 : number_of_page);

                ReCalculatePageNumbers(res.data.current_page_no, res.data.int_total_pages);

                DisplayDataF(cached_data, tbl_listofso);

            } else {
                alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

}

function DisplayDataF(data, tbl_obj) {

    var w = new Array();
    for (var i = 0; i < data.length; i++) {
        w.push("<tr>");

        for (var x = 0; x < data[i].length; x++) {
            w.push("<td>");

            if (x == 0) {
                w.push("<a href=\"javascript:;\" EventID=\"" + data[i][x] + "\" soId=\"" + data[i][1] + "\"  Month=\"" + getMonthNumber(data[i][3]) + "\"  Year=\"" + data[i][4] + "\" >" + data[i][x] + "</a>");
            } 
            else {
                w.push(data[i][x]);
            }

            w.push("</td>");
        }
        w.push("</tr>");
    }

    tbl_obj.find("tr:gt(0)").remove();

    // tbl_approved
    tbl_obj.append(w.join(""));

    // bind the clicking of links
    tbl_obj.find("a").click(
        function () {
            var EventID = $(this).attr("EventID");
            var soId = $(this).attr("soId");
            var Year = $(this).attr("Year");
            var Month = $(this).attr("Month");
            Month = (parseInt(Month) - 1).toString();

            setTimeout(function () {
                // window.location = baseUrl + 'Calendar/CalendarView?soId=' + soId + '&year=' + Year + '&month=' + Month + '&EventId=' + EventID;
                window.location = baseUrl + 'Calendar/MyCalendar?soId=' + soId + '&month=' + Month + '&year=' + Year;
            }, 0);

        }
    );
}

function getMonthNumber(month_) {

    var month = new Array();

    month = ["",
              "JANUARY",
              "FEBRUARY",
              "MARCH",
              "APRIL",
              "MAY",
              "JUNE",
              "JULY",
              "AUGUST",
              "SEPTEMBER",
              "OCTOBER",
              "NOVEMBER",
              "DECEMBER"];

    return month.indexOf(month_);

}

function ShowEntries() {
    //var SelectOptionEntries = document.getElementById("SelectOptionEntries");
    GetListOfDocs(current_page.text());
}
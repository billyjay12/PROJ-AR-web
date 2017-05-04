var lnk_first = null;
var lnk_prev = null;
var lnk_next = null;
var lnk_last = null;

var first_page_no = 1;
var next_page_no = 0;
var prev_page_no = 0;
var last_page_no = 0;

var total_rec = null;
var current_page = null;
var total_page = null;
var txt_search = null;

var tbl_listofsochanges = null;

$(function () {

    lnk_first = $("#lnk_first");
    lnk_prev = $("#lnk_prev");
    lnk_next = $("#lnk_next");
    lnk_last = $("#lnk_last");

    total_page = $("#total_page");
    current_page = $("#current_page");
    total_rec = $("#total_rec");
    txt_search = $("#txt_search");

    tbl_listofsochanges = $("#tbl_listofsochanges");

    GetListOfDoc(first_page_no);

    lnk_first.click(
        function () {
            GetListOfDoc(first_page_no);
        }
    );

    lnk_prev.click(
        function () {
            GetListOfDoc(prev_page_no);
        }
    );

    lnk_next.click(
        function () {
            GetListOfDoc(next_page_no);
        }
    );

    lnk_last.click(
        function () {
            GetListOfDoc(last_page_no);
        }
    );

    txt_search.keyup(
        function () {
            GetListOfDoc(first_page_no);
        }
    );

});

function GetListOfDoc(page_no) {

    var DocStatusDescription = null;
    DocStatusDescription = "";

    var Keyword = txt_search.attr("value");
    var record_per_page = $("#SelectOptionEntries option:selected").text();

    var new_obj = {
        COMMAND: "",
        PAGENO: page_no,
        SearchType: "Name",
        DocStatusDecription: DocStatusDescription,
        rec_per_page: record_per_page,
        asm_id: userId,
        keyword: Keyword
    }

    $.ajax({
        dataType: "json", contentType: "application/json; charset=utf-8", data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetEventList?status=CHANGES_FOR_APPROVAL",
        success: function (res) {
            if (!res.iserror) {
                cached_data = res.data.list;
                number_of_page = res.data.int_total_pages;

                current_pageno = res.data.current_page_no;

                total_rec.text(res.data.total_records);
                current_page.text(current_pageno == 0 ? 1 : current_pageno);
                total_page.text(number_of_page == 0 ? 1 : number_of_page);

                ReCalculatePageNumbers(res.data.current_page_no, res.data.int_total_pages);

                DisplayDataF(cached_data, tbl_listofsochanges);
            }
            else
                alert(res.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status, thrownError);
        }
    });

}

function DisplayDataF(data, tbl_obj) {
    var w = new Array();
    for (var i = 0; i < data.length; i++) {
        w.push("<tr>");

        for (var x = 0; x < data[i].length; x++) {
            w.push("<td>");

            if (x == 0)
                w.push("<a href=\"javascript:;\" EventID=\"" + data[i][x] + "\" soId=\"" + data[i][1] + "\" Month=\"" + getMonthNumber(data[i][3]) + "\" Year=\"" + data[i][4] + "\" SoName=\"" + data[i][2] + "\">" + data[i][x] + "</a>");
            else
                w.push(data[i][x]);

            w.push("</td>");
        }

        w.push("</tr>");
    }
    tbl_obj.find("tr:gt(0)").remove();

    tbl_obj.append(w.join(""));

    tbl_obj.find("a").click(
        function () {
            var EventID = $(this).attr("EventID");
            var soId = $(this).attr("soId");
            var Year = $(this).attr("Year");
            var Month = $(this).attr("Month");
            var SoName = $(this).attr("SoName");

            setTimeout(
                function () {
                    window.location = baseUrl + "Calendar/ChangesDtls?soId=" + soId + "&year=" + Year + "&month=" + Month + "&EventId=" + EventID + "&SoName=" + SoName;
                }, 0
            );
        }
    );
}


function getMonthNumber(month_) {
    var months = new Array();

    months = ["",
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
                "DECEMBER"
              ];

    return months.indexOf(month_);
}

function ShowEntries() {
    GetListOfDoc(first_page_no);
}
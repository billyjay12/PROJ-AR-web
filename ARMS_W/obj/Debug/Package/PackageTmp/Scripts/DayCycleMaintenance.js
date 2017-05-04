var txt_daycycle = null;

var btn_save = null;
var btn_cancel = null;
var txt_rangedaycecle = null;
var txt_startDayOfTheMonth = null;

$(function () {
    txt_daycycle = $("#txt_daycycle");

    btn_save = $("#btn_save");
    btn_cancel = $("#btn_cancel");
    txt_rangedaycecle = $("#txt_rangedaycecle");
    txt_startDayOfTheMonth = $("#txt_startDayOfTheMonth");

    txt_daycycle.forceNumeric();
    txt_startDayOfTheMonth.forceNumeric();

    $("#tab_main").tabs();

    //load details
    getDayCycleCount();

    btn_cancel.click(function () {
        if (confirm('Are you sure you want to cancel?')) {
            location.reload();
        }
    });

    btn_save.click(function () {


        if (txt_daycycle.attr("value") != "" || txt_startDayOfTheMonth.attr("value") != "") {
            var dayofmonth = parseInt(txt_startDayOfTheMonth.attr("value"));
            if (dayofmonth > 0 && dayofmonth < 31)
                UpdateDayCycle();
            else
                alert("Start Month Day Counting Schedule should be 1-30 only..!");
        }
    });
});

function getDayCycleCount() {
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "Maintenance/getDayCycleCount",
        success: function (res) {
            if (!res.iserror) {
                txt_daycycle.attr("value", res.data.dayCycleNo);
                txt_rangedaycecle.attr("value", res.data.rangeDayCycle);
                txt_startDayOfTheMonth.attr("value", res.data.startDayOfTheMonth);
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function UpdateDayCycle() {
    var new_obj = {
        DayCycleCount: txt_daycycle.attr("value") ,
        rangeDayCycle: txt_rangedaycecle.attr("value"),
        //startDayOfTheMonth:txt_startDayOfTheMonth.attr("value")
        startDayOfTheMonth: ipaddress
    }

    //UPDATE 
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Maintenance/UpdateDayCycle",
        success: function (res) {
            if (!res.iserror) {
                alert("Success");
                location.reload();
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


jQuery.fn.forceNumeric = function () {

    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            // numbers   
                         key >= 48 && key <= 57 ||
            // Numeric keypad
                         key >= 96 && key <= 105 ||
            // comma, period and minus, . on keypad
                        key == 190 || key == 188 || key == 109 || key == 110 ||
            // Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
            // Home and End
                        key == 35 || key == 36 ||
            // left and right arrows
                        key == 37 || key == 39 ||
            // Del and Ins
                        key == 46 || key == 45)
                return true;

            return false;
        });
    });
}
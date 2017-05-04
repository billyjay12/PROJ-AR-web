var dialog_transaction_log_box = null;

$(function () {
    dialog_transaction_log_box = $("#dialog_transaction_log_box");
});

function display_transaction_logs(id) {
    getRouteChanges(id);
}

function getRouteChanges(id) {
    var getDate = $("#" + id).fullCalendar('getDate');
    var new_obj = { EventID: 'EVNT000004' }
    $.ajax({
        data: "EventMonth=" + getDate.getMonth() +
              "&EventYear=" + getDate.getFullYear() +
              "&EmpIDNo=" + soId,
        type: 'POST', url: baseUrl + 'Calendar/getSOMonthlyCoverageRouteChanges',
        success: function (res) {
            dialog_transaction_log_box.dialog({ title: 'Transaction log changes', width: 'inherit', height: '300', resizable: false, modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $('.ui-button-text').each(function (i) {
                $(this).html($(this).parent().attr('text'))
            });
            dialog_transaction_log_box.find("table").remove();
            dialog_transaction_log_box.append(res);
        },
        error: function (xhr, ajaxOption, throwError) {
            alert(xhr.status); alert(throwError);
        }
    });
}
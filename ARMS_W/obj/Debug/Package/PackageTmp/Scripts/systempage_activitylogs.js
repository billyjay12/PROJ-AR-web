var tbl_activitylist = null;

$(function () {
    tbl_activitylist = $("#tbl_activitylist");

    InitializeFirstLoad();

});

function InitializeFirstLoad() {
    getListOfActivities();
    setInterval(function () { getListOfActivities(); }, 60000);
}

function getListOfActivities() {
    $.ajax({
        type: 'GET', url: baseUrl + "SystemPage/getListOfActivities",
        success: function (res) {
            $("#tbl_activitylist tbody tr").remove();
            $("#tbl_activitylist tbody").html(res);
            //tbl_activitylist.find('tbody tr').remove();
            //tbl_activitylist.find('tbody').html(res);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

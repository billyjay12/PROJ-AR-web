var tbl_list = null;
var txt_date_as_of = null;

$(function () {
    tbl_list = $("#tbl_list");
    txt_date_as_of = $("#txt_date_as_of");

    blockUI();

    $.ajax({
        type: "POST", url: baseUrl + "ItemMasterFile/getItemsAvailable",
        success: function (res) {
            if (!res.iserror) {
                //addItemToDisplay(res.data.list_items);

                $("#tbl_list tbody").append(res.data.table);
                $("#txt_date_as_of").attr("value", res.data.dateasof);

                var oTable = $('#tbl_list').dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "bSortClasses": false,
                    "iDisplayLength": 25
                });

                $("#tbl_list_filter").find('input[aria-controls="tbl_list"]').css("width", "80%");

                unblockUI();
                $(".DataTables_sort_wrapper").find('span').css("float", "right");

                $("#tbl_list_filter").find("input[type=\"text\"]").addClass("waterMarkInput");
                $("#tbl_list_filter").find("input[type=\"text\"]").attr("title", "Search Content: [column 1]<space>[column 2]<space>[column 3]");
                $("#tbl_list_filter").find("input[type=\"text\"]").val($("#tbl_list_filter").find("input[type=\"text\"]").attr("title"));
                $('.waterMarkInput').focus(function () {
                    if ($(this).val() == $(this).attr("title")) { $(this).val(""); }
                }).blur(function () {
                    if ($(this).val() == "") { $(this).val($(this).attr("title")); }
                });

            }
            else
                alert(res.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

});

function blockUI() {
    $.blockUI({ css: {
        border: 'none',
        padding: '15px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .5,
        color: '#fff'
    }
    });
}

function unblockUI() {
    // $.HidePreloader();
    $.unblockUI();
}


var lbldateasof = null;
var lblGrossSales = null;
var lblCM = null;
var lblUnpostedSales = null;
var lblPostedSales = null;
var lblPending = null;
var lblBalanceOrder = null;
var lblNetPostedSales = null;
var lblNoTransactingAccts = null;

$(function () {
    lbldateasof = $("#lbldateasof");
    lblGrossSales = $("#lblGrossSales");
    lblCM = $("#lblCM");
    lblUnpostedSales = $("#lblUnpostedSales");
    lblPostedSales = $("#lblPostedSales");
    lblPending = $("#lblPending");
    lblBalanceOrder = $("#lblBalanceOrder");
    lblNetPostedSales = $("#lblNetPostedSales");
    lblNoTransactingAccts = $("#lblNoTransactingAccts");

    getSalesInfo();
});

function getSalesInfo() {
    var new_obj = {
        userId: ""
    }

    $.ajax({
        //  dataType: 'json',
        //contentType: 'application/json; charset=utf-8', 
        //  data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "SecurePage/getSalesInfo",
        success: function (res) {

            $("#tblSalesInfo").hide();
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
                    $("#tblSalesInfo").show();
                }
            }
            else {
                alert(res.message);
            }
            is_processing = false;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

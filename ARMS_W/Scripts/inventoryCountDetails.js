var date = new Date();
var d = date.getDate();
var m = date.getMonth();
var y = date.getFullYear();

var txt_brand = null;
var txt_prodgroup = null;
var txt_itmdesc = null;
var txt_itmcode = null;
var txt_ssr = null;
var txt_begnv = null;
var txt_endnv = null;
var txt_act_selloutpcs = null;
var txt_act_amount = null;
var txt_remarks = null;

var txt_startdate = null;
var txt_enddate = null;

var txt_ftm = null;

var spn_statusCount = null;

var txt_inventoryid = null;
var txt_empId = null;
var txt_empName = null;
var txt_pareto = null;
var txt_area = null;
var txt_territory = null;
var txt_acctName = null;
var txt_acctCode = null;
var txt_acctAddress = null;
var txt_whsId = null;
var txt_whsFirstName = null;
var txt_whsMiddleName = null;
var txt_whsLastName = null;
var txt_whscontactno = null;
var txt_prevcountdate = null;
var txt_currdate = null;
var txt_nxtcount = null;
var txt_countrange = null;

var txt_overallRemarks = null;
var txt_totalamount = null;
var txt_totalActAmount = null;
var txt_totalSForecastAmount0 = null;
var txt_totalSForecastAmount1 = null;
var txt_totalSForecastAmount2 = null;
var txt_totalSForecastAmount3 = null;

var txt_totalVarAmount = null;

var txt_dateencoded = null;

var tbl_column_controller = null;

var btnrad_yes = null;
var btnrad_no = null;

var btn_add = null;
var btn_save = null;
var btn_return = null;
var btn_cancel = null;

var btn_edit = null;
var btn_doneEdit = null;

var grp_edit = null;
var grp_save = null;

var btn_save_draft = null;

var TotalAmount = 0;
var master_checkbox = null;

$(function () {
    $("#tab_main").tabs();

    txt_brand = $("#txt_brand");
    txt_prodgroup = $("#txt_prodgroup");
    txt_itmdesc = $("#txt_itmdesc");
    txt_ssr = $("#txt_ssr");
    txt_begnv = $("#txt_begnv");
    txt_endnv = $("#txt_endnv");
    txt_act_selloutpcs = $("#txt_act_selloutpcs");
    txt_act_amount = $("#txt_act_amount");
    txt_remarks = $("#txt_remarks");
    txt_itmcode = $("#txt_itmcode");

    txt_inventoryid = $("#txt_inventoryid");
    txt_empId = $("#txt_empId");
    txt_empName = $("#txt_empName");
    txt_pareto = $("#txt_pareto");
    txt_area = $("#txt_area");
    txt_territory = $("#txt_territory");
    txt_acctName = $("#txt_acctName");
    txt_acctCode = $("#txt_acctCode");
    txt_acctAddress = $("#txt_acctAddress");
    txt_whsFirstName = $("#txt_whsFirstName");
    txt_whsMiddleName = $("#txt_whsMiddleName");
    txt_whsLastName = $("#txt_whsLastName");
    txt_whsId = $("#txt_whsId");

    btnrad_yes = $("#btnrad_yes");
    btnrad_no = $("#btnrad_no");

    txt_dateencoded = $("#txt_dateencoded");

    txt_startdate = $("#txt_startdate");
    txt_enddate = $("#txt_enddate");

    txt_ftm = $("#txt_ftm");

    txt_whscontactno = $("#txt_whscontactno");
    txt_prevcountdate = $("#txt_prevcountdate");
    txt_currdate = $("#txt_currdate");
    txt_nxtcount = $("#txt_nxtcount");
    txt_countrange = $("#txt_countrange");

    txt_overallRemarks = $("#txt_overallRemarks");
    txt_totalamount = $("#txt_totalamount");
    txt_totalActAmount = $("#txt_totalActAmount");
    txt_totalSForecastAmount0 = $("#txt_totalSForecastAmount0");
    txt_totalSForecastAmount1 = $("#txt_totalSForecastAmount1");
    txt_totalSForecastAmount2 = $("#txt_totalSForecastAmount2");
    txt_totalSForecastAmount3 = $("#txt_totalSForecastAmount3");
    txt_totalVarAmount = $("#txt_totalVarAmount");

    spn_statusCount = $("#spn_statusCount");

    btn_add = $("#btn_add");
    btn_save = $("#btn_save");
    btn_save_draft = $("#btn_save_draft");
    btn_return = $("#btn_return");
    btn_cancel = $("#btn_cancel");

    btn_forecast = $("#btn_forecast");
    btn_edit = $("#btn_edit");
    btn_save_edit = $("#btn_save_edit");

    btn_searchWhsIncharge = $("#btn_searchWhsIncharge");
    btn_searchAcctName = $("#btn_searchAcctName");
    btn_searchAcctCode = $("#btn_searchAcctCode");
    btn_searchSOID = $("#btn_searchSOID");

    grp_edit = $("#grp_edit");
    grp_save = $("#grp_save");


    //beautification of table using jquery theme
    $("#tbl_details th").addClass("ui-state-default");


    btn_edit = $("#btn_edit");
    btn_doneEdit = $("#btn_doneEdit");

    grp_edit.hide();
    grp_save.hide();

    btn_doneEdit.hide();
    blockUI();
    getInventoryDetails();
    readonlyFields();

    tbl_column_controller = $("#tbl_column_controller");
    master_checkbox = $("#master_checkbox");

    tbl_column_controller.find(".chkbox_column").click(
        function () {
            //ShowHide_Column($(this));
            if ($(this).attr("tbltype") == "freezehdr")
                ShowHide_Column_Freezehdr($(this));
            else
                ShowHide_Column_Scrollhdr($(this));
        }
    );

    master_checkbox.click(
        function () {
            $(".chkbox_column").attr("checked", $(this).is(":checked") ? true : false);
            $(".chkbox_column").each(function () {
                //ShowHide_Column($(this));
                if ($(this).attr("tbltype") == "freezehdr")
                    ShowHide_Column_Freezehdr($(this));
                else
                    ShowHide_Column_Scrollhdr($(this));
            });
        }
    );

    $("#aaaa").click(function () {
        alert($("#tbl_details").html());
    });
});


function ShowHide_Column_Scrollhdr(obj) {
    var colNo_ = 0;
    var colNo = obj.attr("name").replace("col", "");
    var show = obj.is(":checked");
    var selector = "#tbl_scroll_header tr[class=\"tr_total\"] td:nth-child(" + colNo + ")," +
                   "#tbl_scroll_header tr[clone=\"true\"] td:nth-child(" + colNo + ")," +
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")";

    colNo_ = parseInt(colNo) + 1;

    if (colNo == 6) { //ACTUAL SELL_OUT
        selector = "#tbl_scroll_header tr td:nth-child(" + colNo + ")," + // PCS column
                   "#tbl_scroll_header tr td:nth-child(" + colNo_ + ")," + // Amount column
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")"; //table header
    }
    else if (colNo == 7) { // VARIANCE
        selector = "#tbl_scroll_header tr td:nth-child(" + colNo_ + ")," + //PCS column
                   "#tbl_scroll_header tr td:nth-child(9)," + //Amount column
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")"; //table header
    }
    else if (colNo == 8) { // SALES FORECAST (0) 
        selector = "#tbl_scroll_header tr td:nth-child(10)," + // PCS column
                   "#tbl_scroll_header tr td:nth-child(11)," + // Amount column
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")"; //table header
    }
    else if (colNo == 9) { // SALES FORECAST (1)
        selector = "#tbl_scroll_header tr td:nth-child(12)," + //PCS column
                   "#tbl_scroll_header tr td:nth-child(13)," + //Amount column
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")"; //table header
    }
    else if (colNo == 14) {
        selector = "#tbl_scroll_header tr td:nth-child(" + colNo + ")," + //PCS column
                   "#tbl_scroll_header tr th:nth-child(10)"; //table header
    }

    if (show) {
        $(selector).show('fast',
                                    function () {
                                        obj.css("display", "table-cell");
                                    }
                                );
    } else {
        $(selector).hide();
    }
}

function ShowHide_Column_Freezehdr(obj) {
    var colNo_ = 0;
    var colNo = obj.attr("name").replace("col", "");
    var show = obj.is(":checked");
    var selector = "#tbl_freeze_header tr[clone=\"true\"] td:nth-child(" + colNo + ")," +
                   "#tbl_freeze_header tr[class=\"last_row\"] td:nth-child(" + colNo + ")," +
                   "#tbl_freeze_header tr th:nth-child(" + colNo + ")";

    if (show) {
        $(selector).show('fast',
                                    function () {
                                        obj.css("display", "table-cell");
                                    }
                                );
    } else {
        $(selector).hide();
    }
}

function disableReadonlyList() {
    $("#tbl_details .cls_input").attr("readonly", false);
}

function enableReadonlyList() {
    $("#tbl_details .cls_input").attr("readonly", true);
}

function getInventoryDetails() {
    var new_obj = { inventoryCountId: inventorycount_id };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Inventory/getInventoryDetails",
        success: function (res) {
            if (!res.iserror) {
                // displayValues(res.data.inventoryHdr, res.data.whsInchargeDtl, res.data.custOutletDtl, res.data.inventoryDtl, res.data.permission);
                displayValues(res.data.inventoryHdr, res.data.permission);
                //alert(res.data.iptest);

                unblockUI();
            } else {
                unblockUI();
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
        }
    });
}

function reload() {
    location.reload();
}

function displayValues(inventoryHdr,permission) {
    txt_inventoryid.attr("value", inventoryHdr.inventoryCountId);
    txt_empId.attr("value", inventoryHdr.empId);
    txt_empName.attr("value", inventoryHdr.empFirstName + ' ' + inventoryHdr.empLastName);

    if (inventoryHdr.pareto == 'Y')
        btnrad_yes.attr("checked", true);
    else
        btnrad_no.attr("checked", true);

    txt_area.attr("value", inventoryHdr.area);
    txt_territory.attr("value", inventoryHdr.territoryName);
    txt_acctCode.attr("value", inventoryHdr.acctCode);

    //cust outlet details
    txt_acctName.attr("value", inventoryHdr.acctName);
    txt_acctAddress.attr("value", inventoryHdr.acctAddress);

    //warehouse incharge detail
    txt_whsFirstName.attr("value", inventoryHdr.whs_details.whsInchargeFirstName);
    txt_whsMiddleName.attr("value", inventoryHdr.whs_details.whsInchargeMiddleName);
    txt_whsLastName.attr("value", inventoryHdr.whs_details.whsInchargeLastName);
    txt_whsId.attr("value", inventoryHdr.whs_details.whsInchargeID);
    txt_whscontactno.attr("value", inventoryHdr.whs_details.whsInchargeContactNo);

    txt_prevcountdate.attr("value", FormatDate(inventoryHdr.prevCountDate));
    txt_currdate.attr("value", FormatDate(inventoryHdr.actualCountDate));
    txt_nxtcount.attr("value", FormatDate(inventoryHdr.nextCountDate));
    txt_countrange.attr("value", inventoryHdr.countRange);

    txt_overallRemarks.attr("value", inventoryHdr.remarks);
    txt_totalActAmount.attr("value", numberWithCommas(inventoryHdr.totalAmount));

    txt_startdate.attr("value", FormatDate(inventoryHdr.start_date));
    txt_enddate.attr("value", FormatDate(inventoryHdr.end_date));

    txt_ftm.attr("value", inventoryHdr.forthemonth);

    txt_dateencoded.attr("value", FormatDate(inventoryHdr.dateTimeStamp));

    spn_statusCount.text(inventoryHdr.inventoryCountStatus);

    spn_statusCount.css("color", inventoryHdr.inventoryCountStatus == "HIT" ? "green" : "red");


    txt_totalVarAmount.attr("value", numberWithCommas(inventoryHdr.totalVarianceAmt));
    txt_totalamount.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt));
  //  txt_totalSForecastAmount0.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt0));
  //  txt_totalSForecastAmount1.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt1));
    $("#txt_totalamount0").attr("value", numberWithCommas(inventoryHdr.totalForecastAmt0));    
    $("#txt_totalamount1").attr("value", numberWithCommas(inventoryHdr.totalForecastAmt1));    
//    txt_totalSForecastAmount2.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt2));
//    txt_totalSForecastAmount3.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt3));

    //$("#tbl_details .last_row").before(inventoryHdr.inventoryCountlist_tablebuilder);
    $("#tbl_freeze_header .last_row").before(inventoryHdr.inventoryCountlist_tablebuilder_freeze);
    $("#tbl_scroll_header .last_row").before(inventoryHdr.inventoryCountlist_tablebuilder);


    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);

    }

function numberWithCommas(x) {
    x = Math.round(x == undefined ? 0 : x);
    return x == undefined ? "0" : x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function addItem_forecast(lineNo, brand, prodGrp, itmCode, itmDesc, ssr, begNv , sellIn, endNv, actSelloutPcs, actSelloutAmt, variancePcs,varianceAmt,forecastFTMpcs0,forecastFTMamt0,forecastFTMpcs1,forecastFTMamt1,forecastFTMpcs2,forecastFTMamt2,forecastFTMpcs3, forecastFTMamt3, remarks,itemPrice) {
    $("#tbl_freeze_header .last_row").before('<tr height="30px" clone="true" class="' + itmCode + '" isNew="' + isNew + '">' +
        '<td><input type="text" value="' + lineNo + '" style="width:40px; text-align:center; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:50px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px; background-color:#ededed;" /></td>' +
        '</tr>');

    $("#tbl_scroll_header .last_row").before('<tr height="30px" class="' + itmCode + '" clone="true" isNew="' + isNew + '">' +
        '<td><input type="number" class="cls_input" value="' + ssr + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:80px;  text-align:center;" /></td>' +
        '<td><input type="text" value="' + begNv + '" style="width:60px; background-color:#ededed; text-align:center;" /></td>' +
        '<td><input type="text" value="' + netSellInPcs + '" style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="number" class="cls_input" value="' + endNv + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:80px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + netOnHand + '" style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(selloutPcs) + '" style="width:50px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(actSelloutAmt) + '" style="width:80px; background-color:#ededed; text-align:right"/></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMpcs0) + '" style="width:70px; background-color:#ededed; text-align:center" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt0) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        '<td><input type="number" class="cls_input forecast1" onblur="validateInput(this);"  value="' + forecastFTMpcs1 + '" oninput="maxLengthCheck(this)" min="' + forecastFTMpcs1 + '" maxLength="6"  code="' + forecastFTMpcs1 + '" style="width:70px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt1) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
//        '<td><input type="number" class="cls_input" value="' + forecastFTMpcs2 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
//        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt2) + '" style="width:80px; background-color:#ededed; text-align:right;" /></td>' +
//        '<td><input type="number" class="cls_input" value="' + forecastFTMpcs3 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
//        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt3) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        '<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '<td><img class="btn" id="btn_delete" src="' + baseUrl + 'Images/delete.png" /></td>' +
        '<td><input type="text" value="' + itemPrice + '" style="display:none" /></td>' +
        '</tr>')
}

function addCommas(str) {
    var new_str = new String(str);
    var isNegative = false;

    if (new_str.indexOf("-") != -1) {
        str = new_str.replace("-", "");
        isNegative = true;
    }

    str = parseFloat(str).toFixed(2);
    var amount = new String(str);
    amount = amount.split("").reverse();
    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if (i != 2) {
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = ',' + output;
        }
    }

    if (isNegative) output = "-" + output;

    return output;
}

function undoAddComma(str) {
    var amount = new String(str);
    for (var i = 0; i < amount.length - 1; i++) {
        if (amount.indexOf(",") != -1) {
            amount = amount.replace(",", "");
        }
        else
            break;
    }
    return amount;
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

function readonlyFields() {
    $("input[type=text]").attr("readonly", true);
    $("input[type=text]").addClass("readonly");
}


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
    //$.HidePreloader();
    $.unblockUI();
}
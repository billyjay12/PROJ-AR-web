var ns4;

var date = new Date();
var d = date.getDate();
var m = date.getMonth();
var y = date.getFullYear();

var txt_brand = null;
var txt_prodgroup = null;
var txt_itmdesc = null;
var txt_itmcode = null;
var txt_actualCount = null;
var txt_remarks = null;

var txt_inventoryauditid = null;

var txt_ftm = null;

var txt_inventoryid = null;
var txt_empId = null;
var txt_empName = null;
var txt_pareto = null;
var txt_area = null;
var txt_acctName = null;
var txt_acctCode = null;
var txt_acctAddress = null;
var txt_whsId = null;
var txt_whsFirstName = null;
var txt_whsMiddleName = null;
var txt_whsLastName = null;
var txt_whscontactno = null;
var txt_currdate = null;
var spn_statusCount = null;

var txt_overallRemarks = null;
var txt_totalCount = null;

var btn_add = null;
var btn_save = null;
var btn_cancel = null;

var btn_edit = null;
var btn_done_edit = null;

var grp_save = null;
var grp_edit = null;

var Total = 0;

var listOfItems = null;

$(document).ready(function () {
    blockUI();
});


$(function () {

    txt_brand = $("#txt_brand");
    txt_prodgroup = $("#txt_prodgroup");
    txt_itmdesc = $("#txt_itmdesc");
    txt_actualCount = $("#txt_actualCount");
    txt_remarks = $("#txt_remarks");
    txt_itmcode = $("#txt_itmcode");

    txt_ftm = $("#txt_ftm");

    txt_inventoryauditid = $("#txt_inventoryauditid");

    txt_inventoryid = $("#txt_inventoryid");
    txt_empId = $("#txt_empId");
    txt_empName = $("#txt_empName");
    txt_pareto = $("#txt_pareto");
    txt_area = $("#txt_area");
    txt_acctName = $("#txt_acctName");
    txt_acctCode = $("#txt_acctCode");
    txt_acctAddress = $("#txt_acctAddress");
    txt_whsFirstName = $("#txt_whsFirstName");
    txt_whsMiddleName = $("#txt_whsMiddleName");
    txt_whsLastName = $("#txt_whsLastName");
    txt_whsId = $("#txt_whsId");
    txt_whscontactno = $("#txt_whscontactno");
    txt_currdate = $("#txt_currdate");


    spn_statusCount = $("#spn_statusCount");

    txt_overallRemarks = $("#txt_overallRemarks");
    txt_totalCount = $("#txt_totalCount");


    btn_add = $("#btn_add");
    btn_save = $("#btn_save");
    btn_cancel = $("#btn_cancel");

    btn_edit = $("#btn_edit");
    btn_done_edit = $("#btn_done_edit");

    grp_edit = $("#grp_edit");
    grp_save = $("#grp_save");

    btn_done_edit.hide();
    //beautification of table using jquery theme
    $("#tbl_details thead th").addClass("ui-state-default");

    //diplay details
    getInventoryDetails();

    //make fields readonly
    readonlyFields()

    //current date
    txt_currdate.attr("value", m + 1 + "/" + d + "/" + y);

    txt_actualCount.forceNumeric();
    getItemList1();
    txt_itmdesc.lookupTextField();

    //    txt_itmcode.lookdown(
    //        { "url": baseUrl + "Inventory/lookUpItemCode", "index_value": "1", "display_rowindex": "5" },
    //        {}, function (res) { return res; },
    //        function (res, all) {
    //            txt_itmcode.attr("value", res);
    //            txt_itmdesc.attr("value", all[1]);
    //            txt_brand.attr("value", all[2]);
    //            txt_prodgroup.attr("value", all[3]);
    //        });

    btn_add.click(function () {
        if (isValid()) {
            if (isNotDuplicate(txt_itmcode.attr("value"))) {
                var line_no = $("#tbl_details").find("tr:gt(0)").length - 2;
                addItem(line_no,
                        txt_brand.attr("value"),
                        txt_prodgroup.attr("value"),
                        txt_itmcode.attr("value"),
                        txt_itmdesc.attr("value"),
                        txt_actualCount.attr("value"),
                        txt_remarks.attr("value"));
                clearField();
            }
        }
    });

    btn_edit.click(function () {

        enableEditFields();

        btn_edit.hide();
        btn_done_edit.show();


        $('#tbl_details .cls_input').forceNumeric();
        $('#tbl_details .cls_input').attr("maxlength", "5");

        $('#tbl_details .remarks').unbind();
        $('#tbl_details .remarks').attr("maxlength", "300");


        grp_save.hide();
    });

    btn_done_edit.click(function () {
        if (validateEditData()) {

            btn_done_edit.hide();
            btn_edit.show();

            grp_save.show();

            disableEditFields();

            //compute Total Actual COUNT
            computeActualCountAmount();

        }
    });

    btn_save.click(function () {
        DisplayPreloader();
        saveInventoryCount();
    });

    btn_cancel.click(function () {
        if (confirm('Are you sure you want to cancel?')) {
            location.reload();
        }
    });
});

function getItemList1() {
    $.ajax({
        type: 'GET',
        url: baseUrl + "Inventory/lookUpItemCode1",
        data: { prevCountDate: "",
            actualCountDate:"",
            acctCode: ""
        },
        success: function (res) {
            listOfItems = res;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function computeActualCountAmount() {
    Total = 0;

    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);
            var actualCount = curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value");

            Total = parseFloat(Total) + parseFloat(undoAddComma(actualCount));

            txt_totalCount.attr("value",numberWithCommas(Total));

        });
}

function saveInventoryCount() {

    var inventorycount_list = new Array();
    var bool_hasRow = false;

    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            inventorycount_list.push({
                lineId: curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value"),
                itemCode: curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value"),
                actualCount: undoAddComma(curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value")),
                remarks: curr_row.find("td:nth-child(7)").find("input[type=text]").attr("value")
            });

            bool_hasRow = true;
        });

    var new_obj = {
        inventoryCountId: inventorycount_id,
        inventoryCountAuditId: txt_inventoryauditid.attr("value"),
        date: txt_currdate.attr("value"),
        remarks: txt_overallRemarks.attr("value"),
        inventoryAudit_details: inventorycount_list
    }

    if (bool_hasRow) {
        //UPDATE
        $.ajax({
            dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
            type: "POST", url: baseUrl + "Inventory/saveAuditInventoryCount",
            success: function (res) {
                if (!res.iserror) {
                    alert("Success");
                    window.location = baseUrl + "Inventory/AuditInventoryCountList";
                } else {
                    HidePreloader();
                    /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                HidePreloader();
                alert(xhr.status); alert(thrownError);
            }
        });
    }
}

function getInventoryDetails() {
    var new_obj = { inventoryCountId: inventorycount_id };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Inventory/getForAuditInventoryDetails",
        success: function (res) {
            if (!res.iserror) {

                displayValues(res.data.inventoryHdr, res.data.permission);
                $('#tbl_details tr[clone="true"]').each(
                                function (index, elem) {
                                    var curr_row = $(elem);

                                    curr_row.find("td:nth-child(6)").find("input[type=text]").decifield();
                                    //$(curr_row)
                                });

                $('.cls_input').click(function () {
                    $(this).select();
                });

                btn_edit.click();
                unblockUI();
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
                unblockUI();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
        }
    });
}


function displayValues(inventoryHdr) {
    txt_inventoryid.attr("value", inventoryHdr.inventoryCountId);
    txt_empId.attr("value", inventoryHdr.empId);
    txt_empName.attr("value", inventoryHdr.empFirstName + ' ' + inventoryHdr.empLastName);
   // txt_pareto.attr("value", inventoryHdr.pareto);
    txt_area.attr("value", inventoryHdr.area);
    txt_acctCode.attr("value", inventoryHdr.acctCode);

    if (inventoryHdr.pareto == 'Y')
        $("#btnrad_yes").attr("checked", true);
    else
        $("#btnrad_no").attr("checked", true);


    //cust outlet details
    txt_acctName.attr("value", inventoryHdr.acctName);
    //txt_acctName.attr("code", inventoryHdr.custoutlet_details.custOutletsID);
    txt_acctAddress.attr("value", inventoryHdr.acctAddress);

    //warehouse incharge detail
    txt_whsFirstName.attr("value", inventoryHdr.whs_details.whsInchargeFirstName);
    txt_whsMiddleName.attr("value", inventoryHdr.whs_details.whsInchargeMiddleName);
    txt_whsLastName.attr("value", inventoryHdr.whs_details.whsInchargeLastName);
    txt_whsId.attr("value", inventoryHdr.whs_details.whsInchargeID);
    txt_whscontactno.attr("value", inventoryHdr.whs_details.whsInchargeContactNo);

    txt_ftm.attr("value", inventoryHdr.forthemonth);

    spn_statusCount.text(inventoryHdr.inventoryCountStatus);

    $(inventoryHdr.inventorycount_list).each(function (index, elem) {
        addItem(elem.lineId, elem.brand, elem.prodGrp, elem.itemCode, elem.itemDesc, "", "");
    });
}

function enableEditFields() {
    $("#tbl_details .cls_input").attr("readonly", false);
    $("#tbl_details .cls_input").css("background", "#fff7dd");

    $("#tbl_details .remarks").css("background", "#ffffff");
}

function disableEditFields() {
    $("#tbl_details .cls_input").attr("readonly", true);
    $("#tbl_details .cls_input").css("background", "#ededed");
}

function readonlyFields() {
    $("input[type=text]").attr("readonly", true);
    $("tr[class!=last_row] input[type=text]").addClass("readonly");


    //not included
    txt_actualCount.attr("readonly", false);
    txt_remarks.attr("readonly", false);

    txt_actualCount.removeClass("readonly");
    txt_remarks.removeClass("readonly");
}

function isNotDuplicate(itemCode) {
    var message = "item is already in the list";
    var Error = false;
    $('#tbl_details tr[clone="true"]').each(function () {
        if ($(this).find('td:eq(3) input').val() == itemCode) {
            Error = true;
        }
    });

    if (Error) { alert(message); }

    return !Error;
}

function validateEditData() {
    var message = "Empty field!";
    var Error = false;
    $('#tbl_details tr[clone="true"]').each(function () {
        if ($(this).find('td:eq(5) input').val() == "") {
            Error = true;
        }
    });

    if (Error) { alert(message); }

    return !Error;
}




function clearField() {
    txt_brand.removeAttr("value");
    txt_prodgroup.removeAttr("value");
    txt_itmcode.removeAttr("value");
    txt_itmdesc.removeAttr("value");
    txt_actualCount.removeAttr("value");
    txt_remarks.removeAttr("value");
}

function isValid() {
    var message = "fill up all empty fields.";
    var isError = false;

    if (txt_brand.attr("value") == "" || txt_prodgroup.attr("value") == "" ||
        txt_itmdesc.attr("value") == "" || txt_itmcode.attr("value") == "" || txt_actualCount.attr("value") == "") {
        isError = true;
    }


    if (isError) { alert(message); }

    return !isError;
}

function recountLineNo() {
    var line_doc_no = $('#tbl_details').find("tr:gt(0)").length - 1;
    var lineno = 0;
    if (line_doc_no > 0)
        $($('#tbl_details').find("tr:gt(0)")).each(
        function (index, elem) {
            var curr_row = $(elem);

            curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value", lineno);

            lineno = lineno + 1;

            if ((line_doc_no - 1) == index) return false;
        });
}


function addItem(lineNo, brand, prodGrp, itmCode, itmDesc, actualCount, remarks) {

    $("#tbl_details .last_row").before('<tr clone="true">' +
        '<td><input type="text" value="' + lineNo + '" style="width:50px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:70px;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px;" /></td>' +
        '<td><input type="text" class="cls_input" value="' + numberWithCommas(actualCount) + '" style="width:100px; text-align:center" /></td>' +
        '<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '<td><img class="btn" id="btn_delete"  src="' + baseUrl + 'Images/delete.png" /></td>' +
        '</tr>').prev().find("#btn_delete").click(function () {
            var row = $(this).parent().parent();

            row.remove();
            var cur_total = row.find("td:nth-child(6)").find("input[type=text]").attr("value");

            Total = Total - parseFloat( undoAddComma(cur_total));


            txt_totalCount.attr("value",Total);

            //repopulate line no
            recountLineNo();
        });

        recountLineNo();
    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);


    Total = parseFloat(undoAddComma(Total)) + parseFloat(undoAddComma(actualCount) == "" ? 0 : undoAddComma(actualCount));

    txt_totalCount.attr("value", numberWithCommas(Total));

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
    $.unblockUI();
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

$.fn.lookupTextField = function () {
    $(this).live({
        focus: function () {
            create_dialog_box($(this).attr('id'));
        }
    });
}

function create_dialog_box(obj_id_to_position) {
    $('#div_last_element').append('<div id="id_bkg" class="dlg_box_bkg" onclick="javascript:hide_dialog_box();"></div>' +
    '<div id="id_content" class="dlg_box_content">' +
    '<table cellspacing="0" cellpadding="0" border="0">' +
    '<tr>' +
    '<td>' +
    '<select  data-placeholder="Select Item.."  id="' + obj_id_to_position + '_value" style="width:' + ($("#"+obj_id_to_position).css("width")) + '; font-family:Arial; size:12px; outline:none;">' +
    '</select>' +
    '</td>' +
    '</tr>' +
    '<tr align="center">' +
    '<td><button onclick="javascript:setValueFromSelect(\'' + obj_id_to_position + '\');" style="cursor:pointer;">Select</button></td>' +
    '</tr>' +
    '</table>' +
    '</div>');


    $('#' + obj_id_to_position + '_value').append(listOfItems).chosen();

    var x = getElLeft(document.getElementById(obj_id_to_position));
    var y = getElTop(document.getElementById(obj_id_to_position));

    $('#id_content').css({
        left: x + '' + 'px',
        top: y + '' + 'px'
    });

    $('#id_content').show('fast');
    $('#id_bkg').show();
    $('#div_last_element select').focus();
}

function hide_dialog_box() {
    $('#id_content').hide();
    $('#id_bkg').hide();
    $("#id_content").remove();
    $("#id_bkg").remove();
}

function setValueFromSelect(obj_id_to_position) {

    var code = $("#id_content select option:selected").attr("code");
    var desc = $("#id_content select option:selected").attr("value");
    var brand = $("#id_content select option:selected").attr("brand");
    var prodgrp = $("#id_content select option:selected").attr("prodgrp");
    var amt = $("#id_content select option:selected").attr("amt");
    var sellin = $("#id_content select option:selected").attr("sellin");

    txt_itmcode.attr("value", code);
    txt_itmdesc.attr("value", desc);
    txt_brand.attr("value", brand);
    txt_prodgroup.attr("value", prodgrp);

    //            txt_itmcode.attr("value", res);
    //            txt_itmdesc.attr("value", all[1]);
    //            txt_brand.attr("value", all[2]);
    //            txt_prodgroup.attr("value", all[3]);

    hide_dialog_box();
}

function numberWithCommas(x) {
    x = Math.round(x == undefined ? 0 : x);
    return x == undefined ? "0" : x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
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


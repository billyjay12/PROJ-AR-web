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
var txt_auditedBy = null;

var txt_ftm = null; ;

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
var txt_currdate = null;
var spn_statusCount = null;

var txt_overallRemarks = null;
var txt_totalCount = null;

var Total = 0;

$(function () {
    txt_brand = $("#txt_brand");
    txt_prodgroup = $("#txt_prodgroup");
    txt_itmdesc = $("#txt_itmdesc");
    txt_actualCount = $("#txt_actualCount");
    txt_remarks = $("#txt_remarks");
    txt_itmcode = $("#txt_itmcode");

    txt_ftm = $("#txt_ftm");

    txt_inventoryauditid = $("#txt_inventoryauditid");
    txt_auditedBy = $("#txt_auditedBy");

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
    txt_whscontactno = $("#txt_whscontactno");
    txt_currdate = $("#txt_currdate");


    spn_statusCount = $("#spn_statusCount");

    txt_overallRemarks = $("#txt_overallRemarks");
    txt_totalCount = $("#txt_totalCount");

    //beautification of table using jquery theme
    $("#tbl_details thead th").addClass("ui-state-default");

    //display details
    getInventoryAuditDetails();

    //readonly
    readonlyFields();
});


function getInventoryAuditDetails() {
    DisplayPreloader();
    var new_obj = { inventoryCountAuditId: inventorycountaudit_id };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Inventory/getInventoryCountAuditDetails",
        success: function (res) {
            if (!res.iserror) {

                displayValues(res.data.inventoryHdr, res.data.permission);

                 // fnFeaturesInit();
//                $('#tbl_details').dataTable({
//                    "bJQueryUI": true,
//                    "sPaginationType": "full_numbers",
//                    "bSortClasses": false,
//                    "bAutoWidth":true
//                });
                HidePreloader();
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
                HidePreloader();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}

function displayValues(inventoryHdr) {
    txt_inventoryauditid.attr("value", inventoryHdr.inventoryCountAuditId);

    txt_auditedBy.attr("value", inventoryHdr.auditedByID + " - " + inventoryHdr.auditedByName);

    txt_inventoryid.attr("value", inventoryHdr.inventoryCountId);
    txt_empId.attr("value", inventoryHdr.empId);
    txt_empName.attr("value", inventoryHdr.empFirstName + ' ' + inventoryHdr.empLastName);

    txt_area.attr("value", inventoryHdr.area);
    txt_territory.attr("value", inventoryHdr.territoryName);
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

    txt_currdate.attr("value", FormatDate(inventoryHdr.date));
    txt_overallRemarks.attr("value", inventoryHdr.remarks);

    txt_ftm.attr("value", inventoryHdr.forthemonth);

    spn_statusCount.text(inventoryHdr.inventoryCountStatus);

    txt_totalCount.attr("value", addCommas(inventoryHdr.totalCount));


    $("#tbl_details .last_row").before(inventoryHdr.inventoryCountlist_tablebuilder);
    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);
   // $(inventoryHdr.inventorycount_list).each(function (index, elem) {
   //     addItem(elem.lineId, elem.brand, elem.prodGrp, elem.itemCode, elem.itemDesc, elem.actualCount, elem.remarks);
   // });
}


//function addItem(lineNo, brand, prodGrp, itmCode, itmDesc, actualCount, remarks) {

//    $("#tbl_details .last_row").before('<tr clone="true">' +
//        '<td><input type="text" value="' + lineNo + '" style="width:50px; text-align:center;" /></td>' +
//        '<td><input type="text" value="' + brand + '"  style="width:60px;" /></td>' +
//        '<td><input type="text" value="' + prodGrp + '" style="width:70px;" /></td>' +
//        '<td><input type="text" value="' + itmCode + '" style="width:120px;" /></td>' +
//        '<td><input type="text" value="' + itmDesc + '" style="width:250px;" /></td>' +
//        '<td><input type="text" value="' + actualCount + '" style="width:100px; text-align:center" /></td>' +
//        '<td><input type="text" class="cls_input" value="' + remarks + '" /></td>' +
//        '</tr>');

//    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
//    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);
//}

function addItem(lineNo, brand, prodGrp, itmCode, itmDesc, actualCount, remarks) {
    $("#tbl_details .last_row").before('<tr clone="true">' +
        '<td><input type="text" value="' + lineNo + '" style="width:50px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:70px;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px;" /></td>' +
        '<td><input type="text" class="cls_input" value="' + actualCount + '" style=" text-align:center" /></td>' +
        '<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '</tr>')

    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);
}

function readonlyFields() {
    $("input[type=text]").attr("readonly", true);
    $("input[type=text]").addClass("readonly");

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
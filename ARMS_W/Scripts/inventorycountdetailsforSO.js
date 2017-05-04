
var ns4;

var date = new Date();
var d = date.getDate();
var m = date.getMonth();
var y = date.getFullYear();

var txt_brand = null;
var txt_prodgroup = null;
var txt_itmdesc = null;
var txt_itmcode = null;
var txt_ssr = null;
var txt_netSellIn = null;
var txt_begnv = null;
var txt_endnv = null;
var txt_act_selloutpcs = null;
var txt_act_amount = null;
var txt_remarks = null;
var txt_hidAmt = null;

var txt_forecastAmt = null;
var txt_forecastPcs = null;

var txt_startdate = null;
var txt_enddate = null;
var txt_ftm = null;;

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
var txt_countduedate = null;

var txt_forecastPcs0 = null;
var txt_forecastPcs1 = null;
var txt_forecastPcs2 = null;
var txt_forecastPcs3 = null;

var spn_statusCount = null;

var txt_overallRemarks = null;
var txt_totalamount = null;

var txt_totalamount0 = null;
var txt_totalamount1 = null;
var txt_totalamount2 = null;
var txt_totalamount3 = null;

var tbl_column_controller = null;
var master_checkbox = null;

var btn_add = null;
var btn_save_draft = null;
var btn_save = null;
var btn_cancel = null;

var btn_edit = null;
var btn_doneEdit = null;

var btn_searchWhsIncharge = null;
var btn_searchAcctCode = null;
var btn_searchAcctName = null;
var btn_searchSOID = null;

var grp_edit = null;
var grp_save = null;


var bool_isNewWhsIncharge = false;

var TotalAmount = 0;
var TotalAmount = 0;
var TotalAmount0 = 0;
var TotalAmount1 = 0;
var TotalAmount2 = 0;
var TotalAmount3 = 0;

var listOfItems = null;
var prev_brand = null;
var actualCountEndValidDate = null;

$(function () {
    $("#tab_main").tabs();

    // DisplayPreloader();
    blockUI();

    txt_brand = $("#txt_brand");
    txt_prodgroup = $("#txt_prodgroup");
    txt_itmdesc = $("#txt_itmdesc");
    txt_ssr = $("#txt_ssr");
    txt_netSellIn = $("#txt_netSellIn");
    txt_begnv = $("#txt_begnv");
    txt_endnv = $("#txt_endnv");
    txt_act_selloutpcs = $("#txt_act_selloutpcs");
    txt_act_amount = $("#txt_act_amount");
    txt_remarks = $("#txt_remarks");
    txt_itmcode = $("#txt_itmcode");
    txt_hidAmt = $("#txt_hidAmt");

    txt_startdate = $("#txt_startdate");
    txt_enddate = $("#txt_enddate");
    txt_ftm = $("#txt_ftm");

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
    txt_prevcountdate = $("#txt_prevcountdate");
    txt_currdate = $("#txt_currdate");
    txt_nxtcount = $("#txt_nxtcount");
    txt_countrange = $("#txt_countrange");
    txt_countduedate = $("#txt_countduedate");

    spn_statusCount = $("#spn_statusCount");

    txt_overallRemarks = $("#txt_overallRemarks");
    txt_totalamount = $("#txt_totalamount");

    txt_forecastAmt = $("#txt_forecastAmt");
    txt_forecastPcs = $("#txt_forecastPcs");

    txt_forecastPcs0 = $("#txt_forecastPcs0");
    txt_forecastPcs1 = $("#txt_forecastPcs1");
    txt_forecastPcs2 = $("#txt_forecastPcs2");
    txt_forecastPcs3 = $("#txt_forecastPcs3");

    txt_totalamount0 = $("#txt_totalamount0");
    txt_totalamount1 = $("#txt_totalamount1");
    txt_totalamount2 = $("#txt_totalamount2");
    txt_totalamount3 = $("#txt_totalamount3");

    btn_add = $("#btn_add");
    btn_save_draft = $("#btn_save_draft");
    btn_save = $("#btn_save");
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


    btn_edit = $("#btn_edit");
    btn_doneEdit = $("#btn_doneEdit");

    tbl_column_controller = $("#tbl_column_controller");
    master_checkbox = $("#master_checkbox");

    tbl_column_controller.find(".chkbox_column").click(
        function () {
            // ShowHide_Column($(this));
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
    txt_itmdesc.lookupTextField();

    // $(".last_row input[type=text]").forceNumeric();
    $(".last_row input[type=text]").attr("maxlength", "6");


    txt_remarks.removeAttr("maxlength");
    txt_remarks.unbind();

    txt_endnv.decifield();
    txt_ssr.decifield();
    txt_forecastPcs1.decifield();
    txt_forecastPcs2.decifield();
    txt_forecastPcs3.decifield();

    readonlyFields();
    getDetails();

    btn_searchAcctName.hide();
    grp_edit.hide();
    grp_save.hide();
    btn_doneEdit.hide();

    getBrandList();

    btn_edit.click(function () {
        btn_edit.hide();
        btn_doneEdit.show();

        btn_save.hide();
        btn_save_draft.hide();
        btn_cancel.hide();

        disableReadonlyList();

        $("#tbl_scroll_header .cls_input").css("background", "#fff7dd");
        $("#tbl_scroll_header .remarks").css("background", "#ffffff");


        // $('#tbl_details .cls_input').forceNumeric();
        $('#tbl_scroll_header .remarks').unbind();
        $("#tbl_scroll_header .cls_input").css("background", "#fff7dd");

        $("#tbl_scroll_header .cls_input").attr("maxlength", "6");
        $("#tbl_scroll_header .remarks").removeAttr("maxlength");


        $('#tbl_scroll_header tr[clone="true"]').each(
                                function (index, elem) {
                                    var curr_row = $(elem);

                                    curr_row.find("td:nth-child(1)").find("input[type=text]").decifield();
                                    curr_row.find("td:nth-child(4)").find("input[type=text]").decifield();
                                    curr_row.find("td:nth-child(10)").find("input[type=text]").decifield();
                                   // curr_row.find("td:nth-child(12)").find("input[type=text]").decifield();
                                    curr_row.find("td:nth-child(14)").find("input[type=text]").decifield();
                                    //$(curr_row)
                                });

        $('.cls_input').click(function () {
            $(this).select();
        });
    });

    btn_doneEdit.click(function () {
        if (validateTablelist()) {
            btn_edit.show();
            btn_doneEdit.hide();


            btn_save.show();
            btn_save_draft.show();
            btn_cancel.show();

            enableReadonlyList();

            $('.cls_input').unbind();
            $("#tbl_details .cls_input").css("background", "#ededed");
            computeActSellOutAmount();
        }
    });

    btn_searchAcctCode.hide();
    btn_searchAcctName.hide();
    btn_searchWhsIncharge.show();

    //beautification of table using jquery theme
    $("#tbl_details th").addClass("ui-state-default");

    btn_add.click(function () {
        if (isValid()) {
            if (isNotDuplicate(txt_itmcode.attr("value"))) {
                var itemPrice = parseFloat(txt_hidAmt.attr("value"));

                //var line_no = $("#tbl_details").find("tr:gt(0)").length - 3;
                var line_no = $('#tbl_freeze_header').find('tr[clone="true"]').length + 1;
                var actSellOut = parseFloat(0) - parseFloat(undoAddComma(txt_endnv.attr("value")));
                //var forecastAmt0 = parseFloat(txt_forecastPcs0.attr("value")) * itemPrice;
                var forecastAmt1 = parseFloat(undoAddComma(txt_forecastPcs1.attr("value"))) * itemPrice;
               // var forecastAmt2 = parseFloat(undoAddComma(txt_forecastPcs2.attr("value"))) * itemPrice;
               // var forecastAmt3 = parseFloat(undoAddComma(txt_forecastPcs3.attr("value"))) * itemPrice;

                addItem(line_no,
                    txt_brand.attr("value"),
                    txt_prodgroup.attr("value"),
                    txt_itmcode.attr("value"),
                    txt_itmdesc.attr("value"),
                    undoAddComma(txt_ssr.attr("value")),
                    "0",
                    undoAddComma(txt_endnv.attr("value")),
                    "",
                    "", // actSellOut,
                    "",
                    txt_remarks.attr("value"),
                    '',
                    undoAddComma(txt_forecastPcs0.attr("value")),
                    "",
                    undoAddComma(txt_forecastPcs1.attr("value")),
                    forecastAmt1,
                    "",//undoAddComma(txt_forecastPcs2.attr("value")),
                    "",//forecastAmt2,
                    "",//undoAddComma(txt_forecastPcs3.attr("value")),
                    "",//forecastAmt3,
                    txt_hidAmt.attr("value"),
                    txt_netSellIn.attr("value"),
                    false);
                clearField();
            }
        }
    });

    btn_save.click(function () {
        if (confirm('Are you sure you want to save?')) {
            if (verifyInput()) {
                DisplayPreloader();
                saveInventoryCount("SAVE");
            }
        }
    });

    btn_cancel.click(function () {
        if (confirm('Are you sure you want to cancel?')) {
            location.reload();
        }
    });

    btn_save_draft.click(function () {
        if (confirm("Note: Saving as draft will not allow you to change the Account Code/Name. Do you want to Proceed?")) {
            if (validateData()) {
                DisplayPreloader();
                saveInventoryCount("SAVE_AS_DRAFT");
            }
        }
    });

    getInventoryDetails();
});


function ShowHide_Column_Scrollhdr(obj) {
    var colNo_ = 0;
    var colNo = obj.attr("name").replace("col", "");
    var show = obj.is(":checked");
    var selector = "#tbl_scroll_header tr[class=\"tr_total\"] td:nth-child(" + colNo + ")," +
                   "#tbl_scroll_header tr[clone=\"true\"] td:nth-child(" + colNo + ")," +
                   "#tbl_scroll_header tr[class=\"last_row\"] td:nth-child(" + colNo + "),#tbl_scroll_header tr th:nth-child(" + colNo + ")";

    if (colNo == 6) {
        colNo_ = parseInt(colNo) + 1;
        selector = "#tbl_scroll_header tr td:nth-child(" + colNo + ")," +
                   "#tbl_scroll_header tr td:nth-child(" + colNo_ + ")," +
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")";
    }
    else if (colNo == 7) {
        selector = "#tbl_scroll_header tr td:nth-child(8)," +
                   "#tbl_scroll_header tr td:nth-child(9)," +
                   "#tbl_scroll_header tr th:nth-child(7)";
    }
    else if (colNo == 9) {
        selector = "#tbl_scroll_header tr td:nth-child(12)," +
                   "#tbl_scroll_header tr td:nth-child(12)," +
                   "#tbl_scroll_header tr th:nth-child(" + colNo + ")";
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

function getInventoryDetails() {
    var new_obj = { inventoryCountId: inventorycount_id };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Inventory/getInventoryDetails",
        success: function (res) {
           if (!res.iserror) {
                //displayValues(res.data.inventoryHdr, res.data.whsInchargeDtl, res.data.custOutletDtl, res.data.inventoryDtl, res.data.permission);
                displayValues(res.data.inventoryHdr, res.data.permission);

                $("#doc_status").infomessage("<b>" + res.data.docstatus.statusDesc + "</b>", "info");
                ApplyRules(res.data.permission);

                actualCountEndValidDate = FormatDate(res.data.inventoryHdr.actualCountEndValidDate);

               // getItemList1();
                //getNextCountInventory(txt_acctCode.attr("value"), txt_acctName.attr("code"));

                btn_searchWhsIncharge.lookdown(
                { "url": baseUrl + "Inventory/lookUpWhsIncharge", "index_value": "1", "display_rowindex": "7" },
                { TMPLookUpType: txt_acctCode.attr("value") }, function (res) {
                    return res;
                },
                function (res, all) {
                    if (all[1] != "None") {
                        bool_isNewWhsIncharge = false;

                        txt_whsId.attr("value", all[1]);
                        txt_whsFirstName.attr("value", all[2]);
                        txt_whsMiddleName.attr("value", all[3]);
                        txt_whsLastName.attr("value", all[4]);
                        txt_whscontactno.attr("value", all[5]);

                        $(".cellWhsName input[type=text]").addClass("readonly");
                        $(".cellWhsName input[type=text]").attr("readonly", true);
                    }
                });

                unblockUI();
            } else {
                /* alert(res.message); */if (res.message == "Session Expired") window.parent.ShowLogin(); else alert(res.message);
                unblockUI();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
        }
    });
}

function getItemList1() {
    $.ajax({
        type: 'GET',
        url: baseUrl + "Inventory/lookUpItemCode1",
        data: { prevCountDate: txt_prevcountdate.attr("value"),
            actualCountDate: txt_currdate.attr("value"),
            acctCode: txt_acctCode.attr("value")
        },
        success: function (res) {
            listOfItems = res;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            //HidePreloader();
            unblockUI();
        }
    });
}

function ApplyRules(permission) {
    if (permission.AllowEditSO == true) {
        grp_edit.show();
        getItemList1();
        if (permission.AllowApprove == true) {
            grp_save.show();
        }
    }
}

function maxLengthCheck(object) {
    //  var max_length = forecast1Field.getAttribute('maxLength'); 
    if (object.value.length > object.maxLength)
        object.value = object.value.slice(0, object.maxLength)
}

function displayValues(inventoryHdr, permission) {
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


    txt_startdate.attr("value", FormatDate(inventoryHdr.start_date));
    txt_enddate.attr("value", FormatDate(inventoryHdr.end_date));


    txt_ftm.attr("value", inventoryHdr.forthemonth);

    spn_statusCount.text(inventoryHdr.inventoryCountStatus);

    if (permission.AllowEditSO) {
        $(inventoryHdr.inventorycount_list).each(function (index, elem) {
            // addItem(elem.lineId, elem.brand, elem.prodGrp, elem.itemCode, elem.itemDesc, elem.ssr, elem.begNvPcs, '', elem.endNvPcs, elem.actualSellOutPcs, elem.actualSellOutAmt, elem.remarks, '', '', '', '', elem.itemPrice);
            addItem(elem.lineId,
                         elem.brand,
                         elem.prodGrp,
                         elem.itemCode,
                         elem.itemDesc,
                         elem.ssr,
                         elem.begNvPcs,
            //elem.sellIn,
                         elem.endNvPcs,
                         elem.netOnHand,
                         elem.actualSellOutPcs,
                         elem.actualSellOutAmt,
                         elem.remarks,
                         "",
                         elem.forecastFTMpcs0,
                         elem.forecastFTMamt0,
                         elem.forecastFTMpcs1,
                         elem.forecastFTMamt1,
                         "",//elem.forecastFTMpcs2,
                         "",//elem.forecastFTMamt2,
                         "",//elem.forecastFTMpcs3,
                         "",//elem.forecastFTMamt3,
                         elem.itemPrice,
                         elem.sellIn,
                         true);

        });


    }
    else {
        $(inventoryHdr.inventorycount_list).each(function (index, elem) {
            //addItem_ViewOnly(elem.lineId, elem.brand, elem.prodGrp, elem.itemCode, elem.itemDesc, elem.ssr, elem.begNvPcs, '', elem.endNvPcs, elem.actualSellOutPcs, elem.actualSellOutAmt, elem.remarks, '', '', '', '', elem.itemPrice);
            addItem_ViewOnly(elem.lineId,
                         elem.brand,
                         elem.prodGrp,
                         elem.itemCode,
                         elem.itemDesc,
                         elem.ssr,
                         elem.begNvPcs,
            //elem.sellIn,
                         elem.endNvPcs,
                         elem.netOnHand,
                         elem.actualSellOutPcs,
                         elem.actualSellOutAmt,
                         elem.remarks,
                         "",
                         elem.forecastFTMpcs0,
                         elem.forecastFTMamt0,
                         elem.forecastFTMpcs1,
                         elem.forecastFTMamt1,
                         "",//elem.forecastFTMpcs2,
                         "",//elem.forecastFTMamt2,
                         "",//elem.forecastFTMpcs3,
                         "",//elem.forecastFTMamt3,
                         elem.itemPrice,
                         elem.sellIn,
                         true);
        });


        txt_totalamount.attr("value", numberWithCommas(inventoryHdr.totalAmount));
        txt_totalamount0.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt0));
        txt_totalamount1.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt1));
       // txt_totalamount2.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt2));
       // txt_totalamount3.attr("value", numberWithCommas(inventoryHdr.totalForecastAmt3));
    }
   // $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
   // $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);

    $("#tbl_freeze_header tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_scroll_header tr[clone=true] input[type=text]").css("background-color", "#ededed");

    $("#tbl_freeze_header tr[clone=true] input[type=text]").attr("readonly", true);
    $("#tbl_scroll_header tr[clone=true] input[type=text]").attr("readonly", true);

    txt_totalamount.css("background-color", "#ededed");
}

function getBrandList() {
    $.ajax({
        type: 'GET',
        url: baseUrl + "Inventory/lookUpBrand",
        success: function (res) {
            txt_brand.append(res);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function computeActSellOutAmount() {
    TotalAmount = 0;

    TotalAmount0 = 0;
    TotalAmount1 = 0;
    //TotalAmount2 = 0;
    //TotalAmount3 = 0;

    $('#tbl_scroll_header tr[clone="true"]').each(
    function (index, elem) {
        var curr_row = $(elem);
        var pcs = 0;
        var price = 0;
        var ssr = undoAddComma(curr_row.find("td:nth-child(1)").find("input[type=number]").attr("value"));
        var begNv = undoAddComma(curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"));
        var sellIn = undoAddComma(curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value"));
        var endNv = undoAddComma(curr_row.find("td:nth-child(4)").find("input[type=number]").attr("value"));
        var forecastpcs0 = undoAddComma(curr_row.find("td:nth-child(8)").find("input[type=text]").attr("value"));
        var forecastpcs1 = undoAddComma(curr_row.find("td:nth-child(10)").find("input[type=number]").attr("value"));
        //var forecastpcs2 = undoAddComma(curr_row.find("td:nth-child(12)").find("input[type=number]").attr("value"));
        //var forecastpcs3 = undoAddComma(curr_row.find("td:nth-child(14)").find("input[type=number]").attr("value"));


        begNv = parseFloat(begNv == "" ? 0 : begNv);
        sellIn = parseFloat(sellIn == "" ? 0 : sellIn);
        endNv = parseFloat(endNv == "" ? 0 : endNv);

        pcs = (begNv + sellIn) - endNv;
        //price = undoAddComma(curr_row.find("td:nth-child(18)").find("input[type=text]").attr("value"));
        price = undoAddComma(curr_row.find("td:nth-child(14)").find("input[type=text]").attr("value"));


        if (pcs < 0)
            curr_row.find("td:nth-child(5)").find("input[type=text]").attr("value", pcs);

        pcs = pcs == "" || pcs < 0 ? 0 : pcs;
        price = price == "" ? 0 : price;

        forecastpcs0 = parseFloat(forecastpcs0 == "" ? 0 : forecastpcs0);
        forecastpcs1 = parseFloat(forecastpcs1 == "" ? 0 : forecastpcs1);
        //forecastpcs2 = parseFloat(forecastpcs2 == "" ? 0 : forecastpcs2);

        //        var totalActualSellout = parseFloat(pcs) * parseFloat(price);
        //        var totalForecastAmt0 = parseFloat(forecastpcs0) * parseFloat(price);
        //        var totalForecastAmt1 = parseFloat(forecastpcs1) * parseFloat(price);
        //        var totalForecastAmt2 = parseFloat(forecastpcs2) * parseFloat(price);
        //        var totalForecastAmt3 = parseFloat(forecastpcs3) * parseFloat(price);

        var totalActualSellout = getProduct(pcs, price);
        var totalForecastAmt0 = getProduct(forecastpcs0, price);
        var totalForecastAmt1 = getProduct(forecastpcs1, price);
        //var totalForecastAmt2 = getProduct(forecastpcs2, price);
        //var totalForecastAmt3 = getProduct(forecastpcs3, price);


        //        TotalAmount = parseFloat(TotalAmount) + parseFloat(totalActualSellout);
        //        TotalAmount0 = parseFloat(TotalAmount0) + parseFloat(totalForecastAmt0);
        //        TotalAmount1 = parseFloat(TotalAmount1) + parseFloat(totalForecastAmt1);
        //        TotalAmount2 = parseFloat(TotalAmount2) + parseFloat(totalForecastAmt2);
        //        TotalAmount3 = parseFloat(TotalAmount3) + parseFloat(totalForecastAmt3);

        TotalAmount = getSum(TotalAmount, totalActualSellout);
        TotalAmount0 = getSum(TotalAmount0, totalForecastAmt0); //parseFloat(TotalAmount0) + parseFloat(totalForecastAmt0);
        TotalAmount1 = getSum(TotalAmount1, totalForecastAmt1); //parseFloat(TotalAmount1) + parseFloat(totalForecastAmt1);
        //TotalAmount2 = getSum(TotalAmount2, totalForecastAmt2);
        //TotalAmount3 = getSum(TotalAmount3, totalForecastAmt3);


        curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value", numberWithCommas(ssr));
        curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value", numberWithCommas(begNv));
        curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value", numberWithCommas(sellIn));
        curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value", numberWithCommas(endNv));
        curr_row.find("td:nth-child(8)").find("input[type=text]").attr("value", numberWithCommas(forecastpcs0));
        curr_row.find("td:nth-child(10)").find("input[type=text]").attr("value", numberWithCommas(forecastpcs1));
        //curr_row.find("td:nth-child(12)").find("input[type=text]").attr("value", numberWithCommas(forecastpcs2));
        //curr_row.find("td:nth-child(14)").find("input[type=text]").attr("value", numberWithCommas(forecastpcs3));

        curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value", numberWithCommas(pcs));
        curr_row.find("td:nth-child(7)").find("input[type=text]").attr("value", numberWithCommas(totalActualSellout));
        curr_row.find("td:nth-child(9)").find("input[type=text]").attr("value", numberWithCommas(totalForecastAmt0));
        curr_row.find("td:nth-child(11)").find("input[type=text]").attr("value", numberWithCommas(totalForecastAmt1));
        //curr_row.find("td:nth-child(13)").find("input[type=text]").attr("value", numberWithCommas(totalForecastAmt2));
        //curr_row.find("td:nth-child(15)").find("input[type=text]").attr("value", numberWithCommas(totalForecastAmt3));

        txt_totalamount.attr("value", numberWithCommas(TotalAmount));

        txt_totalamount0.attr("value", numberWithCommas(TotalAmount0));
        txt_totalamount1.attr("value", numberWithCommas(TotalAmount1));
        //txt_totalamount2.attr("value", numberWithCommas(TotalAmount2));
        //txt_totalamount3.attr("value", numberWithCommas(TotalAmount3));
    });
}

function getSum(x1, x2) {
    return parseFloat(x1 != "" ? x1 : 0) + parseFloat(x2 != "" ? x2 : 0);
}

function getProduct(x1, x2) {
    return parseFloat(x1 != "" ? x1 : 0) * parseFloat(x2 != "" ? x2 : 0);
}

function validateData() {
    var isError = false;
    var message = "Fill up all empty fields..!";

    $('tr[class!=last_row] input[type="text"]').each(function (index, elem) {
        if ($(this).attr("value") == "") {
            $(this).addClass("required");
        }
    });

    if (isError) { alert(message); }

    return !isError;

}

function verifyInput() {
    markRequiredField();
    var message = "";
    var tempmessage = "The following field is required to be filled: \n";
    var Error = false;
    $(".required").each(
            function () {
                if ($(this).attr("value") == "" || $(this).attr("value") == "First Name" || $(this).attr("value") == "Middle Name" || $(this).attr("value") == "Last Name") {
                    tempmessage += "\t" + $(this).attr("id") + "\n";
                    Error = true;
                }
            });

    if (Error) { message += tempmessage; alert(message); }

    return !Error;
}

function markRequiredField() {
    txt_empId.addClass("required");
    txt_acctCode.addClass("required");
    txt_acctName.addClass("required");
    txt_currdate.addClass("required");
    txt_whsFirstName.addClass("required");
    txt_whsMiddleName.addClass("required");
    txt_whsLastName.addClass("required");
    txt_whscontactno.addClass("required");
}

function RemoveMarkRequiredField() {
    $("#txt_owner_maker").removeClass("required");
    $("#txt_owner_cp_no").removeClass("required");
    $("#txt_owner_landline_no").removeClass("required");
    $("#txt_firstName").removeClass("required");
    $("#txt_midName").removeClass("required");
    $("#txt_lastName").removeClass("required");
    $("#txt_cellphone_no").removeClass("required");
    txt_PresentationDate.removeClass("required");
    txt_ProgramPresenter.removeClass("required");
    txt_AccountCode.removeClass("required");

    txt_empId.removeClass("required");
    txt_empName.removeClass("required");
    txt_acctCode.removeClass("required");
    txt_acctName.removeClass("required");
    txt_acctAddress.removeClass("required");
    txt_pareto.removeClass("required");
    txt_area.removeClass("required");
    txt_currdate.removeClass("required");
    txt_whsFirstName.removeClass("required");
    txt_whsMiddleName.removeClass("required");
    txt_whsLastName.removeClass("required");
    txt_whscontactno.removeClass("required");
    txt_totalamount.removeClass("required");
}

function validateTablelist() {
    var isError = false;
    var message = "There are items that dont have value..!";

    $("#tbl_scroll_header tr[clone=true]").each(function (index, elem) {
        var curr_row = $(elem);
        if (curr_row.find("td:nth-child(1)").find("input[type=number]").attr("value") == "") {
            curr_row.find("td:nth-child(1)").find("input[type=number]").css("background", "#fff7dd");
            isError = true;
        }
        if (curr_row.find("td:nth-child(4)").find("input[type=number]").attr("value") == "") {
            curr_row.find("td:nth-child(4)").find("input[type=number]").css("background", "#fff7dd");
            isError = true;
        }
        if (curr_row.find("td:nth-child(10)").find("input[type=number]").attr("value") == "") {
            curr_row.find("td:nth-child(10)").find("input[type=number]").css("background", "#fff7dd");
            isError = true;
        }
        if (curr_row.find("td:nth-child(12)").find("input[type=number]").attr("value") == "") {
            curr_row.find("td:nth-child(13)").find("input[type=number]").css("background", "#fff7dd");
            isError = true;
        }
        if (curr_row.find("td:nth-child(15)").find("input[type=number]").attr("value") == "") {
            curr_row.find("td:nth-child(15)").find("input[type=number]").css("background", "#fff7dd");
            isError = true;
        }


    });


    if (isError) { alert(message); }

    return !isError;
}


function saveInventoryCount(act_type) {

    var inventorycount_list = new Array();
    var bool_hasRow = false;

//    $('#tbl_details tr[clone="true"]').each(
//        function (index, elem) {
//            var curr_row = $(elem);

//            inventorycount_list.push({
//                lineId: curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value"),
//                itemCode: curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value"),
//                ssr: curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value"),
//                begNvPcs: curr_row.find("td:nth-child(7)").find("input[type=text]").attr("value"),
//                sellIn: curr_row.find("td:nth-child(8)").find("input[type=text]").attr("value"),
//                endNvPcs: curr_row.find("td:nth-child(9)").find("input[type=text]").attr("value"),
//                netOnHand: curr_row.find("td:nth-child(10)").find("input[type=text]").attr("value"),
//                actualSellOutPcs: curr_row.find("td:nth-child(11)").find("input[type=text]").attr("value"),
//                actualSellOutAmt: undoAddComma(curr_row.find("td:nth-child(12)").find("input[type=text]").attr("value")),
//                forecastFTMpcs0: curr_row.find("td:nth-child(13)").find("input[type=text]").attr("value"),
//                forecastFTMamt0: undoAddComma(curr_row.find("td:nth-child(14)").find("input[type=text]").attr("value")),
//                forecastFTMpcs1: curr_row.find("td:nth-child(15)").find("input[type=text]").attr("value"),
//                forecastFTMamt1: undoAddComma(curr_row.find("td:nth-child(16)").find("input[type=text]").attr("value")),
//                forecastFTMpcs2: curr_row.find("td:nth-child(17)").find("input[type=text]").attr("value"),
//                forecastFTMamt2: undoAddComma(curr_row.find("td:nth-child(18)").find("input[type=text]").attr("value")),
//                forecastFTMpcs3: curr_row.find("td:nth-child(19)").find("input[type=text]").attr("value"),
//                forecastFTMamt3: undoAddComma(curr_row.find("td:nth-child(20)").find("input[type=text]").attr("value")),
//                remarks: curr_row.find("td:nth-child(21)").find("input[type=text]").attr("value")
//            });

//            bool_hasRow = true;
    //        });
    $('#tbl_scroll_header tr[clone="true"]').each(
            function (index, elem) {


                var cls_itmCode = $(this).attr("class");

                var curr_row = $(elem);
                var curr_row_freeze_header = $("#tbl_freeze_header").find("tr[class=\"" + cls_itmCode + "\"]");

                inventorycount_list.push({
                    lineId: curr_row_freeze_header.find("td:nth-child(1)").find("input[type=text]").attr("value"),
                    itemCode: curr_row_freeze_header.find("td:nth-child(4)").find("input[type=text]").attr("value"),
                    ssr: curr_row.find("td:nth-child(1)").find("input[type=number]").attr("value"),
                    begNvPcs: curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"),
                    sellIn: curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value"),
                    endNvPcs: curr_row.find("td:nth-child(4)").find("input[type=number]").attr("value"),
                    netOnHand: undoAddComma(curr_row.find("td:nth-child(5)").find("input[type=text]").attr("value")),
                    actualSellOutPcs: curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value"),
                    actualSellOutAmt: undoAddComma(curr_row.find("td:nth-child(7)").find("input[type=text]").attr("value")),
                    forecastFTMpcs0: curr_row.find("td:nth-child(8)").find("input[type=text]").attr("value"),
                    forecastFTMamt0: undoAddComma(curr_row.find("td:nth-child(9)").find("input[type=text]").attr("value")),
                    forecastFTMpcs1: curr_row.find("td:nth-child(10)").find("input[type=number]").attr("value"),
                    forecastFTMamt1: undoAddComma(curr_row.find("td:nth-child(11)").find("input[type=text]").attr("value")),
                    forecastFTMpcs2: "",//curr_row.find("td:nth-child(12)").find("input[type=number]").attr("value"),
                    forecastFTMamt2: "",//undoAddComma(curr_row.find("td:nth-child(13)").find("input[type=text]").attr("value")),
                    forecastFTMpcs3: "",//curr_row.find("td:nth-child(14)").find("input[type=number]").attr("value"),
                    forecastFTMamt3: "",//undoAddComma(curr_row.find("td:nth-child(15)").find("input[type=text]").attr("value")),
                    remarks: curr_row.find("td:nth-child(12)").find("input[type=text]").attr("value")
                });

                bool_hasRow = true;
                if (curr_row.find("td:nth-child(4)").find("input[type=number]").attr("value") == ""
                    || curr_row.find("td:nth-child(4)").find("input[type=number]").attr("value") == undefined) {
                    //   alert(curr_row.find("td:nth-child(9)").find("input[type=text]").attr("value"));
                    // return false;
                    bool_hasRow = false;
                }
            });

    if (bool_isNewWhsIncharge == true) {
        var whs_dtl = {
            whsInchargeID: txt_whsId.attr("value"),
            whsInchargeFirstName: txt_whsFirstName.attr("value"),
            whsInchargeMiddleName: txt_whsMiddleName.attr("value"),
            whsInchargeLastName: txt_whsLastName.attr("value"),
            whsInchargeContactNo: txt_whscontactno.attr("value")
        }
    }

    var new_obj = {
        inventoryCountId: inventorycount_id,
        act_type: act_type,
        empId: txt_empId.attr("value"),
        acctCode: txt_acctCode.attr("value"),
        prevCountDate: txt_prevcountdate.attr("value"),
        actualCountDate: txt_currdate.attr("value"),
        nextCountDate: txt_nxtcount.attr("value"),
        countRange: txt_countrange.attr("value"),
        remarks: txt_overallRemarks.attr("value"),
        totalAmount: undoAddComma(txt_totalamount.attr("value")),
        whsInchargeID: bool_isNewWhsIncharge == true ? null : txt_whsId.attr("value"),
        newWhsIncharge: bool_isNewWhsIncharge,
        inventorycount_list: inventorycount_list,
        whs_details: whs_dtl,
        inventoryCountStatus: spn_statusCount.attr("value"),
        actualCountEndValidDate: actualCountEndValidDate
    }
    //return false;
    if (bool_hasRow) {
        //UPDATE
        $.ajax({
            dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
            type: "POST", url: baseUrl + "Inventory/saveInventoryCount",
            success: function (res) {
                if (!res.iserror) {
                    alert("Success");
                    window.location = baseUrl + "Inventory/InventoryCountList";
                } else {
                    // HidePreloader();
                    unblockUI();
                    /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
                //HidePreloader();
                unblockUI();
            }
        });
    }
}


function numberWithCommas(x) {
    x = Math.round(x == undefined ? 0 : x);
    return x == undefined ? "0" : x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


function validateInput(forecast1Field) {
    var currentValue = undoAddComma(forecast1Field.code);
    if (forecast1Field.value != "") {
        var val = undoAddComma(forecast1Field.value);
        try {
            val = parseFloat(val);
        }
        catch (err) { }
        if (val < currentValue) {
            alert("input more than the current quantity(" + currentValue + ")");
            forecast1Field.value = currentValue;
            return false;
        }
    }
    return true;
}

function addItem(lineNo, brand, prodGrp, itmCode, itmDesc, ssr, begNv,
                 endNv, netOnHand, actSelloutPcs, actSelloutAmt, remarks, isReadonly,
                 forecastFTMpcs0, forecastFTMamt0, forecastFTMpcs1, forecastFTMamt1,
                  forecastFTMpcs2, forecastFTMamt2, forecastFTMpcs3, forecastFTMamt3,
                   itemPrice, netSellInPcs, isLoadedNetOnHand) {

    var selloutPcs = 0;
    var totalSellOutAmt = 0;
    netSellInPcs = netSellInPcs == "" ? 0 : netSellInPcs;
    if (itemPrice != undefined && itemPrice != "" && itemPrice != null) {

        selloutPcs = (parseFloat(begNv == "" ? 0 : undoAddComma(begNv)) + parseFloat(netSellInPcs == "" ? 0 : netSellInPcs)) - parseFloat(endNv == "" ? 0 : undoAddComma(endNv));

        //totalSellOutAmt = parseFloat(itemPrice) * parseFloat(actSelloutPcs == "" ? selloutPcs : actSelloutPcs);
        totalSellOutAmt = getProduct(itemPrice, actSelloutPcs == "" ? selloutPcs : actSelloutPcs);
        actSelloutAmt = totalSellOutAmt;

    }

    //if (!isLoadedNetOnHand) {
        if (selloutPcs <= 0) {
            netOnHand = selloutPcs;

            totalSellOutAmt = totalSellOutAmt - netOnHand;

            selloutPcs = 0;
            actSelloutAmt = 0;
        }
    //}

    forecastFTMamt0 = forecastFTMamt0 == "" ? 0 : forecastFTMamt0;
    forecastFTMpcs0 = forecastFTMpcs0 == "" ? 0 : undoAddComma(forecastFTMpcs0);

    forecastFTMamt1 = forecastFTMamt1 == "" ? 0 : forecastFTMamt1;
    forecastFTMpcs1 = forecastFTMpcs1 == "" ? 0 : undoAddComma(forecastFTMpcs1);

    forecastFTMamt2 = forecastFTMamt2 == "" ? 0 : forecastFTMamt2;
    forecastFTMpcs2 = forecastFTMpcs2 == "" ? 0 : undoAddComma(forecastFTMpcs2);

    forecastFTMamt3 = forecastFTMamt3 == undefined ? 0 : forecastFTMamt3;
    forecastFTMpcs3 = forecastFTMpcs3 == undefined ? 0 : undoAddComma(forecastFTMpcs3);

    $("#tbl_freeze_header .last_row").before('<tr height="30px" clone="true" class="' + itmCode + '">' +
        '<td><input type="text" value="' + lineNo + '" style="width:40px; text-align:center; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:50px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px; background-color:#ededed;" /></td>' +
        '</tr>');

    $("#tbl_scroll_header .last_row").before('<tr height="30px" class="' + itmCode + '" clone="true">' +
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
        //'<td><input type="number" class="cls_input" value="' + forecastFTMpcs2 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt2) + '" style="width:80px; background-color:#ededed; text-align:right;" /></td>' +
        //'<td><input type="number" class="cls_input" value="' + forecastFTMpcs3 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt3) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        '<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '<td><img class="btn" id="btn_delete" src="' + baseUrl + 'Images/delete.png" /></td>' +
        '<td><input type="text" value="' + itemPrice + '" style="display:none" /></td>' +
        '</tr>').prev().find("#btn_delete").click(function () {
            var row = $(this).parent().parent();
            var cls_itmCode = $(this).parent().parent().attr("class");

            $("#tbl_freeze_header").find("tr[class=\"" + cls_itmCode + "\"]").remove();

            row.remove();
            var cur_totalamount = undoAddComma(row.find("td:nth-child(7)").find("input[type=text]").attr("value"));

            var cur_totalamount0 = undoAddComma(row.find("td:nth-child(9)").find("input[type=text]").attr("value"));
            var cur_totalamount1 = undoAddComma(row.find("td:nth-child(11)").find("input[type=text]").attr("value"));
            //var cur_totalamount2 = undoAddComma(row.find("td:nth-child(13)").find("input[type=text]").attr("value"));
            //var cur_totalamount3 = undoAddComma(row.find("td:nth-child(15)").find("input[type=text]").attr("value"));
            
            TotalAmount = TotalAmount - parseFloat(cur_totalamount);

            TotalAmount0 = TotalAmount0 - parseFloat(cur_totalamount0);
            TotalAmount1 = TotalAmount1 - parseFloat(cur_totalamount1);
            //TotalAmount2 = TotalAmount2 - parseFloat(cur_totalamount2);
            //TotalAmount3 = TotalAmount3 - parseFloat(cur_totalamount3);

            txt_totalamount.attr("value", numberWithCommas(TotalAmount));

            txt_totalamount0.attr("value", numberWithCommas(TotalAmount0));
            txt_totalamount1.attr("value", numberWithCommas(TotalAmount1));
            //txt_totalamount2.attr("value", numberWithCommas(TotalAmount2));
            //txt_totalamount3.attr("value", numberWithCommas(TotalAmount3));

            //repopulate line no
            recountLineNo();
        });

        $("#tbl_scroll_header tr[clone=true] input[type=text]").css("background-color", "#ededed");
        $("#tbl_scroll_header tr[clone=true] input[type=number]").css("background-color", "#ededed");
   // $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);

    $("#tbl_freeze_header tr[clone=true] input").attr("readonly", true);
    $("#tbl_scroll_header tr[clone=true] input").attr("readonly", true);

    TotalAmount = getSum(TotalAmount, undoAddComma(actSelloutAmt));

    TotalAmount0 = getSum(TotalAmount0, undoAddComma(forecastFTMamt0));
    TotalAmount1 = getSum(TotalAmount1, undoAddComma(forecastFTMamt1));
    //TotalAmount2 = getSum(TotalAmount2, undoAddComma(forecastFTMamt2));
    //TotalAmount3 = getSum(TotalAmount3, undoAddComma(forecastFTMamt3));

    txt_totalamount.attr("value", numberWithCommas(TotalAmount));

    txt_totalamount0.attr("value", numberWithCommas(TotalAmount0));
    txt_totalamount1.attr("value", numberWithCommas(TotalAmount1));
    //txt_totalamount2.attr("value", numberWithCommas(TotalAmount2));
    //txt_totalamount3.attr("value", numberWithCommas(TotalAmount3));

    $(".chkbox_column").each(function () {
        if (!$(this).is(":checked")) {
            //ShowHide_Column($(this));
            if ($(this).attr("tbltype") == "freezehdr")
                ShowHide_Column_Freezehdr($(this));
            else
                ShowHide_Column_Scrollhdr($(this));
        }
    });

}

function addItem_ViewOnly(lineNo, brand, prodGrp, itmCode, itmDesc, ssr, begNv,
                 endNv,netOnHand, actSelloutPcs, actSelloutAmt, remarks, isReadonly,
                 forecastFTMpcs0, forecastFTMamt0, forecastFTMpcs1, forecastFTMamt1,
                  forecastFTMpcs2, forecastFTMamt2, forecastFTMpcs3, forecastFTMamt3,
                   itemPrice, netSellInPcs) {

    $("#tbl_freeze_header .last_row").before('<tr height="30px" clone="true" class="' + itmCode + '">' +
        '<td><input type="text" value="' + lineNo + '" style="width:40px; text-align:center; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:50px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px; background-color:#ededed;" /></td>' +
        '</tr>');

    $("#tbl_scroll_header .last_row").before('<tr height="30px" class="' + itmCode + '" clone="true">' +
        '<td><input type="number" class="cls_input" value="' + ssr + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:80px;  text-align:center;" /></td>' +
        '<td><input type="text" value="' + begNv + '" style="width:60px; background-color:#ededed; text-align:center;" /></td>' +
        '<td><input type="text" value="' + netSellInPcs + '" style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="number" class="cls_input" value="' + endNv + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:80px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + netOnHand + '" style="width:60px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(actSelloutPcs) + '" style="width:50px; background-color:#ededed;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(actSelloutAmt) + '" style="width:80px; background-color:#ededed; text-align:right"/></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMpcs0) + '" style="width:70px; background-color:#ededed; text-align:center" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt0) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        '<td><input type="number" class="cls_input forecast1" onblur="validateInput(this);"  value="' + forecastFTMpcs1 + '" oninput="maxLengthCheck(this)" min="' + forecastFTMpcs1 + '" maxLength="6"  code="' + forecastFTMpcs1 + '" style="width:70px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt1) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        //'<td><input type="number" class="cls_input" value="' + forecastFTMpcs2 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt2) + '" style="width:80px; background-color:#ededed; text-align:right;" /></td>' +
        //'<td><input type="number" class="cls_input" value="' + forecastFTMpcs3 + '" oninput="maxLengthCheck(this)" maxLength="6" style="width:70px; text-align:center;" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt3) + '" style="width:80px; background-color:#ededed;  text-align:right;" /></td>' +
        '<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '<td><input type="text" value="' + itemPrice + '" style="display:none" /></td>' +
        '</tr>');

//   $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
    //    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);

    

    $("#tbl_freeze_header input[type=text]").css("background-color", "#ededed");
    $("#tbl_scroll_header input[type=number]").css("background-color", "#ededed");
    $("#tbl_freeze_header input[type=number]").attr("readonly", true);
    $("#tbl_scroll_header input[type=number]").attr("readonly", true);


    //    TotalAmount = TotalAmount + actSelloutAmt;

    //    txt_totalamount.attr("value", TotalAmount);

    btn_add.hide();


    $(".chkbox_column").each(function () {
        if (!$(this).is(":checked")) {
            //ShowHide_Column($(this));
            if ($(this).attr("tbltype") == "freezehdr")
                ShowHide_Column_Freezehdr($(this));
            else
                ShowHide_Column_Scrollhdr($(this));
        }
    });
}

function addItem_ViewOnly1(lineNo, brand, prodGrp, itmCode, itmDesc, ssr, begNv,
                 endNv, actSelloutPcs, actSelloutAmt, remarks, isReadonly,
                 forecastFTMpcs0, forecastFTMamt0, forecastFTMpcs1, forecastFTMamt1,
                  forecastFTMpcs2, forecastFTMamt2, forecastFTMpcs3, forecastFTMamt3,
                   itemPrice, netSellInPcs) {

    $("#tbl_details .last_row").before('<tr clone="true">' +
        '<td><input type="text" value="' + lineNo + '" style="width:50px; text-align:center;" /></td>' +
        '<td><input type="text" value="' + brand + '"  style="width:60px;" /></td>' +
        '<td><input type="text" value="' + prodGrp + '" style="width:70px;" /></td>' +
        '<td><input type="text" value="' + itmCode + '" style="width:120px;" /></td>' +
        '<td><input type="text" value="' + itmDesc + '" style="width:250px;" /></td>' +
        '<td><input type="text" class="cls_input" value="' + numberWithCommas(ssr) + '" style="width:80px;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(begNv) + '" style="width:60px;" /></td>' +
        '<td><input type="text" value="' + netSellInPcs + '" style="width:60px;" /></td>' +
        '<td><input type="text" class="cls_input" value="' + numberWithCommas(endNv) + '" style="width:60px;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(actSelloutPcs) + '" style="width:50px;" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(actSelloutAmt) + '" style="width:80px; text-align:right"/></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMpcs0) + '" style="width:50px;text-align:center" /></td>' +
        '<td><input type="text" value="' + numberWithCommas(forecastFTMamt0) + '" style="width:80px; text-align:right;" /></td>' +
        '<td><input type="text" class="cls_input" onblur="validateInput(this);"  code="' + forecastFTMpcs1 + '" value="' + forecastFTMpcs1 + '" style="width:50px;text-align:center" /></td>' +
        '<td><input type="text" value="' +numberWithCommas( forecastFTMamt1) + '" style="width:80px; text-align:right;" /></td>' +
        '<td><input type="text" class="cls_input" value="' + forecastFTMpcs2 + '" style="width:50px;text-align:center" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt2 )+ '" style="width:80px; text-align:right;" /></td>' +
        //'<td><input type="text" class="cls_input" value="' + forecastFTMpcs3 + '" style="width:50px;text-align:center" /></td>' +
        //'<td><input type="text" value="' + numberWithCommas(forecastFTMamt3) + '" style="width:80px; text-align:right;" /></td>' +
        //'<td><input type="text" class="cls_input remarks" value="' + remarks + '" /></td>' +
        '<td><input type="text" value="' + itemPrice + '" style="display:none" /></td>' +
        '</tr>');


//    $("#tbl_details tr[clone=true] input[type=text]").css("background-color", "#ededed");
//    $("#tbl_details tr[clone=true] input[type=text]").attr("readonly", true);

    $("#tbl_freeze_header tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_scroll_header tr[clone=true] input[type=text]").css("background-color", "#ededed");
    $("#tbl_freeze_header tr[clone=true] input[type=text]").attr("readonly", true);
    $("#tbl_scroll_header tr[clone=true] input[type=text]").attr("readonly", true);

//    TotalAmount = TotalAmount + actSelloutAmt;

//    txt_totalamount.attr("value", TotalAmount);

    btn_add.hide();

}

function isValid() {
    var message = "fill up all empty fields.";
    var isError = false;

    if (txt_brand.attr("value") == "" || txt_prodgroup.attr("value") == "" ||
         txt_forecastPcs1.attr("value") == "" || txt_forecastPcs2.attr("value") == "" ||
         txt_forecastPcs3.attr("value") == "" || txt_ssr.attr("value") == "" ||
         txt_itmcode.attr("value") == "" || txt_endnv.attr("value") == "") {
        isError = true;
    }


    if (isError) { alert(message); }

    return !isError;
}

function clearField() {
    txt_brand.removeAttr("value");
    txt_prodgroup.removeAttr("value");
    txt_itmcode.removeAttr("value");
    txt_itmdesc.removeAttr("value");
    txt_ssr.removeAttr("value");
    txt_endnv.removeAttr("value");
    txt_act_selloutpcs.removeAttr("value");
    txt_remarks.removeAttr("value");
    txt_netSellIn.removeAttr("value");

    txt_forecastPcs0.removeAttr("value");
    txt_forecastPcs1.removeAttr("value");
    txt_forecastPcs2.removeAttr("value");
    txt_forecastPcs3.removeAttr("value");


}

//function recountLineNo() {
//    var line_doc_no = $('#tbl_details').find("tr:gt(0)").length - 1;
//    var lineno = 0;
//    if (line_doc_no > 0)
//        $($('#tbl_details').find("tr:gt(0)")).each(
//        function (index, elem) {
//            var curr_row = $(elem);

//            curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value", lineno);

//            lineno = lineno + 1;

//            if ((line_doc_no - 1) == index) return false;
//        }
//    );
//}

function recountLineNo() {
//    // var line_doc_no = $('#tbl_details').find("tr:gt(0)").length - 1;

//    var line_doc_no = $('#tbl_details tr[clone="true"]').length;
//    var lineno = 1;
//    if (line_doc_no > 0)
//    //$('#tbl_details').find("tr:gt(0)").each(
//        $('#tbl_details tr[clone="true"]').each(
//        function (index, elem) {
//            var curr_row = $(elem);

//            curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value", lineno);

//            lineno = lineno + 1;

//            if ((line_doc_no - 1) == index) return false;
//        }
    //    );



    var line_doc_no = $('#tbl_freeze_header tr[clone="true"]').length;
    var lineno = 1;
    if (line_doc_no > 0)
    //$('#tbl_details').find("tr:gt(0)").each(
        $('#tbl_freeze_header tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value", lineno);

            lineno = lineno + 1;

            if ((line_doc_no - 1) == index) return false;
        }
    );
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
    $("tr[class!=last_row] input[type=text]").addClass("readonly");
    txt_itmdesc.removeClass("readonly");
}



function InputTextbox_function() {
    $('.cellWhsName input[type="text"]').click(function () {
        $(this).select();
    });

    $('.cellWhsName input[type="text"]').focusout(function () {
        if ($(this).attr('value') == "") {
            switch ($(this).attr('id')) {
                case "txt_firstName":
                    txt_whsFirstName.attr('value', "First Name");
                    break;
                case "txt_midName":
                    txt_whsMiddleName.attr('value', "Middle Name");
                    break;
                case "txt_lastName":
                    txt_whsLastName.attr('value', "Last Name");
                    break;
            }
        }
    });

    $('.cellWhsName input[name="lastrow"]').click(function () {
        $(this).select();
    });
}

function newWhsIncharge() {
    txt_whsId.attr("value", "system generated");

    $(".cellWhsName input[type=text]").removeClass("readonly");
    $(".cellWhsName input[type=text]").attr("readonly", false);
    $(".cellWhsName input[type=text]").removeAttr("value");

    InputTextbox_function();

    bool_isNewWhsIncharge = true;
}


function getDetails() {
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "Inventory/getDetails",
        success: function (res) {
            if (!res.iserror) {
                txt_countrange.attr("value", res.data.list.DayCycleCount);
                //txt_nxtcount.attr("value", res.data.date);
                txt_empId.attr("value", res.data.empDetails.empIDNo);
                txt_empName.attr("value", res.data.empDetails.empLastName + ", " + res.data.empDetails.empFirstName);

            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


//function disableReadonlyList() {

//    $("#tbl_details .cls_input").attr("readonly", false);
//    $("#tbl_details .cls_input").css("background-color", "#ededed");

//    txt_ssr.attr("readonly", false);
//    txt_endnv.attr("readonly", false);
//    txt_remarks.attr("readonly", false);

//}

function disableReadonlyList() {

    $("#tbl_details .cls_input").attr("readonly", false);
    $("#tbl_details .cls_input").css("background-color", "#ededed");

    //remain itemcode readonly
    txt_begnv.attr("readonly", true);
    txt_itmdesc.attr("readonly", true);

    txt_brand.attr("readonly", false);
    txt_prodgroup.attr("readonly", false);
    txt_ssr.attr("readonly", false);
    txt_endnv.attr("readonly", false);
    txt_act_amount.attr("readonly", false);
    txt_remarks.attr("readonly", false);

    txt_forecastPcs1.attr("readonly", false);
    txt_forecastPcs2.attr("readonly", false);
    txt_forecastPcs3.attr("readonly", false);
}



function enableReadonlyList() {
    $('#tbl_details input').attr("readonly", true);

}

function isNotDuplicate(itemCode) {
    var message = "item is already in the list";
    var Error = false;
    $('#tbl_freeze_header tr[clone="true"]').each(function () {
        if ($(this).find('td:eq(3) input').val() == itemCode) {
            Error = true;
        }
    });

    if (Error) { alert(message); }

    return !Error;
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
    '<select  data-placeholder="Select Actual Count Date.."  id="' + obj_id_to_position + '_value" style="width:' + ($("#" + obj_id_to_position).css("width")) + '; font-family:Arial; size:12px; outline:none;">' +
    '</select>' +
    '</td>' +
    '</tr>' +
    '<tr align="center">' +
    '<td><button onclick="javascript:setValueFromSelect(\'' + obj_id_to_position + '\');" style="cursor:pointer;">Select</button></td>' +
    '</tr>' +
    '</table>' +
    '</div>');


    //$('#' + obj_id_to_position + '_value').append(listOfItems).chosen();

    if (txt_currdate.attr("value") == "")
        $('#' + obj_id_to_position + '_value').chosen();
    else
        if (prev_brand == $('#txt_brand option:selected').attr('value') && prev_brand != null) {
            $('#' + obj_id_to_position + '_value').append(listOfItems).chosen();
        }
        else
            $.ajax({
                type: 'GET',
                url: baseUrl + "Inventory/itemfilterbybrandLookUp",
                data: {
                    Brand: $('#txt_brand option:selected').attr('value'),
                    prevCountDate: txt_prevcountdate.attr("value"),
                    actualCountDate: txt_currdate.attr("value"),
                    acctCode: txt_acctCode.attr("value")
                },
                success: function (res) {
                    listOfItems = res;
                    prev_brand = $('#txt_brand option:selected').attr('value');
                    $('#' + obj_id_to_position + '_value').append(res).chosen();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status); alert(thrownError);
                }
            });



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
    txt_hidAmt.attr("value", amt);
    txt_netSellIn.attr("value", sellin);

    hide_dialog_box();
}
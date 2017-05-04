var txt_soname = null;
var txt_amount = null;

var btn_add = null;
var btn_update = null;
var btn_delete = null;

var btn_save = null;
var btn_cancel = null;

var search_name = null;
var select_year = null;

var date = new Date();

$(document).ready(function () {
    for (i = 0; i < 20; i++) {
        $("#select_year").append('<option value="' + parseInt(parseInt(year) + i) + '">' + parseInt(parseInt(year) + i) + '</option>');
        $("#select_listsalestarget_year").append('<option value="' + parseInt(parseInt(year) + i) + '">' + parseInt(parseInt(year) + i) + '</option>');
    }

    $.ajax({
        type: 'GET', url: baseUrl + "Maintenance/getSalesOfficer",
        success: function (res) {
            txt_soname.append(res).chosen();
            $("#select_listsalestarget_so").append(res).chosen();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

});

$(function () {

    txt_soname = $("#txt_soname");
    txt_amount = $("#txt_amount");

    btn_add = $("#btn_add");
    btn_delete = $("#btn_delete");
    btn_update = $("#btn_update");

    btn_save = $("#btn_save");
    btn_cancel = $("#btn_cancel");

    search_name = $("#search_name");

    select_year = $("#select_year");

    $("#tab_main").tabs();

    txt_amount.forceNumeric();

    txt_amount.autoNumeric();

    //load details
    getSalesTarget();

    //beautification of table using jquery theme
    $("#tbl_details thead th").addClass("ui-state-default");
    $("#tbl_details_delete thead th").addClass("ui-state-default");

    $("#tbl_details tfoot input[type=text]").attr("readonly", true);

    $("#tbl_details tfoot input[type=text]").css("background", "#ededed");

    $("#div_tbl_delete").hide();

    btn_delete.hide();

    $("#select_year").change(function () {
        load_data();
    });

    $("#select_month").change(function () {
        load_data();
    });

    $("#txt_soname").change(function () {
        load_data();
    });

    $("#select_listsalestarget_year").change(function () {
        getSalesTarget();
    });

    $("#select_listsalestarget_month").change(function () {
        getSalesTarget();
    });

    $("#select_listsalestarget_so").change(function () {
        getSalesTarget();
    });

    $("#lnk_upload_salestarget").uploadlink2(
                baseUrl + "Maintenance/getUploadedSalesTarget",
                "txt_FileAttachment",
                "TESTING",
                function (res) {
                    var error_msg = "";
                    var counter = 0;
                    if (res.indexOf("Error:") > -1) {
                        alert(res);
                        return;
                    }

                    $(res).each(function (index, elem) {

                        var flag = false;
                        $('#tbl_details tr[clone="true"]').each(
                            function (index, e) {
                                var curr_row = $(e);

                                var year = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
                                var month = curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value");
                                var name = curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value");

                                if (year == elem.year && month == elem.month && name == elem.empfullname) {
                                    bool_exist = true;
                                    error_msg = "item is already in the list. Use Update button to update the amount.";
                                    counter += 1;
                                    flag = true;
                                    return;
                                }
                            }
                        );

                        if (flag == false) {
                            $("#tbl_details .last_row").before('<tr clone="true">' +
                                '<td><input type="text" style="width:60px" value="' + elem.year + '" readonly="readonly" /></td>' +
                                '<td><input type="text" style="width:80px" value="' + elem.month + '" readonly="readonly" /></td>' +
                                '<td><input type="text" code="' + elem.empid + '" value="' + elem.empfullname + '" readonly="readonly" /></td>' +
                                '<td><input type="text" style="text-align:right" value="' + (elem.prevsalestarget == 0 ? 0 : addCommas(elem.prevsalestarget)) + '" readonly="readonly" /></td>' +
                                '<td><input type="text" style="text-align:right" value="' + addCommas(elem.salestarget) + '" readonly="readonly" /></td>' +
                                '<td><input type="text" value="' + elem.remarks + '" readonly="readonly" /></td>' +
                                '<td><img class="btn" id="btn_delete_img" src="' + baseUrl + 'Images/delete.png"/></td>' +
                                '</tr>').prev().find("#btn_delete_img").click(
                                                                          function () {
                                                                              //remove to current table
                                                                              $(this).parent().parent().remove();
                                                                              $("#div_tbl_delete").hide();

                                                                              var row_count = $("#tbl_details tbody").find("tr").length - 1;
                                                                              if (row_count > 0)
                                                                                  $("#tbl_details tfoot").hide();
                                                                              else
                                                                                  $("#tbl_details tfoot").show();
                                                                          });

                        }
                    });
                    alert((res.length - counter) + " out of " + res.length + " record/s uploaded.");
                    var row_count = $("#tbl_details tbody").find("tr").length - 1;
                    if (row_count > 0)
                        $("#tbl_details tfoot").hide();
                    else
                        $("#tbl_details tfoot").show();
                }
                );

    btn_add.click(function () {
        if ($("#txt_cur_salestarget").attr("value") == "") {
            alert("select SO Name");
            return;
        }
        if (alreadyInRemoveItems()) {
            if (txt_amount.attr("value") != "") {
                addItem($("#txt_soname option:selected").attr("value"), txt_amount.attr("value"));
                clear_addfields();

                var row_count = $("#tbl_details tbody").find("tr").length - 1;
                if (row_count > 0)
                    $("#tbl_details tfoot").hide();
                else
                    $("#tbl_details tfoot").show();
            }
            else
                alert("fill up empty fields..!");
        }
    });

    btn_update.click(function () {
        updateItem();
        clear_addfields();

        var row_count = $("#tbl_details tbody").find("tr").length - 1;
        if (row_count > 0)
            $("#tbl_details tfoot").hide();
        else
            $("#tbl_details tfoot").show();
    });

    btn_delete.click(function () {
        deleteItem();
        clear_addfields();

        var row_count = $("#tbl_details_delete tbody").find("tr").length - 1;
        if (row_count > 0)
        //  $("#tbl_details_delete tfoot").hide();
            $("#div_tbl_delete").show();
        else
            $("#div_tbl_delete").hide();
        // $("#tbl_details_delete tfoot").show();

        var row_count = $("#tbl_details tbody").find("tr").length - 1;
        if (row_count > 0)
            $("#tbl_details tfoot").hide();
        else
            $("#tbl_details tfoot").show();
    });

    btn_save.click(function () {
        UpdateSalesTarget();
    });

    btn_cancel.click(function () {
        if (confirm('Are you sure you want to cancel?')) {
            location.reload();
        }
    });

});

function load_data() {

    $("#txt_status").text("");
    $("#txt_cur_salestarget").removeAttr("value");

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf8',
        data: JSON.stringify(
                {
                    year: $("#select_year option:selected").val(),
                    month: $("#select_month option:selected").val(),
                    empidno: $("#txt_soname option:selected").attr("code")
                }
            ),
        type: 'POST', url: baseUrl + "Maintenance/getSalesTarget",
        success: function (res) {
            $("#txt_status").text(res == null ? "New Sales Target" : "For Update Sales Target");
            $("#txt_cur_salestarget").attr("value", res == null ? "N/A" : addCommas(res));
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr, status); alert(thrownError);
        }
    });
}

function getSalesTarget() {
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            year: $("#select_listsalestarget_year option:selected").val(),
            month: $("#select_listsalestarget_month option:selected").val(),
            empidno: $("#select_listsalestarget_so option:selected").attr("code")
        }),
        type: "POST", url: baseUrl + "Maintenance/getSalesTargetList",
        success: function (res) {
            if (!res.iserror) {
                $("#tbl_listofsalestarget tr[clone=true]").remove();
                $(res.data.list).each(function (index, elem) {
                    //   addItem(elem.Description, elem.Amount);
                    $("#tbl_listofsalestarget .last_row").before('<tr clone="true">' +
                            '<td><input type="text" style="width:70px; background-color:#ededed; text-align:center;"  value="' + elem.year + '" readonly="readonly" /></td>' +
                            '<td><input type="text" style="background-color:#ededed; text-align:center;" value="' + elem.month + '" readonly="readonly" /></td>' +
                            '<td><input type="text" style="background-color:#ededed; width:260px" value="' + elem.empfullname + '" readonly="readonly" /></td>' +
                            '<td><input type="text" style="background-color:#ededed; text-align:right;" value="' + addCommas(elem.salestarget) + '" readonly="readonly" /></td>' +
                            '</tr>');
                });
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function alreadyInRemoveItems() {
    var message = "Error...! description already in the remove list.";
    var isError = false;
    $('#tbl_details_delete tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var desc = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
            var amount = addCommas(curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"));

            if (desc == txt_desc.attr("value")) {
                isError = true;
                return;
            }
        });
        if (isError) { alert(message); }

        return !isError;
}

function UpdateSalesTarget() {

    var salestarget = new Array();
    var item_deleted = new Array();

    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var year = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
            var month = curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value");
            var empid = curr_row.find("td:nth-child(3)").find("input[type=text]").attr("code");
            var empname = curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value");
            var cur_amount = undoAddComma(curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value"));
            var amount = undoAddComma(curr_row.find("td:nth-child(5)").find("input[type=text]").attr("value"));
            var remarks = curr_row.find("td:nth-child(6)").find("input[type=text]").attr("value");

            salestarget.push({
                year: year,
                month: month,
                empid: empid,
                empfullname:empname,
                prevsalestarget: cur_amount,
                salestarget: amount,
                remarks: remarks
            });
        });

        $('#tbl_details_delete tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var desc = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
            var amount = undoAddComma(curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value"));

            item_deleted.push({
                _desc: desc,
                _amount: amount
            });
        });
//    var row_count = $('#tbl_details tbody').find("tr").length;

//        for (var x = 2; x <= row_count; x++) {
//            var cur_row = $('#tbl_details tbody').find("tr:nth-child(" + x + ")");
//            item_changes.push({
//                _desc: $('#tbl_details tbody').find("tr:nth-child(" + x + ")").find("td:nth-child(1)").find("input[type=text]").attr("value"),
//                _amount: undoAddComma($('#tbl_details').find("tr:nth-child(" + x + ")").find("td:nth-child(2)").find("input[type=text]").attr("value")) //removing comma
//            });
//        }

//        var row_count = $('#tbl_details_delete tbody').find("tr").length - 2;
//        for (var x = 2; x <= row_count; x++) {
//            var cur_row = $('#tbl_details_delete tbody').find("tr:nth-child(" + x + ")");
//            item_deleted.push({
//                _desc: $('#tbl_details_delete tbody').find("tr:nth-child(" + x + ")").find("td:nth-child(1)").find("input[type=text]").attr("value"),
//                _amount: undoAddComma($('#tbl_details_delete tbody').find("tr:nth-child(" + x + ")").find("td:nth-child(2)").find("input[type=text]").attr("value")) //removing comma
//            });
//        }

        var new_obj = { page_param: salestarget }

    //UPDATE 
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Maintenance/UpdateSalesTargetMaintenance",
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

function clear_addfields() {
//    txt_soname.removeAttr("value");
    txt_amount.removeAttr("value");
}

function addItem(soname, amount) {
    var bool_exist = false;
    var error_msg = "";

    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var year = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
            var month = curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value");
            var name = curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value");

            if (year == $("#select_year option:selected").attr("value") && month == $("#select_month option:selected").attr("value").toUpperCase() && name == soname) {
                bool_exist = true;
                error_msg = "item is already in the list. Use Update button to update the amount.";
                return;
            }
        }
    );

        if (bool_exist == false) {
            $("#tbl_details .last_row").before('<tr clone="true">' +
                '<td><input type="text" style="width:60px" value="' + $("#select_year option:selected").attr("value") + '" readonly="readonly" /></td>' +
                '<td><input type="text" style="width:80px" value="' + $("#select_month option:selected").attr("value").toUpperCase() + '" readonly="readonly" /></td>' +
                '<td><input type="text" code="' + $("#txt_soname option:selected").attr("code") + '" value="' + soname + '" readonly="readonly" /></td>' +
                '<td><input type="text" style="text-align:right" value="' + ($("#txt_cur_salestarget").attr("value") == "N/A" ? 0 : $("#txt_cur_salestarget").attr("value")) + '" readonly="readonly" /></td>' +
                '<td><input type="text" style="text-align:right" value="' + amount + '" readonly="readonly" /></td>' +
                '<td><input type="text" value="' + $("#txt_status").text() + '" readonly="readonly" /></td>' +
                '<td><img class="btn" id="btn_delete_img" src="' + baseUrl + 'Images/delete.png"/></td>' +
                '</tr>').prev().find("#btn_delete_img").click(
                                                          function () {

                                                              //undo delete
                                                              //                                                              deleteItem(
                                                              //                                                                $(this).parent().parent().find("td:eq(0) input").attr("value"),
                                                              //                                                               undoAddComma($(this).parent().parent().find("td:eq(1) input").attr("value"))
                                                              //                                                              )



                                                              //remove to current table
                                                              $(this).parent().parent().remove();

                                                              //                                                              var row_count = $("#tbl_details_delete tbody").find("tr").length - 1;
                                                              //                                                              if (row_count > 0)
                                                              //                                                                  $("#div_tbl_delete").show();
                                                              //                                                              else
                                                              //                                                                  $("#div_tbl_delete").hide();

                                                              var row_count = $("#tbl_details tbody").find("tr").length - 1;
                                                              if (row_count > 0)
                                                                  $("#tbl_details tfoot").hide();
                                                              else
                                                                  $("#tbl_details tfoot").show();
                                                          });
            
            //look down txt_code
            //_lookUpCode();

            $("#tbl_details tfoot").hide();
        }
        else
            alert(error_msg);

}

function updateItem() {

    if (txt_amount.attr("value") == "") { alert("Enter sales target amount."); return; }
    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var year = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
            var month = curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value");
            var name = curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value");
            var cur_amount = curr_row.find("td:nth-child(4)").find("input[type=text]").attr("value");
            var amount = undoAddComma(curr_row.find("td:nth-child(5)").find("input[type=text]").attr("value"));


            var year1 = $("#txt_soname option:selected").attr("value");
            var month1 = $("#select_year option:selected").attr("value");
            var name1 = $("#select_month option:selected").attr("value");

            if (name == $("#txt_soname option:selected").attr("value") && year == $("#select_year option:selected").attr("value") && month == $("#select_month option:selected").attr("value").toUpperCase()) {

                //change the value
                curr_row.find("td:nth-child(3)").find("input[type=text]").attr("value", $("#txt_soname option:selected").attr("value"));
                curr_row.find("td:nth-child(5)").find("input[type=text]").attr("value", txt_amount.attr("value"));

                //change text box color
                if (name != $("#txt_soname option:selected").attr("value")) { curr_row.find("td:nth-child(3)").find("input[type=text]").addClass("required"); }
                if (amount != addCommas(txt_amount.attr("value"))) { curr_row.find("td:nth-child(5)").find("input[type=text]").addClass("required"); }
                curr_row.find("td:nth-child(5)").find("input[type=text]").focus();
                return;
            }
        }
    );
}

function deleteItem(desc,amt) {
    $('#tbl_details tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);

            var desc_ = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");

            if (desc_ == desc) {

                curr_row.find("td:nth-child(2)").parent().remove();
                $("#tbl_details_delete .last_row").before('<tr clone="true">' +
                    '<td><input type="text" value="' + desc + '" readonly="readonly" /></td>' +
                    '<td><input type="text" value="' + addCommas(amt) + '" readonly="readonly" /></td>' +
                    '<td><img class="btn" id="btn_undodelete" src="' + baseUrl + 'Images/add.png"/></td>' +
                    '</tr>').prev().find("#btn_undodelete").click(
                                                          function () {

                                                              //undo delete
                                                              addItem(
                                                                $(this).parent().parent().find("td:eq(0) input").attr("value"),
                                                                undoAddComma($(this).parent().parent().find("td:eq(1) input").attr("value"))
                                                              )

                                                            

                                                              //remove to delete table
                                                              $(this).parent().parent().remove();

                                                              var row_count = $("#tbl_details_delete tbody").find("tr").length - 1;
                                                              if (row_count > 0)
                                                                  $("#div_tbl_delete").show();
                                                              else
                                                                  $("#div_tbl_delete").hide();

                                                          });


                //look down txt_code
                _lookUpCode();


                return;
            }
        }
    );

}


function addCommas(str) {
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


function _lookUpCode() {
    var data = new Array();

    //LOOK DOWN for OR NUMBER if already exist

    //Store the new Code for lookdown purposes

    $('#tbl_details tr[clone="true"]').each(
                function (index, elem) {
                    var curr_row = $(elem);

                    var desc = curr_row.find("td:nth-child(1)").find("input[type=text]").attr("value");
                    var amount = curr_row.find("td:nth-child(2)").find("input[type=text]").attr("value");

                    data.push({
                        _desc: desc,
                        _amount: undoAddComma(amount)
                    });
                }
            );



    var new_obj = {
        list_code: data
    }

    $("#lookup_code").attr('value', JSON.stringify(new_obj));

    txt_soname.unbind();
    txt_soname.bind();
    txt_soname.lookdown(
                { "url": baseUrl + "Maintenance/lookUpCode", "index_value": "1", "display_rowindex": "1" },
                { page_param: $("#lookup_code").attr('value') },
                function (res) {
                    return res;
                },
                function (res, all) {
                    txt_desc.attr("value", res);
                    txt_amount.attr("value", all[1]);
                });
}

function previousSO() {
    var index = $("#txt_soname").prop("selectedIndex");
    index -= 1;

    $("#txt_soname option").eq(index).attr('selected', 'selected');
    $("#txt_soname").chosen().change();
    $("#txt_soname").trigger("liszt:updated");
}

function nextSO() {
    var index = $("#txt_soname").prop("selectedIndex");
    index += 1;
    $('#txt_soname option').eq(index).attr('selected', 'selected');
    $('#txt_soname').chosen().change();
    $("#txt_soname").trigger("liszt:updated");
}
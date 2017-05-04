var ns4;


var txt_outletId = null;
var txt_outletName = null;


var btn_save = null;
var btn_cancel = null;

var txt_empId = null;
var txt_empName = null;

var listOfItems = null;

$(function () {
    displayOutlets();

    txt_outletId = $("#txt_outletId");
    txt_outletName = $("#txt_outletName");

    txt_empId = $("#txt_empId");
    txt_empName = $("#txt_empName");

    btn_cancel = $("#btn_cancel");

    btn_save = $("#btn_save");

    enableReadonly();

    getItemList1();

    // $("#txt_empId").lookupTextField();

    // btn_save.lookupTextField();

    $("#txt_empId").focus(function () {
        create_dialog_box($(this).attr('id'));
    });

    btn_save.click(function () {
        if (txt_empName.val() != "" && txt_outletName.val() != "")
            updateOutlet();
        else
            alert("Select Account and Sales officer..!");
    });

    btn_cancel.click(function () {
        clearFields();
    });

    //    txt_empId.lookdown(
    //    { "url": baseUrl + "Maintenance/lookUpSalesOfficerEmployee", "index_value": "1", "display_rowindex": "2" },
    //    {}, function (res) { return res; },
    //    function (res, all) {
    //        txt_empId.attr("value", res);
    //        txt_empName.attr("value", all[1]);
    //    });


});


$.fn.lookupTextField = function () {
    $(this).live({
        focus: function () {
            create_dialog_box($(this).attr('id'));
        }
    });
}


function clearFields() {
    txt_outletId.removeAttr("value");
    txt_outletName.removeAttr("value");
    txt_empId.removeAttr("value");
    txt_empName.removeAttr("value");

    txt_empId.css("background", "#ededed");
}

function verifyInput() {
    markRequiredField();
    var message = "";
    var tempmessage = "The following field is required to be filled: \n";
    var Error = false;
    $(".required").each(
            function () {
                if ($(this).attr("value") == "") {
                    tempmessage += "\t" + $(this).attr("title") + "\n";
                    Error = true;
                }
            });

    if (Error) { message += tempmessage; alert(message); }

    return !Error;
}

function getItemList1() {
    $.ajax({
        type: 'GET',
        url: baseUrl + "Maintenance/lookUpSalesOfficerEmployee",
        success: function (res) {
            listOfItems = res;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}


function displayOutlets() {
    blockUI();
   // $.blockUI();

   // DisplayPreloader();
   // HidePreloader();
    $.ajax({
        type: "POST", url: baseUrl + "Maintenance/getOutlets",
        success: function (res) {
            $(res).each(function (index, item) {
                addItemToDisplay(item.ccaNum, item.acctCode, item.acctName, item.acctAddress, item.prevCountDate, item.nextCountDate , item.empName, item.empId);
            });
            //HidePreloader();
            // fnFeaturesInit();
            $('#tbl_list').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bSortClasses": false
            });
            $('.btn_check').hide();
            unblockUI();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
           // HidePreloader();
        }
    });
}

function addItemToDisplay(ccaNum, custOutletId, custOutletName, custOutletLocation, prevCountDate, nextCountDate, empName, empId) {
    var outletname = custOutletName;
    if (outletname.indexOf("'") != -1) {
        outletname = outletname.replace("'", "");
    }
    $('#tbl_list tbody').append('<tr clone="true">' +
                                //'<td><span>' + ccaNum + '</span></td>' +
                                 '<td align="center"><a href="' + baseUrl + 'Document/AccountsDetails?ccanum=' + ccaNum + '">' + ccaNum + '</a></td>' +
    // '<td align="center"><a href="javascript:selected(\'' + custOutletId + '\',\'' + outletname + '\',\'' + empId + '\', \'' + empName + '\')" >' + custOutletId + '</a></td>' +
                                '<td align="center"><span>' + custOutletId + '</span></td>' +
                                '<td align="center"><span>' + custOutletName + '</span></td>' +
                                '<td><span>' + custOutletLocation + '</span></td>' +
                                '<td align="center"><span>' + FormatDate(prevCountDate) + '</span></td>' +
                                '<td align="center"><span>' + FormatDate(nextCountDate) + '</span></td>' +
                                '<td><span style="white-space:nowrap">' + empName + '</span></td>' +
                                '</tr>');

}

function fnFeaturesInit() {
    /* Not particularly modular this - but does nicely :-) */
    $('ul.limit_length>li').each(function (i) {
        if (i > 10) {
            this.style.display = 'none';
        }
    });

    $('ul.limit_length').append('<li class="css_link">Show more<\/li>');
    $('ul.limit_length li.css_link').click(function () {
        $('ul.limit_length li').each(function (i) {
            if (i > 5) {
                this.style.display = 'list-item';
            }
        });
        $('ul.limit_length li.css_link').css('display', 'none');
    });
}


function selected(custOutletId, custOutletName, empId, empName) {
    txt_outletId.attr("value", custOutletId);
    txt_outletName.attr("value", custOutletName);
    txt_empId.attr("value", empId);
    txt_empName.attr("value", empName);

    txt_empId.css("background-color", "#fff7dd");
}


function updateOutlet() {
    $.ajax({
        type: "POST", url: baseUrl + "Maintenance/UpdateOutlet",
        data:
		    "_custOutletId=" + txt_outletId.attr("value") + "&" +
		    "empId=" + txt_empId.attr("value") + "&" +
		    "",
        success: function (res) {
            // location.reload();
            alert("successfully updated!");
            updateTableList();
            clearFields();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function updateTableList() {
    $('#tbl_list tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);
            if (curr_row.find("td:nth-child(2)").find("a").html() == txt_outletId.attr("value")) {

                curr_row.find("td:nth-child(7)").find("span").html(txt_empName.attr("value"));

                return false;
            }
        });
    }

    

function enableReadonly() {
    $('input[type="text"]').attr("readonly", true);
    $('input[type="text"]').addClass("readonly");

    //when session expires remove readonly for username and password textfield
    $("#txtUsername").removeClass("readonly");
    $("#txtUsername").attr("readonly", false);
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
   //  $.HidePreloader();
    $.unblockUI();
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
    var name = $("#id_content select option:selected").attr("value");


    txt_empId.attr("value", code);
    txt_empName.attr("value", name);

    hide_dialog_box();
}
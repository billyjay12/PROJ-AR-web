//wala pani nagamit


$(function () {
    $("#txt_accountName").lookupTextField();

});

function displayData(acctCode) {

}


/**CHOSEN STYLE LOOK UP**/
$.fn.lookupTextField = function (isPlanned) {
    $(this).live({
        focus: function () {
            //$(this).unbind();
            create_dialog_box($(this).attr('id'), ($("#chk_isunPlanned").is(":checked") ? "T" : "F"));
        }
    });
}

function create_dialog_box(obj_id_to_position, isPlanned) {
    $('#div_last_element').append('<div id="id_bkg" class="dlg_box_bkg" onclick="javascript:hide_dialog_box();"></div>' +
    '<div id="id_content" class="dlg_box_content">' +
    '<table cellspacing="0" cellpadding="0" border="0">' +
    '<tr>' +
    '<td>' +
    '<select id="' + obj_id_to_position + '_value" style="width:' + ($("#" + obj_id_to_position).css("width")) + '; font-family:Arial; size:12px; outline:none;">' +
    '</select>' +
    '</td>' +
    '</tr>' +
    '<tr align="center">' +
    '<td><button onclick="javascript:setValueFromSelect(\'' + obj_id_to_position + '\',\'' + isPlanned + '\');" style="cursor:pointer;">Select</button></td>' +
    '</tr>' +
    '</table>' +
    '</div>');

    $.ajax({
        type: 'GET',
        url: baseUrl + "Calendar/lookUpAccount",
        data: {
            Eventmonth: Eventmonth,
            Eventday: Eventday,
            Eventyear: Eventyear,
            soId: soId,
            obj: obj_id_to_position,
            isplanned: isPlanned
        },
        success: function (res) {
            listOfItems = res;
            $('#' + obj_id_to_position + '_value').append(res).chosen().trigger("liszt:updated");
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

function setValueFromSelect(obj_id_to_position, isPlanned) {

    var Acctcode = $("#id_content select option:selected").attr("code");
    var name = $("#id_content select option:selected").attr("value");
    var address = $("#id_content select option:selected").attr("Addrs");
    var contactperson = $("#id_content select option:selected").attr("contactper");
    var phone = $("#id_content select option:selected").attr("Phone");
    var acctclxn = $("#id_content select option:selected").attr("classfxn");
    var hotelname = $("#id_content select option:selected").attr("HotelName");
    var hotelcontactno = $("#id_content select option:selected").attr("hotelContact");

    if (obj_id_to_position == "txt_accountName") {
        ClearFieldsCoverage();
        txt_acctcode.attr("value", Acctcode);
        txt_accountName.attr("value", name);
        txt_contactPerson.attr("value", contactperson);
        txt_accountClass.attr("value", acctclxn);
        txt_accountAddress.attr("value", address);
        txt_ContactNumber.attr("value", phone);
        txt_hotelname.attr("value", hotelname);
        txt_hotel_contact.attr("value", hotelcontactno);

        checkisInCoverage(f_eventId, soId, Eventday, Eventmonth, Eventyear, Acctcode);
    }
    if (obj_id_to_position == "txt_cr_accountname") {
        if (isPlanned == "T") {
            ClearFieldsCallreport();

            txt_cr_accountCode.attr("value", Acctcode);
            txt_cr_accountname.attr("value", name);
            txt_cr_contactperson.attr("value", contactperson);
            txt_cr_accountclass.attr("value", acctclxn);
            txt_cr_accountaddress.attr("value", address);
            txt_cr_contactpersonNo.attr("value", contactperson);

            /*inserted by billyjaydelima*/
            $("#location_in").text('').css("color", "#000000")
            $("#time_in").text('').css("color", "#000000");
            $("#location_out").text('').css("color", "#000000");
            $("#time_out").text('').css("color", "#000000");

            txt_hidden_cr_linenum.attr("value", "");
            /*end*/
            txt_cr_contactpersonNo.attr("value", phone);
            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
        }
        else {
            ClearFieldsCallreport();
            txt_cr_accountCode.attr("value", Acctcode);
            txt_cr_accountname.attr("value", name);
            txt_cr_contactperson.attr("value", contactperson);
            txt_cr_accountclass.attr("value", acctclxn);
            txt_cr_accountaddress.attr("value", address);
            txt_cr_contactpersonNo.attr("value", phone);
            txt_cr_hotelname.attr("value", hotelname);
            txt_cr_hotelcontact.attr("value", hotelcontactno);

            GetCoverageDtls(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
            $("#Logchanges").show();
            Getobjectivecode(f_eventId, Eventmonth, Eventday, Eventyear, soId, Acctcode);
        }
    }
    hide_dialog_box();
}
/**END CHOSEN STYLE LOOK UP**/
var ns4;

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));

    if (obj == "txt_item") {
        $("#" + "txt_product").attr("value", $("#id_content select option:selected").attr("val_itemName"));
        GetExtraDatas("ListOfitemNames", $("#id_content select option:selected").text(), "txt_item", "txt_product");
    }

    if (obj == "txt_price") {
        $("#" + obj).attr("value", GetId($("#id_content select option:selected").attr("value")));
        $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").attr("value")));
    }

    if (obj =="txt_buyer") {
        $("#" + "txt_address").attr("value", $("#id_content select option:selected").attr("val_address"));
        $("#" + "txt_tel_no").attr("value", $("#id_content select option:selected").attr("val_itemName"));
    }

    if (obj =="txt_referred_to") {
        $("#" + "txt_referred_to").attr("value", $("#id_content select option:selected").attr("val_referal"));
    }

    if (obj =="txt_unit") {
        $("#" + "txt_unit").attr("value", $("#id_content select option:selected").attr("val_unit"));
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function GetId(strVal) {
    return strVal;
}

function GetValue(strVal) {
    return strVal;
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}
function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();

    // hide other lookup dialog
    $("#div_pl").hide();
    $("#div_pl").remove();
    $(document).unbind("mousedown");

    var param  = "_str_data=" + str_data;

    if (obj_id_to_store == "txt_price") {
        var itemcode = $("#txt_item").attr("value");
        if (itemcode == "") { HidePreloader(); return; }
        param = param + "&keyword1=" + itemcode;
    }

    $.ajax({
        type: "POST", url: baseUrl + "eMat/GetList",
        data: param,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    } else {
        return "true";
    }
}

function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    var d_length = res_rows.length;
    for (i = 0; i < d_length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {

                if (obj_id_to_position =="txt_item") {
                    w = w + "<option val_itemName=\"" + res_cols[1] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
                }

                if (obj_id_to_position =="txt_buyer") {
                    w = w + "<option val_itemName=\"" + res_cols[3] + "\" value=\"" + res_cols[0] + "\" val_address=\"" + res_cols[2] + "\">" + res_cols[1] + "</option>";
                }

                if (obj_id_to_position =="txt_referred_to") {
                    w = w + "<option val_referal=\"" + res_cols[1] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
                }

                if (obj_id_to_position =="txt_unit") {
                    w = w + "<option val_unit=\"" + res_cols[1] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
                }

                if (obj_id_to_position == "txt_price") {
                    w = w + "<option val_itemName=\"" + res_cols[1] + "\" value=\"" + res_cols[2] + "\">" + res_cols[2] + " - " + res_cols[0] + "" + "</option>";
                }

            }
        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    // $("body").after(w);
    $("body").append(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();

    $("#id_content_upload").remove();
    $("#id_bkg_upload").remove();
}

function DelCurrRow(tbl_id, r_id) {
    //$("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(2) input[type=text]").attr("value");
    var qty_total = $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value");
    var orig_qty = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(5) input[type=text]").attr("value");
  
    $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value", parseInt(qty_total) - parseInt(orig_qty));

    var over_total = $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value");
    var partial_total = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(6) input[type=text]").attr("value");

    var tmp_total = parseFloat(over_total) - parseFloat(partial_total);
    var new_total = Math.round(tmp_total * Math.pow(10, 2)) / Math.pow(10, 2);

    $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value", new_total);
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();

}

function AddProduct() {
    // check fields if empty
    /* first field */
    var val_one = $("#table_details tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#table_details tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } 

    var val_three = $("#table_details tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_three) == true) {
            alert("PRICE MUST BE A NUMBER.");
            return;
        }
    }

    var val_five = $("#table_details tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value");
    if (val_five == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } 

    var val_four = $("#table_details tr:last").prev().find("td:nth-child(5) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_four) == true) {
            alert("QUANTITY MUST BE A NUMBER.");
            return;
        }
    }

    var tmp_total = parseFloat(val_three) * parseFloat(val_four)
    

    var rowid = $("#table_details tr").length - 1;
    rowid++;
    $("#table_details tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:99%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" style=\"width:95%; text-align:left;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + val_five + "\" style=\"width:95%; text-align:left;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + val_four + "\" style=\"width:95%; text-align:left;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + tmp_total + "\" style=\"width:98%; text-align:left;\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('table_details'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
    );

    // save the total value
    var cached_total_value = $("#table_details tr:last").find("td:nth-child(3) input[type=text]").attr("value");

    var total = parseFloat(cached_total_value) + parseFloat(tmp_total) ;
    var new_total = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);

    $("#table_details tr:last").find("td:nth-child(2) input[type=text]").attr("value", parseInt($("#table_details tr:last").find("td:nth-child(2) input[type=text]").attr("value")) + parseInt(val_four));
    $("#table_details tr:last").find("td:nth-child(3) input[type=text]").attr("value", new_total);

    // save it in the table
    $("#table_details").attr("cached_total_value", new_total);

    // clear values
    $("#table_details tr:last").prev().find("td input[type=text]").attr("value", "");
}

function SaveEmatDoc() {
    
    DisplayPreloader();

    var status = "9";

    if (CheckRequiredFields() == false) {
        HidePreloader();
        return;
    }

    var encodedBy ="";
    encodedBy = $("#txt_encoded_by").attr("value");

    var buyer = "";
    buyer = $("#txt_buyer").attr("value");

    var buyerAdd = "";
    buyerAdd = $("#txt_address").attr("value");
    
    var ematDoc="";
    ematDoc = $("#txt_doc_num").attr("value");

    var telNo = "";
    telNo = $("#txt_tel_no").attr("value");

    var contactPerson = "";
    contactPerson = $("#txt_contact_person").attr("value");
    
    var terms = "";
    terms = $("#txt_terms").attr("value");
    
    var deliveryDate = "";
    deliveryDate = $("#txt_delivery_date").attr("value");
    
    var deliveryInstrct = "";
    deliveryInstrct = $("#txt_delivery_instruction").attr("value");
    
    var acctCode = "";
    acctCode = $("#txt_referred_to").attr("value");
    
    var submttdTo = "";
    submttdTo = $("#txt_Submitted_to").attr("value");
    
    var submttdContactNum = "";
    submttdContactNum = $("#txt_contact_number").attr("value");

    var tradeSalesRep = "";
    tradeSalesRep = $("#txt_trade_sales_rep").attr("value");

    var confirmedDelBy = "";
    confirmedDelBy = $("#txt_confirmed_delivery").attr("value");

    // saving emat details
    
    var product= new Array();
    var loop_count = 1;
    var r_count = $("#table_details tr").length - 1;
    $("#table_details tr").each(
        function (index, element) {
            if (loop_count > 1 && loop_count < r_count) {
                product.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr("value")
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr("value")
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr("value")
                    + "|" + $(element).find("td:nth-child(4) input[type=text]").attr("value")
                    + "|" + $(element).find("td:nth-child(5) input[type=text]").attr("value")
                    + "|" + $(element).find("td:nth-child(6) input[type=text]").attr("value")
                );
            }
            loop_count++;
        }
    );

    var params = {
        status: status,
        encodedBy: encodedBy,
        buyer: buyer,
        ematDoc: ematDoc,
        buyerAdd: buyerAdd,
        telNo: telNo,
        contactPerson: contactPerson,
        terms: terms,
        deliveryDate: deliveryDate,
        deliveryInstrct: deliveryInstrct,
        acctCode: acctCode,
        submttdTo: submttdTo,
        submttdContactNum: submttdContactNum,
        tradeSalesRep: tradeSalesRep,
        confirmedDelBy: confirmedDelBy,
        product: product
    };

    params = $.param(params, true);

    $.ajax({
        type: "POST", url: baseUrl + "eMAT/ConfirmEmatTrans",
        data: params ,
        success: function (res) {
            
            if (SrvResultMsg.GetMsgType(res) != "error") {

                alert("SUCCESSFULLY SAVED!");
                window.location = baseUrl + "eMAT/eMATDetails?eMATno=" + SrvResultMsg.GetMessage(res);
            } else {
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}


function DocSave() {
    SaveEmatDoc();
}

function UpdateStatus() {
    var docNum = $("#txt_ematdocnum").attr('value');
    
    $.ajax({
        type: "POST", url: baseUrl + "eMAT/SendEMATDoc",
        data:
			"eMATno=" + val_eMATno + "&" +
			"region=" + val_region + "&" +
			"",
        success: function (res) {
            if (res.substring(0, 3) == "00:") {
                location.reload();
            } else {
                // error
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function SendEMATDoc() {
    var val_eMATno = $("#txt_eMAT_no").attr('value');
    var val_region = $("#txt_referred_to").attr('value');

    $.ajax({
        type: "POST", url: baseUrl + "eMAT/SendEMATDoc",
        data:
			"eMATno=" + val_eMATno + "&" +
			"region=" + val_region + "&" +
			"",
        success: function (res) {
            if (res.substring(0, 3) == "00:") {
                location.reload();
            } else {
                // error
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function cancel() {
    var okToRefresh = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (okToRefresh) {
        location.reload();
    }
}

function hidebutton() {
    $("#btn_sendto_csmanager").hide();
    $("#btn_cancel").hide();
}

function GetExtraDatas(val_lookup_type, val_par1, val_main_obj, val_obj1, val_obj2, val_obj3, val_obj4) {
    var ajx_data = "";

    if (val_main_obj == "txt_acct_code" || val_main_obj == "txt_item") {
        ajx_data = "_str_data=" + val_lookup_type + "&par1=" + val_par1;
    } else {
        ajx_data = "_str_data=" + val_lookup_type;
    }

    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: ajx_data,
        success: function (res) {
            if (IsError(res)) {
                // StrResultTags(res);

                var data_to_add = StrResultTags(res);
                var res_rows = data_to_add.split("#$");
                var d_length = res_rows.length;
                for (i = 0; i < d_length; i++) {
                    var res_cols = res_rows[i].split("|");
                    if (res_cols[0] != null) {
                        if (res_cols[0] != "") {
                            // 
                            $("#" + val_obj1).attr("value", res_cols[0]);
                        }
                    }
                }
            } 
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}

function CheckRequiredFields() {
    var lacking_fields = "";

    if ($("#txt_doc_num").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Emat No."; } else { lacking_fields = "Emat No."; }
    }

    if ($("#txt_buyer").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Buyer"; } else { lacking_fields = "Buyer"; }
    }

    if ($("#txt_address").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Address"; } else { lacking_fields = "Address"; }
    }

    if ($("#txt_tel_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tel. No."; } else { lacking_fields = "Tel. No."; }
    }

    if ($("#txt_contact_person").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Person"; } else { lacking_fields = "Contact Person"; }
    }

    if ($("#txt_terms").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Terms"; } else { lacking_fields = "Terms"; }
    }

    if ($("#txt_delivery_date").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Delivery Date"; } else { lacking_fields = "Delivery Date"; }
    }

    if ($("#txt_delivery_instruction").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Delivery Instruction"; } else { lacking_fields = "Delivery Instruction"; }
    }

    if ($("#txt_referred_to").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Refered To"; } else { lacking_fields = "Refered To"; }
    }

    if ($("#txt_Submitted_to").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Submitted To"; } else { lacking_fields = "Submitted To"; }
    }
    
    if ($("#txt_trade_sales_rep").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Sales Rep."; } else { lacking_fields = "Sales Rep."; }
    }

    if ($("#txt_confirmed_delivery").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Confirmed By"; } else { lacking_fields = "Confirmed By"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else 
    {
        return true;
    }
}

function CheckEmatDoc() {
    // account code
    var proposed_emat_doc = "";
    proposed_emat_doc = $("#txt_doc_num").attr('value');

    if (proposed_emat_doc == "") { return; }

    $.ajax({
        type: "POST", url: baseUrl + "eMat/IsEmatExist",
        data: "proposed_emat_doc=" + proposed_emat_doc,
        success: function (res) {
            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert(SrvResultMsg.GetMessage(res));
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

/* LOOKUP */
function BindToLookUpLive(obj) {
    $("#" + obj).bind('keyup', function () {
        if ($("#" + obj).attr("value").length > 2) {
            $("#div_live_lookup").hide();
            LookUpLiveData(obj);
        } else {
            HideLiveLookUp();
        }
        
        $("#" + obj).parent().parent().find("td:nth-child(2) input[type=text]").attr("value", "");
    });

}

function HideLiveLookUp() {
    $("#div_live_lookup").hide();
    $("#div_live_lookup").remove();
}

function HandleSuccessGET(obj, res) {
    var w = "<div id=\"div_live_lookup\" >";

    // w = w + "<table style=\"width:100%; background:#ffffff;\" cellpadding=\"1\" cellspacing=\"0\" border=\"0\" ><tr><td align=\"right\">";
    // w = w + "<a href=\"javascript:;\" onclick=\"javascript:HideLiveLookUp();\" >Close</a></td></tr></table>";

    var splitted_val = res.split("#$");
    var d_length = splitted_val.length;
    if (res.length > 0)
    for (i = 0; i < d_length; i++) {
        var res_cols = splitted_val[i].split("|");
        if (splitted_val[0] != null) {
            if (res_cols[0] != "") {
                w = w + "<div><a href=\"javascript:;\" val1=\"" + obj + "\" val2=\"" + res_cols[0] + "\" val3=\"" + res_cols[1] + "\" >" + res_cols[0] + "</a></div>";
            }
        }
    }

    w = w + "</div>";

    // append
    $("body").after(w);

    // bind
    $("#div_live_lookup div a").bind('click', function () {
        
        $("#" + obj).attr("value", $(this).attr("val2"));
        $("#" + obj).parent().parent().find("td:nth-child(2) input[type=text]").attr("value", $(this).attr("val3"));
        HideLiveLookUp();
    });

    // set position
    var btnY = getElLeft(document.getElementById(obj));
    var btnX = getElTop(document.getElementById(obj));

    var txtboxheight = document.getElementById(obj).offsetHeight;

    btnX = txtboxheight + btnX;
    $("#div_live_lookup").css('top', btnX + '' + 'px');
    $("#div_live_lookup").css('left', btnY + '' + 'px');

    // show 
    $("#div_live_lookup").show("fast");

}

function HandleErrorGET(obj) {
    
}

function LookUpLiveData(obj) {
    var keyword = "";
    keyword = $("#" + obj).attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "eMat/GetJList",
        data: "_str_data=ListOfitemCode&keyword=" + keyword,
        success: function (res) { HandleSuccessGET(obj, res); },
        error: function (xhr, ajaxOptions, thrownError) { HandleErrorGET(obj); }
    });
}


function ShowPLButton(obj) {

    var w = "<div id=\"div_pl\" style=\"border:1px solid #ededed; background:#ffffff; padding:1px; position:absolute; \">" +
    "<a id=\"div_pl_a\" style=\"font-family:Arial; font-size:11px; text-decoration:none; \" href=\"javascript:;\" onclick=\"javascript:LookUpData('txt_price','ListOfPrice');\" >Pricelist <img src=\"" + baseUrl + "Images/magnifier.png\" style=\"border:0;\" /></a>" +
    "</div>";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj));
    var btnX = getElTop(document.getElementById(obj));

    var txtboxheight = document.getElementById(obj).offsetHeight;

    $(document).bind("mousedown", function (event) {
        if (event.target.id != "txt_price" && event.target.id != "div_pl_a") {
            $("#div_pl").hide();
            $("#div_pl").remove();
        }
    });

    btnX = txtboxheight + btnX;
    $("#div_pl").css('top', btnX + '' + 'px');
    $("#div_pl").css('left', btnY + '' + 'px');

    // show 
    $("#div_pl").show("fast");

}
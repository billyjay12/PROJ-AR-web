var ns4;
function LookUpData(obj_id_to_store, str_data, str_acct_code) {
    $.ajax({
        type: "POST", url: baseUrl + "SQL/Details",
        data: "str_data=" + str_data + "&" +
            "acctCode=" + str_acct_code ,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    }
    else {
        return "true";
    }
}


function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		    "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		    "<div id=\"id_content\" class=\"dlg_box_content\">" +
		    "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		    "<tr><td align=\"left\" style=\"overflow:auto; width: 800px;\">" +
		    "<table id=\"bouncedcheckstable\">";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<td style=\"width:100px;\">" + res_cols[0] + "</td>";
                w = w + "<td style=\"width:100px;\">" + res_cols[1] + "</td>";
                w = w + "<td style=\"width:600px;\">" + res_cols[2] + "</td>";
                w = w + "<td style=\"width:1000px;\">" + res_cols[3] + "</td>";
                w = w + "</tr>";
            }
        }
        if (res_cols[1] == null) {
            w = w + "No Incidents of Dishonored Checks So Far!";
        }
    }

    w = w + "" +
            "</table>" +
		    "<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"OK\">" +
		    "</td></tr></table></div>" +
		    "";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function LookForBouncedChecks() {
    var str_acct_code = "";
    str_acct_code = $("#txt_acctCode").attr('value');

    LookUpData('txt_disChecks', 'list_of_bounced_checks', str_acct_code);
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function SetValueFromSelect(obj) {
    $("#" + obj).attr("value","");
    $("#" + obj).attr("value", $("#bouncedcheckstable tr").length);

    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
    $("#bouncedcheckstable").remove();
}

function AddFields(col1, col2, col3) {
    var rowid = $("#tbl_br7 tr").length - 1;
    rowid++;
    $("#tbl_br7 tr:last").prev().after(
		    "<tr RowId=\"" + rowid + "\">" +
			    "<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" size=\"30\" /></td>" +
                "<td><input type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" size=\"30\" /></td>" +
                "<td><input type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" size=\"30\" /></td>" +
		    "</tr>"
        );
    $("#tbl_br7 tr td img").hide();    
}

function Hide() {
    $("#tbl_br7 tr td img").hide();
}

function HideBorders() {
    $("#comExAgr").hide();
    $("#comAcctPer").hide();
    $("#strat_plans").hide();
    $("#plans").hide();
    $("#support").hide();
    $("#changes_mf").hide();
    $("#proposed_credit_change").hide();
    $("#other_info").hide();
}

function HideRemarksFromSSM() {
    $("#div_ssm").hide();
}

function HideRemarksFromFNM() {
    $("#div_fnm").hide();
}

function HideAreaRemarks() {
    $("#area_remarks").hide();
}

function HideCEOAreaRemarks() {
    $("#txt_ceoRemarks").hide();
    $("#ceo_areaRemarks").hide();
}

function DisableEditingAll() {

    $("#txt_comExAgr").attr("readonly", "readonly");
    $("#txt_comAcctPer").attr("readonly", "readonly");
    $("#txt_STReason").attr("readonly", "readonly");
    $("#txt_plan").attr("readonly", "readonly");
    $("#txt_support").attr("readonly", "readonly");
    $("#txt_ExstcrdLimit").attr("readonly", "readonly");
    $("#txt_ReccrdLimit").attr("readonly", "readonly");
    $("#txt_ExstcrdTerm").attr("readonly", "readonly");
    $("#txt_ReccrdTerm").attr("readonly", "readonly");
    $("#txt_length_of_payment").attr("readonly", "readonly");
    $("#txt_remarks").attr("readonly", "readonly");
    $("#txt_disChecks").attr("readonly", "readonly");
    $("#txt_disremarks").attr("readonly", "readonly");
    $("#txt_field").attr("readonly", "readonly");
    $("#txt_existing_val").attr("readonly", "readonly");
    $("#txt_revised_val").attr("readonly", "readonly");
    $("#txt_ceoRemarks").attr("readonly", "readonly");
    $("#ceo_areaRemarks").attr("readonly", "readonly");
    $("#txt_other_info").attr("readonly", "readonly");
}

function DisableEditingForCEO() {
    $("#txt_comExAgr").attr("readonly", "readonly");
    $("#txt_comAcctPer").attr("readonly", "readonly");
    $("#txt_STReason").attr("readonly", "readonly");
    $("#txt_plan").attr("readonly", "readonly");
    $("#txt_support").attr("readonly", "readonly");
    $("#txt_ExstcrdLimit").attr("readonly", "readonly");
    $("#txt_ReccrdLimit").attr("readonly", "readonly");
    $("#txt_ExstcrdTerm").attr("readonly", "readonly");
    $("#txt_ReccrdTerm").attr("readonly", "readonly");
    $("#txt_length_of_payment").attr("readonly", "readonly");
    $("#txt_remarks").attr("readonly", "readonly");
    $("#txt_disChecks").attr("readonly", "readonly");
    $("#txt_disremarks").attr("readonly", "readonly");
    $("#txt_field").attr("readonly", "readonly");
    $("#txt_existing_val").attr("readonly", "readonly");
    $("#txt_revised_val").attr("readonly", "readonly");
    $("#ceo_areaRemarks").attr("readonly", "readonly");
}

function DisableRemarkVPTFI() {
    $("#txt_areaRemarks").attr("readonly", "readonly");
}

function DisableRemarkVPBSM() {
   $("#txt_area_field").attr("readonly", "readonly");
}

function DisableEditingRemarks() { 
  $("#txt_area_field").attr("readonly", "readonly");
  $("#txt_areaRemarks").attr("readonly", "readonly");
}

function DisableEditingFF() {
    $("#txt_length_of_payment").attr("readonly", "readonly");
    $("#txt_exst_credit_term").attr("readonly", "readonly");
    $("#txt_remarks").attr("readonly", "readonly");
    $("#txt_disChecks").attr("readonly", "readonly");
    $("#txt_disremarks").attr("readonly", "readonly");
}

function DisableEditing() {

    $("#txt_comExAgr").attr("readonly", "readonly");
    $("#txt_comAcctPer").attr("readonly", "readonly");
    $("#txt_STReason").attr("readonly", "readonly");
    $("#txt_plan").attr("readonly", "readonly");
    $("#txt_support").attr("readonly", "readonly");
    $("#txt_ExstcrdLimit").attr("readonly", "readonly");
    $("#txt_ReccrdLimit").attr("readonly", "readonly");
    $("#txt_ExstcrdTerm").attr("readonly", "readonly");
    $("#txt_ReccrdTerm").attr("readonly", "readonly");
    $("#txt_field").attr("readonly", "readonly");
    $("#txt_existing_val").attr("readonly", "readonly");
    $("#txt_revised_val").attr("readonly", "readonly");
    $("#txt_other_info").attr("readonly", "readonly");
}

function DisableEditingRemarksfromSSM() {
    $("#txt_remarksfromSSM").attr("readonly", "readonly");

}

function DisableEditingRemarksfromFNM() {
    $("#txt_remarksfromFNM").attr("readonly", "readonly");

}


function DisableField() {
    $("#txt_STOrigAnn").attr("disabled", "disabled");
    $("#txt_STRevAnn").attr("disabled", "disabled");
    $("#txt_ReccrdLimit").attr("disabled", "disabled");
    $("#txt_ReccrdTerm").attr("disabled", "disabled");
    
    
}


	// For CSR
function BusReviewDocSave() 
{
        DisplayPreloader();
        var ccaNum = $("ccaNum")
 
        var br_no;
        br_no = $("#txt_brn").attr("value");

        var date = "";
        date = $("#txt_br_date").attr("value");

        var acctCode = "";
        acctCode = $("#txt_acctCode").attr("value");

        var comExAgr = "";
        comExAgr = $("#txt_comExAgr").attr("value");

        if (comExAgr == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [COMMENTS ON EXISTING AGREEMENTS] FIELD.");
            HidePreloader();
            return;
        }

        var comAcctPer = "";
        comAcctPer = $("#txt_comAcctPer").attr("value");

        if (comAcctPer == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [COMMENTS ON CURRENT ACCOUNT PERFORMANCE] FIELD.");
            HidePreloader();
            return;
        }

        var STOrigAnn = "";
        STOrigAnn = $("#txt_STOrigAnn").attr("value");

        if (STOrigAnn == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [ORIGINAL ANNUAL] FIELD.");
            HidePreloader();
            return;
        }

        var STRevAnn;
        STRevAnn = $("#txt_STRevAnn").attr("value");

        if (STRevAnn == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REVISED ANNUAL] FIELD.");
            HidePreloader();
            return;
            
        }

        var STReason = "";
        STReason = $("#txt_STReason").attr("value");

        if (STReason == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REASON] FIELD.");
            HidePreloader();
            return;
        }

        var plan = "";
        plan = $("#txt_plan").attr("value");

        if (plan == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [INCENTIVE PLANNING] FIELD.");
            HidePreloader();
            return
        }

        var field = "";
        row_count = $("#tbl_br7 tr ").length - 1;
        for (i = 2; i <= row_count; i++) {
            if (field != "") { field = field + "$"; }
            field = field + $("#tbl_br7 tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
            field = field + $("#tbl_br7 tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
            field = field + $("#tbl_br7 tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        }

        var support = "";
        support = $("#txt_support").attr("value");

        if (support == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [SUPPORT] FIELD.");
            HidePreloader();
            return;

        }

        var other_info = "";
        other_info = $("#txt_other_info").attr("value");

        if (other_info == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [OTHER INFORMATION] FIELD.");
            HidePreloader();
            return;
        }

        var ExstcrdLimit;
        ExstcrdLimit = $("#txt_ExstcrdLimit").attr("value");

        var ReccrdLimit = "";
        ReccrdLimit = $("#txt_ReccrdLimit").attr("value");

        if (ReccrdLimit == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [RECOMMENDED CREDIT LIMIT] FIELD.");
            HidePreloader();
            return;
        }

        var ExstcrdTerm = "";
        ExstcrdTerm = $("#txt_ExstcrdTerm").attr("value");

        var ReccrdTerm = "";
        ReccrdTerm = $("#txt_ReccrdTerm").attr("value");

        if (ReccrdTerm == "") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [RECOMMENDED CREDIT TERM] FIELD.");
            HidePreloader();
            return;

        }

        var encoded_by = "";
        encoded_by = $("#txt_encoded_by").attr("value");

        // AMPERSAND
        comExAgr = EncodeAmpersand(comExAgr);
        comAcctPer = EncodeAmpersand(comAcctPer);
        plan = EncodeAmpersand(plan);
        support = EncodeAmpersand(support);
        other_info = EncodeAmpersand(other_info);
        STReason = EncodeAmpersand(STReason);

        // DisablePage();
        $.ajax({
          type: "POST", url: baseUrl + "BusinessReview/SaveBusinessReviewDoc",
            data: "" +
                "ccaNum=" + ccaNum + "&" +
                "br_no=" + br_no + "&" +
                "date=" + date + "&" +
                "acctCode=" + acctCode + "&" +
                "comExAgr=" + comExAgr + "&" +
                "comAcctPer=" + comAcctPer + "&" +
                "STOrigAnn=" + STOrigAnn + "&" +
                "STRevAnn=" + STRevAnn + "&" +
                "STReason=" + STReason + "&" +
                "plan=" + plan + "&" +
                "support=" + support + "&" +
                "other_info=" + other_info + "&" +
                "ExstcrdLimit=" + ExstcrdLimit + "&" +
                "ReccrdLimit=" + ReccrdLimit + "&" +
                "ExstcrdTerm=" + ExstcrdTerm + "&" +
                "encoded_by=" + encoded_by + "&" +
                "ReccrdTerm=" + ReccrdTerm +
                "",
            success: function (res) {
                CallRouting('Disapprove', br_no,"");
            },
            error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
        });

    }

function CallRouting(val_action_type, val_brNo, obj) {
    DisplayPreloader();
    var remarks = $("#" + obj).attr("value");
    var status = "";
    if (obj != undefined && obj == "txt_remarksfromFNM") {
        status = "3";
    }
        
        
if (remarks == "" && obj !="") {
    if (obj == "txt_remarksfromSSM") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS FROM SSM] FIELD.");
            HidePreloader();
            return;
        }

        if (obj == "txt_remarksfromFNM") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS FROM FINANCE MANAGER] FIELD.");
            HidePreloader();
            return;
        }

        if (obj == "txt_area_field") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS FROM VP - FINANCE] FIELD.");
            HidePreloader();
            return;
        }

        if (obj == "txt_areaRemarks") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS FROM SALES DIRECTOR] FIELD.");
            HidePreloader();
            return;
        }

        if (obj == "txt_ceoRemarks") {
            alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS FROM CEO] FIELD.");
            HidePreloader();
            return;
        }

    }
    if (remarks != undefined)
    {
        remarks = EncodeAmpersand(remarks);
    }
    
    $.ajax({
        type: "POST", url: baseUrl + "BusinessReview/CallRouting",
        data:
		"val_action_type=" + val_action_type + "&" +
        "val_brNo=" + val_brNo + "&" +
        "remarks=" + remarks +
		""
		,
        success: function (res) {
            if (obj != undefined && obj == "txt_remarksfromFNM" && val_action_type == "Approve") {
                alert("DOCUMENT WAS APPROVED. PLEASE FILL IN FINANCE DETAILS.");
                HidePreloader();
                location.reload();
            }

            if (obj != undefined && obj == "txt_remarksfromFNM" && val_action_type=="Disapprove") {
                alert("SUCCESSFULLY SAVED!");
                window.location = baseUrl + "BusinessReview/BusinessReviewList";
            }

            if (status == "") {
                alert("SUCCESSFULLY SAVED!");
                window.location = baseUrl + "BusinessReview/BusinessReviewList";
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });
}
			
function Cancel() {
    var isCancelled = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (isCancelled) {
        window.history.back()
    }
}


//SAVING FINANCE DETAILS
function SaveFinanceDoc() {
    var br_no;
    br_no = $("#txt_brn").attr("value");

    var lenPayment = "";
    lenPayment = $("#txt_length_of_payment").attr("value");

    if (lenPayment == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [LENGTH OF PAYMENT] FIELD.");
        HidePreloader();
        return;
    }

    var existCreditTerm = "";
    existCreditTerm = $("#txt_exst_credit_term").attr("value");
                        
    var remarksCredTerm = "";
    remarksCredTerm = $("#txt_remarks").attr("value");

    if (remarksCredTerm == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS] FIELD.");
        HidePreloader();
        return;
    }

    var dishonCheck = "";
    dishonCheck = $("#txt_disChecks").attr("value");

    if (dishonCheck == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [INCIDENTS OF DISHONORED CHECKS] FIELD.");
        HidePreloader();
        return;
    }

    var remarksDisCheck = "";
    remarksDisCheck = $("#txt_disremarks").attr("value");

    if (remarksDisCheck == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [REMARKS] FIELD.");
        HidePreloader();
        return;
    }

    // AMPERSAND
    remarksCredTerm = EncodeAmpersand(remarksCredTerm);
    remarksDisCheck = EncodeAmpersand(remarksDisCheck);
    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "BusinessReview/SaveFinanceDoc",
        data: "busReviewNo=" + br_no + "&" +
                "lenPayment=" + lenPayment + "&" +
                "existCreditTerm=" + existCreditTerm + "&" +
                "remarksCredTerm=" + remarksCredTerm + "&" +
                "dishonCheck=" + dishonCheck + "&" +
                "remarksDisCheck=" + remarksDisCheck +
                "",
        success: function (res) {
            alert("SUCCESSFULLY SAVED!");
            HidePreloader();
            window.location = baseUrl + "BusinessReview/BusinessReviewList";
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}
		
		

function isNumberKey(evt,txt_obj) {
    if ($("#" + txt_obj).attr("value") == "") {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 44 || charCode > 57 || charCode == 45 || charCode == 47)) {
            return false;
        }
        else {
            return true;
        }

    }
}



function AddComma(txt_obj) {
    var num = $("#" + txt_obj).attr("value");
    var val_three = "";

    if (parseFloat(num) >= 1000) {
        val_three = formatNumberDec(num, 2, 1);
        // return val_three;
    }
    else {
        val_three = formatNumberDec(num, 2, 0);
    }
            
    $("#" + txt_obj).attr("value", "");
    $("#" + txt_obj).attr("value", val_three);
}

function BindToTextFormatting(text_object) {
    $("#" + text_object).css("text-align", "right");

    $("#" + text_object).bind("keypress",
		function (e) {
		    if (($(this).attr("value").length == 0 || $(this).attr("value").indexOf(".") > -1) && e.which == 46) return false;
		    if ($(this).attr("value").length == 0 && e.which == 48) return false;
		    if ((e.which >= 48 && e.which <= 57) || e.which == 46) return true;
		    return false;
		}
	);

    $("#" + text_object).bind("keyup",
		function (e) {
		    if ((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || e.which == 8) {
		        var reversed_result = ""; var tmp_result = "";
		        var prev_val = $(this).attr("value").replace(/,/g, "");
		        var pos_of_dot = prev_val.indexOf(".");
		        if (pos_of_dot == -1) pos_of_dot = prev_val.length;
		        var no_decimal_val = prev_val.substring(0, pos_of_dot);

		        var icounter = 0;
		        for (var i = no_decimal_val.length - 1; i >= 0; i = i - 1) {
		            icounter++; reversed_result = reversed_result + no_decimal_val.substring(i, i + 1); if (icounter % 3 == 0 && i > 0) reversed_result = reversed_result + ",";
		        }
		        for (var i = reversed_result.length - 1; i >= 0; i--) { tmp_result = tmp_result + reversed_result.substring(i, i + 1); }
		        $(this).attr("value", tmp_result + prev_val.substring(pos_of_dot, pos_of_dot + 100));
		    }
		}
	);

}


function LookUpDataCredTerms(obj_id_to_store, str_data) {
           
    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBoxCredTerm(obj_id_to_store, StrResultTags(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });

    // show dialog box/window
}

function LookUpDataAcctDetails(obj_id_to_store, str_data) {
    var acctcode = $("#txt_acctCode").attr("value");
    $.ajax({
        type: "POST", url: baseUrl + "SQL/ListOfAcctDetails",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBoxCredTerm(obj_id_to_store, StrResultTags(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
    // show dialog box/window
}

        
function CreateDialogBoxCredTerm(obj_id_to_position, data_to_add) {
         
    var w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
        "<div id=\"id_content\" class=\"dlg_box_content\">" +
        "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
        "<tr><td align=\"right\" >" +
        "<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");

        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            }
        }
    }

    w = w + "" +
        "\n</select>" +
        "<br /> <input onclick=\"javascript:SetValueFromSelectCredTerm('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
        "</td></tr></table></div>" +
        "";

    // append
    $("body").after(w);
    
    var btnY = getElLeft($("#" + obj_id_to_position)[0]);
    var btnX = getElTop($("#" + obj_id_to_position)[0]);
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SetValueFromSelectCredTerm(obj) {
    $("#" + obj).attr("value", $("#id_content select option:selected").text());
    $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
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

function GetId(strVal) {
    return strVal.substring(0, strVal.indexOf('-') - 1);
    //    return strVal
}

function HideFinanceTable() {
    $("#div_finance").hide();
    $("#finance_table").hide();
}

function HideTbl_improvements() {
    $("#txt_area_field").hide();
    $("#comments").hide();
}

function GetValue(strVal) {
    return strVal.substring(strVal.indexOf('-') + 2, 200);
    //    return strVal;
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

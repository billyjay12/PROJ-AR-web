var ns4;

/* for browsing data for a certain field */
/* should only display two column */
function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "Customer/GetFilteredList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

    // show dialog box/window
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
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option price_desc=\"" + res_cols[2] + "\" val_area=\"" + res_cols[2] + "\" val_region=\"" + res_cols[3] + "\" grp_name=\"" + res_cols[4] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            }
        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
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

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();

    $("#id_content_upload").remove();
    $("#id_bkg_upload").remove();
}

function CreateUploadingBox(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseUrl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/CcaUploadDialogBox\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBox('" + obj_id_to_position + "');\" />" +
		"</div>" +
		"</div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content_upload").css('top', btnX + '' + 'px');
    $("#id_content_upload").css('left', btnY + '' + 'px');

    // show 
    $("#id_content_upload").show("fast");
    $("#id_bkg_upload").show();
}

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", $("#id_content select option:selected").text());
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));

    if (obj == "txt_acct_territory") {
        $("#txt_area").attr("value", GetValue($("#id_content select option:selected").attr("val_area")));
        $("#txt_area").attr("value_id", GetId($("#id_content select option:selected").attr("val_area")));

        $("#txt_region").attr("value", GetValue($("#id_content select option:selected").attr("val_region")));
        $("#txt_region").attr("value_id", GetId($("#id_content select option:selected").attr("val_region")));


        /* Code added by Billy Jay (05/05/2015) */
        $("#" + obj).attr("grp_name_", $("#id_content select option:selected").attr("grp_name"));

        //if ($("#id_content select option:selected").attr("grp_name") == "GT") {
        $("#txt_credit_terms_architectural_brand").addClass("required_fields");
        $("#txt_credit_terms_eco_lumber").addClass("required_fields");

        $("#txt_order_limit_ab").addClass("required_fields");
        $("#txt_order_limit_tr").addClass("required_fields");
            // $("#txt_credit_terms_eco_plywood").addClass("required_fields");
       // }
       // else {
        //    $("#txt_credit_terms_architectural_brand").removeClass("required_fields");
        //    $("#txt_credit_terms_eco_lumber").removeClass("required_fields");

        //    $("#txt_order_limit_ab").removeClass("required_fields");
       //     $("#txt_order_limit_tr").removeClass("required_fields");
            // $("#txt_credit_terms_eco_plywood").removeClass("required_fields");
        //}

        if ($("#slt_acct_classification").attr("value") == "WALKIN") {
            if ($("#txt_region").attr("value").toUpperCase().search("VISMIN") > -1) {
                $("#txt_tax_id").attr("value", "000 000 000 002");
                $("#txt_tax_id").attr("default_value", "000 000 000 002");
            } else {
                $("#txt_tax_id").attr("value", "000 000 000 001");
                $("#txt_tax_id").attr("default_value", "000 000 000 001");
            }
        }
        else {
            if ($("#txt_region").attr("value").toUpperCase().search("VISMIN") > -1) {
                $("#txt_tax_id").attr("default_value", "000 000 000 002");
            } else {
                $("#txt_tax_id").attr("default_value", "000 000 000 001");
            }
        }
        /* End  Code added by Billy Jay (05/05/2015) */
    }

    if (obj == "txt_type_of_account") {
        $("#" + obj).attr("value", $("#id_content select option:selected").text());
        $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));
    }

    if (obj == "txt_mw_price_code") {
        $("#txt_mw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_ww_price_code") {
        $("#txt_ww_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_pw_price_code") {
        $("#txt_pw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_pwf_price_code") {
        $("#txt_pwf_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_pwr_price_code") {
        $("#txt_pwr_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_gw_price_code") {
        $("#txt_gw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_tw_price_code") {
        $("#txt_tw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_mz_price_code") {
        $("#txt_mz_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_nw_price_code") {
        $("#txt_nw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_ec_price_code") {
        $("#txt_ec_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_ecu_price_code") {
        $("#txt_ecu_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function GetId(strVal) {
    var tmp_str = strVal.substring(0, strVal.indexOf('-'));
    return tmp_str.replace(/^\s*|\s*$/gi, "");
}

function GetValue(strVal) {
    if (strVal.indexOf('-') > -1) {
        return strVal.substring(strVal.indexOf('-') + 2, 200);
    } else {
        return strVal;
    }
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function DeleteFileAttachment(attch_type) {

    var ccanum = "";
    var filename = "";
    var obj = "";

    if (attch_type == "AOI") { obj = "txt_articles_of_inc"; }
    if (attch_type == "FS") { obj = "txt_financial_statement"; }
    if (attch_type == "ITR") { obj = "txt_ITR"; }
    if (attch_type == "BIR") { obj = "txt_bir_reg"; }
    if (attch_type == "BP") { obj = "txt_business_permit"; }
    if (attch_type == "OTHER") { obj = "txt_attch_other"; }

    // get ccaNum
    if ($("#txt_acct_ccanum").length > 0) {
        ccanum = $("#txt_acct_ccanum").attr('value');
    }

    // get filename
    filename = $("#" + obj).attr('value');

    if (filename == "") {
        HidePreloader();
        return;
    }

    if (filename != "") {
        $.ajax({
            type: "POST", url: baseUrl + "SQL/DeleteFileAttachment",
            data:
			"attachment_type=" + attch_type + "&" +
            "acct_ccanum=" + ccanum + "&" +
            "filename=" + filename +
			""
			,
            success: function (res) {

                if (SrvResultMsg.GetMsgType(res) != "error") {
                    // clear the value in textbox
                    $("#" + obj).attr('value', '');

                    // success
                    alert("ATTACHMENT DELETED!");
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

}

function Savedata() {
    DisplayPreloader();

    var acct_classification = "";
    acct_classification = $("#slt_acct_classification").attr("value");

    var request_id = "";
    request_id = $("#txt_request_id").attr("value");

    var acct_business_type = "";
    
    if (
        $("#txt_sole_owner_name").attr('value') != "" ||
        $("#txt_sole_nationality").attr('value') != "" ||
        $("#txt_sole_gen_manager").attr('value') != "" ||
        $("#txt_sole_fin_manager").attr('value') != ""
    ) {
        acct_business_type = "SoleProprietorship";
    } else if (
        $("#txt_partner_gen_manager").attr('value') != "" ||
        $("#txt_partner_fin_manager").attr('value') != "" ||
        $("#tbl_partner_list tr").length > 1
    ) {
        acct_business_type = "Partnership";
    } else if (
        $("#txt_corpo_inc_date").attr('value') != "" ||
        $("#txt_corpo_auth_cap_stock").attr('value') != "" ||
        $("#txt_corpo_subscb_cap_stock").attr('value') != "" ||
        $("#txt_corpo_paidin_cap_stock").attr('value') != "" ||
        $("#tbl_corpo_list tr").length > 1 ||
        $("#txt_corpo_ceo").attr('value') != "" ||
        $("#txt_corpo_vp_fin").attr('value') != "" ||
        $("#txt_corpo_gen_man").attr('value') != ""
    ) {
        acct_business_type = "Corporation";
    } else {
        acct_business_type = "";
    }

    // added field
    var acct_category_value = "";
    acct_category_value = $("#txt_category_value").attr("value");

    var acct_category_prem = "";
    acct_category_prem = $("#txt_category_prem").attr("value");

    var acct_business_class = "";
    acct_business_class = $("#txt_buss_class").attr("value");

    var acct_type_of_account = "";
    acct_type_of_account = $("#txt_type_of_account").attr("value_id");

    // account type
    var acct_type = "";
    if ($("#acc_type_direct").attr("checked") == "checked") {
        acct_type = "direct";
    } else {
        acct_type = "indirect";
    }

    // key account
    var acct_key_account = "";
    if ($("#acc_key_yes").attr("checked") == "checked") {
        acct_key_account = "1";
    } else {
        acct_key_account = "0";
    }

    // account code
    var acct_code = "";
    acct_code = $("#txt_acct_code").attr('value');

    // account class
    var acct_class = "";
    acct_class = $("#txt_acct_class").attr('value');

    // account name
    var acct_name = "";
    acct_name = $("#txt_acct_name").attr('value');

    // phone no
    var acct_phone_no = "";
    acct_phone_no = $("#txt_phone_no").attr('value');

    // phone 2
    var acct_phone_no_2 = "";
    acct_phone_no_2 = $("#txt_phone_no_2").attr('value');

    // cellphone
    var acct_cellphone = "";
    acct_cellphone = $("#txt_cellphone").attr('value');

    // account officer
    var acct_acct_officer = "";
    acct_acct_officer = $("#txt_acct_officer").attr('value');

    // fax
    var acct_fax_no = "";
    acct_fax_no = $("#txt_fax_no").attr('value');

    // territory
    var acct_territory = "";
    acct_territory = $("#txt_acct_territory").attr('value');

    // email
    var acct_email = "";
    acct_email = $("#txt_email_add").attr('value');

    // office hours
    var acct_office_hours = "";
    acct_office_hours = $("#txt_office_hours").attr('value');

    // area
    var acct_area = "";
    acct_area = $("#txt_area").attr('value');

    // store hours
    var acct_store_hours = "";
    acct_store_hours = $("#txt_store_hours").attr('value');

    // region
    var acct_region = "";
    acct_region = $("#txt_region").attr('value');

    // no. of years in business
    var acct_years_in_business = "";
    acct_years_in_business = $("#txt_yrs_business").attr('value');

    // no. of years w/ matimco
    var acct_years_with_matimco = "";
    acct_years_with_matimco = $("#txt_yrs_matimco").attr('value');

    // tax id
    var acct_tax_id = "";
    acct_tax_id = $("#txt_tax_id").attr('value');

    // vat no
    var acct_vat_no = "";
    acct_vat_no = $("#txt_vat_no").attr('value');

    // registered name
    var acct_reg_name = "";
    acct_reg_name = $("#txt_reg_name").attr('value');

    // business address
    var acct_business_add = "";
    acct_business_add = $("#txt_business_add").attr('value');

    // delivery address
    var acct_delivery_add = "";
    acct_delivery_add = $("#txt_delivery_add").attr('value');

    //		IF Sole Proprietorship
    // name of owner
    var sole_owner_name = "";
    sole_owner_name = $("#txt_sole_owner_name").attr('value');

    // nationality
    var sole_nationality = "";
    sole_nationality = $("#txt_sole_nationality").attr('value');

    // gen. manager
    var sole_gen_manager = "";
    sole_gen_manager = $("#txt_sole_gen_manager").attr('value');

    // finance manager
    var sole_fin_manager = "";
    sole_fin_manager = $("#txt_sole_fin_manager").attr('value');

    // others
    var sole_others = "";
    sole_others = $("#txt_sole_others").attr('value');

    var i = 0;
    var row_count

    //		 IF partnership
    // [partner, nationality, contributed capital] - must be two entries
    var list_of_partner = new Array();
    row_count = $("#tbl_partner_list tr").length;
    var loop_count = 0;
    $("#tbl_partner_list tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_partner.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                );
            }
        }
    );

    // gen. manager
    var partner_gen_manager = "";
    partner_gen_manager = $("#txt_partner_gen_manager").attr('value');

    // finance manager
    var partner_fin_manager = "";
    partner_fin_manager = $("#txt_partner_fin_manager").attr('value');

    // others
    var partner_others = "";
    partner_others = $("#txt_partner_others").attr('value');


    //		IF Corporation
    // date of inc.
    var corp_date_inc = "";
    corp_date_inc = $("#txt_corpo_inc_date").attr('value');

    // Auth capital stock
    var corp_auth_cap_stock = "";
    corp_auth_cap_stock = $("#txt_corpo_auth_cap_stock").attr('value');

    // subscribed capital stock
    var corp_subc_cap_stock = "";
    corp_subc_cap_stock = $("#txt_corpo_subscb_cap_stock").attr('value');

    // paid in capital stock
    var corp_paidin_cap_stock = "";
    corp_paidin_cap_stock = $("#txt_corpo_paidin_cap_stock").attr('value');

    // [major stock holders, nationality, %owned] - at least two
    var list_major_stockholder = new Array();
    row_count = $("#tbl_corpo_list tr").length;
    var loop_count = 0;
    $("#tbl_corpo_list tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_major_stockholder.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                );
            }
        }
    );

    // president
    var corp_ceo = "";
    corp_ceo = $("#txt_corpo_ceo").attr('value');

    // vp finance
    var corp_vp_fin = "";
    corp_vp_fin = $("#txt_corpo_vp_fin").attr('value');

    // gen. manager
    var corp_gen_man = "";
    corp_gen_man = $("#txt_corpo_gen_man").attr('value');

    // no. of employees
    var acct_num_employees = "";
    acct_num_employees = $("#txt_no_employees").attr('value');

    // [position, no. of employees]
    var list_of_employee_no = new Array();
    row_count = $("#tbl_emp_pos_list tr").length;
    var loop_count = 0;
    $("#tbl_emp_pos_list tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 2 && loop_count < row_count) {
                list_of_employee_no.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                );
            }
        }
    );

    // FILE ATTACHMENTS
    // articles of inc.
    var acct_article_of_inc = "";
    acct_article_of_inc = $("#txt_articles_of_inc").attr('value');

    // financial statements
    var acct_financial_statement = "";
    acct_financial_statement = $("#txt_financial_statement").attr('value');

    // income tax return
    var acct_itr = "";
    acct_itr = $("#txt_ITR").attr('value');

    // bor registration
    var acct_bir_reg = "";
    acct_bir_reg = $("#txt_bir_reg").attr('value');

    // latest business permit
    var acct_business_permit = "";
    acct_business_permit = $("#txt_business_permit").attr('value');

    // other attachment
    var acct_attch_other = "";
    acct_attch_other = $("#txt_attch_other").attr('value');

    /* Code added by Billy Jay (04/23/2015) */
    var acct_prop_credit_term_architectural_brand = "";
    acct_prop_credit_term_architectural_brand = $("#txt_credit_terms_architectural_brand").attr("value");

    var acct_prop_credit_term_eco_lumber = "";
    acct_prop_credit_term_eco_lumber = $("#txt_credit_terms_eco_lumber").attr("value");

    var acct_prop_credit_term_eco_plywood = "";
    acct_prop_credit_term_eco_plywood = $("#txt_credit_terms_eco_plywood").attr("value");

    // proposed credit terms remarks
    var acct_prop_credit_term_architectural_brand_remarks = "";
    acct_prop_credit_term_architectural_brand_remarks = $("#txt_credit_terms_architectural_brand_remarks").attr('value');

    var acct_prop_credit_term_eco_lumber_remarks = "";
    acct_prop_credit_term_eco_lumber_remarks = $("#txt_credit_terms_eco_lumber_remarks").attr('value');


    var acct_prop_credit_term_eco_plywood_remarks = "";
    acct_prop_credit_term_eco_plywood_remarks = $("#txt_credit_terms_eco_plywood_remarks").attr('value');

    // proposed order limit
    var acct_prop_order_limit_ab = "";
    acct_prop_order_limit_ab = $("#txt_order_limit_ab").attr('value');

    var acct_prop_order_limit_tr = "";
    acct_prop_order_limit_tr = $("#txt_order_limit_tr").attr('value');

    // proposed order limit remarks
    var acct_prop_order_limit_remarks_ab = "";
    acct_prop_order_limit_remarks_ab = $("#txt_order_limit_remarks_ab").attr('value');
    var acct_prop_order_limit_remarks_tr = "";
    acct_prop_order_limit_remarks_tr = $("#txt_order_limit_remarks_tr").attr('value');

    /* End Code added by Billy Jay (04/23/2015) */

    // proposed credit terms
    var acct_prop_credit_term = "";
    acct_prop_credit_term = $("#txt_credit_terms").attr('value');

    // proposed credit limit
    var acct_prop_credit_limit = "";
    acct_prop_credit_limit = $("#txt_credit_limit").attr('value');

    // proposed credit terms remarks
    var acct_prop_credit_term_remarks = "";
    acct_prop_credit_term_remarks = $("#txt_credit_terms_remarks").attr('value');

    // proposed credit limit remarks
    var acct_prop_credit_limit_remarks = "";
    acct_prop_credit_limit_remarks = $("#txt_credit_limit_remarks").attr('value');

    // [matwood]
    // code
    var acct_mw_price_code = "";
    acct_mw_price_code = $("#txt_mw_price_code").attr('value');

    // description
    var acct_mw_price_desc = "";
    acct_mw_price_desc = $("#txt_mw_price_desc").attr('value');

    // commision & discounts
    var acct_mw_price_commision_disc = "";
    acct_mw_price_commision_disc = $("#txt_mw_price_commision_disc").attr('value');

    // remarks
    var acct_mw_price_remarks = "";
    acct_mw_price_remarks = $("#txt_mw_price_remarks").attr('value');

    // [weatherwood]
    // code
    var acct_ww_price_code = "";
    acct_ww_price_code = $("#txt_ww_price_code").attr('value');

    // description
    var acct_ww_price_desc = "";
    acct_ww_price_desc = $("#txt_ww_price_desc").attr('value');

    // commision & discounts
    var acct_ww_price_commision_disc = "";
    acct_ww_price_commision_disc = $("#txt_ww_price_commision_disc").attr('value');

    // remarks
    var acct_ww_price_remarks = "";
    acct_ww_price_remarks = $("#txt_ww_price_remarks").attr('value');

    // [pcw]
    // code
    var acct_pw_price_code = "";
    acct_pw_price_code = $("#txt_pw_price_code").attr('value');

    // description
    var acct_pw_price_desc = "";
    acct_pw_price_desc = $("#txt_pw_price_desc").attr('value');

    // commision & discounts
    var acct_pw_price_commision_disc = "";
    acct_pw_price_commision_disc = $("#txt_pw_price_commision_disc").attr('value');

    // remarks
    var acct_pw_price_remarks = "";
    acct_pw_price_remarks = $("#txt_pw_price_remarks").attr('value');

    // [pcw frames]
    // code
    var acct_pwf_price_code = "";
    acct_pwf_price_code = $("#txt_pwf_price_code").attr('value');

    // description
    var acct_pwf_price_desc = "";
    acct_pwf_price_desc = $("#txt_pwf_price_desc").attr('value');

    // commision & discounts
    var acct_pwf_price_commision_disc = "";
    acct_pwf_price_commision_disc = $("#txt_pwf_price_commision_disc").attr('value');

    // remarks
    var acct_pwf_price_remarks = "";
    acct_pwf_price_remarks = $("#txt_pwf_price_remarks").attr('value');

    // [pcw regular]
    // code
    var acct_pwr_price_code = "";
    acct_pwr_price_code = $("#txt_pwr_price_code").attr('value');

    // description
    var acct_pwr_price_desc = "";
    acct_pwr_price_desc = $("#txt_pwr_price_desc").attr('value');

    // commision & discounts
    var acct_pwr_price_commision_disc = "";
    acct_pwr_price_commision_disc = $("#txt_pwr_price_commision_disc").attr('value');

    // remarks
    var acct_pwr_price_remarks = "";
    acct_pwr_price_remarks = $("#txt_pwr_price_remarks").attr('value');

    // [gudwood]
    // code
    var acct_gw_price_code = "";
    acct_gw_price_code = $("#txt_gw_price_code").attr('value');

    // description
    var acct_gw_price_desc = "";
    acct_gw_price_desc = $("#txt_gw_price_desc").attr('value');

    // commision & discounts
    var acct_gw_price_commision_disc = "";
    acct_gw_price_commision_disc = $("#txt_gw_price_commision_disc").attr('value');

    // remarks
    var acct_gw_price_remarks = "";
    acct_gw_price_remarks = $("#txt_gw_price_remarks").attr('value');

    // [trusswood]
    // code
    var acct_tw_price_code = "";
    acct_tw_price_code = $("#txt_tw_price_code").attr('value');

    // description
    var acct_tw_price_desc = "";
    acct_tw_price_desc = $("#txt_tw_price_desc").attr('value');

    // commision & discounts
    var acct_tw_price_commision_disc = "";
    acct_tw_price_commision_disc = $("#txt_tw_price_commision_disc").attr('value');

    // remarks
    var acct_tw_price_remarks = "";
    acct_tw_price_remarks = $("#txt_tw_price_remarks").attr('value');

    // [muzuwood]
    // code
    var acct_mz_price_code = "";
    acct_mz_price_code = $("#txt_mz_price_code").attr('value');

    // description
    var acct_mz_price_desc = "";
    acct_mz_price_desc = $("#txt_mz_price_desc").attr('value');

    // commision & discounts
    var acct_mz_price_commision_disc = "";
    acct_mz_price_commision_disc = $("#txt_mz_price_commision_disc").attr('value');

    // remarks
    var acct_mz_price_remarks = "";
    acct_mz_price_remarks = $("#txt_mz_price_remarks").attr('value');

    // [nuwood]
    // code
    var acct_nw_price_code = "";
    acct_nw_price_code = $("#txt_nw_price_code").attr('value');

    // description
    var acct_nw_price_desc = "";
    acct_nw_price_desc = $("#txt_nw_price_desc").attr('value');

    // commision & discounts
    var acct_nw_price_commision_disc = "";
    acct_nw_price_commision_disc = $("#txt_nw_price_commision_disc").attr('value');

    // remarks
    var acct_nw_price_remarks = "";
    acct_nw_price_remarks = $("#txt_nw_price_remarks").attr('value');

    // [ecofor treated]
    // code
    var acct_ec_price_code = "";
    acct_ec_price_code = $("#txt_ec_price_code").attr('value');

    // description
    var acct_ec_price_desc = "";
    acct_ec_price_desc = $("#txt_ec_price_desc").attr('value');

    // commision & discounts
    var acct_ec_price_commision_disc = "";
    acct_ec_price_commision_disc = $("#txt_ec_price_commision_disc").attr('value');

    // remarks
    var acct_ec_price_remarks = "";
    acct_ec_price_remarks = $("#txt_ec_price_remarks").attr('value');

    // [ecofor untreated]
    // code
    var acct_ecu_price_code = "";
    acct_ecu_price_code = $("#txt_ecu_price_code").attr('value');

    // description
    var acct_ecu_price_desc = "";
    acct_ecu_price_desc = $("#txt_ecu_price_desc").attr('value');

    // commision & discounts
    var acct_ecu_price_commision_disc = "";
    acct_ecu_price_commision_disc = $("#txt_ecu_price_commision_disc").attr('value');

    // remarks
    var acct_ecu_price_remarks = "";
    acct_ecu_price_remarks = $("#txt_ecu_price_remarks").attr('value');

    // socio economic class of cust
    var acct_socio_eco_class = "";
    acct_socio_eco_class = $("#txt_eco_class_of_customer").attr('value');

    // no. of outlets
    var acct_num_outlets = "";
    acct_num_outlets = $("#txt_no_of_outlets").attr('value');

    // [name of outlet, location, store size, warehouse size]
    var list_of_outlets = new Array();
    row_count = $("#tbl_outlet_list tr").length;
    var loop_count = 0;
    $("#tbl_outlet_list tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_outlets.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
                );
            }
        }
    );

    // [name, address, selling terms, est. monthly purchases]
    var list_of_major_customer = new Array();
    row_count = $("#tbl_mjcust_list tr").length;
    var loop_count = 0;
    $("#tbl_mjcust_list tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_major_customer.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
                );
            }
        }
    );

    // major line
    var acct_major_prod_line = "";
    acct_major_prod_line = $("#txt_prod_major_line").attr('value');

    // other prod. line
    var acct_other_prod_line = "";
    acct_other_prod_line = $("#txt_prod_other_line").attr('value');

    // plywood
    var acct_supplier_on_plywood = "";
    acct_supplier_on_plywood = $("#txt_const_mat_plywood").attr('value');

    // steel
    var acct_supplier_on_steel = "";
    acct_supplier_on_steel = $("#txt_const_mat_steel").attr('value');

    // cement
    var acct_supplier_on_cement = "";
    acct_supplier_on_cement = $("#txt_const_mat_cement").attr('value');

    // concrete hollowblock
    var acct_supplier_on_con_hollowblock = "";
    acct_supplier_on_con_hollowblock = $("#txt_const_mat_hb").attr('value');

    // others
    var acct_supplier_on_others = "";
    acct_supplier_on_others = $("#txt_const_mat_others").attr('value');


    // major vol.
    var acct_major_vol_business = "";
    acct_major_vol_business = $("#txt_major_vol_business").attr('value');

    // monthly wood vol.
    var acct_monthly_wood_vol = "";
    acct_monthly_wood_vol = $("#txt_wood_vol").attr('value');

    // discounts enjoyed
    var acct_discount_enjoyed = "";
    acct_discount_enjoyed = $("#txt_discount_enjoyed").attr('value');

    // [supplier, monthly vol., contact person, contact number, products usually purchased, credit terms, other deals offerd]
    var list_of_other_wood_suppliers = new Array();
    row_count = $("#tbl_wood_supplier tr").length;
    var loop_count = 0;
    $("#tbl_wood_supplier tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_other_wood_suppliers.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(5) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(6) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(7) input[type=text]").attr('value')
                );
            }
        }
    );

    var acct_iniPODetails = "";
    acct_iniPODetails = $("#txt_ini_po_details").attr("value");
    
    // 
    var params = {
        request_id: request_id,
        acct_classification: acct_classification,
        acct_category_value: acct_category_value,
        acct_category_prem: acct_category_prem,
        acct_business_class: acct_business_class, 
        acct_type_of_account: acct_type_of_account,
        acct_type: acct_type,
        acct_key_account: acct_key_account,
        acct_code: acct_code,
        acct_class: acct_class,
        acct_name: acct_name,
        acct_phone_no: acct_phone_no,

        acct_phone_no_2: acct_phone_no_2,
        acct_cellphone: acct_cellphone,

        acct_acct_officer: acct_acct_officer,
        acct_fax_no: acct_fax_no,
        acct_territory: acct_territory,
        acct_email: acct_email,
        acct_office_hours: acct_office_hours,
        acct_area: acct_area,
        acct_store_hours: acct_store_hours,
        acct_region: acct_region,
        acct_years_in_business: acct_years_in_business,
        acct_years_with_matimco: acct_years_with_matimco,
        acct_tax_id: acct_tax_id,
        acct_vat_no: acct_vat_no,
        acct_reg_name: acct_reg_name,
        acct_business_add: acct_business_add,
        acct_delivery_add: acct_delivery_add,
        acct_business_type: acct_business_type,
        sole_owner_name: sole_owner_name,
        sole_nationality: sole_nationality,
        sole_gen_manager: sole_gen_manager,
        sole_fin_manager: sole_fin_manager,
        sole_others: sole_others,
        list_of_partner: list_of_partner,
        partner_gen_manager: partner_gen_manager,
        partner_fin_manager: partner_fin_manager,
        partner_others: partner_others,
        corp_date_inc: corp_date_inc,
        corp_auth_cap_stock: corp_auth_cap_stock,
        corp_subc_cap_stock: corp_subc_cap_stock,
        corp_paidin_cap_stock: corp_paidin_cap_stock,
        list_major_stockholder: list_major_stockholder,
        corp_ceo: corp_ceo,
        corp_vp_fin: corp_vp_fin,
        corp_gen_man: corp_gen_man,
        acct_num_employees: acct_num_employees,
        list_of_employee_no: list_of_employee_no,
        acct_article_of_inc: acct_article_of_inc,
        acct_financial_statement: acct_financial_statement,
        acct_itr: acct_itr,
        acct_bir_reg: acct_bir_reg,
        acct_business_permit: acct_business_permit,
        acct_attch_other: acct_attch_other,
        /* Code added by Billy Jay (04/23/2015) */

        acct_prop_credit_term_architectural_brand: acct_prop_credit_term_architectural_brand,
        acct_prop_credit_term_eco_lumber: acct_prop_credit_term_eco_lumber,
        acct_prop_credit_term_eco_plywood: acct_prop_credit_term_eco_plywood,

        acct_prop_credit_term_architectural_brand_remarks: acct_prop_credit_term_architectural_brand_remarks,
        acct_prop_credit_term_eco_lumber_remarks: acct_prop_credit_term_eco_lumber_remarks,
        acct_prop_credit_term_eco_plywood_remarks: acct_prop_credit_term_eco_plywood_remarks,


        acct_prop_order_limit_ab: undoAddComma(acct_prop_order_limit_ab),
        acct_prop_order_limit_tr: undoAddComma(acct_prop_order_limit_tr),

        acct_prop_order_limit_remarks_ab: acct_prop_order_limit_remarks_ab,
        acct_prop_order_limit_remarks_tr: acct_prop_order_limit_remarks_tr,

        /* End Code added by Billy Jay (04/23/2015) */
        acct_prop_credit_term: acct_prop_credit_term,
        acct_prop_credit_limit: acct_prop_credit_limit,
        acct_prop_credit_term_remarks: acct_prop_credit_term_remarks,
        acct_prop_credit_limit_remarks: acct_prop_credit_limit_remarks,
        acct_mw_price_code: acct_mw_price_code,
        acct_mw_price_desc: acct_mw_price_desc,
        acct_mw_price_commision_disc: acct_mw_price_commision_disc,
        acct_mw_price_remarks: acct_mw_price_remarks,
        acct_ww_price_code: acct_ww_price_code,
        acct_ww_price_desc: acct_ww_price_desc,
        acct_ww_price_commision_disc: acct_ww_price_commision_disc,
        acct_ww_price_remarks: acct_ww_price_remarks,
        acct_pwf_price_code: acct_pwf_price_code,
        acct_pwf_price_desc: acct_pwf_price_desc,
        acct_pwf_price_commision_disc: acct_pwf_price_commision_disc,
        acct_pwf_price_remarks: acct_pwf_price_remarks,
        acct_pwr_price_code: acct_pwr_price_code,
        acct_pwr_price_desc: acct_pwr_price_desc,
        acct_pwr_price_commision_disc: acct_pwr_price_commision_disc,
        acct_pwr_price_remarks: acct_pwr_price_remarks,
        acct_gw_price_code: acct_gw_price_code,
        acct_gw_price_desc: acct_gw_price_desc,
        acct_gw_price_commision_disc: acct_gw_price_commision_disc,
        acct_gw_price_remarks: acct_gw_price_remarks,
        acct_tw_price_code: acct_tw_price_code,
        acct_tw_price_desc: acct_tw_price_desc,
        acct_tw_price_commision_disc: acct_tw_price_commision_disc,
        acct_tw_price_remarks: acct_tw_price_remarks,

        acct_mz_price_code: acct_mz_price_code,
        acct_mz_price_desc: acct_mz_price_desc,
        acct_mz_price_commision_disc: acct_mz_price_commision_disc,
        acct_mz_price_remarks: acct_mz_price_remarks,

        acct_nw_price_code: acct_nw_price_code,
        acct_nw_price_desc: acct_nw_price_desc,
        acct_nw_price_commision_disc: acct_nw_price_commision_disc,
        acct_nw_price_remarks: acct_nw_price_remarks,

        acct_ec_price_code: acct_ec_price_code,
        acct_ec_price_desc: acct_ec_price_desc,
        acct_ec_price_commision_disc: acct_ec_price_commision_disc,
        acct_ec_price_remarks: acct_ec_price_remarks,

        acct_ecu_price_code: acct_ecu_price_code,
        acct_ecu_price_desc: acct_ecu_price_desc,
        acct_ecu_price_commision_disc: acct_ecu_price_commision_disc,
        acct_ecu_price_remarks: acct_ecu_price_remarks,

        acct_socio_eco_class: acct_socio_eco_class,
        acct_num_outlets: acct_num_outlets,
        list_of_outlets: list_of_outlets,
        list_of_major_customer: list_of_major_customer,
        acct_major_prod_line: acct_major_prod_line,
        acct_other_prod_line: acct_other_prod_line,
        acct_supplier_on_plywood: acct_supplier_on_plywood,
        acct_supplier_on_steel: acct_supplier_on_steel,
        acct_supplier_on_cement: acct_supplier_on_cement,
        acct_supplier_on_con_hollowblock: acct_supplier_on_con_hollowblock,
        acct_supplier_on_others: acct_supplier_on_others,
        acct_major_vol_business: acct_major_vol_business,
        acct_monthly_wood_vol: acct_monthly_wood_vol,
        acct_discount_enjoyed: acct_discount_enjoyed,
        list_of_other_wood_suppliers: list_of_other_wood_suppliers,

        acct_ini_po_details: acct_iniPODetails //added line by BJD 05/31/2016
    };

    // send through ajx
    $.ajax({
        type: "POST", url: baseUrl + "Customer/AddCustomer",
        data: $.param(params, true),
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                window.location = baseUrl + "Document/AccountsDetails?ccanum=" + SrvResultMsg.GetMessage(res);
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
                HidePreloader();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });

}

/* delete current row */
function DelCurrRow(tbl_id, r_id) {
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}

function isRowIDExist(table, rowid) {
    var exist = false;
    $(table + " tr").each(function () {
        if ($(this).attr("RowId") != undefined && $(this).attr("RowId") == rowid) exist = true;
    }
    );

    return exist;
}

function AddEntryPartnership() {

    // check fields if empty
    /* first field */
    var val_one = $("#tbl_partner_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_partner_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = "";
    // var val_three = $("#tbl_partner_list tr:last td:nth-child(3) input[type=text]").attr("value");
    // if (val_three == "") {
    //     alert("FIELD CANNOT BE EMPTY!");
    //     return;
    // }

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_partner_list", rowid))

    $("#tbl_partner_list tr:last").prev().after(
		"<tr RowId=\"" + parseInt(rowid) + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_partner_list'," + parseInt(rowid) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_partner_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_partner_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_partner_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
}

function AddEntryCorporation() {
    // tbl_corpo_list

    // check fields if empty
    /* first field */
    var val_one = $("#tbl_corpo_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_corpo_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_corpo_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_corpo_list", rowid))

    $("#tbl_corpo_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_corpo_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    $("#tbl_corpo_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_corpo_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_corpo_list tr:last td:nth-child(3) input[type=text]").attr("value", "");

}

function AddEntryEmployeePos() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_emp_pos_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_emp_pos_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_two) == true) {
            alert("NO. OF EMPLOYEE MUST BE A NUMBER.");
            return;
        }
    }

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_emp_pos_list", rowid))

    $("#tbl_emp_pos_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_emp_pos_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_emp_pos_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_emp_pos_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
}

function AddEntryOutlet() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_outlet_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_outlet_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_outlet_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_outlet_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_outlet_list", rowid))

    $("#tbl_outlet_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_outlet_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_outlet_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_outlet_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_outlet_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_outlet_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
}


function AddEntryMajorCustomer() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_mjcust_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_mjcust_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_mjcust_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_mjcust_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_four) == true) {
            alert("ESTIMATED MONTHLY PURCHASES MUST BE A NUMBER.");
            return;
        }
    }

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_mjcust_list", rowid))

    $("#tbl_mjcust_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_mjcust_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_mjcust_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_mjcust_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_mjcust_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_mjcust_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
}

function AddEntryWoodSupplier() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_wood_supplier tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_wood_supplier tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_wood_supplier tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_wood_supplier tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fifth field */
    var val_five = $("#tbl_wood_supplier tr:last td:nth-child(5) input[type=text]").attr("value");
    if (val_five == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* sixth field */
    var val_six = $("#tbl_wood_supplier tr:last td:nth-child(6) input[type=text]").attr("value");
    if (val_six == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* seventh field */
    var val_seven = "";
    /*
    var val_seven = $("#tbl_wood_supplier tr:last td:nth-child(7) input[type=text]").attr("value");
    if (val_seven == "") {
    alert("Field cannot be empty!.");
    return;
    }
    */

    var rowid = 0;
    do rowid++;
    while (isRowIDExist("#tbl_wood_supplier", rowid))

    $("#tbl_wood_supplier tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_five + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_six + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_seven + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_wood_supplier'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_wood_supplier tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(4) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(5) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(6) input[type=text]").attr("value", "");
    $("#tbl_wood_supplier tr:last td:nth-child(7) input[type=text]").attr("value", "");
}

function Save_Doc() {
    var val_type = "";
    if ($("#acc_type_direct").attr("checked") == "checked") {
        val_type = "direct";
    } else {
        val_type = "indirect";
    }

    if (CheckCommisionAndDiscount() != "") {
        alert(CheckCommisionAndDiscount());
        return;
    }

    if ($("#slt_acct_classification").attr("value") == "REGULAR" && val_type == "direct") {
        if (CheckRequiredFieldsRegular()) {
            Savedata();
        }
    } else {
        if ($("#slt_acct_classification").attr("value") == "REGULAR") {
            if (CheckRequiredFieldsRegular_S()) {
                Savedata();
            }
        } else {
            if (CheckRequiredFieldsWalkin()) {
                Savedata();
            }
        }
        
    }
    /*
    if ($("#slt_acct_classification").attr("value") == "REGULAR") {
        if (CheckRequiredFieldsRegular()) {
            Savedata();
        }
    } else {
        if (CheckRequiredFieldsWalkin()) {
            Savedata();
        }
    }
    */
}

function CheckRequiredFieldsRegular_S() {

    var lacking_fields = "";

    // account code
    if ($("#txt_acct_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    // account name
    if ($("#txt_acct_name").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    // account officer
    if ($("#txt_acct_officer").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Officer"; } else { lacking_fields = "Account Officer"; }
    }

    // phone no
    if ($("#txt_phone_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Phone Number"; } else { lacking_fields = "Phone Number"; }
    }

    // territory
    if ($("#txt_acct_territory").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Territory"; } else { lacking_fields = "Territory"; }
    }

    // no. of years in the business
    if ($("#txt_yrs_business").attr('value') != "") {
        if (isNaN($("#txt_yrs_business").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years in business] must be a number."; } else { lacking_fields = "[No. of years in business] must be a number."; }
        }
    }

    // no. of years with matimco
    if ($("#txt_yrs_matimco").attr('value') != "") {
        if (isNaN($("#txt_yrs_matimco").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years with Matimco] must be a number."; } else { lacking_fields = "[No. of years with Matimco] must be a number."; }
        }
    }

    // check if integer [ No of Employees]
    if ($("#txt_no_employees").attr('value') != "") {
        if (isNaN($("#txt_no_employees").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "No. of employee must be a number"; } else { lacking_fields = "No.of employee must be a number"; }
        }
    }

    // check if integer [ No of outlets]
    if ($("#txt_no_of_outlets").attr('value') != "") {
        if (isNaN($("#txt_no_of_outlets").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Number of outlets must be a number"; } else { lacking_fields = "No.. of outlets must be a number"; }
        }
    }

    //Account Category Value Brands
    if ($("#txt_category_value").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Category Value Brands"; } else { lacking_fields = "Account Category Value Brands"; }
    }

    //Account Category Premium Brands
    if ($("#txt_category_prem").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Category Premium Brands"; } else { lacking_fields = "Account Category Premium Brands"; }
    }

    //Business Classification
    if ($("#txt_buss_class").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Business Classification"; } else { lacking_fields = "Business Classification"; }
    }

    //Type of Account
    if ($("#txt_type_of_account").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type of Account"; } else { lacking_fields = "Type of Account"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function CheckRequiredFieldsWalkin() {

    var lacking_fields = "";

    // account code
    if ($("#txt_acct_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    // account name
    if ($("#txt_acct_name").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    // account officer
    if ($("#txt_acct_officer").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Officer"; } else { lacking_fields = "Account Officer"; }
    }

    // phone no
    if ($("#txt_phone_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Phone Number"; } else { lacking_fields = "Phone Number"; }
    }

    // territory
    if ($("#txt_acct_territory").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Territory"; } else { lacking_fields = "Territory"; }
    }

    // no. of years in the business
    if ($("#txt_yrs_business").attr('value') != "") {
        if (isNaN($("#txt_yrs_business").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years in business] must be a number."; } else { lacking_fields = "[No. of years in business] must be a number."; }
        }
    }

    // tax id
    if ($("#txt_tax_id").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tax ID"; } else { lacking_fields = "Tax ID"; }
    }

    // no. of years with matimco
    if ($("#txt_yrs_matimco").attr('value') != "") {
        if (isNaN($("#txt_yrs_matimco").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years with Matimco] must be a number."; } else { lacking_fields = "[No. of years with Matimco] must be a number."; }
        }
    }

    // check if integer [ No of Employees]
    if ($("#txt_no_employees").attr('value') != "") {
        if (isNaN($("#txt_no_employees").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "No. of employee must be a number"; } else { lacking_fields = "No.of employee must be a number"; }
        }
    }

    // check if integer [ No of outlets]
    if ($("#txt_no_of_outlets").attr('value') != "") {
        if (isNaN($("#txt_no_of_outlets").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Number of outlets must be a number"; } else { lacking_fields = "No.. of outlets must be a number"; }
        }
    }

    //BIR Attachment
    if ($("txt_bir_reg").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "BIR Registration Form Attachment"; } else { lacking_fields = "BIR Registration Form Attachment"; }
    }

    //Customer Info Sheet Attachment
    if ($("#txt_articles_of_inc").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Customer Info Sheet Attachment"; } else { lacking_fields = "Customer Info Sheet Attachment"; }
    }

    /* Code added by Billy Jay (04/23/2015) */

    //if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
        // Proposed credit terms
        if ($("#txt_credit_terms_architectural_brand").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Architectural Brand Credit Terms"; } else { lacking_fields = "Architectural Brand Credit Terms"; }
        }

        if ($("#txt_credit_terms_eco_lumber").attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Lumber Credit Terms"; } else { lacking_fields = "Ecofor Lumber Credit Terms"; }
        }

        //        if ($("#txt_credit_terms_eco_plywood").attr("value") == "") {
        //            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Plywood Credit Terms"; } else { lacking_fields = "Ecofor Plywood Credit Terms"; }
        //        }

        // ORDER LIMIT
        if ($("#txt_order_limit_ab").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Terms"; } else { lacking_fields = "AB Order Terms"; }
        }
        if ($("#txt_order_limit_tr").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Terms"; } else { lacking_fields = "TR Order Terms"; }
        }
  //  }

    /* End Code added (04/23/2015) */

    // CREDIT LIMIT
    if ($("#txt_credit_terms").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Terms"; } else { lacking_fields = "Credit Terms"; }
    }

    // proposed credit limit
    if ($("#txt_credit_limit").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit"; } else { lacking_fields = "Credit Limit"; }
    }

    // proposed price list code for matwood
    if ($("#txt_mw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "MW Price Code"; } else { lacking_fields = "MW Price Code"; }
    }

    // proposed price list code for weatherwood
    if ($("#txt_ww_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "WW Price Code"; } else { lacking_fields = "WW Price Code"; }
    }

    // proposed price list code for pcw
    if ($("#txt_pw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PW Price Code"; } else { lacking_fields = "PW Price Code"; }
    }

    // proposed price list code for pcw frames
    if ($("#txt_pwf_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PW Frames Price Code"; } else { lacking_fields = "PW Frames Price Code"; }
    }

    // proposed price list code for pcw regular
    if ($("#txt_pwr_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PW Regular Items Price Code"; } else { lacking_fields = "PW Regular Items Price Code"; }
    }

    // proposed price list code for gudwood
    if ($("#txt_gw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "GW Price Code"; } else { lacking_fields = "GW Price Code"; }
    }

    // proposed price list code for gudwood
    if ($("#txt_tw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TW Price Code"; } else { lacking_fields = "TW Price Code"; }
    }

    // proposed price list code for muzuwood
    if ($("#txt_mz_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "MZ Price Code"; } else { lacking_fields = "MZ Price Code"; }
    }

    // proposed price list code for nuwood
    if ($("#txt_nw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "NW Price Code"; } else { lacking_fields = "NW Price Code"; }
    }

    // proposed price list code for ecofor treated
    if ($("#txt_ec_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "EC Price Code"; } else { lacking_fields = "EC Price Code"; }
    }

    // proposed price list code for ecofor untreated
    if ($("#txt_ecu_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "ECU Price Code"; } else { lacking_fields = "ECU Price Code"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function CheckRequiredFieldsRegular() {
    var lacking_fields = "";

    // ** MAIN INFO **

    // account code
    if ($("#txt_acct_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    // account name
    if ($("#txt_acct_name").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    // account officer
    if ($("#txt_acct_officer").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Officer"; } else { lacking_fields = "Account Officer"; }
    }

    // phone no
    if ($("#txt_phone_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Phone Number"; } else { lacking_fields = "Phone Number"; }
    }

    // territory
    if ($("#txt_acct_territory").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Territory"; } else { lacking_fields = "Territory"; }
    }

    // no. of years in the business
    if ($("#txt_yrs_business").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "No. of years in Business"; } else { lacking_fields = "No. of years in Business"; }
    } else {
        if (isNaN($("#txt_yrs_business").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years in Business] is not a number"; } else { lacking_fields = "[No. of years in Business] is not a number"; }
        }
    }

    // no. of years with matimco
    if ($("#txt_yrs_matimco").attr('value') != "") {
        if (isNaN($("#txt_yrs_matimco").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[No. of years w/ MATIMCO] is not a number"; } else { lacking_fields = "[No. of years w/ MATIMCO] is not a number"; }
        }
    }

    // tax id
    if ($("#txt_tax_id").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tax ID"; } else { lacking_fields = "Tax ID"; }
    }

    // vat reg no
    if ($("#txt_vat_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Vat Registration No."; } else { lacking_fields = "Vat Registration No."; }
    }

    // registered name
    if ($("#txt_reg_name").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Registered Name"; } else { lacking_fields = "Registered Name"; }
    }

    // business address
    if ($("#txt_business_add").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Business Address"; } else { lacking_fields = "Business Address"; }
    }

    // delivery address
    if ($("#txt_delivery_add").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Delivery Address"; } else { lacking_fields = "Delivery Address"; }
    }

    var SelectedBusinessType = "";
    if (
        $("#txt_sole_owner_name").attr('value') != "" ||
        $("#txt_sole_nationality").attr('value') != "" ||
        $("#txt_sole_gen_manager").attr('value') != "" ||
        $("#txt_sole_fin_manager").attr('value') != ""
    ) {
        SelectedBusinessType = "SoleProprietorship";
    } else if (
        $("#txt_partner_gen_manager").attr('value') != "" ||
        $("#txt_partner_fin_manager").attr('value') != "" ||
        $("#tbl_partner_list tr").length > 2
    ) {
        SelectedBusinessType = "Partnership";
    } else if (
        $("#txt_corpo_inc_date").attr('value') != "" ||
        $("#txt_corpo_auth_cap_stock").attr('value') != "" ||
        $("#txt_corpo_subscb_cap_stock").attr('value') != "" ||
        $("#txt_corpo_paidin_cap_stock").attr('value') != "" ||
        $("#tbl_corpo_list tr").length > 2 ||
        $("#txt_corpo_ceo").attr('value') != "" ||
        $("#txt_corpo_vp_fin").attr('value') != "" ||
        $("#txt_corpo_gen_man").attr('value') != ""
    ) {
        SelectedBusinessType = "Corporation";
    } else {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PLEASE FILL IN THE DATA UNDER BUSINESS TYPE"; } else { lacking_fields = "PLEASE FILL IN THE DATA UNDER BUSINESS TYPE"; }
    }

    //		IF sole proprietorship
    if (SelectedBusinessType == "SoleProprietorship") {
        // name of owner
        if ($("#txt_sole_owner_name").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Sole] Owner Name"; } else { lacking_fields = "[Sole] Owner Name"; }
        }

        // nationality
        if ($("#txt_sole_nationality").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Sole] Nationality"; } else { lacking_fields = "[Sole] Nationality"; }
        }

        
    }

    //		IF partnership
    if (SelectedBusinessType == "Partnership") {
        // [partner, nationality, contributed capital] - must be two entries
        // -- count the rows
        if (($("#tbl_partner_list tr").length - 2) != 2) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "List of partnership must be 2"; } else { lacking_fields = "List of partnership must be 2"; }
        }

        
    }

    //		IF Corporation
    if (SelectedBusinessType == "Corporation") {
        

        // president CEO
        if ($("#txt_corpo_ceo").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Corp] CEO"; } else { lacking_fields = "[Corp] CEO"; }
        }

        
    }

    // check if integer [ No of Employees]
    if ($("#txt_no_employees").attr('value') != "") {
        if (isNaN($("#txt_no_employees").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "No. of employees must be a number"; } else { lacking_fields = "No. of employees must be a number"; }
        }
    }

    // check if integer [ No of outlets]
    if ($("#txt_no_of_outlets").attr('value') != "") {
        if (isNaN($("#txt_no_of_outlets").attr('value')) == true) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "No. of outlets must be a number"; } else { lacking_fields = "No. of outlets must be a number"; }
        }
    }

    /* Code added by Billy Jay (04/23/2015) */

   // if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
        // Proposed credit terms
        if ($("#txt_credit_terms_architectural_brand").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Architectural Brand Credit Terms"; } else { lacking_fields = "Architectural Brand Credit Terms"; }
        }

        if ($("#txt_credit_terms_eco_lumber").attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Lumber Credit Terms"; } else { lacking_fields = "Ecofor Lumber Credit Terms"; }
        }

        //        if ($("#txt_credit_terms_eco_plywood").attr("value") == "") {
        //            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Plywood Credit Terms"; } else { lacking_fields = "Ecofor Plywood Credit Terms"; }
        //        }

        // ORDER LIMIT
        if ($("#txt_order_limit_ab").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Terms"; } else { lacking_fields = "AB Order Terms"; }
        }
        if ($("#txt_order_limit_tr").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Terms"; } else { lacking_fields = "TR Order Terms"; }
        }
   // }

    /* End Code added (04/23/2015) */

    // ** BUSINESS INFO **
    // Proposed creidt terms
    if ($("#txt_credit_terms").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Terms"; } else { lacking_fields = "Credit Terms"; }
    }

    // proposed credit limit
    if ($("#txt_credit_limit").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit"; } else { lacking_fields = "Credit Limit"; }
    }

    // proposed price list code for matwood
    if ($("#txt_mw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Matwood Price Code"; } else { lacking_fields = "Matwood Price Code"; }
    }

    // proposed price list code for weatherwood
    if ($("#txt_ww_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "WeatherWood Price Code"; } else { lacking_fields = "WeatherWood Price Code"; }
    }

    // proposed price list code for pcw
    if ($("#txt_pw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PCW Price Code"; } else { lacking_fields = "PCW Price Code"; }
    }

    // proposed price list code for pcw frames
    if ($("#txt_pwf_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PCW Frames Price Code"; } else { lacking_fields = "PCW Frames Price Code"; }
    }

    // proposed price list code for pcw regular items
    if ($("#txt_pwr_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PCW Regular Items Price Code"; } else { lacking_fields = "PCW Regular Items Price Code"; }
    }

    // proposed price list code for gudwood
    if ($("#txt_gw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "GudWood Price Code"; } else { lacking_fields = "GudWood Price Code"; }
    }

    // proposed price list code for trusswood
    if ($("#txt_tw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TrussWood Price Code"; } else { lacking_fields = "TrussWood Price Code"; }
    }

    // proposed price list code for muzuwood
    if ($("#txt_mz_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "MZ Price Code"; } else { lacking_fields = "MZ Price Code"; }
    }

    // proposed price list code for nuwood
    if ($("#txt_nw_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "NW Price Code"; } else { lacking_fields = "NW Price Code"; }
    }

    // proposed price list code for ecofor treated
    if ($("#txt_ec_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "EC Price Code"; } else { lacking_fields = "EC Price Code"; }
    }

    // proposed price list code for ecofor untreated
    if ($("#txt_ecu_price_code").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "ECU Price Code"; } else { lacking_fields = "ECU Price Code"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function SwitchRequiredFields(val_classification) {

    RemoveClass();

    var val_type = "";
    if ($("#acc_type_direct").attr("checked") == "checked") {
        val_type = "direct";
    } else {
        val_type = "indirect";
    }

    if (val_classification == "REGULAR") {
        $("#txt_tax_id").removeAttr("value");
        if (val_type == "direct") {
            MakreReqL();
        } else {
            markReqRegS();
        }
    } else {
        $("#txt_tax_id").attr("value",$("#txt_tax_id").attr("default_value"));
        MakreReqS();
    }
}

function SwitchREQFields() {

}

function MakreReqL() {
    $("#txt_acct_code").addClass("required_fields");
    $("#txt_acct_class").addClass("required_fields");
    $("#txt_acct_name").addClass("required_fields");
    $("#txt_acct_officer").addClass("required_fields");
    $("#txt_phone_no").addClass("required_fields");
    $("#txt_acct_territory").addClass("required_fields");
    $("#txt_yrs_business").addClass("required_fields");
    $("#txt_tax_id").addClass("required_fields");
    $("#txt_vat_no").addClass("required_fields");
    $("#txt_reg_name").addClass("required_fields");
    $("#txt_business_add").addClass("required_fields");
    $("#txt_delivery_add").addClass("required_fields");

    $("#txt_sole_owner_name").addClass("required_fields");
    $("#txt_sole_nationality").addClass("required_fields");

    // $("#tbl_partner_list tr td input[type=text]").addClass("required_fields");
    $("#tbl_partner_list tr td:nth-child(1) input[type=text]").addClass("required_fields");
    $("#tbl_partner_list tr td:nth-child(2) input[type=text]").addClass("required_fields");

    $("#txt_corpo_ceo").addClass("required_fields");

    $("#txt_credit_terms_architectural_brand").addClass("required_fields");
    $("#txt_credit_terms_eco_lumber").addClass("required_fields");
    $("#txt_order_limit_ab").addClass("required_fields");
    $("#txt_order_limit_tr").addClass("required_fields");

    $("#txt_credit_terms").addClass("required_fields");
    $("#txt_credit_limit").addClass("required_fields");
    $("#txt_mw_price_code").addClass("required_fields");
    $("#txt_mw_price_desc").addClass("required_fields");
    $("#txt_ww_price_code").addClass("required_fields");
    $("#txt_ww_price_desc").addClass("required_fields");
    $("#txt_pw_price_code").addClass("required_fields");
    $("#txt_pw_price_desc").addClass("required_fields");
    $("#txt_pwf_price_code").addClass("required_fields");
    $("#txt_pwf_price_desc").addClass("required_fields");
    $("#txt_pwr_price_code").addClass("required_fields");
    $("#txt_pwr_price_desc").addClass("required_fields");
    $("#txt_gw_price_code").addClass("required_fields");
    $("#txt_gw_price_desc").addClass("required_fields");
    $("#txt_tw_price_code").addClass("required_fields");
    $("#txt_tw_price_desc").addClass("required_fields");

    $("#txt_mz_price_code").addClass("required_fields");
    $("#txt_mz_price_desc").addClass("required_fields");

    $("#txt_nw_price_code").addClass("required_fields");
    $("#txt_nw_price_desc").addClass("required_fields");

    $("#txt_ec_price_code").addClass("required_fields");
    $("#txt_ec_price_desc").addClass("required_fields");

    $("#txt_ecu_price_code").addClass("required_fields");
    $("#txt_ecu_price_desc").addClass("required_fields");

    $("#txt_category_value").addClass("required_fields");
    $("#txt_category_prem").addClass("required_fields");
    $("#txt_buss_class").addClass("required_fields");
    $("#txt_type_of_account").addClass("required_fields");

    $("#txt_bir_reg").addClass("required_fields");
    // txt_articles_of_inc was renamed into [Customer Info Sheet]
    $("#txt_articles_of_inc").addClass("required_fields");
}

function markReqRegS() {
    $("#txt_acct_code").addClass("required_fields");
    $("#txt_acct_class").addClass("required_fields");
    $("#txt_acct_name").addClass("required_fields");
    $("#txt_acct_officer").addClass("required_fields");
    $("#txt_fax_no").addClass("required_fields");
    $("#txt_phone_no").addClass("required_fields");
    $("#txt_acct_territory").addClass("required_fields");

    $("#txt_bir_reg").addClass("required_fields");
    // txt_articles_of_inc was renamed into [Customer Info Sheet]
    $("#txt_articles_of_inc").addClass("required_fields");
}

function MakreReqS() {
    $("#txt_acct_code").addClass("required_fields");
    $("#txt_acct_class").addClass("required_fields");
    $("#txt_acct_name").addClass("required_fields");
    $("#txt_acct_officer").addClass("required_fields");
    $("#txt_phone_no").addClass("required_fields");
    $("#txt_acct_territory").addClass("required_fields");

    $("#txt_credit_terms_architectural_brand").addClass("required_fields");
    $("#txt_credit_terms_eco_lumber").addClass("required_fields");
    $("#txt_order_limit_ab").addClass("required_fields");
    $("#txt_order_limit_tr").addClass("required_fields");

    $("#txt_credit_terms").addClass("required_fields");
    $("#txt_credit_limit").addClass("required_fields");
    $("#txt_mw_price_code").addClass("required_fields");
    $("#txt_mw_price_desc").addClass("required_fields");
    $("#txt_ww_price_code").addClass("required_fields");
    $("#txt_ww_price_desc").addClass("required_fields");
    $("#txt_pw_price_code").addClass("required_fields");
    $("#txt_pw_price_desc").addClass("required_fields");
    $("#txt_pwf_price_code").addClass("required_fields");
    $("#txt_pwf_price_desc").addClass("required_fields");
    $("#txt_pwr_price_code").addClass("required_fields");
    $("#txt_pwr_price_desc").addClass("required_fields");
    $("#txt_gw_price_code").addClass("required_fields");
    $("#txt_gw_price_desc").addClass("required_fields");
    $("#txt_tw_price_code").addClass("required_fields");
    $("#txt_tw_price_desc").addClass("required_fields");


    $("#txt_mz_price_code").addClass("required_fields");
    $("#txt_mz_price_desc").addClass("required_fields");

    $("#txt_nw_price_code").addClass("required_fields");
    $("#txt_nw_price_desc").addClass("required_fields");

    $("#txt_ec_price_code").addClass("required_fields");
    $("#txt_ec_price_desc").addClass("required_fields");

    $("#txt_ecu_price_code").addClass("required_fields");
    $("#txt_ecu_price_desc").addClass("required_fields");

    $("#txt_category_value").addClass("required_fields");
    $("#txt_category_prem").addClass("required_fields");
    $("#txt_buss_class").addClass("required_fields");
    $("#txt_type_of_account").addClass("required_fields");

    $("#txt_category_value").addClass("required_fields");
    $("#txt_category_prem").addClass("required_fields");
    $("#txt_buss_class").addClass("required_fields");
    $("#txt_type_of_account").addClass("required_fields");

    $("#txt_bir_reg").addClass("required_fields");
    // txt_articles_of_inc was renamed into [Customer Info Sheet]
    $("#txt_articles_of_inc").addClass("required_fields");
}

function RemoveClass() {
    $("#txt_acct_code").removeClass("required_fields");
    $("#txt_acct_class").removeClass("required_fields");
    $("#txt_acct_name").removeClass("required_fields");
    $("#txt_acct_officer").removeClass("required_fields");
    $("#txt_fax_no").removeClass("required_fields");
    $("#txt_phone_no").removeClass("required_fields");
    $("#txt_acct_territory").removeClass("required_fields");
    $("#txt_yrs_business").removeClass("required_fields");
    $("#txt_tax_id").addClass("required_fields");
    $("#txt_vat_no").removeClass("required_fields");
    $("#txt_reg_name").removeClass("required_fields");
    $("#txt_business_add").removeClass("required_fields");
    $("#txt_delivery_add").removeClass("required_fields");
    $("#txt_sole_owner_name").removeClass("required_fields");
    $("#txt_sole_nationality").removeClass("required_fields");
    $("#txt_sole_gen_manager").removeClass("required_fields");
    $("#txt_sole_fin_manager").removeClass("required_fields");
    $("#txt_sole_others").removeClass("required_fields");
    $("#tbl_partner_list tr td input[type=text]").removeClass("required_fields");
    $("#txt_partner_gen_manager").removeClass("required_fields");
    $("#txt_partner_fin_manager").removeClass("required_fields");
    $("#txt_partner_others").removeClass("required_fields");
    $("#txt_corpo_inc_date").removeClass("required_fields");
    $("#txt_corpo_auth_cap_stock").removeClass("required_fields");
    $("#txt_corpo_subscb_cap_stock").removeClass("required_fields");
    $("#txt_corpo_paidin_cap_stock").removeClass("required_fields");
    $("#tbl_corpo_list tr td input[type=text]").removeClass("required_fields");
    $("#txt_corpo_ceo").removeClass("required_fields");
    $("#txt_corpo_vp_fin").removeClass("required_fields");
    $("#txt_corpo_gen_man").removeClass("required_fields");
    $("#txt_credit_terms").removeClass("required_fields");
    $("#txt_credit_limit").removeClass("required_fields");

    $("#txt_credit_terms_architectural_brand").removeClass("required_fields");
    $("#txt_credit_terms_eco_lumber").removeClass("required_fields");
    $("#txt_order_limit_ab").removeClass("required_fields");
    $("#txt_order_limit_tr").removeClass("required_fields");

    $("#txt_mw_price_code").removeClass("required_fields");
    $("#txt_mw_price_desc").removeClass("required_fields");
    $("#txt_ww_price_code").removeClass("required_fields");
    $("#txt_ww_price_desc").removeClass("required_fields");
    $("#txt_pw_price_code").removeClass("required_fields");
    $("#txt_pw_price_desc").removeClass("required_fields");
    $("#txt_pwf_price_code").removeClass("required_fields");
    $("#txt_pwf_price_desc").removeClass("required_fields");
    $("#txt_pwr_price_code").removeClass("required_fields");
    $("#txt_pwr_price_desc").removeClass("required_fields");
    $("#txt_gw_price_code").removeClass("required_fields");
    $("#txt_gw_price_desc").removeClass("required_fields");
    $("#txt_tw_price_code").removeClass("required_fields");
    $("#txt_tw_price_desc").removeClass("required_fields");

    $("#txt_mz_price_code").removeClass("required_fields");
    $("#txt_mz_price_desc").removeClass("required_fields");
    $("#txt_nw_price_code").removeClass("required_fields");
    $("#txt_nw_price_desc").removeClass("required_fields");
    $("#txt_ec_price_code").removeClass("required_fields");
    $("#txt_ec_price_desc").removeClass("required_fields");
    $("#txt_ecu_price_code").removeClass("required_fields");
    $("#txt_ecu_price_desc").removeClass("required_fields");


    $("#txt_category_value").addClass("required_fields");
    $("#txt_category_prem").addClass("required_fields");
    $("#txt_buss_class").addClass("required_fields");
    $("#txt_type_of_account").addClass("required_fields");

    $("#txt_bir_reg").removeClass("required_fields");
    $("#txt_articles_of_inc").removeClass("required_fields");
}

function Cancel() {
    var isCancelled = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (isCancelled) {
        window.history.back()
    }
}

function txt_tax_id_onkeypress(e) {
    var keycode = (e.keyCode) ? e.keyCode : e.which;
    if (keycode == 8) return true;

    if (!(keycode > 47 && keycode < 58) || $("#txt_tax_id").attr('value').length == 15) {
        return false;
    } else {
        if ($("#txt_tax_id").attr('value').length == 3 || $("#txt_tax_id").attr('value').length == 7 || $("#txt_tax_id").attr('value').length == 11) {
            $("#txt_tax_id").attr('value', $("#txt_tax_id").attr('value') + '-');
        }
    }
    return true;
}

function InputNumberOnly(e, obj) {
    var keycode = (e.keyCode) ? e.keyCode : e.which;
    if (keycode == 8) return true;

    if (!((keycode > 47 && keycode < 58) || keycode == 46)) {
        return false;
    } else {
        if (keycode == 46) {
            for (i = 0; i < $("#" + obj).attr('value').length; i++) {
                if ($("#" + obj).attr('value').charAt(i) == ".")
                    return false;
            }
        }
    }
    return true;
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
		    if ((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105)) {
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

function CheckCommisionAndDiscount() {
    var error_message = "";

    if ($("#txt_mw_price_commision_disc").attr("value") != "" && $("#txt_mw_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON MATWOOD PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON MATWOOD PRICELIST IS EMPTY\n"; }
    }

    if ($("#txt_ww_price_commision_disc").attr("value") != "" && $("#txt_ww_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON WEATHERWOOD PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON WEATHERWOOD PRICELIST IS EMPTY\n"; }
    }

    if ($("#txt_pwf_price_commision_disc").attr("value") != "" && $("#txt_pwf_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON PCW FRAMES PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON PCW FRAMES PRICELIST IS EMPTY\n"; }
    }

    if ($("#txt_pwr_price_commision_disc").attr("value") != "" && $("#txt_pwr_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON PCW REGULAR PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON PCW REGULAR PRICELIST IS EMPTY\n"; }
    }

    if ($("#txt_gw_price_commision_disc").attr("value") != "" && $("#txt_gw_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON GUDWOOD PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON GUDWOOD PRICELIST IS EMPTY\n"; }
    }

    if ($("#txt_tw_price_commision_disc").attr("value") != "" && $("#txt_tw_price_remarks").attr("value") == "") {
        if (error_message != "") { error_message = error_message + "REMARKS ON TRUSSWOOD PRICELIST IS EMPTY\n"; } else { error_message = "REMARKS ON TRUSSWOOD PRICELIST IS EMPTY\n"; }
    }

    return error_message;
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
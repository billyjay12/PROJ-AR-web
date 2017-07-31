var ns4;
var isProcessing = false;
var isProcessing_ = false;

function AddDataTBL(obj, col1, col2, col3, col4, col5, col6, col7, col8) {
	var str_rows = "";

	var rowid = $("#" + obj + " tr").length - 2;
	rowid++;

	str_rows = str_rows + "<tr RowId=\"" + rowid + "\">";

	var str_td_row1 = "<td><input style=\"width:95%;\" type=\"text\" value=\"";
	var str_td_row2 = "\" readonly=readonly class=\"readonly_fields\" /></td>";

	if (col1 != undefined) str_rows = str_rows + str_td_row1 + col1 + str_td_row2;
	if (col2 != undefined) str_rows = str_rows + str_td_row1 + col2 + str_td_row2;
	if (col3 != undefined) str_rows = str_rows + str_td_row1 + col3 + str_td_row2;
	if (col4 != undefined) str_rows = str_rows + str_td_row1 + col4 + str_td_row2;
	if (col5 != undefined) str_rows = str_rows + str_td_row1 + col5 + str_td_row2;
	if (col6 != undefined) str_rows = str_rows + str_td_row1 + col6 + str_td_row2;
	if (col7 != undefined) str_rows = str_rows + str_td_row1 + col7 + str_td_row2;
	if (col8 != undefined) {
		if (obj == "tbl_bank_list") str_rows = str_rows + str_td_row1 + col8 + "\" readonly=readonly tobubble=\"tobubble\" class=\"readonly_fields\" /></td>";
		else str_rows = str_rows + str_td_row1 + col8 + str_td_row2;
	}

	str_rows = str_rows + "<td><a href=\"javascript:DelCurrRow('" + obj + "'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>";

	str_rows = str_rows + "</tr>";

	$("#" + obj + " tr:last").prev().after(str_rows);
}

/* delete current row */
function DelCurrRow(tbl_id, r_id) {
	$("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}

function AddEntryCommon(obj_list) {
	var obj = obj_list;
	var values = new Array();

	values.push($("#" + obj + " tr:last").find("td:nth-child(1) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(2) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(3) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(4) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(5) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(6) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(7) input[type=text]").attr("value"));
	values.push($("#" + obj + " tr:last").find("td:nth-child(8) input[type=text]").attr("value"));

	var toskip = new Array("NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO");

	// skip
	if (obj == "tbl_partner_list") {
		toskip[2] = "YES";
	}

	for (var i = 0; i < values.length; i++) {
		if (values[i] != undefined) {
			if (values[i] == "" && toskip[i] == "NO") {
				alert("FIELD CANNOT BE EMPTY!");
				return;
			}
		}
	}

	// additional checking
	if (obj == "tbl_mjcust_list") {
		if (isNaN(values[3]) == true) {
			alert("ESTIMATED MONTHLY PURCHASES MUST BE A NUMBER.");
			return;
		}
	}

	if (obj == "tbl_asset_list") {
		if (isNaN(values[3]) == true) {
			alert("% OF OWNERSHIP MUST BE A NUMBER.");
			return;
		}
	}

	if (obj == "tbl_vehicle_list") {
		if (isNaN(values[2]) == true) {
			alert("QUANTITY FIELD MUST BE A NUMBER.");
			return;
		}
	}

	var last_id = $("#" + obj + " tr:last").prev().attr("RowId");
	var new_id = 0;
	if (last_id != undefined) {
		new_id = parseInt(last_id) + 1;
	}

	var str_tbl_row = "<tr RowId=\"" + (parseInt(new_id)) + "\">";
	for (var i = 0; i < values.length; i++) {
		if (values[i] != undefined) {
			if (obj == "tbl_bank_list" && i == 7) {
				str_tbl_row = str_tbl_row + "<td><input style=\"width:98%;\" type=\"text\" value=\"" + values[i] + "\" readonly=readonly class=\"readonly_fields\" tobubble=\"tobubble\" /></td>";
			} else {
				str_tbl_row = str_tbl_row + "<td><input style=\"width:98%;\" type=\"text\" value=\"" + values[i] + "\" readonly=readonly class=\"readonly_fields\" /></td>";
			}
		} else {
			break;
		}
	}
	str_tbl_row = str_tbl_row + "<td><a href=\"javascript:DelCurrRow('" + obj + "'," + (parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>";

	$("#" + obj + " tr:last").prev().after(str_tbl_row);
	$("#" + obj + " tr:last").find("td input[type=text]").attr("value", "");
}

function AddEntryEmployeePos() {
	var obj = "tbl_emp_pos_list";
	// check fields if empty
	/* first field */
	var val_one = $("#" + obj + " tr:last td:nth-child(1) input[type=text]").attr("value");
	if (val_one == "") {
		alert("FIELD CANNOT BE EMPTY!");
		return;
	}

	/* second field */
	var val_two = $("#" + obj + " tr:last td:nth-child(2) input[type=text]").attr("value");
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

	// get last id
	var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
	var new_id = 0;
	if (last_id != undefined) {
		new_id = parseInt(last_id) + 1;
	}

	var rowid = $("#" + obj + " tr").length - 2;
	rowid++;
	$("#tbl_emp_pos_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_emp_pos_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

	// clear values
	$("#" + obj + " tr:last").find("td input[type=text]").attr("value", "");
}


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
				w = w + "" +
					"<option " +
						"valterritory=\"" + res_cols[1] + "\" " +
						"price_desc=\"" + res_cols[2] + "\" " +
						"val_area=\"" + res_cols[2] + "\" " +
						"val_region=\"" + res_cols[3] + "\" " +
						"grp_name=\"" + res_cols[4] + "\" " +
						"value=\"" + res_cols[0] + "\">" + res_cols[1] +
					"</option>";
			}
		}
	}

	w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

	// append
	$("body").append(w);

	// set position
	var btnY = getElLeft($("#" + obj_id_to_position)[0]);
	var btnX = getElTop($("#" + obj_id_to_position)[0]);
	$("#id_content").css('top', btnX + '' + 'px');
	$("#id_content").css('left', btnY + '' + 'px');

	// show 
	$("#id_content").show("fast");
	$("#id_bkg").show();
}

function SaveToTextBox(txt_box) {
	// forupload
	var new_file_name = "";
	new_file_name = $("#uploadframe").contents().find('body #file_name').attr('value');

	if (typeof (new_file_name) !== "undefined" && new_file_name != "") {
		$("#" + txt_box + "_forupload").attr("value", "true");
	}

	$("#" + txt_box).attr('value', new_file_name);
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
	var str_opt = "#id_content select option:selected";

	$("#" + obj).attr("value", $(str_opt).text());
	$("#" + obj).attr("value_id", $(str_opt).attr('value'));

	if (obj == "txt_acct_territory") {
		$("#" + obj).attr("value", $(str_opt).attr("valterritory"));
		$("#" + obj).attr("grp_name_", $(str_opt).attr("grp_name"));

		$("#txt_credit_terms_architectural_brand").addClass("required_fields");
		$("#txt_credit_terms_eco_lumber").addClass("required_fields");

		$("#txt_order_limit_ab").addClass("required_fields");
		$("#txt_order_limit_tr").addClass("required_fields");

		//        if ($(str_opt).attr("grp_name") == "GT") {
		//            $("#txt_credit_terms_architectural_brand").addClass("required_fields");
		//            $("#txt_credit_terms_eco_lumber").addClass("required_fields");

		//            $("#txt_order_limit_ab").addClass("required_fields");
		//            $("#txt_order_limit_tr").addClass("required_fields");
		//            // $("#txt_credit_terms_eco_plywood").addClass("required_fields");
		//        }
		//        else {
		//            $("#txt_credit_terms_architectural_brand").removeClass("required_fields");
		//            $("#txt_credit_terms_eco_lumber").removeClass("required_fields");

		//            $("#txt_order_limit_ab").removeClass("required_fields");
		//            $("#txt_order_limit_tr").removeClass("required_fields");
		//            // $("#txt_credit_terms_eco_plywood").removeClass("required_fields");
		//        }

		$("#txt_area").attr("value", GetValue($(str_opt).attr("val_area")));
		$("#txt_area").attr("value_id", GetId($(str_opt).attr("val_area")));

		$("#txt_region").attr("value", GetValue($(str_opt).attr("val_region")));
		$("#txt_region").attr("value_id", GetId($(str_opt).attr("val_region")));

		if ($("#txt_acct_classification").attr("value") == "WALKIN") {
			if ($("#txt_region").attr("value").toUpperCase().search("VISMIN") > -1) {
				$("#txt_tax_id").attr("value", "000 000 000 002");
			} else {
				$("#txt_tax_id").attr("value", "000 000 000 001");
			}
		}
	}

	if (obj == "txt_mw_price_code" || obj == "txt_ww_price_code" || obj == "txt_pwf_price_code" || obj == "txt_pwr_price_code" || obj == "txt_gw_price_code" || obj == "txt_tw_price_code" || obj == "txt_mz_price_code" || obj == "txt_nw_price_code" || obj == "txt_ec_price_code" || obj == "txt_ecu_price_code") {
		$("#" + obj.replace('_code', '_desc')).attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
	}

	if (obj == "txt_activity" || obj == "txt_act_type") {
		//  $("#" + obj.replace('_code', '_desc')).attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
		filter_activity_table();
	}

	$("#id_content").hide("fast");
	$("#id_bkg").hide();

	$("#id_content").remove();
	$("#id_bkg").remove();
}

function filter_activity_table() {
	var activity = $("#txt_activity").attr("value");
	var type = $("#txt_act_type").attr("value");
	$('#tblActivities tr[rowid!=""]').each(function (i, e) {
		if ($(this).attr("rowid") != undefined) {
			var act = $(this).find('td:eq(0) input').attr("value").toString();
			var typ = $(this).find('td:eq(1) input').attr("value").toString();

			if (activity == 'All' && type != 'All') {
				if (typ.indexOf(type) == -1) {
					$(this).hide();
				}
				else {
					$(this).show();
				}
			}
			else if (type == 'All' && activity != 'All') {
				if (act.indexOf(activity) == -1) {
					$(this).hide();
				}
				else {
					$(this).show();
				}
			}
			else if (activity == 'All' && type == 'All') {
				$(this).show();
			}
			else {
				if (act.indexOf(activity) == -1 || typ.indexOf(type) == -1) {
					$(this).hide();
				}
				else {
					$(this).show();
				}
			}
		}
	});
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


function DisableEditingCI() {
	$("#tbl_bank_list tr:last").hide();
	$("#tbl_bank_list tr td img").hide();

	$("#tbl_land_list tr:last").hide();
	$("#tbl_land_list tr td img").hide();

	$("#tbl_building_list tr:last").hide();
	$("#tbl_building_list tr td img").hide();

	$("#tbl_vehicle_list tr:last").hide();
	$("#tbl_vehicle_list tr td img").hide();

	$("#tbl_asset_list tr:last").hide();
	$("#tbl_asset_list tr td img").hide();

	$("#txt_other_assets").attr("readonly", "readonly");

	$("#txt_cibi_remakrs").attr("readonly", "readonly");
	$("#txt_supplyinfo_remakrs").attr("readonly", "readonly");
}


function DisableEditing(val_business_type) {
	var rd_o = "readonly";

	$("#tbl_partner_list tr:last").hide();
	$("#tbl_partner_list tr td img").hide();

	$("#tbl_corpo_list tr:last").hide();
	$("#tbl_corpo_list tr td img").hide();

	$("#tbl_emp_pos_list tr:last").hide();
	$("#tbl_emp_pos_list tr td img").hide();

	$("#tbl_outlet_list tr:last").hide();
	$("#tbl_outlet_list tr td img").hide();

	$("#tbl_mjcust_list tr:last").hide();
	$("#tbl_mjcust_list tr td img").hide();

	$("#tbl_wood_supplier tr:last").hide();
	$("#tbl_wood_supplier tr td img").hide();

	$("#tbl_birthday_events tr:last").hide();
	$("#tbl_birthday_events tr td img").hide();

	$("#tbl_specified_events tr:last").hide();
	$("#tbl_specified_events tr td img").hide();

	if (val_business_type == "0") {
		$('#sub_tab').tabs("disable", 1);
		$('#sub_tab').tabs("disable", 2);
	}

	if (val_business_type == "1") {
		$('#sub_tab').tabs("disable", 0);
		$('#sub_tab').tabs("disable", 2);
	}

	if (val_business_type == "2") {
		$('#sub_tab').tabs("disable", 0);
		$('#sub_tab').tabs("disable", 1);
	}

	// option button
	$("#acc_type_direct").attr('disabled', 'disabled');
	$("#acc_type_indirect").attr('disabled', 'disabled');
	$("#acc_key_yes").attr('disabled', 'disabled');
	$("#acc_key_no").attr('disabled', 'disabled');

	$("#slt_acct_classification").attr('disabled', 'disabled');

	// NEW FIELDS
	$("#txt_category_value").attr("onclick", "");
	$("#txt_category_prem").attr("onclick", "");
	$("#txt_buss_class").attr("onclick", "");

	$("#txt_type_of_account").attr("onclick", "");

	$("#txt_acct_code").attr(rd_o, rd_o);
	$("#txt_acct_name").attr(rd_o, rd_o);
	$("#txt_phone_no").attr(rd_o, rd_o);
	$("#txt_phone_no_2").attr(rd_o, rd_o);
	$("#txt_cellphone").attr(rd_o, rd_o);
	$("#txt_fax_no").attr(rd_o, rd_o);
	$("#txt_email_add").attr(rd_o, rd_o);
	$("#txt_office_hours").attr(rd_o, rd_o);
	$("#txt_area").attr(rd_o, rd_o);
	$("#txt_store_hours").attr(rd_o, rd_o);
	$("#txt_region").attr(rd_o, rd_o);
	$("#txt_yrs_business").attr(rd_o, rd_o);
	$("#txt_yrs_matimco").attr(rd_o, rd_o);
	$("#txt_tax_id").attr(rd_o, rd_o);
	$("#txt_vat_no").attr(rd_o, rd_o);
	$("#txt_vat_no").attr('onclick', '');
	$("#txt_reg_name").attr(rd_o, rd_o);
	$("#txt_business_add").attr(rd_o, rd_o);
	$("#txt_delivery_add").attr(rd_o, rd_o);

	$("#txt_acct_class").attr('onclick', '');
	$("#txt_acct_territory").attr('onclick', '');
	$("#txt_acct_officer").attr('onclick', '');

	$("#txt_sole_owner_name").attr(rd_o, rd_o);
	$("#txt_sole_nationality").attr(rd_o, rd_o);
	$("#txt_sole_gen_manager").attr(rd_o, rd_o);
	$("#txt_sole_fin_manager").attr(rd_o, rd_o);
	$("#txt_sole_others").attr(rd_o, rd_o);

	$("#txt_partner_gen_manager").attr(rd_o, rd_o);
	$("#txt_partner_fin_manager").attr(rd_o, rd_o);
	$("#txt_partner_others").attr(rd_o, rd_o);

	$("#txt_corpo_inc_date").attr(rd_o, rd_o);
	$("#txt_corpo_auth_cap_stock").attr(rd_o, rd_o);
	$("#txt_corpo_subscb_cap_stock").attr(rd_o, rd_o);
	$("#txt_corpo_paidin_cap_stock").attr(rd_o, rd_o);
	$("#txt_corpo_ceo").attr(rd_o, rd_o);
	$("#txt_corpo_vp_fin").attr(rd_o, rd_o);
	$("#txt_corpo_gen_man").attr(rd_o, rd_o);

	$("#txt_no_employees").attr(rd_o, rd_o);

	$("#txt_articles_of_inc").attr('onclick', '');
	$("#txt_financial_statement").attr('onclick', '');
	$("#txt_ITR").attr('onclick', '');
	$("#txt_bir_reg").attr('onclick', '');
	$("#txt_business_permit").attr('onclick', '');
	$("#txt_attch_other").attr('onclick', '');

	$("#txt_articles_of_inc").parent().parent().find("td a:first").hide();
	$("#txt_financial_statement").parent().parent().find("td a:first").hide();
	$("#txt_ITR").parent().parent().find("td a:first").hide();
	$("#txt_bir_reg").parent().parent().find("td a:first").hide();
	$("#txt_business_permit").parent().parent().find("td a:first").hide();
	$("#txt_attch_other").parent().parent().find("td a:first").hide();

	$("#txt_articles_of_inc").attr(rd_o, rd_o);
	$("#txt_financial_statement").attr(rd_o, rd_o);
	$("#txt_ITR").attr(rd_o, rd_o);
	$("#txt_bir_reg").attr(rd_o, rd_o);
	$("#txt_business_permit").attr(rd_o, rd_o);

	$("#txt_eco_class_of_customer").attr(rd_o, rd_o);
	$("#txt_no_of_outlets").attr(rd_o, rd_o);

	$("#txt_mw_price_code").attr(rd_o, rd_o);
	$("#txt_mw_price_desc").attr(rd_o, rd_o);
	$("#txt_mw_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_mw_price_remarks").attr(rd_o, rd_o);

	$("#txt_ww_price_code").attr(rd_o, rd_o);
	$("#txt_ww_price_desc").attr(rd_o, rd_o);
	$("#txt_ww_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_ww_price_remarks").attr(rd_o, rd_o);

	$("#txt_pwf_price_code").attr(rd_o, rd_o);
	$("#txt_pwf_price_desc").attr(rd_o, rd_o);
	$("#txt_pwf_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_pwf_price_remarks").attr(rd_o, rd_o);

	$("#txt_pwr_price_code").attr(rd_o, rd_o);
	$("#txt_pwr_price_desc").attr(rd_o, rd_o);
	$("#txt_pwr_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_pwr_price_remarks").attr(rd_o, rd_o);

	$("#txt_gw_price_code").attr(rd_o, rd_o);
	$("#txt_gw_price_desc").attr(rd_o, rd_o);
	$("#txt_gw_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_gw_price_remarks").attr(rd_o, rd_o);

	$("#txt_tw_price_code").attr(rd_o, rd_o);
	$("#txt_tw_price_desc").attr(rd_o, rd_o);
	$("#txt_tw_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_tw_price_remarks").attr(rd_o, rd_o);

	$("#txt_mz_price_code").attr(rd_o, rd_o);
	$("#txt_mz_price_desc").attr(rd_o, rd_o);
	$("#txt_mz_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_mz_price_remarks").attr(rd_o, rd_o);

	$("#txt_nw_price_code").attr(rd_o, rd_o);
	$("#txt_nw_price_desc").attr(rd_o, rd_o);
	$("#txt_nw_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_nw_price_remarks").attr(rd_o, rd_o);

	$("#txt_ec_price_code").attr(rd_o, rd_o);
	$("#txt_ec_price_desc").attr(rd_o, rd_o);
	$("#txt_ec_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_ec_price_remarks").attr(rd_o, rd_o);

	$("#txt_ecu_price_code").attr(rd_o, rd_o);
	$("#txt_ecu_price_desc").attr(rd_o, rd_o);
	$("#txt_ecu_price_commision_disc").attr(rd_o, rd_o);
	$("#txt_ecu_price_remarks").attr(rd_o, rd_o);

	$("#txt_prod_major_line").attr(rd_o, rd_o);
	$("#txt_prod_other_line").attr(rd_o, rd_o);
	$("#txt_const_mat_plywood").attr(rd_o, rd_o);
	$("#txt_const_mat_steel").attr(rd_o, rd_o);
	$("#txt_const_mat_cement").attr(rd_o, rd_o);
	$("#txt_const_mat_hb").attr(rd_o, rd_o);
	$("#txt_const_mat_others").attr(rd_o, rd_o);
	$("#txt_major_vol_business").attr(rd_o, rd_o);
	$("#txt_wood_vol").attr(rd_o, rd_o);
	$("#txt_discount_enjoyed").attr(rd_o, rd_o);

	$("#txt_mw_price_code").attr('onclick', '');
	$("#txt_ww_price_code").attr('onclick', '');
	$("#txt_pwf_price_code").attr('onclick', '');
	$("#txt_pwr_price_code").attr('onclick', '');
	$("#txt_gw_price_code").attr('onclick', '');
	$("#txt_tw_price_code").attr('onclick', '');

	$("#txt_mz_price_code").attr('onclick', '');
	$("#txt_nw_price_code").attr('onclick', '');
	$("#txt_ec_price_code").attr('onclick', '');
	$("#txt_ecu_price_code").attr('onclick', '');
}

function DisAbleCreditLT() {
	/* Code added by Billy Jay (04/23/2015) */
	$("#txt_credit_terms_architectural_brand").attr('readonly', 'readonly'); $("#txt_credit_terms_architectural_brand").attr('onclick', '');
	$("#txt_credit_terms_eco_lumber").attr('readonly', 'readonly'); $("#txt_credit_terms_eco_lumber").attr('onclick', '');
	$("#txt_credit_terms_eco_plywood").attr('readonly', 'readonly'); $("#txt_credit_terms_eco_plywood").attr('onclick', '');

	$("#txt_credit_terms_architectural_brand_remarks").attr('readonly', 'readonly');
	$("#txt_credit_terms_eco_lumber_remarks").attr('readonly', 'readonly');
	$("#txt_credit_terms_eco_plywood_remarks").attr('readonly', 'readonly');


	$("#txt_order_limit_ab").attr('readonly', 'readonly');
	$("#txt_order_limit_tr").attr('readonly', 'readonly');
	$("#txt_order_limit_remarks_ab").attr('readonly', 'readonly');
	$("#txt_order_limit_remarks_tr").attr('readonly', 'readonly');
	/* End Code added by Billy Jay (04/23/2015) */

	$("#txt_credit_terms").attr('readonly', 'readonly'); $("#txt_credit_terms").attr('onclick', '');
	$("#txt_credit_limit").attr('readonly', 'readonly');
	$("#txt_credit_terms_remarks").attr('readonly', 'readonly');
	$("#txt_credit_limit_remarks").attr('readonly', 'readonly');
}


// SAVING
function Save_Doc() {

	if (CheckRequiredFields()) {
		Savedata();
	}
}

function CheckRequiredFields() {
	var acct_type = $("#acc_type_direct").attr('checked') == 'checked' ? "direct" : "indirect";

	// DISREGARD REQUIRED FIELDS when personalinfo_tab_access_only
	if (personalInfo_tab_access_only_ == "True") {
		return true;
	}

	// ADDITIONAL CHECKING FOR COMMSION AND DESCOUNTS
	if (CheckCommisionAndDiscount() != "") {
		alert(CheckCommisionAndDiscount());
		return false;
	}

	if ($("#txt_acct_classification").attr('value') == "REGULAR") {
		if (acct_type == "direct") {
			return CheckRequiredFieldsRegular();
		} else {
			return CheckRequiredFieldsRegularSmall();
		}
	} else {
		return CheckRequiredFieldsWalkIn();
	}
}

function CheckRequiredFieldsRegularSmall() {
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

	// tax id
	if ($("#txt_tax_id").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tax ID"; } else { lacking_fields = "Tax ID"; }
	}


	// Vat Type
	if ($("#txt_vat_no").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "VAT Type"; } else { lacking_fields = "VAT Type"; }
	}

	//BIR Attachment
	//  if ($("txt_bir_reg").attr('value') == "") {
	//      if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "BIR Registration Form Attachment"; } else { lacking_fields = "BIR Registration Form Attachment"; }
	//  }

	//Customer Info Sheet Attachment
	// if ($("#txt_articles_of_inc").attr('value') == "") {
	//     if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Customer Info Sheet Attachment"; } else { lacking_fields = "Customer Info Sheet Attachment"; }
	// }

	// check if integer [ No of Employees]
	if ($("#txt_no_employees").attr('value') != "") {
		if (isNaN($("#txt_no_employees").attr('value')) == true) {
			if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Number of employees] must be a number."; } else { lacking_fields = "[Number of employees] must be a number."; }
		}
	}

	// check if integer [ No of outlets]
	if ($("#txt_no_of_outlets").attr('value') != "") {
		if (isNaN($("#txt_no_of_outlets").attr('value')) == true) {
			if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Number of outlets] must be a number."; } else { lacking_fields = "[Number of outlets] must be a number."; }
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

	//Type of account
	if ($("#txt_type_of_account").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type of Account"; } else { lacking_fields = "Type of Account"; }
	}

	if (lacking_fields != "") {
		alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
		return false;
	}

	return true;
}

function CheckRequiredFieldsWalkIn() {
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

	// tax id
	if ($("#txt_tax_id").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tax ID"; } else { lacking_fields = "Tax ID"; }
	}

	// Vat Type
	if ($("#txt_vat_no").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "VAT Type"; } else { lacking_fields = "VAT Type"; }
	}

	// check if integer [ No of Employees]
	if ($("#txt_no_employees").attr('value') != "") {
		if (isNaN($("#txt_no_employees").attr('value')) == true) {
			if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Number of employees] must be a number."; } else { lacking_fields = "[Number of employees] must be a number."; }
		}
	}

	// check if integer [ No of outlets]
	if ($("#txt_no_of_outlets").attr('value') != "") {
		if (isNaN($("#txt_no_of_outlets").attr('value')) == true) {
			if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Number of outlets] must be a number."; } else { lacking_fields = "[Number of outlets] must be a number."; }
		}
	}

	// ** BUSINESS INFO **
	/* Code added by Billy Jay (04/23/2015) */

	// Proposed credit terms
	//if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
	if ($("#txt_credit_terms_architectural_brand").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Architectural Brand Credit Terms"; } else { lacking_fields = "Architectural Brand Credit Terms"; }
	}

	if ($("#txt_credit_terms_eco_lumber").attr("value") == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Lumber Credit Terms"; } else { lacking_fields = "Ecofor Lumber Credit Terms"; }
	}

	//        if ($("#txt_credit_terms_eco_plywood").attr("value") == "") {
	//            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Plywood Credit Terms"; } else { lacking_fields = "Ecofor Plywood Credit Terms"; }
	//        } 
	if ($("#txt_order_limit_ab").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Limit"; } else { lacking_fields = "AB Order Limit"; }
	}
	else if (isNumeric($("#txt_order_limit_ab").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Limit Invalid"; } else { lacking_fields = "AB Order Limit Invalid"; }
	}

	if ($("#txt_order_limit_tr").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Limit"; } else { lacking_fields = "TR Order Limit"; }
	}
	else if (isNumeric($("#txt_order_limit_tr").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Limit Invalid"; } else { lacking_fields = "TR Order Limit Invalid"; }
	}
	//}

	/* End Code added (04/23/2015) */

	/* Commented Code  by Billy Jay (04/23/2015) 

	// Proposed creidt terms
	if ($("#txt_credit_terms").attr('value') == "") {
	if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Terms"; } else { lacking_fields = "Credit Terms"; }
	}

	End Commented Code  by Billy Jay (04/23/2015)  */

	// proposed credit limit
	if ($("#txt_credit_limit").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit"; } else { lacking_fields = "Credit Limit"; }
	}
	else if (isNumeric($("#txt_credit_limit").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit Invalid"; } else { lacking_fields = "Credit Limit Invalid"; }
	}


	// proposed price list code for matwood
	if ($("#txt_mw_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Matwood Price Code"; } else { lacking_fields = "Matwood Price Code"; }
	}

	// proposed price list code for weatherwood
	if ($("#txt_ww_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "WeatherWood Price Code"; } else { lacking_fields = "WeatherWood Price Code"; }
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
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "EC Price Code"; } else { lacking_fields = "EC (Treated) Price Code"; }
	}

	// proposed price list code for ecofor untreated
	if ($("#txt_ecu_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "ECU Price Code"; } else { lacking_fields = "ECU (UnTreated) Price Code"; }
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

	//Type of account
	if ($("#txt_type_of_account").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type of Account"; } else { lacking_fields = "Type of Account"; }
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

	// Vat Type
	if ($("#txt_vat_no").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "VAT Type"; } else { lacking_fields = "VAT Type"; }
	}


	//BIR Attachment
	if ($("txt_bir_reg").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "BIR Registration Form Attachment"; } else { lacking_fields = "BIR Registration Form Attachment"; }
	}

	//Customer Info Sheet Attachment
	if ($("#txt_articles_of_inc").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Customer Info Sheet Attachment"; } else { lacking_fields = "Customer Info Sheet Attachment"; }
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

	// ** BUSINESS INFO **
	/* Code added by Billy Jay (04/23/2015) */

	// Proposed credit terms
	//  if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
	if ($("#txt_credit_terms_architectural_brand").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Architectural Brand Credit Terms"; } else { lacking_fields = "Architectural Brand Credit Terms"; }
	}

	if ($("#txt_credit_terms_eco_lumber").attr("value") == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Lumber Credit Terms"; } else { lacking_fields = "Ecofor Lumber Credit Terms"; }
	}

	//        if ($("#txt_credit_terms_eco_plywood").attr("value") == "") {
	//            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Ecofor Plywood Credit Terms"; } else { lacking_fields = "Ecofor Plywood Credit Terms"; }
	//        } 
	if ($("#txt_order_limit_ab").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Limit"; } else { lacking_fields = "AB Order Limit"; }
	}
	else if (isNumeric($("#txt_order_limit_ab").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "AB Order Limit Invalid"; } else { lacking_fields = "AB Order Limit Invalid"; }
	}

	if ($("#txt_order_limit_tr").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Limit"; } else { lacking_fields = "TR ORder Limit"; }
	}
	else if (isNumeric($("#txt_order_limit_tr").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "TR Order Limit Invalid"; } else { lacking_fields = "TR Order Limit Invalid"; }
	}
	// }

	/* End Code added (04/23/2015) */

	/* Commented Code  by Billy Jay (04/23/2015) 

	// Proposed creidt terms
	if ($("#txt_credit_terms").attr('value') == "") {
	if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Terms"; } else { lacking_fields = "Credit Terms"; }
	}
	
	End Commented Code  by Billy Jay (04/23/2015)  */

	// proposed credit limit
	if ($("#txt_credit_limit").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit"; } else { lacking_fields = "Credit Limit"; }
	}
	else if (isNumeric($("#txt_credit_limit").attr('value')) == false) {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Credit Limit Invalid"; } else { lacking_fields = "Credit Limit Invalid"; }
	}


	// proposed price list code for matwood
	if ($("#txt_mw_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Matwood Price Code"; } else { lacking_fields = "Matwood Price Code"; }
	}

	// proposed price list code for weatherwood
	if ($("#txt_ww_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "WeatherWood Price Code"; } else { lacking_fields = "WeatherWood Price Code"; }
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
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "EC Price Code"; } else { lacking_fields = "EC (Treated) Price Code"; }
	}

	// proposed price list code for ecofor untreated
	if ($("#txt_ecu_price_code").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "ECU Price Code"; } else { lacking_fields = "EC (UnTreated) Price Code"; }
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

	//Type of account
	if ($("#txt_type_of_account").attr('value') == "") {
		if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type of Account"; } else { lacking_fields = "Type of Account"; }
	}

	if (lacking_fields != "") {
		alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
		return false;
	}

	return true;
}

function Savedata() {

	// check current doc status
	DisplayPreloader();

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
	$("#tbl_partner_list tr").length > 2
	) {
		acct_business_type = "Partnership";
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
		acct_business_type = "Corporation";
	} else {
		acct_business_type = "";
	}

	// category value
	var acct_category_value = "";
	acct_category_value = $("#txt_category_value").attr("value");
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_category_value = acct_category_value;
	}

	// category prem
	var acct_category_prem = "";
	acct_category_prem = $("#txt_category_prem").attr("value");
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_category_prem = acct_category_prem;
	}

	var acct_business_class = "";
	acct_business_class = $("#txt_buss_class").attr("value");
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_BusinessClass = acct_business_class;
	}

	// type of account
	var acct_type_of_account = "";
	acct_type_of_account = $("#txt_type_of_account").attr("value_id");
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_BusinessClass = acct_type_of_account;
	}

	// account type
	var acct_type = "";
	if ($("#acc_type_direct").attr("checked") == "checked") {
		acct_type = "DIRECT";
	} else {
		acct_type = "INDIRECT";
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
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_name = acct_name;
	}

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
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_acct_officer = acct_acct_officer;
	}

	// fax
	var acct_fax_no = "";
	acct_fax_no = $("#txt_fax_no").attr('value');

	// territory
	var acct_territory = "";
	acct_territory = $("#txt_acct_territory").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_territory = acct_territory;
	}

	// email
	var acct_email = "";
	acct_email = $("#txt_email_add").attr('value');

	// office hours
	var acct_office_hours = "";
	acct_office_hours = $("#txt_office_hours").attr('value');

	// area
	var acct_area = "";
	acct_area = $("#txt_area").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_area = acct_area;
	}

	// store hours
	var acct_store_hours = "";
	acct_store_hours = $("#txt_store_hours").attr('value');

	// region
	var acct_region = "";
	acct_region = $("#txt_region").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_region = acct_region;
	}

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
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_reg_name = acct_reg_name;
	}

	// business address
	var acct_business_add = "";
	acct_business_add = $("#txt_business_add").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_business_add = acct_business_add;
	}

	// delivery address
	var acct_delivery_add = "";
	acct_delivery_add = $("#txt_delivery_add").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_delivery_add = acct_delivery_add;
	}

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
	var acct_article_of_inc_forupload = "";
	acct_article_of_inc_forupload = $("#txt_articles_of_inc_forupload").attr('value');

	// financial statements
	var acct_financial_statement = "";
	acct_financial_statement = $("#txt_financial_statement").attr('value');
	var acct_financial_statement_forupload = "";
	acct_financial_statement_forupload = $("#txt_financial_statement_forupload").attr('value');

	// income tax return
	var acct_itr = "";
	acct_itr = $("#txt_ITR").attr('value');
	var acct_itr_forupload = "";
	acct_itr_forupload = $("#txt_ITR_forupload").attr('value');

	// bor registration
	var acct_bir_reg = "";
	acct_bir_reg = $("#txt_bir_reg").attr('value');
	var acct_bir_reg_forupload = "";
	acct_bir_reg_forupload = $("#txt_bir_reg_forupload").attr('value');

	// latest business permit
	var acct_business_permit = "";
	acct_business_permit = $("#txt_business_permit").attr('value');
	var acct_business_permit_forupload = "";
	acct_business_permit_forupload = $("#txt_business_permit_forupload").attr('value');

	// other attachment
	var acct_attch_other = "";
	acct_attch_other = $("#txt_attch_other").attr('value');
	var acct_attch_other_forupload = "";
	acct_attch_other_forupload = $("#txt_attch_other_forupload").attr('value');

	/* Code added by Billy Jay (04/23/2015) */

	var acct_prop_credit_term_architectural_brand = "";
	acct_prop_credit_term_architectural_brand = $("#txt_credit_terms_architectural_brand").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_architectural_brand = acct_prop_credit_term_architectural_brand;

	}

	var acct_prop_credit_term_ecofor_lumber = "";
	acct_prop_credit_term_ecofor_lumber = $("#txt_credit_terms_eco_lumber").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_ecofor_lumber = acct_prop_credit_term_ecofor_lumber;
	}

	var acct_prop_credit_term_ecofor_plywood = "";
	acct_prop_credit_term_ecofor_plywood = $("#txt_credit_terms_eco_plywood").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_ecofor_plywood = acct_prop_credit_term_ecofor_plywood;
	}

	// credit terms remarks
	var acct_prop_credit_term_remarks_architectural_brand = "";
	acct_prop_credit_term_remarks_architectural_brand = $("#txt_credit_terms_architectural_brand_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_remarks_architectural_brand = acct_prop_credit_term_remarks_architectural_brand;
	}

	var acct_prop_credit_term_remarks_ecofor_lumber = "";
	acct_prop_credit_term_remarks_ecofor_lumber = $("#txt_credit_terms_eco_lumber_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_remarks_ecofor_lumber = acct_prop_credit_term_remarks_ecofor_lumber;
	}

	var acct_prop_credit_term_remarks_ecofor_plywood = "";
	acct_prop_credit_term_remarks_ecofor_plywood = $("#txt_credit_terms_eco_plywood_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_remarks_ecofor_plywood = acct_prop_credit_term_remarks_ecofor_plywood;

	}

	// AB proposed order limit
	var acct_prob_order_limit_ab = "";
	acct_prob_order_limit_ab = $("#txt_order_limit_ab").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prob_order_limit_ab = acct_prob_order_limit_ab;
	}

	// TR proposed order limit
	var acct_prob_order_limit_tr = "";
	acct_prob_order_limit_tr = $("#txt_order_limit_tr").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prob_order_limit_tr = acct_prob_order_limit_tr;
	}

	// AB order limit remarks
	var acct_prob_order_limit_remarks_ab = "";
	acct_prob_order_limit_remarks_ab = $("#txt_order_limit_remarks_ab").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prob_order_limit_remarks_ab = acct_prob_order_limit_remarks_ab;
	}

	// TR order limit remarks
	var acct_prob_order_limit_remarks_tr = "";
	acct_prob_order_limit_remarks_tr = $("#txt_order_limit_remarks_tr").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prob_order_limit_remarks_tr = acct_prob_order_limit_remarks_tr;
	}

	/* End Code added by Billy Jay (04/23/2015) */

	// proposed credit terms
	var acct_prop_credit_term = "";
	acct_prop_credit_term = $("#txt_credit_terms").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term = acct_prop_credit_term;
	}

	// proposed credit limit
	var acct_prop_credit_limit = "";
	acct_prop_credit_limit = $("#txt_credit_limit").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_limit = acct_prop_credit_limit;
	}

	// credit terms remarks
	var acct_prop_credit_term_remarks = "";
	acct_prop_credit_term_remarks = $("#txt_credit_terms_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_term_remarks = acct_prop_credit_term_remarks;
	}

	// credit limit remarks
	var acct_prop_credit_limit_remarks = "";
	acct_prop_credit_limit_remarks = $("#txt_credit_limit_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_prop_credit_limit_remarks = acct_prop_credit_limit_remarks;
	}

	// [matwood]
	// code
	var acct_mw_price_code = "";
	acct_mw_price_code = $("#txt_mw_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mw_price_code = acct_mw_price_code;
	}

	// description
	var acct_mw_price_desc = "";
	acct_mw_price_desc = $("#txt_mw_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mw_price_desc = acct_mw_price_desc;
	}

	// commission & discount
	var acct_mw_price_commision_disc = "";
	acct_mw_price_commision_disc = $("#txt_mw_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mw_price_commision_disc = acct_mw_price_commision_disc;
	}

	// remarks
	var acct_mw_price_remarks = "";
	acct_mw_price_remarks = $("#txt_mw_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mw_price_remarks = acct_mw_price_remarks;
	}

	// [weatherwood]
	// code
	var acct_ww_price_code = "";
	acct_ww_price_code = $("#txt_ww_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ww_price_code = acct_ww_price_code;
	}

	// description
	var acct_ww_price_desc = "";
	acct_ww_price_desc = $("#txt_ww_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ww_price_desc = acct_ww_price_desc;
	}

	// commission & discount
	var acct_ww_price_commision_disc = "";
	acct_ww_price_commision_disc = $("#txt_ww_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ww_price_commision_disc = acct_ww_price_commision_disc;
	}

	// remarks
	var acct_ww_price_remarks = "";
	acct_ww_price_remarks = $("#txt_ww_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ww_price_remarks = acct_ww_price_remarks;
	}

	// [pcw - frames]
	// code
	var acct_pwf_price_code = "";
	acct_pwf_price_code = $("#txt_pwf_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwf_price_code = acct_pwf_price_code;
	}

	// description
	var acct_pwf_price_desc = "";
	acct_pwf_price_desc = $("#txt_pwf_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwf_price_desc = acct_pwf_price_desc;
	}

	// commission & discount
	var acct_pwf_price_commision_disc = "";
	acct_pwf_price_commision_disc = $("#txt_pwf_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwf_price_commision_disc = acct_pwf_price_commision_disc;
	}

	// remarks
	var acct_pwf_price_remarks = "";
	acct_pwf_price_remarks = $("#txt_pwf_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwf_price_remarks = acct_pwf_price_remarks;
	}

	// [pcw - regular items]
	// code
	var acct_pwr_price_code = "";
	acct_pwr_price_code = $("#txt_pwr_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwr_price_code = acct_pwr_price_code;
	}

	// description
	var acct_pwr_price_desc = "";
	acct_pwr_price_desc = $("#txt_pwr_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwr_price_desc = acct_pwr_price_desc;
	}

	// commission & discount
	var acct_pwr_price_commision_disc = "";
	acct_pwr_price_commision_disc = $("#txt_pwr_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwr_price_commision_disc = acct_pwr_price_commision_disc;
	}

	// remarks
	var acct_pwr_price_remarks = "";
	acct_pwr_price_remarks = $("#txt_pwr_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_pwr_price_remarks = acct_pwr_price_remarks;
	}

	// [gudwood]
	// code
	var acct_gw_price_code = "";
	acct_gw_price_code = $("#txt_gw_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_gw_price_code = acct_gw_price_code;
	}

	// description
	var acct_gw_price_desc = "";
	acct_gw_price_desc = $("#txt_gw_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_gw_price_desc = acct_gw_price_desc;
	}

	// commission & discount
	var acct_gw_price_commision_disc = "";
	acct_gw_price_commision_disc = $("#txt_gw_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_gw_price_commision_disc = acct_gw_price_commision_disc;
	}

	// remarks
	var acct_gw_price_remarks = "";
	acct_gw_price_remarks = $("#txt_gw_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_gw_price_remarks = acct_gw_price_remarks;
	}

	// [trusswood]
	// code
	var acct_tw_price_code = "";
	acct_tw_price_code = $("#txt_tw_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_tw_price_code = acct_tw_price_code;
	}

	// description
	var acct_tw_price_desc = "";
	acct_tw_price_desc = $("#txt_tw_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_tw_price_desc = acct_tw_price_desc;
	}

	// commission & discount
	var acct_tw_price_commision_disc = "";
	acct_tw_price_commision_disc = $("#txt_tw_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_tw_price_commision_disc = acct_tw_price_commision_disc;
	}

	// remarks
	var acct_tw_price_remarks = "";
	acct_tw_price_remarks = $("#txt_tw_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_tw_price_remarks = acct_tw_price_remarks;
	}

	// [muzuwood]
	// code
	var acct_mz_price_code = "";
	acct_mz_price_code = $("#txt_mz_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mz_price_code = acct_mz_price_code;
	}

	// description
	var acct_mz_price_desc = "";
	acct_mz_price_desc = $("#txt_mz_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mz_price_desc = acct_mz_price_desc;
	}

	// commission & discount
	var acct_mz_price_commision_disc = "";
	acct_mz_price_commision_disc = $("#txt_mz_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mz_price_commision_disc = acct_mz_price_commision_disc;
	}

	// remarks
	var acct_mz_price_remarks = "";
	acct_mz_price_remarks = $("#txt_mz_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_mz_price_remarks = acct_mz_price_remarks;
	}

	// [nuwood]
	// code
	var acct_nw_price_code = "";
	acct_nw_price_code = $("#txt_nw_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_nw_price_code = acct_nw_price_code;
	}

	// description
	var acct_nw_price_desc = "";
	acct_nw_price_desc = $("#txt_nw_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_nw_price_desc = acct_nw_price_desc;
	}

	// commission & discount
	var acct_nw_price_commision_disc = "";
	acct_nw_price_commision_disc = $("#txt_nw_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_nw_price_commision_disc = acct_nw_price_commision_disc;
	}

	// remarks
	var acct_nw_price_remarks = "";
	acct_nw_price_remarks = $("#txt_nw_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_nw_price_remarks = acct_nw_price_remarks;
	}

	// [ecofor treated]
	// code
	var acct_ec_price_code = "";
	acct_ec_price_code = $("#txt_ec_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ec_price_code = acct_ec_price_code;
	}

	// description
	var acct_ec_price_desc = "";
	acct_ec_price_desc = $("#txt_ec_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ec_price_desc = acct_ec_price_desc;
	}

	// commission & discount
	var acct_ec_price_commision_disc = "";
	acct_ec_price_commision_disc = $("#txt_ec_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ec_price_commision_disc = acct_ec_price_commision_disc;
	}

	// remarks
	var acct_ec_price_remarks = "";
	acct_ec_price_remarks = $("#txt_ec_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ec_price_remarks = acct_ec_price_remarks;
	}


	// [ecofor untreated]
	// code
	var acct_ecu_price_code = "";
	acct_ecu_price_code = $("#txt_ecu_price_code").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ecu_price_code = acct_ecu_price_code;
	}

	// description
	var acct_ecu_price_desc = "";
	acct_ecu_price_desc = $("#txt_ecu_price_desc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ecu_price_desc = acct_ecu_price_desc;
	}

	// commission & discount
	var acct_ecu_price_commision_disc = "";
	acct_ecu_price_commision_disc = $("#txt_ecu_price_commision_disc").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ecu_price_commision_disc = acct_ecu_price_commision_disc;
	}

	// remarks
	var acct_ecu_price_remarks = "";
	acct_ecu_price_remarks = $("#txt_ecu_price_remarks").attr('value');
	if (customerHeaderStatus != '1000') {
		// save currently selected values
		g_acct_ecu_price_remarks = acct_ecu_price_remarks;
	}

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

	// [name, event, date, contactnumber]
	var list_of_events = new Array();
	row_count = $("#tbl_birthday_events tr").length;
	var loop_count = 0;
	$("#tbl_birthday_events tr").each(
	function (index, element) {
		loop_count++;
		if (loop_count > 1 && loop_count < row_count) {
			list_of_events.push(
	$(element).find("td:nth-child(1) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
	+ "|" + "F"
	);
		}
	}
	);
	// [name, event, date, contactnumber] --specified event
	row_count = $("#tbl_specified_events tr").length;
	var loop_count = 0;
	$("#tbl_specified_events tr").each(
	function (index, element) {
		loop_count++;
		if (loop_count > 1 && loop_count < row_count) {
			list_of_events.push(
	$(element).find("td:nth-child(1) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
	+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
	+ "|" + "T"
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

	// GET account classification and ccanum 
	var acct_ccanum = "";
	acct_ccanum = $("#txt_acct_ccanum").attr('value');

	var acct_classification = "";
	acct_classification = $("#txt_acct_classification").attr('value');

	var other_changes = "";
	other_changes = GetDocChanges();

	var remarks_ = "";
	if ($("input[obj=txt_doc_remarks]").attr('value') != undefined) {
		remarks_ = $("input[obj=txt_doc_remarks]").attr('value');
	}

	var acct_iniPODetails = "";
	acct_iniPODetails = $("#txt_ini_po_details").attr("value");

	var params = {
		acct_category_value: acct_category_value,
		acct_category_prem: acct_category_prem,
		acct_business_class: acct_business_class,
		acct_type_of_account: acct_type_of_account,
		acct_classification: acct_classification,
		acct_type: acct_type,
		acct_key_account: acct_key_account,
		acct_code: acct_code,
		acct_class: acct_class,
		acct_name: g_acct_name,
		proposed_new_acct_name: acct_name,
		acct_phone_no: acct_phone_no,
		acct_phone_no_2: acct_phone_no_2,
		acct_cellphone: acct_cellphone,
		acct_acct_officer: g_acct_acct_officer,
		proposed_new_acct_officer: acct_acct_officer,
		acct_fax_no: acct_fax_no,
		acct_territory: g_acct_territory,
		proposed_new_acct_territory: acct_territory,
		acct_email: acct_email,
		acct_office_hours: acct_office_hours,
		acct_area: g_acct_area,
		proposed_acct_new_area: acct_area,
		acct_store_hours: acct_store_hours,
		acct_region: g_acct_region,
		proposed_new_acct_region: acct_region,
		acct_years_in_business: acct_years_in_business,
		acct_years_with_matimco: acct_years_with_matimco,
		acct_tax_id: acct_tax_id,
		acct_vat_no: acct_vat_no,
		acct_reg_name: g_acct_reg_name,
		proposed_new_acct_reg_name: acct_reg_name,
		acct_business_add: g_acct_business_add,
		proposed_new_acct_business_add: acct_business_add,
		acct_delivery_add: g_acct_delivery_add,
		proposed_new_acct_delivery_add: acct_delivery_add,
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
		acct_article_of_inc_forupload: acct_article_of_inc_forupload,
		acct_financial_statement: acct_financial_statement,
		acct_financial_statement_forupload: acct_financial_statement_forupload,
		acct_itr: acct_itr,
		acct_itr_forupload: acct_itr_forupload,
		acct_bir_reg: acct_bir_reg,
		acct_bir_reg_forupload: acct_bir_reg_forupload,
		acct_business_permit: acct_business_permit,
		acct_business_permit_forupload: acct_business_permit_forupload,
		acct_attch_other: acct_attch_other,
		acct_attch_other_forupload: acct_attch_other_forupload,

		/* Code added by Billy Jay (04/23/2015) */
		acct_prop_credit_term_architectural_brand: g_acct_prop_credit_term_architectural_brand,
		acct_prop_credit_term_ecofor_lumber: g_acct_prop_credit_term_ecofor_lumber,
		acct_prop_credit_term_ecofor_plywood: g_acct_prop_credit_term_ecofor_plywood,

		acct_prop_credit_term_remarks_architectural_brand: g_acct_prop_credit_term_remarks_architectural_brand,
		acct_prop_credit_term_remarks_ecofor_lumber: g_acct_prop_credit_term_remarks_ecofor_lumber,
		acct_prop_credit_term_remarks_ecofor_plywood: g_acct_prop_credit_term_remarks_ecofor_plywood,

		proposed_new_acct_prop_credit_term_architectural_brand: acct_prop_credit_term_architectural_brand,
		proposed_new_acct_prop_credit_term_ecofor_lumber: acct_prop_credit_term_ecofor_lumber,
		proposed_new_acct_prop_credit_term_ecofor_plywood: acct_prop_credit_term_ecofor_plywood,

		proposed_new_acct_credit_term_remarks_architectural_brand: acct_prop_credit_term_remarks_architectural_brand,
		proposed_new_acct_credit_term_remarks_ecofor_lumber: acct_prop_credit_term_remarks_ecofor_lumber,
		proposed_new_acct_credit_term_remarks_ecofor_plywood: acct_prop_credit_term_remarks_ecofor_plywood,


		acct_prop_order_limit_ab: undoAddComma(g_acct_prob_order_limit_ab),
		acct_prop_order_limit_tr: undoAddComma(g_acct_prob_order_limit_tr),

		proposed_new_acct_prop_order_limit_ab: undoAddComma(acct_prob_order_limit_ab),
		proposed_new_acct_prop_order_limit_tr: undoAddComma(acct_prob_order_limit_tr),

		acct_prop_order_limit_remarks_ab: g_acct_prob_order_limit_remarks_ab,
		acct_prop_order_limit_remarks_tr: g_acct_prob_order_limit_remarks_tr,

		proposed_new_acct_prop_order_limit_remarks_ab: acct_prob_order_limit_remarks_ab,
		proposed_new_acct_prop_order_limit_remarks_tr: acct_prob_order_limit_remarks_tr,

		/* End Code added by Billy Jay (04/23/2015) */

		acct_prop_credit_term: g_acct_prop_credit_term,
		proposed_new_acct_prop_credit_term: acct_prop_credit_term,
		acct_prop_credit_limit: g_acct_prop_credit_limit,
		proposed_new_acct_prop_credit_limit: acct_prop_credit_limit,
		acct_prop_credit_term_remarks: g_acct_prop_credit_term_remarks,
		proposed_new_acct_prop_credit_term_remarks: acct_prop_credit_term_remarks,
		acct_prop_credit_limit_remarks: g_acct_prop_credit_limit_remarks,
		proposed_new_acct_prop_credit_limit_remarks: acct_prop_credit_limit_remarks,
		acct_mw_price_code: g_acct_mw_price_code,
		proposed_new_acct_mw_price_code: acct_mw_price_code,
		acct_mw_price_desc: g_acct_mw_price_desc,
		proposed_new_acct_mw_price_desc: acct_mw_price_desc,
		acct_mw_price_commision_disc: g_acct_mw_price_commision_disc,
		proposed_new_acct_mw_price_commision_disc: acct_mw_price_commision_disc,
		acct_mw_price_remarks: g_acct_mw_price_remarks,
		proposed_new_acct_mw_price_remarks: acct_mw_price_remarks,
		acct_ww_price_code: g_acct_ww_price_code,
		proposed_new_acct_ww_price_code: acct_ww_price_code,
		acct_ww_price_desc: g_acct_ww_price_desc,
		proposed_new_acct_ww_price_desc: acct_ww_price_desc,
		acct_ww_price_commision_disc: g_acct_ww_price_commision_disc,
		proposed_new_acct_ww_price_commision_disc: acct_ww_price_commision_disc,
		acct_ww_price_remarks: g_acct_ww_price_remarks,
		proposed_new_acct_ww_price_remarks: acct_ww_price_remarks,
		acct_pwf_price_code: g_acct_pwf_price_code,
		proposed_new_acct_pwf_price_code: acct_pwf_price_code,
		acct_pwf_price_desc: g_acct_pwf_price_desc,
		proposed_new_acct_pwf_price_desc: acct_pwf_price_desc,
		acct_pwf_price_commision_disc: g_acct_pwf_price_commision_disc,
		proposed_new_acct_pwf_price_commision_disc: acct_pwf_price_commision_disc,
		acct_pwf_price_remarks: g_acct_pwf_price_remarks,
		proposed_new_acct_pwf_price_remarks: acct_pwf_price_remarks,
		acct_pwr_price_code: g_acct_pwr_price_code,
		proposed_new_acct_pwr_price_code: acct_pwr_price_code,
		acct_pwr_price_desc: g_acct_pwr_price_desc,
		proposed_new_acct_pwr_price_desc: acct_pwr_price_desc,
		acct_pwr_price_commision_disc: g_acct_pwr_price_commision_disc,
		proposed_new_acct_pwr_price_commision_disc: acct_pwr_price_commision_disc,
		acct_pwr_price_remarks: g_acct_pwr_price_remarks,
		proposed_new_acct_pwr_price_remarks: acct_pwr_price_remarks,
		acct_gw_price_code: g_acct_gw_price_code,
		proposed_new_acct_gw_price_code: acct_gw_price_code,
		acct_gw_price_desc: g_acct_gw_price_desc,
		proposed_new_acct_gw_price_desc: acct_gw_price_desc,
		acct_gw_price_commision_disc: g_acct_gw_price_commision_disc,
		proposed_new_acct_gw_price_commision_disc: acct_gw_price_commision_disc,
		acct_gw_price_remarks: g_acct_gw_price_remarks,
		proposed_new_acct_gw_price_remarks: acct_gw_price_remarks,
		acct_tw_price_code: g_acct_tw_price_code,
		proposed_new_acct_tw_price_code: acct_tw_price_code,
		acct_tw_price_desc: g_acct_tw_price_desc,
		proposed_new_acct_tw_price_desc: acct_tw_price_desc,
		acct_tw_price_commision_disc: g_acct_tw_price_commision_disc,
		proposed_new_acct_tw_price_commision_disc: acct_tw_price_commision_disc,
		acct_tw_price_remarks: g_acct_tw_price_remarks,
		proposed_new_acct_tw_price_remarks: acct_tw_price_remarks,

		acct_mz_price_code: g_acct_mz_price_code,
		proposed_new_acct_mz_price_code: acct_mz_price_code,
		acct_mz_price_desc: g_acct_mz_price_desc,
		proposed_new_acct_mz_price_desc: acct_mz_price_desc,
		acct_mz_price_commision_disc: g_acct_mz_price_commision_disc,
		proposed_new_acct_mz_price_commision_disc: acct_mz_price_commision_disc,
		acct_mz_price_remarks: g_acct_mz_price_remarks,
		proposed_new_acct_mz_price_remarks: acct_mz_price_remarks,

		acct_nw_price_code: g_acct_nw_price_code,
		proposed_new_acct_nw_price_code: acct_nw_price_code,
		acct_nw_price_desc: g_acct_nw_price_desc,
		proposed_new_acct_nw_price_desc: acct_nw_price_desc,
		acct_nw_price_commision_disc: g_acct_nw_price_commision_disc,
		proposed_new_acct_nw_price_commision_disc: acct_nw_price_commision_disc,
		acct_nw_price_remarks: g_acct_nw_price_remarks,
		proposed_new_acct_nw_price_remarks: acct_nw_price_remarks,

		acct_ec_price_code: g_acct_ec_price_code,
		proposed_new_acct_ec_price_code: acct_ec_price_code,
		acct_ec_price_desc: g_acct_ec_price_desc,
		proposed_new_acct_ec_price_desc: acct_ec_price_desc,
		acct_ec_price_commision_disc: g_acct_ec_price_commision_disc,
		proposed_new_acct_ec_price_commision_disc: acct_ec_price_commision_disc,
		acct_ec_price_remarks: g_acct_ec_price_remarks,
		proposed_new_acct_ec_price_remarks: acct_ec_price_remarks,

		acct_ecu_price_code: g_acct_ecu_price_code,
		proposed_new_acct_ecu_price_code: acct_ecu_price_code,
		acct_ecu_price_desc: g_acct_ecu_price_desc,
		proposed_new_acct_ecu_price_desc: acct_ecu_price_desc,
		acct_ecu_price_commision_disc: g_acct_ecu_price_commision_disc,
		proposed_new_acct_ecu_price_commision_disc: acct_ecu_price_commision_disc,
		acct_ecu_price_remarks: g_acct_ecu_price_remarks,
		proposed_new_acct_ecu_price_remarks: acct_ecu_price_remarks,

		acct_socio_eco_class: acct_socio_eco_class,
		acct_num_outlets: acct_num_outlets,
		list_of_outlets: list_of_outlets,
		list_of_events: list_of_events,
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
		acct_ccanum: acct_ccanum,
		other_changes: other_changes,

		apprver_remarks: remarks_,

		acct_ini_po_details: acct_iniPODetails //added line by BJD 05/31/2016
	};

	// send through ajx
	$.ajax({
		type: "POST", url: baseUrl + "Customer/SaveCustomer",
		data: $.param(params, true),
		success: function (res) {

			if (SrvResultMsg.GetMsgType(res) != "error") {
				// success
				alert("SUCCESSFULLY SAVED!");

				// refresh
				location.reload();
			} else {
				// error
				alert(SrvResultMsg.GetMessage(res));
			}

			HidePreloader();
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status); alert(thrownError); HidePreloader();
		}
	});

}

function DeleteFileAttachment(attch_type) {

	var ccanum = "";
	var filename = "";
	var obj = "";

	DisplayPreloader();

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
	"doc_id=" + ccanum + "&" +
	"filename=" + filename,
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
				HidePreloader();
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert(xhr.status); alert(thrownError); HidePreloader();
			}
		});
	}

}

function MarkCustomerCreationDocument(val_action_type, val_ccanum, final_acct_code, val_pos_id) {
	/* val_mark_type = {'disapprove', 'endorse/verified/approve/passed', 'send back'} */
	DisplayPreloader();

	if ((val_action_type == "SEND_BACK_TO_REQUESTER" || val_action_type == "SEND_BACK_TO_CNC" || val_action_type == "DISAPPROVE") && $("input[obj=txt_doc_remarks]").attr('value') == "") {
		alert("PLEASE PROVIDE A REMARKS.");
		HidePreloader();
		return;
	}

	/* FOR CUSTOMER CODE CREATION */
	if (val_action_type == "APPROVE" && $("#txt_final_account_code").attr("value") != undefined && $("#txt_final_account_code").attr("value") == "") {
		alert("[FINAL ACCOUNT CODE] CANNOT BE EMPTY.");
		HidePreloader();
		return;
	}

	// check if Supplier info/CIBI is empty
	if (($("#txt_cibi_remakrs").attr('value') == "" || $("#txt_supplyinfo_remakrs").attr('value') == "" || g_number_of_banks < 1) && val_pos_id == "7" && val_action_type == "APPROVE") {
		alert("CIBI REMARKS/SUPPLIER INFO/LIST OF BANKS REMARKS IS/ARE EMPTY");
		HidePreloader(); return;
	}

	var remarks = "";
	if ($("input[obj=txt_doc_remarks]").attr('value') != undefined) {
		remarks = $("input[obj=txt_doc_remarks]").attr('value');
	}

	var acct_classification = "";
	acct_classification = $("#txt_acct_classification").attr('value');

	// AMPERSAND
	remarks = EncodeAmpersand(remarks);

	if (final_acct_code == undefined) { final_acct_code = ""; }

	if (!isProcessing) {
		isProcessing = true;
		$.ajax({
			type: "POST", url: baseUrl + "Customer/MarkCustomerCreationDocument",
			data:
			"action_type=" + val_action_type + "&" +
			"acct_ccanum=" + val_ccanum + "&" +
			"remarks=" + remarks + "&" +
			"final_acct_code=" + final_acct_code + "&" +
			"acct_classification=" + acct_classification + "&" +
			"DocChangesRouteType=5"
			,
			success: function (res) {
				if (SrvResultMsg.GetMsgType(res) != "error") {
					// success
					alert("SUCCESSFULLY SAVED!");
					location.reload();
				} else {
					// error
					alert(SrvResultMsg.GetMessage(res));
				}

				isProcessing = false;
				HidePreloader();
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert(xhr.status); alert(thrownError); HidePreloader();
				isProcessing = false;
			}
		});
	}
}

function MarkDocChangesStatus(val_action_type, val_ccanum, val_pos_id) {
	DisplayPreloader();

	if ((val_action_type == "SEND_BACK_TO_REQUESTER" || val_action_type == "SEND_BACK_TO_CNC" || val_action_type == "DISAPPROVE") && $("input[obj=txt_doc_remarks]").attr('value') == "") {
		alert("PLEASE PROVIDE A REMARKS.");
		HidePreloader();
		return;
	}

	// check if Supplier info/CIBI is empty
	if (($("#txt_cibi_remakrs").attr('value') == "" || $("#txt_supplyinfo_remakrs").attr('value') == "" || g_number_of_banks < 1) && val_pos_id == "7" && val_action_type == "APPROVE") {
		alert("CIBI REMARKS/SUPPLIER INFO/LIST OF BANKS REMARKS IS/ARE EMPTY");
		HidePreloader(); return;
	}

	var remarks = "";
	if ($("input[obj=txt_doc_remarks]").attr('value') != undefined) {
		remarks = $("input[obj=txt_doc_remarks]").attr('value');
	}

	remarks = EncodeAmpersand(remarks);

	var params = {
		action_type: val_action_type,
		acct_ccanum: val_ccanum,
		remarks: remarks
	};

	var jparam = $.param(params, true);
	if (!isProcessing_) {
		isProcessing_ = true;
		$.ajax({
			type: "POST", url: baseUrl + "Customer/MarkDocumentChanges",
			data: params,
			success: function (res) {

				if (SrvResultMsg.GetMsgType(res) != "error") {
					alert("SUCCESSFULLY SAVED!");
					location.reload();
				} else {
					alert(SrvResultMsg.GetMessage(res));
				}
				HidePreloader();
				isProcessing_ = false;
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert(xhr.status); alert(thrownError); HidePreloader();
				isProcessing_ = false;
			}
		});
	}
}

function SaveCIInfo(DisplayAlertMessage) {
	DisplayPreloader();

	var acct_code = "";
	acct_code = $("#txt_acct_code").attr('value');

	var list_of_bank = new Array();
	row_count = $("#tbl_bank_list tr").length;
	var loop_count = 0;
	$("#tbl_bank_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_bank.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(5) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(6) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(7) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(8) input[type=text]").attr('value')
				);
			}
		}
	);


	// [type, area, location, owned by]
	var list_of_land = new Array();
	row_count = $("#tbl_land_list tr").length;
	var loop_count = 0;
	$("#tbl_land_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_land.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);

	// [type, area, location, owned by]
	var list_of_building = new Array();
	row_count = $("#tbl_building_list tr").length;
	var loop_count = 0;
	$("#tbl_building_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_building.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);

	// [type, model, quantity]
	var list_of_vehicle = new Array();
	row_count = $("#tbl_vehicle_list tr").length;
	var loop_count = 0;
	$("#tbl_vehicle_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_vehicle.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
				);
			}
		}
	);

	// other assets
	var acct_other_assets = "";
	acct_other_assets = $("#txt_other_assets").attr('value');

	// [registed name, nature, location, ownership]
	var list_of_assets = new Array();
	row_count = $("#tbl_asset_list tr").length;
	var loop_count = 0;
	$("#tbl_asset_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_assets.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);

	/* GET account classification and ccanum */
	var acct_ccanum = "";
	acct_ccanum = $("#txt_acct_ccanum").attr('value');

	// ADDITIONAL FIELDS
	var acct_cibi_remarks = "";
	acct_cibi_remarks = $("#txt_cibi_remakrs").attr('value');

	var acct_supplyinfo_remarks = "";
	acct_supplyinfo_remarks = $("#txt_supplyinfo_remakrs").attr('value');

	var params = {
		acct_code: acct_code,
		list_of_bank: list_of_bank,
		list_of_land: list_of_land,
		list_of_building: list_of_building,
		list_of_vehicle: list_of_vehicle,
		acct_other_assets: acct_other_assets,
		list_of_assets: list_of_assets,
		acct_ccanum: acct_ccanum,
		acct_cibi_remarks: acct_cibi_remarks,
		acct_supplyinfo_remarks: acct_supplyinfo_remarks
	};

	// send through ajx
	$.ajax({
		type: "POST", url: baseUrl + "Customer/SaveCreditInvestigationInfo",
		data: $.param(params, true),
		success: function (res) {

			if (SrvResultMsg.GetMsgType(res) != "error") {
				if (DisplayAlertMessage == 'true') {
					// success
					alert("SUCCESSFULLY SAVED!");

					// refresh
					location.reload();
				}
			} else {
				// error
				alert(SrvResultMsg.GetMessage(res));
			}
			HidePreloader();
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status); alert(thrownError); HidePreloader();
		}
	});

}

function SwitchRequiredFields(val_classification, val_acct_type) {
	var rqd = "required_fields";
	if (val_classification == "REGULAR" && val_acct_type == "direct") {
		$("#txt_acct_code").addClass(rqd);
		$("#txt_acct_class").addClass(rqd);
		$("#txt_acct_name").addClass(rqd);
		$("#txt_acct_officer").addClass(rqd);
		$("#txt_phone_no").addClass(rqd);
		$("#txt_acct_territory").addClass(rqd);
		$("#txt_yrs_business").addClass(rqd);
		$("#txt_tax_id").addClass(rqd);
		$("#txt_vat_no").addClass(rqd);
		$("#txt_bir_reg").addClass(rqd);
		$("#txt_reg_name").addClass(rqd);
		$("#txt_business_add").addClass(rqd);
		$("#txt_delivery_add").addClass(rqd);
		$("#txt_sole_owner_name").addClass(rqd);
		$("#txt_sole_nationality").addClass(rqd);
		$("#tbl_partner_list tr td:nth-child(1) input[type=text]").addClass(rqd);
		$("#tbl_partner_list tr td:nth-child(2) input[type=text]").addClass(rqd);
		$("#txt_corpo_ceo").addClass(rqd);
		$("txt_bir_reg").addClass(rqd);

		/* Code added by Billy Jay (04/23/2015) */
		//if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
		$("#txt_credit_terms_architectural_brand").addClass(rqd);
		$("#txt_credit_terms_eco_lumber").addClass(rqd);

		$("#txt_order_limit_ab").addClass(rqd);
		$("#txt_order_limit_tr").addClass(rqd);
		//  $("#txt_credit_terms_eco_plywood").addClass(rqd);
		//}
		/* End Code added by Billy Jay (04/23/2015) */

		$("#txt_credit_terms").addClass(rqd);
		$("#txt_credit_limit").addClass(rqd);
		$("#txt_mw_price_code").addClass(rqd);
		$("#txt_mw_price_desc").addClass(rqd);
		$("#txt_ww_price_code").addClass(rqd);
		$("#txt_ww_price_desc").addClass(rqd);
		$("#txt_pwf_price_code").addClass(rqd);
		$("#txt_pwf_price_desc").addClass(rqd);
		$("#txt_pwr_price_code").addClass(rqd);
		$("#txt_pwr_price_desc").addClass(rqd);
		$("#txt_gw_price_code").addClass(rqd);
		$("#txt_gw_price_desc").addClass(rqd);
		$("#txt_tw_price_code").addClass(rqd);
		$("#txt_tw_price_desc").addClass(rqd);

		$("#txt_mz_price_code").addClass(rqd);
		$("#txt_mz_price_desc").addClass(rqd);
		$("#txt_nw_price_code").addClass(rqd);
		$("#txt_nw_price_desc").addClass(rqd);
		$("#txt_ec_price_code").addClass(rqd);
		$("#txt_ec_price_desc").addClass(rqd);
		$("#txt_ecu_price_code").addClass(rqd);
		$("#txt_ecu_price_desc").addClass(rqd);

		// txt_articles_of_inc was renamed into [Customer Info Sheet]
		$("#txt_articles_of_inc").addClass(rqd);

	} else {
		if (val_classification == "REGULAR") {
			$("#txt_acct_code").addClass(rqd);
			$("#txt_acct_class").addClass(rqd);
			$("#txt_acct_name").addClass(rqd);
			$("#txt_acct_officer").addClass(rqd);
			$("#txt_phone_no").addClass(rqd);
			$("#txt_acct_territory").addClass(rqd);
			$("#txt_tax_id").addClass(rqd);
			$("#txt_vat_no").addClass(rqd);

			//REQUIRED ATTACHMENT REMOVED.
			//$("#txt_bir_reg").addClass(rqd);

			// txt_articles_of_inc was renamed into [Customer Info Sheet]
			//$("#txt_articles_of_inc").addClass(rqd);

		} else {
			$("#txt_acct_code").addClass(rqd);
			$("#txt_acct_class").addClass(rqd);
			$("#txt_acct_name").addClass(rqd);
			$("#txt_acct_officer").addClass(rqd);
			$("#txt_phone_no").addClass(rqd);
			$("#txt_acct_territory").addClass(rqd);
			$("#txt_tax_id").addClass(rqd);
			$("#txt_vat_no").addClass(rqd);

			/* Code added by Billy Jay (04/23/2015) */
			//if ($("#txt_acct_territory").attr("grp_name_") == "GT") {
			$("#txt_credit_terms_architectural_brand").addClass(rqd);
			$("#txt_credit_terms_eco_lumber").addClass(rqd);

			$("#txt_order_limit_ab").addClass(rqd);
			$("#txt_order_limit_tr").addClass(rqd);
			//$("#txt_credit_terms_eco_plywood").addClass(rqd);
			/* End Code added by Billy Jay (04/23/2015) */
			//}
			$("#txt_credit_terms").addClass(rqd);
			$("#txt_credit_limit").addClass(rqd);
			$("#txt_mw_price_code").addClass(rqd);
			$("#txt_mw_price_desc").addClass(rqd);
			$("#txt_ww_price_code").addClass(rqd);
			$("#txt_ww_price_desc").addClass(rqd);
			$("#txt_pwf_price_code").addClass(rqd);
			$("#txt_pwf_price_desc").addClass(rqd);
			$("#txt_pwr_price_code").addClass(rqd);
			$("#txt_pwr_price_desc").addClass(rqd);
			$("#txt_gw_price_code").addClass(rqd);
			$("#txt_gw_price_desc").addClass(rqd);
			$("#txt_tw_price_code").addClass(rqd);
			$("#txt_tw_price_desc").addClass(rqd);

			$("#txt_mz_price_code").addClass(rqd);
			$("#txt_mz_price_desc").addClass(rqd);
			$("#txt_nw_price_code").addClass(rqd);
			$("#txt_nw_price_desc").addClass(rqd);
			$("#txt_ec_price_code").addClass(rqd);
			$("#txt_ec_price_desc").addClass(rqd);
			$("#txt_ecu_price_code").addClass(rqd);
			$("#txt_ecu_price_desc").addClass(rqd);
		}
	}
}

function RemoveClass() {
	var rqd = "required_fields";
	$("#txt_acct_code").removeClass(rqd);
	$("#txt_acct_class").removeClass(rqd);
	$("#txt_acct_name").removeClass(rqd);
	$("#txt_acct_officer").removeClass(rqd);
	$("#txt_fax_no").removeClass(rqd);
	$("#txt_phone_no").removeClass(rqd);
	$("#txt_acct_territory").removeClass(rqd);
	$("#txt_yrs_business").removeClass(rqd);
	$("#txt_tax_id").removeClass(rqd);
	$("#txt_vat_no").removeClass(rqd);
	$("#txt_bir_reg").removeClass(rqd);
	$("#txt_reg_name").removeClass(rqd);
	$("#txt_business_add").removeClass(rqd);
	$("#txt_delivery_add").removeClass(rqd);
	$("#txt_sole_owner_name").removeClass(rqd);
	$("#txt_sole_nationality").removeClass(rqd);
	$("#txt_sole_gen_manager").removeClass(rqd);
	$("#txt_sole_fin_manager").removeClass(rqd);
	$("#txt_sole_others").removeClass(rqd);
	$("#tbl_partner_list tr td input[type=text]").removeClass(rqd);
	$("#txt_partner_gen_manager").removeClass(rqd);
	$("#txt_partner_fin_manager").removeClass(rqd);
	$("#txt_partner_others").removeClass(rqd);
	$("#txt_corpo_inc_date").removeClass(rqd);
	$("#txt_corpo_auth_cap_stock").removeClass(rqd);
	$("#txt_corpo_subscb_cap_stock").removeClass(rqd);
	$("#txt_corpo_paidin_cap_stock").removeClass(rqd);
	$("#tbl_corpo_list tr td input[type=text]").removeClass(rqd);
	$("#txt_corpo_ceo").removeClass(rqd);
	$("#txt_corpo_vp_fin").removeClass(rqd);
	$("#txt_corpo_gen_man").removeClass(rqd);
	$("#txt_credit_terms").removeClass(rqd);

	/* Code added by Billy Jay (04/23/2015) */
	$("#txt_credit_terms_architectural_brand").removeClass(rqd);
	$("#txt_credit_terms_eco_lumber").removeClass(rqd);
	$("#txt_credit_terms_eco_plywood").removeClass(rqd);

	$("#txt_order_limit_ab").removeClass(rqd);
	$("#txt_order_limit_tr").removeClass(rqd);
	/* End Code added by Billy Jay (04/23/2015) */

	$("#txt_credit_limit").removeClass(rqd);
	$("#txt_mw_price_code").removeClass(rqd);
	$("#txt_mw_price_desc").removeClass(rqd);
	$("#txt_ww_price_code").removeClass(rqd);
	$("#txt_ww_price_desc").removeClass(rqd);
	$("#txt_pwf_price_code").removeClass(rqd);
	$("#txt_pwf_price_desc").removeClass(rqd);
	$("#txt_pwr_price_code").removeClass(rqd);
	$("#txt_pwr_price_desc").removeClass(rqd);
	$("#txt_gw_price_code").removeClass(rqd);
	$("#txt_gw_price_desc").removeClass(rqd);
	$("#txt_tw_price_code").removeClass(rqd);
	$("#txt_tw_price_desc").removeClass(rqd);

	$("#txt_mz_price_code").removeClass(rqd);
	$("#txt_mz_price_desc").removeClass(rqd);
	$("#txt_nw_price_code").removeClass(rqd);
	$("#txt_nw_price_desc").removeClass(rqd);
	$("#txt_ec_price_code").removeClass(rqd);
	$("#txt_ec_price_desc").removeClass(rqd);
	$("#txt_ecu_price_code").removeClass(rqd);
	$("#txt_ecu_price_desc").removeClass(rqd);

	$("#txt_bir_reg").removeClass(rqd);
	$("#txt_articles_of_inc").removeClass(rqd);
}

function MarkAndApprove_FNM(val_action_type, val_ccanum) {

	if ($("#txt_final_account_code").attr('value') == "") {
		alert("PLEASE FILL IN THE REQUIRED FIELDS!");
		return;
	}

	var final_acct_code = $("#txt_final_account_code").attr('value');

	MarkCustomerCreationDocument(val_action_type, val_ccanum, final_acct_code);

}

function GetDocChanges() {
	var str_changes = "";
	var obj = "";

	var list_of_object = new Array();

	list_of_object.push({ obj: "txt_acct_classification", desc: "Account Classification" });
	list_of_object.push({ obj: "txt_category", desc: "Account Category" });
	list_of_object.push({ obj: "txt_transtype", desc: "Transaction Type" });
	list_of_object.push({ obj: "txt_buss_class", desc: "Business Class" });
	list_of_object.push({ obj: "txt_type_of_account", desc: "Type of Account" });
	list_of_object.push({ obj: "txt_acct_code", desc: "Account Code" });
	list_of_object.push({ obj: "txt_acct_name", desc: "Account Name" });
	list_of_object.push({ obj: "txt_phone_no", desc: "Phone Number" });
	list_of_object.push({ obj: "txt_phone_no_2", desc: "Phone Number 2" });
	list_of_object.push({ obj: "txt_cellphone", desc: "Cellphone" });
	list_of_object.push({ obj: "txt_acct_officer", desc: "Account Officer" });
	list_of_object.push({ obj: "txt_fax_no", desc: "Fax No" });
	list_of_object.push({ obj: "txt_acct_territory", desc: "Territory" });
	list_of_object.push({ obj: "txt_email_add", desc: "Email Add" });
	list_of_object.push({ obj: "txt_office_hours", desc: "Office Hours" });
	list_of_object.push({ obj: "txt_area", desc: "Area" });
	list_of_object.push({ obj: "txt_store_hours", desc: "Store Hours" });
	list_of_object.push({ obj: "txt_region", desc: "Region" });
	list_of_object.push({ obj: "txt_yrs_business", desc: "No. of Years in Business" });
	list_of_object.push({ obj: "txt_yrs_matimco", desc: "No. of Years with Matimco" });
	list_of_object.push({ obj: "txt_tax_id", desc: "Tax ID" });
	list_of_object.push({ obj: "txt_vat_no", desc: "Vat Type" });
	list_of_object.push({ obj: "txt_reg_name", desc: "Registered Name" });
	list_of_object.push({ obj: "txt_business_add", desc: "Business Address" });
	list_of_object.push({ obj: "txt_delivery_add", desc: "Delivery Address" });
	list_of_object.push({ obj: "txt_no_employees", desc: "No. of Employees" });
	list_of_object.push({ obj: "txt_sole_owner_name", desc: "[Sole] Owner Name" });
	list_of_object.push({ obj: "txt_sole_nationality", desc: "[Sole] Nationality" });
	list_of_object.push({ obj: "txt_sole_gen_manager", desc: "[Sole] Gen. Manager" });
	list_of_object.push({ obj: "txt_sole_fin_manager", desc: "[Sole] Fin. Manager" });
	list_of_object.push({ obj: "txt_partner_gen_manager", desc: "[Partnership] Gen. Manager" });
	list_of_object.push({ obj: "txt_partner_fin_manager", desc: "[Partnership] Fin. Manager" });
	list_of_object.push({ obj: "txt_corpo_inc_date", desc: "[Corpo] Incorporation Date" });
	list_of_object.push({ obj: "txt_corpo_auth_cap_stock", desc: "[Corpo] Authorized Cap. Stock" });
	list_of_object.push({ obj: "txt_corpo_subscb_cap_stock", desc: "[Corpo] Subscribed Cap. Stock" });
	list_of_object.push({ obj: "txt_corpo_paidin_cap_stock", desc: "[Corpo] Paid-In Cap. Stock" });
	list_of_object.push({ obj: "txt_corpo_ceo", desc: "[Corpo] CEO" });
	list_of_object.push({ obj: "txt_corpo_vp_fin", desc: "[Corpo] VP Fin." });
	list_of_object.push({ obj: "txt_corpo_gen_man", desc: "[Corpo] Gen. Manager" });
	list_of_object.push({ obj: "txt_articles_of_inc", desc: "[Attachment] Customer Info Sheet" });
	list_of_object.push({ obj: "txt_financial_statement", desc: "[Attachment] Bank Authorization" });
	list_of_object.push({ obj: "txt_ITR", desc: "[Attachment] ITR" });
	list_of_object.push({ obj: "txt_bir_reg", desc: "[Attachment] BIR Reg." });
	list_of_object.push({ obj: "txt_business_permit", desc: "[Attachment] Business Permit" });

	/* Code added by Billy Jay (04/23/2015) */
	list_of_object.push({ obj: "txt_credit_terms_architectural_brand", desc: "Architectural Brand Credit Terms" });
	list_of_object.push({ obj: "txt_credit_terms_eco_lumber", desc: "Ecofor Lumber Credit Terms" });
	list_of_object.push({ obj: "txt_credit_terms_eco_plywood", desc: "Ecofor Plywood Credit Terms" });

	list_of_object.push({ obj: "txt_credit_terms_architectural_brand_remarks", desc: "Architectural Brand Credit Terms Remarks" });
	list_of_object.push({ obj: "txt_credit_terms_eco_lumber_remarks", desc: "Ecofor Lumber Credit Terms Remarks" });
	list_of_object.push({ obj: "txt_credit_terms_eco_plywood_remarks", desc: "Ecofor Plywood Credit Terms Remarks" });

	list_of_object.push({ obj: "txt_order_limit_ab", desc: "AB Order Limit" });
	list_of_object.push({ obj: "txt_order_limit_tr", desc: "TR Order Limit" });

	list_of_object.push({ obj: "txt_order_limit_remarks_ab", desc: "AB Order Limit Remarks" });
	list_of_object.push({ obj: "txt_order_limit_remarks_tr", desc: "TR Order Limit Remarks" });
	/* End Code added by Billy Jay (04/23/2015) */

	list_of_object.push({ obj: "txt_credit_terms", desc: "Credit Terms" });
	list_of_object.push({ obj: "txt_credit_limit", desc: "Credit Limit" });
	list_of_object.push({ obj: "txt_credit_terms_remarks", desc: "Credit Terms Remarks" });
	list_of_object.push({ obj: "txt_credit_limit_remarks", desc: "Credit Limit Remarks" });
	list_of_object.push({ obj: "txt_mw_price_code", desc: "Matwood Price Code" });
	list_of_object.push({ obj: "txt_ww_price_code", desc: "Weatherwood Price Code" });
	list_of_object.push({ obj: "txt_pwf_price_code", desc: "PCW Frames Price Code" });
	list_of_object.push({ obj: "txt_pwr_price_code", desc: "PCW Regular Price Code" });
	list_of_object.push({ obj: "txt_gw_price_code", desc: "Gudwood Price Code" });
	list_of_object.push({ obj: "txt_tw_price_code", desc: "Trusswood Price Code" });

	list_of_object.push({ obj: "txt_mz_price_code", desc: "Muzuwood Price Code" });
	list_of_object.push({ obj: "txt_nw_price_code", desc: "Nuwood Price Code" });
	list_of_object.push({ obj: "txt_ec_price_code", desc: "Ecofor (Treated) Price Code" });
	list_of_object.push({ obj: "txt_ecu_price_code", desc: "Ecofor (UnTreated) Price Code" });

	list_of_object.push({ obj: "txt_mw_price_remarks", desc: "Matwood Price Remarks" });
	list_of_object.push({ obj: "txt_ww_price_remarks", desc: "Weatherwood Price Remarks" });
	list_of_object.push({ obj: "txt_pwf_price_remarks", desc: "PCW Frames Price Remarks" });
	list_of_object.push({ obj: "txt_pwr_price_remarks", desc: "PCW Regular Price Remarks" });
	list_of_object.push({ obj: "txt_gw_price_remarks", desc: "Gudwood Price Remarks" });
	list_of_object.push({ obj: "txt_tw_price_remarks", desc: "Trusswood Price Remarks" });

	list_of_object.push({ obj: "txt_mz_price_remarks", desc: "Muzuwood Price Remarks" });
	list_of_object.push({ obj: "txt_nw_price_remarks", desc: "Nuwood Price Remarks" });
	list_of_object.push({ obj: "txt_ec_price_remarks", desc: "Ecofor (Treated) Price Remarks" });
	list_of_object.push({ obj: "txt_ecu_price_remarks", desc: "Ecofor (UnTreated) Price Remarks" });

	list_of_object.push({ obj: "txt_mw_price_commision_disc", desc: "Matwood Comm. and Disc." });
	list_of_object.push({ obj: "txt_ww_price_commision_disc", desc: "Weatherwood Comm. and Disc." });
	list_of_object.push({ obj: "txt_pwf_price_commision_disc", desc: "PCW Frames Comm. and Disc." });
	list_of_object.push({ obj: "txt_pwr_price_commision_disc", desc: "PCW Regular Comm. and Disc." });
	list_of_object.push({ obj: "txt_gw_price_commision_disc", desc: "Gudwood Comm. and Disc." });
	list_of_object.push({ obj: "txt_tw_price_commision_disc", desc: "Trusswood Comm. and Disc." });

	list_of_object.push({ obj: "txt_mz_price_commision_disc", desc: "Muzuwood Comm. and Disc." });
	list_of_object.push({ obj: "txt_nw_price_commision_disc", desc: "Nuwood Comm. and Disc." });
	list_of_object.push({ obj: "txt_ec_price_commision_disc", desc: "Ecofor (Treated) Comm. and Disc." });
	list_of_object.push({ obj: "txt_ecu_price_commision_disc", desc: "Ecofor (UnTreated) Comm. and Disc." });

	list_of_object.push({ obj: "txt_eco_class_of_customer", desc: "Eco class of Customer" });
	list_of_object.push({ obj: "txt_no_of_outlets", desc: "No. of Outlets" });

	$.each(list_of_object, function (key, value) {
		if ($("#" + value.obj).attr('orig_value') != undefined) {
			if ($("#" + value.obj).attr('orig_value') != $("#" + value.obj).attr('value')) {
				if (str_changes != "") { str_changes = str_changes + "^"; }
				str_changes =
					str_changes + value.desc + "|" +
					$("#" + value.obj).attr('orig_value') + "|" +
					$("#" + value.obj).attr('value');
			}
		}
	});

	return str_changes;
}


function CheckAcctcode() {

	// account code
	var final_acct_code = "";
	final_acct_code = $("#txt_final_account_code").attr('value');

	// ccanum
	var ccanum;
	ccanum = $("#txt_acct_ccanum").attr('value');

	if (final_acct_code == "") { return; }

	$.ajax({
		type: "POST", url: baseUrl + "Customer/CheckAcctcodeifFoundnSAP",
		data:
			"final_acct_code=" + final_acct_code + "&" +
			"ccanum=" + ccanum
			,
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

function SaveCIInfo_WithCredLT(DisplayAlertMessage) {
	DisplayPreloader();

	if ($("#txt_credit_limit").attr('value') == "" && $("#txt_order_limit_ab").attr('value') == "" && $("#txt_order_limit_tr").attr('value') == "") {
		alert("CREDIT LIMIT CANNOT BE EMPTY!");
		HidePreloader();
		return;
	}

	var acct_code = "";
	acct_code = $("#txt_acct_code").attr('value');

	var list_of_bank = new Array();
	row_count = $("#tbl_bank_list tr").length;
	var loop_count = 0;
	$("#tbl_bank_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_bank.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(5) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(6) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(7) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(8) input[type=text]").attr('value')
				);
			}
		}
	);

	// [type, area, location, owned by]
	var list_of_land = new Array();
	row_count = $("#tbl_land_list tr").length;
	var loop_count = 0;
	$("#tbl_land_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_land.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);


	// [type, area, location, owned by]
	var list_of_building = new Array();
	row_count = $("#tbl_building_list tr").length;
	var loop_count = 0;
	$("#tbl_building_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_building.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);

	// [type, model, quantity]
	var list_of_vehicle = new Array();
	row_count = $("#tbl_vehicle_list tr").length;
	var loop_count = 0;
	$("#tbl_vehicle_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_vehicle.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
				);
			}
		}
	);

	// other assets
	var acct_other_assets = "";
	acct_other_assets = $("#txt_other_assets").attr('value');

	// [registed name, nature, location, ownership]
	var list_of_assets = new Array();
	row_count = $("#tbl_asset_list tr").length;
	var loop_count = 0;
	$("#tbl_asset_list tr").each(
		function (index, element) {
			loop_count++;
			if (loop_count > 1 && loop_count < row_count) {
				list_of_assets.push(
					$(element).find("td:nth-child(1) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
					+ "|" + $(element).find("td:nth-child(4) input[type=text]").attr('value')
				);
			}
		}
	);

	/* GET account classification and ccanum */
	var acct_ccanum = "";
	acct_ccanum = $("#txt_acct_ccanum").attr('value');

	// ADDITIONAL FIELDS
	var acct_cibi_remarks = "";
	acct_cibi_remarks = $("#txt_cibi_remakrs").attr('value');

	var acct_supplyinfo_remarks = "";
	acct_supplyinfo_remarks = $("#txt_supplyinfo_remakrs").attr('value');

	// to shoten some text
	var c_terms = "txt_credit_terms";
	var c_limit = "txt_credit_limit";

	/* Code added by Billy Jay (04/23/2015) */

	var o_limit_ab = "txt_order_limit_ab";
	var o_limit_tr = "txt_order_limit_tr";

	var c_terms_architectural_brand = "txt_credit_terms_architectural_brand";
	var c_terms_ecofor_lumber = "txt_credit_terms_eco_lumber";
	var c_terms_ecofor_plywood = "txt_credit_terms_eco_plywood";

	var acct_prop_credit_terms_architectural_brand = "";
	acct_prop_credit_terms_architectural_brand = $("#" + c_terms_architectural_brand).attr('value');

	var acct_prop_credit_terms_ecofor_lumber = "";
	acct_prop_credit_terms_ecofor_lumber = $("#" + c_terms_ecofor_lumber).attr('value');

	var acct_prop_credit_terms_ecofor_plywood = "";
	acct_prop_credit_terms_ecofor_plywood = $("#" + c_terms_ecofor_plywood).attr('value');

	// proposed order limit ab
	var acct_prob_order_limit_ab = "";
	acct_prob_order_limit_ab = $("#" + o_limit_ab).attr('value');

	// proposed order limit tr
	var acct_prob_order_limit_tr = "";
	acct_prob_order_limit_tr = $("#" + o_limit_tr).attr('value');
	/* End Code added by Billy Jay (04/23/2015) */

	// proposed credit terms
	var acct_prop_credit_term = "";
	acct_prop_credit_term = $("#" + c_terms).attr('value');

	// proposed credit limit
	var acct_prop_credit_limit = "";
	acct_prop_credit_limit = $("#" + c_limit).attr('value');

	// get the changes made
	var str_changes = "";
	if ($("#" + c_terms).attr('orig_value') != null) {
		if ($("#" + c_terms).attr('orig_value') != acct_prop_credit_term) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "Credit Terms" + "|" + $("#" + c_terms).attr('orig_value') + "|" + acct_prop_credit_term;
		}
	}

	/* Code added by Billy Jay (04/23/2015) */
	if ($("#" + c_terms).attr('orig_value') != null) {
		if ($("#" + c_terms).attr('orig_value') != acct_prop_credit_terms_architectural_brand) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "Credit Terms Architectural Brand" + "|" + $("#" + c_terms_architectural_brand).attr('orig_value') + "|" + acct_prop_credit_terms_architectural_brand;
		}
	}
	if ($("#" + c_terms).attr('orig_value') != null) {
		if ($("#" + c_terms).attr('orig_value') != acct_prop_credit_terms_ecofor_lumber) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "Credit Terms Ecofor Lumber" + "|" + $("#" + c_terms_ecofor_lumber).attr('orig_value') + "|" + acct_prop_credit_terms_ecofor_lumber;
		}
	}
	if ($("#" + c_terms).attr('orig_value') != null) {
		if ($("#" + c_terms).attr('orig_value') != acct_prop_credit_terms_ecofor_plywood) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "Credit Terms Ecofor Plywood" + "|" + $("#" + c_terms_ecofor_plywood).attr('orig_value') + "|" + acct_prop_credit_terms_ecofor_plywood;
		}
	}

	if ($("#" + o_limit_ab).attr('orig_value') != null) {
		if ($("#" + o_limit_ab).attr('orig_value') != acct_prob_order_limit_ab) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "AB Order Limit" + "|" + $("#" + o_limit_ab).attr('orig_value') + "|" + acct_prob_order_limit_ab;
		}
	}
	if ($("#" + o_limit_tr).attr('orig_value') != null) {
		if ($("#" + o_limit_ab).attr('orig_value') != acct_prob_order_limit_tr) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "TR Order Limit" + "|" + $("#" + o_limit_tr).attr('orig_value') + "|" + acct_prob_order_limit_tr;
		}
	}
	/* End Code added by Billy Jay (04/23/2015) */

	if ($("#" + c_limit).attr('orig_value') != null) {
		if ($("#" + c_limit).attr('orig_value') != acct_prop_credit_limit) {
			if (str_changes != "") { str_changes = str_changes + "^"; }
			str_changes = str_changes + "Credit Limit" + "|" + $("#" + c_limit).attr('orig_value') + "|" + acct_prop_credit_limit;
		}
	}


	var params = {
		acct_code: acct_code,
		list_of_bank: list_of_bank,
		list_of_land: list_of_land,
		list_of_building: list_of_building,
		list_of_vehicle: list_of_vehicle,
		acct_other_assets: acct_other_assets,
		list_of_assets: list_of_assets,
		acct_ccanum: acct_ccanum,
		acct_cibi_remarks: acct_cibi_remarks,
		acct_supplyinfo_remarks: acct_supplyinfo_remarks,

		/* Code added by Billy Jay (04/23/2015) */
		acct_prop_credit_terms_architectural_brand: acct_prop_credit_terms_architectural_brand,
		acct_prop_credit_terms_ecofor_lumber: acct_prop_credit_terms_ecofor_lumber,
		acct_prop_credit_terms_ecofor_plywood: acct_prop_credit_terms_ecofor_plywood,

		acct_prob_order_limit_ab: undoAddComma(acct_prob_order_limit_ab),
		acct_prob_order_limit_tr: undoAddComma(acct_prob_order_limit_tr),
		/* End Code added by Billy Jay (04/23/2015) */

		acct_prop_credit_term: acct_prop_credit_term,
		acct_prop_credit_limit: acct_prop_credit_limit,
		str_changes: str_changes
	};

	// send through ajx
	$.ajax({
		type: "POST", url: baseUrl + "Customer/SaveEditedInfo",
		data: $.param(params, true),
		success: function (res) {

			if (SrvResultMsg.GetMsgType(res) != "error") {

				if (DisplayAlertMessage == 'true') {
					// success
					alert("SUCCESSFULLY SAVED!");

					// refresh
					location.reload();
				}
			} else {
				// error
				alert(SrvResultMsg.GetMessage(res));
			}
			HidePreloader();
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status); alert(thrownError); HidePreloader();
		}
	});

}

function ShowBubble(strvalue, obj) {
	var w = "" +
		"<div id=\"div_bubbler\" style=\"position:absolute; padding:3px; background:#e2f4ff; width:100px;\" >" +
			"<a href=\"javascript:;\" onclick=\"javascript:PreviewBubble();\" ><img src=\"" + baseUrl + "Images/magnifier.png\" style=\"border:0;\" /></a>" +
			"<p style=\"border:1px solid #cdcdcd; font-size:12px; font-family:arial; padding:5px; background:#e2f4ff; position:absolute; display:none;\">" +
				strvalue +
			"</p>" +
		"</div>" +
		"";

	$("body").after(w);

	$("#div_bubbler").bind("mouseout", function (e) {
		HideBubble();
	});

	// set position
	var p = obj;
	var position = p.offset();
	var btnY = position.left;
	var btnX = position.top - ($(obj).height() + 3);

	$("#div_bubbler").css('top', btnX + '' + 'px');
	$("#div_bubbler").css('left', btnY + '' + 'px');

	// show 
	$("#div_bubbler").show();
}

function PreviewBubble() {
	$("#div_bubbler p").show();
}

function HideBubble() {
	$("#div_bubbler").hide();
	$("#div_bubbler").unbind('mouseout');
	$("div[id=div_bubbler]").remove();
}

// KEY PRESS
// TAX ID
function txt_tax_id_onkeypress(e) {
	var obj = "txt_tax_id";
	var keycode = (e.keyCode) ? e.keyCode : e.which;
	if (keycode == 8) return true;

	if (!(keycode > 47 && keycode < 58) || $("#" + obj).attr('value').length == 15) {
		return false;
	} else {
		if ($("#" + obj).attr('value').length == 3 || $("#" + obj).attr('value').length == 7 || $("#" + obj).attr('value').length == 11) {
			$("#" + obj).attr('value', $("#" + obj).attr('value') + '-');
		}
	}
	return true;
}

// txt_credit_limit
function txt_creditlimit_onkeypress(e) {

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

	$("#" + text_object).bind("click",
		function (e) {
			$("#" + text_object).select();
		}
	);

	$("#" + text_object).bind("keypress",
		function (e) {
			if (($(this).attr("value").length == 0 || $(this).attr("value").indexOf(".") > -1) && e.which == 46) return false;
			if ($(this).attr("value").length == 0 && e.which == 48) return false;
			if ((e.which >= 48 && e.which <= 57) || e.which == 46 || e.which == 08) return true;
			if (e.which == 8 || e.which == 46) return true;
			return false;
		}
	);

	$("#" + text_object).bind("keyup",
		function (e) {
			if ((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || e.which == 08) {
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

function isNumeric(n) {
	n = n.replace(/,/g, "");
	return !isNaN(parseFloat(n)) && isFinite(n);
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

function UpdateCustSapUpdate() {
	DisplayPreloader();

	var ccanum = "";
	ccanum = $("#txt_acct_ccanum").attr('value');

	$.ajax({
		type: "POST", url: baseUrl + "Customer/UpdateCustSapUpdate",
		data: "ccanum=" + ccanum,
		success: function (res) {

			if (SrvResultMsg.GetMsgType(res) != "error") {
				// success
				location.reload();
			} else {
				// error
				alert(SrvResultMsg.GetMessage(res));
			}

			HidePreloader();
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status); alert(thrownError); HidePreloader();
		}
	});
}

function ShowReportViewer(rpt_name) {

	var w = "";
	var params = new Array();

	var acct_code = $("#txt_acct_code").attr("value");
	var final_acct_code = $("#txt_final_acct_code").attr("value");

	if (final_acct_code != "" && final_acct_code != undefined) {
		params["cardcode"] = final_acct_code;
	} else {
		params["cardcode"] = acct_code;
	}

	window.parent.HideMenuFrame();

	switch (rpt_name) {
		case "pending_orders":
			params["page"] = "pending_orders"; break;
		case "mtd_sales":
			break;
		case "deliveries":
			params["page"] = "deliveries"; break;
		case "balance_order":
			params["page"] = "balance_orders"; break;
		case "collections":
			params["page"] = "collections"; break;
		case "account_balance":
			params["page"] = "account_balance"; break;
		case "bounce_checks":
			params["page"] = "bounce_checks"; break;
		case "mtdytd_sales":
			params["page"] = "mtdytd_sales"; break;
		case "past_dues":
			params["page"] = "past_dues"; break;
		default:
	}

	CB_REPORT(params);
}

function CB_REPORT(parameters) {
	var processed_parameters = "";

	for (var arr_name in parameters) {
		if (processed_parameters != "") { processed_parameters = processed_parameters + "&"; }
		processed_parameters = processed_parameters + arr_name + "=" + parameters[arr_name];
	}

	var w = "";
	w = w + "<div id=\"html_report_maker_background\" style=\" opacity: 0.60; filter:alpha(opacity=60); background:#3a3a3a; position:fixed; top:0; left:0; height:100%; width:100%; \" >";
	w = w + "</div>";

	w = w + "<div id=\"html_report_maker_content\" style=\"padding:0px; background:white; position:fixed; top:50%; left:50%; margin-left:-400px; margin-top:-300px; width:800px; height:600px; \" >";
	w = w + "<div style=\"background:#3b6bac; padding:2px; \"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ></div>";
	w = w + "<iframe frameborder=\"0\" style=\"width:796px; height:570px; \" src=\"" + baseUrl + "Reports/Page/" + parameters["page"] + ".aspx?" + processed_parameters + "\">";
	w = w + "</iframe>";

	w = w + "</div>";

	$("body").after(w);

	$("#html_report_maker_background").show();
	$("#html_report_maker_content").show("fast");
}

function HideHTMLReport() {
	window.parent.ShowMenuFrame();

	$("#html_report_maker_content").hide("fast");
	$("#html_report_maker_background").hide();

	$("#html_report_maker_content").remove();
	$("#html_report_maker_background").remove();
}

function SetBubbles() {
	// tobubble
	var str_input_bubble = "input[tobubble=tobubble]";
	$(str_input_bubble).live("mouseover",
		function () {
			if (!$("#div_bubbler_holder").is(":visible"))
				CreateBubble($(this).attr("value"), $(this));
		}
	);

	$(str_input_bubble).live("focusin",
		function () {
			if (!$("#div_bubbler_holder").is(":visible"))
				CreateBubble($(this).attr("value"), $(this));
		}
	);

	$(str_input_bubble).live("focusout",
		function () {
			HideBubbler();
		}
	);

	$(str_input_bubble).live("mouseout",
		function () {
			HideBubbler();
		}
	);
}

function CreateBubble(value, elem) {
	var w = "";

	var obj_h = "#div_bubbler_holder";
	var obj_h_bkg = "#div_bubbler_holder_bkg";

	w = w + "";
	w = w + "<div id=\"div_bubbler_holder_bkg\" style=\"position:absolute; background:#595959; opacity: 0.40; filter:alpha(opacity=40) padding:6px; border:1px solid #595959; color:#595959; \" >";
	w = w + value;
	w = w + "</div>";
	w = w + "<div id=\"div_bubbler_holder\" style=\"max-width:250px; position:absolute; background:#276497; color:#ffffff; padding:3px; \" >";
	w = w + value;
	w = w + "</div>";
	w = w + "";

	$("body").append(w);

	// position
	var btnY = getElLeft($(elem)[0]);
	var btnX = getElTop($(elem)[0]);
	var btnHeight = $(elem).height() + "px";
	var btnWidth = $(elem).width() + "px";

	var content_final_width = $("#div_bubbler_holder").width() + 12;
	var content_final_height = $("#div_bubbler_holder").height() + 12;

	var content_final_top = parseInt(btnX) + parseInt(btnHeight) + 10;
	var content_final_left = parseInt(btnY) - 20;

	$(obj_h).css('top', content_final_top + '' + 'px');
	$(obj_h).css('left', content_final_left + '' + 'px');

	$(obj_h_bkg).css('top', (content_final_top - 4) + '' + 'px');
	$(obj_h_bkg).css('left', (content_final_left - 4) + '' + 'px');

	$(obj_h_bkg).css('width', content_final_width + '' + 'px');
	$(obj_h_bkg).css('height', content_final_height + '' + 'px');

	$(obj_h).show("slow");
	$(obj_h_bkg).show("slow");

}

function HideBubbler() {
	var obj_h = "#div_bubbler_holder";
	var obj_h_bkg = "#div_bubbler_holder_bkg";

	$(obj_h).hide("fast");
	$(obj_h).remove();

	$(obj_h_bkg).hide("fast");
	$(obj_h_bkg).remove();
}

function BindApproverButtons() {
	// customer creation
	var ccanum = $("#txt_acct_ccanum").attr("value");

	$("#btn_save").click(function (e) {
		Save_Doc();
	});

	$("input[bname=cust_create_approve]").click(function (e) {
		if ($(this).attr("pos_id") != "" && $(this).attr("pos_id") != undefined) {
			SaveCIInfo('false'); //modified oct 25 2013 (parameter to show alert message or not)
			MarkCustomerCreationDocument('APPROVE', ccanum, '', $(this).attr("pos_id"));
		} else if ($(this).attr("for_cust_code_create") == "YES") {
			SaveCIInfo_WithCredLT('false');  //modified oct 25 2013
			MarkAndApprove_FNM('APPROVE', ccanum);
		} else {
			SaveCIInfo_WithCredLT('false');  //modified oct 25 2013
			MarkCustomerCreationDocument('APPROVE', ccanum);
		}
	});

	$("input[bname=cust_create_disapprove]").click(function (e) {
		MarkCustomerCreationDocument('DISAPPROVE', ccanum);
	});

	$("input[bname=cust_create_returncsr]").click(function (e) {
		MarkCustomerCreationDocument('SEND_BACK_TO_REQUESTER', ccanum);
	});

	$("input[bname=cust_create_returncnc]").click(function (e) {
		MarkCustomerCreationDocument('SEND_BACK_TO_CNC', ccanum);
	});

	// customer changes
	$("input[bname=cust_change_approve]").click(function (e) {
		if ($(this).attr("pos_id") != "" && $(this).attr("pos_id") != undefined) {
			SaveCIInfo('false'); //modified oct 25 2013
			MarkDocChangesStatus('APPROVE', ccanum, $(this).attr("pos_id"));
		} else {
			SaveCIInfo_WithCredLT('false');  //modified oct 25 2013
			MarkDocChangesStatus('APPROVE', ccanum);
		}
	});

	$("input[bname=cust_change_disapprove]").click(function (e) {
		MarkDocChangesStatus('DISAPPROVE', ccanum);
	});

	$("input[bname=cust_change_returncsr]").click(function (e) {
		MarkDocChangesStatus('SEND_BACK_TO_REQUESTER', ccanum);
	});

	$("input[bname=cust_change_returncnc]").click(function (e) {
		MarkDocChangesStatus('SEND_BACK_TO_CNC', ccanum);
	});

}

function GenerateFileDlLink(ccanum, file_name) {
	var encoded_file_name = file_name.replace(/\&/gi, "%26");
	encoded_file_name = encoded_file_name.replace(/\'/gi, "%27");
	encoded_file_name = encoded_file_name.replace(/#/gi, "%23");

	return "<a href=\"" + baseUrl + "SQL/DownloadFile?doctype=CCA&fileName=" + encoded_file_name + "&id=" + ccanum + "\"><img src=\"" + baseUrl + "Images/page_white_get.png\" style=\"border:0;\" /></a>";
}

function LoadAcountStatus() {
	// send through ajx
	var params = {
		CardCode: g_acct_code
	};
	$.ajax({
		type: "POST", url: baseUrl + "Document/GetAccountStatus",
		data: $.param(params, true),
		success: function (res) {
			$("#txt_transaction_type").attr("value", res);
		},
		error: function (xhr, ajaxOptions, thrownError) {
			// alert(xhr.status); alert(thrownError); HidePreloader();
		}
	});
}


function addCommas(str) {
	if (str == "") str = 0;
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
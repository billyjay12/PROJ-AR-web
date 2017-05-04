var ns4;
function AddPartners(col1, col2, col3) {

    var rowid = $("#tbl_partner_list tr").length - 2;
    rowid++;
    $("#tbl_partner_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_partner_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}


function AddCorpo(col1, col2, col3) {

    var rowid = $("#tbl_corpo_list tr").length - 2;
    rowid++;
    $("#tbl_corpo_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_corpo_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddEmployees(col1, col2) {

    var rowid = $("#tbl_emp_pos_list tr").length - 2;
    rowid++;
    $("#tbl_emp_pos_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_emp_pos_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddOutlets(col1, col2, col3, col4) {

    var rowid = $("#tbl_outlet_list tr").length - 2;
    rowid++;
    $("#tbl_outlet_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_outlet_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddMajorCustomers(col1, col2, col3, col4) {

    var rowid = $("#tbl_mjcust_list tr").length - 2;
    rowid++;
    $("#tbl_mjcust_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_mjcust_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddOtherSuppliers(col1, col2, col3, col4, col5, col6) {

    var rowid = $("#tbl_wood_supplier tr").length - 2;
    rowid++;
    $("#tbl_wood_supplier tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col5 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col6 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_wood_supplier'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

/* -  - */

function AddBanks(col1, col2, col3, col4, col5, col6, col7, col8) {

    var rowid = $("#tbl_bank_list tr").length - 2;
    rowid++;
    $("#tbl_bank_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col5 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col6 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col7 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col8 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_bank_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddLands(col1, col2, col3, col4) {
    var rowid = $("#tbl_land_list tr").length - 2;
    rowid++;
    $("#tbl_land_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_land_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddBuilding(col1, col2, col3, col4) {
    var rowid = $("#tbl_building_list tr").length - 2;
    rowid++;
    $("#tbl_building_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_building_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddVehicles(col1, col2, col3) {
    var rowid = $("#tbl_vehicle_list tr").length - 2;
    rowid++;
    $("#tbl_vehicle_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_vehicle_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddOtherBusiness(col1, col2, col3, col4) {
    var rowid = $("#tbl_asset_list tr").length - 2;
    rowid++;
    $("#tbl_asset_list tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + col3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + col4 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_asset_list'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

/* delete current row */
function DelCurrRow(tbl_id, r_id) {
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
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
    var val_three = $("#tbl_partner_list tr:last td:nth-child(3) input[type=text]").attr("value");
    // if (val_three == "") {
    //     alert("FIELD CANNOT BE EMPTY!");
    //     return;
    // }

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_partner_list tr").length - 2;
    rowid++;
    $("#tbl_partner_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_partner_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_corpo_list tr").length - 2;
    rowid++;
    $("#tbl_corpo_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_corpo_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_emp_pos_list tr").length - 2;
    rowid++;
    $("#tbl_emp_pos_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_emp_pos_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_outlet_list tr").length - 2;
    rowid++;
    $("#tbl_outlet_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_outlet_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_mjcust_list tr").length - 2;
    rowid++;
    $("#tbl_mjcust_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_mjcust_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_wood_supplier tr").length - 2;
    rowid++;
    $("#tbl_wood_supplier tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_five + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_six + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_seven + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_wood_supplier'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
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

function AddEntryBank() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_bank_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_bank_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_bank_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_bank_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fifth field */
    var val_five = $("#tbl_bank_list tr:last td:nth-child(5) input[type=text]").attr("value");
    if (val_five == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* sixth field */
    var val_six = $("#tbl_bank_list tr:last td:nth-child(6) input[type=text]").attr("value");
    if (val_six == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* seventh field */
    var val_seven = $("#tbl_bank_list tr:last td:nth-child(7) input[type=text]").attr("value");
    if (val_seven == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var val_eight = $("#tbl_bank_list tr:last td:nth-child(8) input[type=text]").attr("value");
    if (val_eight == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_bank_list tr").length - 2;
    rowid++;
    $("#tbl_bank_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_five + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_six + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_seven + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:98%;\" type=\"text\" value=\"" + val_eight + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_bank_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_bank_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(5) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(6) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(7) input[type=text]").attr("value", "");
    $("#tbl_bank_list tr:last td:nth-child(8) input[type=text]").attr("value", "");
}

function AddEntryLands() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_land_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_land_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_land_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_land_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_land_list tr").length - 2;
    rowid++;
    $("#tbl_land_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_land_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_land_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_land_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_land_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_land_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
}

function AddEntryBuildings() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_building_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_building_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_building_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_building_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_building_list tr").length - 2;
    rowid++;
    $("#tbl_building_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_building_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_building_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_building_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_building_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_building_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
}

function AddEntryVehicles() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_vehicle_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_vehicle_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_vehicle_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_vehicle_list tr").length - 2;
    rowid++;
    $("#tbl_vehicle_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_vehicle_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_vehicle_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_vehicle_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_vehicle_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
}

// other assets
function AddEntryAssets() {
    // check fields if empty
    /* first field */
    var val_one = $("#tbl_asset_list tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_asset_list tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_asset_list tr:last td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* fourth field */
    var val_four = $("#tbl_asset_list tr:last td:nth-child(4) input[type=text]").attr("value");
    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var val_four = $("#tbl_asset_list tr:last td:nth-child(4) input[type=text]").attr("value");
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

    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_asset_list tr").length - 2;
    rowid++;
    $("#tbl_asset_list tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:95%;\" type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_asset_list'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_asset_list tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_asset_list tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_asset_list tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_asset_list tr:last td:nth-child(4) input[type=text]").attr("value", "");
}


/* for browsing data for a certain field */
/* should only display two column */
function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();
    $.ajax({
        type: "POST", url: "../SQL/GetList",
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
                w = w + "" +
                    "<option " +
                        "price_desc=\"" + res_cols[2] + "\" " +
                        "val_area=\"" + res_cols[2] + "\" " +
                        "val_region=\"" + res_cols[3] + "\" " +
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
    $("body").after(w);

    // set position
    // var btnY = getElLeft(document.getElementById(obj_id_to_position));
    // var btnX = getElTop(document.getElementById(obj_id_to_position));
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
    $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));

    if (obj == "txt_acct_territory") {

        $("#txt_area").attr("value", GetValue($("#id_content select option:selected").attr("val_area")));
        $("#txt_area").attr("value_id", GetId($("#id_content select option:selected").attr("val_area")));

        $("#txt_region").attr("value", GetValue($("#id_content select option:selected").attr("val_region")));
        $("#txt_region").attr("value_id", GetId($("#id_content select option:selected").attr("val_region")));

        if ($("#txt_acct_classification").attr("value") == "WALKIN") {
            if ($("#txt_region").attr("value").toUpperCase().search("VISMIN") > -1) {
                $("#txt_tax_id").attr("value", "00 00 00 00 2");
            } else {
                $("#txt_tax_id").attr("value", "00 00 00 00 1");
            }
        }

    }

    if (obj == "txt_mw_price_code") {
        $("#txt_mw_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
    }

    if (obj == "txt_ww_price_code") {
        $("#txt_ww_price_desc").attr("value", GetValue($("#id_content select option:selected").attr("price_desc")));
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

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
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
}


function DisableEditing(val_business_type) {
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

    $("#txt_acct_code").attr('readonly', 'readonly');
    $("#txt_acct_name").attr('readonly', 'readonly');
    $("#txt_phone_no").attr('readonly', 'readonly');
    $("#txt_fax_no").attr('readonly', 'readonly');
    $("#txt_email_add").attr('readonly', 'readonly');
    $("#txt_office_hours").attr('readonly', 'readonly');
    $("#txt_area").attr('readonly', 'readonly');
    $("#txt_store_hours").attr('readonly', 'readonly');
    $("#txt_region").attr('readonly', 'readonly');
    $("#txt_yrs_business").attr('readonly', 'readonly');
    $("#txt_yrs_matimco").attr('readonly', 'readonly');
    $("#txt_tax_id").attr('readonly', 'readonly');
    $("#txt_vat_no").attr('readonly', 'readonly');
    $("#txt_vat_no").attr('onclick', '');
    $("#txt_reg_name").attr('readonly', 'readonly');
    $("#txt_business_add").attr('readonly', 'readonly');
    $("#txt_delivery_add").attr('readonly', 'readonly');

    $("#txt_acct_class").attr('onclick', '');
    $("#txt_acct_territory").attr('onclick', '');
    $("#txt_acct_officer").attr('onclick', '');

    // $("#sub_tab").prev().hide();

    $("#txt_sole_owner_name").attr('readonly', 'readonly');
    $("#txt_sole_nationality").attr('readonly', 'readonly');
    $("#txt_sole_gen_manager").attr('readonly', 'readonly');
    $("#txt_sole_fin_manager").attr('readonly', 'readonly');
    $("#txt_sole_others").attr('readonly', 'readonly');

    $("#txt_partner_gen_manager").attr('readonly', 'readonly');
    $("#txt_partner_fin_manager").attr('readonly', 'readonly');
    $("#txt_partner_others").attr('readonly', 'readonly');

    $("#txt_corpo_inc_date").attr('readonly', 'readonly');
    $("#txt_corpo_auth_cap_stock").attr('readonly', 'readonly');
    $("#txt_corpo_subscb_cap_stock").attr('readonly', 'readonly');
    $("#txt_corpo_paidin_cap_stock").attr('readonly', 'readonly');
    $("#txt_corpo_ceo").attr('readonly', 'readonly');
    $("#txt_corpo_vp_fin").attr('readonly', 'readonly');
    $("#txt_corpo_gen_man").attr('readonly', 'readonly');

    $("#txt_no_employees").attr('readonly', 'readonly');

    $("#txt_articles_of_inc").attr('onclick', '');
    $("#txt_financial_statement").attr('onclick', '');
    $("#txt_ITR").attr('onclick', '');
    $("#txt_bir_reg").attr('onclick', '');
    $("#txt_business_permit").attr('onclick', '');
    $("#txt_articles_of_inc").parent().parent().find("td a:first").hide();
    $("#txt_financial_statement").parent().parent().find("td a:first").hide();
    $("#txt_ITR").parent().parent().find("td a:first").hide();
    $("#txt_bir_reg").parent().parent().find("td a:first").hide();
    $("#txt_business_permit").parent().parent().find("td a:first").hide();

    $("#txt_articles_of_inc").attr('readonly', 'readonly');
    $("#txt_financial_statement").attr('readonly', 'readonly');
    $("#txt_ITR").attr('readonly', 'readonly');
    $("#txt_bir_reg").attr('readonly', 'readonly');
    $("#txt_business_permit").attr('readonly', 'readonly');

    $("#txt_credit_terms").attr('readonly', 'readonly'); $("#txt_credit_terms").attr('onclick', '');
    $("#txt_credit_limit").attr('readonly', 'readonly');
    $("#txt_eco_class_of_customer").attr('readonly', 'readonly');
    $("#txt_no_of_outlets").attr('readonly', 'readonly');

    $("#txt_mw_price_code").attr('readonly', 'readonly');
    $("#txt_mw_price_desc").attr('readonly', 'readonly');
    $("#txt_mw_price_remarks").attr('readonly', 'readonly');

    $("#txt_ww_price_code").attr('readonly', 'readonly');
    $("#txt_ww_price_desc").attr('readonly', 'readonly');
    $("#txt_ww_price_remarks").attr('readonly', 'readonly');

    $("#txt_pwf_price_code").attr('readonly', 'readonly');
    $("#txt_pwf_price_desc").attr('readonly', 'readonly');
    $("#txt_pwf_price_remarks").attr('readonly', 'readonly');

    $("#txt_pwr_price_code").attr('readonly', 'readonly');
    $("#txt_pwr_price_desc").attr('readonly', 'readonly');
    $("#txt_pwr_price_remarks").attr('readonly', 'readonly');

    $("#txt_gw_price_code").attr('readonly', 'readonly');
    $("#txt_gw_price_desc").attr('readonly', 'readonly');
    $("#txt_gw_price_remarks").attr('readonly', 'readonly');

    $("#txt_tw_price_code").attr('readonly', 'readonly');
    $("#txt_tw_price_desc").attr('readonly', 'readonly');
    $("#txt_tw_price_remarks").attr('readonly', 'readonly');

    $("#txt_prod_major_line").attr('readonly', 'readonly');
    $("#txt_prod_other_line").attr('readonly', 'readonly');
    $("#txt_const_mat_plywood").attr('readonly', 'readonly');
    $("#txt_const_mat_steel").attr('readonly', 'readonly');
    $("#txt_const_mat_cement").attr('readonly', 'readonly');
    $("#txt_const_mat_hb").attr('readonly', 'readonly');
    $("#txt_const_mat_others").attr('readonly', 'readonly');
    $("#txt_major_vol_business").attr('readonly', 'readonly');
    $("#txt_wood_vol").attr('readonly', 'readonly');
    $("#txt_discount_enjoyed").attr('readonly', 'readonly');

    $("#txt_mw_price_code").attr('onclick', '');
    $("#txt_ww_price_code").attr('onclick', '');
    $("#txt_pwf_price_code").attr('onclick', '');
    $("#txt_pwr_price_code").attr('onclick', '');
    $("#txt_gw_price_code").attr('onclick', '');
    $("#txt_tw_price_code").attr('onclick', '');
}


// SAVING
function Save_Doc() {

    if (CheckRequiredFields()) {
        Savedata();
    }

    // Savedata();
}

function CheckRequiredFields() {
    var acct_type = $("#acc_type_direct").attr('checked') == 'checked' ? "direct" : "indirect";

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

    // vat reg no
    if ($("#txt_vat_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Vat Type"; } else { lacking_fields = "Vat Type"; }
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


    //BIR Form
    if ($("#txt_bir_reg").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "BIR Registration Form not attached"; } else { lacking_fields = "BIR Registration Form not attached"; }
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

    // vat reg no
    if ($("#txt_vat_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Vat Type"; } else { lacking_fields = "Vat Type"; }
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

    // tax id
    if ($("#txt_tax_id").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Tax ID"; } else { lacking_fields = "Tax ID"; }
    }

    // vat reg no
    if ($("#txt_vat_no").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Vat Type"; } else { lacking_fields = "Vat Type"; }
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



    //BIR Form
    if ($("#txt_bir_reg").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "BIR Registration Form not attached"; } else { lacking_fields = "BIR Registration Form not attached"; }
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

        // [major stock holders, nationality, %owned] - at least two
        // if (($("#tbl_corpo_list tr").length - 2) < 2) {
        //     if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "List of Corp must be at least 2"; } else { lacking_fields = "List of Corp must be at least 2"; }
        // }

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
    if (customerHeaderStatus != '1000') {
        // save currently selected values
        g_acct_name = acct_name;
    }

    // phone no
    var acct_phone_no = "";
    acct_phone_no = $("#txt_phone_no").attr('value');

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
    var list_of_partner = "";
    row_count = $("#tbl_partner_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_partner != "") {
            list_of_partner = list_of_partner + "$";
        }
        // first column
        list_of_partner = list_of_partner + $("#tbl_partner_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_partner = list_of_partner + $("#tbl_partner_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_partner = list_of_partner + $("#tbl_partner_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value');
    }

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
    var list_major_stockholder = "";
    row_count = $("#tbl_corpo_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_major_stockholder != "") {
            list_major_stockholder = list_major_stockholder + "$";
        }
        // first column
        list_major_stockholder = list_major_stockholder + $("#tbl_corpo_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_major_stockholder = list_major_stockholder + $("#tbl_corpo_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_major_stockholder = list_major_stockholder + $("#tbl_corpo_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value');
    }

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
    var list_of_employee_no = "";
    row_count = $("#tbl_emp_pos_list tr").length - 1;
    for (i = 3; i <= row_count; i++) {
        if (list_of_employee_no != "") {
            list_of_employee_no = list_of_employee_no + "$";
        }
        // first column
        list_of_employee_no = list_of_employee_no + $("#tbl_emp_pos_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_employee_no = list_of_employee_no + $("#tbl_emp_pos_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
    }

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

    // remarks
    var acct_tw_price_remarks = "";
    acct_tw_price_remarks = $("#txt_tw_price_remarks").attr('value');
    if (customerHeaderStatus != '1000') {
        // save currently selected values
        g_acct_tw_price_remarks = acct_tw_price_remarks;
    }

    // socio economic class of cust
    var acct_socio_eco_class = "";
    acct_socio_eco_class = $("#txt_eco_class_of_customer").attr('value');

    // no. of outlets
    var acct_num_outlets = "";
    acct_num_outlets = $("#txt_no_of_outlets").attr('value');

    // [name of outlet, location, store size, warehouse size]
    var list_of_outlets = "";
    row_count = $("#tbl_outlet_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_outlets != "") {
            list_of_outlets = list_of_outlets + "$";
        }
        // first column
        list_of_outlets = list_of_outlets + $("#tbl_outlet_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_outlets = list_of_outlets + $("#tbl_outlet_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_outlets = list_of_outlets + $("#tbl_outlet_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_outlets = list_of_outlets + $("#tbl_outlet_list tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
    }

    // [name, address, selling terms, est. monthly purchases]
    var list_of_major_customer = "";
    row_count = $("#tbl_mjcust_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_major_customer != "") {
            list_of_major_customer = list_of_major_customer + "$";
        }
        // first column
        list_of_major_customer = list_of_major_customer + $("#tbl_mjcust_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_major_customer = list_of_major_customer + $("#tbl_mjcust_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_major_customer = list_of_major_customer + $("#tbl_mjcust_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_major_customer = list_of_major_customer + $("#tbl_mjcust_list tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
    }

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
    var list_of_other_wood_suppliers = "";
    row_count = $("#tbl_wood_supplier tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_other_wood_suppliers != "") {
            list_of_other_wood_suppliers = list_of_other_wood_suppliers + "$";
        }
        // first column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
        // fifth column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(5) input").attr('value') + "|";
        // sixth column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(6) input").attr('value') + "|";
        // seventh column
        list_of_other_wood_suppliers = list_of_other_wood_suppliers + $("#tbl_wood_supplier tr:nth-child(" + i + ") td:nth-child(7) input").attr('value') + "|";
    }

    // CREDIT INVESTIGATION
    // [bank, branch, address, acct. no., contact no., contact person, avg. daily balance, remarks]
    var list_of_bank = "";
    row_count = $("#tbl_bank_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_bank != "") {
            list_of_bank = list_of_bank + "$";
        }
        // first column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
        // fifth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(5) input").attr('value') + "|";
        // sixth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(6) input").attr('value') + "|";
        // seventh column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(7) input").attr('value') + "|";
        // eigth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(8) input").attr('value') + "|";
    }

    // [type, area, location, owned by]
    var list_of_land = "";
    row_count = $("#tbl_land_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_land != "") {
            list_of_land = list_of_land + "$";
        }
        // first column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // [type, area, location, owned by]
    var list_of_building = "";
    row_count = $("#tbl_building_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_building != "") {
            list_of_building = list_of_building + "$";
        }
        // first column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // [type, model, quantity]
    var list_of_vehicle = "";
    row_count = $("#tbl_vehicle_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_vehicle != "") {
            list_of_vehicle = list_of_vehicle + "$";
        }
        // first column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // other assets
    var acct_other_assets = "";
    acct_other_assets = $("#txt_other_assets").attr('value');

    /* GET account classification and ccanum */
    var acct_ccanum = "";
    acct_ccanum = $("#txt_acct_ccanum").attr('value');

    var acct_classification = "";
    acct_classification = $("#txt_acct_classification").attr('value');

    var other_changes = "";
    other_changes = GetDocChanges();

    // send through ajx
    $.ajax({
        type: "POST", url: baseUrl + "Customer/SaveCustomer",
        data:
			"acct_classification=" + acct_classification + "&" +
            "acct_type=" + acct_type + "&" +
			"acct_key_account=" + acct_key_account + "&" +
			"acct_code=" + acct_code + "&" +
			"acct_class=" + acct_class + "&" +
			"acct_name=" + g_acct_name + "&" +
            "proposed_new_acct_name=" + acct_name + "&" +
			"acct_phone_no=" + acct_phone_no + "&" +
			"acct_acct_officer=" + g_acct_acct_officer + "&" +
            "proposed_new_acct_officer=" + acct_acct_officer + "&" +
			"acct_fax_no=" + acct_fax_no + "&" +
			"acct_territory=" + g_acct_territory + "&" +
            "proposed_new_acct_territory=" + acct_territory + "&" +
			"acct_email=" + acct_email + "&" +
			"acct_office_hours=" + acct_office_hours + "&" +
			"acct_area=" + g_acct_area + "&" +
            "proposed_acct_new_area=" + acct_area + "&" +
			"acct_store_hours=" + acct_store_hours + "&" +
			"acct_region=" + g_acct_region + "&" +
            "proposed_new_acct_region=" + acct_region + "&" +
			"acct_years_in_business=" + acct_years_in_business + "&" +
			"acct_years_with_matimco=" + acct_years_with_matimco + "&" +
			"acct_tax_id=" + acct_tax_id + "&" +
			"acct_vat_no=" + acct_vat_no + "&" +
			"acct_reg_name=" + g_acct_reg_name + "&" +
            "proposed_new_acct_reg_name=" + acct_reg_name + "&" +
			"acct_business_add=" + g_acct_business_add + "&" +
            "proposed_new_acct_business_add=" + acct_business_add + "&" +
			"acct_delivery_add=" + g_acct_delivery_add + "&" +
            "proposed_new_acct_delivery_add=" + acct_delivery_add + "&" +
			"acct_business_type=" + acct_business_type + "&" +
			"sole_owner_name=" + sole_owner_name + "&" +
			"sole_nationality=" + sole_nationality + "&" +
			"sole_gen_manager=" + sole_gen_manager + "&" +
			"sole_fin_manager=" + sole_fin_manager + "&" +
			"sole_others=" + sole_others + "&" +
			"list_of_partner=" + list_of_partner + "&" +
			"partner_gen_manager=" + partner_gen_manager + "&" +
			"partner_fin_manager=" + partner_fin_manager + "&" +
			"partner_others=" + partner_others + "&" +
			"corp_date_inc=" + corp_date_inc + "&" +
			"corp_auth_cap_stock=" + corp_auth_cap_stock + "&" +
			"corp_subc_cap_stock=" + corp_subc_cap_stock + "&" +
			"corp_paidin_cap_stock=" + corp_paidin_cap_stock + "&" +
			"list_major_stockholder=" + list_major_stockholder + "&" +
			"corp_ceo=" + corp_ceo + "&" +
			"corp_vp_fin=" + corp_vp_fin + "&" +
			"corp_gen_man=" + corp_gen_man + "&" +
			"acct_num_employees=" + acct_num_employees + "&" +
			"list_of_employee_no=" + list_of_employee_no + "&" +
			"acct_article_of_inc=" + acct_article_of_inc + "&" +
            "acct_article_of_inc_forupload=" + acct_article_of_inc_forupload + "&" +
			"acct_financial_statement=" + acct_financial_statement + "&" +
            "acct_financial_statement_forupload=" + acct_financial_statement_forupload + "&" +
			"acct_itr=" + acct_itr + "&" +
            "acct_itr_forupload=" + acct_itr_forupload + "&" +
			"acct_bir_reg=" + acct_bir_reg + "&" +
            "acct_bir_reg_forupload=" + acct_bir_reg_forupload + "&" +
			"acct_business_permit=" + acct_business_permit + "&" +
            "acct_business_permit_forupload=" + acct_business_permit_forupload + "&" +
			"acct_prop_credit_term=" + g_acct_prop_credit_term + "&" +
            "proposed_new_acct_prop_credit_term=" + acct_prop_credit_term + "&" +
			"acct_prop_credit_limit=" + g_acct_prop_credit_limit + "&" +
            "proposed_new_acct_prop_credit_limit=" + acct_prop_credit_limit + "&" +
			"acct_mw_price_code=" + g_acct_mw_price_code + "&" +
            "proposed_new_acct_mw_price_code=" + acct_mw_price_code + "&" +
			"acct_mw_price_desc=" + g_acct_mw_price_desc + "&" +
            "proposed_new_acct_mw_price_desc=" + acct_mw_price_desc + "&" +
			"acct_mw_price_remarks=" + g_acct_mw_price_remarks + "&" +
            "proposed_new_acct_mw_price_remarks=" + acct_mw_price_remarks + "&" +
			"acct_ww_price_code=" + g_acct_ww_price_code + "&" +
            "proposed_new_acct_ww_price_code=" + acct_ww_price_code + "&" +
			"acct_ww_price_desc=" + g_acct_ww_price_desc + "&" +
            "proposed_new_acct_ww_price_desc=" + acct_ww_price_desc + "&" +
			"acct_ww_price_remarks=" + g_acct_ww_price_remarks + "&" +
            "proposed_new_acct_ww_price_remarks=" + acct_ww_price_remarks + "&" +
            "acct_pwf_price_code=" + g_acct_pwf_price_code + "&" +
            "proposed_new_acct_pwf_price_code=" + acct_pwf_price_code + "&" +
            "acct_pwf_price_desc=" + g_acct_pwf_price_desc + "&" +
            "proposed_new_acct_pwf_price_desc=" + acct_pwf_price_desc + "&" +
            "acct_pwf_price_remarks=" + g_acct_pwf_price_remarks + "&" +
            "proposed_new_acct_pwf_price_remarks=" + acct_pwf_price_remarks + "&" +
            "acct_pwr_price_code=" + g_acct_pwr_price_code + "&" +
            "proposed_new_acct_pwr_price_code=" + acct_pwr_price_code + "&" +
            "acct_pwr_price_desc=" + g_acct_pwr_price_desc + "&" +
            "proposed_new_acct_pwr_price_desc=" + acct_pwr_price_desc + "&" +
            "acct_pwr_price_remarks=" + g_acct_pwr_price_remarks + "&" +
            "proposed_new_acct_pwr_price_remarks=" + acct_pwr_price_remarks + "&" +
			"acct_gw_price_code=" + g_acct_gw_price_code + "&" +
            "proposed_new_acct_gw_price_code=" + acct_gw_price_code + "&" +
			"acct_gw_price_desc=" + g_acct_gw_price_desc + "&" +
            "proposed_new_acct_gw_price_desc=" + acct_gw_price_desc + "&" +
			"acct_gw_price_remarks=" + g_acct_gw_price_remarks + "&" +
            "proposed_new_acct_gw_price_remarks=" + acct_gw_price_remarks + "&" +
            "acct_tw_price_code=" + g_acct_tw_price_code + "&" +
            "proposed_new_acct_tw_price_code=" + acct_tw_price_code + "&" +
			"acct_tw_price_desc=" + g_acct_tw_price_desc + "&" +
            "proposed_new_acct_tw_price_desc=" + acct_tw_price_desc + "&" +
			"acct_tw_price_remarks=" + g_acct_tw_price_remarks + "&" +
            "proposed_new_acct_tw_price_remarks=" + acct_tw_price_remarks + "&" +
			"acct_socio_eco_class=" + acct_socio_eco_class + "&" +
			"acct_num_outlets=" + acct_num_outlets + "&" +
			"list_of_outlets=" + list_of_outlets + "&" +
			"list_of_major_customer=" + list_of_major_customer + "&" +
			"acct_major_prod_line=" + acct_major_prod_line + "&" +
			"acct_other_prod_line=" + acct_other_prod_line + "&" +
			"acct_supplier_on_plywood=" + acct_supplier_on_plywood + "&" +
			"acct_supplier_on_steel=" + acct_supplier_on_steel + "&" +
			"acct_supplier_on_cement=" + acct_supplier_on_cement + "&" +
			"acct_supplier_on_con_hollowblock=" + acct_supplier_on_con_hollowblock + "&" +
			"acct_supplier_on_others=" + acct_supplier_on_others + "&" +
			"acct_major_vol_business=" + acct_major_vol_business + "&" +
			"acct_monthly_wood_vol=" + acct_monthly_wood_vol + "&" +
			"acct_discount_enjoyed=" + acct_discount_enjoyed + "&" +
			"list_of_other_wood_suppliers=" + list_of_other_wood_suppliers + "&" +
			"list_of_bank=" + list_of_bank + "&" +
			"list_of_land=" + list_of_land + "&" +
			"list_of_building=" + list_of_building + "&" +
			"list_of_vehicle=" + list_of_vehicle + "&" +
			"acct_other_assets=" + acct_other_assets + "&" +
            "acct_ccanum=" + acct_ccanum + "&" +
            "other_changes=" + other_changes + 
			""
			,
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

function CheckDataChanges(
        val_acct_name,
        val_acct_acct_officer,
        val_acct_territory,
        val_acct_area,
        val_acct_region,
        val_acct_reg_name,
        val_acct_business_add,
        val_acct_delivery_add,
        val_acct_prop_credit_term,
        val_acct_prop_credit_limit,
        val_acct_mw_price_code,
        val_acct_mw_price_desc,
        val_acct_mw_price_remarks,
        val_acct_ww_price_code,
        val_acct_ww_price_desc,
        val_acct_ww_price_remarks,
        val_acct_pwf_price_code,
        val_acct_pwf_price_desc,
        val_acct_pwf_price_remarks,
        val_acct_pwr_price_code,
        val_acct_pwr_price_desc,
        val_acct_pwr_price_remarks,
        val_acct_gw_price_code,
        val_acct_gw_price_desc,
        val_acct_gw_price_remarks
) {

    /* compare g_ and val_ */
    if (
        val_acct_prop_credit_term != g_acct_prop_credit_term ||
        val_acct_prop_credit_limit != g_acct_prop_credit_limit ||
        val_acct_mw_price_code != g_acct_mw_price_code ||
        val_acct_mw_price_desc != g_acct_mw_price_desc ||
        val_acct_mw_price_remarks != g_acct_mw_price_remarks ||
        val_acct_ww_price_code != g_acct_ww_price_code ||
        val_acct_ww_price_desc != g_acct_ww_price_desc ||
        val_acct_ww_price_remarks != g_acct_ww_price_remarks ||
        val_acct_pwf_price_code != g_acct_pwf_price_code ||
        val_acct_pwf_price_desc != g_acct_pwf_price_desc ||
        val_acct_pwf_price_remarks != g_acct_pwf_price_remarks ||
        val_acct_pwr_price_code != g_acct_pwr_price_code ||
        val_acct_pwr_price_desc != g_acct_pwr_price_desc ||
        val_acct_pwr_price_remarks != g_acct_pwr_price_remarks ||
        val_acct_gw_price_code != g_acct_gw_price_code ||
        val_acct_gw_price_desc != g_acct_gw_price_desc ||
        val_acct_gw_price_remarks != g_acct_gw_price_remarks
    ) {
        return "route_1";
    }

    if (
        val_acct_name != g_acct_name ||
        val_acct_acct_officer != g_acct_acct_officer ||
        val_acct_territory != g_acct_territory ||
        val_acct_area != g_acct_area ||
        val_acct_region != g_acct_region ||
        val_acct_reg_name != g_acct_reg_name ||
        val_acct_business_add != g_acct_business_add ||
        val_acct_delivery_add != g_acct_delivery_add
    ) {
        return "route_2";
    }

    return "";

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

    // get ccaNum
    if ($("#txt_acct_ccanum").length > 0) {
        ccanum = $("#txt_acct_ccanum").attr('value');
    }

    // get filename
    filename = $("#" + obj).attr('value');

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
                HidePreloader();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError); HidePreloader();
            }
        });
    }

}

function MarkCustomerCreationDocument(val_action_type, val_ccanum, final_acct_code) {
    /*
    val_mark_type = {'disapprove', 'endorse/verified/approve/passed', 'send back'}
    */

    DisplayPreloader();

    var remarks = "";

    var acct_classification = "";
    acct_classification = $("#txt_acct_classification").attr('value');

    if (final_acct_code == undefined) { final_acct_code = ""; }

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
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function MarkDocChangesStatus(val_action_type, val_ccanum) {
    var remarks = "";
    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "Customer/MarkDocumentChanges",
        data:
			"action_type=" + val_action_type + "&" +
            "acct_ccanum=" + val_ccanum + "&" +
            "remarks=" + remarks +
			""
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
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function SaveCIInfo() {
    DisplayPreloader();

    var acct_code = "";
    acct_code = $("#txt_acct_code").attr('value');

    var list_of_bank = "";
    row_count = $("#tbl_bank_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_bank != "") {
            list_of_bank = list_of_bank + "$";
        }
        // first column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
        // fifth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(5) input").attr('value') + "|";
        // sixth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(6) input").attr('value') + "|";
        // seventh column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(7) input").attr('value') + "|";
        // eigth column
        list_of_bank = list_of_bank + $("#tbl_bank_list tr:nth-child(" + i + ") td:nth-child(8) input").attr('value') + "|";
    }

    // [type, area, location, owned by]
    var list_of_land = "";
    row_count = $("#tbl_land_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_land != "") {
            list_of_land = list_of_land + "$";
        }
        // first column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_land = list_of_land + $("#tbl_land_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // [type, area, location, owned by]
    var list_of_building = "";
    row_count = $("#tbl_building_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_building != "") {
            list_of_building = list_of_building + "$";
        }
        // first column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_building = list_of_building + $("#tbl_building_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // [type, model, quantity]
    var list_of_vehicle = "";
    row_count = $("#tbl_vehicle_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_vehicle != "") {
            list_of_vehicle = list_of_vehicle + "$";
        }
        // first column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_vehicle = list_of_vehicle + $("#tbl_vehicle_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }

    // other assets
    var acct_other_assets = "";
    acct_other_assets = $("#txt_other_assets").attr('value');

    // [registed name, nature, location, ownership]
    var list_of_assets = "";
    row_count = $("#tbl_asset_list tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_assets != "") {
            list_of_assets = list_of_assets + "$";
        }
        // first column
        list_of_assets = list_of_assets + $("#tbl_asset_list tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_assets = list_of_assets + $("#tbl_asset_list tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_assets = list_of_assets + $("#tbl_asset_list tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        // fourth column
        list_of_assets = list_of_assets + $("#tbl_asset_list tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
    }

    /* GET account classification and ccanum */
    var acct_ccanum = "";
    acct_ccanum = $("#txt_acct_ccanum").attr('value');

    // send through ajx
    $.ajax({
        type: "POST", url: baseUrl + "Customer/SaveCreditInvestigationInfo",
        data:
			"acct_code=" + acct_code + "&" +
			"list_of_bank=" + list_of_bank + "&" +
			"list_of_land=" + list_of_land + "&" +
			"list_of_building=" + list_of_building + "&" +
			"list_of_vehicle=" + list_of_vehicle + "&" +
			"acct_other_assets=" + acct_other_assets + "&" +
            "list_of_assets=" + list_of_assets + "&" +
            "acct_ccanum=" + acct_ccanum +
			""
			,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // refresh
                // PENDING
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

    if (val_classification == "REGULAR" && val_acct_type == "direct") {
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
        $("#tbl_partner_list tr td:nth-child(1) input[type=text]").addClass("required_fields");
        $("#tbl_partner_list tr td:nth-child(2) input[type=text]").addClass("required_fields");
        $("#txt_corpo_ceo").addClass("required_fields");
        $("#txt_bir_reg").addClass("required_fields");
        $("#txt_credit_terms").addClass("required_fields");
        $("#txt_credit_limit").addClass("required_fields");
        $("#txt_mw_price_code").addClass("required_fields");
        $("#txt_mw_price_desc").addClass("required_fields");
        $("#txt_ww_price_code").addClass("required_fields");
        $("#txt_ww_price_desc").addClass("required_fields");
        $("#txt_pwf_price_code").addClass("required_fields");
        $("#txt_pwf_price_desc").addClass("required_fields");
        $("#txt_pwr_price_code").addClass("required_fields");
        $("#txt_pwr_price_desc").addClass("required_fields");
        $("#txt_gw_price_code").addClass("required_fields");
        $("#txt_gw_price_desc").addClass("required_fields");
        $("#txt_tw_price_code").addClass("required_fields");
        $("#txt_tw_price_desc").addClass("required_fields");
    } else {
        if (val_classification == "REGULAR") {
            $("#txt_acct_code").addClass("required_fields");
            $("#txt_acct_class").addClass("required_fields");
            $("#txt_acct_name").addClass("required_fields");
            $("#txt_acct_officer").addClass("required_fields");
            $("#txt_phone_no").addClass("required_fields");
            $("#txt_acct_territory").addClass("required_fields");
            $("#txt_tax_id").addClass("required_fields");
            $("#txt_vat_no").addClass("required_fields");
            $("#txt_bir_reg").addClass("required_fields");
        } else {
            $("#txt_acct_code").addClass("required_fields");
            $("#txt_acct_class").addClass("required_fields");
            $("#txt_acct_name").addClass("required_fields");
            $("#txt_acct_officer").addClass("required_fields");
            $("#txt_phone_no").addClass("required_fields");
            $("#txt_acct_territory").addClass("required_fields");
            $("#txt_tax_id").addClass("required_fields");
            $("#txt_vat_no").addClass("required_fields");
            $("#txt_credit_terms").addClass("required_fields");
            $("#txt_credit_limit").addClass("required_fields");
            $("#txt_mw_price_code").addClass("required_fields");
            $("#txt_mw_price_desc").addClass("required_fields");
            $("#txt_ww_price_code").addClass("required_fields");
            $("#txt_ww_price_desc").addClass("required_fields");
            $("#txt_pwf_price_code").addClass("required_fields");
            $("#txt_pwf_price_desc").addClass("required_fields");
            $("#txt_pwr_price_code").addClass("required_fields");
            $("#txt_pwr_price_desc").addClass("required_fields");
            $("#txt_gw_price_code").addClass("required_fields");
            $("#txt_gw_price_desc").addClass("required_fields");
            $("#txt_tw_price_code").addClass("required_fields");
            $("#txt_tw_price_desc").addClass("required_fields");
        }
    }
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
    $("#txt_tax_id").removeClass("required_fields");
    $("#txt_vat_no").removeClass("required_fields");
    $("#txt_bir_reg").removeClass("required_fields");
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
    $("#txt_mw_price_code").removeClass("required_fields");
    $("#txt_mw_price_desc").removeClass("required_fields");
    $("#txt_ww_price_code").removeClass("required_fields");
    $("#txt_ww_price_desc").removeClass("required_fields");
    $("#txt_pwf_price_code").removeClass("required_fields");
    $("#txt_pwf_price_desc").removeClass("required_fields");
    $("#txt_pwr_price_code").removeClass("required_fields");
    $("#txt_pwr_price_desc").removeClass("required_fields");
    $("#txt_gw_price_code").removeClass("required_fields");
    $("#txt_gw_price_desc").removeClass("required_fields");
    $("#txt_tw_price_code").removeClass("required_fields");
    $("#txt_tw_price_desc").removeClass("required_fields");
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

    if (
        $("#txt_acct_classification").attr('orig_value') !=
        $("#txt_acct_classification").attr('value')
    ) {
        str_changes = str_changes +
            "Account Classification%3A " +
            $("#txt_acct_classification").attr('orig_value') +
            " %3D%3E " +
            "%3C%3E" + $("#txt_acct_classification").attr('value') + "%3C%2Fb%3E" +
            "\\n";
    }

    if (
        $("#txt_acct_code").attr('orig_value') !=
        $("#txt_acct_code").attr('value')
    ) {
        str_changes = str_changes +
            "Account Code%3A " +
            $("#txt_acct_code").attr('orig_value') +
            " %3D%3E " +
            "%3C%3E" + $("#txt_acct_code").attr('value') + "%3C%2Fb%3E" +
            "\\n";
    }

    if (
        $("#txt_phone_no").attr('orig_value') !=
        $("#txt_phone_no").attr('value')
    ) {
        str_changes = str_changes +
            "Phone Number%3A " +
            $("#txt_phone_no").attr('orig_value') +
            " %3D%3E " +
            "%3C%3E" + $("#txt_phone_no").attr('value') + "%3C%2Fb%3E" +
            "\\n";
    }

    return str_changes;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }
}

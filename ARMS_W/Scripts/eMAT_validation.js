function Save_eMAT() {
	if (validation()) {
		Savedata();
	}
}

function TestFunction() {
	
}

function validation() {
    
        // buyer
    if ($("#txt_buyer").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // adress
    if ($("#txt_address").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // telephone no.
    if ($("#txt_tel_no").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // contact person
    if ($("#txt_contact_person").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // terms
    if ($("#txt_terms").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // date of delivery 
    if ($("#txt_delivery_date").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // delivery instructions
    if ($("#txt_delivery_instruction").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // referred to(account name)
    if ($("#txt_referred_to").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // submitted to(account clerk)
    if ($("#Submitted_to").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         //contact number
    if ($("#txt_contact_number").attr('value') != "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }


         // delivery confirmation
    if ($("#txt_confirmed_delivery").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

         // sales representative
    if ($("#txt_trade_sales_rep").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }
        // table
        // products
    if ($("#txt_product").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

        // prices
    if ($("#txt_price").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

        // quantity
    if ($("#txt_quantity").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

}
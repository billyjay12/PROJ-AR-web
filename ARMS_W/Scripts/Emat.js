function validation() {

    if ($("#txt_buyer").attr('value') == "") {
        alert("PLEASE FILL THE NAME OF BUYER!");
        return false;
    }

    if ($("#txt_address").attr('value') == "") {
        alert("PLEASE FILL ADDRESS!");
        return false;
    }

    if ($("#txt_tel_no").attr('value') == "") {
        alert("PLEASE FILL UP CONTACT PERSON!");
        return false;
    }
    if ($("#txt_contact_person").attr('value') == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS!");
        return false;
    }

    if ($("#txt_terms").attr('value') == "") {
        alert("PLEASE FILL UP TERMS!");
        return false;
    }

    if ($("#txt_delivery_date").attr('value') == "") {
        alert("PLEASE FILL UP THE DATE OF DELIVERY!");
        return false;
    }
    if ($("# txt_delivery_instruction").attr('value') == "") {
        alert("PLEASE FILL DELIVERY INSTRUCTIONS!");
        return false;
    }
    if ($("#txt_contact_number").attr('value') == "") {
        alert("PLEASE FILL UP CONTACT NUMBER!");
        return false;
    }
    if ($("#txt_confirmed_delivery").attr('value') == "") {
        alert("PLEASE FILL UP CONFIRMED DELIVERY BY!");
        return false;
    }
    if ($("#txt_trade_sales_rep").attr('value') == "") {
        alert("PLEASE FILL UP SALES REPRESENTATIVE!");
        return false;
    }


}
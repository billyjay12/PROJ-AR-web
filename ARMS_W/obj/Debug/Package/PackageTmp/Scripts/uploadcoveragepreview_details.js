var div_collection = null;
var div_sales = null;
var div_customerservice = null;
var div_merchandise = null;
var div_inventory = null;

var tbl_collection = null;
var tbl_sales = null;
var tbl_customerservice = null;
var tbl_merchandise = null;
var tbl_inventory = null;

var btn_save = null;
var btn_cancel = null;

var tbl_object_menu = null;

$(function () {
    div_collection = $("#div_collection");
    div_sales = $("#div_sales");
    div_customerservice = $("#div_customerservice");
    div_merchandise = $("#div_merchandise");
    div_inventory = $("#div_inventory");

    tbl_collection = $("#tbl_collection");
    tbl_sales = $("#tbl_sales");
    tbl_customerservice = $("#tbl_customerservice");
    tbl_merchandise = $("#tbl_merchandise");
    tbl_inventory = $("#tbl_inventory");

    tbl_object_menu = $("#tbl_objective_menu");

    btn_save = $("#btn_save");
    btn_cancel = $("#btn_cancel");

    hide_div_objectives();

    //fetch data from server
    getData();

    btn_cancel.click(function () {
        if (confirm("Do you really want to cancel?")) {
            cancel_upload();
        }
    });

    btn_save.click(function () {
        if (confirm("Do you really want to save?")) {
            save_upload();
        }
    });

    tbl_object_menu.find('a').click(function () {
        hide_div_objectives();
        removeClass();
        $(this).addClass("highlight");

        switch ($(this).text()) {
            case "Collection": div_collection.show(); break;
            case "Merchandise": div_merchandise.show(); break;
            case "Customer Service": div_customerservice.show(); break;
            case "Sales": div_sales.show(); break;
            case "Inventory": div_inventory.show(); break;
            default: return;
        }
    });
});

function save_upload() {

    var coverage_events = new Array();

    var rows_collection = tbl_collection.find("tr:gt(0)");
    $(rows_collection).each(function (i, e) {
        var objective_itm_str = $(e).find(".detail_adder").find("data").html();
        var objective_itm = jQuery.parseJSON(objective_itm_str);

        var account_code = $(e).find("td:nth-child(1)").html();
        var contact_person = $(e).find("td:nth-child(4)").html();
        var contact_person_no = $(e).find("td:nth-child(5)").html();
        var hotel_name = $(e).find("td:nth-child(6)").html();
        var hotel_num = $(e).find("td:nth-child(7)").html();

        var amt_collection = "";
        var brand_collection = "";

        var objective_per_accounts = new Array();

        if (objective_per_accounts == null) {

        } else {
            $(objective_itm).each(function (i, e) {
                objective_per_accounts.push({ objective_code: "C", brand: e[0], planned_amount: undoAddComma(e[1]) });
            });
        }

        coverage_events.push({
            account_code: account_code,
            contact_person: contact_person,
            contact_person_no: contact_person_no,
            hotel_name: hotel_name,
            hotel_num: hotel_num,
            list_objectives: objective_per_accounts
        });

    });


    var rows_sales = tbl_sales.find("tr:gt(0)");
    $(rows_sales).each(function (i, e) {
        var objective_itm_str = $(e).find(".detail_adder").find("data").html();
        var objective_itm = jQuery.parseJSON(objective_itm_str);

        var account_code = $(e).find("td:nth-child(1)").html();
        var contact_person = $(e).find("td:nth-child(4)").html();
        var contact_person_no = $(e).find("td:nth-child(5)").html();
        var hotel_name = $(e).find("td:nth-child(6)").html();
        var hotel_num = $(e).find("td:nth-child(7)").html();

        var amt_collection = "";
        var brand_collection = "";

        var objective_per_accounts = new Array();

        if (objective_per_accounts == null) {

        } else {
            $(objective_itm).each(function (i, e) {
                objective_per_accounts.push({ objective_code: "S", brand: e[0], planned_amount: undoAddComma(e[1]) });
            });
        }

        coverage_events.push({
            account_code: account_code,
            contact_person: contact_person,
            contact_person_no: contact_person_no,
            hotel_name: hotel_name,
            hotel_num: hotel_num,
            list_objectives: objective_per_accounts
        });

    });

    var rows_merchandise = tbl_merchandise.find("tr:gt(0)");
    $(rows_merchandise).each(function (i, e) {
        var objective_itm_str = $(e).find(".detail_adder").find("data").html();
        var objective_itm = jQuery.parseJSON(objective_itm_str);

        var account_code = $(e).find("td:nth-child(1)").html();
        var contact_person = $(e).find("td:nth-child(4)").html();
        var contact_person_no = $(e).find("td:nth-child(5)").html();
        var hotel_name = $(e).find("td:nth-child(6)").html();
        var hotel_num = $(e).find("td:nth-child(7)").html();
        var store_checking = $(e).find("td:nth-child(8)").html();

        var amt_collection = "";
        var brand_collection = "";

        var objective_per_accounts = new Array();

        if (objective_per_accounts == null) {

        } else {
            $(objective_itm).each(function (i, e) {
                objective_per_accounts.push({ objective_code: "M", product_presented: e[0], counter_clerk: e[1], counter_clerk_no:e[2] });
            });
        }

        coverage_events.push({
            account_code: account_code,
            contact_person: contact_person,
            contact_person_no: contact_person_no,
            hotel_name: hotel_name,
            hotel_num: hotel_num,
            store_checking: store_checking,
            list_objectives: objective_per_accounts
        });

    });

    var rows_customerservice = tbl_customerservice.find("tr:gt(0)");
    $(rows_customerservice).each(function (i, e) {
        var account_code = $(e).find("td:nth-child(1)").html();
        var contact_person = $(e).find("td:nth-child(4)").html();
        var contact_person_no = $(e).find("td:nth-child(5)").html();
        var hotel_name = $(e).find("td:nth-child(6)").html();
        var hotel_num = $(e).find("td:nth-child(7)").html();
        var issues_and_concerns = $(e).find("td:nth-child(8)").html();

        var amt_collection = "";
        var brand_collection = "";

        var objective_per_accounts = new Array();

        objective_per_accounts.push({ objective_code: "CS" });

        coverage_events.push({
            account_code: account_code,
            contact_person: contact_person,
            contact_person_no: contact_person_no,
            hotel_name: hotel_name,
            hotel_num: hotel_num,
            issues_and_concerns: issues_and_concerns,
            list_objectives: objective_per_accounts
        });

    });

    var new_obj = {
        event_day: event_day,
        event_month: event_month,
        event_year: event_year,
        so_id: soId,
        counter_id : counter_id,
        list_accounts: coverage_events
    }



    $.ajax({
        dataType: "json", contentType: "application/json; charset=utf-8", data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/SaveUploadPreview",
        // dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        //  type: "POST", url: baseUrl + "Calendar/SaveUploadPreview",
        success: function (res) {
            alert("Successfully saved!");
            window.location = baseUrl + "Calendar/Memo?date=" + Eventdate + "&day=" + event_day + "&month=" + event_month + "&year=" + event_year + "&soId=" + soId;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function get_data_from_table() {

}

function cancel_upload() {
    var new_obj = { counter_id: counter_id };

    $.ajax({
        dataType: 'JSON', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "Calendar/DeleteUploadPreview",
        success: function (res) {
            window.location = baseUrl + "Calendar/Memo?date=" + Eventdate + "&day=" + event_day + "&month=" + event_month + "&year=" + event_year + "&soId=" + soId;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function hide_div_objectives() {
    $("#div_collection").hide();
    $("#div_sales").hide();
    $("#div_customerservice").hide();
    $("#div_merchandise").hide();
    $("#div_inventory").hide();
}

function removeClass() {
    $(".lnk_objcode a").removeClass("highlight");
}

function getData() {
    var new_obj = {
        counterid: counter_id
    }

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/getTemporaryUploadData",
        success: function (res) {

            DisplayData(res.data.collection, "tbl_collection");
            DisplayData(res.data.sales, "tbl_sales");
            DisplayData(res.data.merchandise, "tbl_merchandise");
            DisplayData(res.data.customerservice, "tbl_customerservice");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


function DisplayData(data, tbl_nme) {

    var icounter = 2;

    $(data).each(
        function (i, e) {
            var w = new Array();

            // var itms = JSON.stringify(e);
            var itms = new Array();

            if (tbl_nme != "tbl_customerservice")
                for (var k = 0; k < e.Sub_coverage.length; k++) {
                     itms.push([e.Sub_coverage[k].Brand, e.Sub_coverage[k].PlannedAmount, e.Sub_coverage[k].ProductPresented, e.Sub_coverage[k].CounterClerk, e.Sub_coverage[k].CounterClerkNo]);
                }
            else {
                itms.push([e.IssuesAndConcerns]);
            }

            var str_itms = JSON.stringify(itms);

            // if (e.isSelected == "Y") {
            w.push("<tr>");

            w.push("<td>"); w.push(e.AccountCode); w.push("</td>");
            w.push("<td>"); w.push(e.AccountName); w.push("</td>");
            w.push("<td>"); w.push(e.AccountAddress); w.push("</td>");
            w.push("<td>"); w.push(e.ContactPerson); w.push("</td>");
            w.push("<td>"); w.push(e.ContactPersonNo); w.push("</td>");
            w.push("<td>"); w.push(e.HotelName); w.push("</td>");
            w.push("<td>"); w.push(e.HotelNum); w.push("</td>");
            if (tbl_nme == "tbl_merchandise") {
                w.push("<td>"); w.push(e.StoreChecking); w.push("</td>");
            }
            if (tbl_nme == "tbl_customerservice") {
                w.push("<td>"); w.push(e.IssuesAndConcerns); w.push("</td>");
            }

            w.push("<td>");

            if (tbl_nme == "tbl_collection" || tbl_nme == "tbl_sales") {
                w.push("<a style=\"text-decoration:none;\" href=\"javascript:ShowItemsDetails(" + icounter + "," + tbl_nme + ");\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/application_view_list.png\" /> View</a>");
            }
            else if(tbl_nme == "tbl_merchandise"){
                w.push("<a style=\"text-decoration:none;\" href=\"javascript:ShowItemsDetails_Merchandise(" + icounter + "," + tbl_nme + ");\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/application_view_list.png\" /> View</a>");
            }

            w.push("<div class=\"detail_adder\" style=\"padding:5px; background:#ededed;position:absolute;display:none;\">");
            w.push("<data>" + str_itms + "</data>");
            w.push("</div>");
            w.push("</td>");
            w.push("</tr>");
            $("#" + tbl_nme).append(w.join(""));

            icounter = icounter + 1;
        }
    );

}

function ShowItemsDetails(row_no,tbl_name) {

    // get the items
    var string_obj = tbl_name.find("tr:nth-child(" + row_no + ")").find(".detail_adder").find("data").html();

    var item = jQuery.parseJSON(string_obj);

    // get the name
    var a_name = tbl_name.find("tr:nth-child(" + row_no + ")").find("td:nth-child(2)").html();

    $("body").append(build_table(row_no,a_name,tbl_name).join(""));

    var item_details_bkg = null;
    item_details_bkg = $("#item_details_bkg");

    var item_details_content = null;
    item_details_content = $("#item_details_content");

    var tbl_content_details = item_details_content.find(".tbl_details");
    var add_button = null;
    var add_button = item_details_content.find("a[command=add]");
    var close_btn = item_details_content.find("a[command=close]");

    add_button.click(
        function () {
            var object_curr_td = $(this).parent();

            var new_value1 = object_curr_td.parent().find("td:nth-child(1)").find("input[type=text]").attr("value");
            var new_value2 = object_curr_td.parent().find("td:nth-child(2)").find("input[type=text]").attr("value");

            if (new_value1 != "" && new_value2 != "") {
                object_curr_td.parent().prev().after(
                    "<tr>" +
                    "<td><input type=\"text\" value=\"" + new_value1 + "\"/></td>" +
                    "<td><input type=\"text\" value=\"" + new_value2 + "\"/></td>" +
                    "<td><a command=\"delete\" href=\"javascript:;\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/delete.png\" /></a></td></tr>"
                );

                object_curr_td.parent().find("td:nth-child(1)").find("input[type=text]").attr("value", "");
                object_curr_td.parent().find("td:nth-child(2)").find("input[type=text]").attr("value", "");
            } else {
                alert("Fields cannot be empty.");
            }
        }
    );

    item_details_content.find(".tbl_details").find("a[command=delete]").live("click",
        function () {
             $(this).parent().parent().remove();
        }
    );

    close_btn.click(

        function () {
            var s = "";

            var current_row_no = item_details_content.find("input[type=hidden]").attr("value");

            if (current_row_no != "") {

                var items_in_tmp_item_list = new Array();

                var row_count = tbl_content_details.find("tr").length;
                tbl_content_details.find("tr").each(
                    function (i, e) {
                        var value1 = $(e).find("td:nth-child(1)").find("input[type=text]").attr("value");
                        var value2 = $(e).find("td:nth-child(2)").find("input[type=text]").attr("value");
                        if (i > 2 && i != (row_count - 1)) {
                            items_in_tmp_item_list.push([value1, value2]);
                        }
                    }
                );
            }

            tbl_name
                .find("tr:nth-child(" + current_row_no + ")")
                .find(".detail_adder")
                .find("data").html(
                    JSON.stringify(items_in_tmp_item_list)
                );

            // clear
            item_details_content.find("input[type=hidden]").attr("value", "");

            item_details_content.hide("fast", function () {
                item_details_bkg.hide();

                item_details_content.remove();
                item_details_bkg.remove();
            });
        }

    );

        $(item).each(
        function (i, e) {
            var row2nd_to_last = tbl_content_details.find("tr:last-child").prev();

            if (tbl_name.selector != "#tbl_merchandise") {
                row2nd_to_last
                .after(
                    "<tr>" +
                    "<td align=\"left\"><input type=\"text\" value=\"" + e[0] + "\" /></td>" +
                    "<td><input type=\"text\" value=\"" + e[1] + "\" /></td>" +
                    "<td><a command=\"delete\" href=\"javascript:;\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/delete.png\" /></a></td>" +
                    "</tr>"
                );
            }
            else {
                row2nd_to_last
                .after(
                    "<tr>" +
                    "<td align=\"left\"><input type=\"text\" value=\"" + e[2] + "\" /></td>" +
                    "<td><input type=\"text\" value=\"" + e[3] + "\" /></td>" +
                    "<td><input type=\"text\" value=\"" + e[4] + "\" /></td>" +
                    "<td><a command=\"delete\" href=\"javascript:;\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/delete.png\" /></a></td>" +
                    "</tr>"
                );
            }
        }
    );


    
    var first_text_box_lp = tbl_content_details.find("tr:last-child").find("td:nth-child(1)").find("input[type=text]");
    var second_text_box_lp = tbl_content_details.find("tr:last-child").find("td:nth-child(2)").find("input[type=text]");

    first_text_box_lp.lookdown(
    { "url": baseUrl + "Calendar/GetBrand", "index_value": "3", "display_rowindex": "3" },
    { "": "M" },
    function (res) { return res; },
    function (res, all) {

        //txt_collectBrand.attr("value", res);
        var _brand = all[1] == "null" ? "" : all[1];
        first_text_box_lp.attr("value", _brand);

    });


    second_text_box_lp.decifield();


    item_details_bkg.show();
    item_details_content.show("fast");
}

// extra
$("body").keyup(function (e) {
    if (e.keyCode == 27) {
        $("#item_details_content").hide("fast", function () {
            $("#item_details_bkg").hide();

            $("#item_details_content").remove();
            $("#item_details_bkg").remove();
        });
    }
});


function ShowItemsDetails_Merchandise(row_no, tbl_name) {

    // get the items
    var string_obj = tbl_name.find("tr:nth-child(" + row_no + ")").find(".detail_adder").find("data").html();

    var item = jQuery.parseJSON(string_obj);

    // get the name
    var a_name = tbl_name.find("tr:nth-child(" + row_no + ")").find("td:nth-child(2)").html();

    $("body").append(build_table(row_no, a_name, tbl_name).join(""));

    var item_details_bkg = null;
    item_details_bkg = $("#item_details_bkg");

    var item_details_content = null;
    item_details_content = $("#item_details_content");

    var tbl_content_details = item_details_content.find(".tbl_details");
    var add_button = null;
    var add_button = item_details_content.find("a[command=add]");
    var close_btn = item_details_content.find("a[command=close]");

    add_button.click(
        function () {
            var object_curr_td = $(this).parent();

            var new_value1 = object_curr_td.parent().find("td:nth-child(1)").find("input[type=text]").attr("value");
            var new_value2 = object_curr_td.parent().find("td:nth-child(2)").find("input[type=text]").attr("value");
            var new_value3 = object_curr_td.parent().find("td:nth-child(3)").find("input[type=text]").attr("value");

            if (new_value1 != "" && new_value2 != "") {
                object_curr_td.parent().prev().after(
                    "<tr>" +
                    "<td><input type=\"text\" value=\"" + new_value1 + "\"/></td>" +
                    "<td><input type=\"text\" value=\"" + new_value2 + "\"/></td>" +
                    "<td><input type=\"text\" value=\"" + new_value3 + "\"/></td>" +
                    "<td><a command=\"delete\" href=\"javascript:;\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/delete.png\" /></a></td></tr>"
                );

                object_curr_td.parent().find("td:nth-child(1)").find("input[type=text]").attr("value", "");
                object_curr_td.parent().find("td:nth-child(2)").find("input[type=text]").attr("value", "");
                object_curr_td.parent().find("td:nth-child(3)").find("input[type=text]").attr("value", "");
            } else {
                alert("Fields cannot be empty.");
            }
        }
    );

    item_details_content.find(".tbl_details").find("a[command=delete]").live("click",
        function () {
            $(this).parent().parent().remove();
        }
    );

        close_btn.click(

        function () {
            var s = "";

            var current_row_no = item_details_content.find("input[type=hidden]").attr("value");

            if (current_row_no != "") {

                var items_in_tmp_item_list = new Array();

                var row_count = tbl_content_details.find("tr").length;
                tbl_content_details.find("tr").each(
                    function (i, e) {
                        var value1 = $(e).find("td:nth-child(1)").find("input[type=text]").attr("value");
                        var value2 = $(e).find("td:nth-child(2)").find("input[type=text]").attr("value");
                        var value3 = $(e).find("td:nth-child(3)").find("input[type=text]").attr("value");
                        if (i > 2 && i != (row_count - 1)) {
                            items_in_tmp_item_list.push(["NULL","NULL",value1, value2, value3]);
                        }
                    }
                );
            }

            tbl_name
                .find("tr:nth-child(" + current_row_no + ")")
                .find(".detail_adder")
                .find("data").html(
                    JSON.stringify(items_in_tmp_item_list)
                );

            // clear
            item_details_content.find("input[type=hidden]").attr("value", "");

            item_details_content.hide("fast", function () {
                item_details_bkg.hide();

                item_details_content.remove();
                item_details_bkg.remove();
            });
        }

    );

    $(item).each(
        function (i, e) {
            var row2nd_to_last = tbl_content_details.find("tr:last-child").prev();

            row2nd_to_last
            .after(
                "<tr>" +
                "<td align=\"left\"><input type=\"text\" value=\"" + e[2] + "\" /></td>" +
                "<td><input type=\"text\" value=\"" + e[3] + "\" /></td>" +
                "<td><input type=\"text\" value=\"" + e[4] + "\" /></td>" +
                "<td><a command=\"delete\" href=\"javascript:;\"><img style=\"border:0;\" src=\"" + baseUrl + "Images/delete.png\" /></a></td>" +
                "</tr>"
            );
        }
    );

    item_details_bkg.show();
    item_details_content.show("fast");
}

// extra
$("body").keyup(function (e) {
    if (e.keyCode == 27) {
        $("#item_details_content").hide("fast", function () {
            $("#item_details_bkg").hide();

            $("#item_details_content").remove();
            $("#item_details_bkg").remove();
        });
    }
});


function build_table(row_no,a_name,tbl_nme) {
    var w = new Array();

    w.push("<div id=\"item_details_bkg\" style=\"display:none; opacity: 0.60; filter:alpha(opacity=60); position:fixed; top:0; left:0; height:100%; width:100%; background:#323232;\"></div>");
    w.push("<div id=\"item_details_content\" style=\"display:none; position:fixed; top:0; left:0; height:100%; width:100%; \">");

    w.push("<input type=\"hidden\" class=\"row_no\" value=\"" + row_no + "\" />");

    w.push("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"height:100%; width:100%; \">");
    w.push("<tr>");
    w.push("<td valign=\"middle\" align=\"center\">");

    w.push("<table class=\"tbl_details\" cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"padding:5px; font-family:arial; font-size:12px; background:#ffffff;\" >");
    w.push("<tr><td style=\"background:#ededed;\" colspan=\"4\" align=\"right\"><a command=\"close\" href=\"javascript:;\">Close</a></td></tr>");

    w.push("<tr>");
    w.push("<td align=\"left\" colspan=\"3\" ><b>Account Name: </b><span class=\"acct_name\">" + a_name + "</span> </td>");
    w.push("</tr>");

    if (tbl_nme.selector == "#tbl_sales" || tbl_nme.selector == "#tbl_collection") {
        w.push("<tr>");
        w.push("<td align=\"left\" ><b>Brand</b></td><td align=\"left\" ><b>Amount</b></td><td></td>");
        w.push("</tr>");

        w.push("<tr>");

        w.push("<td><input type=\"text\" readonly=\"readonly\" /></td>");
        w.push("<td><input type=\"text\" /></td>");
        w.push("<td><a command=\"add\" href=\"javascript:;\">add</a></td>");
    }
    else if(tbl_nme.selector =="#tbl_merchandise")
    {
        w.push("<tr>");
        w.push("<td align=\"left\" ><b>Product Presented</b></td><td align=\"left\" ><b>Counter Clerk</b></td><td><b>Mobile No.</b></td><td></td>");
        w.push("</tr>");

        w.push("<tr>");

        w.push("<td><input type=\"text\" /></td>");
        w.push("<td><input type=\"text\" /></td>");
        w.push("<td><input type=\"text\" /></td>");
        w.push("<td><a command=\"add\" href=\"javascript:;\">add</a></td>");
    }

   

    w.push("</tr>");
    w.push("</table>");

    w.push("</td>");
    w.push("</tr>");
    w.push("</table>");
    w.push("</div>");

    return w;
}


function addCommas(str) {
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
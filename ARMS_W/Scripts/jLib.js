
/*
    making ajax request
*/
function AjxRqstJquery(url, params, call_back_on_success, call_back_on_success_with_error) {
    $.ajax({
        type: "POST", url: url, data: $.param(params, true),
        success: function (res) {
            if (!res.iserror) {
                call_back_on_success(res);
                
            } else {
                call_back_on_success_with_error(res);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function AjxRqstJson2(url, params, call_back_on_success, call_back_on_success_with_error) {
    $.ajax({
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        type: "POST", url: url, data: JSON.stringify(params),
        success: function (res) {
            if (!res.iserror) {
                call_back_on_success(res);
                
            } else {
                call_back_on_success_with_error(res);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

/*
* jQuery UI LookDown 1.8.16
*
* Copyright 2011, AUTHORS.txt (http://jqueryui.com/about)
* Dual licensed under the MIT or GPL Version 2 licenses.
* http://jquery.org/license
*
* http://docs.jquery.com/UI/LookDown
*
* Depends:
*	jquery.ui.position.js
*
*
*   USAGE:
*       $(element).lookdown(
*           settings,
*           parameter_for_url,
*           function_for_handling_incoming_data (must use return to return the data to be processed),
*           function_for_handling_onselect_event
*       );
*/
(function ($, undefined) {
    $.fn.lookdown = function (options, extra_param, call_back_onsuccess, call_back_onselect) {
        var ObjectId = $(this).attr("id");
        var Elem = $(this);
        var nextElemObj = "";
        var PROP_NAME = "_JSONLookDownDataN";
        var nextElem = "div[" + PROP_NAME + " = " + PROP_NAME + "]";

        var settings = {
            "url": "",
            "index_value": "",
            "display_rowindex": ""
        };

        if (options) { $.extend(settings, options); }

       Elem.click(function () {
            if (Elem.next(nextElem).length == 0) {
                get_data_for_lookup(settings["url"], AfterRequest);
            }
        });

        $(document).mousedown(function (event) {
            var targt = $(event.target);
            var str_test = "";

            var lookup_div_table = targt.parents(nextElem).length;
            var lookup_div = $(targt).attr(PROP_NAME);

            if (lookup_div != PROP_NAME && lookup_div_table == 0 && ObjectId != $(targt).attr("id")) {
                Elem.next(nextElem).hide("fast", function () {
                    $(this).unbind("click");
                    $(this).remove();
                });
            }

        });

        // set default values
        if (settings["index_value"] == "") settings["index_value"] = "1";
        if (settings["display_rowindex"] == "") settings["display_rowindex"] = "1";

        function AfterRequest(res) {
            var list_of_data;

            if (call_back_onsuccess != null) {
                list_of_data = call_back_onsuccess(res);
            }
            if (list_of_data.length > 0 && jQuery.isArray(list_of_data)) {

                var style = "display:none;background:#f7f7f7; position:absolute; z-index:9999; padding:2px 2px 2px 5px; border:1px solid #cdcdcd;";

                if (list_of_data.length > 10) style = style + "overflow:scroll; height:160px; overflow-x:hidden";

                // create a box
                var w = "";
                var w_buffer = new Array();

                w_buffer.push("<div class='lud_box' " + PROP_NAME + "='" + PROP_NAME + "' style='" + style + "' >");

                // rows
                w_buffer.push("<table cellspacing='0' cellpadding='1' border='0' style='color:#4d4d4d;' >");
                

//                w_buffer.push("<tr align=\"left\">");
//                w_buffer.push("<th><input type=\"text\" style=\"width:95%\" /></th>");
//                w_buffer.push("</tr>");


                for (var i = 0; i < list_of_data.length; i++) {
                    // colums
                    w_buffer.push("<tr align=\"left\">");
                    if(!jQuery.isArray(list_of_data[i])) {
                        // directly get the value 
                        w_buffer.push("<td style=\"cursor:pointer;\" mrk='lud_table_tr_td' >" +list_of_data[i] + "</td>");
                    } else {
                        var res_col = list_of_data[i];
                        var x = 0;
                        for (var key in res_col) {
                            var s_style = "";
                            if (parseInt(settings["display_rowindex"]) - 1 == x) s_style = "cursor:pointer; display:block;";
                            else s_style = "cursor:pointer; display:none;";
                            w_buffer.push("<td style='" + s_style + "' mrk='lud_table_tr_td' >" + res_col[key] + "</td>");
                            x++;
                        }
                    }

                    w_buffer.push("</tr>");
                }
                w_buffer.push("<table>");
                w_buffer.push("</div>");

                // append after the element
                w = w_buffer.join("");
                Elem.after(w);
                
                /*
                Elem.next(nextElem).position({
                    of: Elem,
                    my: "left top",
                    at: "left bottom",
                    of: Elem
                });
                */
               // alert($("div[class='lud_box']").html());
                var e_pos =  Elem.position();
                var e_pos_offsetheight = Elem.height();
                var e_margin_top = 3;
                var e_margin_left = 1;

                Elem.next(nextElem)
                    .css("top", e_pos.top + e_pos_offsetheight + e_margin_top)
                    .css("left", e_pos.left + e_margin_left);

                Elem.next(nextElem).show("fast");

                nextElemObj = $(Elem).next(nextElem);
                // bind the lud_box
                Elem.next(nextElem).find("table td").bind("click", function () {
                    var value = {
                        value: $(this).parent().find("td:nth-child(" + parseInt(settings["display_rowindex"]) + ")").html(),
                        id: $(this).parent().find("td:nth-child(" + parseInt(settings["index_value"]) + ")").html()
                    };
                    var value = new Array();
                    $(this).parent().find("td").each(
                        function (index, element) {
                            value.push($(element).html());
                        }
                    );

                    call_back_onselect(value[parseInt(settings["index_value"]) - 1], value);

                    // hide and remove lookup
                    Elem.next(nextElem).hide("fast", function () {
                        $(this).unbind("click");
                        $(this).remove();
                    });
                });

                
                Elem.next(nextElem).find("table th").bind("keyup", function () {
                    searchKeyUp();
                        }
                    );


            }
        }

        function searchKeyUp() {

            if (call_back_onsuccess != null) {
                list_of_data = call_back_onsuccess(res);
            }

            if (list_of_data.length > 0 && jQuery.isArray(list_of_data)) {

                var style = "display:none;background:#f7f7f7; position:absolute; z-index:9999; padding:2px 2px 2px 5px; border:1px solid #cdcdcd;";

                if (list_of_data.length > 10) style = style + "overflow:scroll; height:160px; overflow-x:hidden";

                // create a box
                var w = "";
                var w_buffer = new Array();

                w_buffer.push("<div class='lud_box' " + PROP_NAME + "='" + PROP_NAME + "' style='" + style + "' >");

                // rows
                w_buffer.push("<table cellspacing='0' cellpadding='1' border='0' style='color:#4d4d4d;' >");
                

//                w_buffer.push("<tr align=\"left\">");
//                w_buffer.push("<th><input type=\"text\" style=\"width:95%\" /></th>");
//                w_buffer.push("</tr>");


                for (var i = 0; i < list_of_data.length; i++) {
                    // colums
                    w_buffer.push("<tr align=\"left\">");
                    if(!jQuery.isArray(list_of_data[i])) {
                        // directly get the value 
                        w_buffer.push("<td style=\"cursor:pointer;\" mrk='lud_table_tr_td' >" +list_of_data[i] + "</td>");
                    } else {
                        var res_col = list_of_data[i];
                        var x = 0;
                        for (var key in res_col) {
                            var s_style = "";
                            if (parseInt(settings["display_rowindex"]) - 1 == x) s_style = "cursor:pointer; display:block;";
                            else s_style = "cursor:pointer; display:none;";
                            w_buffer.push("<td style='" + s_style + "' mrk='lud_table_tr_td' >" + res_col[key] + "</td>");
                            x++;
                        }
                    }

                    w_buffer.push("</tr>");
                }
                w_buffer.push("<table>");
                w_buffer.push("</div>");

                // append after the element
                w = w_buffer.join("");
                Elem.after(w);
                
                /*
                Elem.next(nextElem).position({
                    of: Elem,
                    my: "left top",
                    at: "left bottom",
                    of: Elem
                });
                */
                
                var e_pos =  Elem.position();
                var e_pos_offsetheight = Elem.height();
                var e_margin_top = 3;
                var e_margin_left = 1;

                Elem.next(nextElem)
                    .css("top", e_pos.top + e_pos_offsetheight + e_margin_top)
                    .css("left", e_pos.left + e_margin_left);

                Elem.next(nextElem).show("fast");

                nextElemObj = $(Elem).next(nextElem);
                // bind the lud_box
                Elem.next(nextElem).find("table td").bind("click", function () {
                    var value = {
                        value: $(this).parent().find("td:nth-child(" + parseInt(settings["display_rowindex"]) + ")").html(),
                        id: $(this).parent().find("td:nth-child(" + parseInt(settings["index_value"]) + ")").html()
                    };
                    var value = new Array();
                    $(this).parent().find("td").each(
                        function (index, element) {
                            value.push($(element).html());
                        }
                    );

                    call_back_onselect(value[parseInt(settings["index_value"]) - 1], value);

                    // hide and remove lookup
                    Elem.next(nextElem).hide("fast", function () {
                        $(this).unbind("click");
                        $(this).remove();
                    });
                });

//                
//                Elem.next(nextElem).find("table th").bind("keyup", function () {
//                    alert("A");
//                        }
//                    );


            }
        }


     

        function get_data_for_lookup(url, call_back) {
            
            $.ajax({ type: "POST", url: url, data: $.param(extra_param, true),
                success: function (res) { call_back(res); },
                error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
            });

        }
    };

})(jQuery);

/*  */
(function ($, undefined) {
    $.fn.datatable = function (call_back_after_clicking_add_button, data, after_add_cb, clicking_delete) {
        var Elem = $(this);
        var settings = { "url": "", "index_value": "", "display_rowindex": "" };
        var is_valid_format = false;
        var column_count = 0;
        var is_data_an_array = false;

        var data_text_box_sizes = new Array();



        // if (options) { $.extend(settings, options); }
        column_count = Elem.find("tr:first-child").find("td").length - 1;

        for(x = 0; x < column_count; x++){
            data_text_box_sizes.push(
				Elem.find("tr:last-child")
					.find("td:nth-child(" + (x + 1) + ")")
					.find("input[type=text]").css("width")
			);
        }
        

        // bind the delete
        Elem.find("a[command=delete_button]").live( "click", 
            function() { 
                $(this).parent().parent().remove(); 
                if( clicking_delete != undefined && clicking_delete != null){
                    clicking_delete();
                }
            }
        );

        // mark finished adding controls
        Elem.attr("iscomplete", "true");

        // check if data is array
        if( jQuery.isArray(data) ){
           // then check if each value in array is also an array
            $(data).each(
                function (index, elem){
                    if( jQuery.isArray(elem) ){
                        is_data_an_array = true;
                        is_valid_format = true;
                    } else {
                        // check if object
                        if( jQuery.isPlainObject(elem) ){
                            var data_column_count = 0;
                            $.each(elem, function() { data_column_count++; });

                            if( column_count != data_column_count) is_valid_format = false
                            else is_valid_format = true;

                        } else {
                            is_valid_format = false;
                        }
                    }
                }
            );
        }

        if( is_valid_format == true ){
            // insert the data
            if( is_data_an_array == true ){
                
                $(data).each( function(index, elem) {
                    var new_tbl_row = "<tr>";
                    $(elem).each(
                        function (index1, elem1){
                            new_tbl_row = new_tbl_row + "<td><input type=\"text\"";
                            new_tbl_row = new_tbl_row + "value=\"" + elem1 + "\"";
                            new_tbl_row = new_tbl_row + " readonly=\"readonly\" style=\"width:" + data_text_box_sizes[index1] + ";\" /></td>";
                        }
                    );

                    // for the delete button
                    new_tbl_row = new_tbl_row + "<td><a command=\"delete_button\" href=\"javascript:;\"> <img style=\"cursor:pointer; border:0px;\" src=\"" + baseurl + "Images/delete.png\"> </a></td></tr>";

                    Elem.find("tr:last-child").prev().after(new_tbl_row);
                });

            } else {
                // if object
                // the top layer should be an array
                if( data != null )
                $(data).each( function (index, elem){
                    var new_tbl_row = "";
                    new_tbl_row = new_tbl_row + "<tr clone='true'>";
                       
                    for(var itm in elem){
                        new_tbl_row = new_tbl_row + "<td><input type=\"text\"";
                        new_tbl_row = new_tbl_row + "value=\"" + elem[itm] + "\"";
                        new_tbl_row = new_tbl_row + " readonly=\"readonly\" /></td>";
                    }

                    // for the delete button
                    new_tbl_row = new_tbl_row + "<td><a command=\"delete_button\" href=\"javascript:;\"> <img style=\"cursor:pointer; border:0px;\" src=\"" + baseurl + "Images/delete.png\"> </a></td>";
                    new_tbl_row = new_tbl_row + "</tr>";

                    Elem.find("tr:last-child").prev().after(new_tbl_row);
                });
            }
        }

        //if( Elem.attr("iscomplete") != "true")
        Elem.find("tr:last-child").find("td:last-child").unbind('click');

        //if( Elem.attr("iscomplete") != "true")
        Elem.find("tr:last-child").find("td:last-child").find("a:first-child").click(function () {
            
		    // add a row after the header
		    var row_count = $(Elem).find("tr").length;
			
            // count the columns
		    var col_count = $(Elem).find("tr:nth-child(" + row_count + ")").find("td").length - 1;
			
		    var data_to_add = new Array();

		    for(var i = 0; i < col_count; i++){
			    data_to_add.push(
				    Elem.find("tr:nth-child(" + row_count + ")")
					    .find("td:nth-child(" + (i + 1) + ")")
					    .find("input[type=text]").attr("value")
			    );
		    }

		    var returned_data = call_back_after_clicking_add_button(data_to_add);
		    if( $.isArray(returned_data)) {
			    // start adding
			    var w = "";
			    w = w + "<tr clone='true'>";
			    for(var x = 0; x < returned_data.length; x++){
                    
				    w = w + "<td><input type=\"text\" ";
				    w = w + "value=\"" + returned_data[x] + "\"";
                    w = w + " readonly=\"readonly\" style=\"width:" + data_text_box_sizes[x] + ";\" /></td>";
			    }
				
			    // the delete button
			    w = w + "<td><a command=\"delete_button\" href=\"javascript:;\"> <img style=\"cursor:pointer; border:0px;\" src=\"" + baseurl + "Images/delete.png\"> </a></td>";
			    w = w + "</tr>";

                // clear the previous values
                Elem.find("tr:last-child").find("input[type=text]").attr("value", "");

                // display the new row
			    Elem.find("tr:last-child").prev().after(w);
                Elem.find("tr:last-child").prev().find("td input[type=text]").removeClass();

                if( after_add_cb != null) after_add_cb();

		    }
			
        });
    };
})(jQuery);

/*re v*/
(function ($, undefined) {
    $.fn.threertable = function (call_back_after_clicking_add_button, onclick_add_cb, data, after_add_cb) {
        var Elem = $(this);
        var settings = { "url": "", "index_value": "", "display_rowindex": "" };
        var is_valid_format = false;
        var column_count = 0;
        var is_data_an_array = false;

        // if (options) { $.extend(settings, options); }

        column_count = Elem.find("tr:first-child").find("td").length - 1;

        // add another column for the add button
        Elem.find("tr:nth-child(1)").append("<td></td>");

        // add another column for the add button
        Elem.find("tr:last-child").append("<td><a command=\"add_button\" href=\"javascript:;\"> <img style=\"cursor:pointer; border:0px;\" src=\"" + baseurl + "Images/add.png\"> </a></td>");

        // bind for delete button
        Elem.find("tr").find("td").find("a[command=delete_button]").live("click", function () {
            $(this).parent().parent().remove();
        });

        // additional design
        for(var z = 1; z <= column_count + 1; z++)
        Elem.find("tr:nth-child(1)").find("td:nth-child(" + z + ")").css("padding", "6px 8px 6px 8px");
        Elem.find("tr:nth-child(1)").css("background", "url(../Images/btn_bkg_hover.jpg)").css("height","28px;");
            
        Elem.css("border", "1px solid #cdcdcd");

        // bind the add button
        Elem.find("tr:last-child").find("td:last-child").find("a[command=add_button]").click(function () {

            // get textbox values
            var new_values = new Array();

            // 1st value
            new_values.push("");

            // loop through columns
            for(var xyz = 1; xyz <= column_count + 1; xyz++) {
                var curr_textbox = Elem.find("tr:last-child").find("td:nth-child(" + xyz + ")").find("input[type=text]");

                if( curr_textbox.attr("idvalue") != undefined && curr_textbox.attr("idvalue") != "") {
                    // replace the first value
                    new_values[0] = curr_textbox.attr("idvalue");
                }
                new_values.push(curr_textbox.attr("value"));
            }

            var is_ok = onclick_add_cb(new_values);

            // add the values
            // build the row
            if (is_ok) {
                var w = "<tr>";

                // insert new row
                var cell_option1 = "align=\"left\" style=\"padding-left:10px;font-weight:bold;\"";
                for(abc = 1; abc <= column_count + 1; abc++) {
                    if(abc == 1){
                        // column 1 and 2
                        w = w + "<td " + cell_option1 + "><input type=\"hidden\" value=\"" + new_values[abc - 1] + "\" />";
                        w = w + "<span>" + new_values[abc] + "</span></td>";
                    } else {
                        w = w + "<td><input type=\"text\" value=\"" + new_values[abc] + "\" /></td>";
                    }
                }

                w = w + "<td>"; w = w + "<a href=\"javascript:;\" command=\"delete_button\" ><img src=\"../Images/delete.png\" style=\"cursor:pointer; border:0px;\" /></a>"; w = w + "</td>";
                w = w + "</tr>";

                // clear values
                Elem.find("tr:last-child").find("input[type=text]").attr("value", "");

                // append
                Elem.find("tr:last-child").prev().after(w);

                // callback after adding
                if( after_add_cb != null) after_add_cb();
            } else {
                // 
            }

        });

        // load the initial data
        $(data).each( function (index, elem) {
            
            var n_value = new Array();
            for (var v in elem) n_value.push(elem[v]);

            var w = "";
            w = w + "<tr>";

            // insert new row
            var cell_option1 = "align=\"left\" style=\"padding-left:10px;font-weight:bold;\"";
            for(abc = 1; abc <= column_count + 1; abc++) {
                if(abc == 1){
                    // column 1 and 2
                    w = w + "<td " + cell_option1 + "><input type=\"hidden\" value=\"" + n_value[abc - 1] + "\" />";
                    w = w + "<span>" + n_value[abc] + "</span></td>";
                } else {
                    w = w + "<td><input type=\"text\" value=\"" + n_value[abc] + "\" /></td>";
                }
            }

            w = w + "<td></td></tr>";

            Elem.find("tr:last-child").prev().after(w);
        });
    };
})(jQuery);


/*re v*/
(function ($, undefined) {
    $.fn.threegtable = function (readonly_rows, onclick_add_cb, data) {
        var Elem = $(this);
        var settings = { "url": "", "index_value": "", "display_rowindex": "" };
        var is_valid_format = false;
        var column_count = 0;
        var is_data_an_array = false;
        var readonly_textbox = readonly_rows;

        // if (options) { $.extend(settings, options); }

        column_count = Elem.find("tr:first-child").find("td").length - 1;

        // add another column for the add button
        Elem.find("tr:nth-child(1)").append("<td></td>");

        // add another column for the add button
        Elem.find("tr:last-child").append("<td><a command=\"add_button\" href=\"javascript:;\"> <img style=\"cursor:pointer; border:0px;\" src=\"" + baseurl + "Images/add.png\"> </a></td>");

        // bind for delete button
        Elem.find("tr").find("td").find("a[command=delete_button]").live("click", function () {
            $(this).parent().parent().remove();
        });

        // additional design
        for (var z = 1; z <= column_count + 1; z++)
            Elem.find("tr:nth-child(1)").find("td:nth-child(" + z + ")").css("padding", "6px 8px 6px 8px");
        Elem.find("tr:nth-child(1)").css("background", "url(../Images/btn_bkg_hover.jpg)").css("height", "28px;");

        Elem.css("border", "1px solid #cdcdcd");

        // bind the add button
        Elem.find("tr:last-child").find("td:last-child").find("a[command=add_button]").click(function () {

            // get textbox values
            var new_values = new Array();
            
            // loop through columns
            for (var xyz = 1; xyz <= column_count + 1; xyz++) 
            new_values.push(Elem.find("tr:last-child").find("td:nth-child(" + xyz + ")").find("input[type=text]").attr("value"));

            var is_ok = onclick_add_cb(new_values);

            // add the values
            // build the row
            if (is_ok) {
                var w = "<tr>";

                // insert new row
                var cell_option1 = "readonly=\"readonly\" style=\"border:1px solid #f7f7f7; color:#525252; background:#fafafa;\"";
                for (abc = 0; abc <= column_count; abc++) {
                    if( jQuery.inArray(abc, readonly_textbox) > -1 ){
                        w = w + "<td><input " + cell_option1 + " type=\"text\" value=\"" + new_values[abc] + "\" /></td>";
                    }else {
                        w = w + "<td><input type=\"text\" value=\"" + new_values[abc] + "\" /></td>";
                    }
                }

                w = w + "<td>"; w = w + "<a href=\"javascript:;\" command=\"delete_button\" ><img src=\"../Images/delete.png\" style=\"cursor:pointer; border:0px;\" /></a>"; w = w + "</td>";
                w = w + "</tr>";

                // clear values
                Elem.find("tr:last-child").find("input[type=text]").attr("value", "");

                // append
                Elem.find("tr:last-child").prev().after(w);
            } else {
                // 
            }

        });

        // load the data
        $(data).each(function (index, elem) {

            var n_value = new Array();
            for (var v in elem) n_value.push(elem[v]);

            var w = "";
            w = w + "<tr>";

            // insert new row
            var cell_option1 = "readonly=\"readonly\" style=\"border:1px solid #f7f7f7; color:#525252; background:#fafafa\"";
            for (abc = 0; abc <= column_count; abc++) {
                if( jQuery.inArray(abc, readonly_textbox) > -1 ){
                    w = w + "<td><input " + cell_option1 + " type=\"text\" value=\"" + n_value[abc] + "\" /></td>";
                }else {
                    w = w + "<td><input type=\"text\" value=\"" + n_value[abc] + "\" /></td>";
                }
            }

            w = w + "<td></td></tr>";

            Elem.find("tr:last-child").prev().after(w);
        });
    };
})(jQuery);

/*
    <div>erorr occured</div>
*/
(function ($, undefined) {
    $.fn.infomessage = function (message, info_type) {
        
        var Elem = $(this);
        
        if(info_type == "error") {
            Elem.css("padding", "7px 13px 7px 13px").css("background", "#fefdf5").css("color", "#e54848").css("border", "1px solid #e54848");
            var tbl_b = "";
            tbl_b = tbl_b + "<table><tr><td><img src=\"../Images/error.png\" stlye=\"border:0px;\" /></td>";
            tbl_b = tbl_b + "<td>" + message + "</td>";
            tbl_b = tbl_b + "</tr></table>";
            Elem.prepend(tbl_b);

        } else if(info_type == "info"){
            Elem.css("padding", "7px 13px 7px 13px").css("background", "#fefdf5").css("color", "#188cd6").css("border", "1px solid #48a1e5");
            var tbl_b = "";
            tbl_b = tbl_b + "<table><tr><td><img src=\"../Images/information.png\" stlye=\"border:0px;\" /></td>";
            tbl_b = tbl_b + "<td>" + message + "</td>";
            tbl_b = tbl_b + "</tr></table>";
            Elem.prepend(tbl_b);
        }

    }
})(jQuery);

// decifield
/*
    numeric input only
*/
(function ($, undefined) {
    $.fn.decifield = function (options) {
        
        var Elem = $(this);
        
        Elem.css("text-align", "right");

        Elem.bind("keypress",
		    function (e) {
		        if ((Elem.attr("value").length == 0 || Elem.attr("value").indexOf(".") > -1) && e.which == 46) return false;
		        if (Elem.attr("value").length == 0 && e.which == 48) return false;
		        if ((e.which >= 48 && e.which <= 57) || e.which == 46) return true;
		        return false;
		    }
	    );

        Elem.bind("keyup",
		    function (e) {
		        if ((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105)) {
                    var txt_value = Elem.attr("value");
                    var value_wo_comma = txt_value.replace(/,/g, "");
                    var whole_no = GetWholeNo(value_wo_comma);
                    var decimal_no = value_wo_comma.replace(whole_no, "");
                    var final_output = "";
                    
                    decimal_no = decimal_no.replace(/\./g, "");
                    if(decimal_no != "") decimal_no = '.' + decimal_no;

                    if(whole_no.length > 3)
                        for(var i = whole_no.length; i > 0; i = i - 3){
                            if( i - 3 > 0) final_output = ',' + whole_no.slice(i - 3, i) + final_output;
                            else final_output = whole_no.slice(0, i) + final_output;
                        }
                    else final_output = whole_no;
                    Elem.attr("value", final_output + decimal_no);
		        }
		    }
	    );

        function GetWholeNo(value) {
            return value.match(/\d*/ig)[0];
        }

    }
})(jQuery);

// table
/*
    
*/
(function ($, undefined) {
    $.fn.tablelist = function (url, initial_data, cb_clicked_link) {
        // element
        var Elem = $(this);

        // count the column
        var column_no = Elem.find("td").length;
        
        var data = initial_data;
        var tbl_header = Elem.find("tr:first-child");

        // set width = "100%"
        Elem.attr("width", "100%");

        // add row for the first, previous, last, next page links
        var div_wrapper = "<div>";
        Elem.wrap(div_wrapper);
        
        var navi_links = "";
        var navi_links_buffer = new Array();

        navi_links_buffer.push("<div comm=\"navs\" >");
        navi_links_buffer.push("<center>");
        navi_links_buffer.push("<a href=\"javascript:;\" comm=first > |<< </a> &nbsp; / &nbsp;");
        navi_links_buffer.push("<a href=\"javascript:;\" comm=prev > < </a> &nbsp; / &nbsp;");
        navi_links_buffer.push("<a href=\"javascript:;\" comm=next > > </a> &nbsp; / &nbsp;");
        navi_links_buffer.push("<a href=\"javascript:;\" comm=last > >>| </a> ");
        navi_links_buffer.push("</center>");
        navi_links_buffer.push("</div>");

        navi_links = navi_links_buffer.join("");
        Elem.after(navi_links);

        // design the table
        Elem.attr("cellspacing", "0").attr("border", "0").attr("cellpadding", "3");

        // design the headers
        tbl_header
            .css("background", "url(../Images/btn_bkg_hover.jpg)")
            .find("td")
            .css("font-weight", "bold").attr("align", "center");

        tbl_header.find("td:gt(0)").css("border-left", "1px solid #ededed");
        Elem.css("border", "1px solid #ededed");

        // bind the created links
        Elem.next().find("a[comm=first]").click(
            function(){
                Elem.find("tr:gt(0)").remove();
                tbl_header.after(BrowseRecord());
            }
        );

        Elem.next().find("a[comm=prev]").click(
            function(){
                Elem.find("tr:gt(0)").remove();
                tbl_header.after(BrowseRecord());
            }
        );

        Elem.next().find("a[comm=next]").click(
            function(){
                
            }
        );

        Elem.next().find("a[comm=last]").click(
            function(){
                
            }
        );

        Elem.find("tr:gt(0)").find("td a").live("click", function(){
            var id = $(this).html();
            cb_clicked_link(id);
        });
        
        tbl_header.after(BrowseRecord());

        function BrowseRecord() {
            // use the cached data
            var starting_rec = 0;
            var ending_rec = 0;

            // get the length
            var new_rows = "";
            var new_rows_buffer = new Array();

            var hover_style = " class=\"grid_hover\" ";
            $(data).each(
                function (index, elem){
                    new_rows_buffer.push("<tr " + hover_style + " >");

                    // columns
                    var new_cols = "";
                    var new_cols_buffer = new Array();

                    var column_divider_style = "";
                    $(elem).each(
                        function (sub_index, sub_elem){
                            if( sub_index != 0 )
                                column_divider_style = " style=\"border-left:1px solid #ededed;\" ";
                            else
                                column_divider_style = "";

                            new_cols_buffer.push("<td " + column_divider_style + " >");

                            if(sub_index == 0){
                                new_cols_buffer.push("<a href=\"javascript:;\">" +sub_elem + "</a></td>");
                            } else{
                                new_cols_buffer.push(sub_elem + "</td>");
                            }
                        }
                    );

                    new_cols = new_cols_buffer.join("");

                    new_rows_buffer.push(new_cols);
                    new_rows_buffer.push("</tr>\n");
                }
            );
            
            new_rows = new_rows_buffer.join("");

            // append
            return new_rows;
        }

        function DisplayPreloader(){

        }

        function GetDataFromUrl(){
            


        }

    }
})(jQuery);


(function ($, undefined) {
    $.fn.uploadlink = function (url, target_textbox, temp_folder) {
        var filename_to_upload = "";
        var is_to_inspect = false;
        var Elem = $(this);

        var s_sub_content = ""
            + "<div style = \"margin:2px 2px 0px 2px; display:none; background:#fff7cc; font-family:arial; font-size:11px; font-weight:bold;\" >"
                + "<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td><img src=\"" + baseUrl + "Images/error.png\" />"
                + "</td><td>Error occured while uploading!</td></tr>"
                + "</table>"
            + "</div>"

            + "<div nname=\"pre_loader\" style='display:none;padding:3px;'>"
                + "<table cellpadding=\"2\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td style=\"font-size:11px; font-weight:bold; font-family:arial;\">Uploading"
                + "</td></tr>"
                + "<tr><td><img src=\"" + baseUrl + "Images/143.gif\" />"
                + "</td></tr>"
                + "</table>"
            + "</div>"

            + "<div style='padding:3px;'>"
                + "<iframe id=\"iframe_uploader\" iframe_uploader=\"iframe_uploader\" frameborder=\"0\" height=\"60px\" >"
                + "</iframe>"
            + "</div>"

            + "<div style=\"text-align:right; padding:3px;\">"
                + "<input nname=\"btn_upload\" type=\"button\" value=\"Upload\" >"
                + "&nbsp; / &nbsp;"
                + "<input nname=\"btn_cancel\" type=\"button\" value=\"Cancel\" >"
            + "</div>"
            + "";

        var s_content = "<div uploader_content=\"uploader_content\" style='display:none; top:50%;left:45%;position:fixed; background:#ffffff;'>" + s_sub_content +  "</div>";
        var s_bkg = "<div uploader_bkg=\"uploader_bkg\" style=\"display:none; background:#000000; opacity: 0.60; filter:alpha(opacity=60); height:100%; width:100%; position:fixed; top:0px; left:0px;\">&nbsp;</div>";

        Elem.click(
            function (){
                $("body").append(s_bkg);
                $("body").append(s_content);
                
                var upload_button =  $("#iframe_uploader").parent().next().find("input[nname=btn_upload]");
                var cancel_button =  $("#iframe_uploader").parent().next().find("input[nname=btn_cancel]");

                // bind
                cancel_button.click(
                    function (){
                        up_content.hide("fast", function(){ up_content.remove(); up_bkg.remove(); } );
                        up_bkg.hide();
                        is_to_inspect = false;
                    }
                );

                upload_button.click(
                    function (){
                        iframe_group.hide(); // hide iframe
                        pre_loader.show(); // show preloader

                        filename_to_upload = iframe.contents().find("body").find("form").find("input").attr("value");
                        if(filename_to_upload != "" && filename_to_upload != null) {
                            filename_to_upload = getFileName(filename_to_upload);
                            is_to_inspect = true;
                            iframe.contents().find("body").find("form").submit();
                        } else {
                            alert("No file Selected!");
                            pre_loader.hide(); // hide preloader
                            iframe_group.show(); // show iframe
                        }
                    }
                );

                var pre_loader = $("#iframe_uploader").parent().prev();
                var error_message = $("#iframe_uploader").parent().prev().prev();

                // manual mode
                var x = document.getElementById("iframe_uploader"); var d = x.contentWindow.document; d.open(); d.close(); 
                
                // auto mode
                var iframe = $("#iframe_uploader");
                var iframe_group = $("#iframe_uploader").parent();

                var up_bkg = $("div[uploader_bkg=uploader_bkg]");
                var up_content = $("div[uploader_content=uploader_content]");

                // include the temporary folder
                iframe.contents().find("body").append(
                    "<form enctype=\"multipart/form-data\" method=\"post\" action=\"" + url + "\" >" +
                    "<center><input type=\"file\" name=\"upload_control\" /></center>" +
                    "<input type=\"hidden\" name=\"tempfolder\" value=\"" + temp_folder + "\" >" +
                    "</form>" 
                );
                
                // additional binds
                iframe.load(function() {
                    
                    if( is_to_inspect == true ){
                        var uploading_res = iframe.contents().find("body").html();
                        if( uploading_res == "0" ){
                            up_content.hide("fast", function(){ up_content.remove(); up_bkg.remove(); } );
                            up_bkg.hide();
                            $("#" + target_textbox).attr("value", filename_to_upload); // place to the textbox
                            is_to_inspect = false;
                        } else {
                            upload_button.hide();
                            pre_loader.hide("fast", function() { error_message.show(); });
                        }
                    }
                });

                up_bkg.show(); up_content.show("fast");
            }
        );

        function getFileName(path) {
            return path.match(/[^\\]*$/i)[0];
            // return path.match(/[-_\w]+[.][\w]+$/i)[0];
        }

    }
})(jQuery);


/**
.
.   This code is Inserted by HTI

**/

(function ($, undefined) {
    $.fn.uploadPic = function (url, target_textbox, temp_folder) {
        var filename_to_upload = "";
        var is_to_inspect = false;
        var Elem = $(this);

        var s_sub_content = ""
            + "<div style = \"margin:2px 2px 0px 2px; display:none; background:#fff7cc; font-family:arial; font-size:11px; font-weight:bold;\" >"
                + "<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td><img src=\"" + baseurl + "Images/error.png\" />"
                + "</td><td>Error occured while uploading!</td></tr>"
                + "</table>"
            + "</div>"

            + "<div nname=\"pre_loader\" style='display:none;padding:3px;'>"
                + "<table cellpadding=\"2\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td style=\"font-size:11px; font-weight:bold; font-family:arial;\">Uploading"
                + "</td></tr>"
                + "<tr><td><img src=\"" + baseurl + "Images/143.gif\" />"
                + "</td></tr>"
                + "</table>"
            + "</div>"

            + "<div style='padding:3px;'>"
                + "<iframe id=\"iframe_uploader\" iframe_uploader=\"iframe_uploader\" frameborder=\"0\" height=\"60px\" >"
                + "</iframe>"
            + "</div>"

            + "<div style=\"text-align:right; padding:3px;\">"
                + "<input nname=\"btn_upload\" type=\"button\" value=\"Upload\" >"
                + "&nbsp; / &nbsp;"
                + "<input nname=\"btn_cancel\" type=\"button\" value=\"Cancel\" >"
            + "</div>"
            + "";

        var s_content = "<div uploader_content=\"uploader_content\" style='display:none; top:50%;left:45%;position:fixed; background:#ffffff;'>" + s_sub_content + "</div>";
        var s_bkg = "<div uploader_bkg=\"uploader_bkg\" style=\"display:none; background:#000000; opacity: 0.60; filter:alpha(opacity=60); height:100%; width:100%; position:fixed; top:0px; left:0px;\">&nbsp;</div>";

        Elem.click(
            function () {
                $("body").append(s_bkg);
                $("body").append(s_content);

                var upload_button = $("#iframe_uploader").parent().next().find("input[nname=btn_upload]");
                var cancel_button = $("#iframe_uploader").parent().next().find("input[nname=btn_cancel]");

                // bind
                cancel_button.click(
                    function () {
                        up_content.hide("fast", function () { up_content.remove(); up_bkg.remove(); });
                        up_bkg.hide();
                        is_to_inspect = false;
                    }
                );

                upload_button.click(
                    function () {
                        iframe_group.hide(); // hide iframe
                        pre_loader.show(); // show preloader

                        filename_to_upload = iframe.contents().find("body").find("form").find("input").attr("value");
                        if (filename_to_upload != "" && filename_to_upload != null) {
                            filename_to_upload = getFileName(filename_to_upload);
                            is_to_inspect = true;
                            iframe.contents().find("body").find("form").submit();
                        } else {
                            alert("No file Selected!");
                            pre_loader.hide(); // hide preloader
                            iframe_group.show(); // show iframe
                        }
                    }
                );

                var pre_loader = $("#iframe_uploader").parent().prev();
                var error_message = $("#iframe_uploader").parent().prev().prev();

                // manual mode
                var x = document.getElementById("iframe_uploader"); var d = x.contentWindow.document; d.open(); d.close();

                // auto mode
                var iframe = $("#iframe_uploader");
                var iframe_group = $("#iframe_uploader").parent();
                var pictureholder = $(".Big_Picture_Box");

                var up_bkg = $("div[uploader_bkg=uploader_bkg]");
                var up_content = $("div[uploader_content=uploader_content]");

                // include the temporary folder
                iframe.contents().find("body").append(
                    "<form enctype=\"multipart/form-data\" method=\"post\" action=\"" + url + "\" >" +
                    "<center><input type=\"file\" name=\"upload_control\" /></center>" +
                    "<input type=\"hidden\" name=\"tempfolder\" value=\"" + temp_folder + "\" >" +
                    "</form>"
                );

                iframe.contents().find("nname=\"btn_upload\"").hide();

                // additional binds
                iframe.load(function () {

                    if (is_to_inspect == true) {
                        var uploading_res = iframe.contents().find("body").html();
                        if (uploading_res == "0") {
                            up_content.hide("fast", function () { up_content.remove(); up_bkg.remove(); });
                            up_bkg.hide();
                            $("#" + target_textbox).attr("value", filename_to_upload); // place to the textbox
                           
        pictureholder.find("#Big_Profile_Pic,img").remove();
         //pictureholder.append("<img id=\"profile_pic\" src=\"/AficionadoPhoto/" + filename_to_upload + "\"  width=84px height=76px; style=\"margin-top:12px;\">");
         pictureholder.append("<img id=\"profile_pic\" src=\""+baseurl+"AficionadoPhoto/" + filename_to_upload + "\"  width=84px height=76px; style=\"margin-top:12px;\">");
        //pictureholder.append("<img id=\"profile_pic\" src=\""+baseurl+"D:/Photo/Aficionado/Temp" + filename_to_upload + "\"  width=84px height=76px; style=\"margin-top:12px;\">");
                               
                            is_to_inspect = false;
                        } else {                           
                            upload_button.hide();
                            pre_loader.hide("fast", function () { error_message.show(); });
                        }
                    }
                });

                up_bkg.show(); up_content.show("fast");
            }
        );

        function getFileName(path) {
            return path.match(/[^\\]*$/i)[0];
            // return path.match(/[-_\w]+[.][\w]+$/i)[0];
        }

    }
})(jQuery);




/**
.
. End code insereteb by HTI
**/

function FormatDate(date_value) {
    if (date_value == null || date_value == undefined || date_value == ""){
        return "";
    }
    var d_n_date = new Date(parseInt(date_value.substr(6)));
    var d_date = d_n_date.getDate();
    var d_month = d_n_date.getMonth() + 1;
    var d_year = d_n_date.getFullYear();

    return d_month + "/" + d_date + "/" + d_year;
}

function FormatFloat(value_with_comma) {
    var new_value = value_with_comma.replace(/,/g , "");
    return new_value;
}

function FormatInt(value_with_comma) {
    var new_value = value_with_comma.replace(/,/g, "");
    new_value = new_value.replace(/\..*/g, "");
    return new_value;
}

function FotmatTextBoxTonumeric(Elem){
    Elem.css("text-align", "right");
    var txt_value = Elem.attr("value");
    var value_wo_comma = txt_value.replace(/,/g, "");
    var whole_no = value_wo_comma.match(/\d*/ig)[0];
    var decimal_no = value_wo_comma.replace(whole_no, "");
    var final_output = "";

    decimal_no = decimal_no.replace(/\./g, "");
    if (decimal_no != "") decimal_no = '.' + decimal_no;
    else decimal_no = ".00";

    if (whole_no.length > 3) {
        for (var i = whole_no.length; i > 0; i = i - 3) {
            if (i - 3 > 0) final_output = ',' + whole_no.slice(i - 3, i) + final_output;
            else final_output = whole_no.slice(0, i) + final_output;
        }
    } else {
        final_output = whole_no; 
    }
    Elem.attr("value", final_output + decimal_no);
}

function FormatToAcctNumber(value) {
    var txt_value = value;
    var value_wo_comma = txt_value.replace(/,/g, "");
    var whole_no = value_wo_comma.match(/-?\d*/ig)[0];
    var decimal_no = value_wo_comma.replace(whole_no, "");
    var final_output = "";

    decimal_no = decimal_no.replace(/\./g, "");
    if (decimal_no != "") {
        if (parseFloat(decimal_no) == 0) {
            decimal_no = '.' + "00";
        } else {
            decimal_no = '.' + decimal_no;
        }
    } else {
        decimal_no = ".00"; 
    }

    if (whole_no.length > 3) {
        for (var i = whole_no.length; i > 0; i = i - 3) {
            if (i - 3 > 0) final_output = ',' + whole_no.slice(i - 3, i) + final_output;
            else final_output = whole_no.slice(0, i) + final_output;
        }
    } else {
        final_output = whole_no;
    }

    return final_output + decimal_no;
}

(function ($, undefined) {
    $.fn.defaulter = function (options) {
        var Elem = $(this);

        Elem.unbind();
    }
})(jQuery);

(function ($, undefined) {
    $.fn.tablelist1 = function (list_of_date, cb_clicked_link1, cb_clicked_link2, cb_clicked_link3, cb_clicked_link4) {
        // element
        var Elem = $(this);

        // remodel the table
		function Remodel(){
			
			var tbl_header = Elem.find("tr:first-child");
			
			// design the table
			Elem.attr("cellspacing", "0").attr("border", "0").attr("cellpadding", "3");

			// design the headers
			tbl_header
				.css("background", "url(../Images/btn_bkg_hover.jpg)")
				.find("td")
				.css("font-weight", "bold").attr("align", "center");

			tbl_header.find("td:gt(0)").css("border-left", "1px solid #ededed");
			Elem.css("border", "1px solid #ededed");
			
			Elem.attr("Remodeled", "true");

            // add rows for the links
            var col_count = Elem.find("tr:nth-child(1)").find("td").length;

            var w = new Array();
            w.push("<tr>");
            w.push("<td align=\"center\" colspan=\"" + col_count + "\" >");

            w.push("<a href=\"javascript:;\"> |<< </a>"); w.push(" &nbsp; &nbsp; ");
            w.push("<a href=\"javascript:;\"> < </a>"); w.push(" &nbsp; &nbsp; ");
            w.push("<a href=\"javascript:;\"> > </a>"); w.push(" &nbsp; &nbsp; ");
            w.push("<a href=\"javascript:;\"> >>| </a>");

            w.push("</td>");
            w.push("</tr>");

            Elem.append(w.join(""));

		}
		
		if( Elem.attr("Remodeled") != "true" && Elem.attr("Remodeled") == null ){
			Remodel();
		}

    }
})(jQuery);


function RemodeltableList(tbl_obj){
			
	var tbl_header = tbl_obj.find("tr:nth-child(1)");
			
	// design the table
	tbl_obj.attr("cellspacing", "0").attr("border", "0").attr("cellpadding", "3");

	// design the headers
	tbl_header
		.css("background", "url(../Images/btn_bkg_hover.jpg)")
		.find("td")
		.css("font-weight", "bold").attr("align", "center");
	

	tbl_header.find("td:gt(0)").css("border-left", "1px solid #ededed");
	tbl_obj.css("border", "1px solid #ededed");
			
	tbl_obj.attr("Remodeled", "true");

}

function DisplayDataToTable(data) {

}

function ReCalculatePageNumbers(current_pageno, total_pageno) {

    last_page_no = total_pageno;

    if (current_pageno > 1) {
        prev_page_no = current_pageno - 1;
    } else {
        prev_page_no = current_pageno;
    }

    if (current_pageno < last_page_no) {
        next_page_no = current_pageno + 1;
    } else {
        next_page_no = last_page_no;
    }

}


(function ($, undefined) {
    $.fn.uploadlink2 = function (url, target_textbox, temp_folder, call_back_success) {
   
        var filename_to_upload = "";
        var is_to_inspect = false;
        var Elem = $(this);

        var s_sub_content = ""
            + "<div style = \"margin:2px 2px 0px 2px; display:none; background:#fff7cc; font-family:arial; font-size:11px; font-weight:bold;\" >"
                + "<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td><img src=\"" + baseUrl + "Images/error.png\" />"
                + "</td><td>Error occured while uploading!</td></tr>"
                + "</table>"
            + "</div>"

            + "<div nname=\"pre_loader\" style='display:none;padding:3px;'>"
                + "<table cellpadding=\"2\" cellspacing=\"0\" border=\"0\" >"
                + "<tr><td style=\"font-size:11px; font-weight:bold; font-family:arial;\">Uploading"
                + "</td></tr>"
                + "<tr><td><img src=\"" + baseUrl + "Images/143.gif\" />"
                + "</td></tr>"
                + "</table>"
            + "</div>"

            + "<div style='padding:3px;'>"
                + "<iframe id=\"iframe_uploader\" iframe_uploader=\"iframe_uploader\" frameborder=\"0\" height=\"60px\" >"
                + "</iframe>"
            + "</div>"

            + "<div style=\"text-align:right; padding:3px;\">"
                + "<input nname=\"btn_upload\" type=\"button\" value=\"Upload\" >"
                + "&nbsp; / &nbsp;"
                + "<input nname=\"btn_cancel\" type=\"button\" value=\"Cancel\" >"
            + "</div>"
            + "";

        var s_content = "<div uploader_content=\"uploader_content\" style='display:none; z-index:100; top:50%;left:45%;position:fixed; background:#ffffff;'>" + s_sub_content +  "</div>";
        var s_bkg = "<div uploader_bkg=\"uploader_bkg\" style=\"display:none; z-index:100; background:#000000; opacity: 0.60; filter:alpha(opacity=60); height:100%; width:100%; position:fixed; top:0px; left:0px;\">&nbsp;</div>";


        Elem.click(

            function (){
                $("body").append(s_bkg);
                $("body").append(s_content);
                
                var upload_button =  $("#iframe_uploader").parent().next().find("input[nname=btn_upload]");
                var cancel_button =  $("#iframe_uploader").parent().next().find("input[nname=btn_cancel]");

                // bind
                cancel_button.click(
                    function (){
                        up_content.hide("fast", function(){ up_content.remove(); up_bkg.remove(); } );
                        up_bkg.hide();
                        is_to_inspect = false;
                        //location.reload();
                    }
                );

                upload_button.click(
                
                    function (){
                            iframe_group.hide(); // hide iframe
                            pre_loader.show(); // show preloader

                            filename_to_upload = iframe.contents().find("body").find("form").find("input").attr("value");
                                if(filename_to_upload != "" && filename_to_upload != null) {
                                    filename_to_upload = getFileName(filename_to_upload);
                                    is_to_inspect = true;
                                    iframe.contents().find("body").find("form").submit();
                                } else {
                                    alert("No file Selected!");
                                    pre_loader.hide(); // hide preloader
                                    iframe_group.show(); // show iframe
                                }
                            }

                    );

                var pre_loader = $("#iframe_uploader").parent().prev();
                var error_message = $("#iframe_uploader").parent().prev().prev();

                // manual mode
                var x = document.getElementById("iframe_uploader"); var d = x.contentWindow.document; d.open(); d.close(); 
                
                // auto mode
                var iframe = $("#iframe_uploader");
                var iframe_group = $("#iframe_uploader").parent();

                var up_bkg = $("div[uploader_bkg=uploader_bkg]");
                var up_content = $("div[uploader_content=uploader_content]");

                // include the temporary folder
                iframe.contents().find("body").append(
                    "<form enctype=\"multipart/form-data\" method=\"post\" action=\"" + url + "\" >" +
                    "<center><input type=\"file\" name=\"upload_control\" /></center>" +
                    "<input type=\"hidden\" name=\"tempfolder\" value=\"" + temp_folder + "\" >" +
                    "</form>" 
                );
                
                // additional binds
                iframe.load(function() {
                    
                    if( is_to_inspect == true ){
                        var uploading_res = iframe.contents().find("body").html();
                        
                        if( uploading_res != "0" ) {
                            if(uploading_res=="Not all customer accounts are assign to this account." || uploading_res=="One of the brand is invalid." || uploading_res=="One of the objective code is invalid" || uploading_res=="Customer account already exist.") {
                                //upload_button.hide();
//                                pre_loader.hide("fast");
                               up_content.hide("fast", function(){ up_content.remove(); up_bkg.remove(); } );
                                up_bkg.hide();
                                is_to_inspect = false;
                                alert(uploading_res);
                            }
                            else {
                            // could only be JSON
                            // return the json object

                            up_content.hide("fast", function(){ up_content.remove(); up_bkg.remove(); } );
                            up_bkg.hide();
                            
                            is_to_inspect = false;
                            call_back_success($.parseJSON(uploading_res));
                            }
                        } else {
                            upload_button.hide();
                            pre_loader.hide("fast", function() { error_message.show(); });
                        }

                    }
                });

                up_bkg.show(); up_content.show("fast");
            }
        );

        function getFileName(path) {
            return path.match(/[^\\]*$/i)[0];
        }
        
        }
})(jQuery);




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

(function ($, undefined) {
    $.fn.addDeliveryAddress = function () {
        var Elem = $(this);
        var txtDeliveryAddress= Elem.parent().prev().find('input[type="text"]');
        var w = new Array();

        w.push("<tr style=\"height:22px;\" valign=\"top\">");
        w.push("<td><input type=\"text\"  value=\""+ (txtDeliveryAddress.attr("value")) +"\" style=\"width:99%;\" maxlength=\"100\"  /></td>");
        w.push("<td><input type=\"image\" src=\"" + baseUrl + "Images/delete.png\" onclick=\"javascript:deleteRow($(this));\" /></td>");
        w.push("</tr");

        Elem.parent().parent().before(w.join(""));
        txtDeliveryAddress.removeAttr("value");

        return this;
    };
})(jQuery);

(function ($,undefined){
    $.fn.populateData = function(data){
        $(data).each(function(index,item){
            var Elem = $(this);
            var txtDeliveryAddress= Elem.parent().prev().find('input[type="text"]');
            var w = new Array();

            w.push("<tr style=\"height:22px;\" valign=\"top\">");
            w.push("<td><input type=\"text\"  value=\""+ item[0] +"\" style=\"width:99%;\" maxlength=\"100\"  /></td>");
            w.push("<td><input type=\"image\" src=\"" + baseUrl + "Images/delete.png\" onclick=\"javascript:deleteRow($(this));\" /></td>");
            w.push("</tr");

            Elem.parent().parent().before(w.join(""));
        });
    };
})(jQuery);

function deleteRow(elem){
    if(confirm("are you sure you want to delete this row?")){
        elem.parent().parent().remove();  
    }
}
   
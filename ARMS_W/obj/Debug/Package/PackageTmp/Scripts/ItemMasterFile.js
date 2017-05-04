var ns4;

$(function () {
    blockUI();
    getItemmasterFile();

});


/**function displayItems() {

    $.ajax({
        type: "POST", url: baseUrl + "ItemMasterFile/GetDetails",
        success: function (res) {
            $(res.itemmasterfile).each(function (index, item) {
                addItemToDisplay(item.ItemCode, item.ItemDesc, item.Multiplier, item.Thickness, item.Width, item.Length, item.Productgrp, item.PLName);
            });

            // fnFeaturesInit();
            $('#tbl_list').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bSortClasses": false
            });
            // $(".DataTables_sort_wrapper").find('span').css("float", "right");
            $('.btn_check').hide();

            unblockUI();
            // HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
            //    HidePreloader();
        }
    });
}**/



function getItemmasterFile() {

    var new_obj = {}


    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseurl + "ItemMasterFile/GetDetails",
        success: function (res) {



            $(res.data.Itemmasterfile).each(function (index, item) {
                addItemToDisplay(item.ItemCode, item.ItemDesc, item.Multiplier, item.Thickness, item.Width, item.Length, item.Productgrp,item.Brand, item.PLName);
            });

            $('#tbl_list').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bSortClasses": false
                             
                              

            });

            unblockUI();

            $('.btn_check').hide();
            $(".DataTables_sort_wrapper").find('span').css("float", "right");

            // HidePreloader();



        },
        error: function (xhr, ajaxOptions, thrownError) {
            unblockUI();
            alert(xhr.status); alert(thrownError);

        }
    });




}



function addItemToDisplay(ItemCode, ItemDesc, Multiplier, Thickness, Width, Length, Productgrp, Brand, PLName) {
   
    $('#tbl_list tbody').append('<tr clone="true">' +
                                '<td><span>' + ItemCode + '</span></td>' +
                                '<td><span>' + ItemDesc + '</span></td>' +
                                '<td><span>' + Multiplier + '</span></td>' +
                                '<td><span>' + Thickness + '</span></td>' +
                                '<td><span>' + Width + '</span></td>' +
                                '<td><span>' + Length + '</span></td>' +
                                '<td><span>' + Productgrp + '</span></td>' +
                                '<td><span>' + Brand + '</span></td>' +
                                '<td><span>' + PLName + '</span></td>' +
                                '</tr>');

}

function fnFeaturesInit() {
    /* Not particularly modular this - but does nicely :-) */
    $('ul.limit_length>li').each(function (i) {
        if (i > 10) {
            this.style.display = 'none';
        }
    });

    $('ul.limit_length').append('<li class="css_link">Show more<\/li>');
    $('ul.limit_length li.css_link').click(function () {
        $('ul.limit_length li').each(function (i) {
            if (i > 5) {
                this.style.display = 'list-item';
            }
        });
        $('ul.limit_length li.css_link').css('display', 'none');
    });
}



function blockUI() {
    $.blockUI({ css: {
        border: 'none',
        padding: '15px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .5,
        color: '#fff'
    }
    });
}

function unblockUI() {
    //   $.HidePreloader();
    $.unblockUI();
}
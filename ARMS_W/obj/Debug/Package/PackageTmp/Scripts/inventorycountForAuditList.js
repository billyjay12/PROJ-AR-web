$(function () {
    //DisplayPreloader();
    blockUI();
    displayInventoryCounts();
});

function displayInventoryCounts() {
    $.ajax({
        type: "POST", url: baseUrl + "Inventory/getForAuditInventoryCountList",
        success: function (res) {
            $(res.data.list).each(function (index, item) {
                addItemToDisplay(item.inventoryCountId, item.empId, item.empName, item.acctCode, item.acctName, item.acctAddress, item.statusDesc, item.roleId, item.invStatus, item.inventoryCountMonth);
            });

            // fnFeaturesInit();

            var oTable = $('#tbl_list').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bSortClasses": false
            });

            // HidePreloader();
            unblockUI();

            $('.btn_check').hide();
            $(".DataTables_sort_wrapper").find('span').css("float", "right");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            // HidePreloader();
            unblockUI();
            alert(xhr.status); alert(thrownError);
        }
    });
}

function addItemToDisplay(inventoryCountId, empId, empName, acctCode, OutletName, Location, StatusDesc, roleId, invStatus, inventoryMonth) {

    $('#tbl_list tbody').append('<tr class="grid_hover" clone="true">' +
        '<td align="center"><a href="' + baseUrl + 'Inventory/CreateNewInventoryCountAudit?InventoryCountId=' + inventoryCountId + '" style="color:#da4707;font-weight:bold;text-decoration:none;">' + inventoryCountId + '</a></td>' +
        '<td align="center"><span>' + empId + '</span></td>' +
        '<td align="center"><span>' + empName + '</span></td>' +
        '<td align="center"><span>' + acctCode + '</span></td>' +
        '<td align="center"><span>' + OutletName + '</span></td>' +
        '<td align="center"><span>' + Location + '</span></td>' +
        '<td align="center"><span>' + inventoryMonth + '</span></td>' +
        '<td align="center"><span>' + invStatus + '</span></td>' +
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
    //$.HidePreloader();
    $.unblockUI();
}

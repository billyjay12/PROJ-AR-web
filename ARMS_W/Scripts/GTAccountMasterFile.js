var txt_pareto = null;
var txt_acctCode = null;
var txt_acctName = null;


var btn_save = null;
var btn_cancel = null;

var isPareto = false;

$(function () {
    //DisplayPreloader();
    blockUI();
    displayAccounts();


    txt_pareto = $("#txt_pareto");
    txt_acctCode = $("#txt_acctCode");
    txt_acctName = $("#txt_acctName");

    btn_cancel = $("#btn_cancel");

    btn_save = $("#btn_save");

    btn_save.click(function () {
        if (txt_acctCode.attr("value") != "" || txt_acctName.attr("value") || txt_pareto.attr("value"))
            updateAccount();
        else
            alert("Select Account Code Below");
    });

    btn_cancel.click(function () {
        $("#btnrad_yes").attr("checked", false);
        $("#btnrad_no").attr("checked", false);
        txt_acctCode.removeAttr("value");
        txt_acctName.removeAttr("value");
    });


});

function displayAccounts() {

    $.ajax({
        type: "POST", url: baseUrl + "MasterFile/GetDetails",
        success: function (res) {
            $(res.accountList).each(function (index, item) {
                addItemToDisplay(item.acct_ccanum, item.acctCode, item.area, item.Pareto, item.acctName, item.firstNameSO, item.lastNameSO);
            });

            $('#tbl_list').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bSortClasses": false
            });
            $('.btn_check').hide();

            unblockUI();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            unblockUI();
            //    HidePreloader();
        }
    });
}

function addItemToDisplay(ccaNum, acctCode, area, pareto, acctName, firstName, lastName) {
    var _acctName = acctName;
    if (_acctName.indexOf("'") != -1) {
        _acctName = _acctName.replace("'", "");
    }
    
    $('#tbl_list tbody').append('<tr clone="true">' +
                                '<td><span>' + ccaNum + '</span></td>' +
                                '<td align="center"><a href="javascript:selected(\'' + acctCode + '\',\'' + _acctName + '\',\'' + pareto + '\')" >' + acctCode + '</a></td>' +
                                '<td><span>' + _acctName + '</span></td>' +
                                '<td><span>' + firstName + ' ' + lastName + '</span></td>' +
                                '<td><span>' + area + '</span></td>' +
                                '<td><span>' + (pareto == true ? "Pareto" : "Non-Pareto") + '</span></td>' +
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


function selected(acctCode, acctName, pareto) {
    txt_acctCode.attr("value", acctCode);
    txt_acctName.attr("value", acctName);

    if (pareto == "true") {
        $("#btnrad_yes").attr("checked", true);
        isPareto = true;

    }
    else {
        isPareto = false;
        $("#btnrad_no").attr("checked", true);
    }
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


function updateAccount() {
    $.ajax({
        type: "POST", url: baseUrl + "MasterFile/UpdateGTAccount",
        data:
		    "_acctCode=" + txt_acctCode.attr("value") + "&" +
		    "_Pareto=" + isPareto + "&" +
		    "",
        success: function (res) {
            alert("Successfully Updated!");
            updateTableList();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function updateTableList() {
    $('#tbl_list tr[clone="true"]').each(
        function (index, elem) {
            var curr_row = $(elem);
            var val_pareto = isPareto == "true" ? "Pareto" : "Non-Pareto";
            if (curr_row.find("td:nth-child(2)").find("a").html() == txt_acctCode.attr("value")) {

                curr_row.find("td:nth-child(6)").find("span").html(val_pareto);
                return false;
            }
        });
}


function handleClick(myRadioButton) {
    isPareto = myRadioButton.value;
}
var btn_menu = null;
var img_con = null;
var isHide = true;

$(document).ready(function () {
    try {
        window.parent.HideMenuFrame();
        window.parent.HideTopFrame();
    }
    catch (err) {
    }
});


$(function () {
    btn_menu = $("#btn_menu");
    img_con = $("#img_con");
    btn_menu.click(function () {
        try {
            if (isHide) {
                window.parent.ShowMenuFrame();
                window.parent.ShowTopFrame();
                img_con.attr("src", baseUrl + "Images/resultset_previous.png");
                isHide = false;
            }
            else {
                window.parent.HideMenuFrame();
                window.parent.HideTopFrame();
                img_con.attr("src", baseUrl + "Images/resultset_next.png");
                isHide = true;
            }

            $(window).trigger("resize");
        }
        catch (err) {
            window.location = baseUrl + "SecurePage/FramedSetted";
        }
    });
});
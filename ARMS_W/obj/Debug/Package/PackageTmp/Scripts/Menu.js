$(function () {

    $(".menu .menu_title").click(
        function () {
            if ($(this).parent().find(".sub_menu_holder:visible").is(':visible') == false && $(this).parent().find("span").text() != "Notifications") {
                $(".menu .sub_menu_holder .menu_1 .sub_menu_holder_1").hide("fast");
                $(".menu .sub_menu_holder").hide("fast");
                $(this).parent().find(".sub_menu_holder").show("fast");
            }
        }
    );

    $(".menu_1 .menu_title_1").click(
        function () {
            if ($(this).parent().find(".sub_menu_holder_1:visible").is(':visible') == false) {
                $(".menu .sub_menu_holder .menu_2 .sub_menu_holder_2").hide("fast");
                $(".menu_1 .sub_menu_holder_1").hide("fast");
                $(this).parent().find(".sub_menu_holder_1").show("fast");
            }
        }
    );

    $(".menu_2 .menu_title_2").click(
        function () {
            if ($(this).parent().find(".sub_menu_holder_2:visible").is(':visible') == false) {
                $(".menu_1 .sub_menu_holder_1 .menu_3 .sub_menu_holder_3").hide("fast"); //---------      
                $(".menu_2 .sub_menu_holder_2").hide("fast");
                $(this).parent().find(".sub_menu_holder_2").show("fast");
            }
        }
    );


});

function close_all_sub_menu() {
    $("#menu_group_1 .sub_menu_group_1_holder").hide('fast');
    $("#menu_group_2 .sub_menu_group_2_holder").hide('fast');
}

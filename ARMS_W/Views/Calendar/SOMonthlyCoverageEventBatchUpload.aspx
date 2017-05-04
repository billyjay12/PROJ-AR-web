<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {
            getSOAccount();
        });

        $(function () {
            $("#lnk_upload_excel").unbind();
            $("#lnk_upload_excel").uploadlink2(
                baseUrl + "Calendar/UploadExcelDataCoveragePlan_Monthly?EventMonth=" + "12" + "&empIdNo=" + "392" + "&EventYear=" + "1",
                "txt_FileAttachment",
                "TESTING",
                function (counterid) {
                    // window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
                    // ShowFramePreviewUpload();
                    displayUploaded(counterid);

                    counter_id_upload = counterid;
                }
             );
        });

        function getSOAccount() {
            $.ajax({
                type: "POST",
                url: baseUrl + "Calendar/GetSoList",
                //data: { soid: soid },
                success: function (res) {
                    $("#soAccount").html(res);
                    //alert(res);
                }
            });
        }

        function displayUploaded(counterid) {
            window.location = baseUrl + 'Calendar/MyCalendar?soId=100821&month=11&year=2016';

            var calendar_date1 = $("#calendar").fullCalendar('getDate');

            var new_obj = { counterid: counterid, eventMonth: calendar_date1.getMonth(), eventYear: calendar_date1.getFullYear() };
            $.ajax({
                dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
                type: 'POST', url: baseUrl + 'Calendar/getTemporaryUploadData_Monthly',
                success: function (res) {
                    uploaded_data = res.data.list;

                    btn_SaveAndSend.hide();

                    $("#doc_stat_msg").html('Upload Coverage Preview MODE');
                    calendar.fullCalendar('destroy');
                    calendar.fullCalendar({
                        header: {
                            left: 'title',
                            right: 'month'
                        },
                        year: calendar_date1.getFullYear(),
                        month: calendar_date1.getMonth(),
                        //  height: 820,
                        events: res.data.list,
                        disableDragging: false,
                        eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {
                            var day_all_events = null;
                            var revert = false;
                            var data = new Array();

                            day_all_events = calendar.fullCalendar('clientEvents');
                            $(day_all_events).each(function (i, e) {
                                if (e.start.toString() == event.start.toString() && e.title.toString() == event.title.toString())
                                    data.push(e);
                            });
                            if (data.length > 1) {
                                revert = true;
                            }
                            if (revert == true) {
                                alert("Account already exist!..");
                                revertFunc();
                            }
                            else if (!confirm(event.title + " was moved " + dayDelta + " days. Are you sure about this change?")) {
                                revertFunc();
                            }
                        },
                        eventClick: function (calEvent, jsEvent, view) {
                            $('input.auto').autoNumeric();

                            $("#txt_date").text(calEvent.start);
                            $("#txt_acct_code").attr("value", calEvent.account_code);
                            $("#txt_acct_name").attr("value", calEvent.title);
                            $("#txt_cntct_person").attr("value", calEvent.contact_person);
                            $("#txt_cntct_person_no").attr("value", calEvent.contact_person_no);
                            $("#txt_hotel_name").attr("value", calEvent.hotel_name);
                            $("#txt_hotel_num").attr("value", calEvent.hotel_num);
                            $("#txt_storechecking").attr("value", calEvent.store_checking);
                            $("#txt_issue_concern").attr("value", calEvent.issues_and_concerns);

                            var obj_code_holder = "";
                            $("#tbl_objectives tr[clone=\"true\"]").remove();

                            $("#collection").removeClass("tabcolor");
                            $("#sales").removeClass("tabcolor");
                            $("#merchandise").removeClass("tabcolor");
                            $("#customerservice").removeClass("tabcolor");

                            $("#tbl_collection_dtls tr[class=\"addedRow\"]").remove();
                            $("#tbl_sales_dtls tr[class=\"addedRow\"]").remove();
                            $("#tbl_mse_details tr[class=\"addedRow\"]").remove();
                            $("#customerservice tr[class=\"addedRow\"]").remove();

                            if (calEvent.store_checking != "" && calEvent.store_checking != null)
                                $("#merchandise").addClass("tabcolor");
                            if (calEvent.issues_and_concerns != "" && calEvent.issues_and_concerns != null)
                                $("#customerservice").addClass("tabcolor");

                            $(calEvent.list_objectives).each(function (i, e) {
                                if (e.objective_code == "C") {
                                    $("#collection").addClass("tabcolor");
                                    $("#tbl_collection_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                  '<td><input type="text"  value="' + e.planned_amount + '"/></td>' +
                                                  '</tr>');
                                    $("#tbl_collection_dtls").attr("objectivecode", e.objective_code);
                                }
                                else if (e.objective_code == "S") {
                                    $("#sales").addClass("tabcolor");
                                    $("#tbl_sales_dtls .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.brand + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.planned_amount + '"/></td>' +
                                                  '</tr>');
                                }
                                else if (e.objective_code == "M") {
                                    $("#merchandise").addClass("tabcolor");
                                    $("#tbl_mse_details .last_row").before('<tr clone="true" class="addedRow"><td class="hiddenTd"><input type="text" readonly="readonly" value="' + e.objective_code + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.product_presented + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.counter_clerk + '"/></td>' +
                                                  '<td><input type="text" readonly="readonly" value="' + e.counter_clerk_no + '"/></td>' +
                                                  '</tr>');
                                }
                                else if (e.objective_code == "CS") {
                                    $("#customerservice").addClass("tabcolor");
                                }

                                obj_code_holder = e.objective_code;
                            });

                            lookUp_function();
                            addButton_function();

                            $(".hiddenTd").hide();
                            display_dialogbox(calEvent);
                        }
                    });
                    $("#tbl_footer").hide();
                    $("#grp_upload_buttons").show();
                    $(".fc-header-center").html("<h2>[UPLOAD PREVIEW MODE]</h2>").css("color", "red");

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status); alert(thrownError);
                }
            });
        }


       

//        $("#lnk_upload_excel").uploadlink2(
//                baseUrl + "Calendar/UploadExcelDataCoveragePlan_Monthly?EventMonth=" + vw_monthholder + "&empIdNo=" + soId + "&EventYear=" + vw_yearholder,
//                "txt_FileAttachment",
//                "TESTING",
//                function (res) {
//                    // window.location = baseUrl + "Calendar/UploadCoveragePreview?counter_id=" + res + "&event_year=" + Eventyear + "&event_day=" + Eventday + "&event_month=" + Eventmonth + "&soId=" + soId + "&Eventdate=" + Eventdate;
//                    // ShowFramePreviewUpload();
//                    displayUploaded(res);

//                    counter_id_upload = res;
//                });
//                /* end */


    </script>
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <b>SO Coverage Upload/Download</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %>
                    - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    <div class="content" style="margin: 5px">
     <div>Please secure a &nbsp<a href="<%:ResolveUrl("~/") %>Template/monthly coverage template.xlsx" id="lnk_excel_template" class="cls_excel_file">Excel template</a> for reference.</div>
    
     <div>Choose SO Account: <select id="soAccount"></select></div>
     <div><input type="file" name="file" id="txtAttachment" /><button id="upload">Upload Data From Excel</button></div>
     
     <div><a href="javascript:;" id="lnk_upload_excel">Upload Data From Excel</a></div>
    </div>
</asp:Content>

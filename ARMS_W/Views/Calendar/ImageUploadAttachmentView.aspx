<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=ResolveUrl("~/") %>Plugins/DataTables-1.10.13/media/css/jquery.dataTables.css" rel="stylesheet"
        type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Plugins/DataTables-1.10.13/media/js/jquery.dataTables.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        getAttachmentData();
        //getdata();
    });

    function getdata() {
        $.ajax({
            type: "POST",
            url: baseUrl + "Calendar/GetDate",
            data: { linenum: "SEC019210" },
            success: function (res) {
                $("#image").attr("src", "data:"+res);
            }
        });
    }

    function getAttachmentData() {
        var TableVideoTitleMaster = $('#tableAttachment').DataTable({
            "bServerSide": true,
            "sAjaxSource": baseUrl + "Calendar/GetAjaxData",
            "aoColumnDefs": [
            //{ "sClass": "hide_me", "aTargets": [0] },
            {"aaData": "eventid" },
            { "aaData": "accountcodename" },
            { "aaData": "PO" },
            //{ "aaData": "selfie" },
            {"aaData": "selfie" },
            //             { "render": function (data, type, row, meta) {
            //                 return '<a src="' + data + '"/>';
            //             }
            //             },
            //                                        { "render": function (data, type, row, meta) {
            //                                            return '<img src="' + data + '"/>';
            //                                        }
            //                                        },
            {"aaData": "warehouse" },
            //                {"render": function (data, type, row, meta) {
            //                    return '<img src="' + data + '"/>';
            //                }
            //            },
            {"aaData": "competition" }
            //                { "render": function (data, type, row, meta) {
            //                    return '<img src="' + data + '"/>';
            //                }
                ]
        });
    }
</script>
<image id="image"></image>
    <table id="tableAttachment">
        <thead>
            <tr>
                <th>Eventid</th>
                <th>accountcodename</th>
                <th>PO</th>
                <th>Selfie</th>
                <th>Warehouse</th>
                <th>Competition</th>
            </tr>
        </thead>
    </table>
</asp:Content>
